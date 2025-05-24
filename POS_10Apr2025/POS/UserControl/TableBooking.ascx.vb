
Imports Telerik.Web.UI
Imports System.Data
Imports Telerik.Web.UI.Calendar

Partial Class UserControl_TableBooking
    Inherits System.Web.UI.UserControl

    Dim oClsDataccess As New ClsDataccess

    Public ReadOnly Property CurrentTabID() As Int32
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("TabId")) Then
                Return Utils.ParseInt(Utils.Decrypt(Request.QueryString("TabId")))
            Else
                Return 0
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            'Common.BindVenue(ddlVenue)
            Common.BindTableStoreDropdown(ddlVenue, CurrentTabID, False)
            dtpDate.SelectedDate = DateTime.Now
            BindType()
            'ddlVenue.Items.Insert(0, New DropDownListItem("All", "0"))


            ''''dtpTime.SelectedTime = TimeSpan.Parse(DateTime.Now.ToString("HH") + ":00")

            'Common.BindVenue(ddlVenue)
            'ddlVenue.Items.Insert(0, New DropDownListItem("All", "0"))
            'ddlVenue.SelectedValue = "0"
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim conn As DBConnection = New DBConnection()
        Dim venue As String = Utils.Encrypt(ddlVenue.SelectedItem.Value)
        Dim bDate As String = Utils.Encrypt(dtpDate.SelectedDate.ToString())
        Dim bTime As String = Utils.Encrypt(ddlType.SelectedItem.Value)

        Dim noOfCovers As String
        'If (ddlNoOfCovers.SelectedValue = "-1") Then
        noOfCovers = Utils.Encrypt(txtNoOfCovers.Text).ToString()
        'Else
        'noOfCovers = Utils.Encrypt(ddlNoOfCovers.SelectedItem.Value).ToString()
        'End If
        Session("IsPostback") = Nothing

        Dim str As String = "select One_booking_at_a_time,MinCovers from BookingSchedule where BookingScheduleID = '" + ddlType.SelectedItem.Value.ToString() + "'"
        Dim ds As DataSet = conn.SelectData(str)
        If (ds.Tables(0).Rows.Count > 0) Then
            If ds.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" And Not ds.Tables(0).Rows(0)("MinCovers").ToString() = 0 Then
                Dim min As Integer = ds.Tables(0).Rows(0)("MinCovers").ToString()
                If txtNoOfCovers.Text < min Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Minimum " + min.ToString() + " Covers Required.');", True)
                Else
                    Response.Redirect("SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers & "&TabId=" & CurrentTabID)
                End If
            Else
                Response.Redirect("SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers & "&TabId=" & CurrentTabID)
            End If
        Else
            Response.Redirect("SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers & "&TabId=" & CurrentTabID)
        End If



    End Sub

    'Protected Sub ddlNoOfCovers_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlNoOfCovers.SelectedIndexChanged
    '    If (ddlNoOfCovers.SelectedValue = "-1") Then
    '        txtNoOfCovers.Visible = True
    '        rfvNoOfCovers.Enabled = True
    '        regNoOfCovers.Enabled = True
    '    Else
    '        txtNoOfCovers.Visible = False
    '        rfvNoOfCovers.Enabled = False
    '        regNoOfCovers.Enabled = False
    '    End If
    'End Sub
    Private Sub BindType()
        ddlType.Items.Clear()
        If ddlVenue.Items.Count > 0 Then
            Dim obj As Common = New Common()
            Dim ds As DataSet = obj.GetTableType(ddlVenue.SelectedValue, dtpDate.SelectedDate)
            If ds IsNot Nothing Then
                If ds.Tables.Count > 0 Then
                    ddlType.DataSource = ds.Tables(0)
                    ddlType.DataTextField = "Name"
                    ddlType.DataValueField = "Id"
                    ddlType.DataBind()
                    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                    oColSqlparram.Add("@BookingSettingsID", Val(ddlVenue.SelectedValue), SqlDbType.Int)
                    Dim dtLive As DataTable = oClsDataccess.GetdataTableSp("Get_Current_Schedule", oColSqlparram)
                    If dtLive.Rows.Count > 0 Then
                        ddlType.SelectedValue = dtLive.Rows(0)("BookingScheduleID").ToString()
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub ddlVenue_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlVenue.SelectedIndexChanged
        BindType()
    End Sub

    Protected Sub dtpDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles dtpDate.SelectedDateChanged
        BindType()
    End Sub
End Class

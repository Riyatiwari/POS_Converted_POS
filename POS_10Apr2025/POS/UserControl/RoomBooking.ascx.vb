
Imports Telerik.Web.UI
Imports System.Data
Imports Telerik.Web.UI.Calendar

Partial Class UserControl_RoomBooking
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
        Try
            If (Not IsPostBack) Then
                Common.BindRoomStoreDropdown(ddlVenue, CurrentTabID)
                'Common.BindVenue(ddlVenue)
                'ddlVenue.Items.Insert(0, New DropDownListItem("All", "0"))
                'ddlVenue.SelectedValue = "0"
                dtpArrival.SelectedDate = DateTime.Now
                dtpDeparture.SelectedDate = DateTime.Now.AddDays(1)
                BindType()
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub BindType()
        Try
            ddlType.Items.Clear()
            If ddlVenue.Items.Count > 0 Then
                Dim obj As Common = New Common()
                Dim ds As DataSet = obj.GetTableType(ddlVenue.SelectedValue, dtpArrival.SelectedDate)
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
        Catch ex As Exception

        End Try      
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If ddlType.Items.Count = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Type not found');", True)
            Else
                Dim venue As String = Utils.Encrypt(ddlVenue.SelectedItem.Value)
                Dim ADate As String = Utils.Encrypt(dtpArrival.SelectedDate)
                Dim DDate As String = Utils.Encrypt(dtpDeparture.SelectedDate)
                Dim btype As String = Utils.Encrypt(ddlType.SelectedItem.Value)
                Response.Redirect("SearchResult.aspx?venue=" + venue + "&arrival=" & ADate & "&departure=" & DDate & "&TabId=" & CurrentTabID & "&TypeId=" & btype)
            End If
        Catch ex As Exception

        End Try 
    End Sub

    Protected Sub dtpArrival_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dtpArrival.SelectedDateChanged
        Try
            dtpDeparture.SelectedDate = Convert.ToDateTime(dtpArrival.SelectedDate).AddDays(1)
            BindType()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlVenue_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlVenue.SelectedIndexChanged
        Try
            BindType()
        Catch ex As Exception

        End Try
    End Sub
End Class

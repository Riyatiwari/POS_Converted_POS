Imports Telerik.Web.UI
Imports System.Data
Imports Telerik.Web.UI.Calendar
Imports System.Globalization

Partial Class UserControl_ucRoomWidget
    Inherits System.Web.UI.UserControl
    Dim conn As DBConnection = New DBConnection()

    Dim oClsDataccess As New ClsDataccess

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            Session.RemoveAll()
            Sessions.Login = 2
            Common.BindRoomStoreDropdownForWidget(ddlVenue)
            dtpArrival.SelectedDate = DateTime.Today.ToShortDateString
            dtpDeparture.SelectedDate = DateTime.Today.AddDays(1)
            BindType()
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If ddlType.Items.Count = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Type not found');", True)
        Else
            Dim venue As String = Utils.Encrypt(ddlVenue.SelectedItem.Value)
            Dim ADate As String = Utils.Encrypt(dtpArrival.SelectedDate)
            Dim DDate As String = Utils.Encrypt(dtpDeparture.SelectedDate)
            Dim btype As String = Utils.Encrypt(ddlType.SelectedItem.Value)
            Dim serverURL As String = ConfigurationManager.AppSettings("ServerURL")
            Dim redirectURL As String = serverURL & "/BookingEasy/SearchResult.aspx?venue=" + venue + "&arrival=" & ADate & "&departure=" & DDate & "&TabId=0&TypeId=" & btype & "&IsOnline=1"
            Me.Page.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "myUniqueKey", "self.parent.location='" & redirectURL & "';", True)
        End If
    End Sub

    Private Sub BindType()
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
    End Sub

    Protected Sub ddlVenue_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlVenue.SelectedIndexChanged
        BindType()
    End Sub

    Protected Sub lnkDateChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDateChange.Click
        BindType()
    End Sub

    Protected Sub dtpArrival_SelectedDateChanged(sender As Object, e As System.EventArgs) Handles dtpArrival.SelectedDateChanged
        dtpDeparture.SelectedDate = Convert.ToDateTime(dtpArrival.SelectedDate).AddDays(1)
        BindType()
    End Sub
End Class

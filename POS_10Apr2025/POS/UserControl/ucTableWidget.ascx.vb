
Imports Telerik.Web.UI
Imports System.Data
Imports Telerik.Web.UI.Calendar
Imports System.Data.SqlClient

Partial Class UserControl_ucTableWidget
    Inherits System.Web.UI.UserControl
    Dim conn As DBConnection = New DBConnection()

    Dim oClsDataccess As New ClsDataccess
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)

    Dim con As SqlConnection = New SqlConnection()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If (Not IsPostBack) Then

                CheckStore()
                Session.RemoveAll()
                Sessions.Login = 2
                'BindTableStoreDropdown(ddlVenue)
                'dtpDate.SelectedDate = DateTime.Today.ToShortDateString
                'BindType()
                'BindMaxCovers()
                'BindVenueAddress()

                BindTableStoreDropdown(ddlVenue, con)

                dtpDate.SelectedDate = DateTime.Today.ToShortDateString

                BindType(con)
                BindMaxCovers(con)
                BindVenueAddress(con)

                Session("WidType") = "Table"
            End If
        Catch ex As Exception
            LogHelper.Error("UserControl_ucTableWidget:Page_Load" + ex.Message)
        End Try

    End Sub

    Public Sub CheckStore()
        Try

            strcon.Open()
            Dim n As String = Request.QueryString("s_name").ToString()  'Session("store").ToString()

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("password") = Decode_data(dt.Rows(0)("db_password"))
                Session("db_name") = dt.Rows(0)("db_name")
                'Session("ConnectionString") = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Session("password") + ";"

                con = New SqlConnection("Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password"))

            End If
            strcon.Close()
        Catch ex As Exception
            strcon.Close()
            LogHelper.Error("Till:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub

    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, ByVal c As SqlConnection, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            'Using connection As New SqlConnection(c)
            c.Open()
            com.CommandText = SPName
            com.CommandType = CommandType.StoredProcedure
            com.Connection = c
            com.Parameters.Clear()
            com.CommandTimeout = 0

            For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                Dim parameter As New SqlClient.SqlParameter
                parameter.ParameterName = oClsSqlParameter.ParaName
                parameter.SqlDbType = oClsSqlParameter.ParaType
                parameter.Value = oClsSqlParameter.ParaValue
                parameter.Direction = oClsSqlParameter.ParaDirection
                com.Parameters.Add(parameter)
            Next

            SpAdepter = New SqlDataAdapter(com)
            sdr = New Data.DataTable
            SpAdepter.Fill(sdr)
            If strTableName <> "" Then sdr.TableName = strTableName

        Catch ex As Exception : Throw ex
            LogHelper.Error("ClsDataccess:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function

    Private Function GetTableType(ByVal settingId As Int32, ByVal bookingDate As DateTime, c As SqlConnection) As DataSet
        Dim strQuery As String = String.Empty
        'strQuery = " SELECT BookingScheduleID AS Id,Name FROM BookingSchedule WHERE BookingSettingsID = " & settingId
        strQuery &= " DECLARE @BookingSettingsID INT"
        strQuery &= " SET @BookingSettingsID = " & settingId
        strQuery &= " DECLARE @BookingDate DATETIME"
        strQuery &= " SET @BookingDate = '" & bookingDate.ToString("yyyy-MM-dd") & "'"
        strQuery &= " SELECT DISTINCT BS.BookingScheduleID AS Id,BS.Name FROM BookingSchedule BS"
        strQuery &= " INNER JOIN BookingScheduleDate BSD ON BS.BookingScheduleID = BSD.BookingScheduleId"
        strQuery &= " WHERE BS.BookingSettingsID = @BookingSettingsID AND (@BookingDate BETWEEN BS.StartDate AND BS.EndDate)"
        strQuery &= " AND BSD.ScheduleDate = @BookingDate AND BSD.IsAvailable = 1"
        Return SelectData(strQuery, c)
    End Function

    Public Shared Sub BindTableStoreDropdown(ByRef ddl As DropDownList, a As SqlConnection)

        Dim que As String = ""
        que &= " SELECT BS.BookingSettingsid,S.StoreName AS Name FROM bookingsettings BS left outer join StoreMaster S ON BS.StoreID = S.OurStoreId WHERE BS.IsActive = 1 AND BS.IsShowOnWidget = 1 AND (BS.bookingtype = '" & Utils.Encrypt("2") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        Dim ds As DataSet = SelectData(que, a)
        ddl.DataSource = ds.Tables(0)
        ddl.DataValueField = "BookingSettingsid"
        ddl.DataTextField = "Name"
        ddl.DataBind()
        ddl.ClearSelection()
    End Sub

    Private Shared Function SelectData(que As String, a As SqlConnection) As DataSet
        Try
            Dim ad As New SqlDataAdapter(que, a)
            Dim ds As New DataSet()
            ad.Fill(ds)
            Return ds
        Catch ex As Exception
            LogHelper.Error("DBConnection:SelectData" + ex.Message)
        End Try
    End Function

    Public Function Encode_data(ByVal str As String) As String
        Try
            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            CheckStore()
            Dim venue As String = Utils.Encrypt(ddlVenue.SelectedItem.Value)
            Dim bDate As String = Utils.Encrypt(dtpDate.SelectedDate)
            Dim bTime As String = Utils.Encrypt(ddlType.SelectedItem.Value)

            Dim noOfCovers As String

            noOfCovers = Utils.Encrypt(ddlNoOfCovers.SelectedItem.Value).ToString()
            Dim serverURL As String = ConfigurationManager.AppSettings("ServerURL")
            'Response.Redirect(serverURL & "/BookingEasy/SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers)

            Dim str As String = "select One_booking_at_a_time,MinCovers from BookingSchedule where BookingScheduleID = '" + ddlType.SelectedValue.ToString() + "'"
            Dim ds As DataSet = SelectData(str, con)
            If ds IsNot Nothing Then
                If (ds.Tables(0).Rows.Count > 0) Then
                    If ds.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" And Not ds.Tables(0).Rows(0)("MinCovers").ToString() = 0 Then
                        Dim min As Integer = ds.Tables(0).Rows(0)("MinCovers").ToString()
                        If ddlNoOfCovers.SelectedItem.Value < min Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Minimum " + min.ToString() + " Covers Required. please call at " + lblVenueName.Text + " for your booking');", True)
                        Else
                            Dim redirectURL As String = serverURL & "/Booking/SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers & "&IsOnline=1"
                            Me.Page.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "myUniqueKey", "self.parent.location='" & redirectURL & "';", True)
                        End If
                    Else
                        Dim redirectURL As String = serverURL & "/Booking/SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers & "&IsOnline=1"
                        Me.Page.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "myUniqueKey", "self.parent.location='" & redirectURL & "';", True)
                    End If
                Else
                    Dim redirectURL As String = serverURL & "/Booking/SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers & "&IsOnline=1"
                    Me.Page.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "myUniqueKey", "self.parent.location='" & redirectURL & "';", True)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub BindType(ByVal c As SqlConnection)
        Try
            ddlType.Items.Clear()
            If ddlVenue.Items.Count > 0 Then
                'Dim obj As Common = New Common()
                Dim ds1 As DataSet = GetTableType(ddlVenue.SelectedValue, Utils.ParseDateTime(dtpDate.SelectedDate), c)
                If ds1 IsNot Nothing Then
                    If ds1.Tables.Count > 0 Then
                        ddlType.DataSource = ds1.Tables(0)
                        ddlType.DataTextField = "Name"
                        ddlType.DataValueField = "Id"
                        ddlType.DataBind()
                        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                        oColSqlparram.Add("@BookingSettingsID", Val(ddlVenue.SelectedValue), SqlDbType.Int)
                        Dim dtLive As DataTable = GetdataTableSp("Get_Current_Schedule", oColSqlparram, con)
                        If dtLive.Rows.Count > 0 Then
                            ddlType.SelectedValue = dtLive.Rows(0)("BookingScheduleID").ToString()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlVenue_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlVenue.SelectedIndexChanged
        Try
            CheckStore()
            BindType(con)
            BindMaxCovers(con)
            BindVenueAddress(con)
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkDateChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDateChange.Click
        Try
            CheckStore()
            BindType(con)
            BindMaxCovers(con)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindMaxCovers(ByVal c As SqlConnection)
        Try
            ddlNoOfCovers.Items.Clear()

            Dim drSchedule As DataRow = GetOnlineMaxCover(Utils.ParseInt(ddlType.SelectedValue), c)
            If drSchedule IsNot Nothing Then
                Dim maxCover As Int32 = Utils.ParseInt(drSchedule("OnlineMaxCovers"))

                For index = 1 To maxCover
                    ddlNoOfCovers.Items.Add(New ListItem(index.ToString(), index.ToString()))
                Next
                btnMaxCover.Text = "More Then " & maxCover.ToString() & " People"
                lblMaxCover.Text = maxCover.ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Function GetOnlineMaxCover(ByVal bookingScheduleId As Int32, ByVal c As SqlConnection) As DataRow
        Dim strQuery As String = String.Empty
        strQuery = " SELECT * FROM BookingSchedule WHERE BookingScheduleID = " & bookingScheduleId
        Dim ds As DataSet = SelectData(strQuery, c)
        If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Protected Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try
            CheckStore()
            BindMaxCovers(con)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindVenueAddress(ByVal c As SqlConnection)
        Try
            Dim dr As DataRow = GetVenueAddressDetail(Utils.ParseInt(ddlVenue.SelectedValue), c)
            lblVenueName.Text = ddlVenue.SelectedItem.Text
            If dr IsNot Nothing Then
                lblNumber.Text = dr("PhoneHome")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function GetVenueAddressDetail(ByVal bookingSettingId As Int32, ByVal c As SqlConnection) As DataRow
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT A.* "
        strQuery &= " FROM bookingsettings BS INNER JOIN Store S ON BS.StoreID = S.StoreID"
        strQuery &= " INNER JOIN Venue V ON S.VenueID = V.VenueID"
        strQuery &= " INNER JOIN Address A ON V.AddressID = A.AddressID"
        strQuery &= " WHERE BS.BookingSettingsid = " & bookingSettingId

        Dim ds As DataSet = SelectData(strQuery, c)
        If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)
        Else
            Return Nothing
        End If
    End Function


End Class

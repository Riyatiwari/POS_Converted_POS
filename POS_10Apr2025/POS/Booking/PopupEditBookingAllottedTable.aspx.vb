Imports System.Data

Partial Class PopupEditBookingAllottedTable
    Inherits System.Web.UI.Page

    'Public Property AccountId() As String
    '    Get
    '        If (Not Request.QueryString("AccountId") Is Nothing) Then
    '            Return Request.QueryString("AccountId")
    '        End If
    '        Return ""
    '    End Get
    '    Set(ByVal value As String)
    '        Request.QueryString("AccountId") = value
    '    End Set
    'End Property
    'Public Property ReffId() As String
    '    Get
    '        If (Not Request.QueryString("ReffId") Is Nothing) Then
    '            Return Request.QueryString("ReffId")
    '        End If
    '        Return ""
    '    End Get
    '    Set(ByVal value As String)
    '        Request.QueryString("ReffId") = value
    '    End Set
    'End Property
    'Public Property StoreId() As String
    '    Get
    '        If (Not Request.QueryString("StoreId") Is Nothing) Then
    '            Return Request.QueryString("StoreId")
    '        End If
    '        Return ""
    '    End Get
    '    Set(ByVal value As String)
    '        Request.QueryString("StoreId") = value
    '    End Set
    'End Property
    Public Property bookingref() As String
        Get
            If (Not Request.QueryString("bookingref") Is Nothing) Then
                Return Request.QueryString("bookingref")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            Request.QueryString("bookingref") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                ChechBooking()
            Catch ex As Exception

            End Try
            'BindRoomType()
            'If ddlRoomType.Items.Count > 0 Then
            '    BindRoomName()
            'Else
            '    lblNoRooms.Text = "No Booking Available at this time"
            'End If
        End If
    End Sub

    Private Sub ChechBooking()
        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData("select 1 from bookings where bookingref = '" + bookingref() + "' and IsSeated = 0")
        'Dim ds As DataSet = conn.SelectData("select 1 as flag from bookings b INNER JOIN BookingSchedule bs ON bs.BookingScheduleID = b.BookingScheduleID where b.bookingref = '" + bookingref() + "' AND CONVERT(VARCHAR(16),CONVERT(VARCHAR(10),b.date,120)+' ' + convert(nvarchar(5),DATEADD(MINUTE,bs.ServiceDuration,b.bookingtime),108)) >= getdate() AND CONVERT(VARCHAR(10),b.date,120) >= CONVERT(VARCHAR(10),getdate(),120) AND b.IsCanceled = 0")
        'Dim ds As DataSet = conn.SelectData("select 1 as flag from bookings b where b.bookingref = '" + bookingref() + "' AND CONVERT(VARCHAR(10),b.date,120) >= CONVERT(VARCHAR(10),getdate(),120) AND (CONVERT(VARCHAR(15),GETDATE(),108) < Convert(varchar(5), DATEADD(minute,(select ServiceDuration from bookingschedule where BookingScheduleID = B.BookingScheduleID),b.bookingtime), 108))")
        If ds.Tables(0).Rows.Count > 0 Then
            divChangeTable.Visible = True
            divErrorBinding.Visible = False
            Bind()
            'Dim ds3 As DataSet = conn.SelectData("select 1 from tables where TableID = (SELECT TableID from M_OpenTable WHERE BookingRef = '" + bookingref() + "')")
            'If ds3.Tables(0).Rows.Count = 0 Then
            '    divChangeTable.Visible = True
            '    divErrorBinding.Visible = False
            '    Bind()
            'Else
            '    Dim ds1 As DataSet = conn.SelectData("select 1 from bookings b inner join M_OpenTable ot ON ot.bookingref = b.bookingref Inner join Tables t on t.TableID = ot.TableID where b.bookingref = '" + bookingref() + "' and Closed <> 1")
            '    If ds1.Tables(0).Rows.Count > 0 Then
            '        divChangeTable.Visible = False
            '        divErrorBinding.Visible = True
            '    Else
            '        Dim ds2 As DataSet = conn.SelectData("select 1 from tables where TableID = (SELECT TableID from M_OpenTable WHERE BookingRef = '" + bookingref() + "') and closed = 1 and Datetimeclosed IS NULL")
            '        If ds2.Tables(0).Rows.Count > 0 Then
            '            divChangeTable.Visible = False
            '            divErrorBinding.Visible = True
            '        Else
            '            divChangeTable.Visible = True
            '            divErrorBinding.Visible = False
            '            Bind()
            '        End If
            '    End If
            'End If
        Else
            divChangeTable.Visible = False
            divErrorBinding.Visible = True
        End If
    End Sub

    Private Sub Bind()
        Try
            Dim que As String = "  "
            que += " SELECT SM.OurStoreId,SM.StoreName AS Location, B.bookingid,B.accountid, BSc.GroupID, B.covers,"
            que += " A.FirstName+' '+A.LastName AS FullName, B.covers, convert(nvarchar(10),B.date,110) AS arrivaldate, B.bookingref, B.bookingtime, BSc.Name AS ScheduleName,convert(nvarchar(5),bookingtime,108) as time "
            que += " FROM bookings B INNER JOIN Account A ON B.accountid = A.accountid left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId  "
            que += " INNER JOIN BookingSchedule BSc ON BSc.BookingScheduleID = b.BookingScheduleID "
            que += " WHERE B.bookingref = '" + bookingref() + "'"
            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)
            If ds.Tables(0).Rows.Count > 0 Then
                lblBookingRef.Text = ds.Tables(0).Rows(0)("bookingref").ToString()
                lblCovers.Text = ds.Tables(0).Rows(0)("covers").ToString()
                lblDate.Text = ds.Tables(0).Rows(0)("arrivaldate").ToString()
                lblTime1.Text = ds.Tables(0).Rows(0)("time").ToString()
                lblLocation.Text = ds.Tables(0).Rows(0)("Location").ToString()
                lblSchedule.Text = ds.Tables(0).Rows(0)("ScheduleName").ToString()
                BindTableForGroup(ds.Tables(0).Rows(0)("GroupID").ToString(), ds.Tables(0).Rows(0)("bookingref").ToString())
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub BindTableForGroup(ByVal GroupId As Integer, ByVal bookingref As String)
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNamesByGroupID(GroupId, bookingref)
            If ds.Tables.Count > 0 Then
                ddlTableSet.DataSource = ds
                ViewState("ds") = ds
                ddlTableSet.DataTextField = "table_name"
                ddlTableSet.DataValueField = "table_id"
                ddlTableSet.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlTableSet_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlTableSet.SelectedIndexChanged
        Try
            If Not ddlTableSet.SelectedValue Is Nothing Then
                Dim conn As DBConnection = New DBConnection()
                Dim objCommon As Common = New Common()
                Dim tablejoin As String = objCommon.GetSelectedValue(ddlTableSet)
                If Not tablejoin Is Nothing Then
                    Dim ds As DataSet = conn.SelectData("SELECT SUM(MaxCover) as MaxCover FROM M_Table b where Table_id in (select items from dbo.Split(('" + tablejoin.ToString() + "'),('#')))")
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows(0)("MaxCover") >= lblCovers.Text Then
                            btnConfitmBooking.OnClientClick = "return confirm('Are you sure you want to Change this Booking ?');"
                        Else
                            btnConfitmBooking.OnClientClick = "return confirm('Are you sure you want to Change this Booking when Maxcover of Selected tables are less then Booking Covers ?');"
                        End If
                        lblMaxCoverl.Visible = True
                        lblMaxCover.Visible = True
                        lblMaxCover.Text = ds.Tables(0).Rows(0)("MaxCover")
                    Else
                        btnConfitmBooking.OnClientClick = "return confirm('Are you sure you want to Update this Record ?');"
                        lblMaxCoverl.Visible = False
                        lblMaxCover.Visible = False
                        lblMaxCover.Text = 0
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnConfitmBooking_Click(sender As Object, e As System.EventArgs) Handles btnConfitmBooking.Click

        If ddlTableSet.SelectedValue Is Nothing Then
            lblError.Visible = True
            lblError.Text = "Please Select Tables From Available Tables"
        Else
            Try
                Dim que As String = ""
                Dim conn As DBConnection = New DBConnection()
                Dim objCommon As Common = New Common()
                Dim tablejoin As String = objCommon.GetSelectedValue(ddlTableSet)

                Dim vals As String() = tablejoin.ToString().Split("#")

                Dim largest As Integer = Integer.MinValue
                Dim smallest As Integer = Integer.MaxValue

                For Each element As Integer In vals
                    largest = Math.Max(largest, element)
                    smallest = Math.Min(smallest, element)
                Next

                Console.WriteLine(largest)
                Console.WriteLine(smallest)
                Dim oDbConnection As New DBConnection()
                Dim query As DataSet = oDbConnection.SelectData("SELECT TableNo FROM M_Table where Table_id = " + smallest.ToString() + "")
                que = "Update Bookings Set Allotted_Tables='" & tablejoin.ToString() & "', TableNo='" & query.Tables(0).Rows(0)("TableNo").ToString() & "' WHERE bookingref = '" & bookingref() & "'"
                conn.ExecuteNonQuery(que)
                que = "if exists (select 1 from M_OpenTable where bookingref = '" & bookingref() & "') begin update Tables set TableNumber = " + query.Tables(0).Rows(0)("TableNo").ToString() + " where TableID = (select tableid from M_OpenTable where bookingref = '" & bookingref() & "') end"
                conn.ExecuteNonQuery(que)
                Bind()
                lblSuccess.Text = "Booking Updated Successfully"
                Session("msgbox_Val") = "Update"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload1", "window.parent.location.reload();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "window.parent.location.reload(false);", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
            Catch ex As Exception

            End Try
        End If

    End Sub

    'Private Sub BindRoomType()

    '    Dim que As String = ""
    '    que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='StoreRoomTypes')DROP TABLE StoreRoomTypes"
    '    que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='bookedrooms')DROP TABLE bookedrooms"
    '    que &= " CREATE TABLE bookedrooms (roomid int null, bookingid int null, date smalldatetime null,storeid int null)"
    '    que &= " INSERT INTO bookedrooms (roomid, bookingid, date,storeid)"
    '    que &= " SELECT B.RoomID,B.bookingid,B.date,S.OurStoreId"
    '    que &= " FROM Bookings B"
    '    que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
    '    que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
    '    que &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
    '    que &= " AND date = '" & Convert.ToDateTime(DateTime.Now).ToString("s") & "'"
    '    que &= " AND IsCanceled = 0 AND S.OurStoreId = '" & StoreId & "'"

    '    que &= " CREATE TABLE StoreRoomTypes(roomtypeid int,storeid int)"
    '    que &= " INSERT INTO StoreRoomTypes(roomtypeid,storeid)"
    '    que &= " SELECT DISTINCT P.ParentID,SM.OurStoreId"
    '    que &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID AND P.IsParent = 0 AND P.INACTIVE = 0"
    '    que &= " INNER JOIN Store S ON PS.StoreID = S.StoreID"
    '    que &= " left outer join StoreMaster SM ON SM.OtherStoreId = S.StoreID"
    '    que &= " INNER JOIN BookingSettings BS ON BS.StoreID = SM.OurStoreId"
    '    que &= " WHERE BS.IsActive = 1 AND P.ProdSort = BS.Sort AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
    '    que &= " AND NOT EXISTS (SELECT 1 FROM bookedrooms BR WHERE BR.roomid = PS.ProductID AND BR.storeid = SM.OurStoreId) AND SM.OurStoreId = '" & StoreId & "'"

    '    que &= " SELECT DISTINCT P.ProductID as RoomTypeID,P.Name as RoomType,cast(Price1Eachsize_1 *.01 as Decimal (10,2))as price, SM.StoreName StoreName,SM.OurStoreId AS StoreId"
    '    que &= " FROM StoreRoomTypes SRT INNER JOIN Product P ON SRT.roomtypeid = P.ProductID"
    '    que &= " left outer join StoreMaster SM ON SRT.storeid = SM.OurStoreId"
    '    que &= " INNER JOIN Store S ON SM.OtherStoreId = S.StoreID"
    '    que &= " INNER JOIN ProdStore PS ON SRT.roomtypeid = PS.ProductID AND SRT.StoreID = SM.OurStoreId"
    '    que &= " DROP TABLE StoreRoomTypes"
    '    que &= " drop table bookedrooms"

    '    Dim conn As DBConnection = New DBConnection()
    '    Dim ds As DataSet = conn.SelectData(que)

    '    'If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
    '    ddlRoomType.DataSource = ds.Tables(0)
    '    ddlRoomType.DataTextField = "RoomType"
    '    ddlRoomType.DataValueField = "RoomTypeID"
    '    ddlRoomType.DataBind()
    '    'End If
    'End Sub

    'Private Sub BindRoomName()

    '    Dim que As String = ""
    '    que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='StoreRooms')DROP TABLE StoreRooms"
    '    que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='bookedrooms')DROP TABLE bookedrooms"

    '    que &= " CREATE TABLE bookedrooms (roomid int null, bookingid int null, date smalldatetime null,storeid int null)"
    '    que &= " INSERT INTO bookedrooms (roomid, bookingid, date,storeid)"
    '    que &= " SELECT B.RoomID,B.bookingid,B.date,S.OurStoreId"
    '    que &= " FROM Bookings B"
    '    que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
    '    que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
    '    que &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
    '    que &= " AND CONVERT(VARCHAR(10),date,120) = CONVERT(VARCHAR(10),'" & Convert.ToDateTime(DateTime.Now).ToString("s") & "',120)"
    '    que &= " AND IsCanceled = 0 "

    '    que &= " CREATE TABLE StoreRooms(roomid int,roomname varchar(100))"
    '    que &= " INSERT INTO StoreRooms(roomid,roomname)"
    '    que &= " SELECT DISTINCT P.ProductId,P.Name"
    '    que &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID AND P.ParentID = '" & ddlRoomType.SelectedItem.Value & "' AND P.INACTIVE = 0"
    '    que &= " INNER JOIN Store S ON PS.StoreID = S.StoreID"
    '    que &= " left outer join StoreMaster SM ON SM.OtherStoreId = S.StoreID"
    '    que &= " INNER JOIN BookingSettings BS ON BS.StoreID = SM.OurStoreId"
    '    que &= " WHERE SM.OurStoreId = '" & StoreId & "' AND P.ProdSort = BS.Sort AND BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
    '    que &= " AND NOT EXISTS (SELECT 1 FROM bookedrooms BR WHERE BR.roomid = PS.ProductID AND BR.storeid = SM.OurStoreId)"

    '    que &= " SELECT DISTINCT roomid,roomname"
    '    que &= " FROM StoreRooms"

    '    que &= " DROP TABLE StoreRooms"
    '    que &= " drop table bookedrooms"

    '    Dim conn As DBConnection = New DBConnection()
    '    Dim ds As DataSet = conn.SelectData(que)

    '    ddlRoomName.DataSource = ds.Tables(0)
    '    ddlRoomName.DataTextField = "roomname"
    '    ddlRoomName.DataValueField = "roomid"
    '    ddlRoomName.DataBind()
    'End Sub

    'Protected Sub btnConfitmBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfitmBooking.Click
    '    Try
    '        If ddlRoomName.SelectedValue > 0 Then
    '            Dim que As String = ""
    '            Dim conn As DBConnection = New DBConnection()
    '            que = "Update Bookings Set Roomid='" & ddlRoomName.SelectedItem.Value & "' WHERE Bookingid = '" & bookingid() & "'"
    '            conn.ExecuteNonQuery(que)
    '            divNoRooms.Visible = True
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
    '        Else
    '            lblNoRooms.Text = "Please Select Room"
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Protected Sub ddlRoomType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlRoomType.SelectedIndexChanged
    '    BindRoomName()
    'End Sub

    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Dim conn As DBConnection = New DBConnection()
        conn.ExecuteNonQuery("UPDATE Bookings SET IsCanceled = 1 WHERE bookingref = '" + bookingref().ToString() + "'")
        conn.ExecuteNonQuery("DELETE FROM Tables WHERE TableID = (SELECT TableID from M_OpenTable WHERE BookingRef = '" + bookingref().ToString() + "')")
        conn.ExecuteNonQuery("DELETE FROM M_OpenTable where bookingref = '" + bookingref().ToString() + "')")
        ChechBooking()
        Session("msgbox_Val") = "Cancel"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload2", "window.parent.location.reload();", False)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "window.parent.location.reload(false);", True)
    End Sub
End Class

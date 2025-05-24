Imports System.Data

Partial Class BookingEasy_PopupEditBookingRoom
    Inherits System.Web.UI.Page

    Public Property AccountId() As String
        Get
            If (Not Request.QueryString("AccountId") Is Nothing) Then
                Return Request.QueryString("AccountId")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            Request.QueryString("AccountId") = value
        End Set
    End Property
    Public Property ReffId() As String
        Get
            If (Not Request.QueryString("ReffId") Is Nothing) Then
                Return Request.QueryString("ReffId")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            Request.QueryString("ReffId") = value
        End Set
    End Property
    Public Property StoreId() As String
        Get
            If (Not Request.QueryString("StoreId") Is Nothing) Then
                Return Request.QueryString("StoreId")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            Request.QueryString("StoreId") = value
        End Set
    End Property
    Public Property bookingid() As String
        Get
            If (Not Request.QueryString("bookingid") Is Nothing) Then
                Return Request.QueryString("bookingid")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            Request.QueryString("bookingid") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Bind()
            BindRoomType()
            If ddlRoomType.Items.Count > 0 Then
                BindRoomName()
            Else
                lblNoRooms.Text = "No Booking Available at this time"
            End If
        End If
    End Sub

    Private Sub Bind()
        Dim que As String = " SELECT bookingref,convert(varchar(12),convert(date,arrivaldate,103),103) as arrivaldate FROM Bookings WHERE bookingid = '" + bookingid() + "' "
        Dim conn As DBConnection = New DBConnection()
        Dim ds = conn.SelectData(que)
        If ds.Tables(0).Rows.Count > 0 Then
            lblBookingRef.Text = ds.Tables(0).Rows(0)("bookingref").ToString()           
            lblDate.Text = ds.Tables(0).Rows(0)("arrivaldate").ToString()
        End If
    End Sub

    Private Sub BindRoomType()

        Dim que As String = ""
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='StoreRoomTypes')DROP TABLE StoreRoomTypes"
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='bookedrooms')DROP TABLE bookedrooms"
        que &= " CREATE TABLE bookedrooms (roomid int null, bookingid int null, date smalldatetime null,storeid int null)"
        que &= " INSERT INTO bookedrooms (roomid, bookingid, date,storeid)"
        que &= " SELECT B.RoomID,B.bookingid,B.date,S.OurStoreId"
        que &= " FROM Bookings B"
        que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
        que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
        que &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        que &= " AND date = '" & Convert.ToDateTime(DateTime.Now).ToString("s") & "'"
        que &= " AND IsCanceled = 0 AND S.OurStoreId = '" & StoreId & "'"

        que &= " CREATE TABLE StoreRoomTypes(roomtypeid int,storeid int)"
        que &= " INSERT INTO StoreRoomTypes(roomtypeid,storeid)"
        que &= " SELECT DISTINCT P.ParentID,SM.OurStoreId"
        que &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID AND P.IsParent = 0 AND P.INACTIVE = 0"
        que &= " INNER JOIN Store S ON PS.StoreID = S.StoreID"
        que &= " left outer join StoreMaster SM ON SM.OtherStoreId = S.StoreID"
        que &= " INNER JOIN BookingSettings BS ON BS.StoreID = SM.OurStoreId"
        que &= " WHERE BS.IsActive = 1 AND P.ProdSort = BS.Sort AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        que &= " AND NOT EXISTS (SELECT 1 FROM bookedrooms BR WHERE BR.roomid = PS.ProductID AND BR.storeid = SM.OurStoreId) AND SM.OurStoreId = '" & StoreId & "'"

        que &= " SELECT DISTINCT P.ProductID as RoomTypeID,P.Name as RoomType,cast(Price1Eachsize_1 *.01 as Decimal (10,2))as price, SM.StoreName StoreName,SM.OurStoreId AS StoreId"
        que &= " FROM StoreRoomTypes SRT INNER JOIN Product P ON SRT.roomtypeid = P.ProductID"
        que &= " left outer join StoreMaster SM ON SRT.storeid = SM.OurStoreId"
        que &= " INNER JOIN Store S ON SM.OtherStoreId = S.StoreID"
        que &= " INNER JOIN ProdStore PS ON SRT.roomtypeid = PS.ProductID AND SRT.StoreID = SM.OurStoreId"
        que &= " DROP TABLE StoreRoomTypes"
        que &= " drop table bookedrooms"

        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData(que)

        'If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
        ddlRoomType.DataSource = ds.Tables(0)
        ddlRoomType.DataTextField = "RoomType"
        ddlRoomType.DataValueField = "RoomTypeID"
        ddlRoomType.DataBind()
        'End If
    End Sub

    Private Sub BindRoomName()

        Dim que As String = ""
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='StoreRooms')DROP TABLE StoreRooms"
        que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='bookedrooms')DROP TABLE bookedrooms"

        que &= " CREATE TABLE bookedrooms (roomid int null, bookingid int null, date smalldatetime null,storeid int null)"
        que &= " INSERT INTO bookedrooms (roomid, bookingid, date,storeid)"
        que &= " SELECT B.RoomID,B.bookingid,B.date,S.OurStoreId"
        que &= " FROM Bookings B"
        que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
        que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
        que &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        que &= " AND CONVERT(VARCHAR(10),date,120) = CONVERT(VARCHAR(10),'" & Convert.ToDateTime(DateTime.Now).ToString("s") & "',120)"
        que &= " AND IsCanceled = 0 "

        que &= " CREATE TABLE StoreRooms(roomid int,roomname varchar(100))"
        que &= " INSERT INTO StoreRooms(roomid,roomname)"
        que &= " SELECT DISTINCT P.ProductId,P.Name"
        que &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID AND P.ParentID = '" & ddlRoomType.SelectedItem.Value & "' AND P.INACTIVE = 0"
        que &= " INNER JOIN Store S ON PS.StoreID = S.StoreID"
        que &= " left outer join StoreMaster SM ON SM.OtherStoreId = S.StoreID"
        que &= " INNER JOIN BookingSettings BS ON BS.StoreID = SM.OurStoreId"
        que &= " WHERE SM.OurStoreId = '" & StoreId & "' AND P.ProdSort = BS.Sort AND BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')"
        que &= " AND NOT EXISTS (SELECT 1 FROM bookedrooms BR WHERE BR.roomid = PS.ProductID AND BR.storeid = SM.OurStoreId)"

        que &= " SELECT DISTINCT roomid,roomname"
        que &= " FROM StoreRooms"

        que &= " DROP TABLE StoreRooms"
        que &= " drop table bookedrooms"

        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData(que)

        ddlRoomName.DataSource = ds.Tables(0)
        ddlRoomName.DataTextField = "roomname"
        ddlRoomName.DataValueField = "roomid"
        ddlRoomName.DataBind()
    End Sub

    Protected Sub btnConfitmBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfitmBooking.Click
        Try
            If ddlRoomName.SelectedValue > 0 Then
                Dim que As String = ""
                Dim conn As DBConnection = New DBConnection()
                que = "Update Bookings Set Roomid='" & ddlRoomName.SelectedItem.Value & "' WHERE Bookingid = '" & bookingid() & "'"
                conn.ExecuteNonQuery(que)
                divNoRooms.Visible = True
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
            Else
                lblNoRooms.Text = "Please Select Room"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlRoomType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlRoomType.SelectedIndexChanged
        BindRoomName()
    End Sub
End Class

Imports System.Data
Imports Telerik.Web.UI

Partial Class Searching
    Inherits System.Web.UI.Page

    '    Dim conn As DBConnection = New DBConnection()

    Public Property AccountId() As String
        Get
            If (Not ViewState("AccountId") Is Nothing) Then
                Return ViewState("AccountId").ToString()
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("AccountId") = value
        End Set
    End Property
    Public Property ReffId() As String
        Get
            If (Not ViewState("ReffId") Is Nothing) Then
                Return ViewState("ReffId").ToString()
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("ReffId") = value
        End Set
    End Property
    Public Property StoreId() As String
        Get
            If (Not ViewState("StoreId") Is Nothing) Then
                Return ViewState("StoreId").ToString()
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("StoreId") = value
        End Set
    End Property
    Public Property ArrDate() As String
        Get
            If (Not ViewState("ArrDate") Is Nothing) Then
                Return ViewState("ArrDate").ToString()
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("ArrDate") = value
        End Set
    End Property
    Public Property DepDate() As String
        Get
            If (Not ViewState("DepDate") Is Nothing) Then
                Return ViewState("DepDate").ToString()
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("DepDate") = value
        End Set
    End Property

    Public Property BookingSettingsid() As String
        Get
            If (Not ViewState("BookingSettingsid") Is Nothing) Then
                Return ViewState("BookingSettingsid").ToString()
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("BookingSettingsid") = value
        End Set
    End Property

    Public Property BookingService() As DataTable
        Get
            Dim conn As DBConnection = New DBConnection()
            If (ViewState("BookingService") IsNot Nothing) Then
                Return DirectCast(ViewState("BookingService"), DataTable)
            End If
            Dim dt As DataTable = conn.SelectData("select * from BookingServices where BookingServiceID = -1").Tables(0)
            dt.Columns("BookingServiceID").AutoIncrement = True
            'dt.Columns.Add("ProductName")
            ViewState("BookingService") = dt
            Return dt
        End Get
        Set(ByVal value As DataTable)
            ViewState("BookingService") = value
        End Set
    End Property
    Public ReadOnly Property CurrencySymbol() As String
        Get
            Dim ds As DataSet = Common.GetXmlData()
            If Not ds.Tables("Currency") Is Nothing Then
                Return DirectCast(ds.Tables("Currency").Rows(0)("Symbol").ToString(), String)
            Else
                Return String.Empty
            End If
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("AllowSetting") = Nothing
            If (Not Page.IsPostBack()) Then

                If (Request.QueryString("search") IsNot Nothing And Request.QueryString("for") IsNot Nothing) Then

                    txtSearch.Text = Request.QueryString("search")
                    If (Request.QueryString("for") = "customer") Then
                        rdoCustomer.Checked = True
                        rdoBooking.Checked = False
                    Else
                        rdoCustomer.Checked = False
                        rdoBooking.Checked = True
                    End If

                    Search()
                    BindSearch()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindSearch()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetDefaultField()
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("DefaultField") = 0 Then
                    lblSearchOption.Text = "(Search By Email)"
                ElseIf ds.Tables(0).Rows(0)("DefaultField") = 1 Then
                    lblSearchOption.Text = "(Search By Mobile)"
                ElseIf ds.Tables(0).Rows(0)("DefaultField") = 2 Then
                    lblSearchOption.Text = "(Search By Email or Mobile)"
                End If
            Else
                lblSearchOption.Text = "(Search By Email)"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Search()
    End Sub

    Private Sub Search()
        If (rdoCustomer.Checked) Then
            SearchClient()
        Else
            SearchBooking()
        End If
    End Sub

    Private Sub SearchClient()
        Try
            Dim SearchOption As Integer = 0
            Dim objCommon As Common = New Common()
            Dim dsField As DataSet = objCommon.GetDefaultField()
            If dsField.Tables(0).Rows.Count > 0 Then
                SearchOption = dsField.Tables(0).Rows(0)("DefaultField")
            Else
                SearchOption = 0
            End If

            Dim ds As DataSet = Common.SearchCustomer(txtSearch.Text.Trim(), SearchOption)

            gvCustomers.DataSource = ds.Tables(0)
            gvCustomers.DataBind()
            If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                gvCustomers.Visible = True
            Else
                gvCustomers.Visible = False
                gvBookings.Visible = False
                gvBookingDetails.Visible = False
                divBookingDetails.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SearchBooking()
        Try

            Dim que As String = " SELECT DISTINCT B.comment as comment,B.bookingid,B.AccountID, B.BookingRef,(CASE ISNULL(B.Period,0) WHEN 0 THEN B.arrivaldate ELSE B.date END) AS Arrivaldate,"
            que &= " (CASE ISNULL(B.Period,0) WHEN 0 THEN B.departuredate ELSE B.date END) AS Departuredate,"
            que &= " (CASE ISNULL(B.Period,0) WHEN 1 THEN 'Restaurent' ELSE 'Hotel' END)AS Period,S.StoreName AS Name, S.OurStoreId AS StoreId,SS.VenueID,B.AccountID,B.BookingSettingsID"
            que &= " FROM Bookings B "
            que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
            que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
            que &= " INNER JOIN Store SS ON SS.StoreID = S.OtherStoreId"
            que &= " WHERE B.BookingRef = '" & txtSearch.Text & "' ORDER BY (CASE ISNULL(B.Period,0) WHEN 0 THEN B.arrivaldate ELSE B.date END)"

            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)

            gvBookings.DataSource = ds.Tables(0)
            gvBookings.DataBind()

            If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                gvBookings.Visible = True
                BindClient(Utils.getInteger(ds.Tables(0).Rows(0)("AccountID")))
            Else
                gvCustomers.Visible = False
                gvBookings.Visible = False
                gvBookingDetails.Visible = False
                divBookingDetails.Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub



    Private Sub BindClient(ByVal accId As Integer)
        Try
            Dim conn As Common = New Common()
            Dim dt As DataTable = conn.GetAccountByIdDt(accId)

            gvCustomers.DataSource = dt
            gvCustomers.DataBind()
            If (dt IsNot Nothing Or dt.Rows.Count) Then
                gvCustomers.Visible = True
            Else
                gvCustomers.Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvCustomers_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvCustomers.ItemCommand
        Try
            If (e.CommandName = "RowClick") Then
                AccountId = TryCast(e.Item, GridDataItem).GetDataKeyValue("accountid").ToString()
                imgbtn1.Visible = True
                BindBookinGrid()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BindBookinGrid()
        Try
            Dim que As String = ""
            que &= " SELECT DISTINCT B.comment as comment,B.bookingid,B.BookingRef,(CASE ISNULL(B.Period,0) WHEN 0 THEN B.arrivaldate ELSE B.date END) AS Arrivaldate,"
            que &= " (CASE ISNULL(B.Period,0) WHEN 0 THEN B.departuredate ELSE B.date END) AS Departuredate,"
            que &= " (CASE ISNULL(B.Period,0) WHEN 1 THEN 'Restaurent' ELSE 'Hotel' END)AS Period,S.StoreName as Name, S.OurStoreId as StoreId,SS.VenueID,B.AccountID,B.BookingSettingsID"
            que &= " FROM Bookings B "
            que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
            que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
            que &= " INNER JOIN Store SS ON SS.StoreID = S.OtherStoreId"
            que &= " WHERE B.AccountID = '" & AccountId & "' AND B.IsCanceled <> 1 ORDER BY (CASE ISNULL(B.Period,0) WHEN 0 THEN B.arrivaldate ELSE B.date END)"

            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)

            gvBookings.DataSource = ds.Tables(0)
            gvBookings.DataBind()

            If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                gvBookings.Visible = True
            Else
                gvBookings.Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvBookings_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvBookings.ItemCommand
        Try
            If (e.CommandName = "CancelBooking") Then
                Dim reff As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingRef").ToString()
                Dim conn As DBConnection = New DBConnection()
                conn.ExecuteNonQuery("UPDATE Bookings SET IsCanceled = 1 WHERE BookingRef = '" + reff + "'")
                conn.ExecuteNonQuery("DELETE FROM Tables WHERE TableID = (SELECT TableID from M_OpenTable WHERE BookingRef = '" + reff + "')")
                conn.ExecuteNonQuery("DELETE FROM M_OpenTable where bookingref = '" + reff + "')")
                BindBookinGrid()
                BindBookinDetailsGrid()

                'ElseIf (e.CommandName = "RowClick") Then
                '    ReffId = TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingRef").ToString()
                '    StoreId = TryCast(e.Item, GridDataItem).GetDataKeyValue("StoreId").ToString()
                '    ArrDate = TryCast(e.Item, GridDataItem).GetDataKeyValue("Arrivaldate").ToString()
                '    DepDate = TryCast(e.Item, GridDataItem).GetDataKeyValue("Departuredate").ToString()
                '    BindBookinGrid()
                '    BindBookinDetailsGrid()
                '    If TryCast(e.Item, GridDataItem).GetDataKeyValue("Period").ToString() = "Hotel" Then
                '        BindRoomType()
                '    End If

                '    If (rdoBooking.Checked) Then
                '        gvBookings.Visible = False
                '    Else
                '        gvBookings.Visible = True
                '    End If

            ElseIf (e.CommandName = "RowClick") Then

                ReffId = TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingRef").ToString()
                StoreId = TryCast(e.Item, GridDataItem).GetDataKeyValue("StoreId").ToString()
                ArrDate = TryCast(e.Item, GridDataItem).GetDataKeyValue("Arrivaldate").ToString()
                DepDate = TryCast(e.Item, GridDataItem).GetDataKeyValue("Departuredate").ToString()
                If TryCast(e.Item, GridDataItem).GetDataKeyValue("Period").ToString() = "Hotel" Then
                    BindBookinDetailsGrid()
                    gvBookingDetails.Visible = True
                    gvBookingDetailsTable.DataSource = Nothing
                    gvBookingDetailsTable.DataBind()
                    gvBookingDetailsTable.Visible = False
                Else
                    BindBookinDetailsGridTable()
                    gvBookingDetailsTable.Visible = True
                    gvBookingDetails.DataSource = Nothing
                    gvBookingDetails.DataBind()
                    gvBookingDetails.Visible = False
                End If
                'BindBookinGrid()
                BindServices()
                'If TryCast(e.Item, GridDataItem).GetDataKeyValue("Period").ToString() = "Hotel" Then
                '    BindRoomType()
                'End If

                'If (rdoBooking.Checked) Then
                '    gvBookings.Visible = False
                'Else
                '    gvBookings.Visible = True
                'End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BindBookinDetailsGridTable()
        Try
            Dim strQuery As String = ""

            strQuery = " SELECT TOP 1 B.*,S.StoreName AS Name,A.FirstName,A.LastName,AD.Mobile,AD.Email1st,S.OurStoreId AS StoreID,A.AccountID,convert(nvarchar(10),B.Date,103) as date1"
            strQuery &= " FROM Bookings B"
            strQuery &= " INNER JOIN BookingSettings BS ON B.BookingSettingsid = BS.BookingSettingsid"
            strQuery &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
            strQuery &= " INNER JOIN Account A ON B.AccountID = A.AccountID"
            strQuery &= " INNER JOIN [Address] AD ON A.AddressId = AD.AddressID"
            strQuery &= " WHERE BookingRef = '" & ReffId & "' AND B.IsCanceled <> 1"

            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(strQuery)
            ViewState("BookingSettingsid") = ds.Tables(0).Rows(0)("BookingSettingsid")
            gvBookingDetailsTable.DataSource = ds.Tables(0)
            gvBookingDetailsTable.DataBind()
            If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                divBookingDetails.Visible = True
                If ds.Tables(0).Rows(0)("IsSeated") = 0 Then
                    Dim value As String = ds.Tables(0).Rows(0)("date").ToString()
                    If Convert.ToDateTime(value) < DateTime.Today Then
                        aUpdateBooking.Visible = False
                        btnSaveServices.Visible = False
                    Else
                        aUpdateBooking.Visible = True
                        btnSaveServices.Visible = True
                    End If
                    aUpdateBooking.Visible = True
                    aUpdateBooking.HRef = "PopupEditBookingTable.aspx?bookingref=" + ds.Tables(0).Rows(0)("bookingref").ToString() + "&customerid=" + ds.Tables(0).Rows(0)("accountid").ToString() + "&venueid=" + ds.Tables(0).Rows(0)("StoreId").ToString() + "&bookingsettingid=" + ds.Tables(0).Rows(0)("BookingSettingsid").ToString() + ""
                    aUpdateBooking.InnerText = "Click here to Update booking for " + ReffId() + ""
                    gvServices.Visible = True
                    btnSaveServices.Visible = True
                Else
                    aUpdateBooking.Visible = False
                    btnSaveServices.Visible = False
                    gvServices.Visible = True
                End If
            Else
                divBookingDetails.Visible = False
                ViewState("BookingSettingsid") = Nothing
                aUpdateBooking.HRef = ""
                aUpdateBooking.InnerText = ""
                btnSaveServices.Visible = False
                gvServices.Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BindServices()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetProducts(StoreId)
            gvServices.DataSource = ds.Tables(0)
            gvServices.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvServices_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvServices.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                If Not CType(dataItem("Action").FindControl("hfProductID"), HiddenField).Value = Nothing Then
                    Dim objCommon As Common = New Common()
                    Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(ReffId())
                    For Each roleRow As DataRow In dsServices.Tables(0).Rows
                        Dim product As Integer = CType(e.Item.FindControl("hfProductID"), HiddenField).Value
                        If roleRow("ProductId") = product Then
                            CType(e.Item.FindControl("chkSelect"), CheckBox).Checked = True
                            CType(e.Item.FindControl("ddlQuantity"), RadDropDownList).SelectedValue = roleRow("Quantity")
                        End If
                    Next
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BindBookinDetailsGrid()
        Try
            Dim que As String = " SELECT b.bookingid, b.bookingref, b.date, P.Name, PT.Name AS RoomType , b.BookingSettingsid FROM Bookings B "
            que &= " INNER JOIN Product P ON P.ProductId = B.Roomid"
            que &= " INNER JOIN Product PT ON PT.ProductId = P.ParentId"
            que &= " WHERE B.BookingRef = '" & ReffId & "' AND B.IsCanceled <> 1"

            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)
            ViewState("BookingSettingsid") = ds.Tables(0).Rows(0)("BookingSettingsid")
            gvBookingDetails.DataSource = ds.Tables(0)
            gvBookingDetails.DataBind()
            If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                divBookingDetails.Visible = True
                Dim value As String = ds.Tables(0).Rows(0)("date").ToString()
                If Convert.ToDateTime(value) < DateTime.Today Then
                    aUpdateBooking.Visible = False
                    btnSaveServices.Visible = False
                Else
                    aUpdateBooking.Visible = True
                    btnSaveServices.Visible = True
                End If

                aUpdateBooking.HRef = "PopupEditBooking.aspx?bookingref=" + ReffId() + "&customerid=" + AccountId() + "&venueid=" + StoreId() + "&bookingsettingid=" + BookingSettingsid() + ""
                aUpdateBooking.InnerText = "Click here to Update booking for " + ReffId() + ""
            Else
                divBookingDetails.Visible = False
                ViewState("BookingSettingsid") = Nothing
                aUpdateBooking.HRef = ""
                aUpdateBooking.InnerText = ""
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DeleteOldBooking()
        Try
            Dim conn As DBConnection = New DBConnection()

            Dim query As String = String.Empty
            query &= " DELETE FROM BookingServices WHERE bookingref = '" & ReffId() & "'"

            conn.Ins_Upd_Del(query)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSaveServices_Click(sender As Object, e As System.EventArgs) Handles btnSaveServices.Click
        Try
            DeleteOldBooking()
            If gvServices.Items.Count > 0 Then
                For Each gvItem As GridDataItem In gvServices.Items
                    Dim chkSelect As CheckBox = CType(gvItem.FindControl("chkSelect"), CheckBox)
                    If chkSelect.Checked Then
                        Dim dr As DataRow = BookingService.NewRow()
                        dr("bookingref") = String.Empty
                        dr("ProductID") = gvItem.GetDataKeyValue("ProductID").ToString()
                        Dim Qty As Integer = Utils.ParseInt(CType(gvItem.FindControl("ddlQuantity"), RadDropDownList).SelectedValue)
                        Dim UnitPrice As Decimal = Utils.ParseDecimal(CType(gvItem.FindControl("lblPrice"), Label).Text)
                        dr("Quantity") = Qty
                        dr("Price") = UnitPrice
                        dr("TotalPrice") = (Qty * UnitPrice)
                        BookingService.Rows.Add(dr)
                    End If
                Next

            End If
            If BookingService.Rows.Count > 0 Then
                For i = 0 To BookingService.Rows.Count - 1
                    BookingService.Rows(i)("bookingref") = ReffId()
                Next
                BookingService.AcceptChanges()
                Common.SaveBookingServices(BookingService)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test1", "alert('Services Updated Successfully');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('" + ex.Message + "');", True)
        End Try

    End Sub

    Protected Sub gvBookingDetailsTable_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvBookingDetailsTable.ItemCommand
        Try
            If (e.CommandName = "CancelBooking") Then
                Dim bookingId As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("bookingid").ToString()
                Dim conn As DBConnection = New DBConnection()
                conn.ExecuteNonQuery("UPDATE Bookings SET IsCanceled = 1 WHERE BookingID = " + bookingId)
                BindBookinDetailsGrid()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvBookingDetails_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvBookingDetails.ItemCommand
        Try
            If (e.CommandName = "CancelBooking") Then
                Dim bookingId As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("bookingid").ToString()
                Dim conn As DBConnection = New DBConnection()
                conn.ExecuteNonQuery("UPDATE Bookings SET IsCanceled = 1 WHERE BookingID = " + bookingId)
                BindBookinDetailsGrid()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkReloadUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReloadUser.Click
        Search()
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
    '    que &= " AND (date BETWEEN '" & Convert.ToDateTime(ArrDate).ToString("s") & "' AND '" & Convert.ToDateTime(DepDate).ToString("s") & "')"
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
    '    BindRoomName()
    '    'End If
    'End Sub

    'Protected Sub ddlRoomType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlRoomType.SelectedIndexChanged
    '    BindRoomName()
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
    '    que &= " AND (date BETWEEN '" & Convert.ToDateTime(ArrDate).ToString("s") & "' AND '" & Convert.ToDateTime(DepDate).ToString("s") & "')"
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

    '    If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
    '        btnChangeRoom.Enabled = True
    '    Else
    '        btnChangeRoom.Enabled = False
    '    End If
    'End Sub

    'Protected Sub btnChangeRoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeRoom.Click

    '    If gvBookingDetails.Items.Count > 0 Then
    '        Dim que As String = ""
    '        Dim conn As DBConnection = New DBConnection()

    '        For Each item As GridDataItem In gvBookingDetails.Items
    '            Dim chk As CheckBox = DirectCast(item.FindControl("chkSelect"), CheckBox)
    '            Dim roomid As String = item.GetDataKeyValue("bookingid").ToString()

    '            If chk.Checked = True Then
    '                que = "Update Bookings Set Roomid='" & ddlRoomName.SelectedItem.Value & "' WHERE Bookingid = '" & roomid & "'"
    '                conn.ExecuteNonQuery(que)
    '            End If
    '        Next
    '        If (que.Length > 0) Then
    '            BindBookinDetailsGrid()
    '            divMessageBox1.Visible = True
    '            divMessageBox1.Attributes.Add("class", "alert alert-success")
    '            lblMessageBox1.Text = "Your room was successfully changed."
    '        Else
    '            divMessageBox1.Visible = True
    '            divMessageBox1.Attributes.Add("class", "alert alert-danger")
    '            lblMessageBox1.Text = "Please select at least one room."
    '        End If
    '    End If

    'End Sub

    Protected Sub gvBookings_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvBookings.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                'accessing Table cell

                Dim hndType As HiddenField = DirectCast(e.Item.FindControl("hndType"), HiddenField)
                If hndType.Value = "Hotel" Then
                    DirectCast(e.Item.FindControl("lnkpopup2"), LinkButton).Visible = True
                Else
                    DirectCast(e.Item.FindControl("lnkpopup1"), LinkButton).Visible = True
                End If

                Dim value As String = DataBinder.Eval(e.Item.DataItem, "Departuredate").ToString()
                Dim arrivaldate As String = DataBinder.Eval(e.Item.DataItem, "Arrivaldate").ToString()
                Dim lnkCancelBooking As LinkButton = DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton)
                'Dim aUpdateBooking As HtmlAnchor = DirectCast(e.Item.FindControl("aUpdateBooking"), HtmlAnchor)
                'Dim aUpdateBookingTable As HtmlAnchor = DirectCast(e.Item.FindControl("aUpdateBookingTable"), HtmlAnchor)
                Dim Period As String = DataBinder.Eval(e.Item.DataItem, "Period").ToString()
                If Convert.ToDateTime(value) < DateTime.Today Then
                    lnkCancelBooking.Visible = False

                Else
                    lnkCancelBooking.Visible = True


                End If

                'If Convert.ToDateTime(arrivaldate) > DateTime.Today Then
                '    If Period = "Hotel" Then
                '        aUpdateBooking.Visible = True
                '        aUpdateBookingTable.Visible = False
                '    Else
                '        aUpdateBooking.Visible = False
                '        aUpdateBookingTable.Visible = True
                '    End If
                'Else
                '    aUpdateBooking.Visible = False
                '    aUpdateBookingTable.Visible = False
                'End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub gvBookingDetails_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvBookingDetails.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                'accessing Table cell


                Dim value As String = DataBinder.Eval(e.Item.DataItem, "date").ToString()
                Dim lnkCancelBooking As LinkButton = DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton)
                'Dim chkSelect As CheckBox = DirectCast(e.Item.FindControl("chkSelect"), CheckBox)
                If Convert.ToDateTime(value) < DateTime.Today Then
                    DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton).Visible = False
                    DirectCast(e.Item.FindControl("aUpdateBooking"), HtmlAnchor).Visible = False
                Else
                    DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton).Visible = True
                    DirectCast(e.Item.FindControl("aUpdateBooking"), HtmlAnchor).Visible = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvBookingDetailsTable_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvBookingDetailsTable.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                'accessing Table cell


                Dim value As String = DataBinder.Eval(e.Item.DataItem, "date").ToString()
                'Dim lnkCancelBooking As LinkButton = DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton)
                'Dim chkSelect As CheckBox = DirectCast(e.Item.FindControl("chkSelect"), CheckBox)
                Dim hdnIsSeated As HiddenField = DirectCast(e.Item.FindControl("hdnIsSeated"), HiddenField)
                If Convert.ToDateTime(value) < DateTime.Today Then
                    DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton).Visible = False
                    DirectCast(e.Item.FindControl("aUpdateBooking"), HtmlAnchor).Visible = False
                    aUpdateBooking.Visible = False
                Else
                    If hdnIsSeated.Value <> 1 Then
                        DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton).Visible = True
                        Dim hdnbookingref As HiddenField = DirectCast(e.Item.FindControl("hdnbookingref"), HiddenField)
                        Dim hdncustomerid As HiddenField = DirectCast(e.Item.FindControl("hdncustomerid"), HiddenField)
                        Dim hdnBookingSettingsid As HiddenField = DirectCast(e.Item.FindControl("hdnBookingSettingsid"), HiddenField)
                        Dim Str As String = ""

                        If Not hdnbookingref Is Nothing And Not hdncustomerid Is Nothing And Not hdnBookingSettingsid Is Nothing Then
                            Str = "PopupEditBookingTable.aspx?bookingref=" + hdnbookingref.Value.ToString + ""
                        End If
                        DirectCast(e.Item.FindControl("aUpdateBooking"), HtmlAnchor).HRef = Str.ToString()
                        DirectCast(e.Item.FindControl("aUpdateBooking"), HtmlAnchor).Visible = True
                        aUpdateBooking.Visible = True
                    Else
                        DirectCast(e.Item.FindControl("lnkCancelBooking"), LinkButton).Visible = False
                        DirectCast(e.Item.FindControl("aUpdateBooking"), HtmlAnchor).Visible = False
                        aUpdateBooking.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub gvBookings_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvBookings.NeedDataSource
        Try
            Dim que As String = ""
            que &= " SELECT DISTINCT B.comment as comment,B.bookingid,B.BookingRef,(CASE ISNULL(B.Period,0) WHEN 0 THEN B.arrivaldate ELSE B.date END) AS Arrivaldate,"
            que &= " (CASE ISNULL(B.Period,0) WHEN 0 THEN B.departuredate ELSE B.date END) AS Departuredate,"
            que &= " (CASE ISNULL(B.Period,0) WHEN 1 THEN 'Restaurent' ELSE 'Hotel' END)AS Period,S.StoreName as Name, S.OurStoreId as StoreId,SS.VenueID,B.AccountID,B.BookingSettingsID"
            que &= " FROM Bookings B "
            que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID"
            que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId"
            que &= " INNER JOIN Store SS ON SS.StoreID = S.OtherStoreId"
            que &= " WHERE B.AccountID = '" & AccountId & "' AND B.IsCanceled <> 1 ORDER BY (CASE ISNULL(B.Period,0) WHEN 0 THEN B.arrivaldate ELSE B.date END)"

            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)

            gvBookings.DataSource = ds.Tables(0)
            gvBookings.DataBind()

            If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                gvBookings.Visible = True
            Else
                gvBookings.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtn1_Click(sender As Object, e As System.EventArgs) Handles imgbtn1.Click

        gvBookings.MasterTableView.GetColumn("Print").Exportable = False
        gvBookings.MasterTableView.GetColumn("No").Display = True
        gvBookings.AllowFilteringByColumn = False
        gvBookings.Rebind()
        gvBookings.ExportSettings.FileName = "Booking Details"
        gvBookings.ExportSettings.ExportOnlyData = True
        gvBookings.ExportSettings.IgnorePaging = True
        gvBookings.ExportSettings.OpenInNewWindow = True
        gvBookings.MasterTableView.ExportToExcel()

    End Sub
End Class

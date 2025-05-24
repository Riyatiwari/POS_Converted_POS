Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar

Partial Class BookingEasy_PopupBookingConfirmEdit
    Inherits System.Web.UI.Page
    'Dim oClsDataccess As New ClsDataccess

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

    Public Property BookingRefNo() As String
        Get
            If (Not ViewState("BookingRefNo") Is Nothing) Then
                Return ViewState("BookingRefNo")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("BookingRefNo") = value
        End Set
    End Property


    Public Property CustomerID() As Int32
        Get
            If (Not ViewState("CustomerID") Is Nothing) Then
                Return ViewState("CustomerID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("CustomerID") = value
        End Set
    End Property

    Public Property VenueID() As Int32
        Get
            If (Not ViewState("VenueID") Is Nothing) Then
                Return ViewState("VenueID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("VenueID") = value
        End Set
    End Property

    Public Property StoreID() As Int32
        Get
            If (Not ViewState("StoreID") Is Nothing) Then
                Return ViewState("StoreID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("StoreID") = value
        End Set
    End Property


    Public Property BookingSettingID() As Int32
        Get
            If (Not ViewState("BookingSettingID") Is Nothing) Then
                Return ViewState("BookingSettingID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("BookingSettingID") = value
        End Set
    End Property


    Public Property ArrivalDate() As String
        Get
            If (Not ViewState("ArrivalDate") Is Nothing) Then
                Return ViewState("ArrivalDate")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("ArrivalDate") = value
        End Set
    End Property

    Public Property NoOfCover() As Int32
        Get
            If (Not ViewState("NoOfCover") Is Nothing) Then
                Return ViewState("NoOfCover")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("NoOfCover") = value
        End Set
    End Property

    Public Property BookingService() As DataTable
        Get
            If (ViewState("BookingService") IsNot Nothing) Then
                Return DirectCast(ViewState("BookingService"), DataTable)
            End If
            Dim conn As DBConnection = New DBConnection()
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

    Public Property BookingScheduleID() As Integer
        Get
            If ViewState("bookingScheduleId") IsNot Nothing Then
                Try
                    Return Utils.getInteger(ViewState("bookingScheduleId").ToString())
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Int32)
            ViewState("bookingScheduleId") = value
        End Set
    End Property

    Public ReadOnly Property bookingScheduleId2() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("bs")) Then
                Return Utils.Decrypt(Request.QueryString("bs")).ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public Property BookingScheduleDateId() As Integer
        Get
            If ViewState("bookingScheduleDateId") IsNot Nothing Then
                Try
                    Return Utils.getInteger(ViewState("bookingScheduleDateId").ToString())
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Int32)
            ViewState("bookingScheduleDateId") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oClsDataccess As New ClsDataccess
            If (Not Page.IsPostBack()) Then

                If Sessions.UserID = 0 Then
                    If Sessions.Login = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload1", "window.parent.location='../Login.aspx';", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
                    End If
                End If

                dtpDate.SelectedDate = DateTime.Today
                BindType()
                If Request.QueryString("bookingref") IsNot Nothing Then

                    Dim dt As DataTable = oClsDataccess.Getdatatable("select OurStoreId as venueid , accountid as customerid, BookingSettingsid from Booking_Initiate where bookingref = '" + Request.QueryString("bookingref").ToString() + "'")

                    If dt.Rows.Count > 0 Then
                        VenueID = Convert.ToInt32(dt.Rows(0)("venueid").ToString())
                        CustomerID = Convert.ToInt32(dt.Rows(0)("customerid").ToString())
                        BookingRefNo = Request.QueryString("bookingref")
                        BookingSettingID = Convert.ToInt32(dt.Rows(0)("BookingSettingsid").ToString())
                        BindVenueName()
                        BindType()
                        Bind()
                    Else
                        divError.Visible = True
                        lblNoBooking.Text = "Can not change settings of this Table."
                        hide()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub hide()
        Try
            pnlSearch.Visible = False
            pnlConfirm.Visible = False

            btnConfitmBooking.Visible = False

            btnChangeBooking.Visible = False

            divInfo.Visible = False

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Bind()
        Try
            Dim que As String = "  "
            que += " SELECT b.comment as comment,SM.OurStoreId,SM.StoreName AS Location, B.bookingid,B.accountid, BSc.GroupID, B.covers,"
            que += " A.FirstName+' '+A.LastName AS FullName, B.covers, convert(nvarchar(10),B.date,103) AS arrivaldate, B.bookingref, B.bookingtime, BSc.Name AS ScheduleName,B.date,convert(nvarchar(5),bookingtime,108) as time "
            que += " FROM Booking_Initiate B INNER JOIN Account A ON B.accountid = A.accountid left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId  "
            que += " INNER JOIN BookingSchedule BSc ON BSc.BookingScheduleID = b.BookingScheduleID "
            que += " WHERE B.bookingref = '" + BookingRefNo.ToString() + "'"
            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)
            If ds.Tables(0).Rows.Count > 0 Then
                lblBookingRef.Text = ds.Tables(0).Rows(0)("bookingref").ToString()
                lblCovers.Text = ds.Tables(0).Rows(0)("covers").ToString()
                lblDate.Text = ds.Tables(0).Rows(0)("arrivaldate").ToString()
                lblTime1.Text = ds.Tables(0).Rows(0)("time").ToString()
                lblLocation.Text = ds.Tables(0).Rows(0)("Location").ToString()
                lblSchedule.Text = ds.Tables(0).Rows(0)("ScheduleName").ToString()
                dtpDate.SelectedDate = ds.Tables(0).Rows(0)("date")
                txtNoOfCovers.Text = ds.Tables(0).Rows(0)("covers").ToString()
                txtComment.Text = ds.Tables(0).Rows(0)("comment").ToString()
                'lblAllottedTable.Text = ds.Tables(0).Rows(0)("Allotted_Tables").ToString()

            End If
        Catch ex As Exception

        End Try

    End Sub



    Protected Sub gvServices_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvServices.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                If Not CType(dataItem("Action").FindControl("hfProductID"), HiddenField).Value = Nothing Then
                    Dim objCommon As Common = New Common()
                    Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(lblBookingRef.Text)
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

    Private Sub BindVenueName()
        Dim conn As DBConnection = New DBConnection()

        Dim query As String = String.Empty
        'query &= " SELECT * FROM Venue WHERE VenueID = '" & VenueID & "'"
        query &= " SELECT V.StoreName as name FROM StoreMaster V Inner Join bookingsettings BS ON BS.StoreID = V.OurStoreId WHERE BS.BookingSettingsid = '" & BookingSettingID & "'"

        Dim ds As DataSet = conn.SelectData(query)
        Dim dt As DataTable = ds.Tables(0)
        lblVenue.Text = dt.Rows(0)("Name").ToString()
        lblTablesOf.Text = dt.Rows(0)("Name").ToString()
    End Sub

    Private Sub BindRepeater()

        divMessageBox.Visible = False

        Dim dt As DataTable = Common.SearchTableGetStore(Convert.ToDateTime(dtpDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"), "01:00", BookingSettingID.ToString(), ddlType.SelectedValue)
        If (dt.Rows.Count > 0) Then
            repTables.DataSource = dt
            repTables.DataBind()
            repTables.Visible = True
        Else
            repTables.Visible = False
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            lblMessageBox.Text = "No tables found under this date-time"
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindRepeater()
    End Sub

    Protected Sub repTables_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repTables.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim repTimeSlot As Repeater = DirectCast(e.Item.FindControl("repTimeSlot"), Repeater)
            Dim timeSlotId As String = DataBinder.Eval(e.Item.DataItem, "TimeSlotId").ToString()
            Dim settingsId As String = DataBinder.Eval(e.Item.DataItem, "SettingID").ToString()

            Dim noOfCovers As String
            'If (ddlNoOfCovers.SelectedValue = "-1") Then
            noOfCovers = txtNoOfCovers.Text
            'Else
            '    noOfCovers = ddlNoOfCovers.SelectedItem.Value
            'End If

            repTimeSlot.DataSource = Common.SearchTableGetTimeSlots(Convert.ToDateTime(dtpDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"), settingsId, noOfCovers, Utils.ParseInt(ddlType.SelectedValue), BookingRefNo, False)
            repTimeSlot.DataBind()
        End If
    End Sub

    Protected Sub repTimeSlot_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        Try
            If (e.CommandName = "BookTable") Then
                StoreID = Utils.ParseInt(DirectCast(source.Parent.FindControl("hdnStoreId"), HiddenField).Value)
                BookingSettingID = Utils.ParseInt(DirectCast(source.Parent.FindControl("hdnSettingId"), HiddenField).Value)
                ArrivalDate = (dtpDate.SelectedDate & " " & e.CommandArgument.ToString()).ToString()

                'If (ddlNoOfCovers.SelectedValue = "-1") Then
                NoOfCover = Utils.ParseInt(txtNoOfCovers.Text)
                'Else
                '    NoOfCover = Utils.ParseInt(ddlNoOfCovers.SelectedItem.Value)
                'End If

                hideAll()
                pnlConfirm.Visible = True
                btnConfitmBooking.Visible = True


                lblConfirmVenue.Text = lblVenue.Text
                lblBookingDate.Text = Format(dtpDate.SelectedDate, "dd/MM/yyyy") & " " & e.CommandArgument.ToString()
                lblNoOfPeople.Text = NoOfCover.ToString()
                BindProduct()

                BookingScheduleID = Utils.ParseInt(ddlType.SelectedValue)
                BookingScheduleDateId = Utils.ParseInt(Common.GetBookingScheduleDateId(dtpDate.SelectedDate, Utils.ParseInt(ddlType.SelectedValue)))
            End If
        Catch ex As Exception

        End Try
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


    Protected Sub btnConfitmBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfitmBooking.Click
        Dim objCommon As Common = New Common()
        divMessageBox.Visible = False
        divMessageBox.Attributes.Add("class", "")

        DeleteOldBooking()

        Dim drSetting As DataRow = objCommon.GetBookingSettings(StoreID)

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

        Dim serviceTotal As Decimal = 0
        If BookingService.Rows.Count > 0 Then
            serviceTotal = Utils.ParseDecimal(BookingService.Compute("Sum(TotalPrice)", ""))
        End If

        Dim roomId As String = "0"

        Dim bookingDate As DateTime = Convert.ToDateTime(ArrivalDate)

        objCommon.CreateBookingInitiate(2, Utils.ParseInt(NoOfCover), txtComment.Text, Utils.ParseInt(roomId), Utils.ParseDateTime(ArrivalDate), Nothing, 0, BookingRefNo, 0, 0,
                                                CustomerID, 1, TimeSpan.Parse(bookingDate.ToString("HH:mm")), Utils.ParseInt(drSetting("BookingSettingsid")), False, Sessions.UserID, Request.UserHostAddress, 0, BookingScheduleID, BookingScheduleDateId, False, StoreID)


        If BookingService.Rows.Count > 0 Then
            For i = 0 To BookingService.Rows.Count - 1
                BookingService.Rows(i)("bookingref") = BookingRefNo
            Next
            BookingService.AcceptChanges()
            Common.SaveBookingServices(BookingService)
        End If


        'BindVenueName()
        'BindType()
        'Bind()

        'Dim opentables As String = "UPDATE tables"
        'opentables &= " SET Datetimeopened = '" & bookingDate.ToString("s") & "',"
        'opentables &= " 	covers = '" & NoOfCover.ToString() & "'"
        'opentables &= " WHERE tableNumber = '" & BookingRefNo & "'"

        'Dim conn As DBConnection = New DBConnection()
        'conn.Ins_Upd_Del(opentables)

        'btnConfitmBooking.PostBackUrl = "Booking_Confirm.aspx?br=" + Utils.Encrypt(lblBookingRef.Text.Trim())

        btnConfitmBooking.PostBackUrl = "Booking_Confirm.aspx?br=" + Utils.Encrypt(lblBookingRef.Text.Trim()) + "Booking_Confirm.aspx?br=" + Utils.Encrypt(bookingScheduleId2())

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload1", "window.parent.location.reload();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)




    End Sub

    Private Sub DeleteOldBooking()
        Dim conn As DBConnection = New DBConnection()

        Dim query As String = String.Empty
        query &= " DELETE FROM Booking_Initiate WHERE bookingref = '" & BookingRefNo & "'"
        query &= " DELETE FROM BookingServices WHERE bookingref = '" & BookingRefNo & "'"

        conn.Ins_Upd_Del(query)
    End Sub

    Private Sub BindType()
        Dim obj As Common = New Common()
        Dim ds As DataSet = obj.GetTableType(BookingSettingID, dtpDate.SelectedDate)
        If ds IsNot Nothing Then
            If ds.Tables.Count > 0 Then
                ddlType.DataSource = ds.Tables(0)
                ddlType.DataTextField = "Name"
                ddlType.DataValueField = "Id"
                ddlType.DataBind()
            End If
        End If

    End Sub

    Public Sub BindProduct()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetProducts(StoreID)
        gvServices.DataSource = ds.Tables(0)
        gvServices.DataBind()

    End Sub

    Protected Sub dtpDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles dtpDate.SelectedDateChanged
        BindType()
    End Sub



    Protected Sub btnChangeBooking_Click(sender As Object, e As System.EventArgs) Handles btnChangeBooking.Click
        Try
            hideAll()

            pnlSearch.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub hideAll()
        Try
            pnlSearch.Visible = False
            pnlConfirm.Visible = False

            btnConfitmBooking.Visible = False

            btnChangeBooking.Visible = False

        Catch ex As Exception

        End Try
    End Sub


End Class

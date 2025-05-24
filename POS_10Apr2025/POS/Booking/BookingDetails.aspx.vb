Imports System.Data
Imports Telerik.Web.UI
Imports System.Globalization
Partial Class BookingEasy_BookingDetails
    Inherits System.Web.UI.Page

    'Dim oClsDataccess As New ClsDataccess
    Public Property ActualStoreID() As Int32
        Get
            If (Not ViewState("ActualStoreID") Is Nothing) Then
                Return ViewState("ActualStoreID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("ActualStoreID") = value
        End Set
    End Property
    Public Property IsOnline() As Boolean
        Get
            If (Not ViewState("IsOnline") Is Nothing) Then
                Return ViewState("IsOnline")
            End If
            Return False
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsOnline") = value
        End Set
    End Property
    Public Property SearchTabName() As String
        Get
            If Not ViewState("SearchTabName") Is Nothing Then
                Return DirectCast(ViewState("SearchTabName"), String)
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState("SearchTabName") = value
        End Set
    End Property

    Public Property HotelTabName() As String
        Get
            If Not ViewState("HotelTabName") Is Nothing Then
                Return DirectCast(ViewState("HotelTabName"), String)
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState("HotelTabName") = value
        End Set
    End Property

    Public Property RestaurentTabName() As String
        Get
            If Not ViewState("RestaurentTabName") Is Nothing Then
                Return DirectCast(ViewState("RestaurentTabName"), String)
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState("RestaurentTabName") = value
        End Set
    End Property

    Public Property TabID() As Int32
        Get
            If Not ViewState("TabID") Is Nothing Then
                Return DirectCast(ViewState("TabID"), Int32)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Int32)
            ViewState("TabID") = value
        End Set
    End Property

    Public ReadOnly Property CurrentTabID() As Int32
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("TabId")) Then
                Return Utils.ParseInt(Utils.Decrypt(Request.QueryString("TabId")))
            Else
                Return 0
            End If
        End Get
    End Property

    Public Property jscript() As String
        Get
            If (Not ViewState("jscript") Is Nothing) Then
                Return ViewState("jscript")
            End If
            Return 0
        End Get
        Set(ByVal value As String)
            ViewState("jscript") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'BindWaitingList()

                If Session("wl") = Nothing Then
                    divMessageBox.Visible = False
                    divMessageBox.Attributes.Add("class", "")
                    lbExistingCust.Text = ""
                End If

                If Session("success") = Nothing Then
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "")
                    lbExistingCust.Text = ""
                    Session("success") = Nothing
                End If

                If Session("msg1") = Nothing Then
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "")
                    lbExistingCust.Text = ""
                    Session("msg1") = Nothing
                End If



                Dim conn As DBConnection = New DBConnection()
                Dim str As String = conn.SingleCell("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;")
                If (str = "0") Then
                    Response.Redirect("NewSettings.aspx")
                Else
                    str = conn.SingleCell("SELECT COUNT(*) AS NoOfRecords FROM BookingSettings WHERE IsActive = 1")
                    If (str = "0") Then
                        Response.Redirect("NewSettings.aspx")
                    End If
                End If

                'If Not String.IsNullOrEmpty(Request.QueryString("TabId")) Then
                '    TabID = Utils.ParseInt(Utils.Decrypt(Request.QueryString("TabId")))
                'End If

                'vighnesh (21-04-2016) //Hide For Open Tables for Schedule Box

                Common.BindTableStoreDropdownAllNoTab(ddlVenue, False)
                BindType()
                BindStoreDropdown()
                'BindLocationType()


                'LoadTabs()
                'ValidateUserRole()

                'BindTypeRoom(ddlStore, ddlTypeRoom)
                'BindTypeRoom(ddlStoreTodaysArrival, ddlTypeTodaysArrival)
                'BindTypeRoom(ddlStoreTodaysDeparture, ddlTypeTodaysDeparture)
                'LoadHotelCalender()
                'ddlStoreTodaysArrival_SelectedIndexChanged(Nothing, Nothing)
                'ddlStoreTodaysDeparture_SelectedIndexChanged(Nothing, Nothing)
                'LoadHotels()
                'LoadRoomType()
                'LoadRooms()
                'LoadRoomCalender()
                rdpDate.SelectedDate = DateTime.Now
                Session("rdpDate") = rdpDate.SelectedDate
                'LoadRestaurentCalender()
                'LoadTabName()


                'LoadRoomBookingByDate()
                If rddlType.SelectedValue <> Nothing Then
                    If rddlType.SelectedValue <> 0 Then
                        str = conn.SingleCell("select GroupID from BookingSchedule where BookingScheduleID = " + rddlType.SelectedValue + "")
                        If (str <> "0") Then
                            RadTabStrip2.Tabs(1).Visible = True
                            RadTabStrip2.Tabs(2).Visible = True
                        ElseIf str = "0" Then
                            RadMultiPage1.PageViews(0).Selected = True
                            RadTabStrip2.Tabs(0).Selected = True
                            RadTabStrip2.Tabs(1).Visible = False
                            RadTabStrip2.Tabs(2).Visible = False
                        End If
                    Else
                        RadMultiPage1.PageViews(0).Selected = True
                        RadTabStrip2.Tabs(0).Selected = True
                        RadTabStrip2.Tabs(1).Visible = False
                        RadTabStrip2.Tabs(2).Visible = False
                    End If
                End If

                ShowHideBookings()
                BindWalkinDropdown()

                If Not Session("wl") = Nothing Then
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-success")
                    lbExistingCust.Text = "Waiting List Created"
                    Session("wl") = Nothing
                Else
                    divMessageBox.Visible = False
                    divMessageBox.Attributes.Add("class", "")
                    lbExistingCust.Text = ""
                End If

                If Not Session("success") = Nothing Then
                    divMessageBox.Visible = True
                    divMessageBox.Attributes.Add("class", "alert alert-success")
                    lbExistingCust.Text = "Booking success"
                    Session("success") = Nothing
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindWalkinDropdown()
        If Not (Session("ddlTypewb1") Is Nothing And Session("ddlVenuewb1") Is Nothing) Then
            drpTableStore.SelectedValue = Session("ddlVenuewb1").ToString()
            BindLocationType()
            rddlType.SelectedValue = Session("ddlTypewb1").ToString()
            LoadTableBookingByDate()
            Bindchart()
            Session("ddlTypewb1") = Nothing
            Session("ddlVenuewb1") = Nothing
        End If
    End Sub
    Protected Sub gvTableBooking_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvTableBooking.ItemCommand
        If (e.CommandName = "btnSeat") Then
            Try
                divMessageBox.Visible = False
                Dim strQuery As String = String.Empty
                strQuery = " SELECT SM.OurStoreId,BSI.Name AS Type,SM.StoreName AS Location, B.bookingid,B.accountid,A.FirstName+' '+A.LastName AS FullName,"
                strQuery &= " B.covers, B.date, B.bookingref, B.bookingtime, b.deposit,"
                strQuery &= " (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table "
                strQuery &= " where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables"
                strQuery &= " FROM bookings B INNER JOIN bookingsettings BS ON B.BookingSettingsid = BS.BookingSettingsid INNER JOIN BookingSchedule BSI ON B.BookingScheduleID = BSI.BookingScheduleID"
                strQuery &= " INNER JOIN Account A ON B.accountid = A.accountid"
                strQuery &= " left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId"
                strQuery &= " WHERE b.bookingref = '" + e.CommandArgument.ToString() + "'"


                Dim objConn As DBConnection = New DBConnection()

                Dim ds As DataSet = objConn.SelectData(strQuery)

                If ds.Tables(0).Rows.Count > 0 Then
                    lblLocationWin.Text = ds.Tables(0).Rows(0)("Location")
                    lblScheduleWin.Text = ds.Tables(0).Rows(0)("Type")
                    lblBokingRefWin.Text = ds.Tables(0).Rows(0)("bookingref")
                    Dim date1 As Date = ds.Tables(0).Rows(0)("date")
                    lblBookingDateWin.Text = date1.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                    lblFullNameWin.Text = ds.Tables(0).Rows(0)("FullName")
                    lblCoversWin.Text = ds.Tables(0).Rows(0)("covers")
                    lblTableWin.Text = ds.Tables(0).Rows(0)("Allotted_Tables")
                    lblTimeWin.Text = ds.Tables(0).Rows(0)("bookingtime").ToString()
                    Dim a As Decimal = ds.Tables(0).Rows(0)("deposit")
                    lblDepoAmount.Text = a.ToString("n2")
                End If

                Dim script As String = "function f(){$find(""" + RadWindow_ContentTemplate.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)


                'Dim strQuery As String = " UPDATE Tables SET Closed = 1 WHERE TableID = (SELECT TableID from M_OpenTable WHERE BookingRef = '" + e.CommandArgument.ToString + "') "
                'Dim conn As DBConnection = New DBConnection()
                'conn.Ins_Upd_Del(strQuery)
                'LoadTableBookingByDate()
            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub gvTableBooking_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvTableBooking.ItemDataBound
        Try
            Dim oClsDataccess As New ClsDataccess
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            If item IsNot Nothing Then

                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                Dim hdnbookingref As HiddenField = TryCast(item.FindControl("hdnbookingref"), HiddenField)
                Dim hdnClosed As HiddenField = TryCast(item.FindControl("hdnClosed"), HiddenField)
                Dim hdnAllotted As HiddenField = TryCast(item.FindControl("hdnAllotted"), HiddenField)

                If (hdnClosed IsNot Nothing) Then
                    Try
                        If hdnClosed.Value = 1 Then
                            e.Item.BackColor = System.Drawing.Color.FromName("#e5e6e6")
                        End If
                    Catch ex As Exception

                    End Try
                End If

                If (hdnbookingref IsNot Nothing) Then
                    Try
                        oClsDataccess = New ClsDataccess
                        Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                        oColSqlPar.Add("@BookingRef", hdnbookingref.Value)
                        Dim dt As DataTable = oClsDataccess.GetdataTableSp("Get_Live_Schedule", oColSqlPar, "Get_Live_Schedule")
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)("flag1").ToString() = "1" Then
                                TryCast(item.FindControl("lnkbtn"), LinkButton).Visible = False
                            Else
                                TryCast(item.FindControl("lnkbtn"), LinkButton).Visible = True
                            End If
                            If dt.Rows(0)("flag2").ToString() = "1" Then
                                TryCast(item.FindControl("lnkpopup"), LinkButton).Visible = False
                            Else
                                TryCast(item.FindControl("lnkpopup"), LinkButton).Visible = True
                            End If
                        End If
                        If hdnAllotted.Value = "No Table Allotted" Then
                            'TryCast(item.FindControl("lnkbtn"), LinkButton).Visible = False
                            e.Item.Style("font-weight") = "bold"

                        End If
                    Catch ex1 As Exception

                    End Try
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Bindchart()
        Dim oClsDataccess As New ClsDataccess
        'Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        'sb.Append("<script language='javascript'>")
        'sb.Append("var lbl = document.getElementById('lblDisplayDate');")
        'sb.Append("lbl.style.color='red';")
        'sb.Append("</script>")

        'If (Not ClientScript.IsStartupScriptRegistered("JSScript")) Then
        '    ClientScript.RegisterStartupScript(Me.GetType(), "JSScript", sb.ToString())
        'End If

        If Val(drpTableStore.SelectedValue) <> 0 And Val(rddlType.SelectedValue) <> 0 Then
            Try
                Dim dt As Date = Date.Today
                If Not rdpDate.SelectedDate Is Nothing Then
                    dt = DateTime.Parse(rdpDate.SelectedDate)
                End If
                Dim ds As New DataSet()

                oClsDataccess = New ClsDataccess
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                oColSqlPar.Add("@BBookingDate", Convert.ToDateTime(dt.ToString("s")).ToString("yyyy-MM-dd"))
                oColSqlPar.Add("@BSettingsID", drpTableStore.SelectedValue)
                oColSqlPar.Add("@BBookingScheduleID", rddlType.SelectedValue)
                ds = oClsDataccess.GetdatasetSp("Get_Chart_Script", oColSqlPar, "Get_Live_Schedule")
                ViewState("jscript") = Nothing
                Dim count As Integer = 0
                If ds.Tables.Count > 0 Then
                    For Each row In ds.Tables(0).Rows
                        If count = 1 Then
                            ViewState("jscript") = ViewState("jscript") + row("Script")
                        Else
                            ViewState("jscript") = row("Script")
                            count = 1
                        End If
                    Next
                End If
                lblErrorScript.Visible = False
                divchart.Visible = True
            Catch ex As Exception

            End Try
        Else
            lblErrorScript.Visible = True
            lblErrorScript.Text = "Must select Location and Type"
            ViewState("jscript") = Nothing
            divchart.Visible = False
        End If

        If drpTableStore.SelectedValue <> 0 And rddlType.SelectedValue <> 0 Then
            lblTableView.Visible = False
            diviFrameTbl.Visible = True
        Else
            lblTableView.Text = "Must select Location and Type"
            lblTableView.Visible = True
            diviFrameTbl.Visible = False
        End If
    End Sub
    Private Sub BindLocationType()
        rddlType.Items.Clear()
        Dim oClsDataccess As New ClsDataccess
        If drpTableStore.Items.Count > 0 Then
            If drpTableStore.SelectedValue = 0 Then
                Dim ddl As RadDropDownList = rddlType
                Dim li As Telerik.Web.UI.DropDownListItem = New Telerik.Web.UI.DropDownListItem("ALL", "0")
                ddl.Items.Insert(0, li)
                ddl.SelectedValue = 0
                Session("rddlTypeId") = rddlType.SelectedValue
                Session("rddlTypeText") = rddlType.SelectedText
            Else
                Dim obj As Common = New Common()
                Dim ds As DataSet = obj.GetTableTypeOnlyBySettingId(drpTableStore.SelectedValue)
                If ds IsNot Nothing Then
                    If ds.Tables.Count > 0 Then
                        rddlType.DataSource = ds.Tables(0)
                        rddlType.DataTextField = "Name"
                        rddlType.DataValueField = "Id"
                        rddlType.DataBind()
                        Dim ddl As RadDropDownList = rddlType
                        Dim li As Telerik.Web.UI.DropDownListItem = New Telerik.Web.UI.DropDownListItem("ALL", "0")
                        ddl.Items.Insert(0, li)
                        If ds.Tables(0).Rows.Count > 0 Then
                            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                            oColSqlparram.Add("@BookingSettingsID", Val(drpTableStore.SelectedValue), SqlDbType.Int)
                            Dim dtLive As DataTable = oClsDataccess.GetdataTableSp("Get_Current_Schedule", oColSqlparram)
                            If dtLive.Rows.Count > 0 Then
                                ddl.SelectedValue = dtLive.Rows(0)("BookingScheduleID").ToString()
                            Else
                                ddl.SelectedIndex = 1
                            End If
                        Else
                            ddl.SelectedIndex = 0
                        End If
                        Session("rddlTypeId") = rddlType.SelectedValue
                        Session("rddlTypeText") = rddlType.SelectedText
                    End If
                End If
            End If
            If rddlType.SelectedValue <> Nothing Then
                If rddlType.SelectedValue <> 0 Then
                    Dim conn As DBConnection = New DBConnection()
                    Dim Str As String = conn.SingleCell("select GroupID from BookingSchedule where BookingScheduleID = " + rddlType.SelectedValue + "")
                    If (Str <> "0") Then
                        RadTabStrip2.Tabs(1).Visible = True
                        RadTabStrip2.Tabs(2).Visible = True
                    ElseIf Str = "0" Then
                        RadMultiPage1.PageViews(0).Selected = True
                        RadTabStrip2.Tabs(0).Selected = True
                        RadTabStrip2.Tabs(1).Visible = False
                        RadTabStrip2.Tabs(2).Visible = False
                    End If
                Else
                    RadMultiPage1.PageViews(0).Selected = True
                    RadTabStrip2.Tabs(0).Selected = True
                    RadTabStrip2.Tabs(1).Visible = False
                    RadTabStrip2.Tabs(2).Visible = False
                End If
            End If
        End If
    End Sub
    Private Sub LoadTableBookingByDate()
        Try
            Dim oClsDataccess As New ClsDataccess
            lblListDate.Text = String.Empty
            lblListVenueName.Text = String.Empty

            gvTableBooking.DataSource = Nothing
            gvTableBooking.DataBind()

            Dim dt As Date = Date.Today
            If Not rdpDate.SelectedDate Is Nothing Then
                dt = DateTime.Parse(rdpDate.SelectedDate)
            End If

            Dim strQuery As String = String.Empty

            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            oColSqlPar.Add("@Location", Val(drpTableStore.SelectedValue), SqlDbType.Int)
            oColSqlPar.Add("@Schedule", Val(rddlType.SelectedValue), SqlDbType.Int)
            oColSqlPar.Add("@date", dt, SqlDbType.DateTime)
            Dim dsBookingList As DataSet = oClsDataccess.GetdatasetSp("P_LoadTableBookingByDate", oColSqlPar, "Table Booking")

            gvTableBooking.DataSource = dsBookingList.Tables(0)
            gvTableBooking.DataBind()

            lblListDate.Text = "Date : " & dt.ToString("dd/MM/yyyy")
            lblListVenueName.Text = "Store : " & drpTableStore.SelectedText

            Bindchart()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub drpTableStore_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles drpTableStore.SelectedIndexChanged
        BindLocationType()
        ShowHideBookings()
        Session("drpTableStoreId") = drpTableStore.SelectedValue
    End Sub
    Protected Sub rddlType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles rddlType.SelectedIndexChanged


        Session("rddlTypeId") = rddlType.SelectedValue
        Session("rddlTypeText") = rddlType.SelectedText
        If rddlType.SelectedValue <> Nothing Then
            If rddlType.SelectedValue <> 0 Then
                Dim conn As DBConnection = New DBConnection()
                Dim Str As String = conn.SingleCell("select GroupID from BookingSchedule where BookingScheduleID = " + rddlType.SelectedValue + "")
                If (Str <> "0") Then
                    RadTabStrip2.Tabs(1).Visible = True
                    RadTabStrip2.Tabs(2).Visible = True
                ElseIf Str = "0" Then
                    RadMultiPage1.PageViews(0).Selected = True
                    RadTabStrip2.Tabs(0).Selected = True
                    RadTabStrip2.Tabs(1).Visible = False
                    RadTabStrip2.Tabs(2).Visible = False
                End If
            Else
                RadMultiPage1.PageViews(0).Selected = True
                RadTabStrip2.Tabs(0).Selected = True
                RadTabStrip2.Tabs(1).Visible = False
                RadTabStrip2.Tabs(2).Visible = False
            End If
        End If
        ShowHideBookings()
    End Sub
    Protected Sub imgbtnRefreshChart_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRefreshChart.Click
        LoadTableBookingByDate()
        Bindchart()
    End Sub

    Protected Sub imgrefreshGrid_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgrefreshGrid.Click
        LoadTableBookingByDate()
        Bindchart()
    End Sub

    Protected Sub btnSaveWin_Click(sender As Object, e As System.EventArgs) Handles btnSaveWin.Click
        Try
            Dim oClsDataccess As New ClsDataccess
            If lblTableWin.Text = "No Table Allotted" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('No Table Allotted');", True)
            Else
                Dim oclsParm As New ColSqlparram
                oclsParm.Add("@ID", 0, SqlDbType.Int)
                oclsParm.Add("@Tran_type", "BookingRefSeat")
                oclsParm.Add("@BookingRef", lblBokingRefWin.Text)
                oClsDataccess.ExecStoredProcedure("P_M_OpenTable", oclsParm)
            End If
            LoadTableBookingByDate()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message + "');", True)
        End Try
        'Dim strQuery As String = " UPDATE Tables SET Closed = 1 WHERE TableID = (SELECT TableID from M_OpenTable WHERE BookingRef = '" + e.CommandArgument.ToString + "') "
        'Dim conn As DBConnection = New DBConnection()
        'conn.Ins_Upd_Del(strQuery)
    End Sub
    Private Sub BindStoreDropdown()
        Common.BindTableStoreDropdownAllNoTab(drpTableStore, False)
        Session("drpTableStoreId") = drpTableStore.SelectedValue
        BindLocationType()
    End Sub
    Protected Sub rdpDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdpDate.SelectedDateChanged
        Session("rdpDate") = rdpDate.SelectedDate
        ShowHideBookings()
    End Sub
    Protected Sub rdoBooking_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdoBooking.SelectedIndexChanged
        ShowHideBookings()
    End Sub
    Private Sub ShowHideBookings()

        If rdoBooking.SelectedValue = "0" Then
            gvTableBooking.Visible = True
            gvCancelBooking.Visible = False
            LoadTableBookingByDate()

        ElseIf rdoBooking.SelectedValue = "1" Then
            gvTableBooking.Visible = False
            gvCancelBooking.Visible = True
            LoadCancelledBookingByDate()
        End If
    End Sub
    Private Sub LoadCancelledBookingByDate()
        Try
            lblListDate.Text = String.Empty
            lblListVenueName.Text = String.Empty

            gvTableBooking.DataSource = Nothing
            gvTableBooking.DataBind()

            Dim dt As Date = Date.Today
            If Not rdpDate.SelectedDate Is Nothing Then
                dt = DateTime.Parse(rdpDate.SelectedDate)
            End If
            Dim strQuery As String = String.Empty

            If drpTableStore.SelectedValue <> 0 And rddlType.SelectedValue = 0 Then
                strQuery = " SELECT B.comment as comment,b.BookingSettingsid,SM.OurStoreId,BSI.Name AS Type,SM.StoreName AS Location, B.bookingid,B.accountid,A.FirstName+' '+A.LastName AS FullName,"
                strQuery &= " B.covers, B.date, B.bookingref, B.bookingtime,ISNULL(bzpT.Closed,0) as Closed,"
                strQuery &= " (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table "
                strQuery &= " where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables"
                strQuery &= " FROM bookings B INNER JOIN bookingsettings BS ON B.BookingSettingsid = BS.BookingSettingsid INNER JOIN BookingSchedule BSI ON B.BookingScheduleID = BSI.BookingScheduleID"
                strQuery &= " INNER JOIN Account A ON B.accountid = A.accountid"
                strQuery &= " left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId"
                strQuery &= " LEFT OUTER JOIN M_OpenTable o on o.bookingref = b.bookingref LEFT OUTER join Tables bzpT on bzpT.TableID = o.TableID"
                strQuery &= " WHERE(B.period = 1 And B.IsCanceled = 1) "
                strQuery &= " AND BS.BookingSettingsid = '" & drpTableStore.SelectedValue & "'"
                strQuery &= "  AND CAST(B.date AS DATE) = '" & dt.ToString("s") & "' ORDER BY ISNULL(bzpT.Closed,0),BSI.Name,B.bookingtime"
            Else
                If Not rddlType.SelectedValue = 0 Then
                    strQuery = " SELECT B.comment as comment,b.BookingSettingsid,SM.OurStoreId,BS.Name AS Type,SM.StoreName AS Location, B.bookingid,B.accountid,A.FirstName+' '+A.LastName AS FullName,"
                    strQuery &= " B.covers, B.date, B.bookingref, B.bookingtime,ISNULL(bzpT.Closed,0) as Closed,"
                    strQuery &= " (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table "
                    strQuery &= " where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables"
                    strQuery &= " FROM bookings B INNER JOIN BookingSchedule BS ON B.BookingScheduleID = BS.BookingScheduleID"
                    strQuery &= " INNER JOIN Account A ON B.accountid = A.accountid"
                    strQuery &= " left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId"
                    strQuery &= " LEFT OUTER JOIN M_OpenTable o on o.bookingref = b.bookingref LEFT OUTER join Tables bzpT on bzpT.TableID = o.TableID"
                    strQuery &= " WHERE(B.period = 1 And B.IsCanceled = 1)"
                    strQuery &= " AND BS.BookingScheduleID = '" & rddlType.SelectedValue & "'"
                    strQuery &= "  AND CAST(B.date AS DATE) = '" & dt.ToString("s") & "' ORDER BY ISNULL(bzpT.Closed,0),B.bookingtime"
                Else
                    strQuery = " SELECT B.comment as comment,b.BookingSettingsid,SM.OurStoreId,BS.Name AS Type,SM.StoreName AS Location, B.bookingid,B.accountid,A.FirstName+' '+A.LastName AS FullName,"
                    strQuery &= " B.covers, B.date, B.bookingref, B.bookingtime,ISNULL(bzpT.Closed,0) as Closed,"
                    strQuery &= " (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table"
                    strQuery &= " where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables"
                    strQuery &= " FROM bookings B INNER JOIN BookingSchedule BS ON B.BookingScheduleID = BS.BookingScheduleID"
                    strQuery &= " INNER JOIN Account A ON B.accountid = A.accountid"
                    strQuery &= " left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId"
                    strQuery &= " LEFT OUTER JOIN M_OpenTable o on o.bookingref = b.bookingref LEFT OUTER join Tables bzpT on bzpT.TableID = o.TableID"
                    strQuery &= " WHERE(B.period = 1 And B.IsCanceled = 1) and ISNULL(bzpT.Closed,2) <> 1 "
                    strQuery &= "  AND CAST(B.date AS DATE) = '" & dt.ToString("s") & "' ORDER BY ISNULL(bzpT.Closed,0),B.bookingtime"
                End If
            End If

            'If Not drpTableStore.SelectedValue = 0 Then
            '    strQuery = " SELECT SM.OurStoreId,SM.StoreName AS Location, B.bookingid,B.accountid,A.FirstName+' '+A.LastName AS FullName,"
            '    strQuery &= " B.covers, B.date, B.bookingref, B.bookingtime,"
            '    strQuery &= " (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table "
            '    strQuery &= " where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables"
            '    strQuery &= " FROM bookings B INNER JOIN bookingsettings BS ON B.BookingSettingsid = BS.BookingSettingsid"
            '    strQuery &= " INNER JOIN Account A ON B.accountid = A.accountid"
            '    strQuery &= " left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId"
            '    strQuery &= " WHERE(B.period = 1 And B.IsCanceled = 0)"
            '    strQuery &= " AND BS.BookingSettingsid = '" & drpTableStore.SelectedValue & "'"
            '    strQuery &= "  AND CAST(B.date AS DATE) = '" & dt.ToString("s") & "' ORDER BY B.bookingtime"
            'Else
            '    strQuery = " SELECT SM.OurStoreId,SM.StoreName AS Location, B.bookingid,B.accountid,A.FirstName+' '+A.LastName AS FullName,"
            '    strQuery &= " B.covers, B.date, B.bookingref, B.bookingtime,"
            '    strQuery &= " (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table"
            '    strQuery &= " where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables"
            '    strQuery &= " FROM bookings B INNER JOIN bookingsettings BS ON B.BookingSettingsid = BS.BookingSettingsid"
            '    strQuery &= " INNER JOIN Account A ON B.accountid = A.accountid"
            '    strQuery &= " left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId"
            '    strQuery &= " WHERE(B.period = 1 And B.IsCanceled = 0)"
            '    strQuery &= "  AND CAST(B.date AS DATE) = '" & dt.ToString("s") & "' ORDER BY B.bookingtime"
            'End If

            Dim objConn As DBConnection = New DBConnection()

            Dim ds As DataSet = objConn.SelectData(strQuery)

            gvCancelBooking.DataSource = ds.Tables(0)
            gvCancelBooking.DataBind()

            lblListDate.Text = "Date : " & dt.ToString("dd/MM/yyyy")
            lblListVenueName.Text = "Store : " & drpTableStore.SelectedText

            Bindchart()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gvCancelBooking_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvCancelBooking.ItemDataBound

        Try
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            If item IsNot Nothing Then

                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                Dim hdnBookingDate As HiddenField = TryCast(item.FindControl("hdnBookingDate"), HiddenField)
                Dim hdnBookingTime As HiddenField = TryCast(item.FindControl("hdnBookingTime"), HiddenField)
                Dim hdnAllotted As HiddenField = TryCast(item.FindControl("hdnAllotted"), HiddenField)
                Dim lnkbtn As LinkButton = TryCast(item.FindControl("lnkbtn"), LinkButton)


                Dim time_AMPM As DateTime = DateTime.Now.ToString("HH:mm:ss tt")
                Dim time_24hr As String = time_AMPM.TimeOfDay.ToString().Substring(0, 8).Trim()

                Dim bookingdate As String = hdnBookingDate.Value.ToString().Substring(0, 10).Trim()

                'Response.Write(bookingdate)
                'Response.Write(time_24hr)


                If ((Format(DateTime.Now.ToShortDateString(), "{0:dd/MM/yyyy}") <= bookingdate) And (time_24hr <= hdnBookingTime.Value.ToString().Trim())) Then
                    lnkbtn.Visible = True
                Else
                    lnkbtn.Visible = False
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gvCancelBooking_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvCancelBooking.ItemCommand
        If (e.CommandName = "btnRestore") Then
            Try
                Dim oClsDataccess As New ClsDataccess
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                oColSqlPar.Add("@bookingid", e.CommandArgument, SqlDbType.Int)
                Dim dtEmp As DataSet = oClsDataccess.GetdatasetSp("P_RestoreBooking", oColSqlPar, "Restore Booking")
                LoadCancelledBookingByDate()
            Catch ex As Exception

            End Try
        End If
    End Sub
    'Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
    '    If rdoBooking.SelectedValue = 0 Then
    '        gvTableBooking.ExportSettings.ExportOnlyData = True
    '        gvTableBooking.ExportSettings.IgnorePaging = True
    '        gvTableBooking.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Biff
    '        gvTableBooking.MasterTableView.ExportToExcel()
    '    ElseIf rdoBooking.SelectedValue = 1 Then
    '        gvCancelBooking.ExportSettings.ExportOnlyData = True
    '        gvCancelBooking.ExportSettings.IgnorePaging = True
    '        gvCancelBooking.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Biff
    '        gvCancelBooking.MasterTableView.ExportToExcel()
    '    End If

    'End Sub
    Protected Sub imgbtn_Click(sender As Object, e As System.EventArgs) Handles imgbtn.Click

        If rdoBooking.SelectedValue = 0 Then

            gvTableBooking.MasterTableView.GetColumn("TemplateColumn").Exportable = False
            gvTableBooking.MasterTableView.GetColumn("Update").Exportable = False
            gvTableBooking.MasterTableView.GetColumn("Print").Exportable = False

            gvTableBooking.MasterTableView.GetColumn("No").Display = True

            gvTableBooking.AllowFilteringByColumn = False
            gvTableBooking.Rebind()
            gvTableBooking.ExportSettings.FileName = "Table Booking"
            gvTableBooking.ExportSettings.IgnorePaging = True
            gvTableBooking.ExportSettings.OpenInNewWindow = True
            gvTableBooking.MasterTableView.ExportToExcel()

        ElseIf rdoBooking.SelectedValue = 1 Then

            gvCancelBooking.MasterTableView.GetColumn("Restore").Exportable = False
            gvCancelBooking.MasterTableView.GetColumn("No").Display = True

            gvCancelBooking.AllowFilteringByColumn = False
            gvCancelBooking.Rebind()
            gvCancelBooking.ExportSettings.FileName = "Cancelled Table Booking"
            gvCancelBooking.ExportSettings.ExportOnlyData = True
            gvCancelBooking.ExportSettings.IgnorePaging = True
            gvCancelBooking.ExportSettings.OpenInNewWindow = True
            gvCancelBooking.MasterTableView.ExportToExcel()
        End If

    End Sub
    Private Sub BindType()
        ddlType.Items.Clear()
        Dim oClsDataccess As New ClsDataccess
        If ddlVenue.Items.Count > 0 Then
            oClsDataccess = New ClsDataccess
            Dim obj As Common = New Common()
            Dim ds As DataSet = obj.GetTableType(ddlVenue.SelectedValue, DateTime.Now)
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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Dim oClsDataccess As New ClsDataccess
            divMessageBox.Visible = True
            divMessageBox.Attributes.Add("class", "")
            lbExistingCust.Text = ""

            divMessageBox1.Visible = False
            lbExistingCust1.Text = ""
            oClsDataccess = New ClsDataccess
            Dim conn As DBConnection = New DBConnection()
            Dim venue As String = Utils.Encrypt(ddlVenue.SelectedItem.Value)
            Dim bDate As String = Utils.Encrypt(DateTime.Now.ToString())
            Dim bTime As String = Utils.Encrypt(ddlType.SelectedItem.Value)

            Session("ddlVenuewb1") = drpTableStore.SelectedValue
            Session("ddlTypewb1") = rddlType.SelectedValue

            Dim noOfCovers As String
            noOfCovers = Utils.Encrypt(txtNoOfCovers.Text).ToString()

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@BookingScheduleID", Val(ddlType.SelectedItem.Value), SqlDbType.Int)
            Dim ds As DataSet = oClsDataccess.GetdatasetSp("Check_Mincovers_OneBooking", oColSqlparram, "MinCover")

            If (ds.Tables(0).Rows.Count > 0) Then
                If ds.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" And Not ds.Tables(0).Rows(0)("MinCovers").ToString() = 0 Then
                    Dim min As Integer = ds.Tables(0).Rows(0)("MinCovers").ToString()
                    If txtNoOfCovers.Text < min Then
                        divMessageBox.Visible = True
                        divMessageBox.Attributes.Add("class", "alert alert-danger")
                        lbExistingCust.Text = "Minimum " + ds.Tables(0).Rows(0)("MinCovers").ToString() + " Covers Required."
                    Else
                        CreateBooking()
                    End If
                Else
                    'Response.Redirect("SearchResultTable.aspx?venue=" & venue & "&date=" & bDate & "&time=" & bTime & "&cover=" & noOfCovers & "&TabId=" & CurrentTabID)
                    CreateBooking()
                End If
            End If
            Bindchart()
            LoadTableBookingByDate()
        Catch ex As Exception

        End Try

    End Sub

    Public Sub CreateBooking()
        Try
            Dim oClsDataccess As New ClsDataccess
            Dim noOfCovers As String
            'If (ddlNoOfCovers.SelectedValue = "-1") Then
            noOfCovers = Utils.Encrypt(txtNoOfCovers.Text).ToString()

            Dim objCommon As Common = New Common()

            If Not Request.QueryString("IsOnline") Is Nothing Then
                IsOnline = True
                BindMaxCovers()
            End If

            If IsOnline Then
                ActualStoreID = objCommon.GetStoreIDForOnlineBooking(Utils.ParseInt(ddlVenue.SelectedValue), DateTime.Now, Utils.ParseInt(ddlType.SelectedValue))
            End If

            'storeId = Utils.Encrypt(DirectCast(source.Parent.FindControl("hdnStoreId"), HiddenField).Value).ToString()

            Dim dt As DataTable = Common.SearchTableGetStore(Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd"), "01:00", ddlVenue.SelectedValue, ddlType.SelectedValue)

            Dim timeSlotId As String = dt.Rows(0)("TimeSlotId").ToString()
            Dim settingsId As String = dt.Rows(0)("SettingID").ToString()
            Dim StoreId As String = dt.Rows(0)("StoreID").ToString()

            Session("strid_1") = dt.Rows(0)("StoreID").ToString()
            Session("stingid_1") = dt.Rows(0)("SettingID").ToString()
            Session("cvr_1") = txtNoOfCovers.Text
            Session("schdlid_1") = ddlType.SelectedValue

            noOfCovers = txtNoOfCovers.Text

            Dim ds1 As New DataSet()
            oClsDataccess = New ClsDataccess
            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            oColSqlPar.Add("@BbookingDate", Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd"), SqlDbType.DateTime)
            oColSqlPar.Add("@BSettingsID", settingsId)
            oColSqlPar.Add("@BcurrentCover", noOfCovers)
            oColSqlPar.Add("@BBookingScheduleID", Utils.ParseInt(ddlType.SelectedValue))
            oColSqlPar.Add("@BCurrBookingRefNo", String.Empty)
            oColSqlPar.Add("@BIsOnline", IsOnline)

            ds1 = oClsDataccess.GetdatasetSp("SearchTableGetTimeSlots", oColSqlPar, "SearchTableGetTimeSlots")

            Dim i As Integer
            Dim count As Integer = 0

            Dim noofrow As Integer
            noofrow = ds1.Tables(0).Rows.Count - 1


            lblLocationW.Text = ddlVenue.SelectedText.ToString()
            lblScheduleW.Text = ddlType.SelectedText.ToString()
            lblBookingdateW.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
            lblBookingtimeW.Text = "No Time Slot Available"
            lblTableW.Text = txtNoOfCovers.Text

            For i = 0 To ds1.Tables(0).Rows.Count - 1


                Dim SlotTime As String = ds1.Tables(0).Rows(i)("SlotTime").ToString()
                SlotTime = TimeSpan.Parse(SlotTime.ToString()).ToString("hh\:mm")
                Dim Now As String = DateTime.Now.ToString("HH:mm")
                Now = Now.Substring(0, 5)
                Now = TimeSpan.Parse(Now.ToString()).ToString("hh\:mm")
                Session("stime_1") = SlotTime
                Dim isAvailable As String = ds1.Tables(0).Rows(i)("isAvailable").ToString()

                If i = 0 Then
                    If Now < SlotTime Then
                        divMessageBox1.Visible = True
                        divMessageBox1.Attributes.Add("class", "alert alert-danger")
                        lbExistingCust1.Text = "Currently no time slot available"
                        Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                        Exit Sub
                    End If
                End If
                If i = noofrow Then
                    If Now > SlotTime Then
                        divMessageBox1.Visible = True
                        divMessageBox1.Attributes.Add("class", "alert alert-danger")
                        lbExistingCust1.Text = "Currently no time slot available"
                        Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                        Exit Sub
                    End If
                    If Now < SlotTime Then
                        If isAvailable = "0" Then
                            divMessageBox1.Visible = True
                            divMessageBox1.Attributes.Add("class", "alert alert-danger")
                            lbExistingCust1.Text = "People are not cover in available tables"
                            Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                            Exit Sub
                        End If
                    End If
                End If

                If Now <= SlotTime Then

                    If isAvailable = "0" Then
                        count = count + 1
                    Else
                        Dim bookingScheduleDateId As String = Common.GetBookingScheduleDateId(System.DateTime.Now, Utils.ParseInt(ddlType.SelectedValue))
                        Dim dateTime As String = System.DateTime.Now.ToString("D") & " " & SlotTime

                        BindTableForBookingScheduleDate(Utils.ParseInt(bookingScheduleDateId), Utils.ParseInt(ddlType.SelectedValue), SlotTime)

                        Dim tablejoin As String = objCommon.GetAllValue(ddlTableSet)

                        Dim sum As Integer = 0
                        Dim ds3 As New DataSet()
                        Dim oColSqlPar3 As ColSqlparram = New ColSqlparram()
                        oColSqlPar3.Add("@tableid", tablejoin)
                        ds3 = oClsDataccess.GetdatasetSp("P_Get_CompareCover", oColSqlPar3, "P_Get_CompareCover")

                        If ds3.Tables.Count > 0 Then
                            Dim maxcover As Integer = ds3.Tables(0).Rows(0)("MaxCover").ToString()
                            sum = maxcover
                        End If
                        If (sum < noOfCovers) Then
                            If count = 2 Then
                                divMessageBox1.Visible = True
                                divMessageBox1.Attributes.Add("class", "alert alert-danger")
                                lbExistingCust1.Text = "People are not cover in available tables"
                                Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                                Exit For
                            End If
                            count = count + 1
                        Else
                            Session("wbdate1") = dateTime.ToString()
                            Session("wbBSettingsID1") = settingsId.ToString()
                            Session("wbBookingScheduleDateId1") = bookingScheduleDateId.ToString()
                            Session("wbOurStoreId1") = StoreId.ToString()
                            Session("wbcovers1") = txtNoOfCovers.Text.ToString()

                            Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                            lblLocationW.Text = ddlVenue.SelectedText.ToString()
                            lblScheduleW.Text = ddlType.SelectedText.ToString()
                            lblBookingdateW.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
                            lblBookingtimeW.Text = SlotTime.ToString()
                            lblTableW.Text = txtNoOfCovers.Text

                            txtNoOfCovers.Text = ""
                            Exit For
                        End If
                    End If
                    If count = 2 Then
                        divMessageBox1.Visible = True
                        divMessageBox1.Attributes.Add("class", "alert alert-danger")
                        lbExistingCust1.Text = "All tables are full"
                        Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                        Exit For
                    End If
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindMaxCovers()

        Dim drSchedule As DataRow = Common.GetOnlineMaxCover(Utils.ParseInt(ddlType.SelectedValue))
        If drSchedule IsNot Nothing Then
            If IsOnline Then
                Dim maxCover As Int32 = Utils.ParseInt(drSchedule("OnlineMaxCovers"))
                regNoOfCovers.MaximumValue = maxCover
            End If
        End If

    End Sub

    Protected Sub ddlVenue_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlVenue.SelectedIndexChanged
        BindType()
        divMessageBox.Visible = False
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlType.SelectedIndexChanged
        divMessageBox.Visible = False
    End Sub

    Public Sub BindTableForBookingScheduleDate(ByVal BookingScheduleDateId As Int32, ByVal BookingScheduleID As Int32, ByVal bookingtime As String)
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNamesByBookingScheduleDateId(BookingScheduleDateId, BookingScheduleID, bookingtime)
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

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim oClsDataccess As New ClsDataccess
        If ddlTableSet.SelectedValue Is Nothing Then
            'lblError.Visible = True
            'lblError.Text = "Please Select Tables From Available Tables"
        Else
            Try
                Dim que As String = ""
                Dim conn As DBConnection = New DBConnection()
                Dim objCommon As Common = New Common()
                Dim tablejoin As String = objCommon.GetSelectedValue(ddlTableSet)

                Dim vals As String() = tablejoin.ToString().Split("#")

                Dim largest As Integer = Integer.MinValue
                Dim smallest As Integer = Integer.MaxValue

                Dim sum As Integer = 0
                Dim ds3 As New DataSet()
                Dim oColSqlPar3 As ColSqlparram = New ColSqlparram()
                oColSqlPar3.Add("@tableid", tablejoin)
                ds3 = oClsDataccess.GetdatasetSp("P_Get_CompareCover", oColSqlPar3, "P_Get_CompareCover")

                If ds3.Tables.Count > 0 Then
                    Dim maxcover As Integer = ds3.Tables(0).Rows(0)("MaxCover").ToString()
                    sum = maxcover
                End If


                For Each element As Integer In vals
                    largest = Math.Max(largest, element)
                    smallest = Math.Min(smallest, element)
                Next
                Console.WriteLine(largest)
                Console.WriteLine(smallest)
                Dim noofcover As Integer

                noofcover = Convert.ToInt32(Session("wbcovers1").ToString())

                If sum < noofcover Then
                    divMessageBox1.Visible = True
                    divMessageBox1.Attributes.Add("class", "alert alert-danger")
                    lbExistingCust1.Text = "No of people is lessthan max cover of alloted tables."
                    Exit Sub
                End If

                divMessageBox1.Visible = False

                Dim oDbConnection As New DBConnection()
                Dim query As DataSet = oDbConnection.SelectData("SELECT TableNo FROM M_Table where Table_id = " + smallest.ToString() + "")

                Dim chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                Dim random = New Random()
                Dim strBookingRef = New String(Enumerable.Repeat(chars, 8).[Select](Function(s) s(random.[Next](s.Length))).ToArray())



                Dim oColSqlPar1 As ColSqlparram = New ColSqlparram()
                oColSqlPar1.Add("@covers", Session("wbcovers1").ToString(), SqlDbType.Int)
                oColSqlPar1.Add("@date", Utils.ParseDateTime(Session("wbdate1").ToString()), SqlDbType.SmallDateTime)
                oColSqlPar1.Add("@BookingRef", strBookingRef)
                oColSqlPar1.Add("@Bookingtime", lblBookingtimeW.Text, SqlDbType.Time)
                oColSqlPar1.Add("@BSettingsID", Session("wbBSettingsID1").ToString(), SqlDbType.Int)
                oColSqlPar1.Add("@CreatedBy", Sessions.UserID)
                oColSqlPar1.Add("@IPAddress", Request.UserHostAddress)
                oColSqlPar1.Add("@BookingScheduleID", Val(ddlType.SelectedValue), SqlDbType.Int)
                oColSqlPar1.Add("@BookingScheduleDateId", Val(Session("wbBookingScheduleDateId1").ToString()), SqlDbType.Int)
                oColSqlPar1.Add("@IsOnline", Utils.ParseBool(IsOnline), SqlDbType.Bit)
                oColSqlPar1.Add("@OurStoreId", Session("wbOurStoreId1").ToString(), SqlDbType.Int)
                Dim ds2 As New DataSet()

                ds2 = oClsDataccess.GetdatasetSp("P_Set_WalkinBooking", oColSqlPar1, "P_Set_WalkinBooking")


                Dim msg As String = ds2.Tables(0).Rows(0)("Booking").ToString()


                que = "Update Bookings Set Allotted_Tables='" & tablejoin.ToString() & "', TableNo='" & query.Tables(0).Rows(0)("TableNo").ToString() & "' WHERE bookingref = '" & strBookingRef & "'"
                conn.ExecuteNonQuery(que)

                que = "if exists (select 1 from M_OpenTable where bookingref = '" & strBookingRef & "') begin update Tables set TableNumber = " + query.Tables(0).Rows(0)("TableNo").ToString() + " where TableID = (select tableid from M_OpenTable where bookingref = '" & strBookingRef & "') end"
                conn.ExecuteNonQuery(que)

                'Bind()
                'lblSuccess.Text = "Booking Updated Successfully"

                Session("msgbox_Val") = "Update"

                If msg = "Sucess" Then
                    Session("success") = "Booking success"
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "alert('Booking success')", True)
                    'Exit For   
                End If


                divMessageBox1.Visible = False
                lbExistingCust1.Text = ""

                Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)


                Session("wbdate1") = Nothing
                Session("wbBSettingsID1") = Nothing
                Session("wbBookingScheduleDateId1") = Nothing
                Session("wbOurStoreId1") = Nothing
                Session("wbcovers1") = Nothing

                'LoadTableBookingByDate()
                'Bindchart()
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

        divMessageBox1.Visible = False
        lbExistingCust1.Text = ""

        Session("wbdate1") = Nothing
        Session("wbBSettingsID1") = Nothing
        Session("wbBookingScheduleDateId1") = Nothing
        Session("wbOurStoreId1") = Nothing
        Session("wbcovers1") = Nothing
    End Sub
    Protected Sub btn_Waitlist_Click(sender As Object, e As System.EventArgs) Handles btn_Waitlist.Click
        Try
            Session("BookingDetails_waiting") = 2
            Dim script As String = "function f(){$find(""" + RadWindow_WalkinBooking.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

            divMessageBox1.Visible = False
            lbExistingCust1.Text = ""

            Response.Redirect("Waitlist_BookingDetailTable.aspx?storeid=" & Utils.Encrypt(Convert.ToInt32(Session("strid_1").ToString)) & "&settingid=" & Utils.Encrypt(Convert.ToInt32(Session("stingid_1").ToString)) & "&datetime=" & Utils.Encrypt(Date.Today & " " & Session("stime_1").ToString()).ToString() & "&covers=" & Utils.Encrypt(Session("cvr_1").ToString()) & "&bookingScheduleId=" & Utils.Encrypt(Session("schdlid_1").ToString()) & "&bookingScheduleDateId=" & Utils.Encrypt(Session("wbBookingScheduleDateId1").ToString()) & "&isonline=" & Utils.Encrypt(Convert.ToInt32(IsOnline)))

            Session("strid_1") = Nothing
            Session("stingid_1") = Nothing
            Session("cvr_1") = Nothing
            Session("schdlid_1") = Nothing
            Session("stime_1") = Nothing



            'Response.Redirect("BookingDetailTable.aspx?storeid=" & storeId & "&settingid=" & settingId & "&datetime=" & DateTime & "&covers=" & noOfCovers & "&bookingScheduleId=" & bookingScheduleId & "&bookingScheduleDateId=" & bookingScheduleDateId & "&isonline=" & intIsOnline)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub RadTabStrip2_TabClick(sender As Object, e As Telerik.Web.UI.RadTabStripEventArgs) Handles RadTabStrip2.TabClick
        Try
            divMessageBox.Visible = False
            divMessageBox.Attributes.Add("class", "")
            lbExistingCust.Text = ""

            LoadTableBookingByDate()
            Bindchart()
        Catch ex As Exception

        End Try
    End Sub
End Class

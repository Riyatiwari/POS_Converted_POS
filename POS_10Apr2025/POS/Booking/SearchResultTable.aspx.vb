Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar
Imports System.Web.UI.DataVisualization.Charting
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient

Partial Class SearchResultTable
    Inherits System.Web.UI.Page
    'Dim conn As DBConnection = New DBConnection()
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

    Public Property TabId() As Int32
        Get
            If (Not ViewState("TabId") Is Nothing) Then
                Return ViewState("TabId")
            End If
            Return 1
        End Get
        Set(ByVal value As Int32)
            ViewState("TabId") = value
        End Set
    End Property

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("AllowSetting") = Nothing

        If (Not Page.IsPostBack()) Then
            Dim objCommon As Common = New Common()
            dtpDate.SelectedDate = DateTime.Now
            rdpFTDate.SelectedDate = DateTime.Now '.AddDays(1)



            'Common.BindVenue(ddlVenue)
            If Not Request.QueryString("IsOnline") Is Nothing Then
                IsOnline = True
            End If

            If Not Request.QueryString("TabId") Is Nothing Then
                TabId = Utils.ParseInt(Request.QueryString("TabId"))
            End If
            Common.BindTableStoreDropdown(ddlVenue, TabId, IsOnline)
            'ddlVenue.Items.Insert(0, New DropDownListItem("All", "0"))
            'ddlVenue.SelectedValue = "0"

            BindType()
            Try
                If (Not Request.QueryString("venue") Is Nothing And Not Request.QueryString("date") Is Nothing And Not Request.QueryString("time") Is Nothing And Not Request.QueryString("cover") Is Nothing) Then
                    ddlVenue.SelectedValue = Utils.Decrypt(Request.QueryString("venue"))
                    dtpDate.SelectedDate = Convert.ToDateTime(Utils.Decrypt(Request.QueryString("date")))
                    rdpFTDate.SelectedDate = Convert.ToDateTime(Utils.Decrypt(Request.QueryString("date")))
                    ''''dtpTime.SelectedTime = TimeSpan.Parse(Utils.Decrypt(Request.QueryString("time")))
                    ddlType.SelectedValue = Utils.Decrypt(Request.QueryString("time"))

                    Dim covers As Integer = Utils.getInteger(Utils.Decrypt(Request.QueryString("cover")))

                    'If (covers > 20) Then
                    txtNoOfCovers.Visible = True
                    rfvNoOfCovers.Enabled = True
                    regNoOfCovers.Enabled = True
                    txtNoOfCovers.Text = covers.ToString()
                    ''ddlNoOfCovers.SelectedValue = "-1"
                    'Else
                    '    txtNoOfCovers.Visible = False
                    '    rfvNoOfCovers.Enabled = False
                    '    regNoOfCovers.Enabled = False
                    '    ddlNoOfCovers.SelectedValue = covers.ToString()
                    'End If
                    BindType()
                    If Not Request.QueryString("IsOnline") Is Nothing Then
                        IsOnline = True
                        BindMaxCovers()
                    End If
                    If IsOnline Then
                        ActualStoreID = objCommon.GetStoreIDForOnlineBooking(Utils.ParseInt(ddlVenue.SelectedValue), dtpDate.SelectedDate, Utils.ParseInt(ddlType.SelectedValue))
                    End If
                    BindRepeater()
                End If
            Catch

            End Try

            ''Session("isonline") = Request.QueryString("IsOnline").ToString()


            Bindchart()

        End If
        If Request.QueryString("IsOnline") IsNot Nothing Then
            Session("isonline") = Request.QueryString("IsOnline").ToString()
        Else
            Session("isonline") = 0
        End If
        If Request.QueryString("IsOnline") = "1" Then


            Dim aNavbarHome As HtmlAnchor = CType(Master.FindControl("aNavbarHome"), HtmlAnchor)

            If aNavbarHome IsNot Nothing Then

                aNavbarHome.Attributes.Remove("href")
                aNavbarHome.Attributes.Add("style", "pointer-events:none;")
            End If

        End If

        If Request.QueryString("IsOnline") = "1" Then
            Try

                Dim s_name As String = If(Session("store"), String.Empty).ToString()
                Dim conn As DBConnection = New DBConnection()
                Dim str As String = "SELECT website FROM m_company WHERE name = '" & s_name & "'"
                Dim ds As DataSet = conn.SelectData(str)
                If (ds.Tables(0).Rows.Count > 0) Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("website")) Then
                        Dim website As String = ds.Tables(0).Rows(0)("website").ToString()
                        Session("website") = website

                    End If

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Bindchart()
        If IsOnline = False Then
            Try
                Dim ds As New DataSet()
                Dim oClsDataccess As New ClsDataccess
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                oColSqlPar.Add("@BBookingDate", Convert.ToDateTime(rdpFTDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"))
                oColSqlPar.Add("@BSettingsID", ddlVenue.SelectedValue)
                oColSqlPar.Add("@BBookingScheduleID", ddlType.SelectedValue)
                ds = oClsDataccess.GetdatasetSp("Get_Chart_Script", oColSqlPar, "Get_Live_Schedule")
                ViewState("jscript") = ""
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
                '    rbcFuturTable.PlotArea.YAxis.MinValue = Convert.ToInt32(ds.Tables(1).Rows(0)("MinSlotTime"))
                '    rbcFuturTable.PlotArea.YAxis.MaxValue = Convert.ToInt32(ds.Tables(1).Rows(0)("MaxSlotTime"))
                '    rbcFuturTable.PlotArea.YAxis.Step = 1
                '    rbcFuturTable.PlotArea.XAxis.AxisCrossingValue = 0
                '    rbcFuturTable.PlotArea.XAxis.MinorGridLines.Visible = False
                '    rbcFuturTable.PlotArea.XAxis.MinorGridLines.Visible = False
                '    For Each row In ds.Tables(0).Rows
                '        rbcFuturTable.PlotArea.XAxis.Items.Add(row("tableNo"))

                '        'Dim dv As DataView = ds.Tables(2).DefaultView
                '        'dv.RowFilter = "TableNo = " + row("tableNo").ToString
                '        'Dim dtFil As DataTable = dv.ToTable

                '        'Dim series1 As New RangeBarSeries()
                '        'If dtFil.Rows.Count > 0 Then

                '        '    For i = 0 To dtFil.Rows.Count - 1

                '        '        Dim sri As New SeriesItem

                '        '        Dim series11 As New RangeSeriesItem
                '        '        series1.Name = dtFil.Rows(i)("TableNo")
                '        '        series1.TooltipsAppearance.Visible = False
                '        '        series1.LabelsAppearance.Visible = True
                '        '        series1.LabelsAppearance.Position = HtmlChart.BarColumnLabelsPosition.Center
                '        '        series1.LabelsAppearance.ClientTemplate = dtFil.Rows(i)("TableNo")
                '        '        series1.LabelsAppearance.FromLabelsAppearance.Visible = False
                '        '        series1.SeriesItems.Add(New RangeSeriesItem(Convert.ToDecimal(dtFil.Rows(i)("start_time")), Convert.ToDecimal(dtFil.Rows(i)("end_time"))))

                '        '        rbcFuturTable.PlotArea.Series.Add(series1)


                '        '    Next

                '        'End If
                '    Next

                '    'Dim sri As New SeriesItem

                '    Dim series1 As New RangeBarSeries()
                '    If ds.Tables(2).Rows.Count > 0 Then

                '        For i = 0 To ds.Tables(2).Rows.Count - 1

                '            Dim sri As New SeriesItem

                '            Dim series11 As New RangeSeriesItem
                '            series1.Name = ds.Tables(2).Rows(i)("TableNo")
                '            series1.TooltipsAppearance.Visible = False
                '            series1.LabelsAppearance.Visible = True
                '            series1.LabelsAppearance.Position = HtmlChart.BarColumnLabelsPosition.Center
                '            series1.LabelsAppearance.ClientTemplate = ds.Tables(2).Rows(i)("TableNo")
                '            series1.LabelsAppearance.FromLabelsAppearance.Visible = False
                '            series1.SeriesItems.Add(New RangeSeriesItem(Convert.ToDecimal(ds.Tables(2).Rows(i)("start_time")), Convert.ToDecimal(ds.Tables(2).Rows(i)("end_time"))))

                '            rbcFuturTable.PlotArea.Series.Add(series1)


                '        Next

                '    End If
                '    rbcFuturTable.DataBind()
                '    'rbcFuturTable.PlotArea.YAxis.MinValue = 0
                '    'rbcFuturTable.PlotArea.YAxis.MaxValue = 24
                '    'rbcFuturTable.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0}:00"
                '    'rbcFuturTable.PlotArea.YAxis.MinorGridLines.Visible = False
                '    'rbcFuturTable.DataBind()

                '    'rbcFuturTable.DataSource = ds.Tables(0)
                '    'rbcFuturTable.DataBind()
                '    'ViewState("Slot_list") = ds.Tables(1)
                '    'ViewState("booking_list") = ds.Tables(2)
                'End If
            Catch ex As Exception

            End Try
        Else
            chart.Visible = False
        End If
    End Sub

    Private Sub BindRepeater()

        divMessageBox.Visible = False

        lblTablesOf.Text = ddlVenue.SelectedText
        Dim dt As DataTable = Common.SearchTableGetStore(Convert.ToDateTime(dtpDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"), "01:00", ddlVenue.SelectedValue, ddlType.SelectedValue)
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
        Dim conn As DBConnection = New DBConnection()
        Dim str As String = "select One_booking_at_a_time,MinCovers from BookingSchedule where BookingScheduleID = '" + ddlType.SelectedValue.ToString() + "'"
        Dim ds As DataSet = conn.SelectData(str)
        If (ds.Tables(0).Rows.Count > 0) Then
            If ds.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" And Not ds.Tables(0).Rows(0)("MinCovers").ToString() = 0 Then
                Dim min As Integer = ds.Tables(0).Rows(0)("MinCovers").ToString()
                If txtNoOfCovers.Text < min Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Minimum " + min.ToString() + " Covers Required.');", True)
                Else
                    BindRepeater()
                End If
            Else
                BindRepeater()
            End If
        Else
            BindRepeater()
        End If
        Bindchart()
    End Sub

    Protected Sub repTables_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repTables.ItemDataBound
        Try
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

                Dim ds As New DataSet()
                Dim oClsDataccess As New ClsDataccess
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                oColSqlPar.Add("@BbookingDate", Convert.ToDateTime(dtpDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"), SqlDbType.DateTime)
                oColSqlPar.Add("@BSettingsID", settingsId)
                oColSqlPar.Add("@BcurrentCover", noOfCovers)
                oColSqlPar.Add("@BBookingScheduleID", Utils.ParseInt(ddlType.SelectedValue))
                oColSqlPar.Add("@BCurrBookingRefNo", String.Empty)
                oColSqlPar.Add("@BIsOnline", IsOnline)
                ds = oClsDataccess.GetdatasetSp("SearchTableGetTimeSlots", oColSqlPar, "SearchTableGetTimeSlots")

                repTimeSlot.DataSource = ds.Tables(0)
                repTimeSlot.DataBind()

                'repTimeSlot.DataSource = Common.SearchTableGetTimeSlots(Convert.ToDateTime(dtpDate.SelectedDate.ToString()).ToString("yyyy-MM-dd"), settingsId, noOfCovers, Utils.ParseInt(ddlType.SelectedValue), String.Empty, IsOnline)
                'repTimeSlot.DataBind()

                'Dim str As String = "select One_booking_at_a_time from BookingSchedule where BookingScheduleID = '" + ddlType.SelectedValue.ToString() + "'"
                'Dim ds As DataSet = conn.SelectData(str)
                'If (ds.Tables(0).Rows.Count > 0) Then
                '    If ds.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" Then
                '        Dim strqry As String = "select * from bookings Where BookingScheduleid = " + ddlType.SelectedValue.ToString() + " and BookingScheduleDateId = " + Common.GetBookingScheduleDateId(dtpDate.SelectedDate, Utils.ParseInt(ddlType.SelectedValue)).ToString() + " and date = '" + dtpDate.SelectedDate & "'"
                '        Dim dsa As DataSet = conn.SelectData(strqry)
                '        If Not (dsa.Tables(0).Rows.Count = 0) Then
                '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Booking is not available on selected schedule, kindly change your schedule');", True)
                '        End If
                '    Else
                '        'Response.Redirect("BookingDetailTable.aspx?storeid=" & storeId & "&settingid=" & settingId & "&datetime=" & DateTime & "&covers=" & noOfCovers & "&bookingScheduleId=" & bookingScheduleId & "&bookingScheduleDateId=" & bookingScheduleDateId & "&isonline=" & intIsOnline) '& "&venue=" & venue & "&time=" & time
                '    End If
                'End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub repTimeSlot_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)

        If (e.CommandName = "BookTable") Then
            Dim conn As DBConnection = New DBConnection()
            Dim str As String = "select One_booking_at_a_time,MinCovers from BookingSchedule where BookingScheduleID = '" + ddlType.SelectedValue.ToString() + "'"
            Dim ds As DataSet = conn.SelectData(str)
            If (ds.Tables(0).Rows.Count > 0) Then
                If ds.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" And Not ds.Tables(0).Rows(0)("MinCovers").ToString() = 0 Then
                    Dim min As Integer = ds.Tables(0).Rows(0)("MinCovers").ToString()
                    If txtNoOfCovers.Text < min Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Minimum " + min.ToString() + " Covers Required.');", True)
                    Else
                        BookTable(source, e)
                    End If
                Else
                    BookTable(source, e)
                End If
            Else
                BookTable(source, e)
            End If

        End If

    End Sub

    Private Sub BookTable(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        Dim storeId As String

        If IsOnline Then
            storeId = Utils.Encrypt(ActualStoreID.ToString())
        Else
            storeId = Utils.Encrypt(DirectCast(source.Parent.FindControl("hdnStoreId"), HiddenField).Value).ToString()
        End If

        Dim settingId As String = Utils.Encrypt(DirectCast(source.Parent.FindControl("hdnSettingId"), HiddenField).Value).ToString()
        'String.Format("{0:dd/MM/yyyy}", dtpDate.SelectedDate)
        Dim dateTime As String = Utils.Encrypt(dtpDate.SelectedDate & " " & e.CommandArgument.ToString()).ToString()

        Session("dtp_Date") = Format(dtpDate.SelectedDate, "dd/MM/yyyy") & " " & e.CommandArgument.ToString()

        Dim bookingScheduleId As String = Utils.Encrypt(ddlType.SelectedValue).ToString()
        Dim bookingScheduleDateId As String = Utils.Encrypt(Common.GetBookingScheduleDateId(dtpDate.SelectedDate, Utils.ParseInt(ddlType.SelectedValue)))
        'Dim time As String = Utils.Encrypt(Utils.ParseInt(ddlType.SelectedValue))
        'Dim venue As String = Utils.Encrypt(Utils.ParseInt(ddlVenue.SelectedValue))

        Dim noOfCovers As String
        'If (ddlNoOfCovers.SelectedValue = "-1") Then
        noOfCovers = Utils.Encrypt(txtNoOfCovers.Text).ToString()
        'Else
        '    noOfCovers = Utils.Encrypt(ddlNoOfCovers.SelectedItem.Value).ToString()
        'End If
        Dim intIsOnline As Int32 = 0
        If IsOnline Then
            intIsOnline = 1
        End If

        Response.Redirect("BookingDetailTable.aspx?storeid=" & storeId & "&settingid=" & settingId & "&datetime=" & dateTime & "&covers=" & noOfCovers & "&bookingScheduleId=" & bookingScheduleId & "&bookingScheduleDateId=" & bookingScheduleDateId & "&isonline=" & intIsOnline) '& "&venue=" & venue & "&time=" & time

        'Dim str As String = "select One_booking_at_a_time from BookingSchedule where BookingScheduleID = '" + ddlType.SelectedValue.ToString() + "'"
        'Dim ds As DataSet = conn.SelectData(str)
        'If (ds.Tables(0).Rows.Count > 0) Then
        '    If ds.Tables(0).Rows(0)("One_booking_at_a_time").ToString() = "1" Then
        '        Dim strqry As String = "select * from bookings Where BookingScheduleid = " + ddlType.SelectedValue.ToString() + " and BookingScheduleDateId = " + Common.GetBookingScheduleDateId(dtpDate.SelectedDate, Utils.ParseInt(ddlType.SelectedValue)).ToString() + " and date = '" + dtpDate.SelectedDate & " " & e.CommandArgument.ToString() + "'"
        '        Dim dsa As DataSet = conn.SelectData(strqry)
        '        If (dsa.Tables(0).Rows.Count = 0) Then
        '            Response.Redirect("BookingDetailTable.aspx?storeid=" & storeId & "&settingid=" & settingId & "&datetime=" & dateTime & "&covers=" & noOfCovers & "&bookingScheduleId=" & bookingScheduleId & "&bookingScheduleDateId=" & bookingScheduleDateId & "&isonline=" & intIsOnline) '& "&venue=" & venue & "&time=" & time
        '        Else
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Booking is not available on selected schedule, kindly change your schedule');", True)
        '        End If
        '    Else

        '    End If
        'End If
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
        Try
            Dim obj As Common = New Common()
            If ddlVenue.Items.Count > 0 Then
                Dim ds As DataSet = obj.GetTableType(ddlVenue.SelectedValue, dtpDate.SelectedDate)
                ddlType.Items.Clear()
                If ds IsNot Nothing Then
                    If ds.Tables.Count > 0 Then
                        ddlType.DataSource = ds.Tables(0)
                        ddlType.DataTextField = "Name"
                        ddlType.DataValueField = "Id"
                        ddlType.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlVenue_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlVenue.SelectedIndexChanged
        BindType()
        BindMaxCovers()
        Bindchart()
    End Sub

    Protected Sub dtpDate_SelectedDateChanged(ByVal sender As Object, ByVal e As SelectedDateChangedEventArgs) Handles dtpDate.SelectedDateChanged
        BindType()
        BindMaxCovers()
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

    Protected Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlType.SelectedIndexChanged
        BindMaxCovers()
        Bindchart()
    End Sub

    Protected Sub rdpFTDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdpFTDate.SelectedDateChanged
        Bindchart()
    End Sub

    Protected Sub imgbtnRefreshChart_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRefreshChart.Click
        Bindchart()
    End Sub
End Class


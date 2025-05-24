Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_BookingScheduleDate
    Inherits System.Web.UI.Page

    Private Property BookingScheduleID() As Integer
        Get
            Try
                Return Utils.getInteger(Session("BookingScheduleID"))
            Catch
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Session("BookingScheduleID") = value
        End Set
    End Property

    Private Property StartDate() As DateTime
        Get

            Return Utils.ParseDateTime(Session("StartDate"))

        End Get
        Set(ByVal value As DateTime)
            Session("StartDate") = value
        End Set
    End Property

    Private Property EndDate() As DateTime
        Get

            Return Utils.ParseDateTime(Session("EndDate"))

        End Get
        Set(ByVal value As DateTime)
            Session("EndDate") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Header.DataBind()
        If (Not Page.IsPostBack) Then
            If Request.QueryString("BookingScheduleID") IsNot Nothing AndAlso Request.QueryString("StartDate") IsNot Nothing AndAlso Request.QueryString("EndDate") IsNot Nothing Then
                BookingScheduleID = Convert.ToInt32(Request.QueryString("BookingScheduleID"))
                StartDate = Utils.ParseDateTime(Request.QueryString("StartDate"))
                EndDate = Utils.ParseDateTime(Request.QueryString("EndDate"))
                lblHdr.Text = "Schedule Period From " & StartDate.ToString("dd/MM/yyyy") & " To " & EndDate.ToString("dd/MM/yyyy")
                BindYears()
                BindMonths()
            End If
        End If
    End Sub

    Private Sub BindYears()
        'Dim noOfYear As Int32 = ((EndDate - StartDate).TotalDays) / 365
        Dim startYear As Int32 = StartDate.Year
        Dim endYear As Int32 = EndDate.Year
        For i = startYear To endYear
            drpYear.Items.Add(New Telerik.Web.UI.DropDownListItem(i.ToString(), i.ToString()))
        Next
    End Sub

    Private Sub BindMonths()

        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("January", 1))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("February", 2))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("March", 3))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("April", 4))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("May", 5))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("June", 6))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("July", 7))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("August", 8))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("September", 9))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("October", 10))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("November", 11))
        drpMonth.Items.Add(New Telerik.Web.UI.DropDownListItem("December", 12))

    End Sub

    Private Sub BindScheduleDate()
        Dim year As Int16 = Utils.ParseInt(drpYear.SelectedValue)
        If year > 0 Then
            Dim month As Int16 = Utils.ParseInt(drpMonth.SelectedValue)
            If month > 0 Then
                Dim sDate As Date = New Date(year, month, 1)
                Dim eDate As Date = sDate.AddMonths(1).AddDays(-1)
                If (sDate >= StartDate Or eDate <= EndDate) Then
                    eDate = eDate.AddSeconds(-1)
                    If sDate < StartDate Then
                        sDate = StartDate
                    End If

                    If eDate > EndDate Then
                        eDate = EndDate
                    End If
                    Dim objCommon As Common = New Common()
                    Dim dt As DataTable = objCommon.GetScheduleDateData(BookingScheduleID, sDate, eDate)
                    If dt.Rows.Count > 0 Then
                        gvDates.DataSource = dt
                        gvDates.DataBind()
                        pnlDate.Visible = True
                        pnlMessage.Visible = False
                    Else
                        pnlDate.Visible = False
                        pnlMessage.Visible = True
                    End If
                Else
                    ' No Time Period Exists
                    pnlDate.Visible = False
                    pnlMessage.Visible = True
                End If


            End If
        End If
    End Sub

    Protected Sub drpYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles drpYear.SelectedIndexChanged
        BindScheduleDate()
    End Sub

    Protected Sub drpMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles drpMonth.SelectedIndexChanged
        BindScheduleDate()
    End Sub


    Protected Sub gvDates_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles gvDates.RowEditing
        Me.gvDates.EditIndex = e.NewEditIndex
        BindScheduleDate()
    End Sub

    Protected Sub gvDates_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles gvDates.RowCancelingEdit
        Me.gvDates.EditIndex = -1
        BindScheduleDate()
    End Sub

    Protected Sub gvDates_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvDates.RowUpdating
        If Page.IsValid Then
            Dim index As Int32 = e.RowIndex

            Dim BookingScheduleDateId As Int32 = DirectCast(gvDates.DataKeys(index).Value, Int32)

            Dim StartTime As TimeSpan = DirectCast(gvDates.Rows(index).FindControl("tpStartTime"), Telerik.Web.UI.RadTimePicker).SelectedTime.Value
            Dim EndTime As TimeSpan = DirectCast(gvDates.Rows(index).FindControl("tpEndTime"), Telerik.Web.UI.RadTimePicker).SelectedTime.Value
            Dim NoOfCover As Int32 = Utils.ParseInt(DirectCast(gvDates.Rows(index).FindControl("txtNoOfCover"), Telerik.Web.UI.RadNumericTextBox).Text)
            Dim serviceDuration As Int32 = Utils.ParseInt(DirectCast(gvDates.Rows(index).FindControl("txtServiceDuration"), Telerik.Web.UI.RadNumericTextBox).Text)
            Dim timeSpan As Int32 = Utils.ParseInt(DirectCast(gvDates.Rows(index).FindControl("txtTimeSpan"), Telerik.Web.UI.RadNumericTextBox).Text)
            Dim isAvailable As Boolean = DirectCast(gvDates.Rows(index).FindControl("chkIsAvailable"), CheckBox).Checked
            Dim paymentType As Int32 = Utils.ParseInt(DirectCast(gvDates.Rows(index).FindControl("drpPaymentType"), Telerik.Web.UI.RadDropDownList).SelectedValue)
            Dim dPercent As Decimal = Utils.ParseDecimal(DirectCast(gvDates.Rows(index).FindControl("txtPercentage"), Telerik.Web.UI.RadNumericTextBox).Text)
            Dim dAmt As Decimal = Utils.ParseDecimal(DirectCast(gvDates.Rows(index).FindControl("txtAmount"), Telerik.Web.UI.RadNumericTextBox).Text)


            Dim strQuery As String = " "
            strQuery &= " UPDATE BookingScheduleDate"
            strQuery &= " SET IsAvailable = " & If(isAvailable, 1, 0) & ","
            strQuery &= " StartTime = '" & StartTime.ToString() & "',"
            strQuery &= " EndTime = '" & EndTime.ToString() & "',"
            strQuery &= " NumberOfCover = " & NoOfCover & ","
            strQuery &= " ServiceDuration = " & serviceDuration & ","
            strQuery &= " TimeSpan = " & timeSpan & ","
            strQuery &= " PaymentType = " & paymentType & ","
            strQuery &= " DepositPercentage = " & dPercent & ","
            strQuery &= " DepositAmount = " & dAmt
            strQuery &= " WHERE BookingScheduleDateId = " & BookingScheduleDateId

            Dim con As DBConnection = New DBConnection()
            con.Ins_Upd_Del(strQuery)

            Me.gvDates.EditIndex = -1
            BindScheduleDate()
        End If
    End Sub

    Protected Sub gvDates_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvDates.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dr As DataRowView
            dr = TryCast(e.Row.DataItem, DataRowView)
            If e.Row.RowIndex = gvDates.EditIndex Then
                Dim drpPaymentType As RadDropDownList = TryCast(e.Row.FindControl("drpPaymentType"), RadDropDownList)
                drpPaymentType.SelectedValue = dr("PaymentType").ToString()
            Else
                Dim lblPaymentType As Label = TryCast(e.Row.FindControl("lblPaymentType"), Label)
                If dr("PaymentType").ToString() = "1" Then
                    lblPaymentType.Text = "Percentage Of Total Bill"
                ElseIf dr("PaymentType").ToString() = "2" Then
                    lblPaymentType.Text = "Open Deposit"
                ElseIf dr("PaymentType").ToString() = "3" Then
                    lblPaymentType.Text = "Deposit Amount"
                ElseIf dr("PaymentType").ToString() = "4" Then
                    lblPaymentType.Text = "Deposit Per Cover"
                End If
            End If
        End If

    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        BindScheduleDate()
    End Sub
End Class

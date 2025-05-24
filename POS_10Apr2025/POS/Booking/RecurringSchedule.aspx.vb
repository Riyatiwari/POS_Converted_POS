Imports System.Data
Imports System.Globalization

Partial Class BookingEasy_RecurringSchedule
    Inherits System.Web.UI.Page

    Private Property BookingScheduleDateId() As Integer
        Get
            Try
                Return Utils.getInteger(Session("BookingScheduleDateId"))
            Catch
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            Session("BookingScheduleDateId") = value
        End Set
    End Property

    Public Property ScheduleStartDate() As DateTime
        Get
            Return Utils.ParseDateTime(Session("ScheduleStartDate"))
        End Get
        Set(ByVal value As DateTime)
            Session("ScheduleStartDate") = value
        End Set
    End Property

    Public Property ScheduleEndDate() As DateTime
        Get
            Return Utils.ParseDateTime(Session("ScheduleEndDate"))
        End Get
        Set(ByVal value As DateTime)
            Session("ScheduleEndDate") = value
        End Set
    End Property

    Public ReadOnly Property StrScheduleStartDate() As String
        Get
            Return Utils.ParseDateTime(Session("ScheduleStartDate")).ToString("dd/MM/yyyy")
        End Get

    End Property

    Public ReadOnly Property StrScheduleEndDate() As String
        Get
            Return Utils.ParseDateTime(Session("ScheduleEndDate")).ToString("dd/MM/yyyy")
        End Get

    End Property

    Public ReadOnly Property StrOccuranceStartDate1() As String
        Get

            Dim d As DateTime
            d = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")

            Return Utils.ParseDateTime(d)
            'Return Utils.ParseDateTime(System.DateTime.Now.ToString("dd/MM/yyyy")).ToString("dd/MM/yyyy")
        End Get

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Header.DataBind()
        If (Not Page.IsPostBack) Then
            If Request.QueryString("BookingScheduleDateId") IsNot Nothing Then
                Dim objCommon As Common = New Common()
                BookingScheduleDateId = Convert.ToInt32(Request.QueryString("BookingScheduleDateId"))
                Dim drScheduleDate As DataRow = objCommon.GetBookingScheduleDateByID(BookingScheduleDateId)
                Dim drBookingSchedule As DataRow = objCommon.GetBookingScheduleByID(Utils.ParseInt(drScheduleDate("BookingScheduleId")))
                ScheduleStartDate = Utils.ParseDateTime(drBookingSchedule("StartDate"))
                ScheduleEndDate = Utils.ParseDateTime(drBookingSchedule("EndDate"))
                'dtOccuStartFrom.Text = StrScheduleStartDate
                dtFrom.Text = StrScheduleStartDate
                dtTo.Text = StrScheduleEndDate
                ViewState("EndDateDays") = DateDiff(DateInterval.Day, ScheduleStartDate, ScheduleEndDate)
                lblStartTime.Text = " Start Time : " & drScheduleDate("StartTime").ToString()
                lblEndTime.Text = " End Time : " & drScheduleDate("EndTime").ToString()
                lblNoOfCover.Text = " No Of Cover : " & drScheduleDate("NumberOfCover").ToString()
                lblServiceDuration.Text = " Service Duration (In Minutes) : " & drScheduleDate("ServiceDuration").ToString()
                lblTimeSpan.Text = " TimeSpan (In Minutes) : " & drScheduleDate("TimeSpan").ToString()

                Dim d As DateTime
                d = System.DateTime.Now
                dtOccuStartFrom.Text = Convert.ToDateTime(d).ToString("dd/MM/yyyy")

            End If
        End If
    End Sub

    Protected Sub btnRecurring_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecurring.Click
        Dim RecurringType As Int32 = 0
        Dim startDate, endDate As DateTime
        Dim noOfOccurance As Int32
        Dim IsOccurance As Boolean
        Dim IsAvailable As Integer = 1

        If rdoDaily.Checked Then
            RecurringType = 1       '' Daily
        ElseIf rdoWeekly.Checked Then
            RecurringType = 2       '' Weekly
        ElseIf rdoMonthly.Checked Then
            RecurringType = 3       '' Monthly
        ElseIf rdoYearly.Checked Then
            RecurringType = 4       '' Yearly
        End If

        If rdoDateRange.Checked Then

            Dim dt1 As DateTime = DateTime.ParseExact(dtOccuStartFrom.Text, "dd/MM/yyyy", Nothing)

            startDate = dt1

            'startDate = Utils.ParseDateTime(dtFrom.Text)
            endDate = startDate.AddDays(ViewState("EndDateDays"))

            IsOccurance = False

        ElseIf rdoOccurance.Checked Then

            IsOccurance = True

            If (txtOccurance.Text > 0) Then

                noOfOccurance = Utils.ParseInt(txtOccurance.Text)

                Dim dt2 As DateTime = DateTime.ParseExact(dtOccuStartFrom.Text, "dd/MM/yyyy", Nothing)
                startDate = dt2


            Else

                'Dim d As DateTime
                'd = System.DateTime.Now.ToString("yyyy/MM/dd")

                Dim dt2 As DateTime = DateTime.ParseExact(dtOccuStartFrom.Text, "dd/MM/yyyy", Nothing)

                noOfOccurance = DateDiff(DateInterval.Day, dt2, ScheduleEndDate)

                'startDate = Utils.ParseDateTime(dtOccuStartFrom.Text)

                startDate = dt2

                'noOfOccurance = DateDiff(DateInterval.Day, d, ScheduleEndDate)
                'd = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            End If

        End If
        Dim lstDays As List(Of Int32) = New List(Of Int32)
        If RecurringType = 2 Then
            If chkSunday.Checked Then
                lstDays.Add(CInt(DayOfWeek.Sunday))
                IsAvailable = 0
            End If
            If chkMonday.Checked Then
                lstDays.Add(CInt(DayOfWeek.Monday))
                IsAvailable = 0
            End If
            If chkTuesday.Checked Then
                lstDays.Add(CInt(DayOfWeek.Tuesday))
                IsAvailable = 0
            End If
            If chkWednesday.Checked Then
                lstDays.Add(CInt(DayOfWeek.Wednesday))
                IsAvailable = 0
            End If
            If chkThursday.Checked Then
                lstDays.Add(CInt(DayOfWeek.Thursday))
                IsAvailable = 0
            End If
            If chkFriday.Checked Then
                lstDays.Add(CInt(DayOfWeek.Friday))
                IsAvailable = 0
            End If
            If chkSaturday.Checked Then
                lstDays.Add(CInt(DayOfWeek.Saturday))
                IsAvailable = 0
            End If
        End If
        Try

            Common.UpdateRecurringSchedule(RecurringType, startDate, endDate, noOfOccurance, IsOccurance, BookingScheduleDateId, lstDays, IsAvailable)
            lblMessage.Text = "Recurring Schedule Created Successfully."
        Catch ex As Exception
            LogHelper.Error("RecurringSchedule:btnRecurring_Click" + ex.Message)
        End Try
    End Sub

    Private Sub BindData()

    End Sub
End Class

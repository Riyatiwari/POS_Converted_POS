
Imports AuthorizeNet
Imports System.Security.Cryptography 'required for fingerprint calculation
Imports System.Data
Imports System.Net.Mail
Imports System.IO

Partial Public Class Booking_Confirm
    Inherits System.Web.UI.Page


    'Dim conn As DBConnection = New DBConnection()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            Try
                BindDetails()
                AmountBind()
                BindRadioList()
            Catch ex As Exception

            End Try
        End If
        If Session("isonline") = "1" Then


            Dim aNavbarHome As HtmlAnchor = CType(Master.FindControl("aNavbarHome"), HtmlAnchor)

            If aNavbarHome IsNot Nothing Then

                aNavbarHome.Attributes.Remove("href")
                aNavbarHome.Attributes.Add("style", "pointer-events:none;")
            End If
        End If
        'Try
        '    If Request.QueryString("br") IsNot Nothing Then
        '        BindDetails()
        '        AmountBind()
        '        'BindRadioList()
        '    End If
        'Catch ex1 As Exception

        'End Try

    End Sub

    Private Sub BindRadioList()
        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData("Select paymentid,Gateway from M_Payment_Gateway where Status = 1")
        If Not ds Is Nothing Then
            If ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                Dim DepositAmount As String = "0.00"
                lblRequiredAmount.Text = DepositAmount
                payNow.Visible = False
                ConfirmNow.Visible = True
                radioField.Visible = False
            Else
                rblPaymentGateway.DataSource = ds
                rblPaymentGateway.DataTextField = "Gateway"
                rblPaymentGateway.DataValueField = "Gateway"
                rblPaymentGateway.DataBind()
                rblPaymentGateway.Items(0).Selected = True
            End If

        End If
    End Sub

    Private Sub AmountBind()
        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData("Select * from bookingSchedule where bookingScheduleId=" + bookingScheduleId().ToString)
        Dim PaymentType As String = ""
        Dim PayAmount As Double = 0
        Dim DepositAmount As Double = 0

        If Val(ds.Tables(0).Rows(0)("PaymentType")) = 1 Then
            Dim field1 As String = CType(Session.Item("WidgetVal"), String)
            If field1 = "1" Then
                deposit_lbl.Visible = True
                deposit_txt.Visible = False
                DepositAmount = (Session("amont_final") * Val(ds.Tables(0).Rows(0)("DepositPercentage"))) / 100
                If DepositAmount = "0" Then
                    payNow.Visible = False
                    ConfirmNow.Visible = True
                    radioField.Visible = False
                Else
                    payNow.Visible = True
                    ConfirmNow.Visible = False
                End If
            ElseIf field1 = "0" Then
                deposit_lbl.Visible = False
                deposit_txt.Visible = True
                DepositAmount = (Session("amont_final") * Val(ds.Tables(0).Rows(0)("DepositPercentage"))) / 100
                radioField.Visible = False
                payNow.Visible = False
                radioField.Visible = False
                ConfirmNow.Visible = True
                txtDeposit.Text = DepositAmount.ToString("N2")
                txtDeposit.ReadOnly = False
                txtDeposit.Focus()
            End If
        ElseIf Val(ds.Tables(0).Rows(0)("PaymentType")) = 2 Then
            Dim field1 As String = CType(Session.Item("WidgetVal"), String)
            If field1 = "1" Then
                deposit_lbl.Visible = True
                deposit_txt.Visible = False
                DepositAmount = "0.00"
                radioField.Visible = False
            ElseIf field1 = "0" Then
                deposit_lbl.Visible = False
                deposit_txt.Visible = True
                txtDeposit.ReadOnly = False
                txtDeposit.Text = "0.00"
                txtDeposit.Focus()
                DepositAmount = Session("amont_final")
                radioField.Visible = False
            End If
            payNow.Visible = False
            ConfirmNow.Visible = True
        ElseIf Val(ds.Tables(0).Rows(0)("PaymentType")) = 3 Then
            Dim field1 As String = CType(Session.Item("WidgetVal"), String)
            If field1 = "1" Then
                deposit_lbl.Visible = True
                deposit_txt.Visible = False
                DepositAmount = Val(ds.Tables(0).Rows(0)("DepositAmount"))
                If DepositAmount = "0" Then
                    payNow.Visible = False
                    ConfirmNow.Visible = True
                    radioField.Visible = False
                Else
                    payNow.Visible = True
                    ConfirmNow.Visible = False
                    radioField.Visible = True
                End If
            ElseIf field1 = "0" Then
                deposit_lbl.Visible = False
                deposit_txt.Visible = True
                DepositAmount = Val(ds.Tables(0).Rows(0)("DepositAmount"))
                txtDeposit.Text = DepositAmount
                txtDeposit.ReadOnly = False
                txtDeposit.Focus()
                payNow.Visible = False
                ConfirmNow.Visible = True
                radioField.Visible = False
            End If
        Else
            Dim field1 As String = CType(Session.Item("WidgetVal"), String)
            If field1 = "1" Then
                Dim no As Integer = lblNoOfCovers.Text
                deposit_lbl.Visible = True
                deposit_txt.Visible = False
                Dim no1 As Integer = Val(ds.Tables(0).Rows(0)("DepositAmount"))
                DepositAmount = (no1 * no)
                If DepositAmount = "0" Then
                    payNow.Visible = False
                    ConfirmNow.Visible = True
                    radioField.Visible = False
                Else
                    payNow.Visible = True
                    ConfirmNow.Visible = False
                    radioField.Visible = True
                End If
            Else
                Dim no As Integer = lblNoOfCovers.Text
                deposit_lbl.Visible = False
                deposit_txt.Visible = True
                radioField.Visible = False
                Dim no1 As Integer = Val(ds.Tables(0).Rows(0)("DepositAmount"))
                DepositAmount = (no1 * no)
                txtDeposit.Text = DepositAmount
                txtDeposit.ReadOnly = False
                txtDeposit.Focus()
                payNow.Visible = False
                ConfirmNow.Visible = True
            End If

        End If
        lblTotalPrice.Text = Val(Session("amont_final")).ToString("n2")
        lblRequiredAmount.Text = DepositAmount.ToString("n2")
    End Sub

    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("br")) Then
                Return Utils.Decrypt(Request.QueryString("br")).ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property bookingScheduleId() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("bs")) Then
                Return Utils.Decrypt(Request.QueryString("bs")).ToString()
            Else
                Return String.Empty
            End If
        End Get
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

    Private Sub BindDetails()
        Dim objCommon As Common = New Common()
        Dim drBooking As DataRow = objCommon.GetBookingDetailByRefInitiate(BookingRef())
        lblFullName.Text = drBooking("FirstName").ToString() + " " + drBooking("LastName").ToString()
        lblEmail.Text = drBooking("Email1st").ToString()
        lblMobile.Text = drBooking("Mobile").ToString()
        lblBookingRef.Text = BookingRef
        lblName.Text = drBooking("Name").ToString()

        Dim bDate As DateTime = Convert.ToDateTime(drBooking("date").ToString())
        Dim bTime As TimeSpan = TimeSpan.Parse(drBooking("bookingtime").ToString())
        lblBookingDate.Text = bDate.ToString("dd/MM/yyyy")
        lblBookingTime.Text = String.Format("{0:hh\:mm}", bTime)
        lblComment.Text = drBooking("comment").ToString()
        lblNoOfCovers.Text = drBooking("covers").ToString()


        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())

        Session("amont_final") = "0"
        Session("description_final") = ""

        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
            rptServices.DataSource = dsServices
            rptServices.DataBind()
            rptServices.Visible = True
        Else
            rptServices.Visible = False
        End If



        hdnStoreID.Value = drBooking("StoreID").ToString()

        btnChangeTable.HRef = "PopupBookingConfirmEdit.aspx?bookingref=" + BookingRef + "&bs=" + Utils.Encrypt(bookingScheduleId()).ToString
    End Sub

    Protected Sub rptServices_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptServices.ItemDataBound
        Try
            Session("amont_final") = Val(Session("amont_final")) + (Val(CType(e.Item.FindControl("hfTotalPrice"), HiddenField).Value))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub payNow_Click(sender As Object, e As System.EventArgs) Handles payNow.Click
        'Dim sqlqry As String = "Select * fom M_Payment_Gateway where PaymentID = '" & rblPaymentGateway.SelectedValue & "'"
        If rblPaymentGateway.SelectedValue = "Authorize.net" Then
            Response.Redirect("Booking_Confirm_Process.aspx?amount=" + lblRequiredAmount.Text.ToString + "&User_id=" + BookingRef())
        ElseIf rblPaymentGateway.SelectedValue = "Paypal" Then
            Response.Redirect("Booking_Confirm_Process_Paypal.aspx?amount=" + lblRequiredAmount.Text.ToString + "&User_id=" + BookingRef())
        ElseIf rblPaymentGateway.SelectedValue = "FirstPayment" Then
            Session("amountReceived") = lblRequiredAmount.Text.ToString
            Response.Redirect("Booking_Confirm_Process_FirstPayment.aspx?amount=" + lblRequiredAmount.Text.ToString + "&User_id=" + BookingRef())
        Else
            Response.Redirect("PageNotFound.aspx")
        End If
    End Sub

    Protected Sub ConfirmNow_Click(sender As Object, e As System.EventArgs) Handles ConfirmNow.Click
        Dim field1 As String = CType(Session.Item("WidgetVal"), String)
        If field1 = "1" Then
            Response.Redirect("TableBookConfimeZeroPayment.aspx?BR=" + Utils.Encrypt(BookingRef()).ToString + "&DA=" + Utils.Encrypt(lblRequiredAmount.Text))
        ElseIf field1 = "0" Then
            Response.Redirect("TableBookConfimeZeroPayment.aspx?BR=" + Utils.Encrypt(BookingRef()).ToString + "&DA=" + Utils.Encrypt(txtDeposit.Text))
        End If
    End Sub
End Class
Imports System.Data
Imports System.Net.Mail
Imports System.IO
Imports AuthorizeNet
Imports System.Security.Cryptography 'required for fingerprint calculation

Partial Class BookingEasy_RoomBookConfime
    Inherits System.Web.UI.Page
    'Dim conn As DBConnection = New DBConnection()

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

    'Public Property TypeID() As String
    '    Get
    '        If (Not Request.QueryString("TypeID") Is Nothing) Then
    '            Return Request.QueryString("TypeID")
    '        End If
    '        Return 1
    '    End Get
    '    Set(ByVal value As String)
    '        Request.QueryString("TypeID") = value
    '    End Set
    'End Property

    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("BookingRef")) Then
                Return Request.QueryString("BookingRef").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("AllowSetting") = Nothing
        If Not IsPostBack Then
            If (Not String.IsNullOrEmpty(BookingRef)) Then
                Session("amount_final_room_new") = Val(Session("amount_final_room"))
                BindDetails()
                AmountBind()
                BindRadioList()
            Else
                Response.Redirect("~/Login.aspx")
            End If
        End If
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
        Dim ds As DataSet = conn.SelectData("Select * from bookingSchedule where bookingScheduleId=" + Request.QueryString("TypeID"))
        Dim PaymentType As String = ""
        Dim PayAmount As Double = 0
        Dim DepositAmount As Double = 0

        If Val(ds.Tables(0).Rows(0)("PaymentType")) = 1 Then '%
            Dim field1 As String = CType(Session.Item("WidgetVal"), String)
            If field1 = "1" Then
                deposit_lbl.Visible = True
                deposit_txt.Visible = False
                DepositAmount = (Session("amount_final_room_new") * Val(ds.Tables(0).Rows(0)("DepositPercentage"))) / 100
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
                DepositAmount = (Session("amount_final_room_new") * Val(ds.Tables(0).Rows(0)("DepositPercentage"))) / 100
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
                txtDeposit.Focus()
                DepositAmount = Session("amount_final_room_new")
                radioField.Visible = False
            End If
            payNow.Visible = False
            ConfirmNow.Visible = True
        ElseIf Val(ds.Tables(0).Rows(0)("PaymentType")) = 3 Then
            Dim field1 As String = CType(Session.Item("WidgetVal"), String)
            If field1 = "1" Then
                deposit_lbl.Visible = True
                deposit_txt.Visible = False
                DepositAmount = Val(ds.Tables(0).Rows(0)("DepositAmount")).ToString("N2")
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
                DepositAmount = Val(ds.Tables(0).Rows(0)("DepositAmount")).ToString("N2")
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
                'Dim no As Integer = lblNoOfCovers.Text
                deposit_lbl.Visible = True
                deposit_txt.Visible = False
                Dim no1 As Double = Val(Session("amount_final_room_new")).ToString("N2")
                DepositAmount = no1
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
                'Dim no As Integer = lblNoOfCovers.Text
                deposit_lbl.Visible = False
                deposit_txt.Visible = True
                radioField.Visible = False
                Dim no1 As Double = Val(Session("amount_final_room_new"))
                DepositAmount = no1
                txtDeposit.Text = DepositAmount
                txtDeposit.ReadOnly = False
                txtDeposit.Focus()
                payNow.Visible = False
                ConfirmNow.Visible = True
            End If

        End If
        lblTotalPrice.Text = Val(Session("amount_final_room")).ToString("n2")
        lblTotalAmount.Text = Val(Session("amount_final_room_new")).ToString("n2")
        lblRequiredAmount.Text = DepositAmount.ToString("n2")
    End Sub

    Private Sub BindDetails()
        Dim objCommon As Common = New Common()
        Dim drBooking As DataRow = objCommon.GetBookingDetailByRefInitiate(BookingRef())
        lblFullName.Text = drBooking("FirstName").ToString() + " " + drBooking("LastName").ToString()
        lblEmail.Text = drBooking("Email1st").ToString()
        lblMobile.Text = drBooking("Mobile").ToString()
        lblBookingRef.Text = BookingRef
        lblName.Text = drBooking("Name").ToString()
        Dim arrival As DateTime = Convert.ToDateTime(drBooking("arrivaldate").ToString())
        Dim depature As DateTime = Convert.ToDateTime(drBooking("departuredate").ToString())
        lblNumberOfNights.Text = DateDiff(DateInterval.Day, arrival, depature).ToString()
        lblArrival.Text = arrival.ToString("dd/MM/yyyy")
        lblDepature.Text = depature.ToString("dd/MM/yyyy")
        Session("description_final") = drBooking("Name").ToString()
        Session("amount_final_room_new") = drBooking("bookingtotal").ToString()

        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())

        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
            rptServices.DataSource = dsServices
            rptServices.DataBind()
            rptServices.Visible = True
        Else
            rptServices.Visible = False
        End If
        'lblDeposite.Text = drBooking("deposit").ToString()
        lblTotalPrice.Text = Session("amount_final_room_new").ToString()
        lblComment.Text = drBooking("comment").ToString()
        lblRoomType.Text = objCommon.GetRoomTypeName(drBooking("Roomid").ToString())
        hdnStoreID.Value = drBooking("StoreID").ToString()
    End Sub

    Protected Sub rptServices_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptServices.ItemDataBound
        Try
            Session("amount_final_room_new") = Val(Session("amount_final_room_new")) + (Val(CType(e.Item.FindControl("hfTotalPrice"), HiddenField).Value))
            Session("description_final") += ", " & CType(e.Item.FindControl("hfDesc"), HiddenField).Value
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub payNow_Click(sender As Object, e As System.EventArgs) Handles payNow.Click
        'Dim sqlqry As String = "Select * fom M_Payment_Gateway where PaymentID = '" & rblPaymentGateway.SelectedValue & "'"
        Session("amountReceived") = lblRequiredAmount.Text  'Deposit amount
        Session("amont_final") = lblTotalAmount.Text
        If rblPaymentGateway.SelectedValue = "Authorize.net" Then
            Response.Redirect("Booking_Confirm_Process.aspx?amount=" + lblRequiredAmount.Text.ToString + "&User_id=" + BookingRef())
        ElseIf rblPaymentGateway.SelectedValue = "Paypal" Then
            Response.Redirect("Booking_Confirm_Process_Paypal.aspx?amount=" + lblRequiredAmount.Text.ToString + "&User_id=" + BookingRef())
        ElseIf rblPaymentGateway.SelectedValue = "FirstPayment" Then
            Response.Redirect("Booking_Confirm_Process_FirstPayment.aspx?amount=" + lblRequiredAmount.Text.ToString + "&User_id=" + BookingRef())
        Else
            Response.Redirect("PageNotFound.aspx")
        End If
    End Sub

    Protected Sub ConfirmNow_Click(sender As Object, e As System.EventArgs) Handles ConfirmNow.Click
        Dim field1 As String = CType(Session.Item("WidgetVal"), String)
        Dim oDbConnection As New DBConnection()
        If field1 = "1" Then
            Session("amount_final_room_new") = lblTotalAmount.Text
            Session("amount_deposited") = lblRequiredAmount.Text
            oDbConnection.ExecuteNonQuery(" UPDATE bookings SET deposit = " + lblRequiredAmount.Text + " where bookingref= '" + BookingRef() + "'")
            Response.Redirect("RoomBookConfime.aspx?BookingRef=" + BookingRef().ToString)
        ElseIf field1 = "0" Then
            Session("amount_final_room_new") = lblTotalAmount.Text
            Session("amount_deposited") = txtDeposit.Text
            oDbConnection.ExecuteNonQuery(" UPDATE bookings SET deposit = " + txtDeposit.Text + " where bookingref= '" + BookingRef() + "'")
            Response.Redirect("RoomBookConfime.aspx?BookingRef=" + BookingRef().ToString + "&DepoAmount=" + txtDeposit.Text)
        End If
    End Sub

End Class

Imports System.Data
Imports System.Net.Mail
Imports System.IO

Partial Class BookingEasy_PopupPrintBookingHotel
    Inherits System.Web.UI.Page

    'Dim oClsDataccess As New ClsDataccess
    'Dim oColSqlPar As ColSqlparram = New ColSqlparram()
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

    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("bookingid")) Then
                Return Request.QueryString("bookingid").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Sessions.UserID = 0 Then
                If Sessions.Login = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload1", "window.parent.location='../Login.aspx';", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
                End If
            End If

            If (BookingRef IsNot String.Empty) Then
                BindDetails()
            End If
        End If
    End Sub

    'Protected Sub ButtonPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrint.Click
    '    divMessage.Visible = False
    '    Session("ctrl") = pnlPrint
    '    ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>")
    'End Sub
    Private Sub BindDetails()
        Dim oClsDataccess As New ClsDataccess
        Dim oColSqlPar As ColSqlparram = New ColSqlparram()
        oColSqlPar.Add("@bookingid", Val(BookingRef()))
        oColSqlPar.Add("@TranType", "h")
        Dim dsBooking As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team")

        If dsBooking.Tables(0).Rows.Count > 0 Then
            lblFullName.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("FullName").ToString())
            lblEmail.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Email1st").ToString())
            lblMobile.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Mobile").ToString())
            lblName.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("LocationName").ToString())
            lblBookingRef.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("BookingRef").ToString())
            lblArrival.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("ArrivalDate").ToString())
            lblDepature.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("DepartureDate").ToString())

            lblNumberOfNights.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("NoOfNights").ToString())
            lblTotalPrice.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("TotalPrice").ToString())

            lblDepositedAmount.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("DepositedAmount").ToString())
            lblComment.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Comment").ToString())

            Dim dsServices As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team1")
            If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(1).Rows.Count > 0 Then
                rptServices.DataSource = dsServices.Tables(1)
                rptServices.DataBind()
                rptServices.Visible = True
            Else
                rptServices.Visible = False
            End If

            Dim dsTotalAmount As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team2")
            If dsBooking.Tables(2).Rows.Count > 0 Then
                lblTotalAmount.Text = Convert.ToString(dsBooking.Tables(2).Rows(0)("TotalAmount").ToString())
                If (lblTotalAmount.Text = "") Then
                    lblTotalAmount.Text = "0.00"
                End If
            End If

            Dim dsRommType As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team2")
            If dsRommType.Tables(3).Rows.Count > 0 Then
                lblRoomType.Text = Convert.ToString(dsBooking.Tables(3).Rows(0)("RoomType").ToString())
            End If

        End If


        'Dim objCommon As Common = New Common()
        'Dim drBooking As DataRow = objCommon.GetBookingDetailByRef(BookingRef())

        'lblFullName.Text = drBooking("FirstName").ToString() + " " + drBooking("LastName").ToString()
        'lblEmail.Text = drBooking("Email1st").ToString()
        'lblMobile.Text = drBooking("Mobile").ToString()
        'lblBookingRef.Text = BookingRef
        'lblName.Text = drBooking("Name").ToString()
        'Dim arrival As DateTime = Convert.ToDateTime(drBooking("arrivaldate").ToString())
        'Dim depature As DateTime = Convert.ToDateTime(drBooking("departuredate").ToString())
        'lblNumberOfNights.Text = DateDiff(DateInterval.Day, arrival, depature).ToString()
        'lblArrival.Text = arrival.ToString("dd/MM/yyyy")
        'lblDepature.Text = depature.ToString("dd/MM/yyyy")
        'lblTotalPrice.Text = drBooking("bookingtotal").ToString()

        'Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())

        'If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
        '    rptServices.DataSource = dsServices
        '    rptServices.DataBind()
        '    rptServices.Visible = True
        'Else
        '    rptServices.Visible = False
        'End If
        'lblTotalAmount.Text = Val(Session("amount_final_room_new")).ToString("N2")
        'lblDepositedAmount.Text = Val(drBooking("deposit")).ToString("N2")
        'lblComment.Text = drBooking("comment").ToString()
        'lblRoomType.Text = objCommon.GetRoomTypeName(drBooking("Roomid").ToString())
        'hdnStoreID.Value = drBooking("StoreID").ToString()
    End Sub

    'Private Sub BindDetails_Restorent()

    '    Dim oColSqlPar As ColSqlparram = New ColSqlparram()
    '    oColSqlPar.Add("@bookingid", Val(BookingRef()))
    '    oColSqlPar.Add("@TranType", "r")
    '    Dim dsBooking As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team")

    '    If dsBooking.Tables(0).Rows.Count > 0 Then
    '        lblFullName.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("FullName").ToString())
    '        lblEmail.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Email1st").ToString())
    '        lblMobile.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Mobile").ToString())
    '        lblName.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("LocationName").ToString())
    '        lblBookingDate.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("BookingDate").ToString())
    '        lblBookingTime.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("bookingtime").ToString())
    '        lblNoOfCovers.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("covers").ToString())
    '        lblBookingRef.Text = BookingRef

    '        lblDepositedAmount.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("DepositedAmount").ToString())
    '        lblComment.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Comment").ToString())

    '        Dim dsServices As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team1")
    '        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(1).Rows.Count > 0 Then
    '            rptServices.DataSource = dsServices.Tables(1)
    '            rptServices.DataBind()
    '            rptServices.Visible = True
    '        Else
    '            rptServices.Visible = False
    '        End If

    '        Dim dsTotalAmount As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team2")
    '        If dsBooking.Tables(2).Rows.Count > 0 Then
    '            lblTotalAmount.Text = Convert.ToString(dsBooking.Tables(2).Rows(0)("TotalAmount").ToString())
    '        End If

    '    End If


    'End Sub

 
End Class

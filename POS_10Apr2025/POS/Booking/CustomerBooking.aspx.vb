Imports System.Data

Partial Class BookingEasy_CustomerBooking
    Inherits System.Web.UI.Page

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
        Session("AllowSetting") = Nothing
    End Sub



    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objCommon As Common = New Common()
        Dim drBooking As DataRow = objCommon.GetBookingDetailByRefAndEmail(txtBookingRef.Text.ToUpper(), txtEmail.Text)
        If drBooking IsNot Nothing Then
            If drBooking IsNot Nothing Then
                If drBooking("period") IsNot Nothing AndAlso drBooking("period").ToString() = "1" Then
                    BindTableDetails(drBooking)
                Else
                    BindRoomDetails(drBooking)
                End If
            Else
                lblMessage.Text = "This is not your booking!!"
                divErrorMessage.Visible = True
                divTableDetail.Visible = False
                divRoomDetail.Visible = False
            End If

        Else
            lblMessage.Text = "No Bookings Found!!"
            divErrorMessage.Visible = True
            divTableDetail.Visible = False
            divRoomDetail.Visible = False
        End If

    End Sub

    Private Sub BindTableDetails(ByVal drBooking As DataRow)
        Dim objCommon As Common = New Common()
        lblResName.Text = drBooking("Name").ToString()

        Dim bDate As DateTime = Convert.ToDateTime(drBooking("date").ToString())
        Dim bTime As TimeSpan = TimeSpan.Parse(drBooking("bookingtime").ToString())
        lblBookingDate.Text = bDate.ToString("dd/MM/yyyy")
        lblBookingTime.Text = String.Format("{0:hh\:mm}", bTime)

        lblNoOfCovers.Text = drBooking("covers").ToString()

        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(txtBookingRef.Text.ToUpper())

        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
            rptTableServices.DataSource = dsServices
            rptTableServices.DataBind()
            rptTableServices.Visible = True
        Else
            rptTableServices.Visible = False
        End If

        divTableDetail.Visible = True
        divRoomDetail.Visible = False
        divErrorMessage.Visible = False
    End Sub


    Private Sub BindRoomDetails(ByVal drBooking As DataRow)
        Dim objCommon As Common = New Common()
        lblName.Text = drBooking("Name").ToString()
        Dim arrival As DateTime = Convert.ToDateTime(drBooking("arrivaldate").ToString())
        Dim depature As DateTime = Convert.ToDateTime(drBooking("departuredate").ToString())
        lblNumberOfNights.Text = DateDiff(DateInterval.Day, arrival, depature).ToString()
        lblArrival.Text = arrival.ToString("dd/MM/yyyy")
        lblDepature.Text = depature.ToString("dd/MM/yyyy")
        lblTotalPrice.Text = drBooking("bookingtotal").ToString()

        Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(txtBookingRef.Text.ToUpper())

        If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
            rptServices.DataSource = dsServices
            rptServices.DataBind()
            rptServices.Visible = True
        Else
            rptServices.Visible = False
        End If

        lblRoomType.Text = objCommon.GetRoomTypeName(drBooking("Roomid").ToString())

        divTableDetail.Visible = False
        divRoomDetail.Visible = True
        divErrorMessage.Visible = False
    End Sub
End Class

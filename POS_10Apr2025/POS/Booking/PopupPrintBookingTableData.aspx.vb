Imports System.Data

Partial Class BookingEasy_PopupPrintBookingTableData
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

    'Protected Sub lnkPrint_Click(sender As Object, e As System.EventArgs) Handles lnkPrint.Click
    '    Try

    '        If BookingRef().ToString IsNot String.Empty Then
    '            BindDetails()
    '        End If

    '        divMessage.Visible = False

    '        Session("ctrl") = pnlPrint
    '        ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>")
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
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
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BindDetails()
        Dim oClsDataccess As New ClsDataccess
        Try
            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            oColSqlPar.Add("@bookingid", Val(BookingRef()))
            oColSqlPar.Add("@TranType", "r")
            Dim dsBooking As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team")

            If dsBooking.Tables(0).Rows.Count > 0 Then
                lblFullName.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("FullName").ToString())
                lblEmail.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Email1st").ToString())
                lblMobile.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Mobile").ToString())
                lblName.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("LocationName").ToString())
                lblBookingDate.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("BookingDate").ToString())
                lblBookingTime.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("bookingtime").ToString())
                lblNoOfCovers.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("covers").ToString())
                lblAlltedTable.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("Allotted_Tables1").ToString())
                lblBookingRef.Text = Convert.ToString(dsBooking.Tables(0).Rows(0)("bookingref").ToString())
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
                    If dsBooking.Tables(2).Rows(0)("TotalAmount").ToString() = "" Then
                        lblTotalAmount.Text = "0.00"
                    Else
                        lblTotalAmount.Text = Convert.ToString(dsBooking.Tables(2).Rows(0)("TotalAmount").ToString())
                    End If
                End If


                'Dim dsDepositeAmount As DataSet = oClsDataccess.GetdatasetSp("Get_BookingDetail", oColSqlPar, "Team3")
                'If dsDepositeAmount.Tables(1).Rows.Count > 0 Then
                '    lblDepositedAmount.Text = Convert.ToString(dsBooking.Tables(3).Rows(0)("DepositedAmount").ToString())
                'End If

                'lblTotalAmount.Text = Session("amont_final").ToString()
                'Dim bDate As DateTime = Convert.ToDateTime(drBooking("date").ToString())
                'Dim bTime As TimeSpan = TimeSpan.Parse(drBooking("bookingtime").ToString())
                'lblBookingDate.Text = bDate.ToString("dd/MM/yyyy")
                'lblBookingTime.Text = String.Format("{0:hh\:mm}", bTime)
                'lblComment.Text = drBooking("comment").ToString()
                'lblNoOfCovers.Text = drBooking("covers").ToString()
                'Dim field1 As String = CType(Session.Item("WidgetVal"), String)
                'If field1 = "1" Then
                '    lblDepositedAmount.Text = "0.00"
                'ElseIf field1 = "0" Then
                '    lblDepositedAmount.Text = "0.00"
                'End If
                'Dim dsServices As DataSet = objCommon.GetBookingServicesByRef(BookingRef())

                'If dsServices IsNot Nothing AndAlso dsServices.Tables.Count > 0 AndAlso dsServices.Tables(0).Rows.Count > 0 Then
                '    rptServices.DataSource = dsServices
                '    rptServices.DataBind()
                '    rptServices.Visible = True
                'Else
                '    rptServices.Visible = False
                'End If

                'hdnStoreID.Value = drBooking("StoreID").ToString()
            End If

        Catch ex As Exception

        End Try


    End Sub
End Class

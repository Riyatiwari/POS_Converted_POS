Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Security.Authentication
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports Newtonsoft.Json.Linq

Partial Class Reciept_pbl
    Inherits BaseClass
    Dim oclsLocation As New Controller_clsLocation()
    Dim oclsPromotion As New Controller_clsPromotion()
    Dim oclsBarcodeExchange As New Controller_clsBarcodeExchange()
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Dim oclsSales As New Controller_clsSales()
    Dim oClsDataccess As New ClsDataccess
    Dim created_date As String
    '  Dim modified_date As String

    Private Shared ReadOnly _httpClient As New HttpClient()
    Dim responseText As String
    Dim responseCode As Integer


    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Dim store As String


            CheckStore(Request.QueryString("store").ToString())
            Dim table_uuid As String = Request.QueryString("tableuuid").ToString()
            oclsSales.table_uuid = table_uuid
            Dim dtS As DataTable = oclsSales.Select_credit_sales_email()

            If dtS.Rows.Count > 0 Then

                For sales = 0 To 0

                    If Val(dtS.Rows(sales)("total_amount").ToString()) > 0 Then

                        Dim builder As New StringBuilder

                        builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
                        builder.Append("<div style='width:auto; color:#000000; font-family:verdana;border:1px solid " + dtS.Rows(sales)("BG_Color").ToString() + ";'>")

                        If dtS.Rows(sales)("L_image").ToString() <> "" Then

                            builder.Append("<div style='width:100%;height:100px;'>")
                            builder.Append("<img width='100%' height='100px' src ='http://testing.mytenderpos.com/LocationImageHandler.ashx?lid=" + dtS.Rows(sales)("Location_id").ToString() + "&sv=" + Session("store").ToString() + "' alt='Logo'/>")
                            builder.Append(" </div>")

                        Else

                            builder.Append("<div style='width:100%;height:100px;background-color:" + dtS.Rows(sales)("BG_Color").ToString() + ";color: " + dtS.Rows(sales)("Font_Color").ToString() + " '>")
                            builder.Append("<table style='width:100%; font-family:verdana;background-color:" + dtS.Rows(sales)("BG_Color").ToString() + ";color: " + dtS.Rows(sales)("Font_Color").ToString() + "'>")
                            builder.Append("<tr><td>&nbsp;")
                            builder.Append("</td></tr>")
                            builder.Append("<tr><td style='text-Align:center;'>")
                            builder.Append(dtS.Rows(sales)("company_name").ToString())
                            builder.Append("</td></tr>")
                            builder.Append("<tr><td>")
                            builder.Append(" Phone : " + dtS.Rows(sales)("cmp_contact").ToString() + " <br/> E-Mail : " + dtS.Rows(sales)("cmp_email").ToString() + "")
                            builder.Append("</td></tr>")
                            builder.Append("<tr><td>&nbsp;")
                            builder.Append("</td></tr>")
                            builder.Append("</table>")
                            builder.Append(" </div>")

                        End If

                        LogHelper.Error("Table_transaction:Check 1")

                        builder.Append("<table style='width:100%;height:150px; color:#000000;margin:5px;font-family:verdana;'>")
                        builder.Append("<tr><td>")
                        builder.Append("Dear Customer, ")
                        builder.Append("</td></tr>")
                        builder.Append("<tr><td>")
                        builder.Append("Thank you for placing order with us. ")
                        builder.Append("</td></tr>")
                        builder.Append("<tr><td>")
                        builder.Append("Order Confirmation order# " + dtS.Rows(sales)("order_num").ToString() + " (" + dtS.Rows(sales)("created_date").ToString() + ")")
                        builder.Append("</td></tr>")

                        builder.Append("</table>")

                        builder.Append("<table style='width:100%; border:1px solid #000000;font-family:verdana;font-size:12px;' cellpadding='0px' cellspacing='0px'>")
                        builder.Append("<tr> <td ></td><td ></td> </tr>")

                        builder.Append("<tr style='height: 50px;'><td colspan='2' style='text-align:center;'><b>" + dtS.Rows(sales)("company_name").ToString() + "<br/>" + dtS.Rows(sales)("location_name").ToString() + "</b></td></tr>")  '"<br/>" + dtS.Rows(0)("name").ToString() +
                        builder.Append("<tr><td colspan='2' style='border-bottom :1px dashed #000000;border-top:1px dashed #000000;'>")
                        builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'>")
                        builder.Append("<tr><td width='25%'>VAT NUMBER</td>")
                        builder.Append("<td>:</td>")
                        builder.Append("<td>" + dtS.Rows(sales)("Vat_No").ToString() + "</td>")
                        builder.Append(" </tr></table> </tr>")
                        builder.Append("<tr><td colspan='2'> <table width='100%'>")

                        'builder.Append("<tr><td >Till Name </td>")
                        'builder.Append("<td>:</td>")
                        'builder.Append("<td >" + Session("store").ToString() + "</td></tr>")
                        builder.Append("<tr><td width='25%'>Date & Time</td>")
                        builder.Append("<td>:</td>")
                        builder.Append("<td >" + dtS.Rows(sales)("created_date").ToString() + "</td></tr>")

                        builder.Append("<tr><td width='25%'>Served By </td>")
                        builder.Append("<td>:</td>")
                        builder.Append("<td >" + dtS.Rows(sales)("StaffName").ToString() + "</td></tr>")
                        builder.Append("<tr><td width='25%'>Order# </td>")
                        builder.Append("<td>:</td>")
                        builder.Append("<td >" + dtS.Rows(sales)("order_num").ToString() + "</td></tr>")

                        LogHelper.Error("Table_transaction:Check 2")

                        If dtS.Rows(sales)("cust_name").ToString() IsNot "" Then
                            builder.Append("<tr><td width='25%'>Customer Name</td><td>:</td> <td >" + dtS.Rows(sales)("cust_name").ToString() + "</td></tr>")
                        End If
                        If dtS.Rows(sales)("Mobile").ToString() IsNot "" Then
                            builder.Append("<tr><td width='25%'>Mobile</td><td>:</td> <td >" + dtS.Rows(sales)("Mobile").ToString() + "</td></tr>")
                        End If
                        builder.Append("<tr><td width='25%'>Email</td><td>:</td> <td >" + dtS.Rows(sales)("cust_email").ToString() + "</td></tr>")

                        If dtS.Rows(sales)("cust_address").ToString() IsNot "" Then
                            builder.Append("<tr><td width='25%'>Address</td><td>:</td> <td >" + dtS.Rows(sales)("cust_address").ToString() + "</td></tr>")
                        End If
                        If dtS.Rows(sales)("table_name").ToString() IsNot "" Then
                            builder.Append("<tr><td width='25%'>Table </td><td>:</td> <td >" + dtS.Rows(sales)("table_name").ToString() + "</td></tr>")
                        End If


                        LogHelper.Error("Table_transaction:Check 3")

                        builder.Append("<tr><td>&nbsp;")
                        builder.Append("</td></tr>")

                        builder.Append("<tr><td Colspan='3' style='text-align:Center'><b>Tax Invoice</b></td></tr>")
                        builder.Append("</table></td></tr>")
                        builder.Append("<tr><td colspan='2' style='border-bottom:1px dashed #000000;border-top:1px dashed #000000'>")
                        builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'>")

                        Dim sub_total As Double = 0
                        Dim group_total As Double = 0
                        Dim sub_total_tax As Double = 0
                        Dim change As Double = 0

                        oclsSales.cmp_id = Val(Session("cmp_id"))
                        oclsSales.sales_id = Val(dtS.Rows(sales)("sales_id").ToString())
                        Dim dtResult As DataTable = oclsSales.Select_tsales_for_email()


                        If dtResult.Rows.Count > 0 Then

                            LogHelper.Error("Table_transaction:Check 4 : count")

                            Dim last_dept As String = ""

                            For index = 0 To dtResult.Rows.Count - 1

                                If (index = 0 Or last_dept <> dtResult(index)("deptCategory").ToString()) And dtResult(index)("Is_Condiment").ToString() = "0" Then

                                    last_dept = dtResult(index)("deptCategory").ToString()

                                    If index <> 0 Then
                                        'GROUP SUBTOTAL
                                        builder.Append("<tr> <td width='70%' style='border-top:1px dashed #000000'><b>Sub Total</b></td>")
                                        builder.Append("<td width='5%' style='border-top:1px dashed #000000'></td><td width='25%' style ='text-align:right; border-top:1px dashed #000000'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>")

                                        group_total = 0
                                    End If

                                    'Department name
                                    builder.Append("<tr><td width='70%'><b>" + last_dept + "</b></td> <td width='5%'> </td> <td width='25%'></td></tr>")

                                End If

                                'product name and price
                                builder.Append("<tr><td>" + dtResult(index)("ProductName").ToString() + " ")
                                If dtResult(index)("SizeName").ToString() IsNot "" Then
                                    builder.Append("<br / > ( " + dtResult(index)("SizeName").ToString() + " ) ")
                                End If
                                builder.Append("</td> <td>" + dtResult(index)("quntity").ToString() + " </td><td style='text-align:right;'>" + String.Format("{0:0.00}", Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())) + "</td></tr>")

                                If dtResult(index)("condiment").ToString() IsNot "" Then
                                    builder.Append("<tr><td> - " + dtResult(index)("condiment").ToString() + " </td> <td></td><td style='text-align:right;'> </td></tr>")
                                End If

                                'If Val(dtResult(index)("discount").ToString()) <> 0 Then

                                '    builder.Append("<tr><td>" + dtResult(index)("discount").ToString() + "(" + dtResult(index)("discount_name").ToString() + ") </td> <td></td><td> </td></tr>")

                                'End If

                                group_total += Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())

                                sub_total += Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())
                                sub_total_tax += Convert.ToDouble(dtResult(index)("tax").ToString())

                                LogHelper.Error("Table_transaction:Check 5")

                            Next
                        End If

                        If group_total <> 0 Then
                            'GROUP SUBTOTAL
                            builder.Append("<tr><td width='70%' style='border-top:1px dashed #000000'><b>Sub Total</b></td>")
                            builder.Append("<td  width='5%' style='border-top:1px dashed #000000'></td><td width='25%' style ='text-align:right; border-top:1px dashed #000000'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>")

                            group_total = 0
                        End If

                        builder.Append("<tr><td>&nbsp;")
                        builder.Append("</td></tr>")
                        builder.Append("</table></td></tr>")

                        builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>")
                        builder.Append("<table width='100%'>")
                        If (Val(dtS.Rows(sales)("delivery_charges").ToString()) > 0) Then


                            builder.Append("<tr><td width='70%'><b>Delivery Charges</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("delivery_charges").ToString())) + "</b></td></tr>")
                            builder.Append("<tr><td width='70%'><b>Total</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtS.Rows(sales)("delivery_charges").ToString())) + "</b></td></tr>")


                        Else
                            builder.Append("<tr><td width='70%'><b>Total</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr> <tr><td>&nbsp;</td></tr> ")

                        End If

                        If (Val(dtS.Rows(sales)("balance").ToString()) <> 0) Then
                            builder.Append("<tr><td width='70%'><b>Balance</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("balance").ToString())) + "</b></td></tr> ")

                        End If

                        If (Val(dtS.Rows(sales)("TIP_AMOUNT").ToString()) <> 0) Then
                            builder.Append(" <tr><td width='70%'><b>Tip Amount</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("TIP_AMOUNT").ToString())) + "</b></td></tr> ")

                        End If

                        If (Val(dtS.Rows(sales)("surcharge_amount").ToString()) <> 0) Then
                            builder.Append("<tr><td width='70%'><b>Surcharge Amount</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("surcharge_amount").ToString())) + "</b></td></tr> ")

                        End If

                        LogHelper.Error("Table_transaction:Check 6")

                        For p = 0 To dtS.Rows.Count - 1

                            builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(p)("payment_amount").ToString()) + Val(dtS.Rows(p)("change").ToString())) + "</b></td></tr> ")

                            change += Val(dtS.Rows(p)("change").ToString())
                        Next


                        If Val(change) > 0 Then

                            builder.Append("<tr><td width='70%'><b>Change</b></td>")
                            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(change)) + "</b></td></tr>")

                        End If


                        builder.Append("<tr><td>&nbsp;</td></tr> </table></td></tr>")

                        builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>")
                        builder.Append("<table width='100%' style='font-family:verdana;font-size:12px;'>")


                        builder.Append("<tr><td width='70%'><b>Tax Information</b></td><td width='5%'></td><td width='25%'></td></tr>")

                        builder.Append("<tr><td>&nbsp;</td></tr>")

                        builder.Append("<tr><td width='70%'>VAT Total Sales</td><td width='5%'></td>")
                        builder.Append("<td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>")

                        builder.Append("<tr><td width='70%'>VAT Tax Collection</td><td width='5%'></td>")
                        builder.Append("<td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total_tax) + "</b></td></tr>")

                        'builder.Append("<tr><td>Non-Taxable Total Sales</td><td></td>")
                        'builder.Append("<td style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>")

                        builder.Append("<tr><td>&nbsp; </td></tr>")

                        builder.Append("</table></td></tr>")

                        If (dtS.Rows(sales)("Integrated_Terminal_ID").ToString() IsNot "" And dtS.Rows(sales)("Integrated_Merchant_ID").ToString() IsNot "" And
                            dtS.Rows(sales)("Integrated_SaleType").ToString() IsNot "" And dtS.Rows(sales)("Integrated_Entry_Method").ToString() IsNot "") Then


                            builder.Append("<tr><td colspan='2'  style='border-bottom:1px dashed #000000;'> <table width='100%' style='font-size: small;'>")

                            builder.Append("<tr><td colspan='3' style='font-weight: bold; border-bottom:1px dashed #000000; text-align : center; '>CARDHOLDER COPY</td></tr>")


                            builder.Append("<tr><td width='15%'>Terminal ID</td>")
                            builder.Append("<td  width='5%'>:</td>")
                            builder.Append("<td >" + dtS.Rows(sales)("Integrated_Terminal_ID").ToString() + "</td></tr>")

                            builder.Append("<tr><td width='15%'>Merchant ID</td>")
                            builder.Append("<td  width='5%'>:</td>")
                            builder.Append("<td >" + dtS.Rows(sales)("Integrated_Merchant_ID").ToString() + "</td></tr>")

                            builder.Append("<tr><td width='15%'>Sale Type</td>")
                            builder.Append("<td  width='5%'>:</td>")
                            builder.Append("<td >" + dtS.Rows(sales)("Integrated_SaleType").ToString() + "</td></tr>")

                            builder.Append("<tr><td width='15%'>Entry Method</td>")
                            builder.Append("<td  width='5%'>:</td>")
                            builder.Append("<td >" + dtS.Rows(sales)("Integrated_Entry_Method").ToString() + "</td></tr>")

                            builder.Append("<tr><td>&nbsp; </td></tr>")

                            builder.Append("<tr><td colspan='3' style='text-align:center;'>Please debit account as shown</td></tr>")
                            builder.Append("<tr><td colspan='3' style='text-align:center;'>Please retain for your recoreds</td></tr>")


                            builder.Append("<tr><td>&nbsp; </td></tr>")

                            builder.Append("</table></td></tr>")

                        End If

                        LogHelper.Error("Table_transaction:Check 7")

                        builder.Append("<tr style='height: 20px;'><td colspan ='2' style='text-align:center;'> <b>Thanks For visiting us </b> </td></tr><tr><td>&nbsp;</td></tr></table>")

                        builder.Append("<div style='width:100%;height:50px; background-color: " + dtS.Rows(sales)("BG_Color").ToString() + ";'>")
                        builder.Append("<img width='100%' height='50px' src ='http://testing.mytenderpos.com/Images/bg/logo.png' alt='footer'/>")
                        builder.Append(" </div>")

                        'builder.Append("Thanks.")
                        builder.Append("</div>")
                        builder.Append("</body> </html>")
                        receiptContainer.InnerHtml = builder.ToString()
                    End If
                Next
            End If


        Catch ex As Exception
            LogHelper.Error("Scheduler_BarExchange:Page_Load" + ex.Message)
        End Try
    End Sub

    Public Sub CheckStore(ByVal store As String)
        Try

            strcon.Open()
            Dim n As String = store

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("password") = Decode_data(dt.Rows(0)("db_password"))
                Session("db_name") = dt.Rows(0)("db_name")
                Session("ConnectionString") = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Session("password") + ";"
            Else


            End If

            strcon.Close()
        Catch ex As Exception
            strcon.Close()
            LogHelper.Error("Scheduler:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function






End Class

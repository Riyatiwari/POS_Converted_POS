Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel
Imports System.Web.Configuration
Imports System.IO
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Security.Authentication
Imports System.Net

Partial Class send_mail_APK
    Inherits BaseClass

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            send_mail()

        Catch ex As Exception
            LogHelper.Error("Send_mail_APK:page_load:" & ex.ToString())
        End Try

    End Sub

    Private Sub send_mail()
        Dim constring As String = ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString

        Using con As New SqlConnection(constring)
            Using cmd As New SqlCommand("select distinct db_server,db_username,db_password,'POS' as db_name from WS_Store WHERE db_server != '62.69.34.96';", con)

                cmd.CommandType = CommandType.Text
                    con.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Using dt As New DataTable()
                            sda.Fill(dt)

                            If dt.Rows.Count > 0 Then

                                'Get all data from POS_controller 28102021
                                Dim dt1 As New DataTable()
                                Using con1 As New SqlConnection(constring)
                                    Using cmd1 As New SqlCommand("select * from WS_Store", con1)
                                        cmd1.CommandType = CommandType.Text
                                        con1.Open()
                                        Using sda1 As New SqlDataAdapter(cmd1)
                                            sda1.Fill(dt1)
                                        End Using
                                    End Using
                                End Using

                                Dim dv As New DataView(dt1)

                                For Each dr As DataRow In dt.Rows
                                    Try
                                        Dim constring1 As String = "Data Source= '" + dr("db_server").ToString() + "';Initial Catalog= '" + dr("db_name").ToString() + "';User ID='" + dr("db_username").ToString() + "';Password= '" + Decode_data(dr("db_password").ToString()) + "'"
                                        Session("Con_string") = "Data Source= '" + dr("db_server").ToString() + "';Initial Catalog= '" + dr("db_name").ToString() + "';User ID='" + dr("db_username").ToString() + "';Password= '" + Decode_data(dr("db_password").ToString()) + "'"

                                        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                                        Dim dtS As DataTable = GetdataTable("Get_AllStore_SalesDetail", oColSqlparram)

                                        If dtS.Rows.Count > 0 Then

                                            For sales = 0 To dtS.Rows.Count - 1

                                                If (Val(dtS.Rows(sales)("total_amount").ToString()) > 0 And Val(dtS.Rows(sales)("payment_amount").ToString()) = 0 And (dtS.Rows(sales)("table_uuid").ToString() <> "0" And dtS.Rows(sales)("table_uuid").ToString() <> "")) Or (Val(dtS.Rows(sales)("payment_amount").ToString()) <> 0 And dtS.Rows(sales)("table_uuid").ToString() = "0") Then

                                                    Session("store_ConnectionString") = "Data Source= '" + dr("db_server").ToString() + "';Initial Catalog= '" + dtS.Rows(sales)("db_name").ToString() + "';User ID='" + dr("db_username").ToString() + "';Password= '" + Decode_data(dr("db_password").ToString()) + "'"

                                                    Dim builder As New StringBuilder

                                                    builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
                                                    builder.Append("<div style='width:70%; color:#000000; font-family:verdana;border:1px solid " + dtS.Rows(sales)("BG_Color").ToString() + ";'>")

                                                    If dtS.Rows(sales)("L_image").ToString() <> "" Then

                                                        dv.RowFilter = "db_name = '" + dtS.Rows(sales)("db_name").ToString() + "'"

                                                        builder.Append("<div style='width:100%;height:100px;'>")
                                                        builder.Append("<img width='100%' height='100px' src ='http://testing.mytenderpos.com/LocationImageHandler.ashx?lid=" + dtS.Rows(sales)("Location_id").ToString() + "&sv=" + dv(0)("store_name").ToString() + "' alt='Logo'/>")
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

                                                    builder.Append("<tr style='height: 50px;'><td colspan='2' style='text-align:center;'><b>" + dtS.Rows(sales)("company_name").ToString() + "<br/>" + dtS.Rows(sales)("Location_name").ToString() + "</b></td></tr>")  '"<br/>" + dtS.Rows(0)("name").ToString() +
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

                                                    '---------------------get t_sales detail-----------------
                                                    Dim oColSqlparram1 As ColSqlparram = New ColSqlparram()
                                                    oColSqlparram1.Add("@cmp_id", Val(dtS.Rows(sales)("cmp_id").ToString()), SqlDbType.Int)
                                                    oColSqlparram1.Add("@sales_id", Val(dtS.Rows(sales)("sales_id").ToString()), SqlDbType.Int)
                                                    Dim dtResult As DataTable = GetdataTableSp("Get_tsales_for_email_APK", oColSqlparram1)

                                                    If dtResult.Rows.Count > 0 Then

                                                        Dim last_dept As String = ""

                                                        For index = 0 To dtResult.Rows.Count - 1

                                                            If index = 0 Or last_dept <> dtResult(index)("deptCategory").ToString() Then

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

                                                            If Val(dtResult(index)("discount").ToString()) <> 0 Then

                                                                builder.Append("<tr><td>" + dtResult(index)("discount").ToString() + "(" + dtResult(index)("discount_name").ToString() + ") </td> <td></td><td> </td></tr>")

                                                            End If

                                                            group_total += Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())

                                                            sub_total += Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())
                                                            sub_total_tax += Convert.ToDouble(dtResult(index)("tax").ToString())
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
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr> <tr><td>&nbsp;</td></tr>")

                                                    End If
                                                    If (Val(dtS.Rows(sales)("Service_charges").ToString()) > 0) Then


                                                        builder.Append("<tr><td width='70%'><b>Service Charges</b></td>")
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("Service_charges").ToString())) + "</b></td></tr>")
                                                        builder.Append("<tr><td width='70%'><b>Total</b></td>")
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtS.Rows(sales)("Service_charges").ToString())) + "</b></td></tr>")


                                                    Else
                                                        builder.Append("<tr><td width='70%'><b>Total</b></td>")
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr> <tr><td>&nbsp;</td></tr>")

                                                    End If

                                                    If (Val(dtS.Rows(sales)("balance").ToString()) > 0) Then
                                                        builder.Append("<tr><td width='70%'><b>Balance</b></td>")
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("balance").ToString())) + "</b></td></tr>")

                                                    End If


                                                    If (Val(dtS.Rows(sales)("TIP_AMOUNT").ToString()) <> 0) Then
                                                        builder.Append(" <tr><td width='70%'><b>Tip Amount</b></td>")
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("TIP_AMOUNT").ToString())) + "</b></td></tr> ")

                                                    End If

                                                    If (Val(dtS.Rows(sales)("surcharge_amount").ToString()) <> 0) Then
                                                        builder.Append("<tr><td width='70%'><b>Surcharge Amount</b></td>")
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("surcharge_amount").ToString())) + "</b></td></tr> ")

                                                    End If

                                                    'If (Val(dtS.Rows(sales)("change").ToString()) > 0) Then

                                                    '    builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>")
                                                    '    builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("payment_amount").ToString()) + Val(dtS.Rows(sales)("change").ToString())) + "</b></td></tr> ")

                                                    '    builder.Append("<tr><td width='70%'><b>Change</b></td>")
                                                    '    builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("change").ToString())) + "</b></td></tr> <tr><td>&nbsp;</td></tr> </table></td></tr>")
                                                    'Else

                                                    '    builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>")
                                                    '    builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("payment_amount").ToString())) + "</b></td></tr> <tr><td>&nbsp;</td></tr> </table></td></tr>")

                                                    'End If

                                                    If dtS.Rows(sales)("table_uuid").ToString() <> "0" And dtS.Rows(sales)("table_uuid").ToString() <> "" Then
                                                        For p = 0 To dtS.Rows.Count - 1

                                                            If dtS.Rows(p)("table_uuid").ToString() = dtS.Rows(sales)("table_uuid").ToString() Then

                                                                If (Val(dtS.Rows(p)("payment_amount").ToString()) + Val(dtS.Rows(p)("change").ToString())) > 0 Then

                                                                    builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>")
                                                                    builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(p)("payment_amount").ToString()) + Val(dtS.Rows(p)("change").ToString())) + "</b></td></tr> ")

                                                                End If

                                                                change += Val(dtS.Rows(p)("change").ToString())

                                                            End If
                                                        Next
                                                    Else
                                                        builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>")
                                                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("payment_amount").ToString()) + Val(dtS.Rows(sales)("change").ToString())) + "</b></td></tr> ")

                                                        change += Val(dtS.Rows(sales)("change").ToString())

                                                    End If

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
                                                        builder.Append("<tr><td colspan='3' style='text-align:center;'>Please retain for your records</td></tr>")


                                                        builder.Append("<tr><td>&nbsp; </td></tr>")

                                                        builder.Append("</table></td></tr>")

                                                    End If

                                                    builder.Append("<tr style='height: 20px;'><td colspan ='2' style='text-align:center;'> <b>Thanks For visiting us </b> </td></tr><tr><td>&nbsp;</td></tr></table>")

                                                    builder.Append("<div style='width:100%;height:50px; background-color: " + dtS.Rows(sales)("BG_Color").ToString() + ";'>")
                                                    builder.Append("<img width='100%' height='50px' src ='http://testing.mytenderpos.com/Images/bg/logo.png' alt='footer'/>")
                                                    builder.Append(" </div>")

                                                    'builder.Append("Thanks.")
                                                    builder.Append("</div>")
                                                    builder.Append("</body> </html>")

                                                    Dim email As String
                                                    Dim Subject As String

                                                    If Not dtS.Rows(sales)("cust_email").ToString() Is Nothing Then
                                                        email = dtS.Rows(sales)("cust_email").ToString()

                                                        'madhvanimitesh@gmail.com  , mitesh.m@technometrics.in
                                                        Subject = "Order Receipt"

                                                        MailTo_receipt(email, Subject, builder.ToString, "", "", "")

                                                        LogHelper.Error("Send_mail_APK:email_id:" & email)

                                                        '--------update email flag in m_sales-------------

                                                        Dim oColSqlparram2 As ColSqlparram = New ColSqlparram()
                                                        If dtS.Rows(sales)("table_uuid").ToString() <> "0" And dtS.Rows(sales)("table_uuid").ToString() <> "" Then

                                                            oColSqlparram2.Add("@sales_id", Val(0), SqlDbType.Int)
                                                            oColSqlparram2.Add("@table_uuid", dtS.Rows(sales)("table_uuid").ToString())
                                                        Else

                                                            oColSqlparram2.Add("@sales_id", Val(dtS.Rows(sales)("sales_id").ToString()), SqlDbType.Int)
                                                            oColSqlparram2.Add("@table_uuid", "0")

                                                        End If

                                                        Dim u_dt As DataTable = GetdataTableSp("update_email_flag_sales", oColSqlparram2)



                                                        'If Val(dtS.Rows(sales)("sales_id").ToString()) = Val(dtS.Rows(sales)("sales_id_1").ToString()) Then
                                                        '    Dim oColSqlparram2 As ColSqlparram = New ColSqlparram()
                                                        '    oColSqlparram2.Add("@sales_id", Val(dtS.Rows(sales)("sales_id").ToString()), SqlDbType.Int)
                                                        '    Dim u_dt As DataTable = GetdataTableSp("update_email_flag_sales", oColSqlparram2)

                                                        'Else
                                                        '    Dim oColSqlparram3 As ColSqlparram = New ColSqlparram()
                                                        '    oColSqlparram3.Add("@sales_id", Val(dtS.Rows(sales)("sales_id").ToString()), SqlDbType.Int)
                                                        '    Dim u_dt As DataTable = GetdataTableSp("update_email_flag_sales", oColSqlparram3)

                                                        '    Dim oColSqlparram4 As ColSqlparram = New ColSqlparram()
                                                        '    oColSqlparram4.Add("@sales_id", Val(dtS.Rows(sales)("sales_id_1").ToString()), SqlDbType.Int)
                                                        '    Dim u_dt1 As DataTable = GetdataTableSp("update_email_flag_sales", oColSqlparram4)

                                                        'End If

                                                    End If

                                                End If
                                            Next


                                        End If
                                    Catch ex As Exception
                                        LogHelper.Error("Send_mail_APK:Send_mail_function:" & ex.Message)
                                    End Try
                                Next

                            End If

                        End Using
                    End Using
            End Using
        End Using

    End Sub

    Public Function GetdataTable(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(Session("Con_string").ToString())
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 0

                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                    Dim parameter As New SqlClient.SqlParameter
                    parameter.ParameterName = oClsSqlParameter.ParaName
                    parameter.SqlDbType = oClsSqlParameter.ParaType
                    parameter.Value = oClsSqlParameter.ParaValue
                    parameter.Direction = oClsSqlParameter.ParaDirection
                    com.Parameters.Add(parameter)
                Next

                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Send_mail_APK:GetdataTable:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function

    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(Session("store_ConnectionString").ToString())
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 0

                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                    Dim parameter As New SqlClient.SqlParameter
                    parameter.ParameterName = oClsSqlParameter.ParaName
                    parameter.SqlDbType = oClsSqlParameter.ParaType
                    parameter.Value = oClsSqlParameter.ParaValue
                    parameter.Direction = oClsSqlParameter.ParaDirection
                    com.Parameters.Add(parameter)
                Next

                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Send_mail_APK:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function

    Public Overloads Sub MailTo_receipt(ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        Dim mailSettings As SmtpSection = CType(ConfigurationManager.GetSection("system.net/mailSettings/smtp"), SmtpSection)
        If Not mailSettings Is Nothing Then
            MailServer_Port = mailSettings.Network.Port
            MailServer = mailSettings.Network.Host
            MailServer_Password = mailSettings.Network.Password
            MailServer_UserName = mailSettings.Network.UserName
            From_Email = mailSettings.From
            Ssl = True
            alas = "TenderPOS"
        End If

        Try

            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()
            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            '    LogHelper.Error("Base_class: 2:" + System.DateTime.Now.ToString())
            If CC <> "" Then
                Email_CC = CC
                oE_Mail.CC.Add(Email_CC)
            End If
            If BCC <> "" Then
                Email_BCC = BCC
                oE_Mail.Bcc.Add(Email_BCC)
            End If

            If attach <> "" Then
                Dim strarr() As String
                Dim i As Integer
                strarr = attach.Split(",")
                For i = 0 To strarr.Length - 1
                    Dim str As String = strarr(i)
                    Dim path As String = str
                    Dim myattach As New System.Net.Mail.Attachment(path)
                    oE_Mail.Attachments.Add(myattach)
                Next
            End If


            oE_Mail.From = New MailAddress(From_Email, alas)
            oE_Mail.IsBodyHtml = True
            oE_Mail.Subject = Subject
            oE_Mail.Body = Body
            'LogHelper.Error("Base_class: 3:" + System.DateTime.Now.ToString())
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12

            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network

            ' LogHelper.Error("Base_class: 4:" + System.DateTime.Now.ToString())
            oSmtpclient.Send(oE_Mail)
            'LogHelper.Error("Base_class: 5:" + System.DateTime.Now.ToString())
        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Send_mail_APK: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
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

Imports Microsoft.VisualBasic
Imports System.Data
Imports Telerik.Web.UI
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data.OleDb
Imports System.Net
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Net.Configuration
Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Exchange.WebServices
Imports Microsoft.Exchange.WebServices.Data
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Mail
Imports System.Threading
Imports System.Security.Authentication


Partial Class CS_Success
    Inherits System.Web.UI.Page

    Dim oclsSales As New Controller_clsSales()
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProductGroup As New Controller_clsCategory
    Dim oclsLocation As New Controller_clsLocation
    Dim oclsProduct As New Controller_clsProduct
    Dim str As String = ""

    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
    Public Property PaymentSuccess As Boolean
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim keys As String() = Request.Form.AllKeys

            'For i As Integer = 0 To keys.Length - 1
            '    Response.Write(keys(i) & ": " & Request.Form(keys(i)) & "<br>")
            'Next
            lblDatetime.Text = System.DateTime.Now.ToString()
            Dim Response1 As String = ""
            Dim FOrderRef As String = ""
            Dim FPaymentRef As String = ""
            '  LogHelper.Error("CS_SUCESS Live : paymentjobref :" + System.DateTime.Now.ToString() + ": " + Request.Form("transactionUnique").ToString())
            '  LogHelper.Error("CS_SUCESS Live : paymentref :" + System.DateTime.Now.ToString() + ": " + Request.Form("xref").ToString())

            Dim stateValue As String = Request.Form("state").ToString()

            If stateValue.ToLower() = "declined" Then
                PaymentSuccess = False
            Else
                PaymentSuccess = True
                LogHelper.Error("CF_SUCESS:Log 1st  ")

                Try


                    Dim paymentjobref As String = Request.Form("transactionUnique").ToString()
                    Dim paymentref As String = Request.Form("xref").ToString()

                    'Dim arrOrdRef As String() = paymentjobref.Split(",")
                    'Dim arrPayRef As String() = paymentref.Split(",")

                    FOrderRef = paymentjobref.ToString()
                    FPaymentRef = paymentref.ToString()
                    lblTranRef.Text = FOrderRef.ToString()

                    '  FPaymentRef = "sdfmkjrfjajrkj"
                    'If Not String.IsNullOrEmpty(FPaymentRef) Then
                    '    PaymentSuccess = True
                    'Else
                    '    PaymentSuccess = False
                    'End If
                Catch ex As Exception
                    ' PaymentSuccess = False
                    LogHelper.Error("CS_SUCESS Live : Payref Count :" + System.DateTime.Now.ToString())
                End Try


                CheckPay(FOrderRef)
                ViewState("order_ref") = FOrderRef
                ViewState("paymentref") = FPaymentRef
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                oColSqlPar.Add("@cmp_id", 0)
                oColSqlPar.Add("@sales_id", Session("sales_id"))
                oColSqlPar.Add("@order_ref", FOrderRef)
                oColSqlPar.Add("@payment_ref", FPaymentRef)
                oColSqlPar.Add("@Tran_Type", "U")
                Dim dt As DataTable = GetdataTableSp("P_M_SALES_update", oColSqlPar, "P_M_SALES_update", str)

                LogHelper.Error("CF_SUCESS:222 3nd  " + Request.Form.AllKeys.Count.ToString())
                For Each name As String In Request.Form.AllKeys
                    Dim value As String = Request.Form(name)
                    LogHelper.Error("name : " + value.ToString())
                Next
                LogHelper.Error("CF_SUCESS:333 4nd  ")


                If dt.Rows.Count > 0 Then
                    lbtnLink.HRef = dt.Rows(0)("Website").ToString()
                    If (dt.Rows(0)("location_id").ToString() = "-1") Then
                        Session("Location_id") = "-1"
                        Session("cmp_id") = "-1"
                        Session("cust_email") = "-1"

                    Else

                        'Response = "<br/>"
                        Response1 = "<b>Order Reference No :</b> <span Style='word-wrap: normal; word-break: break-all;'> " + FOrderRef + "  </span><br/><br/> "
                        Response1 += "<b>Payment Reference No : </b> <span Style='word-wrap: normal; word-break: break-all;'>" + FPaymentRef + "</span> <br/> "

                        '    lblResponse.InnerHtml = Response.ToString()

                        Session("Location_id") = dt.Rows(0)("location_id").ToString()
                        Session("cmp_id") = dt.Rows(0)("cmp_id").ToString()
                        Session("cust_email") = dt.Rows(0)("cust_email").ToString()
                    End If
                End If
                'If Session("store").ToString.ToLower() = "VictoriaBattersea" Or Session("store") = "victoriabattersea" Then
                '    Session("diliveryOption") = "2"
                'End If



                LogHelper.Error("CF_SUCESS:Log 2nd  ")
                ' LogHelper.Error("CF_SUCESS: responseCode :" + System.DateTime.Now.ToString() + ": " + Request.Form("responseCode").ToString())
                LogHelper.Error("CF_SUCESS: responseMessage :" + System.DateTime.Now.ToString() + ": " + Request.Form("responseMessage").ToString())
                LogHelper.Error("CF_SUCESS: cardNumberMask :" + System.DateTime.Now.ToString() + ": " + Request.Form("cardNumberMask").ToString())
                LogHelper.Error("CF_SUCESS: authorisationCode :" + System.DateTime.Now.ToString() + ": " + Request.Form("authorisationCode").ToString())
                LogHelper.Error("CF_SUCESS: state :" + System.DateTime.Now.ToString() + ": " + Request.Form("state").ToString())
                LogHelper.Error("CS_SUCESS Live : paymentjobref :" + System.DateTime.Now.ToString() + ": " + Request.Form("transactionUnique").ToString())
                LogHelper.Error("CS_SUCESS Live : paymentref :" + System.DateTime.Now.ToString() + ": " + Request.Form("xref").ToString())

                LogHelper.Error("CF_SUCESS: Cust_email :" + System.DateTime.Now.ToString() + ": " + Session("cust_email").ToString())
                'If Not (Session("cust_email").ToString() = "" Or Session("cust_email").ToString() = "-1") Then
                get_msg()
                    send_mail()
                ' send_mail_cust()

                'End If

            End If
            LogHelper.Error("CF_SUCESS: responseMessage :" + System.DateTime.Now.ToString() + ": " + Request.Form("responseMessage").ToString())
            LogHelper.Error("CF_SUCESS: cardNumberMask :" + System.DateTime.Now.ToString() + ": " + Request.Form("cardNumberMask").ToString())
            LogHelper.Error("CF_SUCESS: authorisationCode :" + System.DateTime.Now.ToString() + ": " + Request.Form("authorisationCode").ToString())
            LogHelper.Error("CF_SUCESS: state :" + System.DateTime.Now.ToString() + ": " + Request.Form("state").ToString())
            LogHelper.Error("CS_SUCESS Live : paymentjobref :" + System.DateTime.Now.ToString() + ": " + Request.Form("transactionUnique").ToString())
            LogHelper.Error("CS_SUCESS Live : paymentref :" + System.DateTime.Now.ToString() + ": " + Request.Form("xref").ToString())
        Catch ex As Exception

        End Try
    End Sub
    'Function send_mail_cust()
    '    Try

    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@location_id", Val(Session("location_id").ToString()), SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", Val(Session("cmp_Id").ToString()), SqlDbType.Int)
    '        Dim dtL As DataTable = GetdataTableSp("Get_M_Location", oColSqlparram, "Get_M_Location", str)

    '        Dim oColSqlParCust As ColSqlparram = New ColSqlparram()
    '        oColSqlParCust.Add("@sales_id", Val(Session("sales_id")), SqlDbType.Decimal)
    '        Dim dtCust As DataTable = GetdataTableSp("Get_Sales_Customer_Details", oColSqlParCust, "Get_Sales_Customer_Details", str)

    '        Session("diliveryOption") = dtCust(0)("diliveryOption").ToString()

    '        Dim builder As New StringBuilder

    '        builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
    '        builder.Append("<div style='width:100%;height:100%; color:#000000; font-family:verdana;border:1px solid " + dtL(0)("BG_Color").ToString() + ";'>")
    '        builder.Append("<div style='width:100%;height:100px;background-color:" + dtL(0)("BG_Color").ToString() + ";color: " + dtL(0)("Font_Color").ToString() + " '>")


    '        builder.Append("<table style='width:100%;height:100px;  font-family:verdana;background-color:" + dtL(0)("BG_Color").ToString() + ";color: " + dtL(0)("Font_Color").ToString() + "'>")
    '        builder.Append("<tr><td>&nbsp;")
    '        builder.Append("</td></tr>")
    '        builder.Append("<tr><td style='text-Align:center;'>")
    '        builder.Append(dtL(0)("company_name").ToString())
    '        builder.Append("</td></tr>")
    '        builder.Append("<tr><td>")
    '        builder.Append(" Phone : " + dtL(0)("contact").ToString() + " <br/> E-Mail : " + dtL(0)("email").ToString() + "")
    '        builder.Append("</td></tr>")
    '        builder.Append("<tr><td>&nbsp;")
    '        builder.Append("</td></tr>")
    '        builder.Append("</table>")

    '        builder.Append(" </div>")
    '        builder.Append("<table style='width:100%;height:200px; color:#000000;margin:5px;font-family:verdana;'>")
    '        builder.Append("<tr><td>")
    '        builder.Append("Dear Customer, ")
    '        builder.Append("</td></tr>")
    '        builder.Append("<tr><td>")
    '        builder.Append("Thank you for placing order with us. ")
    '        builder.Append("</td></tr>")
    '        builder.Append("<tr><td>")
    '        builder.Append("Order Confirmation order# " + ViewState("order_ref") + " (" + System.DateTime.Now.ToString() + ")")
    '        builder.Append("</td></tr>")

    '        builder.Append("<tr><td>")
    '        builder.Append("Your order will be processed on  " + dtCust(0)("deliver_date").ToString() + " " + dtCust(0)("deliver_time").ToString() + " ")
    '        builder.Append("</td></tr>")

    '        builder.Append("<tr><td>")
    '        builder.Append("&nbsp; ")
    '        builder.Append("</td></tr>")
    '        builder.Append("</table>")

    '        builder.Append("<table style=' border:1px solid #000000;font-family:verdana;font-size:12px;' cellpadding='0px' cellspacing='0px'>")
    '        builder.Append("<tr> <td ></td><td ></td> </tr>")

    '        builder.Append("<tr><td colspan='2' style='text-align:center;'><b>" + Session("store_name").ToString() + "<br/>" + dtL.Rows(0)("name").ToString() + "</b></td></tr>")
    '        builder.Append("<tr><td colspan='2' style='border-bottom :1px dashed #000000;border-top:1px dashed #000000;'>")
    '        builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'><tr>")
    '        builder.Append("<td width='30%'>VAT NUMBER</td>")
    '        builder.Append("<td width='5%'>:</td>")
    '        builder.Append("<td width='65%'>" + dtL.Rows(0)("Vat_No").ToString() + "</td>")
    '        builder.Append(" </tr></table> </tr>")
    '        builder.Append("<tr><td colspan='2'> <table width='100%'>")
    '        'builder.Append("<tr><td width='30%'>Location </td>")
    '        'builder.Append("<td width='5%'>:</td>")
    '        'builder.Append("<td width='65%' >" + dtL.Rows(0)("name").ToString() + "</td></tr>")
    '        builder.Append("<tr><td >Date & Time</td>")
    '        builder.Append("<td>:</td>")
    '        builder.Append("<td >" + System.DateTime.Now + "</td></tr>")
    '        builder.Append("<tr><td >Served By </td>")
    '        builder.Append("<td>:</td>")
    '        builder.Append("<td >Manager1</td></tr>")
    '        builder.Append("<tr><td >Order# </td>")
    '        builder.Append("<td>:</td>")

    '        builder.Append("<td >" + ViewState("order_ref") + "</td></tr>")

    '        builder.Append("<tr><td >Customer Name</td><td>:</td> <td >" + dtCust(0)("web_name").ToString() + "</td></tr>")
    '        builder.Append("<tr><td >Mobile</td><td>:</td> <td >" + dtCust(0)("web_mobile").ToString() + "</td></tr>")
    '        builder.Append("<tr><td >Email</td><td>:</td> <td >" + dtCust(0)("web_email").ToString() + "</td></tr>")
    '        builder.Append("<tr><td >Address</td><td>:</td> <td >" + dtCust(0)("web_address").ToString() + "</td></tr>")
    '        builder.Append("<tr><td >Table No</td><td>:</td> <td >" + dtCust(0)("table_name").ToString() + "</td></tr>")

    '        builder.Append("<tr><td Colspan='3' style='text-align:Center'><b>Tax Invoice</b></td></tr>")
    '        builder.Append("</table></td></tr>")
    '        builder.Append("<tr><td colspan='2' style='border-bottom:1px dashed #000000;border-top:1px dashed #000000'>")
    '        builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'>")

    '        Dim sub_total As Double = 0
    '        Dim group_total As Double = 0

    '        Dim oColSqlPar As ColSqlparram = New ColSqlparram()
    '        oColSqlPar.Add("@sales_id", Val(Session("sales_id")), SqlDbType.Decimal)
    '        Dim dtcart As DataTable = GetdataTableSp("Get_Cart", oColSqlPar, "Get_Cart", str)

    '        Dim dtN As DataTable = dtcart

    '        'temp table with department name and ID
    '        Dim dt As DataTable = New DataTable()
    '        dt.Columns.Add("row_id", GetType(Int32))
    '        dt.Columns.Add("ProductId", GetType(Int32))
    '        dt.Columns.Add("SizeId", GetType(Int32))
    '        dt.Columns.Add("ProductName", GetType(String))
    '        dt.Columns.Add("SizeName", GetType(String))
    '        dt.Columns.Add("Price", GetType(Double))
    '        dt.Columns.Add("tax", GetType(Double))
    '        dt.Columns.Add("condiment", GetType(String))
    '        dt.Columns.Add("condimentId", GetType(String))
    '        dt.Columns.Add("condimentprice", GetType(String))
    '        dt.Columns.Add("discount", GetType(Double))
    '        dt.Columns.Add("discount_name", GetType(String))
    '        dt.Columns.Add("promo_id", GetType(String))
    '        dt.Columns.Add("product_groupid", GetType(String))
    '        dt.Columns.Add("taxid", GetType(String))
    '        dt.Columns.Add("taxper", GetType(Double))
    '        dt.Columns.Add("deptId", GetType(Double))
    '        dt.Columns.Add("deptName", GetType(String))
    '        dt.Columns.Add("qty", GetType(Double))
    '        dt.Columns.Add("ActualCostPer", GetType(Double))
    '        dt.Columns.Add("DeliveryCharges", GetType(Double))


    '        If dtN.Rows.Count > 0 Then

    '            For index = 0 To dtN.Rows.Count - 1

    '                'oclsProduct.product_id = Val(dtN(index)("ProductId").ToString())
    '                'oclsProduct.cmp_id = Val(Session("cmp_id").ToString())
    '                'Dim dt_dept As DataTable = oclsProduct.Select_dept()



    '                oColSqlparram = New ColSqlparram()
    '                oColSqlparram.Add("@product_id", Val(dtN(index)("ProductId").ToString()), SqlDbType.Int)
    '                oColSqlparram.Add("@cmp_id", Val(Session("cmp_id").ToString()), SqlDbType.Int)
    '                Dim dt_dept As DataTable = GetdataTableSp("Get_Department_by_product", oColSqlparram, "Get_Department_by_product", str)

    '                Dim dr As DataRow = dt.NewRow()

    '                dr("ProductId") = Val(dtN(index)("ProductId").ToString())
    '                dr("SizeId") = Val(dtN(index)("SizeId").ToString())
    '                dr("ProductName") = dtN(index)("ProductName").ToString()
    '                dr("SizeName") = dtN(index)("SizeName").ToString()
    '                dr("Price") = dtN(index)("Price").ToString()
    '                dr("deptId") = Val(dt_dept(0)("Department_id").ToString())
    '                dr("deptName") = dt_dept(0)("Name").ToString()
    '                dr("condiment") = dtN(index)("condiment").ToString()
    '                dr("condimentId") = dtN(index)("condimentId").ToString()
    '                dr("condimentprice") = dtN(index)("condiment").ToString()
    '                dr("discount") = dtN(index)("discount").ToString()
    '                dr("discount_name") = dtN(index)("discount_name").ToString()
    '                dr("promo_id") = dtN(index)("promo_id").ToString()
    '                dr("product_groupid") = dtN(index)("product_groupid").ToString()
    '                dr("taxid") = dtN(index)("taxid").ToString()
    '                dr("taxper") = dtN(index)("taxper").ToString()
    '                dr("qty") = "1"
    '                dr("ActualCostPer") = "0"
    '                dr("DeliveryCharges") = dtN(index)("DeliveryCharges").ToString()
    '                dt.Rows.Add(dr)
    '            Next
    '        End If

    '        dt.DefaultView.Sort = "deptId ASC"
    '        Dim dtResult As DataTable = dt.DefaultView.ToTable()

    '        If dtResult.Rows.Count > 0 Then

    '            Dim last_dept As String = ""

    '            For index = 0 To dtResult.Rows.Count - 1

    '                If index = 0 Or last_dept <> dtResult(index)("deptName").ToString() Then

    '                    last_dept = dtResult(index)("deptName").ToString()

    '                    If index <> 0 Then
    '                        'GROUP SUBTOTAL
    '                        builder.Append("<tr><td><b> Sub Total</b></td>")
    '                        builder.Append("<td></td><td style ='text-align:right;'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>")

    '                        group_total = 0
    '                    End If

    '                    'Department name
    '                    builder.Append("<tr><td width='70%'><b>" + last_dept + "</b></td> <td width='5%'> </td> <td width='25%'></td></tr>")

    '                End If

    '                'product name and price
    '                builder.Append("<tr><td>" + dtResult(index)("ProductName").ToString() + " </td>                                    <td>1</td><td style='text-align:right;'>" + String.Format("{0:0.00}", Convert.ToDouble(dtResult(index)("Price").ToString())) + "</td></tr>")
    '                builder.Append("<tr><td>" + dtResult(index)("condiment").ToString() + " </td>                                    <td></td><td style='text-align:right;'> </td></tr>")
    '                builder.Append("<tr><td>" + dtResult(index)("discount").ToString() + "(" + dtResult(index)("discount_name").ToString() + ") </td>                                    <td></td><td> </td></tr>")
    '                group_total += Convert.ToDouble(dtResult(index)("Price").ToString())

    '                sub_total += Convert.ToDouble(dtResult(index)("Price").ToString())
    '            Next
    '        End If

    '        If group_total <> 0 Then
    '            'GROUP SUBTOTAL
    '            builder.Append("<tr><td><b> Sub Total</b></td>")
    '            builder.Append("<td></td><td style ='text-align:right;'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>")

    '            group_total = 0
    '        End If

    '        'builder.Append("<tr><td><b>Total</b></td>")
    '        'builder.Append("<td></td><td style ='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>")
    '        builder.Append("</table></td></tr>")
    '        builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>")
    '        builder.Append("<table width='100%'>")
    '        If (Session("diliveryOption").ToString() = "1.00") Then

    '            builder.Append("<tr><td width='70%'><b>Delivery Charges</b></td>")
    '            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtCust(0)("delivery_charges").ToString())) + "</b></td></tr>")
    '            builder.Append("<tr><td width='70%'><b>Total</b></td>")
    '            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtCust(0)("delivery_charges").ToString())) + "</b></td></tr></table></td></tr>")
    '        Else
    '            builder.Append("<tr><td width='70%'><b>Total</b></td>")
    '            builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr></table></td></tr>")
    '        End If

    '        builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>")
    '        builder.Append("<table width='100%' style='font-family:verdana;font-size:12px;'><tr><td width='70%'><b>Tax Information</b></td>")
    '        builder.Append("<td width='5%'></td><td width='25%'></td></tr>")
    '        builder.Append("<tr><td>Non-Taxable Total Sales</td><td></td>")
    '        builder.Append("<td style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr></table></td></tr>")
    '        builder.Append("<tr style='height: 50px;'><td colspan ='2' style='text-align:center;'> <b>Thanks For visiting us </b> </td></tr><tr><td>&nbsp;</td></tr></table>")

    '        builder.Append("Thanks.")
    '        builder.Append("<div>")
    '        builder.Append("</body> </html>")

    '        Dim email As String
    '        Dim Subject As String

    '        If Not Session("cust_email") Is Nothing Then
    '            email = Session("cust_email")



    '            'madhvanimitesh@gmail.com
    '            Subject = "Order Receipt"
    '            MailTo_receipt(Val(Session("cmp_id")), Val(Session("location_id")), email, Subject, builder.ToString, "", "", "")


    '            If Not dtL.Rows(0)("LoctionEmail") Is Nothing Then
    '                email = dtL.Rows(0)("LoctionEmail")



    '                'madhvanimitesh@gmail.com
    '                Subject = "Order Received"

    '                builder.Replace("Customer", "Manager")
    '                builder.Replace("Thank you for placing order with us. ", "Order Received from customer.")

    '                MailTo_receipt(Val(Session("cmp_id")), Val(Session("location_id")), email, Subject, builder.ToString, "", "", "")

    '                'destroy session


    '            End If

    '            'destroy session
    '            Session.Remove("cust_email")
    '            Session.Remove("chk_SendMail")

    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Thanks: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

    '    End Try
    'End Function
    Function send_mail()
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@location_id", Val(Session("location_id").ToString()), SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_Id").ToString()), SqlDbType.Int)
            Dim dtL As DataTable = GetdataTableSp("Get_M_Location", oColSqlparram, "Get_M_Location", str)

            Dim oColSqlParCust As ColSqlparram = New ColSqlparram()
            oColSqlParCust.Add("@sales_id", Val(Session("sales_id")), SqlDbType.Decimal)
            Dim dtCust As DataTable = GetdataTableSp("Get_Sales_Customer_Details", oColSqlParCust, "Get_Sales_Customer_Details", str)

            Session("diliveryOption") = dtCust(0)("diliveryOption").ToString()

            Dim builder As New StringBuilder

            builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
            builder.Append("<div style='width:100%;height:100%; color:#000000; font-family:verdana;border:1px solid " + dtL(0)("BG_Color").ToString() + ";'>")
            builder.Append("<div style='width:100%;height:100px;background-color:" + dtL(0)("BG_Color").ToString() + ";color: " + dtL(0)("Font_Color").ToString() + " '>")


            builder.Append("<table style='width:100%;height:100px;  font-family:verdana;background-color:" + dtL(0)("BG_Color").ToString() + ";color: " + dtL(0)("Font_Color").ToString() + "'>")
            builder.Append("<tr><td>&nbsp;")
            builder.Append("</td></tr>")
            builder.Append("<tr><td style='text-Align:center;'>")
            builder.Append(dtL(0)("company_name").ToString())
            builder.Append("</td></tr>")
            builder.Append("<tr><td>")
            builder.Append(" Phone : " + dtL(0)("contact").ToString() + " <br/> E-Mail : " + dtL(0)("email").ToString() + "")
            builder.Append("</td></tr>")
            builder.Append("<tr><td>&nbsp;")
            builder.Append("</td></tr>")
            builder.Append("</table>")

            builder.Append(" </div>")
            builder.Append("<table style='width:100%;height:200px; color:#000000;margin:5px;font-family:verdana;'>")
            builder.Append("<tr><td>")
            builder.Append("Dear Customer, ")
            builder.Append("</td></tr>")
            builder.Append("<tr><td>")
            builder.Append("Thank you for placing order with us. ")
            builder.Append("</td></tr>")
            builder.Append("<tr><td>")
            builder.Append("Order Confirmation order# " + ViewState("order_ref") + " (" + System.DateTime.Now.ToString() + ")")
            builder.Append("</td></tr>")

            builder.Append("<tr><td>")
            builder.Append("Your order will be processed on  " + dtCust(0)("deliver_date").ToString() + " " + dtCust(0)("deliver_time").ToString() + " ")
            builder.Append("</td></tr>")

            builder.Append("<tr><td>")
            builder.Append("&nbsp; ")
            builder.Append("</td></tr>")
            builder.Append("</table>")

            builder.Append("<table style=' border:1px solid #000000;font-family:verdana;font-size:12px;' cellpadding='0px' cellspacing='0px'>")
            builder.Append("<tr> <td ></td><td ></td> </tr>")

            builder.Append("<tr><td colspan='2' style='text-align:center;'><b>" + Session("store_name").ToString() + "<br/>" + dtL.Rows(0)("name").ToString() + "</b></td></tr>")
            builder.Append("<tr><td colspan='2' style='border-bottom :1px dashed #000000;border-top:1px dashed #000000;'>")
            builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'><tr>")
            builder.Append("<td width='30%'>VAT NUMBER</td>")
            builder.Append("<td width='5%'>:</td>")
            builder.Append("<td width='65%'>" + dtL.Rows(0)("Vat_No").ToString() + "</td>")
            builder.Append(" </tr></table> </tr>")
            builder.Append("<tr><td colspan='2'> <table width='100%'>")
            'builder.Append("<tr><td width='30%'>Location </td>")
            'builder.Append("<td width='5%'>:</td>")
            'builder.Append("<td width='65%' >" + dtL.Rows(0)("name").ToString() + "</td></tr>")
            builder.Append("<tr><td >Date & Time</td>")
            builder.Append("<td>:</td>")
            builder.Append("<td >" + System.DateTime.Now + "</td></tr>")
            builder.Append("<tr><td >Served By </td>")
            builder.Append("<td>:</td>")
            builder.Append("<td >Manager1</td></tr>")
            builder.Append("<tr><td >Order# </td>")
            builder.Append("<td>:</td>")

            builder.Append("<td >" + ViewState("order_ref") + "</td></tr>")

            builder.Append("<tr><td >Customer Name</td><td>:</td> <td >" + dtCust(0)("web_name").ToString() + "</td></tr>")
            builder.Append("<tr><td >Mobile</td><td>:</td> <td >" + dtCust(0)("web_mobile").ToString() + "</td></tr>")
            builder.Append("<tr><td >Email</td><td>:</td> <td >" + dtCust(0)("web_email").ToString() + "</td></tr>")
            builder.Append("<tr><td >Address</td><td>:</td> <td >" + dtCust(0)("web_address").ToString() + "</td></tr>")
            builder.Append("<tr><td >Table No</td><td>:</td> <td >" + dtCust(0)("table_name").ToString() + "</td></tr>")

            builder.Append("<tr><td Colspan='3' style='text-align:Center'><b>Tax Invoice</b></td></tr>")
            builder.Append("</table></td></tr>")
            builder.Append("<tr><td colspan='2' style='border-bottom:1px dashed #000000;border-top:1px dashed #000000'>")
            builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'>")

            Dim sub_total As Double = 0
            Dim group_total As Double = 0

            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            oColSqlPar.Add("@sales_id", Val(Session("sales_id")), SqlDbType.Decimal)
            Dim dtcart As DataTable = GetdataTableSp("Get_Cart", oColSqlPar, "Get_Cart", str)

            Dim dtN As DataTable = dtcart

            'temp table with department name and ID
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("row_id", GetType(Int32))
            dt.Columns.Add("ProductId", GetType(Int32))
            dt.Columns.Add("SizeId", GetType(Int32))
            dt.Columns.Add("ProductName", GetType(String))
            dt.Columns.Add("SizeName", GetType(String))
            dt.Columns.Add("Price", GetType(Double))
            dt.Columns.Add("tax", GetType(Double))
            dt.Columns.Add("condiment", GetType(String))
            dt.Columns.Add("condimentId", GetType(String))
            dt.Columns.Add("condimentprice", GetType(String))
            dt.Columns.Add("discount", GetType(Double))
            dt.Columns.Add("discount_name", GetType(String))
            dt.Columns.Add("promo_id", GetType(String))
            dt.Columns.Add("product_groupid", GetType(String))
            dt.Columns.Add("taxid", GetType(String))
            dt.Columns.Add("taxper", GetType(Double))
            dt.Columns.Add("deptId", GetType(Double))
            dt.Columns.Add("deptName", GetType(String))
            dt.Columns.Add("qty", GetType(Double))
            dt.Columns.Add("ActualCostPer", GetType(Double))
            dt.Columns.Add("DeliveryCharges", GetType(Double))
            dt.Columns.Add("ServiceCharges", GetType(Double))


            If dtN.Rows.Count > 0 Then

                For index = 0 To dtN.Rows.Count - 1

                    'oclsProduct.product_id = Val(dtN(index)("ProductId").ToString())
                    'oclsProduct.cmp_id = Val(Session("cmp_id").ToString())
                    'Dim dt_dept As DataTable = oclsProduct.Select_dept()



                    oColSqlparram = New ColSqlparram()
                    oColSqlparram.Add("@product_id", Val(dtN(index)("ProductId").ToString()), SqlDbType.Int)
                    oColSqlparram.Add("@cmp_id", Val(Session("cmp_id").ToString()), SqlDbType.Int)
                    Dim dt_dept As DataTable = GetdataTableSp("Get_Department_by_product", oColSqlparram, "Get_Department_by_product", str)

                    Dim dr As DataRow = dt.NewRow()

                    dr("ProductId") = Val(dtN(index)("ProductId").ToString())
                    dr("SizeId") = Val(dtN(index)("SizeId").ToString())
                    dr("ProductName") = dtN(index)("ProductName").ToString()
                    dr("SizeName") = dtN(index)("SizeName").ToString()
                    dr("Price") = dtN(index)("Price").ToString()
                    dr("deptId") = Val(dt_dept(0)("Department_id").ToString())
                    dr("deptName") = dt_dept(0)("Name").ToString()
                    dr("condiment") = dtN(index)("condiment").ToString()
                    dr("condimentId") = dtN(index)("condimentId").ToString()
                    dr("condimentprice") = dtN(index)("condiment").ToString()
                    dr("discount") = dtN(index)("discount").ToString()
                    dr("discount_name") = dtN(index)("discount_name").ToString()
                    dr("promo_id") = dtN(index)("promo_id").ToString()
                    dr("product_groupid") = dtN(index)("product_groupid").ToString()
                    dr("taxid") = dtN(index)("taxid").ToString()
                    dr("taxper") = dtN(index)("taxper").ToString()
                    dr("qty") = "1"
                    dr("ActualCostPer") = "0"
                    dr("DeliveryCharges") = dtN(index)("DeliveryCharges").ToString()
                    dr("ServiceCharges") = dtN(index)("ServiceCharges").ToString()
                    dt.Rows.Add(dr)
                Next
            End If

            dt.DefaultView.Sort = "deptId ASC"
            Dim dtResult As DataTable = dt.DefaultView.ToTable()

            If dtResult.Rows.Count > 0 Then

                Dim last_dept As String = ""

                For index = 0 To dtResult.Rows.Count - 1

                    If index = 0 Or last_dept <> dtResult(index)("deptName").ToString() Then

                        last_dept = dtResult(index)("deptName").ToString()

                        If index <> 0 Then
                            'GROUP SUBTOTAL
                            builder.Append("<tr><td><b> Sub Total</b></td>")
                            builder.Append("<td></td><td style ='text-align:right;'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>")

                            group_total = 0
                        End If

                        'Department name
                        builder.Append("<tr><td width='70%'><b>" + last_dept + "</b></td> <td width='5%'> </td> <td width='25%'></td></tr>")

                    End If

                    'product name and price
                    builder.Append("<tr><td>" + dtResult(index)("ProductName").ToString() + " </td>                                    <td>1</td><td style='text-align:right;'>" + String.Format("{0:0.00}", Convert.ToDouble(dtResult(index)("Price").ToString())) + "</td></tr>")
                    builder.Append("<tr><td>" + dtResult(index)("condiment").ToString() + " </td>                                    <td></td><td style='text-align:right;'> </td></tr>")
                    builder.Append("<tr><td>" + dtResult(index)("discount").ToString() + "(" + dtResult(index)("discount_name").ToString() + ") </td>                                    <td></td><td> </td></tr>")
                    group_total += Convert.ToDouble(dtResult(index)("Price").ToString())

                    sub_total += Convert.ToDouble(dtResult(index)("Price").ToString())
                Next
            End If

            If group_total <> 0 Then
                'GROUP SUBTOTAL
                builder.Append("<tr><td><b> Sub Total</b></td>")
                builder.Append("<td></td><td style ='text-align:right;'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>")

                group_total = 0
            End If

            'builder.Append("<tr><td><b>Total</b></td>")
            'builder.Append("<td></td><td style ='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>")
            builder.Append("</table></td></tr>")
            builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>")
            builder.Append("<table width='100%'>")
            If (Session("diliveryOption").ToString() = "1.00") Then

                builder.Append("<tr><td width='70%'><b>Delivery Charges</b></td>")
                builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtCust(0)("delivery_charges").ToString())) + "</b></td></tr>")

                builder.Append("<tr><td width='70%'><b>Service Charges</b></td>")
                builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtCust(0)("service_charges").ToString())) + "</b></td></tr>")
                builder.Append("<tr><td width='70%'><b>Total</b></td>")
                builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtCust(0)("delivery_charges").ToString()) + Val(dtCust(0)("Service_charges").ToString())) + "</b></td></tr></table></td></tr>")
            ElseIf (Session("diliveryOption").ToString() = "0.00" OrElse Session("diliveryOption").ToString() = "2.00" OrElse Session("diliveryOption").ToString() = "3.00") Then

                builder.Append("<tr><td width='70%'><b>Service Charges</b></td>")
                builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtCust(0)("service_charges").ToString())) + "</b></td></tr>")
                builder.Append("<tr><td width='70%'><b>Total</b></td>")
                builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtCust(0)("Service_charges").ToString())) + "</b></td></tr></table></td></tr>")
            Else
                builder.Append("<tr><td width='70%'><b>Total</b></td>")
                builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr></table></td></tr>")
            End If

            builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>")
            builder.Append("<table width='100%' style='font-family:verdana;font-size:12px;'><tr><td width='70%'><b>Tax Information</b></td>")
            builder.Append("<td width='5%'></td><td width='25%'></td></tr>")
            builder.Append("<tr><td>Non-Taxable Total Sales</td><td></td>")
            builder.Append("<td style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr></table></td></tr>")
            builder.Append("<tr style='height: 50px;'><td colspan ='2' style='text-align:center;'> <b>Thanks For visiting us </b> </td></tr><tr><td>&nbsp;</td></tr></table>")

            builder.Append("Thanks.")
            builder.Append("<div>")
            builder.Append("</body> </html>")
            LogHelper.Error("CF_SUCESS: mail body ")
            Dim email As String
            Dim Subject As String
            LogHelper.Error("0.1" + dtL.Rows(0)("LoctionEmail").ToString() )
            If Not Session("cust_email") Is Nothing Or Session("cust_email").ToString() = "-1" Then
                email = Session("cust_email")



                'madhvanimitesh@gmail.com
                Subject = "Order Receipt"
                Try

                    LogHelper.Error("1")
                    MailTo_receipt(Val(Session("cmp_id")), Val(Session("location_id")), email, Subject, builder.ToString, "", "", "")
                Catch ex As Exception
                    LogHelper.Error("CF_SUCESS: Cust_email : mail sent error to customer" + System.DateTime.Now.ToString() + ": " + ex.Message.ToString())

                End Try
                LogHelper.Error("CF_SUCESS: Cust_email : mail sent successfully to customer" + System.DateTime.Now.ToString() + ": " + Session("cust_email").ToString())

                LogHelper.Error("1.1")
                'destroy session
                Session.Remove("cust_email")
                Session.Remove("chk_SendMail")

            End If
            LogHelper.Error("2")
            If Not dtL.Rows(0)("LoctionEmail") Is Nothing Then
                email = dtL.Rows(0)("LoctionEmail")


                LogHelper.Error("3")
                'madhvanimitesh@gmail.com
                Subject = "Order Received"

                builder.Replace("Customer", "Manager")
                builder.Replace("Thank you for placing order with us. ", "Order Received from customer.")

                Try
                    MailTo_receipt(Val(Session("cmp_id")), Val(Session("location_id")), email, Subject, builder.ToString, "", "", "")
                    LogHelper.Error("CF_SUCESS: Cust_email : mail sent successfully to manager" + System.DateTime.Now.ToString() + ": " + email)
                    '  LogHelper.Error("CF_SUCESS: Inside MailTo_receipt Function")
                Catch ex As Exception
                    LogHelper.Error("Thanks: manager MailTo_receipt 2:" + System.DateTime.Now.ToString() + ": " + ex.Message)

                End Try
                LogHelper.Error("4")
                'destroy session
                Session.Remove("cust_email")
                Session.Remove("chk_SendMail")

            End If
            LogHelper.Error("CF_SUCESS: Cust_email :  inside thesendmail() function")
        Catch ex As Exception
            LogHelper.Error("Thanks: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        End Try
    End Function

    Private Sub get_msg()
        Try

            Dim oColSqlPar As ColSqlparram = New ColSqlparram()
            oColSqlPar.Add("@sales_id", Session("sales_id"))
            oColSqlPar.Add("@location_id", 0)
            Dim dt As DataTable = GetdataTableSp("Get_Location_by_salesID", oColSqlPar, "Get_Location_by_salesID", str)

            If dt.Rows.Count > 0 Then
                'lbl_terms.InnerText = dt.Rows(0)("header_reciept").ToString()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CheckStore()
        Try

            Dim n As String = Session("store_name")

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                str = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Decode_data(dt.Rows(0)("db_password")) + ";"
            Else


            End If
        Catch ex As Exception
            LogHelper.Error("Till:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub


    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "", Optional ByVal constr As String = "") As System.Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(constr)
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
                sdr = New System.Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Till:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing
        End Try
        Return sdr
    End Function
    Public Sub CheckPay(ByVal order_ref As String)
        Try

            strcon.Open()

            Dim cmd As SqlCommand = New SqlCommand("Get_M_Pay_check", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@order_ref", order_ref)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("store_name") = dt.Rows(0)("store_name")
                Session("sales_id") = dt.Rows(0)("sales_id")

                CheckStore()
            Else
            End If
        Catch ex As Exception
            LogHelper.Error("CF_success:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub


    Public Sub MailTo_receipt(ByVal CmpID As Int32, ByVal LocationID As Int32, ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
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
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network
            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network

            oSmtpclient.Send(oE_Mail)

        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Base_class: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
        End Try
    End Sub
End Class

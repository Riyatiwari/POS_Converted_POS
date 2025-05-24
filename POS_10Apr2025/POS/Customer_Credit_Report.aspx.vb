Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing
Imports Telerik.Web.UI.Export

Partial Class Customer_Credit_Report
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsCredit As New Controller_clsCreditAccount
    Dim oclsSales As New Controller_clsSales
    Dim dt As DataTable
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                bind_ddl()

                Dim customerId As Integer
                Dim startDate As DateTime
                Dim endDate As DateTime


                If Integer.TryParse(Request.QueryString("customer_id"), customerId) Then
                    RadAccount.SelectedValue = customerId.ToString()
                End If

                If DateTime.TryParse(Request.QueryString("start_date"), startDate) Then
                    txtFromDate.SelectedDate = startDate
                Else
                    txtFromDate.SelectedDate = System.DateTime.Now ' First day of current month
                End If

                If DateTime.TryParse(Request.QueryString("end_date"), endDate) Then
                    txtToDate.SelectedDate = endDate
                Else
                    txtToDate.SelectedDate = System.DateTime.Now ' Current date
                End If


                'txtFromDate.SelectedDate = System.DateTime.Now
                'txtToDate.SelectedDate = System.DateTime.Now


                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Customer_Credit_Report:Page_Load:" + ex.Message)
        End Try

    End Sub


    Public Sub bindGrid()
        Try

            oclsCredit.cmp_id = Session("cmp_id")
            oclsCredit.customer_web_id = RadAccount.SelectedValue

            If txtFromDate.SelectedDate Is Nothing Then
                oclsCredit.start_date = DateTime.Now
            Else
                oclsCredit.start_date = txtFromDate.SelectedDate
            End If


            If txtToDate.SelectedDate Is Nothing Then
                oclsCredit.end_date = DateTime.Now
            Else
                oclsCredit.end_date = txtToDate.SelectedDate
            End If

            Dim dtCustomer As DataTable = oclsCredit.Select_customer()

            If dtCustomer.Rows.Count > 0 Then
                rdCustomer.DataSource = dtCustomer
                rdCustomer.DataBind()
            Else
                rdCustomer.DataSource = String.Empty
                rdCustomer.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Credit_Account_List:bindGrid" + ex.Message)
        End Try

    End Sub

    Public Sub bind_ddl()
        Try
            oclsCredit.cmp_id = Session("cmp_id")

            Dim dt As DataTable = oclsCredit.SelectAccount_cust()
            If (dt.Rows.Count > 0) Then

                RadAccount.DataSource = dt
                RadAccount.DataTextField = "CustomerName"
                RadAccount.DataValueField = "customer_id"
                RadAccount.DataBind()

            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            RadAccount.Items.Insert(0, li)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Customer_Credit_Report:bindGrid" + ex.Message)
        End Try

    End Sub

    Protected Sub lnkView_Click(sender As Object, e As EventArgs) Handles lnkView.Click
        Try

            oclsCredit.cmp_id = Session("cmp_id")
            oclsCredit.customer_web_id = RadAccount.SelectedValue

            If txtFromDate.SelectedDate Is Nothing Then
                oclsCredit.start_date = DateTime.Now
            Else
                oclsCredit.start_date = txtFromDate.SelectedDate
            End If


            If txtToDate.SelectedDate Is Nothing Then
                oclsCredit.end_date = DateTime.Now
            Else
                oclsCredit.end_date = txtToDate.SelectedDate
            End If

            Dim dtCustomer As DataTable = oclsCredit.Select_customer()

            If dtCustomer.Rows.Count > 0 Then
                rdCustomer.DataSource = dtCustomer
                rdCustomer.DataBind()
            Else
                rdCustomer.DataSource = String.Empty
                rdCustomer.DataBind()
            End If
        Catch ex As Exception
            LogHelper.Error("Customer_Credit_Report:lnkNew_Click:" + ex.Message)
        End Try
    End Sub

    'Protected Sub txtFromDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
    '    txtToDate.MinDate = txtFromDate.SelectedDate

    'End Sub
    Protected Sub rdCustomer_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try

            Dim i As Integer = e.Item.ItemIndex
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            Dim sales_id As String = TryCast(rdCustomer.Items(i).FindControl("hf_sales_id"), HiddenField).Value
            Dim rptOrders As Repeater = TryCast(rdCustomer.Items(i).FindControl("rptOrders"), Repeater)
            Dim pnlOrders As Panel = TryCast(rdCustomer.Items(i).FindControl("pnlOrders"), Panel)
            Dim btnExpand As Button = TryCast(rdCustomer.Items(i).FindControl("btnExpand"), Button)

            If e.CommandName = "Mail" Then

                send_mail(Val(e.CommandArgument.ToString()))

            Else
                If btnExpand.Text = "+" Then

                    oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                    oColSqlparram.Add("@Sale_id", Val(sales_id), SqlDbType.Int)
                    dt = oClsDal.GetdataTableSp("Credit_Transaction_Details", oColSqlparram)


                    If dt.Rows.Count > 0 Then
                        rptOrders.DataSource = dt
                        rptOrders.DataBind()
                    Else
                        rptOrders.DataSource = String.Empty
                        rptOrders.DataBind()
                    End If

                    btnExpand.Text = "-"
                    pnlOrders.Visible = True
                Else
                    btnExpand.Text = "+"
                    pnlOrders.Visible = False
                End If
            End If

        Catch ex As Exception
            LogHelper.Error("Customer_Credit_Report:rdCustomer_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkview_Click1(sender As Object, e As EventArgs)
        Try
            Dim btn As LinkButton = DirectCast(sender, LinkButton)
            Dim arr As String() = btn.CommandArgument.Split("#")
            Dim sales As Int32 = Val(arr(0).ToString())
            Dim tranuuid As String = arr(1).ToString()
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@sales_id", Val(sales), SqlDbType.Int)
            oColSqlparram.Add("@tran_uuid", tranuuid)
            dt = oClsDal.GetdataTableSp("R_Table_Transaction_Details_View", oColSqlparram)

            If (dt.Rows.Count > 0) Then

                Session("dt") = dt

                Dim url As String = "Table_Transaction_Detail.aspx"

                ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

            End If

            'rdDeatilView.DataSource = dt
            'rdDeatilView.DataBind()
            'If (dt.Rows.Count > 0) Then
            '    lblSurcharge.Text = dt.Rows(0)("Surcharge_Amount").ToString()
            '    lblPayment.Text = dt.Rows(0)("Payment_Type").ToString()

            'End If

            'upDetail.Update()
            'Dim script As String = "function f(){$find(""" + rdwinDetail.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyallergy", script, True)

        Catch ex As Exception

        End Try
    End Sub


    Function send_mail(sales_id As Integer)
        Try
            '--------------------get sales detail----------------------------
            oclsSales.sales_id = Val(sales_id)
            'oclsSales.cmp_id = Val(Session("cmp_id"))
            Dim dtS As DataTable = oclsSales.Select_credit_sales_email()

            If dtS.Rows.Count > 0 Then

                For sales = 0 To dtS.Rows.Count - 1


                    Dim builder As New StringBuilder

                    builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
                    builder.Append("<div style='width:100%;height:100%; color:#000000; font-family:verdana;border:1px solid " + dtS(0)("BG_Color").ToString() + ";'>")
                    builder.Append("<div style='width:100%;height:100px;background-color:" + dtS(0)("BG_Color").ToString() + ";color: " + dtS(0)("Font_Color").ToString() + " '>")


                    builder.Append("<table style='width:100%;height:100px;  font-family:verdana;background-color:" + dtS(0)("BG_Color").ToString() + ";color: " + dtS(0)("Font_Color").ToString() + "'>")
                    builder.Append("<tr><td>&nbsp;")
                    builder.Append("</td></tr>")
                    builder.Append("<tr><td style='text-Align:center;'>")
                    builder.Append(dtS(0)("company_name").ToString())
                    builder.Append("</td></tr>")
                    builder.Append("<tr><td>")
                    builder.Append(" Phone : " + dtS(0)("cmp_contact").ToString() + " <br/> E-Mail : " + dtS(0)("cmp_email").ToString() + "")
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
                    builder.Append("Order Confirmation order# " + dtS(sales)("sales_id").ToString() + " (" + System.DateTime.Now.ToString() + ")")
                    builder.Append("</td></tr>")

                    builder.Append("</table>")

                    builder.Append("<table style='width:100%; border:1px solid #000000;font-family:verdana;font-size:12px;' cellpadding='0px' cellspacing='0px'>")
                    builder.Append("<tr> <td ></td><td ></td> </tr>")

                    builder.Append("<tr style='height: 50px;'><td colspan='2' style='text-align:center;'><b>" + Session("store").ToString() + "<br/>" + dtS.Rows(0)("location_name").ToString() + "</b></td></tr>")  '"<br/>" + dtS.Rows(0)("name").ToString() +
                    builder.Append("<tr><td colspan='2' style='border-bottom :1px dashed #000000;border-top:1px dashed #000000;'>")
                    builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'><tr>")
                    builder.Append("<td width='30%'>VAT NUMBER</td>")
                    builder.Append("<td width='5%'>:</td>")
                    builder.Append("<td width='65%'>" + dtS.Rows(0)("Vat_No").ToString() + "</td>")
                    builder.Append(" </tr></table> </tr>")
                    builder.Append("<tr><td colspan='2'> <table width='100%'>")

                    'builder.Append("<tr><td >Till Name </td>")
                    'builder.Append("<td>:</td>")
                    'builder.Append("<td >" + Session("store").ToString() + "</td></tr>")
                    builder.Append("<tr><td >Date & Time</td>")
                    builder.Append("<td>:</td>")
                    builder.Append("<td >" + System.DateTime.Now.ToString() + "</td></tr>")

                    builder.Append("<tr><td >Served By </td>")
                    builder.Append("<td>:</td>")
                    builder.Append("<td >" + dtS.Rows(sales)("StaffName").ToString() + "</td></tr>")
                    builder.Append("<tr><td >Order# </td>")
                    builder.Append("<td>:</td>")
                    builder.Append("<td >" + dtS(sales)("sales_id").ToString() + "</td></tr>")

                    builder.Append("<tr><td >Customer Name</td><td>:</td> <td >" + dtS(sales)("cust_name").ToString() + "</td></tr>")
                    builder.Append("<tr><td >Mobile</td><td>:</td> <td >" + dtS(sales)("Mobile").ToString() + "</td></tr>")
                    builder.Append("<tr><td >Email</td><td>:</td> <td >" + dtS(sales)("cust_email").ToString() + "</td></tr>")
                    builder.Append("<tr><td >Address</td><td>:</td> <td >" + dtS(sales)("cust_address").ToString() + "</td></tr>")
                    'builder.Append("<tr><td >Table No</td><td>:</td> <td >" + Session("CartTable").ToString() + "</td></tr>")

                    builder.Append("<tr><td >Table </td><td>:</td> <td >" + dtS.Rows(sales)("table_name").ToString() + "</td></tr>")

                    builder.Append("<tr><td>&nbsp;")
                    builder.Append("</td></tr>")

                    builder.Append("<tr><td Colspan='3' style='text-align:Center'><b>Tax Invoice</b></td></tr>")
                    builder.Append("</table></td></tr>")
                    builder.Append("<tr><td colspan='2' style='border-bottom:1px dashed #000000;border-top:1px dashed #000000'>")
                    builder.Append("<table width='100%' style='  font-family:verdana;font-size:12px;'>")

                    Dim sub_total As Double = 0
                    Dim group_total As Double = 0
                    Dim sub_total_tax As Double = 0


                    oclsSales.cmp_id = Val(Session("cmp_id"))
                    oclsSales.sales_id = Val(dtS.Rows(sales)("sales_id").ToString())
                    Dim dtResult As DataTable = oclsSales.Select_tsales_for_email()

                    If dtResult.Rows.Count > 0 Then

                        Dim last_dept As String = ""

                        For index = 0 To dtResult.Rows.Count - 1

                            If index = 0 Or last_dept <> dtResult(index)("deptName").ToString() Then

                                last_dept = dtResult(index)("deptName").ToString()

                                If index <> 0 Then
                                    'GROUP SUBTOTAL
                                    builder.Append("<tr> <td colspan='2' width='70%' style='border-top:1px dashed #000000'><b>Sub Total</b></td>")
                                    builder.Append("<td width='5%' style='border-top:1px dashed #000000'></td><td width='25%' style ='text-align:right; border-top:1px dashed #000000'><b>" + String.Format("{0:0.00}", group_total) + "</b></td></tr>")

                                    group_total = 0
                                End If

                                'Department name
                                builder.Append("<tr><td width='70%'><b>" + last_dept + "</b></td> <td width='5%'> </td> <td width='25%'></td></tr>")

                            End If

                            'product name and price
                            builder.Append("<tr><td>" + dtResult(index)("ProductName").ToString() + " </td> <td>1</td><td style='text-align:right;'>" + String.Format("{0:0.00}", Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())) + "</td></tr>")
                            builder.Append("<tr><td>" + dtResult(index)("condiment").ToString() + " </td> <td></td><td style='text-align:right;'> </td></tr>")
                            builder.Append("<tr><td>" + dtResult(index)("discount").ToString() + "(" + dtResult(index)("discount_name").ToString() + ") </td> <td></td><td> </td></tr>")

                            group_total += Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())

                            sub_total += Convert.ToDouble(dtResult(index)("sales_total_amount").ToString())
                            sub_total_tax += Convert.ToDouble(dtResult(index)("tax").ToString())
                        Next
                    End If

                    If group_total <> 0 Then
                        'GROUP SUBTOTAL
                        builder.Append("<tr><td width='70%' colspan='2' style='border-top:1px dashed #000000'><b>Sub Total</b></td>")
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
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtS.Rows(sales)("delivery_charges").ToString())) + "</b></td></tr></table></td></tr>")
                    Else
                        builder.Append("<tr><td width='70%'><b>Total</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr></table></td></tr>")
                    End If
                    If (Val(dtS.Rows(sales)("Service_charges").ToString()) > 0) Then


                        builder.Append("<tr><td width='70%'><b>Service Charges</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("Service_charges").ToString())) + "</b></td></tr>")
                        builder.Append("<tr><td width='70%'><b>Total</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtS.Rows(sales)("Service_charges").ToString())) + "</b></td></tr></table></td></tr>")
                    Else
                        builder.Append("<tr><td width='70%'><b>Total</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr></table></td></tr>")
                    End If




                    builder.Append("<tr><td colspan ='2' style='border-bottom:1px dashed #000000;'>")
                    builder.Append("<table width='100%' style='font-family:verdana;font-size:12px;'><tr><td width='70%'><b>Tax Information</b></td>")
                    builder.Append("<td width='5%'></td><td width='25%'></td></tr>")

                    builder.Append("<tr><td>&nbsp;")
                    builder.Append("</td></tr>")

                    builder.Append("<tr><td width='70%'>VAT Total Sales</td><td width='5%'></td>")
                    builder.Append("<td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>")

                    builder.Append("<tr><td width='70%'>VAT Tax Collection</td><td width='5%'></td>")
                    builder.Append("<td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total_tax) + "</b></td></tr>")

                    'builder.Append("<tr><td>Non-Taxable Total Sales</td><td></td>")
                    'builder.Append("<td style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total) + "</b></td></tr>")


                    builder.Append("<tr><td>&nbsp; </td></tr>")

                    builder.Append("</table></td></tr>")
                    builder.Append("<tr style='height: 20px;'><td colspan ='2' style='text-align:center;'> <b>Thanks For visiting us </b> </td></tr><tr><td>&nbsp;</td></tr></table>")

                    builder.Append("<br />")

                    builder.Append("Thanks.")
                    builder.Append("<br />")
                    builder.Append("<div>")
                    builder.Append("</body> </html>")

                    Dim email As String
                    Dim Subject As String

                    If Not dtS.Rows(sales)("cust_email").ToString() Is Nothing Then
                        email = dtS.Rows(sales)("cust_email").ToString()

                        'madhvanimitesh@gmail.com
                        Subject = "Order Receipt"

                        'MailTo_receipt(Val(Session("cmp_id")), Val(0), email, Subject, builder.ToString, "", "", "")

                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Mail sent successfully.');", True)
                    End If
                Next


            End If

        Catch ex As Exception
            LogHelper.Error("Customer_credit_report: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        End Try
    End Function

End Class

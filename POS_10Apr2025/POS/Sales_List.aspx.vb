Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing
'Imports Telerik.Reporting

Partial Class Sales_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsSales As New Controller_clsSales


    Public Sub bindGrid()
        Try
            oclsSales.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsSales.SelectAll()

            If dt.Rows.Count > 0 Then
                rdSales.DataSource = dt
                rdSales.DataBind()
            Else
                rdSales.DataSource = String.Empty
                rdSales.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Sales_List:bindGrid:" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsSales.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsSales.SelectAll()

            If dt.Rows.Count > 0 Then
                rdSales.DataSource = dt
            Else
                rdSales.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Sales_List:rebindGrid:" + ex.Message)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Then
                    divCustomer.Visible = True
                Else
                    divCustomer.Visible = False
                End If

                bindGrid()
            End If
            ' bindGrid()
        Catch ex As Exception
            LogHelper.Error("Sales_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Sales List"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_add = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If

            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Sales_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdSales_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdSales.ItemCreated
    '    If TypeOf e.Item Is GridFilteringItem Then
    '        For Each nColumn As GridColumn In rdSales.MasterTableView.Columns
    '            If TypeOf nColumn Is GridDateTimeColumn Then
    '                Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                Dim dateItem As RadDateInput = DirectCast(filerItem(nColumn.UniqueName).Controls(1), RadDateInput)
    '                dateItem.TabIndex = 1
    '            ElseIf TypeOf nColumn Is GridBoundColumn Then
    '                Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                textItem.TabIndex = 1
    '            End If
    '        Next
    '    End If
    'End Sub

    'Protected Sub rdSales_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdSales.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then

    '            Dim hdntransaction_count As HiddenField = (TryCast(e.Item.FindControl("hdntransaction_count"), HiddenField))
    '            Dim hdntsales_count As HiddenField = (TryCast(e.Item.FindControl("hdntsales_count"), HiddenField))
    '            Dim lblstr As Label = (TryCast(e.Item.FindControl("lblstar"), Label))
    '            Dim msalescount As Integer
    '            Dim tsalescount As Integer

    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)


    '            If hdntransaction_count.Value <> String.Empty Then

    '                msalescount = hdntransaction_count.Value

    '            End If

    '            If hdntsales_count.Value <> String.Empty Then

    '                tsalescount = hdntsales_count.Value

    '            End If

    '            If (msalescount <> tsalescount) Then
    '                'dataItem.BackColor = Drawing.Color.Bisque
    '                lblstr.Visible = True
    '            Else

    '            End If

    '            If Val(ViewState("view")) = 1 Then
    '                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
    '                Else
    '                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
    '                End If

    '            End If

    '    Catch ex As Exception
    '        LogHelper.Error("Sales_List:rdSales_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub


    'Protected Sub rdSales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdSales.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Sales_List:rdSales_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub rdSales_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Session("sales_id") = Val(e.CommandArgument)
                Response.Redirect("Sales_View.aspx", False)

            ElseIf e.CommandName = "Mail" Then

                Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)

                Session("set_salesID") = e.CommandArgument.ToString()

                Dim dt1 As DataTable
                Dim oClsDal1 As ClsDataccess = New ClsDataccess()
                Dim oColSqlparram1 As ColSqlparram = New ColSqlparram()
                oColSqlparram1.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                oColSqlparram1.Add("@sales_id", e.CommandArgument.ToString())
                dt1 = oClsDal1.GetdataTableSp("get_CustDetail_by_salesID_ForNonTable", oColSqlparram1)

                If dt1.Rows.Count > 0 Then

                    txt_Email.Text = dt1.Rows(0)("Cust_Email").ToString()

                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Sales_List:rdSales_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Protected Sub rdSales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If (TypeOf e.Item Is RepeaterItem) Then
                Dim hd_is_table As HiddenField = TryCast(e.Item.FindControl("hd_is_table"), HiddenField)
                Dim btn_mail As LinkButton = TryCast(e.Item.FindControl("btn_mail"), LinkButton)

                If hd_is_table.Value = 0 Then
                    btn_mail.Visible = True
                Else
                    btn_mail.Visible = False
                End If

            End If

        Catch ex As Exception
            LogHelper.Error("Sales_List:rdSales_ItemDataBound:" + ex.Message)
        End Try
    End Sub
    Protected Sub btn_send_Click(sender As Object, e As EventArgs)
        Try

            If (txt_Email.Text.ToString() IsNot "") And (Session("set_salesID").ToString() IsNot "") Then

                send_mail(txt_Email.Text.ToString(), Session("set_salesID").ToString())

                Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)

            End If

        Catch ex As Exception
            LogHelper.Error("Sales_List:btn_send_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btn_cancel_Click(sender As Object, e As EventArgs)
        Try
            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception
            LogHelper.Error("Sales_List:btn_cancel_Click:" + ex.Message)
        End Try
    End Sub

    Function send_mail(email_id As String, salesID As String)
        Try
            '--------------------get sales detail----------------------------
            oclsSales.sales_id = Val(salesID)
            oclsSales.cmp_id = Val(Session("cmp_id").ToString())
            Dim dtS As DataTable = oclsSales.Select_NonTable_SalesDetail_for_email()

            If dtS.Rows.Count > 0 Then
                LogHelper.Error("Table_transaction:Count:" & dtS.Rows.Count)
                For sales = 0 To dtS.Rows.Count - 1

                    Dim builder As New StringBuilder

                    builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
                    builder.Append("<div style='width:70%; color:#000000; font-family:verdana;border:1px solid " + dtS.Rows(sales)("BG_Color").ToString() + ";'>")

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

                    oclsSales.cmp_id = Val(Session("cmp_id"))
                    oclsSales.sales_id = Val(dtS.Rows(sales)("sales_id").ToString())
                    Dim dtResult As DataTable = oclsSales.Select_tsales_for_email()


                    If dtResult.Rows.Count > 0 Then

                        LogHelper.Error("Table_transaction:Check 4 : count")

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
                    Dim total As Double = sub_total
                    If (Val(dtS.Rows(sales)("delivery_charges").ToString()) > 0) Then

                        builder.Append("<tr><td width='70%'><b>Delivery Charges</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("delivery_charges").ToString())) + "</b></td></tr>")
                        'builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("delivery_charges").ToString())) + "</b></td></tr>")
                        total += Val(dtS.Rows(sales)("delivery_charges").ToString())
                        'builder.Append("<tr><td width='70%'><b>Total</b></td>")
                        'builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", sub_total + Val(dtS.Rows(sales)("delivery_charges").ToString())) + "</b></td></tr>")


                    End If

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''service_charges added on 22 july 2024 by riya 

                    If (Val(dtS.Rows(sales)("Service_charges").ToString()) > 0) Then

                        builder.Append("<tr><td width='70%'><b>Service Charges</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("Service_charges").ToString())) + "</b></td></tr>")
                        ' builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("Service_charges").ToString())) + "</b></td></tr>")
                        total += Val(dtS.Rows(sales)("Service_charges").ToString())

                    End If


                    builder.Append("<tr><td width='70%'><b>Total</b></td>")
                    builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", total) + "</b></td></tr>")
                    If (Val(dtS.Rows(sales)("balance").ToString()) > 0) Then
                        builder.Append("<tr><td>&nbsp;</td></tr> <tr><td width='70%'><b>Balance</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("balance").ToString())) + "</b></td></tr> ")

                    End If

                    LogHelper.Error("Table_transaction:Check 6")

                    If (Val(dtS.Rows(sales)("change").ToString()) > 0) Then

                        builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("payment_amount").ToString()) + Val(dtS.Rows(sales)("change").ToString())) + "</b></td></tr> ")

                        builder.Append("<tr><td width='70%'><b>Change</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("change").ToString())) + "</b></td></tr> <tr><td>&nbsp;</td></tr> </table></td></tr>")
                    Else

                        builder.Append("<tr><td width='70%'><b>Cash Tender Amount</b></td>")
                        builder.Append("<td width='5%'></td><td width='25%' style='text-align:right;'><b>" + String.Format("{0:0.00}", Val(dtS.Rows(sales)("payment_amount").ToString())) + "</b></td></tr> <tr><td>&nbsp;</td></tr> </table></td></tr>")

                    End If


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

                    Dim email As String
                    Dim Subject As String

                    email = txt_Email.Text.ToString()

                    'madhvanimitesh@gmail.com , mitesh.m@technometrics.in
                    Subject = "Order Receipt"

                    LogHelper.Error("Table_transaction:email_id:" & email)

                    MailTo_receipt(Val(Session("cmp_id")), Val(0), email, Subject, builder.ToString, "", "", "")

                    LogHelper.Error("Table_transaction:email_id:" & email)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Mail sent successfully.');", True)

                Next

            End If

        Catch ex As Exception
            LogHelper.Error("Sales_List:send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        End Try

    End Function
End Class


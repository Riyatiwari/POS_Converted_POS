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

Partial Class Table_Transaction
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole
    Dim oclsProductGroup As New Controller_clsCategoryMain
    Dim dt As DataTable
    Public Sub binddll()
        Try
            If radVenue.SelectedIndex = -1 Then
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TableReport:binddll:" + ex.Message)
        End Try
    End Sub


    Public Sub binddllloc()
        Try
            If radLocation.SelectedIndex = -1 Then
                oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TableReport:binddll:" + ex.Message)
        End Try
    End Sub


    Public Sub binddllMachine()
        Try
            If radMachine.SelectedIndex = -1 Then
                oclsBind.BindMachine(radMachine, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("TableReport:binddll:" + ex.Message)
        End Try
    End Sub
    Private Sub Table_Transaction_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If
            binddll()
            binddllloc()
            binddllMachine()
            divcustom.Visible = True
            txtFrom.SelectedDate = System.DateTime.Now
            txtTo.SelectedDate = System.DateTime.Now
            BindData()

        End If
    End Sub

    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radVenue.SelectedIndexChanged
        Try
            If radVenue.SelectedIndex = 0 Then
                radLocation.Items.Clear()
                radMachine.Items.Clear()
            Else
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
                radMachine.Items.Clear()
            End If

        Catch ex As Exception
            LogHelper.Error("ZReport:radVenue_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radLocation.SelectedIndexChanged
        Try
            If radLocation.SelectedIndex = 0 Then
                radMachine.Items.Clear()
            Else
                oclsBind.BindMachineByLocation(radMachine, Val(Session("cmp_id")), Val(radLocation.SelectedValue))
            End If
        Catch ex As Exception
            LogHelper.Error("ZReport:radLocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        BindData()
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True

            Else
                divcustom.Visible = False

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Public Sub BindData()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            If Request.QueryString("Date") IsNot Nothing Then
                txtFrom.SelectedDate = Convert.ToDateTime(Request.QueryString("Date")).ToString("yyyy-MM-dd hh:mm tt")
                txtTo.SelectedDate = Convert.ToDateTime(Request.QueryString("Date")).ToString("yyyy-MM-dd hh:mm tt")
                oColSqlparram.Add("@from_date", Convert.ToDateTime(Request.QueryString("Date")).ToString("yyyy-MM-dd hh:mm tt"), SqlDbType.DateTime)
                oColSqlparram.Add("@to_date", Convert.ToDateTime(Request.QueryString("Date")).ToString("yyyy-MM-dd hh:mm tt"), SqlDbType.DateTime)

            Else
                oColSqlparram.Add("@from_date", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
                oColSqlparram.Add("@to_date", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)

            End If
            If Request.QueryString("L_ID") IsNot Nothing And Request.QueryString("L_ID") IsNot "" Then
                radLocation.SelectedValue = Val(Request.QueryString("L_ID").ToString())
                oColSqlparram.Add("@location_id", Val(Request.QueryString("L_ID").ToString()))
            Else
                oColSqlparram.Add("@location_id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))
            End If


            oColSqlparram.Add("@machine_id", IIf(radMachine.SelectedIndex > 0, radMachine.SelectedValue, "0"))
            oColSqlparram.Add("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            oColSqlparram.Add("@open_table", IIf(chkOpenTables.Checked = True, 1, 0), SqlDbType.Int)
            oColSqlparram.Add("@PayType", IIf(ddl_PayType.SelectedIndex > 0, ddl_PayType.SelectedValue, "-1"))

            dt = oClsDal.GetdataTableSp("R_Table_Transaction", oColSqlparram)

            If Request.QueryString("L_ID") IsNot Nothing And Request.QueryString("L_ID") IsNot "" And
              Request.QueryString("Date") IsNot Nothing And Request.QueryString("T_name") IsNot Nothing And
              Request.QueryString("S_ID") IsNot Nothing And Request.QueryString("S_ID") IsNot "" Then

                Dim dv As DataView = dt.DefaultView

                dv.RowFilter = "table_name='" + Request.QueryString("T_name").ToString() + "' AND sales_id=" + Request.QueryString("S_ID").ToString()

                If dv.ToTable.Rows.Count > 0 Then
                    rdCategory.DataSource = dt
                    rdCategory.DataBind()
                Else
                    rdCategory.DataSource = String.Empty
                    rdCategory.DataBind()
                End If
            Else

                If dt.Rows.Count > 0 Then
                    rdCategory.DataSource = dt
                    rdCategory.DataBind()
                Else
                    rdCategory.DataSource = String.Empty
                    rdCategory.DataBind()
                End If

            End If

        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lnkview_Click(sender As Object, e As EventArgs)
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

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdCategory_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try

            If e.CommandName = "Mail" Then

                Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)

                Session("set_salesID") = e.CommandArgument.ToString()

                Dim dt1 As DataTable
                Dim oClsDal1 As ClsDataccess = New ClsDataccess()
                Dim oColSqlparram1 As ColSqlparram = New ColSqlparram()
                oColSqlparram1.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                oColSqlparram1.Add("@sales_id", e.CommandArgument.ToString())
                dt1 = oClsDal1.GetdataTableSp("get_customer_detail_by_salesID", oColSqlparram1)

                If dt1.Rows.Count > 0 Then

                    txt_Email.Text = dt1.Rows(0)("Cust_Email").ToString()

                End If

            Else

                Dim i As Integer = e.Item.ItemIndex
                Dim oClsDal As ClsDataccess = New ClsDataccess()
                Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                'Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
                Dim hdlocation As HiddenField = DirectCast(rdCategory.Items(i).FindControl("hdLocation"), HiddenField)
                Dim hddatetime As HiddenField = DirectCast(rdCategory.Items(i).FindControl("hdDatetime"), HiddenField)
                Dim hdtableuuid As HiddenField = DirectCast(rdCategory.Items(i).FindControl("hdtable_uuid"), HiddenField)
                Dim Ref_id As HiddenField = DirectCast(rdCategory.Items(i).FindControl("hfNested_Ref_id"), HiddenField)
                Dim rptOrders As Repeater = DirectCast(rdCategory.Items(i).FindControl("rptOrders"), Repeater)
                Dim pnlOrders As Panel = DirectCast(rdCategory.Items(i).FindControl("pnlOrders"), Panel)
                Dim btnExpand As Button = DirectCast(rdCategory.Items(i).FindControl("btnExpand"), Button)


                If btnExpand.Text = "+" Then

                    oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                    oColSqlparram.Add("@ref_id", Val(Ref_id.Value), SqlDbType.Int)
                    oColSqlparram.Add("@location_id", Val(hdlocation.Value), SqlDbType.Int)
                    oColSqlparram.Add("@for_date", hddatetime.Value, SqlDbType.DateTime)
                    oColSqlparram.Add("@table_uuid", hdtableuuid.Value)
                    dt = oClsDal.GetdataTableSp("R_Table_Transaction_Details", oColSqlparram)


                    If dt.Rows.Count > 0 Then
                        rptOrders.DataSource = dt
                        rptOrders.DataBind()
                    Else
                        rptOrders.DataSource = String.Empty
                        rptOrders.DataBind()
                    End If

                    btnExpand.Text = "-"
                    pnlOrders.Visible = True

                    'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", "myFunction(); ", True)
                Else
                    btnExpand.Text = "+"
                    pnlOrders.Visible = False
                End If

            End If

            If e.CommandName = "close" Then
                Dim dt1 As DataTable
                Dim oClsDal1 As ClsDataccess = New ClsDataccess()
                Dim oColSqlparram1 As ColSqlparram = New ColSqlparram()
                oColSqlparram1.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                oColSqlparram1.Add("@table_uuid", e.CommandArgument.ToString())
                dt1 = oClsDal1.GetdataTableSp("R_Table_Transaction_Close", oColSqlparram1)

                If dt1.Rows.Count > 0 Then
                    BindData()
                End If

            End If

        Catch ex As Exception
            LogHelper.Error("table_transaction:rdCategory_ItemCommand:" + ex.Message)
        End Try
    End Sub


    Protected Sub btn_cancel_Click(sender As Object, e As EventArgs)
        Try
            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception

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

        End Try
    End Sub

    Function send_mail(email_id As String, table_uuid As String)
        Try
            '--------------------get sales detail----------------------------
            oclsSales.table_uuid = table_uuid
            Dim dtS As DataTable = oclsSales.Select_credit_sales_email()

            If dtS.Rows.Count > 0 Then

                For sales = 0 To 0

                    If Val(dtS.Rows(sales)("total_amount").ToString()) > 0 Then

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

                        Dim email As String
                        Dim Subject As String

                        email = txt_Email.Text.ToString()

                        'madhvanimitesh@gmail.com , mitesh.m@technometrics.in
                        Subject = "Order Receipt"

                        LogHelper.Error("Table_transaction:email_id:" & email)

                        MailTo_receipt(Val(Session("cmp_id")), Val(0), email, Subject, builder.ToString, "", "", "")
                        'MailTo_new(Val(Session("cmp_id")), email, Subject, builder.ToString, "", "", "")

                        LogHelper.Error("Table_transaction:email_id:" & email)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Mail sent successfully.');", True)


                    End If

                Next





            End If

        Catch ex As Exception
            LogHelper.Error("Table_transaction: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        End Try

    End Function

End Class

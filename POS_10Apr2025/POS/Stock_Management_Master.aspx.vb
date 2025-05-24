Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient


Partial Class Stock_Management_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclstock As New Controller_clsStock()
    Dim oclsProduct As New Controller_clsProduct()
    Dim oclsSize As New Controller_clsSize()
    Dim t As Decimal
    Dim p As String
    Dim I As Decimal
    Dim M As String
    Dim tax_id As Integer
    Dim mode As String = ""
    Dim tax_val As Decimal = 0
    Dim t_id As Integer = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Not Session("stock_id") = Nothing Then
                    If Session("cmp_id") = Nothing Then
                        Response.Redirect("SignIn.aspx", False)
                    End If
                    gd_Product.Visible = True
                    oclsBind.BindLocation_new(rdlocation, Val(Session("cmp_id")))
                    rdlocation.SelectedIndex = 0
                    BindProduct()
                    BindGrid_Edit()
                    bindIngredientGrid()
                    rdTemplate.Enabled = False
                Else
                    rdTemplate.Enabled = True
                    gd_Product.Visible = True
                    txtdate.SelectedDate = DateTime.Now
                    txtdate.MaxDate = DateTime.Now

                    'Add on 16/03/2021
                    txtdate.MinDate = DateTime.Now

                    If Session("cmp_id") = Nothing Then
                        Response.Redirect("SignIn.aspx", False)
                    End If

                    gd_Product.Visible = True

                    oclsBind.BindLocation_new(rdlocation, Val(Session("cmp_id")))
                    rdlocation.SelectedIndex = 0
                    oclstock.cmp_id = Val(Session("cmp_id"))
                    oclstock.product_id = 0
                    oclstock.SingleRecord = 0
                    Dim dtSize As DataTable = oclstock.Select_SizeNPrice_By_Product()
                    ViewState("View_Size") = dtSize
                    BindGrid()
                    bindSortingNo()
                    bindIngredientGrid()

                    oclsProduct.Pro_Id = "0"
                    oclsProduct.cmp_id = Val(Session("cmp_id"))
                    oclsProduct.row_id = 0
                    Dim dt As DataTable = oclsProduct.Get_Product_For_Stock_change()
                    ViewState("Stock_Data_Change") = dt
                End If

                oclsBind.bindTemplate(rdTemplate, Val(Session("cmp_id")))

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:Page_Load" + ex.Message)
        End Try
    End Sub


    Private Sub bindSortingNo()
        Try
            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.form_name = "Stock Purchase"
            Dim dtTable As DataTable = oclstock.Select1()
            If dtTable.Rows.Count > 0 Then
                Dim dt As String = (dtTable.Rows(0)("receipt_number"))
                txtreceipt.Text = dt
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:bindSortingNo" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGrid_Edit()
        Try
            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.stk_rec_id = Val(Session("stock_id"))
            Dim dt As DataTable = oclstock.SelecStockManagementDetail_Edit()

            ViewState("Stock_Data_Change") = dt

            Dim dv As DataView = dt.DefaultView
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                gd_Product.DataSource = dv
                gd_Product.DataBind()
            Else
                gd_Product.Enabled = True
                gd_Product.Visible = True
                gd_Product.DataSource = String.Empty
                gd_Product.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Detail:BindGrid_Edit:" + ex.Message)
        End Try
    End Sub

    Public Sub bindIngredientGrid()
        Try
            Dim dtsize As DataTable = DirectCast(ViewState("oldProduct_Data"), DataTable)

            If dtsize IsNot Nothing Then

                For Each item As RepeaterItem In rdProduct_list.Items
                    Dim value As String
                    Dim hdProductID As HiddenField = item.FindControl("hdProductID")
                    Dim chk_productID As CheckBox = item.FindControl("chk_productID")

                    value = hdProductID.Value.ToString()

                    For Each row1 As DataRow In dtsize.Rows

                        If value = row1("product_id") Then
                            chk_productID.Checked = True
                            Exit For
                        Else
                            chk_productID.Checked = False
                        End If
                    Next

                Next
                'up_Ingredient.Update()
            Else
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

                If dt.Rows.Count > 0 Then
                    'rdcopyProduct.DataSource = dt
                    'rdcopyProduct.DataBind()

                    rdProduct_list.DataSource = dt
                    rdProduct_list.DataBind()
                Else
                    rdProduct_list.DataSource = String.Empty
                    rdProduct_list.DataBind()
                End If
                'up_Ingredient.Update()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stock_Management_Master:bindIngredientGrid:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Try
            Session("stock_id") = Nothing
            Response.Redirect("Stock_Management_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            gd_Product.Visible = True
            Dim RecordInsert As Integer = 1
            Dim dtgrid As Integer = 1

            Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            If dt1.Rows.Count <= 0 Then
                dtgrid = 0
            End If

            LogHelper.Error("Stock_Management_Master:btnSave_Click : for_loop Count :" + gd_Product.Rows.Count.ToString() + ": Store_name" + Session("store").ToString())


            For Each item As GridViewRow In gd_Product.Rows

                LogHelper.Error("Stock_Management_Master:btnSave_Click : for_loop : Product" + CType(item.FindControl("ddlproduct"), DropDownList).SelectedValue.ToString() + ": Store_name" + Session("store").ToString())

                If Val(CType(item.FindControl("ddlproductgroup"), DropDownList).SelectedValue) = 0 Then
                    RecordInsert = 0
                    Exit For
                End If
                If Val(CType(item.FindControl("ddlproduct"), DropDownList).SelectedValue) = 0 Then
                    RecordInsert = 0
                    Exit For
                End If
                If Val(CType(item.FindControl("txttotalStock"), TextBox).Text) = 0 Then
                    RecordInsert = 0
                    Exit For
                End If
                If Val(CType(item.FindControl("ddltotalunit"), DropDownList).SelectedValue) = 0 Then
                    RecordInsert = 0
                    Exit For
                End If

            Next
            If dtgrid = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please Insert Stock Purchase Record in Grid');", True)
            ElseIf RecordInsert = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Fields with * can not be Blank');", True)
            ElseIf chkTemplete.Checked = True And txtTemplete_Name.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Add Templete Name.');", True)
            Else
                If Session("stock_id") = Nothing Then
                    Insert_Size_Add()
                Else
                    Insert_Size_Edit()
                End If

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Stock_Management_Master:btnSave_Click" + ex.Message + "Store_name : " + Session("store").ToString())
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try

            Session("stock_id") = Nothing
            Clear()

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            rdlocation.SelectedIndex = 0
            txtdate.SelectedDate = DateTime.Now
            txtsupplier.Text = ""
            txtsuppliercode.Text = ""
            rdTemplate.SelectedIndex = 0
            gd_Product.DataSource = String.Empty
            gd_Product.DataBind()

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:Clear" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdproduct_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdproduct.ItemDataBound
    '    Try

    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '            oclsBind.BindProductGroup(CType(e.Item.FindControl("ddlproductgroup"), RadComboBox), Val(Session("cmp_id")))
    '            oclsBind.BindUnit(CType(e.Item.FindControl("ddltotalunit"), RadComboBox), Val(Session("cmp_id")))
    '            oclsBind.BindProduct(CType(e.Item.FindControl("ddlproduct"), RadComboBox), Val(Session("cmp_id")))
    '            oclsBind.BindTax(CType(e.Item.FindControl("ddltax"), RadComboBox), Val(Session("cmp_id")))

    '            Dim lb As RadTextBox = CType(e.Item.FindControl("txtfinalprice"), RadTextBox)
    '            Dim lb1 As RadTextBox = CType(e.Item.FindControl("txttotalprice"), RadTextBox)
    '            Dim lb2 As RadTextBox = CType(e.Item.FindControl("txttaxamount"), RadTextBox)


    '            If Session("stock_id") = Nothing Then
    '                Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

    '                For Each row As DataRow In dt1.Rows
    '                    If CType(e.Item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

    '                        CType(e.Item.FindControl("ddltax"), RadComboBox).SelectedValue = row("tax_id").ToString()

    '                        CType(e.Item.FindControl("ddlproduct"), RadComboBox).SelectedValue = row("Product_id").ToString()
    '                        CType(e.Item.FindControl("ddlproductgroup"), RadComboBox).SelectedValue = row("Product_group").ToString()

    '                        CType(e.Item.FindControl("ddltotalunit"), RadComboBox).FindItemByText("Qty").Selected = True
    '                        CType(e.Item.FindControl("hdunit"), HiddenField).Value = CType(e.Item.FindControl("ddltotalunit"), RadComboBox).SelectedValue
    '                        row("unit_id") = Val(CType(e.Item.FindControl("ddltotalunit"), RadComboBox).SelectedValue)
    '                    End If

    '                Next
    '            Else
    '                Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

    '                For Each row As DataRow In dt1.Rows
    '                    If CType(e.Item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

    '                        CType(e.Item.FindControl("ddltax"), RadComboBox).SelectedValue = row("tax_id").ToString()

    '                        CType(e.Item.FindControl("ddlproduct"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdproduct"), HiddenField).Value
    '                        If CType(e.Item.FindControl("hdproductgroup"), HiddenField).Value > 0 Then
    '                            CType(e.Item.FindControl("ddlproductgroup"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdproductgroup"), HiddenField).Value
    '                            ViewState("productgroup") = CType(e.Item.FindControl("hdproductgroup"), HiddenField).Value
    '                        Else
    '                            CType(e.Item.FindControl("ddlproductgroup"), RadComboBox).SelectedValue = ViewState("productgroup")
    '                        End If
    '                        CType(e.Item.FindControl("ddltotalunit"), RadComboBox).FindItemByText("Qty").Selected = True
    '                        CType(e.Item.FindControl("hdunit"), HiddenField).Value = CType(e.Item.FindControl("ddltotalunit"), RadComboBox).SelectedValue

    '                    End If
    '                Next
    '            End If

    '            If CType(e.Item.FindControl("hdnactive"), HiddenField).Value = 0 Then
    '                CType(e.Item.FindControl("chkisdamage"), CheckBox).Checked = False
    '            Else
    '                CType(e.Item.FindControl("chkisdamage"), CheckBox).Checked = True
    '            End If

    '            If CType(e.Item.FindControl("hdtax"), HiddenField).Value = 0 Then
    '                CType(e.Item.FindControl("chktax"), CheckBox).Checked = False
    '            Else
    '                CType(e.Item.FindControl("chktax"), CheckBox).Checked = True
    '                Product_sum_tax("DataBound")
    '            End If

    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:rdProduct_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub BindGrid()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                gd_Product.DataSource = dv
                gd_Product.DataBind()
            Else
                gd_Product.Enabled = True
                gd_Product.Visible = True
                gd_Product.DataSource = String.Empty
                gd_Product.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGrid_Delete()
        Try
            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)  'DirectCast(ViewState("Product_Data"), DataTable)
            ViewState("oldProduct_Data") = dt
            Dim dv As DataView = dt.DefaultView
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                gd_Product.DataSource = dv
                gd_Product.DataBind()
            Else
                gd_Product.Enabled = True
                gd_Product.Visible = True
                gd_Product.DataSource = String.Empty
                gd_Product.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:BindGrid_Delete:" + ex.Message)
        End Try
    End Sub

    Protected Sub SaveRecordInDT()
        Try

            Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            For Each item As GridViewRow In gd_Product.Rows
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("Product_group") = Val(CType(item.FindControl("ddlproductgroup"), DropDownList).SelectedValue)
                        row("Product_id") = Val(CType(item.FindControl("ddlproduct"), DropDownList).SelectedValue)
                        row("Total_Stock") = Val(CType(item.FindControl("txttotalStock"), TextBox).Text)
                        row("unit_id") = Val(CType(item.FindControl("ddltotalunit"), DropDownList).SelectedValue)
                        row("Price") = Val(CType(item.FindControl("txtprice"), TextBox).Text)
                        row("is_damage") = Val(IIf(CType(item.FindControl("chkisdamage"), CheckBox).Checked = True, 1, 0))
                        row("Total_Price") = Val(CType(item.FindControl("txttotalprice"), TextBox).Text)
                        row("Tax") = Val(IIf(CType(item.FindControl("chktax"), CheckBox).Checked = True, 1, 0))
                        row("tax_amount") = Val(CType(item.FindControl("txttaxamount"), TextBox).Text)
                        row("final_price") = Val(CType(item.FindControl("txtfinalprice"), TextBox).Text)
                        row("tax_id") = (CType(item.FindControl("ddltax"), DropDownList).SelectedValue)
                        row("BuyingSizeCost") = Val(CType(item.FindControl("hdBuyingSizeCost"), HiddenField).Value.ToString())
                        row("cost_id") = Val(CType(item.FindControl("hdCostID"), HiddenField).Value.ToString())
                    End If
                Next
            Next
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:SaveRecordInDT:" + ex.Message)
        End Try
    End Sub

    'Protected Sub ddlproductgroup_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
    '    Try
    '        Dim ddlSection As RadComboBox = CType(sender, RadComboBox)
    '        Dim editItem As GridDataItem = CType(ddlSection.NamingContainer, GridDataItem)
    '        oclsBind.BindProductByProductGroup(CType(editItem.FindControl("ddlproduct"), RadComboBox), Val(Session("cmp_id")), ddlSection.SelectedValue)
    '        SaveRecordInDT()

    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:txtsize_TextChanged:" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub ddlproduct_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
    '    Try
    '        Dim ddlpro As RadComboBox = CType(sender, RadComboBox)
    '        Dim row As GridDataItem = CType(ddlpro.NamingContainer, GridDataItem)
    '        Dim ddlunit As RadComboBox = CType(row.FindControl("ddltotalunit"), RadComboBox)

    '        oclstock.cmp_id = Val(Session("cmp_id"))
    '        oclstock.product_id = ddlpro.SelectedValue

    '        Dim dtDevice As DataTable = oclstock.select_tax()

    '        If dtDevice.Rows.Count > 0 Then
    '            ddlunit.SelectedValue = Val(dtDevice.Rows(0)("Unit_id"))
    '            If ddlunit.SelectedValue > 0 Then
    '                oclsBind.BindUnitByProduct(ddlunit, Val(Session("cmp_id")), ddlpro.SelectedValue)
    '                'ddlunit = False
    '            Else
    '                oclsBind.BindUnit(CType(row.FindControl("ddltotalunit"), RadComboBox), Val(Session("cmp_id")))
    '                'ddlunit.Enabled = True
    '            End If
    '        End If

    '        SaveRecordInDT()
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:txtSizeName_TextChanged:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub txtreceipt_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txtreceipt_TextChanged:" + ex.Message)
        End Try
    End Sub

    'Protected Sub ddltotalunit_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
    '    Try
    '        SaveRecordInDT()
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:txtPriceLocation_TextChanged:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub txtprice_TextChanged(sender As Object, e As EventArgs)
        Try

            Product_sum_tax("Each_Cost")

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txtprice_TextChanged:" + ex.Message)
        End Try

    End Sub

    Protected Sub txttotalStock_TextChanged(sender As Object, e As EventArgs)
        Try

            Product_sum_tax("QTY")

            ' upWall.Update()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txttotalStock_TextChanged:" + ex.Message)
        End Try

    End Sub

    Protected Sub chkisdamage_CheckedChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:chkisdamage_CheckedChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub chktax_CheckedChanged(sender As Object, e As EventArgs)
        Try

            Product_sum_tax("CHKTAX")

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:chktax_CheckedChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txttaxamount_TextChanged(sender As Object, e As EventArgs)
        Try
            Product_sum_tax("Tax_AMT")
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txttaxamount_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txttotalprice_TextChanged(sender As Object, e As EventArgs)
        Try

            Product_sum_tax("Total")

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txttotalprice_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txtfinalprice_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txtfinalprice_TextChanged:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdproduct_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rdproduct.ItemCommand
    '    Try
    '        If e.CommandName = "DeleteVal" Then
    '            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
    '            Delete_Size_N_Price(id)
    '            BindGrid_Delete()
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:rdSizeNPrice_ItemCommand:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub Delete_Size_N_Price(ByVal id As Integer)
        Try
            Dim dtsize As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            For Each row As DataRow In dtsize.Rows

                If row("row_id") = id Then
                    dtsize.Rows.Remove(row)
                    row.EndEdit()
                    dtsize.AcceptChanges()
                    Exit For
                End If
            Next

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:Delete_Size_N_Price:" + ex.Message)
        End Try
    End Sub

    Protected Sub Insert_Size_Edit()
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim stock_rec_id As String
            Dim templete_id As Integer

            If Session("stock_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 0
                stock_rec_id = oclstock.InsUpdDel_SizeNPrice()
            Else
                oclstock.stk_rec_id = Val(Session("stock_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 0
                stock_rec_id = oclstock.Upd_SizeNPrice()
            End If

            '--------------Insert templete Master--------------
            If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then
                oclstock.stk_rec_id = stock_rec_id
                oclstock.templete_name = txtTemplete_Name.Text.ToString()
                templete_id = oclstock.Insert_templete_master()

            End If


            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            For Each row As DataRow In dt.Rows
                oclstock.stock_id = Val(row("stock_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = rdlocation.SelectedValue
                'oclstock.code = txtCode.Text.Trim()
                oclstock.product_id = Val(row("Product_id"))
                oclstock.product_group_id = Val(row("product_group"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.login_id = Val(Session("login_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.price = Val(row("price"))
                'oclstock.tax = Val(row("tax"))
                oclstock.supplier_id = txtsupplier.Text
                oclstock.total_stock = Val(row("total_stock"))
                oclstock.total_unit_id = Val(row("unit_id"))
                oclstock.is_damage = Val(row("Is_damage"))
                oclstock.damage_stock = 0
                oclstock.damage_unit_id = 0
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.total_price = Val(row("Total_Price"))
                oclstock.final_price = Val(row("final_price"))
                oclstock.inc_exe_tax = Val(row("Tax"))
                oclstock.tax_amount = Val(row("tax_amount"))
                oclstock.tax = Val(row("tax_id"))
                oclstock.stk_rec_id = stock_rec_id
                oclstock.BuyingSizeCost = Val(row("BuyingSizeCost"))
                oclstock.cost_id = Val(row("cost_id"))

                If Val(row("stock_id")) = 0 Then
                    oclstock.stock_id = 0
                    oclstock.Insert()
                Else
                    oclstock.stock_id = Val(row("stock_id"))
                    oclstock.Update()
                End If

                '--------------Insert as management templete --------------
                If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then

                    If templete_id > 0 Then
                        oclstock.Templete_id = templete_id
                        oclstock.Insert_templete()
                    End If

                ElseIf Session("Templete_id") IsNot Nothing Then

                    oclstock.Templete_id = Val(Session("Templete_id"))
                    oclstock.Update_templete()

                End If

            Next

            If Session("stock_id") = Nothing Then
                Session("Success") = "Record inserted successfully"
                Response.Redirect("Stock_Management_List.aspx", False)
            Else
                Session("Success") = "Record updated successfully"
                Response.Redirect("Stock_Management_List.aspx", False)
            End If

            Session("stock_id") = Nothing
            Session("Templete_id") = Nothing

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:Insert_Size_Edit:" + ex.Message + "Store_name : " + Session("store").ToString())
        End Try
    End Sub

    Protected Sub Insert_Size_Add()
        Try
            LogHelper.Error("Stock_Management_Master:Insert_Size_Add: Insert function start" + Session("store").ToString())
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim stock_rec_id As String
            Dim templete_id As Integer

            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            If Session("stock_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 0
                stock_rec_id = oclstock.InsUpdDel_SizeNPrice()

                '--------------Insert templete Master--------------
                If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then

                    oclstock.stk_rec_id = stock_rec_id
                    oclstock.templete_name = txtTemplete_Name.Text.ToString()
                    templete_id = oclstock.Insert_templete_master()

                End If


            Else
                oclstock.stk_rec_id = Val(Session("stock_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 0
                stock_rec_id = oclstock.Upd_SizeNPrice()
            End If

            LogHelper.Error("Stock_Management_Master:Insert_Size_Add: Insert For_loop count : " + dt.Rows.Count.ToString() + " Store_name :" + Session("store").ToString())

            Dim cnt As Integer = 1
            For Each row As DataRow In dt.Rows

                LogHelper.Error("Stock_Management_Master:Insert_Size_Add: Count : " + cnt.ToString() + " : Product_ID :" + row("Product_id").ToString())
                cnt = cnt + 1
            Next

            For Each row As DataRow In dt.Rows

                LogHelper.Error("Stock_Management_Master:Insert_Size_Add: Insert For_loop start : Store_name :" + Session("store").ToString() + " , Product_ID :" + row("Product_id").ToString())

                oclstock.stock_id = 0
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = rdlocation.SelectedValue
                'oclstock.code = txtCode.Text.Trim()
                oclstock.product_id = Val(row("Product_id"))
                oclstock.product_group_id = Val(row("product_group"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.login_id = Val(Session("login_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.price = Val(row("price"))
                'oclstock.tax = Val(row("tax"))
                oclstock.supplier_id = txtsupplier.Text
                oclstock.total_stock = Val(row("total_stock"))
                oclstock.total_unit_id = Val(row("unit_id"))
                oclstock.is_damage = Val(row("Is_damage"))
                oclstock.damage_stock = 0
                oclstock.damage_unit_id = 0
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.total_price = Val(row("Total_Price"))
                oclstock.final_price = Val(row("final_price"))
                oclstock.inc_exe_tax = Val(row("Tax"))
                oclstock.tax_amount = Val(row("tax_amount"))
                oclstock.tax = Val(row("tax_id"))
                oclstock.stk_rec_id = stock_rec_id
                oclstock.BuyingSizeCost = Val(row("BuyingSizeCost"))
                oclstock.cost_id = Val(row("cost_id"))

                If Session("stock_id") = Nothing Then
                    oclstock.stock_id = 0
                    oclstock.Insert()
                    LogHelper.Error("Stock_Management_Master:Insert_Size_Add: Insert Completed : Store_name :" + Session("store").ToString() + " , Product_ID :" + row("Product_id").ToString())

                Else
                    oclstock.stock_id = Val(Session("stock_id"))
                    oclstock.Update()

                    LogHelper.Error("Stock_Management_Master:Insert_Size_Add: Update Completed : Store_name :" + Session("store").ToString() + " , Product_ID :" + row("Product_id").ToString())

                End If

                '--------------Insert as management templete --------------
                If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then

                    If templete_id > 0 Then
                        oclstock.Templete_id = templete_id
                        oclstock.Insert_templete()
                    End If

                ElseIf Session("Templete_id") IsNot Nothing Then

                    oclstock.Templete_id = Val(Session("Templete_id"))
                    oclstock.Update_templete()

                End If

            Next

            LogHelper.Error("Stock_Management_Master:Insert_Size_Add: Insert For_loop End : Store_name :" + Session("store").ToString())


            If Session("stock_id") = Nothing Then

                Session("Success") = "Record inserted successfully"
                Response.Redirect("Stock_Management_List.aspx", False)

            Else

                Session("Success") = "Record updated successfully"
                Response.Redirect("Stock_Management_List.aspx", False)

            End If
            Session("stock_id") = Nothing
            Session("Templete_id") = Nothing

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:Insert_Size_Add:" + ex.Message + "Store_name : " + Session("store").ToString())
        End Try
    End Sub

    Protected Sub Insert_Size_Edit_Submit()
        Try
            LogHelper.Error("Stock_Management_Master:Insert_Size_Edit_Submit: Update function start : Store_name :" + Session("store").ToString())

            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim stock_rec_id As String
            Dim templete_id As Integer

            If Session("stock_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 1
                stock_rec_id = oclstock.InsUpdDel_SizeNPrice()
            Else
                oclstock.stk_rec_id = Val(Session("stock_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 1
                stock_rec_id = oclstock.Upd_SizeNPrice()
            End If

            '--------------Insert templete Master--------------
            If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then

                oclstock.stk_rec_id = stock_rec_id
                oclstock.templete_name = txtTemplete_Name.Text.ToString()
                templete_id = oclstock.Insert_templete_master()

            End If

            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            LogHelper.Error("Stock_Management_Master:Insert_Size_Edit_Submit: Update For_loop Count : " + dt.Rows.Count.ToString() + " Store_name :" + Session("store").ToString())


            For Each row As DataRow In dt.Rows

                LogHelper.Error("Stock_Management_Master:Insert_Size_Edit_Submit: Update For_loop start : Product_ID : " + row("Product_id").ToString() + " Store_name :" + Session("store").ToString())

                oclstock.stock_id = Val(row("stock_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = rdlocation.SelectedValue
                'oclstock.code = txtCode.Text.Trim()
                oclstock.product_id = Val(row("Product_id"))
                oclstock.product_group_id = Val(row("product_group"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.login_id = Val(Session("login_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.price = Val(row("price"))
                'oclstock.tax = Val(row("tax"))
                oclstock.supplier_id = txtsupplier.Text
                oclstock.total_stock = Val(row("total_stock"))
                oclstock.total_unit_id = Val(row("unit_id"))
                oclstock.is_damage = Val(row("Is_damage"))
                oclstock.damage_stock = 0
                oclstock.damage_unit_id = 0
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.total_price = Val(row("Total_Price"))
                oclstock.final_price = Val(row("final_price"))
                oclstock.inc_exe_tax = Val(row("Tax"))
                oclstock.tax_amount = Val(row("tax_amount"))
                oclstock.tax = Val(row("tax_id"))
                oclstock.stk_rec_id = stock_rec_id
                oclstock.BuyingSizeCost = Val(row("BuyingSizeCost"))
                oclstock.cost_id = Val(row("cost_id"))

                If Val(row("stock_id")) = 0 Then
                    oclstock.stock_id = 0
                    oclstock.Insert()


                    LogHelper.Error("Stock_Management_Master:Insert_Size_Edit_Submit: Insert Completed : Product_ID : " + row("Product_id").ToString() + " Store_name :" + Session("store").ToString())

                Else
                    oclstock.stock_id = Val(row("stock_id"))
                    oclstock.Update()


                    LogHelper.Error("Stock_Management_Master:Insert_Size_Edit_Submit: Update Completed : Product_ID : " + row("Product_id").ToString() + " Store_name :" + Session("store").ToString())


                End If

                '--------------Insert as management templete --------------
                If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then

                    If templete_id > 0 Then
                        oclstock.Templete_id = templete_id
                        oclstock.Insert_templete()
                    End If

                ElseIf Session("Templete_id") IsNot Nothing Then

                    oclstock.Templete_id = Val(Session("Templete_id"))
                    oclstock.Update_templete()

                End If

            Next

            LogHelper.Error("Stock_Management_Master:Insert_Size_Edit_Submit: Update For_loop End : Store_name :" + Session("store").ToString())


            If Session("stock_id") = Nothing Then
                Session("Success") = "Record inserted successfully"
                Response.Redirect("Stock_Management_List.aspx", False)
            Else
                Session("Success") = "Record updated successfully"
                Response.Redirect("Stock_Management_List.aspx", False)
            End If

            Session("stock_id") = Nothing
            Session("Templete_id") = Nothing

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:Insert_Size_Edit_Submit:" + ex.Message + "Store_name : " + Session("store").ToString())
        End Try
    End Sub

    Protected Sub Insert_Size_Add_Submit()
        Try

            LogHelper.Error("Stock_Management_Master:Insert_Size_Add_Submit: Insert function start" + Session("store").ToString())


            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim stock_rec_id As String
            Dim templete_id As Integer

            If Session("stock_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 1
                stock_rec_id = oclstock.InsUpdDel_SizeNPrice()

            Else
                oclstock.stk_rec_id = Val(Session("stock_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.total_stock = 0
                oclstock.price = 0
                oclstock.supplier_id = txtsupplier.Text
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.is_finalsubmit = 1
                stock_rec_id = oclstock.Upd_SizeNPrice()
            End If

            '--------------Insert templete Master--------------
            If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then

                oclstock.stk_rec_id = stock_rec_id
                oclstock.templete_name = txtTemplete_Name.Text.ToString()
                templete_id = oclstock.Insert_templete_master()

            End If

            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            LogHelper.Error("Stock_Management_Master:Insert_Size_Add_Submit: for_loop count : " + dt.Rows.Count.ToString() + " store : " + Session("store").ToString())


            For Each row As DataRow In dt.Rows
                LogHelper.Error("Stock_Management_Master:Insert_Size_Add_Submit: for_loop start : Product_ID : " + row("Product_id").ToString() + " store : " + Session("store").ToString())

                oclstock.stock_id = 0
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = rdlocation.SelectedValue
                'oclstock.code = txtCode.Text.Trim()
                oclstock.product_id = Val(row("Product_id"))
                oclstock.product_group_id = Val(row("product_group"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.login_id = Val(Session("login_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.price = Val(row("price"))
                'oclstock.tax = Val(row("tax"))
                oclstock.supplier_id = txtsupplier.Text
                oclstock.total_stock = Val(row("total_stock"))
                oclstock.total_unit_id = Val(row("unit_id"))
                oclstock.is_damage = Val(row("Is_damage"))
                oclstock.damage_stock = 0
                oclstock.damage_unit_id = 0
                oclstock.supplier_code = txtsuppliercode.Text
                oclstock.total_price = Val(row("Total_Price"))
                oclstock.final_price = Val(row("final_price"))
                oclstock.inc_exe_tax = Val(row("Tax"))
                oclstock.tax_amount = Val(row("tax_amount"))
                oclstock.tax = Val(row("tax_id"))
                oclstock.stk_rec_id = stock_rec_id
                oclstock.BuyingSizeCost = Val(row("BuyingSizeCost"))
                oclstock.cost_id = Val(row("cost_id"))

                If Val(row("stock_id")) = 0 Then
                    oclstock.stock_id = 0
                    oclstock.Insert()
                Else
                    oclstock.stock_id = Val(row("stock_id"))
                    oclstock.Update()

                End If

                '--------------Insert as management templete --------------
                If chkTemplete.Checked = True And txtTemplete_Name.Text <> "" Then

                    If templete_id > 0 Then
                        oclstock.Templete_id = templete_id
                        oclstock.Insert_templete()

                        LogHelper.Error("Stock_Management_Master:Insert_Size_Add_Submit: Record Inserted : Product_ID : " + row("Product_id").ToString() + " store : " + Session("store").ToString())

                    End If

                ElseIf Session("Templete_id") IsNot Nothing Then

                    oclstock.Templete_id = Val(Session("Templete_id"))
                    oclstock.Update_templete()

                    LogHelper.Error("Stock_Management_Master:Insert_Size_Add_Submit: Record Updated : Product_ID : " + row("Product_id").ToString() + " store : " + Session("store").ToString())


                End If
            Next
            LogHelper.Error("Stock_Management_Master:Insert_Size_Add_Submit: for_loop end : store : " + Session("store").ToString())


            If Session("stock_id") = Nothing Then
                Session("Success") = "Record inserted successfully"
                Response.Redirect("Stock_Management_List.aspx", False)
            Else
                Session("Success") = "Record updated successfully"
                Response.Redirect("Stock_Management_List.aspx", False)
            End If

            Session("stock_id") = Nothing
            Session("Templete_id") = Nothing

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:Insert_Size_Add_Submit:" + ex.Message + "Store_name : " + Session("store").ToString())
        End Try
    End Sub

    Public Sub BindProduct()
        Try
            oclstock.stock_id = Val(Session("stock_id"))
            oclstock.cmp_id = Val(Session("cmp_id"))
            Dim dtProduct As DataTable = oclstock.Select()
            If dtProduct.Rows.Count > 0 Then
                txtreceipt.Text = dtProduct.Rows(0)("receipt_number").ToString
                rdlocation.SelectedValue = dtProduct.Rows(0)("Location_id")
                txtdate.SelectedDate = dtProduct.Rows(0)("stock_date")
                txtsupplier.Text = dtProduct.Rows(0)("Supplier").ToString
                txtsuppliercode.Text = dtProduct.Rows(0)("Supplier_code").ToString

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stock_Management_Master:BindProduct:" + ex.Message)
        End Try

    End Sub

    Public Sub Product_sum_tax(ByVal text As String)

        For Each item As GridViewRow In gd_Product.Rows
            Dim txtprice As TextBox = CType(item.FindControl("txtprice"), TextBox)
            Dim txttotalStock As TextBox = CType(item.FindControl("txttotalStock"), TextBox)
            Dim txtfinalprice As TextBox = CType(item.FindControl("txtfinalprice"), TextBox)
            Dim txttotalprice As TextBox = CType(item.FindControl("txttotalprice"), TextBox)
            Dim txttaxamount As TextBox = CType(item.FindControl("txttaxamount"), TextBox)
            Dim txttax As DropDownList = CType(item.FindControl("ddltax"), DropDownList)
            Dim hdQTY As HiddenField = CType(item.FindControl("hdQTY"), HiddenField)
            Dim chktax As CheckBox = CType(item.FindControl("chktax"), CheckBox)
            Dim hdBuyingSizeCost As HiddenField = CType(item.FindControl("hdBuyingSizeCost"), HiddenField)
            Dim hdCostID As HiddenField = CType(item.FindControl("hdCostID"), HiddenField)

            If text = "QTY" Then

                txttotalprice.Text = Math.Round(Convert.ToDecimal(txtprice.Text) * Convert.ToDecimal(txttotalStock.Text), 2)
                txtfinalprice.Text = Math.Round(Convert.ToDecimal(txtprice.Text) * Convert.ToDecimal(txttotalStock.Text), 2)

            ElseIf text = "Each_Cost" Then

                txttotalprice.Text = Math.Round(Convert.ToDecimal(txtprice.Text) * Convert.ToDecimal(txttotalStock.Text), 2)
                txtfinalprice.Text = Math.Round(Convert.ToDecimal(txtprice.Text) * Convert.ToDecimal(txttotalStock.Text), 2)

                oclstock.cost_id = Val(hdCostID.Value.ToString())
                Dim dtCost As DataTable = oclstock.Select_by_cost()

                hdBuyingSizeCost.Value = (txtprice.Text.ToString() * Val(dtCost.Rows(0)("size"))).ToString()

            ElseIf text = "Total" Then

                txtprice.Text = Math.Round(Convert.ToDecimal(txttotalprice.Text) / Convert.ToDecimal(txttotalStock.Text), 2)
                txtfinalprice.Text = Convert.ToDecimal(txttotalprice.Text)

            End If

            '-----------------added if condition for calculate selected tax-----------
            If txttax.SelectedItem.Value <> "SELECT" Then

                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.tax_id = txttax.SelectedValue
                Dim dt_tax As DataTable = oclstock.select_tax_deatil()

                If dt_tax.Rows.Count > 0 Then
                    mode = dt_tax.Rows(0)("Mode")
                    tax_val = Val(dt_tax.Rows(0)("Value"))
                    t_id = dt_tax.Rows(0)("Tax_id")


                    If t_id > 0 Then
                        txttax.SelectedValue = Val(t_id)
                        If mode = "%" Then
                            I = Val(tax_val * txtprice.Text / 100)
                        Else
                            I = tax_val
                        End If
                        p = Val(I * txttotalprice.Text / txtprice.Text)
                        txttaxamount.Text = Val(p)
                        txtfinalprice.Text = Convert.ToDecimal(txttotalprice.Text) + Convert.ToDecimal(txttaxamount.Text)

                    Else
                        txttax.SelectedValue = 0

                    End If

                End If

                SaveRecordInDT()

            Else

                '---------------Tax not selected than claculate it from buying size tax----------------
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.product_id = CType(item.FindControl("ddlproduct"), DropDownList).SelectedValue
                oclsProduct.Location_id = rdlocation.SelectedValue
                Dim dt1 As DataSet = oclsProduct.Select_cost_size()

                If dt1.Tables(0).Rows.Count > 0 Then

                    If dt1.Tables(0).Rows(0)("tax_id") = t_id Or t_id = 0 Then
                        t = Val(dt1.Tables(0).Rows(0)("Value"))
                        M = dt1.Tables(0).Rows(0)("Mode")
                        tax_id = dt1.Tables(0).Rows(0)("tax_id")

                    Else

                        t = Val(tax_val)
                        M = mode
                        tax_id = t_id

                    End If

                    Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)
                    For Each row1 As DataRow In dt.Rows
                        If tax_id > 0 Then
                            row1("tax_id") = tax_id
                            txttax.SelectedValue = Val(tax_id)
                            If M = "%" Then
                                I = Val(t * txtprice.Text / 100)
                            Else
                                I = t
                            End If
                            p = Val(I * txttotalprice.Text / txtprice.Text)
                            txttaxamount.Text = Val(p)
                            txtfinalprice.Text = Convert.ToDecimal(txttotalprice.Text) + Convert.ToDecimal(txttaxamount.Text)
                            SaveRecordInDT()
                        Else
                            row1("tax_id1") = 0
                            txttax.SelectedValue = 0
                            SaveRecordInDT()
                        End If
                    Next

                Else
                    txttaxamount.Text = 0
                    txtfinalprice.Text = txttotalprice.Text
                    SaveRecordInDT()
                End If

            End If

        Next
    End Sub

    Public Sub btn_ProductIngredient_Click(sender As Object, e As EventArgs) Handles btn_ProductIngredient.Click
        Try
            LogHelper.Error("Stock_Management_Master:btn_ProductIngredient_Click: OK click START")
            Session("rowcount") = "1"
            Dim dt1 As New DataTable
            Dim Pro_Id As String = ""

            dt1.Columns.Add("product_id", GetType(Integer))

            Dim dtingredient As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            For Each item As RepeaterItem In rdProduct_list.Items

                Dim hdProductID As HiddenField = item.FindControl("hdProductID")
                Dim chk_productID As WebControls.CheckBox = item.FindControl("chk_productID")
                If chk_productID.Checked = True Then

                    Dim row As DataRow = dt1.NewRow()
                    row("product_id") = hdProductID.Value.ToString()

                    Pro_Id += hdProductID.Value.ToString() + ","
                    dt1.Rows.Add(row)

                End If

            Next

            dt1.AcceptChanges()

            LogHelper.Error("Stock_Management_Master:btn_ProductIngredient_Click: OK click dt1 count : " + dt1.Rows.Count.ToString())


            If dt1.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');  $('#Psummary thead tr').clone(true).appendTo('#Psummary thead'); Grid();  popup(); ", True)
                'popupClose();
            Else
                Dim rowTotal As Integer = 0

                oclsProduct.Pro_Id = Pro_Id
                oclsProduct.cmp_id = Val(Session("cmp_id"))

                If dtingredient IsNot Nothing Then
                    oclsProduct.row_id = dtingredient.Rows.Count
                Else
                    Try

                    Catch ex As Exception

                    End Try
                End If

                Dim dt As DataTable = oclsProduct.Get_Product_For_Stock_change()
                If dt.Rows.Count > 0 Then
                    txt_Name.Text = dt.Rows(0)("product").ToString()
                    link.HRef = "Product_Master.aspx?product_id=" + dt.Rows(0)("product_id").ToString()
                    hf_Product_Id.Value = dt.Rows(0)("product_id").ToString()
                    hf_Category_Id.Value = dt.Rows(0)("Category_id").ToString()
                    hf_Unit_Id.Value = dt.Rows(0)("Unit_id").ToString()
                    hf_Row_Id.Value = 0
                    txt_Qty.Text = Val(dt.Rows(0)("Total_Stock").ToString())
                    txt_Price.Text = Val(dt.Rows(0)("Price").ToString())
                    txt_TotalPrice.Text = Val(dt.Rows(0)("Total_Price").ToString())

                    LogHelper.Error("Stock_Management_Master:btn_ProductIngredient_Click: OK click Save in Viewstate : ")


                    ViewState("Stock_Data_Change_New") = dt

                    If dt.Rows.Count = 1 Then
                        btn_Save.Visible = True
                        btn_Next.Visible = False
                    End If

                    dv_prodctlist.Visible = False
                    dv_wizard.Visible = True
                    bindDdlCost()
                    txt_Qty.Text = ""
                    txt_Qty.Focus()

                    LogHelper.Error("Stock_Management_Master:btn_ProductIngredient_Click: OK click Before call popup : ")

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", " popup(); check(); ", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert(' Please fill in the missing fields data!');  $('#Psummary thead tr').clone(true).appendTo('#Psummary thead'); Grid();  popup(); ", True)
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Stock_Management_Master:btn_ProductIngredient_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btn_ProductIngrdientCancel_Click(sender As Object, e As EventArgs) Handles btn_ProductIngrdientCancel.Click
        Try

            ReBindGrid()

            LogHelper.Error("Stock_Management_Master: btn_ProductIngrdientCancel_Click : Cancel click : ")


        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btn_ProductIngrdientCancel_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub ReBindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.Location_id = Val(rdlocation.SelectedValue)
            Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

            If dt.Rows.Count > 0 Then
                'rdcopyProduct.DataSource = dt

                rdProduct_list.DataSource = dt
                rdProduct_list.DataBind()
            Else
                'rdcopyProduct.DataSource = String.Empty

                rdProduct_list.DataSource = String.Empty
                rdProduct_list.DataBind()
            End If


            LogHelper.Error("Stock_Management_Master: ReBindGrid : ReBindGrid function : ")


        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:ReBindGrid():" + ex.Message)
        End Try
    End Sub

    Public Sub BindSizeCost(ByVal proid As Integer)
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.product_id = proid
            oclsProduct.Location_id = rdlocation.SelectedValue
            Dim dt As DataSet = oclsProduct.Select_cost_size()
            Dim qty As Integer

            'If Val(dt.Tables(3).Rows(0)("unit_count").ToString()) > 0 Then

            '    Dim rowcount As Int32 = Val(hf_Row_Id.Value.ToString())

            '    Dim dtStock As DataTable = DirectCast(ViewState("Stock_Data_Change_New"), DataTable)
            '    If dtStock.Rows.Count > rowcount + 1 Then
            '        rowcount += 1
            '    End If
            '    If dtStock.Rows.Count = rowcount Then
            '        btn_Save.Visible = True
            '        btn_Next.Visible = False
            '    End If

            'ElseIf Val(dt.Tables(2).Rows(0)("unit_count").ToString()) > 0 And Val(dt.Tables(3).Rows(0)("unit_count").ToString()) = 0 Then

            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Product does not have buying size unit, Kindly add buying size unit.'); popup(); ", True)

            '    btn_Save.Visible = False
            '    btn_Next.Visible = False
            'End If

            lblActualQty.Text = txt_Qty.Text
            lblActualQty.Text = "0"
            hfActualunitid.Value = 0
            lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
            lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
            If txt_Qty.Text = "" Then
                qty = 0
            Else
                qty = Val(txt_Qty.Text)
            End If

            If ddlCOstSize.SelectedValue > 0 Then

                Session("cost_available") = "Yes"

                For index = 0 To dt.Tables(0).Rows.Count - 1
                    If ddlCOstSize.SelectedValue.ToString() = dt.Tables(0).Rows(index)("Cost_id").ToString() Then

                        If dt.Tables(0).Rows(index)("Unit").ToString() = "Qty" Then

                            txt_Price.Text = Val(dt.Tables(0).Rows(index)("Cost").ToString())
                            txt_TaxAmount.Text = (Val(txt_Price.Text) * qty) * Val(dt.Tables(0).Rows(index)("Value").ToString()) / 100
                            txt_TotalPrice.Text = Val(txt_Price.Text) * qty
                            lblActualQty.Text = qty * Val(dt.Tables(0).Rows(index)("size").ToString())
                            lblActuanlUnit.Text = "Qty" 'dt.Rows(index)("unit").ToString()
                            hfActualunitid.Value = dt.Tables(0).Rows(index)("unit_id").ToString()
                            lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                            lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                            '------------Added on 02/11/2022---------------
                            hdTax_ID.Value = dt.Tables(0).Rows(index)("tax_id").ToString()
                        Else
                            txt_Price.Text = Val(dt.Tables(0).Rows(index)("Cost").ToString())
                            txt_TaxAmount.Text = (Val(txt_Price.Text) * qty) * Val(dt.Tables(0).Rows(index)("Value").ToString()) / 100
                            txt_TotalPrice.Text = Val(txt_Price.Text) * qty
                            lblActualQty.Text = (qty * Val(dt.Tables(0).Rows(index)("size").ToString())) / Val(dt.Tables(0).Rows(index)("base").ToString())
                            lblActuanlUnit.Text = "Qty" 'dt.Rows(index)("unit").ToString()
                            hfActualunitid.Value = dt.Tables(0).Rows(index)("unit_id").ToString()
                            lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                            lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                            '------------Added on 02/11/2022---------------
                            hdTax_ID.Value = dt.Tables(0).Rows(index)("tax_id").ToString()
                        End If

                    End If

                Next
            Else

                Session("cost_available") = "No"

                If dt.Tables(1).Rows.Count > 0 Then
                    lblActualQty.Text = qty / Val(dt.Tables(1).Rows(0)("base").ToString())
                    lblActuanlUnit.Text = "Qty"
                    hfActualunitid.Value = 0
                    lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                    lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                Else
                    lblActualQty.Text = txt_Qty.Text
                    lblActualQty.Text = "0"
                    hfActualunitid.Value = 0
                    lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                    lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                End If

            End If


        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:BindSizeCost:" + ex.Message + "Store_name : " + Session("store").ToString())
        End Try
    End Sub

    Public Sub BindSizeCostPrice(ByVal proid As Integer)
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.product_id = proid
            oclsProduct.Location_id = rdlocation.SelectedValue
            Dim dt As DataSet = oclsProduct.Select_cost_size()
            Dim qty As Integer


            lblActualQty.Text = txt_Qty.Text
            lblActualQty.Text = "0"
            hfActualunitid.Value = 0
            lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
            lblTotalAmount.Text = Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)
            If txt_Qty.Text = "" Then
                qty = 0
            Else
                qty = Val(txt_Qty.Text)

            End If
            If ddlCOstSize.SelectedValue > 0 Then

                For index = 0 To dt.Tables(0).Rows.Count - 1
                    If ddlCOstSize.SelectedValue.ToString() = dt.Tables(0).Rows(index)("Cost_id").ToString() Then

                        If dt.Tables(0).Rows(index)("Unit").ToString() = "Qty" Then

                            '    txt_Price.Text = Val(dt.Tables(0).Rows(index)("Cost").ToString())
                            txt_TaxAmount.Text = (Val(txt_Price.Text) * qty) * Val(dt.Tables(0).Rows(index)("Value").ToString()) / 100
                            txt_TotalPrice.Text = Val(txt_Price.Text) * qty
                            lblActualQty.Text = qty * Val(dt.Tables(0).Rows(index)("size").ToString())
                            lblActuanlUnit.Text = "Qty" 'dt.Rows(index)("unit").ToString()
                            hfActualunitid.Value = dt.Tables(0).Rows(index)("unit_id").ToString()
                            lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                            lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                        Else
                            ' txt_Price.Text = Val(dt.Tables(0).Rows(index)("Cost").ToString())
                            txt_TaxAmount.Text = (Val(txt_Price.Text) * qty) * Val(dt.Tables(0).Rows(index)("Value").ToString()) / 100
                            txt_TotalPrice.Text = Val(txt_Price.Text) * qty
                            lblActualQty.Text = (qty * Val(dt.Tables(0).Rows(index)("size").ToString())) / Val(dt.Tables(0).Rows(index)("base").ToString())
                            lblActuanlUnit.Text = "Qty" 'dt.Rows(index)("unit").ToString()
                            hfActualunitid.Value = dt.Tables(0).Rows(index)("unit_id").ToString()
                            lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                            lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                        End If

                    End If

                Next
            Else

                If dt.Tables(1).Rows.Count > 0 Then
                    lblActualQty.Text = qty / Val(dt.Tables(1).Rows(0)("base").ToString())
                    lblActuanlUnit.Text = "Qty"
                    hfActualunitid.Value = 0
                    lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                    lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                Else
                    lblActualQty.Text = txt_Qty.Text
                    lblActualQty.Text = "0"
                    hfActualunitid.Value = 0
                    lblCostEach.Text = (Val(txt_TotalPrice.Text) / Val(lblActualQty.Text)).ToString("n2")
                    lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
                End If
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:BindSizeCostPrice:" + ex.Message)
        End Try
    End Sub

    Public Sub bindDdlCost()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.product_id = hf_Product_Id.Value
            oclsProduct.Location_id = rdlocation.SelectedValue
            Dim dt As DataSet = oclsProduct.Select_cost_size()

            If dt.Tables(0).Rows.Count > 0 Then
                ddlCOstSize.DataSource = dt.Tables(0)
                ddlCOstSize.DataValueField = "cost_id"
                ddlCOstSize.DataTextField = "size"
                ddlCOstSize.DataBind()

            Else
                ddlCOstSize.DataSource = dt.Tables(0)
                ddlCOstSize.DataBind()
                'Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
                'ddlCOstSize.Items.Insert(0, li)
                ddlCOstSize.Items.Insert(0, "--SELECT--")
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:bindDdlCost:" + ex.Message + "Store_name : " + Session("store").ToString())
        End Try
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Try

            LogHelper.Error("Stock_Management_Master: btn_Cancel_Click: cancel click Start ")


            dv_prodctlist.Visible = True
            dv_wizard.Visible = False

            lblActualQty.Text = "0"
            lblCostEach.Text = "0"
            txt_TaxAmount.Text = ""
            lblTotalAmount.Text = "0"

            ReBindGrid()

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btn_Cancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btn_Next_Click(sender As Object, e As EventArgs) Handles btn_Next.Click
        Try

            LogHelper.Error("Stock_Management_Master: btn_Next_Click: Next click Start ")


            Dim rowcount As Int32 = Val(hf_Row_Id.Value.ToString())

            Dim dtStock As DataTable = DirectCast(ViewState("Stock_Data_Change_New"), DataTable)

            dtStock.Rows(rowcount)("Total_Stock") = Val(lblActualQty.Text)
            dtStock.Rows(rowcount)("Price") = Val(lblCostEach.Text)
            dtStock.Rows(rowcount)("Total_Price") = Val(txt_TotalPrice.Text)
            dtStock.Rows(rowcount)("tax_amount") = Val(txt_TaxAmount.Text)
            dtStock.Rows(rowcount)("final_price") = Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)
            dtStock.Rows(rowcount)("tax_id") = Val(hdTax_ID.Value)
            dtStock.Rows(rowcount)("BuyingSizeCost") = Val(txt_Price.Text)
            dtStock.Rows(rowcount)("cost_id") = Val(ddlCOstSize.SelectedValue.ToString())

            If chkTax.Checked = True Then
                dtStock.Rows(rowcount)("Tax") = 1
            Else
                dtStock.Rows(rowcount)("Tax") = 0
            End If

            dtStock.AcceptChanges()


            LogHelper.Error("Stock_Management_Master: btn_Next_Click: Next click: Detail save in Viewstate ")


            txt_Name.Text = ""
            link.HRef = ""
            txt_Qty.Text = ""
            hf_Row_Id.Value = ""
            hf_Product_Id.Value = ""
            hf_Category_Id.Value = ""
            hf_Unit_Id.Value = ""
            txt_Price.Text = ""
            txt_TotalPrice.Text = ""
            txt_TaxAmount.Text = ""
            lblActualQty.Text = "0"
            lblActuanlUnit.Text = ""
            lblCostEach.Text = "0"
            lblTotalAmount.Text = "0"
            hdTax_ID.Value = ""

            Dim dt As DataTable = Nothing
            ddlCOstSize.DataSource = dt
            ddlCOstSize.DataBind()


            If dtStock.Rows.Count > rowcount + 1 Then
                rowcount += 1
                txt_Name.Text = dtStock.Rows(rowcount)("product").ToString()
                link.HRef = "Product_Master.aspx?product_id=" + dtStock.Rows(rowcount)("product_id").ToString()
                hf_Product_Id.Value = dtStock.Rows(rowcount)("product_id").ToString()
                hf_Category_Id.Value = dtStock.Rows(rowcount)("Category_id").ToString()
                hf_Unit_Id.Value = dtStock.Rows(rowcount)("Unit_id").ToString()
                hf_Row_Id.Value = rowcount
                txt_Qty.Text = Val(dtStock.Rows(rowcount)("Total_Stock").ToString())
                txt_Price.Text = Val(dtStock.Rows(rowcount)("Price").ToString())
                txt_TotalPrice.Text = Val(dtStock.Rows(rowcount)("Total_Price").ToString())
                txt_TaxAmount.Text = Val(dtStock.Rows(rowcount)("tax_amount").ToString())

            End If
            bindDdlCost()
            If dtStock.Rows.Count - 1 = rowcount Then
                btn_Save.Visible = True
                btn_Next.Visible = False
            End If
            ViewState("Stock_Data_Change_New") = dtStock
            txt_Qty.Text = ""
            txt_Qty.Focus()

            LogHelper.Error("Stock_Management_Master: btn_Next_Click: Next click: Before popup ")


            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "popup(); check();", True)

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btn_Next_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Try

            LogHelper.Error("Stock_Management_Master: btn_Save_Click: save click start")


            Dim rowcount As Int32 = Val(hf_Row_Id.Value.ToString())

            Dim dtStock_Exist As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            Dim dtStock As DataTable = DirectCast(ViewState("Stock_Data_Change_New"), DataTable)

            dtStock.Rows(rowcount)("Total_Stock") = Val(lblActualQty.Text)
            dtStock.Rows(rowcount)("Price") = Val(lblCostEach.Text)
            dtStock.Rows(rowcount)("Total_Price") = Val(txt_TotalPrice.Text)
            dtStock.Rows(rowcount)("tax_amount") = Val(txt_TaxAmount.Text)
            dtStock.Rows(rowcount)("final_price") = Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)
            dtStock.Rows(rowcount)("tax_id") = Val(hdTax_ID.Value)
            dtStock.Rows(rowcount)("BuyingSizeCost") = Val(txt_Price.Text)
            dtStock.Rows(rowcount)("cost_id") = Val(ddlCOstSize.SelectedValue.ToString())

            If chkTax.Checked = True Then
                dtStock.Rows(rowcount)("Tax") = 1
            Else
                dtStock.Rows(rowcount)("Tax") = 0
            End If

            dtStock.AcceptChanges()

            LogHelper.Error("Stock_Management_Master: btn_Save_Click: save click : Detail save in View state")

            txt_Name.Text = ""
            link.HRef = ""
            txt_Qty.Text = ""
            hf_Row_Id.Value = ""
            hf_Product_Id.Value = ""
            hf_Category_Id.Value = ""
            hf_Unit_Id.Value = ""
            txt_Price.Text = ""
            txt_TotalPrice.Text = 0
            txt_TaxAmount.Text = 0
            lblActualQty.Text = 0
            lblCostEach.Text = 0
            lblTotalAmount.Text = 0
            hdTax_ID.Value = ""

            Try
                dtStock_Exist.Merge(dtStock, True, MissingSchemaAction.Ignore)
            Catch ex As Exception
                dtStock_Exist = dtStock
            End Try

            dtStock_Exist.AcceptChanges()
            ViewState("Stock_Data_Change") = dtStock_Exist

            LogHelper.Error("Stock_Management_Master: btn_Save_Click: save click bind detail in grid")

            Dim dv As DataView = dtStock_Exist.DefaultView
            If dtStock_Exist.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                'rdproduct.DataSource = dv
                'rdproduct.DataBind()

                gd_Product.DataSource = dv
                gd_Product.DataBind()

                ' upWall.Update()
            Else
                gd_Product.Enabled = True
                gd_Product.Visible = True
                'rdproduct.DataSource = String.Empty
                'rdproduct.DataBind()

                gd_Product.DataSource = String.Empty
                gd_Product.DataBind()
                ' upWall.Update()
            End If

            dv_prodctlist.Visible = True
            dv_wizard.Visible = False
            btn_Save.Visible = False
            btn_Next.Visible = True
            Session("isfirsttime") = "1"
            bindIngredientGrid()


            LogHelper.Error("Stock_Management_Master: btn_Save_Click: save click before popup close")

            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", " popupClose();", True)

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btn_Save_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If Session("stock_id") = Nothing Then
                Insert_Size_Add_Submit()
            Else
                Insert_Size_Edit_Submit()
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btnSubmit_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub rdlocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rdlocation.SelectedIndexChanged
        Try
            If rdlocation.SelectedIndex = 0 Then

                rdProduct_list.DataSource = String.Empty
                rdProduct_list.DataBind()

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

                If dt.Rows.Count > 0 Then
                    'rdcopyProduct.DataSource = dt
                    'rdcopyProduct.DataBind()

                    rdProduct_list.DataSource = dt
                    rdProduct_list.DataBind()

                Else
                    'rdcopyProduct.DataSource = String.Empty
                    'rdcopyProduct.DataBind()

                    rdProduct_list.DataSource = String.Empty
                    rdProduct_list.DataBind()
                End If
            Else
                'rdcopyProduct.DataSource = String.Empty
                'rdcopyProduct.DataBind()

                'up_Ingredient.Update()

                rdProduct_list.DataSource = String.Empty
                rdProduct_list.DataBind()

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

                If dt.Rows.Count > 0 Then
                    'rdcopyProduct.DataSource = dt
                    'rdcopyProduct.DataBind()

                    rdProduct_list.DataSource = dt
                    rdProduct_list.DataBind()
                Else
                    'rdcopyProduct.DataSource = String.Empty
                    'rdcopyProduct.DataBind()

                    rdProduct_list.DataSource = String.Empty
                    rdProduct_list.DataBind()
                End If
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:rdlocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    'Protected Sub ddlCOstSize_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
    '    Try
    '        BindSizeCost(hf_Product_Id.Value)

    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", " popup();", True)

    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:ddlCOstSize_SelectedIndexChanged:" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub txt_Qty_TextChanged(sender As Object, e As EventArgs)
        Try
            If Not txt_Price.Text = "" Then
                BindSizeCost(hf_Product_Id.Value)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", " popup();", True)
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txt_Qty_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txt_Price_TextChanged(sender As Object, e As EventArgs) Handles txt_Price.TextChanged
        Dim Qty As Decimal = Val(txt_Qty.Text)
        Dim Price As Decimal = Val(txt_Price.Text)

        txt_TotalPrice.Text = Format(Val(Qty * Price), "0.00")

        If Not txt_Price.Text = "" Then
            BindSizeCostPrice(hf_Product_Id.Value)
        End If

        chkTax.Focus()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "popup(); check();", True)
    End Sub

    Private Sub txt_TaxAmount_TextChanged(sender As Object, e As EventArgs) Handles txt_TaxAmount.TextChanged
        Try
            lblTotalAmount.Text = (Val(txt_TotalPrice.Text) + Val(txt_TaxAmount.Text)).ToString("n2")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", " popup();", True)
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txt_TaxAmount_TextChanged:" + ex.Message)
        End Try
    End Sub
    'Protected Sub ddltax_SelectedIndexChanged1(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
    '    Try

    '        Dim ddltax As RadComboBox = CType(sender, RadComboBox)
    '        Dim row As GridDataItem = CType(ddltax.NamingContainer, GridDataItem)

    '        Dim txtprice As RadTextBox = CType(row.FindControl("txtprice"), RadTextBox)
    '        Dim txtfinalprice As RadTextBox = CType(row.FindControl("txtfinalprice"), RadTextBox)
    '        Dim txttotalprice As RadTextBox = CType(row.FindControl("txttotalprice"), RadTextBox)
    '        Dim txttaxamount As RadTextBox = CType(row.FindControl("txttaxamount"), RadTextBox)
    '        Dim txttax As RadComboBox = CType(row.FindControl("ddltax"), RadComboBox)

    '        oclstock.cmp_id = Val(Session("cmp_id"))
    '        oclstock.tax_id = ddltax.SelectedValue
    '        Dim dt_tax As DataTable = oclstock.select_tax_deatil()

    '        If dt_tax.Rows.Count > 0 Then
    '            mode = dt_tax.Rows(0)("Mode")
    '            tax_val = Val(dt_tax.Rows(0)("Value"))
    '            t_id = dt_tax.Rows(0)("Tax_id")


    '            If t_id > 0 Then
    '                txttax.SelectedValue = Val(t_id)
    '                If mode = "%" Then
    '                    I = Val(tax_val * txtprice.Text / 100)
    '                Else
    '                    I = tax_val
    '                End If
    '                p = Val(I * txttotalprice.Text / txtprice.Text)
    '                txttaxamount.Text = Val(p)
    '                txtfinalprice.Text = Convert.ToDecimal(txttotalprice.Text) + Convert.ToDecimal(txttaxamount.Text)

    '            Else
    '                txttax.SelectedValue = 0

    '            End If

    '        End If

    '        SaveRecordInDT()
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:chkIsActive_CheckedChanged:" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub chkTemplete_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkTemplete.Checked = True Then

                DivTempleteName.Visible = True
            Else

                DivTempleteName.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:chkTemplete_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub rdTemplate_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            If Val(rdTemplate.SelectedValue) <> 0 Then

                '---------------bind detail ---------------------
                oclstock.Templete_id = Val(rdTemplate.SelectedValue)
                oclstock.cmp_id = Val(Session("cmp_id"))
                Dim dtProduct As DataTable = oclstock.Select_template()
                If dtProduct.Rows.Count > 0 Then
                    'txtreceipt.Text = dtProduct.Rows(0)("receipt_number").ToString
                    rdlocation.SelectedValue = dtProduct.Rows(0)("Location_id")
                    'txtdate.SelectedDate = dtProduct.Rows(0)("stock_date")
                    txtsupplier.Text = dtProduct.Rows(0)("Supplier").ToString
                    txtsuppliercode.Text = dtProduct.Rows(0)("Supplier_code").ToString
                    txtdate.SelectedDate = System.DateTime.Now

                End If

                '------------------bind grid detail-------------------

                Dim dt As DataTable = oclstock.SelectStockTemplateDetail_Edit()

                ViewState("Stock_Data_Change") = dt
                Session("Templete_id") = Val(rdTemplate.SelectedValue)

                Dim dv As DataView = dt.DefaultView
                If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                    'rdproduct.DataSource = dv
                    'rdproduct.DataBind()

                    gd_Product.DataSource = dv
                    gd_Product.DataBind()
                Else
                    gd_Product.Enabled = True
                    gd_Product.Visible = True
                    'rdproduct.DataSource = String.Empty
                    'rdproduct.DataBind()

                    gd_Product.DataSource = String.Empty
                    gd_Product.DataBind()

                End If
            Else

                'txtreceipt.Text = ""
                rdlocation.SelectedValue = 0
                txtdate.SelectedDate = System.DateTime.Now
                txtsupplier.Text = ""
                txtsuppliercode.Text = ""

                'rdproduct.DataSource = String.Empty
                'rdproduct.DataBind()

                gd_Product.DataSource = String.Empty
                gd_Product.DataBind()

                ViewState("Stock_Data_Change") = Nothing

            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:rdTemplate_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtTemplete_Name_TextChanged(sender As Object, e As EventArgs)
        Try

            oclstock.templete_name = txtTemplete_Name.Text.ToString()
            oclstock.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclstock.check_template_name()
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Template name already exists.');", True)
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txtTemplete_Name_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs)
        Try

            bindDdlCost()

            BindSizeCost(hf_Product_Id.Value)


            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", " popup();", True)
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btnRefresh_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub gd_product_rowcommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Size_N_Price(id)
                BindGrid_Delete()
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:gd_product_rowcommand:" + ex.Message)
        End Try
    End Sub


    Protected Sub gd_product_rowdatabound(sender As Object, e As GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                oclsBind.BindProductGroup(CType(e.Row.FindControl("ddlproductgroup"), DropDownList), Val(Session("cmp_id")))
                oclsBind.BindUnit(CType(e.Row.FindControl("ddltotalunit"), DropDownList), Val(Session("cmp_id")))
                oclsBind.BindProduct(CType(e.Row.FindControl("ddlproduct"), DropDownList), Val(Session("cmp_id")))
                oclsBind.BindTax(CType(e.Row.FindControl("ddltax"), DropDownList), Val(Session("cmp_id")))

                Dim lb As TextBox = CType(e.Row.FindControl("txtfinalprice"), TextBox)
                Dim lb1 As TextBox = CType(e.Row.FindControl("txttotalprice"), TextBox)
                Dim lb2 As TextBox = CType(e.Row.FindControl("txttaxamount"), TextBox)


                If Session("stock_id") = Nothing Then
                    Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

                    For Each row As DataRow In dt1.Rows
                        If CType(e.Row.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

                            CType(e.Row.FindControl("ddltax"), DropDownList).SelectedValue = row("tax_id").ToString()

                            CType(e.Row.FindControl("ddlproduct"), DropDownList).SelectedValue = row("Product_id").ToString()
                            CType(e.Row.FindControl("ddlproductgroup"), DropDownList).SelectedValue = row("Product_group").ToString()

                            CType(e.Row.FindControl("ddltotalunit"), DropDownList).Items.FindByText("Qty").Selected = True '.FindItemByText("Qty").Selected = True
                            CType(e.Row.FindControl("hdunit"), HiddenField).Value = CType(e.Row.FindControl("ddltotalunit"), DropDownList).SelectedValue
                            row("unit_id") = Val(CType(e.Row.FindControl("ddltotalunit"), DropDownList).SelectedValue)
                        End If

                    Next
                Else
                    Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

                    For Each row As DataRow In dt1.Rows
                        If CType(e.Row.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

                            CType(e.Row.FindControl("ddltax"), DropDownList).SelectedValue = row("tax_id").ToString()

                            CType(e.Row.FindControl("ddlproduct"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdproduct"), HiddenField).Value
                            If CType(e.Row.FindControl("hdproductgroup"), HiddenField).Value > 0 Then
                                CType(e.Row.FindControl("ddlproductgroup"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdproductgroup"), HiddenField).Value
                                ViewState("productgroup") = CType(e.Row.FindControl("hdproductgroup"), HiddenField).Value
                            Else
                                CType(e.Row.FindControl("ddlproductgroup"), DropDownList).SelectedValue = ViewState("productgroup")
                            End If
                            CType(e.Row.FindControl("ddltotalunit"), DropDownList).Items.FindByText("Qty").Selected = True    '.FindItemByText("Qty").Selected = True
                            CType(e.Row.FindControl("hdunit"), HiddenField).Value = CType(e.Row.FindControl("ddltotalunit"), RadComboBox).SelectedValue

                        End If
                    Next
                End If

                If CType(e.Row.FindControl("hdnactive"), HiddenField).Value = 0 Then
                    CType(e.Row.FindControl("chkisdamage"), CheckBox).Checked = False
                Else
                    CType(e.Row.FindControl("chkisdamage"), CheckBox).Checked = True
                End If

                If CType(e.Row.FindControl("hdtax"), HiddenField).Value = 0 Then
                    CType(e.Row.FindControl("chktax"), CheckBox).Checked = False
                Else
                    CType(e.Row.FindControl("chktax"), CheckBox).Checked = True
                    Product_sum_tax("DataBound")
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:gd_product_rowdatabound:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlproduct_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
    Protected Sub ddlproductgroup_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddlSection As DropDownList = CType(sender, DropDownList)
            Dim editItem As GridDataItem = CType(ddlSection.NamingContainer, GridDataItem)
            oclsBind.BindProductByProductGroup(CType(editItem.FindControl("ddlproduct"), DropDownList), Val(Session("cmp_id")), ddlSection.SelectedValue)
            SaveRecordInDT()

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:ddlproductgroup_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddltotalunit_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
    Protected Sub ddltax_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim ddltax As DropDownList = CType(sender, DropDownList)
            Dim row As GridViewRow = CType(ddltax.NamingContainer, GridViewRow)

            Dim txtprice As TextBox = CType(row.FindControl("txtprice"), TextBox)
            Dim txtfinalprice As TextBox = CType(row.FindControl("txtfinalprice"), TextBox)
            Dim txttotalprice As TextBox = CType(row.FindControl("txttotalprice"), TextBox)
            Dim txttaxamount As TextBox = CType(row.FindControl("txttaxamount"), TextBox)
            Dim txttax As DropDownList = CType(row.FindControl("ddltax"), DropDownList)

            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.tax_id = ddltax.SelectedValue
            Dim dt_tax As DataTable = oclstock.select_tax_deatil()

            If dt_tax.Rows.Count > 0 Then
                mode = dt_tax.Rows(0)("Mode")
                tax_val = Val(dt_tax.Rows(0)("Value"))
                t_id = dt_tax.Rows(0)("Tax_id")


                If t_id > 0 Then
                    txttax.SelectedValue = Val(t_id)
                    If mode = "%" Then
                        I = Val(tax_val * txtprice.Text / 100)
                    Else
                        I = tax_val
                    End If
                    p = Val(I * txttotalprice.Text / txtprice.Text)
                    txttaxamount.Text = Val(p)
                    txtfinalprice.Text = Convert.ToDecimal(txttotalprice.Text) + Convert.ToDecimal(txttaxamount.Text)

                Else
                    txttax.SelectedValue = 0

                End If

            End If

            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:ddltax_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlCOstSize_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            BindSizeCost(hf_Product_Id.Value)

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", " popup();", True)

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:ddlCOstSize_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
End Class




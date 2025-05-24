Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web
Imports Telerik.Web.UI
Imports System.Data.SqlClient
'Imports System.Windows.Forms
Imports System.Web.Services
Imports System.Web.UI.WebControls
'Imports System.Windows.Forms
Imports AjaxControlToolkit
Public Class GlobalVariables
    'Public Shared UserName As String = "Tim Johnson"
    'Public Shared UserAge As Integer = 39
    'Public Shared grd As GridView = New GridView()
    'Public Shared tbcDynamic As AjaxControlToolkit.TabContainer
End Class

Partial Class Product_Master
    'Inherits ITemplate
    Inherits BaseClass
    Dim oclsProduct As New Controller_clsProduct()
    Dim oclsTax As New Controller_clsTax()
    Dim oclsSize As New Controller_clsSize()
    Dim oclsPrice As New Controller_clsPrice()
    Dim oclsUnit As New Controller_clsUnit()
    Dim oclsBarcodeSize As New Controller_clsBarcodeSize()
    Dim oclsProductIngredient As New Controller_clsProductIngredient()
    Dim oclsCondiment As New Controller_clsCondiment()
    Dim oclsProductCondiment As New Controller_clsProductCodiment()
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Dim oclsBind As New clsBinding
    Dim oclsAllergy As New Controller_clsAllergy()
    Public oSessionManager As New SessionManager
    Dim objclProdImg As New Controller_clsProductImg()
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Show_Product_ForIngredient"
    Dim dtPrc As DataTable

    Dim oclLoc As New Controller_clsLocation()

    Dim tbcDynamic As AjaxControlToolkit.TabContainer

    'Dim ajxTab As AjaxControlToolkit.TabContainer

    'Public tbpnlProcessCategory As TabPanel = New TabPanel()

    'Dim grd As GridView = New GridView()
    'Public Shared tbcDynamic As AjaxControlToolkit.TabContainer = New AjaxControlToolkit.TabContainer
    'Public Shared tbpnlProcessCategory As TabPanel = New TabPanel()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try




            If Session("show_chips") = "1" Then
                divDV.Visible = True
            Else
                divDV.Visible = False
            End If
            Session("product_id") = Request.QueryString("product_id")
            bindAutocompateIngredient()
            bindAutocompateCondiment()
            bind_PricesBy_Cmp()
            LogHelper.Error("Product_Master:Page_Load:1")
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                LogHelper.Error("Product_Master:Page_Load:2")
                oclsBind.BindProductGroup(radCategory, Val(Session("cmp_id")))
                oclsBind.BindDepartment(radDept, Val(Session("cmp_id")))
                oclsBind.BindCourse(radCourse, Val(Session("cmp_id")))
                oclsBind.BindPrinter(radPrinter, Val(Session("cmp_id")))

                oclsBind.BindKeyMap(ddKeyMap, Val(Session("cmp_id")))
                oclsBind.BindUnit(radUnit, Val(Session("cmp_id")))
                oclsBind.BindLocationProduct(ddLocation, Val(Session("cmp_id")))

                oclsBind.BindLocationProduct(ddlocation2, Val(Session("cmp_id")))
                oclsBind.BindPrices_By_Cmp(ddPrices, Val(Session("cmp_id")))

                '---------------added on 21/02/2022--------------
                oclsBind.BindPrices_By_Cmp(ddlFromPricelvl, Val(Session("cmp_id")))
                oclsBind.BindPrices_By_Cmp(ddlToPricelvl, Val(Session("cmp_id")))
                '---------------21/02/2022--------------

                bindIngredientGrid()
                bindCondimentGrid()
                ddLocation.SelectedIndex = 0
                'RadTabStrip1.SelectedIndex = 0

                oclsProduct.product_id = 0
                oclsProduct.cmp_id = Val(Session("cmp_id"))

                If Session("product_id") = Nothing And Session("copy_product_id") = Nothing Then
                    oclsProduct.SingleRecord = 1

                    If GridView1.Columns.Count > 0 Then
                        GridView1.Columns(6).Visible = False
                    End If

                Else
                    oclsProduct.SingleRecord = 0
                End If

                ddPrices.SelectedIndex = 1

                oclsProduct.Location_id = ddLocation.SelectedValue
                oclsProduct.Product_Price_Id = ddPrices.SelectedIndex

                getPrcNSizes()

                'bind_Tabs()

                Dim dtCost As DataTable = oclsProduct.Select_SizeNCost_By_Product()
                ViewState("View_Cost") = dtCost
                BindGridforPageload()

                txtBase.Text = "1.00"
                radUnit.ClearSelection()
                Try
                    ' radUnit.FindItemByText("Qty").Selected = True
                    radUnit.Items.FindByText("Qty").Selected = True
                Catch ex As Exception

                End Try

                For Each item As GridViewRow In GridView1.Rows
                    oclsBind.BindUnit(CType(item.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                    CType(item.FindControl("ddUnit"), DropDownList).SelectedValue = Val(radUnit.SelectedValue.ToString())
                    CType(item.FindControl("ddUnit"), DropDownList).Enabled = True
                Next
                For Each item As GridViewRow In GridView2.Rows
                    oclsBind.BindUnit(CType(item.FindControl("ddUnit2"), DropDownList), Val(Session("cmp_id")))
                    CType(item.FindControl("ddUnit2"), DropDownList).SelectedValue = Val(radUnit.SelectedValue.ToString())
                    CType(item.FindControl("ddUnit2"), DropDownList).Enabled = True
                Next
                LogHelper.Error("Product_Master:Page_Load:9")
                Dim dt2 As New DataTable()
                dt2.Columns.AddRange(New DataColumn(5) {New DataColumn("Barcode_Size_Id"), New DataColumn("Barcode_Size"), New DataColumn("Size_Unit"), New DataColumn("Size_Id"), New DataColumn("is_saved"), New DataColumn("action")})
                ViewState("View_Barcode_Size") = dt2
                LogHelper.Error("Product_Master:Page_Load:10")
                If Not Session("product_id") = Nothing Then

                    getPrcNSizes()

                    Session("copy_product_id") = Nothing
                    selectIngredientList()
                    selectCondimentList()
                    LogHelper.Error("Product_Master:Page_Load:11")
                    BindProduct()
                    BindSize()
                    BindSizeCost()
                    'BindPrice()
                    BindBarcode_Size()
                    bind_drp_Size()
                    BindNewGrid()
                    BindGrid()
                    BindCost()
                    BindNewGridCost()
                    bindImage()
                ElseIf Not Session("copy_product_id") = Nothing Then
                    Session("product_id") = Nothing
                    selectIngredientList_for_copy()
                    selectCondimentList_for_copy()
                    BindProduct_for_copy()
                    BindSize()
                    BindSizeCost()
                    BindNewGridCost()
                    'BindPrice()
                    BindBarcode_Size_for_copy()
                    bind_drp_Size()
                    BindNewGrid()
                    BindGrid_copy()
                Else
                    BindDefaultSize()
                    BindGridBarcode_Size()
                    'Return
                End If

            End If



            If Session("product_type") Is DBNull.Value OrElse String.IsNullOrEmpty(Session("product_type")) Then
                Session("product_type") = 0
            End If
            If Session("product_type") = "2" Then
                radPrinter.Visible = False
                'Label3.visible = False
                RadPanelItem1.Visible = False  ''''Condiments
                allergylbl.Visible = False
                ingredientslbl.Visible = False
                buylbl.Visible = False
                radPrinter.Visible = False
                printer.Visible = False
                Course.Visible = False
                radCourse.Visible = False
                location.Visible = False
                RequiredFieldValidator6.Visible = False
                ddLocation.Visible = False
                lblcost.Visible = False
                lblLocCOst.Visible = False
                lblque.Visible = False
                lblLocQty.Visible = False
                ddPrices.Visible = False
                lblprice.Visible = False
                ' lblupload.Visible = True
                ' RadAsyncUpload1.Visible = True
            Else
                radPrinter.Visible = True
                RadPanelItem1.Visible = True
                allergylbl.Visible = True
                ingredientslbl.Visible = True
                buylbl.Visible = True
                radPrinter.Visible = True
                printer.Visible = True
                Course.Visible = True
                radCourse.Visible = True
                location.Visible = True
                ddlocation2.Visible = True
                RequiredFieldValidator6.Visible = True
                ddLocation.Visible = True
                ' loca.visible = False
                lblcost.Visible = True
                lblLocCOst.Visible = True
                lblque.Visible = True
                lblLocQty.Visible = True
                ddPrices.Visible = True
                lblprice.Visible = True
                'lblupload.Visible = False
                'RadAsyncUpload1.Visible = false

            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:Page_Load:" + ex.Message)
        End Try
    End Sub



    Protected Sub UpdateSize(ByVal id As Integer)
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each row As DataRow In dt1.Rows
                If row("Size_Id") = id Then
                    row("is_saved") = IIf(row("is_saved") = 1, 1, 0)
                    If row("is_saved") = 1 And row("action") = 0 Then
                        row("action") = 1
                    ElseIf row("is_saved") = 0 Then
                        row("action") = 1
                    End If
                    row("Active") = IIf(row("Active").ToString() = "Active", "Inactive", "Active")
                    row.EndEdit()
                    dt1.AcceptChanges()
                End If
            Next
            bind_drp_Size()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Function_Type_List:deleteFunctionType" + ex.Message)
        End Try
    End Sub


    Protected Sub BindCost()
        Try
            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.Location_id = Val(ddLocation.SelectedValue)

            Dim dtPRice As DataTable = oclsProduct.select_product_price_current()

            If dtPRice.Rows.Count > 0 Then
                lblLocCOst.Text = dtPRice.Rows(0)("price").ToString()
                lblLocQty.Text = dtPRice.Rows(0)("stock_closing").ToString()
            Else
                lblLocCOst.Text = "0.00"
                lblLocQty.Text = "0"
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindCost:" + ex.Message)
        End Try
    End Sub
    Protected Sub BindGrid()
        Try

            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0
            Dim dtIngredient As DataTable = oclsProduct.Select_Ingredient_By_Product()
            Dim dvIngredient As DataView = dtIngredient.DefaultView

            ViewState("View_Ingredient") = dtIngredient
            ViewState("Ingredient_Data") = dtIngredient

            If dtIngredient.Rows.Count > 0 And dvIngredient.ToTable.Rows.Count > 0 Then
                rdIngredient.DataSource = dvIngredient
                rdIngredient.DataBind()
            End If

            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0
            Dim dtCondiment As DataTable = oclsProduct.Select_Condiment_By_Product()
            Dim dvCondiment As DataView = dtCondiment.DefaultView

            ViewState("Condiment_Data") = dtCondiment

            If dtCondiment.Rows.Count > 0 And dvCondiment.ToTable.Rows.Count > 0 Then
                rdCondiment.DataSource = dvCondiment
                rdCondiment.DataBind()
            End If

            oclsProduct.product_id = Val(Session("product_id"))

            Dim dtAllergy As DataTable = oclsProduct.Select_Allergy_By_Product()
            Dim dvAllergy As DataView = dtAllergy.DefaultView
            ViewState("Allergy_Data") = dtAllergy
            If dtAllergy.Rows.Count > 0 And dvAllergy.ToTable.Rows.Count > 0 Then
                rdSAllergy.DataSource = dtAllergy
                rdSAllergy.DataBind()
            End If
            'BindGridCost()
            'BindNewGridCost()
            'bind_Tabs("")
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGrid_copy()
        Try

            oclsProduct.product_id = Val(Session("copy_product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0
            Dim dtIngredient As DataTable = oclsProduct.Select_Ingredient_By_Product()
            Dim dvIngredient As DataView = dtIngredient.DefaultView

            ViewState("View_Ingredient") = dtIngredient
            ViewState("Ingredient_Data") = dtIngredient

            If dtIngredient.Rows.Count > 0 And dvIngredient.ToTable.Rows.Count > 0 Then
                rdIngredient.DataSource = dvIngredient
                rdIngredient.DataBind()
            End If

            oclsProduct.product_id = Val(Session("copy_product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0
            Dim dtCondiment As DataTable = oclsProduct.Select_Condiment_By_Product()
            Dim dvCondiment As DataView = dtCondiment.DefaultView

            ViewState("Condiment_Data") = dtCondiment

            If dtCondiment.Rows.Count > 0 And dvCondiment.ToTable.Rows.Count > 0 Then
                rdCondiment.DataSource = dvCondiment
                rdCondiment.DataBind()
            End If

            oclsProduct.product_id = Val(Session("copy_product_id"))

            Dim dtAllergy As DataTable = oclsProduct.Select_Allergy_By_Product()
            Dim dvAllergy As DataView = dtAllergy.DefaultView
            ViewState("Allergy_Data") = dtAllergy
            If dtAllergy.Rows.Count > 0 And dvAllergy.ToTable.Rows.Count > 0 Then
                rdSAllergy.DataSource = dtAllergy
                rdSAllergy.DataBind()
            End If
            'BindGridCost()
            'BindNewGridCost()
            'bind_Tabs("")
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid_copy:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGridCost()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Cost"), DataTable)
            Dim dv As DataView = dt.DefaultView

            If Not Session("copy_product_id") = Nothing Then

            Else
                If ddlocation2.SelectedValue <> 0 Then
                    dv.RowFilter = "action <> '2' AND Location_id = " + ddlocation2.SelectedValue.ToString()
                Else
                    dv.RowFilter = "action <> '2'"

                End If
            End If

            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                'rdSizeNCost.DataSource = dv
                'rdSizeNCost.DataBind()

                GridView2.DataSource = dv
                GridView2.DataBind()

                If dv.Count >= 4 Then
                    btnNewSizeCost.Enabled = False
                Else
                    btnNewSizeCost.Enabled = True
                End If
            Else
                btnNewSizeCost.Enabled = True
                'rdSizeNCost.DataSource = String.Empty
                'rdSizeNCost.DataBind()
                GridView2.DataSource = String.Empty
                GridView2.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGridCost:" + ex.Message)
        End Try
    End Sub
    Protected Sub BindGrid_Delete()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView

            If ddLocation.SelectedValue <> 0 Then
                dv.RowFilter = "action <> '2' AND Location_id = " + ddLocation.SelectedValue.ToString()
            Else
                dv.RowFilter = "action <> '2'"
            End If

            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                GridView1.DataSource = dv
                GridView1.DataBind()

                'rdSizeNPrice.DataSource = dv
                'rdSizeNPrice.DataBind()

                If dv.Count >= 4 Then
                    btnAddNewSize.Enabled = False
                Else
                    btnAddNewSize.Enabled = True
                End If
            Else
                GridView1.DataSource = String.Empty
                GridView1.DataBind()

                btnAddNewSize.Enabled = True
            End If

            Dim dtIngredient As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)
            Dim dvIngredient As DataView = dtIngredient.DefaultView

            ViewState("View_Ingredient") = dtIngredient

            If dtIngredient.Rows.Count > 0 And dvIngredient.ToTable.Rows.Count > 0 Then
                rdIngredient.DataSource = dvIngredient
                rdIngredient.DataBind()
            Else
                rdIngredient.DataSource = String.Empty
                rdIngredient.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid:" + ex.Message)
        End Try
    End Sub
    Protected Sub BindGrid_DeleteAllergy()
        Try
            Dim dtALlergy As DataTable = DirectCast(ViewState("Allergy_Data"), DataTable)
            Dim dvALlergy As DataView = dtALlergy.DefaultView

            If dtALlergy.Rows.Count > 0 And dvALlergy.ToTable.Rows.Count > 0 Then
                rdSAllergy.DataSource = dvALlergy
                rdSAllergy.DataBind()
            Else
                rdSAllergy.DataSource = String.Empty
                rdSAllergy.DataBind()
            End If
            upallergyLIst.Update()
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGrid_DeleteCondiment()
        Try
            Dim dtCondiment As DataTable = DirectCast(ViewState("Condiment_Data"), DataTable)
            Dim dvCondiment As DataView = dtCondiment.DefaultView

            If dtCondiment.Rows.Count > 0 And dvCondiment.ToTable.Rows.Count > 0 Then
                rdCondiment.DataSource = dvCondiment
                rdCondiment.DataBind()
            Else
                rdCondiment.DataSource = String.Empty
                rdCondiment.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid:" + ex.Message)
        End Try
    End Sub
    Public Sub bindIngredientGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredient()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
                rdcopyProduct.DataBind()
            Else
                rdcopyProduct.DataSource = String.Empty
                rdcopyProduct.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Change_Product_Group_List:bindGrid:" + ex.Message)
        End Try

    End Sub
    Public Sub bindCondimentGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.Get_M_Condiment_ForProduct()

            If dt.Rows.Count > 0 Then
                RadCondiment.DataSource = dt
                RadCondiment.DataBind()
            Else
                RadCondiment.DataSource = String.Empty
                RadCondiment.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Change_Product_Group_List:bindGrid:" + ex.Message)
        End Try

    End Sub
    Protected Sub BindGridforPageload()
        Try

            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView

            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then

                If Val(Session("IsAddTax2").ToString()) = 1 Then

                    If GridView1.Columns.Count > 0 Then
                        GridView1.Columns(6).Visible = True
                    End If

                Else
                    If GridView1.Columns.Count > 0 Then
                        GridView1.Columns(6).Visible = False
                    End If
                End If

                GridView1.DataSource = dv
                GridView1.DataBind()

                If dv.Count >= 4 Then
                    btnAddNewSize.Enabled = False
                Else
                    btnAddNewSize.Enabled = True
                End If
            Else

                GridView1.DataSource = String.Empty
                GridView1.DataBind()

                btnAddNewSize.Enabled = True
            End If


            Dim dtc As DataTable = DirectCast(ViewState("View_Cost"), DataTable)
            Dim dvc As DataView = dtc.DefaultView

            If dtc.Rows.Count > 0 And dvc.ToTable.Rows.Count > 0 Then

                GridView2.DataSource = dvc
                GridView2.DataBind()

                If dvc.Count >= 4 Then
                    btnAddNewSize.Enabled = False
                Else
                    btnAddNewSize.Enabled = True
                End If
            Else

                GridView2.DataSource = String.Empty
                GridView2.DataBind()

                btnAddNewSize.Enabled = True
            End If

            Dim dvIngredient As DataView
            Dim dtProduct As DataTable = DirectCast(ViewState("View_Ingredient"), DataTable)
            Try
                dvIngredient = dtProduct.DefaultView

                If dtProduct.Rows.Count > 0 And dvIngredient.ToTable.Rows.Count > 0 Then
                    rdIngredient.DataSource = dvIngredient
                    rdIngredient.DataBind()
                End If
            Catch ex As Exception

            End Try
            Dim dtCondiment As DataTable = DirectCast(ViewState("View_Condiment"), DataTable)
            Dim dvCondiment As DataView = dtCondiment.DefaultView

            If dtCondiment.Rows.Count > 0 And dvCondiment.ToTable.Rows.Count > 0 Then
                rdCondiment.DataSource = dvCondiment
                rdCondiment.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Size_N_Price(id)
                BindGrid()
                BindNewGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdSizeNPrice_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Cost(id)
                'BindGrid()
                BindNewGridCost()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Delete_Size_N_Price(ByVal id As Integer)
        Try
            Dim dtsize As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            'Dim dtPrice As DataTable = DirectCast(ViewState("View_Price"), DataTable)
            Dim dtbarcode As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)


            Dim foundRow1() As DataRow
            foundRow1 = dtbarcode.Select("action <> 2")
            For Each dr2 As DataRow In foundRow1
                If Val(dr2("Size_Id")) = id Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Size can not be deleted,it is already assigned');", True)
                    Exit Sub
                End If
            Next
            For Each row As DataRow In dtsize.Rows
                If row("row_Id") = id Then

                    oclsSize.Size_id = row("Size_Id")
                    oclsSize.cmp_id = Val(Session("cmp_id"))
                    oclsSize.Product_Price_Id = Val(row("Product_Price_Id"))
                    oclsSize.Price_id = Val(row("Price_Id"))
                    oclsSize.Del_SizeNPrice()

                    row("action") = 2
                    row.Delete()
                    dtsize.AcceptChanges()

                    Exit For
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
            'hfSize_Id.Value = ""
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Delete_Ingredient(ByVal id As Integer)
        Try

            Dim dtIngredient As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)

            For Each row As DataRow In dtIngredient.Rows
                If row("row_Id") = id Then
                    row.Delete()
                    dtIngredient.AcceptChanges()
                    Exit For
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
        Catch ex As Exception
            LogHelper.Error("Product_Master:delete_ingredient:" + ex.Message)
        End Try
    End Sub
    Protected Sub Delete_ALlergy(ByVal id As Integer)
        Try
            Dim dtAllergy As DataTable = DirectCast(ViewState("Allergy_Data"), DataTable)

            For Each row As DataRow In dtAllergy.Rows
                If row("Allergy_id") = id Then
                    row.Delete()
                    dtAllergy.AcceptChanges()
                    Exit For
                End If
            Next
            ViewState("Allergy_Data") = dtAllergy
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
        Catch ex As Exception
            LogHelper.Error("Product_Master:delete_allergy:" + ex.Message)
        End Try
    End Sub
    Protected Sub Delete_Condiment(ByVal id As Integer)
        Try
            Dim dtCondiment As DataTable = DirectCast(ViewState("Condiment_Data"), DataTable)

            For Each row As DataRow In dtCondiment.Rows
                If row("row_Id") = id Then
                    row.Delete()
                    dtCondiment.AcceptChanges()
                    Exit For
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
        Catch ex As Exception
            LogHelper.Error("Product_Master:delete_condiment:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click
        Try
            If hdBarcode_size.Value = "" Then

                Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)
                Dim v As String

                oclsBarcodeSize.cmp_id = Val(Session("cmp_id"))
                oclsBarcodeSize.product_id = Val(Session("product_id"))
                oclsBarcodeSize.Barcode = txtBarcodeM.Text.Trim()
                Dim dtbarcode As DataTable = oclsBarcodeSize.Select_Barcode_exists()

                If dtbarcode.Rows.Count > 0 Then
                    v = dtbarcode.Rows(0)("val").ToString()
                End If
                If v = 1 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record already exists');", True)
                    Exit Sub
                End If

                Dim foundRow() As DataRow
                foundRow = dt.Select("action <>2")

                '-----------comment on 28/07/2021---------------
                For Each row As DataRow In foundRow
                    If row("Barcode_Size").ToString.ToUpper = txtBarcodeM.Text.Trim().ToUpper Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record already exists');", True)
                        Exit Sub
                        'ElseIf row("Size_Unit").ToString.ToUpper = ddBarcodeSize.SelectedItem.Text.ToString.ToUpper Then
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record already exists');", True)
                        '    Exit Sub
                    End If
                Next
                '-----------comment on 28/07/2021---------------

                Dim myRow As DataRow
                myRow = dt.NewRow()
                myRow("Barcode_Size_Id") = dt.Rows.Count + 1
                myRow("Barcode_Size") = txtBarcodeM.Text.Trim()
                myRow("Size_Unit") = ddBarcodeSize.SelectedItem.Text
                myRow("Size_Id") = Val(ddBarcodeSize.SelectedValue)
                myRow("is_saved") = 0
                myRow("action") = 1
                dt.Rows.Add(myRow)
                dt.AcceptChanges()

                BindGridBarcode_Size()
                ViewState("View_Barcode_Size") = dt
                txtBarcodeM.Text = String.Empty
                ddBarcodeSize.SelectedIndex = 0
                rdBarcodeSize.Visible = True
                hdBarcode_size.Value = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record inserted successfully');", True)

                'update
            Else

                Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)
                Dim v As String

                oclsBarcodeSize.cmp_id = Val(Session("cmp_id"))
                oclsBarcodeSize.product_id = Val(Session("product_id"))
                oclsBarcodeSize.Barcode = txtBarcodeM.Text.Trim()

                Dim dtbarcode As DataTable = oclsBarcodeSize.Select_Barcode_exists()

                If dtbarcode.Rows.Count > 0 Then
                    v = dtbarcode.Rows(0)("val").ToString()
                End If
                If v = 1 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record already exists');", True)
                    Exit Sub
                End If

                Dim foundRow() As DataRow
                foundRow = dt.Select("action <> 2 and Barcode_Size_Id <>" + hdBarcode_size.Value.ToString)

                For Each row As DataRow In foundRow

                    If row("Barcode_Size").ToString.ToUpper = txtBarcodeM.Text.Trim().ToUpper Or row("Size_Unit").ToString.ToUpper = ddBarcodeSize.SelectedItem.Text.ToString.ToUpper Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record already exists');", True)
                        Exit Sub
                    End If
                Next
                For Each row As DataRow In dt.Rows
                    If row("Barcode_Size_Id") = hdBarcode_size.Value Then
                        row("Barcode_Size") = txtBarcodeM.Text.Trim()
                        row("Size_Unit") = ddBarcodeSize.SelectedItem.Text
                        row("Size_Id") = Val(ddBarcodeSize.SelectedValue)
                        row("is_saved") = IIf(row("is_saved") = 1, 1, 0)
                        If row("is_saved") = 1 And row("action") = 0 Then
                            row("action") = 1
                        ElseIf row("is_saved") = 0 Then
                            row("action") = 1
                        End If
                        row.EndEdit()
                        btnBarcode.Text = "Save"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record updated successfully');", True)
                    End If
                Next

                BindGridBarcode_Size()
                txtBarcodeM.Text = String.Empty
                ddBarcodeSize.SelectedIndex = 0
                rdBarcodeSize.Visible = True
                hdBarcode_size.Value = ""
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:btnBarcode_Click:" + ex.Message)
        End Try
    End Sub
    Public Sub BindDefaultSize()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            bind_drp_Size()
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindDefualtsize:" + ex.Message)
        End Try
    End Sub
    Public Sub BindSizeCost()
        Try
            If Not Session("copy_product_id") = Nothing Then
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                If Val(Session("Product_id")) = Nothing Then
                    oclsProduct.product_id = Val(Session("copy_product_id"))
                Else
                    oclsProduct.product_id = Val(Session("product_id"))
                End If

                oclsProduct.SingleRecord = 0
                Dim dtSize As DataTable = oclsProduct.Select_SizeNCost_By_Product()

                Dim s As String = Session("copy_product_Location_Id")
                Dim sarr() As String = s.Split("#")
                For index = 0 To dtSize.Rows.Count - 1
                    Dim iMatch As Integer = 0
                    For index2 = 0 To sarr.Count - 1
                        If dtSize.Rows(index)("location_id") = sarr(index2).ToString() Then
                            iMatch = 1
                            Exit For

                        End If
                    Next
                    If iMatch = 0 Then
                        dtSize.Rows(index).Delete()
                    End If
                Next
                dtSize.AcceptChanges()

                Dim dt As DataTable = DirectCast(ViewState("View_Cost"), DataTable)
                dt.Merge(dtSize, True, MissingSchemaAction.Ignore)
            Else
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                If Val(Session("Product_id")) = Nothing Then
                    oclsProduct.product_id = Val(Session("copy_product_id"))
                Else
                    oclsProduct.product_id = Val(Session("product_id"))
                End If

                oclsProduct.SingleRecord = 0
                Dim dtSize As DataTable = oclsProduct.Select_SizeNCost_By_Product()

                Dim dt As DataTable = DirectCast(ViewState("View_Cost"), DataTable)

                dt.Merge(dtSize)
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindSizeCost:" + ex.Message)
        End Try

    End Sub
    Public Sub BindSize()
        Try

            If Not Session("copy_product_id") = Nothing Then
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                If Val(Session("Product_id")) = Nothing Then
                    oclsProduct.product_id = Val(Session("copy_product_id"))
                Else
                    oclsProduct.product_id = Val(Session("product_id"))
                End If

                oclsProduct.SingleRecord = 0

                Dim dtSize As DataTable = oclsProduct.Select_SizeNPrice_By_Product_Copy_Product()
                Dim s As String = Session("copy_product_Location_Id")
                Dim sarr() As String = s.Split("#")
                For index = 0 To dtSize.Rows.Count - 1
                    Dim iMatch As Integer = 0
                    For index2 = 0 To sarr.Count - 1
                        If dtSize.Rows(index)("location_id") = sarr(index2).ToString() Then
                            iMatch = 1
                            Exit For

                        End If
                    Next
                    If iMatch = 0 Then
                        dtSize.Rows(index).Delete()
                    End If
                Next
                dtSize.AcceptChanges()
                Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
                dt.Merge(dtSize, True, MissingSchemaAction.Ignore)
            Else
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                If Val(Session("Product_id")) = Nothing Then
                    oclsProduct.product_id = Val(Session("copy_product_id"))
                Else
                    oclsProduct.product_id = Val(Session("product_id"))
                End If

                oclsProduct.SingleRecord = 0
                Dim dtSize As DataTable = oclsProduct.Select_SizeNPrice_By_Product()

                Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)

                dt.Merge(dtSize)
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindSize:" + ex.Message)
        End Try
    End Sub
    Public Sub BindSize_for_copy()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.product_id = Val(Session("copy_product_id"))
            oclsProduct.SingleRecord = 0
            Dim dtSize As DataTable = oclsProduct.Select_SizeNPrice_By_Product()

            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            dt.Merge(dtSize)

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindSize:" + ex.Message)
        End Try
    End Sub
    Protected Sub BindBarcode_Size()
        Try
            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dtProduct As DataTable = oclsProduct.SelectBarcode_Size()

            Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)

            Dim dtSize As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            If dtProduct.Rows.Count > 0 Then
                For Each row As DataRow In dtProduct.Rows
                    Dim myRow As DataRow
                    myRow = dt.NewRow()
                    myRow("Barcode_Size_Id") = Val(row("Barcode_Size_Id"))
                    myRow("Barcode_Size") = row("Barcode_Size").ToString()
                    myRow("Size_Unit") = row("Size_Unit").ToString()

                    Dim SizeVal As Integer = Val(row("Size_Id"))

                    If Val(row("Size_Id")) <> 0 Then
                        For Each rowSize As DataRow In dtSize.Rows
                            If rowSize("Size_Id") = row("Size_Id") Then
                                SizeVal = Val(rowSize("row_id"))
                                Exit For
                            End If
                        Next
                    End If

                    myRow("Size_Id") = SizeVal
                    myRow("is_saved") = 1
                    myRow("action") = 0
                    dt.Rows.Add(myRow)
                    dt.AcceptChanges()
                Next
            End If

            rdBarcodeSize.Visible = True
            rdBarcodeSize.DataSource = dt
            rdBarcodeSize.DataBind()

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindBarcode_Size:" + ex.Message)
        End Try
    End Sub
    Protected Sub BindBarcode_Size_for_copy()
        Try
            oclsProduct.product_id = Val(Session("copy_product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dtProduct As DataTable = oclsProduct.SelectBarcode_Size()

            Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)

            Dim dtSize As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            If dtProduct.Rows.Count > 0 Then
                For Each row As DataRow In dtProduct.Rows
                    Dim myRow As DataRow
                    myRow = dt.NewRow()
                    myRow("Barcode_Size_Id") = Val(row("Barcode_Size_Id"))
                    myRow("Barcode_Size") = row("Barcode_Size").ToString()
                    myRow("Size_Unit") = row("Size_Unit").ToString()

                    Dim SizeVal As Integer = Val(row("Size_Id"))

                    If Val(row("Size_Id")) <> 0 Then
                        For Each rowSize As DataRow In dtSize.Rows
                            If rowSize("Size_Id") = row("Size_Id") Then
                                SizeVal = Val(rowSize("row_id"))
                                Exit For
                            End If
                        Next
                    End If
                    myRow("Size_Id") = SizeVal
                    myRow("is_saved") = 1
                    myRow("action") = 0
                    dt.Rows.Add(myRow)
                    dt.AcceptChanges()
                Next
            End If

            rdBarcodeSize.Visible = True
            rdBarcodeSize.DataSource = dt
            rdBarcodeSize.DataBind()

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindBarcode_Size:" + ex.Message)
        End Try
    End Sub
    Protected Sub bind_PricesBy_Cmp()
        Dim dt As DataTable = oclsProduct.SelectPricesByCmp()
        dtPrc = dt
    End Sub
    Protected Sub bind_drp_Size()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            If Val(Session("Product_id")) = Nothing Then
                oclsProduct.product_id = Val(Session("copy_product_id"))
            Else
                oclsProduct.product_id = Val(Session("product_id"))
            End If

            oclsProduct.SingleRecord = 0
            Dim dt As DataTable = oclsProduct.Select_SizeNPrice_By_Product()

            'Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = "active = 1"

            If dt.Rows.Count > 0 Then

                ddBarcodeSize.DataSource = dv
                ddBarcodeSize.DataTextField = "Size_Unit"
                ddBarcodeSize.DataValueField = "row_Id"
                ddBarcodeSize.DataBind()

                'Dim li1 As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
                ddBarcodeSize.Items.Insert(0, "SELECT")

            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:bind_drp_Size:" + ex.Message)
        End Try
    End Sub
    Protected Sub bindAutocompateIngredient()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.product_id = Val(Session("product_id")) Or Val(Session("copy_product_id"))
            Dim dtProduct As DataTable = oclsProduct.SelectIngredient()

        Catch ex As Exception
            LogHelper.Error("Product_Master:bindAutocompateIngredient:" + ex.Message)
        End Try
    End Sub
    Protected Sub bindAutocompateCondiment()
        Try
            If (Session("copy_condiment") IsNot Nothing) Then
                If (Session("copy_condiment").ToString() = "1") Then

                    oclsProduct.product_id = Val(Session("product_id")) Or Val(Session("copy_product_id"))
                    oclsCondiment.cmp_id = Val(Session("cmp_id"))

                    Dim dt As DataTable = oclsProduct.Select_Condiment_By_Product()

                    If dt.Rows.Count > 0 Then
                        rdCondiment.DataSource = dt
                        rdCondiment.DataBind()
                    End If
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:bindAutocompateIngredient:" + ex.Message)
        End Try
    End Sub
    Protected Sub BindGridBarcode_Size()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = "action <> '2'"

            If dt.Rows.Count > 0 Then
                rdBarcodeSize.DataSource = dv
                rdBarcodeSize.DataBind()
            Else
                rdBarcodeSize.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGridBarcode_Size:" + ex.Message)
        End Try
    End Sub
    Protected Sub selectIngredientList()
        Try
            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0

            Dim dtIngredien As DataTable = oclsProduct.Select_Ingredient_By_Product()

            Dim dt As DataTable = DirectCast(ViewState("View_Ingredient"), DataTable)

            dt.Merge(dtIngredien, True, MissingSchemaAction.Ignore)

            If dt.Rows.Count > 0 Then
                rdIngredient.DataSource = dt
                rdIngredient.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:selectIngredientList:" + ex.Message)
        End Try
    End Sub
    Protected Sub selectIngredientList_for_copy()
        Try
            oclsProduct.product_id = Val(Session("copy_product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0

            Dim dtIngredien As DataTable = oclsProduct.Select_Ingredient_By_Product()

            Dim dt As DataTable = DirectCast(ViewState("View_Ingredient"), DataTable)

            dt.Merge(dtIngredien, True, MissingSchemaAction.Ignore)

            If dt.Rows.Count > 0 Then
                rdIngredient.DataSource = dt
                rdIngredient.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:selectIngredientList:" + ex.Message)
        End Try
    End Sub
    Protected Sub selectCondimentList()
        Try
            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0

            Dim dtCondiment As DataTable = oclsProduct.Select_Condiment_By_Product()

            '-------06/07/2022------------
            ViewState("check") = dtCondiment

            Dim dt As DataTable = DirectCast(ViewState("View_Condiment"), DataTable)

            dt.Merge(dtCondiment, True, MissingSchemaAction.Ignore)

            If dt.Rows.Count > 0 Then
                rdCondiment.DataSource = dt
                rdCondiment.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:selectCondimentList:" + ex.Message)
        End Try
    End Sub
    Protected Sub selectCondimentList_for_copy()
        Try
            oclsProductCondiment.product_id = Val(Session("copy_product_id"))
            oclsProductCondiment.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0

            Dim dtCondiment As DataTable = oclsProduct.Select_Condiment_By_Product()

            '-------06/07/2022------------
            ViewState("check") = dtCondiment

            Dim dt As DataTable = DirectCast(ViewState("View_Condiment"), DataTable)

            dt.Merge(dtCondiment, True, MissingSchemaAction.Ignore)

            If dt.Rows.Count > 0 Then
                rdCondiment.DataSource = dt
                rdCondiment.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:selectCondimentList:" + ex.Message)
        End Try
    End Sub
    Public Sub getPrcNSizes()

        Dim dtSize As DataTable = oclsProduct.Select_SizeNPrice_By_Product()
        ViewState("View_Size") = dtSize

        'If (dtSize.Rows.Count > 0) Then
        '    bind_Tabs()
        'End If

    End Sub

    Private Sub btnShowText_Click(ByVal sender As Object, ByVal e As EventArgs)
        'throw new NotImplementedException();
        'Dim txt As TextBox = CType(tbcDynamic.ActiveTab.FindControl(("txtB" + tbcDynamic.ActiveTabIndex)), TextBox)
        'lblMessage.Text = txt.Text
        'Response.Write(txt.Text);
    End Sub

    Public Sub bindImage()
        Try
            objclProdImg.cmp_id = Val(Session("cmp_id"))
            objclProdImg.product_id = Val(Session("product_id"))
            Dim dt As DataTable = objclProdImg.Select()
            If dt.Rows.Count > 0 Then

                If dt.Rows(0)("image_name") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Dim bytes As Byte() = DirectCast(dt.Rows(0)("image_name"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                    hdlogo.Value = Convert.ToBase64String(bytes)

                    ViewState("Image") = 1

                    RadBinaryImage1.DataValue = Nothing

                    RadGrid1.DataSource = dt
                    RadGrid1.DataBind()

                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindImage:" + ex.Message)
        End Try
    End Sub
    Public Sub BindProduct()
        Try
            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dtProduct As DataTable = oclsProduct.Select()
            If dtProduct.Rows.Count > 0 Then
                txtPName.Text = dtProduct.Rows(0)("name").ToString
                radCategory.ClearSelection()
                radCategory.SelectedValue = Val(dtProduct.Rows(0)("category_id"))
                ddKeyMap.ClearSelection()
                ddKeyMap.SelectedValue = Val(dtProduct.Rows(0)("key_map_id"))
                txtdescription.Text = dtProduct.Rows(0)("description").ToString
                radDept.ClearSelection()
                radDept.SelectedValue = Val(dtProduct.Rows(0)("department_id"))
                radCourse.ClearSelection()
                radCourse.SelectedValue = Val(dtProduct.Rows(0)("course_id"))
                Dim collection As IList(Of RadComboBoxItem) = radPrinter.Items
                Dim stArr As String() = dtProduct.Rows(0)("printer_id").ToString().Split("#")

                For i As Integer = 0 To stArr.Length - 1
                    For Each item As RadComboBoxItem In collection
                        If stArr(i).ToString = item.Value Then
                            item.Checked = True
                            Exit For
                        End If
                    Next
                Next

                txtList.Text = dtProduct.Rows(0)("list").ToString
                txtOtherId.Text = dtProduct.Rows(0)("other_id").ToString
                txtOtherSize.Text = dtProduct.Rows(0)("other_size").ToString
                txtBase.Text = dtProduct.Rows(0)("Base").ToString()
                radUnit.SelectedValue = dtProduct.Rows(0)("Unit_id").ToString()
                chkActive.Checked = IIf(dtProduct.Rows(0)("active") = "Yes", True, False).ToString()
                chksizezero.Checked = IIf(dtProduct.Rows(0)("size_zero") = 1, True, False).ToString()
                chkstock.Checked = IIf(dtProduct.Rows(0)("is_stock") = 1, True, False).ToString()
                chckIsCondiment.Checked = IIf(dtProduct.Rows(0)("Is_Condiment") = 1, True, False).ToString()
                chckCloakRoom.Checked = IIf(dtProduct.Rows(0)("Cloak_Room") = 1, True, False).ToString()
                chk_PriceOnScaleWeight.Checked = IIf(dtProduct.Rows(0)("Is_PriceOnScaleWeight") = 1, True, False).ToString()
                chk_IsHouse.Checked = IIf(dtProduct.Rows(0)("IsHouse") = 1, True, False).ToString()
                chk_IsPkgProduct.Checked = IIf(dtProduct.Rows(0)("IsPkgProduct") = 1, True, False).ToString()
                radIs_DanceVoucher.SelectedValue = dtProduct.Rows(0)("Is_DanceVoucher").ToString()
                txtSortingNo.Text = dtProduct.Rows(0)("SortingNo").ToString()
                chk_additionalTax.Checked = IIf(dtProduct.Rows(0)("IsAdditionalTax") = 1, True, False).ToString()
                chk_ForKiosk.Checked = IIf(dtProduct.Rows(0)("ForKiosk") = 1, True, False).ToString()
                chk_IsOutOfStock.Checked = IIf(dtProduct.Rows(0)("Is_OutOfStock") = 1, True, False).ToString()
                If chk_ForKiosk.Checked = True Then
                    fileupload.MaxFileSize = 104857600
                Else
                    fileupload.MaxFileSize = 104857600
                End If
                If dtProduct.Rows(0)("pImage") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Try
                        Dim bytes As Byte() = DirectCast(dtProduct.Rows(0)("pImage"), Byte())
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                        hdlogo.Value = Convert.ToBase64String(bytes)
                        ViewState("Image") = 1
                        RadBinaryImage1.DataValue = dtProduct.Rows(0)("pImage")
                    Catch ex As Exception
                    End Try
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindProduct:" + ex.Message)
        End Try
    End Sub
    Public Sub BindProduct_for_copy()
        Try
            oclsProduct.product_id = Session("copy_product_id")
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dtProduct As DataTable = oclsProduct.Select()
            If dtProduct.Rows.Count > 0 Then
                '----------------09/11/2022---------------------------
                txtPName.Text = dtProduct.Rows(0)("name").ToString()
                '---------------------------
                radCategory.ClearSelection()
                radCategory.SelectedValue = Val(dtProduct.Rows(0)("category_id"))
                ddKeyMap.ClearSelection()
                ddKeyMap.SelectedValue = Val(dtProduct.Rows(0)("key_map_id"))

                txtdescription.Text = dtProduct.Rows(0)("description").ToString
                radDept.ClearSelection()
                radDept.SelectedValue = Val(dtProduct.Rows(0)("department_id"))
                radCourse.ClearSelection()
                radCourse.SelectedValue = Val(dtProduct.Rows(0)("course_id"))
                Dim collection As IList(Of RadComboBoxItem) = radPrinter.Items
                Dim stArr As String() = dtProduct.Rows(0)("printer_id").ToString().Split("#")

                For i As Integer = 0 To stArr.Length - 1
                    For Each item As RadComboBoxItem In collection
                        If stArr(i).ToString = item.Value Then
                            item.Checked = True
                            Exit For
                        End If
                    Next
                Next
                txtList.Text = dtProduct.Rows(0)("list").ToString
                txtOtherId.Text = dtProduct.Rows(0)("other_id").ToString
                txtOtherSize.Text = dtProduct.Rows(0)("other_size").ToString
                txtBase.Text = dtProduct.Rows(0)("Base").ToString()
                radUnit.SelectedValue = dtProduct.Rows(0)("Unit_id").ToString()
                chkActive.Checked = IIf(dtProduct.Rows(0)("active") = "Yes", True, False).ToString()
                chksizezero.Checked = IIf(dtProduct.Rows(0)("size_zero") = 1, True, False).ToString()
                chkstock.Checked = IIf(dtProduct.Rows(0)("is_stock") = 1, True, False).ToString()
                '----------------09/11/2022---------------------------
                chckIsCondiment.Checked = IIf(dtProduct.Rows(0)("Is_Condiment") = 1, True, False).ToString()
                chckCloakRoom.Checked = IIf(dtProduct.Rows(0)("Cloak_Room") = 1, True, False).ToString()
                chk_PriceOnScaleWeight.Checked = IIf(dtProduct.Rows(0)("Is_PriceOnScaleWeight") = 1, True, False).ToString()
                chk_IsHouse.Checked = IIf(dtProduct.Rows(0)("IsHouse") = 1, True, False).ToString()
                chk_IsPkgProduct.Checked = IIf(dtProduct.Rows(0)("IsPkgProduct") = 1, True, False).ToString()
                radIs_DanceVoucher.SelectedValue = dtProduct.Rows(0)("Is_DanceVoucher").ToString()
                txtSortingNo.Text = dtProduct.Rows(0)("SortingNo").ToString()
                chk_additionalTax.Checked = IIf(dtProduct.Rows(0)("IsAdditionalTax") = 1, True, False).ToString()
                chk_ForKiosk.Checked = IIf(dtProduct.Rows(0)("ForKiosk") = 1, True, False).ToString()
                chk_IsOutOfStock.Checked = IIf(dtProduct.Rows(0)("Is_OutOfStock") = 1, True, False).ToString()

                If dtProduct.Rows(0)("pImage") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Try
                        Dim bytes As Byte() = DirectCast(dtProduct.Rows(0)("pImage"), Byte())
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                        hdlogo.Value = Convert.ToBase64String(bytes)
                        ViewState("Image") = 1
                        RadBinaryImage1.DataValue = dtProduct.Rows(0)("pImage")
                    Catch ex As Exception
                    End Try
                End If
                '------------------------------------
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindProduct:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("product_id") = Nothing Then
                Clear()
                clearIngrediean_condiment()
            Else
                Clear1()
                BindProduct()
                resetSizePriceBarcode()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub resetSizePriceBarcode()
        Try
            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.SelectProduct_Size()
            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dtbarcode As DataTable = oclsProduct.SelectBarcode_Size()
            If dtbarcode.Rows.Count > 0 Then
                rdBarcodeSize.DataSource = dtbarcode
                rdBarcodeSize.DataBind()
            Else
                rdBarcodeSize.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:resetSizePrice:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Session("copy_product_id") = Nothing
            Response.Redirect("Product_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub Clear1()
        Try
            ddLocation.SelectedIndex = 0
            txtBarcodeM.Text = String.Empty

            oclsProductCondiment.product_id = Val(Session("product_id"))
            oclsProductCondiment.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProductCondiment.SelectCondiment()

            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))

            Dim dtIngredien As DataTable = oclsProduct.SelectIngredientByProduct()

        Catch ex As Exception
            LogHelper.Error("Product_Master:Clear1:" + ex.Message)
        End Try
    End Sub
    Private Sub clearIngrediean_condiment()
        Try

        Catch ex As Exception
            LogHelper.Error("Product_Master:clearIngrediean_condiment:" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                dt.Rows(i).Delete()
            Next
            BindDefaultSize()
            rdBarcodeSize.DataSource = String.Empty
            rdBarcodeSize.DataBind()

            Dim dtb As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)

            For i As Integer = dtb.Rows.Count - 1 To 0 Step -1
                dtb.Rows(i).Delete()
            Next
            rdBarcodeSize.Visible = False
            txtPName.Text = ""
            radCategory.ClearSelection()
            radDept.ClearSelection()
            radCourse.ClearSelection()
            radPrinter.ClearCheckedItems()
            ddKeyMap.ClearSelection()
            'ddTax.ClearSelection()
            txtList.Text = Nothing
            txtOtherId.Text = ""
            txtOtherSize.Text = ""
            txtdescription.Text = ""
            chkActive.Checked = True
            txtBase.Text = ""
            radUnit.ClearSelection()
            ddLocation.SelectedIndex = 0
            txtBarcodeM.Text = String.Empty
            radIs_DanceVoucher.ClearSelection()
        Catch ex As Exception
            LogHelper.Error("Product_Master:Clear:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim dt_condiment As DataTable = DirectCast(ViewState("Condiment_Data"), DataTable)

            If Not hdBarcode_size.Value = "" Or Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please update barcode data');", True)
            ElseIf chk_IsPkgProduct.Checked = True Then
                If dt_condiment Is Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('This is Package Product, Add at least one Condiment.');", True)
                ElseIf dt_condiment.Rows.Count = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('This is Package Product, Add at least one Condiment.');", True)
                ElseIf chk_IsPkgProduct.Checked = True And chk_PriceOnScaleWeight.Checked = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('You can't select both Price on Scale Weight and Is Package Product at the same time.');", True)
                Else
                    Insert_Data()
                End If
            Else
                Insert_Data()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:btnSave_Click:" + ex.Message)
        End Try


    End Sub

    Private Sub Insert_Data()
        Try

            oclsProduct.P_Image = RadBinaryImage1.DataValue
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.Category_id = Val(radCategory.SelectedValue)
            oclsProduct.key_map_id = 0
            oclsProduct.code = ""
            oclsProduct.name = txtPName.Text.Trim()
            oclsProduct.price = 0
            oclsProduct.barcode = ""
            oclsProduct.description = txtdescription.Text
            oclsProduct.Is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsProduct.Ip_address = Request.UserHostAddress
            oclsProduct.login_id = Val(Session("login_id"))
            oclsProduct.department_id = Val(radDept.SelectedValue)
            oclsProduct.course_id = Val(radCourse.SelectedValue)
            oclsProduct.list = Val(txtList.Text)
            oclsProduct.printer_id = GetSelectedValue(radPrinter)
            oclsProduct.Tax_id = 0
            oclsProduct.Actual_Price = 0
            oclsProduct.Tax = 0
            oclsProduct.machine_id = 0
            oclsProduct.other_id = txtOtherId.Text
            oclsProduct.other_size = txtOtherSize.Text
            oclsProduct.Is_Ingredient = 1
            oclsProduct.Is_Condiment = IIf(chckIsCondiment.Checked = True, 1, 0)
            oclsProduct.Cloak_Room = IIf(chckCloakRoom.Checked = True, 1, 0)
            oclsProduct.Base = Val(txtBase.Text)
            oclsProduct.Unit_id = Val(IIf(radUnit.SelectedValue = "", 0, radUnit.SelectedValue))
            oclsProduct.is_DanceVoucher = Val(IIf(radIs_DanceVoucher.SelectedValue = "", 0, radIs_DanceVoucher.SelectedValue))
            oclsProduct.size_zero = IIf(chksizezero.Checked = True, 1, 0)
            oclsProduct.is_stock = IIf(chkstock.Checked = True, 1, 0)
            oclsProduct.Is_PriceOnScaleWeight = IIf(chk_PriceOnScaleWeight.Checked = True, 1, 0)
            oclsProduct.IsHouse = IIf(chk_IsHouse.Checked = True, 1, 0)
            oclsProduct.IsPkgProduct = IIf(chk_IsPkgProduct.Checked = True, 1, 0)
            oclsProduct.SortingNo = Val(txtSortingNo.Text)
            oclsProduct.IsAdditionalTax = IIf(chk_additionalTax.Checked = True, 1, 0)
            oclsProduct.ForKiosk = IIf(chk_ForKiosk.Checked = True, 1, 0)
            oclsProduct.Is_OutOfStock = IIf(chk_IsOutOfStock.Checked = True, 1, 0)

            SaveRecordInDT_From_DynamicGrid()
            SaveRecordInDTCostFrom_GV()

            If Session("product_id") = Nothing Then
                oclsProduct.product_id = 0
                Dim id As Integer = oclsProduct.Insert()
                Session("Last_product_id") = id
                Dim i As Integer = 0
                Dim j As Integer = 0
                For Each file As UploadedFile In fileupload.UploadedFiles
                    i = i + 1
                Next
                If i > 0 Then
                    RadBinaryImage1.DataValue = Nothing
                    For Each file As UploadedFile In fileupload.UploadedFiles
                        Dim image As Byte()
                        Dim fileLength As Long = fileupload.UploadedFiles(j).InputStream.Length
                        image = New Byte(fileLength - 1) {}
                        fileupload.UploadedFiles(j).InputStream.Read(image, 0, image.Length)
                        RadBinaryImage1.DataValue = image
                        Image1.Visible = True
                        Dim bytes As Byte() = RadBinaryImage1.DataValue
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                        objclProdImg.image_name = RadBinaryImage1.DataValue

                        objclProdImg.cmp_id = Session("cmp_id")
                        objclProdImg.product_id = id

                        objclProdImg.Insert()
                        j = j + 1
                    Next
                Else
                    RadBinaryImage1.DataValue = ViewState("logo")
                End If
                Insert_Size(id)
                Insert_Cost(id)
                Insert_Barcode(id)
                Insert_ProductIngredient(id)
                Insert_Condiment(id)
                insert_Allergy(id)
                Session("Success") = "Record inserted successfully"
            Else

                oclsProduct.product_id = Val(Session("product_id"))
                oclsProduct.Update()
                Session("Last_product_id") = Val(Session("product_id"))
                Dim i As Integer = 0
                Dim j As Integer = 0
                For Each file As UploadedFile In fileupload.UploadedFiles
                    i = i + 1
                Next
                If i > 0 Then
                    RadBinaryImage1.DataValue = Nothing
                    For Each file As UploadedFile In fileupload.UploadedFiles

                        Dim image As Byte()
                        Dim fileLength As Long = fileupload.UploadedFiles(j).InputStream.Length
                        image = New Byte(fileLength - 1) {}
                        fileupload.UploadedFiles(j).InputStream.Read(image, 0, image.Length)
                        RadBinaryImage1.DataValue = image
                        Image1.Visible = True
                        Dim bytes As Byte() = RadBinaryImage1.DataValue
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                        objclProdImg.image_name = RadBinaryImage1.DataValue

                        objclProdImg.cmp_id = Session("cmp_id")
                        objclProdImg.product_id = Val(Session("product_id"))

                        objclProdImg.Insert()
                        j = j + 1
                    Next
                Else
                    RadBinaryImage1.DataValue = ViewState("logo")
                End If
                Insert_Size(Val(Session("product_id")))
                Insert_Cost(Val(Session("product_id")))
                Insert_Condiment(Val(Session("product_id")))
                Insert_Barcode(Val(Session("Product_id")))
                Insert_ProductIngredient(Val(Session("Product_id")))
                insert_Allergy(Val(Session("Product_id")))
                Session("Success") = "Record updated successfully"
            End If

            Session("product_id") = Nothing
            Session("copy_product_id") = Nothing
            Session("copy_product_Location_Id") = Nothing
            Response.Redirect("Product_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:Insert_Data:" + ex.Message)
        End Try
    End Sub

    Protected Sub Insert_Size(ByVal id As Integer)
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("Size_Id"), New DataColumn("ref_size_id")})
            Dim size_id As Integer

            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dtbarcode As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)

            Dim price As Decimal
            Dim per As Decimal
            Dim Actual As Decimal
            Dim amt As Decimal
            Dim Tax1 As Decimal

            Dim price2 As Decimal
            Dim per2 As Decimal
            Dim Actual2 As Decimal
            Dim amt2 As Decimal
            Dim Tax2 As Decimal

            For Each row As DataRow In dt.Rows
                Try
                    If Session("IsExclusiveTax") IsNot Nothing Then
                        If Val(Session("IsExclusiveTax").ToString()) = 0 Then

                            oclsTax.Tax_id = row("Tax_id").ToString
                            If Val(row("Tax_id").ToString) > 0 Then
                                'oclsTax.Tax_id = Val(ddTax.SelectedValue)
                                oclsTax.cmp_id = Val(Session("cmp_id"))
                                Dim dtTax As DataTable = oclsTax.Select()
                                Dim hvalue As Decimal
                                Dim hmode As String

                                If dtTax.Rows.Count > 0 Then
                                    hvalue = Convert.ToDecimal(dtTax.Rows(0)("Value").ToString)
                                    hmode = dtTax.Rows(0)("Mode").ToString

                                End If
                                price = Val(row("Price"))
                                'Val(row("Price"))
                                Dim t As Decimal
                                If hmode = "%" Then
                                    per = Val(hvalue)
                                    t = 1 + (per / 100)

                                    Actual = Math.Round(price / t, 2)

                                ElseIf hmode = "Amt" Then
                                    amt = Val(hvalue)
                                    Actual = price - amt
                                Else
                                    Actual = price
                                End If

                                Tax1 = Math.Round(price - Actual, 2)

                                row("Tax") = Tax1
                            Else
                                price = Val(row("Price"))
                                Tax1 = 0
                                row("Tax") = 0

                            End If

                            '------------------Tax2 calculation 13/04/2022-------------------------
                            oclsTax.Tax_id = row("Tax_id2").ToString
                            If Val(row("Tax_id2").ToString) > 0 Then
                                'oclsTax.Tax_id = Val(ddTax.SelectedValue)
                                oclsTax.cmp_id = Val(Session("cmp_id"))
                                Dim dtTax2 As DataTable = oclsTax.Select()
                                Dim hvalue2 As Decimal
                                Dim hmode2 As String

                                If dtTax2.Rows.Count > 0 Then
                                    hvalue2 = Convert.ToDecimal(dtTax2.Rows(0)("Value").ToString)
                                    hmode2 = dtTax2.Rows(0)("Mode").ToString

                                End If
                                price2 = Val(row("Price"))
                                'Val(row("Price"))
                                Dim t2 As Decimal
                                If hmode2 = "%" Then
                                    per2 = Val(hvalue2)
                                    t2 = 1 + (per2 / 100)

                                    Actual2 = Math.Round(price2 / t2, 2)

                                ElseIf hmode2 = "Amt" Then
                                    amt2 = Val(hvalue2)
                                    Actual2 = price2 - amt2
                                Else
                                    Actual2 = price2
                                End If

                                Tax2 = Math.Round(price - Actual2, 2)
                                row("Tax2") = Tax2
                            Else
                                price2 = Val(row("Price"))
                                Tax2 = 0
                                row("Tax2") = 0

                            End If

                            row("Actual_Price") = Math.Round(price - (Tax1 + Tax2), 2)
                            row.EndEdit()
                            dt.AcceptChanges()
                        Else
                            '---------------------add new logic for exclusive tax ------

                            oclsTax.Tax_id = row("Tax_id").ToString
                            If Val(row("Tax_id").ToString) > 0 Then
                                oclsTax.cmp_id = Val(Session("cmp_id"))
                                Dim dtTax As DataTable = oclsTax.Select()
                                Dim hvalue As Decimal
                                Dim hmode As String

                                If dtTax.Rows.Count > 0 Then
                                    hvalue = Convert.ToDecimal(dtTax.Rows(0)("Value").ToString)
                                    hmode = dtTax.Rows(0)("Mode").ToString

                                End If
                                Actual = Val(row("Price"))
                                Dim t As Decimal
                                If hmode = "%" Then
                                    per = Val(hvalue)
                                    t = Actual * (per / 100)

                                ElseIf hmode = "Amt" Then
                                    t = Val(hvalue)
                                Else
                                    t = 0
                                End If

                                Tax1 = Math.Round(t, 2)

                                row("Tax") = Tax1
                            Else
                                Actual = Val(row("Price"))
                                Tax1 = 0
                                row("Tax") = 0

                            End If

                            '------------------Tax2 calculation 13/04/2022-------------------------
                            oclsTax.Tax_id = row("Tax_id2").ToString
                            If Val(row("Tax_id2").ToString) > 0 Then
                                oclsTax.cmp_id = Val(Session("cmp_id"))
                                Dim dtTax2 As DataTable = oclsTax.Select()
                                Dim hvalue2 As Decimal
                                Dim hmode2 As String

                                If dtTax2.Rows.Count > 0 Then
                                    hvalue2 = Convert.ToDecimal(dtTax2.Rows(0)("Value").ToString)
                                    hmode2 = dtTax2.Rows(0)("Mode").ToString

                                End If
                                Actual2 = Val(row("Price"))
                                Dim t2 As Decimal
                                If hmode2 = "%" Then
                                    per2 = Val(hvalue2)
                                    t2 = Actual2 * (per2 / 100)

                                ElseIf hmode2 = "Amt" Then
                                    t2 = Val(hvalue2)

                                Else
                                    t2 = 0
                                End If

                                Tax2 = Math.Round(t2, 2)
                                row("Tax2") = Tax2
                            Else
                                Actual2 = Val(row("Price"))
                                Tax2 = 0
                                row("Tax2") = 0

                            End If

                            row("Actual_Price") = Math.Round(Actual, 2)
                            row("Price") = Math.Round(Actual + (Tax1 + Tax2), 2)

                            row.EndEdit()
                            dt.AcceptChanges()

                        End If
                    End If

                Catch ex As Exception

                End Try
            Next

            If Not Session("copy_product_id") = Nothing Then
                Dim stArr As String() = Session("copy_product_Location_Id").ToString().Split("#")
                For Each row As DataRow In dt.Rows
                    Try
                        If row("action") = 1 Then
                            oclsSize.product_id = id
                            If Not Session("copy_product_id") = Nothing Then
                                oclsSize.Size_id = Nothing
                            Else
                                oclsSize.Size_id = row("Size_Id")
                            End If
                            oclsSize.Name = row("Name")
                            oclsSize.Size = row("Size")
                            oclsSize.Unit_id = row("Unit_Id")
                            oclsSize.is_active = IIf(row("Active") = 1, 1, 0)
                            oclsSize.click_and_collect = IIf(row("click_and_collect") = 1, 1, 0)
                            oclsSize.deliver = IIf(row("deliver") = 1, 1, 0)
                            oclsSize.Order_at_table = IIf(row("Order_at_table") = 1, 1, 0)


                            If Not Session("copy_product_id") = Nothing Then
                                oclsSize.Price_id = Nothing
                            Else
                                oclsSize.Price_id = row("Price_Id")
                            End If

                            If Not IsDBNull(row("Price")) Then
                                oclsSize.Price = row("Price")
                            Else
                                oclsSize.Price = 0
                            End If
                            oclsSize.Location_Id = Val(row("Location_id"))
                            oclsSize.Actual_Price = Val(row("Actual_Price"))
                            oclsSize.Tax = Val(row("Tax"))
                            oclsSize.Tax_id = row("Tax_id")
                            '------ tax2 added on 13/04/2022-----------
                            oclsSize.Tax2 = Val(row("Tax2"))
                            oclsSize.Tax_id2 = row("Tax_id2")
                            oclsSize.sorting_no = row("sorting_no")

                            oclsSize.login_id = Val(Session("login_id"))
                            oclsSize.cmp_id = Val(Session("cmp_id"))
                            oclsSize.Ip_address = Request.UserHostAddress
                            oclsSize.action = row("action")

                            'If ddPrices.SelectedIndex <> 0 Then
                            '    oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            'Else
                            '    oclsSize.Product_Price_Id = 0
                            'End If

                            '------Change 01/11/2021----------
                            If ddPrices.SelectedIndex <> 0 And row("Product_Price_Id") = ddPrices.SelectedValue Then
                                oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            Else
                                oclsSize.Product_Price_Id = Val(row("Product_Price_Id"))
                            End If
                            '------01/11/2021----------


                            size_id = oclsSize.InsUpdDel_SizeNPrice()

                            For Each row_barcode As DataRow In dtbarcode.Rows
                                If Val(row("row_id")) = Val(row_barcode("Size_Id")) Then
                                    row_barcode("Size_Id") = size_id
                                    row.EndEdit()
                                    dt.AcceptChanges()
                                End If
                            Next
                        ElseIf row("action") = 2 Then
                            oclsSize.product_id = id
                            oclsSize.Size_id = row("Size_Id")
                            oclsSize.Name = row("Name")
                            oclsSize.Size = row("Size")
                            oclsSize.Unit_id = row("Unit_Id")
                            oclsSize.is_active = IIf(row("Active") = 1, 1, 0)
                            oclsSize.click_and_collect = IIf(row("click_and_collect") = 1, 1, 0)
                            oclsSize.deliver = IIf(row("deliver") = 1, 1, 0)
                            oclsSize.Order_at_table = IIf(row("Order_at_table") = 1, 1, 0)
                            oclsSize.Price_id = row("Price_Id")
                            oclsSize.Price = Val(row("Price"))

                            If Session("product_id") = Nothing Then
                                oclsSize.Location_Id = ddLocation.SelectedValue
                            Else
                                oclsSize.Location_Id = row("Location_Id")
                            End If
                            oclsSize.Actual_Price = Val(row("Actual_Price"))
                            oclsSize.Tax = Val(row("Tax"))
                            oclsSize.Tax_id = row("Tax_id")
                            '------ tax2 added on 13/04/2022-----------
                            oclsSize.Tax2 = Val(row("Tax2"))
                            oclsSize.Tax_id2 = row("Tax_id2")
                            oclsSize.sorting_no = row("sorting_no")

                            oclsSize.login_id = Val(Session("login_id"))
                            oclsSize.cmp_id = Val(Session("cmp_id"))
                            oclsSize.Ip_address = Request.UserHostAddress
                            oclsSize.action = row("action")

                            If ddPrices.SelectedIndex <> 0 Then
                                oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            Else
                                oclsSize.Product_Price_Id = 0
                            End If

                            '------Change 01/11/2021----------
                            'If ddPrices.SelectedIndex <> 0 And row("Location_Id") = ddLocation.SelectedValue Then
                            '    oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            'Else
                            '    oclsSize.Product_Price_Id = Val(row("Product_Price_Id"))
                            'End If
                            '------01/11/2021----------


                            oclsSize.InsUpdDel_SizeNPrice()

                        End If

                    Catch ex As Exception
                        LogHelper.Error("Product_Master:for 1jjk:" + ex.Message)
                    End Try
                Next
            Else
                For Each row As DataRow In dt.Rows
                    Try


                        'If Val(row("Tax_id").ToString) > 0 Then
                        If row("action") = 1 Then
                            oclsSize.product_id = id
                            If Not Session("copy_product_id") = Nothing Then
                                oclsSize.Size_id = Nothing
                            Else
                                oclsSize.Size_id = row("Size_Id")
                            End If
                            oclsSize.Name = row("Name")
                            oclsSize.Size = row("Size")
                            oclsSize.Unit_id = row("Unit_Id")

                            oclsSize.is_active = IIf(row("Active") = 1, 1, 0)
                            oclsSize.click_and_collect = IIf(row("click_and_collect") = 1, 1, 0)
                            oclsSize.deliver = IIf(row("deliver") = 1, 1, 0)
                            oclsSize.Order_at_table = IIf(row("Order_at_table") = 1, 1, 0)

                            If Not Session("copy_product_id") = Nothing Then
                                oclsSize.Price_id = Nothing
                            Else
                                oclsSize.Price_id = row("Price_Id")
                            End If

                            If Not IsDBNull(row("Price")) Then
                                oclsSize.Price = row("Price")
                            Else
                                'If row("Price") = "" Then
                                oclsSize.Price = 0
                                'End If
                            End If


                            If Session("product_id") = Nothing And Session("copy_product_id") = Nothing Then
                                oclsSize.Location_Id = row("Location_Id")
                            ElseIf Not IsDBNull(row("Location_Id")) Then
                                oclsSize.Location_Id = row("Location_Id")
                            Else
                                oclsSize.Location_Id = 0
                            End If

                            oclsSize.Actual_Price = Val(row("Actual_Price"))
                            oclsSize.Tax = Val(row("Tax"))
                            oclsSize.Tax_id = row("Tax_id")
                            '------ tax2 added on 13/04/2022-----------
                            oclsSize.Tax2 = Val(row("Tax2"))
                            oclsSize.Tax_id2 = row("Tax_id2")
                            oclsSize.sorting_no = row("sorting_no")

                            oclsSize.login_id = Val(Session("login_id"))
                            oclsSize.cmp_id = Val(Session("cmp_id"))
                            oclsSize.Ip_address = Request.UserHostAddress
                            oclsSize.action = row("action")


                            '------Change 01/11/2021----------

                            'If ddPrices.SelectedIndex <> 0 Then
                            '    oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            'Else
                            '    oclsSize.Product_Price_Id = 0
                            'End If

                            If ddPrices.SelectedIndex <> 0 And row("Product_Price_Id") = ddPrices.SelectedValue Then
                                oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            Else
                                oclsSize.Product_Price_Id = Val(row("Product_Price_Id"))
                                '----------------Start if condition add on 16/03/2022----------------
                                'If Val(row("Product_Price_Id")) = 0 Then
                                '    oclsSize.Product_Price_Id = ddPrices.SelectedValue
                                'Else
                                '    oclsSize.Product_Price_Id = Val(row("Product_Price_Id"))
                                'End If
                                '----------------End----------------
                            End If
                            '------01/11/2021----------

                            size_id = oclsSize.InsUpdDel_SizeNPrice()

                            For Each row_barcode As DataRow In dtbarcode.Rows
                                If Val(row("row_id")) = Val(row_barcode("Size_Id")) Then
                                    row_barcode("Size_Id") = size_id
                                    row.EndEdit()
                                    dt.AcceptChanges()
                                End If
                            Next
                        ElseIf row("action") = 2 Then
                            oclsSize.product_id = id
                            oclsSize.Size_id = row("Size_Id")
                            oclsSize.Name = row("Name")
                            oclsSize.Size = row("Size")
                            oclsSize.Unit_id = row("Unit_Id")
                            oclsSize.is_active = IIf(row("Active") = 1, 1, 0)
                            oclsSize.click_and_collect = IIf(row("click_and_collect") = 1, 1, 0)
                            oclsSize.deliver = IIf(row("deliver") = 1, 1, 0)
                            oclsSize.Order_at_table = IIf(row("Order_at_table") = 1, 1, 0)

                            oclsSize.Price_id = row("Price_Id")

                            oclsSize.Price = Val(row("Price"))
                            'oclsSize.Location_Id = row("Location_Id")
                            If Session("product_id") = Nothing Then
                                oclsSize.Location_Id = ddLocation.SelectedValue
                            Else
                                oclsSize.Location_Id = row("Location_Id")
                            End If
                            oclsSize.Actual_Price = Val(row("Actual_Price"))
                            oclsSize.Tax = Val(row("Tax"))
                            oclsSize.Tax_id = row("Tax_id")
                            '------ tax2 added on 13/04/2022-----------
                            oclsSize.Tax2 = Val(row("Tax2"))
                            oclsSize.Tax_id2 = row("Tax_id2")
                            oclsSize.sorting_no = row("sorting_no")

                            oclsSize.login_id = Val(Session("login_id"))
                            oclsSize.cmp_id = Val(Session("cmp_id"))
                            oclsSize.Ip_address = Request.UserHostAddress
                            oclsSize.action = row("action")

                            '------Change 01/11/2021----------

                            'If ddPrices.SelectedIndex <> 0 Then
                            '    oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            'Else
                            '    oclsSize.Product_Price_Id = 0
                            'End If


                            If ddPrices.SelectedIndex <> 0 And row("Product_Price_Id") = ddPrices.SelectedValue Then
                                oclsSize.Product_Price_Id = ddPrices.SelectedValue
                            Else
                                oclsSize.Product_Price_Id = Val(row("Product_Price_Id"))
                            End If
                            '------01/11/2021----------


                            oclsSize.InsUpdDel_SizeNPrice()

                        End If
                        'End If
                    Catch ex As Exception
                        LogHelper.Error("Product_Master:for 2:" + ex.Message)
                    End Try
                Next
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:InsertSize:" + ex.Message)
        End Try
    End Sub
    Protected Sub Insert_Cost(ByVal id As Integer)
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("Size_Id"), New DataColumn("ref_size_id")})
            Dim size_id As Integer

            Dim dt As DataTable = DirectCast(ViewState("View_Cost"), DataTable)

            Dim price As Decimal
            Dim per As Decimal
            Dim Actual As Decimal
            Dim amt As Decimal

            For Each row As DataRow In dt.Rows

                oclsTax.Tax_id = row("Tax_id").ToString
                If Val(row("Tax_id").ToString) > 0 Then
                    oclsTax.cmp_id = Val(Session("cmp_id"))
                    Dim dtTax As DataTable = oclsTax.Select()
                    Dim hvalue As Decimal
                    Dim hmode As String

                    If dtTax.Rows.Count > 0 Then
                        hvalue = Convert.ToDecimal(dtTax.Rows(0)("Value").ToString)
                        hmode = dtTax.Rows(0)("Mode").ToString
                    End If
                    price = Val(row("Price"))
                    'Val(row("Price"))
                    Dim t As Decimal
                    If hmode = "%" Then
                        per = Val(hvalue)
                        t = 1 + (per / 100)

                        Actual = price / t

                    ElseIf hmode = "Amt" Then
                        amt = Val(hvalue)
                        Actual = price - amt
                    Else
                        Actual = price
                    End If

                    row("Actual_Price") = Actual
                    row("Tax") = price - Actual

                    row.EndEdit()
                    dt.AcceptChanges()
                End If
            Next

            If Not Session("copy_product_id") = Nothing Then
                Dim stArr As String() = Session("copy_product_Location_Id").ToString().Split("#")

                'For i As Integer = 0 To stArr.Length - 1

                For Each row As DataRow In dt.Rows
                    'If Val(row("Tax_id").ToString) > 0 Then
                    If row("action") = 1 Then
                        oclsSize.product_id = id
                        If Not Session("copy_product_id") = Nothing Then
                            oclsSize.Size_id = Nothing
                        Else
                            oclsSize.Size_id = row("Size_Id")
                        End If
                        oclsSize.Name = row("Name")
                        oclsSize.Size = row("Size")
                        oclsSize.Unit_id = row("Unit_Id")
                        oclsSize.is_active = IIf(row("Active") = 1, 1, 0)

                        If Not Session("copy_product_id") = Nothing Then
                            oclsSize.Price_id = Nothing
                        Else
                            oclsSize.Price_id = row("Price_Id")
                        End If

                        If Not IsDBNull(row("Price")) Then
                            oclsSize.Price = row("Price")
                        Else
                            'If row("Price") = "" Then
                            oclsSize.Price = 0
                            'End If
                        End If
                        oclsSize.Location_Id = Val(row("Location_Id"))  'Val(stArr(i).ToString)  '----09/11/2022---------
                        oclsSize.Actual_Price = Val(row("Actual_Price"))
                        oclsSize.Tax = Val(row("Tax"))
                        oclsSize.Tax_id = row("Tax_id")
                        '------ tax2 added on 13/04/2022-----------
                        'oclsSize.Tax2 = Val(row("Tax2"))
                        'oclsSize.Tax_id2 = row("Tax_id2")

                        oclsSize.login_id = Val(Session("login_id"))
                        oclsSize.cmp_id = Val(Session("cmp_id"))
                        oclsSize.Ip_address = Request.UserHostAddress
                        oclsSize.action = row("action")
                        size_id = oclsSize.InsUpdDel_SizeNCost()

                    ElseIf row("action") = 2 Then

                        oclsSize.product_id = id
                        oclsSize.Size_id = row("Size_Id")
                        oclsSize.Name = row("Name")
                        oclsSize.Size = row("Size")
                        oclsSize.Unit_id = row("Unit_Id")
                        oclsSize.is_active = IIf(row("Active") = 1, 1, 0)
                        oclsSize.Price_id = row("Price_Id")
                        oclsSize.Price = Val(row("Price"))
                        If Session("product_id") = Nothing Then
                            oclsSize.Location_Id = ddlocation2.SelectedValue
                        Else
                            oclsSize.Location_Id = row("Location_Id")
                        End If
                        oclsSize.Actual_Price = Val(row("Actual_Price"))
                        oclsSize.Tax = Val(row("Tax"))
                        oclsSize.Tax_id = row("Tax_id")
                        '------ tax2 added on 13/04/2022-----------
                        'oclsSize.Tax2 = Val(row("Tax2"))
                        'oclsSize.Tax_id2 = row("Tax_id2")

                        oclsSize.login_id = Val(Session("login_id"))
                        oclsSize.cmp_id = Val(Session("cmp_id"))
                        oclsSize.Ip_address = Request.UserHostAddress
                        oclsSize.action = row("action")

                        oclsSize.InsUpdDel_SizeNCost()

                    End If
                Next
                ' Next
            Else
                For Each row As DataRow In dt.Rows
                    'If Val(row("Tax_id").ToString) > 0 Then
                    If row("action") = 1 Then
                        oclsSize.product_id = id
                        If Not Session("copy_product_id") = Nothing Then
                            oclsSize.Size_id = Nothing
                        Else
                            oclsSize.Size_id = row("Size_Id")
                        End If
                        oclsSize.Name = row("Name")
                        oclsSize.Size = row("Size")
                        oclsSize.Unit_id = row("Unit_Id")
                        oclsSize.is_active = IIf(row("Active") = 1, 1, 0)

                        If Not Session("copy_product_id") = Nothing Then
                            oclsSize.Price_id = Nothing
                        Else
                            oclsSize.Price_id = row("Price_Id")
                        End If

                        If Not IsDBNull(row("Price")) Then
                            oclsSize.Price = row("Price")
                        Else
                            'If row("Price") = "" Then
                            oclsSize.Price = 0
                            'End If
                        End If


                        If Session("product_id") = Nothing And Session("copy_product_id") = Nothing Then
                            oclsSize.Location_Id = Val(row("Location_Id")) 'ddlocation2.SelectedValue
                        ElseIf Not IsDBNull(row("Location_Id")) Then
                            oclsSize.Location_Id = row("Location_Id")
                        Else
                            oclsSize.Location_Id = 0
                        End If

                        oclsSize.Actual_Price = Val(row("Actual_Price"))
                        oclsSize.Tax = Val(row("Tax"))
                        oclsSize.Tax_id = row("Tax_id")
                        '------ tax2 added on 13/04/2022-----------
                        'oclsSize.Tax2 = Val(row("Tax2"))
                        'oclsSize.Tax_id2 = row("Tax_id2")

                        oclsSize.login_id = Val(Session("login_id"))
                        oclsSize.cmp_id = Val(Session("cmp_id"))
                        oclsSize.Ip_address = Request.UserHostAddress
                        oclsSize.action = row("action")

                        size_id = oclsSize.InsUpdDel_SizeNCost()

                    ElseIf row("action") = 2 Then
                        oclsSize.product_id = id
                        oclsSize.Size_id = row("Size_Id")
                        oclsSize.Name = row("Name")
                        oclsSize.Size = row("Size")
                        oclsSize.Unit_id = row("Unit_Id")
                        oclsSize.is_active = IIf(row("Active") = 1, 1, 0)

                        oclsSize.Price_id = row("Price_Id")
                        oclsSize.Price = Val(row("Price"))
                        If Session("product_id") = Nothing Then
                            oclsSize.Location_Id = ddlocation2.SelectedValue
                        Else
                            oclsSize.Location_Id = row("Location_Id")
                        End If
                        oclsSize.Actual_Price = Val(row("Actual_Price"))
                        oclsSize.Tax = Val(row("Tax"))
                        oclsSize.Tax_id = row("Tax_id")
                        '------ tax2 added on 13/04/2022-----------
                        'oclsSize.Tax2 = Val(row("Tax2"))
                        'oclsSize.Tax_id2 = row("Tax_id2")

                        oclsSize.login_id = Val(Session("login_id"))
                        oclsSize.cmp_id = Val(Session("cmp_id"))
                        oclsSize.Ip_address = Request.UserHostAddress
                        oclsSize.action = row("action")

                        oclsSize.InsUpdDel_SizeNCost()

                    End If
                    'End If
                Next
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:InsertCost:" + ex.Message)
        End Try
    End Sub
    Protected Sub Insert_Barcode(ByVal id As Integer)
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)

            For Each dr As DataRow In dt.Rows

                If dr("is_saved") = 0 And dr("action") = 1 Then
                    oclsBarcodeSize.Barcode_Size_id = dr("Barcode_Size_id")
                    oclsBarcodeSize.product_id = id
                    oclsBarcodeSize.Size_Id = dr("Size_Id")
                    oclsBarcodeSize.Barcode = dr("Barcode_Size")
                    oclsBarcodeSize.login_id = Val(Session("login_id"))
                    oclsBarcodeSize.cmp_id = Val(Session("cmp_id"))
                    oclsBarcodeSize.Ip_address = Request.UserHostAddress
                    oclsBarcodeSize.is_active = 1
                    oclsBarcodeSize.insert()
                ElseIf dr("is_saved") = 1 And dr("action") = 1 Then
                    oclsBarcodeSize.Barcode_Size_id = dr("Barcode_Size_id")
                    oclsBarcodeSize.product_id = id
                    oclsBarcodeSize.Size_Id = dr("Size_Id")
                    oclsBarcodeSize.Barcode = dr("Barcode_Size")
                    oclsBarcodeSize.login_id = Val(Session("login_id"))
                    oclsBarcodeSize.cmp_id = Val(Session("cmp_id"))
                    oclsBarcodeSize.Ip_address = Request.UserHostAddress
                    oclsBarcodeSize.is_active = 1
                    oclsBarcodeSize.Update()

                ElseIf dr("is_saved") = 1 And dr("action") = 2 Then

                    oclsBarcodeSize.Barcode_Size_id = dr("Barcode_Size_id")
                    oclsBarcodeSize.Size_Id = 0
                    oclsBarcodeSize.Barcode = ""
                    oclsBarcodeSize.product_id = 0
                    oclsBarcodeSize.Ip_address = ""
                    oclsBarcodeSize.cmp_id = Val(Session("cmp_id"))
                    oclsBarcodeSize.login_id = 0
                    oclsBarcodeSize.Tran_Type = "D"
                    oclsBarcodeSize.is_active = 0
                    oclsBarcodeSize.Delete()
                End If
            Next
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:Insert_Barcode:" + ex.Message)
        End Try

    End Sub

    Protected Sub Insert_ProductIngredient(ByVal id As Integer)
        Try

            oclsProductIngredient.product_id = id
            oclsProductIngredient.cmp_id = Val(Session("cmp_id"))
            oclsProductIngredient.Size_id = 0
            oclsProductIngredient.DeleteProductIngredient_By_Product()

            For Each item As GridDataItem In rdIngredient.Items

                oclsProductIngredient.product_id = id
                oclsProductIngredient.Ingredient_id = Val(CType(item.FindControl("hdnProductSize_Id"), HiddenField).Value)
                oclsProductIngredient.Size_id = 0
                oclsProductIngredient.cmp_id = Val(Session("cmp_id"))
                oclsProductIngredient.Ip_address = Request.UserHostAddress
                oclsProductIngredient.login_id = Val(Session("login_id"))
                oclsProductIngredient.Price = 0
                oclsProductIngredient.Qty = Val(CType(item.FindControl("txtIngredientQtyLocation"), TextBox).Text)
                oclsProductIngredient.Insert()

            Next

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:Insert_ProductIngredient:" + ex.Message)
        End Try

    End Sub
    Protected Sub Insert_Condiment(ByVal id As Integer)
        Try

            oclsProductCondiment.product_id = id
            oclsProductCondiment.cmp_id = Val(Session("cmp_id"))
            oclsProductCondiment.Condiment_Id = 0
            oclsProductCondiment.DeleteProductCondiment_By_Product()

            For Each item As GridDataItem In rdCondiment.Items

                oclsProductCondiment.product_id = Val(id)
                oclsProductCondiment.Condiment_Id = Val(CType(item.FindControl("hdnCondiment_Id"), HiddenField).Value)
                oclsProductCondiment.cmp_id = Val(Session("cmp_id"))
                oclsProductCondiment.ip_address = Request.UserHostAddress
                oclsProductCondiment.login_id = Val(Session("login_id"))
                oclsProductCondiment.Price = Val(CType(item.FindControl("txtCondimentPriceLocation"), TextBox).Text)
                oclsProductCondiment.Choice = Val(CType(item.FindControl("ddlchoices"), DropDownList).SelectedValue)
                oclsProductCondiment.min_select = Val(CType(item.FindControl("txtminselect"), TextBox).Text)
                oclsProductCondiment.max_select = Val(CType(item.FindControl("txtmaxselect"), TextBox).Text)
                oclsProductCondiment.Insert()

            Next

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:Insert_ProductIngredient:" + ex.Message)
        End Try

    End Sub
    Protected Sub insert_Allergy(ByVal id As Integer)
        Try
            Dim dtAllergy As DataTable = DirectCast(ViewState("Allergy_Data"), DataTable)
            oclsProduct.product_id = id
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.delete_Allergy_By_Product()

            For i As Integer = 0 To dtAllergy.Rows.Count - 1

                oclsProduct.product_id = id
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.allergy_id = Val(dtAllergy.Rows(i)("allergy_id").ToString())
                oclsProduct.Insert_Allergy_By_Product()

            Next
        Catch ex As Exception
            LogHelper.Error("Product_Master:insert_Allergy:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnAddNewSize_Click(sender As Object, e As EventArgs)
        Try
            Dim RecordInsert As Integer = 1

            If RecordInsert = 1 Then

                SaveRecordInDT_From_DynamicGrid()

                Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.product_id = 0
                oclsProduct.SingleRecord = 1
                Dim dtSize As DataTable = oclsProduct.Select_SizeNPrice_By_Product()

                If dt1.Rows.Count > 0 Then
                    dtSize.Rows(0)("row_Id") = dt1.Rows.Count + 1
                    dtSize.Rows(0)("Location_id") = Val(ddLocation.SelectedValue)
                    dtSize.Rows(0)("Product_Price_Id") = Val(ddPrices.SelectedValue)

                    Dim dv As DataView = dt1.DefaultView
                    dv.RowFilter = "Location_id = " & Val(ddLocation.SelectedValue) & "  "

                    If dv.ToTable.Rows.Count > 0 Then
                        Dim a As Integer = dt1.Compute("Max(sorting_no)", "sorting_no >= 0 and Location_id = " & Val(ddLocation.SelectedValue) & "")
                        dtSize.Rows(0)("sorting_no") = a + 1
                    Else
                        dtSize.Rows(0)("sorting_no") = 1
                    End If

                    '----------------Get MIN tax_id ------------
                    oclsTax.cmp_id = Val(Session("cmp_id"))
                    Dim tax_dt As DataTable = oclsTax.SelectTaxByCmp()
                    Dim a1 As Integer = 0
                    Dim a2 As Integer = 0
                    If tax_dt.Rows.Count > 0 Then
                        a1 = tax_dt.Rows(0)("Tax_id")
                    End If

                    If tax_dt.Rows.Count > 1 Then
                        a2 = tax_dt.Rows(1)("Tax_id")
                    End If
                    '----------------Get MIN tax_id ------------

                    dtSize.Rows(0)("Tax_id") = Val(a1)
                    dtSize.Rows(0)("Tax_id2") = Val(a2)

                End If

                dt1.Merge(dtSize)

                ViewState("View_Size") = dt1

                BindNewGrid()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Size And Unit can not be blank');", True)
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:btnAddNewSize_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub SaveRecordInDT()
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each item As GridViewRow In GridView1.Rows
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then


                        row("Name") = CType(item.FindControl("txtSizeName"), TextBox).Text
                        row("Size") = Val(CType(item.FindControl("txtsize"), TextBox).Text)
                        row("Product_Price_Id") = Val(ddPrices.SelectedValue.ToString())
                        row("Price") = Val(CType(item.FindControl("txtPriceLocation"), TextBox).Text)
                        row("active") = Val(IIf(CType(item.FindControl("chkIsActive"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))
                        row("click_and_collect") = Val(IIf(CType(item.FindControl("chkIsClickcollect"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))
                        row("deliver") = Val(IIf(CType(item.FindControl("chkIsDeliver"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))
                        row("Order_at_table") = Val(IIf(CType(item.FindControl("chkIsOrderattable"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))
                        row("Tax_id") = Val(CType(item.FindControl("ddTaxx"), DropDownList).SelectedValue)
                        '------ tax2 added on 13/04/2022-----------
                        row("Tax_id2") = Val(CType(item.FindControl("ddTaxx_2"), DropDownList).SelectedValue)

                        row("Location_Id") = Val(CType(item.FindControl("ddGvLocation"), DropDownList).SelectedValue)
                        If Val(row("Size")) <> 0 And Val(row("Unit_id")) <> 0 Then
                            row("Size_Unit") = row("Size").ToString & " " & CType(item.FindControl("ddUnit"), DropDownList).SelectedItem.Text.ToString() & "-" & ddLocation.SelectedItem.Text.ToString()
                        Else
                            row("Size_Unit") = ""
                        End If

                    End If

                    '------------------------29/03/2022------------------------
                    If Session("sID") IsNot Nothing And Session("sID") > 0 And Session("s_name") IsNot Nothing Then
                        If Val(row("Size_Id").ToString()) = Val(Session("sID").ToString()) And Session("s_name") <> "" Then

                            row("Name") = Session("s_name").ToString()

                        End If
                    End If

                    If Session("sID_1") IsNot Nothing And Session("sID_1") > 0 And Session("size") IsNot Nothing Then
                        If Val(row("Size_Id").ToString()) = Val(Session("sID_1").ToString()) And Val(Session("size")) <> 0 Then

                            row("Size") = Val(Session("size").ToString())

                        End If
                    End If

                    If Session("sID_2") IsNot Nothing And Session("sID_2") > 0 And Session("Unit") IsNot Nothing Then
                        If Val(row("Size_Id").ToString()) = Val(Session("sID_2").ToString()) And Val(Session("Unit")) <> 0 Then

                            row("Unit_id") = Val(Session("Unit"))

                        End If
                    End If

                    '------------------------29/03/2022------------------------

                    '--------------------added sorting no. 29/04/2022 ---------------
                    If Session("sID_3") IsNot Nothing And Session("sorting_no") IsNot Nothing Then
                        If Val(row("Size_Id").ToString()) = Val(Session("sID_3").ToString()) And Val(Session("sorting_no")) <> 0 Then

                            row("sorting_no") = Val(Session("sorting_no"))

                        End If
                    End If
                Next
            Next

            bind_drp_Size()

        Catch ex As Exception
            LogHelper.Error("Product_Master:SaveRecordInDT:" + ex.Message)
        End Try
    End Sub
    Protected Sub SaveIngredientRecordInDT()
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)
            For Each item As GridDataItem In rdIngredient.Items
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdnIngredientrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("product_id") = Val(CType(item.FindControl("hdnProductSize_Id"), HiddenField).Value)
                        row("Product") = CType(item.FindControl("radAutoIngredient"), TextBox).Text
                        row("Base_Size") = CType(item.FindControl("txtIngredientBaseSize"), TextBox).Text
                        row("Unit_id") = Val(CType(item.FindControl("ddIngredientUnit"), DropDownList).SelectedValue)
                        row("Unit") = CType(item.FindControl("ddIngredientUnit"), DropDownList).SelectedItem
                        row("Qty") = Val(CType(item.FindControl("txtIngredientQtyLocation"), TextBox).Text)
                    End If
                Next
            Next

            'bind_drp_Size()

        Catch ex As Exception
            LogHelper.Error("Product_Master:SaveIngredientRecordInDT:" + ex.Message)
        End Try
    End Sub

    Protected Sub SaveCondimentRecordInDT()
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("Condiment_Data"), DataTable)

            For Each item As GridDataItem In rdCondiment.Items
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdnCondimentrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("Condiment_Id") = Val(CType(item.FindControl("hdnCondiment_Id"), HiddenField).Value)
                        row("Condiment") = CType(item.FindControl("radAutoCondiment"), TextBox).Text
                        row("Base_Size") = CType(item.FindControl("txtIngredientBaseSize"), TextBox).Text
                        row("Price") = Val(CType(item.FindControl("txtCondimentPriceLocation"), TextBox).Text)
                    End If
                Next
            Next

        Catch ex As Exception
            LogHelper.Error("Product_Master:SaveIngredientRecordInDT:" + ex.Message)
        End Try
    End Sub

    Protected Sub txtsize_TextChanged(sender As Object, e As EventArgs)
        Try

            Dim size As Decimal = 0
            Dim sID_1 As Integer = 0
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each item As GridViewRow In GridView1.Rows
                For Each row As DataRow In dt1.Rows
                    If (CType(item.FindControl("txtSize"), TextBox).Text <> row("Size").ToString()) And CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

                        Dim dv As DataView = dt1.DefaultView
                        dv.RowFilter = " Unit_id = '" + CType(item.FindControl("ddUnit"), DropDownList).SelectedValue + "' AND Size = '" + CType(item.FindControl("txtSize"), TextBox).Text + "' AND Size_Id <> '" + CType(item.FindControl("hdnSize_Id"), HiddenField).Value + "' and Location_id = " & Val(ddLocation.SelectedValue) & " "

                        If dv.ToTable.Rows.Count > 0 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('This size and unit is already exists.');", True)

                            CType(item.FindControl("txtSize"), TextBox).Text = "0"
                            size = "0"

                        Else
                            size = CType(item.FindControl("txtSize"), TextBox).Text

                        End If

                        sID_1 = Val(CType(item.FindControl("hdnSize_Id"), HiddenField).Value)

                    End If
                Next
            Next


            Session("size") = size
            Session("sID_1") = sID_1

            SaveRecordInDT()

        Catch ex As Exception
            LogHelper.Error("Product_Master:txtsize_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtSizeName_TextChanged(sender As Object, e As EventArgs)
        Try

            'Dim confirmValue As String = ""
            'confirmValue = Request.Form("confirm_value").ToString()
            'If (confirmValue <> "" And confirmValue <> "No") Then

            'CheckForSpecialChar()
            Dim s_name As String = ""
            Dim sID As Integer = 0
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each item As GridViewRow In GridView1.Rows
                For Each row As DataRow In dt1.Rows
                    If (CType(item.FindControl("txtSizeName"), TextBox).Text <> row("Name").ToString()) And CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

                        s_name = CType(item.FindControl("txtSizeName"), TextBox).Text

                        sID = Val(CType(item.FindControl("hdnSize_Id"), HiddenField).Value)

                    End If
                Next
                CType(item.FindControl("txtsize"), TextBox).Focus()

            Next
            Session("s_name") = s_name
            Session("sID") = sID
            SaveRecordInDT()

            'End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:txtSizeName_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txtPriceLocation_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:txtPriceLocation_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub chkIsActive_CheckedChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:chkIsActive_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Private Sub rdIngredient_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rdIngredient.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Ingredient(id)
                BindGrid_Delete()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdIngredient_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Private Sub rdIngredient_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rdIngredient.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                oclsBind.BindUnit(CType(e.Item.FindControl("ddIngredientUnit"), DropDownList), Val(Session("cmp_id")))
                If Session("product_id") = Nothing Then
                    Dim dt1 As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)

                    For Each row As DataRow In dt1.Rows
                        If CType(e.Item.FindControl("hdnIngredientrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                            CType(e.Item.FindControl("ddIngredientUnit"), DropDownList).SelectedValue = row("Unit_id").ToString()
                            CType(e.Item.FindControl("ddIngredientUnit"), DropDownList).Enabled = False
                        End If

                    Next
                Else
                    Dim dt1 As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)

                    For Each row As DataRow In dt1.Rows
                        If CType(e.Item.FindControl("hdnIngredientrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

                            CType(e.Item.FindControl("ddIngredientUnit"), DropDownList).SelectedValue = row("Unit_id").ToString()
                            CType(e.Item.FindControl("ddIngredientUnit"), DropDownList).Enabled = False

                        End If
                    Next

                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdProduct_ItemDataBound:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtIngredientsize_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveIngredientRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:txtIngredientsize_TextChanged:" + ex.Message)
        End Try
    End Sub
    'Protected Sub radAutoIngredient_TextChanged(sender As Object, e As AutoCompleteTextEventArgs)
    '    Try
    '        For Each item As GridDataItem In rdIngredient.Items

    '            For Each entry As AutoCompleteBoxEntry In CType(item.FindControl("radAutoIngredient"), RadAutoCompleteBox).Entries

    '                Dim value As String = entry.Value
    '                Dim stArr As String() = value.Split("#")
    '                Dim Size_Id As Integer
    '                Dim s As String

    '                Dim name As String = entry.Text
    '                Dim stArrname As String() = name.Split(" ")

    '                For i As Integer = 0 To stArr.Length - 1

    '                    s = stArr(i)

    '                    CType(item.FindControl("hdnProductSize_Id"), HiddenField).Value = Val(entry.Value)

    '                    oclsProduct.cmp_id = Val(Session("cmp_id"))
    '                    oclsProduct.product_id = Val(s)
    '                    Dim dtUnit As DataTable = oclsProduct.GetUnit_By_Product()

    '                    If dtUnit.Rows.Count > 0 Then
    '                        For Each row As DataRow In dtUnit.Rows

    '                            oclsBind.BindUnit(CType(item.FindControl("ddIngredientUnit"), DropDownList), Val(Session("cmp_id")))

    '                            CType(item.FindControl("ddIngredientUnit"), DropDownList).SelectedValue = Val(row("Unit_id"))
    '                        Next
    '                    End If
    '                    Exit For
    '                Next

    '                For i As Integer = 0 To stArrname.Length - 1
    '                    If stArrname.Count > 1 Then
    '                        If stArrname(i + 1) IsNot "" Then
    '                            Dim productname As String = stArrname(i)
    '                            Dim Sizename As String = stArrname(i + 1)

    '                            oclsProductIngredient.cmp_id = Val(Session("cmp_id"))
    '                            oclsProductIngredient.Ingredient_id = Val(s)
    '                            oclsProductIngredient.Size = Sizename
    '                            Dim dt As DataTable = oclsProductIngredient.GetSize_By_Ingredient()

    '                            If dt.Rows.Count > 0 Then
    '                                For Each row As DataRow In dt.Rows
    '                                    Size_Id = Val(row("Size_Id"))
    '                                Next
    '                            End If

    '                            CType(item.FindControl("radAutoIngredient"), RadAutoCompleteBox).Entries.Clear()
    '                            CType(item.FindControl("radAutoIngredient"), RadAutoCompleteBox).Entries.Add(New AutoCompleteBoxEntry(productname, Val(entry.Value)))
    '                            CType(item.FindControl("txtIngredientsize"), TextBox).Text = Sizename.ToString()
    '                            CType(item.FindControl("hdnIngredientSize_Id"), HiddenField).Value = Val(Size_Id)
    '                        End If
    '                    End If

    '                    Exit For
    '                Next
    '            Next


    '        Next
    '    Catch ex As Exception
    '        LogHelper.Error("Product_Master: radAutoIngredient_TextChanged:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnAddNewIngredient_Click(sender As Object, e As EventArgs)
        Try
            Dim dtingredient As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)

            If dtingredient Is Nothing Then

            ElseIf dtingredient.Rows.Count = 0 Then
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredient()

                If dt.Rows.Count > 0 Then
                    rdcopyProduct.DataSource = dt
                    rdcopyProduct.DataBind()
                Else
                    rdcopyProduct.DataSource = String.Empty
                    rdcopyProduct.DataBind()
                End If
            End If
            up_Pro_Ingredient.Update()


            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)


        Catch ex As Exception
            LogHelper.Error("Product_Master:btnAddNewSize_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtIngredientPriceLocation_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveIngredientRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:txtIngredientPriceLocation_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnAddNewCondiment_Click(sender As Object, e As EventArgs)
        Try
            Dim dtingredient As DataTable = DirectCast(ViewState("Condiment_Data"), DataTable)

            If dtingredient Is Nothing Then

            ElseIf dtingredient.Rows.Count = 0 Then
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                Dim dt As DataTable = oclsProduct.Get_M_Condiment_ForProduct()

                If dt.Rows.Count > 0 Then
                    RadCondiment.DataSource = dt
                    RadCondiment.DataBind()
                Else
                    RadCondiment.DataSource = String.Empty
                    RadCondiment.DataBind()
                End If
            End If
            up_Pro_Condiment.Update()
            Dim script As String = "function f(){$find(""" + rwCondimentDetail.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)


        Catch ex As Exception
            LogHelper.Error("Product_Master:btnAddNewSize_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtCondimentPriceLocation_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveCondimentRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:txtCondimentPriceLocation_TextChanged:" + ex.Message)
        End Try
    End Sub
    Private Sub rdCondiment_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rdCondiment.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Condiment(id)
                BindGrid_DeleteCondiment()

            ElseIf e.CommandName = "EditCondi" Then
                Session("Condiment_id") = Convert.ToInt32(e.CommandArgument)
                Session("is_fromProduct") = 1
                Dim url As String = "Condiment_Master.aspx"
                ScriptManager.RegisterClientScriptBlock(Me, GetType(String), "", "openWindow('" + url + "');", True)
                BindGrid_DeleteCondiment()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdCondiment_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Private Sub rdsallergy_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rdSAllergy.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_ALlergy(id)
                BindGrid_DeleteAllergy()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdCondiment_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Private Sub rdCondiment_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rdCondiment.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                CType(e.Item.FindControl("ddlchoices"), DropDownList).SelectedValue = CType(e.Item.FindControl("hfieldChoice"), HiddenField).Value.ToString()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdCondiment_ItemDataBound:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddIngredientSize_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            SaveIngredientRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:ddIngredientSize_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txtIngredientQtyLocation_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveIngredientRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:txtIngredientQtyLocation_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnAddViewCondiment_Click(sender As Object, e As EventArgs)
        Try

            Dim url As String = "Condiment_List.aspx"
            ScriptManager.RegisterClientScriptBlock(Me, GetType(String), "", "openWindow('" + url + "');", True)
        Catch ex As Exception
            LogHelper.Error("Product_Master:btnAddViewCondiment_Click:" + ex.Message)
        End Try
    End Sub


    Private Sub btn_ProductIngredient_Click(sender As Object, e As EventArgs) Handles btn_ProductIngredient.Click
        Try
            Dim dt1 As New DataTable
            dt1.Columns.Add("product_id", GetType(Integer))

            For Each item As GridItem In rdcopyProduct.MasterTableView.Items
                Dim value As String
                Dim dataitem As GridDataItem = CType(item, GridDataItem)
                Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)

                If checkBox.Checked Then
                    value = dataitem.GetDataKeyValue("product_id").ToString()
                    Session("product_For_Ingredient_id") = value.ToString()
                    Dim row As DataRow = dt1.NewRow()
                    row("product_id") = dataitem.GetDataKeyValue("product_id").ToString()
                    dt1.Rows.Add(row)
                End If
            Next

            ViewState("Product_Data") = dt1

            If Session("product_For_Ingredient_id") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else

                If Session("product_id") = Nothing Then
                    Dim Pro_Id As String

                    Dim AllProduct_id As List(Of String) = New List(Of String)

                    For Each row As DataRow In dt1.Rows
                        AllProduct_id.Add(row("product_id").ToString())
                    Next

                    Pro_Id = String.Join(",", AllProduct_id.ToArray())

                    oclsProduct.Pro_Id = Pro_Id
                    oclsProduct.cmp_id = Val(Session("cmp_id"))
                    Dim dt As DataTable = oclsProduct.Get_Product_For_Ingredient()

                    Dim dtingredient As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)

                    If dtingredient IsNot Nothing Then
                        Dim rows_to_remove As New List(Of DataRow)()

                        For Each row1 As DataRow In dtingredient.Rows
                            For Each row2 As DataRow In dt.Rows
                                If row1("product_id").ToString() = row2("product_id").ToString() Then
                                    rows_to_remove.Add(row2)
                                End If

                            Next
                        Next

                        For Each row As DataRow In rows_to_remove
                            dt.Rows.Remove(row)
                            dt.AcceptChanges()
                        Next

                        dtingredient.Merge(dt, True, MissingSchemaAction.Ignore)
                        ViewState("Ingredient_Data") = dtingredient

                        Dim dv As DataView = dtingredient.DefaultView

                        ViewState("Ingredient_Data") = dtingredient

                        rdIngredient.DataSource = dv
                        rdIngredient.DataBind()
                        upWall.Update()
                    Else
                        Dim dv As DataView = dt.DefaultView
                        ViewState("Ingredient_Data") = dt
                        rdIngredient.DataSource = dv
                        rdIngredient.DataBind()
                        upWall.Update()
                    End If

                    Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                Else
                    Dim Pro_Id As String
                    Dim AllProduct_id As List(Of String) = New List(Of String)
                    For Each row As DataRow In dt1.Rows
                        AllProduct_id.Add(row("product_id").ToString())
                    Next

                    Dim dtingredient As DataTable = DirectCast(ViewState("Ingredient_Data"), DataTable)
                    For Each row As DataRow In dtingredient.Rows
                        AllProduct_id.Add(row("product_id").ToString())
                    Next

                    Pro_Id = String.Join(",", AllProduct_id.ToArray())

                    oclsProduct.Pro_Id = Pro_Id
                    oclsProduct.cmp_id = Val(Session("cmp_id"))
                    Dim dt As DataTable = oclsProduct.Get_Product_For_Ingredient()

                    Dim rows_to_remove As New List(Of DataRow)()

                    For Each row1 As DataRow In dtingredient.Rows
                        For Each row2 As DataRow In dt.Rows
                            If row1("product_id").ToString() = row2("product_id").ToString() Then
                                rows_to_remove.Add(row2)
                            End If
                        Next
                    Next

                    For Each row As DataRow In rows_to_remove
                        dt.Rows.Remove(row)
                        dt.AcceptChanges()
                    Next

                    dtingredient.Merge(dt, True, MissingSchemaAction.Ignore)
                    ViewState("Ingredient_Data") = dtingredient

                    Dim dv As DataView = dtingredient.DefaultView

                    ViewState("Ingredient_Data") = dtingredient

                    rdIngredient.DataSource = dv
                    rdIngredient.DataBind()
                    upWall.Update()

                    Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                End If


            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub btn_ProductCondiment_Click(sender As Object, e As EventArgs) Handles btn_ProductCondiment.Click
        Try
            Dim dt1 As New DataTable

            dt1.Columns.Add("Condiment_id", GetType(Integer))

            For Each item As GridItem In RadCondiment.MasterTableView.Items
                Dim value As String
                Dim dataitem As GridDataItem = CType(item, GridDataItem)
                Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)

                If checkBox.Checked Then
                    value = dataitem.GetDataKeyValue("Condiment_Id").ToString()
                    Session("productForCondiment_Id") = value.ToString()

                    Dim row As DataRow = dt1.NewRow()
                    row("Condiment_id") = dataitem.GetDataKeyValue("Condiment_Id").ToString()

                    dt1.Rows.Add(row)
                End If
            Next

            If Session("productForCondiment_Id") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else
                If Session("product_id") = Nothing Then
                    Dim Condiment_Id As String
                    Dim AllCondiment_id As List(Of String) = New List(Of String)

                    For Each row As DataRow In dt1.Rows
                        AllCondiment_id.Add(row("Condiment_id").ToString())
                    Next

                    Condiment_Id = String.Join(",", AllCondiment_id.ToArray())

                    oclsProduct.Pro_Id = Condiment_Id
                    oclsProduct.cmp_id = Val(Session("cmp_id"))
                    Dim dt As DataTable = oclsProduct.Get_Product_For_Condiment()
                    '-------06/07/2022------------
                    ViewState("check") = dt

                    Dim dtingredient As DataTable = DirectCast(ViewState("Condiment_Data"), DataTable)

                    If dtingredient IsNot Nothing Then
                        Dim rows_to_remove As New List(Of DataRow)()
                        For Each row1 As DataRow In dtingredient.Rows
                            For Each row2 As DataRow In dt.Rows
                                If row1("Condiment_id").ToString() = row2("Condiment_id").ToString() Then
                                    rows_to_remove.Add(row2)
                                End If

                            Next
                        Next

                        For Each row As DataRow In rows_to_remove
                            dt.Rows.Remove(row)
                            dt.AcceptChanges()
                        Next

                        dtingredient.Merge(dt, True, MissingSchemaAction.Ignore)
                        ViewState("Condiment_Data") = dtingredient

                        Dim dv As DataView = dtingredient.DefaultView

                        ViewState("Condiment_Data") = dtingredient

                        rdCondiment.DataSource = dv
                        rdCondiment.DataBind()
                        up_Condiment.Update()

                    Else

                        Dim dv As DataView = dt.DefaultView

                        ViewState("Condiment_Data") = dt

                        rdCondiment.DataSource = dv
                        rdCondiment.DataBind()
                        up_Condiment.Update()
                    End If

                    Dim script As String = "function f(){$find(""" + rwCondimentDetail.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                Else
                    Dim Condiment_Id As String

                    Dim AllCondiment_id As List(Of String) = New List(Of String)

                    For Each row As DataRow In dt1.Rows
                        AllCondiment_id.Add(row("Condiment_id").ToString())
                    Next

                    Dim dtingredient As DataTable = DirectCast(ViewState("Condiment_Data"), DataTable)
                    '-------06/07/2022------------
                    ViewState("check") = ViewState("Condiment_Data")

                    For Each row As DataRow In dtingredient.Rows
                        AllCondiment_id.Add(row("Condiment_id").ToString())
                    Next

                    Condiment_Id = String.Join(",", AllCondiment_id.ToArray())

                    oclsProduct.Pro_Id = Condiment_Id
                    oclsProduct.cmp_id = Val(Session("cmp_id"))
                    Dim dt As DataTable = oclsProduct.Get_Product_For_Condiment()
                    'Dim dv As DataView = dt.DefaultView

                    Dim rows_to_remove As New List(Of DataRow)()

                    For Each row1 As DataRow In dtingredient.Rows
                        For Each row2 As DataRow In dt.Rows
                            If row1("Condiment_id").ToString() = row2("Condiment_id").ToString() Then

                                rows_to_remove.Add(row2)
                            End If

                        Next
                    Next

                    For Each row As DataRow In rows_to_remove
                        dt.Rows.Remove(row)
                        dt.AcceptChanges()
                    Next
                    '--------------------06/07/2022------------------
                    Dim dv1 As DataView = dtingredient.DefaultView
                    dv1.RowFilter = "Choice = '1'"
                    If dv1.ToTable.Rows.Count > 0 Then

                        For Each row As DataRow In dt.Rows

                            row("min_select") = dv1.ToTable.Rows(0)("min_select")
                            row("max_select") = dv1.ToTable.Rows(0)("max_select")
                            dt.AcceptChanges()
                        Next
                    Else
                        For Each row As DataRow In dt.Rows

                            row("min_select") = 0
                            row("max_select") = 0
                            dt.AcceptChanges()
                        Next
                    End If

                    dv1.RowFilter = ""
                    '-----------------------06/07/2022---------------------

                    dtingredient.Merge(dt, True, MissingSchemaAction.Ignore)
                    ViewState("Ingredient_Data") = dtingredient

                    Dim dv As DataView = dtingredient.DefaultView

                    ViewState("Condiment_Data") = dtingredient

                    rdCondiment.DataSource = dv
                    rdCondiment.DataBind()
                    up_Condiment.Update()

                    Dim script As String = "function f(){$find(""" + rwCondimentDetail.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:btn_ProductCondiment_Click:" + ex.Message)
        End Try
    End Sub
    ' Search for ingredient
    Protected Sub rdcopyProduct_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdcopyProduct.NeedDataSource
        Try
            ReBindGrid()
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdcopyProduct_NeedDataSource:" + ex.Message)
        End Try
    End Sub
    Public Sub ReBindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredient()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
            Else
                rdcopyProduct.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:ReBindGrid():" + ex.Message)
        End Try
    End Sub
    ' Search for Condiment
    Private Sub RadCondiment_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadCondiment.NeedDataSource
        Try
            ReBindGrid_Condiment()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ReBindGrid_Condiment()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.Get_M_Condiment_ForProduct()

            If dt.Rows.Count > 0 Then
                RadCondiment.DataSource = dt
            Else
                RadCondiment.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:ReBindGrid():" + ex.Message)
        End Try
    End Sub
    Private Sub btn_ProductIngrdientCancel_Click(sender As Object, e As EventArgs) Handles btn_ProductIngrdientCancel.Click
        Try
            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btn_ProductCondimentCancel_Click(sender As Object, e As EventArgs) Handles btn_ProductCondimentCancel.Click
        Try
            Dim script As String = "function f(){$find(""" + rwCondimentDetail.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnNewSizeCost_Click(sender As Object, e As EventArgs)
        Try
            Dim RecordInsert As Integer = 1

            If RecordInsert = 1 Then
                SaveRecordInDTCostFrom_GV()

                Dim dt1 As DataTable = DirectCast(ViewState("View_Cost"), DataTable)

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.product_id = 0
                oclsProduct.SingleRecord = 1
                Dim dtSize As DataTable = oclsProduct.Select_SizeNCost_By_Product()

                If dt1.Rows.Count > 0 Then
                    dtSize.Rows(0)("row_Id") = dt1.Rows.Count + 1
                End If

                'dtSize.Rows(0)("Location_Id") = Val(ddlocation2.SelectedValue)


                dt1.Merge(dtSize)

                'BindGrid()
                BindNewGridCost()

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Size And Unit can not be blank');", True)
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:btnAddNewSize_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtSizeName2_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDTCost()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtsize2_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDTCost()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtPriceLocation2_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDTCost()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SaveRecordInDTCost()
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("View_Cost"), DataTable)

            For Each item As GridViewRow In GridView2.Rows
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("Name") = CType(item.FindControl("txtSizeName2"), TextBox).Text.ToString()
                        row("Size_Id") = Val(CType(item.FindControl("hdnSize_Id"), HiddenField).Value)
                        row("Size") = Val(CType(item.FindControl("txtsize2"), TextBox).Text.ToString())
                        row("Unit_id") = Val(CType(item.FindControl("ddUnit2"), DropDownList).SelectedValue)
                        row("Price_Id") = 0
                        row("Price") = Val(CType(item.FindControl("txtPriceLocation2"), TextBox).Text.ToString())
                        row("active") = 1
                        row("Tax_id") = Val(CType(item.FindControl("ddTax2"), DropDownList).SelectedValue)
                        row("Location_Id") = Val(ddlocation2.SelectedValue)
                        If Val(row("Size")) <> 0 And Val(row("Unit_id")) <> 0 Then
                            row("Size_Unit") = row("Size").ToString & " " & CType(item.FindControl("ddUnit2"), DropDownList).SelectedItem.Text.ToString() & "-" & ddlocation2.SelectedItem.Text.ToString()
                        Else
                            row("Size_Unit") = ""
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            LogHelper.Error("Product_Master:SaveRecordInDT:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnAllergyAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dtAllergy As DataTable = DirectCast(ViewState("Allergy_Data"), DataTable)

            oclsAllergy.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsAllergy.SelectAll()

            If dt.Rows.Count > 0 Then
                rdAllergy.DataSource = dt
                rdAllergy.DataBind()
            Else
                rdAllergy.DataSource = String.Empty
                rdAllergy.DataBind()
            End If

            upAllergy.Update()
            Dim script As String = "function f(){$find(""" + rdwinAllergy.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyallergy", script, True)

        Catch ex As Exception
            LogHelper.Error("Product_Master:btnAllergyAdd_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancelAllergy_Click(sender As Object, e As EventArgs)
        Dim script As String = "function f(){$find(""" + rdwinAllergy.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyallergy", script, True)
    End Sub
    Protected Sub btnSaveAllergy_Click(sender As Object, e As EventArgs)
        Try
            Dim dtAllergy As DataTable = DirectCast(ViewState("Allergy_Data"), DataTable)

            For Each item As GridItem In rdAllergy.MasterTableView.Items
                Dim value As String
                Dim dataitem As GridDataItem = CType(item, GridDataItem)
                Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)

                If checkBox.Checked Then

                    Dim row As DataRow = dtAllergy.NewRow()
                    row("allergy_id") = TryCast(item, GridDataItem).GetDataKeyValue("Allergy_id").ToString()
                    row("cmp_id") = Val(Session("cmp_Id"))
                    row("product_id") = Val(Session("product_id"))
                    row("tran_id") = 0
                    row("name") = TryCast(item, GridDataItem).GetDataKeyValue("name").ToString()
                    dtAllergy.Rows.Add(row)
                End If
            Next

            dtAllergy.AcceptChanges()

            ViewState("Allergy_Data") = dtAllergy

            rdSAllergy.DataSource = dtAllergy
            rdSAllergy.DataBind()
            upallergyLIst.Update()

            Dim script As String = "function f(){$find(""" + rdwinAllergy.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyallergy", script, True)

        Catch ex As Exception
            LogHelper.Error("Product_Master:btnSaveAllergy_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                objclProdImg.id = id
                objclProdImg.delete_image()
                bindImage()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdCondiment_ItemCommand:" + ex.Message)
        End Try
    End Sub
    Protected Sub chkIsOrderattable_CheckedChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:chkIsOrderattable_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub chkIsDeliver_CheckedChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:chkIsDeliver_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub chkIsClickcollect_CheckedChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:chkIsClickcollect_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtminselect_TextChanged(sender As Object, e As EventArgs)
        Try

            Dim currentrow As GridTableCell = CType((CType(sender, TextBox)).Parent, GridTableCell)
            Dim txtminChangevalue As Integer = CType(currentrow.FindControl("txtminselect"), TextBox).Text
            Dim selectedDropdown As Integer = CType(currentrow.FindControl("ddlchoices"), DropDownList).SelectedValue
            For Each item As GridDataItem In rdCondiment.MasterTableView.Items
                Dim Dropdown As DropDownList = CType(item("ChoiceSelect").FindControl("ddlchoices"), DropDownList)
                Dim mintext As TextBox = CType(item("minselected").FindControl("txtminselect"), TextBox)

                If Dropdown.SelectedValue = selectedDropdown Then
                    mintext.Text = txtminChangevalue
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub txtmaxselect_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim currentrow As GridTableCell = CType((CType(sender, TextBox)).Parent, GridTableCell)
            Dim txtmaxChangevalue As Integer = CType(currentrow.FindControl("txtmaxselect"), TextBox).Text
            Dim selectedDropdown As Integer = CType(currentrow.FindControl("ddlchoices"), DropDownList).SelectedValue

            For Each item As GridDataItem In rdCondiment.MasterTableView.Items
                Dim Dropdown As DropDownList = CType(item("ChoiceSelect").FindControl("ddlchoices"), DropDownList)
                Dim maxtext As TextBox = CType(item("maxselected").FindControl("txtmaxselect"), TextBox)

                If Dropdown.SelectedValue = selectedDropdown Then
                    maxtext.Text = txtmaxChangevalue
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    'end
    Protected Sub BindGrid_From_Tab(ByVal id As Integer)
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView
            If id <> 0 Then
                dv.RowFilter = "action <> '2' AND Location_id = " + id.ToString()
            Else
                dv.RowFilter = "action <> '2'"
            End If
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then

                GridView1.DataSource = dv
                GridView1.DataBind()

                If dv.Count >= 4 Then
                    btnAddNewSize.Enabled = False
                Else
                    btnAddNewSize.Enabled = True
                End If
            Else
                GridView1.DataSource = String.Empty
                GridView1.DataBind()

                btnAddNewSize.Enabled = True
            End If

            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0
            Dim dtIngredient As DataTable = oclsProduct.Select_Ingredient_By_Product()
            Dim dvIngredient As DataView = dtIngredient.DefaultView

            ViewState("View_Ingredient") = dtIngredient
            ViewState("Ingredient_Data") = dtIngredient

            If dtIngredient.Rows.Count > 0 And dvIngredient.ToTable.Rows.Count > 0 Then
                rdIngredient.DataSource = dvIngredient
                rdIngredient.DataBind()
            End If

            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.SingleRecord = 0
            Dim dtCondiment As DataTable = oclsProduct.Select_Condiment_By_Product()
            Dim dvCondiment As DataView = dtCondiment.DefaultView

            ViewState("Condiment_Data") = dtCondiment

            If dtCondiment.Rows.Count > 0 And dvCondiment.ToTable.Rows.Count > 0 Then
                rdCondiment.DataSource = dvCondiment
                rdCondiment.DataBind()
            End If

            oclsProduct.product_id = Val(Session("product_id"))

            Dim dtAllergy As DataTable = oclsProduct.Select_Allergy_By_Product()
            Dim dvAllergy As DataView = dtAllergy.DefaultView
            ViewState("Allergy_Data") = dtAllergy
            If dtAllergy.Rows.Count > 0 And dvAllergy.ToTable.Rows.Count > 0 Then
                rdSAllergy.DataSource = dtAllergy
                rdSAllergy.DataBind()
            End If
            BindGridCost()
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid:" + ex.Message)
        End Try
    End Sub

    Public Class DynamicTemplate
        Implements System.Web.UI.ITemplate
        Private item As ListItemType

        Public Sub New(item As ListItemType)
            Me.item = item
        End Sub

        Public Sub InstantiateIn(container As Control) Implements ITemplate.InstantiateIn
            Throw New NotImplementedException()
        End Sub
    End Class

    Public Sub bind_Tabs(ByRef str As String)
        Try
            oclLoc.cmp_id = Val(Session("cmp_id"))
            Dim dt As New DataTable()
            dt = oclLoc.SelectLocationByCmp

            Dim cnt As Integer = 0
            Dim LocName As String = ""
            Dim LocId As Integer = 0

            If (dt.Rows.Count > 0) Then
                tbcDynamic = New AjaxControlToolkit.TabContainer
                'Dim dto As DataTable = DirectCast(ViewState("View_Size"), DataTable)

                For Each dtdtRow As DataRow In dt.Rows
                    LocName = dt.Rows(cnt)("name")
                    LocId = dt.Rows(cnt)("Location_Id")
                    Dim tbpnlProcessCategory As TabPanel = New TabPanel()
                    tbpnlProcessCategory.HeaderText = LocName
                    tbpnlProcessCategory.ID = "Tab" & LocId.ToString()

                    tbcDynamic.Tabs.Add(tbpnlProcessCategory)
                    'tbpnlProcessCategory.CssClass = "DynTabCSS"

                    Dim tb1 As Table = New Table()
                    Dim tr1 As TableRow = New TableRow()
                    Dim tc1 As TableCell = New TableCell()
                    Dim btnShowText As Label = New Label()

                    'btnShowText.Text = "LocationId_" & LocId & "_" & LocName
                    btnShowText.Text = LocName
                    btnShowText.ID = "btn" & LocId
                    'AddHandler btnShowText.Click, AddressOf Me.btnShowText_Click
                    tc1.Controls.Add(btnShowText)
                    Dim grd As GridView = New GridView()

                    grd.Visible = True
                    grd.AutoGenerateColumns = False
                    grd.ID = "gridView_Dynmc"
                    Dim dtM As DataTable = DirectCast(ViewState("View_Size"), DataTable)
                    Dim dv As DataView = dtM.DefaultView
                    If LocId <> 0 Then
                        If (str = "load") Then
                            dv.RowFilter = "action <> '2' AND Location_id = '0' "
                        Else
                            dv.RowFilter = "action <> '2' AND Location_id = " + LocId.ToString()
                        End If
                    Else
                        dv.RowFilter = "action <> '2'"
                    End If

                    If dtM.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then

                        For i As Integer = 0 To dtM.Columns.Count - 1
                            Try
                                Dim tempField As BoundField = New BoundField()
                                tempField.HeaderText = dtM.Columns(i).ColumnName.ToString()
                                tempField.DataField = dtM.Columns(i).ColumnName.ToString()
                                grd.Columns.Add(tempField)

                                'tempField.ItemTemplate = tempField

                                'Dim boundField As BoundField = New BoundField()
                                'boundField.DataField = dtM.Columns(i).ColumnName.ToString()
                                'boundField.HeaderText = dtM.Columns(i).ColumnName.ToString()
                                'grd.Columns.Add(boundField)
                            Catch ex As Exception
                                LogHelper.Error("Product_Master:loop:" + ex.Message)
                            End Try
                        Next

                        AddHandler grd.RowDataBound, AddressOf Me.grd_ItemDataBound
                        'AddHandler grd.RowCreated, AddressOf Me.grd_ItemDataCreated
                        'AddHandler grd.ItemCommand, AddressOf Me.grd_ItemCommand

                        grd.DataSource = dv
                        grd.DataBind()

                        grd.Columns(0).Visible = False
                        grd.Columns(1).Visible = False
                        grd.Columns(3).Visible = False
                        'grd.Columns(5).Visible = False
                        grd.Columns(6).Visible = False
                        grd.Columns(9).Visible = False
                        grd.Columns(10).Visible = False
                        grd.Columns(11).Visible = False
                        grd.Columns(12).Visible = False
                        grd.CssClass = "table table-striped table-bordered table-hover"
                        If dv.Count >= 4 Then
                            'btnAddNewSize.Enabled = False
                        Else
                            'btnAddNewSize.Enabled = True
                        End If
                    Else
                        grd.DataSource = String.Empty
                        grd.DataBind()
                        'btnAddNewSize.Enabled = True
                    End If
                    tc1.Controls.Add(grd)
                    tr1.Cells.Add(tc1)
                    tb1.Rows.Add(tr1)

                    tbcDynamic.Tabs(cnt).Controls.Add(tb1)
                    cnt += 1
                Next

            End If

            PC1.Controls.Add(tbcDynamic)
        Catch ex As Exception
            LogHelper.Error("Product_Master:bind_Tabs:" + ex.Message)
        End Try
    End Sub

    Protected Sub grd_ItemDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            'If (TypeOf e.Row.DataItem Is GridDataItem) Then

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(5).Text = "Qty."
                e.Row.Cells(8).Text = "Is Active"
                e.Row.Cells(13).Text = "Tax"
                e.Row.Cells(14).Text = "Click and Collect"
                e.Row.Cells(15).Text = "Deliver"
                e.Row.Cells(16).Text = "Order at table"
            End If

            'Dim c As CheckBox = New CheckBox()
            'c.ID = "cb_" & e.Row.DataItem.RowIndex.ToString()
            'c.Text = "Test"
            'e.Row.DataItem.Cells(0).Controls.Add(c)


            If e.Row.RowType = DataControlRowType.DataRow Then



                'e.Row.Cells(1).Visible = False
                'e.Row.Cells(3).Visible = False
                'e.Row.Cells(5).Visible = False
                'e.Row.Cells(6).Visible = False
                'e.Row.Cells(13).Visible = False

                Dim txtName As TextBox = New TextBox()
                txtName.ID = "txtName"
                txtName.Attributes.Add("onclick", "return selectTab(this);")

                txtName.Text = (TryCast(e.Row.DataItem, DataRowView)).Row("Name").ToString()
                e.Row.Cells(2).Controls.Add(txtName)


                Dim txtSize As TextBox = New TextBox()
                txtSize.ID = "txtSize"
                txtSize.Attributes.Add("onclick", "return selectTab(this);")
                txtSize.Text = (TryCast(e.Row.DataItem, DataRowView)).Row("Size").ToString()
                e.Row.Cells(4).Controls.Add(txtSize)
                e.Row.Cells(4).Controls.Add(txtSize)

                Dim txtPrc As TextBox = New TextBox()
                txtPrc.ID = "txtPrice"
                txtPrc.Attributes.Add("onclick", "return selectTab(this);")
                txtPrc.Text = (TryCast(e.Row.DataItem, DataRowView)).Row("Price").ToString()
                e.Row.Cells(7).Controls.Add(txtPrc)
                e.Row.Cells(7).Controls.Add(txtPrc)

                Dim chkIsActive As CheckBox = New CheckBox()
                chkIsActive.ID = "chkIsActive"
                If ((TryCast(e.Row.DataItem, DataRowView)).Row("active").ToString() = 1) Then
                    e.Row.Cells(8).Controls.Add(chkIsActive)
                    chkIsActive.Checked = True
                Else
                    e.Row.Cells(8).Controls.Add(chkIsActive)
                    chkIsActive.Checked = False
                End If

                Dim ddIsCollect As CheckBox = New CheckBox()
                ddIsCollect.ID = "chkCollect"
                If ((TryCast(e.Row.DataItem, DataRowView)).Row("click_and_collect").ToString() = 1) Then
                    e.Row.Cells(14).Controls.Add(ddIsCollect)
                    ddIsCollect.Checked = True
                Else
                    e.Row.Cells(14).Controls.Add(ddIsCollect)
                    ddIsCollect.Checked = False
                End If

                Dim chkIsdeliver As CheckBox = New CheckBox()
                chkIsdeliver.ID = "chkIsdeliver"
                If ((TryCast(e.Row.DataItem, DataRowView)).Row("deliver").ToString() = 1) Then
                    e.Row.Cells(15).Controls.Add(chkIsdeliver)
                    chkIsdeliver.Checked = True
                Else
                    e.Row.Cells(15).Controls.Add(chkIsdeliver)
                    chkIsdeliver.Checked = False
                End If

                Dim chkOrder_at_table As CheckBox = New CheckBox()
                chkOrder_at_table.ID = "chkOrder_at_table"
                If ((TryCast(e.Row.DataItem, DataRowView)).Row("Order_at_table").ToString() = 1) Then
                    e.Row.Cells(16).Controls.Add(chkOrder_at_table)
                    chkOrder_at_table.Checked = True
                Else
                    e.Row.Cells(16).Controls.Add(chkOrder_at_table)
                    chkOrder_at_table.Checked = False
                End If


                Dim ddQty As DropDownList = New DropDownList()
                ddQty.ID = "ddQty"
                If ((TryCast(e.Row.DataItem, DataRowView)).Row("Unit_Id").ToString() IsNot "0") Then

                    e.Row.Cells(5).Controls.Add(ddQty)
                    oclsBind.BindUnit(ddQty, Val(Session("cmp_id")))
                    ddQty.SelectedValue = (TryCast(e.Row.DataItem, DataRowView)).Row("Unit_Id").ToString()
                Else

                    e.Row.Cells(5).Controls.Add(ddQty)
                    oclsBind.BindUnit(ddQty, Val(Session("cmp_id")))
                    ddQty.SelectedValue = (TryCast(e.Row.DataItem, DataRowView)).Row("Unit_Id").ToString()
                End If



                Dim ddTax As DropDownList = New DropDownList()
                ddTax.ID = "ddTax"
                If Session("product_id") = Nothing And Session("copy_product_id") = Nothing Then
                    e.Row.Cells(13).Controls.Add(ddTax)
                    oclsBind.BindTaxDirect(ddTax, Val(Session("cmp_id")))
                    ddTax.SelectedItem.Text = "20% VAT"
                Else
                    e.Row.Cells(13).Controls.Add(ddTax)
                    oclsBind.BindTaxDirect(ddTax, Val(Session("cmp_id")))
                    ddTax.SelectedValue = (TryCast(e.Row.DataItem, DataRowView)).Row("Tax_Id").ToString()
                End If


            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:grd_ItemDataBound:" + ex.Message)
        End Try
    End Sub

    Protected Sub grd_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs)
        Try
            'If e.CommandName = "DeleteVal" Then
            '    Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            '    Delete_Size_N_Price(id)
            '    BindGrid()
            'End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdSizeNPrice_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub grd_ItemDataCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    PC1 = (CType(e.Row.Cells(0).Controls(1), PlaceHolder))
            '    Dim tb As TextBox = New TextBox()
            '    pc1.Controls.Add(tb)
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub SaveRecordInDT_From_DynamicGrid()
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each item As GridViewRow In GridView1.Rows
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("Name") = CType(item.FindControl("txtSizeName"), TextBox).Text
                        'row("Size_Id") = Val(CType(item.FindControl("hdnSize_Id"), HiddenField).Value)
                        row("Size") = Val(CType(item.FindControl("txtsize"), TextBox).Text)
                        row("Unit_id") = Val(CType(item.FindControl("ddUnit"), DropDownList).SelectedValue)
                        'row("Price_Id") = Val(CType(item.FindControl("hdnPrice_Id"), HiddenField).Value)
                        row("Price") = Val(CType(item.FindControl("txtPriceLocation"), TextBox).Text)
                        row("active") = Val(IIf(CType(item.FindControl("chkIsActive"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))
                        row("click_and_collect") = Val(IIf(CType(item.FindControl("chkIsClickcollect"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))

                        row("deliver") = Val(IIf(CType(item.FindControl("chkIsDeliver"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))

                        row("Order_at_table") = Val(IIf(CType(item.FindControl("chkIsOrderattable"), System.Web.UI.WebControls.CheckBox).Checked = True, 1, 0))

                        row("Tax_id") = Val(CType(item.FindControl("ddTaxx"), DropDownList).SelectedValue)
                        '------ tax2 added on 13/04/2022-----------
                        row("Tax_id2") = Val(CType(item.FindControl("ddTaxx_2"), DropDownList).SelectedValue)

                        row("Location_Id") = Val(CType(item.FindControl("ddGvLocation"), DropDownList).SelectedValue)
                        'row("Location_Id") = Val(ddLocation.SelectedValue)
                        'row("Location_Id") = Val(tbpnlProcessCategory.ID)
                        If Val(row("Size")) <> 0 And Val(row("Unit_id")) <> 0 Then
                            row("Size_Unit") = row("Size").ToString & " " & CType(item.FindControl("ddUnit"), DropDownList).SelectedItem.Text.ToString() & "-" & ddLocation.SelectedItem.Text.ToString()
                        Else
                            row("Size_Unit") = ""
                        End If

                        '--------------------added sorting no. 29/04/2022 ---------------
                        row("sorting_no") = Val(CType(item.FindControl("txtSortingNum"), TextBox).Text)

                    End If
                Next
            Next

            ViewState("View_Size") = dt1
            bind_drp_Size()

        Catch ex As Exception
            LogHelper.Error("Product_Master:SaveRecordInDT:" + ex.Message)
        End Try
    End Sub

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindNewGrid()
    End Sub
    Protected Sub OnPageIndexChanging2(sender As Object, e As GridViewPageEventArgs)
        GridView2.PageIndex = e.NewPageIndex
        BindNewGridCost()
    End Sub
    Protected Sub BindNewGrid()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView
            If ddLocation.SelectedValue <> 0 Then
                dv.RowFilter = "action <> '2'"
            Else
                dv.RowFilter = "action <> '2'"
            End If

            '---------------- uncomment on  07092021 ------------------
            If ddLocation.SelectedValue <> 0 Then
                dv.RowFilter = "action <> '2' and (Location_id = 0 or Location_id = " + ddLocation.SelectedValue.ToString() + ") and (Product_Price_Id = 0 or Product_Price_Id = " + ddPrices.SelectedValue.ToString() + " )"
                'and Product_Price_Id = '" + ddPrices.SelectedValue.ToString() + "'
            Else
                dv.RowFilter = "action <> '2'"
            End If
            '---------------- uncomment on  07092021 ------------------

            '---------------------added on 14/04/2022-------------------
            If Val(Session("IsAddTax2").ToString()) = 1 Then

                If GridView1.Columns.Count > 0 Then
                    GridView1.Columns(6).Visible = True
                End If

            Else
                If GridView1.Columns.Count > 0 Then
                    GridView1.Columns(6).Visible = False
                End If
            End If
            '---------------------added on 14/04/2022-------------------

            If dv.Count > 0 Then
                GridView1.DataSource = dv
                GridView1.DataBind()
                'If dv.Count >= 4 Then
                '    btnAddNewSize.Enabled = False
                'Else
                '    btnAddNewSize.Enabled = True
                'End If
            Else
                GridView1.DataSource = dv
                GridView1.DataBind()
                btnAddNewSize.Enabled = True
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindNewGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                'Dim dataItem As GridDataItem = CType(e.Row, GridDataItem)

                Dim hFLoc As HiddenField = CType(e.Row.FindControl("hdFLoc"), HiddenField)
                If hFLoc.Value <> String.Empty Then
                    Dim dp As DropDownList = CType(e.Row.FindControl("ddGvLocation"), DropDownList)
                    oclsBind.BindLocationProduct(dp, Val(Session("cmp_id")))
                    dp.SelectedValue = ddLocation.SelectedValue 'hFLoc.Value
                End If

                Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

                If radUnit.SelectedItem.Text.ToString() = "Qty" Then
                    oclsBind.BindUnit(CType(e.Row.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                    CType(e.Row.FindControl("ddUnit"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdnUnit_Id"), HiddenField).Value ' Val(dt1.Rows(0)("Unit_id").ToString)
                    CType(e.Row.FindControl("ddUnit"), DropDownList).Enabled = True

                ElseIf radUnit.SelectedItem.Text.ToString() = "Gm" Then
                    oclsBind.BindUnitFor_Gm(CType(e.Row.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                    CType(e.Row.FindControl("ddUnit"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdnUnit_Id"), HiddenField).Value  'Val(dt1.Rows(0)("Unit_id").ToString)
                    CType(e.Row.FindControl("ddUnit"), DropDownList).Enabled = True

                ElseIf radUnit.SelectedItem.Text.ToString() = "Ml" Then
                    oclsBind.BindUnitFor_Ml(CType(e.Row.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                    CType(e.Row.FindControl("ddUnit"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdnUnit_Id"), HiddenField).Value  'Val(dt1.Rows(0)("Unit_id").ToString)
                    CType(e.Row.FindControl("ddUnit"), DropDownList).Enabled = True

                End If

                oclsBind.BindTaxDirect(CType(e.Row.FindControl("ddTaxx"), DropDownList), Val(Session("cmp_id")))
                oclsBind.BindTaxDirect(CType(e.Row.FindControl("ddTaxx_2"), DropDownList), Val(Session("cmp_id")))

                ' Temp ---------------------------
                Dim hdTxId As HiddenField = CType(e.Row.FindControl("hdnTax_Id"), HiddenField)
                CType(e.Row.FindControl("ddTaxx"), DropDownList).SelectedValue = hdTxId.Value

                '---------------------tax2 added on 14/04/2022-------------------
                If Val(Session("IsAddTax2").ToString()) = 1 Then
                    Dim hdTxId2 As HiddenField = CType(e.Row.FindControl("hdnTax_Id2"), HiddenField)
                    CType(e.Row.FindControl("ddTaxx_2"), DropDownList).SelectedValue = hdTxId2.Value
                Else
                    CType(e.Row.FindControl("ddTaxx_2"), DropDownList).SelectedValue = 0
                End If


                'If Session("product_id") = Nothing And Session("copy_product_id") = Nothing Then
                '    CType(e.Row.FindControl("ddTaxx"), DropDownList).Items.FindByValue(CType(e.Row.FindControl("hdnTax_Id"), HiddenField).Value).Selected = True
                '    'FindByText("20% VAT").Selected = True

                '    '---------------------added on 14/04/2022-------------------
                '    If Val(Session("IsAddTax2").ToString()) = 1 Then
                '        CType(e.Row.FindControl("ddTaxx_2"), DropDownList).Items.FindByValue(CType(e.Row.FindControl("hdnTax_Id2"), HiddenField).Value).Selected = True
                '        'FindByText("20% VAT").Selected = True
                '    Else
                '        CType(e.Row.FindControl("ddTaxx_2"), DropDownList).Items.FindByText("SELECT").Selected = True
                '    End If
                '    '---------------------added on 14/04/2022-------------------

                'Else
                '    Dim hdTxId As HiddenField = CType(e.Row.FindControl("hdnTax_Id"), HiddenField)
                '    CType(e.Row.FindControl("ddTaxx"), DropDownList).SelectedValue = hdTxId.Value

                '    '---------------------tax2 added on 14/04/2022-------------------
                '    If Val(Session("IsAddTax2").ToString()) = 1 Then
                '        Dim hdTxId2 As HiddenField = CType(e.Row.FindControl("hdnTax_Id2"), HiddenField)
                '        CType(e.Row.FindControl("ddTaxx_2"), DropDownList).SelectedValue = hdTxId2.Value
                '    Else
                '        CType(e.Row.FindControl("ddTaxx_2"), DropDownList).SelectedValue = 0
                '    End If

                '    '---------------------tax2 added on 14/04/2022-------------------

                'End If

                If CType(e.Row.FindControl("hdnIsactive"), HiddenField).Value = 0 Then
                    CType(e.Row.FindControl("chkIsActive"), System.Web.UI.WebControls.CheckBox).Checked = False
                Else
                    CType(e.Row.FindControl("chkIsActive"), System.Web.UI.WebControls.CheckBox).Checked = True
                End If

                If CType(e.Row.FindControl("hdnIsclickcollect"), HiddenField).Value = 0 Then
                    CType(e.Row.FindControl("chkIsClickcollect"), System.Web.UI.WebControls.CheckBox).Checked = False
                Else
                    CType(e.Row.FindControl("chkIsClickcollect"), System.Web.UI.WebControls.CheckBox).Checked = True
                End If

                If CType(e.Row.FindControl("hdnIsDeliver"), HiddenField).Value = 0 Then
                    CType(e.Row.FindControl("chkIsDeliver"), System.Web.UI.WebControls.CheckBox).Checked = False
                Else
                    CType(e.Row.FindControl("chkIsDeliver"), System.Web.UI.WebControls.CheckBox).Checked = True
                End If

                If CType(e.Row.FindControl("hdnIsorderattable"), HiddenField).Value = 0 Then
                    CType(e.Row.FindControl("chkIsorderattable"), System.Web.UI.WebControls.CheckBox).Checked = False
                Else
                    CType(e.Row.FindControl("chkIsorderattable"), System.Web.UI.WebControls.CheckBox).Checked = True
                End If

                'Dim hFUnit As HiddenField = CType(e.Row.FindControl("hdnUnit_Id"), HiddenField)
                'Dim dU As DropDownList = CType(e.Row.FindControl("ddUnit"), DropDownList)
                'oclsBind.BindUnit(dU, Val(Session("cmp_id")))
                'dU.SelectedValue = hFUnit.Value

            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:BindNewGrid:" + ex.Message)
        End Try
    End Sub
    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hFLoc As HiddenField = CType(e.Row.FindControl("hdFLoc2"), HiddenField)

                If hFLoc.Value <> String.Empty Then
                    Dim dp As DropDownList = CType(e.Row.FindControl("ddGvLocation2"), DropDownList)
                    oclsBind.BindLocationProduct(dp, Val(Session("cmp_id")))
                    dp.SelectedValue = ddlocation2.SelectedValue 'hFLoc.Value
                End If

                If radUnit.SelectedItem.Text.ToString() = "Qty" Then
                    oclsBind.BindUnit(CType(e.Row.FindControl("ddUnit2"), DropDownList), Val(Session("cmp_id")))
                    Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)
                    CType(e.Row.FindControl("ddUnit2"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdnUnit_Id"), HiddenField).Value ' Val(dt1.Rows(0)("Unit_id").ToString)
                    CType(e.Row.FindControl("ddUnit2"), DropDownList).Enabled = True

                ElseIf radUnit.SelectedItem.Text.ToString() = "Gm" Then
                    oclsBind.BindUnitFor_Gm(CType(e.Row.FindControl("ddUnit2"), DropDownList), Val(Session("cmp_id")))
                    Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)
                    CType(e.Row.FindControl("ddUnit2"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdnUnit_Id"), HiddenField).Value  'Val(dt1.Rows(0)("Unit_id").ToString)
                    CType(e.Row.FindControl("ddUnit2"), DropDownList).Enabled = True
                ElseIf radUnit.SelectedItem.Text.ToString() = "Ml" Then
                    oclsBind.BindUnitFor_Ml(CType(e.Row.FindControl("ddUnit2"), DropDownList), Val(Session("cmp_id")))
                    Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)
                    CType(e.Row.FindControl("ddUnit2"), DropDownList).SelectedValue = CType(e.Row.FindControl("hdnUnit_Id"), HiddenField).Value  'Val(dt1.Rows(0)("Unit_id").ToString)
                    CType(e.Row.FindControl("ddUnit2"), DropDownList).Enabled = True
                End If

                oclsBind.BindTaxDirect(CType(e.Row.FindControl("ddTax2"), DropDownList), Val(Session("cmp_id")))

                If Session("product_id") = Nothing And Session("copy_product_id") = Nothing Then
                    CType(e.Row.FindControl("ddTax2"), DropDownList).Items.FindByText("20% VAT").Selected = True
                Else
                    Dim txId As Integer
                    Dim hdTxId As HiddenField = CType(e.Row.FindControl("hdnTax_Id"), HiddenField)
                    txId = hdTxId.Value
                    CType(e.Row.FindControl("ddTax2"), DropDownList).SelectedValue = txId
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BindNewGridCost()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Cost"), DataTable)
            Dim dv As DataView = dt.DefaultView

            'If Not Session("copy_product_id") = Nothing Then

            'Else

            '    'If ddlocation2.SelectedValue <> 0 Then
            '    '    dv.RowFilter = "action <> '2' "
            '    'Else
            '    '    dv.RowFilter = "action <> '2'"
            '    'End If
            'End If

            dv.RowFilter = "action <> '2'"

            If ddlocation2.SelectedValue <> 0 Then
                dv.RowFilter = "action <> '2' and (Location_id = 0 or Location_id = " + ddlocation2.SelectedValue.ToString() + " )"
                'and Product_Price_Id = '" + ddPrices.SelectedValue.ToString() + "'
            Else
                dv.RowFilter = "action <> '2'"
            End If

            If dv.Count > 0 Then
                GridView2.DataSource = dv
                GridView2.DataBind()
                'If dv.Count >= 4 Then
                '    btnNewSizeCost.Enabled = False
                'Else
                '    btnNewSizeCost.Enabled = True
                'End If
            Else
                btnNewSizeCost.Enabled = True
                GridView2.DataSource = dv
                GridView2.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGridCost:" + ex.Message)
        End Try
    End Sub
    Protected Sub SaveRecordInDTCostFrom_GV()
        Try
            Dim dtCost As DataTable = DirectCast(ViewState("View_Cost"), DataTable)

            For Each item As GridViewRow In GridView2.Rows
                For Each row As DataRow In dtCost.Rows
                    If CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("Name") = CType(item.FindControl("txtSizeName2"), TextBox).Text.ToString()
                        row("Size_Id") = Val(CType(item.FindControl("hdnSize_Id"), HiddenField).Value)
                        row("Size") = Val(CType(item.FindControl("txtsize2"), TextBox).Text)
                        row("Unit_id") = Val(CType(item.FindControl("ddUnit2"), DropDownList).SelectedValue)
                        row("Price_Id") = 0
                        row("Price") = Val(CType(item.FindControl("txtPriceLocation2"), TextBox).Text)
                        row("active") = 1
                        row("Tax_id") = Val(CType(item.FindControl("ddTax2"), DropDownList).SelectedValue)
                        row("Location_Id") = Val(CType(item.FindControl("ddGvLocation2"), DropDownList).SelectedValue)
                        If Val(row("Size")) <> 0 And Val(row("Unit_id")) <> 0 Then
                            row("Size_Unit") = row("Size").ToString & " " & CType(item.FindControl("ddUnit2"), DropDownList).SelectedItem.Text.ToString() & "-" & ddlocation2.SelectedItem.Text.ToString()
                        Else
                            row("Size_Unit") = ""
                        End If
                    End If
                Next
            Next


            ViewState("View_Cost") = dtCost
            bind_drp_Size()
        Catch ex As Exception
            LogHelper.Error("Product_Master:SaveRecordInDTCostFrom_GV:" + ex.Message)
        End Try
    End Sub
    Protected Sub Delete_Cost(ByVal id As Integer)
        Try
            Dim dtCost As DataTable = DirectCast(ViewState("View_Cost"), DataTable)

            For Each row As DataRow In dtCost.Rows
                If row("row_Id") = id Then
                    oclsSize.Size_id = row("Size_Id")
                    oclsSize.cmp_id = Val(Session("cmp_id"))
                    oclsSize.Del_BuyingCost()

                    row("action") = 2
                    row.Delete()
                    dtCost.AcceptChanges()
                    Exit For
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub radUnit_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try
            For Each item As GridViewRow In GridView1.Rows

                If radUnit.SelectedItem.Text.ToString() = "Qty" Then
                    oclsBind.BindUnit(CType(item.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                    CType(item.FindControl("ddUnit"), DropDownList).SelectedValue = Val(radUnit.SelectedValue.ToString())
                    CType(item.FindControl("ddUnit"), DropDownList).Enabled = True
                ElseIf radUnit.SelectedItem.Text.ToString() = "Gm" Then
                    oclsBind.BindUnitFor_Gm(CType(item.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                    CType(item.FindControl("ddUnit"), DropDownList).Enabled = True
                ElseIf radUnit.SelectedItem.Text.ToString() = "Ml" Then
                    oclsBind.BindUnitFor_Ml(CType(item.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                    CType(item.FindControl("ddUnit"), DropDownList).Enabled = True
                End If


            Next
        Catch ex As Exception
            LogHelper.Error("Product_Master:radUnit_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddLocation_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try
            BindGrid()
            BindCost()
            BindNewGrid()
            BindNewGridCost()
        Catch ex As Exception
            LogHelper.Error("Product_Master:ddLocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddPrices_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try

            BindGrid()
            BindNewGrid()
        Catch ex As Exception
            LogHelper.Error("Product_Master:btnAddNewSize_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddUnit_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try

            Dim Unit As Decimal = 0
            Dim sID_2 As Integer = 0
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each item As GridViewRow In GridView1.Rows
                For Each row As DataRow In dt1.Rows
                    If (CType(item.FindControl("ddUnit"), DropDownList).SelectedValue <> row("Unit_id").ToString()) And CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then


                        Dim dv As DataView = dt1.DefaultView
                        dv.RowFilter = "Unit_id = '" + CType(item.FindControl("ddUnit"), DropDownList).SelectedValue + "' and Location_id = " & Val(ddLocation.SelectedValue) & " AND Size = '" + CType(item.FindControl("txtSize"), TextBox).Text + "' AND Size_Id <> '" + CType(item.FindControl("hdnSize_Id"), HiddenField).Value + "' "

                        If dv.ToTable.Rows.Count > 0 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('This size and unit is already exists.');", True)

                            oclsBind.BindUnit(CType(item.FindControl("ddUnit"), DropDownList), Val(Session("cmp_id")))
                            CType(item.FindControl("ddUnit"), DropDownList).SelectedValue = 0
                            Unit = 0

                        Else
                            Unit = CType(item.FindControl("ddUnit"), DropDownList).SelectedValue

                        End If

                        sID_2 = Val(CType(item.FindControl("hdnSize_Id"), HiddenField).Value)

                    End If
                Next
            Next


            Session("Unit") = Unit
            Session("sID_2") = sID_2

            SaveRecordInDT()


        Catch ex As Exception
            LogHelper.Error("Product_Master:ddUnit_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddTaxx_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:ddTax_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddUnit2_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try
            SaveRecordInDTCost()
        Catch ex As Exception
            LogHelper.Error("Product_Master:ddUnit2_SelectedIndexChanged1:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddTax2_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try
            SaveRecordInDTCost()
        Catch ex As Exception
            LogHelper.Error("Product_Master:ddTax2_SelectedIndexChanged1:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddIngredientUnit_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            SaveIngredientRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Product_Master:ddIngredientUnit_SelectedIndexChanged1:" + ex.Message)
        End Try
    End Sub
    Protected Sub chk_PriceOnScaleWeight_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_IsPkgProduct.Checked = True And chk_PriceOnScaleWeight.Checked = True Then
                lbl_error.InnerText = "You can't select both Price by Weight and Package Product at the same time."

            Else
                lbl_error.InnerText = ""
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:chk_PriceOnScaleWeight_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub chk_IsPkgProduct_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_IsPkgProduct.Checked = True And chk_PriceOnScaleWeight.Checked = True Then
                lbl_error.InnerText = "You can't select both Price on Scale Weight and Is Package Product at the same time."
            Else
                lbl_error.InnerText = ""
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:chk_IsPkgProduct_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub btn_copy_Click(sender As Object, e As EventArgs)
        Try

            Dim script As String = "function f(){$find(""" + rwCopyLevel.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)

        Catch ex As Exception
            LogHelper.Error("Product_Master:btn_copy_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btn_levelSave_Click(sender As Object, e As EventArgs)
        Try
            If Val(ddlFromPricelvl.SelectedValue) <> Val(ddlToPricelvl.SelectedValue) And Val(ddlPriceType.SelectedValue) > 0 And txt_Value.Text <> "" Then
                oclsSize.product_id = Val(Session("product_id").ToString())
                oclsSize.FromlevelID = Val(ddlFromPricelvl.SelectedValue)
                oclsSize.TolevelID = Val(ddlToPricelvl.SelectedValue)
                oclsSize.PriceType = Val(ddlPriceType.SelectedValue)
                oclsSize.Prc_Value = Val(txt_Value.Text)
                oclsSize.cmp_id = Val(Session("cmp_id"))

                Dim size As Integer = oclsSize.Copy_SizeNPrice()

                If Not size = Nothing Then

                    oclsProduct.cmp_id = Val(Session("cmp_id"))
                    oclsProduct.product_id = Val(Session("product_id"))
                    oclsProduct.SingleRecord = 0
                    Dim dtSize As DataTable = oclsProduct.Select_SizeNPrice_By_Product()

                    ViewState("View_Size") = Nothing

                    ViewState("View_Size") = dtSize

                    BindGrid_Delete()

                End If

                Dim script As String = "function f(){$find(""" + rwCopyLevel.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Kindly fill all fields for copy price level.');", True)

            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:btn_levelSave_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btn_levelCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim script As String = "function f(){$find(""" + rwCopyLevel.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception
            LogHelper.Error("Product_Master:btn_levelCancel_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddlToPricelvl_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Val(ddlFromPricelvl.SelectedValue) = Val(ddlToPricelvl.SelectedValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Both price level are same, can not copy price level.');", True)
            End If

            '---------- check from level have value or not
            oclsSize.product_id = Val(Session("product_id").ToString())
            oclsSize.FromlevelID = Val(ddlFromPricelvl.SelectedValue)
            oclsSize.cmp_id = Val(Session("cmp_id"))

            Dim dtSize As DataTable = oclsSize.Select_SizeNPrice()

            If dtSize.Rows.Count > 0 Then
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ddlFromPricelvl.SelectedItem.ToString() + " have not any size and price value.');", True)
            End If

            '---------- check to level have value or not
            oclsSize.product_id = Val(Session("product_id").ToString())
            oclsSize.FromlevelID = Val(ddlToPricelvl.SelectedValue)
            oclsSize.cmp_id = Val(Session("cmp_id"))

            Dim dtSize_1 As DataTable = oclsSize.Select_SizeNPrice()

            If dtSize_1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ddlToPricelvl.SelectedItem.ToString() + " have size and price value.');", True)

            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:ddlToPricelvl_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddlFromPricelvl_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            If Val(ddlFromPricelvl.SelectedValue) = Val(ddlToPricelvl.SelectedValue) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Both price level are same, can not copy price level.');", True)
            End If

            '---------- check from level have value or not
            oclsSize.product_id = Val(Session("product_id").ToString())
            oclsSize.FromlevelID = Val(ddlFromPricelvl.SelectedValue)
            oclsSize.cmp_id = Val(Session("cmp_id"))

            Dim dtSize As DataTable = oclsSize.Select_SizeNPrice()

            If dtSize.Rows.Count > 0 Then
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ddlFromPricelvl.SelectedItem.ToString() + " have not any size and price value.');", True)
            End If

            '---------- check to level have value or not
            oclsSize.product_id = Val(Session("product_id").ToString())
            oclsSize.FromlevelID = Val(ddlToPricelvl.SelectedValue)
            oclsSize.cmp_id = Val(Session("cmp_id"))

            Dim dtSize_1 As DataTable = oclsSize.Select_SizeNPrice()

            If dtSize_1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ddlToPricelvl.SelectedItem.ToString() + " have size and price value.');", True)

            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:ddlFromPricelvl_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddTaxx_2_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()

        Catch ex As Exception
            LogHelper.Error("Product_Master:ddTaxx_2_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub chk_additionalTax_CheckedChanged(sender As Object, e As EventArgs)
        Try
            BindNewGrid()
        Catch ex As Exception
            LogHelper.Error("Product_Master:chk_additionalTax_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtSortingNum_TextChanged(sender As Object, e As EventArgs)
        Try

            Dim S_no As Integer = 0
            Dim sID_3 As Integer = 0
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each item As GridViewRow In GridView1.Rows
                For Each row As DataRow In dt1.Rows
                    If (CType(item.FindControl("txtSortingNum"), TextBox).Text <> row("sorting_no").ToString()) And CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then

                        Dim dv As DataView = dt1.DefaultView
                        dv.RowFilter = "sorting_no = '" + CType(item.FindControl("txtSortingNum"), TextBox).Text + "' AND Size_Id <> '" + CType(item.FindControl("hdnSize_Id"), HiddenField).Value + "'  and Location_id = " & Val(ddLocation.SelectedValue) & "  "

                        If dv.ToTable.Rows.Count > 0 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Sorting number already exists.');", True)

                            CType(item.FindControl("txtSortingNum"), TextBox).Text = Val(CType(item.FindControl("txtSortingNum"), TextBox).Text) + 1
                            S_no = Val(CType(item.FindControl("txtSortingNum"), TextBox).Text) + 1

                        Else
                            S_no = CType(item.FindControl("txtSortingNum"), TextBox).Text

                        End If

                        sID_3 = Val(CType(item.FindControl("hdnSize_Id"), HiddenField).Value)
                    End If
                Next
            Next

            Session("sorting_no") = S_no
            Session("sID_3") = sID_3

            SaveRecordInDT()

        Catch ex As Exception
            LogHelper.Error("Product_Master:txtSortingNum_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub AttachedTo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        AttachedTo.PageIndex = e.NewPageIndex
        BindAssociatedGrid()
    End Sub

    Private Sub BindAssociatedGrid()
        Try

            oclsProduct.product_id = Val(Session("product_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dtProduct As DataTable = oclsProduct.Select_AttachedTo()

            If dtProduct.Rows.Count > 0 Then
                AttachedTo.DataSource = dtProduct
                AttachedTo.DataBind()
            Else
                AttachedTo.DataSource = String.Empty
                AttachedTo.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindAssociatedGrid:" + ex.Message)
        End Try
    End Sub
    Protected Sub lbn_Associated_Click(sender As Object, e As EventArgs)
        Try
            If (lbn_Associated.Text = "Show Product Associated To") Then
                lbn_Associated.Text = "Hide Product Associated To"
                AttachedTo.Visible = True
            Else
                lbn_Associated.Text = "Show Product Associated To"
                AttachedTo.Visible = False
            End If
            BindAssociatedGrid()
        Catch ex As Exception
            LogHelper.Error("Product_Master:lbn_Associated_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub AttachedTo_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                Response.Redirect(String.Format("Product_Master.aspx?product_id={0}", e.CommandArgument.ToString()))
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:AttachedTo_RowCommand:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddlchoices_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim currentrow As GridTableCell = CType((CType(sender, DropDownList)).Parent, GridTableCell)
            Dim txtmin_currentRow As TextBox = CType(currentrow.FindControl("txtminselect"), TextBox)
            Dim txtmax_currentRow As TextBox = CType(currentrow.FindControl("txtmaxselect"), TextBox)
            Dim selectedDropdown As Integer = CType(currentrow.FindControl("ddlchoices"), DropDownList).SelectedValue

            Dim dt As DataTable = DirectCast(ViewState("check"), DataTable)
            Dim dv1 As DataView = dt.DefaultView
            dv1.RowFilter = "Choice = " + selectedDropdown.ToString()

            If dv1.ToTable.Rows.Count > 0 Then
                txtmin_currentRow.Text = dv1.ToTable.Rows(0)("min_select")
                txtmax_currentRow.Text = dv1.ToTable.Rows(0)("max_select")
            Else
                txtmin_currentRow.Text = 0.00
                txtmax_currentRow.Text = 0.00
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:ddlchoices_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub rdBarcodeSize_ItemCommand(sender As Object, e As GridCommandEventArgs)
        Try

            If e.CommandName = "DeleteBarcode" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Barcode(id)
                BindGrid_DeleteBarcode()
            End If

            If e.CommandName = "EditBarcode" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)

                Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)

                Dim dv As DataView = dt.DefaultView
                dv.RowFilter = "Barcode_Size_Id = " & Val(id) & "  "

                txtBarcodeM.Text = dv.ToTable.Rows(0)("Barcode_Size").ToString()
                ddBarcodeSize.SelectedValue = Val(dv.ToTable.Rows(0)("Size_Id").ToString())

                hdBarcode_size.Value = Convert.ToInt32(e.CommandArgument).ToString()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdBarcodeSize_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub Delete_Barcode(ByVal id As Integer)
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)
            For Each row As DataRow In dt.Rows

                If row("Barcode_Size_Id") = id Then
                    'row.Delete()
                    'dt.AcceptChanges()
                    'Exit For
                    If row("is_saved") = 1 Then
                        row("action") = 2
                        row.EndEdit()
                        dt.AcceptChanges()
                        Exit For
                    ElseIf (row("is_saved") = 0) Then
                        row.Delete()
                        dt.AcceptChanges()
                        Exit For
                    End If
                End If
            Next

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
            hdBarcode_size.Value = ""
        Catch ex As Exception
            LogHelper.Error("Product_Master:Delete_Price:" + ex.Message)
        End Try

    End Sub

    Protected Sub BindGrid_DeleteBarcode()
        Try
            Dim dtBarcode As DataTable = DirectCast(ViewState("View_Barcode_Size"), DataTable)
            Dim dvBarcode As DataView = dtBarcode.DefaultView
            dvBarcode.RowFilter = "action <> 2"


            If dtBarcode.Rows.Count > 0 And dvBarcode.ToTable.Rows.Count > 0 Then
                rdBarcodeSize.DataSource = dvBarcode
                rdBarcodeSize.DataBind()
            Else
                rdBarcodeSize.DataSource = String.Empty
                rdBarcodeSize.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindGrid:" + ex.Message)
        End Try
    End Sub


    Protected Sub chk_ForKiosk_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_ForKiosk.Checked = True Then
                fileupload.MaxFileSize = 104857600
            Else
                fileupload.MaxFileSize = 104857600
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
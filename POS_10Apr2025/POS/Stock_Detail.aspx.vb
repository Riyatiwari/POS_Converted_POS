Imports System.Data
Imports System.IO
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.OleDb

Imports OfficeOpenXml
Imports System.Data.Common

Partial Class Stock_Detail
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclstock As New Controller_clsStock()
    Dim oclsProduct As New Controller_clsProduct()

    ' Static rowcount As Integer = 2
    'Dim rowcount As Integer = 2

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                HideColumns()
                If Not Session("Stk_Chg_rec_id") = Nothing Then

                    If Session("cmp_id") = Nothing Then
                        Response.Redirect("SignIn.aspx", False)
                    End If
                    oclsBind.BindLocation(rdlocation, Val(Session("cmp_id")))
                    rdlocation.SelectedIndex = 0

                    BindGridMain_Edit()
                    BindGrid_Edit()
                    bindIngredientGrid()

                Else
                    txtdate.SelectedDate = DateTime.Now
                    txtdate.MaxDate = DateTime.Now
                    If Session("cmp_id") = Nothing Then
                        Response.Redirect("SignIn.aspx", False)
                    End If
                    oclsBind.BindLocation(rdlocation, Val(Session("cmp_id")))
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

                    Dim e1 As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
                    ddlstockdetails_SelectedIndexChanged(ddlstockdetails, e1)
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub bindSortingNo()
        Try
            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.form_name = "Stock Changes"
            Dim dtTable As DataTable = oclstock.Select1()
            If dtTable.Rows.Count > 0 Then
                'Dim rec As String = "CHG00000000"
                Dim dt As String = (dtTable.Rows(0)("receipt_number"))
                txtreceipt.Text = dt

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:bindSortingNo" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim RecordInsert As Integer = 1
            Dim stkonhand As Integer = 1
            Dim qty As Integer = 1
            Dim cntqty As Integer = 1
            Dim dtgrid As Integer = 1

            'Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)
            If dt.Rows.Count <= 0 Then
                dtgrid = 0
            End If

            For Each row As GridViewRow In rdproduct.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim ddlProductGroup As DropDownList = CType(row.FindControl("ddlproductgroup"), DropDownList)
                    Dim ddlProduct As DropDownList = CType(row.FindControl("ddlproduct"), DropDownList)

                    If ddlProductGroup IsNot Nothing AndAlso Val(ddlProductGroup.SelectedValue) = 0 Then
                        RecordInsert = 0
                        Exit For
                    End If
                    If ddlProduct IsNot Nothing AndAlso Val(ddlProduct.SelectedValue) = 0 Then
                        RecordInsert = 0
                        Exit For
                    End If
                End If

                'If Val(CType(item.FindControl("ddlproductgroup"), RadComboBox).SelectedValue) = 0 Then
                '    RecordInsert = 0
                '    Exit For
                'End If
                'If Val(CType(item.FindControl("ddlproduct"), RadComboBox).SelectedValue) = 0 Then
                '    RecordInsert = 0
                '    Exit For
                'End If
            Next

            If ddlstockdetails.SelectedItem.Text IsNot "Stock take" Then
                For Each row As GridViewRow In rdproduct.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim txtStockOnHand As TextBox = CType(row.FindControl("txtstockonhand"), TextBox)

                        If txtStockOnHand IsNot Nothing AndAlso Val(txtStockOnHand.Text) = 0 Then
                            stkonhand = 0
                            Exit For
                        End If
                    End If
                Next
            End If


            For Each row As GridViewRow In rdproduct.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim txtTotalStock As TextBox = CType(row.FindControl("txttotalStock"), TextBox)
                    Dim txtCountQuantity As TextBox = CType(row.FindControl("txtcountquantity"), TextBox)

                    If ddlstockdetails.SelectedValue = 1 OrElse ddlstockdetails.SelectedValue = 2 Then
                        If txtTotalStock IsNot Nothing AndAlso txtTotalStock.Text = "0" Then
                            qty = 0
                        End If
                    ElseIf ddlstockdetails.SelectedValue = 3 Then
                        If txtCountQuantity IsNot Nothing AndAlso txtCountQuantity.Text = "0" Then
                            cntqty = 0
                        End If
                        Exit For
                    End If
                End If
            Next

            If Session("Stk_Chg_rec_id") Is Nothing Then
                Insert_Stk_detail_Add()
            Else
                Insert_Stk_detail_Edit()
            End If
            'Insert_Stk_detail()
            'End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Stock_Detail:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Session("stock_id") = Nothing
            Clear()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            rdlocation.SelectedIndex = 0
            txtdate.SelectedDate = DateTime.Now
            rdproduct.DataSource = String.Empty
            rdproduct.DataBind()

        Catch ex As Exception
            LogHelper.Error("Stock_Detail:Clear" + ex.Message)
        End Try
    End Sub

    Public Sub bindIngredientGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.Location_id = Val(rdlocation.SelectedValue)
            Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

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

    Protected Sub btnaddProduct_Click(sender As Object, e As EventArgs) Handles btnaddProduct.Click
        Try
            rdproduct.Visible = True

            ViewState("Product_Data") = Nothing

            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)

            'Dim RecordInsert As Integer = 1

            'For Each item As GridDataItem In rdproduct.Items
            '    If Val(CType(item.FindControl("ddlproductgroup"), RadComboBox).SelectedValue) = 0 Then
            '        RecordInsert = 0
            '        Exit For
            '    End If
            '    If Val(CType(item.FindControl("ddlproduct"), RadComboBox).SelectedValue) = 0 Then
            '        RecordInsert = 0
            '        Exit For
            '    End If
            'Next

            'If RecordInsert = 1 Then
            '    Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            '    oclstock.cmp_id = Val(Session("cmp_id"))
            '    oclstock.product_id = 0
            '    oclstock.SingleRecord = 1
            '    Dim dtSize As DataTable = oclstock.Select_SizeNPrice_By_Product()

            '    If dt1.Rows.Count > 0 Then
            '        dtSize.Rows(0)("row_Id") = dt1.Rows.Count + 1
            '    End If

            '    dt1.Merge(dtSize)

            '    BindGrid()
            '   Dim e1 As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            '    ddlstockdetails_SelectedIndexChanged(ddlstockdetails, e1)

            'Else
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Fields with * can not be blank');", True)
            'End If


        Catch ex As Exception
            LogHelper.Error("Stock_Detail:btnaddProduct_Click:" + ex.Message)
        End Try

    End Sub

    Protected Sub rdproduct_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles rdproduct.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim dataRow As GridViewRow = CType(e.Row, GridViewRow)

                oclsBind.BindProductGroup(CType(dataRow.FindControl("ddlproductgroup"), DropDownList), Val(Session("cmp_id")))
                oclsBind.BindUnit(CType(dataRow.FindControl("ddltotalunit"), DropDownList), Val(Session("cmp_id")))
                oclsBind.BindProduct(CType(dataRow.FindControl("ddlproduct"), DropDownList), Val(Session("cmp_id")))
                oclsBind.BindTax(CType(dataRow.FindControl("ddltax"), DropDownList), Val(Session("cmp_id")))



                If Session("Stk_Chg_rec_id") Is Nothing Then
                    Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

                    For Each row As DataRow In dt1.Rows
                        If CType(dataRow.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                            CType(dataRow.FindControl("ddlproduct"), DropDownList).SelectedValue = row("Product_id").ToString()
                            CType(dataRow.FindControl("ddlproductgroup"), DropDownList).SelectedValue = row("Product_group").ToString()
                            CType(dataRow.FindControl("ddltotalunit"), DropDownList).SelectedValue = row("unit_id").ToString()

                            Dim txtqty As TextBox = CType(dataRow.FindControl("txttotalStock"), TextBox)
                            Dim txtcountqty As TextBox = CType(dataRow.FindControl("txtcountquantity"), TextBox)
                            Dim txtstkonhand As TextBox = CType(dataRow.FindControl("txtstockonhand"), TextBox)
                            Dim txtbasesize As TextBox = CType(dataRow.FindControl("txtbasesize"), TextBox)


                            ' Dim txtqty As TextBox = CType(dataRow.FindControl("txttotalStock"), TextBox)
                            ' Dim txtcountqty As TextBox = CType(dataRow.FindControl("txtcountquantity"), TextBox)
                            ' Dim txtstkonhand As TextBox = CType(dataRow.FindControl("txtstockonhand"), TextBox)
                            'Dim lblVariance As Label = CType(e.Row.FindControl("lblVariance"), Label)


                            'If lblVariance IsNot Nothing Then


                            '    Integer.TryParse(txttotalStock.Text, totalStock)
                            '    Integer.TryParse(txtcountquantity.Text, countQuantity)
                            '    Integer.TryParse(txtstkonhand.Text, stockOnHand)

                            '    Dim variance As Integer = 0

                            '    If ddlstockdetails.SelectedValue = "1" Then
                            '        variance = countQuantity + stockOnHand
                            '    ElseIf ddlstockdetails.SelectedValue = "2" Then
                            '        variance = totalStock + stockOnHand
                            '    End If

                            '    lblVariance.Text = variance.ToString()
                            'End If
                            If ddlstockdetails.SelectedValue = 1 Or ddlstockdetails.SelectedValue = 2 Then
                                txtcountqty.Enabled = False
                                txtqty.Enabled = True
                            Else
                                txtqty.Enabled = False
                                txtcountqty.Enabled = True
                            End If


                        End If
                    Next
                Else
                    Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

                    For Each row As DataRow In dt1.Rows
                        If CType(dataRow.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                            CType(dataRow.FindControl("ddlproduct"), DropDownList).SelectedValue = CType(dataRow.FindControl("hdproduct"), HiddenField).Value
                            If Val(CType(dataRow.FindControl("hdproductgroup"), HiddenField).Value) > 0 Then
                                CType(dataRow.FindControl("ddlproductgroup"), DropDownList).SelectedValue = CType(dataRow.FindControl("hdproductgroup"), HiddenField).Value
                                ViewState("productgroup") = CType(dataRow.FindControl("hdproductgroup"), HiddenField).Value
                            Else
                                CType(dataRow.FindControl("ddlproductgroup"), DropDownList).SelectedValue = ViewState("productgroup").ToString()
                                '  Dim lblproductgroup As Label
                                '  CType(e.Row.FindControl("lblproductgroup"), Label).Text = ViewState("productgroup").ToString()
                            End If

                            CType(dataRow.FindControl("ddltotalunit"), DropDownList).SelectedValue = CType(dataRow.FindControl("hdunit"), HiddenField).Value

                            Dim txtqty As TextBox = CType(dataRow.FindControl("txttotalStock"), TextBox)
                            Dim txtcountqty As TextBox = CType(dataRow.FindControl("txtcountquantity"), TextBox)
                            Dim txtstkonhand As TextBox = CType(dataRow.FindControl("txtstockonhand"), TextBox)
                            Dim txtbaseunit As TextBox = CType(dataRow.FindControl("txtbaseunit"), TextBox)

                            oclstock.cmp_id = Val(Session("cmp_id"))
                            oclstock.product_id = CType(dataRow.FindControl("ddlproduct"), DropDownList).SelectedValue
                            oclstock.Location_id = Val(rdlocation.SelectedValue)

                            ' Dim txtqty As TextBox = CType(dataRow.FindControl("txttotalStock"), TextBox)
                            '  Dim txtcountqty As TextBox = CType(dataRow.FindControl("txtcountquantity"), TextBox)
                            '  Dim txtstkonhand As TextBox = CType(dataRow.FindControl("txtstockonhand"), TextBox)


                            Dim dttran As DataTable = oclstock.SelectTranDetail()
                            If dttran.Rows.Count > 0 Then
                                Dim stockClosing As String = dttran.Rows(0)("Stock_Closing").ToString()


                                If Not String.IsNullOrEmpty(stockClosing) Then

                                    txtstkonhand.Text = stockClosing
                                    txtstkonhand.ReadOnly = True
                                Else
                                    txtstkonhand.Text = ""
                                End If
                            Else
                                txtstkonhand.Text = ""
                            End If

                            'Dim txtqty As TextBox = CType(dataRow.FindControl("txttotalStock"), TextBox)
                            ' Dim txtcountqty As TextBox = CType(dataRow.FindControl("txtcountquantity"), TextBox)
                            'Dim txtstkonhand As TextBox = CType(dataRow.FindControl("txtstockonhand"), TextBox)
                            'Dim lblVariance As Label = CType(e.Row.FindControl("lblVariance"), Label)

                            If e.Row.RowType = DataControlRowType.DataRow Then
                                Dim txtstockonhand As TextBox = CType(e.Row.FindControl("txtstockonhand"), TextBox)
                                Dim txttotalStock As TextBox = CType(e.Row.FindControl("txttotalStock"), TextBox)
                                Dim txtcountquantity As TextBox = CType(e.Row.FindControl("txtcountquantity"), TextBox)
                                Dim lblVariance As Label = CType(e.Row.FindControl("lblVariance"), Label)
                                Dim totalStockText As String = txttotalStock.Text
                                Dim countQuantityText As String = txtcountquantity.Text
                                LogHelper.Error("Total Stock Text: " + totalStockText)
                                LogHelper.Error("Count Quantity Text: " + countQuantityText)

                                Dim baseSizeText As String = CType(dataRow.FindControl("baseunit"), TextBox).Text

                                If baseSizeText.EndsWith("Ml") Then

                                    Dim baseSizeInML As Decimal = Decimal.Parse(baseSizeText.Replace("Ml", "").Trim())


                                    Dim txttotalStockML As TextBox = CType(dataRow.FindControl("txttotalStock"), TextBox)
                                    Dim txtcountquantityML As TextBox = CType(dataRow.FindControl("txtcountquantity"), TextBox)


                                    Dim quantity As Decimal = Decimal.Parse(txttotalStockML.Text)
                                    Dim countQuantityML As Decimal = Decimal.Parse(CType(e.Row.FindControl("txtcountquantity"), TextBox).Text)



                                    txttotalStockML.Text = (quantity / baseSizeInML).ToString()
                                    'txtcountquantityML.Text = (countQuantityML / baseSizeInML).ToString
                                    'txtcountquantity.Text = String.Format("{0:0.00}", countQuantityML / baseSizeInML)

                                End If

                                '  Dim totalStockText As String = txttotalStock.Text
                                '   Dim countQuantityText As String = txtcountquantity.Text
                                'Dim stockOnHandText As String = txtstockonhand.Text

                                'Dim stockOnHand As String = 0
                                'Dim totalStock As Integer = 0
                                'Dim countQuantity As Integer = 0
                                ''If txtstockonhand IsNot Nothing AndAlso txttotalStock IsNot Nothing AndAlso txtcountquantity IsNot Nothing AndAlso lblVariance IsNot Nothing Then
                                'If Integer.TryParse(stockOnHandText, stockOnHand) AndAlso Integer.TryParse(totalStockText, totalStock) AndAlso Integer.TryParse(countQuantityText, countQuantity) Then

                                '    LogHelper.Error("Total Stock Text1: " + totalStockText)
                                '    LogHelper.Error("Count Quantity Text1: " + countQuantityText)
                                '    ' Extract numeric part from txtstockonhand
                                '    stockOnHandText = txtstockonhand.Text.Trim()
                                '    Dim stockOnHandValue As String = stockOnHandText.Split(" ")(0)

                                '    LogHelper.Error("Stock On Hand Value: " + stockOnHandValue)


                                '    If Not Integer.TryParse(stockOnHandValue, stockOnHand) Then
                                '        LogHelper.Error("Failed to parse Stock On Hand value.")
                                '    End If


                                '    LogHelper.Error("Parsed Stock On Hand: " + stockOnHand.ToString())

                                '    ' Parse totalStock and countQuantity as integers
                                '    If Not Integer.TryParse(txttotalStock.Text, totalStock) Then
                                '        LogHelper.Error("Failed to parse Total Stock value.")
                                '    End If

                                '    ' Log the parsed value
                                '    LogHelper.Error("Parsed Total Stock: " + totalStock.ToString())

                                '    If Not Integer.TryParse(txtcountquantity.Text, countQuantity) Then
                                '        LogHelper.Error("Failed to parse Count Quantity value.")
                                '    End If

                                '    ' Log the parsed value
                                '    LogHelper.Error("Parsed Count Quantity: " + countQuantity.ToString())

                                '    ' Calculate variance based on ddlstockdetails.SelectedValue
                                '    If ddlstockdetails.SelectedValue = "1" Then
                                '        lblVariance.Text = (stockOnHandValue + totalStockText).ToString()
                                '    ElseIf ddlstockdetails.SelectedValue = "2" Then
                                '        lblVariance.Text = (stockOnHandValue + txtcountquantity.Text).ToString()
                                '    End If


                                '    LogHelper.Error("Calculated Variance: " + lblVariance.Text)
                                ' End If
                                Dim stockOnHand As Decimal = 0
                                Dim totalStock As Decimal = 0
                                Dim countQuantity As Decimal = 0


                                Dim stockOnHandText As String = txtstockonhand.Text.Trim()
                                Dim stockOnHandValue As String = stockOnHandText.Split(" ")(0)


                                LogHelper.Error("Stock On Hand Value: " + stockOnHandValue)

                                If Not Decimal.TryParse(stockOnHandValue, stockOnHand) Then
                                    LogHelper.Error("Failed to parse Stock On Hand value.")
                                End If


                                If Not Decimal.TryParse(txttotalStock.Text, totalStock) Then
                                    LogHelper.Error("Failed to parse Total Stock value.")
                                End If

                                If Not Decimal.TryParse(txtcountquantity.Text, countQuantity) Then
                                    LogHelper.Error("Failed to parse Count Quantity value.")
                                End If


                                If ddlstockdetails.SelectedValue = "2" Then
                                    lblVariance.Text = (totalStock - stockOnHand).ToString()
                                ElseIf ddlstockdetails.SelectedValue = "3" Then
                                    lblVariance.Text = (countQuantity - stockOnHand).ToString()
                                End If


                            End If




                            If ddlstockdetails.SelectedValue = 1 Or ddlstockdetails.SelectedValue = 2 Then
                                txtcountqty.Enabled = False
                                txtcountqty.Visible = False
                                txtqty.Enabled = True
                                txtqty.Visible = True
                            Else
                                txtqty.Enabled = False
                                txtqty.Visible = False
                                txtcountqty.Enabled = True
                                txtcountqty.Visible = True
                            End If
                            If e.Row.RowType = DataControlRowType.EmptyDataRow OrElse e.Row.RowType = DataControlRowType.Pager Then
                                Return
                            End If


                            Dim hideQuantityColumn As Boolean = ddlstockdetails.SelectedValue = "1" Or ddlstockdetails.SelectedValue = "2"


                            If e.Row.RowType = DataControlRowType.Header Then
                                If hideQuantityColumn Then
                                    e.Row.Cells(4).Visible = False
                                Else
                                    e.Row.Cells(3).Visible = False
                                End If
                            End If


                            If e.Row.RowType = DataControlRowType.DataRow Then
                                If hideQuantityColumn Then
                                    e.Row.Cells(4).Visible = False
                                Else
                                    e.Row.Cells(3).Visible = False
                                End If
                            End If

                            'If e.Row.RowType = DataControlRowType.DataRow Then
                            '    ' Check the condition to determine which columns to hide
                            '    If ddlstockdetails.SelectedValue = 1 Or ddlstockdetails.SelectedValue = 2 Then
                            '        ' Hide the Quantity and Count Quantity columns for data rows
                            '        e.Row.Cells(5).Visible = False ' Assuming Quantity is at index 4
                            '        e.Row.Cells(6).Visible = False ' Assuming Count Quantity is at index 5
                            '    Else

                            '        e.Row.Cells(5).Visible = True
                            '        e.Row.Cells(6).Visible = True
                            '    End If
                            'End If
                            'If Val(CType(dataRow.FindControl("hdnactive"), HiddenField).Value) = 0 Then
                            '    CType(dataRow.FindControl("chkisdamage"), CheckBox).Checked = False
                            'Else
                            '    CType(dataRow.FindControl("chkisdamage"), CheckBox).Checked = True
                            'End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:rdProduct_ItemDataBound:" + ex.Message)
        End Try
    End Sub

    Protected Sub HideColumns()

        Dim hideQuantityColumn As Boolean = ddlstockdetails.SelectedValue = "1" Or ddlstockdetails.SelectedValue = "2"

        rdproduct.Columns(4).Visible = Not hideQuantityColumn
        rdproduct.Columns(3).Visible = hideQuantityColumn
    End Sub

    Protected Sub BindGrid()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView

            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdproduct.DataSource = dv
                rdproduct.DataBind()
            Else
                rdproduct.Enabled = True
                rdproduct.Visible = True
                rdproduct.DataSource = String.Empty
                rdproduct.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Detail:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGrid_Delete()
        Try
            Dim dt As DataTable = DirectCast(ViewState("Product_Data"), DataTable)
            ViewState("oldProduct_Data_Change") = dt
            'ViewState("Stock_Data_Change") = dt
            Dim dv As DataView = dt.DefaultView
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdproduct.DataSource = dv
                rdproduct.DataBind()
            Else
                rdproduct.Enabled = True
                rdproduct.Visible = True
                rdproduct.DataSource = String.Empty
                rdproduct.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGrid_Edit()
        Try
            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
            Dim dt As DataTable = oclstock.SelecStockDetail_Edit()

            ViewState("Stock_Data_Change") = dt



            'Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdproduct.DataSource = dv
                rdproduct.DataBind()
                ddlstockdetails.Enabled = False
                rdlocation.Enabled = False
                txtdate.Enabled = False
            Else
                rdproduct.Enabled = True
                rdproduct.Visible = True
                rdproduct.DataSource = String.Empty
                rdproduct.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Detail:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGridMain_Edit()
        Try
            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
            Dim dt As DataTable = oclstock.Selectdetailrec_Main_Detail()

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    txtreceipt.Text = row("Chg_rec_num")
                    rdlocation.SelectedValue = row("location_id")
                    txtdate.SelectedDate = row("for_date")
                    ddlstockdetails.SelectedValue = row("stock_detail")
                    rdStockDesc.Text = row("stock_desc")
                Next

            End If


        Catch ex As Exception
            LogHelper.Error("Stock_Detail:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub SaveRecordInDT()
        Try
            'Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dt1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            For Each row As GridViewRow In rdproduct.Rows
                For Each dtRow As DataRow In dt1.Rows
                    If CType(row.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = dtRow("row_Id").ToString() Then




                        dtRow("count_quantity") = Val(CType(row.FindControl("txtcountquantity"), TextBox).Text)
                        dtRow("total_stock") = Val(CType(row.FindControl("txttotalStock"), TextBox).Text)
                        Dim lblVariance As Label = CType(row.FindControl("lblVariance"), Label)
                        Dim countQuantityTextBox As TextBox = CType(row.FindControl("txtcountquantity"), TextBox)
                        Dim totalStockTextBox As TextBox = CType(row.FindControl("txttotalStock"), TextBox)
                        Dim hdunit As HiddenField = CType(row.FindControl("hdunit"), HiddenField)
                        'Dim baseUnit As String = dtRow("base_size")
                        'If baseUnit.EndsWith("Ml", StringComparison.OrdinalIgnoreCase) Then
                        '    Dim baseSizeInML As Decimal = Decimal.Parse(baseUnit.Replace("Ml", "").Trim())
                        '    Dim countQuantity As Decimal = Decimal.Parse(countQuantityTextBox.Text)
                        '    countQuantity /= baseSizeInML
                        '    countQuantityTextBox.Text = countQuantity.ToString()
                        '    countQuantityTextBox.Text = String.Format("{0:0.00}", countQuantityML / baseSizeInML)
                        'End If
                        'lblVariance.Text = dtRow("stock_on_hand") + dtRow("count_quantity")




                        'If ddlstockdetails.SelectedValue = "2" Then
                        '    lblVariance.Text = dtRow("total_stock") - dtRow("stock_on_hand")
                        'ElseIf ddlstockdetails.SelectedValue = "3" Then
                        '    lblVariance.Text = dtRow("count_quantity") - dtRow("stock_on_hand")
                        'End If

                        Dim countQuantityString As String = dtRow("count_quantity").ToString()
                        Dim QuantityString As String = dtRow("total_stock").ToString()
                        oclstock.cmp_id = Val(Session("cmp_id"))
                        oclstock.product_id = CType(row.FindControl("ddlproduct"), DropDownList).SelectedValue
                        oclstock.Location_id = Val(rdlocation.SelectedValue)
                        Dim txtstkonhand As TextBox = CType(row.FindControl("txtstockonhand"), TextBox)
                        Dim dttran As DataTable = oclstock.SelectTranDetail()

                        If dttran.Rows.Count > 0 Then
                            Dim stockClosing As String = dttran.Rows(0)("Stock_Closing").ToString()
                            Dim stockOnHandText As String = stockClosing.Trim()
                            Dim stockOnHandValue As String = stockOnHandText.Split(" ")(0)

                            ' first row misscalculation cozof stock_on_hand---stock_closing



                            If ddlstockdetails.SelectedValue = "2" Then
                                lblVariance.Text = (QuantityString - stockOnHandValue).ToString()
                            ElseIf ddlstockdetails.SelectedValue = "3" Then
                                lblVariance.Text = (countQuantityString - stockOnHandValue).ToString()
                            End If
                        End If


                        'dtRow("Base_size") = Val(CType(row.FindControl("txtbaseunit"), RadTextBox).Text.Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", ""))
                        dtRow("stock_on_hand") = Val(CType(row.FindControl("txtstockonhand"), TextBox).Text.Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", ""))
                        'dtRow("Product_group") = Val(CType(row.FindControl("ddlproductgroup"), RadComboBox).SelectedValue)
                        '    dtRow("Product_id") = Val(CType(row.FindControl("ddlproduct"), RadComboBox).SelectedValue)

                        'dtRow("unit_id") = Val(CType(row.FindControl("hdunit"), HiddenField))
                        'dtRow("unit_id") = dttran.Rows(0)("unit_id").ToString()
                        dtRow("unit_id") = dt1.Rows(0)("unit_id").ToString()



                    End If

                Next
            Next
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:SaveRecordInDT:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlproductgroup_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim ddlSection As RadComboBox = CType(sender, RadComboBox)
            Dim editItem As GridDataItem = CType(ddlSection.NamingContainer, GridDataItem)

            oclsBind.BindProductByProductGroup(CType(editItem.FindControl("ddlproduct"), RadComboBox), Val(Session("cmp_id")), ddlSection.SelectedValue)
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:ddlproductgroup_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlproduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim ddlproduct As RadComboBox = CType(sender, RadComboBox)
            Dim row As GridDataItem = CType(ddlproduct.NamingContainer, GridDataItem)
            Dim txtstkonhand As TextBox = CType(row.FindControl("txtstockonhand"), TextBox)
            Dim txtbaseunit As RadTextBox = CType(row.FindControl("txtbaseunit"), RadTextBox)
            Dim ddlunit As RadComboBox = CType(row.FindControl("ddltotalunit"), RadComboBox)
            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.product_id = CType(row.FindControl("ddlproduct"), RadComboBox).SelectedValue
            oclstock.Location_id = Val(rdlocation.SelectedValue)
            Dim dttran As DataTable = oclstock.SelectTranDetail()
            If dttran.Rows.Count > 0 Then
                txtstkonhand.Text = dttran.Rows(0)("Stock_Closing")
                ddlunit.SelectedValue = dttran.Rows(0)("Unit_id")
                'oclsBind.BindUnitByProduct(ddlunit, Val(Session("cmp_id")), ddlproduct.SelectedValue)
                txtstkonhand.ReadOnly = True
                SaveRecordInDT()
            Else
                txtstkonhand.Text = ""
                oclsBind.BindUnit(CType(row.FindControl("ddltotalunit"), RadComboBox), Val(Session("cmp_id")))
                SaveRecordInDT()
            End If
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:ddlproduct_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txttotalStock_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:txttotalStock_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddltotalunit_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:ddltotalunit_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub txtprice_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:chkIsActive_CheckedChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub chkisdamage_CheckedChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:txtprice_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlstockdetails_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlstockdetails.SelectedIndexChanged
        Try
            HideColumns()
            For Each row As GridViewRow In rdproduct.Rows
                Dim txtqty As RadTextBox = CType(row.FindControl("txttotalStock"), RadTextBox)
                Dim txtcountqty As RadTextBox = CType(row.FindControl("txtcountquantity"), RadTextBox)
                Dim rfqty As RequiredFieldValidator = CType(row.FindControl("rfqty"), RequiredFieldValidator)
                Dim rfcntqty As RequiredFieldValidator = CType(row.FindControl("rfcntqty"), RequiredFieldValidator)

                If ddlstockdetails.SelectedValue = 1 Or ddlstockdetails.SelectedValue = 2 Then
                    txtcountqty.Enabled = False
                    txtqty.Enabled = True
                Else
                    txtqty.Enabled = False
                    txtcountqty.Enabled = True
                End If
            Next
            rdproduct.DataBind()

            'SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:ddlstockdetails_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtcountquantity_TextChanged(sender As Object, e As EventArgs)
        Try


            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:chkIsActive_CheckedChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtstockonhand_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:txtcountquantity_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtbaseunit_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:txtbaseunit_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub Insert_Stk_detail_Add()
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim Stk_Chg_rec_id As String

            If Session("Stk_Chg_rec_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 0

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.InsUpdDel_ChgRec()
            Else
                oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 0

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.Upd_ChgRec()
            End If

            'oclstock.stock_receive_date = txtdate.SelectedDate
            'oclstock.Location_id = Val(rdlocation.SelectedValue)
            'oclstock.login_id = Val(Session("login_id"))
            'oclstock.cmp_id = Val(Session("cmp_id"))
            'oclstock.ip_address = Request.UserHostAddress
            'oclstock.receipt_number = txtreceipt.Text
            'oclstock.stock_detail = ddlstockdetails.SelectedValue
            'Stk_Chg_rec_id = oclstock.InsUpdDel_ChgRec()

            'Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)
            If dt.Rows.Count > 0 Then



                For Each row As DataRow In dt.Rows
                    oclstock.stk_det_id = 0
                    oclstock.cmp_id = Val(Session("cmp_id"))
                    oclstock.Location_id = rdlocation.SelectedValue
                    'oclstock.code = txtCode.Text.Trim()
                    oclstock.product_id = Val(row("Product_id"))
                    oclstock.product_group_id = Val(row("product_group"))
                    oclstock.ip_address = Request.UserHostAddress
                    oclstock.login_id = Val(Session("login_id"))
                    oclstock.stock_receive_date = txtdate.SelectedDate
                    oclstock.total_stock = Val(row("total_stock"))
                    oclstock.stock_detail = (ddlstockdetails.SelectedValue)
                    oclstock.total_unit_id = Val(row("unit_id"))
                    oclstock.Stockon_Hand_Qty = Val(row("stock_on_hand"))
                    oclstock.Count_Qty = Val(row("count_quantity"))
                    oclstock.Stk_Chg_rec_id = Stk_Chg_rec_id

                    If Session("Stk_Chg_rec_id") = Nothing Then
                        oclstock.stk_det_id = 0
                        oclstock.Ins_stock_detail()
                        Response.Redirect("stock_detail_list.aspx", False)
                        Session("Success") = "Record inserted successfully"
                        Session("Stk_Chg_rec_id") = Nothing
                    Else
                        oclstock.stk_det_id = Session("Stk_Chg_rec_id")
                        oclstock.Upd_stock_detail()
                        Response.Redirect("stock_detail_list.aspx", False)
                        Session("Success") = "Record updated successfully"
                        Session("Stk_Chg_rec_id") = Nothing

                        'oclstock.stk_det_id = Val(row("stk_det_id"))
                        'oclstock.Upd_stock_detail()
                        'Response.Redirect("stock_detail_list.aspx", False)
                        'Session("Success") = "Record updated successfully"
                    End If
                Next
            Else
                Response.Redirect("stock_detail_list.aspx", False)
                Session("Success") = "Record inserted successfully"
                Session("Stk_Chg_rec_id") = Nothing
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:Insert_Stk_detail:" + ex.Message)
        End Try
    End Sub

    Protected Sub Insert_Stk_detail_Edit()
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim Stk_Chg_rec_id As String

            If Session("Stk_Chg_rec_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 0

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.InsUpdDel_ChgRec()
            Else
                oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 0

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.Upd_ChgRec()
            End If

            'oclstock.stock_receive_date = txtdate.SelectedDate
            'oclstock.Location_id = Val(rdlocation.SelectedValue)
            'oclstock.login_id = Val(Session("login_id"))
            'oclstock.cmp_id = Val(Session("cmp_id"))
            'oclstock.ip_address = Request.UserHostAddress
            'oclstock.receipt_number = txtreceipt.Text
            'oclstock.stock_detail = ddlstockdetails.SelectedValue
            'Stk_Chg_rec_id = oclstock.InsUpdDel_ChgRec()

            'Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            'Dim dt_Edit As DataTable = DirectCast(ViewState("Edit_Dt"), DataTable)
            'Dim dt_NewEdit As DataTable = DirectCast(ViewState("NewEdit_Dt"), DataTable)

            'Dim rows_to_remove1 As New List(Of DataRow)()

            'For Each row1 As DataRow In dt.Rows
            '    For Each row2 As DataRow In dt_Edit.Rows
            '        If Val(row1("row_id").ToString()) = Val(row2("row_id").ToString()) Then
            '            rows_to_remove1.Add(row1)
            '        End If

            '    Next
            'Next

            'For Each row As DataRow In rows_to_remove1
            '    dt.Rows.Remove(row)
            '    dt.AcceptChanges()
            'Next

            For Each row As DataRow In dt.Rows
                oclstock.stk_det_id = Val(row("stk_det_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = rdlocation.SelectedValue
                'oclstock.code = txtCode.Text.Trim()
                oclstock.product_id = Val(row("Product_id"))
                oclstock.product_group_id = Val(row("product_group"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.login_id = Val(Session("login_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.total_stock = Val(row("total_stock"))
                oclstock.stock_detail = (ddlstockdetails.SelectedValue)
                oclstock.total_unit_id = Val(row("unit_id"))
                oclstock.Stockon_Hand_Qty = Val(row("stock_on_hand"))
                oclstock.Count_Qty = Val(row("count_quantity"))
                'Dim base_size_str As String
                'base_size_str = row("base_size")

                'Dim base_size As Double
                'If InStr(base_size_str, "Ml") > 0 Then

                '    base_size = Val(Replace(base_size_str, "Ml", ""))

                '    oclstock.Count_Qty = Val(row("count_quantity")) * base_size
                'Else

                '    oclstock.Count_Qty = Val(row("count_quantity"))
                'End If
                oclstock.Stk_Chg_rec_id = Stk_Chg_rec_id

                If Val(row("stk_det_id")) = 0 Then
                    oclstock.stk_det_id = 0
                    oclstock.Ins_stock_detail()

                Else
                    'If row("stk_det_id") Is Nothing Then
                    '    oclstock.stk_det_id = 0
                    '    oclstock.Ins_stock_detail()
                    '    Response.Redirect("stock_detail_list.aspx", False)
                    '    Session("Success") = "Record updated successfully"
                    'Else
                    oclstock.stk_det_id = Val(row("stk_det_id"))
                    oclstock.Upd_stock_detail()

                    'End If
                    'oclstock.stk_det_id = Val(row("stk_det_id"))
                    'oclstock.Upd_stock_detail()
                    'Response.Redirect("stock_detail_list.aspx", False)
                    'Session("Success") = "Record updated successfully"
                End If
            Next

            If Session("Stk_Chg_rec_id") = Nothing Then
                Session("Success") = "Record inserted successfully"
                Response.Redirect("stock_detail_list.aspx", False)
            Else
                Session("Success") = "Record updated successfully"
                Response.Redirect("stock_detail_list.aspx", False)
            End If

            'If dt_NewEdit IsNot Nothing Then
            '    Dim rows_to_remove As New List(Of DataRow)()

            '    For Each row1 As DataRow In dt_NewEdit.Rows
            '        For Each row2 As DataRow In dt_Edit.Rows
            '            If Val(row1("row_id").ToString()) = Val(row2("row_id").ToString()) Then
            '                'rows_to_remove.Add(row1)
            '            Else
            '                rows_to_remove.Add(row1)
            '            End If

            '        Next
            '    Next

            '    For Each row As DataRow In rows_to_remove
            '        dt_NewEdit.Rows.Remove(row)
            '        dt_NewEdit.AcceptChanges()
            '    Next

            'Session("Stk_Chg_rec_id") = Nothing

            '    For Each row As DataRow In dt.Rows
            '        oclstock.stk_det_id = 0
            '        oclstock.cmp_id = Val(Session("cmp_id"))
            '        oclstock.Location_id = rdlocation.SelectedValue
            '        'oclstock.code = txtCode.Text.Trim()
            '        oclstock.product_id = Val(row("Product_id"))
            '        oclstock.product_group_id = Val(row("product_group"))
            '        oclstock.ip_address = Request.UserHostAddress
            '        oclstock.login_id = Val(Session("login_id"))
            '        oclstock.stock_receive_date = txtdate.SelectedDate
            '        oclstock.total_stock = Val(row("total_stock"))
            '        oclstock.stock_detail = (ddlstockdetails.SelectedValue)
            '        oclstock.total_unit_id = Val(row("Total_unit_id"))
            '        oclstock.Stockon_Hand_Qty = Val(row("stock_on_hand"))
            '        oclstock.Count_Qty = Val(row("count_quantity"))
            '        oclstock.Stk_Chg_rec_id = Stk_Chg_rec_id

            '        If Session("Stk_Chg_rec_id") = Nothing Then
            '            oclstock.stk_det_id = 0
            '            oclstock.Ins_stock_detail()
            '            Response.Redirect("stock_detail_list.aspx", False)
            '            Session("Success") = "Record inserted successfully"
            '        End If
            '    Next
            'End If

        Catch ex As Exception
            LogHelper.Error("Stock_Detail:Insert_Stk_detail:" + ex.Message)
        End Try
    End Sub

    Protected Sub Insert_Stk_detail_submit_Add()
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim Stk_Chg_rec_id As String

            If Session("Stk_Chg_rec_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 1

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.InsUpdDel_ChgRec()
            Else
                oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 1

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.Upd_ChgRec()
            End If



            'Dim dt As DataTable = DirectCast(ViewState("oldProduct_Data_Change"), DataTable)
            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)


            For Each row As DataRow In dt.Rows
                oclstock.stk_det_id = 0
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = rdlocation.SelectedValue
                'oclstock.code = txtCode.Text.Trim()
                oclstock.product_id = Val(row("Product_id"))
                oclstock.product_group_id = Val(row("product_group"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.login_id = Val(Session("login_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.total_stock = Val(row("total_stock"))
                oclstock.stock_detail = (ddlstockdetails.SelectedValue)
                oclstock.total_unit_id = Val(row("unit_id"))
                oclstock.Stockon_Hand_Qty = Val(row("stock_on_hand"))
                oclstock.Count_Qty = Val(row("count_quantity"))
                oclstock.Stk_Chg_rec_id = Stk_Chg_rec_id

                If Val(row("stk_det_id")) = 0 Then
                    oclstock.stk_det_id = 0
                    oclstock.Ins_stock_detail()

                Else

                    oclstock.stk_det_id = Val(row("stk_det_id"))
                    oclstock.Upd_stock_detail()


                End If
            Next

            If Session("Stk_Chg_rec_id") = Nothing Then
                Session("Success") = "Record inserted successfully"
                Response.Redirect("stock_detail_list.aspx", False)
            Else
                Session("Success") = "Record updated successfully"
                Response.Redirect("stock_detail_list.aspx", False)
            End If


        Catch ex As Exception
            LogHelper.Error("Stock_Detail:Insert_Stk_detail:" + ex.Message)
        End Try
    End Sub

    Protected Sub Insert_Stk_detail_submit_Edit()
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim Stk_Chg_rec_id As String

            If Session("Stk_Chg_rec_id") = Nothing Then
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 1

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.InsUpdDel_ChgRec()
            Else
                oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                oclstock.login_id = Val(Session("login_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.receipt_number = txtreceipt.Text
                oclstock.stock_detail = ddlstockdetails.SelectedValue
                oclstock.is_finalsubmit = 1

                If (rdStockDesc.Text IsNot "") Then
                    oclstock.stock_Desc = rdStockDesc.Text
                Else
                    oclstock.stock_Desc = ""
                End If

                Stk_Chg_rec_id = oclstock.Upd_ChgRec()
            End If


            Dim dt As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            For Each row As DataRow In dt.Rows
                oclstock.stk_det_id = Val(row("stk_det_id"))
                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = rdlocation.SelectedValue
                'oclstock.code = txtCode.Text.Trim()
                oclstock.product_id = Val(row("Product_id"))
                oclstock.product_group_id = Val(row("product_group"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.login_id = Val(Session("login_id"))
                oclstock.stock_receive_date = txtdate.SelectedDate
                oclstock.total_stock = Val(row("total_stock"))
                oclstock.stock_detail = (ddlstockdetails.SelectedValue)
                oclstock.total_unit_id = Val(row("unit_id"))
                oclstock.Stockon_Hand_Qty = Val(row("stock_on_hand"))
                oclstock.Count_Qty = Val(row("count_quantity"))
                oclstock.Stk_Chg_rec_id = Stk_Chg_rec_id

                If Val(row("stk_det_id")) = 0 Then
                    oclstock.stk_det_id = 0
                    oclstock.Ins_stock_detail()

                Else

                    oclstock.stk_det_id = Val(row("stk_det_id"))
                    oclstock.Upd_stock_detail()


                End If
            Next

            If Session("Stk_Chg_rec_id") = Nothing Then
                Session("Success") = "Record inserted successfully"
                Response.Redirect("stock_detail_list.aspx", False)
            Else
                Session("Success") = "Record updated successfully"
                Response.Redirect("stock_detail_list.aspx", False)
            End If



        Catch ex As Exception
            LogHelper.Error("Stock_Detail:Insert_Stk_detail:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Stock_Detail_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub rdproduct_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles rdproduct.RowCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Size_N_Price(id)
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:rdproduct_RowCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub Delete_Size_N_Price(ByVal id As Integer)
        Try
            'Dim dtsize As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dtsize As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)
            For Each row As DataRow In dtsize.Rows
                If row("row_id") = id Then
                    dtsize.Rows.Remove(row)
                    row.EndEdit()
                    dtsize.AcceptChanges()
                    Exit For
                End If
            Next

            Dim dv As DataView = dtsize.DefaultView
            If dtsize.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdproduct.DataSource = dv
                rdproduct.DataBind()
            Else
                rdproduct.Enabled = True
                rdproduct.Visible = True
                rdproduct.DataSource =  String.Empty
                rdproduct.DataBind()
            End If

            'Dim dtsize1 As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            'For Each row As DataRow In dtsize1.Rows

            '    If row("row_id") = id Then
            '        dtsize1.Rows.Remove(row)
            '        row.EndEdit()
            '        dtsize1.AcceptChanges()
            '        Exit For
            '    End If
            'Next

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            LogHelper.Error("Stock_Detail:Delete_Size_N_Price:" + ex.Message)
        End Try
    End Sub


    Private Sub btn_ProductIngredient_Click(sender As Object, e As EventArgs) Handles btn_ProductIngredient.Click
        Try
            Session("rowcount") = "1"
            Session("isfirsttime") = "0"
            Dim Pro_Id As String = ""
            Dim dt1 As New DataTable
            dt1.Columns.Add("product_id", GetType(Integer))

            Dim dtingredient As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)


            For Each item As GridItem In rdcopyProduct.MasterTableView.GetSelectedItems
                Dim value As String
                Dim dataitem As GridDataItem = CType(item, GridDataItem)
                Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)

                Dim row As DataRow = dt1.NewRow()

                row("product_id") = Val(dataitem.GetDataKeyValue("product_id").ToString())

                Pro_Id += dataitem.GetDataKeyValue("product_id").ToString() + ","
                dt1.Rows.Add(row)

            Next
            dt1.AcceptChanges()

            If dt1.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else
                Dim rowTotal As Integer = 0



                oclsProduct.Pro_Id = Pro_Id
                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.row_id = dtingredient.Rows.Count
                Dim dt As DataTable = oclsProduct.Get_Product_For_Stock_change()



                txt_Name.Text = dt.Rows(0)("product").ToString()
                hf_Product_Id.Value = dt.Rows(0)("product_id").ToString()
                hf_Category_Id.Value = dt.Rows(0)("Category_id").ToString()
                hf_Unit_Id.Value = dt.Rows(0)("Unit_id").ToString()
                hf_Row_Id.Value = 0
                txt_Qty.Text = Val(dt.Rows(0)("Total_Stock").ToString())
                txt_baseunit.Text = dt.Rows(0)("base_Size").ToString()

                ViewState("Stock_Data_Change_New") = dt

                If dt.Rows.Count = 1 Then
                    btn_Save.Visible = True
                    btn_Next.Visible = False
                End If

                dv_prodctlist.Visible = False
                dv_wizard.Visible = True
                txt_Qty.Text = ""
                txt_Qty.Focus()

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "check();", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "check(); alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btn_ProductIngrdientCancel_Click(sender As Object, e As EventArgs) Handles btn_ProductIngrdientCancel.Click
        Try
            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception

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
            oclsProduct.Location_id = Val(rdlocation.SelectedValue)
            Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
            Else
                rdcopyProduct.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:ReBindGrid():" + ex.Message)
        End Try
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Try
            dv_prodctlist.Visible = True
            dv_wizard.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_Next_Click(sender As Object, e As EventArgs) Handles btn_Next.Click
        Try

            Dim rowcount As Int32 = Val(hf_Row_Id.Value.ToString())

            Dim dtStock As DataTable = DirectCast(ViewState("Stock_Data_Change_New"), DataTable)

            If ddlstockdetails.SelectedValue = 3 Then
                dtStock.Rows(rowcount)("count_quantity") = Val(txt_Qty.Text)
            Else
                dtStock.Rows(rowcount)("Total_Stock") = Val(txt_Qty.Text)
            End If

            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.product_id = dtStock.Rows(rowcount)("product_id")
            oclstock.Location_id = Val(rdlocation.SelectedValue)
            Dim dttran As DataTable = oclstock.SelectTranDetail()

            If dttran.Rows.Count > 0 Then
                dtStock.Rows(rowcount)("stock_on_hand") = dttran.Rows(0)("Stock_Closing").Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", "")
            Else
                dtStock.Rows(rowcount)("stock_on_hand") = "0.00"

            End If

            dtStock.AcceptChanges()

            txt_Name.Text = ""
            txt_Qty.Text = ""
            txt_baseunit.Text = ""
            hf_Product_Id.Value = ""
            hf_Category_Id.Value = ""
            hf_Unit_Id.Value = ""

            If dtStock.Rows.Count > rowcount + 1 Then
                rowcount += 1
                txt_Name.Text = dtStock.Rows(rowcount)("product").ToString()
                txt_baseunit.Text = dtStock.Rows(rowcount)("Base_size").ToString()
                hf_Product_Id.Value = dtStock.Rows(rowcount)("product_id").ToString()
                hf_Category_Id.Value = dtStock.Rows(rowcount)("Category_id").ToString()
                hf_Unit_Id.Value = dtStock.Rows(rowcount)("Unit_id").ToString()

                hf_Row_Id.Value = rowcount
                txt_Qty.Text = Val(dtStock.Rows(rowcount)("Total_Stock").ToString())

            End If
            If dtStock.Rows.Count - 1 = rowcount Then
                btn_Save.Visible = True
                btn_Next.Visible = False
            End If
            ViewState("Stock_Data_Change_New") = dtStock
            txt_Qty.Text = ""
            txt_Qty.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "check();", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Try

            Dim rowcount As Int32 = Val(hf_Row_Id.Value.ToString())

            Dim dtStock_Exist As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

            Dim dtStock As DataTable = DirectCast(ViewState("Stock_Data_Change_New"), DataTable)

            If ddlstockdetails.SelectedValue = 3 Then
                dtStock.Rows(rowcount)("count_quantity") = Val(txt_Qty.Text)
            Else
                dtStock.Rows(rowcount)("Total_Stock") = Val(txt_Qty.Text)
            End If
            oclstock.cmp_id = Val(Session("cmp_id"))
            oclstock.product_id = dtStock.Rows(rowcount)("product_id")
            oclstock.Location_id = Val(rdlocation.SelectedValue)
            Dim dttran As DataTable = oclstock.SelectTranDetail()

            If dttran.Rows.Count > 0 Then
                dtStock.Rows(rowcount)("stock_on_hand") = dttran.Rows(0)("Stock_Closing").Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", "")
            Else
                dtStock.Rows(rowcount)("stock_on_hand") = "0.00"

            End If


            dtStock.AcceptChanges()

            txt_Name.Text = ""
            txt_Qty.Text = ""
            txt_baseunit.Text = ""

            hf_Product_Id.Value = ""
            hf_Category_Id.Value = ""
            hf_Unit_Id.Value = ""
            Try
                dtStock_Exist.Merge(dtStock, True, MissingSchemaAction.Ignore)
                'For Each row As DataRow In dtStock.Rows
                '    dtStock_Exist.Rows.Add(row)
                'Next


            Catch ex As Exception
                dtStock_Exist = dtStock
            End Try

            dtStock_Exist.AcceptChanges()
            ViewState("Stock_Data_Change") = dtStock_Exist

            Dim dv As DataView = dtStock_Exist.DefaultView
            If dtStock_Exist.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdproduct.DataSource = dv
                rdproduct.DataBind()
                upWall.Update()
            Else
                rdproduct.Enabled = True
                rdproduct.Visible = True
                rdproduct.DataSource = String.Empty
                rdproduct.DataBind()
                upWall.Update()
            End If

            dv_prodctlist.Visible = True
            dv_wizard.Visible = False
            btn_Save.Visible = False
            btn_Next.Visible = True

            Session("isfirsttime") = "1"
            bindIngredientGrid()
            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)

        Catch ex As Exception
            LogHelper.Error("Product_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If Session("Stk_Chg_rec_id") = Nothing Then
                Insert_Stk_detail_submit_Add()
            Else
                Insert_Stk_detail_submit_Edit()
            End If
            'Insert_Stk_detail_submit()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Stock_Detail:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Private Sub rdlocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rdlocation.SelectedIndexChanged
        Try
            If rdlocation.SelectedIndex = 0 Then

                rdcopyProduct.DataSource = String.Empty
                rdcopyProduct.DataBind()

                up_Pro_Ingredient.Update()

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

                If dt.Rows.Count > 0 Then
                    rdcopyProduct.DataSource = dt
                    rdcopyProduct.DataBind()
                Else
                    rdcopyProduct.DataSource = String.Empty
                    rdcopyProduct.DataBind()
                End If
            Else
                rdcopyProduct.DataSource = String.Empty
                rdcopyProduct.DataBind()

                up_Pro_Ingredient.Update()

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredientLocation()

                If dt.Rows.Count > 0 Then
                    rdcopyProduct.DataSource = dt
                    rdcopyProduct.DataBind()
                Else
                    rdcopyProduct.DataSource = String.Empty
                    rdcopyProduct.DataBind()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Try

            If Val(rdlocation.SelectedValue) = 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Please select location.');", True)

            Else
                Dim fileName As String = "Stock_Take" & System.DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "")

                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclstock.download_stock_detail()

                If dt.Rows.Count > 0 Then
                    grid_view.DataSource = dt
                    grid_view.DataBind()
                Else
                    grid_view.DataSource = Nothing
                    grid_view.DataBind()
                End If

                ExportExcel(grid_view, fileName)


            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Detail:btnDownload_Click" + ex.Message)
        End Try
    End Sub


    Public Sub ExportExcel(ByVal Grv As GridView, ByVal ExclName As String, ByVal Optional downloadTokenValue As String = "")

        Try

            Dim dt As DataTable = CType(Grv.DataSource, DataTable)
            System.Web.HttpContext.Current.Response.ClearContent()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AppendCookie(New HttpCookie("fileDownloadToken", downloadTokenValue))
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", ExclName & ".xls"))
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel"

            Dim sw As StringWriter = New StringWriter()
            Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
            Grv.AllowPaging = False
            Grv.RenderControl(htw)
            System.Web.HttpContext.Current.Response.Write(sw.ToString())
            System.Web.HttpContext.Current.Response.[End]()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail:ExportExcel" + ex.Message)
        End Try
    End Sub


    'Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
    '    Try
    '        If FileUpload1.PostedFile.FileName = "" Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('File is required');", True)
    '            Return
    '        Else
    '            Dim excelPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
    '            FileUpload1.SaveAs(excelPath)
    '            Dim conString As String = String.Empty
    '            Dim storedProc As String = String.Empty
    '            Dim sheet1 As String = String.Empty
    '            Dim extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
    '            Select Case extension
    '                Case ".xls" ' Excel 97-03.
    '                    conString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1';", excelPath)
    '                Case ".xlsx" ' Excel 07 or higher.
    '                    conString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';", excelPath)
    '                Case Else
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", "alert('Invalid file format');", True)
    '                    Return
    '            End Select
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Connection String: {0}');", conString), True)

    '            'Read the Sheet Name.
    '            Dim dtExcelData As New DataTable()
    '            '  conString = String.Format(conString, excelPath)

    '            'Using excel_con As OleDbConnection = New OleDbConnection(conString)
    '            '    excel_con.Open()
    '            '    sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing).Rows(0)("TABLE_NAME").ToString()
    '            '    excel_con.Close()
    '            '    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)
    '            '        oda.Fill(dtExcelData)
    '            '    End Using
    '            'End Using

    '            Try
    '                Using excel_con As New OleDbConnection(conString)
    '                    excel_con.Open()
    '                    Dim schemaTable As DataTable = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
    '                    If schemaTable Is Nothing OrElse schemaTable.Rows.Count = 0 Then
    '                        Throw New Exception("No sheets found in the Excel file.")
    '                    End If
    '                    sheet1 = schemaTable.Rows(0)("TABLE_NAME").ToString()
    '                    excel_con.Close()
    '                End Using
    '            Catch ex As Exception
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Error reading data from Excel sheet: {0}');", ex.Message), True)

    '                Return
    '            End Try


    '            Try
    '                Using excel_con As New OleDbConnection(conString)
    '                    Using oda As New OleDbDataAdapter(String.Format("SELECT * FROM [{0}]", sheet1), excel_con)
    '                        oda.Fill(dtExcelData)
    '                    End Using
    '                End Using
    '            Catch ex As Exception
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Error reading data from Excel sheet: {0}');", ex.Message), True)
    '                Return
    '            End Try


    '            Dim dv As DataView = dtExcelData.DefaultView
    '            dv.RowFilter = String.Format("CONVERT(count_quantity, System.String) <> '0 Qty'")

    '            '---------------------------------------------------------------------------------------------

    '            Session("rowcount") = "1"
    '            Session("isfirsttime") = "0"
    '            Dim Pro_Id As String = ""
    '            Dim dt1 As New DataTable
    '            dt1.Columns.Add("product_id", GetType(Integer))

    '            Dim dtingredient As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

    '            If dv.ToTable.Rows.Count > 0 Then

    '                ViewState("Stock_Data_xl") = dv.ToTable

    '                For index = 0 To dv.ToTable.Rows.Count - 1

    '                    Dim row As DataRow = dt1.NewRow()

    '                    row("product_id") = Val(dv.ToTable.Rows(index)("product_id").ToString())

    '                    Pro_Id += dv.ToTable.Rows(index)("product_id").ToString() + ","
    '                    dt1.Rows.Add(row)

    '                Next

    '                dt1.AcceptChanges()

    '                If dt1.Rows.Count > 0 Then

    '                    Dim rowTotal As Integer = 0


    '                    oclsProduct.Pro_Id = Pro_Id
    '                    oclsProduct.cmp_id = Val(Session("cmp_id"))
    '                    oclsProduct.row_id = dtingredient.Rows.Count
    '                    Dim dt As DataTable = oclsProduct.Get_Product_For_Stock_change_import()

    '                    ViewState("Stock_Data_Change_New") = dt


    '                    '------------------------------------------------------------------------------------

    '                    'Dim rowcount As Int32 = Val(hf_Row_Id.Value.ToString())

    '                    Dim dtStock_Exist As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

    '                    Dim dtStock As DataTable = DirectCast(ViewState("Stock_Data_Change_New"), DataTable)

    '                    If dtStock.Rows.Count > 0 Then

    '                        For s = 0 To dtStock.Rows.Count - 1
    '                            For index = 0 To dv.ToTable.Rows.Count - 1

    '                                If Val(dtStock.Rows(s)("Product_id")) = Val(dv.ToTable.Rows(index)("Product_id")) Then

    '                                    If ddlstockdetails.SelectedValue = 3 Then
    '                                        dtStock.Rows(s)("count_quantity") = Val(dv.ToTable.Rows(index)("count_quantity"))
    '                                    Else
    '                                        dtStock.Rows(s)("Total_Stock") = Val(dv.ToTable.Rows(index)("count_quantity"))
    '                                    End If

    '                                    oclstock.cmp_id = Val(Session("cmp_id"))
    '                                    oclstock.product_id = Val(dv.ToTable.Rows(index)("Product_id"))
    '                                    oclstock.Location_id = Val(rdlocation.SelectedValue)
    '                                    Dim dttran As DataTable = oclstock.SelectTranDetail()
    '                                    If dttran.Rows.Count > 0 Then
    '                                        'txtstkonhand.Text = dttran.Rows(0)("Stock_Closing")
    '                                        'txtstkonhand.ReadOnly = True
    '                                        dtStock.Rows(s)("stock_on_hand") = dttran.Rows(0)("Stock_Closing").Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", "")

    '                                    Else
    '                                        dtStock.Rows(s)("stock_on_hand") = ""
    '                                    End If

    '                                    'dtStock.Rows(s)("stock_on_hand") = dtExcelData.Rows(index)("stock_on_hand").Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", "")

    '                                    dtStock.AcceptChanges()

    '                                    Exit For
    '                                End If

    '                            Next
    '                        Next

    '                    End If


    '                    Try
    '                        dtStock_Exist.Merge(dtStock, True, MissingSchemaAction.Ignore)

    '                    Catch ex As Exception
    '                        dtStock_Exist = dtStock
    '                    End Try

    '                    dtStock_Exist.AcceptChanges()
    '                    ViewState("Stock_Data_Change") = dtStock_Exist

    '                    Dim dv1 As DataView = dtStock_Exist.DefaultView

    '                    If dtStock_Exist.Rows.Count > 0 And dv1.ToTable.Rows.Count > 0 Then
    '                        rdproduct.DataSource = dv1
    '                        rdproduct.DataBind()

    '                    Else
    '                        rdproduct.Enabled = True
    '                        rdproduct.Visible = True
    '                        rdproduct.DataSource = String.Empty
    '                        rdproduct.DataBind()

    '                    End If

    '                    Session("isfirsttime") = "1"
    '                    ' bindIngredientGrid()

    '                End If
    '            End If

    '        End If
    '    Catch ex As Exception
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
    '    End Try
    'End Sub

    'Private Function IsProviderInstalled(provider As String) As Boolean
    '    Dim isInstalled As Boolean = False
    '    Try
    '        Dim factories As DataTable = DbProviderFactories.GetFactoryClasses()
    '        For Each row As DataRow In factories.Rows
    '            If row("InvariantName").ToString() = provider Then
    '                isInstalled = True
    '                Exit For
    '            End If
    '        Next
    '    Catch ex As Exception
    '        ' Handle the exception if needed
    '    End Try
    '    Return isInstalled
    'End Function
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If FileUpload1.PostedFile.FileName = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('File is required');", True)
                Return
            Else
                Dim excelPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
                FileUpload1.SaveAs(excelPath)
                Dim conString As String = String.Empty
                Dim storedProc As String = String.Empty
                Dim sheet1 As String = String.Empty
                Dim extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

                Select Case extension
                    Case ".xls" 'Excel 97-03.
                        storedProc = "spx_ImportFromExcel03"
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelPath & ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';"
                    Case ".xlsx" 'Excel 07 or higher.
                        storedProc = "spx_ImportFromExcel07"
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & excelPath & ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';"
                    Case Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", "alert('Invalid file format');", True)
                        Return
                End Select


                'Select Case extension
                '    Case ".xls"
                '        'Excel 97-03
                '        conString = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                '        Exit Select
                '    Case ".xlsx"
                '        'Excel 07
                '        conString = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                '        Exit Select
                'End Select


                'Select Case extension
                '    Case ".xls" ' Excel 97-03.
                '        storedProc = "spx_ImportFromExcel03"
                '        conString = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                '    Case ".xlsx" ' Excel 07 or higher.
                '        storedProc = "spx_ImportFromExcel07"
                '        conString = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                '    Case Else
                '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", "alert('Invalid file format');", True)
                '        Return
                'End Select


                'If Not IsProviderInstalled("Microsoft.ACE.OLEDB") Then
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", "alert('Microsoft.ACE.OLEDB.12.0 provider is not installed.');", True)

                'End If

                ' Verify the connection string
                conString = String.Format(conString, excelPath)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Connection String: {0}');", conString), True)


                ' Read the Sheet Name and Data
                Dim dtExcelData As New DataTable()
                Try
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", "alert('Initializing connection...');", True)
                    Using excel_con As New OleDbConnection(conString)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", "alert('Opening connection...');", True)
                        excel_con.Open()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", "alert('Connection opened.');", True)

                        Dim schemaTable As DataTable = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                        If schemaTable Is Nothing OrElse schemaTable.Rows.Count = 0 Then
                            Throw New Exception("No sheets found in the Excel file.")
                        End If
                        sheet1 = schemaTable.Rows(0)("TABLE_NAME").ToString()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Sheet Name: {0}');", sheet1), True)
                    End Using
                Catch ex As OleDbException
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('OLEDB Error: {0}');", ex.Message), True)
                    Return
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Error reading schema from Excel: {0}');", ex.Message), True)
                    Return
                End Try

                Try
                    Using excel_con As New OleDbConnection(conString)
                        Using oda As New OleDbDataAdapter(String.Format("SELECT * FROM [{0}]", sheet1), excel_con)
                            oda.Fill(dtExcelData)
                        End Using
                    End Using
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Rows in dtExcelData: {0}');", dtExcelData.Rows.Count), True)
                Catch ex As OleDbException
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('OLEDB Error: {0}');", ex.Message), True)
                    Return
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "test", String.Format("alert('Error reading data from Excel sheet: {0}');", ex.Message), True)
                    Return
                End Try
                Dim dv As DataView = dtExcelData.DefaultView
                dv.RowFilter = String.Format("CONVERT(count_quantity, System.String) <> '0 Qty'")

                '---------------------------------------------------------------------------------------------

                Session("rowcount") = "1"
                Session("isfirsttime") = "0"
                Dim Pro_Id As String = ""
                Dim dt1 As New DataTable
                dt1.Columns.Add("product_id", GetType(Integer))

                Dim dtingredient As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

                If dv.ToTable.Rows.Count > 0 Then

                    ViewState("Stock_Data_xl") = dv.ToTable

                    For index = 0 To dv.ToTable.Rows.Count - 1

                        Dim row As DataRow = dt1.NewRow()

                        row("product_id") = Val(dv.ToTable.Rows(index)("product_id").ToString())

                        Pro_Id += dv.ToTable.Rows(index)("product_id").ToString() + ","
                        dt1.Rows.Add(row)

                    Next

                    dt1.AcceptChanges()

                    If dt1.Rows.Count > 0 Then

                        Dim rowTotal As Integer = 0


                        oclsProduct.Pro_Id = Pro_Id
                        oclsProduct.cmp_id = Val(Session("cmp_id"))
                        oclsProduct.row_id = dtingredient.Rows.Count
                        Dim dt As DataTable = oclsProduct.Get_Product_For_Stock_change_import()

                        ViewState("Stock_Data_Change_New") = dt


                        '------------------------------------------------------------------------------------

                        'Dim rowcount As Int32 = Val(hf_Row_Id.Value.ToString())

                        Dim dtStock_Exist As DataTable = DirectCast(ViewState("Stock_Data_Change"), DataTable)

                        Dim dtStock As DataTable = DirectCast(ViewState("Stock_Data_Change_New"), DataTable)

                        If dtStock.Rows.Count > 0 Then

                            For s = 0 To dtStock.Rows.Count - 1
                                For index = 0 To dv.ToTable.Rows.Count - 1

                                    If Val(dtStock.Rows(s)("Product_id")) = Val(dv.ToTable.Rows(index)("Product_id")) Then

                                        If ddlstockdetails.SelectedValue = 3 Then
                                            dtStock.Rows(s)("count_quantity") = Val(dv.ToTable.Rows(index)("count_quantity"))
                                        Else
                                            dtStock.Rows(s)("Total_Stock") = Val(dv.ToTable.Rows(index)("count_quantity"))
                                        End If

                                        oclstock.cmp_id = Val(Session("cmp_id"))
                                        oclstock.product_id = Val(dv.ToTable.Rows(index)("Product_id"))
                                        oclstock.Location_id = Val(rdlocation.SelectedValue)
                                        Dim dttran As DataTable = oclstock.SelectTranDetail()
                                        If dttran.Rows.Count > 0 Then
                                            'txtstkonhand.Text = dttran.Rows(0)("Stock_Closing")
                                            'txtstkonhand.ReadOnly = True
                                            dtStock.Rows(s)("stock_on_hand") = dttran.Rows(0)("Stock_Closing").Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", "")

                                        Else
                                            dtStock.Rows(s)("stock_on_hand") = ""
                                        End If

                                        'dtStock.Rows(s)("stock_on_hand") = dtExcelData.Rows(index)("stock_on_hand").Replace("kg", "").Replace("Qty", "").Replace("ML", "").Replace("Gm", "")

                                        dtStock.AcceptChanges()

                                        Exit For
                                    End If

                                Next
                            Next

                        End If


                        Try
                            dtStock_Exist.Merge(dtStock, True, MissingSchemaAction.Ignore)

                        Catch ex As Exception
                            dtStock_Exist = dtStock
                        End Try

                        dtStock_Exist.AcceptChanges()
                        ViewState("Stock_Data_Change") = dtStock_Exist

                        Dim dv1 As DataView = dtStock_Exist.DefaultView

                        If dtStock_Exist.Rows.Count > 0 And dv1.ToTable.Rows.Count > 0 Then
                            rdproduct.DataSource = dv1
                            rdproduct.DataBind()

                        Else
                            rdproduct.Enabled = True
                            rdproduct.Visible = True
                            rdproduct.DataSource = String.Empty
                            rdproduct.DataBind()

                        End If

                        Session("isfirsttime") = "1"
                        ' bindIngredientGrid()

                    End If
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
        End Try
    End Sub

    Private Function WorksheetToDataTable(worksheet As ExcelWorksheet) As DataTable
        Dim dt As New DataTable()
        Dim hasHeader As Boolean = True ' adjust if needed

        For Each firstRowCell In worksheet.Cells(1, 1, 1, worksheet.Dimension.End.Column)
            dt.Columns.Add(If(hasHeader, firstRowCell.Text, String.Format("Column {0}", firstRowCell.Start.Column)))
        Next

        Dim startRow As Integer = If(hasHeader, 2, 1)
        For rowNum As Integer = startRow To worksheet.Dimension.End.Row
            Dim wsRow = worksheet.Cells(rowNum, 1, rowNum, worksheet.Dimension.End.Column)
            Dim row As DataRow = dt.NewRow()
            For Each cell In wsRow
                row(cell.Start.Column - 1) = cell.Text
            Next
            dt.Rows.Add(row)
        Next

        Return dt
    End Function
    Protected Sub searchBox_TextChanged(sender As Object, e As EventArgs)
        FilterGridView(SearchBox.Text.ToLower())
    End Sub
    'Protected Sub txtSearchProductGroup_TextChanged(sender As Object, e As EventArgs)
    '    FilterGridView()
    'End Sub

    'Protected Sub txtSearchProduct_TextChanged(sender As Object, e As EventArgs)
    '    FilterGridView()
    'End Sub
    Private Sub FilterGridView(searchText As String)

        oclstock.cmp_id = Val(Session("cmp_id"))
        oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
        Dim dataTable As DataTable = oclstock.SelecStockDetail_Edit()

        ' Filter the DataTable based on the search text
        Dim filteredRows As DataRow() = dataTable.Select("Product_group LIKE '%" & searchText & "%' OR Product_id LIKE '%" & searchText & "%' OR Base_size LIKE '%" & searchText & "%' OR stock_on_hand LIKE '%" & searchText & "%' OR Total_Stock LIKE '%" & searchText & "%' OR count_quantity LIKE '%" & searchText & "%'")

        ' Bind the filtered data to the GridView
        Dim filteredDataTable As DataTable = dataTable.Clone()
        For Each row As DataRow In filteredRows
            filteredDataTable.ImportRow(row)
        Next
        rdproduct.DataSource = filteredDataTable
        rdproduct.DataBind()
    End Sub

    Protected Sub SearchBox_TextChanged1(sender As Object, e As EventArgs)
        Dim searchText As String = SearchBox.Text.ToLower()
        For Each row As GridViewRow In rdproduct.Rows
            Dim productGroup As String = TryCast(row.FindControl("ddlproductgroup"), DropDownList).SelectedItem.Text.ToLower()
            Dim product As String = TryCast(row.FindControl("ddlproduct"), DropDownList).SelectedItem.Text.ToLower()
            Dim baseUnit As String = TryCast(row.FindControl("baseunit"), TextBox).Text.ToLower()
            '  Dim stockOnHand As String = TryCast(row.FindControl("txtbasesize"), TextBox).Text.ToLower()
            Dim quantity As String = TryCast(row.FindControl("txttotalStock"), TextBox).Text.ToLower()
            Dim countQuantity As String = TryCast(row.FindControl("txtcountquantity"), TextBox).Text.ToLower()

            If productGroup.Contains(searchText) OrElse product.Contains(searchText) OrElse baseUnit.Contains(searchText) OrElse quantity.Contains(searchText) OrElse countQuantity.Contains(searchText) Then   'OrElse stockOnHand.Contains(searchText)
                row.Visible = True
            Else
                row.Visible = False
            End If
        Next
    End Sub
    'Protected Sub FilterGridView(sender As Object, e As EventArgs)
    '    ' Get the search text from each TextBox
    '    Dim searchProductGroup As String = TryCast(rdproduct.HeaderRow.FindControl("txtSearchProductGroup"), TextBox).Text.ToLower()
    '    Dim searchProduct As String = TryCast(rdproduct.HeaderRow.FindControl("txtSearchProduct"), TextBox).Text.ToLower()
    '    ' Add similar lines for other search boxes

    '    oclstock.cmp_id = Val(Session("cmp_id"))
    '    oclstock.Stk_Chg_rec_id = Val(Session("Stk_Chg_rec_id"))
    '    Dim dataTable As DataTable = oclstock.SelecStockDetail_Edit()

    '    ' Build the filter expression based on the search text
    '    Dim filterExpression As String = "1 = 1" ' This ensures that the filter string is always valid
    '    If Not String.IsNullOrEmpty(searchProductGroup) Then
    '        filterExpression &= " AND Product_group LIKE '%" & searchProductGroup & "%'"
    '    End If
    '    If Not String.IsNullOrEmpty(searchProduct) Then
    '        filterExpression &= " AND Product_id LIKE '%" & searchProduct & "%'"
    '    End If
    '    ' Add similar conditions for other search fields

    '    ' Filter the DataTable based on the filter expression
    '    Dim filteredRows As DataRow() = dataTable.Select(filterExpression)

    '    ' Bind the filtered data to the GridView
    '    Dim filteredDataTable As DataTable = dataTable.Clone()
    '    For Each row As DataRow In filteredRows
    '        filteredDataTable.ImportRow(row)
    '    Next
    '    rdproduct.DataSource = filteredDataTable
    '    rdproduct.DataBind()
    'End Sub
End Class


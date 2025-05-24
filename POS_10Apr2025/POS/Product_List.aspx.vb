Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports Telerik.Web.UI
'Imports Telerik.Reporting

Partial Class Product_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProduct As New Controller_clsProduct
    Dim oclsCategory As Controller_clsCategory = New Controller_clsCategory()
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Product_List"
    Dim oclsLocation As Controller_clsLocation = New Controller_clsLocation()
    Dim dtSize As DataTable
    Dim dtCondiment As DataTable
    Dim dtBarcodeDetails As DataTable
    Dim dtPrinter As DataTable



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try




            If Not IsPostBack Then
                hdsearchvalueAfterEdit.Value = Session("JquerySearchFilter")
                If Not Session("success") = Nothing Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                ElseIf Not Session("Update") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("Update").ToString() + "');", True)
                    Session("Update") = Nothing
                End If
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                    ViewState("add") = 1
                    ViewState("edit") = 1
                    ViewState("delete") = 1
                Else
                    ViewState("view") = 0
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divPGroup.Visible = True
                Else
                    divPGroup.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
                bindAllLocation()

            End If
            If Session("product_type") Is DBNull.Value OrElse String.IsNullOrEmpty(Session("product_type")) Then
                Session("product_type") = 0
            End If
            If Session("product_type") = "2" Then
                lnkchangesize.Visible = False
                lnkChangeProductGroup.Visible = False
                lnkgrp.Visible = True
                lnkdep.Visible = True
            Else
                lnkChangeProductGroup.Visible = True
                lnkchangesize.Visible = True
                lnkgrp.Visible = False
                lnkdep.Visible = False

            End If

        Catch ex As Exception
            LogHelper.Error("Product_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub bindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))


            If chkActive.Checked = True Then
                oclsProduct.active = 1
            Else
                oclsProduct.active = 0
            End If


            Dim dt As DataTable = oclsProduct.SelectAll()


            oSessionManager.ResetSessionDT(Page_Name.ToString())

            If dt.Rows.Count > 0 Then
                rdProduct.DataSource = dt
                rdProduct.DataBind()
            Else
                rdProduct.DataSource = String.Empty
                rdProduct.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_List:bindGrid:" + ex.Message)
        End Try

    End Sub

    Protected Sub bindAllLocation()
        Dim str As String = ""
        oclsLocation.cmp_id = Val(Session("cmp_id"))
        Dim dtMachine As DataTable = oclsLocation.SelectLocationByCmp()

        For index = 0 To dtMachine.Rows.Count - 1

            Dim value As String

            value = dtMachine.Rows(index)("Location_id").ToString()

            If str.ToString = "" Then
                str = value
            Else
                str = str & "#" + value.ToString()
            End If

        Next
        Session("copy_product_Location_Id") = str.ToString()
    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Product Master"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If
            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If

            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Product_List:RoleCheck:" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try


            If Not IsPostBack Then
                Dim strVal As String = ""
                If HttpContext.Current.Session(DefStr + Page_Name) Is Nothing Then

                    oSessionManager.CreateSessionDT(Page_Name.ToString, strVal.ToString)
                Else
                    Dim FilterExpression As String = ""
                    For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
                        If row("FIN").ToString() <> "" And row("VAL").ToString() <> "" Then
                            Dim arr As Array = row("VAL").ToString().Split("#")
                            If Not [String].IsNullOrEmpty(FilterExpression) Then
                                FilterExpression += " AND "
                            End If
                            If arr.Length - 1 = 0 Then
                                Dim a As Integer = 0
                                Try
                                    DateTime.Parse(row("VAL"))
                                Catch ex As Exception
                                    a = 1
                                End Try

                                If a = 1 Then
                                    FilterExpression += "([" + row("FIN").ToString() + "] Like '%" + row("VAL").ToString() + "%')"
                                Else
                                    FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL") + "' )"
                                End If
                            Else
                                If row("VAL").ToString.Contains("01/01/1900") Then
                                    FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL").ToString().Replace("01/01/1900", "").Replace("#", "") + "' )"
                                Else
                                    FilterExpression += "(([" + row("FIN").ToString() + "] >= '" + arr(0) + "') AND ([" + row("FIN").ToString() + "] <= '" + arr(1) + "'))"
                                End If
                            End If
                        End If
                    Next


                End If

            End If

        Catch ex As Exception
            LogHelper.Error("Employee_list:Page_LoadComplete:" + ex.Message)
        End Try
    End Sub


    Protected Sub deleteProduct(ByVal id As Integer, ByVal del_status As String)
        Try
            oclsProduct.product_id = Val(id)
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.Category_id = 0
            oclsProduct.key_map_id = 0
            oclsProduct.code = ""
            oclsProduct.name = ""
            oclsProduct.price = 0
            oclsProduct.barcode = ""
            oclsProduct.description = ""
            oclsProduct.Is_active = 0
            oclsProduct.Ip_address = ""
            oclsProduct.login_id = 0
            oclsProduct.department_id = 0
            oclsProduct.course_id = 0
            oclsProduct.list = 0
            oclsProduct.Tax_id = 0
            oclsProduct.Actual_Price = 0
            oclsProduct.Tax = 0
            oclsProduct.printer_id = ""
            oclsProduct.machine_id = 0
            oclsProduct.other_id = ""
            oclsProduct.other_size = ""
            oclsProduct.Is_Ingredient = 0
            oclsProduct.Is_Condiment = 0
            oclsProduct.Tran_Type = del_status
            oclsProduct.Base = 0
            oclsProduct.Unit_id = 0
            oclsProduct.size_zero = 0
            oclsProduct.is_stock = 0
            oclsProduct.Cloak_Room = 0
            oclsProduct.is_DanceVoucher = 0
            oclsProduct.Is_PriceOnScaleWeight = 0
            oclsProduct.IsHouse = 0
            oclsProduct.IsPkgProduct = 0
            oclsProduct.SortingNo = 0
            oclsProduct.IsAdditionalTax = 0
            oclsProduct.ForKiosk = 0

            oclsProduct.Delete()

            Session("Success") = "Record deleted successfully"
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)
            Response.Redirect("Product_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_List:deleteProduct:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("product_id") = Nothing
            Response.Redirect("Product_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkgrp_Click(sender As Object, e As EventArgs) Handles lnkgrp.Click
        Try
            Session("product_id") = Nothing
            Response.Redirect("Product_Group_Main_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkdep_Click(sender As Object, e As EventArgs) Handles lnkdep.Click
        Try
            Session("product_id") = Nothing
            Response.Redirect("Department_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub



    Public Sub ReBindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.SelectAll()

            If dt.Rows.Count > 0 Then
                rdProduct.DataSource = dt
            Else
                rdProduct.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Product_List:ReBindGrid():" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkCopy_Click(sender As Object, e As EventArgs) Handles lnkCopy.Click
        Try
            Session("product_id") = Nothing
            Response.Redirect("Copy_Product_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub lnkChangeProductGroup_Click(sender As Object, e As EventArgs) Handles lnkChangeProductGroup.Click
        Try
            Response.Redirect("Change_Product_Group_List.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
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
                        conString = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                    Case ".xlsx" 'Excel 07 or higher.
                        storedProc = "spx_ImportFromExcel07"
                        conString = ConfigurationManager.ConnectionStrings("Excel07+ConString").ConnectionString
                End Select

                'Read the Sheet Name.
                Dim dtExcelData As New DataTable()
                conString = String.Format(conString, excelPath)
                Using excel_con As OleDbConnection = New OleDbConnection(conString)
                    excel_con.Open()
                    sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing).Rows(0)("TABLE_NAME").ToString()
                    excel_con.Close()
                    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)
                        oda.Fill(dtExcelData)
                    End Using
                End Using

                ViewState("Excel_Data") = dtExcelData

                Dim names As List(Of String) = New List(Of String)

                For Each column As DataColumn In dtExcelData.Columns
                    names.Add(column.ColumnName)
                Next

                ddlproductGroupCategory.DataSource = names.ToArray()
                ddlproductGroupCategory.DataBind()
                ddlproductGroupCategory.Items.Insert("0", "--select--")

                ddlproductGroup.DataSource = names.ToArray()
                ddlproductGroup.DataBind()
                ddlproductGroup.Items.Insert("0", "--select--")

                ddlproductName.DataSource = names.ToArray()
                ddlproductName.DataBind()
                ddlproductName.Items.Insert("0", "--select--")

                ddlPrice.DataSource = names.ToArray()
                ddlPrice.DataBind()
                ddlPrice.Items.Insert("0", "--select--")

                ddlUnit.DataSource = names.ToArray()
                ddlUnit.DataBind()
                ddlUnit.Items.Insert("0", "--select--")
                dv_Mapping.Visible = True
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            If ddlproductGroup.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Product Group is required');", True)
                Return
            ElseIf ddlproductGroupCategory.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Product Group Category is required');", True)
                Return
            ElseIf ddlproductName.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Product Name is required');", True)
                Return
            ElseIf ddlPrice.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Price is required');", True)
                Return
            ElseIf ddlUnit.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Unit is required');", True)
                Return
            Else
                Dim dt1 As DataTable = DirectCast(ViewState("Excel_Data"), DataTable)

                For Each row As DataRow In dt1.Rows
                    If row(ddlproductGroup.SelectedValue).ToString IsNot "" Then
                        oclsProduct.cmp_id = Val(Session("cmp_id"))
                        oclsProduct.MainCategoryName = row(ddlproductGroupCategory.SelectedValue).ToString
                        oclsProduct.GroupName = row(ddlproductGroup.SelectedValue).ToString
                        oclsProduct.name = row(ddlproductName.SelectedValue).ToString
                        oclsProduct.price = Val(row(ddlPrice.SelectedValue).ToString())
                        oclsProduct.UnitName = row(ddlUnit.SelectedValue).ToString
                        oclsProduct.ImportData()
                        Session("Success") = "Record imported successfully"
                    End If
                Next
                Response.Redirect("Product_List.aspx", False)
            End If


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
        End Try
    End Sub
    Public Sub rdProduct_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdProduct.ItemCommand
        If e.CommandName = "Edit" Then
            Session("product_id") = Convert.ToInt32(e.CommandArgument)
            Session("JquerySearchFilter") = hdnsearchvalue.Value
            Session("copy_product_id") = Nothing

            Response.Redirect("Product_Master.aspx", False)


        ElseIf e.CommandName = "Delete" Then
            deleteProduct(Convert.ToInt32(e.CommandArgument), "D")
        ElseIf e.CommandName = "Copy" Then
            Session("product_id") = Nothing
            Session("copy_condiment") = hdCondiments.Value.ToString()
            Session("copy_product_id") = Convert.ToInt32(e.CommandArgument).ToString()
            Response.Redirect("Product_Master.aspx", False)
        ElseIf e.CommandName = "Act" Then


            If chkActive.Checked = True Then

                oclsProduct.Del_Status = "D"

            Else

                oclsProduct.Del_Status = "A"

            End If

            deleteProduct(Convert.ToInt32(e.CommandArgument), oclsProduct.Del_Status)

        End If



    End Sub

    Public Sub rptSizeDetails_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptSizeDetails.ItemCommand
        If e.CommandName = "Edit" Then
            Session("product_id") = Convert.ToInt32(e.CommandArgument)
            Session("JquerySearchFilter") = hdnsearchvalue.Value
            ' Response.Redirect("Product_Master.aspx", False)
            Server.Transfer("Product_Master.aspx", True)
        ElseIf e.CommandName = "Delete" Then
            deleteProduct(Convert.ToInt32(e.CommandArgument), "D")
        ElseIf e.CommandName = "Copy" Then
            Session("product_id") = Nothing
            Session("copy_condiment") = hdCondiments.Value.ToString()
            Session("copy_product_id") = Convert.ToInt32(e.CommandArgument).ToString()
            Response.Redirect("Product_Master.aspx", False)
        ElseIf e.CommandName = "Act" Then


            If chkActive.Checked = True Then

                oclsProduct.Del_Status = "D"

            Else

                oclsProduct.Del_Status = "A"

            End If

            deleteProduct(Convert.ToInt32(e.CommandArgument), oclsProduct.Del_Status)

        End If
    End Sub

    Public Sub rptzCondimentDetails_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptzCondimentDetails.ItemCommand

        If e.CommandName = "Edit" Then
            Session("product_id") = Convert.ToInt32(e.CommandArgument)
            Session("JquerySearchFilter") = hdnsearchvalue.Value
            Response.Redirect("Product_Master.aspx", False)
            'Server.Transfer("Product_Master.aspx", False)
        ElseIf e.CommandName = "Delete" Then
            deleteProduct(Convert.ToInt32(e.CommandArgument), "D")
        ElseIf e.CommandName = "Copy" Then
            Session("product_id") = Nothing
            Session("copy_condiment") = hdCondiments.Value.ToString()
            Session("copy_product_id") = Convert.ToInt32(e.CommandArgument).ToString()
            Response.Redirect("Product_Master.aspx", False)
        ElseIf e.CommandName = "Act" Then


            If chkActive.Checked = True Then

                oclsProduct.Del_Status = "D"

            Else

                oclsProduct.Del_Status = "A"

            End If

            deleteProduct(Convert.ToInt32(e.CommandArgument), oclsProduct.Del_Status)

        End If
    End Sub


    Public Sub rptPrinterDetail_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptPrinterDetail.ItemCommand

        If e.CommandName = "Edit" Then
            Session("product_id") = Convert.ToInt32(e.CommandArgument)
            Session("JquerySearchFilter") = hdnsearchvalue.Value
            'Response.Redirect("Product_Master.aspx", False)
            Server.Transfer("Product_Master.aspx", False)
        ElseIf e.CommandName = "Delete" Then
            deleteProduct(Convert.ToInt32(e.CommandArgument), "D")
        ElseIf e.CommandName = "Copy" Then
            Session("product_id") = Nothing
            Session("copy_condiment") = hdCondiments.Value.ToString()
            Session("copy_product_id") = Convert.ToInt32(e.CommandArgument).ToString()
            Response.Redirect("Product_Master.aspx", False)
        ElseIf e.CommandName = "Act" Then


            If chkActive.Checked = True Then

                oclsProduct.Del_Status = "D"

            Else

                oclsProduct.Del_Status = "A"

            End If

            deleteProduct(Convert.ToInt32(e.CommandArgument), oclsProduct.Del_Status)

        End If

    End Sub

    Public Sub rptzBarcodeDetails_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptzBarcodeDetails.ItemCommand

        If e.CommandName = "Edit" Then
            Session("product_id") = Convert.ToInt32(e.CommandArgument)
            Session("JquerySearchFilter") = hdnsearchvalue.Value
            Response.Redirect("Product_Master.aspx", False)
        ElseIf e.CommandName = "Delete" Then
            deleteProduct(Convert.ToInt32(e.CommandArgument), "D")
        ElseIf e.CommandName = "Copy" Then
            Session("product_id") = Nothing
            Session("copy_condiment") = hdCondiments.Value.ToString()
            Session("copy_product_id") = Convert.ToInt32(e.CommandArgument).ToString()
            Response.Redirect("Product_Master.aspx", False)
        ElseIf e.CommandName = "Act" Then


            If chkActive.Checked = True Then

                oclsProduct.Del_Status = "D"

            Else

                oclsProduct.Del_Status = "A"

            End If

            deleteProduct(Convert.ToInt32(e.CommandArgument), oclsProduct.Del_Status)

        End If
    End Sub



    Private Sub lnkchangesize_Click(sender As Object, e As EventArgs) Handles lnkchangesize.Click
        Response.Redirect("Change_Size.aspx", False)
    End Sub

    Private Sub BindSizeDetails()
        If chkActive.Checked = True Then
            oclsProduct.active = 1
        Else
            oclsProduct.active = 0
        End If
        Dim dtReport As DataTable = oclsProduct.Get_Size_Details()
        dtSize = dtReport
        If dtReport.Rows.Count > 0 Then
            rptSizeDetails.DataSource = dtReport
            rptSizeDetails.DataBind()
        End If

    End Sub

    Private Sub BindCondimentDetails()
        If chkActive.Checked = True Then
            oclsProduct.active = 1
        Else
            oclsProduct.active = 0
        End If
        Dim dtCon As DataTable = oclsProduct.Get_Size_Details()
        dtCondiment = dtCon
        If dtCon.Rows.Count > 0 Then
            rptzCondimentDetails.DataSource = dtCon
            rptzCondimentDetails.DataBind()
        End If
    End Sub


    Private Sub BindBarcodeDetails()
        If chkActive.Checked = True Then
            oclsProduct.active = 1
        Else
            oclsProduct.active = 0
        End If
        Dim dtBar As DataTable = oclsProduct.Get_Size_Details()
        dtBarcodeDetails = dtBar
        If dtBar.Rows.Count > 0 Then
            rptzBarcodeDetails.DataSource = dtBar
            rptzBarcodeDetails.DataBind()
        End If
    End Sub

    Private Sub BindPrinterDetails()
        If chkActive.Checked = True Then
            oclsProduct.active = 1
        Else
            oclsProduct.active = 0
        End If
        Dim dtPrint As DataTable = oclsProduct.Get_Size_Details()
        dtPrinter = dtPrint
        If dtPrint.Rows.Count > 0 Then
            rptPrinterDetail.DataSource = dtPrint
            rptPrinterDetail.DataBind()
        End If
    End Sub

    Protected Sub chkActive_CheckedChanged(sender As Object, e As EventArgs)
        If chkActive.Checked = True Then
            oclsProduct.active = 1
        Else
            oclsProduct.active = 0
        End If

        If (ddCategory.SelectedValue = 0) Then
            bindGrid()
        ElseIf (ddCategory.SelectedValue = 1) Then
            BindSizeDetails()
        ElseIf (ddCategory.SelectedValue = 2) Then
            BindCondimentDetails()
        ElseIf (ddCategory.SelectedValue = 3) Then
            BindBarcodeDetails()
        ElseIf (ddCategory.SelectedValue = 4) Then
            BindPrinterDetails()
        End If

    End Sub

    Private Sub lnkChangePrice_Click(sender As Object, e As EventArgs) Handles lnkChangePrice.Click

        If (ddCategory.SelectedValue = 1) Then

            oclsProduct.Ip_address = Request.UserHostAddress
            oclsProduct.login_id = Val(Session("login_id"))
            oclsProduct.cmp_id = Val(Session("cmp_id"))

            For Each repeaterItem As RepeaterItem In rptSizeDetails.Items
                Dim ChkPrc As CheckBox = TryCast(repeaterItem.FindControl("chbx"), CheckBox)

                If (ChkPrc.Checked = True) Then

                    If repeaterItem.ItemType = ListItemType.Item Or repeaterItem.ItemType = ListItemType.AlternatingItem Then
                        Dim txPrice As TextBox = TryCast(repeaterItem.FindControl("txtPrice"), TextBox)

                        Dim Price As String = txPrice.Text

                        If (Price IsNot "") Then
                            oclsProduct.price = Convert.ToDecimal(Price)
                        End If

                        Dim txPriceId As Label = TryCast(repeaterItem.FindControl("lblPriceId"), Label)
                        Dim PriceId As String = txPriceId.Text

                        If PriceId IsNot Nothing Then
                            oclsProduct.price_Id = Convert.ToInt64(PriceId)
                        End If

                        Dim hfSizeId As HiddenField = CType(repeaterItem.FindControl("hdfSizeId"), HiddenField)

                        Dim hdSizeId As Int64 = hfSizeId.Value

                        oclsProduct.size_Id = hdSizeId

                        Dim txSizeName As TextBox = TryCast(repeaterItem.FindControl("txtSizeName"), TextBox)

                        Dim SizeName As String = txSizeName.Text

                        If (SizeName IsNot "") Then
                            oclsProduct.SizeName = SizeName
                        End If

                        Dim txSize As TextBox = TryCast(repeaterItem.FindControl("txtSize"), TextBox)

                        Dim Size As String = txSize.Text

                        If (Size IsNot "") Then
                            oclsProduct.size = Size
                        End If



                        Dim hdfUnitId As HiddenField = CType(repeaterItem.FindControl("hdfUnitId"), HiddenField)

                        Dim UnitId As Int64 = hdfUnitId.Value

                        oclsProduct.Unit_id = UnitId


                        Dim txUnit As TextBox = TryCast(repeaterItem.FindControl("txtUnit"), TextBox)

                        Dim Unit As String = txUnit.Text

                        If (Unit IsNot "") Then
                            oclsProduct.UnitName = Unit
                        End If


                        Dim ddTax As DropDownList = TryCast(repeaterItem.FindControl("ddlTx"), DropDownList)

                        If ddTax.SelectedItem.Value > 0 Then
                            oclsProduct.Tax_id = ddTax.SelectedItem.Value
                        Else
                            oclsProduct.Tax_id = 0
                        End If


                        Dim chkClickAndCollect As CheckBox = TryCast(repeaterItem.FindControl("chkClickAndCollect"), CheckBox)
                        If chkClickAndCollect.Checked = True Then
                            oclsProduct.click_and_collect = 1
                        Else
                            oclsProduct.click_and_collect = 0
                        End If


                        Dim ChkDeliver As CheckBox = TryCast(repeaterItem.FindControl("ChkDeliver"), CheckBox)
                        If ChkDeliver.Checked = True Then
                            oclsProduct.deliver = 1
                        Else
                            oclsProduct.deliver = 0
                        End If

                        Dim ChkOrderAtTable As CheckBox = TryCast(repeaterItem.FindControl("ChkOrderAtTable"), CheckBox)
                        If ChkOrderAtTable.Checked = True Then
                            oclsProduct.Order_at_table = 1
                        Else
                            oclsProduct.Order_at_table = 0
                        End If


                    End If

                    oclsProduct.UpdatePrice()

                End If
            Next

            oclsProduct.cat_id = ddCategory.SelectedValue
            BindSizeDetails()

        End If
    End Sub

    Protected Sub rptSizeDetails_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim ddlTx As DropDownList = (TryCast(e.Item.FindControl("ddlTx"), DropDownList))

            Dim lblTxx As Label = (TryCast(e.Item.FindControl("lblTx0"), Label))

            oclsBind.BindTax1(ddlTx, Val(Session("cmp_id")))

            Dim hdfId As HiddenField = (TryCast(e.Item.FindControl("hdfTaxId"), HiddenField))

            If hdfId.Value <> String.Empty Then
                Dim txId As Int64 = hdfId.Value
                ddlTx.SelectedValue = txId
                lblTxx.Text = ddlTx.SelectedItem.Text
                ddlTx.Visible = True
            Else
                ddlTx.SelectedValue = 0
                lblTxx.Text = 0
                ddlTx.Visible = True
            End If

            Dim HdClickAndCollect As HiddenField = (TryCast(e.Item.FindControl("HdClickAndCollect"), HiddenField))
            Dim chkClickAndCollect As CheckBox = (TryCast(e.Item.FindControl("chkClickAndCollect"), CheckBox))
            Dim lblClickAndCollect As Label = (TryCast(e.Item.FindControl("lblClickAndCollect0"), Label))

            If HdClickAndCollect.Value <> String.Empty Then

                Dim chClickAndCollect As Int64
                If HdClickAndCollect.Value = "Yes" Then
                    chClickAndCollect = 1
                Else
                    chClickAndCollect = 0
                End If

                If (chClickAndCollect = 1) Then
                    chkClickAndCollect.Checked = True
                    lblClickAndCollect.Text = "Yes"
                Else
                    chkClickAndCollect.Checked = False
                    lblClickAndCollect.Text = "No"
                End If

            End If


            Dim HdChkDeliver As HiddenField = (TryCast(e.Item.FindControl("HdChkDeliver"), HiddenField))
            Dim ChkDeliver As CheckBox = (TryCast(e.Item.FindControl("ChkDeliver"), CheckBox))
            Dim lblDeliver As Label = (TryCast(e.Item.FindControl("lblDeliver0"), Label))
            If HdChkDeliver.Value <> String.Empty Then

                Dim chChkDeliver As Int64

                If HdChkDeliver.Value = "Yes" Then
                    chChkDeliver = 1
                Else
                    chChkDeliver = 0
                End If

                If chChkDeliver = 1 Then
                    ChkDeliver.Checked = True
                    lblDeliver.Text = "Yes"
                Else
                    ChkDeliver.Checked = False
                    lblDeliver.Text = "No"
                End If

            End If


            Dim HdChkOrderAtTable As HiddenField = (TryCast(e.Item.FindControl("HdChkOrderAtTable"), HiddenField))
            Dim ChkOrderAtTable As CheckBox = (TryCast(e.Item.FindControl("ChkOrderAtTable"), CheckBox))
            Dim lblOrderAtTable As Label = (TryCast(e.Item.FindControl("lblOrderAtTable0"), Label))
            If HdChkOrderAtTable.Value <> String.Empty Then
                Dim VChkOrderAtTable As Int64

                If HdChkOrderAtTable.Value = "Yes" Then
                    VChkOrderAtTable = 1
                Else
                    VChkOrderAtTable = 0
                End If

                If (VChkOrderAtTable = 1) Then
                    ChkOrderAtTable.Checked = True
                    lblOrderAtTable.Text = "Yes"
                Else
                    ChkOrderAtTable.Checked = False
                    lblOrderAtTable.Text = "No"
                End If
            End If
        End If
    End Sub
    Protected Sub ddCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        oclsProduct.cmp_id = Val(Session("cmp_id"))

        If ddCategory.SelectedValue = 0 Then
            PnlSizeDetails.Visible = False
            PnlCondimentDetails.Visible = False
            PnlBarcodeDetails.Visible = False
            PnlPsummary.Visible = True
            PnlPrinterDetail.Visible = False
            lnkChangePrice.Visible = False
        ElseIf ddCategory.SelectedValue = 1 Then
            oclsProduct.cat_id = ddCategory.SelectedValue
            BindSizeDetails()
            lnkChangePrice.Visible = True
            PnlSizeDetails.Visible = True
            PnlCondimentDetails.Visible = False
            PnlBarcodeDetails.Visible = False
            PnlPsummary.Visible = False
            PnlPrinterDetail.Visible = False
        ElseIf ddCategory.SelectedValue = 2 Then
            oclsProduct.cat_id = ddCategory.SelectedValue
            BindCondimentDetails()
            PnlSizeDetails.Visible = False
            PnlCondimentDetails.Visible = True
            PnlBarcodeDetails.Visible = False
            PnlPsummary.Visible = False
            PnlPrinterDetail.Visible = False
            lnkChangePrice.Visible = False
        ElseIf ddCategory.SelectedValue = 3 Then
            oclsProduct.cat_id = ddCategory.SelectedValue
            BindBarcodeDetails()
            PnlSizeDetails.Visible = False
            PnlCondimentDetails.Visible = False
            PnlBarcodeDetails.Visible = True
            PnlPsummary.Visible = False
            PnlPrinterDetail.Visible = False
            lnkChangePrice.Visible = False
        ElseIf ddCategory.SelectedValue = 4 Then
            oclsProduct.cat_id = ddCategory.SelectedValue
            PnlSizeDetails.Visible = False
            PnlCondimentDetails.Visible = False
            PnlBarcodeDetails.Visible = False
            PnlPsummary.Visible = False
            PnlPrinterDetail.Visible = True
            BindPrinterDetails()
            lnkChangePrice.Visible = False
        End If
    End Sub
    Protected Sub lnk_Import_Click(sender As Object, e As EventArgs)
        Response.Redirect("Product_Import.aspx", False)
    End Sub
End Class

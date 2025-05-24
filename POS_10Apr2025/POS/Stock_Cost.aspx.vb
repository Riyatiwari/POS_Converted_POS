Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Data.OleDb

Partial Class Stock_Cost
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProduct As New Controller_clsProduct
    Dim oclsCategory As Controller_clsCategory = New Controller_clsCategory()
    Dim oclsBind As New clsBinding
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Stock_Cost"


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
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
                    'lnkNew.Visible = True
                Else
                    'lnkNew.Visible = False
                End If
                'txtForDate.SelectedDate = System.DateTime.Now
                oclsBind.BindLocation_new(rdlocation, Val(Session("cmp_id")))
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Cost:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub bindGrid()
        Try
            If Val(rdlocation.SelectedValue) = 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Please select location.');", True)

            Else

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclsProduct.Select_productCost_ByLocation()

                If dt.Rows.Count > 0 Then
                    rdcopyProduct.DataSource = dt
                    rdcopyProduct.DataBind()
                Else
                    rdcopyProduct.DataSource = String.Empty
                    rdcopyProduct.DataBind()
                End If
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stock_Cost:bindGrid:" + ex.Message)
        End Try

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
            LogHelper.Error("Stock_Cost:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Public Sub ReBindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.SelectAll()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
            Else
                rdcopyProduct.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Cost:ReBindGrid():" + ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            If Val(rdlocation.SelectedValue) = 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Please select location.');", True)

            Else

                For Each item As RepeaterItem In rdcopyProduct.Items
                    Dim value As String

                    Dim cost As RadNumericTextBox = item.FindControl("txtcost")
                    Dim hd_product_id As Label = item.FindControl("hd_product_id")

                    If Val(cost.Text) > 0 Then
                        value = hd_product_id.Text
                        Session("change_productgroup_id") = value.ToString()

                        oclsProduct.product_id = hd_product_id.Text
                        oclsProduct.price = Val(cost.Text)
                        oclsProduct.created_date = System.DateTime.Now
                        oclsProduct.Location_id = rdlocation.SelectedValue
                        oclsProduct.cmp_id = Val(Session("cmp_id"))
                        oclsProduct.Product_Cost()

                    End If

                Next
                If Session("change_productgroup_id") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record changed successfully');", True)
                    bindGrid()

                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Stock_Cost:btnSave_Click:" + ex.Message)
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

                    'dtExcelData.Columns.AddRange(New DataColumn(3) {New DataColumn("Group", GetType(String)),
                    '                                        New DataColumn("Name", GetType(String)),
                    '                                        New DataColumn("Price", GetType(Decimal)),
                    '                                        New DataColumn("Unit", GetType(String))})

                    Using oda As New OleDbDataAdapter((Convert.ToString("SELECT * FROM [") & sheet1) + "]", excel_con)
                        oda.Fill(dtExcelData)
                    End Using
                End Using

                ViewState("Excel_Data") = dtExcelData

                Dim names As List(Of String) = New List(Of String)

                'For Each column As DataColumn In dtExcelData.Columns
                '    names.Add(column.ColumnName)
                'Next

                For Each row As DataRow In dtExcelData.Rows
                    For Each rowP As RepeaterItem In rdcopyProduct.Items

                        If DirectCast(rowP.FindControl("lblname"), Label).Text.ToString().ToLower() = row("product").ToString().ToLower() Then
                            Dim txtCost As RadNumericTextBox = rowP.FindControl("txtcost")
                            txtCost.Value = row("cost")
                        End If
                    Next
                Next



                'For Each row As DataRow In dtExcelData.Rows

                'If row("Group").ToString IsNot "" Then
                '    oclsProduct.cmp_id = Val(Session("cmp_id"))
                '    oclsProduct.GroupName = row("Group").ToString
                '    oclsProduct.name = row("Name").ToString
                '    oclsProduct.price = Val(row("Price").ToString())
                '    oclsProduct.UnitName = row("Unit").ToString
                '    oclsProduct.ImportData()
                '    Session("Success") = "Record imported successfully"
                'End If
                'Next

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
        End Try
    End Sub

    Private Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Response.Redirect("Product_Cost_list.aspx")
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Try
            If Val(rdlocation.SelectedValue) > 0 Then
                Dim fileName As String = "Product_Cost_" & System.DateTime.Now.ToString("ddMMyyyy")

                oclsProduct.cmp_id = Val(Session("cmp_id"))
                oclsProduct.Location_id = Val(rdlocation.SelectedValue)
                Dim dt As DataTable = oclsProduct.SelectProductBylocation()

                If dt.Rows.Count > 0 Then
                    grid_view1.DataSource = dt
                    grid_view1.DataBind()
                Else
                    grid_view1.DataSource = Nothing
                    grid_view1.DataBind()
                End If

                ExportExcel(grid_view1, fileName)

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Please select location.');", True)
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Cost:btnDownload_Click:" + ex.Message)
        End Try
    End Sub


    Public Sub ExportExcel(ByVal Grv As GridView, ByVal ExclName As String, ByVal Optional downloadTokenValue As String = "")

        Try

            Dim dt As DataTable = CType(Grv.DataSource, DataTable)
            System.Web.HttpContext.Current.Response.ClearContent()
            System.Web.HttpContext.Current.Response.Buffer = True
            System.Web.HttpContext.Current.Response.AppendCookie(New HttpCookie("fileDownloadToken", downloadTokenValue))
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", ExclName & ".xls"))
            System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.UTF8
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel"

            Dim sw As StringWriter = New StringWriter()
            Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
            Grv.AllowPaging = False
            Grv.RenderControl(htw)
            System.Web.HttpContext.Current.Response.Write(sw.ToString())
            System.Web.HttpContext.Current.Response.[End]()
        Catch ex As Exception
            LogHelper.Error("Stock_Cost:ExportExcel:" + ex.Message)
        End Try
    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub rdlocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If Val(rdlocation.SelectedValue) > 0 Then
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Cost:rdlocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    'Protected Sub btnView_Click(sender As Object, e As EventArgs)
    '    Try
    '        If Val(rdlocation.SelectedValue) > 0 Then
    '            bindGrid()
    '        Else
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Please select location.');", True)
    '        End If

    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Cost:btnView_Click:" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub btnShowPanel_Click(sender As Object, e As EventArgs)
        Try
            divUpload.Visible = True
            bindGrid()
        Catch ex As Exception

        End Try
    End Sub
End Class

Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.IO

Partial Class Product_Group_Master
    Inherits BaseClass
    Dim oclsCategory As New Controller_clsCategory()
    Dim oclsCategoryMain As New Controller_clsCategoryMain()
    Dim oclsBind As New clsBinding
    Dim oclsMachine As New Controller_clsMachine()
    Dim oclsCategory_Detail As New Controller_clsCategory_Detail()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If


                bindmachine()
                oclsBind.BindKeyMap(ddKeyMap, Val(Session("cmp_id")))
                oclsBind.BindProductGroupMain(ddlMainCategory, Val(Session("cmp_id")))
                oclsCategory.SelectLocation(chklocation, Val(Session("cmp_id")))
                If Not Session("category_id") = Nothing Then
                    BindProductGroup()
                Else
                    bindSortingNo()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Group_Master:Page_Load:" + ex.Message)
        End Try
    End Sub
    Private Sub bindSortingNo()
        Try
            oclsCategory.cmp_id = Val(Session("cmp_id"))
            oclsCategory.form_name = "Product Group"
            Dim dtTable As DataTable = oclsCategory.Select1()
            If dtTable.Rows.Count > 0 Then
                txtsortingno.Text = Val(dtTable.Rows(0)("SNO"))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Group_Master:bindSortingNo:" + ex.Message)
        End Try
    End Sub
    Private Sub BindProductGroup()
        Try
            oclsCategory.Category_id = Val(Session("category_id"))
            oclsCategory.cmp_id = Val(Session("cmp_id"))
            Dim dtCategory As DataTable = oclsCategory.Select()
            If dtCategory.Rows.Count > 0 Then
                txtCName.Text = dtCategory.Rows(0)("name").ToString
                txtCdescription.Text = dtCategory.Rows(0)("description").ToString
                ddKeyMap.ClearSelection()
                ddKeyMap.SelectedValue = Val(dtCategory.Rows(0)("key_map_id"))

                If dtCategory.Rows(0)("is_active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
                If dtCategory.Rows(0)("is_web_available").ToString() = "Yes" Then
                    chkwebo.Checked = True
                Else
                    chkwebo.Checked = False
                End If

                txtsortingno.Text = dtCategory.Rows(0)("sorting_no").ToString

                ddlMainCategory.ClearSelection()
                ddlMainCategory.SelectedValue = Val(dtCategory.Rows(0)("maincategory_id"))

                Dim commasepratedlocations As String = dtCategory.Rows(0)("location_id").ToString
                Dim Arraycommasepratedlocations As String() = Split(commasepratedlocations, "#")


                'For Each item As ListItem In chklocation.Items
                '    For index As Integer = 0 To Arraycommasepratedlocations.Length - 1
                '        If item.Value = Val(Arraycommasepratedlocations(index)) Then
                '            item.Selected = True
                '        End If
                '    Next
                'Next

                If dtCategory.Rows(0)("click_and_collect").ToString() = "Yes" Then
                    chkIsClickcollect.Checked = True
                Else
                    chkIsClickcollect.Checked = False
                End If

                If dtCategory.Rows(0)("deliver").ToString() = "Yes" Then
                    chkIsDeliver.Checked = True
                Else
                    chkIsDeliver.Checked = False
                End If

                If dtCategory.Rows(0)("Order_at_table").ToString() = "Yes" Then
                    chkIsOrderattable.Checked = True
                Else
                    chkIsOrderattable.Checked = False
                End If

                rdDescSales.Text = dtCategory.Rows(0)("web_sales_description").ToString

                If dtCategory.Rows(0)("Aimage") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Dim bytes As Byte() = DirectCast(dtCategory.Rows(0)("Aimage"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                    hdlogo.Value = Convert.ToBase64String(bytes)

                    ViewState("Image") = 1
                    ViewState("logo") = dtCategory.Rows(0)("Aimage")
                    'RadBinaryImage1.DataValue = dtCategory.Rows(0)("Aimage")
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Group_Master:BindProductGroup:" + ex.Message)
        End Try

    End Sub

    Public Sub bindmachine()
        Try
            oclsCategory_Detail.cmp_id = Val(Session("cmp_id"))
            Dim dtMachine As DataTable = oclsCategory_Detail.Select()
            If dtMachine.Rows.Count > 0 Then
                rdMachineDetail.DataSource = dtMachine
                rdMachineDetail.DataBind()
            Else
                rdMachineDetail.DataSource = Nothing
                rdMachineDetail.DataBind()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Group_Master:bindmachine:" + ex.Message)
        End Try

    End Sub

    Private Sub Savemachine(ByVal id As Integer)
        Try
            Try
                oclsCategory_Detail.Category_Detail_id = 0
                oclsCategory_Detail.Category_id = id
                oclsCategory_Detail.Machine_id = 0
                oclsCategory_Detail.cmp_id = Val(Session("cmp_id"))
                oclsCategory_Detail.ip_address = Request.UserHostAddress
                oclsCategory_Detail.Login_id = Val(Session("Login_id"))

                oclsCategory_Detail.Delete()
            Catch ex As Exception
            End Try

            'For Each item As GridDataItem In rdMachineDetail.Items
            For Each row As GridViewRow In rdMachineDetail.Rows
                'Dim ddlSize As RadComboBox = DirectCast(row.FindControl("ddlDevice"), RadComboBox)
                Dim Checkbox_id As CheckBox = TryCast(row.FindControl("chk_Machine"), CheckBox)
                Dim Machine_id As Integer = TryCast(row.FindControl("hdmachine_id"), HiddenField).Value


                If Checkbox_id.Checked = True Then
                    oclsCategory_Detail.Category_Detail_id = 0
                    oclsCategory_Detail.Category_id = id
                    oclsCategory_Detail.Machine_id = Machine_id
                    oclsCategory_Detail.cmp_id = Val(Session("cmp_id"))
                    oclsCategory_Detail.ip_address = Request.UserHostAddress
                    oclsCategory_Detail.Login_id = Val(Session("Login_id"))

                    oclsCategory_Detail.Insert()
                End If
            Next

        Catch ex As Exception
            LogHelper.Error("Product_Group_Master:bindmachine:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("category_id") = Nothing Then
                Clear()
            Else
                BindProductGroup()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Group_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Product_Group_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_Group_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtCName.Text = ""
            txtCdescription.Text = ""
            ddKeyMap.ClearSelection()
        Catch ex As Exception
            LogHelper.Error("Product_Group_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsCategory.cmp_id = Val(Session("cmp_id"))
            oclsCategory.name = txtCName.Text.Trim()
            oclsCategory.description = txtCdescription.Text
            'oclsCategory.key_map_id = Val(ddKeyMap.SelectedValue)
            oclsCategory.key_map_id = 0
            oclsCategory.Is_active = IIf(chkActive.Checked = True, 1, 0)

            oclsCategory.Ip_address = Request.UserHostAddress
            oclsCategory.login_id = Val(Session("login_id"))
            oclsCategory.machine_id = 0
            oclsCategory.sorting_no = Val(txtsortingno.Text)
            oclsCategory.Is_web_available = IIf(chkwebo.Checked = True, 1, 0)
            oclsCategory.main_category_id = Val(ddlMainCategory.SelectedValue)
            Dim loc As String = ""

            'For Each item As ListItem In chklocation.Items
            '    If item.Selected Then

            '        loc = item.Value + "#" + loc


            '    End If
            'Next

            oclsCategory.Strlocation_id = loc.Trim.TrimEnd("#")
            oclsCategory.click_and_collect = IIf(chkIsClickcollect.Checked = True, 1, 0)
            oclsCategory.deliver = IIf(chkIsDeliver.Checked = True, 1, 0)
            oclsCategory.Order_at_table = IIf(chkIsOrderattable.Checked = True, 1, 0)
            oclsCategory.Description_sales = rdDescSales.Text

            Dim bytes As Byte()

            If fileupload.HasFile Then
                Using br As BinaryReader = New BinaryReader(fileupload.PostedFile.InputStream)
                    bytes = br.ReadBytes(fileupload.PostedFile.ContentLength)
                End Using
            Else
                bytes = DirectCast(ViewState("logo"), Byte())
                'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                'Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                'hdlogo.Value = Convert.ToBase64String(bytes)
            End If


            oclsCategory.Aimage = bytes

            'Dim i As Integer = 0
            'For Each file As UploadedFile In fileupload.UploadedFiles
            '    i = i + 1
            'Next
            'If i > 0 Then
            '    RadBinaryImage1.DataValue = Nothing
            '    For Each file As UploadedFile In fileupload.UploadedFiles
            '        Dim image As Byte()
            '        Dim fileLength As Long = fileupload.UploadedFiles(0).InputStream.Length
            '        image = New Byte(fileLength - 1) {}
            '        fileupload.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
            '        RadBinaryImage1.DataValue = image
            '        Image1.Visible = True
            '        Dim bytes As Byte() = RadBinaryImage1.DataValue
            '        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            '        Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
            '    Next
            'Else
            '    RadBinaryImage1.DataValue = ViewState("logo")
            'End If
            'oclsCategory.Aimage = RadBinaryImage1.DataValue



            Dim dt As DataTable
            If Session("Category_id") = Nothing Then
                oclsCategory.Category_id = 0
                dt = oclsCategory.Insert()
                Dim Category_id As Integer = dt.Rows(0)("Category_id").ToString
                Savemachine(Category_id)
                Session("Success") = "Record inserted successfully"

                Dim s As String
                'If dt.Rows.Count > 0 Then
                '    s = dt.Rows(0)("msg")
                'End If

                If s <> "" Then
                    Session("category_id") = Nothing
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + dt.Rows(0)("msg") + "');window.location='Product_Group_List.aspx';", True)
                Else
                    Session("category_id") = Nothing
                    Response.Redirect("Product_Group_List.aspx", False)
                End If

            Else
                oclsCategory.Category_id = Val(Session("category_id"))
                dt = oclsCategory.Update()
                Savemachine(Val(Session("category_id")))
                Session("Success") = "Record updated successfully"
                Dim s As String
                If dt.Rows.Count > 0 Then
                    s = dt.Rows(0)("msg")
                End If
                If s <> "" Then
                    Session("category_id") = Nothing
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + dt.Rows(0)("msg") + "');window.location='Product_Group_List.aspx';", True)
                Else
                    Session("category_id") = Nothing
                    Response.Redirect("Product_Group_List.aspx", False)
                End If
            End If
            'Session("category_id") = Nothing
            'Response.Redirect("Product_Group_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Group_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdMachineDetail_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rdMachineDetail.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then

    '            Dim device_id As HiddenField = CType(e.Item.FindControl("hddevice_id"), HiddenField)
    '            Dim checkbox_id As CheckBox = DirectCast(e.Item.FindControl("chk_Machine"), CheckBox)
    '            Dim machine_id As HiddenField = CType(e.Item.FindControl("hdmachine_id"), HiddenField)

    '            'Dim li As RadComboBoxItem = New RadComboBoxItem("No printer", "0")
    '            'ddlSize.Items.Insert(0, li)

    '            oclsCategory_Detail.cmp_id = Val(Session("cmp_id"))
    '            oclsCategory_Detail.Category_id = Val(Session("category_id"))
    '            Dim dtMachine As DataTable = oclsCategory_Detail.SelectMachineByCategory()
    '            If dtMachine.Rows.Count > 0 Then
    '                For Each row As DataRow In dtMachine.Rows

    '                    If Val(machine_id.Value) = Val(row("Machine_id")) Then
    '                        checkbox_id.Checked = True
    '                    End If
    '                Next
    '            End If
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub rdMachineDetail_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim device_id As HiddenField = CType(e.Row.FindControl("hddevice_id"), HiddenField)
                Dim checkbox_id As CheckBox = DirectCast(e.Row.FindControl("chk_Machine"), CheckBox)
                Dim machine_id As HiddenField = CType(e.Row.FindControl("hdmachine_id"), HiddenField)

                'Dim li As RadComboBoxItem = New RadComboBoxItem("No printer", "0")
                'ddlSize.Items.Insert(0, li)

                oclsCategory_Detail.cmp_id = Val(Session("cmp_id"))
                oclsCategory_Detail.Category_id = Val(Session("category_id"))
                Dim dtMachine As DataTable = oclsCategory_Detail.SelectMachineByCategory()
                If dtMachine.Rows.Count > 0 Then
                    For Each row As DataRow In dtMachine.Rows

                        If Val(machine_id.Value) = Val(row("Machine_id")) Then
                            checkbox_id.Checked = True
                        End If
                    Next
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class

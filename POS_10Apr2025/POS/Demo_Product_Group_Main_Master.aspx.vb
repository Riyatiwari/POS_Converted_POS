Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.IO
Partial Class Demo_Product_Group_Main_Master
    Inherits BaseClass
    Dim oclsCategory As New Controller_clsCategoryMainDemo()
    Dim oclsBind As New clsBinding
    Dim oclsMachine As New Controller_clsMachine()
    Dim oclsCategory_Detail As New Controller_clsCategory_Detail()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("category_id") = Nothing Then
                    BindProductGroup()
                    BindGrid_Edit()
                Else
                    bindSortingNo()
                    BindGrid()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Product_Group_Main_Master:Page_Load:" + ex.Message)
        End Try
    End Sub
    Private Sub bindSortingNo()
        Try
            oclsCategory.cmp_id = Val(Session("cmp_id"))
            oclsCategory.form_name = "Group Category"
            Dim dtTable As DataTable = oclsCategory.Select1()
            If dtTable.Rows.Count > 0 Then
                txtsortingno.Text = Val(dtTable.Rows(0)("SNO"))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Group_Main_Master:bindSortingNo:" + ex.Message)
        End Try
    End Sub
    Private Sub BindProductGroup()
        Try
            oclsCategory.Category_id = Val(Session("category_id"))


            Dim pth As String
            pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & oclsCategory.Category_id.ToString & ".jpg"

            If System.IO.File.Exists(pth) Then

                Image1.Src = "~/Product_Group_Category_Images/Group_Category_" & Val(Session("category_id")) & ".jpg"

            End If

            pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & oclsCategory.Category_id.ToString & ".png"
            If System.IO.File.Exists(pth) Then

                Image1.Src = "~/Product_Group_Category_Images/Group_Category_" & Val(Session("category_id")) & ".png"

            End If

            pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & oclsCategory.Category_id.ToString & ".jpeg"
            If System.IO.File.Exists(pth) Then

                Image1.Src = "~/Product_Group_Category_Images/Group_Category_" & Val(Session("category_id")) & ".jpeg"

            End If


            'Image1.Src = "~/Product_Group_Category_Images/Group_Category_" & Val(Session("category_id")) & ".png"

            oclsCategory.cmp_id = Val(Session("cmp_id"))
            Dim dtCategory As DataTable = oclsCategory.Select()
            If dtCategory.Rows.Count > 0 Then
                txtCName.Text = dtCategory.Rows(0)("name").ToString
                txtCdescription.Text = dtCategory.Rows(0)("description").ToString

                If dtCategory.Rows(0)("is_active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If
                If dtCategory.Rows(0)("is_available_online").ToString() = "Yes" Then
                    chkONline.Checked = True
                Else
                    chkONline.Checked = False
                End If

                txtsortingno.Text = dtCategory.Rows(0)("sorting_no").ToString

                'If dtCategory.Rows(0)("category_image") IsNot DBNull.Value Then
                '    Image1.Visible = True
                '    Dim bytes As Byte() = DirectCast(dtCategory.Rows(0)("category_image"), Byte())
                '    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                '    Image1.Src = Convert.ToString("data:image/jpg;base64,") & base64String

                '    hdlogo.Value = Convert.ToBase64String(bytes)

                '    ViewState("category_image") = 1

                '    RadBinaryImage1.DataValue = Nothing

                '    ViewState("imgCategory") = dtCategory

                'End If


            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Group_Main_Master:BindProductGroup:" + ex.Message)
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
            LogHelper.Error("Product_Group_Main_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Product_Group_Main_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Product_Group_Main_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtCName.Text = ""
            txtCdescription.Text = ""
            BindGrid()
        Catch ex As Exception
            LogHelper.Error("Product_Group_Main_Master:Clear:" + ex.Message)
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
            oclsCategory.Is_web_available = IIf(chkONline.Checked = True, 1, 0)
            oclsCategory.Ip_address = Request.UserHostAddress
            oclsCategory.login_id = Val(Session("login_id"))
            oclsCategory.machine_id = 0
            oclsCategory.sorting_no = Val(txtsortingno.Text)

            Dim dt As DataTable
            If Session("Category_id") = Nothing Then
                oclsCategory.Category_id = 0




                'img
                'oclsCategory.category_image = RadBinaryImage1.DataValue
                'Dim i As Integer = 0
                'Dim j As Integer = 0
                'For Each file As UploadedFile In fileupload.UploadedFiles
                '    i = i + 1
                'Next
                'If i > 0 Then
                '    RadBinaryImage1.DataValue = Nothing
                '    For Each file As UploadedFile In fileupload.UploadedFiles
                '        'Dim image As Byte()
                '        'Dim fileLength As Long = fileupload.UploadedFiles(j).InputStream.Length
                '        'image = New Byte(fileLength - 1) {}
                '        'fileupload.UploadedFiles(j).InputStream.Read(image, 0, image.Length)
                '        'RadBinaryImage1.DataValue = image
                '        'Image1.Visible = True
                '        'Dim bytes As Byte() = RadBinaryImage1.DataValue
                '        'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                '        'Image1.Src = Convert.ToString("data:image/jpg;base64,") & base64String
                '        'oclsCategory.category_image = RadBinaryImage1.DataValue
                '        'j = j + 1


                '        'Dim path As String = Server.MapPath("~/uploaded_image_product/" + file.GetName())
                '        'fileupload.SaveAs(path)

                '        'Image1.ImageUrl = "~/UploadedImages/" & path.GetFileName(FileUpload1.FileName)
                '    Next
                'Else
                '    RadBinaryImage1.DataValue = ViewState("logo")
                'End If

                'img end




                dt = oclsCategory.Insert()
                Dim Category_id As Integer = dt.Rows(0)("Category_id").ToString

                For Each f As UploadedFile In fileupload.UploadedFiles
                    f.SaveAs("D:\Working\POS\Product_Group_Category_Images\" & "Group_Category_" & Category_id.ToString & f.GetExtension, True)
                Next

                Dim isCollect As Integer
                Dim isDeliver As Integer
                Dim isAttable As Integer
                Dim Location_Id As Integer

                For Each item As GridViewRow In GridView1.Rows
                    isCollect = IIf(CType(item.FindControl("chkCollect"), CheckBox).Checked, 1, 0)
                    isDeliver = IIf(CType(item.FindControl("chkDeliver"), CheckBox).Checked, 1, 0)
                    isAttable = IIf(CType(item.FindControl("chkTable"), CheckBox).Checked, 1, 0)
                    Location_Id = (CType(item.FindControl("hflocation_id"), HiddenField).Value)
                    oclsCategory.Location_Id = Location_Id
                    oclsCategory.click_and_collect = isCollect
                    oclsCategory.deliver = isDeliver
                    oclsCategory.Order_at_table = isAttable

                    If (Category_id <> 0) Then
                        oclsCategory.Category_id = Category_id
                    Else
                        oclsCategory.Category_id = 0
                    End If

                    oclsCategory.Insert_Size_Details()
                Next


                Session("Success") = "Record inserted successfully"

                Dim s As String


                If s <> "" Then
                    Session("category_id") = Nothing
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + dt.Rows(0)("msg") + "');window.location='Product_Group_Main_List.aspx';", True)
                Else
                    Session("category_id") = Nothing
                    Response.Redirect("Product_Group_Main_List.aspx", False)
                End If

            Else
                oclsCategory.Category_id = Val(Session("category_id"))

                'img

                'oclsCategory.category_image = RadBinaryImage1.DataValue
                'Dim i As Integer = 0
                'Dim j As Integer = 0
                'For Each file As UploadedFile In fileupload.UploadedFiles
                '    i = i + 1
                'Next
                'If i > 0 Then
                '    RadBinaryImage1.DataValue = Nothing
                '    For Each file As UploadedFile In fileupload.UploadedFiles
                '        Dim image As Byte()
                '        Dim fileLength As Long = fileupload.UploadedFiles(j).InputStream.Length
                '        image = New Byte(fileLength - 1) {}
                '        fileupload.UploadedFiles(j).InputStream.Read(image, 0, image.Length)
                '        RadBinaryImage1.DataValue = image
                '        Image1.Visible = True
                '        Dim bytes As Byte() = RadBinaryImage1.DataValue
                '        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                '        'Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                '        Image1.Src = Convert.ToString("data:image/jpg;base64,") & base64String

                '        oclsCategory.category_image = RadBinaryImage1.DataValue

                '        j = j + 1
                '    Next
                'Else
                '    Dim dtCategory As DataTable = ViewState("imgCategory")
                '    Dim bytes As Byte() = DirectCast(dtCategory.Rows(0)("category_image"), Byte())

                '    RadBinaryImage1.DataValue = bytes

                '    oclsCategory.category_image = RadBinaryImage1.DataValue

                'End If

                'img end

                For Each f As UploadedFile In fileupload.UploadedFiles

                    Dim pth As String
                    pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & oclsCategory.Category_id.ToString & f.GetExtension

                    If System.IO.File.Exists(pth) Then

                        System.IO.File.Delete(pth)

                        f.SaveAs("D:\Working\POS\Product_Group_Category_Images\" & "Group_Category_" & Val(Session("category_id")).ToString & f.GetExtension, True)
                    Else
                        pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & oclsCategory.Category_id.ToString & ".jpg"
                        System.IO.File.Delete(pth)
                        pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & oclsCategory.Category_id.ToString & ".png"
                        System.IO.File.Delete(pth)
                        pth = "D:\Working\POS\Product_Group_Category_Images\Group_Category_" & oclsCategory.Category_id.ToString & ".jpeg"
                        System.IO.File.Delete(pth)
                        f.SaveAs("D:\Working\POS\Product_Group_Category_Images\" & "Group_Category_" & Val(Session("category_id")).ToString & f.GetExtension, True)
                    End If

                Next


                dt = oclsCategory.Update()


                Dim isCollect As Integer
                Dim isDeliver As Integer
                Dim isAttable As Integer
                Dim Location_Id As Integer
                'Dim Hdsize_Detail_id As Integer

                For Each item As GridViewRow In GridView1.Rows

                    isCollect = IIf(CType(item.FindControl("chkCollect"), CheckBox).Checked, 1, 0)
                    isDeliver = IIf(CType(item.FindControl("chkDeliver"), CheckBox).Checked, 1, 0)
                    isAttable = IIf(CType(item.FindControl("chkTable"), CheckBox).Checked, 1, 0)
                    Location_Id = (CType(item.FindControl("hflocation_id"), HiddenField).Value)
                    oclsCategory.Location_Id = Location_Id
                    oclsCategory.click_and_collect = isCollect
                    oclsCategory.deliver = isDeliver
                    oclsCategory.Order_at_table = isAttable

                    oclsCategory.Update_Size_Details()
                Next


                Session("Success") = "Record updated successfully"
                Dim s As String
                If dt.Rows.Count > 0 Then
                    s = dt.Rows(0)("msg")
                End If
                If s <> "" Then
                    Session("category_id") = Nothing
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('" + dt.Rows(0)("msg") + "');window.location='Product_Group_Main_List.aspx';", True)
                Else
                    Session("category_id") = Nothing
                    Response.Redirect("Product_Group_Main_List.aspx", False)
                End If
            End If
            'Session("category_id") = Nothing
            'Response.Redirect("Product_Group_Main_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_Group_Main_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub BindGrid()
        Try
            Dim dtFomrs As DataTable = oclsCategory.SelectLoc()
            ViewState("dt") = dtFomrs
            GridView1.DataSource = dtFomrs
            GridView1.DataBind()


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Group_Master:BindFormGrid:" + ex.Message)
        End Try
    End Sub

    Public Sub BindGrid_Edit()
        Try
            oclsCategory.Category_id = Session("category_id")

            Dim dtFomrs As DataTable = oclsCategory.SelectLoc_By_Id()
            ViewState("dt") = dtFomrs
            GridView1.DataSource = dtFomrs
            GridView1.DataBind()


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Group_Master:BindFormGrid:" + ex.Message)
        End Try

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim HdClickAndCollect As HiddenField = (TryCast(e.Row.FindControl("HdClickAndCollect"), HiddenField))

            Dim HdChkDeliver As HiddenField = (TryCast(e.Row.FindControl("HdChkDeliver"), HiddenField))

            Dim HdChkOrderAtTable As HiddenField = (TryCast(e.Row.FindControl("HdChkOrderAtTable"), HiddenField))

            Dim chkCollect As CheckBox = TryCast(e.Row.FindControl("chkCollect"), CheckBox)
            Dim chkDeliver As CheckBox = TryCast(e.Row.FindControl("chkDeliver"), CheckBox)
            Dim chkTable As CheckBox = TryCast(e.Row.FindControl("chkTable"), CheckBox)

            Dim ds As DataSet = New DataSet()
            Dim dt As DataTable = ViewState("dt")

            If HdClickAndCollect.Value <> String.Empty Then
                If HdClickAndCollect.Value <> String.Empty Then
                    Dim chCollect As Int64 = HdClickAndCollect.Value
                    If chCollect = 1 Then
                        chkCollect.Checked = True
                    Else
                        chkCollect.Checked = False
                    End If
                End If
            End If


            If HdChkDeliver.Value <> String.Empty Then

                If HdChkDeliver.Value <> String.Empty Then
                    Dim chChkDeliver As Int64 = HdChkDeliver.Value
                    If chChkDeliver = 1 Then
                        chkDeliver.Checked = True
                    Else
                        chkDeliver.Checked = False
                    End If
                End If
            End If

            If HdChkOrderAtTable.Value <> String.Empty Then
                If HdChkOrderAtTable.Value <> String.Empty Then
                    Dim chtbl As Int64 = HdChkOrderAtTable.Value
                    If chtbl = 1 Then
                        chkTable.Checked = True
                    Else
                        chkTable.Checked = False
                    End If
                End If
            End If
        End If
    End Sub


End Class

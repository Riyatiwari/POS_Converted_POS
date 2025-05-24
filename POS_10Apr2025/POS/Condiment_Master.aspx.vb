Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Condiment_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsCondiment As New Controller_clsCondiment()
    Dim oclsProduct As New Controller_clsProduct()
    Dim oclsLocation As New Controller_clsLocation()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                oclsBind.BindProductGroup(ddl_productGroup, Val(Session("cmp_id")))
                'oclsBind.BindProduct(radProduct, Val(Session("cmp_id")))

                If Not Session("Condiment_id") = Nothing Then
                    BindCondiment()
                    BindImage(Val(Session("Condiment_Id")))

                End If
                If radProduct.SelectedIndex = 0 Then
                    ddlunit.Items.Clear()
                    oclsBind.BindUnit(ddlunit, Val(Session("cmp_id")))
                End If

                '----------------------14/07/2022-------------------
                oclsLocation.cmp_id = Val(Session("cmp_id"))
                Dim dt As DataTable = oclsLocation.SelectLocationByCmp()
                If dt.Rows.Count > 1 Then
                    Session("l_count") = dt.Rows.Count
                    rdb_IsBySize.Items.FindByValue("1").Enabled = False
                    rdb_IsBySize.Items.FindByValue("0").Selected = True
                Else
                    rdb_IsBySize.Items.FindByValue("1").Enabled = True
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Condiment_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindCondiment()
        Try
            Dim sender As Object
            Dim e As EventArgs

            oclsCondiment.cmp_id = Val(Session("cmp_id"))
            oclsCondiment.Condiment_Id = Val(Session("Condiment_id"))

            Dim dt As DataTable = oclsCondiment.Select()
            If dt.Rows.Count > 0 Then
                txCondiment.Text = dt.Rows(0)("Condiment").ToString

                oclsBind.BindProductGroup(ddl_productGroup, Val(Session("cmp_id")))
                ddl_productGroup.ClearSelection()
                ddl_productGroup.SelectedValue = Val(dt.Rows(0)("category_id"))
                radProduct.ClearSelection()
                oclsBind.BindProductByProductGroup(radProduct, Val(Session("cmp_id")), Val(ddl_productGroup.SelectedValue))
                radProduct.SelectedValue = Val(dt.Rows(0)("product_id"))
                radAddSub.SelectedValue = Val(dt.Rows(0)("is_add_substract"))
                txtquantity.Text = Val(dt.Rows(0)("quantity"))
                'ddlunit.SelectedValue = Val(dt.Rows(0)("Unit"))
                ddlchoices.SelectedValue = Val(dt.Rows(0)("choices"))

                Try
                    radProduct_SelectedIndexChanged(sender, e)
                    ddlunit.ClearSelection()
                    ddlunit.SelectedValue = Val(dt.Rows(0)("unit"))
                    'ddlunit.FindItemByValue(Val(dt.Rows(0)("unit"))).Selected = True
                Catch ex As Exception
                    LogHelper.Error("Condiment_Master:BindCondiment" + ex.Message)
                End Try
                If dt.Rows(0)("Active") = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

                If Val(dt.Rows(0)("UseProductCondi")) = 1 Then
                    chkProductCondi.Checked = True
                Else
                    chkProductCondi.Checked = False
                End If

                '-----------------------14/07/2022-----------------------------
                If Session("l_count") IsNot Nothing Then
                    If Val(Session("l_count")) > 1 Then
                        BySize.Visible = False
                        ByQty.Visible = True
                        rdb_IsBySize.SelectedValue = Val(0)
                    Else
                        rdb_IsBySize.SelectedValue = Val(dt.Rows(0)("IsBySize"))
                        If Val(dt.Rows(0)("IsBySize")) = 1 Then
                            BindRadioButton()
                            rdbList.SelectedValue = dt.Rows(0)("sizeID").ToString()
                            BySize.Visible = True
                            ByQty.Visible = False
                        Else
                            BySize.Visible = False
                            ByQty.Visible = True
                        End If
                    End If
                Else
                    rdb_IsBySize.SelectedValue = Val(dt.Rows(0)("IsBySize"))
                    If Val(dt.Rows(0)("IsBySize")) = 1 Then
                        BindRadioButton()
                        rdbList.SelectedValue = dt.Rows(0)("sizeID").ToString()
                        BySize.Visible = True
                        ByQty.Visible = False
                    Else
                        BySize.Visible = False
                        ByQty.Visible = True
                    End If
                End If


            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Condiment_Master:BindFunctionType" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Condiment_id") = Nothing Then
                Clear()
            Else
                BindCondiment()
            End If
        Catch ex As Exception
            LogHelper.Error("Condiment_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Condiment_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Condiment_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txCondiment.Text = ""
            radProduct.SelectedIndex = 0
            chkActive.Checked = True
            chkProductCondi.Checked = False
            radAddSub.ClearSelection()
            ddlchoices.ClearSelection()
        Catch ex As Exception
            LogHelper.Error("Condiment_Master:Clear" + ex.Message)
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not radProduct.SelectedValue = "0" Then
                If radAddSub.SelectedValue = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Select condiment type ');", True)
                    Return
                End If

                If rdb_IsBySize.SelectedValue = "0" Then
                    If txtquantity.Text = "" Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Quantity is required');", True)
                        Return
                    End If
                    If ddlunit.SelectedValue = "0" Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Unit is required');", True)
                        Return
                    End If
                Else
                    If Val(rdbList.SelectedValue) = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Selling Size is required');", True)
                        Return
                    End If
                End If

            End If

            oclsCondiment.cmp_id = Val(Session("cmp_id"))
            oclsCondiment.Condiment = txCondiment.Text.Trim()
            oclsCondiment.ip_address = Request.UserHostAddress
            oclsCondiment.login_id = Val(Session("login_id"))
            oclsCondiment.product_id = Val(IIf(radProduct.SelectedValue = "", 0, radProduct.SelectedValue))
            oclsCondiment.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsCondiment.is_add_substract = Val(radAddSub.SelectedValue)
            oclsCondiment.Quantity = Val(txtquantity.Text)
            oclsCondiment.Unit = Val(ddlunit.SelectedValue)
            oclsCondiment.choices = Val(ddlchoices.SelectedValue)
            oclsCondiment.IsBySize = Val(rdb_IsBySize.SelectedValue)
            If Val(rdb_IsBySize.SelectedValue) = 1 Then
                oclsCondiment.sizeID = Val(rdbList.SelectedValue)
            Else
                oclsCondiment.sizeID = 0
            End If

            oclsCondiment.UseProductCondi = IIf(chkProductCondi.Checked = True, 1, 0)

            If Session("Condiment_id") = Nothing Then
                oclsCondiment.Condiment_Id = 0
                Dim id As Integer = oclsCondiment.Insert()
                If id <> 0 Then
                    ImageSave(id)
                End If
                Session("Success") = "Record inserted successfully"
            Else
                oclsCondiment.Condiment_Id = Val(Session("Condiment_id"))
                oclsCondiment.Update()
                ImageSave(Val(Session("Condiment_id")))
                Session("Success") = "Record updated successfully"
            End If
            Session("Condiment_id") = Nothing

            If Session("is_fromProduct") = 1 Then
                Dim script As String = "function f(){window.close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                Session("Success") = Nothing
                Session("is_fromProduct") = Nothing
            Else
                Response.Redirect("Condiment_List.aspx", False)
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Condiment_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    'Protected Sub radproduct_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radProduct.SelectedIndexChanged
    '    Try
    '        If radProduct.SelectedIndex = 0 Then
    '            ddlunit.Items.Clear()
    '            rdbList.Items.Clear()
    '            oclsBind.BindUnit(ddlunit, Val(Session("cmp_id")))
    '        Else
    '            oclsBind.BindUnitByProduct(ddlunit, Val(Session("cmp_id")), Val(radProduct.SelectedValue))
    '            BindRadioButton()
    '        End If

    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:txtPriceLocation_TextChanged:" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub ImageSave(id As Integer)
        oclsCondiment.cmp_id = Val(Session("cmp_id"))
        oclsCondiment.condiment_image = RadBinaryImage1.DataValue
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

                oclsCondiment.condiment_image = RadBinaryImage1.DataValue

                oclsCondiment.cmp_id = Session("cmp_id")


                oclsCondiment.Condiment_Id = id
                If Session("Condiment_id") = Nothing Then
                    oclsCondiment.ImageInsert()
                    BindImage(id)

                Else
                    oclsCondiment.ImageInsert()
                    BindImage(Val(Session("Condiment_Id")))

                End If
                j = j + 1
            Next
        Else
            RadBinaryImage1.DataValue = ViewState("logo")
        End If
    End Sub
    Public Sub BindImage(id As Integer)
        Try
            oclsCondiment.cmp_id = Val(Session("cmp_id"))
            oclsCondiment.Condiment_Id = id
            Dim dt As DataTable = oclsCondiment.ImageSelect()
            If dt.Rows.Count > 0 Then


                If dt.Rows(0)("condiment_image") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Dim bytes As Byte() = DirectCast(dt.Rows(0)("condiment_image"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                    hdlogo.Value = Convert.ToBase64String(bytes)

                    ViewState("condiment_image") = 1

                    RadBinaryImage1.DataValue = Nothing

                    RadGrid1.DataSource = dt
                    RadGrid1.DataBind()

                End If



            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                oclsCondiment.cmp_id = Val(Session("cmp_id"))
                oclsCondiment.Condiment_Image_id = id
                oclsCondiment.ImgDelete()
                If Not Session("Condiment_id") = Nothing Then
                    BindImage(Val(Session("Condiment_Id")))
                End If
                Response.Redirect("Condiment_Master.aspx")

            End If
        Catch ex As Exception
            LogHelper.Error("Condiment_Master:rdCondiment_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Public Sub BindRadioButton()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id").ToString())
            oclsProduct.product_id = Val(radProduct.SelectedValue)
            Dim dt As DataTable = oclsProduct.SelectProduct_Size_new()

            If dt.Rows.Count > 0 Then

                rdbList.DataSource = dt
                rdbList.DataTextField = "name"
                rdbList.DataValueField = "Size_id"
                rdbList.DataBind()
            Else
                rdbList.DataSource = String.Empty
                rdbList.DataBind()

            End If

        Catch ex As Exception
            LogHelper.Error("Condiment_Master:BindRadioButton:" + ex.Message)
        End Try
    End Sub

    Protected Sub ddl_productGroup_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            oclsBind.BindProductByProductGroup(radProduct, Val(Session("cmp_id")), Val(ddl_productGroup.SelectedValue))
            If radProduct.SelectedIndex = 0 Then
                ddlunit.Items.Clear()
                rdbList.Items.Clear()
                oclsBind.BindUnit(ddlunit, Val(Session("cmp_id")))
            Else
                oclsBind.BindUnitByProduct(ddlunit, Val(Session("cmp_id")), Val(radProduct.SelectedValue))
                BindRadioButton()
            End If
        Catch ex As Exception
            LogHelper.Error("Condiment_Master:BindRadioButton:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdb_IsBySize_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try

            If rdb_IsBySize.SelectedValue = "0" Then

                ByQty.Visible = True
                BySize.Visible = False

            Else
                BindRadioButton()

                ByQty.Visible = False
                BySize.Visible = True

            End If

        Catch ex As Exception
            LogHelper.Error("Condiment_Master:rdb_IsBySize_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub radProduct_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radProduct.SelectedIndex = 0 Then
                ddlunit.Items.Clear()
                rdbList.Items.Clear()
                oclsBind.BindUnit(ddlunit, Val(Session("cmp_id")))
            Else
                oclsBind.BindUnitByProduct(ddlunit, Val(Session("cmp_id")), Val(radProduct.SelectedValue))
                BindRadioButton()
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:txtPriceLocation_TextChanged:" + ex.Message)
        End Try
    End Sub
End Class

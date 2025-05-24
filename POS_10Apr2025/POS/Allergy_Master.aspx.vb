Imports System.Data
Imports System.IO
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Allergy_Master
    Inherits BaseClass
    Dim objclsAllergyMaster As New Controller_clsAllergy()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("allergy_id") = Nothing Then
                    BindAllergy()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Profile_Master:Page_Load" + ex.Message)
        End Try
    End Sub
    Private Sub BindAllergy()
        Try
            Dim sender As Object
            Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            objclsAllergyMaster.cmp_id = Val(Session("cmp_id"))
            objclsAllergyMaster.allergy_id = Val(Session("allergy_id"))
            Dim dt As DataTable = objclsAllergyMaster.Select()
            If dt.Rows.Count > 0 Then
                txtname.Text = dt.Rows(0)("name").ToString
                txtdescription.Text = dt.Rows(0)("description").ToString
                'RadBinaryImage1.DataValue = dt.Rows(0)("amount").ToString

                If dt.Rows(0)("Aimage") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Dim bytes As Byte() = DirectCast(dt.Rows(0)("Aimage"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                    hdlogo.Value = Convert.ToBase64String(bytes)

                    ViewState("Image") = 1
                    ViewState("logo") = dt.Rows(0)("Aimage")
                    'RadBinaryImage1.DataValue = dt.Rows(0)("Aimage")
                End If



            End If




        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Allergy_Master:BindProfile:" + ex.Message)
        End Try

    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
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

            If Not hdBarcode_size.Value = "" Or Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please update barcode data');", True)
            Else

            End If

            Dim bytes As Byte()
            Using br As BinaryReader = New BinaryReader(fileupload.PostedFile.InputStream)
                bytes = br.ReadBytes(fileupload.PostedFile.ContentLength)
            End Using
            objclsAllergyMaster.Aimage = bytes

            'objclsAllergyMaster.Aimage = RadBinaryImage1.DataValue

            objclsAllergyMaster.cmp_id = Session("cmp_id")
            objclsAllergyMaster.name = txtname.Text
            objclsAllergyMaster.description = txtdescription.Text

            If Session("allergy_id") = Nothing Then
                objclsAllergyMaster.allergy_id = 0
                objclsAllergyMaster.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                objclsAllergyMaster.allergy_id = Val(Session("allergy_id"))
                objclsAllergyMaster.Update()
                Session("Success") = "Record updated successfully"
            End If

            Session("allergy_id") = Nothing
            Response.Redirect("Allergy_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Allergy_Master:BindAllergy:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)

        Try
            If Session("profile_id") = Nothing Then
                Clear()
            Else
                BindAllergy()
            End If
        Catch ex As Exception
            LogHelper.Error("Profile_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txtname.Text = ""
            txtdescription.Text = ""
            Image1.ImageUrl = ""
        Catch ex As Exception
            LogHelper.Error("Profile_Master:Clear" + ex.Message)
        End Try
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("Allergy_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Profile_Master:lnkNew_Click" + ex.Message)
        End Try
    End Sub
End Class

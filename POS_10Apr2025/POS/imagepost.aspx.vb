Imports System.IO

Partial Class imagepost
    Inherits BaseClass

    Protected Sub buttonLabel_ServerClick(sender As Object, e As System.EventArgs) Handles buttonLabel.ServerClick
        Try
            Dim img As FileUpload = CType(FileUpload1, FileUpload)

            Dim imgByte As Byte() = Nothing

            If img.HasFile AndAlso Not img.PostedFile Is Nothing Then

                'To create a PostedFile

                Dim File As HttpPostedFile = FileUpload1.PostedFile

                'Create byte Array with file len

                'imgByte = New Byte(File.ContentLength - 1) {}

                ''force the control to load data in array

                'File.InputStream.Read(imgByte, 0, File.ContentLength)

                ''Encode the data
                'Dim encoded = HttpUtility.UrlEncode(Convert.ToBase64String(imgByte))

                simForm.Action = "http://localhost:51827/POS_WS_Upload.aspx"
                ModuleName.Value = "UserProfile"
                'Image.Value = encoded
                'Session("image") = encoded
                'Session("module") = "Document"
                'Response.Redirect("POS_WS_Upload.aspx")


                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Auto_Submit", "document.simForm.submit();", True)
                'Server.Transfer("POS_WS_Upload.aspx", True)

                'Dim bytes As Byte() = imgByte
                'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                'Label1.Text = Convert.ToString("data:image/jpg;base64,") & base64String


                'Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
                'Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

                '' Convert byte[] to Image
                'ms.Write(imageBytes, 0, imageBytes.Length)
                'Dim image__1 As Image = image.FromStream(ms, True)

                ''Dim byteArray As Byte() = Encoding.ASCII.GetBytes(imgByte)
                ''Console.WriteLine("Uploading to {0} ...", uriString)
                ' '' Upload the input string using the HTTP 1.0 POST method. 
                ''Dim responseArray As Byte() = myWebClient.UploadData(uriString, "POST", byteArray)

            End If
        Catch ex As Exception
            LogHelper.Error("imagepost:buttonLabel_ServerClick:" + ex.Message)
        End Try
    End Sub
End Class

Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class mailReset_Password
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess
    Dim objclsLogin As New clsLogin
    Dim oLogin As New Controller_clsLogin()

    Private Function DecryptEmailAndStoreUUID(encryptedEmail As String, encryptedStoreUUID As String) As Tuple(Of String, String)
        Dim decryptedEmail As String = Decrypt(encryptedEmail)
        Dim decryptedStoreUUID As String = encryptedStoreUUID
        Return Tuple.Create(decryptedEmail, decryptedStoreUUID)
    End Function



    Private Function IsLinkExpired(linkTimestamp As DateTime) As Boolean

        Dim currentTime As DateTime = DateTime.Now
        Dim expirationTime As DateTime = linkTimestamp.AddMinutes(1)
        Return currentTime > expirationTime
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try


            If Not String.IsNullOrEmpty(Request.QueryString("el")) AndAlso Not String.IsNullOrEmpty(Request.QueryString("sid")) Then

                Dim mEmail As String = Request.QueryString("el")
                Dim encryptedStoreUuid As String = Request.QueryString("sid")
                Dim encryptedExpirationTime As String = Request.QueryString("exp")

                Dim linkTimestamp As DateTime? = DecryptTimestamp(encryptedExpirationTime)
                If linkTimestamp.HasValue AndAlso IsLinkExpired(linkTimestamp.Value) Then
                    lblMessage.Text = "The password reset link has expired."
                    Return
                End If


                Dim email As String = mEmail
                Dim storeUuid As String = encryptedStoreUuid
                Session("email") = email
                Session("storeuuid") = storeUuid


            End If
        Catch ex As Exception
            LogHelper.Error("mailReset_Password:btnReset_Click:" + ex.Message)
        End Try
    End Sub


    Private Function DecryptTimestamp(encryptedTimestamp As String) As DateTime?
        Try


            Dim format As String = "dd/MM/yyyy hh:mm:ss tt"
            Dim decryptedTimestamp As String = encryptedTimestamp
            LogHelper.Error("Decrypted Timestamp: " & decryptedTimestamp)

            Dim parsedDateTime As DateTime
            If DateTime.TryParseExact(decryptedTimestamp, format, CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDateTime) Then
                LogHelper.Error("Parsed DateTime: " & parsedDateTime.ToString("yyyy-MM-dd HH:mm:ss"))
                'LogHelper.Error("Parsed DateTime: " & parsedDateTime.ToString("yyyy-MM-dd HH:mm:ss"))
                Return parsedDateTime
            Else
                LogHelper.Error("Failed to parse decrypted timestamp. Expected format: yyyy-MM-dd HH:mm:ss")
                Return Nothing
            End If
        Catch ex As Exception
            LogHelper.Error("Error in DecryptTimestamp: " & ex.Message)
            Return Nothing
        End Try
    End Function
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try


            Dim newPassword As String = txtNewPassword.Text.Trim()
            Dim confirmPassword As String = txtConfirmPassword.Text.Trim()
            If newPassword <> confirmPassword Then

                lblMessage.Text = "Passwords do not match."
                Return
            End If

            'Dim linkTimestamp As DateTime? = DecryptTimestamp(Request.QueryString("exp"))
            'If Not linkTimestamp.HasValue OrElse IsLinkExpired(linkTimestamp.Value) Then
            '    lblMessage.Text = "The password reset link has expired."
            '    Return
            'End If



            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()

            Dim cmd As SqlCommand = New SqlCommand("resetEmail", strcon)
            cmd.CommandType = CommandType.StoredProcedure

            'cmd.Parameters.AddWithValue("@StoreUUID", Encrypt(Session("StoreUUID").ToString()))
            cmd.Parameters.AddWithValue("@StoreUUID", Session("storeuuid"))
            cmd.Parameters.AddWithValue("@Username", Session("email"))
            cmd.Parameters.AddWithValue("@password", Encrypt(newPassword))

            Dim result As Object = cmd.ExecuteNonQuery()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Successfully Reset Password');", True)
            Response.Redirect("User_Access.aspx", False)
        Catch ex As Exception
            LogHelper.Error("mailReset_Password:btnReset_Click:" + ex.Message)
        End Try


    End Sub

End Class

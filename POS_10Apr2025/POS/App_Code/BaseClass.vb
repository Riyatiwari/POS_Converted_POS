Imports Microsoft.VisualBasic
Imports System.Data
Imports Telerik.Web.UI
Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data.OleDb
Imports System.Net
Imports System.Configuration
Imports System.Web
Imports System.Globalization
Imports Microsoft.Exchange.WebServices.Data
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Mail
Imports System.Threading
Imports Newtonsoft.Json.Linq
Imports System.Net.Configuration
Imports System.Security.Authentication
Imports System.Collections.Generic
Imports System.Web.UI.WebControls

Public Class BaseClass
    Inherits System.Web.UI.Page
    Public oClsDataccess1 As New ClsDataccess
    Public oSessionManager1 As New SessionManager

    Public Const Page_Items As Integer = 15
    Public RedirectPath As String


    Dim bltransfer As Boolean
    Dim _VCurrentPage As Integer
    Public Shared admin_payroll As String
    Public Enum Pages
        Index
        Company
        Login
    End Enum
    Public Enum tabs
        personal
        contacts
        emergency
        depedants
        immigration
        jobs
        reporting
        experience
        education
        skills
        launguage
        licence
        document
        allownce
        salary
        Insurance
        Other
        Compentence
    End Enum
    Dim _CurrentPage As Pages
    Dim _StrRedirect As String = String.Empty
    Dim LoginRequired As Boolean = True
    Public Property CurrentPage() As Pages
        Get
            Return _CurrentPage
        End Get
        Set(ByVal value As Pages)
            _CurrentPage = value
        End Set
    End Property
    Public Property StrRedirect() As String
        Get
            Return _StrRedirect
        End Get
        Set(ByVal value As String)
            _StrRedirect = value
        End Set
    End Property
    Public Property VCurrentPage() As Integer
        Get
            Return _VCurrentPage
        End Get
        Set(ByVal value As Integer)
            _VCurrentPage = value
        End Set
    End Property


    Public Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function
    Public Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function

    'Public Function GetSelectedValue(ByRef ddl As RadComboBox) As String
    '    Try
    '        Dim sb As New StringBuilder()
    '        Dim collection As IList(Of RadComboBoxItem) = ddl.CheckedItems

    '        If (collection.Count <> 0) Then

    '            sb.Append("")

    '            For Each item As RadComboBoxItem In collection
    '                If sb.ToString = "" Then
    '                    sb.Append(item.Value)
    '                Else
    '                    sb.Append("#" + item.Value)
    '                End If

    '            Next
    '        End If
    '        Return sb.ToString

    '    Catch ex As Exception
    '        Return ""

    '    End Try
    'End Function

    'Public Function GetSelectedDefault(ByRef ddl As RadComboBox) As String
    '    Try
    '        Dim sb As New StringBuilder()
    '        Dim collection As IList(Of RadComboBoxItem) = ddl.Items

    '        If (collection.Count <> 0) Then

    '            sb.Append("")

    '            For Each item As RadComboBoxItem In collection
    '                If sb.ToString = "" Then
    '                    sb.Append(item.Value)
    '                Else
    '                    sb.Append("#" + item.Value)
    '                End If

    '            Next
    '        End If
    '        Return sb.ToString

    '    Catch ex As Exception
    '        Return ""

    '    End Try
    'End Function

    Public Function ReadExcel(ByVal fileExtension As String, ByVal fileLocation As String) As DataTable
        Dim dtExcelRecords As New DataTable()
        Try
            Dim connectionString As String = ""
            If fileExtension = ".xls" Then
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
            ElseIf fileExtension = ".xlsx" Then
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileLocation & ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
            End If

            Dim con As New OleDbConnection(connectionString)
            Dim cmd As New OleDbCommand()
            cmd.CommandType = System.Data.CommandType.Text
            cmd.Connection = con
            Dim dAdapter As New OleDbDataAdapter(cmd)

            con.Open()
            Dim dtExcelSheetName As DataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim getExcelSheetName As String = dtExcelSheetName.Rows(0)("Table_Name").ToString()
            cmd.CommandText = "SELECT * FROM [" & getExcelSheetName & "]"
            dAdapter.SelectCommand = cmd
            dAdapter.Fill(dtExcelRecords)
            con.Close()
            Return dtExcelRecords
        Catch ex As Exception
            Return dtExcelRecords
        End Try

    End Function

    Private Shared Function RemoteCertificateValidationCallback(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function
    Public Sub MailTo(ByVal CmpID As Int32, ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Dim oClsDataccess As New ClsDataccess

        Dim dtMailDetail As DataTable = oClsDataccess.Getdatatable("select * from S_Email_Settings where Company_id = " + CmpID.ToString)
        If dtMailDetail.Rows.Count > 0 Then


            If dtMailDetail.Rows(0)("Is_MES") = "1" Then
                Try
                    Dim service As New ExchangeService(ExchangeVersion.Exchange2007_SP1)
                    service.Url = New Uri(dtMailDetail.Rows(0)("MES_URI"))

                    service.UseDefaultCredentials = True
                    service.TraceEnabled = True
                    service.Credentials = New WebCredentials(dtMailDetail.Rows(0)("MailServer_UserName"), dtMailDetail.Rows(0)("MailServer_Password"))
                    ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf RemoteCertificateValidationCallback)

                    Dim message As New EmailMessage(service)
                    message.Subject = Subject

                    If CC <> "" Then
                        If CC.Contains(",") Then
                            Dim strSp() As String
                            strSp = CC.Split(",")
                            For index As Integer = 0 To strSp.Length - 1
                                message.ToRecipients.Add(strSp(index))
                            Next
                        Else
                            message.CcRecipients.Add(CC)
                        End If
                    End If

                    If BCC <> "" Then
                        If BCC.Contains(",") Then
                            Dim strSp() As String
                            strSp = BCC.Split(",")
                            For index As Integer = 0 To strSp.Length - 1
                                message.ToRecipients.Add(strSp(index))
                            Next
                        Else
                            message.BccRecipients.Add(BCC)
                        End If
                    End If
                    message.Body = Body
                    If To_EmailId.Contains(",") Then
                        Dim strSp() As String
                        strSp = To_EmailId.Split(",")
                        For index As Integer = 0 To strSp.Length - 1
                            message.ToRecipients.Add(strSp(index))
                        Next
                    Else
                        message.ToRecipients.Add(To_EmailId)
                    End If

                    message.ReplyTo.Add(dtMailDetail.Rows(0)("Reply_To"))

                    message.Body.BodyType = BodyType.HTML
                    If attach <> "" Then
                        Dim strarr() As String
                        Dim i As Integer
                        strarr = attach.Split(",")
                        For i = 0 To strarr.Length - 1
                            Dim str As String = strarr(i)
                            Dim path As String = str
                            message.Attachments.AddFileAttachment(path)
                        Next
                    End If
                    message.Save()
                    message.SendAndSaveCopy()

                Catch ex As Exception
                    Err.Raise(Err.Number, , ex.ToString)
                Finally

                End Try
            Else
                Dim MailServer As String
                Dim MailServer_UserName As String
                Dim MailServer_Password As String
                Dim MailServer_Port As Integer
                Dim From_Email As String
                Dim Ssl As Boolean
                Try
                    MailServer = dtMailDetail.Rows(0)("MailServer")
                    MailServer_UserName = dtMailDetail.Rows(0)("MailServer_UserName")
                    MailServer_Password = dtMailDetail.Rows(0)("MailServer_Password")
                    MailServer_Port = dtMailDetail.Rows(0)("Port")
                    From_Email = dtMailDetail.Rows(0)("From_Email")
                    Ssl = IIf(dtMailDetail.Rows(0)("ssl") = "1", True, False)
                    Dim Email_CC As String
                    Dim Email_BCC As String
                    Dim oE_Mail As New MailMessage()
                    oE_Mail.To.Clear()
                    If To_EmailId <> "" Then
                        oE_Mail.To.Add(To_EmailId)
                    End If

                    If CC <> "" Then
                        Email_CC = CC
                        oE_Mail.CC.Add(Email_CC)
                    End If
                    If BCC <> "" Then
                        Email_BCC = BCC
                        oE_Mail.Bcc.Add(Email_BCC)
                    End If

                    If attach <> "" Then
                        Dim strarr() As String
                        Dim i As Integer
                        strarr = attach.Split(",")
                        For i = 0 To strarr.Length - 1
                            Dim str As String = strarr(i)
                            Dim path As String = str
                            Dim myattach As New System.Net.Mail.Attachment(path)
                            oE_Mail.Attachments.Add(myattach)
                        Next
                    End If


                    oE_Mail.From = New MailAddress(From_Email, dtMailDetail.Rows(0)("Alias"))
                    oE_Mail.IsBodyHtml = True
                    oE_Mail.Subject = Subject
                    oE_Mail.Body = Body

                    oE_Mail.Priority = MailPriority.High
                    Dim oSmtpclient As New SmtpClient()
                    oSmtpclient.Host = MailServer
                    oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
                    oSmtpclient.Port = MailServer_Port
                    oSmtpclient.EnableSsl = Ssl
                    oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network
                    oSmtpclient.Send(oE_Mail)

                Catch ex As Exception
                    Err.Raise(Err.Number, , ex.ToString)
                Finally
                    MailServer = Nothing
                    MailServer_UserName = Nothing
                    MailServer_Password = Nothing
                    MailServer_Port = Nothing
                    From_Email = Nothing
                    Ssl = Nothing
                End Try
            End If
        End If
    End Sub

    Public Sub MailTo_receipt(ByVal CmpID As Int32, ByVal LocationID As Int32, ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        Dim mailSettings As SmtpSection = CType(ConfigurationManager.GetSection("system.net/mailSettings/smtp"), SmtpSection)

        If Not mailSettings Is Nothing Then
            MailServer_Port = mailSettings.Network.Port
            MailServer = mailSettings.Network.Host
            MailServer_Password = mailSettings.Network.Password
            MailServer_UserName = mailSettings.Network.UserName
            From_Email = mailSettings.From
            Ssl = True
            alas = "TenderPOS"
        End If

        Try

            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()
            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            If CC <> "" Then
                Email_CC = CC
                oE_Mail.CC.Add(Email_CC)
            End If
            If BCC <> "" Then
                Email_BCC = BCC
                oE_Mail.Bcc.Add(Email_BCC)
            End If

            If attach <> "" Then
                Dim strarr() As String
                Dim i As Integer
                strarr = attach.Split(",")
                For i = 0 To strarr.Length - 1
                    Dim str As String = strarr(i)
                    Dim path As String = str
                    Dim myattach As New System.Net.Mail.Attachment(path)
                    oE_Mail.Attachments.Add(myattach)
                Next
            End If


            oE_Mail.From = New MailAddress(From_Email, alas)
            oE_Mail.IsBodyHtml = True
            oE_Mail.Subject = Subject
            oE_Mail.Body = Body
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network
            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network

            oSmtpclient.Send(oE_Mail)

        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Base_class: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
        End Try
    End Sub
    Public Sub MailTo_receiptA(ByVal CmpID As Int32, ByVal LocationID As Int32, ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String, ByVal aliasN As String)
        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        Dim mailSettings As SmtpSection = CType(ConfigurationManager.GetSection("system.net/mailSettings/smtp"), SmtpSection)

        If Not mailSettings Is Nothing Then
            MailServer_Port = mailSettings.Network.Port
            MailServer = mailSettings.Network.Host
            MailServer_Password = mailSettings.Network.Password
            MailServer_UserName = mailSettings.Network.UserName
            From_Email = mailSettings.From
            Ssl = True
            alas = aliasN
        End If

        Try

            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()
            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            If CC <> "" Then
                Email_CC = CC
                oE_Mail.CC.Add(Email_CC)
            End If
            If BCC <> "" Then
                Email_BCC = BCC
                oE_Mail.Bcc.Add(Email_BCC)
            End If

            If attach <> "" Then
                Dim strarr() As String
                Dim i As Integer
                strarr = attach.Split(",")
                For i = 0 To strarr.Length - 1
                    Dim str As String = strarr(i)
                    Dim path As String = str
                    Dim myattach As New System.Net.Mail.Attachment(path)
                    oE_Mail.Attachments.Add(myattach)
                Next
            End If


            oE_Mail.From = New MailAddress(From_Email, alas)
            oE_Mail.IsBodyHtml = True
            oE_Mail.Subject = Subject
            oE_Mail.Body = Body
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12

            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network

            oSmtpclient.Send(oE_Mail)

        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Base_class: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
        End Try
    End Sub

    Public Sub MailTo_new(ByVal CmpID As Int32, ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Try
            Dim oClsDataccess As New ClsDataccess

            'Dim MailServer As String
            'Dim MailServer_UserName As String
            'Dim MailServer_Password As String
            'Dim MailServer_Port As Integer
            'Dim From_Email As String
            'Dim Ssl As Boolean
            'Dim alas As String

            'Dim mailSettings As SmtpSection = CType(ConfigurationManager.GetSection("system.net/mailSettings/smtp"), SmtpSection)

            'If Not mailSettings Is Nothing Then
            '    MailServer_Port = mailSettings.Network.Port
            '    MailServer = mailSettings.Network.Host
            '    MailServer_Password = mailSettings.Network.Password
            '    MailServer_UserName = mailSettings.Network.UserName
            '    From_Email = mailSettings.From
            '    Ssl = True
            '    alas = "TenderPOS"
            'End If

            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()

            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            If CC <> "" Then
                Email_CC = CC
                oE_Mail.CC.Add(Email_CC)
            End If
            If BCC <> "" Then
                Email_BCC = BCC
                oE_Mail.Bcc.Add(Email_BCC)
            End If

            If attach <> "" Then
                Dim strarr() As String
                Dim i As Integer
                strarr = attach.Split(",")
                For i = 0 To strarr.Length - 1
                    Dim str As String = strarr(i)
                    Dim path As String = str
                    Dim myattach As New System.Net.Mail.Attachment(path)
                    oE_Mail.Attachments.Add(myattach)
                Next
            End If


            oE_Mail.From = New MailAddress("noreply@tenderpos.com", "TenderPOS")
            oE_Mail.IsBodyHtml = True
            oE_Mail.Subject = Subject
            oE_Mail.Body = Body

            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = "smtp.office365.com"

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12

            oSmtpclient.Port = 587
            oSmtpclient.EnableSsl = True
            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.Credentials = New Net.NetworkCredential("noreply@tenderpos.com", "Jaw51965")
            'oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network

            oSmtpclient.Send(oE_Mail)

            'Dim service As New ExchangeService(ExchangeVersion.Exchange2007_SP1)
            'service.Url = New Uri("https://computername.domain.contoso.com/EWS/Exchange.asmx")

            'service.UseDefaultCredentials = True
            'service.TraceEnabled = True
            'service.Credentials = New WebCredentials(MailServer_UserName, MailServer_Password)
            'ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf RemoteCertificateValidationCallback)

            'Dim message As New EmailMessage(service)
            'message.Subject = Subject

            'If CC <> "" Then
            '    If CC.Contains(",") Then
            '        Dim strSp() As String
            '        strSp = CC.Split(",")
            '        For index As Integer = 0 To strSp.Length - 1
            '            message.ToRecipients.Add(strSp(index))
            '        Next
            '    Else
            '        message.CcRecipients.Add(CC)
            '    End If
            'End If

            'If BCC <> "" Then
            '    If BCC.Contains(",") Then
            '        Dim strSp() As String
            '        strSp = BCC.Split(",")
            '        For index As Integer = 0 To strSp.Length - 1
            '            message.ToRecipients.Add(strSp(index))
            '        Next
            '    Else
            '        message.BccRecipients.Add(BCC)
            '    End If
            'End If
            'message.Body = Body
            'If To_EmailId.Contains(",") Then
            '    Dim strSp() As String
            '    strSp = To_EmailId.Split(",")
            '    For index As Integer = 0 To strSp.Length - 1
            '        message.ToRecipients.Add(strSp(index))
            '    Next
            'Else
            '    message.ToRecipients.Add(To_EmailId)
            'End If

            'message.ReplyTo.Add("developer@technometrics.in")

            'message.Body.BodyType = BodyType.HTML
            'If attach <> "" Then
            '    Dim strarr() As String
            '    Dim i As Integer
            '    strarr = attach.Split(",")
            '    For i = 0 To strarr.Length - 1
            '        Dim str As String = strarr(i)
            '        Dim path As String = str
            '        message.Attachments.AddFileAttachment(path)
            '    Next
            'End If
            'message.Save()
            'message.SendAndSaveCopy()

        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)
        Finally

        End Try

    End Sub

    Public Function GetSelectedValue(ByRef ddl As RadComboBox) As String
        Try
            Dim sb As New StringBuilder()
            Dim collection As IList(Of RadComboBoxItem) = ddl.CheckedItems

            If (collection.Count <> 0) Then

                sb.Append("")

                For Each item As RadComboBoxItem In collection
                    If sb.ToString = "" Then
                        sb.Append(item.Value)
                    Else
                        sb.Append("#" + item.Value)
                    End If

                Next
            End If
            Return sb.ToString

        Catch ex As Exception
            Return ""

        End Try
    End Function

    Public Function GetSelectedValue_new(ByRef ddl As RadComboBox) As String
        Try
            Dim sb As New StringBuilder()
            Dim collection As IList(Of RadComboBoxItem) = ddl.CheckedItems

            If (collection.Count <> 0) Then

                sb.Append("")

                For Each item As RadComboBoxItem In collection
                    If sb.ToString = "" Then
                        sb.Append(item.Value + "#")
                    Else
                        sb.Append(item.Value + "#")
                    End If

                Next
            End If
            Return sb.ToString

        Catch ex As Exception
            Return ""

        End Try
    End Function


    'Protected Function CheckDateSelected(dt As RadDatePicker) As DateTime
    '    Dim FirtDate As DateTime = New DateTime(1900, 1, 1)
    '    If dt.SelectedDate Is Nothing Then
    '        Return FirtDate
    '    Else
    '        Return dt.SelectedDate
    '    End If
    '    Return FirtDate

    'End Function


    Public Sub EncryptFile(inputFilePath As String, outputfilePath As String, inputFile As String)
        Dim EncryptionKey As String = "WEBH5RMSGS09C23"
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            System.IO.File.Copy(inputFilePath, inputFile, True)
            File.Delete(inputFilePath)
            Using fs As New FileStream(outputfilePath, FileMode.Create)
                Try
                    Using cs As New CryptoStream(fs, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        Try
                            Dim fsIn As New FileStream(inputFile, FileMode.Open)
                            Try
                                Dim data As Integer
                                While (Assign(data, fsIn.ReadByte())) <> -1
                                    cs.WriteByte(CByte(data))
                                End While
                                fsIn.Dispose()
                                cs.Dispose()
                            Catch ex As Exception
                                fsIn.Dispose()
                                cs.Dispose()
                                fs.Dispose()
                                System.IO.File.Copy(inputFile, inputFilePath, True)
                            End Try
                        Catch ex As Exception
                            cs.Dispose()
                            fs.Dispose()
                            System.IO.File.Copy(inputFile, inputFilePath, True)
                        End Try
                    End Using
                    fs.Dispose()
                Catch ex As Exception
                    fs.Dispose()
                    System.IO.File.Copy(inputFile, inputFilePath, True)
                End Try
            End Using
            encryptor.Dispose()
        End Using
        File.Delete(inputFile)
    End Sub

    Public Sub DecryptFile(inputFilePath As String, outputfilePath As String, inputFile As String)
        Dim EncryptionKey As String = "WEBH5RMSGS09C23"
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
             &H65, &H64, &H76, &H65, &H64, &H65,
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            System.IO.File.Copy(inputFilePath, inputFile, True)
            File.Delete(inputFilePath)
            Using fs As New FileStream(inputFile, FileMode.Open)
                Try
                    Using cs As New CryptoStream(fs, encryptor.CreateDecryptor(), CryptoStreamMode.Read)
                        Try
                            Using fsOut As New FileStream(outputfilePath, FileMode.Create)
                                Try
                                    Dim data As Integer
                                    While (Assign(data, cs.ReadByte())) <> -1
                                        fsOut.WriteByte(CByte(data))
                                    End While
                                    fsOut.Dispose()
                                Catch ex As Exception
                                    fsOut.Dispose()
                                    cs.Dispose()
                                    System.IO.File.Copy(inputFile, inputFilePath, True)
                                End Try
                            End Using
                            cs.Dispose()
                        Catch ex As Exception
                            cs.Dispose()
                            System.IO.File.Copy(inputFile, inputFilePath, True)
                        End Try
                    End Using
                    fs.Dispose()
                Catch ex As Exception
                    fs.Dispose()
                    System.IO.File.Copy(inputFile, inputFilePath, True)
                End Try
            End Using
            encryptor.Dispose()
        End Using
        File.Delete(inputFile)
    End Sub

    Private Shared Function Assign(Of T)(ByRef source As T, ByVal value As T) As T
        source = value
        Return value
    End Function


    Public Function GetSelectedDefault(ByRef ddl As RadComboBox) As String
        Try
            Dim sb As New StringBuilder()
            Dim collection As IList(Of RadComboBoxItem) = ddl.Items

            If (collection.Count <> 0) Then

                sb.Append("")

                For Each item As RadComboBoxItem In collection
                    If item.Value <> "0" Then
                        If sb.ToString = "" Then
                            sb.Append(item.Value)
                        Else
                            sb.Append("#" + item.Value)
                        End If
                    End If
                Next
            End If
            Return sb.ToString

        Catch ex As Exception
            Return ""

        End Try
    End Function




    Protected Function getFollowingSunday(aDate As DateTime) As DateTime
        Return aDate.AddDays(7 - CInt(aDate.DayOfWeek))
    End Function

    Protected Function CheckDateSelected(dt As RadDatePicker) As DateTime
        Dim FirtDate As DateTime = New DateTime(1900, 1, 1)
        If dt.SelectedDate Is Nothing Then
            Return FirtDate
        Else
            Return dt.SelectedDate
        End If
        Return FirtDate

    End Function

    Protected Overrides Sub InitializeCulture()
        Try
            Dim language As String = "en-us"
            'Detect User's Language.
            If Session("Local_Language") IsNot Nothing Then
                'Set the Language.
                language = Session("Local_Language").ToString()
            End If
            'Set theu Clture.
            Thread.CurrentThread.CurrentCulture = New CultureInfo(language)
            Thread.CurrentThread.CurrentUICulture = New CultureInfo(language)
            ' If language = "en-us" Then

            Dim WeekCulture As String = ConfigurationManager.AppSettings("WeekCulture")
            If Not WeekCulture Is Nothing Then
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = WeekCulture
            End If

            Try
                Dim LanguageCulture As String = ConfigurationManager.AppSettings("DateCulture")
                If Not LanguageCulture Is Nothing Then
                    Dim us As New CultureInfo(LanguageCulture.ToString())
                    Dim ShortDatePattern As String = us.DateTimeFormat.ShortDatePattern
                    Dim DateSeparator As String = us.DateTimeFormat.DateSeparator
                    Dim newCulture As CultureInfo = DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
                    newCulture.DateTimeFormat.ShortDatePattern = ShortDatePattern
                    newCulture.DateTimeFormat.DateSeparator = DateSeparator
                    Thread.CurrentThread.CurrentCulture = newCulture
                Else
                    Dim newCulture As CultureInfo = DirectCast(System.Threading.Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
                    newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                    newCulture.DateTimeFormat.DateSeparator = "/"
                    Thread.CurrentThread.CurrentCulture = newCulture
                End If
            Catch ex As Exception

            End Try
            ' End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function ValidateEmail(ByVal EmailId As String) As Integer
        Dim Valid As Integer = 0
        Try
            Dim EmailApiUrl As String = ConfigurationManager.AppSettings("EmailApiUrl")
            Dim EmailApiUsername As String = ConfigurationManager.AppSettings("EmailApiUsername")
            Dim EmailApiPassword As String = ConfigurationManager.AppSettings("EmailApiPassword")

            Dim apiUrl As String = EmailApiUrl.ToString()
            Dim apiUsername As String = EmailApiUsername.ToString()
            Dim apiPassword As String = EmailApiPassword.ToString()
            Dim email As String = EmailId.ToString()

            Dim webClient As New WebClient()
            Dim result As String = webClient.DownloadString(apiUrl.ToString() + "usr=" + apiUsername.ToString() + "&pwd=" + apiPassword.ToString() + "&check=" + email.ToString())

            Dim objJSON As JObject = Nothing
            objJSON = JObject.Parse(result)

            If objJSON("verify_status") IsNot Nothing Then
                Valid = objJSON("verify_status")
            Else
                Valid = 3
            End If
            'If objJSON("verify_status") IsNot Nothing Then
            '    Response.Write(String.Format("The email address {0} is {1}", email, If(Convert.ToBoolean(Convert.ToInt32(objJSON("verify_status").ToString())), "GOOD", "BAD or cannot be verified")))
            'End If
            'If objJSON("authentication_status") IsNot Nothing Then
            '    Response.Write(String.Format("authentication_status: {0} (your authentication status: 1 = success; 0 = invalid user)", objJSON("authentication_status").ToString()))
            'End If
            'If objJSON("limit_status") IsNot Nothing Then
            '    Response.Write(String.Format("limit_status: {0}", objJSON("limit_status").ToString()))
            'End If
            'If objJSON("limit_desc") IsNot Nothing Then
            '    Response.Write(String.Format("limit_desc: {0}", objJSON("limit_desc").ToString()))
            'End If
            'If objJSON("verify_status") IsNot Nothing Then
            '    Response.Write(String.Format("verify_status: {0} (entered email is: 1 = OK; 0 = BAD)", objJSON("verify_status").ToString()))
            'End If
            'If objJSON("verify_status_desc") IsNot Nothing Then
            '    Response.Write(String.Format("verify_status_desc: {0}", objJSON("verify_status_desc").ToString()))
            'End If
            Return Valid
        Catch ex As Exception
            LogHelper.Error("BaseClass:ValidateEmail:" + ex.Message)
            Return 2
        End Try
    End Function

    Public Function CheckNValidateEmail(ByVal cmp_id As Integer, ByVal EmailId As String, ByVal login_id As Integer, ByVal ip_address As String) As Integer
        Dim Valid As Integer = 0
        Try
            Dim dtValidEmail As DataTable
            dtValidEmail = CheckVerifyEmail(cmp_id, EmailId)
            If dtValidEmail.Rows.Count > 0 Then
                If Val(dtValidEmail.Rows(0)("msg")) = 0 Then
                    Try
                        Dim EmailApiUrl As String = ConfigurationManager.AppSettings("EmailApiUrl")
                        Dim EmailApiKey As String = ConfigurationManager.AppSettings("EmailApiKey")
                        'Dim EmailApiUsername As String = ConfigurationManager.AppSettings("EmailApiUsername")
                        'Dim EmailApiPassword As String = ConfigurationManager.AppSettings("EmailApiPassword")

                        Dim apiUrl As String = EmailApiUrl.ToString()
                        Dim apiKey As String = EmailApiKey.ToString()
                        'Dim apiUsername As String = EmailApiUsername.ToString()
                        'Dim apiPassword As String = EmailApiPassword.ToString()
                        Dim email As String = EmailId.ToString()

                        Dim webClient As New WebClient()
                        'Dim result As String = webClient.DownloadString(apiUrl.ToString() + "usr=" + apiUsername.ToString() + "&pwd=" + apiPassword.ToString() + "&check=" + email.ToString())
                        Dim result As String = webClient.DownloadString(apiUrl.ToString() + "EmailAddress=" + email.ToString() + "&APIKey=" + apiKey.ToString())

                        Dim objJSON As JObject = Nothing
                        objJSON = JObject.Parse(result)

                        Dim Status As String = ""

                        If objJSON("status") IsNot Nothing Then
                            If objJSON("status").ToString.StartsWith("2") Then
                                Valid = 1 ' Valid Email ID
                                Status = objJSON("status").ToString + " - " + objJSON("details").ToString
                            ElseIf objJSON("status").ToString.StartsWith("3") Then
                                Valid = 0 ' Not Valid Email ID
                                Status = objJSON("status").ToString + " - " + objJSON("details").ToString
                            ElseIf objJSON("status").ToString.StartsWith("4") Then
                                Valid = 0 ' Not Valid Email ID
                                Status = objJSON("status").ToString + " - " + objJSON("details").ToString
                            ElseIf objJSON("status").ToString.StartsWith("1") Then
                                Valid = 3 ' Not Verified Email ID
                                Status = objJSON("status").ToString + " - " + objJSON("details").ToString
                            End If
                        Else
                            Status = "No response found"
                            Valid = 4
                        End If

                        SaveEmailVerifiedDirectSave(Session("cmp_id"), HttpContext.Current.Request.UserHostAddress.ToString(), email.ToString(), Valid, Status)

                        'If objJSON("verify_status") IsNot Nothing Then
                        '    Valid = objJSON("verify_status")
                        'Else
                        '    Valid = 3
                        'End If
                    Catch ex As Exception
                        LogHelper.Error("BaseClass:CheckNValidateEmail:EmailValidateError:" + ex.Message)
                        Valid = 2
                        SaveEmailVerifiedDirectSave(Session("cmp_id"), HttpContext.Current.Request.UserHostAddress.ToString(), EmailId.ToString(), 2, ex.Message.ToString())
                    End Try
                Else
                    Valid = Val(dtValidEmail.Rows(0)("Status"))
                    SaveEmailLogs(Session("cmp_id"), HttpContext.Current.Request.UserHostAddress.ToString(), EmailId.ToString(), Valid, "")
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("BaseClass:CheckNValidateEmail:EmailValidateError:" + ex.Message)
            Valid = 4
        End Try
        'SaveEmailVerify(cmp_id, EmailId, Valid, login_id, ip_address)
        Return Valid
    End Function

    Public Function CheckVerifyEmail(ByVal cmp_id As Integer, ByVal EmailId As String) As DataTable
        Dim dtEmail As DataTable
        Try
            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@cmp_id", Val(cmp_id), SqlDbType.Int)
            oclsParm.Add("@Email_Id", EmailId.ToString())
            dtEmail = oClsDataccess1.GetdataTableSp("Get_U_EmailVerify_List_FromEmail", oclsParm)
            Return dtEmail
        Catch ex As Exception
            LogHelper.Error("BaseClass:CheckVerifyEmail:" + ex.Message)
            Return dtEmail
        End Try
    End Function

    Public Sub SaveEmailVerify(ByVal cmp_id As Integer, ByVal EmailId As String, ByVal Status As Integer, ByVal login_id As Integer, ByVal ip_address As String)
        Try
            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@Tran_id", 0, SqlDbType.Int)
            oclsParm.Add("@cmp_id", Val(cmp_id), SqlDbType.Int)
            oclsParm.Add("@Email", EmailId.ToString())
            oclsParm.Add("@Status", Val(Status))
            oclsParm.Add("@login_id", Val(login_id))
            oclsParm.Add("@ip_address", ip_address.ToString())
            oclsParm.Add("@Tran_Type", "I")
            oClsDataccess1.ExecStoredProcedure("P_U_EmailVerify_Log_N_List_Save", oclsParm)
        Catch ex As Exception
            LogHelper.Error("BaseClass:SaveEmailVerify:" + ex.Message)
        End Try
    End Sub

    Public Sub SaveEmailLogs(ByVal cmp_id As Integer, ByVal ip_address As String, ByVal Email_id As String, ByVal Status As Integer, ByVal LogsVal As String)
        Try
            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@Tran_id", 0, SqlDbType.Int)
            oclsParm.Add("@cmp_id", Val(cmp_id), SqlDbType.Int)
            oclsParm.Add("@Email_Id", Email_id.ToString())
            oclsParm.Add("@Status", Status, SqlDbType.Int)
            oclsParm.Add("@login_id", Val(Session("login_id")), SqlDbType.Int)
            oclsParm.Add("@ip_address", ip_address.ToString())
            oclsParm.Add("@Tran_Type", "I")
            oclsParm.Add("@StatusLogs", LogsVal.ToString())
            oClsDataccess1.ExecStoredProcedure("P_U_EmailVerify_Log", oclsParm)
        Catch ex As Exception
            LogHelper.Error("BaseClass:SaveEmailVerifiedDirectSave:" + ex.Message)
        End Try
    End Sub

    Public Sub SaveEmailVerifiedDirectSave(ByVal cmp_id As Integer, ByVal ip_address As String, ByVal Email_id As String, ByVal Status As Integer, ByVal LogsVal As String)
        Try
            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@Tran_id", 0, SqlDbType.Int)
            oclsParm.Add("@cmp_id", Val(cmp_id), SqlDbType.Int)
            oclsParm.Add("@Work_Email", Email_id.ToString())
            oclsParm.Add("@Valid_work_email", Status, SqlDbType.Int)
            oclsParm.Add("@msgLogs", LogsVal.ToString())
            oclsParm.Add("@login_id", Val(Session("login_id")), SqlDbType.Int)
            oclsParm.Add("@ip_address", ip_address.ToString())
            oclsParm.Add("@Tran_Type", "I")
            oClsDataccess1.ExecStoredProcedure("P_U_EmailVerify_Log_List", oclsParm)
        Catch ex As Exception
            LogHelper.Error("BaseClass:SaveEmailVerifiedDirectSave:" + ex.Message)
        End Try
    End Sub

    Public Sub AddValueToGridFilter(ByVal RadGridMain As RadGrid, ByVal DefStr As String, ByVal Page_Name As String)
        Try
            For Each item As GridFilteringItem In RadGridMain.MasterTableView.GetItems(GridItemType.FilteringItem)

                For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
                    If row("FIN").ToString() <> "" And row("val").ToString() <> "" Then
                        Dim val As Integer = 0
                        Try
                            TryCast(item(row("FIN").ToString()).Controls(0), TextBox).Text = row("VAL").ToString
                        Catch ex As Exception
                            val = 1
                        End Try
                        If val = 1 Then
                            Try
                                If row("VAL") = "" Then
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = "ALL"
                                Else
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = row("VAL").ToString
                                End If
                            Catch ex As Exception
                                val = 2
                            End Try
                        End If
                        If val = 2 Then
                            Try
                                If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing And Not TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker) Is Nothing Then
                                    If row("val").ToString().Contains("01/01/1900") Then
                                        Dim arr As Array = row("val").ToString().Split("#")
                                        If arr(0).ToString.Contains("01/01/1900") = False Then
                                            TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
                                        End If
                                        If arr(1).ToString.Contains("01/01/1900") = False Then
                                            TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
                                        End If
                                    ElseIf row("val").ToString().Contains("#") Then
                                        Dim arr As Array = row("val").ToString().Split("#")
                                        TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
                                        TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
                                    End If
                                End If
                            Catch ex As Exception
                                val = 3
                            End Try
                        End If
                        If val = 3 Then
                            Try
                                If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing Then
                                    TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = row("VAL").ToString
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            LogHelper.Error("BaseClass:AddValueToGridFilter:" + ex.Message)
        End Try
    End Sub

    Public Sub RadGridFilterBind(ByVal RadGridMain As RadGrid, ByVal DefStr As String, ByVal Page_Name As String)
        Try
            Dim strVal As String = ""
            If HttpContext.Current.Session(DefStr + Page_Name) Is Nothing Then
                For Each column As GridColumn In RadGridMain.MasterTableView.Columns
                    strVal += column.UniqueName + "#"
                Next
                oSessionManager1.CreateSessionDT(Page_Name.ToString, strVal.ToString)
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
                If FilterExpression = "" Then
                    RadGridMain.MasterTableView.FilterExpression = String.Empty
                    RadGridMain.MasterTableView.Rebind()
                Else
                    RadGridMain.MasterTableView.FilterExpression = FilterExpression
                    RadGridMain.MasterTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
                    RadGridMain.MasterTableView.Rebind()
                End If

                AddValueToGridFilter(RadGridMain, DefStr, Page_Name)

            End If
        Catch ex As Exception
            LogHelper.Error("BaseClass:RadGridFilterBind:" + ex.Message)
        End Try
    End Sub

    Public Sub GenerateXML_BranchList()
        Try
            Dim oclsParm As New ColSqlparram
            Dim dtBranch As DataSet
            oclsParm.Add("@cmp_id", Val(HttpContext.Current.Session("cmp_id")), SqlDbType.Int)
            oclsParm.Add("@emp_id", Val(HttpContext.Current.Session("emp_id")), SqlDbType.Int)
            oclsParm.Add("@branch_right", IIf(HttpContext.Current.Session("branch_right") = "", "", HttpContext.Current.Session("branch_right")))
            dtBranch = oClsDataccess1.GetdatasetSp("GenerateBranch_List", oclsParm, "branch")

            If Not System.IO.File.Exists(Server.MapPath("..").ToString() + "\Files\branch\branch_" + HttpContext.Current.Session("cmp_id").ToString() + "_" + HttpContext.Current.Session("emp_id").ToString() + ".xml") Then
                dtBranch.WriteXml(Server.MapPath("..").ToString() + "\Files\branch\branch_" + HttpContext.Current.Session("cmp_id").ToString() + "_" + HttpContext.Current.Session("emp_id").ToString() + ".xml")
            Else
                If System.IO.File.GetLastWriteTime(Server.MapPath("..").ToString() + "\Files\branch\branch_" + HttpContext.Current.Session("cmp_id").ToString() + "_" + HttpContext.Current.Session("emp_id").ToString() + ".xml") < DateTime.Now.AddHours(-4) Then
                    dtBranch.WriteXml(Server.MapPath("..").ToString() + "\Files\branch\branch_" + HttpContext.Current.Session("cmp_id").ToString() + "_" + HttpContext.Current.Session("emp_id").ToString() + ".xml")
                Else
                    Dim ds As New DataSet
                    ds.ReadXml(Server.MapPath("..").ToString() + "\Files\branch\branch_" + Session("cmp_id").ToString() + "_" + Session("emp_id").ToString() + ".xml")
                    If ds.Tables.Count = 0 Then
                        dtBranch.WriteXml(Server.MapPath("..").ToString() + "\Files\branch\branch_" + Session("cmp_id").ToString() + "_" + Session("emp_id").ToString() + ".xml")
                    End If
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("BaseClass:GenerateXML_BranchList:" + ex.Message)
        End Try
    End Sub

End Class


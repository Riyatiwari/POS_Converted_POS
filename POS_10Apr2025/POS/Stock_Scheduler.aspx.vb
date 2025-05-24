Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel
Imports System.Web.Configuration
Imports System.IO
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Security.Authentication
Imports System.Net

Partial Class Stock_Scheduler
    Inherits BaseClass

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Process_Stock()

        Catch ex As Exception
            LogHelper.Error("Stock_Scheduler:page_load:" & ex.ToString())
        End Try

    End Sub

    Private Sub Process_Stock()
        Dim constring As String = ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString

        Using con As New SqlConnection(constring)
            Using cmd As New SqlCommand("select distinct db_server,db_username,db_password,db_name as db_name from WS_Store where isNull(Stock_Process,0) = 1 ", con)
                cmd.CommandType = CommandType.Text
                con.Open()
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        If dt.Rows.Count > 0 Then





                            For Each dr As DataRow In dt.Rows
                                Try


                                    Dim constring1 As String = "Data Source= '" + dr("db_server").ToString() + "';Initial Catalog= '" + dr("db_name").ToString() + "';User ID='" + dr("db_username").ToString() + "';Password= '" + Decode_data(dr("db_password").ToString()) + "'"
                                    Session("Con_string") = "Data Source= '" + dr("db_server").ToString() + "';Initial Catalog= '" + dr("db_name").ToString() + "';User ID='" + dr("db_username").ToString() + "';Password= '" + Decode_data(dr("db_password").ToString()) + "'"

                                    Dim oColSqlparram As ColSqlparram = New ColSqlparram()

                                    Dim dtS As DataTable = GetdataTable("P_Stock_Process", oColSqlparram)
                                    LogHelper.Error("Stock_Scheduler:Process_Stock:" & dr("db_name").ToString() & ": Sucess")

                                Catch ex As Exception
                                    LogHelper.Error("Stock_Scheduler:Process_Stock:" & dr("db_name").ToString() & ":" & ex.Message)
                                End Try
                            Next

                        End If

                    End Using
                End Using
            End Using
        End Using

    End Sub

    Public Function GetdataTable(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(Session("Con_string").ToString())
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 0

                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                    Dim parameter As New SqlClient.SqlParameter
                    parameter.ParameterName = oClsSqlParameter.ParaName
                    parameter.SqlDbType = oClsSqlParameter.ParaType
                    parameter.Value = oClsSqlParameter.ParaValue
                    parameter.Direction = oClsSqlParameter.ParaDirection
                    com.Parameters.Add(parameter)
                Next

                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Stock_Scheduler:GetdataTable:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function

    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(Session("store_ConnectionString").ToString())
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 0

                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                    Dim parameter As New SqlClient.SqlParameter
                    parameter.ParameterName = oClsSqlParameter.ParaName
                    parameter.SqlDbType = oClsSqlParameter.ParaType
                    parameter.Value = oClsSqlParameter.ParaValue
                    parameter.Direction = oClsSqlParameter.ParaDirection
                    com.Parameters.Add(parameter)
                Next

                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Stock_Scheduler:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function

    Public Overloads Sub MailTo_receipt(ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
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
            '    LogHelper.Error("Base_class: 2:" + System.DateTime.Now.ToString())
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
            'LogHelper.Error("Base_class: 3:" + System.DateTime.Now.ToString())
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

            ' LogHelper.Error("Base_class: 4:" + System.DateTime.Now.ToString())
            oSmtpclient.Send(oE_Mail)
            'LogHelper.Error("Base_class: 5:" + System.DateTime.Now.ToString())
        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Stock_Scheduler: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
        End Try
    End Sub

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function


End Class


Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports Telerik.Reporting
Imports iTextSharp.text.html
Imports System.Configuration
Imports System.Data.SqlClient.SqlConnection

Imports System.Windows.Forms
Imports System.Net.Mail
Imports System.Net.Configuration
Imports System.Web.Configuration

Partial Class Robo

    Inherits BaseClass
    Dim Subject As String
    Dim Body As String

    Dim conn1 As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("conn1").ConnectionString)

    Dim conn2 As New SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("conn2").ConnectionString)
    Dim htmlreportstore As String = ""

    Public Function Report(ByVal script As String, ByVal conection As SqlConnection) As DataTable
        Dim dt As New DataTable()
        Try

            Dim cmd As New SqlClient.SqlCommand()
            Dim adp As New SqlClient.SqlDataAdapter()
            Try
                Using connection As New SqlConnection(conection.ConnectionString.ToString)
                    connection.Open()
                    cmd.CommandTimeout = 0
                    cmd.CommandText = script
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = connection
                    adp.SelectCommand = cmd
                    dt.Clear()
                    adp.Fill(dt)
                End Using
            Catch ex As Exception
                LogHelper.Error("datatable:" & ex.Message)


                cmd = Nothing
                adp = Nothing
            End Try


            'If dt.Rows.Count > 0 Then
            '    htmlreportstore = HtmlReport(dt, conection)

            'End If
            Return dt
        Catch ex As Exception
            Return dt

        End Try

    End Function
    Private Sub Robo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            RequestMail()


        Catch ex As Exception

        End Try
    End Sub

    Public Sub RequestMail()
        Try


            Dim script2 As String = "DECLARE @Database TABLE (DbName SYSNAME) " +
                                    " DECLARE @DbName AS SYSNAME " +
                                    " SET @DbName = '' " +
                                    " create Table #table ( " +
                                    " Sales_count numeric(18,0), " +
                                    " Sales_tran_count numeric(18, 0), " +
                                    " Database_Name nvarchar(100), " +
                                    " last_sync_time datetime " +
                                    ") " +
                                    " INSERT INTO @Database (DbName)" +
                                    " SELECT NAME" +
                                    " FROM master.dbo.sysdatabases" +
                                    " WHERE NAME <> 'tempdb' and name <> 'POS_Controller'" +
                                    " ORDER BY NAME ASC" +
                                    " WHILE @DbName Is Not NULL" +
                                    " BEGIN" +
                                    "   SET @DbName = (" +
                                    "           SELECT MIN(DbName)" +
                                    "           FROM @Database" +
                                    "           WHERE DbName > @DbName" +
                                    "           )" +
                                    "	 declare @string nvarchar(max)" +
                                    "	 set @string = 'insert into #table " +
                                    "	 SELECT  (select count( * ) from ' + @DbName + '.dbo.m_sales),(select count( * ) from  ' + @DbName + '.dbo.t_sales),''' + @DbName + ''' , (select max(created_date) from ' + @DbName + '.dbo.M_Sales) '" +
                                    "	 print @string" +
                                    "	 BEGIN TRY" +
                                    "		 exec (@string)" +
                                    "	 END TRY " +
                                    " BEGIN CATCH " +
                                    "	 END CATCH " +
                                    " END " +
                                     " select *,GETDATE() AS Date,@@SERVERNAME AS 'Server Name'  from #table order by last_sync_time  " +
                                         "  drop table #table"



            Dim Datatbl As DataTable = Report(script2, conn2)
            Dim report1 As String = HtmlReport(Datatbl)

            Dim script As String = "DECLARE @Database TABLE (DbName SYSNAME) " +
                                       " DECLARE @DbName AS SYSNAME" +
                                        " SET @DbName = ''" +
                                        " create table #table ( " +
                                        " Sales_count numeric(18,0)," +
                                        " Database_Name nvarchar(100)," +
                                        " last_sync_time datetime," +
                                        " machine_name nvarchar(100)" +
                                        ") " +
                                     " INSERT INTO @Database (DbName) " +
                                        " Select NAME  " +
                                        " FROM master.dbo.sysdatabases " +
                                        " WHERE NAME <> 'tempdb' and name <> 'POS_Controller'" +
                                        " ORDER BY NAME ASC" +
                                        " WHILE @DbName IS NOT NULL" +
                                        " BEGIN" +
                                        "     SET @DbName = (" +
                                        "             SELECT MIN(DbName)" +
                                        "             FROM @Database" +
                                        "	            WHERE DbName > @DbName" +
                                        "	            )" +
                                        "	     declare @string nvarchar(max) " +
                                        "	     set @string = 'insert into #table " +
                                        "	     SELECT   count( * ) , ''' + @DbName + ''' ,  max(s.created_date) ,l.name" +
                                        "	        from ' + @DbName + '.dbo.M_Sales s" +
                                        "      left outer join ' + @DbName + '.dbo.m_machine l on l.machine_id = s.machine_id" +
                                        "	        group by l.name'" +
                                        "      print @string" +
                                        "      BEGIN TRY" +
                                        "          exec (@string)" +
                                        "        End Try" +
                                         "    BEGIN CATCH " +
                                         " END CATCH " +
                                         "    End" +
                                         " select *,GETDATE() AS Date,@@SERVERNAME AS 'Server Name'  from #table order by last_sync_time  " +
                                         "  drop table #table"



            Dim Datatbl2 As DataTable = Report(script, conn1)
            Dim report2 As String = HtmlReport2(Datatbl2)
            Dim completereport As String = (report1 + report2)
            SenMail(completereport)
        Catch ex As Exception
            Dim s As String = ex.Message.ToString()
        End Try
    End Sub
    Public Function HtmlReport(dtMailDetail As DataTable) As String

        Dim srtbhtmlreport As StringBuilder = New StringBuilder()
        'If ConfigurationManager.ConnectionStrings Then
        srtbhtmlreport.Append("<table width='100%' cellspacing='0' cellpadding='2' align='center' border='0' style='margin-top:10px'>")
        srtbhtmlreport.Append("<tr>" +
                             "<td align='left' style='font-size:larger'>Hi,</td>" +
                            "</tr>")
        srtbhtmlreport.Append("<tr>" +
                             "<td align='left' colspan='8' style='font-size:larger'> Date : " + dtMailDetail.Rows(0)("Date").ToString + "</td>" +
         "</tr>")
        srtbhtmlreport.Append("<tr>" +
                          "<td align='left' colspan='8' style='font-size:larger'> Server Name : " + dtMailDetail.Rows(0)("Server Name").ToString + "</td>" +
                              "</tr>")
        srtbhtmlreport.Append("</table>")


        'Select Case conection.ToString
        '    Case "conn2"

        '        reporttable = ConfigurationManager.ConnectionStrings("conn2").ConnectionString

        '        ConfigurationManager.ConnectionStrings = "conn2"
        srtbhtmlreport.Append("<table width='100%' cellspacing='0' cellpadding='2' align='center' border='1' style='margin-top:10px'>")
        srtbhtmlreport.Append("<tr align='center'>" +
                              "<td align='left' colspan='8' style='font-size:larger'>Sales_count </td>" +
                             "<td align='left' colspan='8' style='font-size:larger'>Database_Name </td>" +
                            "<td align='left' colspan='8' style='font-size:larger'>last_sync_time </td>" +
                            "<td align='left' colspan='8' style='font-size:larger'>Sales_tran_count </td>" +
                            "</tr>")
        Dim j As Integer

        For j = 0 To dtMailDetail.Rows.Count - 1
            srtbhtmlreport.Append("<tr>" +
                                "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(j)("Sales_count").ToString + "</td>" +
                                 "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(j)("Database_Name").ToString + "</td>" +
                                "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(j)("last_sync_time").ToString + "</td>" +
                                "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(j)("Sales_tran_count").ToString + "</td>" +
                         "</tr>")
        Next

        srtbhtmlreport.Append("</table>")
        Return srtbhtmlreport.ToString()
    End Function

    Public Function HtmlReport2(dtMailDetail As DataTable) As String

        Dim srtbhtmlreports As StringBuilder = New StringBuilder()
        srtbhtmlreports.Append("<table width='100%' cellspacing='0' cellpadding='2' align='center' border='1' style='margin-top:30px'>")
        srtbhtmlreports.Append("<tr align='center'>" +
                              "<td align='left' colspan='8' style='font-size:larger'>Sales_count </td>" +
                             "<td align='left' colspan='8' style='font-size:larger'>Database_Name </td>" +
                            "<td align='left' colspan='8' style='font-size:larger'>last_sync_time </td>" +
                            "<td align='left' colspan='8' style='font-size:larger'>machine_name </td>" +
                            "</tr>")
        Dim i As Integer

        For i = 0 To dtMailDetail.Rows.Count - 1
            srtbhtmlreports.Append("<tr>" +
                                "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(i)("Sales_count").ToString + "</td>" +
                                 "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(i)("Database_Name").ToString + "</td>" +
                                "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(i)("last_sync_time").ToString + "</td>" +
                                "<td align='left' colspan='8' style='font-size:larger'>" + dtMailDetail.Rows(i)("machine_name").ToString + "</td>" +
                         "</tr>")
        Next

        srtbhtmlreports.Append("</table>")


        srtbhtmlreports.Append("<table width='100%' style='margin-top:20px'>")
        srtbhtmlreports.Append("<tr>" +
                               "<td align='left' colspan='32' style='font-size:larger'>Thank You</td>" +
                               "</tr>")
        srtbhtmlreports.Append("</table>")
        Return srtbhtmlreports.ToString()


    End Function


    Public Sub SenMail(ByVal htmlreportstore As String)


        Dim tabledata As String = htmlreportstore

        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        Dim configurationFile As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.config")
        Dim mailSettings As MailSettingsSectionGroup = configurationFile.GetSectionGroup("system.net/mailSettings")

        MailServer_Port = mailSettings.Smtp.Network.Port
        MailServer = mailSettings.Smtp.Network.Host
        MailServer_Password = mailSettings.Smtp.Network.Password
        MailServer_UserName = mailSettings.Smtp.Network.UserName
        MailServer_Password = "Do4safet"
        MailServer_UserName = "bstenderpos@gmail.com"
        From_Email = mailSettings.Smtp.From
        'From_Email = "mitesh.m@technometrics.in"
        Ssl = True
        alas = "Tender POS Sync"


        Dim Email_CC As String
        Dim Email_BCC As String
        Dim oE_Mail As New MailMessage()
        oE_Mail.To.Clear()

        'oE_Mail.To.Add("bhaumik.joshi@technometrics.in")
        oE_Mail.To.Add("madhvanimitesh@gmail.com")
        'oE_Mail.CC.Add("")
        'oE_Mail.Bcc.Add("")



        oE_Mail.From = New MailAddress(From_Email, alas)
        oE_Mail.IsBodyHtml = True
        oE_Mail.Subject = Subject
        oE_Mail.Body = tabledata


        oE_Mail.Priority = MailPriority.High
        Dim oSmtpclient As New SmtpClient()
        oSmtpclient.Host = MailServer
        oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
        oSmtpclient.Port = MailServer_Port
        oSmtpclient.EnableSsl = Ssl
        oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network
        oSmtpclient.Send(oE_Mail)
    End Sub
End Class

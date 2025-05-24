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
Imports System
Imports System.Windows.Forms
Imports System.Net.Mail




Partial Class EatOut
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try
            'ReportViewer1.Visible = True
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)


            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))
            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)

            oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", txtForDate.SelectedDate))
            'sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", txtToDate.SelectedDate))

            oColSqlparram.Add("@location_id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))
            oColSqlparram.Add("@machine_id", IIf(radMachine.SelectedIndex > 0, radMachine.SelectedValue, "0"))
            oColSqlparram.Add("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            oColSqlparram.Add("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "Today"))

            Dim dt As DataTable


            dt = oClsDal.GetdataTableSp("WS_Get_HelpOUt", oColSqlparram)



            rptzSUmmary.DataSource = dt
            rptzSUmmary.DataBind()
            ViewState("emaildata") = dt
        Catch ex As Exception
            LogHelper.Error("EatOut:BindTable:" + ex.Message)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                Else
                    ViewState("view") = 0
                End If

                If Val(ViewState("view")) = 1 Then
                    divFunction.Visible = True
                Else
                    divFunction.Visible = False
                End If

                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now
                BindTable()
                binddll()
            End If
        Catch ex As Exception
            LogHelper.Error("EatOut:Page_Load:" + ex.Message)
        End Try


    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Eat Out"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("EatOut:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("EatOut:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub binddll()
        Try
            If radVenue.SelectedIndex = -1 Then
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("EatOut:binddll:" + ex.Message)
        End Try
    End Sub

    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radVenue.SelectedIndexChanged
        Try
            If radVenue.SelectedIndex = 0 Then
                radLocation.Items.Clear()
                radMachine.Items.Clear()
            Else
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
                radMachine.Items.Clear()
            End If

        Catch ex As Exception
            LogHelper.Error("EatOut:radVenue_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radLocation.SelectedIndexChanged
        Try
            If radLocation.SelectedIndex = 0 Then
                radMachine.Items.Clear()
            Else
                oclsBind.BindMachineByLocation(radMachine, Val(Session("cmp_id")), Val(radLocation.SelectedValue))
            End If
        Catch ex As Exception
            LogHelper.Error("EatOut:radLocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("EatOut:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    'Protected Sub btnpdf_Click(sender As Object, e As EventArgs)
    '    Try
    'Response.ContentType = "application/pdf"
    'Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
    'Response.Cache.SetCacheability(HttpCacheability.NoCache)
    'Dim stringWriter As StringWriter = New StringWriter()
    'Dim htmlTextWriter As HtmlTextWriter = New HtmlTextWriter(stringWriter)
    'dincgtion.RenderControl(htmlTextWriter)
    'Dim stringReader As StringReader = New StringReader(stringWriter.ToString())
    'Dim Doc As Document = New Document(PageSize.A4)
    'Dim htmlparser As HTMLWorker = New HTMLWorker(Doc)
    'PdfWriter.GetInstance(Doc, Response.OutputStream)
    ''Doc.Open()
    ''htmlparser.Parse(str)
    ''Doc.Close()
    ''Response.Write(Doc)
    ''Response.[End]()
    'Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
    'Response.ContentType = "application/pdf"
    'Response.Cache.SetCacheability(HttpCacheability.NoCache)
    'Dim sw As StringWriter = New StringWriter()
    'Dim hw As HtmlTextWriter = New HtmlTextWriter(sw)
    'dincgtion.RenderControl(hw)
    'Dim sr As StringReader = New StringReader(sw.ToString())
    'Dim pdfDoc As Document = New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
    'Dim htmlparser As HTMLWorker = New HTMLWorker(pdfDoc)
    'PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
    'Dim strpath As String = Server.MapPath("~") & "/Downloads"
    'Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, New FileStream(strpath, FileMode.Create))
    'pdfDoc.Open()
    'htmlparser.Parse(sr)
    'pdfDoc.Close()

    'Response.Write(pdfDoc)
    ''Response.[End]()
    '    Catch ex As Exception
    '        LogHelper.Error("EatOut:radDuration_SelectedIndexChanged:" + ex.Message)
    '    End Try
    'End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    'End Sub

    'Protected Sub btnemail_Click(sender As Object, e As EventArgs)
    '    Try
    '        If txtemail.Text = "" Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please Enter The Mail');", True)
    '            txtemail.Focus()
    '            Return
    '        End If
    '        If Not HtmlReport() = "" Then
    '            Dim report As String = HtmlReport()
    '            Dim mail As MailMessage = New MailMessage()
    '            Dim SmtpServer As SmtpClient = New SmtpClient("smtp.gmail.com")
    '            mail.From = New MailAddress("ABC@gmail.com")
    '            mail.[To].Add(txtemail.Text)
    '            mail.Subject = "Z-Report"
    '            mail.Body = "mail with attachment"
    '            Dim attachment As System.Net.Mail.Attachment
    '            attachment = New System.Net.Mail.Attachment(report)
    '            mail.Attachments.Add(attachment)
    '            SmtpServer.Port = 587
    '            SmtpServer.Credentials = New System.Net.NetworkCredential("username", "password")
    '            SmtpServer.EnableSsl = True
    '            SmtpServer.Send(mail)
    '            MessageBox.Show("mail Send")
    '        End If

    '    Catch ex As Exception
    '        Console.WriteLine(ex.ToString())
    '    End Try
    'End Sub
    Public Function HtmlReport() As String
        Dim srtbhtmlreport As StringBuilder = New StringBuilder()
        If Not ViewState("emaildata") Is Nothing Then
            Dim dt As DataTable = ViewState("emaildata")
            srtbhtmlreport.Append("<table width='70%' cellspacing='0' cellpadding='2' align='center' border='0' style='margin-top:10px'>")
            srtbhtmlreport.Append("<tr align='center'>" +
                                  "<td align='center' colspan='8' style='font-size:larger'>Z Report</td>" +
                                   "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                     "<td align='center' colspan='8' style='font-size:larger'>The Kings Head</td>" +
                                      "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                  "<td align='center' colspan='8' style='font-size:larger'>The Broadway</td>" +
                                   "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                  "<td align='center' colspan='8' style='font-size:larger'>Thatcham RG193HP</td>" +
                                   "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                  "<td align='center' colspan='8' style='font-size:larger'>PH +44 1635 862145</td>" +
                                   "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                  "<td align='center' colspan='8' style='font-size:larger'>CLAC Leisure Ltd Reg#07182565 VAT#102572452</td>" +
                                   "</tr>")


            srtbhtmlreport.Append("</table>")
            srtbhtmlreport.Append("<table width='70%' cellspacing='0' cellpadding='2' align='center' border='0' style='margin-top:10px'>")
            srtbhtmlreport.Append("<tr>" +
                                  "<td align='left' colspan='8' style='font-size:larger'>First Transaction Date :</td>" +
                                  "<td align='left' colspan='8' style='font-size:larger'>From Date:</td>" +
                                  "</tr>")
            srtbhtmlreport.Append("<tr>" +
                                 "<td align='left' colspan='8' style='font-size:larger'>Last Transaction Date :</td>" +
                                 "<td align='left' colspan='8' style='font-size:larger'>To Date:</td>" +
                                "</tr>")
            srtbhtmlreport.Append("<tr>" +
                                 "<td align='left' colspan='8' style='font-size:larger'>Number Of Transaction:</td>" +
                                 "<td align='left' colspan='8' style='font-size:larger'>Number Of Returns:</td>" +
                                 "</tr>")

            srtbhtmlreport.Append("</table>")
            srtbhtmlreport.Append("<table width='70%' cellspacing='0' cellpadding='2' align='center' border='1' style='margin-top:10px'>")
            srtbhtmlreport.Append("<tr>" +
                                  "<td align='left' colspan='8' style='font-size:larger'>Description</td>" +
             "<td align='left' colspan='8' style='font-size:larger'>Number</td>" +
             "</tr>")
            Dim i As Integer

            For i = 0 To dt.Rows.Count - 1
                srtbhtmlreport.Append("<tr>" +
                                                 "<td align='left' colspan='8' style='font-size:larger'>" + dt.Rows(i)("Description") + "</td>" +
                            "<td align='left' colspan='8' style='font-size:larger'>" + dt.Rows(i)("Number") + "</td>" +
                            "</tr>")
            Next

            srtbhtmlreport.Append("</table>")

        End If

        Return srtbhtmlreport.ToString()
    End Function

End Class

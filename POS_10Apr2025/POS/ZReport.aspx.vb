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

Imports System.Net.Configuration
Imports System.Web.Configuration



Partial Class ZReport
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
            oColSqlparram.Add("@salestype", IIf(radsalesType.SelectedIndex > 0, radsalesType.SelectedValue.ToString, "All"))
            oColSqlparram.Add("@shift_name", IIf(radshifttype.SelectedIndex > 0, radshifttype.SelectedItem.Value.ToString, "0"))
            oColSqlparram.Add("@login_id", IIf(rdOperator.SelectedIndex > 0, rdOperator.SelectedValue, "0"))

            Dim dt As DataTable

            dt = oClsDal.GetdataTableSp("WS_Get_Z_Report", oColSqlparram)

            'dt.Rows.Add("")

            If dt.Rows.Count > 0 Then
                tdAddress.InnerHtml = dt.Rows(0)("receipt_header").ToString()
                lblFirst.Text = dt.Rows(0)("Tran_First_Dt").ToString()
                lblLsttransction.Text = dt.Rows(0)("tran_last_dt").ToString()
                lblfromdate.Text = dt.Rows(0)("FromDate").ToString()
                lblTodate.Text = dt.Rows(0)("ToDate").ToString()
                lblNoofTransaction.Text = dt.Rows(0)("noofsales").ToString()
                lblReturn.Text = dt.Rows(0)("noofreturns").ToString()

                hf_NumOfTran.Value = Val(dt.Rows(0)("noofsales").ToString())
            End If

            rptzSUmmary.DataSource = dt
            rptzSUmmary.DataBind()
            ViewState("emaildata") = dt

        Catch ex As Exception
            LogHelper.Error("ZReport:BindTable:" + ex.Message)
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

                oclsBind.BindStaff(rdOperator, Val(Session("cmp_id")))

                binddll()
                getShifts()
                BindTable()
            End If
        Catch ex As Exception
            LogHelper.Error("ZReport:Page_Load:" + ex.Message)
        End Try
    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Z Report"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("ZReport:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("ZReport:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub binddll()
        Try
            If radVenue.SelectedIndex = -1 Then
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("ZReport:binddll:" + ex.Message)
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
            LogHelper.Error("ZReport:radVenue_SelectedIndexChanged:" + ex.Message)
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
            LogHelper.Error("ZReport:radLocation_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
                getShifts()
            Else
                divcustom.Visible = False
                getShifts()
            End If
        Catch ex As Exception
            LogHelper.Error("ZReport:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Public Sub getShifts()
        oclsBind.BindShiftByDuration(radshifttype, Val(Session("cmp_id")), IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate), IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate), (radDuration.SelectedItem.Value))
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
    '        LogHelper.Error("ZReport:radDuration_SelectedIndexChanged:" + ex.Message)
    '    End Try
    'End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    'End Sub

    Protected Sub btnemail_Click(sender As Object, e As EventArgs)
        Try

            If txtemail.Text IsNot "" Then
                If Not HtmlReport() = "" Then

                    Dim email As String
                    Dim Subject As String

                    email = txtemail.Text.ToString()

                    'madhvanimitesh@gmail.com
                    Subject = "Z Report"

                    LogHelper.Error("Z_Report:email_id:" & email)
                    MailTo_receipt(Val(Session("cmp_id")), Val(0), email, Subject, HtmlReport().ToString(), "", "", "")

                    Dim alrtMsg As String = "Email sent."
                    Dim script As String = "window.onload = function(){ alert('"
                    script &= alrtMsg
                    script &= "')};"
                    ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)


                    'Dim MailServer_UserName As String
                    'Dim MailServer_Password As String

                    'Dim configurationFile As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.config")
                    'Dim mailSettings As MailSettingsSectionGroup = configurationFile.GetSectionGroup("system.net/mailSettings")

                    'If Not mailSettings Is Nothing Then
                    '    MailServer_Password = mailSettings.Smtp.Network.Password
                    '    MailServer_UserName = mailSettings.Smtp.Network.UserName
                    'End If

                    'Dim report As String = HtmlReport()

                    'Dim message As New MailMessage()
                    'Dim fromAdd As MailAddress = New MailAddress(MailServer_UserName)
                    ''Dim fromAdd As MailAddress = New MailAddress("tmstab7@gmail.com")
                    'With message
                    '    .[To].Add(txtemail.Text)
                    '    .Subject = "Z Report"
                    '    .From = fromAdd
                    '    .IsBodyHtml = True
                    '    .Priority = MailPriority.Normal
                    '    .BodyEncoding = Encoding.Default
                    '    .Body = report
                    '    '.Body = "<center><table><tr><td><h1>Your Message TEST</h1><br/><br/></td></tr>"
                    '    '.Body = .Body + "</table></center>"
                    'End With
                    'Dim host = Request.Url.Host
                    'Dim smtpClient As New SmtpClient("smtp.gmail.com", "25")
                    'With smtpClient

                    '    .EnableSsl = True
                    '    .UseDefaultCredentials = False
                    '    .Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
                    '    '.Credentials = New System.Net.NetworkCredential("tmstab7@gmail.com", "TecHn0@2019")
                    '    .DeliveryMethod = SmtpDeliveryMethod.Network
                    '    .Send(message)
                    '    .Dispose()


                    'Dim alrtMsg As String = "Email sent."
                    '    Dim script As String = "window.onload = function(){ alert('"
                    '    script &= alrtMsg
                    '    script &= "')};"
                    '    ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)

                    'End With

                    txtemail.Text = ""
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please Enter The Mail');", True)
                txtemail.Focus()
                Return
            End If


        Catch ex As Exception

            Dim message As String = ex.Message
            Dim script As String = "window.onload = function(){ alert('"
            script &= message
            script &= "')};"
            ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)

            'Console.WriteLine(ex.ToString())
        End Try
    End Sub
    Public Function HtmlReport() As String

        Dim srtbhtmlreport As StringBuilder = New StringBuilder()
        If Not ViewState("emaildata") Is Nothing Then
            Dim VenName As String = ""
            If (radVenue.SelectedIndex > 0) Then
                VenName = radVenue.SelectedItem.Text
            Else
                VenName = ""
            End If
            Dim Adds As String = ""
            If (radLocation.SelectedIndex > 0) Then
                Dim conn As DBConnection = New DBConnection()
                Dim que = "select Address from M_Location where location_id = '" + radLocation.SelectedValue + "'"
                Dim ds As DataSet = conn.SelectData(que)
                If (ds IsNot Nothing) Then
                    Dim dtdt As DataTable = ds.Tables(0).Columns("Address").Table
                    Adds = dtdt.Rows(0)("Address").ToString()
                Else
                    Adds = ""
                End If
            Else
                Adds = ""
            End If

            Dim dt As DataTable = ViewState("emaildata")
            srtbhtmlreport.Append("<table width='100%' cellspacing='0' cellpadding='2' align='center' border='0' style='margin-top:10px'>")
            srtbhtmlreport.Append("<tr align='center'>" +
                              "<td align='center' colspan='8' style='font-size:larger'>Z Report</td>" +
                               "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                 "<td align='center' colspan='8' style='font-size:larger'>" + tdAddress.InnerHtml + "</td>" +
                                  "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                  "<td align='center' colspan='8' style='font-size:larger'>" + VenName + "</td>" +
                                   "</tr>")

            srtbhtmlreport.Append("<tr align='center'>" +
                                  "<td align='center' colspan='8' style='font-size:larger'>" + Adds + "</td>" +
                                   "</tr>")

            'srtbhtmlreport.Append("<tr align='center'>" +
            '                      "<td align='center' colspan='8' style='font-size:larger'>PH +44 1635 862145</td>" +
            '                       "</tr>")

            'srtbhtmlreport.Append("<tr align='center'>" +
            '                      "<td align='center' colspan='8' style='font-size:larger'>CLAC Leisure Ltd Reg#07182565 VAT#102572452</td>" +
            '                       "</tr>")


            srtbhtmlreport.Append("</table>")
            srtbhtmlreport.Append("<table width='100%' cellspacing='0' cellpadding='2' align='center' border='0' style='margin-top:10px'>")


            srtbhtmlreport.Append("<tr>" +
                              "<td align='left' colspan='8' style='font-size:larger'>First Transaction Date :</td>" +
                              "<td align='left' colspan='8' style='font-size:larger'> " + lblFirst.Text + " </td>" +
                              "</tr>")

            srtbhtmlreport.Append("<tr>" +
                             "<td align='left' colspan='8' style='font-size:larger'>Last Transaction Date :</td>" +
                             "<td align='left' colspan='8' style='font-size:larger'>" + lblLsttransction.Text + "</td>" +
                            "</tr>")

            srtbhtmlreport.Append("<tr>" +
                             "<td align='left' colspan='8' style='font-size:larger'>From Date :</td>" +
                             "<td align='left' colspan='8' style='font-size:larger'>" + lblfromdate.Text + "</td>" +
                            "</tr>")

            srtbhtmlreport.Append("<tr>" +
                             "<td align='left' colspan='8' style='font-size:larger'>To Date :</td>" +
                             "<td align='left' colspan='8' style='font-size:larger'>" + lblTodate.Text + "</td>" +
                            "</tr>")

            srtbhtmlreport.Append("<tr>" +
                             "<td align='left' colspan='8' style='font-size:larger'>Number Of Transaction:</td>" +
                             "<td align='left' colspan='8' style='font-size:larger'>" + lblNoofTransaction.Text + "</td>" +
                             "</tr>")

            srtbhtmlreport.Append("<tr>" +
                             "<td align='left' colspan='8' style='font-size:larger'>Number Of Returns:</td>" +
                             "<td align='left' colspan='8' style='font-size:larger'>" + lblReturn.Text + "</td>" +
                             "</tr>")


            srtbhtmlreport.Append("</table>")
            srtbhtmlreport.Append("<table width='100%' cellspacing='0' cellpadding='2' align='center' border='1' style='margin-top:10px'>")
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
    Protected Sub txtForDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub
    Protected Sub txtToDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub

    Protected Sub rptzSUmmary_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class

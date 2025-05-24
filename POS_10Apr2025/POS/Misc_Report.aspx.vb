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

Partial Class Misc_Report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)


            If Request.QueryString("Date") IsNot Nothing Then
                txtForDate.SelectedDate = Convert.ToDateTime(Request.QueryString("Date")).ToString("yyyy-MM-dd hh:mm tt")
                oColSqlparram.Add("@date1", Convert.ToDateTime(Request.QueryString("Date")).ToString("yyyy-MM-dd hh:mm tt"))
            Else
                oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
            End If

            If Request.QueryString("E_Date") IsNot Nothing Then
                txtToDate.SelectedDate = Convert.ToDateTime(Request.QueryString("E_Date")).ToString("yyyy-MM-dd hh:mm tt")
                divcustom.Visible = True

                oColSqlparram.Add("@date2", Convert.ToDateTime(Request.QueryString("E_Date")).ToString("yyyy-MM-dd hh:mm tt"))
            Else
                oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            End If

            If Request.QueryString("C_ID") IsNot Nothing Then
                oColSqlparram.Add("@cmp_id", Val(Request.QueryString("C_ID").ToString()))
            Else
                oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))
            End If

            If Request.QueryString("L_ID") IsNot Nothing And Request.QueryString("L_ID") IsNot "" Then
                radVenue.SelectedValue = Val(Request.QueryString("V_ID").ToString())
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(Request.QueryString("V_ID").ToString()))
                radLocation.SelectedValue = Val(Request.QueryString("L_ID").ToString())

                oColSqlparram.Add("@location_id", Val(Request.QueryString("L_ID").ToString()))
            Else
                oColSqlparram.Add("@location_id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))

            End If

            Dim dt As DataTable
            dt = oClsDal.GetdataTableSp("Misc_report", oColSqlparram)

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

                ViewState("view") = 1

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

    Protected Sub radLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radLocation.SelectedIndexChanged
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

    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As EventArgs)
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

End Class

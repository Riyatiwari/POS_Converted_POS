
Partial Class PDF_Demo
    Inherits System.Web.UI.Page

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        ReportViewer1.Visible = False
        Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
        Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

        Dim sourceReportSource = New Telerik.Reporting.UriReportSource() With {
             .Uri = Server.MapPath("~/Stock_Management_Detail.trdx")
        }
        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", 1039))
        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("stkrecid", 20690))

        'Me.ReportViewer1.ReportSource = sourceReportSource
        'Me.ReportViewer1.RefreshReport()

        RenderReport(sourceReportSource)
    End Sub

    Public Sub RenderReport(reportSource As Telerik.Reporting.ReportSource)
        Try
            Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
            Dim documentName As String = ""

            Dim deviceInfo As New System.Collections.Hashtable()
            deviceInfo("OutputFormat") = "PDF"

            Dim result As Telerik.Reporting.Processing.RenderingResult = reportProcessor.RenderReport("PDF", reportSource, deviceInfo)

            Dim fileName As String = result.DocumentName + "." + result.Extension
            Response.Clear()
            Response.ContentType = result.MimeType
            Response.Cache.SetCacheability(HttpCacheability.Private)
            Response.Expires = -1
            Response.Buffer = True
            Response.AddHeader("Content-Disposition", String.Format("{0};FileName=""{1}""", "attachment", fileName))
            Response.BinaryWrite(result.DocumentBytes)
            'Response.Flush()
            Response.End()
            'Response.Close()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:RenderReport:" + ex.Message)
        End Try

    End Sub

    Private Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        ReportViewer1.Visible = True
        Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
        Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

        Dim sourceReportSource = New Telerik.Reporting.UriReportSource() With {
             .Uri = Server.MapPath("~/Stock_Management_Detail.trdx")
        }

        ' Dim sourceReportSource = New Telerik.Reporting.UriReportSource()
        'sourceReportSource.Uri = Server.MapPath("\Stock_Management_Detail.trdx")
        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", 1039))
        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("stkrecid", 20690))

        'Me.ReportViewer1.ReportSource = sourceReportSource
        'Me.ReportViewer1.RefreshReport()

        RenderReport(sourceReportSource)
    End Sub
End Class

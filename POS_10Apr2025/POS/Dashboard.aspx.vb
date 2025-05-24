Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports Telerik.Web.UI

Partial Class Dashboard
    Inherits BaseClass
    Dim oclsBind As New clsBinding

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                ' page.clientscript.registerstartupscript(me.gettype(), "renderchart", string.format("renderchart({0});", jsonchartdata), true)
            End If
            Dim chartdata = GetDepartmentChartData(DateTime.Today.AddDays(-7), DateTime.Today, If(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"), IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "This Week"))


            Dim jsonchartdata As String = New JavaScriptSerializer().Serialize(chartdata)
            BindSalesLineChart()
            bindSyncStatus()
            BindPieChartData()

            If radVenue.SelectedIndex < 0 Then
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
                Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
                Dim con As New SqlConnection(connectionString)
                Dim dt As New DataTable()
                Dim cmd As New SqlCommand("WS_Get_Dashboard")
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_id")))
                cmd.Parameters.AddWithValue("@date1", "")
                cmd.Parameters.AddWithValue("@date2", "")
                cmd.Parameters.AddWithValue("@location_id", "0")
                cmd.Parameters.AddWithValue("@machine_id", "0")
                cmd.Parameters.AddWithValue("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
                cmd.Parameters.AddWithValue("@Display_Report", 2)
                cmd.Parameters.AddWithValue("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "This Week"))
                con.Open()
                dt.Load(cmd.ExecuteReader(), LoadOption.Upsert)
                con.Close()
                If dt.Rows.Count > 0 Then
                    For Each row In dt.Rows
                        tillchart.DataSource = dt
                        tillchart.PlotArea.XAxis.DataLabelsField = "display_name"
                    Next
                    tillchart.DataBind()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Page_Load:till_report:" + ex.Message)
        End Try
    End Sub



    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDepartmentChartData(ByVal date1 As DateTime, ByVal date2 As DateTime, ByVal venue_id As Integer, ByVal duration As String) As Object

        Try


            Dim chartData As New List(Of Object)()
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            'Dim con As New SqlConnection(connectionString)
            'Dim dt As New DataTable()
            'Dim command As New SqlCommand("GetDepartmentChartData", con)
            'command.CommandType = CommandType.StoredProcedure
            'command.Parameters.AddWithValue("@date1", date1)
            'command.Parameters.AddWithValue("@date2", date2)
            'command.Parameters.AddWithValue("@cmp_id", cmp_id)
            'command.Parameters.AddWithValue("@location_id", location_id)
            'command.Parameters.AddWithValue("@machine_id", machine_id)
            'con.Open()



            'Using sda As New SqlDataAdapter(command)
            '    sda.Fill(dt)

            '    If dt.Rows.Count > 0 Then
            '        For Each row As DataRow In dt.Rows
            '            Dim departmentName As String = row("DepartmentName").ToString()
            '            Dim dateValue As Date = Convert.ToDateTime(row("DateValue"))
            '            Dim value As Decimal = Convert.ToDecimal(row("Value"))
            '            Dim departmentData As New List(Of Object)()
            '            departmentData.Add(dateValue)
            '            departmentData.Add(value)

            '            Dim datasetObject = New With {
            '            .label = departmentName,
            '            .data = departmentData
            '        }
            '            chartData.Add(datasetObject)
            '        Next
            '    End If
            'End Using


            Dim ds As New DataSet()
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand("GetDepartmentChartData", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@date1", "")
                    cmd.Parameters.AddWithValue("@date2", "")
                    cmd.Parameters.AddWithValue("@duration", duration)
                    cmd.Parameters.AddWithValue("@venue_id", venue_id)


                    con.Open()
                    Dim sda As New SqlDataAdapter(cmd)
                    sda.Fill(ds)
                    If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dt As DataTable = ds.Tables(0)
                        For Each row As DataRow In dt.Rows
                            Dim departmentName As String = row("DepartmentName").ToString()
                            Dim dateValue As String = row("DayOfWeek").ToString()
                            Dim value As Decimal = Convert.ToDecimal(row("Value"))

                            Dim datasetObject = New With {
                                .label = departmentName,
                                .data = New Object() {dateValue, value}
                            }

                            chartData.Add(datasetObject)
                        Next
                    End If

                    'If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    '    Dim dt As DataTable = ds.Tables(0)
                    '    For Each row As DataRow In dt.Rows
                    '        Dim departmentName As String = ds.Tables(0).Rows("DepartmentName").ToString()
                    '        Dim dateValue As Date = Convert.ToDateTime(ds.Tables(0).Rows("DateValue"))
                    '        Dim value As Decimal = Convert.ToDecimal(ds.Tables(0).Rows("Value"))

                    '        Dim datasetObject = New With {
                    '            .label = departmentName,
                    '            .data = New Object() {dateValue, value}
                    '        }

                    '        chartData.Add(datasetObject)
                    '    Next
                    'End If

                End Using
            End Using



            Return chartData

        Catch ex As Exception
            LogHelper.Error("Page_Load:BindChartData:" + ex.Message)
        End Try
    End Function


    'Private Shared Function CalculateDailyData(connectionString As String, departmentName As String, day As Integer) As Integer
    '    Dim query As String = "SELECT SUM(Value) FROM DepartmentData WHERE DepartmentName = @DepartmentName AND DayOfWeek = @DayOfWeek"
    '    Dim dailyData As Integer = 0

    '    Using connection As New SqlConnection(connectionString)
    '        Using command As New SqlCommand(query, connection)
    '            command.Parameters.AddWithValue("@DepartmentName", departmentName)
    '            command.Parameters.AddWithValue("@DayOfWeek", day)
    '            connection.Open()
    '            Dim result As Object = command.ExecuteScalar()
    '            If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
    '                dailyData = Convert.ToInt32(result)
    '            End If
    '        End Using
    '    End Using

    '    Return dailyData
    'End Function


    'Private Shared Function GetRandomColor() As String
    '    Dim rand As New Random()
    '    Dim r As Integer = rand.Next(0, 256)
    '    Dim g As Integer = rand.Next(0, 256)
    '    Dim b As Integer = rand.Next(0, 256)
    '    Return $"rgba({r}, {g}, {b}, 1)"
    'End Function













    Private Sub BindPieChartData()
        Try
            Dim labels As New List(Of String)()
            Dim values As New List(Of Decimal)()

            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim con As New SqlConnection(connectionString)
            Dim ds As New DataSet()
            Dim cmd As New SqlCommand("GetPieChartData")
            cmd.Connection = con

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@date1", "")
            cmd.Parameters.AddWithValue("@date2", "")
            cmd.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_id")))
            cmd.Parameters.AddWithValue("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "This Week"))
            cmd.Parameters.AddWithValue("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, 0))
            Dim sda As New SqlDataAdapter(cmd)
            sda.Fill(ds)

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dt As DataTable = ds.Tables(0)
                For Each row As DataRow In dt.Rows
                    labels.Add(row("Category").ToString())
                    Dim value As Decimal = If(row("Value") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(row("Value").ToString()), Convert.ToDecimal(row("Value")), 0)
                    values.Add(value)
                Next
            Else
                labels.AddRange({"Food", "Beverages", "Other"})
                values.AddRange({0, 0, 0})


            End If

            Dim Data As New Dictionary(Of String, Object)()
            Data("labels") = labels
            Data("datasets") = New List(Of Object)()
            Data("datasets").Add(New With {
            .data = values,
            .backgroundColor = {
               "rgba(54, 162, 235, 0.8)",
                "rgba(255, 99, 132, 0.8)",
                "rgba(255, 159, 64, 0.8)",
                "rgba(153, 102, 255, 0.8)",
                "rgba(255, 206, 86, 0.8)",
                "rgba(75, 192, 192, 0.8)"
            }
        })

            Dim serializer As New JavaScriptSerializer()
            Dim jsonData As String = serializer.Serialize(Data)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "bindPieChart", "var pieChartData = " & jsonData & ";", True)
        Catch ex As Exception
            LogHelper.Error("Page_Load:BindPieChartData:" + ex.Message)
        End Try
    End Sub

    Private Sub BindSalesLineChart()
        Try
            Dim labels As New List(Of String)()
            Dim amounts As New List(Of Decimal)()
            Dim quantities As New List(Of Integer)()

            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")

            Dim con As New SqlConnection(connectionString)
            Dim ds As New DataSet()
            Dim cmd As New SqlCommand("GetSalesData")
            cmd.Parameters.AddWithValue("@date1", "")
            cmd.Parameters.AddWithValue("@date2", "")
            cmd.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_id")))
            cmd.Parameters.AddWithValue("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "This Week"))
            cmd.Parameters.AddWithValue("@venue_id", If(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            Dim sda As New SqlDataAdapter(cmd)
            sda.Fill(ds)

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    Dim name As String = If(row("name") IsNot DBNull.Value, row("name").ToString(), "")
                    Dim amount As Decimal = If(row("TotalAmount") IsNot DBNull.Value, Convert.ToDecimal(row("TotalAmount")), 0D)
                    Dim formattedAmount As String = amount.ToString("F2")
                    Dim quantity As Integer = If(row("TotalQuantity") IsNot DBNull.Value, Convert.ToInt32(row("TotalQuantity")), 0)
                    labels.Add(name)
                    amounts.Add(Convert.ToDecimal(formattedAmount))
                    quantities.Add(quantity)
                Next
            Else
                labels.Add("")
                amounts.Add(0D)
                quantities.Add(0)
            End If

            Dim chartData As New Dictionary(Of String, Object)()
            chartData("labels") = labels
            chartData("datasets") = New List(Of Object) From {
                New With {
                    .label = "Sales Amount",
                    .data = amounts,
                    .backgroundColor = "rgba(75, 192, 192, 0.2)",
        .borderColor = "rgba(75, 192, 192, 1)",
        .borderWidth = 1
    },
    New With {
        .label = "Sales Quantity",
        .data = quantities,
        .backgroundColor = "rgba(255, 99, 132, 0.2)",
        .borderColor = "rgba(255, 99, 132, 1)",
        .borderWidth = 1
    }
}

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonData As String = serializer.Serialize(chartData)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "bindSalesLineChart", "var salesLineChartData = " & jsonData & ";", True)

        Catch ex As Exception
            LogHelper.Error("Page_Load:BindSalesLineChart:" + ex.Message)
        End Try
    End Sub









    Private Sub radVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radVenue.SelectedIndexChanged
        Try
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim con As New SqlConnection(connectionString)
            Dim dt As New DataTable()
            Dim cmd As New SqlCommand("WS_Get_Dashboard")
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_id")))
            cmd.Parameters.AddWithValue("@date1", "")
            cmd.Parameters.AddWithValue("@date2", "")
            cmd.Parameters.AddWithValue("@location_id", "0")
            cmd.Parameters.AddWithValue("@machine_id", "0")
            cmd.Parameters.AddWithValue("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            cmd.Parameters.AddWithValue("@Display_Report", 2)
            cmd.Parameters.AddWithValue("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "This Week"))
            Dim sda As New SqlDataAdapter(cmd)
            sda.Fill(dt)

            If dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    tillchart.DataSource = dt
                    tillchart.PlotArea.XAxis.DataLabelsField = "display_name"
                Next
                tillchart.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim con As New SqlConnection(connectionString)
            Dim dt As New DataTable()
            Dim cmd As New SqlCommand("WS_Get_Dashboard")
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_id")))
            cmd.Parameters.AddWithValue("@date1", "")
            cmd.Parameters.AddWithValue("@date2", "")
            cmd.Parameters.AddWithValue("@location_id", "0")
            cmd.Parameters.AddWithValue("@machine_id", "0")
            cmd.Parameters.AddWithValue("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            cmd.Parameters.AddWithValue("@Display_Report", 2)
            cmd.Parameters.AddWithValue("@duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "This Week"))
            Dim sda As New SqlDataAdapter(cmd)
            sda.Fill(dt)

            If dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    tillchart.DataSource = dt
                    tillchart.PlotArea.XAxis.DataLabelsField = "display_name"
                Next
                tillchart.DataBind()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bindSyncStatus()
        Try

            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim con As New SqlConnection(connectionString)
            Dim ds As New DataSet()
            Dim dt As New DataTable()
            Dim cmd As New SqlCommand("WS_Get_SyncStatus")
            cmd.Connection = con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_id")))
            cmd.Parameters.AddWithValue("@date1", "")
            cmd.Parameters.AddWithValue("@date2", "")
            cmd.Parameters.AddWithValue("@duration", "This Month")
            Dim sda As New SqlDataAdapter(cmd)
            sda.Fill(ds)

            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then

                Session("dt1") = ds.Tables(1)

                imgPhoto.ImageUrl = "~/images/red_img.png"
                btnSendMail.Visible = True
            Else
                imgPhoto.ImageUrl = "~/images/green_img.png"
                btnSendMail.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Dashboard:bindSyncStatus:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSendMail_Click(sender As Object, e As EventArgs)
        Try

            Dim builder As New StringBuilder
            Dim email As String
            Dim Subject As String


            If Session("dt1") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(Session("dt1"), DataTable)

                builder.Append("<html> <head></head><body style='font-family:verdana;font-size:12px;'>")
                builder.Append("<div style='width:70%; color:#000000; font-family:verdana;'>")

                builder.Append("<table style='width:100%;height:150px; color:#000000;margin:5px;font-family:verdana;'>")
                builder.Append("<tr><td>")
                builder.Append("Hello Support Team, ")
                builder.Append("</td></tr>")
                builder.Append("<tr><td></td></tr>")
                builder.Append("<tr><td>")
                builder.Append("Sync Issue")
                builder.Append("</td></tr>")
                builder.Append("<tr><td></td></tr>")
                builder.Append("<tr><td><table border=1 width='80%' style='font-family:verdana;font-size:13px;border-collapse: collapse;'>")
                builder.Append("<tr><td width='30%'><b>Issue Date</b></td> <td><b>Description</b></td></tr>")

                For index = 0 To dt.Rows.Count - 1
                    builder.Append("<tr><td width='40%'>" + dt.Rows(index)("for_date").ToString() + "</td>")
                    builder.Append("<td>" + dt.Rows(index)("sync_desc").ToString() + "</td></tr>")
                Next

                builder.Append("</table></td></tr> ")
                builder.Append("<tr><td></td></tr>")
                builder.Append("<tr style='height: 20px;'><td> <b>Thanks, </b> <br/> TenderPOS. </td></tr>")

                builder.Append("</table>")

                builder.Append("</div>")
                builder.Append("</body> </html>")

                'email = "developer@technometrics.in"
                email = "support@tenderpos.com"
                'madhvanimitesh@gmail.com , mitesh.m@technometrics.in
                Subject = Session("cmp_name").ToString() + "Record Sync Status"

                MailTo_receipt(Val(Session("cmp_id")), Val(0), email, Subject, builder.ToString(), "", "mitesh@tenderpos.com", "")

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Mail sent successfully.');", True)

                Session("dt1") = Nothing
            End If


        Catch ex As Exception
            LogHelper.Error("Dashboard:btnSendMail_Click:" + ex.Message)
        End Try
    End Sub
End Class

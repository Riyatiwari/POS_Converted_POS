Imports System.Data

Partial Class Transaction_Detail
    Inherits System.Web.UI.Page

    Private Sub page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If

            Dim dt As DataTable = DirectCast(Session("dt"), DataTable)

            If (dt.Rows.Count > 0) Then
                Dim sb As StringBuilder = New StringBuilder()

                'Table start.
                sb.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>")
                'Adding HeaderRow.
                sb.Append("<tr>")
                sb.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'> Name </th>"))
                sb.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'> Till </th>"))
                sb.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'> Operator </th>"))
                sb.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'> Quantity </th>"))
                sb.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'> Total Amount </th>"))
                sb.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'> Tax </th>"))
                sb.Append(("<th style='background-color: #B8DBFD;border: 1px solid #ccc'> Date </th>"))
                sb.Append("</tr>")

                'Adding DataRow.
                For Each row As DataRow In dt.Rows
                    sb.Append("<tr>")
                    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" + row("Name").ToString() + " - " + row("size_name").ToString() + "</td>"))
                    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" + row("Till").ToString() + "</td>"))
                    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" + row("UserName").ToString() + "</td>"))
                    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" + row("quntity").ToString() + "</td>"))
                    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" + row("sales_total_amount").ToString() + "</td>"))
                    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" + row("sales_tax_amount").ToString() + "</td>"))
                    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" + row("created_date").ToString() + "</td>"))

                    sb.Append("</tr>")
                Next
                'Table end.
                sb.Append("</table>")
                ltTable.Text = sb.ToString

                Session("dt") = ""
            End If

        End If
    End Sub

End Class

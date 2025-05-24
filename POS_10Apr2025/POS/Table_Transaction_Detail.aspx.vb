Imports System.Data
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing

Partial Class Table_Transaction_Detail
    Inherits System.Web.UI.Page

    Private Sub Table_Transaction_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If

            Dim dt As DataTable = DirectCast(Session("dt"), DataTable)

            If (dt.Rows.Count > 0) Then


                'rptTranDetail.DataSource = dt
                'rptTranDetail.DataBind()

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
                    ''sb.Append("</tr>")
                    'For Each column As DataColumn In dt.Columns
                    '    sb.Append(("<td style='width:100px;border: 1px solid #ccc'>" _
                    '            + (row(column.ColumnName).ToString + "</td>")))
                    'Next
                    sb.Append("</tr>")
                Next
                'Table end.
                sb.Append("</table>")
                ltTable.Text = sb.ToString


                If Session("Tran_Report") = "Tran_Report" Then
                    footer.Visible = False
                    If Session("pay_type") = "1" Then
                        header.Visible = True
                    Else
                        header.Visible = False
                    End If

                    lblpayment_type.Text = dt.Rows(0)("Payment_Type").ToString()
                    lbltable_name.Text = dt.Rows(0)("table_name").ToString()
                    lblRef_No.Text = dt.Rows(0)("ref_id").ToString()
                    lblpayment_amt.Text = dt.Rows(0)("payment_amount").ToString()
                Else

                    lblSurcharge.Text = dt.Rows(0)("Surcharge_Amount").ToString()
                    lblPayment.Text = dt.Rows(0)("Payment_Type").ToString()
                    lblChange.Text = dt.Rows(0)("change").ToString()
                    lblTip.Text = dt.Rows(0)("tip_amount").ToString()
                    Try
                        'lbltable_name.Text = dt.Rows(0)("table_name").ToString()
                        lblTableStatus.Text = dt.Rows(0)("status").ToString()
                        lblPaymentamount.Text = dt.Rows(0)("Payment_Amount").ToString()
                    Catch ex As Exception

                    End Try


                    footer.Visible = True
                    header.Visible = False

                End If


                Session("Tran_Report") = ""
                Session("pay_type") = ""
                Session("dt") = ""
            End If

        End If
    End Sub

End Class

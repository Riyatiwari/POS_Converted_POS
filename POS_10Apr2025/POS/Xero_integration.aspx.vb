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
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates


Partial Class Xero_integration
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Dim oclsLocation As New Controller_clsLocation()
    Dim dt As DataTable

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
            End If
        Catch ex As Exception
            LogHelper.Error("Xero_integration:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub txtForDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs)
        txtToDate.SelectedDate = txtForDate.SelectedDate

    End Sub

    Protected Sub radVenue_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If radVenue.SelectedIndex = 0 Then
                radLocation.Items.Clear()
            Else
                oclsBind.BindLocationByVenue(radLocation, Val(Session("cmp_id")), Val(radVenue.SelectedValue))
            End If

        Catch ex As Exception
            LogHelper.Error("Xero_integration:radVenue_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub btn_view_Click(sender As Object, e As EventArgs)
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Xero_integration:btn_view_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub BindTable()
        Try

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
            oColSqlparram.Add("@location_id", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0"))
            oColSqlparram.Add("@venue_id", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedValue, "0"))
            oColSqlparram.Add("@duration", "Custom")


            dt = oClsDal.GetdataTableSp("Get_detail_for_Xero_integration", oColSqlparram)

            If dt.Rows.Count > 0 Then
                rdIntegration_list.DataSource = dt
                rdIntegration_list.DataBind()

                ViewState("detail") = dt
            Else
                rdIntegration_list.DataSource = String.Empty
                rdIntegration_list.DataBind()
            End If
        Catch ex As Exception
            LogHelper.Error("Xero_integration:BindTable:" + ex.Message)
        End Try
    End Sub

    Protected Sub btn_submit_Click(sender As Object, e As EventArgs)
        Try

            Dim dt As DataTable = DirectCast(ViewState("detail"), DataTable)
            Dim dv As DataView = dt.DefaultView

            Dim Sdt As DataTable = New DataTable()
            Dim Idt As DataTable = New DataTable()

            If Session("InvoiceList_start") IsNot Nothing Then

                Sdt = Session("InvoiceList_start")

                For Each column As Data.DataColumn In Sdt.Columns

                    Idt.Columns.Add(column.ColumnName)

                Next
            End If


            Dim str As String
            Dim not_in_str As String
            For Each item As RepeaterItem In rdIntegration_list.Items

                Dim oClsDal As ClsDataccess = New ClsDataccess()
                Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
                Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

                Dim hd_location_id As HiddenField = item.FindControl("hd_location_id")
                Dim hd_Date As HiddenField = item.FindControl("hd_Date")
                Dim hd_tax As HiddenField = item.FindControl("hd_tax")
                Dim chk_location_id As WebControls.CheckBox = item.FindControl("chk_location_id")
                If chk_location_id.Checked = True Then

                    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                    Dim FromDate As String
                    Dim ToDate As String
                    FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
                    ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)
                    oColSqlparram.Add("@date1", Convert.ToDateTime(hd_Date.Value.ToString()).ToString("yyyy-MM-dd hh:mm tt"))
                    oColSqlparram.Add("@date2", Convert.ToDateTime(hd_Date.Value.ToString()).ToString("yyyy-MM-dd hh:mm tt"))
                    oColSqlparram.Add("@location_id", Val(hd_location_id.Value.ToString()))
                    oColSqlparram.Add("@Tax", Val(hd_tax.Value.ToString()))
                    oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")))

                    Dim chkdt As DataTable = oClsDal.GetdataTableSp("Check_generated_xero_invoice", oColSqlparram)

                    If chkdt.Rows.Count > 0 Then
                        not_in_str += hd_location_id.Value.ToString() + ","
                        str = ""
                    Else

                        Dim d As DataRow = Sdt.NewRow()

                        dv.RowFilter = "location_id in (" + hd_location_id.Value.ToString() + ") and Date = '" + hd_Date.Value.ToString() + "' and Tax = " + hd_tax.Value.ToString() + "  "

                        d = dv.ToTable.Rows(0)

                        Idt.ImportRow(d)

                    End If

                End If
            Next

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            If Idt.Rows.Count > 0 Then

                Session("InvoiceList") = Idt

                For Each row As DataRow In Idt.Rows

                    oclsLocation.cmp_id = Val(Session("cmp_id"))
                    oclsLocation.location_id = Val(row("Location_id").ToString())
                    Dim dtLocation As DataTable = oclsLocation.Select()
                    If dtLocation.Rows.Count > 0 Then

                        Dim clientid As String = dtLocation.Rows(0)("clientid").ToString
                        Dim clientsecret As String = dtLocation.Rows(0)("clientsecret").ToString
                        Dim redirect_uri As String = dtLocation.Rows(0)("redirect_uri").ToString
                        Session("Store_name") = Session("store").ToString()
                        Session("cmp_ID") = Session("cmp_id").ToString()

                        Dim url As String = "https://login.xero.com/identity/connect/authorize?response_type=code&client_id=" + clientid + "&redirect_uri=" + redirect_uri + "&scope=accounting.transactions&state=12345678"

                        Dim script As String = "window.open('" & url & "', 'popup_window', 'width=450,height=500,left=450,top=50,resizable=no');"
                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "", script, True)

                    End If

                Next

            Else

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Can not push this invoice, It is already pushed to xero.');", True)

            End If


        Catch ex As Exception
            LogHelper.Error("Xero_integration:btn_submit_Click:" + ex.Message)
        End Try
    End Sub

    Private Function AcceptAllCertifications(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        Throw New NotImplementedException()
    End Function

    Public Function Encode_data(ByVal str As String) As String
        Try
            Dim s As String = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))

            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub rdIntegration_list_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try

            If e.Item.ItemType = ListItemType.Header Then
                Dim rdIntegration_list As Repeater = TryCast(e.Item.FindControl("Header1"), Repeater)
                rdIntegration_list.DataSource = dt.Columns
                rdIntegration_list.DataBind()

                Session("InvoiceList_start") = dt

            End If

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim rdIntegration_list As Repeater = TryCast(e.Item.FindControl("Item1"), Repeater)
                Dim row = TryCast(e.Item.DataItem, System.Data.DataRowView)
                rdIntegration_list.DataSource = row.Row.ItemArray
                rdIntegration_list.DataBind()


            End If

        Catch ex As Exception
            LogHelper.Error("Xero_integration:rdIntegration_list_ItemDataBound:" + ex.Message)
        End Try
    End Sub
End Class

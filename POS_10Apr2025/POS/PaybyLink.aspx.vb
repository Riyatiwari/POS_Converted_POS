Imports System.Data
Imports System.Data.SqlClient

Partial Class GetQR
    Inherits System.Web.UI.Page
    Dim oclspaybylink As New Controller_clsPayByLink
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim tid As Double = 0
        If Not IsPostBack = True Then
            lblMsg.Text = ""

            Dim amount As Double = Double.Parse(Request.QueryString("Amount").ToString())
            Dim refid As Double = Double.Parse(Request.QueryString("refid").ToString())
            Dim store As String = Request.QueryString("store").ToString()
            Dim lid As Double = Double.Parse(Request.QueryString("lid").ToString())
            Dim pay_uuid As String = Request.QueryString("payuuid").ToString()
            Try
                tid = Integer.Parse(Request.QueryString("tid").ToString())
            Catch ex As Exception

            End Try

            Session("cmp_id") = Request.QueryString("cv").ToString()
            Session("store") = store
            CheckStore(store)
            oclspaybylink.pay_uuid = pay_uuid
            oclspaybylink.till_id = tid

            Dim dt As DataTable = oclspaybylink.checkRequest()

            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0)(0).ToString() = "1") Then
                    If (dt.Rows(0)("paymentType").ToString() = "2") Then
                        Response.Redirect("cashflow.aspx?id=" + refid.ToString() + "&cid=" + Session("cmp_id").ToString() + "&lid=" + lid.ToString() + "&store=" + store + "&amount=" + amount.ToString() + "&payuuid=" + Request.QueryString("payuuid").ToString() + "&tid=" + tid.ToString())
                    ElseIf (dt.Rows(0)("paymentType").ToString() = "4") Then
                        Response.Redirect("CardStream.aspx?id=" + refid.ToString() + "&cid=" + Session("cmp_id").ToString() + "&lid=" + lid.ToString() + "&store=" + store + "&amount=" + amount.ToString() + "&payuuid=" + Request.QueryString("payuuid").ToString() + "&tid=" + tid.ToString())
                    Else
                        lblMsg.Text = "Can't process the request now, please try after sometime."
                    End If


                ElseIf (dt.Rows(0)(0).ToString() = "2") Then
                        lblMsg.Text = "This is already been paid. Thank you"

                End If
            End If
        End If
    End Sub
    Public Function Encode_data(ByVal str As String) As String
        Try
            Return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub CheckStore(ByVal store As String)
        Try

            strcon.Open()
            Dim n As String = store

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("password") = Decode_data(dt.Rows(0)("db_password"))
                Session("db_name") = dt.Rows(0)("db_name")
                Session("ConnectionString") = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Session("password") + ";"
            Else


            End If
            strcon.Close()
        Catch ex As Exception
            strcon.Close()
            LogHelper.Error("Paybylink:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub



End Class

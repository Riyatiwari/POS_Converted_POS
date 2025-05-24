
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Class CardStream
    Inherits System.Web.UI.Page
    Dim str As String = ""
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
    Dim oclsSales As New Controller_clsSales()
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProductGroup As New Controller_clsCategory
    Dim oclspaybylink As New Controller_clsPayByLink

    Dim oclsLocation As New Controller_clsLocation
    Private Sub CardStream_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim amount As Double = Double.Parse(Request.QueryString("amount").ToString())
            Dim payuuid As String = Request.QueryString("payuuid").ToString()
            CheckStore(Request.QueryString("store").ToString())
            oclsLocation.cmp_id = Val(Request.QueryString("cid").ToString())
            oclsLocation.location_id = Val(Request.QueryString("lid").ToString())
            Dim dt As DataTable = oclsLocation.Select_url()

            Dim key As String = dt(0)("api_key").ToString()
            Dim data As NameValueCollection = New NameValueCollection()
            data.Add("action", "SALE")
            data.Add("amount", (amount * 100))
            data.Add("countryCode", "826")
            data.Add("currencyCode", "826")
            data.Add("merchantID", dt(0)("config_id").ToString())
            data.Add("orderRef", "Test purchase")
            data.Add("redirectURL", "https://live.mytenderpos.com/CS_Success.aspx")
            'data.Add("callbackURL", "https://live.mytenderpos.com/CS_Success.aspx")
            data.Add("transactionUnique", payuuid)
            data.Add("type", "1")
            Dim sign As String = getSHA512(data, key)
            data.Add("signature", sign) '"3d28bd905ba390d088db93f371563c9f18de6bb6bc88513334ecd111bd30df11764d1b0337002d0aef64da2e67a5a00f4ca79e8d405a38ac4090ae4f56cb71c6")

            If Not (sign = "") Then
                'Response.Write(sign)
                Insert_IN_controller(Request.QueryString("id").ToString(), Session("store").ToString(), payuuid)

                RedirectAndPOST(Me.Page, dt(0)("url").ToString(), data)
            End If
        Catch ex As Exception

        End Try


    End Sub
    Public Sub Insert_IN_controller(ByVal sales_id As String, ByVal store_name As String, ByVal order_ref As String)
        Try

            strcon.Open()
            ' Dim n As String = Session("store").ToString() 'WebConfigurationManager.AppSettings("Store")

            Dim cmd As SqlCommand = New SqlCommand("M_Pay_check_add", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@sales_id", sales_id)
            cmd.Parameters.AddWithValue("@store_name", store_name)
            cmd.Parameters.AddWithValue("@order_ref", order_ref)
            cmd.Parameters.AddWithValue("@Tran_Type", "I")
            cmd.Parameters.AddWithValue("@source_type", "1")
            cmd.ExecuteNonQuery()


        Catch ex As Exception
            LogHelper.Error("paybylink_Cashflow:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Shared Sub RedirectAndPOST(ByVal page As Page, ByVal destinationUrl As String, ByVal data As NameValueCollection)
        Dim strForm As String = PreparePOSTForm(destinationUrl, data)
        page.Controls.Add(New LiteralControl(strForm))
    End Sub
    Private Shared Function PreparePOSTForm(ByVal url As String, ByVal data As NameValueCollection) As String
        Dim formID As String = "PostForm"
        Dim strForm As StringBuilder = New StringBuilder()
        strForm.Append("<form id=""" & formID & """ name=""" & formID & """ action=""" & url & """ method=""POST"">")

        For Each key As String In data
            strForm.Append("<input type=""hidden"" name=""" & key & """ value=""" & data(key) & """>")
        Next

        strForm.Append("</form>")
        Dim strScript As StringBuilder = New StringBuilder()
        strScript.Append("<script language='javascript'>")
        strScript.Append("var v" & formID & " = document." & formID & ";")
        strScript.Append("v" & formID & ".submit();")
        strScript.Append("</script>")
        Return strForm.ToString() + strScript.ToString()
    End Function

    Public Function getSHA512(ByVal data As NameValueCollection, ByVal Shakey As String) As String
        Try
            Dim haskey As String = ""
            For Each key As String In data
                Dim dataTemp As String = ""
                dataTemp = data(key)
                dataTemp = dataTemp.Replace(" ", "+")
                dataTemp = dataTemp.Replace("://", "%3A%2F%2F")
                dataTemp = dataTemp.Replace("/", "%2F")
                'dataTemp = dataTemp.Replace("\r", "%0A")
                'dataTemp = dataTemp.Replace("\n\r", "%0A")
                haskey += key + "=" + dataTemp + "&"

            Next
            '   haskey = haskey.Replace("currencyCode", "cyCode")
            haskey = System.Net.WebUtility.HtmlDecode(haskey)
            haskey = haskey.Substring(0, haskey.Length - 1)
            'haskey = haskey + Shakey

            Dim sha512 As SHA512 = SHA512Managed.Create()

            Dim bytes As Byte() = Encoding.UTF8.GetBytes(haskey + Shakey)
            Dim hash As Byte() = sha512.ComputeHash(bytes)

            Dim stringBuilder As New StringBuilder()

            For i As Integer = 0 To hash.Length - 1
                stringBuilder.Append(hash(i).ToString("x2"))
            Next

            Return stringBuilder.ToString()

        Catch ex As Exception
            Return ""
            Dim err As String = ex.Message.ToString()
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
            LogHelper.Error("Cardstream:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class

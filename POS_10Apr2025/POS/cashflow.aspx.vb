Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Security.Cryptography
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports System.Data
Imports System.Security.Authentication

Partial Class PaybyLink
    Inherits System.Web.UI.Page
    Dim oclsSales As New Controller_clsSales()
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProductGroup As New Controller_clsCategory
    Dim oclspaybylink As New Controller_clsPayByLink
    Dim str As String = ""
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)

    Dim oclsLocation As New Controller_clsLocation
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CheckStore(Request.QueryString("store").ToString())
            oclsLocation.cmp_id = Val(Request.QueryString("cid").ToString())
            oclsLocation.location_id = Val(Request.QueryString("lid").ToString())
            Dim dt As DataTable = oclsLocation.Select_url()
            LogHelper.Error("paybylink_Cashflow:Page_Load:1")
            If dt.Rows.Count > 0 Then
                LogHelper.Error("paybylink_Cashflow:Page_Load:2")
                Dim url As String = dt(0)("url").ToString()     '"https://gateway-int.cashflows.com/api/gateway/payment-jobs"
                Dim data As String = "{""amountToCollect"": '" + Request.QueryString("amount").ToString() + "' , ""currency"": ""GBP"", ""locale"": ""en_GB""}"
                Dim myReq As WebRequest = WebRequest.Create(url)
                myReq.Method = "POST"
                myReq.ContentLength = data.Length
                myReq.ContentType = "application/json; charset=UTF-8"
                Dim enc As UTF8Encoding = New UTF8Encoding()
                myReq.Headers.Add("configurationId", dt(0)("config_id").ToString())
                myReq.Headers.Add("hash", GenerateSHA512String(dt(0)("api_key").ToString() + data))
                LogHelper.Error("paybylink_Cashflow:Page_Load:3")
                '570c69cb14c5c447ec691a30189c3b092815cb773f936d96ce1ee2f49d88e13269a7439afb8647bb4700862d1aa3b069f746ce3f02b00bef2f6736dbb9dac2d5
                'myReq.Headers.Add("Content-Type", "application/json")

                Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
                Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
                ServicePointManager.SecurityProtocol = Tls12

                Using ds As Stream = myReq.GetRequestStream()
                    ds.Write(enc.GetBytes(data), 0, data.Length)
                End Using

                LogHelper.Error("paybylink_Cashflow:Page_Load:4" + url + " " + dt(0)("api_key").ToString() + " " + dt(0)("config_id").ToString())

                Dim wr As WebResponse = myReq.GetResponse()
                Dim receiveStream As Stream = wr.GetResponseStream()
                Dim reader As StreamReader = New StreamReader(receiveStream, Encoding.UTF8)
                Dim content As String = reader.ReadToEnd()
                'Response.Write(content)
                LogHelper.Error("paybylink_Cashflow:Page_Load:5")
                Dim ser As JObject = JObject.Parse(content)
                Dim data1 As List(Of JToken) = ser.Children().ToList
                Dim Url1 As String = ""
                Dim ordr_ref As String = ""

                For Each item As JProperty In data1
                    item.CreateReader()
                    Select Case item.Name
                        Case "data"
                            For Each item_ref As JProperty In item.Value
                                item_ref.CreateReader()
                                Select Case item_ref.Name
                                    Case "reference"
                                        ordr_ref = item_ref.Value.ToString()
                                End Select
                            Next

                        Case "links"
                            For Each item1 As JProperty In item.Value
                                item1.CreateReader()
                                Select Case item1.Name
                                    Case "action"
                                        For Each item2 As JProperty In item1.Value
                                            item2.CreateReader()
                                            Select Case item2.Name
                                                Case "url"
                                                    Url1 = item2.Value.ToString()

                                            End Select
                                        Next

                                        ' Url1 = item1.Value.ToString()

                                End Select
                            Next

                            ' Url1 = item.Value.ToString()

                    End Select
                Next


                '  UPDATE ORDER_REF NUMBER
                CheckStore(Request.QueryString("store").ToString())
                oclspaybylink.pay_uuid = Request.QueryString("payuuid").ToString()
                oclspaybylink.ref_id = ordr_ref
                Dim dtOrder As DataTable = oclspaybylink.UpdateOrderRequest()





                Insert_IN_controller(Request.QueryString("id").ToString(), Session("store").ToString(), ordr_ref)


                Response.Redirect(Url1)

            End If

        Catch ex As Exception
            LogHelper.Error("paybylink_Cashflow:Page_Load:" & ex.Message)
        End Try

    End Sub

    Public Shared Function GenerateSHA512String(ByVal inputString) As String
        Dim sha512 As SHA512 = SHA512Managed.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
        Dim hash As Byte() = sha512.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString()
    End Function

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

            'Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            'Dim dt As DataTable = New DataTable()
            'adp.Fill(dt)

            'If dt.Rows.Count > 0 Then
            '    'Session("db_server") = dt.Rows(0)("db_server")
            '    'Session("user_name") = dt.Rows(0)("db_username")
            '    'Session("password") = Decode_data(dt.Rows(0)("db_password"))
            '    'Session("db_name") = dt.Rows(0)("db_name")
            '    str = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Decode_data(dt.Rows(0)("db_password")) + ";"
            'Else
            'End If
        Catch ex As Exception
            LogHelper.Error("paybylink_Cashflow:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
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
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class

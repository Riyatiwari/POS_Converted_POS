Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class Xero_Redirected
    Inherits BaseClass

    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
    Dim con As SqlConnection = New SqlConnection()
    Dim col1() As String
    Dim col2() As String
    Dim col3() As String
    Dim col4() As String
    Dim col5() As String

    Private Sub Page_load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("InvoiceList") IsNot Nothing Then

            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim Sdt As DataTable = Session("InvoiceList")

            If Sdt.Rows.Count > 0 Then

                LogHelper.Error("Xero_Redirected 2: InvoiceList : " & Sdt.Rows.Count.ToString())
                CheckStore(Session("Store_name").ToString())

                con.Open()
                Dim cmd As SqlCommand = New SqlCommand("Get_M_Location_new", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@location_id", Val(Sdt.Rows(0)("Location_id").ToString()))
                cmd.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_ID").ToString()))
                Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim dtLocation As DataTable = New DataTable()
                adp.Fill(dtLocation)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                con.Close()

                If dtLocation.Rows.Count > 0 Then

                    LogHelper.Error("Xero_Redirected 3: dtLocation : " & dtLocation.Rows.Count.ToString())

                    Dim clientid As String = dtLocation.Rows(0)("clientid").ToString 'ConfigurationManager.AppSettings("clientid")
                    Dim clientsecret As String = dtLocation.Rows(0)("clientsecret").ToString 'ConfigurationManager.AppSettings("clientsecret")
                    Dim redirect_uri As String = dtLocation.Rows(0)("redirect_uri").ToString 'ConfigurationManager.AppSettings("redirect_uri")

                    If Request.QueryString("code") IsNot Nothing Then

                        LogHelper.Error("Xero_Redirected 4: code : " & Request.QueryString("code").ToString())

                        Dim code As String = Request.QueryString("code").ToString()

                        '-------------Get access_token-----------------------
                        Dim url As String = "https://identity.xero.com/connect/token"
                        Dim datadet As String = "grant_type=authorization_code" +
                                                    "&code=" + code +
                                                    "&redirect_uri=" + redirect_uri


                        Dim webrequest As HttpWebRequest = HttpWebRequest.Create(url)

                        webrequest.KeepAlive = False
                        webrequest.Method = "POST"
                        webrequest.Headers.Add("Authorization", "Basic " + Encode_data(clientid + ":" + clientsecret))
                        webrequest.ContentType = "application/x-www-form-urlencoded"

                        Using ds1 As StreamWriter = New StreamWriter(webrequest.GetRequestStream())
                            ds1.Write(datadet)
                        End Using

                        Dim httpResponse As HttpWebResponse = CType(webrequest.GetResponse(), HttpWebResponse)

                        LogHelper.Error("Xero_Redirected 5: access_token : ")

                        Dim token_type As String
                        Dim access_token As String
                        Dim refresh_token As String
                        Dim tenantId As String
                        Dim ContactID As String
                        Dim strRes As String
                        Using ds2 As StreamReader = New StreamReader(httpResponse.GetResponseStream())
                            strRes = ds2.ReadToEnd()
                            Dim ser_cust As JObject = JObject.Parse(strRes)
                            Dim data1_cust As List(Of JToken) = ser_cust.Children().ToList

                            For Each item As JProperty In data1_cust
                                item.CreateReader()
                                Select Case item.Name
                                    Case "access_token"
                                        access_token = item.Value.ToString()
                                    Case "token_type"
                                        token_type = item.Value.ToString()
                                    Case "refresh_token"
                                        refresh_token = item.Value.ToString()
                                End Select
                            Next

                        End Using


                        If access_token IsNot Nothing Then
                            '------------Get tenantId---------------
                            Dim url1 As String = "https://api.xero.com/connections"

                            LogHelper.Error("Xero_Redirected 6: tenantId : ")

                            Dim webrequest1 As HttpWebRequest = HttpWebRequest.Create(url1)

                            webrequest1.Method = "GET"
                            webrequest1.Headers.Add("Authorization", token_type + " " + access_token)
                            webrequest1.ContentType = "application/json"

                            Dim httpResponse1 As HttpWebResponse = CType(webrequest1.GetResponse(), HttpWebResponse)

                            Dim strRes1 As String
                            Dim str As String
                            Using ds1 As StreamReader = New StreamReader(httpResponse1.GetResponseStream())
                                strRes1 = ds1.ReadToEnd()

                                Dim result = JsonConvert.DeserializeObject(Of ArrayList)(strRes1)
                                str = result(0).ToString()
                                Dim ser_cust1 As JObject = JObject.Parse(str)
                                Dim data1 As List(Of JToken) = ser_cust1.Children().ToList

                                For Each item As JProperty In data1
                                    item.CreateReader()
                                    Select Case item.Name
                                        Case "tenantId"
                                            tenantId = item.Value.ToString()

                                    End Select
                                Next
                            End Using


                            '------create invoice -------
                            If tenantId IsNot Nothing Then

                                '-----------------Get ContactID --------------------

                                If dtLocation.Rows(0)("contactID").ToString() <> "" Then

                                    ContactID = dtLocation.Rows(0)("contactID").ToString()

                                Else

                                    '----------------------create contact ------------------

                                    Dim contact_name As String = Session("Store_name").ToString() + "_" + dtLocation.Rows(0)("name").ToString() + "_POS"
                                    Dim cust_URL As String = "https://api.xero.com/api.xro/2.0/Contacts"

                                    Dim cust_WebReq As HttpWebRequest = HttpWebRequest.Create(cust_URL)
                                    Dim cust_data As String = "{ ""Contacts"": [ { ""Name"": """ + contact_name + """ } ] }"

                                    cust_WebReq.Method = "PUT"
                                    cust_WebReq.ContentLength = cust_data.Length
                                    cust_WebReq.ContentType = "application/json"
                                    Dim enc As UTF8Encoding = New UTF8Encoding()
                                    cust_WebReq.Headers.Add("Authorization", token_type + " " + access_token)
                                    cust_WebReq.Headers.Add("Xero-Tenant-Id", tenantId)

                                    Using cust_ds As Stream = cust_WebReq.GetRequestStream()
                                        cust_ds.Write(enc.GetBytes(cust_data), 0, cust_data.Length)
                                    End Using

                                    Dim cust_wr As WebResponse = cust_WebReq.GetResponse()
                                    Dim rs_stream As Stream = cust_wr.GetResponseStream()

                                    Dim cust_reader As StreamReader = New StreamReader(rs_stream, Encoding.UTF8)
                                    Dim cust_content As String = cust_reader.ReadToEnd()

                                    Dim cust_obj As JObject = JObject.Parse(cust_content)
                                    Dim cust As List(Of JToken) = cust_obj.Children().ToList


                                    For Each item As JProperty In cust
                                        item.CreateReader()
                                        Select Case item.Name
                                            Case "Contacts"
                                                Dim value As String = item.Value.ToString()

                                                Dim _res = JsonConvert.DeserializeObject(Of ArrayList)(value)
                                                Dim res_val As String = _res(0).ToString()

                                                Dim ID As JObject = JObject.Parse(res_val)
                                                Dim c_ID As List(Of JToken) = ID.Children().ToList
                                                For Each item_ref As JProperty In c_ID
                                                    item_ref.CreateReader()
                                                    Select Case item_ref.Name
                                                        Case "ContactID"
                                                            ContactID = item_ref.Value.ToString()
                                                    End Select
                                                Next
                                        End Select
                                    Next

                                    '---------------- contactID insert into database ----------------
                                    If ContactID <> "" Then

                                        con.Open()

                                        Dim cmd1 As SqlCommand = New SqlCommand("Update_M_Location", con)
                                        cmd1.CommandType = CommandType.StoredProcedure

                                        cmd1.Parameters.AddWithValue("@location_id", Val(dtLocation.Rows(0)("location_id").ToString()))
                                        cmd1.Parameters.AddWithValue("@cmp_id", Val(dtLocation.Rows(0)("cmp_id").ToString()))
                                        cmd1.Parameters.AddWithValue("@ContactID", ContactID)
                                        cmd1.Parameters.AddWithValue("@Tran_Type", "U")
                                        cmd1.ExecuteNonQuery()
                                        cmd1.Dispose()
                                        con.Close()

                                    End If

                                End If

                                '----------------------------------------

                                If Sdt.Columns.Count = 9 Then

                                    col1 = Sdt.Columns(4).ToString().Split("_")
                                    col2 = Sdt.Columns(5).ToString().Split("_")
                                    col3 = Sdt.Columns(6).ToString().Split("_")
                                    col4 = Sdt.Columns(7).ToString().Split("_")
                                    col5 = Sdt.Columns(8).ToString().Split("_")


                                ElseIf Sdt.Columns.Count = 8 Then

                                    col1 = Sdt.Columns(4).ToString().Split("_")
                                    col2 = Sdt.Columns(5).ToString().Split("_")
                                    col3 = Sdt.Columns(6).ToString().Split("_")
                                    col4 = Sdt.Columns(7).ToString().Split("_")
                                    col5 = ("").Split("_")

                                ElseIf Sdt.Columns.Count = 7 Then

                                    col1 = Sdt.Columns(4).ToString().Split("_")
                                    col2 = Sdt.Columns(5).ToString().Split("_")
                                    col3 = Sdt.Columns(6).ToString().Split("_")
                                    col4 = ("").Split("_")
                                    col5 = ("").Split("_")
                                ElseIf Sdt.Columns.Count = 6 Then

                                    col1 = Sdt.Columns(4).ToString().Split("_")
                                    col2 = Sdt.Columns(5).ToString().Split("_")
                                    col3 = ("").Split("_")
                                    col4 = ("").Split("_")
                                    col5 = ("").Split("_")

                                ElseIf Sdt.Columns.Count = 5 Then

                                    col1 = Sdt.Columns(4).ToString().Split("_")
                                    col2 = ("").Split("_")
                                    col3 = ("").Split("_")
                                    col4 = ("").Split("_")
                                    col5 = ("").Split("_")

                                End If

                                '----------------------Get TAX----------------------
                                Dim taxDT As DataTable = New DataTable()
                                Dim Taxurl As String = "https://api.xero.com/api.xro/2.0/TaxRates"

                                Dim tReq As HttpWebRequest = HttpWebRequest.Create(Taxurl)

                                tReq.Method = "GET"
                                tReq.Headers.Add("Authorization", token_type + " " + access_token)
                                tReq.Headers.Add("Xero-Tenant-Id", tenantId)
                                tReq.ContentType = "application/json"

                                Dim getResponse1 As HttpWebResponse = CType(tReq.GetResponse(), HttpWebResponse)

                                Dim tstrRes1 As String
                                Using ds1 As StreamReader = New StreamReader(getResponse1.GetResponseStream())
                                    tstrRes1 = ds1.ReadToEnd()


                                    Dim jsonLinq = JObject.Parse(tstrRes1)

                                    ' Find the first array using Linq
                                    Dim srcArray = jsonLinq.Descendants().Where(Function(d) TypeOf d Is JArray).First()
                                    Dim trgArray = New JArray()
                                    For Each row As JObject In srcArray.Children(Of JObject)()
                                        Dim cleanRow = New JObject()
                                        For Each column As JProperty In row.Properties()
                                            ' Only include JValue types
                                            If TypeOf column.Value Is JValue Then
                                                cleanRow.Add(column.Name, column.Value)
                                            End If
                                        Next
                                        trgArray.Add(cleanRow)
                                    Next

                                    taxDT = JsonConvert.DeserializeObject(Of DataTable)(trgArray.ToString())

                                End Using

                                '-------------------------------------------------------

                                If taxDT.Rows.Count > 0 Then

                                    Dim tdt As DataTable = New DataTable()
                                    Dim dv As DataView = taxDT.DefaultView
                                    Dim d As DataRow = tdt.NewRow()

                                    Dim invoiceurl As String = "https://api.xero.com/api.xro/2.0/Invoices"

                                    For Each row As DataRow In Sdt.Rows

                                        Dim val1 As String
                                        Dim val2 As String
                                        Dim val3 As String
                                        Dim val4 As String
                                        Dim val5 As String
                                        Dim data As String
                                        Dim TaxType As String

                                        '-----------------SET TAXTYPE ---------------------
                                        dv.RowFilter = "ReportTaxType = 'OUTPUT' AND EffectiveRate=" + row("Tax").ToString() + " "
                                        d = dv.ToTable.Rows(0)
                                        TaxType = dv.ToTable.Rows(0)("TaxType")
                                        '-----------------SET TAXTYPE ---------------------

                                        Dim L_name As String = row("Location").ToString()
                                        Dim F_date As String = DateTime.Parse(row("Date").ToString()).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                                        Dim T_date As String = DateTime.Parse(row("Date").ToString()).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)

                                        If Sdt.Columns.Count = 9 Then

                                            val1 = row(Sdt.Columns(4).ToString()).ToString()
                                            val2 = row(Sdt.Columns(5).ToString()).ToString()
                                            val3 = row(Sdt.Columns(6).ToString()).ToString()
                                            val4 = row(Sdt.Columns(7).ToString()).ToString()
                                            val5 = row(Sdt.Columns(8).ToString()).ToString()

                                            data = "{ ""Invoices"": [ { ""Type"": ""ACCREC"",""Contact"": { ""ContactID"": """ + ContactID.ToString() + """ }, ""LineItems"": [ { ""Description"": """ + col1(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val1 + " , ""AccountCode"": """ + col1(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val1 + " } , { ""Description"": """ + col2(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val2 + ", ""AccountCode"": """ + col2(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val2 + "} , { ""Description"": """ + col3(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val3 + ", ""AccountCode"": """ + col3(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val3 + "},{ ""Description"": """ + col4(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val4 + ", ""AccountCode"": """ + col4(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val4 + "},{ ""Description"": """ + col5(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val5 + ", ""AccountCode"": """ + col5(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val5 + "}], ""Date"": """ + T_date + """, ""DueDate"": """ + T_date + """, ""Reference"": """ + L_name + "_" + F_date + "_" + T_date + """, ""Status"": ""AUTHORISED"" } ] }"

                                        ElseIf Sdt.Columns.Count = 8 Then

                                            val1 = row(Sdt.Columns(4).ToString()).ToString()
                                            val2 = row(Sdt.Columns(5).ToString()).ToString()
                                            val3 = row(Sdt.Columns(6).ToString()).ToString()
                                            val4 = row(Sdt.Columns(7).ToString()).ToString()
                                            val5 = "0"

                                            data = "{ ""Invoices"": [ { ""Type"": ""ACCREC"",""Contact"": { ""ContactID"": """ + ContactID.ToString() + """ }, ""LineItems"": [ { ""Description"": """ + col1(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val1 + " , ""AccountCode"": """ + col1(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val1 + " } , { ""Description"": """ + col2(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val2 + ", ""AccountCode"": """ + col2(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val2 + "} , { ""Description"": """ + col3(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val3 + ", ""AccountCode"": """ + col3(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val3 + "},{ ""Description"": """ + col4(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val4 + ", ""AccountCode"": """ + col4(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val4 + "}], ""Date"": """ + T_date + """, ""DueDate"": """ + T_date + """, ""Reference"": """ + L_name + "_" + F_date + "_" + T_date + """, ""Status"": ""AUTHORISED"" } ] }"

                                        ElseIf Sdt.Columns.Count = 7 Then

                                            val1 = row(Sdt.Columns(4).ToString()).ToString()
                                            val2 = row(Sdt.Columns(5).ToString()).ToString()
                                            val3 = row(Sdt.Columns(6).ToString()).ToString()
                                            val4 = "0"
                                            val5 = "0"

                                            data = "{ ""Invoices"": [ { ""Type"": ""ACCREC"",""Contact"": { ""ContactID"": """ + ContactID.ToString() + """ }, ""LineItems"": [ { ""Description"": """ + col1(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val1 + " , ""AccountCode"": """ + col1(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val1 + " } , { ""Description"": """ + col2(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val2 + ", ""AccountCode"": """ + col2(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val2 + "} , { ""Description"": """ + col3(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val3 + ", ""AccountCode"": """ + col3(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val3 + "}], ""Date"": """ + T_date + """, ""DueDate"": """ + T_date + """, ""Reference"": """ + L_name + "_" + F_date + "_" + T_date + """, ""Status"": ""AUTHORISED"" } ] }"


                                        ElseIf Sdt.Columns.Count = 6 Then

                                            val1 = row(Sdt.Columns(4).ToString()).ToString()
                                            val2 = row(Sdt.Columns(5).ToString()).ToString()
                                            val3 = "0"
                                            val4 = "0"
                                            val5 = "0"

                                            data = "{ ""Invoices"": [ { ""Type"": ""ACCREC"",""Contact"": { ""ContactID"": """ + ContactID.ToString() + """ }, ""LineItems"": [ { ""Description"": """ + col1(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val1 + " , ""AccountCode"": """ + col1(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val1 + " } , { ""Description"": """ + col2(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val2 + ", ""AccountCode"": """ + col2(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val2 + "}], ""Date"": """ + T_date + """, ""DueDate"": """ + T_date + """, ""Reference"": """ + L_name + "_" + F_date + "_" + T_date + """, ""Status"": ""AUTHORISED"" } ] }"


                                        ElseIf Sdt.Columns.Count = 5 Then
                                            val1 = row(Sdt.Columns(4).ToString()).ToString()
                                            val2 = "0"
                                            val3 = "0"
                                            val4 = "0"
                                            val5 = "0"

                                            data = "{ ""Invoices"": [ { ""Type"": ""ACCREC"",""Contact"": { ""ContactID"": """ + ContactID.ToString() + """ }, ""LineItems"": [ { ""Description"": """ + col1(0).ToString() + """, ""Quantity"": 1, ""UnitAmount"": " + val1 + " , ""AccountCode"": """ + col1(1).ToString() + """, ""TaxType"": """ + TaxType + """ , ""LineAmount"": " + val1 + " } ], ""Date"": """ + T_date + """, ""DueDate"": """ + T_date + """, ""Reference"": """ + L_name + "_" + F_date + "_" + T_date + """, ""Status"": ""AUTHORISED"" } ] }"

                                        End If


                                        Dim myReq As HttpWebRequest = HttpWebRequest.Create(invoiceurl)
                                        myReq.Method = "PUT"
                                        myReq.ContentLength = data.Length
                                        myReq.ContentType = "application/json"
                                        Dim enc As UTF8Encoding = New UTF8Encoding()
                                        myReq.Headers.Add("Authorization", token_type + " " + access_token)
                                        myReq.Headers.Add("Xero-Tenant-Id", tenantId)

                                        LogHelper.Error("Xero_Redirected 8: InvoiceID : req ")
                                        Using ds_cust As Stream = myReq.GetRequestStream()
                                            ds_cust.Write(enc.GetBytes(data), 0, data.Length)
                                        End Using

                                        Dim wr_cust As WebResponse = myReq.GetResponse()
                                        Dim receiveStream As Stream = wr_cust.GetResponseStream()

                                        LogHelper.Error("Xero_Redirected 9: InvoiceID : response ")

                                        Dim reader As StreamReader = New StreamReader(receiveStream, Encoding.UTF8)
                                        Dim content As String = reader.ReadToEnd()

                                        Dim inv_data As JObject = JObject.Parse(content)
                                        Dim inv As List(Of JToken) = inv_data.Children().ToList

                                        Dim inv_sts As String = ""
                                        Dim inv_ID As String = ""
                                        Dim inv_Number As String = ""

                                        For Each item As JProperty In inv
                                            item.CreateReader()
                                            Select Case item.Name
                                                Case "Status"
                                                    inv_sts = item.Value.ToString()

                                                Case "Invoices"
                                                    Dim value As String = item.Value.ToString()

                                                    Dim _res = JsonConvert.DeserializeObject(Of ArrayList)(value)
                                                    Dim res_val As String = _res(0).ToString()

                                                    Dim ID As JObject = JObject.Parse(res_val)
                                                    Dim c_ID As List(Of JToken) = ID.Children().ToList
                                                    For Each item_ref As JProperty In c_ID
                                                        item_ref.CreateReader()
                                                        Select Case item_ref.Name
                                                            Case "InvoiceNumber"
                                                                inv_Number = item_ref.Value.ToString()
                                                            Case "InvoiceID"
                                                                inv_ID = item_ref.Value.ToString()
                                                        End Select
                                                    Next
                                            End Select
                                        Next

                                        LogHelper.Error("Xero_Redirected 10: InvoiceID : ")

                                        con.Open()

                                        Dim cmd1 As SqlCommand = New SqlCommand("Insert_into_Integration_Xero", con)
                                        cmd1.CommandType = CommandType.StoredProcedure

                                        cmd1.Parameters.AddWithValue("@location_id", Val(row("location_id").ToString()))
                                        cmd1.Parameters.AddWithValue("@cmp_id", Val(Session("cmp_ID").ToString()))
                                        cmd1.Parameters.AddWithValue("@Process_Status", inv_sts)
                                        cmd1.Parameters.AddWithValue("@Process_type", inv_Number)
                                        cmd1.Parameters.AddWithValue("@Process_type_ID", inv_ID)
                                        cmd1.Parameters.AddWithValue("@ResponseValue", content)
                                        cmd1.Parameters.AddWithValue("@S_Date", Convert.ToDateTime(row("Date").ToString()).ToString("yyyy-MM-dd hh:mm tt"))
                                        cmd1.Parameters.AddWithValue("@E_Date", Convert.ToDateTime(row("Date").ToString()).ToString("yyyy-MM-dd hh:mm tt"))
                                        cmd1.Parameters.AddWithValue("@ip_address", "::1")
                                        cmd1.Parameters.AddWithValue("@Tran_Type", "I")
                                        cmd1.Parameters.AddWithValue("@cat1_name", col1(0).ToString())
                                        cmd1.Parameters.AddWithValue("@cat1_value", Val(val1))
                                        cmd1.Parameters.AddWithValue("@cat2_name", col2(0).ToString())
                                        cmd1.Parameters.AddWithValue("@cat2_value", Val(val2))
                                        cmd1.Parameters.AddWithValue("@cat3_name", col3(0).ToString())
                                        cmd1.Parameters.AddWithValue("@cat3_value", Val(val3))
                                        cmd1.Parameters.AddWithValue("@cat4_name", col4(0).ToString())
                                        cmd1.Parameters.AddWithValue("@cat4_value", Val(val4))
                                        cmd1.Parameters.AddWithValue("@cat5_name", col5(0).ToString())
                                        cmd1.Parameters.AddWithValue("@cat5_value", Val(val5))
                                        cmd1.Parameters.AddWithValue("@Tax", Val(row("Tax").ToString()))
                                        cmd1.ExecuteNonQuery()
                                        cmd1.Dispose()
                                        con.Close()

                                    Next

                                End If

                            End If

                        End If
                    End If


                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "CloseWindowScript", "window.close();", True)

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Invoice generated successfully');", True)
                End If


            End If

        End If

    End Sub



    Public Sub CheckStore(ByVal str As String)
        Try
            strcon.Open()

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", str)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("db_server") = dt.Rows(0)("db_server")
                Session("user_name") = dt.Rows(0)("db_username")
                Session("password") = Decode_data(dt.Rows(0)("db_password"))
                Session("db_name") = dt.Rows(0)("db_name")

                con = New SqlConnection("Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password"))

            Else
            End If
            strcon.Close()
        Catch ex As Exception
            strcon.Close()
            LogHelper.Error("Till:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
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

End Class

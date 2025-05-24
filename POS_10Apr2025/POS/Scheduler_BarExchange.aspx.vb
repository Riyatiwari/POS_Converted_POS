Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Security.Authentication
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports Newtonsoft.Json.Linq

Partial Class Scheduler_BarExchange
    Inherits BaseClass
    Dim oclsLocation As New Controller_clsLocation()
    Dim oclsPromotion As New Controller_clsPromotion()
    Dim oclsBarcodeExchange As New Controller_clsBarcodeExchange()
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Dim oClsDataccess As New ClsDataccess
    Dim created_date As String
    '  Dim modified_date As String

    Private Shared ReadOnly _httpClient As New HttpClient()
    Dim responseText As String
    Dim responseCode As Integer
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString

            Using con As New SqlConnection(constring)
                Using cmd As New SqlCommand("select store_name ,db_server,db_username,db_password, db_name from WS_Store where isbarstock=1 ", con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using sda As New SqlDataAdapter(cmd)
                        Using dt As New DataTable()
                            sda.Fill(dt)

                            If dt.Rows.Count > 0 Then

                                'Get all data from POS_controller 28102021
                                Dim dt1 As New DataTable()
                                Using con1 As New SqlConnection(constring)
                                    Using cmd1 As New SqlCommand("select * from WS_Store", con1)
                                        cmd1.CommandType = CommandType.Text
                                        con1.Open()
                                        Using sda1 As New SqlDataAdapter(cmd1)
                                            sda1.Fill(dt1)
                                        End Using
                                    End Using
                                End Using

                                Dim dv As New DataView(dt1)

                                For Each dr As DataRow In dt.Rows
                                    Try
                                        Dim constring1 As String = "Data Source= '" + dr("db_server").ToString() + "';Initial Catalog= '" + dr("db_name").ToString() + "';User ID='" + dr("db_username").ToString() + "';Password= '" + Decode_data(dr("db_password").ToString()) + "'"
                                        Session("Con_string") = "Data Source= '" + dr("db_server").ToString() + "';Initial Catalog= '" + dr("db_name").ToString() + "';User ID='" + dr("db_username").ToString() + "';Password= '" + Decode_data(dr("db_password").ToString()) + "'"




                                        Dim oColSqlparram As New ColSqlparram()

                                        Dim store As String
                                        ViewState("store") = dr("Store_name").ToString()

                                        'CheckStore(Request.QueryString("store").ToString())
                                        'Dim ds As DataTable = oclsBarcodeExchange.Get_stockRecords()

                                        'Using conn As New SqlConnection(constring1)
                                        '    conn.Open()


                                        '    Dim storeName As String
                                        '    Dim cmd2 As New SqlCommand("SELECT name FROM m_company", conn)


                                        '    Using reader As SqlDataReader = cmd2.ExecuteReader()
                                        '        If reader.Read() Then
                                        '            storeName = reader("name").ToString()
                                        '            store = storeName
                                        '        End If
                                        '    End Using
                                        'End Using

                                        'Dim oColSqlparram1 As ColSqlparram = New ColSqlparram()
                                        If Session("Con_string") IsNot Nothing Then
                                            'Dim dtlogin As DataTable = GetdataTableSp("Get_barRecords", oColSqlparram)

                                            'If dtlogin.Rows.Count > 0 AndAlso Convert.ToInt32(dt.Rows(0)("is_BarStockExchange")) = 1 Then
                                            '    store = dt.Rows(0)("name")

                                            LogHelper.Error("Scheduler_BarExchange:price_Click: StoreName: " & ViewState("store"))


                                            sendSales()
                                                getPrice()

                                            'End If
                                        Else
                                            LogHelper.Error("Page_Load: Session('store_ConnectionString') is not set.")
                                        End If

                                    Catch ex As Exception
                                        LogHelper.Error("Stock_Scheduler:Process_Stock:" & dr("db_name").ToString() & ":" & ex.Message)
                                    End Try
                                Next

                            End If

                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception
            LogHelper.Error("Scheduler_BarExchange:Page_Load" + ex.Message)
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
            LogHelper.Error("Scheduler:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function




    Private Function getPrice()
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        Dim oColSqlparram As New ColSqlparram()
        'Dim dt As DataTable = oclsPromotion.Select_urlforsales()
        Dim dt As DataTable = GetdataTableSp("Get_Api_requestSales", oColSqlparram)
        Dim ItemsNr As String
        Dim prod_id As String

        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()
            'Dim dt1 As DataTable = oclsBarcodeExchange.Select_GetPrice()
            Dim oColSqlparram1 As New ColSqlparram()
            Dim dt1 As DataTable = GetdataTableSp("GetBarstockItems", oColSqlparram1)
            For Each row2 As DataRow In dt1.Rows

                prod_id = row2("Product_id").ToString()
                ItemsNr = row2("other_id").ToString()
                Dim token As String = value
                Dim format As Integer = 2
                Dim priceFactor As Integer = 100
                Dim ItemNrs As String = ItemsNr
                Dim url As String = ApiUrl & "?Token=" & token & "&Mode=3" & "&PriceFactor=" & priceFactor & "&ItemNr=" & prod_id & "&prod_id=" & prod_id

                LogHelper.Error("Scheduler_BarExchange:price_Click: Start request")
                LogHelper.Error("Scheduler_BarExchange:price_Click: Url: " & url)
                Dim responseText As String = SendRequest(url)
                responseText = responseText.Trim()

                Dim parts As String() = responseText.Split("|"c)
                Dim other_id As String = String.Empty
                Dim price As Decimal = 0
                For i As Integer = 0 To parts.Length - 1 Step 2
                    If parts(i) = "ItemNr" Then
                        other_id = parts(i + 1)
                    ElseIf parts(i) = "ItemPrice" Then
                        price = Decimal.Parse(parts(i + 1))
                        'price /= 100
                    End If
                Next
                If String.IsNullOrEmpty(other_id) Or price <= 0 Then
                    responseText = "Invalid response received."
                Else
                    Dim oColSqlparram2 As New ColSqlparram()

                    'oclsBarcodeExchange.other_id = prod_id

                    'Dim dt2 As DataTable = oclsBarcodeExchange.update_GetPrice()
                    oColSqlparram2.Add(New ClsSqlParameter("@price", price))
                    oColSqlparram2.Add(New ClsSqlParameter("@other_id", prod_id))
                    Dim dt2 As DataTable = GetdataTableSp("updateBarstockItems", oColSqlparram2)
                    LogHelper.Error("Scheduler_BarExchange:price_Click: ResponseText:  " & responseText & "Price update Successfully")
                    ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Items updated successfully. Response Code: " & responseCode & "');", True)
                End If

                LogHelper.Error("Scheduler_BarExchange:price_Click:End request")
                LogHelper.Error("Scheduler_BarExchange:price_Click: ResponseText: " & responseText)
                LogHelper.Error("Scheduler_BarExchange:price_Click: ResponseCode: " & responseCode)

            Next

        Next
    End Function



    Private Function sendSales()
        Dim oColSqlparram As New ColSqlparram()
        'Dim dt As DataTable = oclsPromotion.Select_urlforsales()
        Dim dt As DataTable = GetdataTableSp("Get_Api_requestSales", oColSqlparram)
        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()
            Dim Itemsqty As String
            Dim ItemsNr As String
            Dim prod_id As String
            Dim tsales_id As String
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim maxAttempts As Integer = 5
            Dim attempt As Integer = 0
            Dim success As Boolean = False


            Do While attempt < maxAttempts AndAlso Not success
                'Dim dt1 As DataTable = oclsBarcodeExchange.Select_Sendsales()
                Dim oColSqlparram1 As New ColSqlparram()
                'Dim dt As DataTable = oclsPromotion.Select_urlforsales()
                Dim dt1 As DataTable = GetdataTableSp("GetBarstockSales", oColSqlparram1)

                If dt1.Rows.Count = 0 Then
                    Exit Do ' Exit the loop if dt1 has no rows
                End If
                If dt1.Rows.Count > 0 Then
                    Dim row2 As DataRow = dt1.Rows(0)
                    ItemsNr = row2("other_id").ToString()
                    prod_id = row2("Product_id").ToString()
                    Itemsqty = row2("quntity").ToString()
                    tsales_id = row2("tsales_id").ToString()


                    Dim token As String = value


                    Dim itemNumbers As String = ItemsNr
                    Dim pr_id As String = prod_id
                    Dim itemQuantities As String = Itemsqty



                    Dim url As String = ApiUrl & "?Token=" & token & "&Mode=4&PriceFactor=100&ItemNr=" & prod_id & "&ItemQty=" & itemQuantities & "&ProdId=" & pr_id


                    LogHelper.Error("Scheduler_BarExchange:sales_Click: Url: " & url)
                    LogHelper.Error("Scheduler_BarExchange:sales_Click: Start request")
                    Dim responseText As String = SendRequest(url).Trim()
                    LogHelper.Error("Scheduler_BarExchange:sales_Click: End request")
                    LogHelper.Error("Scheduler_BarExchange:sales_Click: ResponseText: " & responseText)
                    LogHelper.Error("Scheduler_BarExchange:sales_Click: ResponseCode: " & responseCode)
                    LogHelper.Error("Scheduler_BarExchange:sales_Click: Otherid: " & ItemsNr)
                    'responseText = responseText.Trim()

                    If responseCode = 200 Then
                        If responseText.Contains("InvalidItemNr") Then
                            LogHelper.Error("Scheduler_BarExchange:sales_Click: InvalidItemNr received. OtherId is: " & ItemsNr)
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "invalidItemNr", "alert('InvalidItemNr received. Please send valideItem number.');", True)
                        Else

                            oclsBarcodeExchange.tsales_id = tsales_id
                            oclsBarcodeExchange.Product_id = prod_id
                            oclsBarcodeExchange.other_id = prod_id
                            ' Dim dt2 As DataTable = oclsBarcodeExchange.update_barstock()
                            Dim oColSqlparram3 As New ColSqlparram()
                            oColSqlparram3.Add(New ClsSqlParameter("@tsales_id", tsales_id))
                            oColSqlparram3.Add(New ClsSqlParameter("@product_id", prod_id))
                            oColSqlparram3.Add(New ClsSqlParameter("@other_id", prod_id))
                            Dim dt2 As DataTable = GetdataTableSp("update_barstockpush", oColSqlparram3)

                            LogHelper.Error("Scheduler_BarExchange:sales_Click: ResponseText: " & responseText)
                            LogHelper.Error("Scheduler_BarExchange:sales_Click: ResponseCode: " & responseCode)
                            LogHelper.Error("Scheduler_BarExchange:sales_Click: tsales id: " & tsales_id)
                            LogHelper.Error("Scheduler_BarExchange:sales_Click: Product Id: " & prod_id)

                            If String.IsNullOrEmpty(responseText) Then
                                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Sales sent successfully. Response Code: " & responseCode & "');", True)
                            Else
                                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Sales sent successfully. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                            End If
                        End If
                    Else
                        If String.IsNullOrEmpty(responseText) Then
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed to send Sales. Response Code: " & responseCode & "');", True)
                        Else
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed to send Sales. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                        End If
                    End If
                End If
                attempt += 1
            Loop

        Next
    End Function
    Private Function SendRequest(url As String, Optional content As String = Nothing) As String


        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        request.Method = "GET"

        Try
            Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
                responseCode = CType(response.StatusCode, Integer)
                Using reader As New StreamReader(response.GetResponseStream())
                    Return reader.ReadToEnd()
                End Using
            End Using
        Catch ex As WebException
            If ex.Response IsNot Nothing Then
                Using response As HttpWebResponse = DirectCast(ex.Response, HttpWebResponse)
                    responseCode = CType(response.StatusCode, Integer)
                    Using reader As New StreamReader(response.GetResponseStream())
                        Return reader.ReadToEnd()
                    End Using
                End Using
            Else
                responseCode = 0
                Return ex.Message
            End If
        End Try
    End Function
    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(Session("Con_string").ToString())
                connection.Open()
                com.CommandText = SPName
                com.CommandType = CommandType.StoredProcedure
                com.Connection = connection
                com.Parameters.Clear()
                com.CommandTimeout = 0

                For Each oClsSqlParameter As ClsSqlParameter In oColSqlparram
                    Dim parameter As New SqlClient.SqlParameter
                    parameter.ParameterName = oClsSqlParameter.ParaName
                    parameter.SqlDbType = oClsSqlParameter.ParaType
                    parameter.Value = oClsSqlParameter.ParaValue
                    parameter.Direction = oClsSqlParameter.ParaDirection
                    com.Parameters.Add(parameter)
                Next

                SpAdepter = New SqlDataAdapter(com)
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Send_mail_APK:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function

End Class

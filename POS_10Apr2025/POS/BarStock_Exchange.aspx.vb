Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Security.Authentication
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports Newtonsoft.Json.Linq

Partial Class BarStock_Exchange
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
    'Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Try


    '        oclsLocation.location_id = IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")
    '        oclsLocation.created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)

    '        Dim dt As DataTable = oclsLocation.Select_urlforCloud()

    '        For Each row1 As DataRow In dt.Rows
    '            Dim store As String = row1("Storename").ToString()
    '            Dim value As String = row1("value").ToString()
    '            Dim ApiUrl As String = row1("Url").ToString()
    '            Dim machineIdFound As Boolean = False



    '            Dim dt2 As DataTable = oclsLocation.Select_tsalesCloud()


    '            If dt2.Rows.Count > 0 Then
    '                For Each row2 As DataRow In dt2.Rows
    '                    ViewState("machine_id") = row2("machine_id").ToString()
    '                    ViewState("quntity") = row2("quntity").ToString()
    '                    ViewState("salesID") = row2("salesID").ToString()
    '                    If IsDBNull(row2("cassette")) Then
    '                        ViewState("cassette") = "0"
    '                    Else
    '                        ViewState("cassette") = row2("cassette").ToString()
    '                    End If


    '                    If Not String.IsNullOrEmpty(ViewState("machine_id")) Then
    '                        Dim location_id As Integer = row1("Location_id").ToString()
    '                        Dim machine_id As Integer = Convert.ToInt32(ViewState("machine_id"))
    '                        Dim timestamp As String = oclsLocation.created_date.ToString("yyyy-MM-ddTHH:mm:ssZ") '("2023-01-01T00:00:00Z")  
    '                        Dim reading As Integer = Convert.ToInt32(ViewState("quntity"))
    '                        Dim cassette As Integer = ViewState("cassette")
    '                        'Dim alertCode As Integer = 777

    '                        Dim url As String = ApiUrl   'https://0gppl7d7y4.execute-api.eu-west-1.amazonaws.com/prod/api/note-message
    '                        Dim data As String = "{""machineId"": " & machine_id & ", ""timestamp"": """ & timestamp & """, ""readings"": [{""reading"": " & reading & ", ""cassette"": " & cassette & "}]}"
    '                        LogHelper.Error("Cloud_Net:SalesID: " & ViewState("salesID"))
    '                        LogHelper.Error("Cloud_Net: Url: " & url)
    '                        LogHelper.Error("Cloud_Net: Header: mangle-cloud-api-key: " & value)
    '                        LogHelper.Error("Cloud_Net: Body: " & data)
    '                        LogHelper.Error("Cloud_Net: Start request")

    '                        Dim myReq As WebRequest = WebRequest.Create(url)
    '                        myReq.Method = "POST"
    '                        myReq.ContentType = "application/json; charset=UTF-8"
    '                        myReq.Headers.Add("mangle-cloud-api-key", value) 'dt.Rows(2)("value").ToString) '"HblxeyRqOlpWek3KoRK31XLBv57YR3fEyTWc9Igd")
    '                        LogHelper.Error("Cloud_Net:btnSave_Click - Location ID: " & oclsLocation.location_id & ", Machine ID: " & ViewState("machine_id") & ", T-Sales ID: " & ViewState("salesID") & ", cassette: " & cassette & ", Quantity: " & reading)
    '                        Dim enc As UTF8Encoding = New UTF8Encoding()
    '                        Dim dataBytes As Byte() = enc.GetBytes(data)

    '                        myReq.ContentLength = dataBytes.Length

    '                        Using ds As Stream = myReq.GetRequestStream()
    '                            ds.Write(dataBytes, 0, dataBytes.Length)
    '                        End Using

    '                        Dim response As WebResponse = myReq.GetResponse()
    '                        LogHelper.Error("Cloud_Net:End request")

    '                        Dim statusCode As Integer = CType(response, HttpWebResponse).StatusCode
    '                        LogHelper.Error("Cloud_Net: Response Code: " & statusCode)
    '                        If statusCode = 200 Then
    '                            oclsLocation.machine_id = machine_id
    '                            oclsLocation.cloudKey = "mangle-cloud-api-key"
    '                            oclsLocation.location_id = location_id
    '                            oclsLocation.gtway_StoreName = store
    '                            oclsLocation.cmp_id = Val(Session("cmp_id"))
    '                            oclsLocation.created_date = timestamp
    '                            oclsLocation.cassette = cassette
    '                            oclsLocation.reading = reading
    '                            oclsLocation.cloudValue = value
    '                            oclsLocation.cloudurl = url
    '                            oclsLocation.Historycloudnet()
    '                        End If



    '                        Using dataStream As Stream = response.GetResponseStream()
    '                            Dim reader As New StreamReader(dataStream)
    '                            Dim responseFromServer As String = reader.ReadToEnd()
    '                            LogHelper.Error("Cloud_Net:Response Code: " & responseFromServer)
    '                            Console.WriteLine(responseFromServer)
    '                        End Using

    '                        response.Close()
    '                        oclsLocation.machine_id = ViewState("machine_id")
    '                        oclsLocation.salesID = ViewState("salesID")
    '                       ' Dim dt3 As DataTable = oclsLocation.Select_updateCloud()

    '                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Record added successfully');", True)

    '                    Else
    '                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('MachineID Not Found');", True)
    '                    End If
    '                Next
    '            Else
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Data Not Found');", True)
    '            End If
    '        Next
    '        oclsLocation.created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
    '        ' oclsLocation.modify_date = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)
    '        oclsLocation.location_id = IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")
    '        created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
    '        '   modified_date = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)


    '    Catch ex As Exception
    '        LogHelper.Error("Cloud_Net:btnSave_Click" + ex.Message)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)

    '    End Try
    'End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then



                '   txtForDate.SelectedDate = System.DateTime.Now
                ' txtTo.SelectedDate = System.DateTime.Now

                'oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
                ' created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
                ' modified_date = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)

                'oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
                'oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            End If
        Catch ex As Exception
            LogHelper.Error("BarStock_Exchange:Page_Load" + ex.Message)
        End Try
    End Sub




    Protected Sub ButtonCategories_Click(sender As Object, e As EventArgs) Handles ButtonCategories.Click
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        LogHelper.Error("BarStock_Exchange:ButtonCategories_Click: Started processing.")
        Dim dt As DataTable = oclsPromotion.Select_urlforsales()

        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()
            ApiUrl = ApiUrl.Replace("service.php", "import.php")
            Dim Itemsqty As String
            Dim ItemsNr As String
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12



            Dim responseText As String

            Dim dt3 As DataTable = oclsBarcodeExchange.Select_Category()


            If dt3.Rows.Count > 0 Then
                Dim categoriesArray As New List(Of Object)
                For Each row As DataRow In dt3.Rows
                    categoriesArray.Add(New With {
                    .categoryNr = row("category_id").ToString(),
                    .categoryNrAlpha = "",
                    .categoryName = row("catergory_name").ToString()
                })
                Next

                Dim posCategoriesJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(categoriesArray)

                Dim token As String =  value
                Dim url As String = ApiUrl & "?Token=" & token & "&Mode=1&ClearCategoryDB=1&POSCategoriesArray=" & posCategoriesJson

                LogHelper.Error("BarStock_Exchange:ButtonCategories_Click: URL: " & url)
                LogHelper.Error("BarStock_Exchange:ButtonCategories_Click: Start request")
                responseText = SendRequest(url, responseCode)
                LogHelper.Error("BarStock_ExchangeButtonCategories_Click: End request")
                responseText = responseText.Trim()
                LogHelper.Error("BarStock_Exchange:ButtonCategories_Click: ResponseCode: " & responseCode)
                LogHelper.Error("BarStock_Exchange:ButtonCategories_Click: ResponseText: " & responseText)
                If responseCode = 200 Then
                    If String.IsNullOrEmpty(responseText) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Categories sent successfully. Response Code: " & responseCode & "');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Categories sent successfully. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                    End If
                Else
                    If String.IsNullOrEmpty(responseText) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed to send category. Response Code: " & responseCode & "');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed to send category. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                    End If
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "noCategories", "alert('No categories found to send.');", True)

            End If
        Next
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('" & responseText.Replace(vbLf, "\n") & "');", True)
    End Sub




    Protected Sub sales_Click(sender As Object, e As EventArgs) Handles sales.Click

        Dim dt As DataTable = oclsPromotion.Select_urlforsales()

        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()
            Dim Itemsqty As String
            Dim ItemsNr As String
            Dim prod_id As String
            Dim tsales_id As String
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


            Dim dt1 As DataTable = oclsBarcodeExchange.Select_Sendsales()
            If dt1.Rows.Count > 0 Then
                Dim row2 As DataRow = dt1.Rows(0)
                ItemsNr = row2("other_id").ToString()
                prod_id = row2("Product_id").ToString()
                Itemsqty = row2("quntity").ToString()
                tsales_id = row2("tsales_id").ToString()


                Dim token As String = value
                'Dim itemNumbers As String = "1|2|3|4"
                'Dim itemQuantities As String = "0|2|3|1"

                Dim itemNumbers As String = ItemsNr
                Dim pr_id As String = prod_id
                Dim itemQuantities As String = Itemsqty



                Dim url As String = ApiUrl & "?Token=" & token & "&Mode=4&PriceFactor=100&ItemNr=" & pr_id & "&ItemQty=" & itemQuantities & "&ProdId=" & pr_id


                LogHelper.Error("BarStock_Exchange:sales_Click: Url: " & url)
                LogHelper.Error("BarStock_Exchange:sales_Click: Start request")
                Dim responseText As String = SendRequest(url).Trim()
                LogHelper.Error("BarStock_Exchange:sales_Click: End request")

                'If responseCode = 200 Then
                '    oclsBarcodeExchange.tsales_id = tsales_id
                '    oclsBarcodeExchange.Product_id = prod_id
                '    oclsBarcodeExchange.other_id = ItemsNr
                '    Dim dt2 As DataTable = oclsBarcodeExchange.update_barstock()
                'End If


                LogHelper.Error("BarStock_Exchange:sales_Click: ResponseText: " & responseText)
                    LogHelper.Error("BarStock_Exchange:sales_Click: ResponseCode: " & responseCode)
                'responseText = responseText.Trim()

                If responseCode = 200 Then
                    If responseText.Contains("InvalidItemNr") Then
                        LogHelper.Error("BarStock_Exchange:sales_Click: InvalidItemNr received. No insertion into Barstock_push.")
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "invalidItemNr", "alert('InvalidItemNr received. Please send valideItem number.');", True)
                    Else
                        ' Insert into Barstock_push table
                        oclsBarcodeExchange.tsales_id = tsales_id
                        oclsBarcodeExchange.Product_id = prod_id
                        oclsBarcodeExchange.other_id = ItemsNr
                        Dim dt2 As DataTable = oclsBarcodeExchange.update_barstock()

                        ' Handle success or failure based on your application logic
                        If String.IsNullOrEmpty(responseText) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Sales sent successfully. Response Code: " & responseCode & "');", True)
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Sales sent successfully. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                        End If
                    End If
                Else
                    If String.IsNullOrEmpty(responseText) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed to send Sales. Response Code: " & responseCode & "');", True)
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed to send Sales. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                        End If
                    End If
                End If
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('" & responseText & "');", True)
        Next

    End Sub




    'Protected Sub sales_Click(sender As Object, e As EventArgs) Handles sales.Click

    '    Dim dt As DataTable = oclsPromotion.Select_urlforsales()

    '    For Each row1 As DataRow In dt.Rows
    '        Dim store As String = row1("Storename").ToString()
    '        Dim value As String = row1("value").ToString()
    '        Dim ApiUrl As String = row1("Url").ToString()

    '        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '        Dim token As String = value
    '        Dim itemNumbers As String = "1|2|3|4"
    '        Dim itemQuantities As String = "0|2|3|1"

    '        Dim url As String = ApiUrl & "?Mode=1&Token=" & token & "&ItemNrArray=" & itemNumbers & "&ItemQtyArray=" & itemQuantities

    '        Dim responseText As String = SendRequest(url)
    '        responseText = responseText.Trim()

    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('" & responseText & "');", True)
    '    Next

    'End Sub

    'Protected Sub price_Click(sender As Object, e As EventArgs) Handles price.Click
    '    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    '    ' oclsPromotion.location_id = IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")
    '    Dim dt As DataTable = oclsPromotion.Select_urlforsales()
    '    For Each row1 As DataRow In dt.Rows
    '        Dim store As String = row1("Storename").ToString()
    '        Dim value As String = row1("value").ToString()
    '        Dim ApiUrl As String = row1("Url").ToString()

    '        Dim token As String = value
    '        Dim format As Integer = 2
    '        Dim priceFactor As Integer = 1

    '        Dim url As String = ApiUrl & "?Mode=2&Token=" & token & "&Format=" & format & "&PriceFactor=" & priceFactor

    '        Dim responseText As String = SendRequest(url)
    '        responseText = responseText.Trim()

    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('" & responseText & "');", True)
    '    Next
    'End Sub



    Protected Sub price_Click(sender As Object, e As EventArgs) Handles price.Click
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        ' oclsPromotion.location_id = IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")
        Dim dt As DataTable = oclsPromotion.Select_urlforsales()
        Dim ItemsNr As String
        Dim prod_id As String

        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()
            Dim dt1 As DataTable = oclsBarcodeExchange.Select_GetPrice()
            For Each row2 As DataRow In dt1.Rows
                ' Dim row2 As DataRow = dt1.Rows(0)
                'ItemsNr = row2("Product_id").ToString()
                prod_id = row2("Product_id").ToString()
                ItemsNr = row2("other_id").ToString()
                   Dim token As String = value
                Dim format As Integer = 2
                Dim priceFactor As Integer = 100
                Dim ItemNrs As String = ItemsNr
                Dim url As String = ApiUrl & "?Token=" & token & "&Mode=3" & "&PriceFactor=" & priceFactor & "&ItemNr=" & prod_id & "&prod_id=" & prod_id
                'Dim otherid As String = row1("Storename").ToString()
                'Dim product_id As String = row1("value").ToString()
                'Dim price As String = row1("Url").ToString()
                'Dim dt2 As DataTable = oclsBarcodeExchange.update_GetPrice()
                LogHelper.Error("BarStock_Exchange:price_Click: Start request")
                LogHelper.Error("BarStock_Exchange:price_Click: Url: " & url)
                Dim responseText As String = SendRequest(url)
                responseText = responseText.Trim()

                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Categories sent successfully. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)

                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "alert( Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)

                Dim parts As String() = responseText.Split("|"c)
                Dim other_id As String = String.Empty
                Dim price As Decimal = 0
                'Dim other_id As String = jsonResponse("ItemN").ToString()
                ' Dim price As Decimal = Decimal.Parse(jsonResponse("price").ToString())
                For i As Integer = 0 To parts.Length - 1 Step 2
                    If parts(i) = "ItemNr" Then
                        other_id = parts(i + 1)
                    ElseIf parts(i) = "ItemPrice" Then
                        price = Decimal.Parse(parts(i + 1))
                    End If
                Next
                If String.IsNullOrEmpty(other_id) Or price <= 0 Then
                    responseText = "Invalid response received."
                Else

                    oclsBarcodeExchange.Price = price
                    oclsBarcodeExchange.other_id = other_id
                    Dim dt2 As DataTable = oclsBarcodeExchange.update_GetPrice()

                    LogHelper.Error("BarStock_Exchange:price_Click: ResponseText:  " & responseText & "Price update Successfully")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Items updated successfully. Response Code: " & responseCode & "');", True)
                End If

                LogHelper.Error("BarStock_Exchange:price_Click:End request")
                LogHelper.Error("BarStock_Exchange:price_Click: ResponseText: " & responseText)
                LogHelper.Error("BarStock_Exchange:price_Click: ResponseCode: " & responseCode)

            Next

        Next
    End Sub
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



    'Protected Sub Import_Click(sender As Object, e As EventArgs)
    '    Dim dt As DataTable = oclsPromotion.Select_urlforsales()
    '    For Each row1 As DataRow In dt.Rows
    '        Dim itemsFile As String = "items.txt"
    '        Dim categoriesFile As String = "categories.txt"
    '        Dim apiUrl As String = "https://www.barstock.de/cloud24/managerprod/epos/barstockapi/importdata.php"
    '        Dim token As String = ""


    '        Dim itemsData As String = File.ReadAllText(itemsFile)
    '        Dim categoriesData As String = File.ReadAllText(categoriesFile)

    '        Dim postData As String = String.Format("token={0}&items={1}&categories={2}", token, itemsData, categoriesData)
    '        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)


    '        Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
    '        request.Method = "POST"
    '        request.ContentType = "application/x-www-form-urlencoded"
    '        request.ContentLength = byteArray.Length


    '        Dim dataStream As Stream = request.GetRequestStream()
    '        dataStream.Write(byteArray, 0, byteArray.Length)
    '        dataStream.Close()


    '        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
    '        Dim responseString As String

    '        Using reader As New StreamReader(response.GetResponseStream())
    '            responseString = reader.ReadToEnd()
    '        End Using

    '        Console.WriteLine(responseString)
    '    Next
    'End Sub

    Protected Sub ButtonItems_Click(sender As Object, e As EventArgs)
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim dt As DataTable = oclsPromotion.Select_urlforsales()
        Dim ItemsNr As String

        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()
            Dim responseText As String
            ApiUrl = ApiUrl.Replace("service.php", "import.php")
            Dim dt4 As DataTable = oclsBarcodeExchange.Select_Items()


            If dt4.Rows.Count > 0 Then
                Dim itemsArray As New List(Of Object)
                For Each row As DataRow In dt4.Rows
                    itemsArray.Add(New With {
                    .itemNr = row("Item_Number").ToString(),
                    .itemNrAlpha = row("Item_Number").ToString(),
                    .itemName = row("Item_Name").ToString(),
                    .itemPrice = Convert.ToDecimal(row("Item_Price")),
                    .itemCategory = row("Item_Category").ToString()
                })
                Next

                ' Convert items array to JSON format
                Dim posItemsJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(itemsArray)


                Dim token As String = value
                Dim url As String = ApiUrl & "?Token=" & token & "&Mode=2&ClearItemsDB=1&Db_Nr=1&POSItemsArray=" & posItemsJson


                LogHelper.Error("BarStock_Exchange:ButtonItems_Click: URL: " & url)
                LogHelper.Error("BarStock_Exchange:ButtonItems_Click: Start request")
                responseText = SendRequest(url, responseCode)
                LogHelper.Error("BarStock_Exchange:ButtonItems_Click: End request")
                LogHelper.Error("BarStock_Exchange:ButtonItems_Click: ResponseCode: " & responseCode)
                LogHelper.Error("BarStock_Exchange:ButtonItems_Click: ResponseText: " & responseText)

                responseText = responseText.Trim()
                If responseCode = 200 Then
                    If String.IsNullOrEmpty(responseText) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Items Sent Successfully. Response Code: " & responseCode & "');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Items Sent Successfully. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                    End If
                Else
                    If String.IsNullOrEmpty(responseText) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed To Send Item. Response Code: " & responseCode & "');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordFailed", "alert('Failed To Send Item. Response Code: " & responseCode & "\nResponse Text: " & responseText.Replace(vbLf, "\n") & "');", True)
                    End If
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "noCategories", "alert('No Items found to send.');", True)

            End If
        Next


    End Sub
End Class

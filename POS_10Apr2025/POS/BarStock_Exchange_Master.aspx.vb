Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Security.Authentication
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class BarStock_Exchange_Master
    Inherits BaseClass
    Dim oclsLocation As New Controller_clsLocation()
    Dim oclsPromotion As New Controller_clsPromotion()
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Dim oClsDataccess As New ClsDataccess
    Dim created_date As String
    '  Dim modified_date As String

    Private Shared ReadOnly _httpClient As New HttpClient()
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
    '                        ' Dim dt3 As DataTable = oclsLocation.Select_updateCloud()

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



                ' txtForDate.SelectedDate = System.DateTime.Now
                ' txtTo.SelectedDate = System.DateTime.Now

                ' oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
                ' created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
                ' modified_date = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)

                'oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
                'oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
            End If
        Catch ex As Exception
            LogHelper.Error("Cloud_Net:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub sales_Click(sender As Object, e As EventArgs) Handles sales.Click

        Dim dt As DataTable = oclsPromotion.Select_urlforsales()

        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim token As String = value
            Dim itemNumbers As String = "1|2|3|4"
            Dim itemQuantities As String = "0|2|3|1"

            Dim url As String = ApiUrl & "?Mode=1&Token=" & token & "&ItemNrArray=" & itemNumbers & "&ItemQtyArray=" & itemQuantities

            Dim responseText As String = SendRequest(url)
            responseText = responseText.Trim()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('" & responseText & "');", True)
        Next

    End Sub

    Protected Sub price_Click(sender As Object, e As EventArgs) Handles price.Click
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim dt As DataTable = oclsPromotion.Select_urlforsales()
        For Each row1 As DataRow In dt.Rows
            Dim store As String = row1("Storename").ToString()
            Dim value As String = row1("value").ToString()
            Dim ApiUrl As String = row1("Url").ToString()

            Dim token As String = value
            Dim format As Integer = 2
            Dim priceFactor As Integer = 1

            Dim url As String = ApiUrl & "?Mode=2&Token=" & token & "&Format=" & format & "&PriceFactor=" & priceFactor

            Dim responseText As String = SendRequest(url)
            responseText = responseText.Trim()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('" & responseText & "');", True)
        Next
    End Sub

    Private Function SendRequest(url As String, Optional content As String = Nothing) As String
        Dim responseContent As String = String.Empty

        Try
            Using client As New System.Net.WebClient()
                responseContent = client.DownloadString(url)
            End Using
        Catch ex As Exception
            responseContent = ex.Message
        End Try

        Return responseContent
    End Function



    Protected Sub Import_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = oclsPromotion.Select_urlforsales()
        For Each row1 As DataRow In dt.Rows
            Dim itemsFile As String = "items.txt"
            Dim categoriesFile As String = "categories.txt"
            Dim apiUrl As String = "https://www.barstock.de/cloud24/managerprod/epos/barstockapi/importdata.php"
            Dim token As String = ""


            Dim itemsData As String = File.ReadAllText(itemsFile)
            Dim categoriesData As String = File.ReadAllText(categoriesFile)

            Dim postData As String = String.Format("token={0}&items={1}&categories={2}", token, itemsData, categoriesData)
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)


            Dim request As HttpWebRequest = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length


            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()


            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim responseString As String

            Using reader As New StreamReader(response.GetResponseStream())
                responseString = reader.ReadToEnd()
            End Using

            Console.WriteLine(responseString)
        Next
    End Sub
End Class

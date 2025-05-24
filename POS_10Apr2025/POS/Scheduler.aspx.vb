Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Security.Authentication
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Scheduler
    Inherits BaseClass
    Dim oclsLocation As New Controller_clsLocation()
    Dim oclsBind As New clsBinding
    Dim oclsRole As Controller_clsRole
    Dim oClsDataccess As New ClsDataccess
    Dim created_date As String
    Dim str As String = ""
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            CheckStore(Request.QueryString("store").ToString())
            oclsLocation.location_id = Val(Request.QueryString("lid").ToString())
            oclsLocation.created_date = System.DateTime.Now

            Dim dt As DataTable = oclsLocation.Select_urlforCloud()

            For Each row1 As DataRow In dt.Rows
                Dim store As String = row1("Storename").ToString()
                Dim value As String = row1("value").ToString()
                Dim ApiUrl As String = row1("Url").ToString()
                Dim machineIdFound As Boolean = False
                Dim reading As Integer
                Dim cassette As Integer


                Dim dt2 As DataTable = oclsLocation.Select_tsalesCloud()


                If dt2.Rows.Count > 0 Then
                    Dim readingsList As New List(Of String)()

                    Dim cassetteReadings As New Dictionary(Of Integer, Integer)()
                    'For Each row2 As DataRow In dt2.Rows
                    '    ViewState("machine_id") = row2("machine_id").ToString()
                    '    ViewState("quntity") = row2("quntity").ToString()
                    '    ViewState("salesID") = row2("salesID").ToString()
                    '    If IsDBNull(row2("cassette")) Then
                    '        ViewState("cassette") = "0"
                    '    Else
                    '        ViewState("cassette") = row2("cassette").ToString()
                    '    End If


                    '    If Not String.IsNullOrEmpty(ViewState("machine_id")) Then
                    '        cassette = Convert.ToInt32(ViewState("cassette"))
                    '        reading = Convert.ToInt32(ViewState("quntity"))
                    '        Dim cassetteReading As String = cassette & "," & reading
                    '        readingsList.Add(cassetteReading)
                    '    End If

                    'Next
                    For Each row2 As DataRow In dt2.Rows
                        cassette = If(IsDBNull(row2("cassette")), 0, Convert.ToInt32(row2("cassette")))
                        reading = If(IsDBNull(row2("quntity")), 0, Convert.ToInt32(row2("quntity")))
                        cassetteReadings(cassette) = reading
                    Next

                    For i As Integer = 1 To 4
                        ' Get the reading for the current cassette, default to 0 if it doesn't exist
                        Dim cassetteReading As Integer = If(cassetteReadings.ContainsKey(i), cassetteReadings(i), 0)
                        ' Add the cassette reading to the list in the format: {"cassette": 1, "reading": 100}
                        readingsList.Add("{""cassette"": " & i & ", ""reading"": " & cassetteReading & "}")
                    Next

                    Dim readingsJson As String = "[" & String.Join(",", readingsList) & "]"
                    If dt2.Rows(0)("machine_id") IsNot DBNull.Value Then
                        ViewState("machine_id") = Convert.ToInt32(dt2.Rows(0)("machine_id"))
                    End If

                    Dim concatenatedData As String = String.Join(";", readingsList)

                    ' Dim readingsJson As String = "[" & String.Join(",", readingsList.Select(Function(x) "{""cassette"": " & x.Split(","c)(0) & ", ""reading"": " & x.Split(","c)(1) & "}")) & "]"




                    Dim location_id As Integer = row1("Location_id").ToString()
                    Dim machineId As Integer = Convert.ToInt32(ViewState("machine_id"))
                    Dim timestamp As String = oclsLocation.created_date.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    Dim data As String = "{""machineId"": " & machineId & ", ""timestamp"": """ & timestamp & """, ""readings"": " & readingsJson & " }"


                    Dim url As String = ApiUrl   'https://0gppl7d7y4.execute-api.eu-west-1.amazonaws.com/prod/api/note-message
                    ' Dim data As String = "{""machineId"": " & machineId & ", ""timestamp"": """ & timestamp & """, ""readings"": [{""reading"": " & reading & ", ""cassette"": " & cassette & "}]}"
                    LogHelper.Error("Cloud_Net:SalesID: " & ViewState("salesID"))
                    LogHelper.Error("Cloud_Net: Url: " & url)
                    LogHelper.Error("Cloud_Net: Header: mangle-cloud-api-key: " & value)
                    LogHelper.Error("Cloud_Net: Body: " & data)
                    LogHelper.Error("Cloud_Net: Start request")

                    Dim myReq As WebRequest = WebRequest.Create(url)
                    myReq.Method = "POST"
                    myReq.ContentType = "application/json; charset=UTF-8"
                    myReq.Headers.Add("mangle-cloud-api-key", value) 'dt.Rows(2)("value").ToString) '"HblxeyRqOlpWek3KoRK31XLBv57YR3fEyTWc9Igd")
                    LogHelper.Error("Cloud_Net:btnSave_Click - Location ID: " & oclsLocation.location_id & ", Machine ID: " & ViewState("machine_id") & ", T-Sales ID: " & ViewState("salesID") & ", Reading: " & readingsJson)
                    Dim enc As UTF8Encoding = New UTF8Encoding()
                    Dim dataBytes As Byte() = enc.GetBytes(data)

                    myReq.ContentLength = dataBytes.Length

                    Using ds As Stream = myReq.GetRequestStream()
                        ds.Write(dataBytes, 0, dataBytes.Length)
                    End Using

                    Dim response As WebResponse = myReq.GetResponse()
                    LogHelper.Error("Cloud_Net:End request")

                    Dim statusCode As Integer = CType(response, HttpWebResponse).StatusCode
                    LogHelper.Error("Cloud_Net: Response Code: " & statusCode)
                    If statusCode = 200 Then
                        For Each row2 As DataRow In dt2.Rows
                            Dim machine_id As Integer = Convert.ToInt32(row2("machine_id"))
                            cassette = If(IsDBNull(row2("cassette")), 0, Convert.ToInt32(row2("cassette")))
                            reading = If(IsDBNull(row2("quntity")), 0, Convert.ToInt32(row2("quntity")))
                            Dim salesID As String = row2("salesID").ToString()


                            oclsLocation.machine_id = machine_id
                            oclsLocation.cloudKey = "mangle-cloud-api-key"
                            oclsLocation.location_id = location_id
                            oclsLocation.gtway_StoreName = store
                            oclsLocation.cmp_id = Val(Session("cmp_id"))
                            oclsLocation.created_date = timestamp
                            oclsLocation.cassette = cassette
                            oclsLocation.reading = reading
                            oclsLocation.cloudValue = value
                            oclsLocation.cloudurl = url
                            oclsLocation.salesID = salesID


                            oclsLocation.Historycloudnet()
                        Next
                    End If



                    Using dataStream As Stream = response.GetResponseStream()
                        Dim reader As New StreamReader(dataStream)
                        Dim responseFromServer As String = reader.ReadToEnd()
                        LogHelper.Error("Cloud_Net:Response Code: " & responseFromServer)
                        Console.WriteLine(responseFromServer)
                    End Using

                    response.Close()
                    oclsLocation.machine_id = ViewState("machine_id")
                    oclsLocation.salesID = ViewState("salesID")
                    Dim dt3 As DataTable = oclsLocation.Select_updateCloud()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Record added successfully');", True)

                Else
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('MachineID Not Found');", True)
                End If

                'Else
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "recordAdded", "alert('Data Not Found');", True)
                'End If
            Next




        Catch ex As Exception
            LogHelper.Error("Scheduler:Page_Load" + ex.Message)
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
    '                        '  Dim alertCode As Integer = 777

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
    '                        Dim dt3 As DataTable = oclsLocation.Select_updateCloud()

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

    '        oclsLocation.location_id = IIf(radLocation.SelectedIndex > 0, radLocation.SelectedValue, "0")
    '        created_date = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)



    '    Catch ex As Exception
    '        LogHelper.Error("Cloud_Net:btnSave_Click" + ex.Message)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)

    '    End Try
    'End Sub

End Class


Imports System.IO
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Partial Class cs_cartnotification

    Inherits BaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            LogHelper.Error("#started1")

            Dim strJSON = [String].Empty

            Context.Request.InputStream.Position = 0
            Using inputStream = New StreamReader(Context.Request.InputStream)
                strJSON = inputStream.ReadToEnd()
            End Using

            Dim ser_cust As JObject = JObject.Parse(strJSON)
            Dim data1_cust As List(Of JToken) = ser_cust.Children().ToList
            Dim Url1_cust As String = ""
            Dim cust_ref As String = ""

            For Each item As JProperty In data1_cust
                item.CreateReader()

                For Each item_ref As JProperty In item.Value
                    item_ref.CreateReader()
                    LogHelper.Error("#" + item_ref.Name.ToString())
                    LogHelper.Error("." + item_ref.Value.ToString())

                Next


            Next
            ' go and process the serJsonDetails object

            'Dim keys As String() = Request.Form.AllKeys
            'LogHelper.Error("#keys.Length1" + keys.Length.ToString())
            'For i As Integer = 0 To keys.Length - 1
            '    LogHelper.Error(keys(i) & ": " & Request.Form(keys(i)) & "<br>")
            'Next

            ' If Not (Request.Form.Keys("paymentJobReference") = Nothing) Then

            '    LogHelper.Error(Request.Form.Keys("paymentReference").ToString())
            'Else
            '    Response.Write("OK")
            'End If

            'For Each Key As String In HttpContext.Current.Request.Form.Keys
            '        Dim value As String = HttpContext.Current.Request.Form(Key)

            '        LogHelper.Error("Field:" + Key + "Value:" + value)
            '    Next


            LogHelper.Error("#end loop1")
        Catch ex As Exception
            LogHelper.Error("An error occurred during data processing1:" + ex.Message)
        Finally
            LogHelper.Error("#End1")
        End Try
    End Sub



End Class

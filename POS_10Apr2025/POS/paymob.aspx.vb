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


Partial Class paymob
    Inherits BaseClass

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ServicePointManager.Expect100Continue = True
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                LogHelper.Error("paymob:page_load: start")

                Dim reader As StreamReader = New StreamReader(Request.InputStream)
                Dim content As String = reader.ReadToEnd()

                LogHelper.Error("paymob:page_load:" + content)


                Dim posturl As String = "https://core.demo.paymob.uk/json/GetToken"
                Dim postdata As String = "{ ""@MsgNum"":""2a487dbd-ebbe-4304-b1eb-8b9c6466f0ba"", ""ServerTime"":""" + TimeZoneInfo.ConvertTimeToUtc(System.DateTime.Now) + """, ""Response"" : { ""Code"" : ""00"", ""Description"" : ""OK"" }}"
                Dim post_myReq As WebRequest = WebRequest.Create(posturl)
                post_myReq.Credentials = CredentialCache.DefaultCredentials
                post_myReq.Method = "POST"
                post_myReq.ContentLength = postdata.Length
                post_myReq.ContentType = "application/json"
                Dim post_enc As UTF8Encoding = New UTF8Encoding()
                'post_myReq.Headers.Add("Authorization", "Basic " + Encode_data(ClientID + ":" + clientsecret))


                Using ds1 As Stream = post_myReq.GetRequestStream()
                    ds1.Write(post_enc.GetBytes(postdata), 0, postdata.Length)
                End Using

                LogHelper.Error("paymob:page_load:check_req")

                Dim wr2 As WebResponse = post_myReq.GetResponse()
                Dim receiveStream2 As Stream = wr2.GetResponseStream()
                Dim reader2 As StreamReader = New StreamReader(receiveStream2, Encoding.UTF8)
                Dim content2 As String = reader2.ReadToEnd()

                LogHelper.Error("paymob:page_load:check" + content2)

                LogHelper.Error("paymob:page_load:end")

            End If
        Catch ex As Exception
            LogHelper.Error("paymob:Page_Load:" + ex.Message)
        End Try
    End Sub

End Class

Imports System.IO
Imports Telerik.Web.UI
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.Services.Protocols
Imports System.Runtime.Serialization.Json
Imports System.Runtime.Serialization
Imports System.Web.Script.Serialization
Imports AjaxControlToolkit
Imports System.Data.SqlClient

Partial Class POS_WS_Upload
    Inherits BaseClass
    Dim oclsBind As New clsBinding

    Public ReadOnly Property Cust_ID() As String
        Get
            If Not String.IsNullOrEmpty(Request.Form("Cust_ID")) Then
                Return Request.Form("Cust_ID").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property Store() As String
        Get
            If Not String.IsNullOrEmpty(Request.Form("Store")) Then
                Return Request.Form("Store").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property Image() As String
        Get
            If Not String.IsNullOrEmpty(Request.Form("Image")) Then
                Return Request.Form("Image").ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            LogHelper.Error("imagepost:Page_Load:START")

            CallMethod()

            LogHelper.Error("imagepost:Page_Load:END")
        Catch ex As Exception
            LogHelper.Error("imagepost:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub CallMethod()
        Try

            Dim str As String
            Dim objwebservices As New ServiceReference1.POS_WebServiceSoapClient

            If Request.QueryString("Function").ToString() = "Customer_Profile_Image" Then
                If Cust_ID <> Nothing And Store <> Nothing And Image <> Nothing Then

                    LogHelper.Error("imagepost:CallMethod: Image :" + Image)
                    Dim strImg As String = Image.Replace("%0A", "").Replace("%3D", "=").Replace("%2B", "+")
                    Dim bytes As Byte() = Convert.FromBase64String(strImg)

                    str = objwebservices.Customer_Profile_Image(Cust_ID, Store, bytes)

                    Response.Write(str.ToString())
                End If
            End If



            ''Get an entry for file upload
            'Dim fileName As String = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()

            'Dim names As [String]() = file.FileName.Split("."c)
            'Dim src As String = (fileName & Convert.ToString(".")) + names(names.Length - 1)

            'file.SaveAs(Server.MapPath("~/Files/" + ModuleName() + "/") & src)
            'Response.Write(src.ToString())





            'LogHelper.Error("imagepost:Page_Load:CallMethod:START")
            'If ModuleName() <> Nothing And Image() <> Nothing Then

            '    Dim str As String
            '    Dim Decode As Byte() = System.Text.Encoding.UTF8.GetBytes(Image()) ' HttpUtility.UrlDecode(Image()) 'System.Convert.FromBase64String(HttpUtility.UrlDecode(Image()))
            '    'Dim bytes As Byte() = Decode
            '    Dim filename = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()
            '    System.IO.File.WriteAllBytes(Server.MapPath("~/Files/" + ModuleName() + "/") + filename.ToString() + ".jpg", Decode)
            '    str = filename.ToString() + ".Png"
            '    If str.ToString() <> "[]" And str.ToString() <> "No User Found." And str.ToString() <> "Invalid Webservice User Access...!" Then
            '        Response.Write(str.ToString())
            '    Else
            '        Response.Write("Invalid Image ...!")
            '        LogHelper.Error("imagepost:Page_Load:CallMethod:ELSE (Invalid Image ...!)")
            '    End If
            'End If
            'LogHelper.Error("imagepost:Page_Load:CallMethod:END")
        Catch ex As Exception
            LogHelper.Error("imagepost:Page_Load:CallMethod():" + ex.Message)
        End Try
    End Sub
    Public Shared Function DecodeFrom64(ByVal m_enc As String) As String
        Dim encodedDataAsBytes As Byte() = System.Convert.FromBase64String(m_enc)
        Dim returnValue As String = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes)
        Return returnValue
    End Function
    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "") As Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(Session("store_ConnectionString").ToString())
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
            LogHelper.Error("POS_WS_Upload:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing

        End Try
        Return sdr
    End Function



    'Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    '    'Dim encoded As Byte() = System.Convert.FromBase64String(Image())
    '    If (File.Exists(Server.MapPath("~/Files/Document"))) Then
    '    End If

    '    Dim Decode As Byte() = System.Convert.FromBase64String(HttpUtility.UrlDecode(Image()))
    '    If ModuleName() <> Nothing Then
    '        Dim bytes As Byte() = Decode
    '        Dim filename = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()
    '        System.IO.File.WriteAllBytes(Server.MapPath("~/Files/Document/") + filename.ToString() + ".jpg", Decode)
    '        lbl1.InnerText = filename + ".jpg"
    '    ElseIf ModuleName() = "Employee" Then
    '        Dim bytes As Byte() = Decode
    '        Dim filename = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()
    '        System.IO.File.WriteAllBytes(Server.MapPath("~/Files/Document/") + filename.ToString() + ".jpg", Decode)
    '        lbl1.InnerText = filename + ".jpg"
    '    ElseIf ModuleName() = "policy" Then
    '        Dim bytes As Byte() = Decode
    '        Dim filename = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()
    '        System.IO.File.WriteAllBytes(Server.MapPath("~/Files/Document/") + filename.ToString() + ".jpg", Decode)
    '        lbl1.InnerText = filename + ".jpg"
    '    End If

    '    'Dim encoded As Byte() = System.Convert.FromBase64String(Image())
    '    'If ModuleName() = "Document" Then
    '    '    Dim bytes As Byte() = encoded
    '    '    System.IO.File.WriteAllBytes("../Files/Document/" + System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString() + ".jpg", bytes)
    '    'ElseIf ModuleName() = "Employee" Then
    '    '    Dim bytes As Byte() = encoded
    '    '    System.IO.File.WriteAllBytes("../Files/Employee/" + System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString() + ".jpg", bytes)
    '    'ElseIf ModuleName() = "policy" Then
    '    '    Dim bytes As Byte() = encoded
    '    '    System.IO.File.WriteAllBytes("../Files/policy/" + System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString() + ".jpg", bytes)
    '    'End If

    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
    '    Dim img As FileUpload = CType(FileUpload1, FileUpload)
    '    Dim imgByte As Byte() = Nothing
    '    If img.HasFile AndAlso Not img.PostedFile Is Nothing Then
    '        Dim File As HttpPostedFile = FileUpload1.PostedFile
    '        imgByte = New Byte(File.ContentLength - 1) {}
    '        File.InputStream.Read(imgByte, 0, File.ContentLength)

    '        Dim encoded = HttpUtility.UrlEncode(Convert.ToBase64String(imgByte))
    '        Dim Decode As Byte() = System.Convert.FromBase64String(HttpUtility.UrlDecode(encoded))
    '        If imgByte Is Decode Then
    '            MsgBox("hi")
    '        End If
    '        If ModuleName() = "Document" Then
    '            Dim bytes As Byte() = imgByte
    '            Dim filename = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()
    '            System.IO.File.WriteAllBytes(Server.MapPath("~/Files/Document/") + filename.ToString() + ".jpg", Decode)
    '            MsgBox(filename)
    '        ElseIf ModuleName() = "Employee" Then
    '            Dim filename = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()
    '            System.IO.File.WriteAllBytes(Server.MapPath("~/Files/Document/") + filename.ToString() + ".jpg", Decode)
    '            MsgBox(filename)
    '        ElseIf ModuleName() = "policy" Then
    '            Dim filename = System.DateTime.Now.ToString.Replace("/", "").Replace(".", "").Replace(":", "").Replace("\", "").Replace(" ", "").ToString()
    '            System.IO.File.WriteAllBytes(Server.MapPath("~/Files/Document/") + filename.ToString() + ".jpg", Decode)
    '            MsgBox(filename)
    '        End If

    '    End If
    'End Sub
End Class

Imports Microsoft.VisualBasic
Imports System.Data
Imports Telerik.Web.UI
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data.OleDb
Imports System.Net
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Net.Configuration

Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.Exchange.WebServices
Imports Microsoft.Exchange.WebServices.Data
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Mail
Imports System.Threading
Imports System.Security.Authentication
Partial Class Payment_Thanks
    Inherits System.Web.UI.Page
    Dim oclsSales As New Controller_clsSales()
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsProductGroup As New Controller_clsCategory
    Dim oclsLocation As New Controller_clsLocation
    Dim oclsProduct As New Controller_clsProduct
    Dim str As String = ""
    Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim ResponseH As String = ""
        Dim FOrderRef As String = ""
        Dim FPaymentRef As String = ""
        LogHelper.Error("CF_SUCESS:paymentjobref :" + System.DateTime.Now.ToString() + ": " + Request.QueryString("paymentjobref").ToString())
        LogHelper.Error("CF_SUCESS: paymentref :" + System.DateTime.Now.ToString() + ": " + Request.QueryString("paymentref").ToString())
        Try


            Dim paymentjobref As String = Request.QueryString("paymentjobref").ToString()
            Dim paymentref As String = Request.QueryString("paymentref").ToString()

            Dim arrOrdRef As String() = paymentjobref.Split(",")
            Dim arrPayRef As String() = paymentref.Split(",")

            FOrderRef = arrOrdRef(0).ToString()
            FPaymentRef = arrPayRef(arrPayRef.Length - 1).ToString

            CheckPay(FOrderRef)

            If Session("source_type").ToString() = "1" Then
                Response.Redirect("https://live.mytenderpos.com/CF_Fail.aspx?paymentjobref=" + Request.QueryString("paymentjobref").ToString() + "&paymentref=" + Request.QueryString("paymentref").ToString())
            Else
                Response.Redirect("https://live.mytenderpos.com/wpos/CF_fail.aspx?paymentjobref=" + Request.QueryString("paymentjobref").ToString() + "&paymentref=" + Request.QueryString("paymentref").ToString())
            End If
        Catch ex As Exception
            LogHelper.Error("CF_SUCESS: Payref Count :" + System.DateTime.Now.ToString())
        End Try


        CheckPay(FOrderRef)
    End Sub

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub CheckStore()
        Try

            Dim n As String = Session("store_name")

            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                str = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Decode_data(dt.Rows(0)("db_password")) + ";"
            Else


            End If
        Catch ex As Exception
            LogHelper.Error("Till:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub

    Public Sub MailTo_receipt(ByVal CmpID As Int32, ByVal LocationID As Int32, ByVal To_EmailId As String, ByVal Subject As String, ByVal Body As String, ByVal CC As String, ByVal BCC As String, ByVal attach As String)
        Dim oClsDataccess As New ClsDataccess
        Dim MailServer As String
        Dim MailServer_UserName As String
        Dim MailServer_Password As String
        Dim MailServer_Port As Integer
        Dim From_Email As String
        Dim Ssl As Boolean
        Dim alas As String

        'Dim dtMailDetail As DataTable = oClsDataccess.Getdatatable("select * from S_Email_Settings where Company_id = " + CmpID.ToString() + " and location_id = " + LocationID.ToString())
        'If dtMailDetail.Rows.Count > 0 Then
        '    MailServer = dtMailDetail.Rows(0)("MailServer")
        '    MailServer_UserName = dtMailDetail.Rows(0)("MailServer_UserName")
        '    MailServer_Password = dtMailDetail.Rows(0)("MailServer_Password")
        '    MailServer_Port = dtMailDetail.Rows(0)("Port")
        '    From_Email = dtMailDetail.Rows(0)("From_Email")
        '    Ssl = IIf(dtMailDetail.Rows(0)("ssl") = "1", True, False)
        '    alas = dtMailDetail.Rows(0)("Alias")
        'Else
        Dim configurationFile As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.config")
        Dim mailSettings As MailSettingsSectionGroup = configurationFile.GetSectionGroup("system.net/mailSettings")
        If Not mailSettings Is Nothing Then
            MailServer_Port = mailSettings.Smtp.Network.Port
            MailServer = mailSettings.Smtp.Network.Host
            MailServer_Password = mailSettings.Smtp.Network.Password
            MailServer_UserName = mailSettings.Smtp.Network.UserName
            From_Email = mailSettings.Smtp.From
            Ssl = True
            alas = "TenderPOS Sales"
        End If
        'End If

        LogHelper.Error("Base_class: 1:" + MailServer_Port.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer_Password.ToString())
        LogHelper.Error("Base_class: 1:" + MailServer_UserName.ToString())
        LogHelper.Error("Base_class: 1:" + From_Email.ToString())
        Try

            '  LogHelper.Error("Base_class: 1:" + System.DateTime.Now.ToString())
            Dim Email_CC As String
            Dim Email_BCC As String
            Dim oE_Mail As New MailMessage()
            oE_Mail.To.Clear()
            If To_EmailId <> "" Then
                oE_Mail.To.Add(To_EmailId)
            End If
            '    LogHelper.Error("Base_class: 2:" + System.DateTime.Now.ToString())
            If CC <> "" Then
                Email_CC = CC
                oE_Mail.CC.Add(Email_CC)
            End If
            If BCC <> "" Then
                Email_BCC = BCC
                oE_Mail.Bcc.Add(Email_BCC)
            End If

            If attach <> "" Then
                Dim strarr() As String
                Dim i As Integer
                strarr = attach.Split(",")
                For i = 0 To strarr.Length - 1
                    Dim str As String = strarr(i)
                    Dim path As String = str
                    Dim myattach As New System.Net.Mail.Attachment(path)
                    oE_Mail.Attachments.Add(myattach)
                Next
            End If


            oE_Mail.From = New MailAddress(From_Email, alas)
            oE_Mail.IsBodyHtml = True
            oE_Mail.Subject = Subject
            oE_Mail.Body = Body
            'LogHelper.Error("Base_class: 3:" + System.DateTime.Now.ToString())
            oE_Mail.Priority = MailPriority.High
            Dim oSmtpclient As New SmtpClient()
            oSmtpclient.Host = MailServer

            Const _Tls12 As SslProtocols = CType(&HC00, SslProtocols)
            Const Tls12 As SecurityProtocolType = CType(_Tls12, SecurityProtocolType)
            ServicePointManager.SecurityProtocol = Tls12

            oSmtpclient.UseDefaultCredentials = False
            oSmtpclient.EnableSsl = Ssl
            oSmtpclient.Credentials = New Net.NetworkCredential(MailServer_UserName, MailServer_Password)
            oSmtpclient.Port = MailServer_Port
            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network


            ' LogHelper.Error("Base_class: 4:" + System.DateTime.Now.ToString())
            oSmtpclient.Send(oE_Mail)
            'LogHelper.Error("Base_class: 5:" + System.DateTime.Now.ToString())
        Catch ex As Exception
            Err.Raise(Err.Number, , ex.ToString)

            LogHelper.Error("Base_class: send_mail:" + System.DateTime.Now.ToString() + ": " + ex.Message)

        Finally
            MailServer = Nothing
            MailServer_UserName = Nothing
            MailServer_Password = Nothing
            MailServer_Port = Nothing
            From_Email = Nothing
            Ssl = Nothing
        End Try
    End Sub

    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "", Optional ByVal constr As String = "") As System.Data.DataTable
        Dim sdr As DataTable = Nothing
        Dim com As New SqlCommand
        Dim SpAdepter As New SqlDataAdapter
        Try
            Using connection As New SqlConnection(constr)
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
                sdr = New System.Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("Till:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing
        End Try
        Return sdr
    End Function

    Public Sub CheckPay(ByVal order_ref As String)
        Try

            strcon.Open()

            Dim cmd As SqlCommand = New SqlCommand("Get_M_Pay_check", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@order_ref", order_ref)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then
                Session("store_name") = dt.Rows(0)("store_name").ToString()
                Session("sales_id") = dt.Rows(0)("sales_id").ToString()
                Session("source_type") = dt.Rows(0)("source_type").ToString()
                CheckStore()
            Else
            End If
        Catch ex As Exception
            LogHelper.Error("CF_success:CheckStore:" + System.DateTime.Now.ToString() + ": " + ex.Message)
        End Try
    End Sub


End Class

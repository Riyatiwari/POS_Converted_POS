<%@ WebHandler Language="VB" Class="CustomerProfileHandler" %>

Imports System.Web
Imports System.Web.Services
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel
Imports System.Web.Configuration
Imports System.Web.SessionState

Public Class CustomerProfileHandler : Implements IHttpHandler

    Dim oclsBind As New clsBinding
    Dim oclsCustomer As New Controller_clsCustomer()
    Dim str As String = ""

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            Dim dt As DataTable = getStore(context.Request.QueryString("store_name").ToString())

            If dt.Rows.Count > 0 Then

                'If context.Request.QueryString("authUsername").ToString() = dt.Rows(0)("user_name").ToString() And
                '        context.Request.QueryString("authPassword").ToString() = dt.Rows(0)("password").ToString() Then

                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                oColSqlPar.Add("@Customer_id", context.Request.QueryString("Customer_id"))
                oColSqlPar.Add("@Cmp_id", context.Request.QueryString("Cmp_id"))

                Dim imgdt As DataTable = GetdataTableSp("Get_Customer_profile", oColSqlPar, "Get_Customer_profile", getconstr(dt))
                If imgdt.Rows.Count > 0 Then
                    context.Response.BinaryWrite(DirectCast(imgdt.Rows(0)("CustomerProfile"), Byte()))

                    context.Response.End()
                    'Else
                    '    Dim oColSqlPar1 As ColSqlparram = New ColSqlparram()
                    '    imgdt = GetdataTableSp("Get_Default_Images", oColSqlPar1, "Get_Default_Images", str)
                    '    context.Response.BinaryWrite(DirectCast(imgdt.Rows(0)("image_name"), Byte()))

                    '    context.Response.End()
                End If

                'End If
            End If


        Catch ex As Exception
            Dim erroe As String = ex.ToString()
        End Try
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


    Public Function getStore(ByVal store_name As String) As DataTable
        Try
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            con.Open()
            Dim cmd As SqlCommand = New SqlCommand("WS_Get_Store", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@store_name", store_name)

            Dim adpter As New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adpter.Fill(dt)
            con.Close()
            Return dt

        Catch ex As Exception
            LogHelper.Error("POS_WS:getStore():" + ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function getconstr(ByVal dt As DataTable) As String
        Try
            Dim constr As String = "Data Source=" & dt.Rows(0)("db_server") & ";" & "Initial Catalog=" & dt.Rows(0)("db_name") & ";" & "User ID=" & dt.Rows(0)("db_username") & ";" & "Password=" & Decode_data(dt.Rows(0)("db_password")) & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
            Return constr

        Catch ex As Exception
            LogHelper.Error("POS_WS:getStore():" + ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetdataTableSp(ByVal SPName As String, ByVal oColSqlparram As ColSqlparram, Optional ByVal strTableName As String = "", Optional ByVal constr As String = "") As Data.DataTable
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
                sdr = New Data.DataTable
                SpAdepter.Fill(sdr)
                If strTableName <> "" Then sdr.TableName = strTableName
            End Using
        Catch ex As Exception : Throw ex
            LogHelper.Error("POS_WebServices:GetdataTableSp:" & ex.Message)
        Finally
            com.Parameters.Clear()
            com = Nothing
            SpAdepter = Nothing
        End Try
        Return sdr
    End Function

End Class
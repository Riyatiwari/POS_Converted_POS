<%@ WebHandler Language="VB" Class="TillImageHandler " %>


Imports System.Web
Imports System.Web.Services
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel
Imports System.Web.Configuration
Imports System.Web.SessionState

Public Class TillImageHandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            Dim dt As DataTable = getStore(context.Request.QueryString("store_name").ToString())
            LogHelper.Error("POS_WS:ProcessRequest:1")
            If dt.Rows.Count > 0 Then

                Dim imgdt As DataTable = New DataTable()
                Dim oColSqlPar As ColSqlparram = New ColSqlparram()
                LogHelper.Error("POS_WS:ProcessRequest:2")

                If Val(context.Request.QueryString("till_id").ToString()) > 0 Then

                    oColSqlPar.Add("@till_id", context.Request.QueryString("till_id"))
                    oColSqlPar.Add("@imageno", context.Request.QueryString("imgno"))
                    LogHelper.Error("POS_WS:ProcessRequest:3")
                    imgdt = GetdataTableSp("WS_Get_till_Image", oColSqlPar, "WS_Get_till_Image", getconstr(dt))
                    LogHelper.Error("POS_WS:ProcessRequest:4" + imgdt.Rows.Count.ToString() + getconstr(dt).ToString())
                End If


                If imgdt.Rows.Count > 0 Then
                    LogHelper.Error("POS_WS:ProcessRequest:5")
                    context.Response.BinaryWrite(DirectCast(imgdt.Rows(0)("limg"), Byte()))

                    context.Response.End()

                End If
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

    Public Function getconstr(ByVal dt1 As DataTable) As String
        Try
            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()
            Dim n As String = dt1.Rows(0)("store_name")
            Dim constr As String = ""
            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            If dt.Rows.Count > 0 Then

                constr = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Decode_data(dt.Rows(0)("db_password")) + ";"
            Else


            End If
            strcon.Close()
            'Dim constr As String = "Data Source=" & dt.Rows(0)("db_server") & ";" & "Initial Catalog=" & dt.Rows(0)("db_name") & ";" & "User ID=" & dt.Rows(0)("db_username") & ";" & "Password=" & Decode_data(dt.Rows(0)("db_password")) & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
            Return constr

        Catch ex As Exception
            LogHelper.Error("POS_WS:getconstr():" + ex.Message)
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
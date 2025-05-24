Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Configuration
Imports System

Public Class DBConnection
    Private con As New SqlConnection("Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password"))

    '
    ' TODO: Add constructor logic here
    '
    Public Sub New()
    End Sub

#Region "QueryFunction"

    Public Sub Ins_Upd_Del(ByVal query As String)
        Try


            con.Open()

            Dim cmd As New SqlCommand(query, con)
            cmd.ExecuteNonQuery()

            cmd.Dispose()
            con.Close()
        Catch ex As Exception
            con.Close()
        End Try
    End Sub

    Public Function ExecuteNonQuery(ByVal query As String) As Integer
        Try
            con.Open()
            Dim cmd As New SqlCommand(query, con)
            Dim retVal As Integer = cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            Return retVal
        Catch ex As Exception
            con.Close()
            Return 0
        End Try


    End Function

    Public Function ExecuteScalar(ByVal query As String) As Object
        con.Open()
        Dim cmd As New SqlCommand(query, con)
        Dim retVal As Object = cmd.ExecuteScalar()
        cmd.Dispose()
        con.Close()
        Return retVal
    End Function

    Public Function SelectData(ByVal query As String) As DataSet
        Try

            Dim ad As New SqlDataAdapter(query, con)

            Dim ds As New DataSet()

            ad.Fill(ds)

            Return ds



        Catch ex As Exception
            LogHelper.Error("DBConnection:SelectData" + ex.Message)
        End Try

    End Function

    Public Function CheckData(ByVal query As String) As Boolean
        Dim ds As DataSet = SelectData(query)

        If ds.Tables(0).Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function SingleRow(ByVal query As String) As DataRow
        Dim ds As DataSet = SelectData(query)
        If ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function SingleCell(ByVal query As String) As String
        Dim ds As DataSet = SelectData(query)

        Return ds.Tables(0).Rows(0)(0).ToString()
    End Function



#End Region

#Region "SPFunction"

    Public Sub SP_Ins_Upd_Del(ByVal spname As String, ByVal p As SqlParameter())
        con.Open()

        Dim cmd As New SqlCommand(spname, con)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.AddRange(p)

        cmd.ExecuteNonQuery()

        cmd.Dispose()
        con.Close()
    End Sub

    Public Function SP_SelectData(ByVal spname As String) As DataSet
        con.Open()

        Dim cmd As New SqlCommand(spname, con)
        cmd.CommandType = CommandType.StoredProcedure

        Dim ad As New SqlDataAdapter(cmd)

        Dim ds As New DataSet()

        ad.Fill(ds)

        cmd.Dispose()
        con.Close()

        Return ds
    End Function

    Public Function SP_SelectData(ByVal spname As String, ByVal p As SqlParameter()) As DataSet
        con.Open()

        Dim cmd As New SqlCommand(spname, con)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.AddRange(p)

        Dim ad As New SqlDataAdapter(cmd)

        Dim ds As New DataSet()

        ad.Fill(ds)

        cmd.Dispose()
        con.Close()

        Return ds
    End Function

    Public Function SP_CheckData(ByVal spname As String) As Boolean
        Dim ds As DataSet = SP_SelectData(spname)

        If ds.Tables(0).Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function SP_CheckData(ByVal spname As String, ByVal p As SqlParameter()) As Boolean
        Dim ds As DataSet = SP_SelectData(spname, p)

        If ds.Tables(0).Rows.Count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function SP_SingleRow(ByVal spname As String) As DataRow
        Dim ds As DataSet = SP_SelectData(spname)

        Return ds.Tables(0).Rows(0)
    End Function

    Public Function SP_SingleRow(ByVal spname As String, ByVal p As SqlParameter()) As DataRow
        Dim ds As DataSet = SP_SelectData(spname, p)

        Return ds.Tables(0).Rows(0)
    End Function

    Public Function SP_SingleCell(ByVal spname As String) As String
        Dim ds As DataSet = SP_SelectData(spname)

        Return ds.Tables(0).Rows(0)(0).ToString()
    End Function

    Public Function SP_SingleCell(ByVal spname As String, ByVal p As SqlParameter()) As String
        Dim ds As DataSet = SP_SelectData(spname, p)

        Return ds.Tables(0).Rows(0)(0).ToString()
    End Function

#End Region
End Class


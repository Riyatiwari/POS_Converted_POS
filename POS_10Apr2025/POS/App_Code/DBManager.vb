''Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System

Public Class DBManager
    Dim dbconnection As SqlConnection
    Dim glb_Tran As SqlClient.SqlTransaction
    Public _ConnectionString As String = ""
    Public singleInstance As DBManager

    Public Sub New()
        _ConnectionString = connection_data()
    End Sub
    Public Function GetSingleton() As DBManager
        Try
            If singleInstance._ConnectionString = "" Then
                singleInstance = New DBManager()
            ElseIf singleInstance._ConnectionString = "Min Pool Size=10;Max Pool Size=500;Connect Timeout=500" Then
                singleInstance = New DBManager()
            End If
            HttpContext.Current.Session("session") = "2"
        Catch ex As Exception
            LogHelper.Error("DBManager:GetSingleton:" + ex.Message)
        End Try
        Return singleInstance
    End Function
    Public Function connection_data() As String
        Dim sb As New SqlConnectionStringBuilder
        Dim strMin_Pool_Size As String
        Dim strMax_Pool_Size As String
        Dim strConnect_Timeout As String
        Dim str_connection As String = String.Empty
        Try
            If HttpContext.Current.Session("db_server") <> "" Or HttpContext.Current.Session("db_server") = Nothing Then
                If str_connection = "" Then
                    If ConfigurationManager.AppSettings("Windows_Authenticate") = "Yes" Then
                        str_connection = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
                    Else
                        str_connection = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
                    End If
                End If
                strMin_Pool_Size = ConfigurationManager.AppSettings("Min_Pool_Size")
                str_connection = str_connection & ";" & "Min pool size=" & strMin_Pool_Size

                strMax_Pool_Size = ConfigurationManager.AppSettings("Max_Pool_Size")
                str_connection = str_connection & ";" & "Max pool size=" & strMax_Pool_Size

                strConnect_Timeout = ConfigurationManager.AppSettings("Connect_Timeout")
                str_connection = str_connection & ";" & "Connect Timeout=" & strConnect_Timeout

                sb.ConnectionString = str_connection
            End If
        Catch ex As Exception
            LogHelper.Error("DBManager:connection_data:" + ex.Message)
        Finally
            strMin_Pool_Size = Nothing
            strMax_Pool_Size = Nothing
            strConnect_Timeout = Nothing
        End Try
        Return sb.ConnectionString
    End Function
    Public Function getconnection() As SqlConnection
        Try
            If dbconnection Is Nothing Then
                dbconnection = New SqlConnection
                dbconnection.ConnectionString = Me.connection_data()
            ElseIf dbconnection.ConnectionString = "" Then
                dbconnection.ConnectionString = Me.connection_data()
            End If
            If dbconnection.State = ConnectionState.Open Then
                dbconnection.Close()
            End If
            If dbconnection IsNot Nothing OrElse dbconnection.State = ConnectionState.Closed Then
                dbconnection.Open()
            End If
        Catch ex As Exception
            If ex.Message.Contains("Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.") Then
                SqlConnection.ClearAllPools()
            End If
            dbconnection.Close()
            dbconnection.Dispose()
            LogHelper.Error("DataManager:" & ex.Message)
        End Try
        Return dbconnection
    End Function
    Public Function getConnString() As String
        Try
            getConnString = singleInstance._ConnectionString
        Catch ex As Exception
            LogHelper.Error("DataManager:getConnString" & ex.Message)
        End Try
    End Function

    Public Function get_first_ConnString() As String
        Try
            'If singleInstance._ConnectionString = String.Empty Then
            '    singleInstance.connection_data()
            'Else
            '    get_first_ConnString = singleInstance._ConnectionString

            'End If
            If _ConnectionString = String.Empty Or _ConnectionString = "Min Pool Size=10;Max Pool Size=100;Connect Timeout=0" Then
                'singleInstance.connection_data()
                connection_data()
            Else
                get_first_ConnString = _ConnectionString

            End If
        Catch ex As Exception
            LogHelper.Error("DataManager:get_first_ConnString" & ex.Message)
        End Try
    End Function
    Public Sub Closeconnection()
        Try
            If dbconnection IsNot Nothing And dbconnection.State = ConnectionState.Open Then
                dbconnection.Close()
            End If
        Catch ex As Exception
            dbconnection.Close()
            dbconnection.Dispose()
            LogHelper.Error("DBManager:Closeconnection:" + ex.Message)
        End Try
    End Sub
    Public Sub Reset_string()
        Try
            singleInstance._ConnectionString = String.Empty
        Catch ex As Exception
            LogHelper.Error("DBManager:Reset_string:" + ex.Message)
        End Try
    End Sub

    Public Sub Destory()
        Try
            Closeconnection()
            dbconnection.Dispose()
        Catch ex As Exception
            LogHelper.Error("DBManager:Destory:" + ex.Message)
        End Try
    End Sub
End Class


' ''Imports Microsoft.VisualBasic
'Imports System.Data
'Imports System.Data.SqlClient
'Imports System.Configuration
'Public Class DBManager
'    Private Shared singleInstance As New DBManager
'    Dim dbconnection As SqlConnection
'    Dim glb_Tran As SqlClient.SqlTransaction
'    Public _ConnectionString As String = ""

'    Private Sub New()
'        _ConnectionString = connection_data()
'    End Sub
'    Public Shared Function GetSingleton() As DBManager
'        Try
'            If singleInstance._ConnectionString = "" Then
'                singleInstance = New DBManager()
'            ElseIf singleInstance._ConnectionString = "Min Pool Size=10;Max Pool Size=500;Connect Timeout=500" Then
'                singleInstance = New DBManager()
'            End If
'        Catch ex As Exception
'            LogHelper.Error("DBManager:GetSingleton:" + ex.Message)

'        End Try
'        Return singleInstance
'    End Function

'    Public Function connection_data() As String
'        Dim sb As New SqlConnectionStringBuilder
'        Dim strMin_Pool_Size As String
'        Dim strMax_Pool_Size As String
'        Dim strConnect_Timeout As String
'        Dim str_connection As String = String.Empty
'        Try

'            'If str_connection = "" Then

'            '    If ConfigurationManager.AppSettings("Windows_Authenticate") = "Yes" Then
'            '        str_connection = "Data Source=" & ConfigurationManager.AppSettings("dataSource") & ";" & "Initial Catalog=" & ConfigurationManager.AppSettings("dataSource_dbname") & ";" & "Trusted_Connection=True;Integrated Security=SSPI"
'            '    Else
'            '        str_connection = "Data Source=" & ConfigurationManager.AppSettings("dataSource") & ";" & "Initial Catalog=" & ConfigurationManager.AppSettings("dataSource_dbname") & ";" & "User ID=" & ConfigurationManager.AppSettings("dataSource_username") & ";" & "Password=" & ConfigurationManager.AppSettings("dataSource_password")
'            '    End If
'            'End If

'            'strMin_Pool_Size = ConfigurationManager.AppSettings("Min_Pool_Size")
'            'str_connection = str_connection & ";" & "Min pool size=" & strMin_Pool_Size

'            'strMax_Pool_Size = ConfigurationManager.AppSettings("Max_Pool_Size")
'            'str_connection = str_connection & ";" & "Max pool size=" & strMax_Pool_Size

'            'strConnect_Timeout = ConfigurationManager.AppSettings("Connect_Timeout")
'            'str_connection = str_connection & ";" & "Connect Timeout=" & strConnect_Timeout

'            'sb.ConnectionString = str_connection
'            If HttpContext.Current.Session("db_server") <> "" Or HttpContext.Current.Session("db_server") = Nothing Then


'                If str_connection = "" Then
'                    If ConfigurationManager.AppSettings("Windows_Authenticate") = "Yes" Then
'                        str_connection = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "Trusted_Connection=True;Integrated Security=SSPI"

'                    Else
'                        str_connection = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")

'                    End If
'                End If


'                strMin_Pool_Size = ConfigurationManager.AppSettings("Min_Pool_Size")
'                str_connection = str_connection & ";" & "Min pool size=" & strMin_Pool_Size

'                strMax_Pool_Size = ConfigurationManager.AppSettings("Max_Pool_Size")
'                str_connection = str_connection & ";" & "Max pool size=" & strMax_Pool_Size

'                strConnect_Timeout = ConfigurationManager.AppSettings("Connect_Timeout")
'                str_connection = str_connection & ";" & "Connect Timeout=" & strConnect_Timeout

'                sb.ConnectionString = str_connection


'            End If
'        Catch ex As Exception
'            LogHelper.Error("DBManager:connection_data:" + ex.Message)
'        Finally
'            strMin_Pool_Size = Nothing
'            strMax_Pool_Size = Nothing
'            strConnect_Timeout = Nothing
'        End Try
'        Return sb.ConnectionString
'    End Function
'    Public Function getconnection() As SqlConnection
'        Try
'            If dbconnection Is Nothing Then
'                dbconnection = New SqlConnection
'                dbconnection.ConnectionString = Me.connection_data()

'            ElseIf dbconnection.ConnectionString = "" Then
'                dbconnection.ConnectionString = Me.connection_data()
'            End If
'            If dbconnection.State = ConnectionState.Open Then
'                dbconnection.Close()
'            End If
'            If dbconnection IsNot Nothing OrElse dbconnection.State = ConnectionState.Closed Then
'                dbconnection.Open()
'            End If
'        Catch ex As Exception
'            If ex.Message.Contains("Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.") Then
'                SqlConnection.ClearAllPools()
'            End If
'            dbconnection.Close()
'            dbconnection.Dispose()
'            LogHelper.Error("DataManager:" & ex.Message)
'        End Try
'        Return dbconnection
'    End Function
'    Public Shared Function getConnString() As String
'        Try
'            getConnString = singleInstance._ConnectionString
'        Catch ex As Exception
'            LogHelper.Error("DataManager:getConnString" & ex.Message)
'        End Try
'    End Function
'    Public Shared Function get_first_ConnString() As String
'        Try
'            If singleInstance._ConnectionString = String.Empty Then
'                singleInstance.connection_data()
'            Else
'                get_first_ConnString = singleInstance._ConnectionString
'            End If
'        Catch ex As Exception
'            LogHelper.Error("DataManager:get_first_ConnString" & ex.Message)
'        End Try
'    End Function
'    Public Sub Closeconnection()
'        Try
'            If dbconnection IsNot Nothing And dbconnection.State = ConnectionState.Open Then
'                dbconnection.Close()
'            End If
'        Catch ex As Exception
'            dbconnection.Close()
'            dbconnection.Dispose()
'            LogHelper.Error("DBManager:Closeconnection:" + ex.Message)
'        End Try
'    End Sub
'    Public Shared Sub Reset_string()
'        Try
'            singleInstance._ConnectionString = String.Empty
'        Catch ex As Exception
'            LogHelper.Error("DBManager:Reset_string:" + ex.Message)
'        End Try
'    End Sub
'    Public Sub Destory()
'        Try
'            Closeconnection()
'            Reset_string()
'            dbconnection.Dispose()
'        Catch ex As Exception
'            LogHelper.Error("DBManager:Destory:" + ex.Message)
'        End Try
'    End Sub
'End Class


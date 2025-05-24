Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Globalization

Imports AppHelper
Imports User_Log
Public Class LogHelper
    Public Shared logfile As String = "\logs\DBLibrary.log"
    Public Shared debugfile As String = "\logs\DBLibrary.dbg"
    Public Shared errlogfile As String = "\logs\DBLibrary.err"

    Public Shared Sub Write(ByVal message As String)

        Dim datewisefile As String = ""
        Dim dt As DateTime = DateTime.Now
        Dim info As Byte() = Nothing
        Dim fs As System.IO.FileStream = Nothing
        Try
            If Not System.IO.Directory.Exists(AppHelper.AppPath & "\logs") Then
                System.IO.Directory.CreateDirectory(AppHelper.AppPath & "\logs")
            End If

            datewisefile = ((dt.Year & "") + "-" + (If(dt.Month <= 9, "0" & dt.Month & "", dt.Month & "")) & "") + "-" + (If(dt.Day <= 9, "0" & dt.Day & "", dt.Day & ""))
            logfile = "\Trace\Trace_" & datewisefile & ".log"
            fs = System.IO.File.Open(AppHelper.AppPath + logfile, System.IO.FileMode.Append)
            info = New System.Text.UTF8Encoding(True).GetBytes((System.DateTime.Now.ToString() & ":") + message & vbLf)

            fs.Write(info, 0, info.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            info = Nothing
        Catch ex As Exception

        Finally
           

        End Try
    End Sub
    Public Shared Sub Debug(ByVal message As String)
        Dim datewisefile As String = ""
        Dim dt As DateTime = DateTime.Now
        Dim fs As System.IO.FileStream = Nothing
        Dim info As Byte() = Nothing
        Dim name As String = AppHelper.AppPath

        Try
            If Not System.IO.Directory.Exists(User_Log.AppPath & "\logs") Then
                System.IO.Directory.CreateDirectory(User_Log.AppPath & "\logs")
            End If

            datewisefile = ((dt.Year & "") + "-" + (If(dt.Month <= 9, "0" & dt.Month & "", dt.Month & "")) & "") + "-" + (If(dt.Day <= 9, "0" & dt.Day & "", dt.Day & ""))
            debugfile = "\logs\Err_" & datewisefile & ".dbg"
            fs = System.IO.File.Open(AppHelper.AppPath + debugfile, System.IO.FileMode.Append)
            info = New System.Text.UTF8Encoding(True).GetBytes((System.DateTime.Now.ToString() & ":") + message & vbLf)

            fs.Write(info, 0, info.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            info = Nothing
        Catch ex As Exception

        Finally
           

        End Try
    End Sub
    Public Shared Sub [Error](ByVal message As String)
        Dim datewisefile As String = ""
        Dim dt As DateTime = DateTime.Now
        Dim fs As System.IO.FileStream = Nothing
        Dim info As Byte() = Nothing
        Try
            If Not System.IO.Directory.Exists(AppHelper.AppPath & "logs") Then
                System.IO.Directory.CreateDirectory(AppHelper.AppPath & "logs")
            End If

            datewisefile = ((dt.Year & "") + "-" + (If(dt.Month <= 9, "0" & dt.Month & "", dt.Month & "")) & "") + "-" + (If(dt.Day <= 9, "0" & dt.Day & "", dt.Day & ""))
            errlogfile = "\Err_" & datewisefile & ".err"
            fs = System.IO.File.Open(AppHelper.AppPath & "logs" + errlogfile, System.IO.FileMode.Append)
            info = New System.Text.UTF8Encoding(True).GetBytes((System.DateTime.Now.ToString() & ":") + message & vbLf)

            fs.Write(info, 0, info.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            info = Nothing
        Catch ex As Exception

        Finally
        End Try
    End Sub
    Public Shared Sub [database_Error](ByVal message As String)
        Dim datewisefile As String = ""
        Dim dt As DateTime = DateTime.Now
        Dim fs As System.IO.FileStream = Nothing
        Dim info As Byte() = Nothing
        Try
            If Not System.IO.Directory.Exists(AppHelper.AppPath & "logs") Then
                System.IO.Directory.CreateDirectory(AppHelper.AppPath & "logs")
            End If
            datewisefile = ((dt.Year & "") + "-" + (If(dt.Month <= 9, "0" & dt.Month & "", dt.Month & "")) & "") + "-" + (If(dt.Day <= 9, "0" & dt.Day & "", dt.Day & ""))
            errlogfile = "\Err_" & datewisefile & ".err"
            fs = System.IO.File.Open(AppHelper.AppPath & "logs" + errlogfile, System.IO.FileMode.Append)
            info = New System.Text.UTF8Encoding(True).GetBytes((System.DateTime.Now.ToString() & ":") + message & vbLf)

            fs.Write(info, 0, info.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            info = Nothing
        Catch ex As Exception
        Finally
        End Try
    End Sub
    Public Shared Sub [User_Error](ByVal message As String)

        Dim datewisefile As String = ""
        Dim dt As DateTime = DateTime.Now
        Dim fs As System.IO.FileStream = Nothing
        Dim info As Byte() = Nothing
        Try
            If Not System.IO.Directory.Exists(User_Log.AppPath & "logs") Then
                System.IO.Directory.CreateDirectory(User_Log.AppPath & "logs")
            End If
            datewisefile = ((dt.Year & "") + "-" + (If(dt.Month <= 9, "0" & dt.Month & "", dt.Month & "")) & "") + "-" + (If(dt.Day <= 9, "0" & dt.Day & "", dt.Day & ""))
            errlogfile = "\Err_" & datewisefile & ".err"
            fs = System.IO.File.Open(User_Log.AppPath & "logs" + errlogfile, System.IO.FileMode.Append)
            info = New System.Text.UTF8Encoding(True).GetBytes((System.DateTime.Now.ToString() & ":") + message & vbLf)
            fs.Write(info, 0, info.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            info = Nothing
        Catch ex As Exception
        Finally


        End Try
    End Sub
    Public Shared Sub [Session_Log](ByVal message As String)

        Dim datewisefile As String = ""
        Dim dt As DateTime = DateTime.Now
        Dim fs As System.IO.FileStream = Nothing
        Dim info As Byte() = Nothing
        Try
            If Not System.IO.Directory.Exists(User_Log.AppPath & "logs") Then
                System.IO.Directory.CreateDirectory(User_Log.AppPath & "logs")
            End If
            datewisefile = ((dt.Year & "") + "-" + (If(dt.Month <= 9, "0" & dt.Month & "", dt.Month & "")) & "") + "-" + (If(dt.Day <= 9, "0" & dt.Day & "", dt.Day & ""))
            errlogfile = "\Session_Time_" & datewisefile & ".txt"
            fs = System.IO.File.Open(User_Log.AppPath & "logs" + errlogfile, System.IO.FileMode.Append)
            info = New System.Text.UTF8Encoding(True).GetBytes((System.DateTime.Now.ToString() & ":") + message & vbLf)
            fs.Write(info, 0, info.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            info = Nothing
        Catch ex As Exception
        Finally


        End Try
    End Sub
    Public Shared Sub [Application_Error_Log](ByVal message As String)

        Dim datewisefile As String = ""
        Dim dt As DateTime = DateTime.Now
        Dim fs As System.IO.FileStream = Nothing
        Dim info As Byte() = Nothing
        Try
            If Not System.IO.Directory.Exists(User_Log.AppPath & "logs") Then
                System.IO.Directory.CreateDirectory(User_Log.AppPath & "logs")
            End If
            datewisefile = ((dt.Year & "") + "-" + (If(dt.Month <= 9, "0" & dt.Month & "", dt.Month & "")) & "") + "-" + (If(dt.Day <= 9, "0" & dt.Day & "", dt.Day & ""))
            errlogfile = "\Application_Error_" & datewisefile & ".Err"
            fs = System.IO.File.Open(User_Log.AppPath & "logs" + errlogfile, System.IO.FileMode.Append)
            info = New System.Text.UTF8Encoding(True).GetBytes((System.DateTime.Now.ToString() & ":") + message & vbLf)
            fs.Write(info, 0, info.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            info = Nothing
        Catch ex As Exception
        Finally

        End Try
    End Sub
End Class

Imports System
Imports System.Web
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
#Region "Admin Helper"
Public Class AppHelper
 
    Public Shared AppPath As String = HttpRuntime.AppDomainAppPath.ToString()

    Public Shared CommonPath As String = "\common"

    Public Sub New()
        LogHelper.Write((Me.ToString() & " AppPath ") + AppPath)

        LogHelper.Write((Me.ToString() & " CommonPath ") + CommonPath)
    End Sub
End Class
#End Region

#Region "UserHelper"
Public Class User_Log
    Public Shared AppPath As String = HttpRuntime.AppDomainAppPath.ToString()
    Public Shared CommonPath As String = "\common"
    Public Sub New()
        LogHelper.Write((Me.ToString() & " AppPath ") + AppPath)

        LogHelper.Write((Me.ToString() & " CommonPath ") + CommonPath)
    End Sub
End Class
#End Region





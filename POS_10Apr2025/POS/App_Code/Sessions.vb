Imports Microsoft.VisualBasic
Imports System.Web
Imports System

Public Class Sessions
    Public Shared Property UserID() As Integer
        Get
            If HttpContext.Current.Session("UserID") Is Nothing Then
                Return 0
            Else
                Return Convert.ToInt32(HttpContext.Current.Session("UserID"))
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("UserID") = value
        End Set
    End Property

    Public Shared Property UserName() As String
        Get
            If HttpContext.Current.Session("UserName") Is Nothing Then
                Return ""
            Else
                Return HttpContext.Current.Session("UserName").ToString()
            End If
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("UserName") = value
        End Set
    End Property

    Public Shared Property store() As String
        Get
            If HttpContext.Current.Session("store") Is Nothing Then
                Return ""
            Else
                Return HttpContext.Current.Session("store").ToString()
            End If
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("store") = value
        End Set
    End Property

    Public Shared Property BookingType() As Integer
        Get
            If HttpContext.Current.Session("BookingType") Is Nothing Then
                Return 0
            Else
                Return Utils.getInteger(HttpContext.Current.Session("BookingType").ToString())
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("BookingType") = value
        End Set
    End Property

    Public Shared Property TabID() As Integer
        Get
            If HttpContext.Current.Session("TabID") Is Nothing Then
                Return 0
            Else
                Return Utils.getInteger(HttpContext.Current.Session("TabID").ToString())
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("TabID") = value
        End Set
    End Property

    Public Shared Property Login() As Integer
        Get
            If HttpContext.Current.Session("Login") Is Nothing Then
                Return 0
            Else
                Return Utils.getInteger(HttpContext.Current.Session("Login").ToString())
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("Login") = value
        End Set
    End Property

End Class

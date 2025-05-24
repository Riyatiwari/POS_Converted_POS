Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsPrefix
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
    Private _Prefix_id As Integer
    Private _Prefix As String
    Private _cmp_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _is_active As Byte
    Private objclsPrefix As clsPrefix
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property Prefix_id() As Integer
        Get
            Return _Prefix_id
        End Get
        Set(ByVal value As Integer)
            _Prefix_id = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property
    Public Property login_id() As Integer
        Get
            Return _login_id
        End Get
        Set(ByVal value As Integer)
            _login_id = value
        End Set
    End Property
#Region "Function"
    Public Function Insert() As Boolean
        Try
            objclsPrefix = New clsPrefix()
            objclsPrefix.Prefix_id = Prefix_id
            objclsPrefix.Prefix = Prefix
            objclsPrefix.cmp_id = cmp_id
            objclsPrefix.Ip_address = Ip_address
            objclsPrefix.login_id = login_id
            objclsPrefix.is_active = is_active
            If objclsPrefix.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsPrefix = New clsPrefix()
            objclsPrefix.Prefix_id = Prefix_id
            objclsPrefix.Prefix = Prefix
            objclsPrefix.cmp_id = cmp_id
            objclsPrefix.Ip_address = Ip_address
            objclsPrefix.login_id = login_id
            objclsPrefix.is_active = is_active
            If objclsPrefix.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsPrefix = New clsPrefix()
            objclsPrefix.Prefix_id = Prefix_id
            objclsPrefix.Prefix = Prefix
            objclsPrefix.cmp_id = cmp_id
            objclsPrefix.Ip_address = Ip_address
            objclsPrefix.login_id = login_id
            objclsPrefix.is_active = is_active
            If objclsPrefix.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsPrefix = New clsPrefix()
            objclsPrefix.cmp_id = cmp_id
            objclsPrefix.Prefix_id = Prefix_id
            dt = objclsPrefix.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsPrefix = New clsPrefix()
            objclsPrefix.cmp_id = cmp_id
            dt = objclsPrefix.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsPrefix
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
    Private _Prefix_id As Integer
    Private _Prefix As String
    Private _cmp_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _is_active As Byte
    Private objclsPrefix As clsPrefix
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property Prefix_id() As Integer
        Get
            Return _Prefix_id
        End Get
        Set(ByVal value As Integer)
            _Prefix_id = value
        End Set
    End Property
    Public Property Prefix() As String
        Get
            Return _Prefix
        End Get
        Set(ByVal value As String)
            _Prefix = value
        End Set
    End Property
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property
    Public Property login_id() As Integer
        Get
            Return _login_id
        End Get
        Set(ByVal value As Integer)
            _login_id = value
        End Set
    End Property
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Prefix_id", Prefix_id, SqlDbType.Int)
            oColSqlparram.Add("@Prefix", Prefix)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Prefix", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Prefix_id", Prefix_id, SqlDbType.Int)
            oColSqlparram.Add("@Prefix", Prefix)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Prefix", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Prefix_id", Prefix_id, SqlDbType.Int)
            oColSqlparram.Add("@Prefix", Prefix)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Prefix", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Prefix_id", Prefix_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Prefix", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Prefix", oColSqlparram)

        Return dtlogin
    End Function
End Class

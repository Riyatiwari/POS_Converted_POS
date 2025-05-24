Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsPayment

#Region "Public Variables"
    Private _Payment_Id As Integer
    Private _cmp_id As Integer
    Private _Name As String
    Private _Hostname As String
    Private _Password As String
    Private _CurrencyCode As Integer
    Private _TransactionType As String
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _login_id As Integer

    Private objclsPayment As clsPayment
#End Region

#Region "Public Property"
    Public Property Payment_Id() As Integer
        Get
            Return _Payment_Id
        End Get
        Set(ByVal value As Integer)
            _Payment_Id = value
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
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property Hostname() As String
        Get
            Return _Hostname
        End Get
        Set(ByVal value As String)
            _Hostname = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property CurrencyCode() As Integer
        Get
            Return _CurrencyCode
        End Get
        Set(ByVal value As Integer)
            _CurrencyCode = value
        End Set
    End Property
    Public Property TransactionType() As String
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As String)
            _TransactionType = value
        End Set
    End Property
    Public Property Is_active() As Byte
        Get
            Return _Is_active
        End Get
        Set(ByVal value As Byte)
            _Is_active = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property

#End Region
#Region "Public Function"
    Public Function Insert() As Boolean
        Try
            objclsPayment = New clsPayment()
            objclsPayment.Payment_Id = Payment_Id
            objclsPayment.cmp_id = cmp_id
            objclsPayment.Name = Name
            objclsPayment.Hostname = Hostname
            objclsPayment.Password = Password
            objclsPayment.CurrencyCode = CurrencyCode
            objclsPayment.TransactionType = TransactionType
            objclsPayment.Is_active = Is_active
            objclsPayment.Ip_address = Ip_address
            objclsPayment.login_id = login_id
            If objclsPayment.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsPayment = New clsPayment()
            objclsPayment.Payment_Id = Payment_Id
            objclsPayment.cmp_id = cmp_id
            objclsPayment.Name = Name
            objclsPayment.Hostname = Hostname
            objclsPayment.Password = Password
            objclsPayment.CurrencyCode = CurrencyCode
            objclsPayment.TransactionType = TransactionType
            objclsPayment.Is_active = Is_active
            objclsPayment.Ip_address = Ip_address
            objclsPayment.login_id = login_id
            If objclsPayment.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsPayment = New clsPayment()
            objclsPayment.Payment_Id = Payment_Id
            objclsPayment.cmp_id = cmp_id
            objclsPayment.Name = Name
            objclsPayment.Hostname = Hostname
            objclsPayment.Password = Password
            objclsPayment.CurrencyCode = CurrencyCode
            objclsPayment.TransactionType = TransactionType
            objclsPayment.Is_active = Is_active
            objclsPayment.Ip_address = Ip_address
            objclsPayment.login_id = login_id
            If objclsPayment.Delete() Then
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
            objclsPayment = New clsPayment()
            objclsPayment.Payment_Id = Payment_Id
            objclsPayment.cmp_id = cmp_id
            dt = objclsPayment.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsPayment = New clsPayment()
            'objclsPayment.Payment_Id = Payment_Id
            objclsPayment.cmp_id = cmp_id
            dt = objclsPayment.SelectAll()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectPayment]() As DataTable
        Dim dt As DataTable
        Try
            objclsPayment = New clsPayment()
            objclsPayment.cmp_id = cmp_id
            dt = objclsPayment.[SelectPayment]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

End Class

Public Class clsPayment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


#Region "Public Variables"
    Private _Payment_Id As Integer
    Private _cmp_id As Integer
    Private _Name As String
    Private _Hostname As String
    Private _Password As String
    Private _CurrencyCode As Integer
    Private _TransactionType As String
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _login_id As Integer

    Private objclsPayment As clsPayment
#End Region

#Region "Public Property"
    Public Property Payment_Id() As Integer
        Get
            Return _Payment_Id
        End Get
        Set(ByVal value As Integer)
            _Payment_Id = value
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
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property Hostname() As String
        Get
            Return _Hostname
        End Get
        Set(ByVal value As String)
            _Hostname = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property CurrencyCode() As Integer
        Get
            Return _CurrencyCode
        End Get
        Set(ByVal value As Integer)
            _CurrencyCode = value
        End Set
    End Property
    Public Property TransactionType() As String
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As String)
            _TransactionType = value
        End Set
    End Property
    Public Property Is_active() As Byte
        Get
            Return _Is_active
        End Get
        Set(ByVal value As Byte)
            _Is_active = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property

#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Payment_Id", Payment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Hostname", Hostname)
            oColSqlparram.Add("@Password", Password)
            oColSqlparram.Add("@CurrencyCode", CurrencyCode, SqlDbType.Int)
            oColSqlparram.Add("@TransactionType", TransactionType)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Payment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Payment_Id", Payment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Hostname", Hostname)
            oColSqlparram.Add("@Password", Password)
            oColSqlparram.Add("@CurrencyCode", CurrencyCode, SqlDbType.Int)
            oColSqlparram.Add("@TransactionType", TransactionType)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Payment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Payment_Id", Payment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Hostname", Hostname)
            oColSqlparram.Add("@Password", Password)
            oColSqlparram.Add("@CurrencyCode", CurrencyCode, SqlDbType.Int)
            oColSqlparram.Add("@TransactionType", TransactionType)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Payment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Payment_Id", Payment_Id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Payment", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Payment_Id", Payment_Id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Payment", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectPayment]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Payment_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function
End Class
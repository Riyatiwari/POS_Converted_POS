Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading



Public Class Controller_clsTax
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Tax_id As Integer
    Private _cmp_id As Integer
    Private _Name As String
    Private _Mode As String
    Private _Value As Decimal
    Private _Is_active As Byte
    Private _Effective_Date As DateTime
    Private _Created_date As System.DateTime
    Private _Modified_date As System.DateTime
    Private _Login_id As Integer
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private objclsTax As clsTax
#End Region

#Region "Public Property"
    Public Property Tax_id() As Integer
        Get
            Return _Tax_id
        End Get
        Set(ByVal value As Integer)
            _Tax_id = value
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
    Public Property Mode() As String
        Get
            Return _Mode
        End Get
        Set(ByVal value As String)
            _Mode = value
        End Set
    End Property
    Public Property Value() As Decimal
        Get
            Return _Value
        End Get
        Set(ByVal value As Decimal)
            _Value = value
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

    Public Property Effective_Date() As System.DateTime
        Get
            Return _Effective_Date

        End Get
        Set(ByVal value As System.DateTime)
            _Effective_Date = value
        End Set
    End Property

    Public Property Created_date() As System.DateTime
        Get
            Return _Created_date

        End Get
        Set(ByVal value As System.DateTime)
            _Created_date = value
        End Set
    End Property
    Public Property Modified_date() As System.DateTime
        Get
            Return _Modified_date

        End Get
        Set(ByVal value As System.DateTime)
            _Modified_date = value
        End Set
    End Property
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            objclsTax = New clsTax()
            objclsTax.Tax_id = Tax_id
            objclsTax.cmp_id = cmp_id
            objclsTax.Name = Name
            objclsTax.Mode = Mode
            objclsTax.Value = Value
            objclsTax.Is_active = Is_active
            objclsTax.Effective_Date = Effective_Date
            objclsTax.Login_id = Login_id
            objclsTax.Ip_address = Ip_address
            objclsTax.machine_id = machine_id
            If objclsTax.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsTax = New clsTax()
            objclsTax.Tax_id = Tax_id
            objclsTax.cmp_id = cmp_id
            objclsTax.Name = Name
            objclsTax.Mode = Mode
            objclsTax.Value = Value
            objclsTax.Is_active = Is_active
            objclsTax.Effective_Date = Effective_Date
            objclsTax.Login_id = Login_id
            objclsTax.Ip_address = Ip_address
            objclsTax.machine_id = machine_id
            If objclsTax.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsTax = New clsTax()
            objclsTax.Tax_id = Tax_id
            objclsTax.cmp_id = cmp_id
            objclsTax.Name = Name
            objclsTax.Mode = Mode
            objclsTax.Value = Value
            objclsTax.Is_active = Is_active
            objclsTax.Effective_Date = Effective_Date
            objclsTax.Login_id = Login_id
            objclsTax.Ip_address = Ip_address
            objclsTax.machine_id = machine_id
            objclsTax.Tran_Type = Tran_Type
            If objclsTax.Delete() Then
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
            objclsTax = New clsTax()
            objclsTax.Tax_id = Tax_id
            objclsTax.cmp_id = cmp_id
            dt = objclsTax.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectByValue]() As DataTable
        Dim dt As DataTable
        Try
            objclsTax = New clsTax()
            objclsTax.Value = Value
            objclsTax.cmp_id = cmp_id
            dt = objclsTax.SelectByValue()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsTax = New clsTax()
            objclsTax.cmp_id = cmp_id
            dt = objclsTax.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_Tax() As DataTable
        Dim dt As DataTable
        Try
            objclsTax = New clsTax()
            objclsTax.cmp_id = cmp_id
            dt = objclsTax.Select_Tax()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectTaxByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsTax = New clsTax()
            objclsTax.cmp_id = cmp_id
            dt = objclsTax.[SelectTaxByCmp]
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsTax
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Tax_id As Integer
    Private _cmp_id As Integer
    Private _Name As String
    Private _Mode As String
    Private _Value As Decimal
    Private _Is_active As Byte
    Private _Effective_Date As DateTime
    Private _Created_date As System.DateTime
    Private _Modified_date As System.DateTime
    Private _Login_id As Integer
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private objclsTax As clsTax
#End Region

#Region "Public Property"
    Public Property Tax_id() As Integer
        Get
            Return _Tax_id
        End Get
        Set(ByVal value As Integer)
            _Tax_id = value
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
    Public Property Mode() As String
        Get
            Return _Mode
        End Get
        Set(ByVal value As String)
            _Mode = value
        End Set
    End Property
    Public Property Value() As Decimal
        Get
            Return _Value
        End Get
        Set(ByVal value As Decimal)
            _Value = value
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

    Public Property Effective_Date() As System.DateTime
        Get
            Return _Effective_Date

        End Get
        Set(ByVal value As System.DateTime)
            _Effective_Date = value
        End Set
    End Property

    Public Property Created_date() As System.DateTime
        Get
            Return _Created_date

        End Get
        Set(ByVal value As System.DateTime)
            _Created_date = value
        End Set
    End Property
    Public Property Modified_date() As System.DateTime
        Get
            Return _Modified_date

        End Get
        Set(ByVal value As System.DateTime)
            _Modified_date = value
        End Set
    End Property
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Mode", Mode)
            oColSqlparram.Add("@Value", Value, SqlDbType.Decimal)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Effective_Date", Effective_Date, SqlDbType.Date)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Tax", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Mode", Mode)
            oColSqlparram.Add("@Value", Value, SqlDbType.Decimal)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Effective_Date", Effective_Date, SqlDbType.Date)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Tax", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Mode", Mode)
            oColSqlparram.Add("@Value", Value, SqlDbType.Decimal)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Effective_Date", Effective_Date, SqlDbType.Date)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Tax", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Tax_Id", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectByValue]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@value", Value, SqlDbType.Decimal)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Tax_by_value", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Tax", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_Tax() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Tax_Max", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectTaxByCmp]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Tax_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

End Class
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsPrice
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Price_id As Integer
    Private _Price As Decimal
    Private _cmp_id As Integer
    Private _Location_Id As Integer
    Private _Size_Id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Actual_Price As Decimal
    Private _Tax As Decimal
    Private _Product_id As Integer
    Private objclsPrice As clsPrice
#End Region

#Region "Public Property"
    Public Property Product_id() As Integer
        Get
            Return _Product_id
        End Get
        Set(ByVal value As Integer)
            _Product_id = value
        End Set
    End Property

    Public Property Price_id() As Integer
        Get
            Return _Price_id
        End Get
        Set(ByVal value As Integer)
            _Price_id = value
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
    Public Property Location_id() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
        End Set
    End Property
    Public Property Size_Id() As Integer
        Get
            Return _Size_Id
        End Get
        Set(ByVal value As Integer)
            _Size_Id = value
        End Set
    End Property
    Public Property Price() As Decimal
        Get
            Return _Price
        End Get
        Set(ByVal value As Decimal)
            _Price = value
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
    Public Property Actual_Price() As Decimal
        Get
            Return _Actual_Price
        End Get
        Set(ByVal value As Decimal)
            _Actual_Price = value
        End Set
    End Property
    Public Property Tax() As Decimal
        Get
            Return _Tax
        End Get
        Set(ByVal value As Decimal)
            _Tax = value
        End Set
    End Property
#End Region
#Region "Function"
    Public Function insert() As Boolean
        Try
            objclsPrice = New clsPrice()
            objclsPrice.Price_id = Price_id
            objclsPrice.cmp_id = cmp_id
            objclsPrice.Location_id = Location_id
            objclsPrice.Size_Id = Size_Id
            objclsPrice.Price = Price
            objclsPrice.Ip_address = Ip_address
            objclsPrice.login_id = login_id
            objclsPrice.Actual_Price = Actual_Price
            objclsPrice.Tax = Tax
            objclsPrice.Product_id = Product_id
            If objclsPrice.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsPrice = New clsPrice()
            objclsPrice.Price_id = Price_id
            objclsPrice.cmp_id = cmp_id
            objclsPrice.Location_id = Location_id
            objclsPrice.Size_Id = Size_Id
            objclsPrice.Price = Price
            objclsPrice.Ip_address = Ip_address
            objclsPrice.login_id = login_id
            objclsPrice.Actual_Price = Actual_Price
            objclsPrice.Tax = Tax
            objclsPrice.Product_id = Product_id
            If objclsPrice.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsPrice = New clsPrice()
            objclsPrice.Price_id = Price_id
            objclsPrice.cmp_id = cmp_id
            objclsPrice.Location_id = Location_id
            objclsPrice.Size_Id = Size_Id
            objclsPrice.Price = Price
            objclsPrice.Ip_address = Ip_address
            objclsPrice.login_id = login_id
            objclsPrice.Tran_Type = Tran_Type
            objclsPrice.Actual_Price = Actual_Price
            objclsPrice.Tax = Tax
            objclsPrice.Product_id = Product_id
            If objclsPrice.Delete() Then
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
            objclsPrice = New clsPrice()
            objclsPrice.Price_id = Price_id
            objclsPrice.cmp_id = cmp_id
            dt = objclsPrice.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsPrice = New clsPrice()
            'objclsPrice.Price_id = Price_id
            objclsPrice.cmp_id = cmp_id
            dt = objclsPrice.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Price]() As DataTable
        Dim dt As DataTable
        Try
            objclsPrice = New clsPrice()
            objclsPrice.cmp_id = cmp_id
            dt = objclsPrice.[Select_Price]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsPrice
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"
    Private _Price_id As Integer
    Private _Price As Decimal
    Private _cmp_id As Integer
    Private _Location_Id As Integer
    Private _Size_Id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Actual_Price As Decimal
    Private _Tax As Decimal
    Private _Product_id As Integer
    Private objclsPrice As clsPrice
#End Region

#Region "Public Property"
    Public Property Product_id() As Integer
        Get
            Return _Product_id
        End Get
        Set(ByVal value As Integer)
            _Product_id = value
        End Set
    End Property

    Public Property Price_id() As Integer
        Get
            Return _Price_id
        End Get
        Set(ByVal value As Integer)
            _Price_id = value
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
    Public Property Location_id() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
        End Set
    End Property
    Public Property Size_Id() As Integer
        Get
            Return _Size_Id
        End Get
        Set(ByVal value As Integer)
            _Size_Id = value
        End Set
    End Property
    Public Property Price() As Decimal
        Get
            Return _Price
        End Get
        Set(ByVal value As Decimal)
            _Price = value
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
    Public Property Actual_Price() As Decimal
        Get
            Return _Actual_Price
        End Get
        Set(ByVal value As Decimal)
            _Actual_Price = value
        End Set
    End Property
    Public Property Tax() As Decimal
        Get
            Return _Tax
        End Get
        Set(ByVal value As Decimal)
            _Tax = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Price_id", Price_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Location_Id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_Id", Size_Id, SqlDbType.Int)
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Actual_Price", Actual_Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Tax", Tax, SqlDbType.Decimal)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Product_id", Product_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Price", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Price_id", Price_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Location_Id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_Id", Size_Id, SqlDbType.Int)
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Actual_Price", Actual_Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Tax", Tax, SqlDbType.Decimal)
            oColSqlparram.Add("@Product_id", Product_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Price", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Price_id", Price_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Location_Id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_Id", Size_Id, SqlDbType.Int)
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Actual_Price", Actual_Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Tax", Tax, SqlDbType.Decimal)
            oColSqlparram.Add("@Product_id", Product_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Price", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Price_id", Price_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Price", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Price", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Price]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Price_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function
End Class

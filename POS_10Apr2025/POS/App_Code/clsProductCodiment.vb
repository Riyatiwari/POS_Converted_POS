Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsProductCodiment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Tran_Id As Integer
    Private _Condiment_Id As Integer
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Price As Decimal
    Private _Choice As Integer
    Private _min_select As Integer
    Private _max_select As Integer


    Private objclsProductCodiment As clsProductCodiment
#End Region

#Region "Public Property"
    Public Property Tran_Id() As Integer
        Get
            Return _Tran_Id
        End Get
        Set(ByVal value As Integer)
            _Tran_Id = value
        End Set
    End Property
    Public Property Condiment_Id() As Integer
        Get
            Return _Condiment_Id
        End Get
        Set(ByVal value As Integer)
            _Condiment_Id = value
        End Set
    End Property
    Public Property product_id() As Integer
        Get
            Return _product_id
        End Get
        Set(ByVal value As Integer)
            _product_id = value
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
    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
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

    Public Property Price() As Decimal
        Get
            Return _Price
        End Get
        Set(ByVal value As Decimal)
            _Price = value
        End Set
    End Property
    Public Property Choice() As Integer
        Get
            Return _Choice
        End Get
        Set(ByVal value As Integer)
            _Choice = value
        End Set
    End Property
    Public Property min_select() As Integer
        Get
            Return _min_select
        End Get
        Set(ByVal value As Integer)
            _min_select = value
        End Set
    End Property
    Public Property max_select() As Integer
        Get
            Return _max_select
        End Get
        Set(ByVal value As Integer)
            _max_select = value
        End Set
    End Property
#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.Tran_Id = Tran_Id
            objclsProductCodiment.Condiment_Id = Condiment_Id
            objclsProductCodiment.cmp_id = cmp_id
            objclsProductCodiment.ip_address = ip_address
            objclsProductCodiment.login_id = login_id
            objclsProductCodiment.product_id = product_id
            objclsProductCodiment.Price = Price
            objclsProductCodiment.Choice = Choice
            objclsProductCodiment.min_select = min_select
            objclsProductCodiment.max_select = max_select
            If objclsProductCodiment.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.Tran_Id = Tran_Id
            objclsProductCodiment.Condiment_Id = Condiment_Id
            objclsProductCodiment.cmp_id = cmp_id
            objclsProductCodiment.ip_address = ip_address
            objclsProductCodiment.login_id = login_id
            objclsProductCodiment.product_id = product_id
            objclsProductCodiment.Price = Price
            objclsProductCodiment.Choice = Choice
            objclsProductCodiment.min_select = min_select
            objclsProductCodiment.max_select = max_select
            If objclsProductCodiment.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.Tran_Id = Tran_Id
            objclsProductCodiment.Condiment_Id = Condiment_Id
            objclsProductCodiment.cmp_id = cmp_id
            objclsProductCodiment.ip_address = ip_address
            objclsProductCodiment.login_id = login_id
            objclsProductCodiment.product_id = product_id
            objclsProductCodiment.Price = Price
            objclsProductCodiment.Choice = Choice
            objclsProductCodiment.min_select = min_select
            objclsProductCodiment.max_select = max_select
            If objclsProductCodiment.Delete() Then
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
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.Condiment_Id = Condiment_Id
            objclsProductCodiment.cmp_id = cmp_id
            dt = objclsProductCodiment.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.cmp_id = cmp_id
            dt = objclsProductCodiment.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectCondiment]() As DataTable
        Dim dt As DataTable
        Try
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.product_id = product_id
            objclsProductCodiment.cmp_id = cmp_id
            dt = objclsProductCodiment.[SelectCondiment]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectCondiment_for_copy]() As DataTable
        Dim dt As DataTable
        Try
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.product_id = product_id
            objclsProductCodiment.cmp_id = cmp_id
            dt = objclsProductCodiment.SelectCondiment_for_copy()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function DeleteProductCondiment_By_Product() As Boolean
        Try
            objclsProductCodiment = New clsProductCodiment()
            objclsProductCodiment.cmp_id = cmp_id
            objclsProductCodiment.product_id = product_id
            objclsProductCodiment.Condiment_Id = Condiment_Id
            If objclsProductCodiment.DeleteProductCondiment_By_Product() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

End Class
Public Class clsProductCodiment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Tran_Id As Integer
    Private _Condiment_Id As Integer
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Price As Decimal
    Private _Choice As Integer
    Private _min_select As Integer
    Private _max_select As Integer
    Private objclsProductCodiment As clsProductCodiment
#End Region

#Region "Public Property"
    Public Property Tran_Id() As Integer
        Get
            Return _Tran_Id
        End Get
        Set(ByVal value As Integer)
            _Tran_Id = value
        End Set
    End Property
    Public Property Condiment_Id() As Integer
        Get
            Return _Condiment_Id
        End Get
        Set(ByVal value As Integer)
            _Condiment_Id = value
        End Set
    End Property
    Public Property product_id() As Integer
        Get
            Return _product_id
        End Get
        Set(ByVal value As Integer)
            _product_id = value
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
    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
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

    Public Property Price() As Decimal
        Get
            Return _Price
        End Get
        Set(ByVal value As Decimal)
            _Price = value
        End Set
    End Property
    Public Property Choice() As Integer
        Get
            Return _Choice
        End Get
        Set(ByVal value As Integer)
            _Choice = value
        End Set
    End Property
    Public Property min_select() As Integer
        Get
            Return _min_select
        End Get
        Set(ByVal value As Integer)
            _min_select = value
        End Set
    End Property
    Public Property max_select() As Integer
        Get
            Return _max_select
        End Get
        Set(ByVal value As Integer)
            _max_select = value
        End Set
    End Property
#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_Id", Tran_Id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Choice", Choice, SqlDbType.Int)
            oColSqlparram.Add("@min_select", min_select, SqlDbType.Int)
            oColSqlparram.Add("@max_select", max_select, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Condiment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_Id", Tran_Id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Choice", Choice, SqlDbType.Int)
            oColSqlparram.Add("@min_select", min_select, SqlDbType.Int)
            oColSqlparram.Add("@max_select", max_select, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Condiment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_Id", Tran_Id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Choice", Choice, SqlDbType.Int)
            oColSqlparram.Add("@min_select", min_select, SqlDbType.Int)
            oColSqlparram.Add("@max_select", max_select, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Condiment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Condiment", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Condiment", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectCondiment]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_Condiment_List", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectCondiment_for_copy]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_Condiment_List", oColSqlparram)

        Return dtlogin
    End Function

    Public Function DeleteProductCondiment_By_Product() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment_id", Condiment_Id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Condimentt_By_Product", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region


End Class

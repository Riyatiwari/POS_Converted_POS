Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsProductIngredient
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Tran_Id As Integer
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _Ingredient_id As Integer
    Private _Size_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Price As Decimal
    Private _Qty As Decimal
    Private _Size As String

    Private objclsProductIngredient As clsProductIngredient
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
    Public Property Size_id() As Integer
        Get
            Return _Size_id
        End Get
        Set(ByVal value As Integer)
            _Size_id = value
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
    Public Property Ingredient_id() As Integer
        Get
            Return _Ingredient_id
        End Get
        Set(ByVal value As Integer)
            _Ingredient_id = value
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

    Public Property Qty() As Decimal
        Get
            Return _Qty
        End Get
        Set(ByVal value As Decimal)
            _Qty = value
        End Set
    End Property

    Public Property Size() As String
        Get
            Return _Size
        End Get
        Set(ByVal value As String)
            _Size = value
        End Set
    End Property

#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            objclsProductIngredient = New clsProductIngredient()
            objclsProductIngredient.Tran_Id = Tran_Id
            objclsProductIngredient.cmp_id = cmp_id
            objclsProductIngredient.Ingredient_id = Ingredient_id
            objclsProductIngredient.Size_id = Size_id
            objclsProductIngredient.Ip_address = Ip_address
            objclsProductIngredient.login_id = login_id
            objclsProductIngredient.product_id = product_id
            objclsProductIngredient.Price = Price
            objclsProductIngredient.Qty = Qty
            If objclsProductIngredient.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsProductIngredient = New clsProductIngredient()
            objclsProductIngredient.Tran_Id = Tran_Id
            objclsProductIngredient.cmp_id = cmp_id
            objclsProductIngredient.Ingredient_id = Ingredient_id
            objclsProductIngredient.Size_id = Size_id
            objclsProductIngredient.Ip_address = Ip_address
            objclsProductIngredient.login_id = login_id
            objclsProductIngredient.product_id = product_id
            objclsProductIngredient.Price = Price
            objclsProductIngredient.Qty = Qty
            If objclsProductIngredient.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsProductIngredient = New clsProductIngredient()
            objclsProductIngredient.Tran_Id = Tran_Id
            objclsProductIngredient.cmp_id = cmp_id
            objclsProductIngredient.Ingredient_id = Ingredient_id
            objclsProductIngredient.Size_id = Size_id
            objclsProductIngredient.Ip_address = Ip_address
            objclsProductIngredient.login_id = login_id
            objclsProductIngredient.product_id = product_id
            objclsProductIngredient.Price = Price
            objclsProductIngredient.Qty = Qty
            If objclsProductIngredient.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function DeleteProductIngredient_By_Product() As Boolean
        Try
            objclsProductIngredient = New clsProductIngredient()
            objclsProductIngredient.cmp_id = cmp_id
            objclsProductIngredient.product_id = product_id
            objclsProductIngredient.Size_id = Size_id
            If objclsProductIngredient.DeleteProductIngredient_By_Product() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [GetSize_By_Ingredient]() As DataTable
        Dim dt As DataTable
        Try
            objclsProductIngredient = New clsProductIngredient()
            objclsProductIngredient.cmp_id = cmp_id
            objclsProductIngredient.Ingredient_id = Ingredient_id
            objclsProductIngredient.Size = Size
            dt = objclsProductIngredient.[GetSize_By_Ingredient]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    'Public Function [Select]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsProductIngredient = New clsProductIngredient()
    '        objclsProductIngredient.Tran_Id = Tran_Id
    '        objclsProductIngredient.cmp_id = cmp_id
    '        dt = objclsProductIngredient.[Select]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    'Public Function [SelectAll]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsProductIngredient = New clsProductIngredient()
    '        objclsProductIngredient.cmp_id = cmp_id
    '        dt = objclsProductIngredient.[SelectAll]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
#End Region

End Class
Public Class clsProductIngredient
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


#Region "Public Variables"
    Private _Tran_Id As Integer
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _Ingredient_id As Integer
    Private _Size_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Price As Decimal
    Private _Qty As Decimal
    Private _Size As String

    Private objclsProductIngredient As clsProductIngredient
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
    Public Property Size_id() As Integer
        Get
            Return _Size_id
        End Get
        Set(ByVal value As Integer)
            _Size_id = value
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
    Public Property Ingredient_id() As Integer
        Get
            Return _Ingredient_id
        End Get
        Set(ByVal value As Integer)
            _Ingredient_id = value
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
    Public Property Qty() As Decimal
        Get
            Return _Qty
        End Get
        Set(ByVal value As Decimal)
            _Qty = value
        End Set
    End Property
    Public Property Size() As String
        Get
            Return _Size
        End Get
        Set(ByVal value As String)
            _Size = value
        End Set
    End Property

#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_Id", Tran_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Ingredient_id", Ingredient_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Qty", Qty, SqlDbType.Decimal)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Ingredient", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_Id", Tran_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Ingredient_id", Ingredient_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Qty", Qty, SqlDbType.Decimal)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Ingredient", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_Id", Tran_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Ingredient_id", Ingredient_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@Price", Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Qty", Qty, SqlDbType.Decimal)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Ingredient", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function DeleteProductIngredient_By_Product() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Ingredient_By_Product", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [GetSize_By_Ingredient]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Ingredient_id", Ingredient_id, SqlDbType.Int)
        oColSqlparram.Add("@Size", Size)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Size_By_Ingredient", oColSqlparram)

        Return dtlogin
    End Function


#End Region

End Class

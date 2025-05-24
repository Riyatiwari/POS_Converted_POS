Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsSize
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Size_id As Integer
    Private _cmp_id As Integer
    Private _Size As String
    Private _product_id As Integer
    Private _Unit_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Name As String
    Private _is_active As Byte

    Private _Price_id As Integer
    Private _Price As Decimal
    Private _Location_Id As Integer
    Private _Actual_Price As Decimal
    Private _Tax As Decimal
    Private _action As String
    Private _Tax_id As Decimal

    Private _click_and_collect As Byte
    Private _deliver As Byte
    Private _Order_at_table As Byte
    Private _Product_Price_Id As Integer

    Private _FromlevelID As Integer
    Private _TolevelID As Integer
    Private _PriceType As Integer
    Private _Prc_Value As Decimal

    Private _Tax2 As Decimal
    Private _Tax_id2 As Decimal
    Private _sorting_no As Integer

    Private _Cost_ID As Integer
    Private _cost As Decimal

    Private objclsSize As clsSize
#End Region

#Region "Public Property"

    Public Property Cost_ID() As Integer
        Get
            Return _Cost_ID
        End Get
        Set(ByVal value As Integer)
            _Cost_ID = value
        End Set
    End Property

    Public Property cost() As Decimal
        Get
            Return _cost
        End Get
        Set(ByVal value As Decimal)
            _cost = value
        End Set
    End Property

    Public Property FromlevelID() As Integer
        Get
            Return _FromlevelID
        End Get
        Set(ByVal value As Integer)
            _FromlevelID = value
        End Set
    End Property
    Public Property TolevelID() As Integer
        Get
            Return _TolevelID
        End Get
        Set(ByVal value As Integer)
            _TolevelID = value
        End Set
    End Property
    Public Property PriceType() As Integer
        Get
            Return _PriceType
        End Get
        Set(ByVal value As Integer)
            _PriceType = value
        End Set
    End Property
    Public Property Prc_Value() As Decimal
        Get
            Return _Prc_Value
        End Get
        Set(ByVal value As Decimal)
            _Prc_Value = value
        End Set
    End Property

    Public Property is_active() As Integer
        Get
            Return _is_active
        End Get
        Set(ByVal value As Integer)
            _is_active = value
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
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
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
    Public Property product_id() As Integer
        Get
            Return _product_id
        End Get
        Set(ByVal value As Integer)
            _product_id = value
        End Set
    End Property
    Public Property Unit_id() As Integer
        Get
            Return _Unit_id
        End Get
        Set(ByVal value As Integer)
            _Unit_id = value
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

    Public Property Price_id() As Integer
        Get
            Return _Price_id
        End Get
        Set(ByVal value As Integer)
            _Price_id = value
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
    Public Property Location_Id() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
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
    Public Property action() As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property
    Public Property Tax_id() As Decimal
        Get
            Return _Tax_id
        End Get
        Set(ByVal value As Decimal)
            _Tax_id = value
        End Set
    End Property

    Public Property Tax2() As Decimal
        Get
            Return _Tax2
        End Get
        Set(ByVal value As Decimal)
            _Tax2 = value
        End Set
    End Property

    Public Property Tax_id2() As Decimal
        Get
            Return _Tax_id2
        End Get
        Set(ByVal value As Decimal)
            _Tax_id2 = value
        End Set
    End Property

    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
        End Set
    End Property


    Public Property click_and_collect() As Byte
        Get
            Return _click_and_collect
        End Get
        Set(ByVal value As Byte)
            _click_and_collect = value
        End Set
    End Property
    Public Property deliver() As Byte
        Get
            Return _deliver
        End Get
        Set(ByVal value As Byte)
            _deliver = value
        End Set
    End Property
    Public Property Order_at_table() As Byte
        Get
            Return _Order_at_table
        End Get
        Set(ByVal value As Byte)
            _Order_at_table = value
        End Set
    End Property

    Public Property Product_Price_Id() As Integer
        Get
            Return _Product_Price_Id
        End Get
        Set(ByVal value As Integer)
            _Product_Price_Id = value
        End Set
    End Property
#End Region
#Region "Function"
    Public Function insert() As Integer

        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            objclsSize.Size = Size
            objclsSize.product_id = product_id
            objclsSize.Unit_id = Unit_id
            objclsSize.Ip_address = Ip_address
            objclsSize.login_id = login_id
            objclsSize.Name = Name
            objclsSize.is_active = is_active
            'If objclsSize.Insert() Then
            '    Return True
            'End If
            'Return False
            Dim r As Integer
            r = objclsSize.Insert()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            objclsSize.Size = Size
            objclsSize.product_id = product_id
            objclsSize.Unit_id = Unit_id
            objclsSize.Ip_address = Ip_address
            objclsSize.login_id = login_id
            objclsSize.Name = Name
            objclsSize.is_active = is_active
            If objclsSize.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            objclsSize.Size = Size
            objclsSize.product_id = product_id
            objclsSize.Unit_id = Unit_id
            objclsSize.Ip_address = Ip_address
            objclsSize.login_id = login_id
            objclsSize.Name = Name
            objclsSize.is_active = is_active
            If objclsSize.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Del_SizeNPrice() As Boolean
        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            objclsSize.Product_Price_Id = Product_Price_Id
            objclsSize.Price_id = Price_id

            If objclsSize.Del_SizeNPrice() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Del_BuyingCost() As Boolean
        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            If objclsSize.Del_BuyingCost() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsUpdDel_SizeNPrice() As Integer

        Try
            objclsSize = New clsSize()
            objclsSize.product_id = product_id
            objclsSize.Size_id = Size_id
            objclsSize.Name = Name
            objclsSize.Size = Size
            objclsSize.Unit_id = Unit_id
            objclsSize.is_active = is_active

            objclsSize.Location_Id = Location_Id
            objclsSize.Price_id = Price_id
            objclsSize.Price = Price
            objclsSize.Actual_Price = Actual_Price
            objclsSize.Tax = Tax
            objclsSize.Tax_id = Tax_id

            objclsSize.Tax2 = Tax2
            objclsSize.Tax_id2 = Tax_id2

            objclsSize.cmp_id = cmp_id
            objclsSize.login_id = login_id
            objclsSize.Ip_address = Ip_address
            objclsSize.action = action

            objclsSize.click_and_collect = click_and_collect
            objclsSize.deliver = deliver
            objclsSize.Order_at_table = Order_at_table
            objclsSize.Product_Price_Id = Product_Price_Id
            objclsSize.sorting_no = sorting_no

            Dim r As Integer
            r = objclsSize.InsUpdDel_SizeNPrice()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Copy_SizeNPrice() As Integer

        Try
            objclsSize = New clsSize()
            objclsSize.product_id = product_id
            objclsSize.FromlevelID = FromlevelID
            objclsSize.TolevelID = TolevelID
            objclsSize.PriceType = PriceType
            objclsSize.Prc_Value = Prc_Value
            objclsSize.cmp_id = cmp_id

            Dim r As Integer
            r = objclsSize.Copy_SizeNPrice()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function InsUpdDel_SizeNCost() As Integer

        Try
            objclsSize = New clsSize()
            objclsSize.product_id = product_id
            objclsSize.Size_id = Size_id
            objclsSize.Name = Name
            objclsSize.Size = Size
            objclsSize.Unit_id = Unit_id
            objclsSize.is_active = is_active

            objclsSize.Location_Id = Location_Id
            objclsSize.Price_id = Price_id
            objclsSize.Price = Price
            objclsSize.Actual_Price = Actual_Price
            objclsSize.Tax = Tax
            objclsSize.Tax_id = Tax_id

            objclsSize.Tax2 = Tax2
            objclsSize.Tax_id2 = Tax_id2

            objclsSize.cmp_id = cmp_id
            objclsSize.login_id = login_id
            objclsSize.Ip_address = Ip_address
            objclsSize.action = action

            Dim r As Integer
            r = objclsSize.InsUpdDel_SizeNCost()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            dt = objclsSize.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsSize = New clsSize()
            'objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            dt = objclsSize.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectProductBySize]() As DataTable
        Dim dt As DataTable
        Try
            objclsSize = New clsSize()
            objclsSize.product_id = product_id
            objclsSize.cmp_id = cmp_id
            dt = objclsSize.[SelectProductBySize]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_SizeNPrice]() As DataTable
        Dim dt As DataTable
        Try
            objclsSize = New clsSize()
            objclsSize.product_id = product_id
            objclsSize.FromlevelID = FromlevelID
            objclsSize.cmp_id = cmp_id
            dt = objclsSize.[Select_SizeNPrice]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectSizeUnitByProduct]() As DataTable
        Dim dt As DataTable
        Try
            objclsSize = New clsSize()
            objclsSize.product_id = product_id
            objclsSize.cmp_id = cmp_id
            dt = objclsSize.[SelectSizeUnitByProduct]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectSizeUnitByProductandLocation]() As DataTable
        Dim dt As DataTable
        Try
            objclsSize = New clsSize()
            objclsSize.product_id = product_id
            objclsSize.cmp_id = cmp_id
            objclsSize.Location_Id = Location_Id
            dt = objclsSize.[SelectSizeUnitByProductandLocation]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateActiveInactive() As Boolean
        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            objclsSize.is_active = is_active
            If objclsSize.UpdateActiveInactive() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateBuyingCost() As Boolean
        Try
            objclsSize = New clsSize()
            objclsSize.Cost_ID = Cost_ID
            objclsSize.cost = cost
            objclsSize.cmp_id = cmp_id
            If objclsSize.UpdateBuyingCost() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [Select_Size]() As DataTable
        Dim dt As DataTable
        Try
            objclsSize = New clsSize()
            objclsSize.Size_id = Size_id
            objclsSize.cmp_id = cmp_id
            dt = objclsSize.[Select_Size]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsSize
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _Size_id As Integer
    Private _cmp_id As Integer
    Private _Size As String
    Private _product_id As Integer
    Private _Unit_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Name As String
    Private _is_active As Byte

    Private _Price_id As Integer
    Private _Price As Decimal
    Private _Location_Id As Integer
    Private _Actual_Price As Decimal
    Private _Tax As Decimal
    Private _action As String
    Private _Tax_id As Decimal

    Private _click_and_collect As Byte
    Private _deliver As Byte
    Private _Order_at_table As Byte
    Private _Product_Price_Id As Integer

    Private _FromlevelID As Integer
    Private _TolevelID As Integer
    Private _PriceType As Integer
    Private _Prc_Value As Decimal

    Private _Tax2 As Decimal
    Private _Tax_id2 As Decimal
    Private _sorting_no As Integer

    Private _Cost_ID As Integer
    Private _cost As Decimal


    Private objclsSize As clsSize
#End Region

#Region "Public Property"

    Public Property Cost_ID() As Integer
        Get
            Return _Cost_ID
        End Get
        Set(ByVal value As Integer)
            _Cost_ID = value
        End Set
    End Property

    Public Property cost() As Decimal
        Get
            Return _cost
        End Get
        Set(ByVal value As Decimal)
            _cost = value
        End Set
    End Property


    Public Property FromlevelID() As Integer
        Get
            Return _FromlevelID
        End Get
        Set(ByVal value As Integer)
            _FromlevelID = value
        End Set
    End Property
    Public Property TolevelID() As Integer
        Get
            Return _TolevelID
        End Get
        Set(ByVal value As Integer)
            _TolevelID = value
        End Set
    End Property
    Public Property PriceType() As Integer
        Get
            Return _PriceType
        End Get
        Set(ByVal value As Integer)
            _PriceType = value
        End Set
    End Property
    Public Property Prc_Value() As Decimal
        Get
            Return _Prc_Value
        End Get
        Set(ByVal value As Decimal)
            _Prc_Value = value
        End Set
    End Property


    Public Property is_active() As Integer
        Get
            Return _is_active
        End Get
        Set(ByVal value As Integer)
            _is_active = value
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
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
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
    Public Property product_id() As Integer
        Get
            Return _product_id
        End Get
        Set(ByVal value As Integer)
            _product_id = value
        End Set
    End Property
    Public Property Unit_id() As Integer
        Get
            Return _Unit_id
        End Get
        Set(ByVal value As Integer)
            _Unit_id = value
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

    Public Property Price_id() As Integer
        Get
            Return _Price_id
        End Get
        Set(ByVal value As Integer)
            _Price_id = value
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
    Public Property Location_Id() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
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
    Public Property action() As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property
    Public Property Tax_id() As Decimal
        Get
            Return _Tax_id
        End Get
        Set(ByVal value As Decimal)
            _Tax_id = value
        End Set
    End Property

    Public Property Tax2() As Decimal
        Get
            Return _Tax2
        End Get
        Set(ByVal value As Decimal)
            _Tax2 = value
        End Set
    End Property

    Public Property Tax_id2() As Decimal
        Get
            Return _Tax_id2
        End Get
        Set(ByVal value As Decimal)
            _Tax_id2 = value
        End Set
    End Property

    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
        End Set
    End Property


    Public Property click_and_collect() As Byte
        Get
            Return _click_and_collect
        End Get
        Set(ByVal value As Byte)
            _click_and_collect = value
        End Set
    End Property
    Public Property deliver() As Byte
        Get
            Return _deliver
        End Get
        Set(ByVal value As Byte)
            _deliver = value
        End Set
    End Property
    Public Property Order_at_table() As Byte
        Get
            Return _Order_at_table
        End Get
        Set(ByVal value As Byte)
            _Order_at_table = value
        End Set
    End Property

    Public Property Product_Price_Id() As Integer
        Get
            Return _Product_Price_Id
        End Get
        Set(ByVal value As Integer)
            _Product_Price_Id = value
        End Set
    End Property

#End Region
    Public Function Insert() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@Size", Size)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Size", oColSqlparram)
            'Return True
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Size", oColSqlparram)
            Return dtlogin.Rows(0)("Size_Id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@Size", Size)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Size", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@Size", Size)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Size", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Del_SizeNPrice() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
            oColSqlparram.Add("@Price_Id", Price_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Delete_M_Size", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Del_BuyingCost() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Delete_M_Size_N_Cost", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function InsUpdDel_SizeNPrice() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Size", Size)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)

            oColSqlparram.Add("@Location_Id", Location_Id, SqlDbType.Int)
            oColSqlparram.Add("@Price_id", Price_id, SqlDbType.Int)
            oColSqlparram.Add("@Price", Price)
            oColSqlparram.Add("@Actual_Price", Actual_Price)
            oColSqlparram.Add("@Tax", Tax)
            oColSqlparram.Add("@Tax_id", Tax_id)

            oColSqlparram.Add("@Tax2", Tax2)
            oColSqlparram.Add("@Tax_id2", Tax_id2)

            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@action", action)

            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.TinyInt)
            oColSqlparram.Add("@deliver", deliver, SqlDbType.TinyInt)
            oColSqlparram.Add("@Order_at_table", Order_at_table, SqlDbType.TinyInt)
            oColSqlparram.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
            oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Size_N_Price", oColSqlparram)
            Return dtlogin.Rows(0)("Size_Id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Copy_SizeNPrice() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@FromlevelID", FromlevelID, SqlDbType.Int)
            oColSqlparram.Add("@TolevelID", TolevelID, SqlDbType.Int)
            oColSqlparram.Add("@PriceType", PriceType, SqlDbType.Int)
            oColSqlparram.Add("@Prc_Value", Prc_Value, SqlDbType.Decimal)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Copy_price_level", oColSqlparram)

            Return dtlogin.Rows(0)("Size_Id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsUpdDel_SizeNCost() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Size", Size)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)

            oColSqlparram.Add("@Location_Id", Location_Id, SqlDbType.Int)
            oColSqlparram.Add("@Price_id", Price_id, SqlDbType.Int)
            oColSqlparram.Add("@Price", Price)
            oColSqlparram.Add("@Actual_Price", Actual_Price)
            oColSqlparram.Add("@Tax", Tax)
            oColSqlparram.Add("@Tax_id", Tax_id)

            oColSqlparram.Add("@Tax2", Tax2)
            oColSqlparram.Add("@Tax_id2", Tax_id2)

            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@action", action)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Size_N_Cost", oColSqlparram)
            Return dtlogin.Rows(0)("Size_Id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Size", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Size", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectProductBySize]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Product_id", product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_By_Size", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_SizeNPrice]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@FromlevelID", FromlevelID, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_SizeNPrice", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectSizeUnitByProduct]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Product_id", product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_By_Size", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectSizeUnitByProductandLocation]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_Id", Location_Id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_By_Size_Location", oColSqlparram)

        Return dtlogin
    End Function

    Public Function UpdateActiveInactive() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Size_Update", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateBuyingCost() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Cost_ID", Cost_ID, SqlDbType.Int)
            oColSqlparram.Add("@cost", cost, SqlDbType.Decimal)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Buying_Cost_Update", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Size]() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Size", oColSqlparram)
            Return dtlogin

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


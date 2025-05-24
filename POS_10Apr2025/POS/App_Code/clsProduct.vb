Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsProduct
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _row_id As Integer

    Private _Category_id As Integer
    Private _key_map_id As Integer
    Private _code As String
    Private _name As String
    Private _price As Decimal
    Private _barcode As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _login_id As Integer
    Private _department_id As Integer
    Private _course_id As Integer
    Private _list As Integer
    Private _printer_id As String
    Private _Actual_Price As Decimal
    Private _Tax_id As Integer
    Private _Tax As String
    Private _machine_id As Integer
    Private _other_id As String
    Private _other_size As String
    Private _SortingNo As Decimal
    Private _Tran_Type As String
    Private _Is_Ingredient As Byte
    Private _Is_Condiment As Byte
    Private _Base As Integer
    Private _Unit_id As Integer
    Private _SingleRecord As Integer
    Private _size_zero As Byte
    Private _GroupName As String
    Private _UnitName As String
    Private _Pro_Id As String
    Private _Location_id As Integer
    Private _allergy_id As Integer

    Private _P_Image As Byte()
    Private _is_stock As Byte
    Private _Product_Price_Id As Integer
    Private _cat_id As Integer
    Private _active As Integer
    Private _Del_Status As String
    Private _price_Id As Integer
    Private _size_Id As Integer
    Private _SizeName As String
    Private _size As Decimal
    Private _click_and_collect As Integer
    Private _deliver As Integer
    Private _Order_at_table As Integer
    Private _is_web_available As Integer
    Private _Cloak_Room As Byte
    Private _is_DanceVoucher As Integer
    Private _Is_PriceOnScaleWeight As Byte
    Private _IsHouse As Byte
    Private _IsPkgProduct As Byte
    Private _MainCategoryName As String
    Private _is_unflag_printer As Integer
    Private _department_name As String
    Private _Printer_name1 As String
    Private _Printer_name2 As String
    Private _Printer_name3 As String
    Private _size1name As String
    Private _size1price As Decimal
    Private _size1 As Decimal
    Private _size1unit As String
    Private _size2name As String
    Private _size2price As Decimal
    Private _size2 As Decimal
    Private _size2unit As String
    Private _size3name As String
    Private _size3price As Decimal
    Private _size3 As Decimal
    Private _size3unit As String
    Private _size4name As String
    Private _size4price As Decimal
    Private _size4 As Decimal
    Private _size4unit As String
    Private _IsAdditionalTax As Byte
    Private _ForKiosk As Byte
    Private _Is_OutOfStock As Byte

    Private _barcode1 As String
    Private _barcode2 As String
    Private _barcode3 As String
    Private _barcode4 As String

    Private objclsProduct As clsProduct
    Private objclsCategory As clsCategory
#End Region

#Region "Public Property"

    Public Property barcode1() As String
        Get
            Return _barcode1
        End Get
        Set(ByVal value As String)
            _barcode1 = value
        End Set
    End Property
    Public Property barcode2() As String
        Get
            Return _barcode2
        End Get
        Set(ByVal value As String)
            _barcode2 = value
        End Set
    End Property
    Public Property barcode3() As String
        Get
            Return _barcode3
        End Get
        Set(ByVal value As String)
            _barcode3 = value
        End Set
    End Property
    Public Property barcode4() As String
        Get
            Return _barcode4
        End Get
        Set(ByVal value As String)
            _barcode4 = value
        End Set
    End Property

    Public Property department_name() As String
        Get
            Return _department_name
        End Get
        Set(ByVal value As String)
            _department_name = value
        End Set
    End Property

    Public Property Printer_name1() As String
        Get
            Return _Printer_name1
        End Get
        Set(ByVal value As String)
            _Printer_name1 = value
        End Set
    End Property
    Public Property Printer_name2() As String
        Get
            Return _Printer_name2
        End Get
        Set(ByVal value As String)
            _Printer_name2 = value
        End Set
    End Property
    Public Property Printer_name3() As String
        Get
            Return _Printer_name3
        End Get
        Set(ByVal value As String)
            _Printer_name3 = value
        End Set
    End Property

    Public Property size1name() As String
        Get
            Return _size1name
        End Get
        Set(ByVal value As String)
            _size1name = value
        End Set
    End Property
    Public Property size2name() As String
        Get
            Return _size2name
        End Get
        Set(ByVal value As String)
            _size2name = value
        End Set
    End Property
    Public Property size3name() As String
        Get
            Return _size3name
        End Get
        Set(ByVal value As String)
            _size3name = value
        End Set
    End Property
    Public Property size4name() As String
        Get
            Return _size4name
        End Get
        Set(ByVal value As String)
            _size4name = value
        End Set
    End Property

    Public Property size1() As Decimal
        Get
            Return _size1
        End Get
        Set(ByVal value As Decimal)
            _size1 = value
        End Set
    End Property
    Public Property size2() As Decimal
        Get
            Return _size2
        End Get
        Set(ByVal value As Decimal)
            _size2 = value
        End Set
    End Property
    Public Property size3() As Decimal
        Get
            Return _size3
        End Get
        Set(ByVal value As Decimal)
            _size3 = value
        End Set
    End Property
    Public Property size4() As Decimal
        Get
            Return _size4
        End Get
        Set(ByVal value As Decimal)
            _size4 = value
        End Set
    End Property

    Public Property size1unit() As String
        Get
            Return _size1unit
        End Get
        Set(ByVal value As String)
            _size1unit = value
        End Set
    End Property
    Public Property size2unit() As String
        Get
            Return _size2unit
        End Get
        Set(ByVal value As String)
            _size2unit = value
        End Set
    End Property
    Public Property size3unit() As String
        Get
            Return _size3unit
        End Get
        Set(ByVal value As String)
            _size3unit = value
        End Set
    End Property
    Public Property size4unit() As String
        Get
            Return _size4unit
        End Get
        Set(ByVal value As String)
            _size4unit = value
        End Set
    End Property


    Public Property size1price() As Decimal
        Get
            Return _size1price
        End Get
        Set(ByVal value As Decimal)
            _size1price = value
        End Set
    End Property
    Public Property size2price() As Decimal
        Get
            Return _size2price
        End Get
        Set(ByVal value As Decimal)
            _size2price = value
        End Set
    End Property
    Public Property size3price() As Decimal
        Get
            Return _size3price
        End Get
        Set(ByVal value As Decimal)
            _size3price = value
        End Set
    End Property
    Public Property size4price() As Decimal
        Get
            Return _size4price
        End Get
        Set(ByVal value As Decimal)
            _size4price = value
        End Set
    End Property


    Public Property is_unflag_printer() As Integer
        Get
            Return _is_unflag_printer
        End Get
        Set(ByVal value As Integer)
            _is_unflag_printer = value
        End Set
    End Property
    Public Property MainCategoryName() As String
        Get
            Return _MainCategoryName
        End Get
        Set(ByVal value As String)
            _MainCategoryName = value
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
    Public Property allergy_id() As Integer
        Get
            Return _allergy_id
        End Get
        Set(ByVal value As Integer)
            _allergy_id = value
        End Set
    End Property
    Public Property Category_id() As Integer
        Get
            Return _Category_id
        End Get
        Set(ByVal value As Integer)
            _Category_id = value
        End Set
    End Property
    Public Property P_Image() As Byte()
        Get
            Return _P_Image
        End Get
        Set(value As Byte())
            _P_Image = value
        End Set
    End Property
    Public Property key_map_id() As Integer
        Get
            Return _key_map_id
        End Get
        Set(ByVal value As Integer)
            _key_map_id = value
        End Set
    End Property
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
    Public Property row_id() As Integer
        Get
            Return _row_id
        End Get
        Set(ByVal value As Integer)
            _row_id = value
        End Set
    End Property
    Public Property code() As String
        Get
            Return _code
        End Get
        Set(ByVal value As String)
            _code = value
        End Set
    End Property
    Public Property Tax() As String
        Get
            Return _Tax
        End Get
        Set(ByVal value As String)
            _Tax = value
        End Set
    End Property
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
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
    Public Property price() As Decimal
        Get
            Return _price
        End Get
        Set(ByVal value As Decimal)
            _price = value
        End Set
    End Property
    Public Property barcode() As String
        Get
            Return _barcode
        End Get
        Set(ByVal value As String)
            _barcode = value
        End Set
    End Property
    Public Property description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property created_date() As System.DateTime
        Get
            Return _created_date

        End Get
        Set(ByVal value As System.DateTime)
            _created_date = value
        End Set
    End Property
    Public Property modify_date() As System.DateTime
        Get
            Return _modify_date

        End Get
        Set(ByVal value As System.DateTime)
            _modify_date = value
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
    Public Property department_id() As Integer
        Get
            Return _department_id
        End Get
        Set(ByVal value As Integer)
            _department_id = value
        End Set
    End Property
    Public Property course_id() As Integer
        Get
            Return _course_id
        End Get
        Set(ByVal value As Integer)
            _course_id = value
        End Set
    End Property
    Public Property list() As Integer
        Get
            Return _list
        End Get
        Set(ByVal value As Integer)
            _list = value
        End Set
    End Property
    Public Property printer_id() As String
        Get
            Return _printer_id
        End Get
        Set(ByVal value As String)
            _printer_id = value
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
    Public Property other_id() As String
        Get
            Return _other_id
        End Get
        Set(ByVal value As String)
            _other_id = value
        End Set
    End Property
    Public Property other_size() As String
        Get
            Return _other_size
        End Get
        Set(ByVal value As String)
            _other_size = value
        End Set
    End Property
    Public Property SortingNo() As Decimal
        Get
            Return _SortingNo
        End Get
        Set(ByVal value As Decimal)
            _SortingNo = value
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
    Public Property Is_Ingredient() As Byte
        Get
            Return _Is_Ingredient
        End Get
        Set(ByVal value As Byte)
            _Is_Ingredient = value
        End Set
    End Property
    Public Property Is_Condiment() As Byte
        Get
            Return _Is_Condiment
        End Get
        Set(ByVal value As Byte)
            _Is_Condiment = value
        End Set
    End Property

    Public Property Cloak_Room() As Byte
        Get
            Return _Cloak_Room
        End Get
        Set(ByVal value As Byte)
            _Cloak_Room = value
        End Set
    End Property

    Public Property IsPkgProduct() As Byte
        Get
            Return _IsPkgProduct
        End Get
        Set(ByVal value As Byte)
            _IsPkgProduct = value
        End Set
    End Property

    Public Property ForKiosk() As Byte
        Get
            Return _ForKiosk
        End Get
        Set(ByVal value As Byte)
            _ForKiosk = value
        End Set
    End Property

    Public Property Is_OutOfStock() As Byte
        Get
            Return _Is_OutOfStock
        End Get
        Set(ByVal value As Byte)
            _Is_OutOfStock = value
        End Set
    End Property

    Public Property IsAdditionalTax() As Byte
        Get
            Return _IsAdditionalTax
        End Get
        Set(ByVal value As Byte)
            _IsAdditionalTax = value
        End Set
    End Property


    Public Property IsHouse() As Byte
        Get
            Return _IsHouse
        End Get
        Set(ByVal value As Byte)
            _IsHouse = value
        End Set
    End Property

    Public Property Is_PriceOnScaleWeight() As Byte
        Get
            Return _Is_PriceOnScaleWeight
        End Get
        Set(ByVal value As Byte)
            _Is_PriceOnScaleWeight = value
        End Set
    End Property

    Public Property is_DanceVoucher() As Integer
        Get
            Return _is_DanceVoucher
        End Get
        Set(ByVal value As Integer)
            _is_DanceVoucher = value
        End Set
    End Property


    Public Property Base() As Integer
        Get
            Return _Base
        End Get
        Set(ByVal value As Integer)
            _Base = value
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
    Public Property SingleRecord() As Integer
        Get
            Return _SingleRecord
        End Get
        Set(ByVal value As Integer)
            _SingleRecord = value
        End Set
    End Property



    Public Property size_zero() As Byte
        Get
            Return _size_zero
        End Get
        Set(ByVal value As Byte)
            _size_zero = value
        End Set
    End Property

    Public Property GroupName() As String
        Get
            Return _GroupName
        End Get
        Set(ByVal value As String)
            _GroupName = value
        End Set
    End Property

    Public Property UnitName() As String
        Get
            Return _UnitName
        End Get
        Set(ByVal value As String)
            _UnitName = value
        End Set
    End Property

    Public Property Pro_Id() As String
        Get
            Return _Pro_Id
        End Get
        Set(ByVal value As String)
            _Pro_Id = value
        End Set
    End Property

    Public Property Location_id() As Integer
        Get
            Return _Location_id
        End Get
        Set(ByVal value As Integer)
            _Location_id = value
        End Set
    End Property
    Public Property is_stock() As Byte
        Get
            Return _is_stock
        End Get
        Set(ByVal value As Byte)
            _is_stock = value
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

    Public Property cat_id() As Integer
        Get
            Return _cat_id
        End Get
        Set(ByVal value As Integer)
            _cat_id = value
        End Set
    End Property

    Public Property active() As Integer
        Get
            Return _active
        End Get
        Set(ByVal value As Integer)
            _active = value
        End Set
    End Property

    Public Property Del_Status() As String
        Get
            Return _Del_Status
        End Get
        Set(ByVal value As String)
            _Del_Status = value
        End Set
    End Property

    Public Property price_Id() As Integer
        Get
            Return _price_Id
        End Get
        Set(ByVal value As Integer)
            _price_Id = value
        End Set
    End Property



    Public Property size_Id() As Integer
        Get
            Return _size_Id
        End Get
        Set(ByVal value As Integer)
            _size_Id = value
        End Set
    End Property


    Public Property SizeName() As String
        Get
            Return _SizeName
        End Get
        Set(ByVal value As String)
            _SizeName = value
        End Set
    End Property


    Public Property size() As Decimal
        Get
            Return _size
        End Get
        Set(ByVal value As Decimal)
            _size = value
        End Set
    End Property


    Public Property click_and_collect() As Integer
        Get
            Return _click_and_collect
        End Get
        Set(ByVal value As Integer)
            _click_and_collect = value
        End Set
    End Property

    Public Property deliver() As Integer
        Get
            Return _deliver
        End Get
        Set(ByVal value As Integer)
            _deliver = value
        End Set
    End Property

    Public Property Order_at_table() As Integer
        Get
            Return _Order_at_table
        End Get
        Set(ByVal value As Integer)
            _Order_at_table = value
        End Set
    End Property

    Public Property is_web_available() As Integer
        Get
            Return _is_web_available
        End Get
        Set(ByVal value As Integer)
            _is_web_available = value
        End Set
    End Property

#End Region

    Public Function Insert() As Integer
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Category_id = Category_id
            objclsProduct.key_map_id = key_map_id
            objclsProduct.code = code
            objclsProduct.name = name
            objclsProduct.price = price
            objclsProduct.barcode = barcode
            objclsProduct.description = description
            objclsProduct.Is_active = Is_active
            objclsProduct.Ip_address = Ip_address
            objclsProduct.login_id = login_id
            objclsProduct.department_id = department_id
            objclsProduct.course_id = course_id
            objclsProduct.list = list
            objclsProduct.printer_id = printer_id
            objclsProduct.Tax_id = Tax_id
            objclsProduct.Actual_Price = Actual_Price
            objclsProduct.Tax = Tax
            objclsProduct.machine_id = machine_id
            objclsProduct.other_id = other_id
            objclsProduct.other_size = other_size
            objclsProduct.Is_Ingredient = Is_Ingredient
            objclsProduct.Is_Condiment = Is_Condiment
            objclsProduct.Base = Base
            objclsProduct.Unit_id = Unit_id
            objclsProduct.size_zero = size_zero
            objclsProduct.P_Image = P_Image
            objclsProduct.is_stock = is_stock
            objclsProduct.Cloak_Room = Cloak_Room
            objclsProduct.is_DanceVoucher = is_DanceVoucher
            objclsProduct.Is_PriceOnScaleWeight = Is_PriceOnScaleWeight
            objclsProduct.IsHouse = IsHouse
            objclsProduct.IsPkgProduct = IsPkgProduct
            objclsProduct.SortingNo = SortingNo
            objclsProduct.IsAdditionalTax = IsAdditionalTax
            objclsProduct.ForKiosk = ForKiosk
            objclsProduct.Is_OutOfStock = Is_OutOfStock

            'If objclsProduct.Insert() Then
            '    Return True
            'End If
            'Return False
            Dim r As Integer
            r = objclsProduct.Insert()
            Return r


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Category_id = Category_id
            objclsProduct.key_map_id = key_map_id
            objclsProduct.code = code
            objclsProduct.name = name
            objclsProduct.price = price
            objclsProduct.barcode = barcode
            objclsProduct.description = description
            objclsProduct.Is_active = Is_active
            objclsProduct.Ip_address = Ip_address
            objclsProduct.login_id = login_id
            objclsProduct.department_id = department_id
            objclsProduct.course_id = course_id
            objclsProduct.list = list
            objclsProduct.printer_id = printer_id
            objclsProduct.Tax_id = Tax_id
            objclsProduct.Tax = Tax
            objclsProduct.Actual_Price = Actual_Price
            objclsProduct.machine_id = machine_id
            objclsProduct.other_id = other_id
            objclsProduct.other_size = other_size
            objclsProduct.Is_Ingredient = Is_Ingredient
            objclsProduct.Is_Condiment = Is_Condiment
            objclsProduct.Base = Base
            objclsProduct.Unit_id = Unit_id
            objclsProduct.size_zero = size_zero
            objclsProduct.P_Image = P_Image
            objclsProduct.is_stock = is_stock
            objclsProduct.Cloak_Room = Cloak_Room
            objclsProduct.is_DanceVoucher = is_DanceVoucher
            objclsProduct.Is_PriceOnScaleWeight = Is_PriceOnScaleWeight
            objclsProduct.IsHouse = IsHouse
            objclsProduct.IsPkgProduct = IsPkgProduct
            objclsProduct.SortingNo = SortingNo
            objclsProduct.IsAdditionalTax = IsAdditionalTax
            objclsProduct.ForKiosk = ForKiosk
            objclsProduct.Is_OutOfStock = Is_OutOfStock

            If objclsProduct.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Category_id = Category_id
            objclsProduct.key_map_id = key_map_id
            objclsProduct.code = code
            objclsProduct.name = name
            objclsProduct.price = price
            objclsProduct.barcode = barcode
            objclsProduct.description = description
            objclsProduct.Is_active = Is_active
            objclsProduct.Ip_address = Ip_address
            objclsProduct.login_id = login_id
            objclsProduct.department_id = department_id
            objclsProduct.course_id = course_id
            objclsProduct.list = list
            objclsProduct.printer_id = printer_id
            objclsProduct.Tax_id = Tax_id
            objclsProduct.Tax = Tax
            objclsProduct.Actual_Price = Actual_Price
            objclsProduct.machine_id = machine_id
            objclsProduct.other_id = other_id
            objclsProduct.other_size = other_size
            objclsProduct.Tran_Type = Tran_Type
            objclsProduct.Is_Ingredient = Is_Ingredient
            objclsProduct.Is_Condiment = Is_Condiment
            objclsProduct.Base = Base
            objclsProduct.Unit_id = Unit_id
            objclsProduct.size_zero = size_zero
            objclsProduct.is_stock = is_stock
            objclsProduct.Cloak_Room = Cloak_Room
            objclsProduct.is_DanceVoucher = is_DanceVoucher
            objclsProduct.Is_PriceOnScaleWeight = Is_PriceOnScaleWeight
            objclsProduct.IsHouse = IsHouse
            objclsProduct.IsPkgProduct = IsPkgProduct
            objclsProduct.SortingNo = SortingNo
            objclsProduct.IsAdditionalTax = IsAdditionalTax
            objclsProduct.ForKiosk = ForKiosk
            objclsProduct.Is_OutOfStock = Is_OutOfStock

            If objclsProduct.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function UpdatePrice() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.price_Id = price_Id
            objclsProduct.price = price
            objclsProduct.Ip_address = Ip_address
            objclsProduct.login_id = login_id
            objclsProduct.cmp_id = cmp_id
            'objclsProduct.Actual_Price = Actual_Price
            objclsProduct.size_Id = size_Id
            objclsProduct.SizeName = SizeName
            objclsProduct.size = size
            objclsProduct.Unit_id = Unit_id
            objclsProduct.UnitName = UnitName
            objclsProduct.Tax_id = Tax_id
            objclsProduct.click_and_collect = click_and_collect
            objclsProduct.deliver = deliver
            objclsProduct.Order_at_table = Order_at_table
            If objclsProduct.UpdatePrice() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_dept]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[Select_dept]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_AttachedTo() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.Select_AttachedTo()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectProduct_Size]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectProduct_Size]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectProduct_Size_new]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectProduct_Size_new]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectPrice]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectPrice]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectBarcode_Size]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectBarcode_Size]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            'objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.active = active
            dt = objclsProduct.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_productCost_ByLocation() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            '
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Location_id = Location_id
            dt = objclsProduct.Select_productCost_ByLocation()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_Size_Details]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.cat_id = cat_id
            objclsProduct.active = active
            dt = objclsProduct.Get_Size_Details()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectAllCost() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            'objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.SelectAllCost()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_M_product_ForIngredient]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.Get_M_product_ForIngredient()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_M_product_ForIngredientLocationCategory]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Location_id = Location_id
            objclsProduct.Category_id = Category_id
            dt = objclsProduct.Get_M_product_ForIngredientLocationCategory()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Get_M_product_LocationCategoryActive() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Location_id = Location_id
            objclsProduct.Category_id = Category_id
            dt = objclsProduct.Get_M_product_LocationCategoryActive()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Get_M_product_ForIngredientLocation]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Location_id = Location_id
            dt = objclsProduct.Get_M_product_ForIngredientLocation()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Get_M_Condiment_ForProduct]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.Get_M_Condiment_ForProduct()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_ProductCode]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.code = code
            dt = objclsProduct.[Get_ProductCode]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectProductByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectProductByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectProductBylocation]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Location_id = Location_id
            dt = objclsProduct.[SelectProductBylocation]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectProductByGroup]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Category_id = Category_id
            dt = objclsProduct.[SelectProductByGroup]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectProductByGroupLocation]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Category_id = Category_id
            objclsProduct.Location_id = Location_id
            dt = objclsProduct.SelectProductByGroupLocation()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectIngredient]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.product_id = product_id
            dt = objclsProduct.[SelectIngredient]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [GetUnit_By_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.product_id = product_id
            dt = objclsProduct.[GetUnit_By_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectSize_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.product_id = product_id
            dt = objclsProduct.[SelectSize_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectIngredientByProduct]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectIngredientByProduct]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [SelectProductCondiment]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectProductCondiment]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_SizeNPrice_By_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.SingleRecord = SingleRecord
            objclsProduct.Product_Price_Id = Product_Price_Id
            objclsProduct.Location_id = Location_id
            dt = objclsProduct.[Select_SizeNPrice_By_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'added by hardik

    Public Function [SelectPricesByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            dt = objclsProduct.[SelectPricesByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'ended by hardik


    Public Function Select_cost_size() As DataSet
        Dim dt As DataSet
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Location_id = Location_id
            dt = objclsProduct.Select_cost_size()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [Select_SizeNCost_By_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.SingleRecord = SingleRecord
            dt = objclsProduct.[Select_SizeNCost_By_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_SizeNPrice_By_Product_Copy_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.SingleRecord = SingleRecord
            dt = objclsProduct.[Select_SizeNPrice_By_Product_Copy_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Copy_Insert() As Integer
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.name = name
            Dim r As Integer
            r = objclsProduct.Copy_Insert()
            Return r

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Change_ProductGroup() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Category_id = Category_id
            objclsProduct.printer_id = printer_id
            objclsProduct.department_id = department_id
            objclsProduct.course_id = course_id
            objclsProduct.Unit_id = Unit_id
            objclsProduct.Base = Base
            objclsProduct.Ip_address = Ip_address

            objclsProduct.is_web_available = is_web_available
            objclsProduct.is_stock = is_stock
            objclsProduct.Is_Condiment = Is_Condiment
            objclsProduct.Cloak_Room = Cloak_Room
            objclsProduct.is_unflag_printer = is_unflag_printer

            If objclsProduct.Change_ProductGroup() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Change_ProductSizeBulk() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Category_id = Category_id
            objclsProduct.Tax_id = Tax_id
            If objclsProduct.Change_ProductSizeBulk() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Product_Cost() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.price = price
            objclsProduct.created_date = created_date
            objclsProduct.Location_id = Location_id
            objclsProduct.cmp_id = cmp_id
            If objclsProduct.Product_Cost() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Ingredient_By_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.SingleRecord = SingleRecord
            dt = objclsProduct.[Select_Ingredient_By_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function select_product_price_current() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.Location_id = Location_id

            dt = objclsProduct.select_product_price_current()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Condiment_By_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.SingleRecord = SingleRecord
            dt = objclsProduct.[Select_Condiment_By_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_Allergy_By_Product() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id

            dt = objclsProduct.Select_Allergy_By_Product()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function delete_Allergy_By_Product() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id

            dt = objclsProduct.delete_Allergy_By_Product()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert_Allergy_By_Product() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.product_id = product_id
            objclsProduct.cmp_id = cmp_id
            objclsProduct.allergy_id = allergy_id

            dt = objclsProduct.Insert_Allergy_By_Product()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function ImportData() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.name = name
            objclsProduct.price = price
            objclsProduct.GroupName = GroupName
            objclsProduct.UnitName = UnitName
            objclsProduct.Ip_address = Ip_address
            objclsProduct.MainCategoryName = MainCategoryName

            If objclsProduct.ImportData() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ImportData_new() As Boolean
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.name = name
            objclsProduct.price = price
            objclsProduct.GroupName = GroupName
            objclsProduct.UnitName = UnitName
            objclsProduct.Ip_address = Ip_address
            objclsProduct.MainCategoryName = MainCategoryName

            objclsProduct.Base = Base
            objclsProduct.department_name = department_name
            objclsProduct.Printer_name1 = Printer_name1
            objclsProduct.Printer_name2 = Printer_name2
            objclsProduct.Printer_name3 = Printer_name3
            objclsProduct.size1name = size1name
            objclsProduct.size1price = size1price
            objclsProduct.size1 = size1
            objclsProduct.size1unit = size1unit

            objclsProduct.size2name = size2name
            objclsProduct.size2price = size2price
            objclsProduct.size2 = size2
            objclsProduct.size2unit = size2unit

            objclsProduct.size3name = size3name
            objclsProduct.size3price = size3price
            objclsProduct.size3 = size3
            objclsProduct.size3unit = size3unit

            objclsProduct.size4name = size4name
            objclsProduct.size4price = size4price
            objclsProduct.size4 = size4
            objclsProduct.size4unit = size4unit

            objclsProduct.barcode1 = barcode1
            objclsProduct.barcode2 = barcode2
            objclsProduct.barcode3 = barcode3
            objclsProduct.barcode4 = barcode4

            If objclsProduct.ImportData_new() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_Product_For_Ingredient]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Pro_Id = Pro_Id
            dt = objclsProduct.[Get_Product_For_Ingredient]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_Product_For_Stock_change]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Pro_Id = Pro_Id
            objclsProduct.row_id = row_id
            dt = objclsProduct.[Get_Product_For_Stock_change]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Get_Product_For_Stock_change_import() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Pro_Id = Pro_Id
            objclsProduct.row_id = row_id
            dt = objclsProduct.Get_Product_For_Stock_change_import()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_Product_For_Condiment]() As DataTable
        Dim dt As DataTable
        Try
            objclsProduct = New clsProduct()
            objclsProduct.cmp_id = cmp_id
            objclsProduct.Pro_Id = Pro_Id
            dt = objclsProduct.[Get_Product_For_Condiment]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsProduct
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


#Region "Public Variables"
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _row_id As Integer
    Private _Category_id As Integer
    Private _key_map_id As Integer
    Private _code As String
    Private _name As String
    Private _price As Decimal

    Private _barcode As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _login_id As Integer
    Private _department_id As Integer
    Private _course_id As Integer
    Private _list As Integer
    Private _printer_id As String
    Private _Actual_Price As Decimal
    Private _Tax_id As Integer
    Private _Tax As String
    Private _machine_id As Integer
    Private _other_id As String
    Private _other_size As String
    Private _SortingNo As Decimal
    Private _Tran_Type As String
    Private _Is_Ingredient As Byte
    Private _Is_Condiment As Byte
    Private _Base As Integer
    Private _Unit_id As Integer
    Private _SingleRecord As Integer
    Private _size_zero As Byte
    Private _GroupName As String
    Private _UnitName As String
    Private _Pro_Id As String
    Private _Location_id As Integer
    Private _P_Image As Byte()
    Private _allergy_id As Integer
    Private _is_stock As Byte
    Private _Product_Price_Id As Integer
    Private _cat_id As Integer
    Private _active As Integer
    Private _Del_Status As String
    Private _price_Id As Integer
    Private _size_Id As Integer
    Private _SizeName As String
    Private _size As Decimal
    Private _click_and_collect As Integer
    Private _deliver As Integer
    Private _Order_at_table As Integer
    Private _is_web_available As Integer
    Private _Cloak_Room As Byte
    Private _is_DanceVoucher As Integer
    Private _Is_PriceOnScaleWeight As Byte
    Private _IsHouse As Byte
    Private _IsPkgProduct As Byte
    Private _MainCategoryName As String
    Private _is_unflag_printer As Integer

    Private _department_name As String
    Private _Printer_name1 As String
    Private _Printer_name2 As String
    Private _Printer_name3 As String
    Private _size1name As String
    Private _size1price As Decimal
    Private _size1 As Decimal
    Private _size1unit As String
    Private _size2name As String
    Private _size2price As Decimal
    Private _size2 As Decimal
    Private _size2unit As String
    Private _size3name As String
    Private _size3price As Decimal
    Private _size3 As Decimal
    Private _size3unit As String
    Private _size4name As String
    Private _size4price As Decimal
    Private _size4 As Decimal
    Private _size4unit As String
    Private _IsAdditionalTax As Byte
    Private _ForKiosk As Byte
    Private _Is_OutOfStock As Byte

    Private _barcode1 As String
    Private _barcode2 As String
    Private _barcode3 As String
    Private _barcode4 As String

    Private objclsProduct As clsProduct
#End Region

#Region "Public Property"

    Public Property barcode1() As String
        Get
            Return _barcode1
        End Get
        Set(ByVal value As String)
            _barcode1 = value
        End Set
    End Property
    Public Property barcode2() As String
        Get
            Return _barcode2
        End Get
        Set(ByVal value As String)
            _barcode2 = value
        End Set
    End Property
    Public Property barcode3() As String
        Get
            Return _barcode3
        End Get
        Set(ByVal value As String)
            _barcode3 = value
        End Set
    End Property
    Public Property barcode4() As String
        Get
            Return _barcode4
        End Get
        Set(ByVal value As String)
            _barcode4 = value
        End Set
    End Property

    Public Property Is_OutOfStock() As Byte
        Get
            Return _Is_OutOfStock
        End Get
        Set(ByVal value As Byte)
            _Is_OutOfStock = value
        End Set
    End Property

    Public Property ForKiosk() As Byte
        Get
            Return _ForKiosk
        End Get
        Set(ByVal value As Byte)
            _ForKiosk = value
        End Set
    End Property

    Public Property IsAdditionalTax() As Byte
        Get
            Return _IsAdditionalTax
        End Get
        Set(ByVal value As Byte)
            _IsAdditionalTax = value
        End Set
    End Property

    Public Property IsPkgProduct() As Byte
        Get
            Return _IsPkgProduct
        End Get
        Set(ByVal value As Byte)
            _IsPkgProduct = value
        End Set
    End Property

    Public Property IsHouse() As Byte
        Get
            Return _IsHouse
        End Get
        Set(ByVal value As Byte)
            _IsHouse = value
        End Set
    End Property

    Public Property department_name() As String
        Get
            Return _department_name
        End Get
        Set(ByVal value As String)
            _department_name = value
        End Set
    End Property
    Public Property Printer_name1() As String
        Get
            Return _Printer_name1
        End Get
        Set(ByVal value As String)
            _Printer_name1 = value
        End Set
    End Property
    Public Property Printer_name2() As String
        Get
            Return _Printer_name2
        End Get
        Set(ByVal value As String)
            _Printer_name2 = value
        End Set
    End Property
    Public Property Printer_name3() As String
        Get
            Return _Printer_name3
        End Get
        Set(ByVal value As String)
            _Printer_name3 = value
        End Set
    End Property


    Public Property size1name() As String
        Get
            Return _size1name
        End Get
        Set(ByVal value As String)
            _size1name = value
        End Set
    End Property
    Public Property size2name() As String
        Get
            Return _size2name
        End Get
        Set(ByVal value As String)
            _size2name = value
        End Set
    End Property
    Public Property size3name() As String
        Get
            Return _size3name
        End Get
        Set(ByVal value As String)
            _size3name = value
        End Set
    End Property
    Public Property size4name() As String
        Get
            Return _size4name
        End Get
        Set(ByVal value As String)
            _size4name = value
        End Set
    End Property


    Public Property size1() As Decimal
        Get
            Return _size1
        End Get
        Set(ByVal value As Decimal)
            _size1 = value
        End Set
    End Property
    Public Property size2() As Decimal
        Get
            Return _size2
        End Get
        Set(ByVal value As Decimal)
            _size2 = value
        End Set
    End Property
    Public Property size3() As Decimal
        Get
            Return _size3
        End Get
        Set(ByVal value As Decimal)
            _size3 = value
        End Set
    End Property
    Public Property size4() As Decimal
        Get
            Return _size4
        End Get
        Set(ByVal value As Decimal)
            _size4 = value
        End Set
    End Property

    Public Property size1unit() As String
        Get
            Return _size1unit
        End Get
        Set(ByVal value As String)
            _size1unit = value
        End Set
    End Property
    Public Property size2unit() As String
        Get
            Return _size2unit
        End Get
        Set(ByVal value As String)
            _size2unit = value
        End Set
    End Property
    Public Property size3unit() As String
        Get
            Return _size3unit
        End Get
        Set(ByVal value As String)
            _size3unit = value
        End Set
    End Property
    Public Property size4unit() As String
        Get
            Return _size4unit
        End Get
        Set(ByVal value As String)
            _size4unit = value
        End Set
    End Property


    Public Property size1price() As Decimal
        Get
            Return _size1price
        End Get
        Set(ByVal value As Decimal)
            _size1price = value
        End Set
    End Property
    Public Property size2price() As Decimal
        Get
            Return _size2price
        End Get
        Set(ByVal value As Decimal)
            _size2price = value
        End Set
    End Property
    Public Property size3price() As Decimal
        Get
            Return _size3price
        End Get
        Set(ByVal value As Decimal)
            _size3price = value
        End Set
    End Property
    Public Property size4price() As Decimal
        Get
            Return _size4price
        End Get
        Set(ByVal value As Decimal)
            _size4price = value
        End Set
    End Property

    Public Property is_unflag_printer() As Integer
        Get
            Return _is_unflag_printer
        End Get
        Set(ByVal value As Integer)
            _is_unflag_printer = value
        End Set
    End Property

    Public Property MainCategoryName() As String
        Get
            Return _MainCategoryName
        End Get
        Set(ByVal value As String)
            _MainCategoryName = value
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
    Public Property P_Image() As Byte()
        Get
            Return _P_Image
        End Get
        Set(value As Byte())
            _P_Image = value
        End Set
    End Property
    Public Property Category_id() As Integer
        Get
            Return _Category_id
        End Get
        Set(ByVal value As Integer)
            _Category_id = value
        End Set
    End Property
    Public Property key_map_id() As Integer
        Get
            Return _key_map_id
        End Get
        Set(ByVal value As Integer)
            _key_map_id = value
        End Set
    End Property
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
    Public Property row_id() As Integer
        Get
            Return _row_id
        End Get
        Set(ByVal value As Integer)
            _row_id = value
        End Set
    End Property
    Public Property code() As String
        Get
            Return _code
        End Get
        Set(ByVal value As String)
            _code = value
        End Set
    End Property
    Public Property Tax() As String
        Get
            Return _Tax
        End Get
        Set(ByVal value As String)
            _Tax = value
        End Set
    End Property
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
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
    Public Property price() As Decimal
        Get
            Return _price
        End Get
        Set(ByVal value As Decimal)
            _price = value
        End Set
    End Property
    Public Property barcode() As String
        Get
            Return _barcode
        End Get
        Set(ByVal value As String)
            _barcode = value
        End Set
    End Property
    Public Property description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property created_date() As System.DateTime
        Get
            Return _created_date

        End Get
        Set(ByVal value As System.DateTime)
            _created_date = value
        End Set
    End Property
    Public Property modify_date() As System.DateTime
        Get
            Return _modify_date

        End Get
        Set(ByVal value As System.DateTime)
            _modify_date = value
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
    Public Property department_id() As Integer
        Get
            Return _department_id
        End Get
        Set(ByVal value As Integer)
            _department_id = value
        End Set
    End Property
    Public Property course_id() As Integer
        Get
            Return _course_id
        End Get
        Set(ByVal value As Integer)
            _course_id = value
        End Set
    End Property
    Public Property list() As Integer
        Get
            Return _list
        End Get
        Set(ByVal value As Integer)
            _list = value
        End Set
    End Property
    Public Property printer_id() As String
        Get
            Return _printer_id
        End Get
        Set(ByVal value As String)
            _printer_id = value
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
    Public Property other_id() As String
        Get
            Return _other_id
        End Get
        Set(ByVal value As String)
            _other_id = value
        End Set
    End Property
    Public Property other_size() As String
        Get
            Return _other_size
        End Get
        Set(ByVal value As String)
            _other_size = value
        End Set
    End Property

    Public Property SortingNo() As Decimal
        Get
            Return _SortingNo
        End Get
        Set(ByVal value As Decimal)
            _SortingNo = value
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
    Public Property Is_Ingredient() As Byte
        Get
            Return _Is_Ingredient
        End Get
        Set(ByVal value As Byte)
            _Is_Ingredient = value
        End Set
    End Property
    Public Property Is_Condiment() As Byte
        Get
            Return _Is_Condiment
        End Get
        Set(ByVal value As Byte)
            _Is_Condiment = value
        End Set
    End Property

    Public Property Cloak_Room() As Byte
        Get
            Return _Cloak_Room
        End Get
        Set(ByVal value As Byte)
            _Cloak_Room = value
        End Set
    End Property

    Public Property Is_PriceOnScaleWeight() As Byte
        Get
            Return _Is_PriceOnScaleWeight
        End Get
        Set(ByVal value As Byte)
            _Is_PriceOnScaleWeight = value
        End Set
    End Property
    Public Property is_DanceVoucher() As Integer
        Get
            Return _is_DanceVoucher
        End Get
        Set(ByVal value As Integer)
            _is_DanceVoucher = value
        End Set
    End Property
    Public Property Base() As Integer
        Get
            Return _Base
        End Get
        Set(ByVal value As Integer)
            _Base = value
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
    Public Property SingleRecord() As Integer
        Get
            Return _SingleRecord
        End Get
        Set(ByVal value As Integer)
            _SingleRecord = value
        End Set
    End Property
    Public Property size_zero() As Byte
        Get
            Return _size_zero
        End Get
        Set(ByVal value As Byte)
            _size_zero = value
        End Set
    End Property

    Public Property GroupName() As String
        Get
            Return _GroupName
        End Get
        Set(ByVal value As String)
            _GroupName = value
        End Set
    End Property

    Public Property UnitName() As String
        Get
            Return _UnitName
        End Get
        Set(ByVal value As String)
            _UnitName = value
        End Set
    End Property

    Public Property Pro_Id() As String
        Get
            Return _Pro_Id
        End Get
        Set(ByVal value As String)
            _Pro_Id = value
        End Set
    End Property

    Public Property Location_id() As Integer
        Get
            Return _Location_id
        End Get
        Set(ByVal value As Integer)
            _Location_id = value
        End Set
    End Property
    Public Property allergy_id() As Integer
        Get
            Return _allergy_id
        End Get
        Set(ByVal value As Integer)
            _allergy_id = value
        End Set
    End Property
    Public Property is_stock() As Byte
        Get
            Return _is_stock
        End Get
        Set(ByVal value As Byte)
            _is_stock = value
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

    Public Property cat_id() As Integer
        Get
            Return _cat_id
        End Get
        Set(ByVal value As Integer)
            _cat_id = value
        End Set
    End Property

    Public Property active() As Integer
        Get
            Return _active
        End Get
        Set(ByVal value As Integer)
            _active = value
        End Set
    End Property

    Public Property Del_Status() As String
        Get
            Return _Del_Status
        End Get
        Set(ByVal value As String)
            _Del_Status = value
        End Set
    End Property


    Public Property price_Id() As Integer
        Get
            Return _price_Id
        End Get
        Set(ByVal value As Integer)
            _price_Id = value
        End Set
    End Property


    Public Property size_Id() As Integer
        Get
            Return _size_Id
        End Get
        Set(ByVal value As Integer)
            _size_Id = value
        End Set
    End Property


    Public Property SizeName() As String
        Get
            Return _SizeName
        End Get
        Set(ByVal value As String)
            _SizeName = value
        End Set
    End Property


    Public Property size() As Decimal
        Get
            Return _size
        End Get
        Set(ByVal value As Decimal)
            _size = value
        End Set
    End Property


    Public Property click_and_collect() As Integer
        Get
            Return _click_and_collect
        End Get
        Set(ByVal value As Integer)
            _click_and_collect = value
        End Set
    End Property

    Public Property deliver() As Integer
        Get
            Return _deliver
        End Get
        Set(ByVal value As Integer)
            _deliver = value
        End Set
    End Property

    Public Property Order_at_table() As Integer
        Get
            Return _Order_at_table
        End Get
        Set(ByVal value As Integer)
            _Order_at_table = value
        End Set
    End Property

    Public Property is_web_available() As Integer
        Get
            Return _is_web_available
        End Get
        Set(ByVal value As Integer)
            _is_web_available = value
        End Set
    End Property
#End Region

    Public Function Insert() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@price", Val(price), SqlDbType.Decimal)
            oColSqlparram.Add("@barcode", barcode)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@course_id", course_id, SqlDbType.Int)
            oColSqlparram.Add("@list", list, SqlDbType.Int)
            oColSqlparram.Add("@printer_id", printer_id)
            oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
            oColSqlparram.Add("@Tax", Tax)
            oColSqlparram.Add("@Actual_Price", Val(Actual_Price), SqlDbType.Decimal)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@other_size", other_size)
            oColSqlparram.Add("@Is_Ingredient", Is_Ingredient, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_Condiment", Is_Condiment, SqlDbType.TinyInt)
            oColSqlparram.Add("@Base", Base)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@size_zero", size_zero, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_stock", is_stock, SqlDbType.TinyInt)
            oColSqlparram.Add("@Cloak_Room", Cloak_Room, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_DanceVoucher", is_DanceVoucher, SqlDbType.Int)
            oColSqlparram.Add("@Is_PriceOnScaleWeight", Is_PriceOnScaleWeight, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsHouse", IsHouse, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsPkgProduct", IsPkgProduct, SqlDbType.TinyInt)
            oColSqlparram.Add("@SortingNo", SortingNo, SqlDbType.Decimal)
            oColSqlparram.Add("@IsAdditionalTax", IsAdditionalTax, SqlDbType.TinyInt)
            oColSqlparram.Add("@ForKiosk", ForKiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_OutOfStock", Is_OutOfStock, SqlDbType.TinyInt)

            If Not P_Image Is Nothing Then
                oColSqlparram.Add("@P_Image", P_Image, SqlDbType.Image)
            End If

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product", oColSqlparram)
            Return dtlogin.Rows(0)("product_id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@barcode", barcode)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@course_id", course_id, SqlDbType.Int)
            oColSqlparram.Add("@list", list, SqlDbType.Int)
            oColSqlparram.Add("@printer_id", printer_id)
            oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
            oColSqlparram.Add("@Tax", Tax)
            oColSqlparram.Add("@Actual_Price", Val(Actual_Price), SqlDbType.Decimal)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@other_size", other_size)
            oColSqlparram.Add("@Is_Ingredient", Is_Ingredient, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_Condiment", Is_Condiment, SqlDbType.TinyInt)
            oColSqlparram.Add("@Base", Base)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@size_zero", size_zero, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_stock", is_stock, SqlDbType.TinyInt)
            oColSqlparram.Add("@Cloak_Room", Cloak_Room, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_DanceVoucher", is_DanceVoucher, SqlDbType.Int)
            oColSqlparram.Add("@Is_PriceOnScaleWeight", Is_PriceOnScaleWeight, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsHouse", IsHouse, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsPkgProduct", IsPkgProduct, SqlDbType.TinyInt)
            oColSqlparram.Add("@SortingNo", SortingNo, SqlDbType.Decimal)
            oColSqlparram.Add("@IsAdditionalTax", IsAdditionalTax, SqlDbType.TinyInt)
            oColSqlparram.Add("@ForKiosk", ForKiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_OutOfStock", Is_OutOfStock, SqlDbType.TinyInt)

            If Not P_Image Is Nothing Then
                oColSqlparram.Add("@P_Image", P_Image, SqlDbType.Image)
            End If
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@barcode", barcode)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", Tran_Type)
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@course_id", course_id, SqlDbType.Int)
            oColSqlparram.Add("@list", list, SqlDbType.Int)
            oColSqlparram.Add("@printer_id", printer_id)
            oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
            oColSqlparram.Add("@Tax", Tax)
            oColSqlparram.Add("@Actual_Price", Val(Actual_Price), SqlDbType.Decimal)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@other_size", other_size)
            oColSqlparram.Add("@Is_Ingredient", Is_Ingredient, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_Condiment", Is_Condiment, SqlDbType.TinyInt)
            oColSqlparram.Add("@Base", Base)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@size_zero", size_zero, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_stock", is_stock, SqlDbType.TinyInt)
            oColSqlparram.Add("@Cloak_Room", Cloak_Room, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_DanceVoucher", is_DanceVoucher, SqlDbType.Int)
            oColSqlparram.Add("@Is_PriceOnScaleWeight", Is_PriceOnScaleWeight, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsHouse", IsHouse, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsPkgProduct", IsPkgProduct, SqlDbType.TinyInt)
            oColSqlparram.Add("@SortingNo", SortingNo, SqlDbType.Decimal)
            oColSqlparram.Add("@IsAdditionalTax", IsAdditionalTax, SqlDbType.TinyInt)
            oColSqlparram.Add("@ForKiosk", ForKiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_OutOfStock", Is_OutOfStock, SqlDbType.TinyInt)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_For_Inactive", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [Select_dept]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Department_by_product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@active", active, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_AttachedTo() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_Associated", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectProduct_Size]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_Size", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectProduct_Size_new]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_Size_new", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectPrice]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Price", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectBarcode_Size]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Barcode_Size_Product", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@active", active, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_productCost_ByLocation() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_ByLocation", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [Get_Size_Details]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@cat_id", cat_id, SqlDbType.Int)
        oColSqlparram.Add("@active", active, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Size_Condiment_Details", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAllCost]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_Cost_List", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Get_M_product_ForIngredient]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_ForIngredient", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Get_M_product_ForIngredientLocation]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_ForIngredient_Location", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Get_M_product_ForIngredientLocationCategory]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_ForIngredient_Location", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Get_M_product_LocationCategoryActive() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_Location_category_active", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Get_M_Condiment_ForProduct]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Condiment_ForProduct", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Get_ProductCode]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@code", code)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_Code", oColSqlparram)
        Return dtlogin
    End Function

    Public Function [SelectProductByCmp]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function SelectProductBylocation() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_By_Location", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectProductByGroup]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@category_id", Category_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_By_PGroup", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectProductByGroupLocation]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@category_id", Category_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_By_PGroup_Location", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectIngredient]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_Ingredients", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [GetUnit_By_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Unit_By_Product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectSize_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Size_Product", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectIngredientByProduct]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_Ingredien_By_Product", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectProductCondiment]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_Condiment", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_SizeNPrice_By_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        oColSqlparram.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Size_N_Price_By_Product", oColSqlparram)

        Return dtlogin
    End Function

    'added by hardik 

    Public Function [SelectPricesByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_PricesBy_Cmp", oColSqlparram)

        Return dtlogin
    End Function
    'ended by hardik
    Public Function [Select_SizeNCost_By_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Size_N_Cost_By_Product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_cost_size]() As DataSet
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataSet = oClsDal.GetdatasetSp("Get_Cost_Size", oColSqlparram, "Cost_Size")

        Return dtlogin
    End Function

    Public Function [Select_SizeNPrice_By_Product_Copy_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Size_N_Price_By_Product_Copy_Product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Copy_Insert() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Copy_Product", oColSqlparram)
            'Return dtlogin.Rows(0)("product_id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Change_ProductGroup() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@printer_id", printer_id)
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@course_id", course_id, SqlDbType.Int)
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@Base", Base)
            oColSqlparram.Add("@Ip_address", Ip_address)

            oColSqlparram.Add("@is_web_available", is_web_available, SqlDbType.Int)
            oColSqlparram.Add("@is_stock", is_stock, SqlDbType.Int)
            oColSqlparram.Add("@Is_Condiment", Is_Condiment, SqlDbType.Int)
            oColSqlparram.Add("@Cloak_Room", Cloak_Room, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_unflag_printer", is_unflag_printer, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_ProductGroup_Change", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Change_ProductSizeBulk() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@tax_id", Tax_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_ProductSize_Change", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Product_Cost() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Cost", price, SqlDbType.Decimal)
            oColSqlparram.Add("@For_date", created_date, SqlDbType.DateTime)
            oColSqlparram.Add("@Location", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Cost", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Ingredient_By_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Ingredient_By_Product", oColSqlparram)

        Return dtlogin
    End Function


    Public Function select_product_price_current() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_Price_Location", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_Allergy_By_Product() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_Allergy", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Insert_Allergy_By_Product() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@allergy_id", allergy_id, SqlDbType.Int)
        oColSqlparram.Add("@tran_type", "I")

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Allergy", oColSqlparram)

        Return dtlogin
    End Function
    Public Function delete_Allergy_By_Product() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@allergy_id", 0, SqlDbType.Int)
        oColSqlparram.Add("@tran_type", "D")

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Allergy", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select_Condiment_By_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Condiment_By_Product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function ImportData() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@GroupName", GroupName)
            oColSqlparram.Add("@UnitName", UnitName)
            oColSqlparram.Add("@MainCategoryName", MainCategoryName)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_ImportData", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ImportData_new() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@GroupName", GroupName)
            oColSqlparram.Add("@UnitName", UnitName)
            oColSqlparram.Add("@MainCategoryName", MainCategoryName)
            oColSqlparram.Add("@Base", Base)
            oColSqlparram.Add("@department_name", department_name)
            oColSqlparram.Add("@Printer_name1", Printer_name1)
            oColSqlparram.Add("@Printer_name2", Printer_name2)
            oColSqlparram.Add("@Printer_name3", Printer_name3)

            oColSqlparram.Add("@size1name", size1name)
            oColSqlparram.Add("@size1price", size1price, SqlDbType.Decimal)
            oColSqlparram.Add("@size2name", size2name)
            oColSqlparram.Add("@size2price", size2price, SqlDbType.Decimal)
            oColSqlparram.Add("@size3name", size3name)
            oColSqlparram.Add("@size3price", size3price, SqlDbType.Decimal)
            oColSqlparram.Add("@size4name", size4name)
            oColSqlparram.Add("@size4price", size4price, SqlDbType.Decimal)

            oColSqlparram.Add("@size1unit", size1unit)
            oColSqlparram.Add("@size1", size1, SqlDbType.Decimal)
            oColSqlparram.Add("@size2unit", size2unit)
            oColSqlparram.Add("@size2", size2, SqlDbType.Decimal)
            oColSqlparram.Add("@size3unit", size3unit)
            oColSqlparram.Add("@size3", size3, SqlDbType.Decimal)
            oColSqlparram.Add("@size4unit", size4unit)
            oColSqlparram.Add("@size4", size4, SqlDbType.Decimal)

            oColSqlparram.Add("@Barcode1", barcode1)
            oColSqlparram.Add("@Barcode2", barcode2)
            oColSqlparram.Add("@Barcode3", barcode3)
            oColSqlparram.Add("@Barcode4", barcode4)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_ImportData_New", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Get_Product_For_Ingredient]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Pro_Id", Pro_Id)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_For_Ingredient", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Get_Product_For_Stock_change]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Pro_Id", Pro_Id)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@row_id", row_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_For_Stock_change", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Get_Product_For_Stock_change_import() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Pro_Id", Pro_Id)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@row_id", row_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_For_Stock_change_import", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Get_Product_For_Condiment]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Pro_Id", Pro_Id)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_For_Condiment", oColSqlparram)

        Return dtlogin
    End Function

    Public Function UpdatePrice() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Price_Id", price_Id, SqlDbType.Int)
            oColSqlparram.Add("@Price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            'oColSqlparram.Add("@Actual_Price", Actual_Price, SqlDbType.Decimal)
            oColSqlparram.Add("@Size_Id", size_Id, SqlDbType.Int)
            oColSqlparram.Add("@SizeName", SizeName)
            oColSqlparram.Add("@size", size, SqlDbType.Decimal)
            oColSqlparram.Add("@Unit_Id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@UnitName", UnitName)
            oColSqlparram.Add("@Tax_id", Tax_id, SqlDbType.Int)
            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.Int)
            oColSqlparram.Add("@deliver", deliver, SqlDbType.Int)
            oColSqlparram.Add("@Order_at_table", Order_at_table, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Price_Update", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
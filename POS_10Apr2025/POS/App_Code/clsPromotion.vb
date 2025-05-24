Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Imports Telerik.Web.UI

Public Class Controller_clsPromotion
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _table_promotion_id As Integer
    Private _cmp_id As Integer
    Private _row_id As Integer
    Private _product_id As Integer
    Private _product_id1 As Integer
    Private _product_id2 As Integer
    Private _product_id3 As Integer
    Private _size_id As Integer
    Private _size_id1 As Integer
    Private _size_id2 As Integer
    Private _size_id3 As Integer
    Private _price_id As Integer
    Private _Discount_type As Integer
    Private _Discount As Decimal
    Private _Recurrence As Integer
    Private _Month_flag As Integer
    Private _Months As String
    Private _Week_flag As Integer
    Private _Weeks As String
    Private _Days As String
    Private _on_day As String
    Private _Recurrence_flag As Integer
    Private _start_time As String
    Private _end_time As String
    Private _combo_flag As Integer
    Private _combo_product_id As String
    Private _location_id As Integer
    Private _machine_id As Integer
    Private _venue_id As Integer
    Private _Is_active As Byte
    Private _startDate As DateTime
    Private _EndDate As DateTime
    Private _product_group As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _promo_value As String
    Private _promo_type As Integer
    Private _duration_flag As Integer
    Private _no_of_product As Integer

    Private _start_time1 As String
    Private _end_time1 As String
    Private _start_time2 As String
    Private _end_time2 As String
    Private _start_time3 As String
    Private _end_time3 As String
    Private _is_endless As Integer
    Private _Pro_Id As String
    Private _Promo_name As String

    Private _promotion_id As String

    Private _product_group1 As Integer
    Private _product_group2 As Integer
    Private _product_group3 As Integer

    Private _Voucher_Type As String
    Private _Voucher_Code As String
    Private _Voucher_start As Integer
    Private _Voucher_end As Integer
    Private _Online_Coupon As String

    Private _mixnmatch As Integer
    Private _isFlatAmount As Integer
    Private _buy1_Get1 As Integer
    Private _freeProduct As Integer
    Private _freeProductAmount As Decimal
    Private _allfreeProduct As Integer
    Private _EachSpend As Integer
    Private _isbarStock As Integer
    Private _FreeQty As Decimal

    Private objclsPromotion As clsPromotion
#End Region

#Region "Public Property"

    Public Property EachSpend() As Integer
        Get
            Return _EachSpend
        End Get
        Set(ByVal value As Integer)
            _EachSpend = value
        End Set
    End Property
    Public Property isbarStock() As Integer
        Get
            Return _isbarStock
        End Get
        Set(ByVal value As Integer)
            _isbarStock = value
        End Set
    End Property
    Public Property FreeQty() As Decimal
        Get
            Return _FreeQty
        End Get
        Set(ByVal value As Decimal)
            _FreeQty = value
        End Set
    End Property

    Public Property freeProduct() As Integer
        Get
            Return _freeProduct
        End Get
        Set(ByVal value As Integer)
            _freeProduct = value
        End Set
    End Property
    Public Property freeProductAmount() As Decimal
        Get
            Return _freeProductAmount
        End Get
        Set(ByVal value As Decimal)
            _freeProductAmount = value
        End Set
    End Property
    Public Property AllfreeProduct() As Integer
        Get
            Return _allfreeProduct
        End Get
        Set(ByVal value As Integer)
            _allfreeProduct = value
        End Set
    End Property
    Public Property table_promotion_id() As Integer
        Get
            Return _table_promotion_id
        End Get
        Set(ByVal value As Integer)
            _table_promotion_id = value
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


    Public Property Pro_Id() As String
        Get
            Return _Pro_Id
        End Get
        Set(ByVal value As String)
            _Pro_Id = value
        End Set
    End Property

    Public Property promotion_id() As String
        Get
            Return _promotion_id
        End Get
        Set(ByVal value As String)
            _promotion_id = value
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

    Public Property product_id1() As Integer
        Get
            Return _product_id1
        End Get
        Set(ByVal value As Integer)
            _product_id1 = value
        End Set
    End Property

    Public Property product_id2() As Integer
        Get
            Return _product_id2
        End Get
        Set(ByVal value As Integer)
            _product_id2 = value
        End Set
    End Property

    Public Property product_id3() As Integer
        Get
            Return _product_id3
        End Get
        Set(ByVal value As Integer)
            _product_id3 = value
        End Set
    End Property
    Public Property size_id() As Integer
        Get
            Return _size_id
        End Get
        Set(ByVal value As Integer)
            _size_id = value
        End Set
    End Property

    Public Property size_id1() As Integer
        Get
            Return _size_id1
        End Get
        Set(ByVal value As Integer)
            _size_id1 = value
        End Set
    End Property

    Public Property size_id2() As Integer
        Get
            Return _size_id2
        End Get
        Set(ByVal value As Integer)
            _size_id2 = value
        End Set
    End Property

    Public Property size_id3() As Integer
        Get
            Return _size_id3
        End Get
        Set(ByVal value As Integer)
            _size_id3 = value
        End Set
    End Property

    Public Property price_id() As Integer
        Get
            Return _price_id
        End Get
        Set(ByVal value As Integer)
            _price_id = value
        End Set
    End Property

    Public Property Discount_type() As Integer
        Get
            Return _Discount_type
        End Get
        Set(ByVal value As Integer)
            _Discount_type = value
        End Set
    End Property

    Public Property Discount() As Decimal
        Get
            Return _Discount
        End Get
        Set(ByVal value As Decimal)
            _Discount = value
        End Set
    End Property
    Public Property Recurrence() As Integer
        Get
            Return _Recurrence
        End Get
        Set(ByVal value As Integer)
            _Recurrence = value
        End Set
    End Property
    Public Property Month_flag() As Integer
        Get
            Return _Month_flag
        End Get
        Set(ByVal value As Integer)
            _Month_flag = value
        End Set
    End Property

    Public Property Months() As String
        Get
            Return _Months
        End Get
        Set(ByVal value As String)
            _Months = value
        End Set
    End Property

    Public Property Promo_name() As String
        Get
            Return _Promo_name
        End Get
        Set(ByVal value As String)
            _Promo_name = value
        End Set
    End Property

    Public Property Week_flag() As Integer
        Get
            Return _Week_flag
        End Get
        Set(ByVal value As Integer)
            _Week_flag = value
        End Set
    End Property


    Public Property duration_flag() As Integer
        Get
            Return _duration_flag
        End Get
        Set(ByVal value As Integer)
            _duration_flag = value
        End Set
    End Property

    Public Property no_of_product() As Integer
        Get
            Return _no_of_product
        End Get
        Set(ByVal value As Integer)
            _no_of_product = value
        End Set
    End Property

    Public Property Weeks() As String
        Get
            Return _Weeks
        End Get
        Set(ByVal value As String)
            _Weeks = value
        End Set
    End Property

    Public Property Days() As String
        Get
            Return _Days
        End Get
        Set(ByVal value As String)
            _Days = value
        End Set
    End Property

    Public Property on_day() As String
        Get
            Return _on_day
        End Get
        Set(ByVal value As String)
            _on_day = value
        End Set
    End Property

    Public Property Recurrence_flag() As Integer
        Get
            Return _Recurrence_flag
        End Get
        Set(ByVal value As Integer)
            _Recurrence_flag = value
        End Set
    End Property

    Public Property start_time() As String
        Get
            Return _start_time
        End Get
        Set(ByVal value As String)
            _start_time = value
        End Set
    End Property

    Public Property start_time1() As String
        Get
            Return _start_time1
        End Get
        Set(ByVal value As String)
            _start_time1 = value
        End Set
    End Property

    Public Property start_time2() As String
        Get
            Return _start_time2
        End Get
        Set(ByVal value As String)
            _start_time2 = value
        End Set
    End Property

    Public Property start_time3() As String
        Get
            Return _start_time3
        End Get
        Set(ByVal value As String)
            _start_time3 = value
        End Set
    End Property

    Public Property end_time() As String
        Get
            Return _end_time
        End Get
        Set(ByVal value As String)
            _end_time = value
        End Set
    End Property

    Public Property end_time1() As String
        Get
            Return _end_time1
        End Get
        Set(ByVal value As String)
            _end_time1 = value
        End Set
    End Property
    Public Property end_time2() As String
        Get
            Return _end_time2
        End Get
        Set(ByVal value As String)
            _end_time2 = value
        End Set
    End Property
    Public Property end_time3() As String
        Get
            Return _end_time3
        End Get
        Set(ByVal value As String)
            _end_time3 = value
        End Set
    End Property

    Public Property is_endless() As Integer
        Get
            Return _is_endless
        End Get
        Set(ByVal value As Integer)
            _is_endless = value
        End Set
    End Property
    Public Property combo_flag() As Integer
        Get
            Return _combo_flag
        End Get
        Set(ByVal value As Integer)
            _combo_flag = value
        End Set
    End Property

    Public Property combo_product_id() As String
        Get
            Return _combo_product_id
        End Get
        Set(ByVal value As String)
            _combo_product_id = value
        End Set
    End Property

    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
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

    Public Property venue_id() As Integer
        Get
            Return _venue_id
        End Get
        Set(ByVal value As Integer)
            _venue_id = value
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

    Public Property startDate() As System.DateTime
        Get
            Return _startDate

        End Get
        Set(ByVal value As System.DateTime)
            _startDate = value
        End Set
    End Property

    Public Property EndDate() As System.DateTime
        Get
            Return _EndDate

        End Get
        Set(ByVal value As System.DateTime)
            _EndDate = value
        End Set
    End Property

    Public Property product_group() As Integer
        Get
            Return _product_group
        End Get
        Set(ByVal value As Integer)
            _product_group = value
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

    Public Property promo_value() As String
        Get
            Return _promo_value
        End Get
        Set(ByVal value As String)
            _promo_value = value
        End Set
    End Property

    Public Property promo_type() As Integer
        Get
            Return _promo_type
        End Get
        Set(ByVal value As Integer)
            _promo_type = value
        End Set
    End Property

    Public Property product_group1() As Integer
        Get
            Return _product_group1
        End Get
        Set(ByVal value As Integer)
            _product_group1 = value
        End Set
    End Property

    Public Property product_group2() As Integer
        Get
            Return _product_group2
        End Get
        Set(ByVal value As Integer)
            _product_group2 = value
        End Set
    End Property

    Public Property product_group3() As Integer
        Get
            Return _product_group3
        End Get
        Set(ByVal value As Integer)
            _product_group3 = value
        End Set
    End Property

    Public Property Voucher_Type() As String
        Get
            Return _Voucher_Type
        End Get
        Set(ByVal value As String)
            _Voucher_Type = value
        End Set
    End Property
    Public Property Voucher_Code() As String
        Get
            Return _Voucher_Code
        End Get
        Set(ByVal value As String)
            _Voucher_Code = value
        End Set
    End Property

    Public Property Voucher_Start() As Integer
        Get
            Return _Voucher_start
        End Get
        Set(ByVal value As Integer)
            _Voucher_start = value
        End Set
    End Property


    Public Property Voucher_end() As Integer
        Get
            Return _Voucher_end
        End Get
        Set(ByVal value As Integer)
            _Voucher_end = value
        End Set
    End Property
    Public Property Online_Coupon() As String
        Get
            Return _Online_Coupon
        End Get
        Set(ByVal value As String)
            _Online_Coupon = value
        End Set
    End Property

    Public Property mixnmatch() As Integer
        Get
            Return _mixnmatch
        End Get
        Set(ByVal value As Integer)
            _mixnmatch = value
        End Set
    End Property

    Public Property isFlatAmount() As Integer
        Get
            Return _isFlatAmount
        End Get
        Set(ByVal value As Integer)
            _isFlatAmount = value
        End Set
    End Property

    Public Property buy1_Get1() As Integer
        Get
            Return _buy1_Get1
        End Get
        Set(ByVal value As Integer)
            _buy1_Get1 = value
        End Set
    End Property
#End Region

    Public Function Insert() As Integer
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.table_promotion_id = table_promotion_id
            objclsPromotion.product_id = product_id
            objclsPromotion.product_id1 = product_id1
            objclsPromotion.product_id2 = product_id2
            objclsPromotion.product_id3 = product_id3
            objclsPromotion.size_id = size_id
            objclsPromotion.size_id1 = size_id1
            objclsPromotion.size_id2 = size_id2
            objclsPromotion.size_id3 = size_id3
            objclsPromotion.price_id = price_id
            objclsPromotion.Discount_type = Discount_type
            objclsPromotion.Discount = Discount
            objclsPromotion.Recurrence = Recurrence
            objclsPromotion.Month_flag = Month_flag
            objclsPromotion.Months = Months
            objclsPromotion.Week_flag = Week_flag
            objclsPromotion.Weeks = Weeks
            objclsPromotion.Days = Days
            objclsPromotion.on_day = on_day
            objclsPromotion.Recurrence_flag = Recurrence_flag
            objclsPromotion.start_time = start_time
            objclsPromotion.start_time1 = start_time1
            objclsPromotion.start_time2 = start_time2
            objclsPromotion.start_time3 = start_time3
            objclsPromotion.end_time = end_time
            objclsPromotion.end_time1 = end_time1
            objclsPromotion.end_time2 = end_time2
            objclsPromotion.end_time3 = end_time3
            objclsPromotion.is_endless = is_endless
            objclsPromotion.startDate = startDate
            objclsPromotion.EndDate = EndDate
            objclsPromotion.combo_flag = combo_flag
            objclsPromotion.combo_product_id = combo_product_id
            objclsPromotion.location_id = location_id
            objclsPromotion.machine_id = machine_id
            objclsPromotion.venue_id = venue_id
            objclsPromotion.cmp_id = cmp_id
            objclsPromotion.Is_active = Is_active
            objclsPromotion.Ip_address = Ip_address
            objclsPromotion.login_id = login_id
            objclsPromotion.product_group = product_group
            objclsPromotion.product_group1 = product_group1
            objclsPromotion.product_group2 = product_group2
            objclsPromotion.product_group3 = product_group3
            objclsPromotion.duration_flag = duration_flag
            objclsPromotion.no_of_product = no_of_product
            objclsPromotion.Promo_name = Promo_name
            objclsPromotion.Voucher_Type = Voucher_Type
            objclsPromotion.Voucher_Code = Voucher_Code
            objclsPromotion.Online_Coupon = Online_Coupon
            objclsPromotion.mixnmatch = mixnmatch
            objclsPromotion.isFlatAmount = isFlatAmount
            objclsPromotion.buy1_Get1 = buy1_Get1
            objclsPromotion.freeProduct = freeProduct
            objclsPromotion.freeProductAmount = freeProductAmount
            objclsPromotion.AllfreeProduct = AllfreeProduct
            objclsPromotion.EachSpend = EachSpend
            objclsPromotion.isbarStock = isbarStock
            objclsPromotion.FreeQty = FreeQty

            Dim r As Integer
            r = objclsPromotion.Insert()
            Return r


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.table_promotion_id = table_promotion_id
            objclsPromotion.product_id = product_id
            objclsPromotion.product_id1 = product_id1
            objclsPromotion.product_id2 = product_id2
            objclsPromotion.product_id3 = product_id3
            objclsPromotion.size_id = size_id
            objclsPromotion.size_id1 = size_id1
            objclsPromotion.size_id2 = size_id2
            objclsPromotion.size_id3 = size_id3
            objclsPromotion.price_id = price_id
            objclsPromotion.Discount_type = Discount_type
            objclsPromotion.Discount = Discount
            objclsPromotion.Recurrence = Recurrence
            objclsPromotion.Month_flag = Month_flag
            objclsPromotion.Months = Months
            objclsPromotion.Week_flag = Week_flag
            objclsPromotion.Weeks = Weeks
            objclsPromotion.Days = Days
            objclsPromotion.on_day = on_day
            objclsPromotion.Recurrence_flag = Recurrence_flag
            objclsPromotion.start_time = start_time
            objclsPromotion.start_time1 = start_time1
            objclsPromotion.start_time2 = start_time2
            objclsPromotion.start_time3 = start_time3
            objclsPromotion.end_time = end_time
            objclsPromotion.end_time1 = end_time1
            objclsPromotion.end_time2 = end_time2
            objclsPromotion.end_time3 = end_time3
            objclsPromotion.is_endless = is_endless
            objclsPromotion.startDate = startDate
            objclsPromotion.EndDate = EndDate
            objclsPromotion.combo_flag = combo_flag
            objclsPromotion.combo_product_id = combo_product_id
            objclsPromotion.location_id = location_id
            objclsPromotion.machine_id = machine_id
            objclsPromotion.venue_id = venue_id
            objclsPromotion.cmp_id = cmp_id
            objclsPromotion.Is_active = Is_active
            objclsPromotion.Ip_address = Ip_address
            objclsPromotion.login_id = login_id
            objclsPromotion.product_group = product_group
            objclsPromotion.product_group1 = product_group1
            objclsPromotion.product_group2 = product_group2
            objclsPromotion.product_group3 = product_group3
            objclsPromotion.duration_flag = duration_flag
            objclsPromotion.no_of_product = no_of_product
            objclsPromotion.Promo_name = Promo_name
            objclsPromotion.Voucher_Type = Voucher_Type
            objclsPromotion.Voucher_Code = Voucher_Code
            objclsPromotion.Online_Coupon = Online_Coupon
            objclsPromotion.mixnmatch = mixnmatch
            objclsPromotion.isFlatAmount = isFlatAmount
            objclsPromotion.buy1_Get1 = buy1_Get1
            objclsPromotion.freeProduct = freeProduct
            objclsPromotion.freeProductAmount = freeProductAmount
            objclsPromotion.AllfreeProduct = AllfreeProduct
            objclsPromotion.EachSpend = EachSpend
            objclsPromotion.isbarStock = isbarStock
            objclsPromotion.FreeQty = FreeQty
            If objclsPromotion.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.table_promotion_id = table_promotion_id
            objclsPromotion.product_id = product_id
            objclsPromotion.product_id1 = product_id1
            objclsPromotion.product_id2 = product_id2
            objclsPromotion.product_id3 = product_id3
            objclsPromotion.size_id = size_id
            objclsPromotion.size_id1 = size_id1
            objclsPromotion.size_id2 = size_id2
            objclsPromotion.size_id3 = size_id3
            objclsPromotion.price_id = price_id
            objclsPromotion.Discount_type = Discount_type
            objclsPromotion.Discount = Discount
            objclsPromotion.Recurrence = Recurrence
            objclsPromotion.Month_flag = Month_flag
            objclsPromotion.Months = Months
            objclsPromotion.Week_flag = Week_flag
            objclsPromotion.Weeks = Weeks
            objclsPromotion.Days = Days
            objclsPromotion.on_day = on_day
            objclsPromotion.Recurrence_flag = Recurrence_flag
            objclsPromotion.start_time = start_time
            objclsPromotion.start_time1 = start_time1
            objclsPromotion.start_time2 = start_time2
            objclsPromotion.start_time3 = start_time3
            objclsPromotion.end_time = end_time
            objclsPromotion.end_time1 = end_time1
            objclsPromotion.end_time2 = end_time2
            objclsPromotion.end_time3 = end_time3
            objclsPromotion.is_endless = is_endless
            objclsPromotion.startDate = startDate
            objclsPromotion.EndDate = EndDate
            objclsPromotion.combo_flag = combo_flag
            objclsPromotion.combo_product_id = combo_product_id
            objclsPromotion.location_id = location_id
            objclsPromotion.machine_id = machine_id
            objclsPromotion.venue_id = venue_id
            objclsPromotion.cmp_id = cmp_id
            objclsPromotion.Is_active = Is_active
            objclsPromotion.Ip_address = Ip_address
            objclsPromotion.login_id = login_id
            objclsPromotion.product_group = product_group
            objclsPromotion.product_group1 = product_group1
            objclsPromotion.product_group2 = product_group2
            objclsPromotion.product_group3 = product_group3
            objclsPromotion.duration_flag = duration_flag
            objclsPromotion.no_of_product = no_of_product
            objclsPromotion.Promo_name = Promo_name
            objclsPromotion.Voucher_Type = Voucher_Type
            objclsPromotion.Voucher_Code = Voucher_Code
            objclsPromotion.Online_Coupon = Online_Coupon
            objclsPromotion.mixnmatch = mixnmatch
            objclsPromotion.isFlatAmount = isFlatAmount
            objclsPromotion.freeProduct = freeProduct
            objclsPromotion.freeProductAmount = freeProductAmount
            objclsPromotion.AllfreeProduct = AllfreeProduct
            objclsPromotion.EachSpend = EachSpend
            objclsPromotion.isbarStock = isbarStock
            objclsPromotion.FreeQty = FreeQty
            If objclsPromotion.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Product_For_Promotoin]() As DataTable
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.cmp_id = cmp_id
            objclsPromotion.Pro_Id = Pro_Id
            dt = objclsPromotion.[Select_Product_For_Promotoin]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Insert_Voucher]() As DataTable
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.cmp_id = cmp_id
            objclsPromotion.Voucher_Code = Voucher_Code
            objclsPromotion.Voucher_Start = Voucher_Start
            objclsPromotion.Voucher_end = Voucher_end
            dt = objclsPromotion.Insert_Voucher()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Get_Voucher]() As DataTable
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.cmp_id = cmp_id

            dt = objclsPromotion.Get_Voucher()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Insert_PromotionType() As Integer
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.table_promotion_id = table_promotion_id
            objclsPromotion.promo_value = promo_value
            objclsPromotion.promo_type = promo_type
            objclsPromotion.cmp_id = cmp_id
            objclsPromotion.Ip_address = Ip_address
            objclsPromotion.login_id = login_id

            If objclsPromotion.Insert_PromotionType() Then
                Return True
            End If
            Return False


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [BindPromotion](ByRef ddl As RadComboBox)
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()

            objclsPromotion.Promo_name = Promo_name
            objclsPromotion.cmp_id = cmp_id
            dt = objclsPromotion.[BindPromotion]()

            ddl.DataSource = dt
            ddl.DataValueField = "Voucher_code"
            ddl.DataTextField = "Voucher_code"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("--SELECT--", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()
            'objclsPromotion.table_promotion_id = table_promotion_id
            objclsPromotion.Promo_name = Promo_name
            objclsPromotion.cmp_id = cmp_id
            dt = objclsPromotion.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete_Edit() As Boolean
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.table_promotion_id = table_promotion_id
            objclsPromotion.cmp_id = cmp_id
            If objclsPromotion.Delete_Edit() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_Voucher() As Boolean
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.Voucher_Code = Voucher_Code
            If objclsPromotion.Delete_Voucher() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [check_Duplicate_Promoname]() As DataTable
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.table_promotion_id = table_promotion_id
            objclsPromotion.Promo_name = Promo_name
            objclsPromotion.cmp_id = cmp_id
            dt = objclsPromotion.[check_Duplicate_Promoname]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_urlforsales() As DataTable
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()
            'objclsPromotion.location_id = location_id
            ' objclsLocation.created_date = created_date
            dt = objclsPromotion.Select_urlforsales()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [check_Duplicate_Promoname_Edit]() As DataTable
        Dim dt As DataTable
        Try
            objclsPromotion = New clsPromotion()
            objclsPromotion.promotion_id = promotion_id
            objclsPromotion.Promo_name = Promo_name
            objclsPromotion.cmp_id = cmp_id
            dt = objclsPromotion.[check_Duplicate_Promoname_Edit]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class

Public Class clsPromotion
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _table_promotion_id As Integer
    Private _cmp_id As Integer
    Private _row_id As Integer
    Private _product_id As Integer
    Private _product_id1 As Integer
    Private _product_id2 As Integer
    Private _product_id3 As Integer
    Private _size_id As Integer
    Private _size_id1 As Integer
    Private _size_id2 As Integer
    Private _size_id3 As Integer
    Private _price_id As Integer
    Private _Discount_type As Integer
    Private _Discount As Decimal
    Private _Recurrence As Integer
    Private _Month_flag As Integer
    Private _Months As String
    Private _Week_flag As Integer
    Private _Weeks As String
    Private _Days As String
    Private _on_day As String
    Private _Recurrence_flag As Integer
    Private _start_time As String
    Private _end_time As String
    Private _combo_flag As Integer
    Private _combo_product_id As String
    Private _location_id As Integer
    Private _machine_id As Integer
    Private _venue_id As Integer
    Private _Is_active As Byte
    Private _startDate As DateTime
    Private _EndDate As DateTime
    Private _product_group As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _promo_value As String
    Private _promo_type As Integer
    Private _duration_flag As Integer
    Private _no_of_product As Integer
    Private _start_time1 As String
    Private _end_time1 As String
    Private _start_time2 As String
    Private _end_time2 As String
    Private _start_time3 As String
    Private _end_time3 As String
    Private _is_endless As Integer
    Private _Promo_name As String
    Private _Pro_Id As String

    Private _promotion_id As String

    Private _product_group1 As Integer
    Private _product_group2 As Integer
    Private _product_group3 As Integer

    Private _Voucher_Type As String
    Private _Voucher_Code As String

    Private _Voucher_start As Integer
    Private _Voucher_end As Integer
    Private _Online_Coupon As String


    Private _mixnmatch As Integer
    Private _isFlatAmount As Integer
    Private _buy1_Get1 As Integer
    Private _freeProduct As Integer
    Private _freeProductAmount As Decimal
    Private _allfreeProduct As Integer
    Private _EachSpend As Integer
    Private _isbarStock As Integer
    Private _FreeQty As Decimal


    Private objclsPromotion As clsPromotion
#End Region

#Region "Public Property"

    Public Property EachSpend() As Integer
        Get
            Return _EachSpend
        End Get
        Set(ByVal value As Integer)
            _EachSpend = value
        End Set
    End Property
    Public Property isbarStock() As Integer
        Get
            Return _isbarStock
        End Get
        Set(ByVal value As Integer)
            _isbarStock = value
        End Set
    End Property
    Public Property FreeQty() As Decimal
        Get
            Return _FreeQty
        End Get
        Set(ByVal value As Decimal)
            _FreeQty = value
        End Set
    End Property

    Public Property AllfreeProduct() As Integer
        Get
            Return _allfreeProduct
        End Get
        Set(ByVal value As Integer)
            _allfreeProduct = value
        End Set
    End Property
    Public Property freeProduct() As Integer
        Get
            Return _freeProduct
        End Get
        Set(ByVal value As Integer)
            _freeProduct = value
        End Set
    End Property
    Public Property freeProductAmount() As Decimal
        Get
            Return _freeProductAmount
        End Get
        Set(ByVal value As Decimal)
            _freeProductAmount = value
        End Set
    End Property

    Public Property mixnmatch() As Integer
        Get
            Return _mixnmatch
        End Get
        Set(ByVal value As Integer)
            _mixnmatch = value
        End Set
    End Property

    Public Property isFlatAmount() As Integer
        Get
            Return _isFlatAmount
        End Get
        Set(ByVal value As Integer)
            _isFlatAmount = value
        End Set
    End Property

    Public Property table_promotion_id() As Integer
        Get
            Return _table_promotion_id
        End Get
        Set(ByVal value As Integer)
            _table_promotion_id = value
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

    Public Property Pro_Id() As String
        Get
            Return _Pro_Id
        End Get
        Set(ByVal value As String)
            _Pro_Id = value
        End Set
    End Property

    Public Property promotion_id() As String
        Get
            Return _promotion_id
        End Get
        Set(ByVal value As String)
            _promotion_id = value
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

    Public Property product_id1() As Integer
        Get
            Return _product_id1
        End Get
        Set(ByVal value As Integer)
            _product_id1 = value
        End Set
    End Property

    Public Property product_id2() As Integer
        Get
            Return _product_id2
        End Get
        Set(ByVal value As Integer)
            _product_id2 = value
        End Set
    End Property

    Public Property product_id3() As Integer
        Get
            Return _product_id3
        End Get
        Set(ByVal value As Integer)
            _product_id3 = value
        End Set
    End Property
    Public Property size_id() As Integer
        Get
            Return _size_id
        End Get
        Set(ByVal value As Integer)
            _size_id = value
        End Set
    End Property

    Public Property size_id1() As Integer
        Get
            Return _size_id1
        End Get
        Set(ByVal value As Integer)
            _size_id1 = value
        End Set
    End Property

    Public Property size_id2() As Integer
        Get
            Return _size_id2
        End Get
        Set(ByVal value As Integer)
            _size_id2 = value
        End Set
    End Property

    Public Property size_id3() As Integer
        Get
            Return _size_id3
        End Get
        Set(ByVal value As Integer)
            _size_id3 = value
        End Set
    End Property

    Public Property price_id() As Integer
        Get
            Return _price_id
        End Get
        Set(ByVal value As Integer)
            _price_id = value
        End Set
    End Property

    Public Property no_of_product() As Integer
        Get
            Return _no_of_product
        End Get
        Set(ByVal value As Integer)
            _no_of_product = value
        End Set
    End Property

    Public Property Discount_type() As Integer
        Get
            Return _Discount_type
        End Get
        Set(ByVal value As Integer)
            _Discount_type = value
        End Set
    End Property

    Public Property Discount() As Decimal
        Get
            Return _Discount
        End Get
        Set(ByVal value As Decimal)
            _Discount = value
        End Set
    End Property
    Public Property Recurrence() As Integer
        Get
            Return _Recurrence
        End Get
        Set(ByVal value As Integer)
            _Recurrence = value
        End Set
    End Property
    Public Property Month_flag() As Integer
        Get
            Return _Month_flag
        End Get
        Set(ByVal value As Integer)
            _Month_flag = value
        End Set
    End Property

    Public Property Months() As String
        Get
            Return _Months
        End Get
        Set(ByVal value As String)
            _Months = value
        End Set
    End Property

    Public Property Week_flag() As Integer
        Get
            Return _Week_flag
        End Get
        Set(ByVal value As Integer)
            _Week_flag = value
        End Set
    End Property

    Public Property duration_flag() As Integer
        Get
            Return _duration_flag
        End Get
        Set(ByVal value As Integer)
            _duration_flag = value
        End Set
    End Property

    Public Property Weeks() As String
        Get
            Return _Weeks
        End Get
        Set(ByVal value As String)
            _Weeks = value
        End Set
    End Property

    Public Property Days() As String
        Get
            Return _Days
        End Get
        Set(ByVal value As String)
            _Days = value
        End Set
    End Property

    Public Property on_day() As String
        Get
            Return _on_day
        End Get
        Set(ByVal value As String)
            _on_day = value
        End Set
    End Property

    Public Property Recurrence_flag() As Integer
        Get
            Return _Recurrence_flag
        End Get
        Set(ByVal value As Integer)
            _Recurrence_flag = value
        End Set
    End Property

    Public Property start_time() As String
        Get
            Return _start_time
        End Get
        Set(ByVal value As String)
            _start_time = value
        End Set
    End Property

    Public Property start_time1() As String
        Get
            Return _start_time1
        End Get
        Set(ByVal value As String)
            _start_time1 = value
        End Set
    End Property

    Public Property start_time2() As String
        Get
            Return _start_time2
        End Get
        Set(ByVal value As String)
            _start_time2 = value
        End Set
    End Property

    Public Property start_time3() As String
        Get
            Return _start_time3
        End Get
        Set(ByVal value As String)
            _start_time3 = value
        End Set
    End Property


    Public Property end_time() As String
        Get
            Return _end_time
        End Get
        Set(ByVal value As String)
            _end_time = value
        End Set
    End Property

    Public Property end_time1() As String
        Get
            Return _end_time1
        End Get
        Set(ByVal value As String)
            _end_time1 = value
        End Set
    End Property
    Public Property end_time2() As String
        Get
            Return _end_time2
        End Get
        Set(ByVal value As String)
            _end_time2 = value
        End Set
    End Property
    Public Property end_time3() As String
        Get
            Return _end_time3
        End Get
        Set(ByVal value As String)
            _end_time3 = value
        End Set
    End Property

    Public Property is_endless() As Integer
        Get
            Return _is_endless
        End Get
        Set(ByVal value As Integer)
            _is_endless = value
        End Set
    End Property

    Public Property combo_flag() As Integer
        Get
            Return _combo_flag
        End Get
        Set(ByVal value As Integer)
            _combo_flag = value
        End Set
    End Property

    Public Property combo_product_id() As String
        Get
            Return _combo_product_id
        End Get
        Set(ByVal value As String)
            _combo_product_id = value
        End Set
    End Property

    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
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

    Public Property venue_id() As Integer
        Get
            Return _venue_id
        End Get
        Set(ByVal value As Integer)
            _venue_id = value
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

    Public Property startDate() As System.DateTime
        Get
            Return _startDate

        End Get
        Set(ByVal value As System.DateTime)
            _startDate = value
        End Set
    End Property

    Public Property EndDate() As System.DateTime
        Get
            Return _EndDate

        End Get
        Set(ByVal value As System.DateTime)
            _EndDate = value
        End Set
    End Property

    Public Property product_group() As Integer
        Get
            Return _product_group
        End Get
        Set(ByVal value As Integer)
            _product_group = value
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

    Public Property promo_value() As String
        Get
            Return _promo_value
        End Get
        Set(ByVal value As String)
            _promo_value = value
        End Set
    End Property

    Public Property promo_type() As Integer
        Get
            Return _promo_type
        End Get
        Set(ByVal value As Integer)
            _promo_type = value
        End Set
    End Property

    Public Property Promo_name() As String
        Get
            Return _Promo_name
        End Get
        Set(ByVal value As String)
            _Promo_name = value
        End Set
    End Property

    Public Property product_group1() As Integer
        Get
            Return _product_group1
        End Get
        Set(ByVal value As Integer)
            _product_group1 = value
        End Set
    End Property

    Public Property product_group2() As Integer
        Get
            Return _product_group2
        End Get
        Set(ByVal value As Integer)
            _product_group2 = value
        End Set
    End Property

    Public Property product_group3() As Integer
        Get
            Return _product_group3
        End Get
        Set(ByVal value As Integer)
            _product_group3 = value
        End Set
    End Property

    Public Property Voucher_Type() As String
        Get
            Return _Voucher_Type
        End Get
        Set(ByVal value As String)
            _Voucher_Type = value
        End Set
    End Property
    Public Property Voucher_Code() As String
        Get
            Return _Voucher_Code
        End Get
        Set(ByVal value As String)
            _Voucher_Code = value
        End Set
    End Property

    Public Property Voucher_Start() As Integer
        Get
            Return _Voucher_start
        End Get
        Set(ByVal value As Integer)
            _Voucher_start = value
        End Set
    End Property


    Public Property Voucher_end() As Integer
        Get
            Return _Voucher_end
        End Get
        Set(ByVal value As Integer)
            _Voucher_end = value
        End Set
    End Property
    Public Property Online_Coupon() As String
        Get
            Return _Online_Coupon
        End Get
        Set(ByVal value As String)
            _Online_Coupon = value
        End Set
    End Property

    Public Property buy1_Get1() As Integer
        Get
            Return _buy1_Get1
        End Get
        Set(ByVal value As Integer)
            _buy1_Get1 = value
        End Set
    End Property
#End Region

    Public Function Insert() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_promotion_id", table_promotion_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id1", product_id1, SqlDbType.Int)
            oColSqlparram.Add("@product_id2", product_id2, SqlDbType.Int)
            oColSqlparram.Add("@product_id3", product_id3, SqlDbType.Int)
            oColSqlparram.Add("@size_id", size_id, SqlDbType.Int)
            oColSqlparram.Add("@size_id1", size_id1, SqlDbType.Int)
            oColSqlparram.Add("@size_id2", size_id2, SqlDbType.Int)
            oColSqlparram.Add("@size_id3", size_id3, SqlDbType.Int)
            oColSqlparram.Add("@price_id", price_id, SqlDbType.Int)
            oColSqlparram.Add("@Discount_type", Discount_type, SqlDbType.Int)
            oColSqlparram.Add("@Discount", Discount)
            oColSqlparram.Add("@Recurrence", Recurrence, SqlDbType.Int)
            oColSqlparram.Add("@Month_flag", Month_flag, SqlDbType.Int)
            oColSqlparram.Add("@Months", Months)
            oColSqlparram.Add("@Week_flag", Week_flag, SqlDbType.Int)
            oColSqlparram.Add("@Weeks", Weeks)
            oColSqlparram.Add("@Days", Days)
            oColSqlparram.Add("@on_day", on_day)
            oColSqlparram.Add("@Recurrence_flag", Recurrence_flag, SqlDbType.Int)
            oColSqlparram.Add("@start_time", start_time)
            oColSqlparram.Add("@start_time1", start_time1)
            oColSqlparram.Add("@start_time2", start_time2)
            oColSqlparram.Add("@start_time3", start_time3)
            oColSqlparram.Add("@end_time", end_time)
            oColSqlparram.Add("@end_time1", end_time1)
            oColSqlparram.Add("@end_time2", end_time2)
            oColSqlparram.Add("@end_time3", end_time3)
            oColSqlparram.Add("@is_endless", is_endless, SqlDbType.Int)
            oColSqlparram.Add("@combo_flag", combo_flag, SqlDbType.Int)
            oColSqlparram.Add("@combo_product_id", combo_product_id)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@startDate", startDate, SqlDbType.DateTime)
            oColSqlparram.Add("@EndDate", EndDate, SqlDbType.DateTime)
            oColSqlparram.Add("@product_group", product_group, SqlDbType.Int)
            oColSqlparram.Add("@product_group1", product_group1, SqlDbType.Int)
            oColSqlparram.Add("@product_group2", product_group2, SqlDbType.Int)
            oColSqlparram.Add("@product_group3", product_group3, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@duration_flag", duration_flag, SqlDbType.Int)
            oColSqlparram.Add("@no_of_product", no_of_product, SqlDbType.Int)
            oColSqlparram.Add("@Promo_name", Promo_name)
            oColSqlparram.Add("@Voucher_Type", Voucher_Type)
            oColSqlparram.Add("@Voucher_Code", Voucher_Code)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Online_Coupon", Online_Coupon)
            oColSqlparram.Add("@mixnmatch", mixnmatch)
            oColSqlparram.Add("@flatamount", isFlatAmount)
            oColSqlparram.Add("@buy1_Get1", buy1_Get1)
            oColSqlparram.Add("@freeProduct", freeProduct, SqlDbType.Int)
            oColSqlparram.Add("@freeProductAmount", freeProductAmount, SqlDbType.Decimal)
            oColSqlparram.Add("@allfreeProduct", AllfreeProduct, SqlDbType.Int)
            oColSqlparram.Add("@EachSpend", EachSpend, SqlDbType.Int)
            oColSqlparram.Add("@is_BarStockExchange", isbarStock, SqlDbType.Int)
            oColSqlparram.Add("@FreeQty", FreeQty, SqlDbType.Decimal)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Promotion", oColSqlparram)
            Return dtlogin.Rows(0)("table_promotion_id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_promotion_id", table_promotion_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id1", product_id1, SqlDbType.Int)
            oColSqlparram.Add("@product_id2", product_id2, SqlDbType.Int)
            oColSqlparram.Add("@product_id3", product_id3, SqlDbType.Int)
            oColSqlparram.Add("@size_id", size_id, SqlDbType.Int)
            oColSqlparram.Add("@size_id1", size_id1, SqlDbType.Int)
            oColSqlparram.Add("@size_id2", size_id2, SqlDbType.Int)
            oColSqlparram.Add("@size_id3", size_id3, SqlDbType.Int)
            oColSqlparram.Add("@price_id", price_id, SqlDbType.Int)
            oColSqlparram.Add("@Discount_type", Discount_type, SqlDbType.Int)
            oColSqlparram.Add("@Discount", Discount)
            oColSqlparram.Add("@Recurrence", Recurrence, SqlDbType.Int)
            oColSqlparram.Add("@Month_flag", Month_flag, SqlDbType.Int)
            oColSqlparram.Add("@Months", Months)
            oColSqlparram.Add("@Week_flag", Week_flag, SqlDbType.Int)
            oColSqlparram.Add("@Weeks", Weeks)
            oColSqlparram.Add("@Days", Days)
            oColSqlparram.Add("@on_day", on_day)
            oColSqlparram.Add("@Recurrence_flag", Recurrence_flag, SqlDbType.Int)
            oColSqlparram.Add("@start_time", start_time)
            oColSqlparram.Add("@start_time1", start_time1)
            oColSqlparram.Add("@start_time2", start_time2)
            oColSqlparram.Add("@start_time3", start_time3)
            oColSqlparram.Add("@end_time", end_time)
            oColSqlparram.Add("@end_time1", end_time1)
            oColSqlparram.Add("@end_time2", end_time2)
            oColSqlparram.Add("@end_time3", end_time3)
            oColSqlparram.Add("@is_endless", is_endless, SqlDbType.Int)
            oColSqlparram.Add("@combo_flag", combo_flag, SqlDbType.Int)
            oColSqlparram.Add("@combo_product_id", combo_product_id)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@startDate", startDate, SqlDbType.DateTime)
            oColSqlparram.Add("@EndDate", EndDate, SqlDbType.DateTime)
            oColSqlparram.Add("@product_group", product_group, SqlDbType.Int)
            oColSqlparram.Add("@product_group1", product_group1, SqlDbType.Int)
            oColSqlparram.Add("@product_group2", product_group2, SqlDbType.Int)
            oColSqlparram.Add("@product_group3", product_group3, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@duration_flag", duration_flag, SqlDbType.Int)
            oColSqlparram.Add("@no_of_product", no_of_product, SqlDbType.Int)
            oColSqlparram.Add("@Promo_name", Promo_name)
            oColSqlparram.Add("@Voucher_Type", Voucher_Type)
            oColSqlparram.Add("@Voucher_Code", Voucher_Code)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@Online_Coupon", Online_Coupon)
            oColSqlparram.Add("@mixnmatch", mixnmatch)
            oColSqlparram.Add("@flatamount", isFlatAmount)
            oColSqlparram.Add("@buy1_Get1", buy1_Get1)
            oColSqlparram.Add("@freeProduct", freeProduct, SqlDbType.Int)
            oColSqlparram.Add("@freeProductAmount", freeProductAmount, SqlDbType.Decimal)
            oColSqlparram.Add("@allfreeProduct", AllfreeProduct, SqlDbType.Int)
            oColSqlparram.Add("@EachSpend", EachSpend, SqlDbType.Int)
            oColSqlparram.Add("@is_BarStockExchange", isbarStock, SqlDbType.Int)
            oColSqlparram.Add("@FreeQty", FreeQty, SqlDbType.Decimal)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Promotion", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_promotion_id", table_promotion_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id1", product_id1, SqlDbType.Int)
            oColSqlparram.Add("@product_id2", product_id2, SqlDbType.Int)
            oColSqlparram.Add("@product_id3", product_id3, SqlDbType.Int)
            oColSqlparram.Add("@size_id", size_id, SqlDbType.Int)
            oColSqlparram.Add("@size_id1", size_id1, SqlDbType.Int)
            oColSqlparram.Add("@size_id2", size_id2, SqlDbType.Int)
            oColSqlparram.Add("@size_id3", size_id3, SqlDbType.Int)
            oColSqlparram.Add("@price_id", price_id, SqlDbType.Int)
            oColSqlparram.Add("@Discount_type", Discount_type, SqlDbType.Int)
            oColSqlparram.Add("@Discount", Discount)
            oColSqlparram.Add("@Recurrence", Recurrence, SqlDbType.Int)
            oColSqlparram.Add("@Month_flag", Month_flag, SqlDbType.Int)
            oColSqlparram.Add("@Months", Months)
            oColSqlparram.Add("@Week_flag", Week_flag, SqlDbType.Int)
            oColSqlparram.Add("@Weeks", Weeks)
            oColSqlparram.Add("@Days", Days)
            oColSqlparram.Add("@on_day", on_day)
            oColSqlparram.Add("@Recurrence_flag", Recurrence_flag, SqlDbType.Int)
            oColSqlparram.Add("@start_time", start_time)
            oColSqlparram.Add("@start_time1", start_time1)
            oColSqlparram.Add("@start_time2", start_time2)
            oColSqlparram.Add("@start_time3", start_time3)
            oColSqlparram.Add("@end_time", end_time)
            oColSqlparram.Add("@end_time1", end_time1)
            oColSqlparram.Add("@end_time2", end_time2)
            oColSqlparram.Add("@end_time3", end_time3)
            oColSqlparram.Add("@is_endless", is_endless, SqlDbType.Int)
            oColSqlparram.Add("@combo_flag", combo_flag, SqlDbType.Int)
            oColSqlparram.Add("@combo_product_id", combo_product_id)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@startDate", startDate, SqlDbType.DateTime)
            oColSqlparram.Add("@EndDate", EndDate, SqlDbType.DateTime)
            oColSqlparram.Add("@product_group", product_group, SqlDbType.Int)
            oColSqlparram.Add("@product_group1", product_group1, SqlDbType.Int)
            oColSqlparram.Add("@product_group2", product_group2, SqlDbType.Int)
            oColSqlparram.Add("@product_group3", product_group3, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@duration_flag", duration_flag, SqlDbType.Int)
            oColSqlparram.Add("@no_of_product", no_of_product, SqlDbType.Int)
            oColSqlparram.Add("@Promo_name", Promo_name)
            oColSqlparram.Add("@Voucher_Type", Voucher_Type)
            oColSqlparram.Add("@Voucher_Code", Voucher_Code)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@Online_Coupon", Online_Coupon)
            oColSqlparram.Add("@mixnmatch", mixnmatch, SqlDbType.Int)
            oColSqlparram.Add("@flatamount", isFlatAmount, SqlDbType.Int)
            oColSqlparram.Add("@buy1_Get1", buy1_Get1, SqlDbType.Int)
            oColSqlparram.Add("@freeProduct", freeProduct, SqlDbType.Int)
            oColSqlparram.Add("@freeProductAmount", freeProductAmount, SqlDbType.Decimal)
            oColSqlparram.Add("@allfreeProduct", AllfreeProduct, SqlDbType.Int)
            oColSqlparram.Add("@EachSpend", EachSpend, SqlDbType.Int)
            oColSqlparram.Add("@is_BarStockExchange", isbarStock, SqlDbType.Int)
            oColSqlparram.Add("@FreeQty", FreeQty, SqlDbType.Decimal)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Promotion", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Product_For_Promotoin]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Pro_Id", Pro_Id)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_grid_For_Promotion", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Insert_PromotionType() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_promotion_id", table_promotion_id, SqlDbType.Int)
            oColSqlparram.Add("@promo_type", promo_type, SqlDbType.Int)
            oColSqlparram.Add("@promo_value", promo_value)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_PromotionType", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@table_promotion_id", table_promotion_id, SqlDbType.Int)
        oColSqlparram.Add("@Promo_name", Promo_name)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Promotion", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [BindPromotion]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Vouchers", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Delete_Edit() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_promotion_id", table_promotion_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Delete_Promotion_Edit", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Insert_Voucher]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@voucher_code", Voucher_Code)
        oColSqlparram.Add("@voucher_start", Voucher_Start, SqlDbType.Int)
        oColSqlparram.Add("@voucher_end", Voucher_end, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_IssueVoucher", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Get_Voucher]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_IssueVoucher", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Delete_Voucher]() As Boolean
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Voucher_Code", Voucher_Code, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("delete_IssueVoucher", oColSqlparram)

        Return True
    End Function
    Public Function [check_Duplicate_Promoname]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@table_promotion_id", table_promotion_id, SqlDbType.Int)
        oColSqlparram.Add("@Promo_name", Promo_name)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("check_Duplicate_Promoname", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_urlforsales() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        'oColSqlparram.Add("@Date", created_date)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Api_requestSales", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [check_Duplicate_Promoname_Edit]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@promotion_id", promotion_id)
        oColSqlparram.Add("@Promo_name", Promo_name)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("check_Duplicate_Promoname_Edit", oColSqlparram)

        Return dtlogin
    End Function


End Class

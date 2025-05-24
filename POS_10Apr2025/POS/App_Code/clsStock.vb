Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsStock
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _stock_id As Integer
    Private _cmp_id As Integer
    Private _location_id As Integer
    Private _product_group_id As Integer
    Private _product_id As Integer
    Private _total_stock As Decimal
    Private _total_unit_id As Integer
    Private _damage_stock As Integer
    Private _Tran_Type As String
    Private _damage_unit_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _stock_receive_date As System.DateTime
    Private _tax As Integer
    Private _Supplier_id As String
    Private _price As Decimal
    Private _Login_id As Integer
    Private _Ip_Address As String
    Private _SingleRecord As Integer
    Private _stk_rec_id As Integer
    Private _form_name As String
    Private _stk_det_id As Integer
    Private _quantity As Integer
    Private _stock_detail As Integer
    Private _is_damage As Integer
    Private _receipt_number As String
    Private _supplier_code As String
    Private _total_price As Decimal
    Private _inc_exe_tax As Integer
    Private _tax_amount As Decimal
    Private _final_price As Decimal
    Private _stockon_hand_qty As Decimal
    Private _count_qty As Decimal
    Private _result_qty As Integer
    Private _stk_chg_rec_id As Integer
    Private _is_finalsubmit As Integer
    Private _stock_Desc As String
    Private _tax_id As Integer
    Private _templete_name As String
    Private _Templete_id As Integer

    Private _FromDate As System.DateTime
    Private _ToDate As System.DateTime
    Private _duration As String
    Private _BuyingSizeCost As Decimal
    Private _cost_id As Integer
    Private objclsstock As clsstock

#End Region

#Region "Public Property"

    Public Property cost_id() As Integer
        Get
            Return _cost_id
        End Get
        Set(ByVal value As Integer)
            _cost_id = value
        End Set
    End Property

    Public Property BuyingSizeCost() As Decimal
        Get
            Return _BuyingSizeCost
        End Get
        Set(ByVal value As Decimal)
            _BuyingSizeCost = value
        End Set
    End Property

    Public Property duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
        End Set
    End Property

    Public Property FromDate() As System.DateTime
        Get
            Return _FromDate
        End Get
        Set(ByVal value As System.DateTime)
            _FromDate = value
        End Set
    End Property
    Public Property ToDate() As System.DateTime
        Get
            Return _ToDate
        End Get
        Set(ByVal value As System.DateTime)
            _ToDate = value
        End Set

    End Property
    Public Property templete_name() As String
        Get
            Return _templete_name
        End Get
        Set(ByVal value As String)
            _templete_name = value
        End Set
    End Property
    Public Property Templete_id() As Integer
        Get
            Return _Templete_id
        End Get
        Set(ByVal value As Integer)
            _Templete_id = value
        End Set
    End Property

    Public Property tax_id() As Integer
        Get
            Return _tax_id
        End Get
        Set(ByVal value As Integer)
            _tax_id = value
        End Set
    End Property
    Public Property stock_id() As Integer
        Get
            Return _stock_id
        End Get
        Set(ByVal value As Integer)
            _stock_id = value
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
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property product_group_id() As Integer
        Get
            Return _product_group_id
        End Get
        Set(ByVal value As Integer)
            _product_group_id = value
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
    Public Property total_stock() As Decimal
        Get
            Return _total_stock
        End Get
        Set(ByVal value As Decimal)
            _total_stock = value
        End Set
    End Property
    Public Property total_unit_id() As Integer
        Get
            Return _total_unit_id
        End Get
        Set(ByVal value As Integer)
            _total_unit_id = value
        End Set
    End Property

    Public Property damage_stock() As Integer
        Get
            Return _damage_stock
        End Get
        Set(ByVal value As Integer)
            _damage_stock = value
        End Set
    End Property
    Public Property damage_unit_id() As Integer
        Get
            Return _damage_unit_id
        End Get
        Set(ByVal value As Integer)
            _damage_unit_id = value
        End Set
    End Property
    Public Property stock_receive_date() As System.DateTime
        Get
            Return _stock_receive_date
        End Get
        Set(ByVal value As System.DateTime)
            _stock_receive_date = value
        End Set
    End Property
    Public Property tax() As Integer
        Get
            Return _tax
        End Get
        Set(ByVal value As Integer)
            _tax = value
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

    Public Property supplier_id() As String
        Get
            Return _Supplier_id
        End Get
        Set(ByVal value As String)
            _Supplier_id = value
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
    Public Property stk_rec_id() As Integer
        Get
            Return _stk_rec_id
        End Get
        Set(ByVal value As Integer)
            _stk_rec_id = value
        End Set
    End Property
    Public Property form_name() As String
        Get
            Return _form_name
        End Get
        Set(ByVal value As String)
            _form_name = value
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

    Public Property ip_address() As String
        Get
            Return _Ip_Address
        End Get
        Set(ByVal value As String)
            _Ip_Address = value
        End Set
    End Property
    Public Property login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
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

    Public Property stk_det_id() As Integer
        Get
            Return _stk_det_id
        End Get
        Set(ByVal value As Integer)
            _stk_det_id = value
        End Set
    End Property
    Public Property quantity() As Integer
        Get
            Return _quantity
        End Get
        Set(ByVal value As Integer)
            _quantity = value
        End Set
    End Property
    Public Property is_damage() As Integer
        Get
            Return _is_damage
        End Get
        Set(ByVal value As Integer)
            _is_damage = value
        End Set
    End Property
    Public Property stock_detail() As Integer
        Get
            Return _stock_detail
        End Get
        Set(ByVal value As Integer)
            _stock_detail = value
        End Set
    End Property

    Public Property supplier_code() As String
        Get
            Return _supplier_code
        End Get
        Set(ByVal value As String)
            _supplier_code = value
        End Set
    End Property

    Public Property receipt_number() As String
        Get
            Return _receipt_number
        End Get
        Set(ByVal value As String)
            _receipt_number = value
        End Set
    End Property

    Public Property total_price() As Decimal
        Get
            Return _total_price
        End Get
        Set(ByVal value As Decimal)
            _total_price = value
        End Set
    End Property
    Public Property inc_exe_tax() As Integer
        Get
            Return _inc_exe_tax
        End Get
        Set(ByVal value As Integer)
            _inc_exe_tax = value
        End Set
    End Property
    Public Property tax_amount() As Decimal
        Get
            Return _tax_amount
        End Get
        Set(ByVal value As Decimal)
            _tax_amount = value
        End Set
    End Property
    Public Property final_price() As Decimal
        Get
            Return _final_price
        End Get
        Set(ByVal value As Decimal)
            _final_price = value
        End Set
    End Property
    Public Property Stockon_Hand_Qty() As Decimal
        Get
            Return _stockon_hand_qty
        End Get
        Set(ByVal value As Decimal)
            _stockon_hand_qty = value
        End Set
    End Property
    Public Property Count_Qty() As Decimal
        Get
            Return _count_qty
        End Get
        Set(ByVal value As Decimal)
            _count_qty = value
        End Set
    End Property
    Public Property Result_Qty() As Integer
        Get
            Return _result_qty
        End Get
        Set(ByVal value As Integer)
            _result_qty = value
        End Set
    End Property


    Public Property Stk_Chg_rec_id() As Integer
        Get
            Return _stk_chg_rec_id
        End Get
        Set(ByVal value As Integer)
            _stk_chg_rec_id = value
        End Set
    End Property

    Public Property is_finalsubmit() As Integer
        Get
            Return _is_finalsubmit
        End Get
        Set(ByVal value As Integer)
            _is_finalsubmit = value
        End Set
    End Property

    Public Property stock_Desc() As String
        Get
            Return _stock_Desc
        End Get
        Set(ByVal value As String)
            _stock_Desc = value
        End Set
    End Property

#End Region

    Public Function Insert_templete_master() As Integer
        Try
            objclsstock = New clsstock()
            objclsstock.cmp_id = cmp_id
            objclsstock.Location_id = Location_id
            objclsstock.login_id = login_id
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.ip_address = ip_address
            objclsstock.templete_name = templete_name
            objclsstock.supplier_code = supplier_code
            objclsstock.supplier_id = supplier_id

            'If objclsstock.Insert_templete_master() Then
            '    Return True
            'End If
            'Return False

            Dim r As Integer
            r = objclsstock.Insert_templete_master()
            Return r

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_template() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.cmp_id = cmp_id
            objclsstock.Location_id = Location_id
            objclsstock.login_id = login_id
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.ip_address = ip_address
            objclsstock.templete_name = templete_name
            objclsstock.supplier_code = supplier_code
            objclsstock.supplier_id = supplier_id
            objclsstock.Templete_id = Templete_id

            If objclsstock.Delete_template() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_templete() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            objclsstock.price = price
            objclsstock.tax = tax
            objclsstock.tax_amount = tax_amount
            objclsstock.total_price = total_price
            objclsstock.final_price = final_price
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.Templete_id = Templete_id
            objclsstock.is_damage = is_damage
            objclsstock.inc_exe_tax = inc_exe_tax
            objclsstock.supplier_code = supplier_code
            objclsstock.supplier_id = supplier_id
            objclsstock.stk_rec_id = stk_rec_id

            objclsstock.Tran_Type = Tran_Type

            If objclsstock.Insert_templete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_templete() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            objclsstock.price = price
            objclsstock.tax = tax
            objclsstock.tax_amount = tax_amount
            objclsstock.total_price = total_price
            objclsstock.final_price = final_price
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.Templete_id = Templete_id
            objclsstock.is_damage = is_damage
            objclsstock.inc_exe_tax = inc_exe_tax
            objclsstock.supplier_code = supplier_code
            objclsstock.supplier_id = supplier_id
            objclsstock.stk_rec_id = stk_rec_id

            objclsstock.Tran_Type = Tran_Type

            If objclsstock.Update_templete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.stock_id = stock_id
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            'objclsstock.damage_stock = damage_stock
            'objclsstock.damage_unit_id = damage_unit_id
            objclsstock.Tran_Type = Tran_Type
            objclsstock.supplier_id = supplier_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.tax = tax
            objclsstock.price = price
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.is_damage = is_damage
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.supplier_code = supplier_code
            objclsstock.total_price = total_price
            objclsstock.final_price = final_price
            objclsstock.inc_exe_tax = inc_exe_tax
            objclsstock.tax_amount = tax_amount
            objclsstock.BuyingSizeCost = BuyingSizeCost
            objclsstock.cost_id = cost_id

            'objclsstock.LAN_NO = LAN_no

            If objclsstock.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.stock_id = stock_id
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            'objclsstock.damage_stock = damage_stock
            'objclsstock.damage_unit_id = damage_unit_id
            objclsstock.Tran_Type = Tran_Type
            objclsstock.supplier_id = supplier_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.tax = tax
            objclsstock.price = price
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.is_damage = is_damage
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.total_price = total_price
            objclsstock.final_price = final_price
            objclsstock.inc_exe_tax = inc_exe_tax
            objclsstock.tax_amount = tax_amount
            objclsstock.supplier_code = supplier_code

            objclsstock.BuyingSizeCost = BuyingSizeCost
            objclsstock.cost_id = cost_id

            If objclsstock.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.stock_id = stock_id
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            'objclsstock.damage_stock = damage_stock
            'objclsstock.damage_unit_id = damage_unit_id
            objclsstock.Tran_Type = Tran_Type
            objclsstock.supplier_id = supplier_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.tax = tax
            objclsstock.price = price
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.is_damage = is_damage
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.total_price = total_price
            objclsstock.final_price = final_price
            objclsstock.inc_exe_tax = inc_exe_tax
            objclsstock.supplier_code = supplier_code
            objclsstock.tax_amount = tax_amount

            objclsstock.BuyingSizeCost = BuyingSizeCost
            objclsstock.cost_id = cost_id

            If objclsstock.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_by_cost() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.cost_id = cost_id
            dt = objclsstock.Select_by_cost()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.stock_id = stock_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_template() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.Templete_id = Templete_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.Select_template()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function check_template_name() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.templete_name = templete_name
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.check_template_name()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            'objclsstock.stock_id = stock_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_dateWise() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.cmp_id = cmp_id
            objclsstock.FromDate = FromDate
            objclsstock.ToDate = ToDate
            objclsstock.duration = duration
            dt = objclsstock.Select_dateWise()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_SizeNPrice_By_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.product_id = product_id
            objclsstock.cmp_id = cmp_id
            objclsstock.SingleRecord = SingleRecord
            dt = objclsstock.[Select_SizeNPrice_By_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsUpdDel_SizeNPrice() As Integer

        Try
            objclsstock = New clsstock()
            objclsstock.product_id = product_id

            objclsstock.Location_id = Location_id

            objclsstock.price = price
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.cmp_id = cmp_id
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.supplier_id = supplier_id
            objclsstock.receipt_number = receipt_number
            objclsstock.supplier_code = supplier_code
            objclsstock.Tran_Type = Tran_Type
            objclsstock.is_finalsubmit = is_finalsubmit

            Dim r As Integer
            r = objclsstock.InsUpdDel_SizeNPrice()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Upd_SizeNPrice() As Integer

        Try
            objclsstock = New clsstock()
            objclsstock.product_id = product_id
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.Location_id = Location_id

            objclsstock.price = price
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.cmp_id = cmp_id
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.supplier_id = supplier_id
            objclsstock.receipt_number = receipt_number
            objclsstock.supplier_code = supplier_code
            objclsstock.Tran_Type = Tran_Type
            objclsstock.is_finalsubmit = is_finalsubmit

            Dim r As Integer
            r = objclsstock.Upd_SizeNPrice()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select1]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.cmp_id = cmp_id
            objclsstock.form_name = form_name
            dt = objclsstock.[Select1]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectAll_template() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.SelectAll_template()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Ins_stock_detail() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.stk_det_id = stk_det_id
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            objclsstock.supplier_id = supplier_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.login_id = login_id
            objclsstock.quantity = quantity
            objclsstock.ip_address = ip_address
            objclsstock.Tran_Type = Tran_Type
            objclsstock.stock_detail = stock_detail
            objclsstock.Stockon_Hand_Qty = Stockon_Hand_Qty
            objclsstock.Count_Qty = Count_Qty
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id
            If objclsstock.Ins_stock_detail() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Upd_stock_detail() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.stk_det_id = stk_det_id
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            objclsstock.supplier_id = supplier_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.login_id = login_id
            objclsstock.quantity = quantity
            objclsstock.ip_address = ip_address
            objclsstock.Tran_Type = Tran_Type
            objclsstock.stock_detail = stock_detail
            objclsstock.Stockon_Hand_Qty = Stockon_Hand_Qty
            objclsstock.Count_Qty = Count_Qty
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id
            If objclsstock.Upd_stock_detail() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelecStockDetail]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            'objclsstock.stock_id = stock_id
            objclsstock.cmp_id = cmp_id
            objclsstock.FromDate = FromDate
            objclsstock.ToDate = ToDate
            objclsstock.duration = duration
            dt = objclsstock.selectstk_detals()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function select_tax() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.product_id = product_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.Select_tax()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectTemplateByCmp() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.SelectTemplateByCmp()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function select_tax_deatil() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.tax_id = tax_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.select_tax_deatil()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectTranDetail]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.product_id = product_id
            objclsstock.cmp_id = cmp_id
            objclsstock.Location_id = Location_id
            dt = objclsstock.SelectTranDetail()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function download_stock_detail() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.cmp_id = cmp_id
            objclsstock.Location_id = Location_id
            dt = objclsstock.download_stock_detail()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function del_stock_detail() As Boolean
        Try
            objclsstock = New clsstock()
            objclsstock.stk_det_id = stk_det_id
            objclsstock.Location_id = Location_id
            objclsstock.cmp_id = cmp_id
            objclsstock.product_group_id = product_group_id
            objclsstock.product_id = product_id
            objclsstock.total_stock = total_stock
            objclsstock.total_unit_id = total_unit_id
            objclsstock.supplier_id = supplier_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.login_id = login_id
            objclsstock.quantity = quantity
            objclsstock.ip_address = ip_address
            objclsstock.Tran_Type = Tran_Type
            objclsstock.stock_detail = stock_detail
            objclsstock.Stockon_Hand_Qty = Stockon_Hand_Qty
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id

            objclsstock.Count_Qty = Count_Qty
            If objclsstock.del_stock_detail() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function InsUpdDel_ChgRec() As Integer
        Try
            objclsstock = New clsstock()
            objclsstock.Location_id = Location_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.cmp_id = cmp_id
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.receipt_number = receipt_number
            objclsstock.stock_detail = stock_detail
            objclsstock.Tran_Type = Tran_Type
            objclsstock.is_finalsubmit = is_finalsubmit
            objclsstock.stock_Desc = stock_Desc
            Dim r As Integer
            r = objclsstock.InsUpdDel_ChgRec()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Upd_ChgRec() As Integer
        Try
            objclsstock = New clsstock()
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id
            objclsstock.Location_id = Location_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.cmp_id = cmp_id
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.receipt_number = receipt_number
            objclsstock.stock_detail = stock_detail
            objclsstock.Tran_Type = Tran_Type
            objclsstock.is_finalsubmit = is_finalsubmit
            objclsstock.stock_Desc = stock_Desc
            Dim r As Integer
            r = objclsstock.Upd_ChgRec()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function




    Public Function [Selectproduct]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.Selectproduct()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Selectdetailrec]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.Selectdetailrec()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function check_pending_stock() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()

            dt = objclsstock.check_pending_stock()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Selectdetailrec_Main_Detail]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.Selectdetailrec_Main_Detail()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function





    Public Function deletereceipt() As Integer

        Try
            objclsstock = New clsstock()
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.product_id = product_id
            objclsstock.Location_id = Location_id
            objclsstock.price = price
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.cmp_id = cmp_id
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.supplier_id = supplier_id
            objclsstock.receipt_number = receipt_number
            objclsstock.supplier_code = supplier_code
            objclsstock.Tran_Type = Tran_Type
            objclsstock.is_finalsubmit = is_finalsubmit

            If objclsstock.deletereceipt() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Del_ChgRec() As Integer
        Try
            objclsstock = New clsstock()
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id
            objclsstock.Location_id = Location_id
            objclsstock.stock_receive_date = stock_receive_date
            objclsstock.cmp_id = cmp_id
            objclsstock.login_id = login_id
            objclsstock.ip_address = ip_address
            objclsstock.receipt_number = receipt_number
            objclsstock.stock_detail = stock_detail
            objclsstock.Tran_Type = Tran_Type
            objclsstock.is_finalsubmit = is_finalsubmit
            If objclsstock.Del_ChgRec() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectproductDetail_By_cmp_stk_rec_Id]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.SelectproductDetail_By_cmp_stk_rec_Id()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelecStockDetail_Edit]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.Stk_Chg_rec_id = Stk_Chg_rec_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.selectStock_Detail_Edit()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelecStockManagementDetail_Edit]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.stk_rec_id = stk_rec_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.selectStockManagement_Detail_Edit()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectStockTemplateDetail_Edit() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsstock()
            objclsstock.Templete_id = Templete_id
            objclsstock.cmp_id = cmp_id
            dt = objclsstock.SelectStockTemplateDetail_Edit()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsstock
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _stock_id As Integer
    Private _cmp_id As Integer
    Private _location_id As Integer
    Private _product_group_id As Integer
    Private _product_id As Integer
    Private _total_stock As Decimal
    Private _total_unit_id As Integer
    Private _damage_stock As Integer

    Private _Tran_Type As String
    Private _damage_unit_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _stock_receive_date As System.DateTime
    Private _tax As Integer
    Private _Supplier_id As String
    Private _price As Decimal
    Private _Login_id As Integer
    Private _Ip_Address As String
    Private _SingleRecord As Integer
    Private _stk_rec_id As Integer
    Private _form_name As String
    Private _stk_det_id As Integer
    Private _quantity As Integer
    Private _stock_detail As Integer
    Private _is_damage As Integer
    Private _receipt_number As String
    Private _supplier_code As String
    Private _total_price As Decimal
    Private _inc_exe_tax As Integer
    Private _tax_amount As Decimal
    Private _final_price As Decimal
    Private _stockon_hand_qty As Decimal
    Private _count_qty As Decimal
    Private _result_qty As Integer
    Private _stk_chg_rec_id As Integer
    Private _is_finalsubmit As Integer
    Private _stock_Desc As String
    Private _tax_id As Integer
    Private _templete_name As String
    Private _Templete_id As Integer

    Private _FromDate As System.DateTime
    Private _ToDate As System.DateTime
    Private _duration As String
    Private _BuyingSizeCost As Decimal
    Private _cost_id As Integer

    'Private _LAN_NO As String
    Private objclsstock As clsstock

#End Region

#Region "Public Property"

    Public Property cost_id() As Integer
        Get
            Return _cost_id
        End Get
        Set(ByVal value As Integer)
            _cost_id = value
        End Set
    End Property

    Public Property BuyingSizeCost() As Decimal
        Get
            Return _BuyingSizeCost
        End Get
        Set(ByVal value As Decimal)
            _BuyingSizeCost = value
        End Set
    End Property

    Public Property duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
        End Set
    End Property


    Public Property FromDate() As System.DateTime
        Get
            Return _FromDate
        End Get
        Set(ByVal value As System.DateTime)
            _FromDate = value
        End Set
    End Property
    Public Property ToDate() As System.DateTime
        Get
            Return _ToDate
        End Get
        Set(ByVal value As System.DateTime)
            _ToDate = value
        End Set

    End Property

    Public Property templete_name() As String
        Get
            Return _templete_name
        End Get
        Set(ByVal value As String)
            _templete_name = value
        End Set
    End Property

    Public Property Templete_id() As Integer
        Get
            Return _Templete_id
        End Get
        Set(ByVal value As Integer)
            _Templete_id = value
        End Set
    End Property

    Public Property tax_id() As Integer
        Get
            Return _tax_id
        End Get
        Set(ByVal value As Integer)
            _tax_id = value
        End Set
    End Property
    Public Property stock_id() As Integer
        Get
            Return _stock_id
        End Get
        Set(ByVal value As Integer)
            _stock_id = value
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
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property product_group_id() As Integer
        Get
            Return _product_group_id
        End Get
        Set(ByVal value As Integer)
            _product_group_id = value
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
    Public Property total_stock() As Decimal
        Get
            Return _total_stock
        End Get
        Set(ByVal value As Decimal)
            _total_stock = value
        End Set
    End Property
    Public Property total_unit_id() As Integer
        Get
            Return _total_unit_id
        End Get
        Set(ByVal value As Integer)
            _total_unit_id = value
        End Set
    End Property

    Public Property is_damage() As Integer
        Get
            Return _is_damage
        End Get
        Set(ByVal value As Integer)
            _is_damage = value
        End Set
    End Property
    'Public Property damage_unit_id() As Integer
    '    Get
    '        Return _damage_unit_id
    '    End Get
    '    Set(ByVal value As Integer)
    '        _damage_unit_id = value
    '    End Set
    'End Property
    Public Property stock_receive_date() As System.DateTime
        Get
            Return _stock_receive_date
        End Get
        Set(ByVal value As System.DateTime)
            _stock_receive_date = value
        End Set
    End Property
    Public Property tax() As Integer
        Get
            Return _tax
        End Get
        Set(ByVal value As Integer)
            _tax = value
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

    Public Property supplier_id() As String
        Get
            Return _Supplier_id
        End Get
        Set(ByVal value As String)
            _Supplier_id = value
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

    Public Property ip_address() As String
        Get
            Return _Ip_Address
        End Get
        Set(ByVal value As String)
            _Ip_Address = value
        End Set
    End Property
    Public Property login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
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
    Public Property SingleRecord() As Integer
        Get
            Return _SingleRecord
        End Get
        Set(ByVal value As Integer)
            _SingleRecord = value
        End Set
    End Property

    Public Property stk_rec_id() As Integer
        Get
            Return _stk_rec_id
        End Get
        Set(ByVal value As Integer)
            _stk_rec_id = value
        End Set
    End Property
    Public Property form_name() As String
        Get
            Return _form_name
        End Get
        Set(ByVal value As String)
            _form_name = value
        End Set
    End Property

    Public Property stk_det_id() As Integer
        Get
            Return _stk_det_id
        End Get
        Set(ByVal value As Integer)
            _stk_det_id = value
        End Set
    End Property
    Public Property quantity() As Integer
        Get
            Return _quantity
        End Get
        Set(ByVal value As Integer)
            _quantity = value
        End Set
    End Property

    Public Property stock_detail() As Integer
        Get
            Return _stock_detail
        End Get
        Set(ByVal value As Integer)
            _stock_detail = value
        End Set
    End Property

    Public Property receipt_number() As String
        Get
            Return _receipt_number
        End Get
        Set(ByVal value As String)
            _receipt_number = value
        End Set
    End Property

    Public Property supplier_code() As String
        Get
            Return _supplier_code
        End Get
        Set(ByVal value As String)
            _supplier_code = value
        End Set
    End Property
    Public Property total_price() As Decimal
        Get
            Return _total_price
        End Get
        Set(ByVal value As Decimal)
            _total_price = value
        End Set
    End Property
    Public Property inc_exe_tax() As Integer
        Get
            Return _inc_exe_tax
        End Get
        Set(ByVal value As Integer)
            _inc_exe_tax = value
        End Set
    End Property
    Public Property tax_amount() As Decimal
        Get
            Return _tax_amount
        End Get
        Set(ByVal value As Decimal)
            _tax_amount = value
        End Set
    End Property
    Public Property final_price() As Decimal
        Get
            Return _final_price
        End Get
        Set(ByVal value As Decimal)
            _final_price = value
        End Set
    End Property
    Public Property Stockon_Hand_Qty() As Decimal
        Get
            Return _stockon_hand_qty
        End Get
        Set(ByVal value As Decimal)
            _stockon_hand_qty = value
        End Set
    End Property
    Public Property Count_Qty() As Decimal
        Get
            Return _count_qty
        End Get
        Set(ByVal value As Decimal)
            _count_qty = value
        End Set
    End Property
    Public Property Result_Qty() As Integer
        Get
            Return _result_qty
        End Get
        Set(ByVal value As Integer)
            _result_qty = value
        End Set
    End Property

    Public Property Stk_Chg_rec_id() As Integer
        Get
            Return _stk_chg_rec_id
        End Get
        Set(ByVal value As Integer)
            _stk_chg_rec_id = value
        End Set
    End Property

    Public Property is_finalsubmit() As Integer
        Get
            Return _is_finalsubmit
        End Get
        Set(ByVal value As Integer)
            _is_finalsubmit = value
        End Set
    End Property

    Public Property stock_Desc() As String
        Get
            Return _stock_Desc
        End Get
        Set(ByVal value As String)
            _stock_Desc = value
        End Set
    End Property
#End Region

    Public Function Insert_templete_master() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@templete_name", templete_name, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier", supplier_id, SqlDbType.NVarChar)

            oColSqlparram.Add("@Tran_Type", "I")

            Dim dt As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Templete_Master", oColSqlparram)
            Return dt.Rows(0)("Templete_id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_template() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@templete_name", templete_name, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@Templete_id", Templete_id, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "D")

            Dim dt As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Templete_Master", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Insert_templete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Decimal)
            oColSqlparram.Add("@unit_id", total_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@tax_id", tax, SqlDbType.Int)
            oColSqlparram.Add("@tax_amount", tax_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@total_price", total_price, SqlDbType.Decimal)
            oColSqlparram.Add("@final_price", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Templete_id", Templete_id, SqlDbType.Int)
            oColSqlparram.Add("@Is_damage", is_damage, SqlDbType.Int)
            oColSqlparram.Add("@inc_exe_tax", inc_exe_tax, SqlDbType.Int)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier_id", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "I")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Management_templete", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_templete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Decimal)
            oColSqlparram.Add("@unit_id", total_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@tax_id", tax, SqlDbType.Int)
            oColSqlparram.Add("@tax_amount", tax_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@total_price", total_price, SqlDbType.Decimal)
            oColSqlparram.Add("@final_price", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Templete_id", Templete_id, SqlDbType.Int)
            oColSqlparram.Add("@Is_damage", is_damage, SqlDbType.Int)
            oColSqlparram.Add("@inc_exe_tax", inc_exe_tax, SqlDbType.Int)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier_id", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "U")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Management_templete", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stock_id", stock_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Decimal)
            oColSqlparram.Add("@total_unit_id", total_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@tax", tax, SqlDbType.Int)
            oColSqlparram.Add("@stock_receive_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@supplier_id", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Is_damage", is_damage, SqlDbType.Int)
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@total_price", total_price, SqlDbType.Decimal)
            oColSqlparram.Add("@inc_exe_tax", inc_exe_tax, SqlDbType.Int)
            oColSqlparram.Add("@tax_amount", tax_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@final_price", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@BuyingSizeCost", BuyingSizeCost, SqlDbType.Decimal)
            oColSqlparram.Add("@cost_id", cost_id, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "I")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Management", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stock_id", stock_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Decimal)
            oColSqlparram.Add("@total_unit_id", total_unit_id, SqlDbType.Int)
            'oColSqlparram.Add("@damage_stock", damage_stock, SqlDbType.Int)
            'oColSqlparram.Add("@damage_unit_id", damage_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@tax", tax, SqlDbType.Int)
            oColSqlparram.Add("@stock_receive_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@supplier_id", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Is_damage", is_damage, SqlDbType.Int)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@total_price", total_price, SqlDbType.Decimal)
            oColSqlparram.Add("@inc_exe_tax", inc_exe_tax, SqlDbType.Int)
            oColSqlparram.Add("@tax_amount", tax_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@final_price", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@BuyingSizeCost", BuyingSizeCost, SqlDbType.Decimal)
            oColSqlparram.Add("@cost_id", cost_id, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Management", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stock_id", stock_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Decimal)
            oColSqlparram.Add("@total_unit_id", total_unit_id, SqlDbType.Int)
            'oColSqlparram.Add("@damage_stock", damage_stock, SqlDbType.Int)
            'oColSqlparram.Add("@damage_unit_id", damage_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@price", price, SqlDbType.Decimal)
            oColSqlparram.Add("@tax", tax, SqlDbType.Int)
            oColSqlparram.Add("@stock_receive_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@supplier_id", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Is_damage", is_damage, SqlDbType.Int)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@total_price", total_price, SqlDbType.Decimal)
            oColSqlparram.Add("@inc_exe_tax", inc_exe_tax, SqlDbType.Int)
            oColSqlparram.Add("@tax_amount", tax_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@final_price", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@BuyingSizeCost", BuyingSizeCost, SqlDbType.Decimal)
            oColSqlparram.Add("@cost_id", cost_id, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Management", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_by_cost() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cost_id", cost_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_Buying_SizeCost", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stock_id", stock_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Management", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_template() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Templete_id", Templete_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Template", oColSqlparram)

        Return dtlogin
    End Function

    Public Function check_template_name() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@templete_name", templete_name)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Check_M_Stock_Template_master", oColSqlparram)

        Return dtlogin
    End Function



    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stock_id", stock_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Management", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_dateWise() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
        oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
        oColSqlparram.Add("@duration", duration)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Management_DateWise", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_SizeNPrice_By_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_grid", oColSqlparram)

        Return dtlogin
    End Function

    Public Function InsUpdDel_SizeNPrice() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@total_amount", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Int)
            oColSqlparram.Add("@supplier", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@receipt_number", receipt_number, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@tran_type", "I")
            oColSqlparram.Add("@is_finalsubmit", is_finalsubmit, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Receipt", oColSqlparram)
            If dtlogin.Rows.Count > 0 Then
                Return dtlogin.Rows(0)("stk_rec_id")
                Return dtlogin.Rows(0)("msg")
            Else
                Return "No Data Found."
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Upd_SizeNPrice() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@total_amount", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Int)
            oColSqlparram.Add("@supplier", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@receipt_number", receipt_number, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@tran_type", "U")
            oColSqlparram.Add("@is_finalsubmit", is_finalsubmit, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Receipt", oColSqlparram)
            If dtlogin.Rows.Count > 0 Then
                Return dtlogin.Rows(0)("stk_rec_id")
                Return dtlogin.Rows(0)("msg")
            Else
                Return "No Data Found."
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select1]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@form_name", form_name)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Autogenerate_Sorting_No_Master", oColSqlparram)

        Return dtlogin
    End Function

    Public Function SelectAll_template() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Template", oColSqlparram)

        Return dtlogin
    End Function


    Public Function Ins_stock_detail() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stk_det_id", stk_det_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@quantity", total_stock, SqlDbType.Decimal)
            oColSqlparram.Add("@unit_id", total_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@stock_detail", stock_detail, SqlDbType.Int)
            oColSqlparram.Add("@stockon_hand_qty", Stockon_Hand_Qty, SqlDbType.Decimal)
            oColSqlparram.Add("@count_qty", Count_Qty, SqlDbType.Decimal)
            oColSqlparram.Add("@stk_chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Upd_stock_detail() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stk_det_id", stk_det_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@quantity", total_stock, SqlDbType.Decimal)
            oColSqlparram.Add("@unit_id", total_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@stock_detail", stock_detail, SqlDbType.Int)
            oColSqlparram.Add("@stockon_hand_qty", Stockon_Hand_Qty, SqlDbType.Decimal)
            oColSqlparram.Add("@count_qty", Count_Qty, SqlDbType.Decimal)
            oColSqlparram.Add("@stk_chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function del_stock_detail() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stk_det_id", stk_det_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@product_group_id", product_group_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@quantity", total_stock, SqlDbType.Int)
            oColSqlparram.Add("@unit_id", total_unit_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@stock_detail", stock_detail, SqlDbType.Int)
            oColSqlparram.Add("@stockon_hand_qty", Stockon_Hand_Qty, SqlDbType.Decimal)
            oColSqlparram.Add("@stk_chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@count_qty", Count_Qty, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function selectstk_detals() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stk_det_id", stock_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@date1", Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt"))
        oColSqlparram.Add("@date2", Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt"))
        oColSqlparram.Add("@duration", duration)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Detail_Datewise", oColSqlparram)
        Return dtlogin
    End Function

    Public Function selectStock_Detail_Edit() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Stk_Chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Detail_Edit", oColSqlparram)
        Return dtlogin
    End Function

    Public Function selectStockManagement_Detail_Edit() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_StockManagement_Detail_Edit", oColSqlparram)
        Return dtlogin
    End Function

    Public Function SelectStockTemplateDetail_Edit() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Templete_id", Templete_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_StockTemplate_Detail_Edit", oColSqlparram)
        Return dtlogin
    End Function


    'Public Function [SelectDeviceByCmp]() As DataTable
    '    Dim ds As New DataSet

    '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '    oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '    Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Device_By_Cmp", oColSqlparram)

    '    Return dtlogin
    'End Function
    'Public Function [SelectDeviceByMachine]() As DataTable
    '    Dim ds As New DataSet

    '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '    oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '    oColSqlparram.Add("@machine_id", stock_id, SqlDbType.Int)
    '    Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Device_By_Machine", oColSqlparram)

    '    Return dtlogin
    'End Function

    Public Function SelectTemplateByCmp() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Templete_by_cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_tax]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_product_For_Tax", oColSqlparram)

        Return dtlogin
    End Function

    Public Function select_tax_deatil() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@tax_id", tax_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Tax_Detail", oColSqlparram)

        Return dtlogin
    End Function

    Public Function SelectTranDetail() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Transaction", oColSqlparram)

        Return dtlogin
    End Function

    Public Function download_stock_detail() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("download_stock_detail", oColSqlparram)

        Return dtlogin
    End Function

    Public Function InsUpdDel_ChgRec() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@receipt_number", receipt_number, SqlDbType.NVarChar)
            oColSqlparram.Add("@stock_detail", stock_detail, SqlDbType.Int)
            oColSqlparram.Add("@tran_type", "I")
            oColSqlparram.Add("@is_finalsubmit", is_finalsubmit, SqlDbType.Int)
            oColSqlparram.Add("@stock_Desc", stock_Desc)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Chg_rec", oColSqlparram)
            If dtlogin.Rows.Count > 0 Then
                Return dtlogin.Rows(0)("stk_chg_rec_id")
                Return dtlogin.Rows(0)("msg")
            Else
                Return "No Data Found."
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Upd_ChgRec() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stk_chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@receipt_number", receipt_number, SqlDbType.NVarChar)
            oColSqlparram.Add("@stock_detail", stock_detail, SqlDbType.Int)
            oColSqlparram.Add("@tran_type", "U")
            oColSqlparram.Add("@is_finalsubmit", is_finalsubmit, SqlDbType.Int)
            oColSqlparram.Add("@stock_Desc", stock_Desc)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Chg_rec", oColSqlparram)
            If dtlogin.Rows.Count > 0 Then
                Return dtlogin.Rows(0)("stk_chg_rec_id")
                Return dtlogin.Rows(0)("msg")
            Else
                Return "No Data Found."
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Selectproduct]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stk_rec_Id", stk_rec_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_stk_rec", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Selectdetailrec]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stk_chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_stk_det", oColSqlparram)

        Return dtlogin
    End Function
    Public Function check_pending_stock() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("p_check_stock_open", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Selectdetailrec_Main_Detail]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stk_chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_stk_det_main_Detail", oColSqlparram)

        Return dtlogin
    End Function



    Public Function deletereceipt() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stk_rec_id", stk_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@total_amount", final_price, SqlDbType.Decimal)
            oColSqlparram.Add("@total_stock", total_stock, SqlDbType.Int)
            oColSqlparram.Add("@supplier", supplier_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@receipt_number", receipt_number, SqlDbType.NVarChar)
            oColSqlparram.Add("@supplier_code", supplier_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@tran_type", "D")
            oColSqlparram.Add("@is_finalsubmit", is_finalsubmit, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Receipt", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Del_ChgRec() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@stk_chg_rec_id", Stk_Chg_rec_id, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", stock_receive_date, SqlDbType.DateTime)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@receipt_number", receipt_number, SqlDbType.NVarChar)
            oColSqlparram.Add("@stock_detail", stock_detail, SqlDbType.Int)
            oColSqlparram.Add("@tran_type", "D")
            oColSqlparram.Add("@is_finalsubmit", is_finalsubmit, SqlDbType.Int)
            oColSqlparram.Add("@stock_Desc", "")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Stock_Chg_rec", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [SelectproductDetail_By_cmp_stk_rec_Id]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@stk_rec_Id", stk_rec_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Stock_Management_Detail_Report", oColSqlparram)

        Return dtlogin
    End Function


End Class

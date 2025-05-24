Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsExpense
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"

    Private _cmp_id As Integer
    Private _Csh_exp_id As Integer
    Private _Bnk_exp_id As Integer
    Private _Bank_Expense_Amount As Integer
    Private _Cash_Expense_Amount As Decimal
    Private _Bank_Expense_Dcptn As String
    Private _Cash_Expense_Dcptn As String
    Private _Tran_Type As Integer
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
    Private _stockon_hand_qty As Integer
    Private _count_qty As Integer
    Private _result_qty As Integer
    Private _stk_chg_rec_id As Integer
    Private _product_id As Integer
    Private _expense_type As Integer
    Private _for_date As DateTime
    Private _Float_value As Decimal

    Private _Banking As Decimal
    Private _Cash_amount As Decimal
    Private _Card_amount As Decimal
    Private _amount_type As Integer
    Private _Machine_id As Integer
    Private _csh_id As Integer
    Private _Location_id As Integer
    Private _start_date As DateTime
    Private _end_date As DateTime
    Private _Duration As String

    Private objclsexp As clsExpense

#End Region

#Region "Public Property"

    Public Property Duration() As String
        Get
            Return _Duration
        End Get
        Set(ByVal value As String)
            _Duration = value
        End Set
    End Property

    Public Property start_date() As System.DateTime
        Get
            Return _start_date

        End Get
        Set(ByVal value As System.DateTime)
            _start_date = value
        End Set
    End Property

    Public Property end_date() As System.DateTime
        Get
            Return _end_date

        End Get
        Set(ByVal value As System.DateTime)
            _end_date = value
        End Set
    End Property


    Public Property Csh_exp_id() As Integer
        Get
            Return _Csh_exp_id
        End Get
        Set(ByVal value As Integer)
            _Csh_exp_id = value
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
    Public Property Bnk_exp_id() As Integer
        Get
            Return _Bnk_exp_id
        End Get
        Set(ByVal value As Integer)
            _Bnk_exp_id = value
        End Set
    End Property
    Public Property Bank_Expense_Amount() As Integer
        Get
            Return Bank_Expense_Amount
        End Get
        Set(ByVal value As Integer)
            _Bank_Expense_Amount = value
        End Set
    End Property
    Public Property Cash_Expense_Amount() As Decimal
        Get
            Return _Cash_Expense_Amount
        End Get
        Set(ByVal value As Decimal)
            _Cash_Expense_Amount = value
        End Set
    End Property
    Public Property Bank_Expense_Dcptn() As String
        Get
            Return _Bank_Expense_Dcptn
        End Get
        Set(ByVal value As String)
            _Bank_Expense_Dcptn = value
        End Set
    End Property
    Public Property Cash_Expense_Dcptn() As String
        Get
            Return _Cash_Expense_Dcptn
        End Get
        Set(ByVal value As String)
            _Cash_Expense_Dcptn = value
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

    Public Property Location_id() As Integer
        Get
            Return _Location_id
        End Get
        Set(ByVal value As Integer)
            _Location_id = value
        End Set
    End Property
    Public Property for_date() As System.DateTime
        Get
            Return _for_date
        End Get
        Set(ByVal value As System.DateTime)
            _for_date = value
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

    Public Property csh_id() As Integer
        Get
            Return _csh_id
        End Get
        Set(ByVal value As Integer)
            _csh_id = value
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
    Public Property Stockon_Hand_Qty() As Integer
        Get
            Return _stockon_hand_qty
        End Get
        Set(ByVal value As Integer)
            _stockon_hand_qty = value
        End Set
    End Property
    Public Property Count_Qty() As Integer
        Get
            Return _count_qty
        End Get
        Set(ByVal value As Integer)
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
    Public Property expense_type() As Integer
        Get
            Return _expense_type
        End Get
        Set(ByVal value As Integer)
            _expense_type = value
        End Set
    End Property
    Public Property Float_Value() As Decimal
        Get
            Return _Float_value
        End Get
        Set(ByVal value As Decimal)
            _Float_value = value
        End Set
    End Property


    Public Property Banking() As Decimal
        Get
            Return _Banking
        End Get
        Set(ByVal value As Decimal)
            _Banking = value
        End Set
    End Property
    Public Property Cash_Amount() As Decimal
        Get
            Return _Cash_amount
        End Get
        Set(ByVal value As Decimal)
            _Cash_amount = value
        End Set
    End Property

    Public Property Card_Amount() As Decimal
        Get
            Return _Card_amount
        End Get
        Set(ByVal value As Decimal)
            _Card_amount = value
        End Set
    End Property
    Public Property Machine_id() As Integer
        Get
            Return _Machine_id
        End Get
        Set(ByVal value As Integer)
            _Machine_id = value
        End Set
    End Property
    Public Property Amount_Type() As Integer
        Get
            Return _amount_type
        End Get
        Set(ByVal value As Integer)
            _amount_type = value
        End Set
    End Property

#End Region

    Public Function Insert_Bank_Expense() As Boolean
        Try
            objclsexp = New clsExpense()
            objclsexp.Bank_Expense_Amount = Bank_Expense_Amount
            objclsexp.Bank_Expense_Dcptn = Bank_Expense_Dcptn
            objclsexp.cmp_id = cmp_id
            objclsexp.login_id = login_id
            objclsexp.ip_address = ip_address

            If objclsexp.Insert_Bank_Expense() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Insert_Cash_Expense() As Boolean
        Try
            objclsexp = New clsExpense()
            objclsexp.Cash_Expense_Amount = Cash_Expense_Amount
            objclsexp.Cash_Expense_Dcptn = Cash_Expense_Dcptn
            objclsexp.cmp_id = cmp_id
            objclsexp.login_id = login_id
            objclsexp.ip_address = ip_address
            objclsexp.expense_type = expense_type
            objclsexp.for_date = for_date
            objclsexp.Float_Value = Float_Value
            objclsexp.Banking = Banking
            objclsexp.Location_id = Location_id

            If objclsexp.Insert_Cash_Expense() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_bank_csh() As Boolean
        Try
            objclsexp = New clsExpense()
            objclsexp.Csh_exp_id = Csh_exp_id
            objclsexp.Cash_Expense_Amount = Cash_Expense_Amount
            objclsexp.Cash_Expense_Dcptn = Cash_Expense_Dcptn
            objclsexp.cmp_id = cmp_id
            objclsexp.login_id = login_id
            objclsexp.ip_address = ip_address
            objclsexp.expense_type = expense_type
            objclsexp.for_date = for_date
            objclsexp.Float_Value = Float_Value
            objclsexp.Banking = Banking
            objclsexp.Location_id = Location_id

            If objclsexp.Delete_bank_csh() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_SizeNPrice_By_Product]() As DataTable
        Dim dt As DataTable
        Try
            objclsexp = New clsExpense()
            'objclsstock.product_id = product_id
            objclsexp.cmp_id = cmp_id
            objclsexp.SingleRecord = SingleRecord
            dt = objclsexp.[Select_SizeNPrice_By_Product]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsexp = New clsExpense()
            'objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsexp.cmp_id = cmp_id
            objclsexp.expense_type = 0
            dt = objclsexp.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Bank_List]() As DataTable
        Dim dt As DataTable
        Try
            objclsexp = New clsExpense()
            'objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsexp.cmp_id = cmp_id
            objclsexp.expense_type = 1
            dt = objclsexp.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_Daily_Report() As Boolean
        Try
            objclsexp = New clsExpense()
            objclsexp.cmp_id = cmp_id
            objclsexp.login_id = login_id
            objclsexp.ip_address = ip_address
            objclsexp.for_date = for_date

            If objclsexp.Insert_Daily_Expense() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_Cash_Declaration() As Boolean
        Try
            objclsexp = New clsExpense()
            objclsexp.csh_id = 0
            objclsexp.Card_Amount = Card_Amount
            objclsexp.Cash_Amount = Cash_Amount
            objclsexp.cmp_id = cmp_id
            objclsexp.login_id = login_id
            objclsexp.ip_address = ip_address
            objclsexp.Amount_Type = Amount_Type
            objclsexp.for_date = for_date
            objclsexp.Machine_id = Machine_id


            If objclsexp.Insert_Cash_Declaration() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_Cash_Declaration() As DataTable
        Dim dt As DataTable
        Try
            objclsexp = New clsExpense()
            'objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsexp.cmp_id = cmp_id
            objclsexp.start_date = start_date
            objclsexp.end_date = end_date
            objclsexp.Duration = Duration

            dt = objclsexp.Select_Cash_Declaration()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_cash_decl() As Boolean
        Try
            objclsexp = New clsExpense()
            objclsexp.csh_id = csh_id
            objclsexp.Card_Amount = Card_Amount
            objclsexp.Cash_Amount = Cash_Amount
            objclsexp.cmp_id = cmp_id
            objclsexp.login_id = login_id
            objclsexp.ip_address = ip_address
            objclsexp.Amount_Type = Amount_Type
            objclsexp.for_date = for_date
            objclsexp.Machine_id = Machine_id


            If objclsexp.Delete_cash_decl() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class

Public Class clsExpense
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"

    Private _cmp_id As Integer
    Private _Csh_exp_id As Integer
    Private _Bnk_exp_id As Integer
    Private _Bank_Expense_Amount As Integer
    Private _Cash_Expense_Amount As Decimal
    Private _Bank_Expense_Dcptn As String
    Private _Cash_Expense_Dcptn As String
    Private _Tran_Type As Integer
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
    Private _stockon_hand_qty As Integer
    Private _count_qty As Integer
    Private _result_qty As Integer
    Private _stk_chg_rec_id As Integer
    Private _product_id As Integer
    Private _expense_type As Integer
    Private _for_date As DateTime
    Private _Float_value As Decimal
    Private _Banking As Decimal
    Private _Cash_amount As Decimal
    Private _Card_amount As Decimal
    Private _amount_type As Integer
    Private _Machine_id As Integer
    Private _csh_id As Integer
    Private _Location_id As Integer
    Private _start_date As DateTime
    Private _end_date As DateTime
    Private _Duration As String


    Private objclsexp As clsExpense

#End Region

#Region "Public Property"

    Public Property Duration() As String
        Get
            Return _Duration
        End Get
        Set(ByVal value As String)
            _Duration = value
        End Set
    End Property

    Public Property start_date() As System.DateTime
        Get
            Return _start_date

        End Get
        Set(ByVal value As System.DateTime)
            _start_date = value
        End Set
    End Property

    Public Property end_date() As System.DateTime
        Get
            Return _end_date

        End Get
        Set(ByVal value As System.DateTime)
            _end_date = value
        End Set
    End Property


    Public Property Csh_exp_id() As Integer
        Get
            Return _Csh_exp_id
        End Get
        Set(ByVal value As Integer)
            _Csh_exp_id = value
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
    Public Property Bnk_exp_id() As Integer
        Get
            Return _Bnk_exp_id
        End Get
        Set(ByVal value As Integer)
            _Bnk_exp_id = value
        End Set
    End Property
    Public Property Bank_Expense_Amount() As Integer
        Get
            Return Bank_Expense_Amount
        End Get
        Set(ByVal value As Integer)
            _Bank_Expense_Amount = value
        End Set
    End Property
    Public Property Cash_Expense_Amount() As Decimal
        Get
            Return _Cash_Expense_Amount
        End Get
        Set(ByVal value As Decimal)
            _Cash_Expense_Amount = value
        End Set
    End Property
    Public Property Bank_Expense_Dcptn() As String
        Get
            Return _Bank_Expense_Dcptn
        End Get
        Set(ByVal value As String)
            _Bank_Expense_Dcptn = value
        End Set
    End Property
    Public Property Cash_Expense_Dcptn() As String
        Get
            Return _Cash_Expense_Dcptn
        End Get
        Set(ByVal value As String)
            _Cash_Expense_Dcptn = value
        End Set
    End Property

    'Public Property damage_stock() As Integer
    '    Get
    '        Return _damage_stock
    '    End Get
    '    Set(ByVal value As Integer)
    '        _damage_stock = value
    '    End Set
    'End Property
    'Public Property damage_unit_id() As Integer
    '    Get
    '        Return _damage_unit_id
    '    End Get
    '    Set(ByVal value As Integer)
    '        _damage_unit_id = value
    '    End Set
    'End Property
    'Public Property stock_receive_date() As System.DateTime
    '    Get
    '        Return _stock_receive_date
    '    End Get
    '    Set(ByVal value As System.DateTime)
    '        _stock_receive_date = value
    '    End Set
    'End Property
    'Public Property tax() As Integer
    '    Get
    '        Return _tax
    '    End Get
    '    Set(ByVal value As Integer)
    '        _tax = value
    '    End Set
    'End Property
    'Public Property price() As Decimal
    '    Get
    '        Return _price
    '    End Get
    '    Set(ByVal value As Decimal)
    '        _price = value
    '    End Set
    'End Property

    'Public Property supplier_id() As String
    '    Get
    '        Return _Supplier_id
    '    End Get
    '    Set(ByVal value As String)
    '        _Supplier_id = value
    '    End Set
    'End Property

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

    'Public Property created_date() As System.DateTime
    '    Get
    '        Return _created_date
    '    End Get
    '    Set(ByVal value As System.DateTime)
    '        _created_date = value
    '    End Set
    'End Property
    'Public Property modify_date() As System.DateTime
    '    Get
    '        Return _modify_date
    '    End Get
    '    Set(ByVal value As System.DateTime)
    '        _modify_date = value
    '    End Set
    'End Property

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
    Public Property Stockon_Hand_Qty() As Integer
        Get
            Return _stockon_hand_qty
        End Get
        Set(ByVal value As Integer)
            _stockon_hand_qty = value
        End Set
    End Property
    Public Property Count_Qty() As Integer
        Get
            Return _count_qty
        End Get
        Set(ByVal value As Integer)
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

    Public Property product_id() As Integer
        Get
            Return _product_id
        End Get
        Set(ByVal value As Integer)
            _product_id = value
        End Set
    End Property
    Public Property expense_type() As Integer
        Get
            Return _expense_type
        End Get
        Set(ByVal value As Integer)
            _expense_type = value
        End Set
    End Property
    Public Property for_date() As System.DateTime
        Get
            Return _for_date
        End Get
        Set(ByVal value As System.DateTime)
            _for_date = value
        End Set
    End Property


    Public Property Float_Value() As Decimal
        Get
            Return _Float_value
        End Get
        Set(ByVal value As Decimal)
            _Float_value = value
        End Set
    End Property

    Public Property Banking() As Decimal
        Get
            Return _Banking
        End Get
        Set(ByVal value As Decimal)
            _Banking = value
        End Set
    End Property

    Public Property Cash_Amount() As Decimal
        Get
            Return _Cash_amount
        End Get
        Set(ByVal value As Decimal)
            _Cash_amount = value
        End Set
    End Property

    Public Property Card_Amount() As Decimal
        Get
            Return _Card_amount
        End Get
        Set(ByVal value As Decimal)
            _Card_amount = value
        End Set
    End Property
    Public Property Machine_id() As Integer
        Get
            Return _Machine_id
        End Get
        Set(ByVal value As Integer)
            _Machine_id = value
        End Set
    End Property
    Public Property Amount_Type() As Integer
        Get
            Return _amount_type
        End Get
        Set(ByVal value As Integer)
            _amount_type = value
        End Set
    End Property
    Public Property csh_id() As Integer
        Get
            Return _csh_id
        End Get
        Set(ByVal value As Integer)
            _csh_id = value
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

#End Region
    Public Function Insert_Bank_Expense() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            'oColSqlparram.Add("@stock_id", stock_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Description", Bank_Expense_Dcptn, SqlDbType.Int)
            oColSqlparram.Add("@Expense_Amount", Bank_Expense_Amount, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@expense_type", expense_type, SqlDbType.Int)

            'oColSqlparram.Add("@Tran_Type", "I")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Card_Expense", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_Cash_Expense() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Csh_exp_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Description", Cash_Expense_Dcptn, SqlDbType.NVarChar)
            oColSqlparram.Add("@Expense_Amount", Cash_Expense_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@expense_type", expense_type, SqlDbType.Int)
            oColSqlparram.Add("@for_date", for_date, SqlDbType.DateTime)
            oColSqlparram.Add("@float", Float_Value, SqlDbType.Decimal)
            oColSqlparram.Add("@banking", Banking, SqlDbType.Decimal)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Decimal)
            oColSqlparram.Add("@tran_type", "I", SqlDbType.NVarChar)


            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cash_Expense", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [Select_SizeNPrice_By_Product]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", 0, SqlDbType.Int)
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_grid", oColSqlparram)
        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@expense_type", expense_type, SqlDbType.TinyInt)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Expense", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [Select_Bank_List]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@expense_type", 1, SqlDbType.TinyInt)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Expense", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Insert_Daily_Expense() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            'oColSqlparram.Add("@stock_id", stock_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@for_date", for_date, SqlDbType.DateTime)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cash_UP", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert_Cash_Declaration() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@csh_id", csh_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@card_amount", Card_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@cash_amount", Cash_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@amount_type", Amount_Type, SqlDbType.Int)
            oColSqlparram.Add("@for_date", for_date, SqlDbType.DateTime)
            oColSqlparram.Add("@machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@tran_type", "I", SqlDbType.NVarChar)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cash_Declaration", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_Cash_Declaration() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@date1 ", start_date, SqlDbType.DateTime)
        oColSqlparram.Add("@date2 ", end_date, SqlDbType.DateTime)
        oColSqlparram.Add("@duration ", Duration)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Cash_Declaration", oColSqlparram)
        Return dtlogin

    End Function
    Public Function Delete_cash_decl() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@csh_id", csh_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@card_amount", Card_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@cash_amount", Cash_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@amount_type", Amount_Type, SqlDbType.Int)
            oColSqlparram.Add("@for_date", for_date, SqlDbType.DateTime)
            oColSqlparram.Add("@machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@tran_type", "D", SqlDbType.NVarChar)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cash_Declaration", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Delete_bank_csh() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Csh_exp_id", Csh_exp_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Description", Cash_Expense_Dcptn, SqlDbType.NVarChar)
            oColSqlparram.Add("@Expense_Amount", Cash_Expense_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@expense_type", expense_type, SqlDbType.Int)
            oColSqlparram.Add("@for_date", for_date, SqlDbType.DateTime)
            oColSqlparram.Add("@float", Float_Value, SqlDbType.Decimal)
            oColSqlparram.Add("@banking", Banking, SqlDbType.Decimal)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Decimal)
            oColSqlparram.Add("@tran_type", "D", SqlDbType.NVarChar)


            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cash_Expense", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function




End Class

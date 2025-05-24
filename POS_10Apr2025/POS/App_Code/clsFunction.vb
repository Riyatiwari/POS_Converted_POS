Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsFunction
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _function_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _code As String
    Private _caption_type As String
    Private _shorting_no As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _item As Decimal
    Private _Tran_Type As String
    Private _form_name As String
    Private _back_color As String
    Private _for_color As String
    Private _machine_id As Integer
    Private _Payment_id As Integer
    Private _big_button As Byte

    Private _pay_type As Integer
    Private _pay_sub_type As String
    Private _isgroupbydept As Byte
    Private _isgroupbycourse As Byte
    Private _dept_id As String
    Private _course_id As String
    Private _Panel_id As Integer
    Private _Parent_id As Integer
    Private _mainfunction_id As Integer
    Private _platformAdd As String
    Private _clientToken As String
    Private _accessToken As String
    Private _serviceid As String
    Private _tax_id As Integer
    Private _EOHelpOut_max_amount_each As Decimal
    Private _Total_Value As Decimal
    Private _Sales_Handling_Fee As Decimal
    Private _Payment_Handling_Fee As Decimal
    Private _Tax_Amount As Decimal
    Private _profile_id As Integer
    Private _DefaultDateTime As String

    Private _ZR_VenueID As Integer
    Private _ZR_LocationID As Integer
    Private _ZR_TillID As String
    Private _CardPayType As Integer

    Private objclsFunction As clsFunction
#End Region

#Region "Public Property"

    Public Property CardPayType() As Integer
        Get
            Return _CardPayType
        End Get
        Set(ByVal value As Integer)
            _CardPayType = value
        End Set
    End Property


    Public Property ZR_VenueID() As Integer
        Get
            Return _ZR_VenueID
        End Get
        Set(ByVal value As Integer)
            _ZR_VenueID = value
        End Set
    End Property

    Public Property ZR_LocationID() As Integer
        Get
            Return _ZR_LocationID
        End Get
        Set(ByVal value As Integer)
            _ZR_LocationID = value
        End Set
    End Property

    Public Property ZR_TillID() As String
        Get
            Return _ZR_TillID
        End Get
        Set(ByVal value As String)
            _ZR_TillID = value
        End Set
    End Property


    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
        End Set
    End Property

    Public Property DefaultDateTime() As String
        Get
            Return _DefaultDateTime
        End Get
        Set(ByVal value As String)
            _DefaultDateTime = value
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
    Public Property EOHelpOut_max_amount_each() As Decimal
        Get
            Return _EOHelpOut_max_amount_each
        End Get
        Set(ByVal value As Decimal)
            _EOHelpOut_max_amount_each = value
        End Set
    End Property

    Public Property Tax_Amount() As Decimal
        Get
            Return _Tax_Amount
        End Get
        Set(ByVal value As Decimal)
            _Tax_Amount = value
        End Set
    End Property
    Public Property Payment_Handling_Fee() As Decimal
        Get
            Return _Payment_Handling_Fee
        End Get
        Set(ByVal value As Decimal)
            _Payment_Handling_Fee = value
        End Set
    End Property
    Public Property Sales_Handling_Fee() As Decimal
        Get
            Return _Sales_Handling_Fee
        End Get
        Set(ByVal value As Decimal)
            _Sales_Handling_Fee = value
        End Set
    End Property
    Public Property Total_Value() As Decimal
        Get
            Return _Total_Value
        End Get
        Set(ByVal value As Decimal)
            _Total_Value = value
        End Set
    End Property
    Public Property Payment_id() As Integer
        Get
            Return _Payment_id
        End Get
        Set(ByVal value As Integer)
            _Payment_id = value
        End Set
    End Property
    Public Property function_id() As Integer
        Get
            Return _function_id
        End Get
        Set(ByVal value As Integer)
            _function_id = value
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
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
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
    Public Property caption_type() As String
        Get
            Return _caption_type
        End Get
        Set(ByVal value As String)
            _caption_type = value
        End Set
    End Property
    Public Property shorting_no() As String
        Get
            Return _shorting_no
        End Get
        Set(ByVal value As String)
            _shorting_no = value
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
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
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
    Public Property item() As Decimal
        Get
            Return _item
        End Get
        Set(ByVal value As Decimal)
            _item = value
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
    Public Property form_name() As String
        Get
            Return _form_name
        End Get
        Set(ByVal value As String)
            _form_name = value
        End Set
    End Property
    Public Property back_color() As String
        Get
            Return _back_color
        End Get
        Set(ByVal value As String)
            _back_color = value
        End Set
    End Property
    Public Property for_color() As String
        Get
            Return _for_color
        End Get
        Set(ByVal value As String)
            _for_color = value
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
    Public Property big_button() As Byte
        Get
            Return _big_button
        End Get
        Set(ByVal value As Byte)
            _big_button = value
        End Set
    End Property

    Public Property pay_type() As Integer
        Get
            Return _pay_type
        End Get
        Set(ByVal value As Integer)
            _pay_type = value
        End Set
    End Property
    Public Property pay_sub_type() As String
        Get
            Return _pay_sub_type
        End Get
        Set(ByVal value As String)
            _pay_sub_type = value
        End Set
    End Property
    Public Property isgroupbydept() As Byte
        Get
            Return _isgroupbydept

        End Get
        Set(ByVal value As Byte)
            _isgroupbydept = value
        End Set
    End Property
    Public Property isgroupbycourse() As Byte
        Get
            Return _isgroupbycourse

        End Get
        Set(ByVal value As Byte)
            _isgroupbycourse = value
        End Set
    End Property
    Public Property dept_id() As String
        Get
            Return _dept_id
        End Get
        Set(ByVal value As String)
            _dept_id = value
        End Set
    End Property
    Public Property course_id() As String
        Get
            Return _course_id
        End Get
        Set(ByVal value As String)
            _course_id = value
        End Set
    End Property

    Public Property Panel_id() As Integer
        Get
            Return _Panel_id
        End Get
        Set(ByVal value As Integer)
            _Panel_id = value
        End Set
    End Property

    Public Property Parent_id() As Integer
        Get
            Return _Parent_id
        End Get
        Set(ByVal value As Integer)
            _Parent_id = value
        End Set
    End Property

    Public Property mainfunction_id() As Integer
        Get
            Return _mainfunction_id
        End Get
        Set(ByVal value As Integer)
            _mainfunction_id = value
        End Set
    End Property

    Public Property platformAdd() As String
        Get
            Return _platformAdd
        End Get
        Set(ByVal value As String)
            _platformAdd = value
        End Set
    End Property
    Public Property clientToken() As String
        Get
            Return _clientToken
        End Get
        Set(ByVal value As String)
            _clientToken = value
        End Set
    End Property
    Public Property accessToken() As String
        Get
            Return _accessToken
        End Get
        Set(ByVal value As String)
            _accessToken = value
        End Set
    End Property
    Public Property serviceid() As String
        Get
            Return _serviceid
        End Get
        Set(ByVal value As String)
            _serviceid = value
        End Set
    End Property
#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.function_id = function_id
            objclsFunction.cmp_id = cmp_id
            objclsFunction.name = name
            objclsFunction.code = code
            objclsFunction.caption_type = caption_type
            objclsFunction.is_active = is_active
            objclsFunction.shorting_no = shorting_no
            objclsFunction.ip_address = ip_address
            objclsFunction.login_id = login_id
            objclsFunction.item = item
            objclsFunction.back_color = back_color
            objclsFunction.for_color = for_color
            objclsFunction.machine_id = machine_id
            objclsFunction.big_button = big_button
            objclsFunction.Payment_id = Payment_id
            objclsFunction.pay_type = pay_type
            objclsFunction.pay_sub_type = pay_sub_type
            objclsFunction.isgroupbydept = isgroupbydept
            objclsFunction.isgroupbycourse = isgroupbycourse
            objclsFunction.dept_id = dept_id
            objclsFunction.course_id = course_id
            objclsFunction.Panel_id = Panel_id
            objclsFunction.Parent_id = Parent_id
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.platformAdd = platformAdd
            objclsFunction.clientToken = clientToken
            objclsFunction.accessToken = accessToken
            objclsFunction.serviceid = serviceid
            objclsFunction.tax_id = tax_id
            objclsFunction.EOHelpOut_max_amount_each = EOHelpOut_max_amount_each
            objclsFunction.Tax_Amount = Tax_Amount
            objclsFunction.Total_Value = Total_Value
            objclsFunction.Sales_Handling_Fee = Sales_Handling_Fee
            objclsFunction.Payment_Handling_Fee = Payment_Handling_Fee
            objclsFunction.profile_id = profile_id
            objclsFunction.DefaultDateTime = DefaultDateTime
            objclsFunction.ZR_VenueID = ZR_VenueID
            objclsFunction.ZR_LocationID = ZR_LocationID
            objclsFunction.ZR_TillID = ZR_TillID
            objclsFunction.CardPayType = CardPayType

            If objclsFunction.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.function_id = function_id
            objclsFunction.cmp_id = cmp_id
            objclsFunction.name = name
            objclsFunction.code = code
            objclsFunction.caption_type = caption_type
            objclsFunction.is_active = is_active
            objclsFunction.shorting_no = shorting_no
            objclsFunction.ip_address = ip_address
            objclsFunction.login_id = login_id
            objclsFunction.item = item
            objclsFunction.back_color = back_color
            objclsFunction.for_color = for_color
            objclsFunction.machine_id = machine_id
            objclsFunction.big_button = big_button
            objclsFunction.Payment_id = Payment_id
            objclsFunction.pay_type = pay_type
            objclsFunction.pay_sub_type = pay_sub_type
            objclsFunction.isgroupbydept = isgroupbydept
            objclsFunction.isgroupbycourse = isgroupbycourse
            objclsFunction.dept_id = dept_id
            objclsFunction.course_id = course_id
            objclsFunction.Panel_id = Panel_id
            objclsFunction.Parent_id = Parent_id
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.platformAdd = platformAdd
            objclsFunction.clientToken = clientToken
            objclsFunction.accessToken = accessToken
            objclsFunction.serviceid = serviceid
            objclsFunction.tax_id = tax_id
            objclsFunction.EOHelpOut_max_amount_each = EOHelpOut_max_amount_each
            objclsFunction.Tax_Amount = Tax_Amount
            objclsFunction.Total_Value = Total_Value
            objclsFunction.Sales_Handling_Fee = Sales_Handling_Fee
            objclsFunction.Payment_Handling_Fee = Payment_Handling_Fee
            objclsFunction.profile_id = profile_id
            objclsFunction.DefaultDateTime = DefaultDateTime
            objclsFunction.ZR_VenueID = ZR_VenueID
            objclsFunction.ZR_LocationID = ZR_LocationID
            objclsFunction.ZR_TillID = ZR_TillID
            objclsFunction.CardPayType = CardPayType

            If objclsFunction.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.function_id = function_id
            objclsFunction.cmp_id = cmp_id
            objclsFunction.name = name
            objclsFunction.code = code
            objclsFunction.caption_type = caption_type
            objclsFunction.is_active = is_active
            objclsFunction.shorting_no = shorting_no
            objclsFunction.ip_address = ip_address
            objclsFunction.login_id = login_id
            objclsFunction.item = item
            objclsFunction.back_color = back_color
            objclsFunction.for_color = for_color
            objclsFunction.Tran_Type = Tran_Type
            objclsFunction.machine_id = machine_id
            objclsFunction.big_button = big_button
            objclsFunction.Payment_id = Payment_id
            objclsFunction.pay_type = pay_type
            objclsFunction.pay_sub_type = pay_sub_type
            objclsFunction.isgroupbydept = isgroupbydept
            objclsFunction.isgroupbycourse = isgroupbycourse
            objclsFunction.dept_id = dept_id
            objclsFunction.course_id = course_id
            objclsFunction.Panel_id = Panel_id
            objclsFunction.Parent_id = Parent_id
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.platformAdd = platformAdd
            objclsFunction.clientToken = clientToken
            objclsFunction.accessToken = accessToken
            objclsFunction.serviceid = serviceid
            objclsFunction.tax_id = tax_id
            objclsFunction.EOHelpOut_max_amount_each = EOHelpOut_max_amount_each
            objclsFunction.Tax_Amount = Tax_Amount
            objclsFunction.Total_Value = Total_Value
            objclsFunction.Sales_Handling_Fee = Sales_Handling_Fee
            objclsFunction.Payment_Handling_Fee = Payment_Handling_Fee
            objclsFunction.profile_id = profile_id
            objclsFunction.DefaultDateTime = DefaultDateTime
            objclsFunction.ZR_VenueID = ZR_VenueID
            objclsFunction.ZR_LocationID = ZR_LocationID
            objclsFunction.ZR_TillID = ZR_TillID
            objclsFunction.CardPayType = CardPayType

            If objclsFunction.Delete() Then
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
            objclsFunction = New clsFunction()
            objclsFunction.function_id = function_id
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectMainFunction]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectMainFunction]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectMainFunction_Swap]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.machine_id = machine_id
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectMainFunction_Swap]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function [SelectPanelFunction]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.Panel_id = Panel_id
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectPanelFunction]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectFunction_Details]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectFunction_Details]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            'objclsFunction.function_id = function_id
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAllMainFunction]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            'objclsFunction.function_id = function_id
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectAllMainFunction]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectFunctionByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectFunctionByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectCardPaymentType]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectCardPaymentType]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectParentFunctionByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectParentFunctionByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectEditPanelByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            objclsFunction.mainfunction_id = mainfunction_id
            dt = objclsFunction.SelectEditPanelByCmp()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectFunctionAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            dt = objclsFunction.[SelectFunctionAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select1]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            objclsFunction.form_name = form_name
            dt = objclsFunction.[Select1]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectFunctionCode]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            objclsFunction.function_id = function_id
            dt = objclsFunction.[SelectFunctionCode]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectFunctionCode_New]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            objclsFunction.function_id = function_id
            objclsFunction.Parent_id = Parent_id
            objclsFunction.machine_id = machine_id
            dt = objclsFunction.[SelectFunctionCode_New]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectFunctionAllCode]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            objclsFunction.function_id = function_id
            dt = objclsFunction.[SelectFunctionAllCode]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectFunctionCodeByTill]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunction = New clsFunction()
            objclsFunction.machine_id = machine_id
            objclsFunction.code = code
            objclsFunction.cmp_id = cmp_id
            objclsFunction.Parent_id = Parent_id
            dt = objclsFunction.[SelectFunctionCodeByTill]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsertMainFunction() As Integer
        Try
            objclsFunction = New clsFunction()
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.cmp_id = cmp_id
            objclsFunction.name = name
            objclsFunction.is_active = is_active
            objclsFunction.ip_address = ip_address
            objclsFunction.login_id = login_id
            objclsFunction.machine_id = machine_id
            Dim r As Integer
            r = objclsFunction.InsertMainFunction()
            Return r
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateMainFunction() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.cmp_id = cmp_id
            objclsFunction.name = name
            objclsFunction.is_active = is_active
            objclsFunction.ip_address = ip_address
            objclsFunction.login_id = login_id
            objclsFunction.machine_id = machine_id
            If objclsFunction.UpdateMainFunction() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function DeleteMainFunction() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.cmp_id = cmp_id
            objclsFunction.name = name
            objclsFunction.is_active = is_active
            objclsFunction.ip_address = ip_address
            objclsFunction.login_id = login_id
            objclsFunction.machine_id = machine_id
            If objclsFunction.DeleteMainFunction() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Copy_Function() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            objclsFunction.mainfunction_id = mainfunction_id
            If objclsFunction.Copy_Function() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Update_Function_Parent() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.Panel_id = Panel_id
            objclsFunction.mainfunction_id = mainfunction_id
            If objclsFunction.Update_Function_Parent() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Update_Function_ParentBy_Till() As Boolean
        Try
            objclsFunction = New clsFunction()
            objclsFunction.cmp_id = cmp_id
            objclsFunction.mainfunction_id = mainfunction_id
            objclsFunction.machine_id = machine_id
            If objclsFunction.Update_Function_ParentBy_Till() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


#End Region

End Class

Public Class clsFunction
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _function_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _code As String
    Private _caption_type As String
    Private _shorting_no As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _item As Decimal
    Private _Tran_Type As String
    Private _form_name As String
    Private _back_color As String
    Private _for_color As String
    Private _machine_id As Integer
    Private _Payment_id As Integer
    Private _big_button As Byte

    Private _pay_type As Integer
    Private _pay_sub_type As String
    Private _isgroupbydept As Byte
    Private _isgroupbycourse As Byte
    Private _dept_id As String
    Private _course_id As String
    Private _Panel_id As Integer
    Private _Parent_id As Integer
    Private _mainfunction_id As Integer
    Private _platformAdd As String
    Private _clientToken As String
    Private _accessToken As String
    Private _serviceid As String
    Private _tax_id As Integer
    Private _EOHelpOut_max_amount_each As Decimal
    Private _Total_Value As Decimal
    Private _Sales_Handling_Fee As Decimal
    Private _Payment_Handling_Fee As Decimal
    Private _Tax_Amount As Decimal
    Private _profile_id As Integer
    Private _DefaultDateTime As String


    Private _ZR_VenueID As Integer
    Private _ZR_LocationID As Integer
    Private _ZR_TillID As String

    Private _CardPayType As Integer
    Private objclsFunction As clsFunction

#End Region

#Region "Public Property"

    Public Property CardPayType() As Integer
        Get
            Return _CardPayType
        End Get
        Set(ByVal value As Integer)
            _CardPayType = value
        End Set
    End Property

    Public Property ZR_VenueID() As Integer
        Get
            Return _ZR_VenueID
        End Get
        Set(ByVal value As Integer)
            _ZR_VenueID = value
        End Set
    End Property

    Public Property ZR_LocationID() As Integer
        Get
            Return _ZR_LocationID
        End Get
        Set(ByVal value As Integer)
            _ZR_LocationID = value
        End Set
    End Property

    Public Property ZR_TillID() As String
        Get
            Return _ZR_TillID
        End Get
        Set(ByVal value As String)
            _ZR_TillID = value
        End Set
    End Property



    Public Property DefaultDateTime() As String
        Get
            Return _DefaultDateTime
        End Get
        Set(ByVal value As String)
            _DefaultDateTime = value
        End Set
    End Property
    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
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
    Public Property EOHelpOut_max_amount_each() As Decimal
        Get
            Return _EOHelpOut_max_amount_each
        End Get
        Set(ByVal value As Decimal)
            _EOHelpOut_max_amount_each = value
        End Set
    End Property


    Public Property Tax_Amount() As Decimal
        Get
            Return _Tax_Amount
        End Get
        Set(ByVal value As Decimal)
            _Tax_Amount = value
        End Set
    End Property
    Public Property Payment_Handling_Fee() As Decimal
        Get
            Return _Payment_Handling_Fee
        End Get
        Set(ByVal value As Decimal)
            _Payment_Handling_Fee = value
        End Set
    End Property
    Public Property Sales_Handling_Fee() As Decimal
        Get
            Return _Sales_Handling_Fee
        End Get
        Set(ByVal value As Decimal)
            _Sales_Handling_Fee = value
        End Set
    End Property
    Public Property Total_Value() As Decimal
        Get
            Return _Total_Value
        End Get
        Set(ByVal value As Decimal)
            _Total_Value = value
        End Set
    End Property
    Public Property platformAdd() As String
        Get
            Return _platformAdd
        End Get
        Set(ByVal value As String)
            _platformAdd = value
        End Set
    End Property
    Public Property clientToken() As String
        Get
            Return _clientToken
        End Get
        Set(ByVal value As String)
            _clientToken = value
        End Set
    End Property
    Public Property accessToken() As String
        Get
            Return _accessToken
        End Get
        Set(ByVal value As String)
            _accessToken = value
        End Set
    End Property
    Public Property serviceid() As String
        Get
            Return _serviceid
        End Get
        Set(ByVal value As String)
            _serviceid = value
        End Set
    End Property
    Public Property Payment_id() As Integer
        Get
            Return _Payment_id
        End Get
        Set(ByVal value As Integer)
            _Payment_id = value
        End Set
    End Property
    Public Property function_id() As Integer
        Get
            Return _function_id
        End Get
        Set(ByVal value As Integer)
            _function_id = value
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
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
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
    Public Property caption_type() As String
        Get
            Return _caption_type
        End Get
        Set(ByVal value As String)
            _caption_type = value
        End Set
    End Property
    Public Property shorting_no() As String
        Get
            Return _shorting_no
        End Get
        Set(ByVal value As String)
            _shorting_no = value
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
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
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
    Public Property item() As Decimal
        Get
            Return _item
        End Get
        Set(ByVal value As Decimal)
            _item = value
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
    Public Property form_name() As String
        Get
            Return _form_name
        End Get
        Set(ByVal value As String)
            _form_name = value
        End Set
    End Property
    Public Property back_color() As String
        Get
            Return _back_color
        End Get
        Set(ByVal value As String)
            _back_color = value
        End Set
    End Property
    Public Property for_color() As String
        Get
            Return _for_color
        End Get
        Set(ByVal value As String)
            _for_color = value
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
    Public Property big_button() As Byte
        Get
            Return _big_button
        End Get
        Set(ByVal value As Byte)
            _big_button = value
        End Set
    End Property

    Public Property pay_type() As Integer
        Get
            Return _pay_type
        End Get
        Set(ByVal value As Integer)
            _pay_type = value
        End Set
    End Property
    Public Property pay_sub_type() As String
        Get
            Return _pay_sub_type
        End Get
        Set(ByVal value As String)
            _pay_sub_type = value
        End Set
    End Property
    Public Property isgroupbydept() As Byte
        Get
            Return _isgroupbydept

        End Get
        Set(ByVal value As Byte)
            _isgroupbydept = value
        End Set
    End Property
    Public Property isgroupbycourse() As Byte
        Get
            Return _isgroupbycourse

        End Get
        Set(ByVal value As Byte)
            _isgroupbycourse = value
        End Set
    End Property
    Public Property dept_id() As String
        Get
            Return _dept_id
        End Get
        Set(ByVal value As String)
            _dept_id = value
        End Set
    End Property
    Public Property course_id() As String
        Get
            Return _course_id
        End Get
        Set(ByVal value As String)
            _course_id = value
        End Set
    End Property

    Public Property Panel_id() As Integer
        Get
            Return _Panel_id
        End Get
        Set(ByVal value As Integer)
            _Panel_id = value
        End Set
    End Property

    Public Property Parent_id() As Integer
        Get
            Return _Parent_id
        End Get
        Set(ByVal value As Integer)
            _Parent_id = value
        End Set
    End Property

    Public Property mainfunction_id() As Integer
        Get
            Return _mainfunction_id
        End Get
        Set(ByVal value As Integer)
            _mainfunction_id = value
        End Set
    End Property
#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@caption_type", caption_type)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@item", item, SqlDbType.Decimal)
            oColSqlparram.Add("@back_color", back_color)
            oColSqlparram.Add("@for_color", for_color)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@big_button", big_button, SqlDbType.TinyInt)
            oColSqlparram.Add("@Payment_id", Payment_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@pay_type", pay_type, SqlDbType.Int)
            oColSqlparram.Add("@pay_sub_type", pay_sub_type)
            oColSqlparram.Add("@is_groupby_dept", isgroupbydept, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_groupby_course", isgroupbycourse, SqlDbType.TinyInt)
            oColSqlparram.Add("@dept_id", dept_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@course_id", course_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
            oColSqlparram.Add("@Parent_id", Parent_id, SqlDbType.Int)
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            oColSqlparram.Add("@platformAdd", platformAdd, SqlDbType.NVarChar)
            oColSqlparram.Add("@clientToken", clientToken, SqlDbType.NVarChar)
            oColSqlparram.Add("@accessToken", accessToken, SqlDbType.NVarChar)
            oColSqlparram.Add("@serviceid", serviceid, SqlDbType.NVarChar)
            oColSqlparram.Add("@tax_id", tax_id, SqlDbType.Int)
            oColSqlparram.Add("@EOHelpOut_max_amount_each", EOHelpOut_max_amount_each, SqlDbType.Decimal)
            oColSqlparram.Add("@Payment_Handling_Fee", Payment_Handling_Fee, SqlDbType.Decimal)
            oColSqlparram.Add("@Sales_Handling_Fee", Sales_Handling_Fee, SqlDbType.Decimal)
            oColSqlparram.Add("@Tax_Amount", Tax_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Total_Value", Total_Value, SqlDbType.Decimal)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@DefaultDateTime", DefaultDateTime)
            oColSqlparram.Add("@ZR_VenueID", ZR_VenueID, SqlDbType.Int)
            oColSqlparram.Add("@ZR_LocationID", ZR_LocationID, SqlDbType.Int)
            oColSqlparram.Add("@ZR_TillID", ZR_TillID)
            oColSqlparram.Add("@CardPayType", CardPayType, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Function", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@caption_type", caption_type)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@item", item, SqlDbType.Decimal)
            oColSqlparram.Add("@back_color", back_color)
            oColSqlparram.Add("@for_color", for_color)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@big_button", big_button, SqlDbType.TinyInt)
            oColSqlparram.Add("@Payment_id", Payment_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@pay_type", pay_type, SqlDbType.Int)
            oColSqlparram.Add("@pay_sub_type", pay_sub_type)
            oColSqlparram.Add("@is_groupby_dept", isgroupbydept, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_groupby_course", isgroupbycourse, SqlDbType.TinyInt)
            oColSqlparram.Add("@dept_id", dept_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@course_id", course_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
            oColSqlparram.Add("@Parent_id", Parent_id, SqlDbType.Int)
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            oColSqlparram.Add("@platformAdd", platformAdd, SqlDbType.NVarChar)
            oColSqlparram.Add("@clientToken", clientToken, SqlDbType.NVarChar)
            oColSqlparram.Add("@accessToken", accessToken, SqlDbType.NVarChar)
            oColSqlparram.Add("@serviceid", serviceid, SqlDbType.NVarChar)
            oColSqlparram.Add("@tax_id", tax_id, SqlDbType.Int)
            oColSqlparram.Add("@EOHelpOut_max_amount_each", EOHelpOut_max_amount_each, SqlDbType.Decimal)
            oColSqlparram.Add("@Payment_Handling_Fee", Payment_Handling_Fee, SqlDbType.Decimal)
            oColSqlparram.Add("@Sales_Handling_Fee", Sales_Handling_Fee, SqlDbType.Decimal)
            oColSqlparram.Add("@Tax_Amount", Tax_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Total_Value", Total_Value, SqlDbType.Decimal)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@DefaultDateTime", DefaultDateTime)
            oColSqlparram.Add("@ZR_VenueID", ZR_VenueID, SqlDbType.Int)
            oColSqlparram.Add("@ZR_LocationID", ZR_LocationID, SqlDbType.Int)
            oColSqlparram.Add("@ZR_TillID", ZR_TillID)
            oColSqlparram.Add("@CardPayType", CardPayType, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Function", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@caption_type", caption_type)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@item", item, SqlDbType.Decimal)
            oColSqlparram.Add("@back_color", back_color)
            oColSqlparram.Add("@for_color", for_color)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@big_button", big_button, SqlDbType.TinyInt)
            oColSqlparram.Add("@Payment_id", Payment_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@pay_type", pay_type, SqlDbType.Int)
            oColSqlparram.Add("@pay_sub_type", pay_sub_type)
            oColSqlparram.Add("@is_groupby_dept", isgroupbydept, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_groupby_course", isgroupbycourse, SqlDbType.TinyInt)
            oColSqlparram.Add("@dept_id", dept_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@course_id", course_id, SqlDbType.NVarChar)
            oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
            oColSqlparram.Add("@Parent_id", Parent_id, SqlDbType.Int)
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            oColSqlparram.Add("@platformAdd", platformAdd, SqlDbType.NVarChar)
            oColSqlparram.Add("@clientToken", clientToken, SqlDbType.NVarChar)
            oColSqlparram.Add("@accessToken", accessToken, SqlDbType.NVarChar)
            oColSqlparram.Add("@serviceid", serviceid, SqlDbType.NVarChar)
            oColSqlparram.Add("@tax_id", tax_id, SqlDbType.Int)
            oColSqlparram.Add("@EOHelpOut_max_amount_each", EOHelpOut_max_amount_each, SqlDbType.Decimal)
            oColSqlparram.Add("@Payment_Handling_Fee", Payment_Handling_Fee, SqlDbType.Decimal)
            oColSqlparram.Add("@Sales_Handling_Fee", Sales_Handling_Fee, SqlDbType.Decimal)
            oColSqlparram.Add("@Tax_Amount", Tax_Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Total_Value", Total_Value, SqlDbType.Decimal)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@DefaultDateTime", DefaultDateTime)
            oColSqlparram.Add("@ZR_VenueID", ZR_VenueID, SqlDbType.Int)
            oColSqlparram.Add("@ZR_LocationID", ZR_LocationID, SqlDbType.Int)
            oColSqlparram.Add("@ZR_TillID", ZR_TillID)
            oColSqlparram.Add("@CardPayType", CardPayType, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Function", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Function", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectMainFunction]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_MainFunction", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectMainFunction_Swap]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_MainFunction_Swap", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectPanelFunction]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
        oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_PanelFunction", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Function", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAllMainFunction]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_MainFunction", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectFunction_Details]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Function_Details", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select1]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@form_name", form_name)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Autogenerate_Sorting_No_Master", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectFunctionByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Function_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectCardPaymentType]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_CardPayType", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectParentFunctionByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Parent_Function_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [SelectEditPanelByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Edit_Panel_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectFunctionAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Function_Type_All", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectFunctionCode_New]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Parent_id", Parent_id, SqlDbType.Int)
        oColSqlparram.Add("@Till_id", machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Function_Code_By_Id", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectFunctionCode]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Function_Code_By_Id", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectFunctionAllCode]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Function_all_Code", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectFunctionCodeByTill]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Till_id", machine_id, SqlDbType.Int)
        oColSqlparram.Add("@code", code)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Parent_id", Parent_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_FunctionCodeByTill", oColSqlparram)

        Return dtlogin
    End Function

    Public Function InsertMainFunction() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainFunction", oColSqlparram)
            Return dtlogin.Rows(0)("mainfunction_id")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateMainFunction() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainFunction", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function DeleteMainFunction() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainFunction", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Copy_Function() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainFunction_Copy", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_Function_Parent() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Update_Parent_Function", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_Function_ParentBy_Till() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@mainfunction_id", mainfunction_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Update_Parent_Function_By_Till", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



#End Region
End Class
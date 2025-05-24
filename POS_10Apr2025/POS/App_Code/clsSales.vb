Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsSales
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _tsales_id As Integer
    Private _sales_id As Integer
    Private _cmp_id As Integer
    Private _total_amount As Integer
    Private _total_discount As Integer
    Private _net_amount As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _date1 As System.DateTime
    Private _date2 As System.DateTime
    Private _Ip_address As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _product_id As Integer
    Private _category_id As Integer
    Private _type As Integer
    Private _location_id As Integer
    Private _ref_id As Integer
    Private _duration As String
    Private _table_uuid As String
    Private objclsSales As clsSales
#End Region

#Region "Public Property"
    Public Property table_uuid() As String
        Get
            Return _table_uuid
        End Get
        Set(ByVal value As String)
            _table_uuid = value
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
    Public Property tsales_id() As Integer
        Get
            Return _tsales_id
        End Get
        Set(ByVal value As Integer)
            _tsales_id = value
        End Set
    End Property
    Public Property sales_id() As Integer
        Get
            Return _sales_id
        End Get
        Set(ByVal value As Integer)
            _sales_id = value
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
    Public Property total_amount() As Integer
        Get
            Return _total_amount
        End Get
        Set(ByVal value As Integer)
            _total_amount = value
        End Set
    End Property
    Public Property total_discount() As Integer
        Get
            Return _total_discount
        End Get
        Set(ByVal value As Integer)
            _total_discount = value
        End Set
    End Property
    Public Property net_amount() As Integer
        Get
            Return _net_amount
        End Get
        Set(ByVal value As Integer)
            _net_amount = value
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

    Public Property created_date() As System.DateTime
        Get
            Return _created_date

        End Get
        Set(ByVal value As System.DateTime)
            _created_date = value
        End Set
    End Property
    Public Property date1() As System.DateTime
        Get
            Return _date1

        End Get
        Set(ByVal value As System.DateTime)
            _date1 = value
        End Set
    End Property
    Public Property date2() As System.DateTime
        Get
            Return _date2

        End Get
        Set(ByVal value As System.DateTime)
            _date2 = value
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
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
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
    Public Property category_id() As Integer
        Get
            Return _category_id
        End Get
        Set(ByVal value As Integer)
            _category_id = value
        End Set

    End Property
    Public Property type() As Integer
        Get
            Return _type
        End Get
        Set(ByVal value As Integer)
            _type = value
        End Set

    End Property

    Public Property ref_id() As Integer
        Get
            Return _ref_id
        End Get
        Set(ByVal value As Integer)
            _ref_id = value
        End Set
    End Property
#End Region

    '    Public Property Ip_address() As String
    '        Get
    '            Return _Ip_address
    '        End Get
    '        Set(ByVal value As String)
    '            _Ip_address = value
    '        End Set
    '    End Property
    '    Public Property login_id() As Integer
    '        Get
    '            Return _login_id
    '        End Get
    '        Set(ByVal value As Integer)
    '            _login_id = value
    '        End Set
    '    End Property
    '#End Region

    '    Public Function Insert() As Boolean
    '        Try
    '            objclsSales = New clsPrinter()
    '            objclsSales.printer_id = printer_id
    '            objclsSales.cmp_id = cmp_id
    '            objclsSales.name = name
    '            objclsSales.palias = palias
    '            objclsSales.Is_active = Is_active
    '            objclsSales.Ip_address = Ip_address
    '            objclsSales.login_id = login_id
    '            If objclsSales.Insert() Then
    '                Return True
    '            End If
    '            Return False
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function
    '    Public Function Update() As Boolean
    '        Try
    '            objclsSales = New clsPrinter()
    '            objclsSales.printer_id = printer_id
    '            objclsSales.cmp_id = cmp_id
    '            objclsSales.name = name
    '            objclsSales.palias = palias
    '            objclsSales.Is_active = Is_active
    '            objclsSales.Ip_address = Ip_address
    '            objclsSales.login_id = login_id
    '            If objclsSales.Update() Then
    '                Return True
    '            End If
    '            Return False
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function
    '    Public Function Delete() As Boolean
    '        Try
    '            objclsSales = New clsPrinter()
    '            objclsSales.printer_id = printer_id
    '            objclsSales.cmp_id = cmp_id
    '            objclsSales.name = name
    '            objclsSales.palias = palias
    '            objclsSales.Is_active = Is_active
    '            objclsSales.Ip_address = Ip_address
    '            objclsSales.login_id = login_id
    '            If objclsSales.Delete() Then
    '                Return True
    '            End If
    '            Return False
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function
    Public Function [SelectIngredient]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.tsales_id = tsales_id
            dt = objclsSales.[SelectIngredient]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectCondiment]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.tsales_id = tsales_id
            dt = objclsSales.[SelectCondiment]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.sales_id = sales_id
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectView]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.sales_id = sales_id
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.[SelectView]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            'objclsSales.sales_id = sales_id
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select1]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.date1 = date1
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.[Select1]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectReport]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.date1 = date1
            objclsSales.date2 = date2
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.[SelectReport]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectSalesDetailsReport]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.date1 = date1
            objclsSales.date2 = date2
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.[SelectSalesDetailsReport]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectSalesProductSummaryReport]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.date1 = date1
            objclsSales.date2 = date2
            objclsSales.cmp_id = cmp_id
            objclsSales.product_id = product_id
            objclsSales.category_id = category_id

            dt = objclsSales.[SelectSalesProductSummaryReport]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Selectcardview]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.sales_id = sales_id
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.[Selectcardview]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectDiscountByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.SelectDiscountByCmp()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Cash_Control_BD]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.cmp_id = cmp_id
            objclsSales.date1 = date1
            objclsSales.date2 = date2
            objclsSales.Location_id = Location_id
            objclsSales.duration = duration
            dt = objclsSales.Cash_Control_BD()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Stock_Transaction]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.cmp_id = cmp_id
            objclsSales.date1 = date1
            objclsSales.date2 = date2
            dt = objclsSales.Stock_Transaction()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Stock_Transaction_Details]() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.cmp_id = cmp_id
            objclsSales.date1 = date1
            objclsSales.date2 = date2
            objclsSales.Location_id = Location_id
            objclsSales.product_id = product_id
            objclsSales.duration = duration
            objclsSales.category_id = category_id
            dt = objclsSales.Stock_Transaction_Details()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function insert_resync() As Boolean
        Try
            objclsSales = New clsSales()
            objclsSales.sales_id = sales_id
            objclsSales.ref_id = ref_id
            objclsSales.machine_id = machine_id
            objclsSales.cmp_id = cmp_id
            If objclsSales.Insert_ReSync() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function delete_sync() As Boolean
        Try
            objclsSales = New clsSales()
            objclsSales.sales_id = sales_id
            If objclsSales.delete_Sync() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_sales_email() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            'objclsSales.Location_id = Location_id
            'objclsSales.cmp_id = cmp_id
            dt = objclsSales.Select_sales_email()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_credit_sales_email() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.table_uuid = table_uuid
            'objclsSales.cmp_id = cmp_id
            dt = objclsSales.Select_credit_sales_email()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_NonTable_SalesDetail_for_email() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.sales_id = sales_id
            objclsSales.cmp_id = cmp_id
            dt = objclsSales.Select_NonTable_SalesDetail_for_email()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Select_tsales_for_email() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.cmp_id = cmp_id
            objclsSales.sales_id = sales_id
            dt = objclsSales.Select_tsales_for_email()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function update_email_flag_sales() As DataTable
        Dim dt As DataTable
        Try
            objclsSales = New clsSales()
            objclsSales.sales_id = sales_id
            dt = objclsSales.update_email_flag_sales()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsSales
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _tsales_id As Integer
    Private _sales_id As Integer
    Private _cmp_id As Integer
    Private _total_amount As Integer
    Private _total_discount As Integer
    Private _net_amount As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _date1 As System.DateTime
    Private _date2 As System.DateTime
    Private _Ip_address As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _product_id As Integer
    Private _category_id As Integer
    Private _location_id As Integer
    Private _type As Integer
    Private _ref_id As Integer
    Private _duration As String
    Private _table_uuid As String
    Private objclsSales As clsSales
#End Region

#Region "Public Property"
    Public Property table_uuid() As String
        Get
            Return _table_uuid
        End Get
        Set(ByVal value As String)
            _table_uuid = value
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
    Public Property Location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property tsales_id() As Integer
        Get
            Return _tsales_id
        End Get
        Set(ByVal value As Integer)
            _tsales_id = value
        End Set
    End Property
    Public Property sales_id() As Integer
        Get
            Return _sales_id
        End Get
        Set(ByVal value As Integer)
            _sales_id = value
        End Set
    End Property
    Public Property total_amount() As Integer
        Get
            Return _total_amount
        End Get
        Set(ByVal value As Integer)
            _total_amount = value
        End Set
    End Property
    Public Property total_discount() As Integer
        Get
            Return _total_discount
        End Get
        Set(ByVal value As Integer)
            _total_discount = value
        End Set
    End Property
    Public Property net_amount() As Integer
        Get
            Return _net_amount
        End Get
        Set(ByVal value As Integer)
            _net_amount = value
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

    Public Property created_date() As System.DateTime
        Get
            Return _created_date

        End Get
        Set(ByVal value As System.DateTime)
            _created_date = value
        End Set
    End Property
    Public Property date1() As System.DateTime
        Get
            Return _date1

        End Get
        Set(ByVal value As System.DateTime)
            _date1 = value
        End Set
    End Property
    Public Property date2() As System.DateTime
        Get
            Return _date2

        End Get
        Set(ByVal value As System.DateTime)
            _date2 = value
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
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
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
    Public Property category_id() As Integer
        Get
            Return _category_id
        End Get
        Set(ByVal value As Integer)
            _category_id = value
        End Set

    End Property
    Public Property type() As Integer
        Get
            Return _type
        End Get
        Set(ByVal value As Integer)
            _type = value
        End Set

    End Property

    Public Property ref_id() As Integer
        Get
            Return _ref_id
        End Get
        Set(ByVal value As Integer)
            _ref_id = value
        End Set
    End Property
#End Region

    'Public Function Insert() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@name", name)
    '        oColSqlparram.Add("@alias", palias)
    '        oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@ip_address", Ip_address)
    '        oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@Tran_Type", "I")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Printer", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    'Public Function Update() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@name", name)
    '        oColSqlparram.Add("@alias", palias)
    '        oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@ip_address", Ip_address)
    '        oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@Tran_Type", "U")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Printer", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    'Public Function Delete() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@name", name)
    '        oColSqlparram.Add("@alias", palias)
    '        oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@ip_address", Ip_address)
    '        oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@Tran_Type", "D")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Printer", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Sales", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectView]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Sales_View", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Sales", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select1]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("WS_Get_Z_Report", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectReport]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@date2", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("WS_R_Sales", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectSalesDetailsReport]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@date2", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("WS_R_TSales", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectSalesProductSummaryReport]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@date2", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@category_id", category_id, SqlDbType.Int)
        'oColSqlparram.Add("@Duration", "This Year", SqlDbType.NVarChar)
        'oColSqlparram.Add("@type", "0", SqlDbType.NVarChar)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_R_Product", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectIngredient]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@tsales_id", tsales_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_T_Sales_Product_Ingredient", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectCondiment]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@tsales_id", tsales_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_T_Sales_Product_Condiment", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Selectcardview]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_T_Payment_card", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectDiscountByCmp]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Discount_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Cash_Control_BD]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@date2", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@LocationId", Location_id, SqlDbType.Int)
        oColSqlparram.Add("@duration", duration)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_R_Cash_Up_BD", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Stock_Transaction]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@date2", date2, SqlDbType.DateTime)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_R_Stock_Transaction_New", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Stock_Transaction_Details]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@date2", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@LocationId", Location_id)
        oColSqlparram.Add("@product_id", product_id)
        oColSqlparram.Add("@duration", duration)
        oColSqlparram.Add("@category_id", category_id)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_R_Stock_Transaction_new_Details", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Insert_ReSync() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
            oColSqlparram.Add("@ref_id", ref_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@flag", 0)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_ReSync", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function delete_Sync() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Delete_Sync", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_sales_email() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_sales_for_email", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_credit_sales_email() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@sales_id", table_uuid)
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_credit_sales_for_email", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_NonTable_SalesDetail_for_email() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_NonTable_SalesDetail_for_email", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_tsales_for_email() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_tsales_for_email", oColSqlparram)

        Return dtlogin
    End Function

    Public Function update_email_flag_sales() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@sales_id", sales_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("update_email_flag_sales", oColSqlparram)

        Return dtlogin
    End Function
End Class

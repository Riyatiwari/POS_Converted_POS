Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsPrinter
    ' Inherits BaseClass
    'Dim oClsDal As ClsDataccess = New ClsDataccess()
    'Public Sub New()
    'End Sub

#Region "Public Variables"
    Private _printer_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _alias As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _printer_ip_address As String
    Private _port As Integer
    Private _network_type As String
    Private _vender_id As Integer
    Private _budrate As String
    Private _device_name As String
    Private _is_product_small_large As Byte
    Private _is_condiment_small_large As Byte
    Private _group_by As Integer
    Private _consile_date As Integer
    Private _group_by_with As Integer
    Private _OrderFlag As Byte
    Private objclsPrinter As clsPrinter
#End Region

#Region "Public Property"
    Public Property is_product_small_large() As Byte
        Get
            Return _is_product_small_large
        End Get
        Set(ByVal value As Byte)
            _is_product_small_large = value
        End Set
    End Property
    Public Property is_condiment_small_large() As Byte
        Get
            Return _is_condiment_small_large
        End Get
        Set(ByVal value As Byte)
            _is_condiment_small_large = value
        End Set
    End Property
    Public Property vender_id() As Integer
        Get
            Return _vender_id
        End Get
        Set(ByVal value As Integer)
            _vender_id = value
        End Set
    End Property
    Public Property budrate() As String
        Get
            Return _budrate
        End Get
        Set(ByVal value As String)
            _budrate = value
        End Set
    End Property
    Public Property device_name() As String
        Get
            Return _device_name
        End Get
        Set(ByVal value As String)
            _device_name = value
        End Set
    End Property
    Public Property printer_ip_address() As String
        Get
            Return _printer_ip_address
        End Get
        Set(ByVal value As String)
            _printer_ip_address = value
        End Set
    End Property
    Public Property port() As Integer
        Get
            Return _port
        End Get
        Set(ByVal value As Integer)
            _port = value
        End Set
    End Property
    Public Property network_type() As String
        Get
            Return _network_type
        End Get
        Set(ByVal value As String)
            _network_type = value
        End Set
    End Property

    Public Property printer_id() As Integer
        Get
            Return _printer_id
        End Get
        Set(ByVal value As Integer)
            _printer_id = value
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
    Public Property palias() As String
        Get
            Return _alias
        End Get
        Set(ByVal value As String)
            _alias = value
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
    Public Property OrderFlag() As Byte
        Get
            Return _OrderFlag
        End Get
        Set(ByVal value As Byte)
            _OrderFlag = value
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
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property

    Public Property group_by() As Integer
        Get
            Return _group_by
        End Get
        Set(ByVal value As Integer)
            _group_by = value
        End Set
    End Property
    Public Property group_by_with() As Integer
        Get
            Return _group_by_with
        End Get
        Set(ByVal value As Integer)
            _group_by_with = value
        End Set
    End Property

    Public Property consile_date() As Integer
        Get
            Return _consile_date
        End Get
        Set(ByVal value As Integer)
            _consile_date = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            objclsPrinter = New clsPrinter()
            objclsPrinter.printer_id = printer_id
            objclsPrinter.cmp_id = cmp_id
            objclsPrinter.name = name
            objclsPrinter.palias = palias
            objclsPrinter.Is_active = Is_active
            objclsPrinter.Ip_address = Ip_address
            objclsPrinter.login_id = login_id
            objclsPrinter.machine_id = machine_id
            objclsPrinter.printer_ip_address = printer_ip_address
            objclsPrinter.port = port
            objclsPrinter.network_type = network_type
            objclsPrinter.vender_id = vender_id
            objclsPrinter.budrate = budrate
            objclsPrinter.device_name = device_name
            objclsPrinter.is_product_small_large = is_product_small_large
            objclsPrinter.is_condiment_small_large = is_condiment_small_large
            objclsPrinter.group_by_with = group_by_with
            objclsPrinter.group_by = group_by
            objclsPrinter.consile_date = consile_date
            objclsPrinter.OrderFlag = OrderFlag

            If objclsPrinter.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsPrinter = New clsPrinter()
            objclsPrinter.printer_id = printer_id
            objclsPrinter.cmp_id = cmp_id
            objclsPrinter.name = name
            objclsPrinter.palias = palias
            objclsPrinter.Is_active = Is_active
            objclsPrinter.Ip_address = Ip_address
            objclsPrinter.login_id = login_id
            objclsPrinter.machine_id = machine_id
            objclsPrinter.printer_ip_address = printer_ip_address
            objclsPrinter.port = port
            objclsPrinter.network_type = network_type
            objclsPrinter.vender_id = vender_id
            objclsPrinter.budrate = budrate
            objclsPrinter.device_name = device_name
            objclsPrinter.is_product_small_large = is_product_small_large
            objclsPrinter.is_condiment_small_large = is_condiment_small_large
            objclsPrinter.group_by_with = group_by_with
            objclsPrinter.group_by = group_by
            objclsPrinter.consile_date = consile_date
            objclsPrinter.OrderFlag = OrderFlag

            If objclsPrinter.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsPrinter = New clsPrinter()
            objclsPrinter.printer_id = printer_id
            objclsPrinter.cmp_id = cmp_id
            objclsPrinter.name = name
            objclsPrinter.palias = palias
            objclsPrinter.Is_active = Is_active
            objclsPrinter.Ip_address = Ip_address
            objclsPrinter.login_id = login_id
            objclsPrinter.Tran_Type = Tran_Type
            objclsPrinter.printer_ip_address = printer_ip_address
            objclsPrinter.port = port
            objclsPrinter.network_type = network_type
            objclsPrinter.vender_id = vender_id
            objclsPrinter.budrate = budrate
            objclsPrinter.device_name = device_name
            objclsPrinter.is_product_small_large = is_product_small_large
            objclsPrinter.is_condiment_small_large = is_condiment_small_large
            objclsPrinter.group_by_with = group_by_with
            objclsPrinter.group_by = group_by
            objclsPrinter.consile_date = consile_date
            objclsPrinter.OrderFlag = OrderFlag

            If objclsPrinter.Delete() Then
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
            objclsPrinter = New clsPrinter()
            objclsPrinter.printer_id = printer_id
            objclsPrinter.cmp_id = cmp_id
            dt = objclsPrinter.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsPrinter = New clsPrinter()
            'objclsPrinter.printer_id = printer_id
            objclsPrinter.cmp_id = cmp_id
            dt = objclsPrinter.SelectAll()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectPrinterByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsPrinter = New clsPrinter()
            objclsPrinter.cmp_id = cmp_id
            dt = objclsPrinter.[SelectPrinterByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPrinter
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _printer_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _alias As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _printer_ip_address As String
    Private _port As Integer
    Private _network_type As String
    Private _vender_id As Integer
    Private _budrate As String
    Private _device_name As String
    Private _is_product_small_large As Byte
    Private _is_condiment_small_large As Byte
    Private _group_by As Integer
    Private _consile_date As Integer
    Private _group_by_with As Integer
    Private _OrderFlag As Byte
    Private objclsPrinter As clsPrinter
#End Region

#Region "Public Property"

    Public Property OrderFlag() As Byte
        Get
            Return _OrderFlag
        End Get
        Set(ByVal value As Byte)
            _OrderFlag = value
        End Set
    End Property

    Public Property is_product_small_large() As Byte
        Get
            Return _is_product_small_large
        End Get
        Set(ByVal value As Byte)
            _is_product_small_large = value
        End Set
    End Property
    Public Property is_condiment_small_large() As Byte
        Get
            Return _is_condiment_small_large
        End Get
        Set(ByVal value As Byte)
            _is_condiment_small_large = value
        End Set
    End Property
    Public Property vender_id() As Integer
        Get
            Return _vender_id
        End Get
        Set(ByVal value As Integer)
            _vender_id = value
        End Set
    End Property
    Public Property budrate() As String
        Get
            Return _budrate
        End Get
        Set(ByVal value As String)
            _budrate = value
        End Set
    End Property
    Public Property device_name() As String
        Get
            Return _device_name
        End Get
        Set(ByVal value As String)
            _device_name = value
        End Set
    End Property
    Public Property printer_ip_address() As String
        Get
            Return _printer_ip_address
        End Get
        Set(ByVal value As String)
            _printer_ip_address = value
        End Set
    End Property
    Public Property port() As Integer
        Get
            Return _port
        End Get
        Set(ByVal value As Integer)
            _port = value
        End Set
    End Property
    Public Property network_type() As String
        Get
            Return _network_type
        End Get
        Set(ByVal value As String)
            _network_type = value
        End Set
    End Property

    Public Property printer_id() As Integer
        Get
            Return _printer_id
        End Get
        Set(ByVal value As Integer)
            _printer_id = value
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
    Public Property palias() As String
        Get
            Return _alias
        End Get
        Set(ByVal value As String)
            _alias = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
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
    Public Property group_by() As Integer
        Get
            Return _group_by
        End Get
        Set(ByVal value As Integer)
            _group_by = value
        End Set
    End Property
    Public Property group_by_with() As Integer
        Get
            Return _group_by_with
        End Get
        Set(ByVal value As Integer)
            _group_by_with = value
        End Set
    End Property

    Public Property consile_date() As Integer
        Get
            Return _consile_date
        End Get
        Set(ByVal value As Integer)
            _consile_date = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@alias", palias)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@printer_ip_address", printer_ip_address)
            oColSqlparram.Add("@port", port, SqlDbType.Int)
            oColSqlparram.Add("@network_type", network_type)
            oColSqlparram.Add("@vender_id", vender_id, SqlDbType.Int)
            oColSqlparram.Add("@budrate", budrate)
            oColSqlparram.Add("@device_name", device_name)
            oColSqlparram.Add("@is_product_small_large", is_product_small_large, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_condiment_small_large", is_condiment_small_large, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by", group_by, SqlDbType.TinyInt)
            oColSqlparram.Add("@consile_date", consile_date, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by_with", group_by_with, SqlDbType.TinyInt)
            oColSqlparram.Add("@OrderFlag", OrderFlag, SqlDbType.TinyInt)

            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Printer", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@alias", palias)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@printer_ip_address", printer_ip_address)
            oColSqlparram.Add("@port", port, SqlDbType.Int)
            oColSqlparram.Add("@network_type", network_type)
            oColSqlparram.Add("@vender_id", vender_id, SqlDbType.Int)
            oColSqlparram.Add("@budrate", budrate)
            oColSqlparram.Add("@device_name", device_name)
            oColSqlparram.Add("@is_product_small_large", is_product_small_large, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_condiment_small_large", is_condiment_small_large, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by", group_by, SqlDbType.TinyInt)
            oColSqlparram.Add("@consile_date", consile_date, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by_with", group_by_with, SqlDbType.TinyInt)
            oColSqlparram.Add("@OrderFlag", OrderFlag, SqlDbType.TinyInt)

            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Printer", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@alias", palias)
            oColSqlparram.Add("@is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@printer_ip_address", printer_ip_address)
            oColSqlparram.Add("@port", port, SqlDbType.Int)
            oColSqlparram.Add("@network_type", network_type)
            oColSqlparram.Add("@vender_id", vender_id, SqlDbType.Int)
            oColSqlparram.Add("@budrate", budrate)
            oColSqlparram.Add("@device_name", device_name)
            oColSqlparram.Add("@is_product_small_large", is_product_small_large, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_condiment_small_large", is_condiment_small_large, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by", group_by, SqlDbType.TinyInt)
            oColSqlparram.Add("@consile_date", consile_date, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by_with", group_by_with, SqlDbType.TinyInt)
            oColSqlparram.Add("@OrderFlag", OrderFlag, SqlDbType.TinyInt)

            oColSqlparram.Add("@Tran_Type", "D")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Printer", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Printer", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@printer_id", printer_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Printer", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectPrinterByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Printer_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function
End Class
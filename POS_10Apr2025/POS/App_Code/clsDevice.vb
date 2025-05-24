Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsDevice
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _device_id As Integer
    Private _machine_id As Integer
    Private _cmp_id As Integer
    Private _code As String
    Private _name As String
    Private _serial_no As String
    Private _ip_address As String
    Private _login_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _Device_Type_id As Integer
    Private _is_active As Byte
    Private _printer_ip_address As String
    Private _port As Integer
    Private _network_type As String
    Private _vender_id As Integer
    Private _budrate As String
    Private _device_name As String
    Private _Width As Integer
    Private _user_name As String
    Private _password As String
    Private _Bluetooth_name As String
    Private _API As String
    Private _service_key As String
    Private _Device_SubType As Integer
    Private _cat_id As Integer
    Private objclsDevice As clsDevice

#End Region

#Region "Public Property"
    Public Property cat_id() As Integer
        Get
            Return _cat_id
        End Get
        Set(ByVal value As Integer)
            _cat_id = value
        End Set
    End Property
    Public Property Width() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal value As Integer)
            _Width = value
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

    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property Device_SubType() As Integer
        Get
            Return _Device_SubType
        End Get
        Set(ByVal value As Integer)
            _Device_SubType = value
        End Set

    End Property
    Public Property device_id() As Integer
        Get
            Return _device_id
        End Get
        Set(ByVal value As Integer)
            _device_id = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
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

    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property serial_no() As String
        Get
            Return _serial_no
        End Get
        Set(ByVal value As String)
            _serial_no = value
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
    Public Property Device_Type_id() As Integer
        Get
            Return _Device_Type_id
        End Get
        Set(ByVal value As Integer)
            _Device_Type_id = value
        End Set
    End Property


    Public Property User_Name() As String
        Get
            Return _user_name
        End Get
        Set(ByVal value As String)
            _user_name = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property
    Public Property Bluetooth_Name() As String
        Get
            Return _Bluetooth_name
        End Get
        Set(ByVal value As String)
            _Bluetooth_name = value
        End Set
    End Property


    Public Property API() As String
        Get
            Return _API
        End Get
        Set(ByVal value As String)
            _API = value
        End Set
    End Property


    Public Property Service_Key() As String
        Get
            Return _service_key
        End Get
        Set(ByVal value As String)
            _service_key = value
        End Set
    End Property

#End Region

    Public Function Insert() As Boolean
        Try
            objclsDevice = New clsDevice()
            objclsDevice.device_id = device_id
            objclsDevice.machine_id = machine_id
            objclsDevice.cmp_id = cmp_id
            objclsDevice.code = code
            objclsDevice.name = name
            objclsDevice.serial_no = serial_no
            objclsDevice.ip_address = ip_address
            objclsDevice.login_id = login_id
            objclsDevice.Device_Type_id = Device_Type_id
            objclsDevice.is_active = is_active
            objclsDevice.printer_ip_address = printer_ip_address
            objclsDevice.port = port
            objclsDevice.network_type = network_type
            objclsDevice.vender_id = vender_id
            objclsDevice.budrate = budrate
            objclsDevice.device_name = device_name
            objclsDevice.Width = Width
            objclsDevice.User_Name = User_Name
            objclsDevice.Password = Password
            objclsDevice.Bluetooth_Name = Bluetooth_Name
            objclsDevice.API = API
            objclsDevice.Service_key = Service_Key
            objclsDevice.Device_SubType = Device_SubType
            objclsDevice.cat_id = cat_id
            'objclsDevice.LAN_NO = LAN_no

            If objclsDevice.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsDevice = New clsDevice()
            objclsDevice.device_id = device_id
            objclsDevice.machine_id = machine_id
            objclsDevice.cmp_id = cmp_id
            objclsDevice.code = code
            objclsDevice.name = name
            objclsDevice.serial_no = serial_no
            objclsDevice.ip_address = ip_address
            objclsDevice.login_id = login_id
            objclsDevice.Device_Type_id = Device_Type_id
            objclsDevice.is_active = is_active
            objclsDevice.printer_ip_address = printer_ip_address
            objclsDevice.port = port
            objclsDevice.network_type = network_type
            objclsDevice.vender_id = vender_id
            objclsDevice.budrate = budrate
            objclsDevice.device_name = device_name
            objclsDevice.Width = Width
            objclsDevice.User_Name = User_Name
            objclsDevice.Password = Password
            objclsDevice.Bluetooth_Name = Bluetooth_Name
            objclsDevice.API = API
            objclsDevice.Service_key = Service_Key
            objclsDevice.Device_SubType = Device_SubType
            objclsDevice.cat_id = cat_id
            If objclsDevice.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsDevice = New clsDevice()
            objclsDevice.device_id = device_id
            objclsDevice.machine_id = machine_id
            objclsDevice.cmp_id = cmp_id
            objclsDevice.code = code
            objclsDevice.name = name
            objclsDevice.serial_no = serial_no
            objclsDevice.ip_address = ip_address
            objclsDevice.login_id = login_id
            objclsDevice.Device_Type_id = Device_Type_id
            objclsDevice.is_active = is_active
            objclsDevice.printer_ip_address = printer_ip_address
            objclsDevice.port = port
            objclsDevice.network_type = network_type
            objclsDevice.vender_id = vender_id
            objclsDevice.budrate = budrate
            objclsDevice.device_name = device_name
            objclsDevice.Width = Width
            objclsDevice.User_Name = User_Name
            objclsDevice.Password = Password
            objclsDevice.Bluetooth_Name = Bluetooth_Name
            objclsDevice.API = API
            objclsDevice.Service_key = Service_Key
            objclsDevice.Device_SubType = Device_SubType
            objclsDevice.cat_id = cat_id
            If objclsDevice.Delete() Then
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
            objclsDevice = New clsDevice()
            objclsDevice.device_id = device_id
            objclsDevice.cmp_id = cmp_id
            dt = objclsDevice.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsDevice = New clsDevice()
            'objclsDevice.device_id = device_id
            objclsDevice.cmp_id = cmp_id
            dt = objclsDevice.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectDeviceByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsDevice = New clsDevice()
            objclsDevice.cmp_id = cmp_id
            dt = objclsDevice.[SelectDeviceByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectDeviceByMachine]() As DataTable
        Dim dt As DataTable
        Try
            objclsDevice = New clsDevice()
            objclsDevice.cmp_id = cmp_id
            objclsDevice.machine_id = machine_id
            dt = objclsDevice.[SelectDeviceByMachine]()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

End Class

Public Class clsDevice
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _device_id As Integer
    Private _machine_id As Integer
    Private _cmp_id As Integer
    Private _code As String
    Private _name As String
    Private _serial_no As String
    Private _ip_address As String
    Private _login_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _Device_Type_id As Integer
    Private _is_active As Byte
    Private _printer_ip_address As String
    Private _port As Integer
    Private _network_type As String
    Private _vender_id As Integer
    Private _budrate As String
    Private _device_name As String
    Private _Width As Integer
    Private _User_Name As String
    Private _Password As String
    Private _Bluetooth_name As String
    Private _API As String
    Private _service_key As String
    Private _Device_SubType As Integer
    Private _cat_id As Integer
    'Private _LAN_NO As String
    Private objclsDevice As clsDevice

#End Region

#Region "Public Property"
    Public Property cat_id() As Integer
        Get
            Return _cat_id
        End Get
        Set(ByVal value As Integer)
            _cat_id = value
        End Set

    End Property
    Public Property Device_SubType() As Integer
        Get
            Return _Device_SubType
        End Get
        Set(ByVal value As Integer)
            _Device_SubType = value
        End Set

    End Property
    Public Property Width() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal value As Integer)
            _Width = value
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

    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property device_id() As Integer
        Get
            Return _device_id
        End Get
        Set(ByVal value As Integer)
            _device_id = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
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

    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property serial_no() As String
        Get
            Return _serial_no
        End Get
        Set(ByVal value As String)
            _serial_no = value
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
    Public Property Device_Type_id() As Integer
        Get
            Return _Device_Type_id
        End Get
        Set(ByVal value As Integer)
            _Device_Type_id = value
        End Set
    End Property

    Public Property User_Name() As String
        Get
            Return _User_Name
        End Get
        Set(ByVal value As String)
            _User_Name = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property Bluetooth_Name() As String
        Get
            Return _Bluetooth_name
        End Get
        Set(ByVal value As String)
            _Bluetooth_name = value
        End Set
    End Property

    Public Property API() As String
        Get
            Return _API
        End Get
        Set(ByVal value As String)
            _API = value
        End Set
    End Property


    Public Property Service_key() As String
        Get
            Return _service_key
        End Get
        Set(ByVal value As String)
            _service_key = value
        End Set
    End Property
#End Region
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@device_id", device_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@serial_no", serial_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@printer_ip_address", printer_ip_address)
            oColSqlparram.Add("@port", port, SqlDbType.Int)
            oColSqlparram.Add("@network_type", network_type)
            oColSqlparram.Add("@vender_id", vender_id, SqlDbType.Int)
            oColSqlparram.Add("@budrate", budrate)
            oColSqlparram.Add("@device_name", device_name)
            oColSqlparram.Add("@width", Width, SqlDbType.Int)
            oColSqlparram.Add("@user_name", User_Name, SqlDbType.NVarChar)
            oColSqlparram.Add("@password", Password, SqlDbType.NVarChar)
            oColSqlparram.Add("@bluetooth_name", Bluetooth_Name, SqlDbType.NVarChar)
            oColSqlparram.Add("@service_key", Service_key, SqlDbType.NVarChar)
            oColSqlparram.Add("@application_profile_id", API, SqlDbType.NVarChar)
            oColSqlparram.Add("@Device_SubType", Device_SubType, SqlDbType.Int)
            oColSqlparram.Add("@cat_id", cat_id, SqlDbType.Int)
            'oColSqlparram.Add("@lan_no", LAN_NO, SqlDbType.NVarChar)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Device", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@device_id", device_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@serial_no", serial_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@printer_ip_address", printer_ip_address)
            oColSqlparram.Add("@port", port, SqlDbType.Int)
            oColSqlparram.Add("@network_type", network_type)
            oColSqlparram.Add("@vender_id", vender_id, SqlDbType.Int)
            oColSqlparram.Add("@budrate", budrate)
            oColSqlparram.Add("@device_name", device_name)
            oColSqlparram.Add("@width", Width, SqlDbType.Int)
            oColSqlparram.Add("@user_name", User_Name, SqlDbType.NVarChar)
            oColSqlparram.Add("@password", Password, SqlDbType.NVarChar)
            oColSqlparram.Add("@bluetooth_name", Bluetooth_Name, SqlDbType.NVarChar)
            oColSqlparram.Add("@service_key", Service_key, SqlDbType.NVarChar)
            oColSqlparram.Add("@application_profile_id", API, SqlDbType.NVarChar)
            oColSqlparram.Add("@Device_SubType", Device_SubType, SqlDbType.Int)
            oColSqlparram.Add("@cat_id", cat_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Device", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@device_id", device_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@serial_no", serial_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@printer_ip_address", printer_ip_address)
            oColSqlparram.Add("@port", port, SqlDbType.Int)
            oColSqlparram.Add("@network_type", network_type)
            oColSqlparram.Add("@vender_id", vender_id, SqlDbType.Int)
            oColSqlparram.Add("@budrate", budrate)
            oColSqlparram.Add("@device_name", device_name)
            oColSqlparram.Add("@width", Width, SqlDbType.Int)
            oColSqlparram.Add("@user_name", User_Name, SqlDbType.NVarChar)
            oColSqlparram.Add("@password", Password, SqlDbType.NVarChar)
            oColSqlparram.Add("@bluetooth_name", Bluetooth_Name, SqlDbType.NVarChar)
            oColSqlparram.Add("@service_key", Service_key, SqlDbType.NVarChar)
            oColSqlparram.Add("@application_profile_id", API, SqlDbType.NVarChar)
            oColSqlparram.Add("@Device_SubType", Device_SubType, SqlDbType.Int)
            oColSqlparram.Add("@cat_id", cat_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Device", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@device_id", device_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Device", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@device_id", device_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Device", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectDeviceByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Device_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectDeviceByMachine]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Device_By_Machine", oColSqlparram)

        Return dtlogin
    End Function

End Class

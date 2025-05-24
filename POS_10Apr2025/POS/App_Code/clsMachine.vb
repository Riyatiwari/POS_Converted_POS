Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Public Class Controller_clsMachine
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _machine_id As Integer
    Private _cmp_id As Integer
    Private _code As String
    Private _name As String
    Private _mac_address As String
    Private _model As String
    Private _serial_no As String
    Private _ip_address As String
    Private _login_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _location_id As Integer
    Private _is_assign As Byte
    Private _is_minipos As Byte
    Private _is_active As Byte
    Private _Receipt_Header As String
    Private _is_master_self As Byte
    Private _Receipt_Footer As String
    Private _keymap_id As Integer
    Private _function_id As Integer
    Private _till_id As Integer
    Private _tillip_address As String
    Private _master_till As Integer
    Private _till_server As Byte
    Private _table_sharing As Byte
    Private _printer_sharing As Byte
    Private _sync_time As String
    Private _back_to_main_function_on_till As Byte
    Private _extraSurcharge As Byte
    Private _date1 As System.DateTime
    Private _date2 As System.DateTime
    Private _duration As String
    Private _shorting_num As Integer
    Private _onlytabletrans As Byte
    Private _AutoSurcharge As Byte
    Private _AutoSurchargeTables As Byte
    Private _AutoSurchargeNonTables As Byte
    Private _AutoSurchargeCloakroom As Byte
    Private _AutoSurchargeChips As Byte
    Private _NoCashDrawer As Byte
    Private _ReSync_Request As Byte
    Private _Service_Controller_Start As Byte
    Private _Service_Websale_print As Byte
    Private _Service_Print_Share As Byte
    Private _Service_print_Share_Other_Till As Byte
    Private _Is_NoLogout As Byte
    Private _Is_PrintServer As Byte
    Private _Is_ServiceBooking As Byte
    Private _Is_OnlineZreport As Byte
    Private _IsKiosk As Byte
    Private _TranLimit As Integer
    Private _gtway_TID As String
    Private _QuickTran As Byte
    Private _tillRequest As Byte
    Private _KioskRequest As Byte
    Private _kitchenPrint As Byte
    Private _ReceiptPrint As Byte
    Private _ElinaTran As Byte

    Private _posLite As Byte
    Private _sunmiSecondScreen As Byte
    Private _secondScreenImage1 As Byte()
    Private _secondScreenImage2 As Byte()
    Private _sunmi_video_path As String
    Private _hardware_type As Integer
    Private _TillUUID As String
    Private _generatecode As Integer
    Private _category_name As String
    Private _valid As DateTime
    Private objclsMachine As clsMachine

#End Region

#Region "Public Property"

    Public Property hardware_type() As Integer
        Get
            Return _hardware_type
        End Get
        Set(ByVal value As Integer)
            _hardware_type = value
        End Set
    End Property
    Public Property POSlite() As Byte
        Get
            Return _posLite
        End Get
        Set(ByVal value As Byte)
            _posLite = value
        End Set
    End Property

    Public Property SunmiSecondScreen() As Byte
        Get
            Return _sunmiSecondScreen
        End Get
        Set(ByVal value As Byte)
            _sunmiSecondScreen = value
        End Set
    End Property
    Public Property SecondScreenImage1() As Byte()
        Get
            Return _secondScreenImage1
        End Get
        Set(ByVal value As Byte())
            _secondScreenImage1 = value
        End Set
    End Property

    Public Property SecondScreenImage2() As Byte()
        Get
            Return _secondScreenImage2
        End Get
        Set(ByVal value As Byte())
            _secondScreenImage2 = value
        End Set
    End Property


    Public Property sunmi_video_path() As String
        Get
            Return _sunmi_video_path
        End Get
        Set(value As String)
            _sunmi_video_path = value
        End Set

    End Property
    Public Property ElinaTran() As Byte
        Get
            Return _ElinaTran
        End Get
        Set(ByVal value As Byte)
            _ElinaTran = value
        End Set
    End Property

    Public Property gtway_TID() As String
        Get
            Return _gtway_TID
        End Get
        Set(ByVal value As String)
            _gtway_TID = value
        End Set
    End Property

    Public Property TranLimit() As Integer
        Get
            Return _TranLimit
        End Get
        Set(ByVal value As Integer)
            _TranLimit = value
        End Set
    End Property

    Public Property Is_PrintServer() As Byte
        Get
            Return _Is_PrintServer
        End Get
        Set(ByVal value As Byte)
            _Is_PrintServer = value
        End Set
    End Property

    Public Property IsKiosk() As Byte
        Get
            Return _IsKiosk
        End Get
        Set(ByVal value As Byte)
            _IsKiosk = value
        End Set
    End Property

    Public Property QuickTran() As Byte
        Get
            Return _QuickTran
        End Get
        Set(ByVal value As Byte)
            _QuickTran = value
        End Set
    End Property
    Public Property tillRequest() As Byte
        Get
            Return _tillRequest
        End Get
        Set(ByVal value As Byte)
            _tillRequest = value
        End Set
    End Property
    Public Property KioskRequest() As Byte
        Get
            Return _KioskRequest
        End Get
        Set(ByVal value As Byte)
            _KioskRequest = value
        End Set
    End Property

    Public Property kitchenPrint() As Byte
        Get
            Return _kitchenPrint
        End Get
        Set(ByVal value As Byte)
            _kitchenPrint = value
        End Set
    End Property

    Public Property ReceiptPrint() As Byte
        Get
            Return _ReceiptPrint
        End Get
        Set(ByVal value As Byte)
            _ReceiptPrint = value
        End Set
    End Property

    Public Property Is_OnlineZreport() As Byte
        Get
            Return _Is_OnlineZreport
        End Get
        Set(ByVal value As Byte)
            _Is_OnlineZreport = value
        End Set
    End Property

    Public Property Is_ServiceBooking() As Byte
        Get
            Return _Is_ServiceBooking
        End Get
        Set(ByVal value As Byte)
            _Is_ServiceBooking = value
        End Set
    End Property


    Public Property Is_NoLogout() As Byte
        Get
            Return _Is_NoLogout
        End Get
        Set(ByVal value As Byte)
            _Is_NoLogout = value
        End Set
    End Property

    Public Property NoCashDrawer() As Byte
        Get
            Return _NoCashDrawer
        End Get
        Set(ByVal value As Byte)
            _NoCashDrawer = value
        End Set
    End Property
    Public Property Service_Controller_Start() As Byte
        Get
            Return _Service_Controller_Start
        End Get
        Set(ByVal value As Byte)
            _Service_Controller_Start = value
        End Set
    End Property
    Public Property Service_Websale_print() As Byte
        Get
            Return _Service_Websale_print
        End Get
        Set(ByVal value As Byte)
            _Service_Websale_print = value
        End Set
    End Property
    Public Property Service_Print_Share() As Byte
        Get
            Return _Service_Print_Share
        End Get
        Set(ByVal value As Byte)
            _Service_Print_Share = value
        End Set
    End Property
    Public Property Service_print_Share_Other_Till() As Byte
        Get
            Return _Service_print_Share_Other_Till
        End Get
        Set(ByVal value As Byte)
            _Service_print_Share_Other_Till = value
        End Set
    End Property
    Public Property ReSync_Request() As Byte
        Get
            Return _ReSync_Request
        End Get
        Set(ByVal value As Byte)
            _ReSync_Request = value
        End Set
    End Property
    Public Property AutoSurcharge() As Byte
        Get
            Return _AutoSurcharge
        End Get
        Set(ByVal value As Byte)
            _AutoSurcharge = value
        End Set
    End Property
    Public Property AutoSurchargeTables() As Byte
        Get
            Return _AutoSurchargeTables
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeTables = value
        End Set
    End Property
    Public Property AutoSurchargeNonTables() As Byte
        Get
            Return _AutoSurchargeNonTables
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeNonTables = value
        End Set
    End Property
    Public Property AutoSurchargeCloakroom() As Byte
        Get
            Return _AutoSurchargeCloakroom
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeCloakroom = value
        End Set
    End Property
    Public Property AutoSurchargeChips() As Byte
        Get
            Return _AutoSurchargeChips
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeChips = value
        End Set
    End Property


    Public Property onlytabletrans() As Byte
        Get
            Return _onlytabletrans
        End Get
        Set(ByVal value As Byte)
            _onlytabletrans = value
        End Set
    End Property
    Public Property extraSurcharge() As Byte
        Get
            Return _extraSurcharge
        End Get
        Set(ByVal value As Byte)
            _extraSurcharge = value
        End Set
    End Property
    Public Property back_to_main_function_on_till() As Byte
        Get
            Return _back_to_main_function_on_till
        End Get
        Set(ByVal value As Byte)
            _back_to_main_function_on_till = value
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

    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property
    Public Property till_id() As Integer
        Get
            Return _till_id
        End Get
        Set(ByVal value As Integer)
            _till_id = value
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

    Public Property mac_address() As String
        Get
            Return _mac_address
        End Get
        Set(ByVal value As String)
            _mac_address = value
        End Set
    End Property

    Public Property model() As String
        Get
            Return _model
        End Get
        Set(ByVal value As String)
            _model = value
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
    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property

    Public Property is_assign() As Byte
        Get
            Return _is_assign
        End Get
        Set(ByVal value As Byte)
            _is_assign = value
        End Set
    End Property
    Public Property is_minipos() As Byte
        Get
            Return _is_minipos
        End Get
        Set(ByVal value As Byte)
            _is_minipos = value
        End Set
    End Property
    Public Property is_master_self() As Byte
        Get
            Return _is_master_self
        End Get
        Set(ByVal value As Byte)
            _is_master_self = value
        End Set
    End Property
    Public Property Receipt_Header() As String
        Get
            Return _Receipt_Header
        End Get
        Set(ByVal value As String)
            _Receipt_Header = value
        End Set
    End Property

    Public Property Receipt_Footer() As String
        Get
            Return _Receipt_Footer
        End Get
        Set(ByVal value As String)
            _Receipt_Footer = value
        End Set
    End Property

    Public Property table_sharing() As Byte
        Get
            Return _table_sharing
        End Get
        Set(ByVal value As Byte)
            _table_sharing = value
        End Set
    End Property
    Public Property printer_sharing() As Byte
        Get
            Return _printer_sharing
        End Get
        Set(ByVal value As Byte)
            _printer_sharing = value
        End Set
    End Property
    Public Property tillip_address() As String
        Get
            Return _tillip_address
        End Get
        Set(ByVal value As String)
            _tillip_address = value
        End Set
    End Property
    Public Property till_server() As Byte
        Get
            Return _till_server
        End Get
        Set(ByVal value As Byte)
            _till_server = value
        End Set
    End Property
    Public Property master_till() As Integer
        Get
            Return _master_till
        End Get
        Set(ByVal value As Integer)
            _master_till = value
        End Set
    End Property

    Public Property sync_time() As String
        Get
            Return _sync_time
        End Get
        Set(ByVal value As String)
            _sync_time = value
        End Set
    End Property

    Public Property keymap_id() As Integer
        Get
            Return _keymap_id
        End Get
        Set(ByVal value As Integer)
            _keymap_id = value
        End Set
    End Property

    Public Property shorting_num() As Integer
        Get
            Return _shorting_num
        End Get
        Set(ByVal value As Integer)
            _shorting_num = value
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

    'add by hardik
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

    Public Property duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
        End Set
    End Property
    Public Property TillUUID() As String
        Get
            Return _TillUUID
        End Get
        Set(ByVal value As String)
            _TillUUID = value
        End Set
    End Property
    Public Property category_name() As String
        Get
            Return _category_name
        End Get
        Set(ByVal value As String)
            _category_name = value
        End Set
    End Property
    Public Property generatecode() As Integer
        Get
            Return _generatecode
        End Get
        Set(ByVal value As Integer)
            _generatecode = value
        End Set
    End Property
    Public Property valid() As DateTime
        Get
            Return _valid
        End Get
        Set(ByVal value As DateTime)
            _valid = value
        End Set
    End Property

#End Region

    Public Function InsertMainFunctionchk() As Integer
        Try
            objclsMachine = New clsMachine()
            objclsMachine.function_id = function_id
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.till_id = till_id

            Dim r As Integer
            r = objclsMachine.InsertMainFunctionchksave()
            Return r
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insertkeymapcheck() As Integer
        Try
            objclsMachine = New clsMachine()
            objclsMachine.keymap_id = keymap_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.till_id = till_id
            objclsMachine.machine_id = machine_id
            objclsMachine.shorting_num = shorting_num

            Dim r As Integer
            r = objclsMachine.Insertkeymapchksave()
            Return r
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function updateMainFunctionchk() As Integer
        Try
            objclsMachine = New clsMachine()
            objclsMachine.function_id = function_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.till_id = till_id

            Dim r As Integer
            r = objclsMachine.updateMainFunctionchksave()
            Return r
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function updatekeymapcheck() As Integer
        Try
            objclsMachine = New clsMachine()
            objclsMachine.keymap_id = keymap_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.till_id = till_id

            Dim r As Integer
            r = objclsMachine.updatekeymapchksave()
            Return r
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert() As Integer
        Try
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.code = code
            objclsMachine.name = name
            objclsMachine.mac_address = mac_address
            objclsMachine.model = model
            objclsMachine.serial_no = serial_no
            objclsMachine.ip_address = ip_address
            objclsMachine.login_id = login_id
            objclsMachine.location_id = location_id
            objclsMachine.is_assign = is_assign
            objclsMachine.is_minipos = is_minipos
            objclsMachine.is_active = is_active
            objclsMachine.Receipt_Header = Receipt_Header
            objclsMachine.Receipt_Footer = Receipt_Footer
            objclsMachine.is_master_self = is_master_self
            objclsMachine.tillip_address = tillip_address
            objclsMachine.till_server = till_server
            objclsMachine.master_till = master_till
            objclsMachine.table_sharing = table_sharing
            objclsMachine.printer_sharing = printer_sharing
            objclsMachine.sync_time = sync_time
            objclsMachine.back_to_main_function_on_till = back_to_main_function_on_till
            objclsMachine.extraSurcharge = extraSurcharge
            objclsMachine.onlytabletrans = onlytabletrans
            objclsMachine.AutoSurcharge = AutoSurcharge
            objclsMachine.AutoSurchargeTables = AutoSurchargeTables
            objclsMachine.AutoSurchargeNonTables = AutoSurchargeNonTables
            objclsMachine.AutoSurchargeCloakroom = AutoSurchargeCloakroom
            objclsMachine.AutoSurchargeChips = AutoSurchargeChips
            objclsMachine.NoCashDrawer = NoCashDrawer
            objclsMachine.ReSync_Request = ReSync_Request
            objclsMachine.Service_Controller_Start = Service_Controller_Start
            objclsMachine.Service_Websale_print = Service_Websale_print
            objclsMachine.Service_Print_Share = Service_Print_Share
            objclsMachine.Service_print_Share_Other_Till = Service_print_Share_Other_Till
            objclsMachine.Is_NoLogout = Is_NoLogout
            objclsMachine.Is_PrintServer = Is_PrintServer
            objclsMachine.Is_ServiceBooking = Is_ServiceBooking
            objclsMachine.Is_OnlineZreport = Is_OnlineZreport
            objclsMachine.IsKiosk = IsKiosk
            objclsMachine.TranLimit = TranLimit
            objclsMachine.gtway_TID = gtway_TID
            objclsMachine.QuickTran = QuickTran
            objclsMachine.tillRequest = tillRequest
            objclsMachine.KioskRequest = KioskRequest
            objclsMachine.kitchenPrint = kitchenPrint
            objclsMachine.ReceiptPrint = ReceiptPrint
            objclsMachine.ElinaTran = ElinaTran

            objclsMachine.SunmiSecondScreen = SunmiSecondScreen
            objclsMachine.SecondScreenImage1 = SecondScreenImage1
            objclsMachine.SecondScreenImage2 = SecondScreenImage2
            objclsMachine.POSlite = POSlite
            objclsMachine.sunmi_video_path = sunmi_video_path
            objclsMachine.hardware_type = hardware_type
            Dim r As Integer
            r = objclsMachine.Insert()
            Return r
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.code = code
            objclsMachine.name = name
            objclsMachine.mac_address = mac_address
            objclsMachine.model = model
            objclsMachine.serial_no = serial_no
            objclsMachine.ip_address = ip_address
            objclsMachine.login_id = login_id
            objclsMachine.location_id = location_id
            objclsMachine.is_assign = is_assign
            objclsMachine.is_minipos = is_minipos
            objclsMachine.is_active = is_active
            objclsMachine.Receipt_Header = Receipt_Header
            objclsMachine.Receipt_Footer = Receipt_Footer
            objclsMachine.is_master_self = is_master_self
            objclsMachine.tillip_address = tillip_address
            objclsMachine.till_server = till_server
            objclsMachine.master_till = master_till
            objclsMachine.table_sharing = table_sharing
            objclsMachine.printer_sharing = printer_sharing
            objclsMachine.sync_time = sync_time
            objclsMachine.back_to_main_function_on_till = back_to_main_function_on_till
            objclsMachine.extraSurcharge = extraSurcharge
            objclsMachine.onlytabletrans = onlytabletrans
            objclsMachine.AutoSurcharge = AutoSurcharge
            objclsMachine.AutoSurchargeTables = AutoSurchargeTables
            objclsMachine.AutoSurchargeNonTables = AutoSurchargeNonTables
            objclsMachine.AutoSurchargeCloakroom = AutoSurchargeCloakroom
            objclsMachine.AutoSurchargeChips = AutoSurchargeChips
            objclsMachine.NoCashDrawer = NoCashDrawer
            objclsMachine.ReSync_Request = ReSync_Request
            objclsMachine.Service_Controller_Start = Service_Controller_Start
            objclsMachine.Service_Websale_print = Service_Websale_print
            objclsMachine.Service_Print_Share = Service_Print_Share
            objclsMachine.Service_print_Share_Other_Till = Service_print_Share_Other_Till
            objclsMachine.Is_NoLogout = Is_NoLogout
            objclsMachine.Is_PrintServer = Is_PrintServer
            objclsMachine.Is_ServiceBooking = Is_ServiceBooking
            objclsMachine.Is_OnlineZreport = Is_OnlineZreport
            objclsMachine.IsKiosk = IsKiosk
            objclsMachine.TranLimit = TranLimit
            objclsMachine.gtway_TID = gtway_TID
            objclsMachine.QuickTran = QuickTran
            objclsMachine.tillRequest = tillRequest
            objclsMachine.KioskRequest = KioskRequest
            objclsMachine.kitchenPrint = kitchenPrint
            objclsMachine.ReceiptPrint = ReceiptPrint
            objclsMachine.ElinaTran = ElinaTran

            objclsMachine.SunmiSecondScreen = SunmiSecondScreen
            objclsMachine.SecondScreenImage1 = SecondScreenImage1
            objclsMachine.SecondScreenImage2 = SecondScreenImage2
            objclsMachine.POSlite = POSlite
            objclsMachine.sunmi_video_path = sunmi_video_path
            objclsMachine.hardware_type = hardware_type
            If objclsMachine.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function registerwithQR() As Boolean
        Try
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.code = code
            objclsMachine.name = name
            objclsMachine.mac_address = mac_address
            objclsMachine.model = model
            objclsMachine.serial_no = serial_no
            objclsMachine.ip_address = ip_address
            objclsMachine.login_id = login_id
            objclsMachine.location_id = location_id
            objclsMachine.is_assign = is_assign
            objclsMachine.is_minipos = is_minipos
            objclsMachine.is_active = is_active
            objclsMachine.Receipt_Header = Receipt_Header
            objclsMachine.Receipt_Footer = Receipt_Footer
            objclsMachine.is_master_self = is_master_self
            objclsMachine.tillip_address = tillip_address
            objclsMachine.till_server = till_server
            objclsMachine.master_till = master_till
            objclsMachine.table_sharing = table_sharing
            objclsMachine.printer_sharing = printer_sharing
            objclsMachine.sync_time = sync_time
            objclsMachine.back_to_main_function_on_till = back_to_main_function_on_till
            objclsMachine.extraSurcharge = extraSurcharge
            objclsMachine.onlytabletrans = onlytabletrans
            objclsMachine.AutoSurcharge = AutoSurcharge
            objclsMachine.AutoSurchargeTables = AutoSurchargeTables
            objclsMachine.AutoSurchargeNonTables = AutoSurchargeNonTables
            objclsMachine.AutoSurchargeCloakroom = AutoSurchargeCloakroom
            objclsMachine.AutoSurchargeChips = AutoSurchargeChips
            objclsMachine.NoCashDrawer = NoCashDrawer
            objclsMachine.ReSync_Request = ReSync_Request
            objclsMachine.Service_Controller_Start = Service_Controller_Start
            objclsMachine.Service_Websale_print = Service_Websale_print
            objclsMachine.Service_Print_Share = Service_Print_Share
            objclsMachine.Service_print_Share_Other_Till = Service_print_Share_Other_Till
            objclsMachine.Is_NoLogout = Is_NoLogout
            objclsMachine.Is_PrintServer = Is_PrintServer
            objclsMachine.Is_ServiceBooking = Is_ServiceBooking
            objclsMachine.Is_OnlineZreport = Is_OnlineZreport
            objclsMachine.IsKiosk = IsKiosk
            objclsMachine.TranLimit = TranLimit
            objclsMachine.gtway_TID = gtway_TID
            objclsMachine.QuickTran = QuickTran
            objclsMachine.tillRequest = tillRequest
            objclsMachine.KioskRequest = KioskRequest
            objclsMachine.kitchenPrint = kitchenPrint
            objclsMachine.ReceiptPrint = ReceiptPrint
            objclsMachine.ElinaTran = ElinaTran

            objclsMachine.SunmiSecondScreen = SunmiSecondScreen
            objclsMachine.SecondScreenImage1 = SecondScreenImage1
            objclsMachine.SecondScreenImage2 = SecondScreenImage2
            objclsMachine.POSlite = POSlite
            objclsMachine.sunmi_video_path = sunmi_video_path
            objclsMachine.hardware_type = hardware_type
            objclsMachine.TillUUID = TillUUID.ToString
            If objclsMachine.registerwithQR() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function generateQR() As Boolean
        Try
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id

            objclsMachine.login_id = login_id
            objclsMachine.location_id = location_id

            objclsMachine.TillUUID = TillUUID.ToString
            If objclsMachine.generateQR() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            objclsMachine.code = code
            objclsMachine.name = name
            objclsMachine.mac_address = mac_address
            objclsMachine.model = model
            objclsMachine.serial_no = serial_no
            objclsMachine.ip_address = ip_address
            objclsMachine.login_id = login_id
            objclsMachine.location_id = location_id
            objclsMachine.Tran_Type = Tran_Type
            objclsMachine.is_assign = is_assign
            objclsMachine.is_minipos = is_minipos
            objclsMachine.is_active = is_active
            objclsMachine.Receipt_Header = Receipt_Header
            objclsMachine.Receipt_Footer = Receipt_Footer
            objclsMachine.tillip_address = tillip_address
            objclsMachine.till_server = till_server
            objclsMachine.master_till = master_till
            objclsMachine.table_sharing = table_sharing
            objclsMachine.printer_sharing = printer_sharing
            objclsMachine.sync_time = sync_time
            objclsMachine.back_to_main_function_on_till = back_to_main_function_on_till
            objclsMachine.extraSurcharge = extraSurcharge
            objclsMachine.onlytabletrans = onlytabletrans
            objclsMachine.AutoSurcharge = AutoSurcharge
            objclsMachine.AutoSurchargeTables = AutoSurchargeTables
            objclsMachine.AutoSurchargeNonTables = AutoSurchargeNonTables
            objclsMachine.AutoSurchargeCloakroom = AutoSurchargeCloakroom
            objclsMachine.AutoSurchargeChips = AutoSurchargeChips
            objclsMachine.NoCashDrawer = NoCashDrawer
            objclsMachine.ReSync_Request = ReSync_Request
            objclsMachine.Service_Controller_Start = Service_Controller_Start
            objclsMachine.Service_Websale_print = Service_Websale_print
            objclsMachine.Service_Print_Share = Service_Print_Share
            objclsMachine.Service_print_Share_Other_Till = Service_print_Share_Other_Till
            objclsMachine.Is_NoLogout = Is_NoLogout
            objclsMachine.Is_PrintServer = Is_PrintServer
            objclsMachine.Is_ServiceBooking = Is_ServiceBooking
            objclsMachine.Is_OnlineZreport = Is_OnlineZreport
            objclsMachine.IsKiosk = IsKiosk
            objclsMachine.TranLimit = TranLimit
            objclsMachine.gtway_TID = gtway_TID
            objclsMachine.QuickTran = QuickTran
            objclsMachine.tillRequest = tillRequest
            objclsMachine.KioskRequest = KioskRequest
            objclsMachine.kitchenPrint = kitchenPrint
            objclsMachine.ReceiptPrint = ReceiptPrint
            objclsMachine.ElinaTran = ElinaTran

            objclsMachine.SunmiSecondScreen = SunmiSecondScreen
            objclsMachine.SecondScreenImage1 = SecondScreenImage1
            objclsMachine.SecondScreenImage2 = SecondScreenImage2
            objclsMachine.POSlite = POSlite
            objclsMachine.sunmi_video_path = sunmi_video_path
            objclsMachine.hardware_type = hardware_type
            If objclsMachine.Delete() Then
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
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            dt = objclsMachine.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            'objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            dt = objclsMachine.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectMachineByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            dt = objclsMachine.[SelectMachineByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectMachineByFunction]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.machine_id = machine_id
            dt = objclsMachine.[SelectMachineByFunction]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectSwapMachineByFunction]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.machine_id = machine_id
            dt = objclsMachine.[SelectSwapMachineByFunction]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectMachineByLocation]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.location_id = location_id
            dt = objclsMachine.[SelectMachineByLocation]()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function [selected_deviceid]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.category_name = category_name
            dt = objclsMachine.[selected_deviceid]()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function [SelectFunctionByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            dt = objclsMachine.[SelectFunctionByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub Bindfunctionmap(ByRef ddl As CheckBoxList, ByVal cmp_id As Integer)
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            Dim dt As DataTable = objclsMachine.[Selectfunction]()

            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "mainfunction_id"
                ddl.DataBind()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Public Sub Bindfunctionmapradio(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            Dim dt As DataTable = objclsMachine.[Selectfunction]()

            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "mainfunction_id"
                ddl.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
    Public Sub Keymap(ByRef ddl As CheckBoxList, ByVal cmp_id As Integer)
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            Dim dt As DataTable = objclsMachine.[Selectkeymap]()

            If dt.Rows.Count > 0 Then
                ddl.DataSource = dt
                ddl.DataTextField = "name"
                ddl.DataValueField = "key_map_id"
                ddl.DataBind()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Public Function [Select_CHK]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            dt = objclsMachine.[Select_CHK]()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function [GetKeymap]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            dt = objclsMachine.[GetKeymap]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [GetFunction]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.machine_id = machine_id
            objclsMachine.cmp_id = cmp_id
            dt = objclsMachine.[Getfunction]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Gettill]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()

            objclsMachine.cmp_id = cmp_id
            objclsMachine.machine_id = machine_id
            dt = objclsMachine.[Gettill]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'add by hardik
    Public Function [SelectShiftByDuration]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.date1 = date1
            objclsMachine.date2 = date2
            objclsMachine.duration = duration
            dt = objclsMachine.[SelectShiftByDuration]()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function [SelectShiftNoByTill]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.date1 = date1
            objclsMachine.date2 = date2
            objclsMachine.duration = duration
            dt = objclsMachine.[SelectShiftNoByTill]()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function delete_keymap_Id() As Integer
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.machine_id = machine_id
            Dim r As Integer
            r = objclsMachine.delete_Existing_Keymap_From_Machine()
            Return r
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function delete_function_Id() As Integer
        Try
            objclsMachine = New clsMachine()
            objclsMachine.cmp_id = cmp_id
            objclsMachine.machine_id = machine_id
            Dim r As Integer
            r = objclsMachine.delete_Existing_functionID()
            Return r
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsMachine
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _machine_id As Integer
    Private _cmp_id As Integer
    Private _code As String
    Private _name As String
    Private _mac_address As String
    Private _model As String
    Private _serial_no As String
    Private _ip_address As String
    Private _login_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _location_id As Integer
    Private _is_assign As Byte
    Private _is_minipos As Byte
    Private _is_active As Byte
    Private _is_master_self As Byte
    Private _Receipt_Header As String
    Private _Receipt_Footer As String
    Private _keymap_id As Integer
    Private _function_id As Integer
    Private _till_id As Integer
    Private _tillip_address As String
    Private _master_till As Integer
    Private _till_server As Byte
    Private _table_sharing As Byte
    Private _printer_sharing As Byte
    Private _sync_time As String
    Private _back_to_main_function_on_till As Byte

    'added by hardik
    Private _extraSurcharge As Byte
    Private _duration As String
    Private _date1 As System.DateTime
    Private _date2 As System.DateTime
    Private _shorting_num As Integer
    Private _onlytabletrans As Byte

    Private _AutoSurcharge As Byte
    Private _AutoSurchargeTables As Byte
    Private _AutoSurchargeNonTables As Byte
    Private _AutoSurchargeCloakroom As Byte
    Private _AutoSurchargeChips As Byte
    Private _NoCashDrawer As Byte
    Private _ReSync_Request As Byte
    Private _Service_Controller_Start As Byte
    Private _Service_Websale_print As Byte
    Private _Service_Print_Share As Byte
    Private _Service_print_Share_Other_Till As Byte
    Private _Is_NoLogout As Byte
    Private _Is_PrintServer As Byte
    Private _Is_ServiceBooking As Byte
    Private _Is_OnlineZreport As Byte
    Private _IsKiosk As Byte
    Private _TranLimit As Integer
    Private _gtway_TID As String
    Private _QuickTran As Byte
    Private _TillRequest As Byte
    Private _kioskRequest As Byte
    Private _kitchenPrint As Byte
    Private _ReceiptPrint As Byte
    Private _ElinaTran As Byte

    Private _posLite As Byte
    Private _sunmiSecondScreen As Byte
    Private _secondScreenImage1 As Byte()
    Private _secondScreenImage2 As Byte()
    Private _sunmi_video_path As String
    Private _hardware_type As Integer
    Private _TillUUID As String
    Private _category_name As String
    Private _generatecode As Integer
    Private _valid As DateTime
    Private objclsMachine As clsMachine

#End Region

#Region "Public Property"
    Public Property hardware_type() As Integer
        Get
            Return _hardware_type
        End Get
        Set(ByVal value As Integer)
            _hardware_type = value
        End Set
    End Property
    Public Property POSlite() As Byte
        Get
            Return _posLite
        End Get
        Set(ByVal value As Byte)
            _posLite = value
        End Set
    End Property

    Public Property SunmiSecondScreen() As Byte
        Get
            Return _sunmiSecondScreen
        End Get
        Set(ByVal value As Byte)
            _sunmiSecondScreen = value
        End Set
    End Property
    Public Property SecondScreenImage1() As Byte()
        Get
            Return _secondScreenImage1
        End Get
        Set(ByVal value As Byte())
            _secondScreenImage1 = value
        End Set
    End Property

    Public Property SecondScreenImage2() As Byte()
        Get
            Return _secondScreenImage2
        End Get
        Set(ByVal value As Byte())
            _secondScreenImage2 = value
        End Set
    End Property


    Public Property sunmi_video_path() As String
        Get
            Return _sunmi_video_path
        End Get
        Set(value As String)
            _sunmi_video_path = value
        End Set

    End Property

    Public Property ElinaTran() As Byte
        Get
            Return _ElinaTran
        End Get
        Set(ByVal value As Byte)
            _ElinaTran = value
        End Set
    End Property

    Public Property gtway_TID() As String
        Get
            Return _gtway_TID
        End Get
        Set(ByVal value As String)
            _gtway_TID = value
        End Set
    End Property

    Public Property TranLimit() As Integer
        Get
            Return _TranLimit
        End Get
        Set(ByVal value As Integer)
            _TranLimit = value
        End Set
    End Property

    Public Property IsKiosk() As Byte
        Get
            Return _IsKiosk
        End Get
        Set(ByVal value As Byte)
            _IsKiosk = value
        End Set

    End Property

    Public Property QuickTran() As Byte
        Get
            Return _QuickTran
        End Get
        Set(ByVal value As Byte)
            _QuickTran = value
        End Set
    End Property
    Public Property tillRequest() As Byte
        Get
            Return _TillRequest
        End Get
        Set(ByVal value As Byte)
            _TillRequest = value
        End Set
    End Property
    Public Property KioskRequest() As Byte
        Get
            Return _kioskRequest
        End Get
        Set(ByVal value As Byte)
            _kioskRequest = value
        End Set
    End Property

    Public Property kitchenPrint() As Byte
        Get
            Return _kitchenPrint
        End Get
        Set(ByVal value As Byte)
            _kitchenPrint = value
        End Set
    End Property

    Public Property ReceiptPrint() As Byte
        Get
            Return _ReceiptPrint
        End Get
        Set(ByVal value As Byte)
            _ReceiptPrint = value
        End Set
    End Property
    Public Property Is_OnlineZreport() As Byte
        Get
            Return _Is_OnlineZreport
        End Get
        Set(ByVal value As Byte)
            _Is_OnlineZreport = value
        End Set
    End Property

    Public Property Is_ServiceBooking() As Byte
        Get
            Return _Is_ServiceBooking
        End Get
        Set(ByVal value As Byte)
            _Is_ServiceBooking = value
        End Set
    End Property


    Public Property Is_PrintServer() As Byte
        Get
            Return _Is_PrintServer
        End Get
        Set(ByVal value As Byte)
            _Is_PrintServer = value
        End Set
    End Property


    Public Property Is_NoLogout() As Byte
        Get
            Return _Is_NoLogout
        End Get
        Set(ByVal value As Byte)
            _Is_NoLogout = value
        End Set
    End Property

    Public Property NoCashDrawer() As Byte
        Get
            Return _NoCashDrawer
        End Get
        Set(ByVal value As Byte)
            _NoCashDrawer = value
        End Set
    End Property

    Public Property Service_Controller_Start() As Byte
        Get
            Return _Service_Controller_Start
        End Get
        Set(ByVal value As Byte)
            _Service_Controller_Start = value
        End Set
    End Property
    Public Property Service_Websale_print() As Byte
        Get
            Return _Service_Websale_print
        End Get
        Set(ByVal value As Byte)
            _Service_Websale_print = value
        End Set
    End Property
    Public Property Service_Print_Share() As Byte
        Get
            Return _Service_Print_Share
        End Get
        Set(ByVal value As Byte)
            _Service_Print_Share = value
        End Set
    End Property
    Public Property Service_print_Share_Other_Till() As Byte
        Get
            Return _Service_print_Share_Other_Till
        End Get
        Set(ByVal value As Byte)
            _Service_print_Share_Other_Till = value
        End Set
    End Property

    Public Property ReSync_Request() As Byte
        Get
            Return _ReSync_Request
        End Get
        Set(ByVal value As Byte)
            _ReSync_Request = value
        End Set
    End Property

    Public Property AutoSurcharge() As Byte
        Get
            Return _AutoSurcharge
        End Get
        Set(ByVal value As Byte)
            _AutoSurcharge = value
        End Set
    End Property
    Public Property AutoSurchargeTables() As Byte
        Get
            Return _AutoSurchargeTables
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeTables = value
        End Set
    End Property
    Public Property AutoSurchargeNonTables() As Byte
        Get
            Return _AutoSurchargeNonTables
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeNonTables = value
        End Set
    End Property
    Public Property AutoSurchargeCloakroom() As Byte
        Get
            Return _AutoSurchargeCloakroom
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeCloakroom = value
        End Set
    End Property
    Public Property AutoSurchargeChips() As Byte
        Get
            Return _AutoSurchargeChips
        End Get
        Set(ByVal value As Byte)
            _AutoSurchargeChips = value
        End Set
    End Property


    Public Property onlytabletrans() As Byte
        Get
            Return _onlytabletrans
        End Get
        Set(ByVal value As Byte)
            _onlytabletrans = value
        End Set
    End Property
    Public Property back_to_main_function_on_till() As Byte
        Get
            Return _back_to_main_function_on_till
        End Get
        Set(ByVal value As Byte)
            _back_to_main_function_on_till = value
        End Set
    End Property
    Public Property extraSurcharge() As Byte
        Get
            Return _extraSurcharge
        End Get
        Set(ByVal value As Byte)
            _extraSurcharge = value
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

    Public Property mac_address() As String
        Get
            Return _mac_address
        End Get
        Set(ByVal value As String)
            _mac_address = value
        End Set
    End Property

    Public Property model() As String
        Get
            Return _model
        End Get
        Set(ByVal value As String)
            _model = value
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
    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property

    Public Property is_assign() As Byte
        Get
            Return _is_assign
        End Get
        Set(ByVal value As Byte)
            _is_assign = value
        End Set
    End Property
    Public Property is_minipos() As Byte
        Get
            Return _is_minipos
        End Get
        Set(ByVal value As Byte)
            _is_minipos = value
        End Set
    End Property
    Public Property is_master_self() As Byte
        Get
            Return _is_master_self
        End Get
        Set(ByVal value As Byte)
            _is_master_self = value
        End Set
    End Property

    Public Property Receipt_Header() As String
        Get
            Return _Receipt_Header
        End Get
        Set(ByVal value As String)
            _Receipt_Header = value
        End Set
    End Property

    Public Property Receipt_Footer() As String
        Get
            Return _Receipt_Footer
        End Get
        Set(ByVal value As String)
            _Receipt_Footer = value
        End Set
    End Property

    Public Property table_sharing() As Byte
        Get
            Return _table_sharing
        End Get
        Set(ByVal value As Byte)
            _table_sharing = value
        End Set
    End Property
    Public Property printer_sharing() As Byte
        Get
            Return _printer_sharing
        End Get
        Set(ByVal value As Byte)
            _printer_sharing = value
        End Set
    End Property
    Public Property till_id() As Integer
        Get
            Return _till_id
        End Get
        Set(ByVal value As Integer)
            _till_id = value
        End Set
    End Property
    Public Property tillip_address() As String
        Get
            Return _tillip_address
        End Get
        Set(ByVal value As String)
            _tillip_address = value
        End Set
    End Property
    Public Property till_server() As Byte
        Get
            Return _till_server
        End Get
        Set(ByVal value As Byte)
            _till_server = value
        End Set
    End Property
    Public Property master_till() As Integer
        Get
            Return _master_till
        End Get
        Set(ByVal value As Integer)
            _master_till = value
        End Set
    End Property

    Public Property sync_time() As String
        Get
            Return _sync_time
        End Get
        Set(ByVal value As String)
            _sync_time = value
        End Set
    End Property

    Public Property keymap_id() As Integer
        Get
            Return _keymap_id
        End Get
        Set(ByVal value As Integer)
            _keymap_id = value
        End Set
    End Property

    Public Property shorting_num() As Integer
        Get
            Return _shorting_num
        End Get
        Set(ByVal value As Integer)
            _shorting_num = value
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


    'add by hardik
    Public Property duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
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
    Public Property TillUUID() As String
        Get
            Return _TillUUID
        End Get
        Set(ByVal value As String)
            _TillUUID = value
        End Set
    End Property
    Public Property category_name() As String
        Get
            Return _category_name
        End Get
        Set(ByVal value As String)
            _category_name = value
        End Set
    End Property
    Public Property generatecode() As Integer
        Get
            Return _generatecode
        End Get
        Set(ByVal value As Integer)
            _generatecode = value
        End Set
    End Property
    Public Property valid() As DateTime
        Get
            Return _valid
        End Get
        Set(ByVal value As DateTime)
            _TillUUID = value
        End Set
    End Property
#End Region

    Public Function Insert() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@mac_address", mac_address)
            oColSqlparram.Add("@serial_no", serial_no)
            oColSqlparram.Add("@model", model)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@is_assign", is_assign, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_minipos", is_minipos, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Receipt_Header", Receipt_Header)
            oColSqlparram.Add("@Receipt_Footer", Receipt_Footer)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@is_master_self", is_master_self, SqlDbType.TinyInt)
            oColSqlparram.Add("@till_server", till_server, SqlDbType.TinyInt)
            oColSqlparram.Add("@master_till", master_till, SqlDbType.Int)
            oColSqlparram.Add("@tillip_address", tillip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@table_sharing", table_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@printer_sharing", printer_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@sync_time", sync_time)
            oColSqlparram.Add("@back_to_main_function_on_till", back_to_main_function_on_till, SqlDbType.TinyInt)
            oColSqlparram.Add("@extraSurcharge", extraSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@onlytabletrans", onlytabletrans, SqlDbType.TinyInt)

            oColSqlparram.Add("@AutoSurcharge", AutoSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeTables", AutoSurchargeTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeNonTables", AutoSurchargeNonTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeCloakroom", AutoSurchargeCloakroom, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeChips", AutoSurchargeChips, SqlDbType.TinyInt)
            oColSqlparram.Add("@NoCashDrawer", NoCashDrawer, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReSync_Request", ReSync_Request, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Controller_Start", Service_Controller_Start, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Websale_print", Service_Websale_print, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Print_Share", Service_Print_Share, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_print_Share_Other_Till", Service_print_Share_Other_Till, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_NoLogout", Is_NoLogout, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_PrintServer", Is_PrintServer, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_ServiceBooking", Is_ServiceBooking, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_OnlineZreport", Is_OnlineZreport, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsKiosk", IsKiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@TranLimit", TranLimit, SqlDbType.Int)
            oColSqlparram.Add("@gtway_TID", gtway_TID)
            oColSqlparram.Add("@QuickTran", QuickTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@tillRequest", tillRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@KioskRequest", KioskRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@kitchenPrint", kitchenPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReceiptPrint", ReceiptPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ElinaTran", ElinaTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@sunmiSecondScreen", SunmiSecondScreen)
            oColSqlparram.Add("@SecondScreenImage1", SecondScreenImage1, SqlDbType.Image)
            oColSqlparram.Add("@SecondScreenImage2", SecondScreenImage2, SqlDbType.Image)
            oColSqlparram.Add("@poslite", POSlite)
            oColSqlparram.Add("@sunmi_video_path", sunmi_video_path)
            oColSqlparram.Add("@hardware_type", hardware_type)

            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Return dtlogin.Rows(0)("machine_id")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@mac_address", mac_address)
            oColSqlparram.Add("@serial_no", serial_no)
            oColSqlparram.Add("@model", model)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@is_assign", is_assign, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_minipos", is_minipos, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)

            oColSqlparram.Add("@Receipt_Header", Receipt_Header)
            oColSqlparram.Add("@Receipt_Footer", Receipt_Footer)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@is_master_self", is_master_self, SqlDbType.TinyInt)
            oColSqlparram.Add("@till_server", till_server, SqlDbType.TinyInt)
            oColSqlparram.Add("@master_till", master_till, SqlDbType.Int)
            oColSqlparram.Add("@tillip_address", tillip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@table_sharing", table_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@printer_sharing", printer_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@sync_time", sync_time)
            oColSqlparram.Add("@back_to_main_function_on_till", back_to_main_function_on_till, SqlDbType.TinyInt)
            oColSqlparram.Add("@extraSurcharge", extraSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@onlytabletrans", onlytabletrans, SqlDbType.TinyInt)

            oColSqlparram.Add("@AutoSurcharge", AutoSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeTables", AutoSurchargeTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeNonTables", AutoSurchargeNonTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeCloakroom", AutoSurchargeCloakroom, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeChips", AutoSurchargeChips, SqlDbType.TinyInt)
            oColSqlparram.Add("@NoCashDrawer", NoCashDrawer, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReSync_Request", ReSync_Request, SqlDbType.TinyInt)

            oColSqlparram.Add("@Service_Controller_Start", Service_Controller_Start, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Websale_print", Service_Websale_print, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Print_Share", Service_Print_Share, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_print_Share_Other_Till", Service_print_Share_Other_Till, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_NoLogout", Is_NoLogout, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_PrintServer", Is_PrintServer, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_ServiceBooking", Is_ServiceBooking, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_OnlineZreport", Is_OnlineZreport, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsKiosk", IsKiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@TranLimit", TranLimit, SqlDbType.Int)
            oColSqlparram.Add("@gtway_TID", gtway_TID)
            oColSqlparram.Add("@QuickTran", QuickTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@tillRequest", tillRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@KioskRequest", KioskRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@kitchenPrint", kitchenPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReceiptPrint", ReceiptPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ElinaTran", ElinaTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@sunmiSecondScreen", SunmiSecondScreen)
            oColSqlparram.Add("@SecondScreenImage1", SecondScreenImage1, SqlDbType.Image)
            oColSqlparram.Add("@SecondScreenImage2", SecondScreenImage2, SqlDbType.Image)
            oColSqlparram.Add("@poslite", POSlite)
            oColSqlparram.Add("@sunmi_video_path", sunmi_video_path)
            oColSqlparram.Add("@hardware_type", hardware_type)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function registerwithQR()
    '    Try

    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)

    '        oColSqlparram.Add("@hardware_type", hardware_type)
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
    '        Return False



    '    Catch ex As Exception

    '    End Try
    'End Function


    Public Function registerwithQR() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@mac_address", mac_address)
            oColSqlparram.Add("@serial_no", serial_no)
            oColSqlparram.Add("@model", model)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@is_assign", is_assign, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_minipos", is_minipos, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)

            oColSqlparram.Add("@Receipt_Header", Receipt_Header)
            oColSqlparram.Add("@Receipt_Footer", Receipt_Footer)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@is_master_self", is_master_self, SqlDbType.TinyInt)
            oColSqlparram.Add("@till_server", till_server, SqlDbType.TinyInt)
            oColSqlparram.Add("@master_till", master_till, SqlDbType.Int)
            oColSqlparram.Add("@tillip_address", tillip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@table_sharing", table_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@printer_sharing", printer_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@sync_time", sync_time)
            oColSqlparram.Add("@back_to_main_function_on_till", back_to_main_function_on_till, SqlDbType.TinyInt)
            oColSqlparram.Add("@extraSurcharge", extraSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@onlytabletrans", onlytabletrans, SqlDbType.TinyInt)

            oColSqlparram.Add("@AutoSurcharge", AutoSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeTables", AutoSurchargeTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeNonTables", AutoSurchargeNonTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeCloakroom", AutoSurchargeCloakroom, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeChips", AutoSurchargeChips, SqlDbType.TinyInt)
            oColSqlparram.Add("@NoCashDrawer", NoCashDrawer, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReSync_Request", ReSync_Request, SqlDbType.TinyInt)

            oColSqlparram.Add("@Service_Controller_Start", Service_Controller_Start, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Websale_print", Service_Websale_print, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Print_Share", Service_Print_Share, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_print_Share_Other_Till", Service_print_Share_Other_Till, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_NoLogout", Is_NoLogout, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_PrintServer", Is_PrintServer, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_ServiceBooking", Is_ServiceBooking, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_OnlineZreport", Is_OnlineZreport, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsKiosk", IsKiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@TranLimit", TranLimit, SqlDbType.Int)
            oColSqlparram.Add("@gtway_TID", gtway_TID)
            oColSqlparram.Add("@QuickTran", QuickTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@tillRequest", tillRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@KioskRequest", KioskRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@kitchenPrint", kitchenPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReceiptPrint", ReceiptPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ElinaTran", ElinaTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@sunmiSecondScreen", SunmiSecondScreen)
            oColSqlparram.Add("@SecondScreenImage1", SecondScreenImage1, SqlDbType.Image)
            oColSqlparram.Add("@SecondScreenImage2", SecondScreenImage2, SqlDbType.Image)
            oColSqlparram.Add("@poslite", POSlite)
            oColSqlparram.Add("@sunmi_video_path", sunmi_video_path)
            oColSqlparram.Add("@hardware_type", hardware_type)
            oColSqlparram.Add("@TillUUID", TillUUID)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_registerforQR", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function generateQR() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@TillUUID", TillUUID)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_generateforQR", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@mac_address", mac_address)
            oColSqlparram.Add("@serial_no", serial_no)
            oColSqlparram.Add("@model", model)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@is_assign", is_assign, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_minipos", is_minipos, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Receipt_Header", Receipt_Header)
            oColSqlparram.Add("@Receipt_Footer", Receipt_Footer)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@is_master_self", is_master_self, SqlDbType.TinyInt)
            oColSqlparram.Add("@till_server", till_server, SqlDbType.TinyInt)
            oColSqlparram.Add("@master_till", master_till, SqlDbType.Int)
            oColSqlparram.Add("@tillip_address", tillip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@table_sharing", table_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@printer_sharing", printer_sharing, SqlDbType.TinyInt)
            oColSqlparram.Add("@sync_time", sync_time)
            oColSqlparram.Add("@back_to_main_function_on_till", back_to_main_function_on_till, SqlDbType.TinyInt)
            oColSqlparram.Add("@extraSurcharge", extraSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@onlytabletrans", onlytabletrans, SqlDbType.TinyInt)

            oColSqlparram.Add("@AutoSurcharge", AutoSurcharge, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeTables", AutoSurchargeTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeNonTables", AutoSurchargeNonTables, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeCloakroom", AutoSurchargeCloakroom, SqlDbType.TinyInt)
            oColSqlparram.Add("@AutoSurchargeChips", AutoSurchargeChips, SqlDbType.TinyInt)
            oColSqlparram.Add("@NoCashDrawer", NoCashDrawer, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReSync_Request", ReSync_Request, SqlDbType.TinyInt)

            oColSqlparram.Add("@Service_Controller_Start", Service_Controller_Start, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Websale_print", Service_Websale_print, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_Print_Share", Service_Print_Share, SqlDbType.TinyInt)
            oColSqlparram.Add("@Service_print_Share_Other_Till", Service_print_Share_Other_Till, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_NoLogout", Is_NoLogout, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_PrintServer", Is_PrintServer, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_ServiceBooking", Is_ServiceBooking, SqlDbType.TinyInt)
            oColSqlparram.Add("@Is_OnlineZreport", Is_OnlineZreport, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsKiosk", IsKiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@TranLimit", TranLimit, SqlDbType.Int)
            oColSqlparram.Add("@gtway_TID", gtway_TID)
            oColSqlparram.Add("@QuickTran", QuickTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@tillRequest", tillRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@KioskRequest", KioskRequest, SqlDbType.TinyInt)
            oColSqlparram.Add("@kitchenPrint", kitchenPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ReceiptPrint", ReceiptPrint, SqlDbType.TinyInt)
            oColSqlparram.Add("@ElinaTran", ElinaTran, SqlDbType.TinyInt)
            oColSqlparram.Add("@sunmiSecondScreen", SunmiSecondScreen)
            oColSqlparram.Add("@SecondScreenImage1", SecondScreenImage1, SqlDbType.Image)
            oColSqlparram.Add("@SecondScreenImage2", SecondScreenImage2, SqlDbType.Image)
            oColSqlparram.Add("@poslite", POSlite)
            oColSqlparram.Add("@sunmi_video_path", sunmi_video_path)
            oColSqlparram.Add("@hardware_type", hardware_type)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Machine", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Machine", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectMachineByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Machine_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectMachineByFunction]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Machine_By_Function", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectSwapMachineByFunction]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_SwapMachine_By_Function", oColSqlparram)

        Return dtlogin
    End Function



    Public Function [SelectMachineByLocation]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Machine_By_Location", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [selected_deviceid]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@devicename", category_name, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Machine_By_device", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectFunctionByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Function_By_Cmp_ForTill", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [Select_CHK]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_KeyMapTill", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Insertkeymapchksave() As Integer
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@keymap_id", keymap_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@till_id", till_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@shorting_num", shorting_num, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_keymap_check", oColSqlparram)
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function InsertMainFunctionchksave() As Integer
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@till_id", till_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainFunction_check", oColSqlparram)
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function updatekeymapchksave() As Integer
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@keymap_id", keymap_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@till_id", till_id, SqlDbType.Int)

            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_keymap_check", oColSqlparram)
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function updateMainFunctionchksave() As Integer
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_id", function_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@till_id", till_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainFunction_check", oColSqlparram)
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Getfunction]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Get_M_MainFunction_check", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [GetKeymap]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Get_M_keymap_check", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Selectkeymap]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_KeyMapTill", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Selectfunction]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_FunctionMap", oColSqlparram)

        Return dtlogin
    End Function

    'add by hardik
    Public Function [SelectShiftByDuration]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@date1", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@date2", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@duration", _duration, SqlDbType.NVarChar)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Shift_Name", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectShiftNoByTill]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_ShiftNoByTill", oColSqlparram)

        Return dtlogin
    End Function

    Public Function delete_Existing_Keymap_From_Machine() As Integer
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_delete_Keymap_Id", oColSqlparram)
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function delete_Existing_functionID() As Integer
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine", oColSqlparram)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_delete_function_Id", oColSqlparram)
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Gettill]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_till", oColSqlparram)

        Return dtlogin
    End Function

End Class



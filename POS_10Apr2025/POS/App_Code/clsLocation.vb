Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsLocation
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _location_id As Integer
    Private _cloud_id As Integer
    Private _cmp_id As Integer
    Private _code As String
    Private _name As String
    Private _address As String
    Private _country_id As Integer
    Private _state_id As Integer
    Private _city_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _ip_address As String
    Private _login_id As Integer
    Private _venue_id As Integer
    Private _Tran_Type As String
    Private _till_auto_log_off As Byte
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _cashback As Byte
    Private _srv_charge_clickcollect As Byte
    Private _srv_charge_kiosk As Byte
    Private _srv_charge_order As Byte
    Private _srv_charge_delivery As Byte
    Private _is_delivery As Byte
    Private _min_amount As Decimal
    Private _judo_id As String
    Private _judo_token As String
    Private _judo_secret As String
    Private _is_skipPay As Byte
    Private _is_email As Byte
    Private _cashflow_id As String
    Private _cashflow_url As String
    Private _cashflow_api_key As String
    Private _onlinepayment As String
    Private _websales_check_schedule As Byte
    Private _schedule_required As Byte
    Private _start_date As String
    Private _end_date As String
    Private _Email As String
    Private _Contact As String
    Private _header_reciept As String
    Private _L_Image As Byte()
    Private _BG_Color As String
    Private _Font_Color As String
    Private _Body_Color As String
    Private _Click_Collect_image As Byte()
    Private _Delivery_image As Byte()
    Private _OrderAtTable_image As Byte()
    Private _click_and_collect As Byte
    Private _Order_table As Byte
    Private _theme As String
    Private _tipAs As String
    Private _delivery_charges As Double
    Private _service_charges As Double
    Private _message_delivery As String
    Private _message_order_table As String
    Private _schedule_cnc As Byte
    Private _oldname As String
    Private _future_booking_days As Integer
    Private _GraphicViewBackground As Byte()
    Private _GraphicViewTable As Byte()
    Private _HourlySlot As Byte
    Private _CustomPay_id As String
    Private _CustomPay_token As String
    Private _CustomPay_secret As String
    Private _CustomPay_Base64 As String
    Private _CustomPay_URL As String
    Private _Is_Email_APK As Byte
    Private _Table_As_Box As String
    Private _Client_ID As String
    Private _clientsecret As String
    Private _redirect_uri As String
    Private _gtway_MerchantID As String
    Private _gtway_StoreID As Integer
    Private _gtway_LocationID As Integer
    Private _gtway_StoreName As String
    Private _gtway_LocationName As String

    Private _posLite As Byte
    Private _sunmiSecondScreen As Byte
    Private _secondScreenImage1 As Byte()
    Private _secondScreenImage2 As Byte()
    Private _secondScreenVideo As Byte()
    Private _sunmi_video_path As String
    Private _GC_Btn_stl As String
    Private _GC_Btn_img_typ As String
    Private _GC_Btn_fnt_size As String
    Private _GC_Btn_bkgd_clr As String
    Private _GC_Btn_txt_clr As String

    Private _C_Btn_stl As String
    Private _C_Btn_img_typ As String
    Private _C_Btn_fnt_size As String
    Private _C_Btn_bkgd_clr As String
    Private _C_Btn_txt_clr As String

    Private _P_Btn_stl As String
    Private _P_Btn_img_typ As String
    Private _P_Btn_fnt_size As String
    Private _P_Btn_bkgd_clr As String
    Private _P_Btn_txt_clr As String

    Private _GC_Btn_img_typ_pos As String
    Private _C_Btn_img_typ_pos As String
    Private _P_Btn_img_typ_pos As String
    Private _Is_GetCovers As Byte

    Private _login_src_bkgd_clr As String
    Private _login_src_login_btn As String
    Private _Till_url As String
    Private _Till_Phn_no As String
    Private _cloudValue As String
    Private _cloudKey As String
    Private _cloudurl As String
    Private _salesID As Integer
    Private _cassette As Integer
    Private _reading As Integer

    Private _POS_logo As Byte()
    Private objclsLocation As clsLocation

#End Region

#Region "Public Property"

    Public Property GC_Btn_img_typ_pos() As String
        Get
            Return _GC_Btn_img_typ_pos
        End Get
        Set(value As String)
            _GC_Btn_img_typ_pos = value
        End Set

    End Property
    Public Property login_src_login_btn() As String
        Get
            Return _login_src_login_btn
        End Get
        Set(value As String)
            _login_src_login_btn = value
        End Set

    End Property
    Public Property Till_url() As String
        Get
            Return _Till_url
        End Get
        Set(value As String)
            _Till_url = value
        End Set

    End Property
    Public Property Till_Phn_no() As String
        Get
            Return _Till_Phn_no
        End Get
        Set(value As String)
            _Till_Phn_no = value
        End Set

    End Property
    Public Property login_src_bkgd_clr() As String
        Get
            Return _login_src_bkgd_clr
        End Get
        Set(value As String)
            _login_src_bkgd_clr = value
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


    Public Property C_Btn_img_typ_pos() As String
        Get
            Return _C_Btn_img_typ_pos
        End Get
        Set(value As String)
            _C_Btn_img_typ_pos = value
        End Set

    End Property
    Public Property P_Btn_img_typ_pos() As String
        Get
            Return _P_Btn_img_typ_pos
        End Get
        Set(value As String)
            _P_Btn_img_typ_pos = value
        End Set

    End Property


    Public Property gtway_StoreName() As String
        Get
            Return _gtway_StoreName
        End Get
        Set(value As String)
            _gtway_StoreName = value
        End Set

    End Property

    Public Property gtway_LocationName() As String
        Get
            Return _gtway_LocationName
        End Get
        Set(value As String)
            _gtway_LocationName = value
        End Set

    End Property

    Public Property gtway_StoreID() As Integer
        Get
            Return _gtway_StoreID
        End Get
        Set(ByVal value As Integer)
            _gtway_StoreID = value
        End Set
    End Property

    Public Property gtway_LocationID() As Integer
        Get
            Return _gtway_LocationID
        End Get
        Set(ByVal value As Integer)
            _gtway_LocationID = value
        End Set
    End Property


    Public Property gtway_MerchantID() As String
        Get
            Return _gtway_MerchantID
        End Get
        Set(value As String)
            _gtway_MerchantID = value
        End Set

    End Property
    Public Property Client_ID() As String
        Get
            Return _Client_ID
        End Get
        Set(value As String)
            _Client_ID = value
        End Set
    End Property

    Public Property clientsecret() As String
        Get
            Return _clientsecret
        End Get
        Set(value As String)
            _clientsecret = value
        End Set
    End Property

    Public Property redirect_uri() As String
        Get
            Return _redirect_uri
        End Get
        Set(value As String)
            _redirect_uri = value
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
    Public Property Table_As_Box() As String
        Get
            Return _Table_As_Box
        End Get
        Set(value As String)
            _Table_As_Box = value
        End Set
    End Property


    Public Property Is_Email_APK() As Byte
        Get
            Return _Is_Email_APK
        End Get
        Set(ByVal value As Byte)
            _Is_Email_APK = value
        End Set
    End Property
    Public Property Is_GetCovers() As Byte
        Get
            Return _Is_GetCovers
        End Get
        Set(ByVal value As Byte)
            _Is_GetCovers = value
        End Set
    End Property



    Public Property CustomPay_id() As String
        Get
            Return _CustomPay_id
        End Get
        Set(value As String)
            _CustomPay_id = value
        End Set
    End Property

    Public Property CustomPay_token() As String
        Get
            Return _CustomPay_token
        End Get
        Set(value As String)
            _CustomPay_token = value
        End Set
    End Property

    Public Property CustomPay_Base64() As String
        Get
            Return _CustomPay_Base64
        End Get
        Set(value As String)
            _CustomPay_Base64 = value
        End Set
    End Property

    Public Property CustomPay_secret() As String
        Get
            Return _CustomPay_secret
        End Get
        Set(value As String)
            _CustomPay_secret = value
        End Set
    End Property

    Public Property CustomPay_URL() As String
        Get
            Return _CustomPay_URL
        End Get
        Set(value As String)
            _CustomPay_URL = value
        End Set
    End Property
    Public Property message_delivery() As String
        Get
            Return _message_delivery
        End Get
        Set(ByVal value As String)
            _message_delivery = value
        End Set
    End Property

    Public Property message_order_table() As String
        Get
            Return _message_order_table
        End Get
        Set(ByVal value As String)
            _message_order_table = value
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

    Public Property cashback() As Byte
        Get
            Return _cashback
        End Get
        Set(ByVal value As Byte)
            _cashback = value
        End Set
    End Property
    Public Property srv_charge_clickcollect() As Byte
        Get
            Return _srv_charge_clickcollect
        End Get
        Set(ByVal value As Byte)
            _srv_charge_clickcollect = value
        End Set
    End Property
    Public Property srv_charge_kiosk() As Byte
        Get
            Return _srv_charge_kiosk
        End Get
        Set(ByVal value As Byte)
            _srv_charge_kiosk = value
        End Set
    End Property
    Public Property srv_charge_order() As Byte
        Get
            Return _srv_charge_order
        End Get
        Set(ByVal value As Byte)
            _srv_charge_order = value
        End Set
    End Property
    Public Property srv_charge_delivery() As Byte
        Get
            Return _srv_charge_delivery
        End Get
        Set(ByVal value As Byte)
            _srv_charge_delivery = value
        End Set
    End Property
    Public Property is_email() As Byte
        Get
            Return _is_email
        End Get
        Set(ByVal value As Byte)
            _is_email = value
        End Set
    End Property
    Public Property is_delivery() As Byte
        Get
            Return _is_delivery
        End Get
        Set(ByVal value As Byte)
            _is_delivery = value
        End Set
    End Property
    Public Property is_skipPay() As Byte
        Get
            Return _is_skipPay
        End Get
        Set(ByVal value As Byte)
            _is_skipPay = value
        End Set
    End Property
    Public Property min_amount() As Decimal
        Get
            Return _min_amount
        End Get
        Set(ByVal value As Decimal)
            _min_amount = value
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
    Public Property cloud_id() As Integer
        Get
            Return _cloud_id
        End Get
        Set(ByVal value As Integer)
            _cloud_id = value
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

    Public Property address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property
    Public Property country_id() As Integer
        Get
            Return _country_id
        End Get
        Set(ByVal value As Integer)
            _country_id = value
        End Set
    End Property
    Public Property state_id() As Integer
        Get
            Return _state_id
        End Get
        Set(ByVal value As Integer)
            _state_id = value
        End Set
    End Property
    Public Property city_id() As Integer
        Get
            Return _city_id
        End Get
        Set(ByVal value As Integer)
            _city_id = value
        End Set
    End Property
    Public Property salesID() As Integer
        Get
            Return _salesID
        End Get
        Set(ByVal value As Integer)
            _salesID = value
        End Set
    End Property
    Public Property cassette() As Integer
        Get
            Return _cassette
        End Get
        Set(ByVal value As Integer)
            _cassette = value
        End Set
    End Property
    Public Property reading() As Integer
        Get
            Return _reading
        End Get
        Set(ByVal value As Integer)
            _reading = value
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
    Public Property venue_id() As Integer
        Get
            Return _venue_id
        End Get
        Set(ByVal value As Integer)
            _venue_id = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property

    Public Property till_auto_log_off() As Byte
        Get
            Return _till_auto_log_off
        End Get
        Set(ByVal value As Byte)
            _till_auto_log_off = value
        End Set
    End Property
    Public Property judo_id() As String
        Get
            Return _judo_id
        End Get
        Set(value As String)
            _judo_id = value
        End Set
    End Property

    Public Property judo_token() As String
        Get
            Return _judo_token
        End Get
        Set(value As String)
            _judo_token = value
        End Set
    End Property

    Public Property judo_secret() As String
        Get
            Return _judo_secret
        End Get
        Set(value As String)
            _judo_secret = value
        End Set
    End Property

    Public Property cashflow_url() As String
        Get
            Return _cashflow_url
        End Get
        Set(value As String)
            _cashflow_url = value
        End Set
    End Property
    Public Property cashflow_id() As String
        Get
            Return _cashflow_id
        End Get
        Set(value As String)
            _cashflow_id = value
        End Set
    End Property
    Public Property cashflow_api_key() As String
        Get
            Return _cashflow_api_key
        End Get
        Set(value As String)
            _cashflow_api_key = value
        End Set
    End Property
    Public Property onlinepayment() As String
        Get
            Return _onlinepayment
        End Get
        Set(ByVal value As String)
            _onlinepayment = value
        End Set
    End Property
    Public Property start_date() As String
        Get
            Return _start_date
        End Get
        Set(ByVal value As String)
            _start_date = value
        End Set
    End Property
    Public Property end_date() As String
        Get
            Return _end_date
        End Get
        Set(ByVal value As String)
            _end_date = value
        End Set
    End Property
    Public Property schedule_cnc() As Byte
        Get
            Return _schedule_cnc
        End Get
        Set(ByVal value As Byte)
            _schedule_cnc = value
        End Set
    End Property

    Public Property HourlySlot() As Byte
        Get
            Return _HourlySlot
        End Get
        Set(ByVal value As Byte)
            _HourlySlot = value
        End Set
    End Property
    Public Property websales_check_schedule() As Byte
        Get
            Return _websales_check_schedule
        End Get
        Set(ByVal value As Byte)
            _websales_check_schedule = value
        End Set
    End Property
    Public Property schedule_required() As Byte
        Get
            Return _schedule_required
        End Get
        Set(ByVal value As Byte)
            _schedule_required = value
        End Set
    End Property
    Public Property Contact() As String
        Get
            Return _Contact
        End Get
        Set(value As String)
            _Contact = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property
    Public Property header_reciept() As String
        Get
            Return _header_reciept
        End Get
        Set(ByVal value As String)
            _header_reciept = value
        End Set
    End Property
    Public Property L_Image() As Byte()
        Get
            Return _L_Image
        End Get
        Set(ByVal value As Byte())
            _L_Image = value
        End Set
    End Property
    Public Property BG_Color() As String
        Get
            Return _BG_Color
        End Get
        Set(ByVal value As String)
            _BG_Color = value
        End Set
    End Property
    Public Property Font_Color() As String
        Get
            Return _Font_Color
        End Get
        Set(ByVal value As String)
            _Font_Color = value
        End Set
    End Property
    Public Property Body_Color() As String
        Get
            Return _Body_Color
        End Get
        Set(ByVal value As String)
            _Body_Color = value
        End Set
    End Property
    Public Property Click_Collect_image() As Byte()
        Get
            Return _Click_Collect_image
        End Get
        Set(ByVal value As Byte())
            _Click_Collect_image = value
        End Set
    End Property
    Public Property POS_logo() As Byte()
        Get
            Return _POS_logo
        End Get
        Set(ByVal value As Byte())
            _POS_logo = value
        End Set
    End Property
    Public Property Delivery_image() As Byte()
        Get
            Return _Delivery_image
        End Get
        Set(ByVal value As Byte())
            _Delivery_image = value
        End Set
    End Property
    Public Property OrderAtTable_image() As Byte()
        Get
            Return _OrderAtTable_image
        End Get
        Set(ByVal value As Byte())
            _OrderAtTable_image = value
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
    Public Property Order_table() As Byte
        Get
            Return _Order_table
        End Get
        Set(ByVal value As Byte)
            _Order_table = value
        End Set
    End Property
    Public Property delivery_charges() As Double
        Get
            Return _delivery_charges
        End Get
        Set(ByVal value As Double)
            _delivery_charges = value
        End Set
    End Property
    Public Property service_charges() As Double
        Get
            Return _service_charges
        End Get
        Set(ByVal value As Double)
            _service_charges = value
        End Set
    End Property
    Public Property theme() As String
        Get
            Return _theme
        End Get
        Set(ByVal value As String)
            _theme = value
        End Set
    End Property
    Public Property tipAs() As String
        Get
            Return _tipAs
        End Get
        Set(ByVal value As String)
            _tipAs = value
        End Set
    End Property

    Public Property oldname() As String
        Get
            Return _oldname
        End Get
        Set(ByVal value As String)
            _oldname = value
        End Set
    End Property

    Public Property future_booking_days() As Integer
        Get
            Return _future_booking_days
        End Get
        Set(ByVal value As Integer)
            _future_booking_days = value
        End Set
    End Property


    Public Property GraphicViewBackground() As Byte()
        Get
            Return _GraphicViewBackground
        End Get
        Set(ByVal value As Byte())
            _GraphicViewBackground = value
        End Set
    End Property

    Public Property GraphicViewTable() As Byte()
        Get
            Return _GraphicViewTable
        End Get
        Set(ByVal value As Byte())
            _GraphicViewTable = value
        End Set
    End Property

    Public Property GC_Btn_stl() As String
        Get
            Return _GC_Btn_stl
        End Get
        Set(value As String)
            _GC_Btn_stl = value
        End Set

    End Property
    Public Property GC_Btn_img_typ() As String
        Get
            Return _GC_Btn_img_typ
        End Get
        Set(value As String)
            _GC_Btn_img_typ = value
        End Set

    End Property
    Public Property GC_Btn_fnt_size() As String
        Get
            Return _GC_Btn_fnt_size
        End Get
        Set(value As String)
            _GC_Btn_fnt_size = value
        End Set

    End Property
    Public Property GC_Btn_bkgd_clr() As String
        Get
            Return _GC_Btn_bkgd_clr
        End Get
        Set(value As String)
            _GC_Btn_bkgd_clr = value
        End Set

    End Property
    Public Property GC_Btn_txt_clr() As String
        Get
            Return _GC_Btn_txt_clr
        End Get
        Set(value As String)
            _GC_Btn_txt_clr = value
        End Set

    End Property

    '----------

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
    Public Property SecondScreenVideo() As Byte()
        Get
            Return _secondScreenVideo
        End Get
        Set(ByVal value As Byte())
            _secondScreenVideo = value
        End Set
    End Property

    Public Property C_Btn_stl() As String
        Get
            Return _C_Btn_stl
        End Get
        Set(value As String)
            _C_Btn_stl = value
        End Set

    End Property
    Public Property C_Btn_img_typ() As String
        Get
            Return _C_Btn_img_typ
        End Get
        Set(value As String)
            _C_Btn_img_typ = value
        End Set

    End Property
    Public Property C_Btn_fnt_size() As String
        Get
            Return _C_Btn_fnt_size
        End Get
        Set(value As String)
            _C_Btn_fnt_size = value
        End Set

    End Property
    Public Property C_Btn_bkgd_clr() As String
        Get
            Return _C_Btn_bkgd_clr
        End Get
        Set(value As String)
            _C_Btn_bkgd_clr = value
        End Set

    End Property
    Public Property C_Btn_txt_clr() As String
        Get
            Return _C_Btn_txt_clr
        End Get
        Set(value As String)
            _C_Btn_txt_clr = value
        End Set

    End Property

    '-------

    Public Property P_Btn_stl() As String
        Get
            Return _P_Btn_stl
        End Get
        Set(value As String)
            _P_Btn_stl = value
        End Set

    End Property
    Public Property P_Btn_img_typ() As String
        Get
            Return _P_Btn_img_typ
        End Get
        Set(value As String)
            _P_Btn_img_typ = value
        End Set

    End Property
    Public Property P_Btn_fnt_size() As String
        Get
            Return _P_Btn_fnt_size
        End Get
        Set(value As String)
            _P_Btn_fnt_size = value
        End Set

    End Property
    Public Property P_Btn_bkgd_clr() As String
        Get
            Return _P_Btn_bkgd_clr
        End Get
        Set(value As String)
            _P_Btn_bkgd_clr = value
        End Set

    End Property
    Public Property P_Btn_txt_clr() As String
        Get
            Return _P_Btn_txt_clr
        End Get
        Set(value As String)
            _P_Btn_txt_clr = value
        End Set

    End Property
    Public Property cloudValue() As String
        Get
            Return _cloudValue
        End Get
        Set(value As String)
            _cloudValue = value
        End Set

    End Property
    Public Property cloudurl() As String
        Get
            Return _cloudurl
        End Get
        Set(value As String)
            _cloudurl = value
        End Set

    End Property
    Public Property cloudKey() As String
        Get
            Return _cloudKey
        End Get
        Set(value As String)
            _cloudKey = value
        End Set

    End Property

#End Region

    Public Function Select_Merchant() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            dt = objclsLocation.Select_Merchant()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_urlforCloud() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            ' objclsLocation.created_date = created_date
            dt = objclsLocation.Select_urlforCloud()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_tsalesCloud() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.created_date = created_date
            ' objclsLocation.modify_date = modify_date
            dt = objclsLocation.Select_tsalesCloud()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_updateCloud() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.created_date = created_date
            objclsLocation.machine_id = machine_id
            objclsLocation.salesID = salesID
            dt = objclsLocation.Select_updateCloud()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_url() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            dt = objclsLocation.Select_url()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ImgDelete() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.Tran_Type = Tran_Type
            If objclsLocation.ImageDelete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ImgDeleteGraphic() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.Tran_Type = Tran_Type
            If objclsLocation.ImageDeleteGraphic() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ImgDeleteTable() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.Tran_Type = Tran_Type
            If objclsLocation.ImageDeleteTable() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.code = code
            objclsLocation.name = name
            objclsLocation.address = address
            objclsLocation.country_id = country_id
            objclsLocation.state_id = state_id
            objclsLocation.city_id = city_id
            objclsLocation.ip_address = ip_address
            objclsLocation.login_id = login_id
            objclsLocation.venue_id = venue_id
            objclsLocation.till_auto_log_off = till_auto_log_off
            objclsLocation.machine_id = machine_id
            objclsLocation.is_active = is_active
            objclsLocation.cashback = cashback
            objclsLocation.srv_charge_clickcollect = srv_charge_clickcollect
            objclsLocation.srv_charge_kiosk = srv_charge_kiosk
            objclsLocation.srv_charge_order = srv_charge_order
            objclsLocation.srv_charge_delivery = srv_charge_delivery
            objclsLocation.min_amount = min_amount
            objclsLocation.is_delivery = is_delivery
            objclsLocation.judo_id = judo_id
            objclsLocation.judo_token = judo_token
            objclsLocation.judo_secret = judo_secret
            objclsLocation.is_skipPay = is_skipPay
            objclsLocation.is_email = is_email
            objclsLocation.cashflow_id = cashflow_id
            objclsLocation.cashflow_url = cashflow_url
            objclsLocation.cashflow_api_key = cashflow_api_key
            objclsLocation.onlinepayment = onlinepayment
            objclsLocation.websales_check_schedule = websales_check_schedule
            objclsLocation.start_date = start_date
            objclsLocation.end_date = end_date
            objclsLocation.Email = Email
            objclsLocation.Contact = Contact
            objclsLocation.header_reciept = header_reciept
            objclsLocation.L_Image = L_Image
            objclsLocation.BG_Color = BG_Color
            objclsLocation.Font_Color = Font_Color
            objclsLocation.Body_Color = Body_Color
            objclsLocation.Click_Collect_image = Click_Collect_image
            objclsLocation.Delivery_image = Delivery_image
            objclsLocation.OrderAtTable_image = OrderAtTable_image
            objclsLocation.click_and_collect = click_and_collect
            objclsLocation.Order_table = Order_table
            objclsLocation.delivery_charges = delivery_charges
            objclsLocation.service_charges = service_charges
            objclsLocation.theme = theme
            objclsLocation.tipAs = tipAs
            objclsLocation.message_delivery = message_delivery
            objclsLocation.message_order_table = message_order_table
            objclsLocation.schedule_required = schedule_required
            objclsLocation.schedule_cnc = schedule_cnc
            objclsLocation.future_booking_days = future_booking_days
            objclsLocation.GraphicViewBackground = GraphicViewBackground
            objclsLocation.GraphicViewTable = GraphicViewTable
            objclsLocation.HourlySlot = HourlySlot
            objclsLocation.CustomPay_id = CustomPay_id
            objclsLocation.CustomPay_token = CustomPay_token
            objclsLocation.CustomPay_secret = CustomPay_secret
            objclsLocation.CustomPay_Base64 = CustomPay_Base64
            objclsLocation.CustomPay_URL = CustomPay_URL
            objclsLocation.Is_Email_APK = Is_Email_APK
            objclsLocation.Is_GetCovers = Is_GetCovers

            objclsLocation.Table_As_Box = Table_As_Box

            objclsLocation.Client_ID = Client_ID
            objclsLocation.clientsecret = clientsecret
            objclsLocation.redirect_uri = redirect_uri
            objclsLocation.gtway_MerchantID = gtway_MerchantID
            objclsLocation.gtway_StoreID = gtway_StoreID
            objclsLocation.gtway_LocationID = gtway_LocationID
            objclsLocation.gtway_StoreName = gtway_StoreName
            objclsLocation.gtway_LocationName = gtway_LocationName

            objclsLocation.GC_Btn_stl = GC_Btn_stl
            objclsLocation.GC_Btn_img_typ = GC_Btn_img_typ
            objclsLocation.GC_Btn_fnt_size = GC_Btn_fnt_size
            objclsLocation.GC_Btn_bkgd_clr = GC_Btn_bkgd_clr
            objclsLocation.GC_Btn_txt_clr = GC_Btn_txt_clr

            objclsLocation.C_Btn_stl = C_Btn_stl
            objclsLocation.C_Btn_img_typ = C_Btn_img_typ
            objclsLocation.C_Btn_fnt_size = C_Btn_fnt_size
            objclsLocation.C_Btn_bkgd_clr = C_Btn_bkgd_clr
            objclsLocation.C_Btn_txt_clr = C_Btn_txt_clr

            objclsLocation.P_Btn_stl = P_Btn_stl
            objclsLocation.P_Btn_img_typ = P_Btn_img_typ
            objclsLocation.P_Btn_fnt_size = P_Btn_fnt_size
            objclsLocation.P_Btn_bkgd_clr = P_Btn_bkgd_clr
            objclsLocation.P_Btn_txt_clr = P_Btn_txt_clr

            objclsLocation.GC_Btn_img_typ_pos = GC_Btn_img_typ_pos
            objclsLocation.C_Btn_img_typ_pos = C_Btn_img_typ_pos
            objclsLocation.P_Btn_img_typ_pos = P_Btn_img_typ_pos

            objclsLocation.login_src_bkgd_clr = login_src_bkgd_clr
            objclsLocation.login_src_login_btn = login_src_login_btn
            objclsLocation.Till_url = Till_url
            objclsLocation.Till_Phn_no = Till_Phn_no



            objclsLocation.SunmiSecondScreen = SunmiSecondScreen
            objclsLocation.SecondScreenImage1 = SecondScreenImage1
            objclsLocation.SecondScreenImage2 = SecondScreenImage2
            objclsLocation.SecondScreenVideo = SecondScreenVideo


            objclsLocation.POS_logo = POS_logo


            objclsLocation.POSlite = POSlite
            objclsLocation.sunmi_video_path = sunmi_video_path

            If objclsLocation.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.code = code
            objclsLocation.name = name
            objclsLocation.address = address
            objclsLocation.country_id = country_id
            objclsLocation.state_id = state_id
            objclsLocation.city_id = city_id
            objclsLocation.ip_address = ip_address
            objclsLocation.login_id = login_id
            objclsLocation.venue_id = venue_id
            objclsLocation.till_auto_log_off = till_auto_log_off
            objclsLocation.machine_id = machine_id
            objclsLocation.is_active = is_active
            objclsLocation.cashback = cashback
            objclsLocation.srv_charge_clickcollect = srv_charge_clickcollect
            objclsLocation.srv_charge_kiosk = srv_charge_kiosk
            objclsLocation.srv_charge_order = srv_charge_order
            objclsLocation.srv_charge_delivery = srv_charge_delivery
            objclsLocation.min_amount = min_amount
            objclsLocation.is_delivery = is_delivery
            objclsLocation.judo_id = judo_id
            objclsLocation.judo_token = judo_token
            objclsLocation.judo_secret = judo_secret
            objclsLocation.is_skipPay = is_skipPay
            objclsLocation.is_email = is_email
            objclsLocation.cashflow_id = cashflow_id
            objclsLocation.cashflow_url = cashflow_url
            objclsLocation.cashflow_api_key = cashflow_api_key
            objclsLocation.onlinepayment = onlinepayment
            objclsLocation.websales_check_schedule = websales_check_schedule
            objclsLocation.start_date = start_date
            objclsLocation.end_date = end_date
            objclsLocation.Email = Email
            objclsLocation.Contact = Contact
            objclsLocation.header_reciept = header_reciept
            objclsLocation.L_Image = L_Image
            objclsLocation.BG_Color = BG_Color
            objclsLocation.Font_Color = Font_Color
            objclsLocation.Body_Color = Body_Color
            objclsLocation.Click_Collect_image = Click_Collect_image
            objclsLocation.Delivery_image = Delivery_image
            objclsLocation.OrderAtTable_image = OrderAtTable_image
            objclsLocation.click_and_collect = click_and_collect
            objclsLocation.Order_table = Order_table
            objclsLocation.delivery_charges = delivery_charges
            objclsLocation.service_charges = service_charges
            objclsLocation.theme = theme
            objclsLocation.tipAs = tipAs
            objclsLocation.message_delivery = message_delivery
            objclsLocation.message_order_table = message_order_table
            objclsLocation.schedule_required = schedule_required
            objclsLocation.schedule_cnc = schedule_cnc
            objclsLocation.oldname = oldname
            objclsLocation.future_booking_days = future_booking_days
            objclsLocation.GraphicViewBackground = GraphicViewBackground
            objclsLocation.GraphicViewTable = GraphicViewTable
            objclsLocation.HourlySlot = HourlySlot
            objclsLocation.CustomPay_id = CustomPay_id
            objclsLocation.CustomPay_token = CustomPay_token
            objclsLocation.CustomPay_secret = CustomPay_secret
            objclsLocation.CustomPay_Base64 = CustomPay_Base64
            objclsLocation.CustomPay_URL = CustomPay_URL
            objclsLocation.Is_Email_APK = Is_Email_APK
            objclsLocation.Is_GetCovers = Is_GetCovers

            objclsLocation.Table_As_Box = Table_As_Box

            objclsLocation.Client_ID = Client_ID
            objclsLocation.clientsecret = clientsecret
            objclsLocation.redirect_uri = redirect_uri
            objclsLocation.gtway_MerchantID = gtway_MerchantID
            objclsLocation.gtway_StoreID = gtway_StoreID
            objclsLocation.gtway_LocationID = gtway_LocationID
            objclsLocation.gtway_StoreName = gtway_StoreName
            objclsLocation.gtway_LocationName = gtway_LocationName

            objclsLocation.GC_Btn_stl = GC_Btn_stl
            objclsLocation.GC_Btn_img_typ = GC_Btn_img_typ
            objclsLocation.GC_Btn_fnt_size = GC_Btn_fnt_size
            objclsLocation.GC_Btn_bkgd_clr = GC_Btn_bkgd_clr
            objclsLocation.GC_Btn_txt_clr = GC_Btn_txt_clr

            objclsLocation.C_Btn_stl = C_Btn_stl
            objclsLocation.C_Btn_img_typ = C_Btn_img_typ
            objclsLocation.C_Btn_fnt_size = C_Btn_fnt_size
            objclsLocation.C_Btn_bkgd_clr = C_Btn_bkgd_clr
            objclsLocation.C_Btn_txt_clr = C_Btn_txt_clr

            objclsLocation.P_Btn_stl = P_Btn_stl
            objclsLocation.P_Btn_img_typ = P_Btn_img_typ
            objclsLocation.P_Btn_fnt_size = P_Btn_fnt_size
            objclsLocation.P_Btn_bkgd_clr = P_Btn_bkgd_clr
            objclsLocation.P_Btn_txt_clr = P_Btn_txt_clr


            objclsLocation.GC_Btn_img_typ_pos = GC_Btn_img_typ_pos
            objclsLocation.C_Btn_img_typ_pos = C_Btn_img_typ_pos
            objclsLocation.P_Btn_img_typ_pos = P_Btn_img_typ_pos


            objclsLocation.login_src_bkgd_clr = login_src_bkgd_clr
            objclsLocation.login_src_login_btn = login_src_login_btn
            objclsLocation.Till_url = Till_url
            objclsLocation.Till_Phn_no = Till_Phn_no


            objclsLocation.SunmiSecondScreen = SunmiSecondScreen
            objclsLocation.SecondScreenImage1 = SecondScreenImage1
            objclsLocation.SecondScreenImage2 = SecondScreenImage2
            '   objclsLocation.SecondScreenvideo = SecondScreenVideo


            objclsLocation.POS_logo = POS_logo

            objclsLocation.POSlite = POSlite
            objclsLocation.sunmi_video_path = sunmi_video_path
            If objclsLocation.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.code = code
            objclsLocation.name = name
            objclsLocation.address = address
            objclsLocation.country_id = country_id
            objclsLocation.state_id = state_id
            objclsLocation.city_id = city_id
            objclsLocation.ip_address = ip_address
            objclsLocation.login_id = login_id
            objclsLocation.venue_id = venue_id
            objclsLocation.Tran_Type = Tran_Type
            objclsLocation.till_auto_log_off = till_auto_log_off
            objclsLocation.machine_id = machine_id
            objclsLocation.is_active = is_active
            objclsLocation.cashback = cashback
            objclsLocation.srv_charge_clickcollect = srv_charge_clickcollect
            objclsLocation.srv_charge_kiosk = srv_charge_kiosk
            objclsLocation.srv_charge_order = srv_charge_order
            objclsLocation.srv_charge_delivery = srv_charge_delivery
            objclsLocation.min_amount = min_amount
            objclsLocation.is_delivery = is_delivery
            objclsLocation.judo_id = judo_id
            objclsLocation.judo_token = judo_token
            objclsLocation.judo_secret = judo_secret
            objclsLocation.is_skipPay = is_skipPay
            objclsLocation.is_email = is_email
            objclsLocation.cashflow_id = cashflow_id
            objclsLocation.cashflow_url = cashflow_url
            objclsLocation.cashflow_api_key = cashflow_api_key
            objclsLocation.onlinepayment = onlinepayment
            objclsLocation.websales_check_schedule = websales_check_schedule
            objclsLocation.start_date = start_date
            objclsLocation.end_date = end_date
            objclsLocation.Email = Email
            objclsLocation.Contact = Contact
            objclsLocation.header_reciept = header_reciept
            objclsLocation.L_Image = L_Image
            objclsLocation.BG_Color = BG_Color
            objclsLocation.Font_Color = Font_Color
            objclsLocation.Body_Color = Body_Color
            objclsLocation.Click_Collect_image = Click_Collect_image
            objclsLocation.Delivery_image = Delivery_image
            objclsLocation.OrderAtTable_image = OrderAtTable_image
            objclsLocation.click_and_collect = click_and_collect
            objclsLocation.Order_table = Order_table
            objclsLocation.delivery_charges = delivery_charges
            objclsLocation.service_charges = service_charges
            objclsLocation.theme = theme
            objclsLocation.tipAs = tipAs
            objclsLocation.message_delivery = message_delivery
            objclsLocation.message_order_table = message_order_table
            objclsLocation.schedule_required = schedule_required
            objclsLocation.schedule_cnc = schedule_cnc
            objclsLocation.oldname = oldname
            objclsLocation.future_booking_days = future_booking_days
            objclsLocation.GraphicViewBackground = GraphicViewBackground
            objclsLocation.GraphicViewTable = GraphicViewTable
            objclsLocation.HourlySlot = HourlySlot
            objclsLocation.CustomPay_id = CustomPay_id
            objclsLocation.CustomPay_token = CustomPay_token
            objclsLocation.CustomPay_secret = CustomPay_secret
            objclsLocation.CustomPay_Base64 = CustomPay_Base64
            objclsLocation.CustomPay_URL = CustomPay_URL
            objclsLocation.Is_Email_APK = Is_Email_APK
            objclsLocation.Is_GetCovers = Is_GetCovers

            objclsLocation.Table_As_Box = Table_As_Box

            objclsLocation.Client_ID = Client_ID
            objclsLocation.clientsecret = clientsecret
            objclsLocation.redirect_uri = redirect_uri
            objclsLocation.gtway_MerchantID = gtway_MerchantID
            objclsLocation.gtway_StoreID = gtway_StoreID
            objclsLocation.gtway_LocationID = gtway_LocationID
            objclsLocation.gtway_StoreName = gtway_StoreName
            objclsLocation.gtway_LocationName = gtway_LocationName

            objclsLocation.GC_Btn_stl = GC_Btn_stl
            objclsLocation.GC_Btn_img_typ = GC_Btn_img_typ
            objclsLocation.GC_Btn_fnt_size = GC_Btn_fnt_size
            objclsLocation.GC_Btn_bkgd_clr = GC_Btn_bkgd_clr
            objclsLocation.GC_Btn_txt_clr = GC_Btn_txt_clr

            objclsLocation.C_Btn_stl = C_Btn_stl
            objclsLocation.C_Btn_img_typ = C_Btn_img_typ
            objclsLocation.C_Btn_fnt_size = C_Btn_fnt_size
            objclsLocation.C_Btn_bkgd_clr = C_Btn_bkgd_clr
            objclsLocation.C_Btn_txt_clr = C_Btn_txt_clr

            objclsLocation.P_Btn_stl = P_Btn_stl
            objclsLocation.P_Btn_img_typ = P_Btn_img_typ
            objclsLocation.P_Btn_fnt_size = P_Btn_fnt_size
            objclsLocation.P_Btn_bkgd_clr = P_Btn_bkgd_clr
            objclsLocation.P_Btn_txt_clr = P_Btn_txt_clr


            objclsLocation.GC_Btn_img_typ_pos = GC_Btn_img_typ_pos
            objclsLocation.C_Btn_img_typ_pos = C_Btn_img_typ_pos
            objclsLocation.P_Btn_img_typ_pos = P_Btn_img_typ_pos

            objclsLocation.login_src_bkgd_clr = login_src_bkgd_clr
            objclsLocation.login_src_login_btn = login_src_login_btn
            objclsLocation.Till_url = Till_url
            objclsLocation.Till_Phn_no = Till_Phn_no

            objclsLocation.SunmiSecondScreen = SunmiSecondScreen
            objclsLocation.SecondScreenImage1 = SecondScreenImage1
            objclsLocation.SecondScreenImage2 = SecondScreenImage2
            ' objclsLocation.SecondScreenvideo = SecondScreenVideo

            objclsLocation.POS_logo = POS_logo

            objclsLocation.POSlite = POSlite
            objclsLocation.sunmi_video_path = sunmi_video_path
            If objclsLocation.Delete() Then
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
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            dt = objclsLocation.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.cmp_id = cmp_id
            dt = objclsLocation.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectLocationByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.cmp_id = cmp_id
            dt = objclsLocation.[SelectLocationByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectLocationByVenue]() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.cmp_id = cmp_id
            objclsLocation.venue_id = venue_id
            dt = objclsLocation.[SelectLocationByVenue]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SyncLocationAllTill() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.cmp_id = cmp_id

            dt = objclsLocation.SyncLocationAllTill()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Function CloudApiInsert() As Boolean
    '    Try
    '        objclsLocation = New clsLocation()
    '    Catch ex As Exception

    '    End Try
    '    objclsLocation.GC_Btn_img_typ_pos = GC_Btn_img_typ_pos
    '    objclsLocation.C_Btn_img_typ_pos = C_Btn_img_typ_pos
    '    objclsLocation.P_Btn_img_typ_pos = P_Btn_img_typ_pos
    '    If objclsLocation.Insert() Then
    '        Return True
    '    End If
    '    Return False
    'End Function
    Public Function CloudApiInsert() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.onlinepayment = onlinepayment

            objclsLocation.cloudValue = cloudValue
            objclsLocation.cloudKey = cloudKey
            objclsLocation.cloudurl = cloudurl
            If objclsLocation.CloudApiInsert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function CloudApiupdate() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.location_id = location_id
            objclsLocation.cloud_id = cloud_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.onlinepayment = onlinepayment

            objclsLocation.cloudValue = cloudValue
            objclsLocation.cloudKey = cloudKey
            objclsLocation.cloudurl = cloudurl

            If objclsLocation.CloudApiupdate() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function [SelectAllApi]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsLocation = New clsLocation()
    '        objclsLocation.cmp_id = cmp_id
    '        dt = objclsLocation.[SelectAllApi]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function [SelectAllApi]() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.cloud_id = cloud_id
            objclsLocation.cmp_id = cmp_id
            dt = objclsLocation.[SelectAllAPI]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function DeletecloudApi() As Boolean
        Try
            objclsLocation = New clsLocation()
            objclsLocation.cloud_id = cloud_id
            objclsLocation.cmp_id = cmp_id
            objclsLocation.location_id = location_id
            'objclsLocation.gtway_LocationID = gtway_LocationID

            'objclsLocation.code = code
            'objclsLocation.name = name
            'objclsLocation.address = address


            'objclsLocation.POS_logo = POS_logo

            'objclsLocation.POSlite = POSlite
            'objclsLocation.sunmi_video_path = sunmi_video_path
            If objclsLocation.DeletecloudApi() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Historycloudnet() As Boolean
        Try
            objclsLocation = New clsLocation()

            objclsLocation.cmp_id = cmp_id
            objclsLocation.cloudurl = cloudurl
            objclsLocation.location_id = location_id
            objclsLocation.machine_id = machine_id
            objclsLocation.reading = reading
            objclsLocation.cassette = cassette
            objclsLocation.cloudKey = cloudKey
            objclsLocation.cloudValue = cloudValue
            objclsLocation.created_date = created_date
            objclsLocation.gtway_StoreName = gtway_StoreName
            objclsLocation.cloudKey = cloudKey

            If objclsLocation.Historycloudnet() Then
                Return True
            End If


            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectTRansferd_data]() As DataTable
        Dim dt As DataTable
        Try
            objclsLocation = New clsLocation()
            objclsLocation.created_date = created_date
            objclsLocation.modify_date = modify_date

            objclsLocation.cmp_id = cmp_id
            objclsLocation.location_id = location_id
            dt = objclsLocation.[SelectTRansferd_data]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsLocation
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _location_id As Integer
    Private _cloud_id As Integer
    Private _cmp_id As Integer
    Private _code As String
    Private _name As String
    Private _address As String
    Private _country_id As Integer
    Private _state_id As Integer
    Private _city_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _ip_address As String
    Private _login_id As Integer
    Private _venue_id As Integer
    Private _Tran_Type As String
    Private _till_auto_log_off As Byte
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _cashback As Byte
    Private _srv_charge_clickcollect As Byte
    Private _srv_charge_kiosk As Byte
    Private _srv_charge_order As Byte
    Private _srv_charge_delivery As Byte
    Private _is_delivery As Byte
    Private _min_amount As Decimal
    Private _judo_id As String
    Private _judo_token As String
    Private _judo_secret As String
    Private _is_skipPay As Byte
    Private _is_email As Byte
    Private _cashflow_id As String
    Private _cashflow_url As String
    Private _cashflow_api_key As String
    Private _onlinepayment As String
    Private _websales_check_schedule As Byte
    Private _schedule_required As Byte
    Private _start_date As String
    Private _end_date As String
    Private _Email As String
    Private _Contact As String
    Private _header_reciept As String
    Private _L_Image As Byte()
    Private _BG_Color As String
    Private _Font_Color As String
    Private _Body_Color As String
    Private _Click_Collect_image As Byte()
    Private _Delivery_image As Byte()
    Private _OrderAtTable_image As Byte()
    Private _click_and_collect As Byte
    Private _Order_table As Byte
    Private _theme As String
    Private _tipAs As String
    Private _delivery_charges As Double
    Private _service_charges As Double
    Private _message_delivery As String
    Private _message_order_table As String
    Private _schedule_cnc As Byte
    Private _oldname As String
    Private _future_booking_days As Integer
    Private _GraphicViewBackground As Byte()
    Private _GraphicViewTable As Byte()
    Private _HourlySlot As Byte
    Private _CustomPay_id As String
    Private _CustomPay_token As String
    Private _CustomPay_secret As String
    Private _CustomPay_Base64 As String
    Private _CustomPay_URL As String
    Private _Is_Email_APK As Byte
    Private _Is_GetCovers As Byte

    Private _Table_As_Box As String
    Private _Client_ID As String
    Private _clientsecret As String
    Private _redirect_uri As String
    Private _gtway_MerchantID As String
    Private _gtway_StoreID As Integer
    Private _gtway_LocationID As Integer
    Private _gtway_StoreName As String
    Private _gtway_LocationName As String

    Private _posLite As Byte
    Private _GC_Btn_stl As String
    Private _GC_Btn_img_typ As String
    Private _GC_Btn_fnt_size As String
    Private _GC_Btn_bkgd_clr As String
    Private _GC_Btn_txt_clr As String

    Private _C_Btn_stl As String
    Private _C_Btn_img_typ As String
    Private _C_Btn_fnt_size As String
    Private _C_Btn_bkgd_clr As String
    Private _C_Btn_txt_clr As String

    Private _P_Btn_stl As String
    Private _P_Btn_img_typ As String
    Private _P_Btn_fnt_size As String
    Private _P_Btn_bkgd_clr As String
    Private _P_Btn_txt_clr As String

    Private _sunmi_video_path As String
    Private _GC_Btn_img_typ_pos As String
    Private _C_Btn_img_typ_pos As String
    Private _P_Btn_img_typ_pos As String

    Private _login_src_bkgd_clr As String
    Private _login_src_login_btn As String
    Private _Till_url As String
    Private _Till_Phn_no As String


    Private _POS_logo As Byte()


    Private _sunmiSecondScreen As Byte
    Private _secondScreenImage1 As Byte()
    Private _secondScreenImage2 As Byte()
    Private _secondScreenVideo As Byte()
    Private _cloudValue As String
    Private _cloudKey As String
    Private _cloudurl As String
    Private _salesID As Integer
    Private _cassette As Integer
    Private _reading As Integer

    Private objclsLocation As clsLocation

#End Region

#Region "Public Property"


    Public Property sunmi_video_path() As Byte
        Get
            Return _sunmi_video_path
        End Get
        Set(ByVal value As Byte)
            _sunmi_video_path = value
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
    Public Property POSlite() As Byte
        Get
            Return _posLite
        End Get
        Set(ByVal value As Byte)
            _posLite = value
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
    Public Property SecondScreenVideo() As Byte()
        Get
            Return _secondScreenVideo
        End Get
        Set(ByVal value As Byte())
            _secondScreenVideo = value
        End Set
    End Property
    Public Property GC_Btn_img_typ_pos() As String
        Get
            Return _GC_Btn_img_typ_pos
        End Get
        Set(value As String)
            _GC_Btn_img_typ_pos = value
        End Set

    End Property
    Public Property C_Btn_img_typ_pos() As String
        Get
            Return _C_Btn_img_typ_pos
        End Get
        Set(value As String)
            _C_Btn_img_typ_pos = value
        End Set

    End Property
    Public Property P_Btn_img_typ_pos() As String
        Get
            Return _P_Btn_img_typ_pos
        End Get
        Set(value As String)
            _P_Btn_img_typ_pos = value
        End Set

    End Property
    Public Property Till_Phn_no() As String
        Get
            Return _Till_Phn_no
        End Get
        Set(value As String)
            _Till_Phn_no = value
        End Set

    End Property
    Public Property Till_url() As String
        Get
            Return _Till_url
        End Get
        Set(value As String)
            _Till_url = value
        End Set

    End Property
    Public Property login_src_login_btn() As String
        Get
            Return _login_src_login_btn
        End Get
        Set(value As String)
            _login_src_login_btn = value
        End Set

    End Property
    Public Property login_src_bkgd_clr() As String
        Get
            Return _login_src_bkgd_clr
        End Get
        Set(value As String)
            _login_src_bkgd_clr = value
        End Set

    End Property
    Public Property GC_Btn_stl() As String
        Get
            Return _GC_Btn_stl
        End Get
        Set(value As String)
            _GC_Btn_stl = value
        End Set

    End Property
    Public Property GC_Btn_img_typ() As String
        Get
            Return _GC_Btn_img_typ
        End Get
        Set(value As String)
            _GC_Btn_img_typ = value
        End Set

    End Property
    Public Property GC_Btn_fnt_size() As String
        Get
            Return _GC_Btn_fnt_size
        End Get
        Set(value As String)
            _GC_Btn_fnt_size = value
        End Set

    End Property
    Public Property GC_Btn_bkgd_clr() As String
        Get
            Return _GC_Btn_bkgd_clr
        End Get
        Set(value As String)
            _GC_Btn_bkgd_clr = value
        End Set

    End Property
    Public Property GC_Btn_txt_clr() As String
        Get
            Return _GC_Btn_txt_clr
        End Get
        Set(value As String)
            _GC_Btn_txt_clr = value
        End Set

    End Property

    '----------

    Public Property C_Btn_stl() As String
        Get
            Return _C_Btn_stl
        End Get
        Set(value As String)
            _C_Btn_stl = value
        End Set

    End Property
    Public Property C_Btn_img_typ() As String
        Get
            Return _C_Btn_img_typ
        End Get
        Set(value As String)
            _C_Btn_img_typ = value
        End Set

    End Property
    Public Property C_Btn_fnt_size() As String
        Get
            Return _C_Btn_fnt_size
        End Get
        Set(value As String)
            _C_Btn_fnt_size = value
        End Set

    End Property
    Public Property C_Btn_bkgd_clr() As String
        Get
            Return _C_Btn_bkgd_clr
        End Get
        Set(value As String)
            _C_Btn_bkgd_clr = value
        End Set

    End Property
    Public Property C_Btn_txt_clr() As String
        Get
            Return _C_Btn_txt_clr
        End Get
        Set(value As String)
            _C_Btn_txt_clr = value
        End Set

    End Property

    '-------

    Public Property P_Btn_stl() As String
        Get
            Return _P_Btn_stl
        End Get
        Set(value As String)
            _P_Btn_stl = value
        End Set

    End Property
    Public Property P_Btn_img_typ() As String
        Get
            Return _P_Btn_img_typ
        End Get
        Set(value As String)
            _P_Btn_img_typ = value
        End Set

    End Property
    Public Property P_Btn_fnt_size() As String
        Get
            Return _P_Btn_fnt_size
        End Get
        Set(value As String)
            _P_Btn_fnt_size = value
        End Set

    End Property
    Public Property P_Btn_bkgd_clr() As String
        Get
            Return _P_Btn_bkgd_clr
        End Get
        Set(value As String)
            _P_Btn_bkgd_clr = value
        End Set

    End Property
    Public Property P_Btn_txt_clr() As String
        Get
            Return _P_Btn_txt_clr
        End Get
        Set(value As String)
            _P_Btn_txt_clr = value
        End Set

    End Property
    Public Property gtway_StoreName() As String
        Get
            Return _gtway_StoreName
        End Get
        Set(value As String)
            _gtway_StoreName = value
        End Set

    End Property

    Public Property gtway_LocationName() As String
        Get
            Return _gtway_LocationName
        End Get
        Set(value As String)
            _gtway_LocationName = value
        End Set

    End Property
    Public Property gtway_StoreID() As Integer
        Get
            Return _gtway_StoreID
        End Get
        Set(ByVal value As Integer)
            _gtway_StoreID = value
        End Set
    End Property

    Public Property gtway_LocationID() As Integer
        Get
            Return _gtway_LocationID
        End Get
        Set(ByVal value As Integer)
            _gtway_LocationID = value
        End Set
    End Property

    Public Property gtway_MerchantID() As String
        Get
            Return _gtway_MerchantID
        End Get
        Set(value As String)
            _gtway_MerchantID = value
        End Set

    End Property


    Public Property Client_ID() As String
        Get
            Return _Client_ID
        End Get
        Set(value As String)
            _Client_ID = value
        End Set
    End Property

    Public Property clientsecret() As String
        Get
            Return _clientsecret
        End Get
        Set(value As String)
            _clientsecret = value
        End Set
    End Property

    Public Property redirect_uri() As String
        Get
            Return _redirect_uri
        End Get
        Set(value As String)
            _redirect_uri = value
        End Set
    End Property

    Public Property Table_As_Box() As String
        Get
            Return _Table_As_Box
        End Get
        Set(value As String)
            _Table_As_Box = value
        End Set
    End Property

    Public Property Is_Email_APK() As Byte
        Get
            Return _Is_Email_APK
        End Get
        Set(ByVal value As Byte)
            _Is_Email_APK = value
        End Set
    End Property
    Public Property Is_GetCovers() As Byte
        Get
            Return _Is_GetCovers
        End Get
        Set(ByVal value As Byte)
            _Is_GetCovers = value
        End Set
    End Property

    Public Property CustomPay_id() As String
        Get
            Return _CustomPay_id
        End Get
        Set(value As String)
            _CustomPay_id = value
        End Set
    End Property

    Public Property CustomPay_token() As String
        Get
            Return _CustomPay_token
        End Get
        Set(value As String)
            _CustomPay_token = value
        End Set
    End Property

    Public Property CustomPay_Base64() As String
        Get
            Return _CustomPay_Base64
        End Get
        Set(value As String)
            _CustomPay_Base64 = value
        End Set
    End Property

    Public Property CustomPay_secret() As String
        Get
            Return _CustomPay_secret
        End Get
        Set(value As String)
            _CustomPay_secret = value
        End Set
    End Property

    Public Property CustomPay_URL() As String
        Get
            Return _CustomPay_URL
        End Get
        Set(value As String)
            _CustomPay_URL = value
        End Set
    End Property

    Public Property message_delivery() As String
        Get
            Return _message_delivery
        End Get
        Set(ByVal value As String)
            _message_delivery = value
        End Set
    End Property

    Public Property message_order_table() As String
        Get
            Return _message_order_table
        End Get
        Set(ByVal value As String)
            _message_order_table = value
        End Set
    End Property
    Public Property is_email() As Byte
        Get
            Return _is_email
        End Get
        Set(ByVal value As Byte)
            _is_email = value
        End Set
    End Property
    Public Property judo_id() As String
        Get
            Return _judo_id
        End Get
        Set(value As String)
            _judo_id = value
        End Set
    End Property
    Public Property schedule_cnc() As Byte
        Get
            Return _schedule_cnc
        End Get
        Set(ByVal value As Byte)
            _schedule_cnc = value
        End Set
    End Property

    Public Property HourlySlot() As Byte
        Get
            Return _HourlySlot
        End Get
        Set(ByVal value As Byte)
            _HourlySlot = value
        End Set
    End Property
    Public Property judo_token() As String
        Get
            Return _judo_token
        End Get
        Set(value As String)
            _judo_token = value
        End Set
    End Property

    Public Property judo_secret() As String
        Get
            Return _judo_secret
        End Get
        Set(value As String)
            _judo_secret = value
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
    Public Property cashback() As Byte
        Get
            Return _cashback
        End Get
        Set(ByVal value As Byte)
            _cashback = value
        End Set
    End Property
    Public Property srv_charge_clickcollect() As Byte
        Get
            Return _srv_charge_clickcollect
        End Get
        Set(ByVal value As Byte)
            _srv_charge_clickcollect = value
        End Set
    End Property
    Public Property srv_charge_kiosk() As Byte
        Get
            Return _srv_charge_kiosk
        End Get
        Set(ByVal value As Byte)
            _srv_charge_kiosk = value
        End Set
    End Property
    Public Property srv_charge_order() As Byte
        Get
            Return _srv_charge_order
        End Get
        Set(ByVal value As Byte)
            _srv_charge_order = value
        End Set
    End Property
    Public Property srv_charge_delivery() As Byte
        Get
            Return _srv_charge_delivery
        End Get
        Set(ByVal value As Byte)
            _srv_charge_delivery = value
        End Set
    End Property
    Public Property is_skipPay() As Byte
        Get
            Return _is_skipPay
        End Get
        Set(ByVal value As Byte)
            _is_skipPay = value
        End Set
    End Property
    Public Property is_delivery() As Byte
        Get
            Return _is_delivery
        End Get
        Set(ByVal value As Byte)
            _is_delivery = value
        End Set
    End Property
    Public Property min_amount() As Decimal
        Get
            Return _min_amount
        End Get
        Set(ByVal value As Decimal)
            _min_amount = value
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
    Public Property cloud_id() As Integer
        Get
            Return _cloud_id
        End Get
        Set(ByVal value As Integer)
            _cloud_id = value
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

    Public Property address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property
    Public Property country_id() As Integer
        Get
            Return _country_id
        End Get
        Set(ByVal value As Integer)
            _country_id = value
        End Set
    End Property
    Public Property state_id() As Integer
        Get
            Return _state_id
        End Get
        Set(ByVal value As Integer)
            _state_id = value
        End Set
    End Property
    Public Property city_id() As Integer
        Get
            Return _city_id
        End Get
        Set(ByVal value As Integer)
            _city_id = value
        End Set
    End Property
    Public Property salesID() As Integer
        Get
            Return _salesID
        End Get
        Set(ByVal value As Integer)
            _salesID = value
        End Set
    End Property
    Public Property cassette() As Integer
        Get
            Return _cassette
        End Get
        Set(ByVal value As Integer)
            _cassette = value
        End Set
    End Property
    Public Property reading() As Integer
        Get
            Return _reading
        End Get
        Set(ByVal value As Integer)
            _reading = value
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
    Public Property venue_id() As Integer
        Get
            Return _venue_id
        End Get
        Set(ByVal value As Integer)
            _venue_id = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property

    Public Property till_auto_log_off() As Byte
        Get
            Return _till_auto_log_off
        End Get
        Set(ByVal value As Byte)
            _till_auto_log_off = value
        End Set
    End Property
    Public Property cashflow_url() As String
        Get
            Return _cashflow_url
        End Get
        Set(value As String)
            _cashflow_url = value
        End Set
    End Property
    Public Property cashflow_id() As String
        Get
            Return _cashflow_id
        End Get
        Set(value As String)
            _cashflow_id = value
        End Set
    End Property
    Public Property cashflow_api_key() As String
        Get
            Return _cashflow_api_key
        End Get
        Set(value As String)
            _cashflow_api_key = value
        End Set
    End Property
    Public Property onlinepayment() As String
        Get
            Return _onlinepayment
        End Get
        Set(ByVal value As String)
            _onlinepayment = value
        End Set
    End Property
    Public Property websales_check_schedule() As Byte
        Get
            Return _websales_check_schedule
        End Get
        Set(ByVal value As Byte)
            _websales_check_schedule = value
        End Set
    End Property
    Public Property schedule_required() As Byte
        Get
            Return _schedule_required
        End Get
        Set(ByVal value As Byte)
            _schedule_required = value
        End Set
    End Property

    Public Property start_date() As String
        Get
            Return _start_date
        End Get
        Set(ByVal value As String)
            _start_date = value
        End Set
    End Property
    Public Property end_date() As String
        Get
            Return _end_date
        End Get
        Set(ByVal value As String)
            _end_date = value
        End Set
    End Property
    Public Property Contact() As String
        Get
            Return _Contact
        End Get
        Set(value As String)
            _Contact = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property

    Public Property header_reciept() As String
        Get
            Return _header_reciept
        End Get
        Set(ByVal value As String)
            _header_reciept = value
        End Set
    End Property
    Public Property L_Image() As Byte()
        Get
            Return _L_Image
        End Get
        Set(ByVal value As Byte())
            _L_Image = value
        End Set
    End Property
    Public Property BG_Color() As String
        Get
            Return _BG_Color
        End Get
        Set(ByVal value As String)
            _BG_Color = value
        End Set
    End Property
    Public Property Font_Color() As String
        Get
            Return _Font_Color
        End Get
        Set(ByVal value As String)
            _Font_Color = value
        End Set
    End Property
    Public Property Body_Color() As String
        Get
            Return _Body_Color
        End Get
        Set(ByVal value As String)
            _Body_Color = value
        End Set
    End Property
    Public Property Click_Collect_image() As Byte()
        Get
            Return _Click_Collect_image
        End Get
        Set(ByVal value As Byte())
            _Click_Collect_image = value
        End Set
    End Property
    Public Property Delivery_image() As Byte()
        Get
            Return _Delivery_image
        End Get
        Set(ByVal value As Byte())
            _Delivery_image = value
        End Set
    End Property
    Public Property OrderAtTable_image() As Byte()
        Get
            Return _OrderAtTable_image
        End Get
        Set(ByVal value As Byte())
            _OrderAtTable_image = value
        End Set
    End Property
    Public Property POS_logo() As Byte()
        Get
            Return _POS_logo
        End Get
        Set(ByVal value As Byte())
            _POS_logo = value
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
    Public Property Order_table() As Byte
        Get
            Return _Order_table
        End Get
        Set(ByVal value As Byte)
            _Order_table = value
        End Set
    End Property
    Public Property delivery_charges() As Double
        Get
            Return _delivery_charges
        End Get
        Set(ByVal value As Double)
            _delivery_charges = value
        End Set
    End Property
    Public Property service_charges() As Double
        Get
            Return _service_charges
        End Get
        Set(ByVal value As Double)
            _service_charges = value
        End Set
    End Property
    Public Property theme() As String
        Get
            Return _theme
        End Get
        Set(ByVal value As String)
            _theme = value
        End Set
    End Property
    Public Property tipAs() As String
        Get
            Return _tipAs
        End Get
        Set(ByVal value As String)
            _tipAs = value
        End Set
    End Property


    Public Property oldname() As String
        Get
            Return _oldname
        End Get
        Set(ByVal value As String)
            _oldname = value
        End Set
    End Property

    Public Property future_booking_days() As Integer
        Get
            Return _future_booking_days
        End Get
        Set(ByVal value As Integer)
            _future_booking_days = value
        End Set
    End Property

    Public Property GraphicViewBackground() As Byte()
        Get
            Return _GraphicViewBackground
        End Get
        Set(ByVal value As Byte())
            _GraphicViewBackground = value
        End Set
    End Property
    Public Property GraphicViewTable() As Byte()
        Get
            Return _GraphicViewTable
        End Get
        Set(ByVal value As Byte())
            _GraphicViewTable = value
        End Set
    End Property
    Public Property cloudValue() As String
        Get
            Return _cloudValue
        End Get
        Set(value As String)
            _cloudValue = value
        End Set

    End Property
    Public Property cloudurl() As String
        Get
            Return _cloudurl
        End Get
        Set(value As String)
            _cloudurl = value
        End Set

    End Property
    Public Property cloudKey() As String
        Get
            Return _cloudKey
        End Get
        Set(value As String)
            _cloudKey = value
        End Set

    End Property

#End Region
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@till_auto_log_off", till_auto_log_off, SqlDbType.TinyInt)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@cashback", cashback, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_clickcollect", srv_charge_clickcollect, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_kiosk", srv_charge_kiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_order", srv_charge_order, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_delivery", srv_charge_delivery, SqlDbType.TinyInt)
            oColSqlparram.Add("@min_payment", min_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@is_delivery", is_delivery, SqlDbType.TinyInt)
            oColSqlparram.Add("@judo_id", judo_id)
            oColSqlparram.Add("@judo_token", judo_token)
            oColSqlparram.Add("@judo_secret", judo_secret)
            oColSqlparram.Add("@is_skipPay", is_skipPay, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_email", is_email, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@cashflow_id", cashflow_id)
            oColSqlparram.Add("@cashflow_url", cashflow_url)
            oColSqlparram.Add("@cashflow_api_key", cashflow_api_key)
            oColSqlparram.Add("@onlinepayment", onlinepayment)
            oColSqlparram.Add("@websales_check_schedule", websales_check_schedule, SqlDbType.TinyInt)
            oColSqlparram.Add("@start_date", start_date)
            oColSqlparram.Add("@end_date", end_date)
            oColSqlparram.Add("@Email", Email)
            oColSqlparram.Add("@Contact", Contact)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@Body_Color", Body_Color)
            oColSqlparram.Add("@header_reciept", header_reciept)
            oColSqlparram.Add("@message_delivery", message_delivery)
            oColSqlparram.Add("@message_order_table", message_order_table)
            oColSqlparram.Add("@schedule_required", schedule_required, SqlDbType.TinyInt)
            oColSqlparram.Add("@schedule_cnc", schedule_cnc, SqlDbType.TinyInt)
            If Not Click_Collect_image Is Nothing Then
                oColSqlparram.Add("@Click_Collect_image", Click_Collect_image, SqlDbType.Image)
            End If
            If Not Delivery_image Is Nothing Then
                oColSqlparram.Add("@Delivery_image", Delivery_image, SqlDbType.Image)
            End If
            If Not OrderAtTable_image Is Nothing Then
                oColSqlparram.Add("@OrderAtTable_image", OrderAtTable_image, SqlDbType.Image)
            End If
            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@L_image", L_Image, SqlDbType.Image)
            End If
            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.TinyInt)
            oColSqlparram.Add("@Order_table", Order_table, SqlDbType.TinyInt)
            oColSqlparram.Add("@delivery_charges", delivery_charges, SqlDbType.Float)
            oColSqlparram.Add("@service_charges", service_charges, SqlDbType.Float)
            oColSqlparram.Add("@theme", theme)
            oColSqlparram.Add("@tipAs", tipAs)
            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@L_image", L_Image, SqlDbType.Image)
            End If
            oColSqlparram.Add("@oldname", "")
            oColSqlparram.Add("@future_booking_days", future_booking_days, SqlDbType.Int)

            If Not GraphicViewBackground Is Nothing Then
                oColSqlparram.Add("@GraphicViewBackground", GraphicViewBackground, SqlDbType.Image)
            End If

            If Not GraphicViewTable Is Nothing Then
                oColSqlparram.Add("@GraphicViewTable", GraphicViewTable, SqlDbType.Image)
            End If

            oColSqlparram.Add("@HourlySlot", HourlySlot, SqlDbType.TinyInt)
            oColSqlparram.Add("@CustomPay_id", CustomPay_id)
            oColSqlparram.Add("@CustomPay_secret", CustomPay_secret)
            oColSqlparram.Add("@CustomPay_token", CustomPay_token)
            oColSqlparram.Add("@CustomPay_Base64", CustomPay_Base64)
            oColSqlparram.Add("@CustomPay_url", CustomPay_URL)
            oColSqlparram.Add("@Is_Email_APK", Is_Email_APK)
            oColSqlparram.Add("@Is_GetCovers", Is_GetCovers)

            oColSqlparram.Add("@Table_As_Box", Table_As_Box)

            oColSqlparram.Add("@Client_ID", Client_ID)
            oColSqlparram.Add("@clientsecret", clientsecret)
            oColSqlparram.Add("@redirect_uri", redirect_uri)
            oColSqlparram.Add("@gtway_MerchantID", gtway_MerchantID)
            oColSqlparram.Add("@gtway_StoreID", gtway_StoreID, SqlDbType.Int)
            oColSqlparram.Add("@gtway_LocationID", gtway_LocationID, SqlDbType.Int)
            oColSqlparram.Add("@gtway_StoreName", gtway_StoreName)
            oColSqlparram.Add("@gtway_LocationName", gtway_LocationName)

            oColSqlparram.Add("@GC_Btn_stl", GC_Btn_stl)
            oColSqlparram.Add("@GC_Btn_img_typ", GC_Btn_img_typ)
            oColSqlparram.Add("@GC_Btn_fnt_size", GC_Btn_fnt_size)
            oColSqlparram.Add("@GC_Btn_bkgd_clr", GC_Btn_bkgd_clr)
            oColSqlparram.Add("@GC_Btn_txt_clr", GC_Btn_txt_clr)

            oColSqlparram.Add("@C_Btn_stl", C_Btn_stl)
            oColSqlparram.Add("@C_Btn_img_typ", C_Btn_img_typ)
            oColSqlparram.Add("@C_Btn_fnt_size", C_Btn_fnt_size)
            oColSqlparram.Add("@C_Btn_bkgd_clr", C_Btn_bkgd_clr)
            oColSqlparram.Add("@C_Btn_txt_clr", C_Btn_txt_clr)

            oColSqlparram.Add("@P_Btn_stl", P_Btn_stl)
            oColSqlparram.Add("@P_Btn_img_typ", P_Btn_img_typ)
            oColSqlparram.Add("@P_Btn_fnt_size", P_Btn_fnt_size)
            oColSqlparram.Add("@P_Btn_bkgd_clr", P_Btn_bkgd_clr)
            oColSqlparram.Add("@P_Btn_txt_clr", P_Btn_txt_clr)

            oColSqlparram.Add("@GC_Btn_img_typ_pos", GC_Btn_img_typ_pos)
            oColSqlparram.Add("@C_Btn_img_typ_pos", C_Btn_img_typ_pos)
            oColSqlparram.Add("@P_Btn_img_typ_pos", P_Btn_img_typ_pos)

            oColSqlparram.Add("@login_src_bkgd_clr", login_src_bkgd_clr)
            oColSqlparram.Add("@login_src_login_btn", login_src_login_btn)
            oColSqlparram.Add("@Till_url", Till_url)
            oColSqlparram.Add("@Till_Phn_no", Till_Phn_no)
            If Not POS_logo Is Nothing Then
                oColSqlparram.Add("@POS_logo", POS_logo, SqlDbType.Image)
            End If
            'oColSqlparram.Add("@sunmiSecondScreen", SunmiSecondScreen)
            'oColSqlparram.Add("@SecondScreenImage1", SecondScreenImage1, SqlDbType.Image)
            'oColSqlparram.Add("@SecondScreenImage2", SecondScreenImage2, SqlDbType.Image)
            'oColSqlparram.Add("@SecondScreenVideo", SecondScreenImage2)


            'oColSqlparram.Add("@poslite", POSlite)
            'oColSqlparram.Add("@sunmi_video_path", sunmi_video_path)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Location", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@till_auto_log_off", till_auto_log_off, SqlDbType.TinyInt)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@cashback", cashback, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_clickcollect", srv_charge_clickcollect, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_kiosk", srv_charge_kiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_order", srv_charge_order, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_delivery", srv_charge_delivery, SqlDbType.TinyInt)
            oColSqlparram.Add("@min_payment", min_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@is_delivery", is_delivery, SqlDbType.TinyInt)
            oColSqlparram.Add("@judo_id", judo_id)
            oColSqlparram.Add("@judo_token", judo_token)
            oColSqlparram.Add("@judo_secret", judo_secret)
            oColSqlparram.Add("@is_skipPay", is_skipPay, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_email", is_email, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@cashflow_id", cashflow_id)
            oColSqlparram.Add("@cashflow_url", cashflow_url)
            oColSqlparram.Add("@cashflow_api_key", cashflow_api_key)
            oColSqlparram.Add("@onlinepayment", onlinepayment)
            oColSqlparram.Add("@websales_check_schedule", websales_check_schedule, SqlDbType.TinyInt)
            oColSqlparram.Add("@start_date", start_date)
            oColSqlparram.Add("@end_date", end_date)
            oColSqlparram.Add("@Email", Email)
            oColSqlparram.Add("@Contact", Contact)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@Body_Color", Body_Color)
            oColSqlparram.Add("@header_reciept", header_reciept)
            oColSqlparram.Add("@message_delivery", message_delivery)
            oColSqlparram.Add("@message_order_table", message_order_table)
            oColSqlparram.Add("@schedule_required", schedule_required, SqlDbType.TinyInt)
            oColSqlparram.Add("@oldname", oldname)
            If Not Click_Collect_image Is Nothing Then
                oColSqlparram.Add("@Click_Collect_image", Click_Collect_image, SqlDbType.Image)
            End If
            If Not Delivery_image Is Nothing Then
                oColSqlparram.Add("@Delivery_image", Delivery_image, SqlDbType.Image)
            End If
            If Not OrderAtTable_image Is Nothing Then
                oColSqlparram.Add("@OrderAtTable_image", OrderAtTable_image, SqlDbType.Image)
            End If
            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@L_image", L_Image, SqlDbType.Image)
            End If
            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.TinyInt)
            oColSqlparram.Add("@Order_table", Order_table, SqlDbType.TinyInt)
            oColSqlparram.Add("@delivery_charges", delivery_charges, SqlDbType.Float)
            oColSqlparram.Add("@service_charges", service_charges, SqlDbType.Float)
            oColSqlparram.Add("@theme", theme)
            oColSqlparram.Add("@tipAs", tipAs)
            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@L_image", L_Image, SqlDbType.Image)
            End If
            oColSqlparram.Add("@schedule_cnc", schedule_cnc, SqlDbType.TinyInt)
            oColSqlparram.Add("@oldname", oldname)
            oColSqlparram.Add("@future_booking_days", future_booking_days, SqlDbType.Int)

            If Not GraphicViewBackground Is Nothing Then
                oColSqlparram.Add("@GraphicViewBackground", GraphicViewBackground, SqlDbType.Image)
            End If

            If Not GraphicViewTable Is Nothing Then
                oColSqlparram.Add("@GraphicViewTable", GraphicViewTable, SqlDbType.Image)
            End If

            oColSqlparram.Add("@HourlySlot", HourlySlot, SqlDbType.TinyInt)
            oColSqlparram.Add("@CustomPay_id", CustomPay_id)
            oColSqlparram.Add("@CustomPay_secret", CustomPay_secret)
            oColSqlparram.Add("@CustomPay_token", CustomPay_token)
            oColSqlparram.Add("@CustomPay_Base64", CustomPay_Base64)
            oColSqlparram.Add("@CustomPay_url", CustomPay_URL)
            oColSqlparram.Add("@Is_Email_APK", Is_Email_APK)
            oColSqlparram.Add("@Is_GetCovers", Is_GetCovers)

            oColSqlparram.Add("@Table_As_Box", Table_As_Box)

            oColSqlparram.Add("@Client_ID", Client_ID)
            oColSqlparram.Add("@clientsecret", clientsecret)
            oColSqlparram.Add("@redirect_uri", redirect_uri)
            oColSqlparram.Add("@gtway_MerchantID", gtway_MerchantID)
            oColSqlparram.Add("@gtway_StoreID", gtway_StoreID, SqlDbType.Int)
            oColSqlparram.Add("@gtway_LocationID", gtway_LocationID, SqlDbType.Int)
            oColSqlparram.Add("@gtway_StoreName", gtway_StoreName)
            oColSqlparram.Add("@gtway_LocationName", gtway_LocationName)

            oColSqlparram.Add("@GC_Btn_stl", GC_Btn_stl)
            oColSqlparram.Add("@GC_Btn_img_typ", GC_Btn_img_typ)
            oColSqlparram.Add("@GC_Btn_fnt_size", GC_Btn_fnt_size)
            oColSqlparram.Add("@GC_Btn_bkgd_clr", GC_Btn_bkgd_clr)
            oColSqlparram.Add("@GC_Btn_txt_clr", GC_Btn_txt_clr)

            oColSqlparram.Add("@C_Btn_stl", C_Btn_stl)
            oColSqlparram.Add("@C_Btn_img_typ", C_Btn_img_typ)
            oColSqlparram.Add("@C_Btn_fnt_size", C_Btn_fnt_size)
            oColSqlparram.Add("@C_Btn_bkgd_clr", C_Btn_bkgd_clr)
            oColSqlparram.Add("@C_Btn_txt_clr", C_Btn_txt_clr)

            oColSqlparram.Add("@P_Btn_stl", P_Btn_stl)
            oColSqlparram.Add("@P_Btn_img_typ", P_Btn_img_typ)
            oColSqlparram.Add("@P_Btn_fnt_size", P_Btn_fnt_size)
            oColSqlparram.Add("@P_Btn_bkgd_clr", P_Btn_bkgd_clr)
            oColSqlparram.Add("@P_Btn_txt_clr", P_Btn_txt_clr)


            oColSqlparram.Add("@GC_Btn_img_typ_pos", GC_Btn_img_typ_pos)
            oColSqlparram.Add("@C_Btn_img_typ_pos", C_Btn_img_typ_pos)
            oColSqlparram.Add("@P_Btn_img_typ_pos", P_Btn_img_typ_pos)

            oColSqlparram.Add("@login_src_bkgd_clr", login_src_bkgd_clr)
            oColSqlparram.Add("@login_src_login_btn", login_src_login_btn)
            oColSqlparram.Add("@Till_url", Till_url)
            oColSqlparram.Add("@Till_Phn_no", Till_Phn_no)

            If Not POS_logo Is Nothing Then
                oColSqlparram.Add("@POS_logo", POS_logo, SqlDbType.Image)
            End If
            'oColSqlparram.Add("@sunmiSecondScreen", SunmiSecondScreen)
            'oColSqlparram.Add("@SecondScreenImage1", SecondScreenImage1, SqlDbType.Image)
            'oColSqlparram.Add("@SecondScreenImage2", SecondScreenImage2, SqlDbType.Image)
            '  oColSqlparram.Add("@SecondScreenVideo", SecondScreenVideo, SqlDbType.Image)

            'oColSqlparram.Add("@poslite", POSlite)
            'oColSqlparram.Add("@sunmi_video_path", sunmi_video_path)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Location", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@code", code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@till_auto_log_off", till_auto_log_off, SqlDbType.TinyInt)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@cashback", cashback, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_clickcollect", srv_charge_clickcollect, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_kiosk", srv_charge_kiosk, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_order", srv_charge_order, SqlDbType.TinyInt)
            oColSqlparram.Add("@srv_charge_delivery", srv_charge_delivery, SqlDbType.TinyInt)
            oColSqlparram.Add("@min_payment", min_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@is_delivery", is_delivery, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_skipPay", is_skipPay, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_email", is_email, SqlDbType.TinyInt)
            oColSqlparram.Add("@cashflow_id", cashflow_id)
            oColSqlparram.Add("@cashflow_url", cashflow_url)
            oColSqlparram.Add("@cashflow_api_key", cashflow_api_key)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@onlinepayment", onlinepayment)
            oColSqlparram.Add("@websales_check_schedule", websales_check_schedule, SqlDbType.TinyInt)
            oColSqlparram.Add("@start_date", start_date)
            oColSqlparram.Add("@end_date", end_date)
            oColSqlparram.Add("@Email", Email)
            oColSqlparram.Add("@Contact", Contact)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@Body_Color", Body_Color)
            oColSqlparram.Add("@header_reciept", header_reciept)
            oColSqlparram.Add("@message_delivery", message_delivery)
            oColSqlparram.Add("@message_order_table", message_order_table)
            oColSqlparram.Add("@schedule_required", schedule_required, SqlDbType.TinyInt)
            If Not Click_Collect_image Is Nothing Then
                oColSqlparram.Add("@Click_Collect_image", Click_Collect_image, SqlDbType.Image)
            End If
            If Not Delivery_image Is Nothing Then
                oColSqlparram.Add("@Delivery_image", Delivery_image, SqlDbType.Image)
            End If
            If Not OrderAtTable_image Is Nothing Then
                oColSqlparram.Add("@OrderAtTable_image", OrderAtTable_image, SqlDbType.Image)
            End If
            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@L_image", L_Image, SqlDbType.Image)
            End If
            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.TinyInt)
            oColSqlparram.Add("@Order_table", Order_table, SqlDbType.TinyInt)
            oColSqlparram.Add("@delivery_charges", delivery_charges, SqlDbType.Float)
            oColSqlparram.Add("@service_charges", service_charges, SqlDbType.Float)
            oColSqlparram.Add("@theme", theme)
            oColSqlparram.Add("@tipAs", tipAs)
            oColSqlparram.Add("@schedule_cnc", schedule_cnc, SqlDbType.TinyInt)
            oColSqlparram.Add("@oldname", oldname)
            oColSqlparram.Add("@future_booking_days", future_booking_days, SqlDbType.Int)

            If Not GraphicViewBackground Is Nothing Then
                oColSqlparram.Add("@GraphicViewBackground", GraphicViewBackground, SqlDbType.Image)
            End If

            If Not GraphicViewTable Is Nothing Then
                oColSqlparram.Add("@GraphicViewTable", GraphicViewTable, SqlDbType.Image)
            End If

            oColSqlparram.Add("@HourlySlot", HourlySlot, SqlDbType.TinyInt)
            oColSqlparram.Add("@CustomPay_id", CustomPay_id)
            oColSqlparram.Add("@CustomPay_secret", CustomPay_secret)
            oColSqlparram.Add("@CustomPay_token", CustomPay_token)
            oColSqlparram.Add("@CustomPay_Base64", CustomPay_Base64)
            oColSqlparram.Add("@CustomPay_url", CustomPay_URL)
            oColSqlparram.Add("@Is_Email_APK", Is_Email_APK)
            oColSqlparram.Add("@Is_GetCovers", Is_GetCovers)

            oColSqlparram.Add("@Table_As_Box", Table_As_Box)

            oColSqlparram.Add("@Client_ID", Client_ID)
            oColSqlparram.Add("@clientsecret", clientsecret)
            oColSqlparram.Add("@redirect_uri", redirect_uri)
            oColSqlparram.Add("@gtway_MerchantID", gtway_MerchantID)
            oColSqlparram.Add("@gtway_StoreID", gtway_StoreID, SqlDbType.Int)
            oColSqlparram.Add("@gtway_LocationID", gtway_LocationID, SqlDbType.Int)
            oColSqlparram.Add("@gtway_StoreName", gtway_StoreName)
            oColSqlparram.Add("@gtway_LocationName", gtway_LocationName)

            oColSqlparram.Add("@GC_Btn_stl", GC_Btn_stl)
            oColSqlparram.Add("@GC_Btn_img_typ", GC_Btn_img_typ)
            oColSqlparram.Add("@GC_Btn_fnt_size", GC_Btn_fnt_size)
            oColSqlparram.Add("@GC_Btn_bkgd_clr", GC_Btn_bkgd_clr)
            oColSqlparram.Add("@GC_Btn_txt_clr", GC_Btn_txt_clr)

            oColSqlparram.Add("@C_Btn_stl", C_Btn_stl)
            oColSqlparram.Add("@C_Btn_img_typ", C_Btn_img_typ)
            oColSqlparram.Add("@C_Btn_fnt_size", C_Btn_fnt_size)
            oColSqlparram.Add("@C_Btn_bkgd_clr", C_Btn_bkgd_clr)
            oColSqlparram.Add("@C_Btn_txt_clr", C_Btn_txt_clr)

            oColSqlparram.Add("@P_Btn_stl", P_Btn_stl)
            oColSqlparram.Add("@P_Btn_img_typ", P_Btn_img_typ)
            oColSqlparram.Add("@P_Btn_fnt_size", P_Btn_fnt_size)
            oColSqlparram.Add("@P_Btn_bkgd_clr", P_Btn_bkgd_clr)
            oColSqlparram.Add("@P_Btn_txt_clr", P_Btn_txt_clr)


            oColSqlparram.Add("@GC_Btn_img_typ_pos", GC_Btn_img_typ_pos)
            oColSqlparram.Add("@C_Btn_img_typ_pos", C_Btn_img_typ_pos)
            oColSqlparram.Add("@P_Btn_img_typ_pos", P_Btn_img_typ_pos)

            oColSqlparram.Add("@login_src_bkgd_clr", login_src_bkgd_clr)
            oColSqlparram.Add("@login_src_login_btn", login_src_login_btn)
            oColSqlparram.Add("@Till_url", Till_url)
            oColSqlparram.Add("@Till_Phn_no", Till_Phn_no)
            If Not POS_logo Is Nothing Then
                oColSqlparram.Add("@POS_logo", POS_logo, SqlDbType.Image)
            End If
            'oColSqlparram.Add("@sunmiSecondScreen", SunmiSecondScreen)
            'oColSqlparram.Add("@SecondScreenImage1", SecondScreenImage1, SqlDbType.Image)
            'oColSqlparram.Add("@SecondScreenImage2", SecondScreenImage2, SqlDbType.Image)
            'oColSqlparram.Add("@SecondScreenVideo", SecondScreenImage2)


            'oColSqlparram.Add("@poslite", POSlite)
            'oColSqlparram.Add("@sunmi_video_path", sunmi_video_path)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Location", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Location", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Location", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectLocationByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Location_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectLocationByVenue]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Location_By_Venue", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_Merchant() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Location_MerchantID", oColSqlparram)

        Return dtlogin
    End Function
    Public Function ImageDelete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)

            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@L_Image", L_Image, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "E")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Location", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ImageDeleteGraphic() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)

            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@GraphicViewBackground", GraphicViewBackground, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "K")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Location", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ImageDeleteTable() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)

            If Not L_Image Is Nothing Then
                oColSqlparram.Add("@GraphicViewTable", GraphicViewTable, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "J")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Location", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SyncLocationAllTill() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Sync_Location_all_tills", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_url() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Location_url", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_urlforCloud() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        'oColSqlparram.Add("@Date", created_date)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Api_request", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_tsalesCloud() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        oColSqlparram.Add("@Date", created_date.ToString("dd-MMM-yyyy"))
        'oColSqlparram.Add("@modify_date", modify_date.ToString("dd-MMM-yyyy"))
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_t_salesData", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_updateCloud() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        oColSqlparram.Add("@Date", created_date.ToString("dd-MMM-yyyy"))
        oColSqlparram.Add("@machine_id", machine_id.ToString())
        oColSqlparram.Add("@salesID", salesID.ToString())
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Update_t_sales", oColSqlparram)

        Return dtlogin
    End Function


    Public Function CloudApiInsert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@value", cloudValue)
            oColSqlparram.Add("@api_key", cloudKey)
            oColSqlparram.Add("@url", cloudurl)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Api", onlinepayment, SqlDbType.Int)


            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cloud_Api", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function CloudApiupdate() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cloud_id", cloud_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@value", cloudValue)
            oColSqlparram.Add("@api_key", cloudKey)
            oColSqlparram.Add("@url", cloudurl)
            oColSqlparram.Add("@Api", onlinepayment, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cloud_Api", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Function [SelectAllAPI]() As DataTable
    '    Dim ds As New DataSet

    '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '    oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '    Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Cloud_Api", oColSqlparram)

    '    Return dtlogin
    'End Function
    Public Function [SelectAllAPI]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@cloud_id", cloud_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Cloud_Api", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectTRansferd_data]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        oColSqlparram.Add("@timestamp", created_date.ToString("dd-MMM-yyyy"))
        oColSqlparram.Add("@end_timestamp", modify_date.ToString("dd-MMM-yyyy"))
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Select_CloudNetHistory", oColSqlparram)

        Return dtlogin
    End Function

    Public Function DeletecloudApi() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cloud_id", cloud_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)


            oColSqlparram.Add("@Tran_Type", "D")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Cloud_Api", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Historycloudnet() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@url", cloudurl)
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id)
            oColSqlparram.Add("@reading", reading, SqlDbType.Int)
            oColSqlparram.Add("@cassette", cassette, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Api_Key", cloudKey)
            oColSqlparram.Add("@value", cloudValue)
            oColSqlparram.Add("@timestamp", created_date.ToString("dd-MMM-yyyy"))
            oColSqlparram.Add("@store_name", gtway_StoreName)




            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("InsertCloudNetHistory", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

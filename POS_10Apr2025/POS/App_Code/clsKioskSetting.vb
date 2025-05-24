Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI

Public Class Controller_clsKioskSetting
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Setting_id As Integer
    Private _machine_id As Integer
    Private _cmp_id As Integer

    Private _BG_style As Integer
    Private _BG_color As String
    Private _BG_g_clr_1 As String
    Private _BG_g_clr_2 As String
    Private _BG_g_clr_3 As String
    Private _BG_Image As Byte()

    Private _QtyBtn_BG_clr As String
    Private _QtyBtn_Border_clr As String
    Private _QtyBtn_Text_clr As String
    Private _Qty_BG_clr As String
    Private _Qty_Text_clr As String
    Private _Qty_FontSize As String
    Private _Is_Qty_label As Byte

    Private _StaticInfoBtn_BG_clr As String
    Private _StaticInfoBtn_Text_clr As String

    Private _TellMeAbtBtn_BG_clr As String
    Private _TellMeAbtBtn_Border_clr As String
    Private _TellMeAbtBtn_Text_clr As String

    Private _TellMeAbtPopUp_Header_clr As String
    Private _TellMeAbtPopUp_OkBtn_Style As Integer
    Private _TellMeAbtPopUp_OkBtn_clr As String
    Private _TellMeAbtPopUp_OkBtn_g_clr_1 As String
    Private _TellMeAbtPopUp_OkBtn_g_clr_2 As String
    Private _TellMeAbtPopUp_OkBtn_g_clr_3 As String

    Private _ProductName_clr As String
    Private _ProductName_FontSize As String

    Private _ConfirmBtn_Style As Integer
    Private _ConfirmBtn_clr As String
    Private _ConfirmBtn_g_clr_1 As String
    Private _ConfirmBtn_g_clr_2 As String
    Private _ConfirmBtn_g_clr_3 As String
    Private _ConfirmBtn_Border_clr As String
    Private _ConfirmBtn_Text_clr As String

    Private _ip_address As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime

    Private _Header_text As String
    Private _Header_text_clr As String
    Private _payment_text As String
    Private _payment_text_clr As String

    Private objclsKioskSetting As clsKioskSetting

#End Region

#Region "Public Property"

    Public Property Setting_id() As Integer
        Get
            Return _Setting_id
        End Get
        Set(ByVal value As Integer)
            _Setting_id = value
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

    Public Property BG_Image() As Byte()
        Get
            Return _BG_Image
        End Get
        Set(ByVal value As Byte())
            _BG_Image = value
        End Set
    End Property

    Public Property BG_style() As Integer
        Get
            Return _BG_style
        End Get
        Set(ByVal value As Integer)
            _BG_style = value
        End Set
    End Property

    Public Property QtyBtn_Border_clr() As String
        Get
            Return _QtyBtn_Border_clr
        End Get
        Set(ByVal value As String)
            _QtyBtn_Border_clr = value
        End Set
    End Property

    Public Property QtyBtn_BG_clr() As String
        Get
            Return _QtyBtn_BG_clr
        End Get
        Set(ByVal value As String)
            _QtyBtn_BG_clr = value
        End Set
    End Property

    Public Property BG_g_clr_3() As String
        Get
            Return _BG_g_clr_3
        End Get
        Set(ByVal value As String)
            _BG_g_clr_3 = value
        End Set
    End Property

    Public Property BG_g_clr_2() As String
        Get
            Return _BG_g_clr_2
        End Get
        Set(ByVal value As String)
            _BG_g_clr_2 = value
        End Set
    End Property

    Public Property BG_g_clr_1() As String
        Get
            Return _BG_g_clr_1
        End Get
        Set(ByVal value As String)
            _BG_g_clr_1 = value
        End Set
    End Property

    Public Property BG_color() As String
        Get
            Return _BG_color
        End Get
        Set(ByVal value As String)
            _BG_color = value
        End Set
    End Property

    Public Property Qty_FontSize() As String
        Get
            Return _Qty_FontSize
        End Get
        Set(ByVal value As String)
            _Qty_FontSize = value
        End Set
    End Property

    Public Property Qty_Text_clr() As String
        Get
            Return _Qty_Text_clr
        End Get
        Set(ByVal value As String)
            _Qty_Text_clr = value
        End Set
    End Property

    Public Property Qty_BG_clr() As String
        Get
            Return _Qty_BG_clr
        End Get
        Set(ByVal value As String)
            _Qty_BG_clr = value
        End Set
    End Property

    Public Property QtyBtn_Text_clr() As String
        Get
            Return _QtyBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _QtyBtn_Text_clr = value
        End Set
    End Property

    Public Property StaticInfoBtn_BG_clr() As String
        Get
            Return _StaticInfoBtn_BG_clr
        End Get
        Set(ByVal value As String)
            _StaticInfoBtn_BG_clr = value
        End Set
    End Property

    Public Property StaticInfoBtn_Text_clr() As String
        Get
            Return _StaticInfoBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _StaticInfoBtn_Text_clr = value
        End Set
    End Property

    Public Property TellMeAbtBtn_BG_clr() As String
        Get
            Return _TellMeAbtBtn_BG_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtBtn_BG_clr = value
        End Set
    End Property

    Public Property TellMeAbtBtn_Border_clr() As String
        Get
            Return _TellMeAbtBtn_Border_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtBtn_Border_clr = value
        End Set
    End Property

    Public Property TellMeAbtBtn_Text_clr() As String
        Get
            Return _TellMeAbtBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtBtn_Text_clr = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_Style() As Integer
        Get
            Return _TellMeAbtPopUp_OkBtn_Style
        End Get
        Set(ByVal value As Integer)
            _TellMeAbtPopUp_OkBtn_Style = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_Header_clr() As String
        Get
            Return _TellMeAbtPopUp_Header_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_Header_clr = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_g_clr_1() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_g_clr_1
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_g_clr_1 = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_clr() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_clr = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_g_clr_3() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_g_clr_3
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_g_clr_3 = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_g_clr_2() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_g_clr_2
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_g_clr_2 = value
        End Set
    End Property

    Public Property ProductName_clr() As String
        Get
            Return _ProductName_clr
        End Get
        Set(ByVal value As String)
            _ProductName_clr = value
        End Set
    End Property

    Public Property ProductName_FontSize() As String
        Get
            Return _ProductName_FontSize
        End Get
        Set(ByVal value As String)
            _ProductName_FontSize = value
        End Set
    End Property

    Public Property ConfirmBtn_Style() As Integer
        Get
            Return _ConfirmBtn_Style
        End Get
        Set(ByVal value As Integer)
            _ConfirmBtn_Style = value
        End Set
    End Property

    Public Property ConfirmBtn_clr() As String
        Get
            Return _ConfirmBtn_clr
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_clr = value
        End Set
    End Property

    Public Property ConfirmBtn_g_clr_1() As String
        Get
            Return _ConfirmBtn_g_clr_1
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_g_clr_1 = value
        End Set
    End Property

    Public Property ConfirmBtn_g_clr_2() As String
        Get
            Return _ConfirmBtn_g_clr_2
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_g_clr_2 = value
        End Set
    End Property

    Public Property ConfirmBtn_g_clr_3() As String
        Get
            Return _ConfirmBtn_g_clr_3
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_g_clr_3 = value
        End Set
    End Property

    Public Property ConfirmBtn_Border_clr() As String
        Get
            Return _ConfirmBtn_Border_clr
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_Border_clr = value
        End Set
    End Property

    Public Property ConfirmBtn_Text_clr() As String
        Get
            Return _ConfirmBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_Text_clr = value
        End Set
    End Property

    Public Property Is_Qty_label() As Byte
        Get
            Return _Is_Qty_label
        End Get
        Set(ByVal value As Byte)
            _Is_Qty_label = value
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
    Public Property Header_text() As String
        Get
            Return _Header_text
        End Get
        Set(ByVal value As String)
            _Header_text = value
        End Set
    End Property
    Public Property Header_text_clr() As String
        Get
            Return _Header_text_clr
        End Get
        Set(ByVal value As String)
            _Header_text_clr = value
        End Set
    End Property

    Public Property payment_text() As String
        Get
            Return _payment_text
        End Get
        Set(ByVal value As String)
            _payment_text = value
        End Set
    End Property

    Public Property payment_text_clr() As String
        Get
            Return _payment_text_clr
        End Get
        Set(ByVal value As String)
            _payment_text_clr = value
        End Set
    End Property


#End Region

    Public Function Insert() As DataTable
        Try
            objclsKioskSetting = New clsKioskSetting()
            objclsKioskSetting.Setting_id = Setting_id
            objclsKioskSetting.machine_id = machine_id
            objclsKioskSetting.cmp_id = cmp_id
            objclsKioskSetting.BG_style = BG_style
            objclsKioskSetting.BG_color = BG_color
            objclsKioskSetting.BG_g_clr_1 = BG_g_clr_1
            objclsKioskSetting.BG_g_clr_2 = BG_g_clr_2
            objclsKioskSetting.BG_g_clr_3 = BG_g_clr_3
            objclsKioskSetting.BG_Image = BG_Image
            objclsKioskSetting.QtyBtn_BG_clr = QtyBtn_BG_clr
            objclsKioskSetting.QtyBtn_Border_clr = QtyBtn_Border_clr
            objclsKioskSetting.QtyBtn_Text_clr = QtyBtn_Text_clr
            objclsKioskSetting.Qty_BG_clr = Qty_BG_clr
            objclsKioskSetting.Qty_Text_clr = Qty_Text_clr
            objclsKioskSetting.Qty_FontSize = Qty_FontSize
            objclsKioskSetting.Is_Qty_label = Is_Qty_label
            objclsKioskSetting.StaticInfoBtn_BG_clr = StaticInfoBtn_BG_clr
            objclsKioskSetting.StaticInfoBtn_Text_clr = StaticInfoBtn_Text_clr

            objclsKioskSetting.TellMeAbtBtn_BG_clr = TellMeAbtBtn_BG_clr
            objclsKioskSetting.TellMeAbtBtn_Border_clr = TellMeAbtBtn_Border_clr
            objclsKioskSetting.TellMeAbtBtn_Text_clr = TellMeAbtBtn_Text_clr

            objclsKioskSetting.TellMeAbtPopUp_Header_clr = TellMeAbtPopUp_Header_clr
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_Style = TellMeAbtPopUp_OkBtn_Style
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_clr = TellMeAbtPopUp_OkBtn_clr
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_1 = TellMeAbtPopUp_OkBtn_g_clr_1
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_2 = TellMeAbtPopUp_OkBtn_g_clr_2
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_3 = TellMeAbtPopUp_OkBtn_g_clr_3

            objclsKioskSetting.ProductName_clr = ProductName_clr
            objclsKioskSetting.ProductName_FontSize = ProductName_FontSize

            objclsKioskSetting.ConfirmBtn_Style = ConfirmBtn_Style
            objclsKioskSetting.ConfirmBtn_clr = ConfirmBtn_clr
            objclsKioskSetting.ConfirmBtn_g_clr_1 = ConfirmBtn_g_clr_1
            objclsKioskSetting.ConfirmBtn_g_clr_2 = ConfirmBtn_g_clr_2
            objclsKioskSetting.ConfirmBtn_g_clr_3 = ConfirmBtn_g_clr_3
            objclsKioskSetting.ConfirmBtn_Border_clr = ConfirmBtn_Border_clr
            objclsKioskSetting.ConfirmBtn_Text_clr = ConfirmBtn_Text_clr

            objclsKioskSetting.ip_address = ip_address

            objclsKioskSetting.Header_text = Header_text
            objclsKioskSetting.Header_text_clr = Header_text_clr
            objclsKioskSetting.payment_text = payment_text
            objclsKioskSetting.payment_text_clr = payment_text_clr


            Dim dt As New DataTable
            dt = objclsKioskSetting.Insert()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As DataTable
        Try
            objclsKioskSetting = New clsKioskSetting()
            objclsKioskSetting.Setting_id = Setting_id
            objclsKioskSetting.machine_id = machine_id
            objclsKioskSetting.cmp_id = cmp_id
            objclsKioskSetting.BG_style = BG_style
            objclsKioskSetting.BG_color = BG_color
            objclsKioskSetting.BG_g_clr_1 = BG_g_clr_1
            objclsKioskSetting.BG_g_clr_2 = BG_g_clr_2
            objclsKioskSetting.BG_g_clr_3 = BG_g_clr_3
            objclsKioskSetting.BG_Image = BG_Image
            objclsKioskSetting.QtyBtn_BG_clr = QtyBtn_BG_clr
            objclsKioskSetting.QtyBtn_Border_clr = QtyBtn_Border_clr
            objclsKioskSetting.QtyBtn_Text_clr = QtyBtn_Text_clr
            objclsKioskSetting.Qty_BG_clr = Qty_BG_clr
            objclsKioskSetting.Qty_Text_clr = Qty_Text_clr
            objclsKioskSetting.Qty_FontSize = Qty_FontSize
            objclsKioskSetting.Is_Qty_label = Is_Qty_label
            objclsKioskSetting.StaticInfoBtn_BG_clr = StaticInfoBtn_BG_clr
            objclsKioskSetting.StaticInfoBtn_Text_clr = StaticInfoBtn_Text_clr

            objclsKioskSetting.TellMeAbtBtn_BG_clr = TellMeAbtBtn_BG_clr
            objclsKioskSetting.TellMeAbtBtn_Border_clr = TellMeAbtBtn_Border_clr
            objclsKioskSetting.TellMeAbtBtn_Text_clr = TellMeAbtBtn_Text_clr

            objclsKioskSetting.TellMeAbtPopUp_Header_clr = TellMeAbtPopUp_Header_clr
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_Style = TellMeAbtPopUp_OkBtn_Style
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_clr = TellMeAbtPopUp_OkBtn_clr
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_1 = TellMeAbtPopUp_OkBtn_g_clr_1
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_2 = TellMeAbtPopUp_OkBtn_g_clr_2
            objclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_3 = TellMeAbtPopUp_OkBtn_g_clr_3

            objclsKioskSetting.ProductName_clr = ProductName_clr
            objclsKioskSetting.ProductName_FontSize = ProductName_FontSize

            objclsKioskSetting.ConfirmBtn_Style = ConfirmBtn_Style
            objclsKioskSetting.ConfirmBtn_clr = ConfirmBtn_clr
            objclsKioskSetting.ConfirmBtn_g_clr_1 = ConfirmBtn_g_clr_1
            objclsKioskSetting.ConfirmBtn_g_clr_2 = ConfirmBtn_g_clr_2
            objclsKioskSetting.ConfirmBtn_g_clr_3 = ConfirmBtn_g_clr_3
            objclsKioskSetting.ConfirmBtn_Border_clr = ConfirmBtn_Border_clr
            objclsKioskSetting.ConfirmBtn_Text_clr = ConfirmBtn_Text_clr

            objclsKioskSetting.ip_address = ip_address
            objclsKioskSetting.Header_text = Header_text
            objclsKioskSetting.Header_text_clr = Header_text_clr
            objclsKioskSetting.payment_text = payment_text
            objclsKioskSetting.payment_text_clr = payment_text_clr

            Dim dt As New DataTable
            dt = objclsKioskSetting.Update()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsKioskSetting = New clsKioskSetting()
            objclsKioskSetting.machine_id = machine_id
            objclsKioskSetting.cmp_id = cmp_id
            dt = objclsKioskSetting.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsKioskSetting
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Setting_id As Integer
    Private _machine_id As Integer
    Private _cmp_id As Integer
    Private _BG_style As Integer
    Private _BG_color As String
    Private _BG_g_clr_1 As String
    Private _BG_g_clr_2 As String
    Private _BG_g_clr_3 As String
    Private _BG_Image As Byte()

    Private _QtyBtn_BG_clr As String
    Private _QtyBtn_Border_clr As String
    Private _QtyBtn_Text_clr As String
    Private _Qty_BG_clr As String
    Private _Qty_Text_clr As String
    Private _Qty_FontSize As String
    Private _Is_Qty_label As Byte

    Private _StaticInfoBtn_BG_clr As String
    Private _StaticInfoBtn_Text_clr As String

    Private _TellMeAbtBtn_BG_clr As String
    Private _TellMeAbtBtn_Border_clr As String
    Private _TellMeAbtBtn_Text_clr As String

    Private _TellMeAbtPopUp_Header_clr As String
    Private _TellMeAbtPopUp_OkBtn_Style As Integer
    Private _TellMeAbtPopUp_OkBtn_clr As String
    Private _TellMeAbtPopUp_OkBtn_g_clr_1 As String
    Private _TellMeAbtPopUp_OkBtn_g_clr_2 As String
    Private _TellMeAbtPopUp_OkBtn_g_clr_3 As String

    Private _ProductName_clr As String
    Private _ProductName_FontSize As String

    Private _ConfirmBtn_Style As Integer
    Private _ConfirmBtn_clr As String
    Private _ConfirmBtn_g_clr_1 As String
    Private _ConfirmBtn_g_clr_2 As String
    Private _ConfirmBtn_g_clr_3 As String
    Private _ConfirmBtn_Border_clr As String
    Private _ConfirmBtn_Text_clr As String

    Private _ip_address As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime

    Private _Header_text As String
    Private _Header_text_clr As String
    Private _payment_text As String
    Private _payment_text_clr As String

#End Region

#Region "Public Property"

    Public Property Header_text() As String
        Get
            Return _Header_text
        End Get
        Set(ByVal value As String)
            _Header_text = value
        End Set
    End Property
    Public Property Header_text_clr() As String
        Get
            Return _Header_text_clr
        End Get
        Set(ByVal value As String)
            _Header_text_clr = value
        End Set
    End Property

    Public Property payment_text() As String
        Get
            Return _payment_text
        End Get
        Set(ByVal value As String)
            _payment_text = value
        End Set
    End Property

    Public Property payment_text_clr() As String
        Get
            Return _payment_text_clr
        End Get
        Set(ByVal value As String)
            _payment_text_clr = value
        End Set
    End Property

    Public Property Setting_id() As Integer
        Get
            Return _Setting_id
        End Get
        Set(ByVal value As Integer)
            _Setting_id = value
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

    Public Property BG_Image() As Byte()
        Get
            Return _BG_Image
        End Get
        Set(ByVal value As Byte())
            _BG_Image = value
        End Set
    End Property

    Public Property BG_style() As Integer
        Get
            Return _BG_style
        End Get
        Set(ByVal value As Integer)
            _BG_style = value
        End Set
    End Property

    Public Property QtyBtn_Border_clr() As String
        Get
            Return _QtyBtn_Border_clr
        End Get
        Set(ByVal value As String)
            _QtyBtn_Border_clr = value
        End Set
    End Property

    Public Property QtyBtn_BG_clr() As String
        Get
            Return _QtyBtn_BG_clr
        End Get
        Set(ByVal value As String)
            _QtyBtn_BG_clr = value
        End Set
    End Property

    Public Property BG_g_clr_3() As String
        Get
            Return _BG_g_clr_3
        End Get
        Set(ByVal value As String)
            _BG_g_clr_3 = value
        End Set
    End Property

    Public Property BG_g_clr_2() As String
        Get
            Return _BG_g_clr_2
        End Get
        Set(ByVal value As String)
            _BG_g_clr_2 = value
        End Set
    End Property

    Public Property BG_g_clr_1() As String
        Get
            Return _BG_g_clr_1
        End Get
        Set(ByVal value As String)
            _BG_g_clr_1 = value
        End Set
    End Property

    Public Property BG_color() As String
        Get
            Return _BG_color
        End Get
        Set(ByVal value As String)
            _BG_color = value
        End Set
    End Property

    Public Property Qty_FontSize() As String
        Get
            Return _Qty_FontSize
        End Get
        Set(ByVal value As String)
            _Qty_FontSize = value
        End Set
    End Property

    Public Property Qty_Text_clr() As String
        Get
            Return _Qty_Text_clr
        End Get
        Set(ByVal value As String)
            _Qty_Text_clr = value
        End Set
    End Property

    Public Property Qty_BG_clr() As String
        Get
            Return _Qty_BG_clr
        End Get
        Set(ByVal value As String)
            _Qty_BG_clr = value
        End Set
    End Property

    Public Property QtyBtn_Text_clr() As String
        Get
            Return _QtyBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _QtyBtn_Text_clr = value
        End Set
    End Property

    Public Property StaticInfoBtn_BG_clr() As String
        Get
            Return _StaticInfoBtn_BG_clr
        End Get
        Set(ByVal value As String)
            _StaticInfoBtn_BG_clr = value
        End Set
    End Property

    Public Property StaticInfoBtn_Text_clr() As String
        Get
            Return _StaticInfoBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _StaticInfoBtn_Text_clr = value
        End Set
    End Property

    Public Property TellMeAbtBtn_BG_clr() As String
        Get
            Return _TellMeAbtBtn_BG_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtBtn_BG_clr = value
        End Set
    End Property

    Public Property TellMeAbtBtn_Border_clr() As String
        Get
            Return _TellMeAbtBtn_Border_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtBtn_Border_clr = value
        End Set
    End Property

    Public Property TellMeAbtBtn_Text_clr() As String
        Get
            Return _TellMeAbtBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtBtn_Text_clr = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_Style() As Integer
        Get
            Return _TellMeAbtPopUp_OkBtn_Style
        End Get
        Set(ByVal value As Integer)
            _TellMeAbtPopUp_OkBtn_Style = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_Header_clr() As String
        Get
            Return _TellMeAbtPopUp_Header_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_Header_clr = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_g_clr_1() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_g_clr_1
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_g_clr_1 = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_clr() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_clr
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_clr = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_g_clr_3() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_g_clr_3
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_g_clr_3 = value
        End Set
    End Property

    Public Property TellMeAbtPopUp_OkBtn_g_clr_2() As String
        Get
            Return _TellMeAbtPopUp_OkBtn_g_clr_2
        End Get
        Set(ByVal value As String)
            _TellMeAbtPopUp_OkBtn_g_clr_2 = value
        End Set
    End Property

    Public Property ProductName_clr() As String
        Get
            Return _ProductName_clr
        End Get
        Set(ByVal value As String)
            _ProductName_clr = value
        End Set
    End Property

    Public Property ProductName_FontSize() As String
        Get
            Return _ProductName_FontSize
        End Get
        Set(ByVal value As String)
            _ProductName_FontSize = value
        End Set
    End Property

    Public Property ConfirmBtn_Style() As Integer
        Get
            Return _ConfirmBtn_Style
        End Get
        Set(ByVal value As Integer)
            _ConfirmBtn_Style = value
        End Set
    End Property

    Public Property ConfirmBtn_clr() As String
        Get
            Return _ConfirmBtn_clr
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_clr = value
        End Set
    End Property

    Public Property ConfirmBtn_g_clr_1() As String
        Get
            Return _ConfirmBtn_g_clr_1
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_g_clr_1 = value
        End Set
    End Property

    Public Property ConfirmBtn_g_clr_2() As String
        Get
            Return _ConfirmBtn_g_clr_2
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_g_clr_2 = value
        End Set
    End Property

    Public Property ConfirmBtn_g_clr_3() As String
        Get
            Return _ConfirmBtn_g_clr_3
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_g_clr_3 = value
        End Set
    End Property

    Public Property ConfirmBtn_Border_clr() As String
        Get
            Return _ConfirmBtn_Border_clr
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_Border_clr = value
        End Set
    End Property

    Public Property ConfirmBtn_Text_clr() As String
        Get
            Return _ConfirmBtn_Text_clr
        End Get
        Set(ByVal value As String)
            _ConfirmBtn_Text_clr = value
        End Set
    End Property

    Public Property Is_Qty_label() As Byte
        Get
            Return _Is_Qty_label
        End Get
        Set(ByVal value As Byte)
            _Is_Qty_label = value
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

#End Region

    Public Function Insert() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Setting_id", Setting_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@BG_style", BG_style, SqlDbType.Int)
            oColSqlparram.Add("@BG_color", BG_color)
            oColSqlparram.Add("@BG_g_clr_1", BG_g_clr_1)
            oColSqlparram.Add("@BG_g_clr_2", BG_g_clr_2)
            oColSqlparram.Add("@BG_g_clr_3", BG_g_clr_3)
            oColSqlparram.Add("@QtyBtn_BG_clr", QtyBtn_BG_clr)
            oColSqlparram.Add("@QtyBtn_Border_clr", QtyBtn_Border_clr)
            oColSqlparram.Add("@QtyBtn_Text_clr", QtyBtn_Text_clr)
            oColSqlparram.Add("@Qty_BG_clr", Qty_BG_clr)
            oColSqlparram.Add("@Qty_Text_clr", Qty_Text_clr)
            oColSqlparram.Add("@Qty_FontSize", Qty_FontSize)
            oColSqlparram.Add("@Is_Qty_label", Is_Qty_label)
            oColSqlparram.Add("@StaticInfoBtn_BG_clr", StaticInfoBtn_BG_clr)
            oColSqlparram.Add("@StaticInfoBtn_Text_clr", StaticInfoBtn_Text_clr)
            oColSqlparram.Add("@TellMeAbtBtn_BG_clr", TellMeAbtBtn_BG_clr)
            oColSqlparram.Add("@TellMeAbtBtn_Border_clr", TellMeAbtBtn_Border_clr)
            oColSqlparram.Add("@TellMeAbtBtn_Text_clr", TellMeAbtBtn_Text_clr)
            oColSqlparram.Add("@TellMeAbtPopUp_Header_clr", TellMeAbtPopUp_Header_clr)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_Style", TellMeAbtPopUp_OkBtn_Style, SqlDbType.Int)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_clr", TellMeAbtPopUp_OkBtn_clr)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_g_clr_1", TellMeAbtPopUp_OkBtn_g_clr_1)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_g_clr_2", TellMeAbtPopUp_OkBtn_g_clr_2)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_g_clr_3", TellMeAbtPopUp_OkBtn_g_clr_3)
            oColSqlparram.Add("@ProductName_clr", ProductName_clr)
            oColSqlparram.Add("@ProductName_FontSize", ProductName_FontSize)

            oColSqlparram.Add("@ConfirmBtn_Style", ConfirmBtn_Style, SqlDbType.Int)
            oColSqlparram.Add("@ConfirmBtn_clr", ConfirmBtn_clr)
            oColSqlparram.Add("@ConfirmBtn_g_clr_1", ConfirmBtn_g_clr_1)
            oColSqlparram.Add("@ConfirmBtn_g_clr_2", ConfirmBtn_g_clr_2)
            oColSqlparram.Add("@ConfirmBtn_g_clr_3", ConfirmBtn_g_clr_3)
            oColSqlparram.Add("@ConfirmBtn_Border_clr", ConfirmBtn_Border_clr)
            oColSqlparram.Add("@ConfirmBtn_Text_clr", ConfirmBtn_Text_clr)

            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@Header_text", Header_text)
            oColSqlparram.Add("@Header_text_clr", Header_text_clr)
            oColSqlparram.Add("@payment_text", payment_text)
            oColSqlparram.Add("@payment_text_clr", payment_text_clr)


            oColSqlparram.Add("@Tran_Type", "I")

            If Not BG_Image Is Nothing Then
                oColSqlparram.Add("@BG_Image", BG_Image, SqlDbType.Image)
            End If
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_KioskSetting", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Setting_id", Setting_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@BG_style", BG_style, SqlDbType.Int)
            oColSqlparram.Add("@BG_color", BG_color)
            oColSqlparram.Add("@BG_g_clr_1", BG_g_clr_1)
            oColSqlparram.Add("@BG_g_clr_2", BG_g_clr_2)
            oColSqlparram.Add("@BG_g_clr_3", BG_g_clr_3)
            oColSqlparram.Add("@QtyBtn_BG_clr", QtyBtn_BG_clr)
            oColSqlparram.Add("@QtyBtn_Border_clr", QtyBtn_Border_clr)
            oColSqlparram.Add("@QtyBtn_Text_clr", QtyBtn_Text_clr)
            oColSqlparram.Add("@Qty_BG_clr", Qty_BG_clr)
            oColSqlparram.Add("@Qty_Text_clr", Qty_Text_clr)
            oColSqlparram.Add("@Qty_FontSize", Qty_FontSize)
            oColSqlparram.Add("@Is_Qty_label", Is_Qty_label)
            oColSqlparram.Add("@StaticInfoBtn_BG_clr", StaticInfoBtn_BG_clr)
            oColSqlparram.Add("@StaticInfoBtn_Text_clr", StaticInfoBtn_Text_clr)
            oColSqlparram.Add("@TellMeAbtBtn_BG_clr", TellMeAbtBtn_BG_clr)
            oColSqlparram.Add("@TellMeAbtBtn_Border_clr", TellMeAbtBtn_Border_clr)
            oColSqlparram.Add("@TellMeAbtBtn_Text_clr", TellMeAbtBtn_Text_clr)
            oColSqlparram.Add("@TellMeAbtPopUp_Header_clr", TellMeAbtPopUp_Header_clr)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_Style", TellMeAbtPopUp_OkBtn_Style, SqlDbType.Int)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_clr", TellMeAbtPopUp_OkBtn_clr)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_g_clr_1", TellMeAbtPopUp_OkBtn_g_clr_1)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_g_clr_2", TellMeAbtPopUp_OkBtn_g_clr_2)
            oColSqlparram.Add("@TellMeAbtPopUp_OkBtn_g_clr_3", TellMeAbtPopUp_OkBtn_g_clr_3)
            oColSqlparram.Add("@ProductName_clr", ProductName_clr)
            oColSqlparram.Add("@ProductName_FontSize", ProductName_FontSize)
            oColSqlparram.Add("@ConfirmBtn_Style", ConfirmBtn_Style, SqlDbType.Int)
            oColSqlparram.Add("@ConfirmBtn_clr", ConfirmBtn_clr)
            oColSqlparram.Add("@ConfirmBtn_g_clr_1", ConfirmBtn_g_clr_1)
            oColSqlparram.Add("@ConfirmBtn_g_clr_2", ConfirmBtn_g_clr_2)
            oColSqlparram.Add("@ConfirmBtn_g_clr_3", ConfirmBtn_g_clr_3)
            oColSqlparram.Add("@ConfirmBtn_Border_clr", ConfirmBtn_Border_clr)
            oColSqlparram.Add("@ConfirmBtn_Text_clr", ConfirmBtn_Text_clr)

            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@Header_text", Header_text)
            oColSqlparram.Add("@Header_text_clr", Header_text_clr)
            oColSqlparram.Add("@payment_text", payment_text)
            oColSqlparram.Add("@payment_text_clr", payment_text_clr)
            oColSqlparram.Add("@Tran_Type", "U")

            If Not BG_Image Is Nothing Then
                oColSqlparram.Add("@BG_Image", BG_Image, SqlDbType.Image)
            End If
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_KioskSetting", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Machine_KioskSetting", oColSqlparram)

        Return dtlogin
    End Function

End Class

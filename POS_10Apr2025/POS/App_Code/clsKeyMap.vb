Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsKey_Map
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _key_map_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _description As String
    Private _shorting_no As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _form_name As String
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _Location_id As Integer
    Private _venue_id As Integer
    Private _Keymap_size As String

    Private _key_map_detail_id As Integer
    Private _Product_Group_id As Integer
    Private _Product_id As Integer
    Private _Size_id As Integer
    Private _BG_Color As String
    Private _FG_Color As String
    Private _matrix As String

    Private _Font_Color As String
    Private _Display_Name As String

    Private _ButtonStyle As String
    Private _ImageOption As String
    Private _ImageStyle As String
    Private _maincategory_id As Integer

    Private objclsKey_Map As clsKey_Map
#End Region

#Region "Public Property"

    Public Property maincategory_id() As Integer
        Get
            Return _maincategory_id
        End Get
        Set(ByVal value As Integer)
            _maincategory_id = value
        End Set
    End Property

    Public Property ButtonStyle() As String
        Get
            Return _ButtonStyle
        End Get
        Set(ByVal value As String)
            _ButtonStyle = value
        End Set
    End Property

    Public Property ImageOption() As String
        Get
            Return _ImageOption
        End Get
        Set(ByVal value As String)
            _ImageOption = value
        End Set
    End Property

    Public Property ImageStyle() As String
        Get
            Return _ImageStyle
        End Get
        Set(ByVal value As String)
            _ImageStyle = value
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
    Public Property key_map_id() As Integer
        Get
            Return _key_map_id
        End Get
        Set(ByVal value As Integer)
            _key_map_id = value
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
    Public Property description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property shorting_no() As Integer
        Get
            Return _shorting_no
        End Get
        Set(ByVal value As Integer)
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
        Set(value As String)
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

    Public Property Location_id() As Integer
        Get
            Return _Location_id
        End Get
        Set(ByVal value As Integer)
            _Location_id = value
        End Set
    End Property

    Public Property Keymap_size() As String
        Get
            Return _Keymap_size
        End Get
        Set(ByVal value As String)
            _Keymap_size = value
        End Set
    End Property

    Public Property key_map_detail_id() As Integer
        Get
            Return _key_map_detail_id
        End Get
        Set(ByVal value As Integer)
            _key_map_detail_id = value
        End Set
    End Property

    Public Property Product_Group_id() As Integer
        Get
            Return _Product_Group_id
        End Get
        Set(ByVal value As Integer)
            _Product_Group_id = value
        End Set
    End Property

    Public Property Product_id() As Integer
        Get
            Return _Product_id
        End Get
        Set(ByVal value As Integer)
            _Product_id = value
        End Set
    End Property

    Public Property Size_id() As Integer
        Get
            Return _Size_id
        End Get
        Set(ByVal value As Integer)
            _Size_id = value
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

    Public Property FG_Color() As String
        Get
            Return _FG_Color
        End Get
        Set(ByVal value As String)
            _FG_Color = value
        End Set
    End Property

    Public Property matrix() As String
        Get
            Return _matrix
        End Get
        Set(ByVal value As String)
            _matrix = value
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

    Public Property Display_Name() As String
        Get
            Return _Display_Name
        End Get
        Set(ByVal value As String)
            _Display_Name = value
        End Set
    End Property



#End Region
#Region "Function"
    Public Function Insert() As Integer
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            objclsKey_Map.name = name
            objclsKey_Map.description = description
            objclsKey_Map.shorting_no = shorting_no
            objclsKey_Map.ip_address = ip_address
            objclsKey_Map.login_id = login_id
            objclsKey_Map.machine_id = machine_id
            objclsKey_Map.venue_id = venue_id
            objclsKey_Map.Location_id = Location_id
            objclsKey_Map.Keymap_size = Keymap_size
            objclsKey_Map.is_active = is_active
            objclsKey_Map.Font_Color = Font_Color
            objclsKey_Map.BG_Color = BG_Color
            objclsKey_Map.Display_Name = Display_Name

            objclsKey_Map.ButtonStyle = ButtonStyle
            objclsKey_Map.ImageOption = ImageOption
            objclsKey_Map.ImageStyle = ImageStyle

            Dim r As Integer
            r = objclsKey_Map.Insert()
            Return r

            'If objclsKey_Map.Insert() Then
            '    Return True
            'End If
            'Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            objclsKey_Map.name = name
            objclsKey_Map.description = description
            objclsKey_Map.shorting_no = shorting_no
            objclsKey_Map.ip_address = ip_address
            objclsKey_Map.login_id = login_id
            objclsKey_Map.machine_id = machine_id
            objclsKey_Map.venue_id = venue_id
            objclsKey_Map.Location_id = Location_id
            objclsKey_Map.Keymap_size = Keymap_size
            objclsKey_Map.is_active = is_active
            objclsKey_Map.Font_Color = Font_Color
            objclsKey_Map.BG_Color = BG_Color
            objclsKey_Map.Display_Name = Display_Name

            objclsKey_Map.ButtonStyle = ButtonStyle
            objclsKey_Map.ImageOption = ImageOption
            objclsKey_Map.ImageStyle = ImageStyle


            If objclsKey_Map.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            objclsKey_Map.name = name
            objclsKey_Map.description = description
            objclsKey_Map.shorting_no = shorting_no
            objclsKey_Map.ip_address = ip_address
            objclsKey_Map.login_id = login_id
            objclsKey_Map.machine_id = machine_id
            objclsKey_Map.venue_id = venue_id
            objclsKey_Map.Location_id = Location_id
            objclsKey_Map.Tran_Type = Tran_Type
            objclsKey_Map.is_active = is_active
            objclsKey_Map.Keymap_size = Keymap_size
            objclsKey_Map.Font_Color = Font_Color
            objclsKey_Map.BG_Color = BG_Color
            objclsKey_Map.Display_Name = Display_Name

            objclsKey_Map.ButtonStyle = ButtonStyle
            objclsKey_Map.ImageOption = ImageOption
            objclsKey_Map.ImageStyle = ImageStyle


            If objclsKey_Map.Delete() Then
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
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            dt = objclsKey_Map.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SelectTills() As DataTable
        Dim dt As DataTable
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            dt = objclsKey_Map.SelectTills()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsKey_Map = New clsKey_Map()
            'objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            dt = objclsKey_Map.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectproductbyLocation]() As DataTable
        Dim dt As DataTable
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.Location_id = Location_id
            objclsKey_Map.Product_id = Product_id
            dt = objclsKey_Map.[SelectproductbyLocation]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectKeyMapByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.cmp_id = cmp_id
            dt = objclsKey_Map.[SelectKeyMapByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select1]() As DataTable
        Dim dt As DataTable
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.cmp_id = cmp_id
            objclsKey_Map.form_name = form_name
            dt = objclsKey_Map.[Select1]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_KeyMapDetails() As Integer
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_detail_id = key_map_detail_id
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.Product_Group_id = Product_Group_id
            objclsKey_Map.Product_id = Product_id
            objclsKey_Map.Size_id = Size_id
            objclsKey_Map.cmp_id = cmp_id
            objclsKey_Map.login_id = login_id
            objclsKey_Map.ip_address = ip_address
            objclsKey_Map.is_active = is_active
            objclsKey_Map.BG_Color = BG_Color
            objclsKey_Map.FG_Color = FG_Color
            objclsKey_Map.matrix = matrix
            objclsKey_Map.maincategory_id = maincategory_id
            If objclsKey_Map.Insert_KeyMapDetails() Then
                Return True
            End If
            Return False


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectKeyMap_Details]() As DataTable
        Dim dt As DataTable
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            dt = objclsKey_Map.[SelectKeyMap_Details]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_Edit() As Boolean
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.key_map_id = key_map_id
            objclsKey_Map.cmp_id = cmp_id
            If objclsKey_Map.Delete_Edit() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Copy_Function() As Boolean
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.cmp_id = cmp_id
            objclsKey_Map.key_map_id = key_map_id
            If objclsKey_Map.Copy_Function() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SyncKeymapAllTill() As Boolean
        Try
            objclsKey_Map = New clsKey_Map()
            objclsKey_Map.cmp_id = cmp_id
            If objclsKey_Map.SyncKeymapAllTill() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

End Class

Public Class clsKey_Map
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _key_map_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _description As String
    Private _shorting_no As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _form_name As String
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _Location_id As Integer
    Private _venue_id As Integer
    Private _Keymap_size As String

    Private _key_map_detail_id As Integer
    Private _Product_Group_id As Integer
    Private _Product_id As Integer
    Private _Size_id As Integer
    Private _BG_Color As String
    Private _FG_Color As String
    Private _matrix As String

    Private _Font_Color As String
    Private _Display_Name As String

    Private _ButtonStyle As String
    Private _ImageOption As String
    Private _ImageStyle As String
    Private _maincategory_id As Integer

    Private objclsKey_Map As clsKey_Map
#End Region

#Region "Public Property"


    Public Property maincategory_id() As Integer
        Get
            Return _maincategory_id
        End Get
        Set(ByVal value As Integer)
            _maincategory_id = value
        End Set
    End Property

    Public Property ButtonStyle() As String
        Get
            Return _ButtonStyle
        End Get
        Set(ByVal value As String)
            _ButtonStyle = value
        End Set
    End Property

    Public Property ImageOption() As String
        Get
            Return _ImageOption
        End Get
        Set(ByVal value As String)
            _ImageOption = value
        End Set
    End Property

    Public Property ImageStyle() As String
        Get
            Return _ImageStyle
        End Get
        Set(ByVal value As String)
            _ImageStyle = value
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
    Public Property key_map_id() As Integer
        Get
            Return _key_map_id
        End Get
        Set(ByVal value As Integer)
            _key_map_id = value
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
    Public Property description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property shorting_no() As Integer
        Get
            Return _shorting_no
        End Get
        Set(ByVal value As Integer)
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
        Set(value As String)
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

    Public Property Location_id() As Integer
        Get
            Return _Location_id
        End Get
        Set(ByVal value As Integer)
            _Location_id = value
        End Set
    End Property

    Public Property Keymap_size() As String
        Get
            Return _Keymap_size
        End Get
        Set(ByVal value As String)
            _Keymap_size = value
        End Set
    End Property

    Public Property key_map_detail_id() As Integer
        Get
            Return _key_map_detail_id
        End Get
        Set(ByVal value As Integer)
            _key_map_detail_id = value
        End Set
    End Property

    Public Property Product_Group_id() As Integer
        Get
            Return _Product_Group_id
        End Get
        Set(ByVal value As Integer)
            _Product_Group_id = value
        End Set
    End Property

    Public Property Product_id() As Integer
        Get
            Return _Product_id
        End Get
        Set(ByVal value As Integer)
            _Product_id = value
        End Set
    End Property

    Public Property Size_id() As Integer
        Get
            Return _Size_id
        End Get
        Set(ByVal value As Integer)
            _Size_id = value
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

    Public Property FG_Color() As String
        Get
            Return _FG_Color
        End Get
        Set(ByVal value As String)
            _FG_Color = value
        End Set
    End Property

    Public Property matrix() As String
        Get
            Return _matrix
        End Get
        Set(ByVal value As String)
            _matrix = value
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

    Public Property Display_Name() As String
        Get
            Return _Display_Name
        End Get
        Set(ByVal value As String)
            _Display_Name = value
        End Set
    End Property
#End Region
#Region "Function"

    Public Function Insert() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Keymap_size", Keymap_size)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@Display_Name", Display_Name)

            oColSqlparram.Add("@ButtonStyle", ButtonStyle)
            oColSqlparram.Add("@ImageOption", ImageOption)
            oColSqlparram.Add("@ImageStyle", ImageStyle)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Key_Map", oColSqlparram)
            Return dtlogin.Rows(0)("key_map_id")
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Key_Map", oColSqlparram)
            'Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Keymap_size", Keymap_size)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@Display_Name", Display_Name)

            oColSqlparram.Add("@ButtonStyle", ButtonStyle)
            oColSqlparram.Add("@ImageOption", ImageOption)
            oColSqlparram.Add("@ImageStyle", ImageStyle)

            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Key_Map", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Keymap_size", Keymap_size)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@Display_Name", Display_Name)

            oColSqlparram.Add("@ButtonStyle", ButtonStyle)
            oColSqlparram.Add("@ImageOption", ImageOption)
            oColSqlparram.Add("@ImageStyle", ImageStyle)

            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Key_Map", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Key_Map", oColSqlparram)

        Return dtlogin
    End Function
    Public Function SelectTills() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Key_Map_Tills", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Key_Map", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectproductbyLocation]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        oColSqlparram.Add("@Product_id", Product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_by_Location_KeyMap", oColSqlparram)

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
    Public Function [SelectKeyMapByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_KeyMap_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Insert_KeyMapDetails() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@key_map_detail_id", key_map_detail_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@Product_Group_id", Product_Group_id, SqlDbType.Int)
            oColSqlparram.Add("@Product_id", Product_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_id", Size_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@FG_Color", FG_Color)
            oColSqlparram.Add("@matrix", matrix)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@maincategory_id", maincategory_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_KeyMapDetails", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectKeyMap_Details]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Key_Map_Details", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Delete_Edit() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Delete_Key_Map_Details_Edit", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Copy_Function() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Key_Map_Copy", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SyncKeymapAllTill() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Key_Map_Sync_all_tills", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region



End Class


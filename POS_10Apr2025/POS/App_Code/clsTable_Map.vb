Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsTable_Map
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Table_map_id As Integer
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
    Private _Tablemap_size As String

    Private _table_map_detail_id As Integer
    Private _table_id As Integer
    Private _Product_id As Integer
    Private _Size_id As Integer
    Private _BG_Color As String
    Private _FG_Color As String
    Private _matrix As String

    Private _Font_Color As String
    Private _Display_Name As String
    Private objclsTable_Map As clsTable_Map
#End Region

#Region "Public Property"
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property Table_map_id() As Integer
        Get
            Return _Table_map_id
        End Get
        Set(ByVal value As Integer)
            _Table_map_id = value
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

    Public Property Tablemap_size() As String
        Get
            Return _Tablemap_size
        End Get
        Set(ByVal value As String)
            _Tablemap_size = value
        End Set
    End Property

    Public Property table_map_detail_id() As Integer
        Get
            Return _table_map_detail_id
        End Get
        Set(ByVal value As Integer)
            _table_map_detail_id = value
        End Set
    End Property

    Public Property table_id() As Integer
        Get
            Return _table_id
        End Get
        Set(ByVal value As Integer)
            _table_id = value
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
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.cmp_id = cmp_id
            objclsTable_Map.Display_Name = Display_Name
            objclsTable_Map.Tablemap_size = Tablemap_size
            objclsTable_Map.ip_address = ip_address
            objclsTable_Map.login_id = login_id
            objclsTable_Map.is_active = is_active
            objclsTable_Map.Font_Color = Font_Color
            objclsTable_Map.BG_Color = BG_Color

            Dim r As Integer
            r = objclsTable_Map.Insert()
            Return r

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.cmp_id = cmp_id
            objclsTable_Map.Display_Name = Display_Name
            objclsTable_Map.Tablemap_size = Tablemap_size
            objclsTable_Map.ip_address = ip_address
            objclsTable_Map.login_id = login_id
            objclsTable_Map.is_active = is_active
            objclsTable_Map.Font_Color = Font_Color
            objclsTable_Map.BG_Color = BG_Color


            If objclsTable_Map.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.cmp_id = cmp_id
            objclsTable_Map.Display_Name = Display_Name
            objclsTable_Map.Tablemap_size = Tablemap_size
            objclsTable_Map.ip_address = ip_address
            objclsTable_Map.login_id = login_id
            objclsTable_Map.is_active = is_active
            objclsTable_Map.Font_Color = Font_Color
            objclsTable_Map.BG_Color = BG_Color


            If objclsTable_Map.Delete() Then
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
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.cmp_id = cmp_id
            dt = objclsTable_Map.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsTable_Map = New clsTable_Map()
            'objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.cmp_id = cmp_id
            dt = objclsTable_Map.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_tableName]() As DataTable
        Dim dt As DataTable
        Try
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.cmp_id = cmp_id
            dt = objclsTable_Map.[Select_tableName]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Insert_TableMapDetails() As Integer
        Try
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.table_map_detail_id = table_map_detail_id
            objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.table_id = table_id
            'objclsTable_Map.cmp_id = cmp_id
            'objclsTable_Map.login_id = login_id
            'objclsTable_Map.ip_address = ip_address
            objclsTable_Map.is_active = is_active
            objclsTable_Map.BG_Color = BG_Color
            objclsTable_Map.FG_Color = FG_Color
            objclsTable_Map.matrix = matrix
            If objclsTable_Map.Insert_TableMapDetails() Then
                Return True
            End If
            Return False


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectTableMap_Details]() As DataTable
        Dim dt As DataTable
        Try
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.cmp_id = cmp_id
            dt = objclsTable_Map.[SelectTableMap_Details]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_Edit() As Boolean
        Try
            objclsTable_Map = New clsTable_Map()
            objclsTable_Map.Table_map_id = Table_map_id
            objclsTable_Map.cmp_id = cmp_id
            If objclsTable_Map.Delete_Edit() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region
End Class
Public Class clsTable_Map
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Table_map_id As Integer
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
    Private _Tablemap_size As String

    Private _table_map_detail_id As Integer
    Private _table_id As Integer
    Private _Product_id As Integer
    Private _Size_id As Integer
    Private _BG_Color As String
    Private _FG_Color As String
    Private _matrix As String

    Private _Font_Color As String
    Private _Display_Name As String

    Private objclsTable_Map As clsTable_Map
#End Region

#Region "Public Property"
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property Table_map_id() As Integer
        Get
            Return _Table_map_id
        End Get
        Set(ByVal value As Integer)
            _Table_map_id = value
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

    Public Property Tablemap_size() As String
        Get
            Return _Tablemap_size
        End Get
        Set(ByVal value As String)
            _Tablemap_size = value
        End Set
    End Property

    Public Property table_map_detail_id() As Integer
        Get
            Return _table_map_detail_id
        End Get
        Set(ByVal value As Integer)
            _table_map_detail_id = value
        End Set
    End Property

    Public Property table_id() As Integer
        Get
            Return _table_id
        End Get
        Set(ByVal value As Integer)
            _table_id = value
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
            oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Display_Name", Display_Name)
            oColSqlparram.Add("@Tablemap_size", Tablemap_size)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")


            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Table_Map", oColSqlparram)
            Return dtlogin.Rows(0)("Table_map_id")
            'Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Table_Map", oColSqlparram)
            'Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Display_Name", Display_Name)
            oColSqlparram.Add("@Tablemap_size", Tablemap_size)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Table_Map", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Display_Name", Display_Name)
            oColSqlparram.Add("@Tablemap_size", Tablemap_size)
            oColSqlparram.Add("@Font_Color", Font_Color)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Table_Map", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Table_Map", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Table_Map", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_tableName]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_TableName", oColSqlparram)

        Return dtlogin
    End Function


    Public Function Insert_TableMapDetails() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_map_detail_id", table_map_detail_id, SqlDbType.Int)
            oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
            oColSqlparram.Add("@table_id", table_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@BG_Color", BG_Color)
            oColSqlparram.Add("@FG_Color", FG_Color)
            oColSqlparram.Add("@matrix", matrix)
            'oColSqlparram.Add("@ip_address", ip_address)
            'oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_TableMapDetails", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectTableMap_Details]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Table_Map_Details", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Delete_Edit() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Table_map_id", Table_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Delete_Table_Map_Details_Edit", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


#End Region
End Class


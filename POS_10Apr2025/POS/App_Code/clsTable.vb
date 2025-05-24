Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsTable
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _table_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _min_cover As Integer
    Private _max_cover As Integer
    Private _is_active As Byte
    Private _is_open As Byte
    Private _shorting_no As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _form_name As String
    Private _machine_id As Integer
    Private _BookingScheduleID As Integer
    Private _Datebooking As System.DateTime
    Private _curr As String
    Private _shape_id As Integer
    Private _location_Id As Integer
    Private objclsTable As clsTable
#End Region

#Region "Public Property"
    Public Property shape_id() As Integer
        Get
            Return _shape_id
        End Get
        Set(ByVal value As Integer)
            _shape_id = value
        End Set
    End Property

    Public Property Location_id() As Integer
        Get
            Return _location_Id
        End Get
        Set(ByVal value As Integer)
            _location_Id = value
        End Set
    End Property
    Public Property BookingScheduleID() As Integer
        Get
            Return _BookingScheduleID
        End Get
        Set(ByVal value As Integer)
            _BookingScheduleID = value
        End Set
    End Property
    Public Property Datebooking() As DateTime
        Get
            Return _Datebooking
        End Get
        Set(ByVal value As DateTime)
            _Datebooking = value
        End Set
    End Property
    Public Property curr() As String
        Get
            Return _curr
        End Get
        Set(ByVal value As String)
            _curr = value
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

    Public Property shorting_no() As Integer
        Get
            Return _shorting_no
        End Get
        Set(ByVal value As Integer)
            _shorting_no = value
        End Set
    End Property
    Public Property min_cover() As Integer
        Get
            Return _min_cover
        End Get
        Set(ByVal value As Integer)
            _min_cover = value
        End Set
    End Property
    Public Property max_cover() As Integer
        Get
            Return _max_cover
        End Get
        Set(ByVal value As Integer)
            _max_cover = value
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
    Public Property is_open() As Byte
        Get
            Return _is_open
        End Get
        Set(ByVal value As Byte)
            _is_open = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
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
#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            objclsTable = New clsTable()
            objclsTable.table_id = table_id
            objclsTable.cmp_id = cmp_id
            objclsTable.name = name
            objclsTable.min_cover = min_cover
            objclsTable.max_cover = max_cover
            objclsTable.is_active = is_active
            objclsTable.is_open = is_open
            objclsTable.shorting_no = shorting_no
            objclsTable.ip_address = ip_address
            objclsTable.login_id = login_id
            objclsTable.machine_id = machine_id
            objclsTable.Location_id = Location_id
            If objclsTable.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsTable = New clsTable()
            objclsTable.table_id = table_id
            objclsTable.cmp_id = cmp_id
            objclsTable.name = name
            objclsTable.min_cover = min_cover
            objclsTable.max_cover = max_cover
            objclsTable.is_active = is_active
            objclsTable.is_open = is_open
            objclsTable.shorting_no = shorting_no
            objclsTable.ip_address = ip_address
            objclsTable.login_id = login_id
            objclsTable.machine_id = machine_id
            objclsTable.Location_id = Location_id
            If objclsTable.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsTable = New clsTable()
            objclsTable.table_id = table_id
            objclsTable.cmp_id = cmp_id
            objclsTable.name = name
            objclsTable.min_cover = min_cover
            objclsTable.max_cover = max_cover
            objclsTable.is_active = is_active
            objclsTable.is_open = is_open
            objclsTable.shorting_no = shorting_no
            objclsTable.ip_address = ip_address
            objclsTable.login_id = login_id
            objclsTable.Tran_Type = Tran_Type
            objclsTable.machine_id = machine_id
            objclsTable.Location_id = Location_id
            If objclsTable.Delete() Then
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
            objclsTable = New clsTable()
            objclsTable.table_id = table_id
            objclsTable.cmp_id = cmp_id
            dt = objclsTable.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsTable = New clsTable()
            'objclsTable.table_id = table_id
            objclsTable.cmp_id = cmp_id
            dt = objclsTable.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function BindDIv() As DataSet
        Dim ds As DataSet
        Try
            objclsTable = New clsTable()

            objclsTable.BookingScheduleID = BookingScheduleID
            objclsTable.Datebooking = Datebooking
            objclsTable.curr = curr
            objclsTable.Location_id = Location_id
            ds = objclsTable.BindDiv()
            Return ds
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetBackgroundColour() As DataSet
        Dim ds As DataSet
        Try
            objclsTable = New clsTable()

            objclsTable.BookingScheduleID = BookingScheduleID

            ds = objclsTable.BackGroundColor()
            Return ds
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetShapes() As DataSet
        Dim ds As DataSet
        Try
            objclsTable = New clsTable()

            ds = objclsTable.GetShapes()
            Return ds
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetShapeDetails() As DataSet
        Dim ds As DataSet
        Try
            objclsTable = New clsTable()

            objclsTable.shape_id = shape_id

            ds = objclsTable.GetShapesDetails()
            Return ds
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select1]() As DataTable
        Dim dt As DataTable
        Try
            objclsTable = New clsTable()
            objclsTable.cmp_id = cmp_id
            objclsTable.form_name = form_name
            dt = objclsTable.[Select1]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

End Class

Public Class clsTable
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _table_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _min_cover As Integer
    Private _max_cover As Integer
    Private _is_active As Byte
    Private _is_open As Byte
    Private _shorting_no As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _form_name As String
    Private _machine_id As Integer
    Private _BookingScheduleID As Integer
    Private _Datebooking As System.DateTime
    Private _curr As String
    Private _shape_id As Integer
    Private _location_id As Integer
    Private objclsTable As clsTable
#End Region

#Region "Public Property"
    Public Property Location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property shape_id() As Integer
        Get
            Return _shape_id
        End Get
        Set(ByVal value As Integer)
            _shape_id = value
        End Set
    End Property
    Public Property BookingScheduleID() As Integer
        Get
            Return _BookingScheduleID
        End Get
        Set(ByVal value As Integer)
            _BookingScheduleID = value
        End Set
    End Property
    Public Property Datebooking() As DateTime
        Get
            Return _Datebooking
        End Get
        Set(ByVal value As DateTime)
            _Datebooking = value
        End Set
    End Property
    Public Property curr() As String
        Get
            Return _curr
        End Get
        Set(ByVal value As String)
            _curr = value
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

    Public Property shorting_no() As Integer
        Get
            Return _shorting_no
        End Get
        Set(ByVal value As Integer)
            _shorting_no = value
        End Set
    End Property
    Public Property min_cover() As Integer
        Get
            Return _min_cover
        End Get
        Set(ByVal value As Integer)
            _min_cover = value
        End Set
    End Property
    Public Property max_cover() As Integer
        Get
            Return _max_cover
        End Get
        Set(ByVal value As Integer)
            _max_cover = value
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
    Public Property is_open() As Byte
        Get
            Return _is_open
        End Get
        Set(ByVal value As Byte)
            _is_open = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
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
#End Region
#Region "Function"

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_id", table_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@min_cover", min_cover, SqlDbType.Int)
            oColSqlparram.Add("@max_cover", max_cover, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_open", is_open, SqlDbType.TinyInt)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Table", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_id", table_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@min_cover", min_cover, SqlDbType.Int)
            oColSqlparram.Add("@max_cover", max_cover, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_open", is_open, SqlDbType.TinyInt)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Table", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@table_id", table_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@min_cover", min_cover, SqlDbType.Int)
            oColSqlparram.Add("@max_cover", max_cover, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_open", is_open, SqlDbType.TinyInt)
            oColSqlparram.Add("@shorting_no", shorting_no)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Table", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@table_id", table_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Table", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Table", oColSqlparram)

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

    Public Function BindDiv() As DataSet
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@BookingScheduleID", BookingScheduleID, SqlDbType.Int)
        oColSqlparram.Add("@Date", Datebooking, SqlDbType.DateTime)
        oColSqlparram.Add("@Cur_Sym", curr)
        oColSqlparram.Add("@Location_id", Location_id, SqlDbType.Int)
        Dim dtlogin As DataSet = oClsDal.GetdatasetSp("Get_Table_Div", oColSqlparram, "Get_Table_Div")



        Return dtlogin
    End Function
    Public Function BackGroundColor() As DataSet
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@BookingScheduleID", BookingScheduleID, SqlDbType.Int)

        Dim dtlogin As DataSet = oClsDal.GetdatasetSp("Get_BackgroundColour", oColSqlparram, "Get_BackgroundColour")



        Return dtlogin
    End Function

    Public Function GetShapes() As DataSet
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataSet = oClsDal.GetdatasetSp("Get_Shapes", oColSqlparram, "Get_Shapes")



        Return dtlogin
    End Function

    Public Function GetShapesDetails() As DataSet
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Shape_id", shape_id, SqlDbType.Int)

        Dim dtlogin As DataSet = oClsDal.GetdatasetSp("Get_Shapes_Details", oColSqlparram, "Get_Shapes_Details")



        Return dtlogin
    End Function
#End Region
End Class


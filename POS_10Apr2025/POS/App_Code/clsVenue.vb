Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsVenue
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _venue_id As Integer
    Private _cmp_id As Integer
    Private _venue_name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _mac_id As String
    Private _sorting_no As Integer
    Private _Tran_Type As String
    Private _form_name As String
    Private _print_receipt As Byte
    Private _print_duplicate_receipt As Byte
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _login_id As Integer
    Private _date_time As String
    Private _group_by As Integer
    Private _consile_date As Integer
    Private _group_by_with As Integer
    Private _RoleVenue As String

    Private objclsVenue As clsVenue
#End Region

#Region "Public Property"

    Public Property RoleVenue() As String
        Get
            Return _RoleVenue
        End Get
        Set(ByVal value As String)
            _RoleVenue = value
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

    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property mac_id() As String
        Get
            Return _mac_id
        End Get
        Set(ByVal value As String)
            _mac_id = value
        End Set
    End Property
    Public Property venue_name() As String
        Get
            Return _venue_name
        End Get
        Set(ByVal value As String)
            _venue_name = value
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
    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
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
    Public Property print_receipt() As String
        Get
            Return _print_receipt
        End Get
        Set(ByVal value As String)
            _print_receipt = value
        End Set
    End Property
    Public Property print_duplicate_receipt() As String
        Get
            Return _print_duplicate_receipt
        End Get
        Set(ByVal value As String)
            _print_duplicate_receipt = value
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

    Public Property date_time() As String
        Get
            Return _date_time
        End Get
        Set(ByVal value As String)
            _date_time = value
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
            objclsVenue = New clsVenue()
            objclsVenue.venue_name = venue_name
            objclsVenue.description = description
            objclsVenue.sorting_no = sorting_no
            objclsVenue.cmp_id = cmp_id
            objclsVenue.mac_id = mac_id
            objclsVenue.modify_date = modify_date
            objclsVenue.created_date = created_date
            objclsVenue.print_receipt = print_receipt
            objclsVenue.print_duplicate_receipt = print_duplicate_receipt
            objclsVenue.machine_id = machine_id
            objclsVenue.is_active = is_active
            objclsVenue.login_id = login_id
            objclsVenue.date_time = date_time
            objclsVenue.group_by_with = group_by_with
            objclsVenue.group_by = group_by
            objclsVenue.consile_date = consile_date
            If objclsVenue.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsVenue = New clsVenue()
            objclsVenue.venue_id = venue_id
            objclsVenue.venue_name = venue_name
            objclsVenue.description = description
            objclsVenue.sorting_no = sorting_no
            objclsVenue.cmp_id = cmp_id
            objclsVenue.mac_id = mac_id
            objclsVenue.modify_date = modify_date
            objclsVenue.print_receipt = print_receipt
            objclsVenue.print_duplicate_receipt = print_duplicate_receipt
            objclsVenue.machine_id = machine_id
            objclsVenue.is_active = is_active
            objclsVenue.login_id = login_id
            objclsVenue.date_time = date_time
            objclsVenue.group_by_with = group_by_with
            objclsVenue.group_by = group_by
            objclsVenue.consile_date = consile_date
            If objclsVenue.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsVenue = New clsVenue()
            objclsVenue.venue_id = venue_id
            objclsVenue.venue_name = venue_name
            objclsVenue.description = description
            objclsVenue.sorting_no = sorting_no
            objclsVenue.cmp_id = cmp_id
            objclsVenue.mac_id = mac_id
            objclsVenue.Tran_Type = Tran_Type
            objclsVenue.print_receipt = print_receipt
            objclsVenue.print_duplicate_receipt = print_duplicate_receipt
            objclsVenue.machine_id = machine_id
            objclsVenue.is_active = is_active
            objclsVenue.login_id = login_id
            objclsVenue.date_time = date_time
            objclsVenue.group_by_with = group_by_with
            objclsVenue.group_by = group_by
            objclsVenue.consile_date = consile_date
            If objclsVenue.Delete() Then
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
            objclsVenue = New clsVenue()
            objclsVenue.venue_id = venue_id
            objclsVenue.cmp_id = cmp_id
            dt = objclsVenue.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsVenue = New clsVenue()
            objclsVenue.cmp_id = cmp_id
            dt = objclsVenue.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select1]() As DataTable
        Dim dt As DataTable
        Try
            objclsVenue = New clsVenue()
            objclsVenue.cmp_id = cmp_id
            objclsVenue.form_name = form_name
            dt = objclsVenue.[Select1]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectVenueByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsVenue = New clsVenue()
            objclsVenue.cmp_id = cmp_id
            dt = objclsVenue.[SelectVenueByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectVenue]() As DataTable
        Dim dt As DataTable
        Try
            objclsVenue = New clsVenue()
            objclsVenue.cmp_id = cmp_id
            dt = objclsVenue.[SelectVenue]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectVenueByRoleVenue() As DataTable
        Dim dt As DataTable
        Try
            objclsVenue = New clsVenue()
            objclsVenue.cmp_id = cmp_id
            objclsVenue.RoleVenue = RoleVenue
            dt = objclsVenue.SelectVenueByRoleVenue()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsVenue
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _venue_id As Integer
    Private _cmp_id As Integer
    Private _venue_name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _mac_id As String
    Private _sorting_no As Integer
    Private _Tran_Type As String
    Private _form_name As String
    Private _print_receipt As Byte
    Private _print_duplicate_receipt As Byte
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _login_id As Integer
    Private _date_time As String
    Private _group_by As Integer
    Private _consile_date As Integer
    Private _group_by_with As Integer
    Private _RoleVenue As String

    Private objclsVenue As clsVenue
#End Region

#Region "Public Property"

    Public Property RoleVenue() As String
        Get
            Return _RoleVenue
        End Get
        Set(ByVal value As String)
            _RoleVenue = value
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

    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property mac_id() As String
        Get
            Return _mac_id
        End Get
        Set(ByVal value As String)
            _mac_id = value
        End Set
    End Property
    Public Property venue_name() As String
        Get
            Return _venue_name
        End Get
        Set(ByVal value As String)
            _venue_name = value
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
    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
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
    Public Property print_receipt() As String
        Get
            Return _print_receipt
        End Get
        Set(ByVal value As String)
            _print_receipt = value
        End Set
    End Property
    Public Property print_duplicate_receipt() As String
        Get
            Return _print_duplicate_receipt
        End Get
        Set(ByVal value As String)
            _print_duplicate_receipt = value
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

    Public Property date_time() As String
        Get
            Return _date_time
        End Get
        Set(ByVal value As String)
            _date_time = value
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
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_name", venue_name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
            oColSqlparram.Add("@mac_id", mac_id)
            oColSqlparram.Add("@print_receipt", print_receipt, SqlDbType.TinyInt)
            oColSqlparram.Add("@print_duplicate_receipt", print_duplicate_receipt, SqlDbType.TinyInt)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@date_time", date_time, SqlDbType.NVarChar)
            oColSqlparram.Add("@group_by", group_by, SqlDbType.TinyInt)
            oColSqlparram.Add("@consile_date", consile_date, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by_with", group_by_with, SqlDbType.TinyInt)

            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Venue", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_name", venue_name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
            oColSqlparram.Add("@mac_id", mac_id)
            oColSqlparram.Add("@print_receipt", print_receipt, SqlDbType.TinyInt)
            oColSqlparram.Add("@print_duplicate_receipt", print_duplicate_receipt, SqlDbType.TinyInt)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@date_time", date_time, SqlDbType.NVarChar)
            oColSqlparram.Add("@group_by", group_by, SqlDbType.TinyInt)
            oColSqlparram.Add("@consile_date", consile_date, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by_with", group_by_with, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Venue", oColSqlparram)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@venue_name", venue_name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
            oColSqlparram.Add("@mac_id", mac_id)
            oColSqlparram.Add("@print_receipt", print_receipt, SqlDbType.TinyInt)
            oColSqlparram.Add("@print_duplicate_receipt", print_duplicate_receipt, SqlDbType.TinyInt)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@date_time", date_time, SqlDbType.NVarChar)
            oColSqlparram.Add("@group_by", group_by, SqlDbType.TinyInt)
            oColSqlparram.Add("@consile_date", consile_date, SqlDbType.TinyInt)
            oColSqlparram.Add("@group_by_with", group_by_with, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Venue", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Venue", oColSqlparram)
        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Venue", oColSqlparram)
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

    Public Function SelectVenueByRoleVenue() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@RoleVenue", RoleVenue)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Venue_By_RoleVenue", oColSqlparram)
        Return dtlogin
    End Function

    Public Function [SelectVenueByCmp]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Venue_By_Cmp", oColSqlparram)
        Return dtlogin
    End Function

    Public Function [SelectVenue]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Venue_By_Cmp_active", oColSqlparram)
        Return dtlogin
    End Function
End Class

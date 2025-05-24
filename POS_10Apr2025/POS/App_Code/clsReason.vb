Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading


Public Class Controller_clsReason
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _reason_id As Integer
    Private _key_map_id As Integer
    Private _cmp_id As Integer
    Private _reason As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _Tran_Type As String
    Private _sorting_no As Integer
    Private _form_name As String

    Private objclsreason As clsreason
#End Region

#Region "Public Property"
    Public Property Reason_id() As Integer
        Get
            Return _reason_id
        End Get
        Set(ByVal value As Integer)
            _reason_id = value
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
    Public Property Reason() As String
        Get
            Return _reason
        End Get
        Set(ByVal value As String)
            _reason = value
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
    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
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
#End Region

    'Public Function Insert() As Boolean
    '    Try
    '        objclsreason = New clsCategory()
    '        objclsreason.Category_id = Category_id
    '        objclsreason.key_map_id = key_map_id
    '        objclsreason.cmp_id = cmp_id
    '        objclsreason.name = name
    '        objclsreason.description = description
    '        objclsreason.Is_active = Is_active
    '        objclsreason.Ip_address = Ip_address
    '        objclsreason.login_id = login_id
    '        objclsreason.machine_id = machine_id
    '        objclsreason.sorting_no = sorting_no
    '        If objclsreason.Insert() Then
    '            Return True
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Insert() As DataTable
        Try
            objclsreason = New clsreason()
            objclsreason.Reason_id = Reason_id
            objclsreason.key_map_id = key_map_id
            objclsreason.cmp_id = cmp_id
            objclsreason.Reason = Reason
            objclsreason.description = description
            objclsreason.Is_active = Is_active
            objclsreason.Ip_address = Ip_address
            objclsreason.login_id = login_id
            objclsreason.machine_id = machine_id
            objclsreason.sorting_no = sorting_no
            objclsreason.Tran_Type = Tran_Type
            Dim dt As New DataTable
            dt = objclsreason.Insert()
            Return dt


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function Update() As Boolean
    '    Try
    '        objclsreason = New clsCategory()
    '        objclsreason.Category_id = Category_id
    '        objclsreason.key_map_id = key_map_id
    '        objclsreason.cmp_id = cmp_id
    '        objclsreason.name = name
    '        objclsreason.description = description
    '        objclsreason.Is_active = Is_active
    '        objclsreason.Ip_address = Ip_address
    '        objclsreason.login_id = login_id
    '        objclsreason.machine_id = machine_id
    '        objclsreason.sorting_no = sorting_no
    '        If objclsreason.Update() Then
    '            Return True
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Update() As DataTable
        Try
            objclsreason = New clsreason()
            objclsreason.Reason_id = Reason_id
            objclsreason.key_map_id = key_map_id
            objclsreason.cmp_id = cmp_id
            objclsreason.Reason = Reason
            objclsreason.description = description
            objclsreason.Is_active = Is_active
            objclsreason.Ip_address = Ip_address
            objclsreason.login_id = login_id
            objclsreason.machine_id = machine_id
            objclsreason.sorting_no = sorting_no
            objclsreason.Tran_Type = Tran_Type
            Dim dt As New DataTable
            dt = objclsreason.Update()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try

            objclsreason = New clsreason()
            objclsreason.Reason_id = Reason_id
            objclsreason.key_map_id = key_map_id
            objclsreason.cmp_id = cmp_id
            objclsreason.Reason = Reason
            objclsreason.description = description
            objclsreason.Is_active = Is_active
            objclsreason.Ip_address = Ip_address
            objclsreason.login_id = login_id
            objclsreason.machine_id = machine_id
            objclsreason.sorting_no = sorting_no
            objclsreason.Tran_Type = Tran_Type
            If objclsreason.Delete() Then
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
            objclsreason = New clsreason()
            objclsreason.Reason_id = Reason_id
            objclsreason.cmp_id = cmp_id
            dt = objclsreason.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsreason = New clsreason()
            'objclsreason.Category_id = Category_id
            objclsreason.cmp_id = cmp_id
            dt = objclsreason.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function [SelectCategoryByCmp]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsreason = New clsCategory()
    '        objclsreason.cmp_id = cmp_id
    '        dt = objclsreason.[SelectCategoryByCmp]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    'Public Function [Select1]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsreason = New clsCategory()
    '        objclsreason.cmp_id = cmp_id
    '        objclsreason.form_name = form_name
    '        dt = objclsreason.[Select1]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    'Public Function [SelectCategoryByCmpALL]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsreason = New clsCategory()
    '        objclsreason.cmp_id = cmp_id
    '        dt = objclsreason.[SelectCategoryByCmpALL]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
End Class

Public Class clsreason
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


#Region "Public Variables"
    Private _Reason_id As Integer
    Private _key_map_id As Integer
    Private _cmp_id As Integer
    Private _Reason As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _Tran_Type As String
    Private _sorting_no As Integer
    Private _form_name As String

    Private objclsreason As clsreason
#End Region

#Region "Public Property"
    Public Property Reason_id() As Integer
        Get
            Return _Reason_id
        End Get
        Set(ByVal value As Integer)
            _Reason_id = value
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
    Public Property Reason() As String
        Get
            Return _Reason
        End Get
        Set(ByVal value As String)
            _Reason = value
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
    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
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
#End Region

    'Public Function Insert() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
    '        oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@name", name)
    '        oColSqlparram.Add("@description", description)
    '        oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@Ip_address", Ip_address)
    '        oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
    '        oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
    '        oColSqlparram.Add("@Tran_Type", "I")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Category", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Insert() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@reason_id", Reason_id, SqlDbType.Int)

            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@reason", Reason)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
         
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Reason", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function Update() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
    '        oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@name", name)
    '        oColSqlparram.Add("@description", description)
    '        oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@Ip_address", Ip_address)
    '        oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
    '        oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
    '        oColSqlparram.Add("@Tran_Type", "U")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Category", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Update() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@reason_id", Reason_id, SqlDbType.Int)

            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@reason", Reason)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
         
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Reason", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@reason_id", Reason_id, SqlDbType.Int)

            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@reason", Reason)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
          
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Reason", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@reason_id", Reason_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_reason", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_reason", oColSqlparram)

        Return dtlogin
    End Function

    'Public Function [SelectCategoryByCmp]() As DataTable
    '    Dim ds As New DataSet

    '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '    oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '    Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Category_By_Cmp", oColSqlparram)

    '    Return dtlogin
    'End Function
    'Public Function [Select1]() As DataTable
    '    Dim ds As New DataSet
    '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '    oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '    oColSqlparram.Add("@form_name", form_name)
    '    Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Autogenerate_Sorting_No_Master", oColSqlparram)

    '    Return dtlogin
    'End Function
    'Public Function [SelectCategoryByCmpALL]() As DataTable
    '    Dim ds As New DataSet

    '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '    oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '    Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Category_By_Cmp_all", oColSqlparram)

    '    Return dtlogin
    'End Function
End Class
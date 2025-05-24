Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsFunctionType
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _function_type_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _is_panel As Byte
    Private objclsFunctonType As clsFunctionType
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

    Public Property is_panel() As Byte
        Get
            Return _is_panel
        End Get
        Set(ByVal value As Byte)
            _is_panel = value
        End Set
    End Property

    Public Property function_type_id() As Integer
        Get
            Return _function_type_id
        End Get
        Set(ByVal value As Integer)
            _function_type_id = value
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
#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            objclsFunctonType = New clsFunctionType()
            objclsFunctonType.function_type_id = function_type_id
            objclsFunctonType.cmp_id = cmp_id
            objclsFunctonType.name = name
            objclsFunctonType.Ip_address = Ip_address
            objclsFunctonType.login_id = login_id
            objclsFunctonType.machine_id = machine_id
            objclsFunctonType.is_active = is_active
            objclsFunctonType.is_panel = is_panel
            If objclsFunctonType.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsFunctonType = New clsFunctionType()
            objclsFunctonType.function_type_id = function_type_id
            objclsFunctonType.cmp_id = cmp_id
            objclsFunctonType.name = name
            objclsFunctonType.Ip_address = Ip_address
            objclsFunctonType.login_id = login_id
            objclsFunctonType.machine_id = machine_id
            objclsFunctonType.is_active = is_active
            objclsFunctonType.is_panel = is_panel
            If objclsFunctonType.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsFunctonType = New clsFunctionType()
            objclsFunctonType.function_type_id = function_type_id
            objclsFunctonType.cmp_id = cmp_id
            objclsFunctonType.name = name
            objclsFunctonType.Ip_address = Ip_address
            objclsFunctonType.login_id = login_id
            objclsFunctonType.Tran_Type = Tran_Type
            objclsFunctonType.machine_id = machine_id
            objclsFunctonType.is_active = is_active
            objclsFunctonType.is_panel = is_panel
            If objclsFunctonType.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function UpdateActiveInactive() As Boolean
        Try
            objclsFunctonType = New clsFunctionType()
            objclsFunctonType.function_type_id = function_type_id
            objclsFunctonType.cmp_id = cmp_id
            objclsFunctonType.is_active = is_active
            If objclsFunctonType.UpdateActiveInactive() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdatePanelNotPanel() As Boolean
        Try
            objclsFunctonType = New clsFunctionType()
            objclsFunctonType.function_type_id = function_type_id
            objclsFunctonType.cmp_id = cmp_id
            objclsFunctonType.is_panel = is_panel
            If objclsFunctonType.UpdatePanelNotPanel() Then
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
            objclsFunctonType = New clsFunctionType()
            objclsFunctonType.function_type_id = function_type_id
            objclsFunctonType.cmp_id = cmp_id
            dt = objclsFunctonType.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunctonType = New clsFunctionType()
            'objclsFunctonType.function_type_id = function_type_id
            objclsFunctonType.cmp_id = cmp_id
            dt = objclsFunctonType.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

End Class

Public Class clsFunctionType
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _function_type_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private _is_active As Byte
    Private _is_panel As Byte
    Private objclsFunctonType As clsFunctionType
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

    Public Property is_panel() As Byte
        Get
            Return _is_panel
        End Get
        Set(ByVal value As Byte)
            _is_panel = value
        End Set
    End Property

    Public Property function_type_id() As Integer
        Get
            Return _function_type_id
        End Get
        Set(ByVal value As Integer)
            _function_type_id = value
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
#End Region
#Region "Function"
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_type_id", function_type_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_panel", is_panel, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Function_Type", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_type_id", function_type_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_panel", is_panel, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Function_Type", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_type_id", function_type_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_panel", is_panel, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Function_Type", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function UpdateActiveInactive() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_type_id", function_type_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Function_Type_Update", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdatePanelNotPanel() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@function_type_id", function_type_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@is_panel", is_panel, SqlDbType.TinyInt)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Function_Type_UpdatePanel", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@function_type_id", function_type_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Function_Type", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@function_type_id", function_type_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Function_Type", oColSqlparram)

        Return dtlogin
    End Function
#End Region

End Class
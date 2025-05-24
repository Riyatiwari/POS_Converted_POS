Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsDevice_Type
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Variable & Property"

    Private _Device_Type_id As Integer
    Private _Device_Type As String
    Private _cmp_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_active As Integer
    Private _cat_id As Integer
    Private objclsDeviceType As clsDevice_Type

    Public Property cat_id() As Integer
        Get
            Return _cat_id
        End Get
        Set(ByVal value As Integer)
            _cat_id = value
        End Set
    End Property
    Public Property is_active() As Integer
        Get
            Return _is_active
        End Get
        Set(ByVal value As Integer)
            _is_active = value
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

    Public Property Device_Type() As String
        Get
            Return _Device_Type
        End Get
        Set(ByVal value As String)
            _Device_Type = value
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
#End Region

#Region "Function"
    Public Function Insert() As Boolean
        Try
            objclsDeviceType = New clsDevice_Type()
            objclsDeviceType.Device_Type_id = Device_Type_id
            objclsDeviceType.Device_Type = Device_Type
            objclsDeviceType.cmp_id = cmp_id
            objclsDeviceType.login_id = login_id
            objclsDeviceType.Ip_address = Ip_address
            objclsDeviceType.is_active = is_active
            If objclsDeviceType.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsDeviceType = New clsDevice_Type()
            objclsDeviceType.Device_Type_id = Device_Type_id
            objclsDeviceType.Device_Type = Device_Type
            objclsDeviceType.cmp_id = cmp_id
            objclsDeviceType.login_id = login_id
            objclsDeviceType.Ip_address = Ip_address
            objclsDeviceType.is_active = is_active
            If objclsDeviceType.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsDeviceType = New clsDevice_Type()
            objclsDeviceType.Device_Type_id = Device_Type_id
            objclsDeviceType.Device_Type = Device_Type
            objclsDeviceType.cmp_id = cmp_id
            objclsDeviceType.login_id = login_id
            objclsDeviceType.Ip_address = Ip_address
            objclsDeviceType.is_active = is_active
            If objclsDeviceType.Delete() Then
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
            objclsDeviceType = New clsDevice_Type()
            objclsDeviceType.cmp_id = cmp_id
            objclsDeviceType.Device_Type_id = Device_Type_id
            dt = objclsDeviceType.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsDeviceType = New clsDevice_Type()
            objclsDeviceType.cmp_id = cmp_id
            dt = objclsDeviceType.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectDeviceType]() As DataTable
        Dim dt As DataTable
        Try
            objclsDeviceType = New clsDevice_Type()
            objclsDeviceType.cmp_id = cmp_id
            dt = objclsDeviceType.[SelectDeviceType]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectDeviceTypecat]() As DataTable
        Dim dt As DataTable
        Try
            objclsDeviceType = New clsDevice_Type()
            objclsDeviceType.cmp_id = cmp_id
            objclsDeviceType.cat_id = cat_id
            dt = objclsDeviceType.SelectDeviceTypeCat()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectDeviceCategory]() As DataTable
        Dim dt As DataTable
        Try
            objclsDeviceType = New clsDevice_Type()
            dt = objclsDeviceType.[SelectDeviceCategory]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region


End Class

Public Class clsDevice_Type
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Variable & Property"

    Private _Device_Type_id As Integer
    Private _Device_Type As String
    Private _cmp_id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_active As Integer
    Private _cat_id As Integer
    Private objclsDeviceType As clsDevice_Type
    Public Property cat_id() As Integer
        Get
            Return _cat_id
        End Get
        Set(ByVal value As Integer)
            _cat_id = value
        End Set
    End Property
    Public Property is_active() As Integer
        Get
            Return _is_active
        End Get
        Set(ByVal value As Integer)
            _is_active = value
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

    Public Property Device_Type() As String
        Get
            Return _Device_Type
        End Get
        Set(ByVal value As String)
            _Device_Type = value
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
#End Region
#Region "Function"

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_Type", Device_Type)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Device_Type", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_Type", Device_Type)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Device_Type", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_Type", Device_Type)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Device_Type", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Device_Type_id", Device_Type_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Device_Type", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Device_Type", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectDeviceType]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Device_Type", oColSqlparram)
        Return dtlogin
    End Function

    Public Function SelectDeviceTypeCat() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@cat_id", cat_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Device_Type_Cat", oColSqlparram)
        Return dtlogin
    End Function
    Public Function [SelectDeviceCategory]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Device_Category", oColSqlparram)
        Return dtlogin
    End Function
#End Region
End Class

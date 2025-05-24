Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading


Public Class Controller_clsPanel
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Panel_id As Integer
    Private _cmp_id As Integer
    Private _Panel As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _is_active As Byte
    Private _Tran_Type As String
    Private objclsPanel As clsPanel
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
    Public Property Panel_id() As Integer
        Get
            Return _Panel_id
        End Get
        Set(ByVal value As Integer)
            _Panel_id = value
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
    Public Property Panel() As String
        Get
            Return _Panel
        End Get
        Set(ByVal value As String)
            _Panel = value
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
    'Public Function insert() As Boolean
    '    Try
    '        objclsPanel = New clsPanel()
    '        objclsPanel.Panel_id = Panel_id
    '        objclsPanel.cmp_id = cmp_id
    '        objclsPanel.Panel = Panel
    '        objclsPanel.Ip_address = Ip_address
    '        objclsPanel.login_id = login_id
    '        objclsPanel.is_active = is_active
    '        If objclsPanel.Insert() Then
    '            Return True
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Function insert() As DataTable
        Try
            objclsPanel = New clsPanel()
            objclsPanel.Panel_id = Panel_id
            objclsPanel.cmp_id = cmp_id
            objclsPanel.Panel = Panel
            objclsPanel.Ip_address = Ip_address
            objclsPanel.login_id = login_id
            objclsPanel.is_active = is_active

            Dim dt As New DataTable
            dt = objclsPanel.Insert()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function Update() As Boolean
    '    Try
    '        objclsPanel = New clsPanel()
    '        objclsPanel.Panel_id = Panel_id
    '        objclsPanel.cmp_id = cmp_id
    '        objclsPanel.Panel = Panel
    '        objclsPanel.Ip_address = Ip_address
    '        objclsPanel.login_id = login_id
    '        objclsPanel.is_active = is_active
    '        If objclsPanel.Update() Then
    '            Return True
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Function Update() As DataTable
        Try
            objclsPanel = New clsPanel()
            objclsPanel.Panel_id = Panel_id
            objclsPanel.cmp_id = cmp_id
            objclsPanel.Panel = Panel
            objclsPanel.Ip_address = Ip_address
            objclsPanel.login_id = login_id
            objclsPanel.is_active = is_active

            Dim dt As New DataTable
            dt = objclsPanel.Update()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsPanel = New clsPanel()
            objclsPanel.Panel_id = Panel_id
            objclsPanel.cmp_id = cmp_id
            objclsPanel.Panel = Panel
            objclsPanel.Ip_address = Ip_address
            objclsPanel.login_id = login_id
            objclsPanel.is_active = is_active
            If objclsPanel.Delete() Then
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
            objclsPanel = New clsPanel()
            objclsPanel.Panel_id = Panel_id
            objclsPanel.cmp_id = cmp_id
            dt = objclsPanel.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsPanel = New clsPanel()
            objclsPanel.cmp_id = cmp_id
            dt = objclsPanel.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Panel]() As DataTable
        Dim dt As DataTable
        Try
            objclsPanel = New clsPanel()
            objclsPanel.cmp_id = cmp_id
            dt = objclsPanel.[Select_Panel]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class

Public Class clsPanel
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _Panel_id As Integer
    Private _cmp_id As Integer
    Private _Panel As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _is_active As Byte
    Private _Tran_Type As String
    Private objclsPanel As clsPanel
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
    Public Property Panel_id() As Integer
        Get
            Return _Panel_id
        End Get
        Set(ByVal value As Integer)
            _Panel_id = value
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
    Public Property Panel() As String
        Get
            Return _Panel
        End Get
        Set(ByVal value As String)
            _Panel = value
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

    'Public Function Insert() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@Panel", Panel)
    '        oColSqlparram.Add("@ip_address", Ip_address)
    '        oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@Tran_Type", "I")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Barcode_Size", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Function Insert() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Panel", Panel)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Panel", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function Update() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@Panel", Panel)
    '        oColSqlparram.Add("@ip_address", Ip_address)
    '        oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@Tran_Type", "U")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Barcode_Size", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Function Update() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Panel", Panel)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Panel", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Panel", Panel)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Panel", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Panel_id", Panel_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Panel", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Panel", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Panel]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Panel_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function
End Class

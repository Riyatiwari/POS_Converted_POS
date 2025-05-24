Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsUnit
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Unit_id As Integer
    Private _cmp_id As Integer
    Private _Unit As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_active As Byte
    Private objclsUnit As clsUnit
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
    Public Property Unit_id() As Integer
        Get
            Return _Unit_id
        End Get
        Set(ByVal value As Integer)
            _Unit_id = value
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
    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
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
    Public Function insert() As Boolean
        Try
            objclsUnit = New clsUnit()
            objclsUnit.Unit_id = Unit_id
            objclsUnit.cmp_id = cmp_id
            objclsUnit.Unit = Unit
            objclsUnit.Ip_address = Ip_address
            objclsUnit.login_id = login_id
            objclsUnit.is_active = is_active
            If objclsUnit.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsUnit = New clsUnit()
            objclsUnit.Unit_id = Unit_id
            objclsUnit.cmp_id = cmp_id
            objclsUnit.Unit = Unit
            objclsUnit.Ip_address = Ip_address
            objclsUnit.login_id = login_id
            objclsUnit.is_active = is_active
            If objclsUnit.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsUnit = New clsUnit()
            objclsUnit.Unit_id = Unit_id
            objclsUnit.cmp_id = cmp_id
            objclsUnit.Unit = Unit
            objclsUnit.Ip_address = Ip_address
            objclsUnit.login_id = login_id
            objclsUnit.Tran_Type = Tran_Type
            objclsUnit.is_active = is_active
            If objclsUnit.Delete() Then
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
            objclsUnit = New clsUnit()
            objclsUnit.Unit_id = Unit_id
            objclsUnit.cmp_id = cmp_id
            dt = objclsUnit.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsUnit = New clsUnit()
            'objclsUnit.Unit_id = Unit_id
            objclsUnit.cmp_id = cmp_id
            dt = objclsUnit.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Unit]() As DataTable
        Dim dt As DataTable
        Try
            objclsUnit = New clsUnit()
            objclsUnit.cmp_id = cmp_id
            dt = objclsUnit.[Select_Unit]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Unit_For_Gm]() As DataTable
        Dim dt As DataTable
        Try
            objclsUnit = New clsUnit()
            objclsUnit.cmp_id = cmp_id
            dt = objclsUnit.Select_Unit_For_Gm()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Unit_For_Ml]() As DataTable
        Dim dt As DataTable
        Try
            objclsUnit = New clsUnit()
            objclsUnit.cmp_id = cmp_id
            dt = objclsUnit.Select_Unit_For_Ml()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [SelectUnitNO]() As DataTable
        Dim dt As DataTable
        Try
            objclsUnit = New clsUnit()

            dt = objclsUnit.[SelectUnitNO]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsUnit
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"
    Private _Unit_id As Integer
    Private _cmp_id As Integer
    Private _Unit As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_active As Byte
    Private objclsUnit As clsUnit
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
    Public Property Unit_id() As Integer
        Get
            Return _Unit_id
        End Get
        Set(ByVal value As Integer)
            _Unit_id = value
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
    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
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

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Unit", Unit)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Unit", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Unit", Unit)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Unit", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Unit", Unit)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Unit", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Unit_id", Unit_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Unit", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Unit", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Unit]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Unit_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Unit_For_Gm]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Unit_By_Cmp_For_Gm", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Unit_For_Ml]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Unit_By_Cmp_For_Ml", oColSqlparram)

        Return dtlogin
    End Function



    Public Function [SelectUnitNO]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Unit", oColSqlparram)

        Return dtlogin
    End Function
End Class

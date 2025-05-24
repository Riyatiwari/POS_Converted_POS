Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsDepartment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Property"

    Private _Department_id As Integer
    Private _Name As String
    Private _Value As Integer
    Private _Is_Active As String
    Private _Cmp_id As Integer
    Private _Ip_Address As String
    Private _Login_id As Integer
    Private _Department_category_id As Integer
    Private oclsDepartment As clsDepartment

    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
        End Set
    End Property
    Public Property Department_category_id() As Integer
        Get
            Return _Department_category_id
        End Get
        Set(ByVal value As Integer)
            _Department_category_id = value
        End Set
    End Property
    Public Property Ip_Address() As String
        Get
            Return _Ip_Address
        End Get
        Set(ByVal value As String)
            _Ip_Address = value
        End Set
    End Property

    Public Property Cmp_id() As Integer
        Get
            Return _Cmp_id
        End Get
        Set(ByVal value As Integer)
            _Cmp_id = value
        End Set
    End Property

    Public Property Is_Active() As String
        Get
            Return _Is_Active
        End Get
        Set(ByVal value As String)
            _Is_Active = value
        End Set
    End Property

    Public Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            _Value = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property Department_id() As Integer
        Get
            Return _Department_id
        End Get
        Set(ByVal value As Integer)
            _Department_id = value
        End Set
    End Property
#End Region
#Region "Fuction"
    Public Function Insert() As Boolean
        Try
            oclsDepartment = New clsDepartment()
            oclsDepartment.Department_id = Department_id
            oclsDepartment.Name = Name
            oclsDepartment.Value = Value
            oclsDepartment.Cmp_id = Cmp_id
            oclsDepartment.Login_id = Login_id
            oclsDepartment.Ip_Address = Ip_Address
            oclsDepartment.Department_category_id = Department_category_id
            If oclsDepartment.Insert() Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function Update() As Boolean
        Try
            oclsDepartment = New clsDepartment()
            oclsDepartment.Department_id = Department_id
            oclsDepartment.Name = Name
            oclsDepartment.Value = Value
            oclsDepartment.Cmp_id = Cmp_id
            oclsDepartment.Login_id = Login_id
            oclsDepartment.Ip_Address = Ip_Address
            oclsDepartment.Department_category_id = Department_category_id
            If oclsDepartment.Update() Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function Delete() As Boolean
        Try
            oclsDepartment = New clsDepartment()
            oclsDepartment.Department_id = Department_id
            oclsDepartment.Name = Name
            oclsDepartment.Value = Value
            oclsDepartment.Cmp_id = Cmp_id
            oclsDepartment.Login_id = Login_id
            oclsDepartment.Ip_Address = Ip_Address
            oclsDepartment.Department_category_id = Department_category_id
            If oclsDepartment.Delete() Then
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
            oclsDepartment = New clsDepartment()
            oclsDepartment.Department_id = Department_id
            oclsDepartment.Cmp_id = Cmp_id
            dt = oclsDepartment.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [QrList]() As DataTable
        Dim dt As DataTable
        Try
            oclsDepartment = New clsDepartment()

            oclsDepartment.Cmp_id = Cmp_id
            dt = oclsDepartment.[QrList]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_By_Cmp]() As DataTable
        Dim dt As DataTable
        Try
            oclsDepartment = New clsDepartment()
            oclsDepartment.Cmp_id = Cmp_id
            dt = oclsDepartment.[Select_By_Cmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsDepartment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Property"

    Private _Department_id As Integer
    Private _Name As String
    Private _Value As Integer
    Private _Is_Active As String
    Private _Cmp_id As Integer
    Private _Ip_Address As String
    Private _Login_id As Integer
    Private _Department_category_id As Integer
    Private oclsDepartment As clsDepartment

    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
        End Set
    End Property

    Public Property Ip_Address() As String
        Get
            Return _Ip_Address
        End Get
        Set(ByVal value As String)
            _Ip_Address = value
        End Set
    End Property

    Public Property Cmp_id() As Integer
        Get
            Return _Cmp_id
        End Get
        Set(ByVal value As Integer)
            _Cmp_id = value
        End Set
    End Property

    Public Property Is_Active() As String
        Get
            Return _Is_Active
        End Get
        Set(ByVal value As String)
            _Is_Active = value
        End Set
    End Property

    Public Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            _Value = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property Department_id() As Integer
        Get
            Return _Department_id
        End Get
        Set(ByVal value As Integer)
            _Department_id = value
        End Set
    End Property
    Public Property Department_category_id() As Integer
        Get
            Return _Department_category_id
        End Get
        Set(ByVal value As Integer)
            _Department_category_id = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Department_id", Department_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Value", Value, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_Address", Ip_Address)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@department_category_id", Department_category_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Department", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Department_id", Department_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Value", Value, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_Address", Ip_Address)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@department_category_id", Department_category_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Department", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Department_id", Department_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Value", Value, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_Address", Ip_Address)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@department_category_id", Department_category_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Department", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Department_id", Department_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Department", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [QrList]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("GetMachineList", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select_By_Cmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Department", oColSqlparram)

        Return dtlogin
    End Function
End Class

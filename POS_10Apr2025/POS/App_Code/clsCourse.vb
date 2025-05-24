Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsCourse
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Property"

    Private _Course_id As Integer
    Private _Name As String
    Private _Value As Integer
    Private _Is_Active As String
    Private _Cmp_id As Integer
    Private _Ip_Address As String
    Private _Login_id As Integer
    Private _Course_Category_id As Integer
    Private _is_checkSlot As Integer
    Private oclsCourse As clsCourse
    Public Property Is_checkSlot() As Integer
        Get
            Return _is_checkSlot
        End Get
        Set(ByVal value As Integer)
            _is_checkSlot = value
        End Set
    End Property
    Public Property Course_Category_id() As Integer
        Get
            Return _Course_Category_id
        End Get
        Set(ByVal value As Integer)
            _Course_Category_id = value
        End Set
    End Property
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

    Public Property Course_id() As Integer
        Get
            Return _Course_id
        End Get
        Set(ByVal value As Integer)
            _Course_id = value
        End Set
    End Property
#End Region
#Region "Fuction"
    Public Function Insert() As Boolean
        Try
            oclsCourse = New clsCourse()
            oclsCourse.Course_id = Course_id
            oclsCourse.Name = Name
            oclsCourse.Value = Value
            oclsCourse.Cmp_id = Cmp_id
            oclsCourse.Login_id = Login_id
            oclsCourse.Ip_Address = Ip_Address
            oclsCourse.Course_Category_id = Course_Category_id
            oclsCourse.Is_checkSlot = Is_checkSlot
            If oclsCourse.Insert() Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function Update() As Boolean
        Try
            oclsCourse = New clsCourse()
            oclsCourse.Course_id = Course_id
            oclsCourse.Name = Name
            oclsCourse.Value = Value
            oclsCourse.Cmp_id = Cmp_id
            oclsCourse.Login_id = Login_id
            oclsCourse.Ip_Address = Ip_Address
            oclsCourse.Course_Category_id = Course_Category_id
            oclsCourse.Is_checkSlot = Is_checkSlot
            If oclsCourse.Update() Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function Delete() As Boolean
        Try
            oclsCourse = New clsCourse()
            oclsCourse.Course_id = Course_id
            oclsCourse.Name = Name
            oclsCourse.Value = Value
            oclsCourse.Cmp_id = Cmp_id
            oclsCourse.Login_id = Login_id
            oclsCourse.Ip_Address = Ip_Address
            oclsCourse.Course_Category_id = Course_Category_id
            oclsCourse.Is_checkSlot = Is_checkSlot
            If oclsCourse.Delete() Then
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
            oclsCourse = New clsCourse()
            oclsCourse.Course_id = Course_id
            oclsCourse.Cmp_id = Cmp_id
            dt = oclsCourse.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_By_Cmp]() As DataTable
        Dim dt As DataTable
        Try
            oclsCourse = New clsCourse()
            oclsCourse.Cmp_id = Cmp_id
            dt = oclsCourse.[Select_By_Cmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsCourse
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Property"

    Private _Course_id As Integer
    Private _Name As String
    Private _Value As Integer
    Private _Is_Active As String
    Private _Cmp_id As Integer
    Private _Ip_Address As String
    Private _Login_id As Integer
    Private _Course_Category_id As Integer
    Private _is_checkSlot As Integer
    Private oclsCourse As clsCourse

    Public Property Is_checkSlot() As Integer
        Get
            Return _is_checkSlot
        End Get
        Set(ByVal value As Integer)
            _is_checkSlot = value
        End Set
    End Property
    Public Property Course_Category_id() As Integer
        Get
            Return _Course_Category_id
        End Get
        Set(ByVal value As Integer)
            _Course_Category_id = value
        End Set
    End Property
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

    Public Property Course_id() As Integer
        Get
            Return _Course_id
        End Get
        Set(ByVal value As Integer)
            _Course_id = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Course_id", Course_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Value", Value, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_Address", Ip_Address)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Course_Category_id", Course_Category_id, SqlDbType.Int)
            oColSqlparram.Add("@is_checkslot", Is_checkSlot, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Course", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Course_id", Course_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Value", Value, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_Address", Ip_Address)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@Course_Category_id", Course_Category_id, SqlDbType.Int)
            oColSqlparram.Add("@is_checkslot", Is_checkSlot, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Course", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Course_id", Course_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Value", Value, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_Address", Ip_Address)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@Course_Category_id", Course_Category_id, SqlDbType.Int)
            oColSqlparram.Add("@is_checkslot", Is_checkSlot, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Course", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Course_id", Course_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Course", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select_By_Cmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Course", oColSqlparram)

        Return dtlogin
    End Function

End Class

Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Public Class Controller_clsCommon
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
    Private _country_id As Integer
    Private _state_id As Integer
    Private _city_id As Integer
    Private _department_id As Integer
    Private _course_id As Integer
    Private _profile_id As Integer
    Public Property country_id() As Integer
        Get
            Return _country_id
        End Get
        Set(ByVal value As Integer)
            _country_id = value
        End Set
    End Property
    Public Property state_id() As Integer
        Get
            Return _state_id
        End Get
        Set(ByVal value As Integer)
            _state_id = value
        End Set
    End Property
    Public Property city_id() As Integer
        Get
            Return _city_id
        End Get
        Set(ByVal value As Integer)
            _city_id = value
        End Set
    End Property
    Public Property department_id() As Integer
        Get
            Return _department_id
        End Get
        Set(ByVal value As Integer)
            _department_id = value
        End Set
    End Property
    Public Property course_id() As Integer
        Get
            Return _course_id
        End Get
        Set(ByVal value As Integer)
            _course_id = value
        End Set
    End Property
    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
        End Set
    End Property
    Private objclsCommon As clsCommon

    Public Function [SelectCountry]() As DataTable
        Dim dt As DataTable
        Try
            objclsCommon = New clsCommon()
            dt = objclsCommon.[SelectCountry]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectState]() As DataTable
        Dim dt As DataTable
        Try
            objclsCommon = New clsCommon()
            objclsCommon.country_id = country_id
            dt = objclsCommon.[SelectState]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectCity]() As DataTable
        Dim dt As DataTable
        Try
            objclsCommon = New clsCommon()
            objclsCommon.state_id = state_id
            dt = objclsCommon.[SelectCity]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectDepartment]() As DataTable
        Dim dt As DataTable
        Try
            objclsCommon = New clsCommon()
            dt = objclsCommon.[SelectDepartment]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectCourse]() As DataTable
        Dim dt As DataTable
        Try
            objclsCommon = New clsCommon()
            dt = objclsCommon.[SelectCourse]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function [SelectCurrency]() As DataTable
        Dim dt As DataTable
        Try
            objclsCommon = New clsCommon()
            dt = objclsCommon.[SelectCurrency]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Function [SelectBusinessDone]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsCommon = New clsCommon()
    '        dt = objclsCommon.[SelectBusinessDone]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Function [Selectprofile]() As DataTable
        Dim dt As DataTable
        Try
            objclsCommon = New clsCommon()
            dt = objclsCommon.Selectprofile()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCommon
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Private _country_id As Integer
    Private _state_id As Integer
    Private _city_id As Integer
    Private _department_id As Integer
    Private _course_id As Integer
    Private _profile_id As Integer
    Public Property country_id() As Integer
        Get
            Return _country_id
        End Get
        Set(ByVal value As Integer)
            _country_id = value
        End Set
    End Property
    Public Property state_id() As Integer
        Get
            Return _state_id
        End Get
        Set(ByVal value As Integer)
            _state_id = value
        End Set
    End Property
    Public Property city_id() As Integer
        Get
            Return _city_id
        End Get
        Set(ByVal value As Integer)
            _city_id = value
        End Set
    End Property
    Public Property department_id() As Integer
        Get
            Return _department_id
        End Get
        Set(ByVal value As Integer)
            _department_id = value
        End Set
    End Property
    Public Property course_id() As Integer
        Get
            Return _course_id
        End Get
        Set(ByVal value As Integer)
            _course_id = value
        End Set
    End Property
    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
        End Set
    End Property

    Public Function [SelectCountry]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Countries", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectState]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_States", oColSqlparram)
        Return dtlogin
    End Function
    Public Function [SelectCity]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_City", oColSqlparram)
        Return dtlogin
    End Function

    Public Function [SelectDepartment]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Department", oColSqlparram)
        Return dtlogin
    End Function
    Public Function [SelectCourse]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Course", oColSqlparram)
        Return dtlogin
    End Function

    Public Function [SelectCurrency]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Currency", oColSqlparram)

        Return dtlogin
    End Function

    'Public Function [SelectBusinessDone]() As DataTable
    '    Dim ds As New DataSet
    '    Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '    Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Departments", oColSqlparram)

    '    Return dtlogin
    'End Function
    Public Function [Selectprofile]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Profile", oColSqlparram)

        Return dtlogin
    End Function

End Class

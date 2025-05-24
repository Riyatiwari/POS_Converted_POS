
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsCourseCategory


    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _course_category_id As Integer
    Private _cmp_id As Integer
    Private _is_active As Byte
    Private _Course_id As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _name As String
    Private objclscoursecategory As clsCourseCategory
#End Region
    Public Property course_category_id() As Integer
        Get
            Return _course_category_id
        End Get
        Set(ByVal value As Integer)
            _course_category_id = value
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
    Public Property Course_id() As String
        Get
            Return _Course_id
        End Get
        Set(ByVal value As String)
            _Course_id = value
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
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
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
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Function Selectcourse() As DataTable
        Dim dt As DataTable
        Try
            objclscoursecategory = New clsCourseCategory()
            objclscoursecategory.cmp_id = cmp_id
            dt = objclscoursecategory.Selectcourse()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert() As Boolean
        Try
            objclscoursecategory = New clsCourseCategory()
            objclscoursecategory.Name = Name
            objclscoursecategory.cmp_id = cmp_id
            objclscoursecategory.course_category_id = course_category_id
            objclscoursecategory.is_active = is_active
            If objclscoursecategory.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclscoursecategory = New clsCourseCategory()
            objclscoursecategory.Name = Name
            objclscoursecategory.cmp_id = cmp_id
            objclscoursecategory.course_category_id = course_category_id
            objclscoursecategory.is_active = is_active

            If objclscoursecategory.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclscoursecategory = New clsCourseCategory()
            objclscoursecategory.Name = Name
            objclscoursecategory.cmp_id = cmp_id
            objclscoursecategory.course_category_id = course_category_id
            objclscoursecategory.is_active = is_active
            If objclscoursecategory.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SelectAll() As DataTable
        Dim dt As DataTable
        Try
            objclscoursecategory = New clsCourseCategory()
            objclscoursecategory.cmp_id = cmp_id
            dt = objclscoursecategory.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclscoursecategory = New clsCourseCategory()
            objclscoursecategory.course_category_id = course_category_id
            objclscoursecategory.cmp_id = cmp_id
            dt = objclscoursecategory.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
Public Class clsCourseCategory

    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"
    Private _course_category_id As Integer
    Private _cmp_id As Integer
    Private _is_active As Byte
    Private _Course_id As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _name As String
    Private objclscoursecategory As clsCourseCategory
#End Region

    Public Property course_category_id() As Integer
        Get
            Return _course_category_id
        End Get
        Set(ByVal value As Integer)
            _course_category_id = value
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
    Public Property Course_id() As String
        Get
            Return _Course_id
        End Get
        Set(ByVal value As String)
            _Course_id = value
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
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
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
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Function Selectcourse() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Course", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@course_category_id", course_category_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Course_Category", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@course_category_id", course_category_id, SqlDbType.Int)
            oColSqlparram.Add("@name", Name)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@course_category_id", course_category_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Course_Category", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@course_category_id", course_category_id, SqlDbType.Int)
            oColSqlparram.Add("@name", Name)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Course_Category", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Course_Category", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@course_category_id", course_category_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)


        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Course_CategoryInfo", oColSqlparram)

        Return dtlogin
    End Function

End Class

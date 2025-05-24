Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsDepartmentCategory


    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _department_category_id As Integer
    Private _cmp_id As Integer
    Private _is_active As Byte
    Private _department_category_name As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _AcCode As Decimal

    Private objclsdeparmentcategory As clsDepartmentCategory
#End Region
    Public Property department_category_id() As Integer
        Get
            Return _department_category_id
        End Get
        Set(ByVal value As Integer)
            _department_category_id = value
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
    Public Property department_category_name() As String
        Get
            Return _department_category_name
        End Get
        Set(ByVal value As String)
            _department_category_name = value
        End Set
    End Property

    Public Property AcCode() As Decimal
        Get
            Return _AcCode
        End Get
        Set(ByVal value As Decimal)
            _AcCode = value
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

    Public Function Insert() As Boolean
        Try
            objclsdeparmentcategory = New clsDepartmentCategory()
            objclsdeparmentcategory.department_category_id = department_category_id
            objclsdeparmentcategory.cmp_id = cmp_id
            objclsdeparmentcategory.department_category_name = department_category_name
            objclsdeparmentcategory.AcCode = AcCode
            objclsdeparmentcategory.is_active = is_active
            If objclsdeparmentcategory.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsdeparmentcategory = New clsDepartmentCategory()
            objclsdeparmentcategory.department_category_id = department_category_id
            objclsdeparmentcategory.cmp_id = cmp_id
            objclsdeparmentcategory.department_category_name = department_category_name
            objclsdeparmentcategory.AcCode = AcCode
            objclsdeparmentcategory.is_active = is_active

            If objclsdeparmentcategory.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsdeparmentcategory = New clsDepartmentCategory()
            objclsdeparmentcategory.department_category_id = department_category_id
            objclsdeparmentcategory.cmp_id = cmp_id
            objclsdeparmentcategory.department_category_name = department_category_name
            objclsdeparmentcategory.AcCode = AcCode
            objclsdeparmentcategory.is_active = is_active
            If objclsdeparmentcategory.Delete() Then
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
            objclsdeparmentcategory = New clsDepartmentCategory()
            objclsdeparmentcategory.cmp_id = cmp_id
            dt = objclsdeparmentcategory.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsdeparmentcategory = New clsDepartmentCategory()
            objclsdeparmentcategory.department_category_id = department_category_id
            objclsdeparmentcategory.cmp_id = cmp_id
            dt = objclsdeparmentcategory.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class






Public Class clsDepartmentCategory

    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _department_category_id As Integer
    Private _cmp_id As Integer
    Private _is_active As Byte
    Private _department_category_name As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _AcCode As Decimal

    Private objclsdeparmentcategory As clsDepartmentCategory
#End Region

    Public Property AcCode() As Decimal
        Get
            Return _AcCode
        End Get
        Set(ByVal value As Decimal)
            _AcCode = value
        End Set
    End Property


    Public Property department_category_id() As Integer
        Get
            Return _department_category_id
        End Get
        Set(ByVal value As Integer)
            _department_category_id = value
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
    Public Property department_category_name() As String
        Get
            Return _department_category_name
        End Get
        Set(ByVal value As String)
            _department_category_name = value
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

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@department_category_id", department_category_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@department_category_name", department_category_name)
            oColSqlparram.Add("@AcCode", AcCode, SqlDbType.Decimal)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
       
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Dep_Category", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
     Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@department_category_id", department_category_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@department_category_name", department_category_name)
            oColSqlparram.Add("@AcCode", AcCode, SqlDbType.Decimal)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Dep_Category", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
       Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@department_category_id", department_category_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@department_category_name", department_category_name)
            oColSqlparram.Add("@AcCode", AcCode, SqlDbType.Decimal)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("M_Dep_Category", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Deparment_Category", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@department_category_id", department_category_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)


        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Deparment_Category", oColSqlparram)

        Return dtlogin
    End Function
End Class

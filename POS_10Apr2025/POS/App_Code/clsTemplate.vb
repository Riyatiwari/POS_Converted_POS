Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System
'Imports Microsoft.ApplicationBlocks.Data

Public Class Controller_clsTemplate
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Constructor"
    Public Sub New()
    End Sub
#End Region

#Region "Private Variables"
    Private _Template_id As Decimal
    Private _Cmp_id As Decimal
    Private _Template_name As String
    Private _subject As String
    Private _template_detail As String
    Private _is_newsletter As Decimal
    Private _Tran_Type As String

    Private objclsTemplate As clsTemplate
#End Region

#Region "Public Properties"
    Public Property Template_id() As Decimal
        Get
            Return _Template_id
        End Get
        Set(ByVal value As Decimal)
            _Template_id = value
        End Set
    End Property
    Public Property Cmp_id() As Decimal
        Get
            Return _Cmp_id
        End Get
        Set(ByVal value As Decimal)
            _Cmp_id = value
        End Set
    End Property
    Public Property Template_name() As String
        Get
            Return _Template_name
        End Get
        Set(ByVal value As String)
            _Template_name = value
        End Set
    End Property
    Public Property subject() As String
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property
    Public Property template_detail() As String
        Get
            Return _template_detail
        End Get
        Set(ByVal value As String)
            _template_detail = value
        End Set
    End Property
    Public Property is_newsletter() As Decimal
        Get
            Return _is_newsletter
        End Get
        Set(ByVal value As Decimal)
            _is_newsletter = value
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

#Region "Public Methods"
    Public Function Insert() As Boolean
        Try
            objclsTemplate = New clsTemplate()
            objclsTemplate.Template_id = Template_id
            objclsTemplate.Cmp_id = Cmp_id
            objclsTemplate.Template_name = Template_name
            objclsTemplate.subject = subject
            objclsTemplate.template_detail = template_detail
            objclsTemplate.is_newsletter = is_newsletter
            objclsTemplate.Tran_Type = Tran_Type
            If objclsTemplate.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsTemplate = New clsTemplate()
            objclsTemplate.Template_id = Template_id
            objclsTemplate.Cmp_id = Cmp_id
            objclsTemplate.Template_name = Template_name
            objclsTemplate.subject = subject
            objclsTemplate.template_detail = template_detail
            objclsTemplate.is_newsletter = is_newsletter

            If objclsTemplate.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsTemplate = New clsTemplate()
            objclsTemplate.Template_id = Template_id
            objclsTemplate.Cmp_id = Cmp_id
            objclsTemplate.Template_name = Template_name
            objclsTemplate.subject = subject
            objclsTemplate.template_detail = template_detail
            objclsTemplate.is_newsletter = is_newsletter
            objclsTemplate.Tran_Type = Tran_Type
            If objclsTemplate.Delete() Then

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
            objclsTemplate = New clsTemplate()
            objclsTemplate.Template_id = Template_id
            objclsTemplate.Cmp_id = Cmp_id
            dt = objclsTemplate.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsTemplate = New clsTemplate()
            'objclsTemplate.Template_id = Template_id
            objclsTemplate.Cmp_id = Cmp_id
            dt = objclsTemplate.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


#End Region


End Class

Public Class clsTemplate
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Constructor"
    Public Sub New()
    End Sub
#End Region


#Region "Private Variables"
    Private _Template_id As Decimal
    Private _Cmp_id As Decimal
    Private _Template_name As String
    Private _subject As String
    Private _template_detail As String
    Private _is_newsletter As Decimal
    Private _Tran_Type As String

    Private objclsTemplate As clsTemplate
#End Region

#Region "Public Properties"
    Public Property Template_id() As Decimal
        Get
            Return _Template_id
        End Get
        Set(ByVal value As Decimal)
            _Template_id = value
        End Set
    End Property
    Public Property Cmp_id() As Decimal
        Get
            Return _Cmp_id
        End Get
        Set(ByVal value As Decimal)
            _Cmp_id = value
        End Set
    End Property
    Public Property Template_name() As String
        Get
            Return _Template_name
        End Get
        Set(ByVal value As String)
            _Template_name = value
        End Set
    End Property
    Public Property subject() As String
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property
    Public Property template_detail() As String
        Get
            Return _template_detail
        End Get
        Set(ByVal value As String)
            _template_detail = value
        End Set
    End Property
    Public Property is_newsletter() As Decimal
        Get
            Return _is_newsletter
        End Get
        Set(ByVal value As Decimal)
            _is_newsletter = value
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

#Region "Public Methods"
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@Template_id", Template_id, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Template_name", Template_name)
            oColSqlparram.Add("@subject", subject)
            oColSqlparram.Add("@template_detail", template_detail)
            oColSqlparram.Add("@is_newsletter", is_newsletter, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oClsDal.ExecStoredProcedure("P_M_Email_Template", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Template_id", Template_id, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Template_name", Template_name)
            oColSqlparram.Add("@subject", subject)
            oColSqlparram.Add("@template_detail", template_detail)
            oColSqlparram.Add("@is_newsletter", is_newsletter, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")

            oClsDal.ExecStoredProcedure("P_M_Email_Template", oColSqlparram)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Template_id", Template_id, SqlDbType.Int)
            oColSqlparram.Add("@Cmp_id", Cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Template_name", Template_name)
            oColSqlparram.Add("@subject", subject)
            oColSqlparram.Add("@template_detail", template_detail)
            oColSqlparram.Add("@is_newsletter", is_newsletter, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")

            oClsDal.GetdataTableSp("P_M_Email_Template", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Template_id", Template_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Email_Templete", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Template_id", Template_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Email_Templete", oColSqlparram)

        Return dtlogin
    End Function


#End Region


End Class
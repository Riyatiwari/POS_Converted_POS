Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsFunctionCode
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Function_Code_id As Integer
    Private _Code As String
    Private _Panel_Id As Integer
    Private objclsFunctionCode As clsFunctionCode
#End Region

#Region "Public Property"

    Public Property Function_Code_id() As Integer
        Get
            Return _Function_Code_id
        End Get
        Set(ByVal value As Integer)
            _Function_Code_id = value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property

    Public Property Panel_Id() As Integer
        Get
            Return _Panel_Id
        End Get
        Set(ByVal value As Integer)
            _Panel_Id = value
        End Set
    End Property

#End Region
#Region "Function"
    Public Function insert() As Boolean
        Try
            objclsFunctionCode = New clsFunctionCode()
            objclsFunctionCode.Function_Code_id = Function_Code_id
            objclsFunctionCode.Code = Code
            objclsFunctionCode.Panel_Id = Panel_Id
            If objclsFunctionCode.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function update() As Boolean
        Try
            objclsFunctionCode = New clsFunctionCode()
            objclsFunctionCode.Panel_Id = Panel_Id
            If objclsFunctionCode.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectFunctioncode]() As DataTable
        Dim dt As DataTable
        Try
            objclsFunctionCode = New clsFunctionCode()
            dt = objclsFunctionCode.SelectFunctioncode()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsFunctionCode
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()


#Region "Public Variables"
    Private _Function_Code_id As Integer
    Private _Code As String
    Private _Panel_Id As Integer
    Private objclsFunctionCode As clsFunctionCode
#End Region

#Region "Public Property"

    Public Property Function_Code_id() As Integer
        Get
            Return _Function_Code_id
        End Get
        Set(ByVal value As Integer)
            _Function_Code_id = value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property

    Public Property Panel_Id() As Integer
        Get
            Return _Panel_Id
        End Get
        Set(ByVal value As Integer)
            _Panel_Id = value
        End Set
    End Property

#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Function_Code_id", Function_Code_id, SqlDbType.Int)
            oColSqlparram.Add("@Code", Code)
            oColSqlparram.Add("@Panel_Id", Panel_Id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Function_Code", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Panel_Id", Panel_Id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Update_M_Function_Code", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectFunctioncode]() As DataTable
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp1("Get_M_Function_Code")
        Return dtlogin
    End Function

End Class

Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsAdvanceOrderDetail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Private Variables"
    Private _Tran_id As Integer
    Private _Advance_Tran_Id As Integer
    Private _Quantity As Decimal
    Private _Unit_id As Integer
    Private objclsOrderDetail As clsorderDetail
#End Region

#Region "Public Property"
    Public Property Tran_id() As Integer
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Integer)
            _Tran_id = value
        End Set
    End Property
    Public Property Advance_Tran_Id() As Integer
        Get
            Return _Advance_Tran_Id
        End Get
        Set(ByVal value As Integer)
            _Advance_Tran_Id = value
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
    Public Property Quantity() As Decimal
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Decimal)
            _Quantity = value
        End Set
    End Property
#End Region

    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsOrderDetail = New clsorderDetail()
            objclsOrderDetail.Advance_Tran_Id = Advance_Tran_Id
            dt = objclsOrderDetail.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsorderDetail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"
    Private _Tran_id As Integer
    Private _Advance_Tran_Id As Integer
    Private _Quantity As Decimal
    Private _Unit_id As Integer
    Private objclsOrderDetail As clsorderDetail
#End Region

#Region "Public Property"
    Public Property Tran_id() As Integer
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Integer)
            _Tran_id = value
        End Set
    End Property
    Public Property Advance_Tran_Id() As Integer
        Get
            Return _Advance_Tran_Id
        End Get
        Set(ByVal value As Integer)
            _Advance_Tran_Id = value
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
    Public Property Quantity() As Decimal
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Decimal)
            _Quantity = value
        End Set
    End Property
#End Region

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Advance_Tran_Id", Advance_Tran_Id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_EventGrid", oColSqlparram)

        Return dtlogin
    End Function
End Class
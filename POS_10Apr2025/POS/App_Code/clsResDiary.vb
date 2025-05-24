Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsResDiary
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _BookingId As Integer
    Private _BookingReference As String
    Private _VisitDateTime As System.DateTime
    Private _Duration As Integer
    Private _TurnTime As Integer
    Private _Covers As Integer
    Private _Tables As String
    Private _SpecialRequests As String
    Private _CustomerId As Integer
    Private _CustomerFullName As String
    Private _CustomerIsVip As Byte
    Private _IsTableLocked As Byte
    Private _HasPromotions As Byte
    Private _HasPayments As Byte
    Private _IsLeaveTimeConfirmed As Byte
    Private _IsWalkIn As Byte
    Private _NumberOfBookings As Integer
    Private _AreaId As Integer
    Private _ServiceId As Integer
    Private _Comments As String
    Private _ArrivalStatus As Integer
    Private _MealStatus As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime

    Private objclsResDiary As clsResDiary
#End Region

#Region "Public Property"
    Public Property BookingId() As Integer
        Get
            Return _BookingId
        End Get
        Set(ByVal value As Integer)
            _BookingId = value
        End Set
    End Property

#End Region

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsResDiary = New clsResDiary()
            objclsResDiary.BookingId = BookingId
            dt = objclsResDiary.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsResDiary
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _BookingId As Integer
    Private _BookingReference As String
    Private _VisitDateTime As System.DateTime
    Private _Duration As Integer
    Private _TurnTime As Integer
    Private _Covers As Integer
    Private _Tables As String
    Private _SpecialRequests As String
    Private _CustomerId As Integer
    Private _CustomerFullName As String
    Private _CustomerIsVip As Byte
    Private _IsTableLocked As Byte
    Private _HasPromotions As Byte
    Private _HasPayments As Byte
    Private _IsLeaveTimeConfirmed As Byte
    Private _IsWalkIn As Byte
    Private _NumberOfBookings As Integer
    Private _AreaId As Integer
    Private _ServiceId As Integer
    Private _Comments As String
    Private _ArrivalStatus As Integer
    Private _MealStatus As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime

    Private objclsResDiary As clsResDiary
#End Region

#Region "Public Property"
    Public Property BookingId() As Integer
        Get
            Return _BookingId
        End Get
        Set(ByVal value As Integer)
            _BookingId = value
        End Set
    End Property

#End Region


    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@BookingId", BookingId, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_ResDiaryBooking", oColSqlparram)

        Return dtlogin
    End Function

End Class

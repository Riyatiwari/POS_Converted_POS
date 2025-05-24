
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Imports Telerik.Web.UI


Public Class Controller_clsVoucher
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()



#Region "Public Variables"
    Private _voucher_id As Integer
    Private _cmp_id As Integer
    Private _voucher_name As String
    Private _Voucher_Type As String
    Private _start_date As DateTime
    Private _end_date As DateTime
    Private _endless As Integer
    Private _Tran_Type As String
    Private _voucher_duration As String

    Private objclsVoucherMaster As clsVoucher
#End Region


    Public Property voucher_id() As Integer
        Get
            Return _voucher_id
        End Get
        Set(ByVal value As Integer)
            _voucher_id = value
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
    Public Property voucher_duration() As String
        Get
            Return _voucher_duration
        End Get
        Set(ByVal value As String)
            _voucher_duration = value
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

    Public Property voucher_name() As String
        Get
            Return _voucher_name
        End Get
        Set(ByVal value As String)
            _voucher_name = value
        End Set
    End Property
    Public Property Voucher_Type() As String
        Get
            Return _Voucher_Type
        End Get
        Set(ByVal value As String)
            _Voucher_Type = value
        End Set
    End Property

    Public Property start_date() As System.DateTime
        Get
            Return _start_date

        End Get
        Set(ByVal value As System.DateTime)
            _start_date = value
        End Set
    End Property

    Public Property end_date() As System.DateTime
        Get
            Return _end_date

        End Get
        Set(ByVal value As System.DateTime)
            _end_date = value
        End Set
    End Property

    Public Property endless() As Integer
        Get
            Return _endless
        End Get
        Set(ByVal value As Integer)
            _endless = value
        End Set
    End Property


    Public Function Insert() As Boolean
        Try
            objclsVoucherMaster = New clsVoucher()
            objclsVoucherMaster.cmp_id = cmp_id
            objclsVoucherMaster.voucher_id = voucher_id
            objclsVoucherMaster.voucher_name = voucher_name
            objclsVoucherMaster.Voucher_Type = Voucher_Type
            objclsVoucherMaster.start_date = start_date
            objclsVoucherMaster.end_date = end_date
            objclsVoucherMaster.endless = endless
            objclsVoucherMaster.voucher_duration = voucher_duration
            objclsVoucherMaster.Tran_Type = Tran_Type
            If objclsVoucherMaster.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsVoucherMaster = New clsVoucher()
            objclsVoucherMaster.cmp_id = cmp_id
            objclsVoucherMaster.voucher_id = voucher_id
            objclsVoucherMaster.voucher_name = voucher_name
            objclsVoucherMaster.Voucher_Type = Voucher_Type
            objclsVoucherMaster.start_date = start_date
            objclsVoucherMaster.end_date = end_date
            objclsVoucherMaster.endless = endless
            objclsVoucherMaster.voucher_duration = voucher_duration
            objclsVoucherMaster.Tran_Type = Tran_Type
            If objclsVoucherMaster.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsVoucherMaster = New clsVoucher()
            objclsVoucherMaster.cmp_id = cmp_id
            objclsVoucherMaster.voucher_id = voucher_id
            objclsVoucherMaster.voucher_name = voucher_name
            objclsVoucherMaster.Voucher_Type = Voucher_Type
            objclsVoucherMaster.start_date = start_date
            objclsVoucherMaster.end_date = end_date
            objclsVoucherMaster.endless = endless
            objclsVoucherMaster.voucher_duration = voucher_duration
            objclsVoucherMaster.Tran_Type = Tran_Type
            If objclsVoucherMaster.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsVoucherMaster = New clsVoucher()
            objclsVoucherMaster.cmp_id = cmp_id
            dt = objclsVoucherMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsVoucherMaster = New clsVoucher()
            objclsVoucherMaster.voucher_id = voucher_id
            objclsVoucherMaster.cmp_id = cmp_id
            dt = objclsVoucherMaster.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class




Public Class clsVoucher
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _voucher_id As Integer
    Private _cmp_id As Integer
    Private _voucher_name As String
    Private _Voucher_Type As String
    Private _start_date As DateTime
    Private _end_date As DateTime
    Private _endless As Integer
    Private _Tran_Type As String
    Private _voucher_duration As String

    Private objclsVoucher As clsVoucher
#End Region

#Region "Public Property"
    Public Property voucher_id() As Integer
        Get
            Return _voucher_id
        End Get
        Set(ByVal value As Integer)
            _voucher_id = value
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
    Public Property voucher_duration() As String
        Get
            Return _voucher_duration
        End Get
        Set(ByVal value As String)
            _voucher_duration = value
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

    Public Property voucher_name() As String
        Get
            Return _voucher_name
        End Get
        Set(ByVal value As String)
            _voucher_name = value
        End Set
    End Property
    Public Property Voucher_Type() As String
        Get
            Return _Voucher_Type
        End Get
        Set(ByVal value As String)
            _Voucher_Type = value
        End Set
    End Property

    Public Property start_date() As System.DateTime
        Get
            Return _start_date

        End Get
        Set(ByVal value As System.DateTime)
            _start_date = value
        End Set
    End Property

    Public Property end_date() As System.DateTime
        Get
            Return _end_date

        End Get
        Set(ByVal value As System.DateTime)
            _end_date = value
        End Set
    End Property

    Public Property endless() As Integer
        Get
            Return _endless
        End Get
        Set(ByVal value As Integer)
            _endless = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@voucher_id ", voucher_id, SqlDbType.Int)
            oColSqlparram.Add("@voucher_name", voucher_name)
            oColSqlparram.Add("@Voucher_Type ", Voucher_Type)
            oColSqlparram.Add("@start_date ", start_date, SqlDbType.DateTime)
            oColSqlparram.Add("@end_date ", end_date, SqlDbType.DateTime)
            oColSqlparram.Add("@endless ", endless, SqlDbType.Int)
            oColSqlparram.Add("@Voucher_duration", voucher_duration)

            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Voucher", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@voucher_id ", voucher_id, SqlDbType.Int)
            oColSqlparram.Add("@voucher_name", voucher_name)
            oColSqlparram.Add("@Voucher_Type ", Voucher_Type)
            oColSqlparram.Add("@start_date ", start_date, SqlDbType.DateTime)
            oColSqlparram.Add("@end_date ", end_date, SqlDbType.DateTime)
            oColSqlparram.Add("@endless ", endless, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Voucher", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@voucher_id ", voucher_id, SqlDbType.Int)
            oColSqlparram.Add("@voucher_name", voucher_name)
            oColSqlparram.Add("@Voucher_Type ", Voucher_Type)
            oColSqlparram.Add("@start_date ", start_date, SqlDbType.DateTime)
            oColSqlparram.Add("@end_date ", end_date, SqlDbType.DateTime)
            oColSqlparram.Add("@endless ", endless, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Voucher", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Voucher", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@voucher_id", voucher_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_VoucherInfo", oColSqlparram)

        Return dtlogin
    End Function
End Class

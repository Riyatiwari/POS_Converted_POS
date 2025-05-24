Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Imports Telerik.Web.UI
Imports System.Web

Public Class Controller_clsIssueVoucher
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()


#Region "Public Variables"

    Private _Issuevoucher_id As Integer
    Private _cmp_id As Integer
    Private _account As Integer
    Private _Voucher_id As Integer
    Private _deposit_amount As Decimal
    Private _issue_datetime As DateTime
    Private _ref_no As String
    Private _Tran_Type As String
    Private _start_date As DateTime
    Private _end_date As DateTime
    Private _voucher_duration As String

    Private objclsIssueVoucherMaster As clsIssueVoucher
#End Region

    Public Property voucher_duration() As String
        Get
            Return _voucher_duration
        End Get
        Set(ByVal value As String)
            _voucher_duration = value
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

    Public Property Issuevoucher_id() As Integer
        Get
            Return _Issuevoucher_id
        End Get
        Set(ByVal value As Integer)
            _Issuevoucher_id = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property

    Public Property account() As Integer
        Get
            Return _account
        End Get
        Set(ByVal value As Integer)
            _account = value
        End Set
    End Property
    Public Property Voucher_id() As String
        Get
            Return _Voucher_id
        End Get
        Set(ByVal value As String)
            _Voucher_id = value
        End Set
    End Property

    Public Property issue_datetime() As System.DateTime
        Get
            Return _issue_datetime

        End Get
        Set(ByVal value As System.DateTime)
            _issue_datetime = value
        End Set
    End Property

    Public Property deposit_amount() As Decimal
        Get
            Return _deposit_amount

        End Get
        Set(ByVal value As Decimal)
            _deposit_amount = value
        End Set
    End Property

    Public Property ref_no() As String
        Get
            Return _ref_no
        End Get
        Set(ByVal value As String)
            _ref_no = value
        End Set
    End Property

    Public Function SelectAccount() As DataTable
        Dim dt As DataTable
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            dt = objclsIssueVoucherMaster.[SelectAcocount]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function SelectVoucher() As DataTable
        Dim dt As DataTable
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            dt = objclsIssueVoucherMaster.[SelectVoucher]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function Insert() As Boolean
        Try


            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            objclsIssueVoucherMaster.account = account
            objclsIssueVoucherMaster.Voucher_id = Voucher_id
            objclsIssueVoucherMaster.deposit_amount = deposit_amount
            objclsIssueVoucherMaster.issue_datetime = issue_datetime
            objclsIssueVoucherMaster.ref_no = ref_no
            objclsIssueVoucherMaster.start_date = start_date
            objclsIssueVoucherMaster.end_date = end_date
            objclsIssueVoucherMaster.voucher_duration = voucher_duration
            'objclsIssueVoucherMaster.Tran_Type = Tran_Type
            If objclsIssueVoucherMaster.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_VoucherTran() As Boolean
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            objclsIssueVoucherMaster.account = account
            objclsIssueVoucherMaster.deposit_amount = deposit_amount
            objclsIssueVoucherMaster.issue_datetime = issue_datetime
            objclsIssueVoucherMaster.ref_no = ref_no
            'objclsIssueVoucherMaster.Tran_Type = Tran_Type
            If objclsIssueVoucherMaster.Insert_VoucherTran() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            objclsIssueVoucherMaster.Issuevoucher_id = Issuevoucher_id
            objclsIssueVoucherMaster.account = account
            objclsIssueVoucherMaster.Voucher_id = Voucher_id
            objclsIssueVoucherMaster.deposit_amount = deposit_amount
            objclsIssueVoucherMaster.issue_datetime = issue_datetime
            objclsIssueVoucherMaster.ref_no = ref_no
            objclsIssueVoucherMaster.start_date = start_date
            objclsIssueVoucherMaster.end_date = end_date
            objclsIssueVoucherMaster.voucher_duration = voucher_duration
            'objclsIssueVoucherMaster.Tran_Type = Tran_Type
            If objclsIssueVoucherMaster.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_VoucherTran() As Boolean
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            objclsIssueVoucherMaster.account = account
            objclsIssueVoucherMaster.deposit_amount = deposit_amount
            objclsIssueVoucherMaster.issue_datetime = issue_datetime
            objclsIssueVoucherMaster.ref_no = ref_no
            'objclsIssueVoucherMaster.Tran_Type = Tran_Type
            If objclsIssueVoucherMaster.Update_VoucherTran() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            objclsIssueVoucherMaster.Issuevoucher_id = Issuevoucher_id
            objclsIssueVoucherMaster.account = account
            objclsIssueVoucherMaster.Voucher_id = Voucher_id
            objclsIssueVoucherMaster.deposit_amount = deposit_amount
            objclsIssueVoucherMaster.issue_datetime = issue_datetime
            objclsIssueVoucherMaster.ref_no = ref_no
            objclsIssueVoucherMaster.Tran_Type = Tran_Type
            If objclsIssueVoucherMaster.Delete() Then
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
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            dt = objclsIssueVoucherMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll_Tran]() As DataTable
        Dim dt As DataTable
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            dt = objclsIssueVoucherMaster.[SelectAll_Tran]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SelectDateWise_Tran() As DataTable
        Dim dt As DataTable
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.cmp_id = cmp_id
            objclsIssueVoucherMaster.start_date = start_date
            objclsIssueVoucherMaster.end_date = end_date
            dt = objclsIssueVoucherMaster.SelectDateWise_Tran()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function GetRefno() As DataTable
        Dim dt As DataTable
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.Issuevoucher_id = Issuevoucher_id
            objclsIssueVoucherMaster.cmp_id = cmp_id
            dt = objclsIssueVoucherMaster.[GetRefno]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsIssueVoucherMaster = New clsIssueVoucher()
            objclsIssueVoucherMaster.Issuevoucher_id = Issuevoucher_id
            objclsIssueVoucherMaster.cmp_id = cmp_id
            dt = objclsIssueVoucherMaster.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsIssueVoucher
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"

    Private _Issuevoucher_id As Integer
    Private _cmp_id As Integer
    Private _account As Integer
    Private _Voucher_id As Integer
    Private _deposit_amount As Decimal
    Private _issue_datetime As DateTime
    Private _ref_no As String
    Private _Tran_Type As String
    Private _start_date As DateTime
    Private _end_date As DateTime
    Private _voucher_duration As String

    Private objclsIssueVoucherMaster As clsIssueVoucher
#End Region

    Public Property voucher_duration() As String
        Get
            Return _voucher_duration
        End Get
        Set(ByVal value As String)
            _voucher_duration = value
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

    Public Property Issuevoucher_id() As Integer
        Get
            Return _Issuevoucher_id
        End Get
        Set(ByVal value As Integer)
            _Issuevoucher_id = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property

    Public Property account() As Integer
        Get
            Return _account
        End Get
        Set(ByVal value As Integer)
            _account = value
        End Set
    End Property
    Public Property Voucher_id() As String
        Get
            Return _Voucher_id
        End Get
        Set(ByVal value As String)
            _Voucher_id = value
        End Set
    End Property

    Public Property issue_datetime() As System.DateTime
        Get
            Return _issue_datetime

        End Get
        Set(ByVal value As System.DateTime)
            _issue_datetime = value
        End Set
    End Property

    Public Property deposit_amount() As Decimal
        Get
            Return _deposit_amount

        End Get
        Set(ByVal value As Decimal)
            _deposit_amount = value
        End Set
    End Property

    Public Property ref_no() As String
        Get
            Return _ref_no
        End Get
        Set(ByVal value As String)
            _ref_no = value
        End Set
    End Property


    Public Function Insert_VoucherTran() As Boolean
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@sales_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@customer_id", account, SqlDbType.Int)
            oColSqlparram.Add("@Amount", deposit_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@VoucherRef_no", ref_no)
            oColSqlparram.Add("@tran_date", issue_datetime, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", HttpContext.Current.Request.UserHostAddress.ToString())
            oColSqlparram.Add("@Tran_Type", "I")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("WS_M_VoucherTran", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_VoucherTran() As Boolean
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@sales_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@customer_id", account, SqlDbType.Int)
            oColSqlparram.Add("@Amount", deposit_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@VoucherRef_no", ref_no)
            oColSqlparram.Add("@tran_date", issue_datetime, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", HttpContext.Current.Request.UserHostAddress.ToString())
            oColSqlparram.Add("@Tran_Type", "U")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("WS_M_VoucherTran", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert() As Boolean
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Issuevoucher_id ", Issuevoucher_id, SqlDbType.Int)
            oColSqlparram.Add("@account", account, SqlDbType.Int)
            oColSqlparram.Add("@Voucher_id ", Voucher_id, SqlDbType.Int)
            oColSqlparram.Add("@deposit_amount ", deposit_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@issue_datetime ", issue_datetime, SqlDbType.DateTime)
            oColSqlparram.Add("@ref_no ", ref_no)
            oColSqlparram.Add("@start_date ", start_date, SqlDbType.DateTime)
            oColSqlparram.Add("@end_date ", end_date, SqlDbType.DateTime)
            oColSqlparram.Add("@Voucher_duration", voucher_duration)


            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_IssueVoucher", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Issuevoucher_id ", Issuevoucher_id, SqlDbType.Int)
            oColSqlparram.Add("@account", account, SqlDbType.Int)
            oColSqlparram.Add("@Voucher_id ", Voucher_id, SqlDbType.Int)
            oColSqlparram.Add("@deposit_amount ", deposit_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@issue_datetime ", issue_datetime, SqlDbType.DateTime)
            oColSqlparram.Add("@ref_no ", ref_no)
            oColSqlparram.Add("@start_date ", start_date, SqlDbType.DateTime)
            oColSqlparram.Add("@end_date ", end_date, SqlDbType.DateTime)
            oColSqlparram.Add("@Voucher_duration", voucher_duration)

            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_IssueVoucher", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Issuevoucher_id ", Issuevoucher_id, SqlDbType.Int)
            oColSqlparram.Add("@account", account, SqlDbType.Int)
            oColSqlparram.Add("@Voucher_id ", Voucher_id, SqlDbType.Int)
            oColSqlparram.Add("@deposit_amount ", deposit_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@issue_datetime ", issue_datetime, SqlDbType.DateTime)
            oColSqlparram.Add("@ref_no ", ref_no)

            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_IssueVoucher", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_IssueVoucher_List", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll_Tran]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_VoucherTran_List", oColSqlparram)

        Return dtlogin
    End Function


    Public Function SelectDateWise_Tran() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@start_date ", start_date, SqlDbType.DateTime)
        oColSqlparram.Add("@end_date ", end_date, SqlDbType.DateTime)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_VoucherTran_DateWise", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [GetRefno]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Issuevoucher_id", Issuevoucher_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Tran_Type", "G")
        Dim dtGetRefno As DataTable = oClsDal.GetdataTableSp("P_M_IssueVoucher", oColSqlparram)

        Return dtGetRefno
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Issuevoucher_id", Issuevoucher_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Issue_Voucher", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAcocount]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Tran_Type", "1")
        Dim dtAcocount As DataTable = oClsDal.GetdataTableSp("Get_Dropdown_Issue_Voucher", oColSqlparram)

        Return dtAcocount
    End Function
    Public Function [SelectVoucher]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Tran_Type", "2")
        Dim dtVoucher As DataTable = oClsDal.GetdataTableSp("Get_Dropdown_Issue_Voucher", oColSqlparram)

        Return dtVoucher
    End Function

End Class

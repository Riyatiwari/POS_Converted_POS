Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsCreditAccount

#Region "Public Variables"
    Private _transcation_id As Integer
    Private _customer_web_id As Integer
    Private _customer_mobile_no As String
    Private _Amount As Decimal
    Private _machine_id As Integer
    Private _location_id As Integer
    Private _pay_uuid As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _start_date As System.DateTime
    Private _end_date As System.DateTime
    Private _Credit_date As System.DateTime
    Private _Tran_Type As String
    Private objclsCredit As clsCreditAccount

#End Region

#Region "Public Property"
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
    Public Property Credit_date() As System.DateTime
        Get
            Return _Credit_date
        End Get
        Set(ByVal value As System.DateTime)
            _Credit_date = value
        End Set
    End Property
    Public Property transcation_id() As Integer
        Get
            Return _transcation_id
        End Get
        Set(ByVal value As Integer)
            _transcation_id = value
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
    Public Property customer_web_id() As Integer
        Get
            Return _customer_web_id
        End Get
        Set(ByVal value As Integer)
            _customer_web_id = value
        End Set
    End Property
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property
    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property customer_mobile_no() As String
        Get
            Return _customer_mobile_no
        End Get
        Set(ByVal value As String)
            _customer_mobile_no = value
        End Set
    End Property
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property
    Public Property pay_uuid() As String
        Get
            Return _pay_uuid
        End Get
        Set(ByVal value As String)
            _pay_uuid = value
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

#End Region


    Public Function Insert() As Boolean
        Try
            objclsCredit = New clsCreditAccount()
            objclsCredit.transcation_id = transcation_id
            objclsCredit.cmp_id = cmp_id
            objclsCredit.customer_web_id = customer_web_id
            objclsCredit.customer_mobile_no = customer_mobile_no
            objclsCredit.Amount = Amount
            objclsCredit.Credit_date = Credit_date
            objclsCredit.Tran_Type = Tran_Type
            If objclsCredit.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsCredit = New clsCreditAccount()
            objclsCredit.transcation_id = transcation_id
            objclsCredit.cmp_id = cmp_id
            objclsCredit.customer_web_id = customer_web_id
            objclsCredit.customer_mobile_no = customer_mobile_no
            objclsCredit.Amount = Amount
            objclsCredit.Credit_date = Credit_date
            objclsCredit.Tran_Type = Tran_Type
            If objclsCredit.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsCredit = New clsCreditAccount()
            objclsCredit.transcation_id = transcation_id
            objclsCredit.cmp_id = cmp_id
            objclsCredit.customer_web_id = customer_web_id
            objclsCredit.customer_mobile_no = customer_mobile_no
            objclsCredit.Amount = Amount
            objclsCredit.Credit_date = Credit_date
            objclsCredit.Tran_Type = Tran_Type
            If objclsCredit.Delete() Then
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
            objclsCredit = New clsCreditAccount()
            'objclsCustomer.cmp_id = cmp_id
            'objclsCustomer.customer_id = customer_id
            objclsCredit.start_date = start_date
            objclsCredit.end_date = end_date
            dt = objclsCredit.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_payment]() As DataTable
        Dim dt As DataTable
        Try
            objclsCredit = New clsCreditAccount()
            'objclsCustomer.cmp_id = cmp_id
            'objclsCustomer.customer_id = customer_id
            dt = objclsCredit.[Select_payment]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectAccount() As DataTable
        Dim dt As DataTable
        Try
            objclsCredit = New clsCreditAccount()
            objclsCredit.cmp_id = cmp_id
            dt = objclsCredit.[SelectAccount]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function SelectAccount_cust() As DataTable
        Dim dt As DataTable
        Try
            objclsCredit = New clsCreditAccount()
            objclsCredit.cmp_id = cmp_id
            dt = objclsCredit.[SelectAccount_cust]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Get_CustDetail() As DataTable
        Dim dt As DataTable
        Try
            objclsCredit = New clsCreditAccount()
            objclsCredit.cmp_id = cmp_id
            objclsCredit.customer_web_id = customer_web_id
            dt = objclsCredit.[Get_CustDetail]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Select_customer() As DataTable
        Dim dt As DataTable
        Try
            objclsCredit = New clsCreditAccount()
            objclsCredit.cmp_id = cmp_id
            objclsCredit.customer_web_id = customer_web_id
            objclsCredit.start_date = start_date
            objclsCredit.end_date = end_date
            dt = objclsCredit.[Select_customer]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

End Class
Public Class clsCreditAccount
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _transcation_id As Integer
    Private _customer_web_id As Integer
    Private _customer_mobile_no As String
    Private _Amount As Decimal
    Private _machine_id As Integer
    Private _location_id As Integer
    Private _pay_uuid As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _start_date As System.DateTime
    Private _end_date As System.DateTime
    Private _Credit_date As System.DateTime
    Private _Tran_Type As String
    Private objclsCustomer As clsCreditAccount

#End Region

#Region "Public Property"

    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
    Public Property Credit_date() As System.DateTime
        Get
            Return _Credit_date
        End Get
        Set(ByVal value As System.DateTime)
            _Credit_date = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property transcation_id() As Integer
        Get
            Return _transcation_id
        End Get
        Set(ByVal value As Integer)
            _transcation_id = value
        End Set
    End Property
    Public Property customer_web_id() As Integer
        Get
            Return _customer_web_id
        End Get
        Set(ByVal value As Integer)
            _customer_web_id = value
        End Set
    End Property
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property
    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property customer_mobile_no() As String
        Get
            Return _customer_mobile_no
        End Get
        Set(ByVal value As String)
            _customer_mobile_no = value
        End Set
    End Property
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property
    Public Property pay_uuid() As String
        Get
            Return _pay_uuid
        End Get
        Set(ByVal value As String)
            _pay_uuid = value
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

#End Region



    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@transcation_id", transcation_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@customer_web_id", customer_web_id, SqlDbType.Int)
            oColSqlparram.Add("@customer_mobile_no", customer_mobile_no)
            oColSqlparram.Add("@Amount", Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Credit_date", Credit_date, SqlDbType.Date)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Credit_Payment_Add", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@transcation_id", transcation_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@customer_web_id", customer_web_id, SqlDbType.Int)
            oColSqlparram.Add("@customer_mobile_no", customer_mobile_no)
            oColSqlparram.Add("@Amount", Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Credit_date", Credit_date, SqlDbType.Date)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Credit_Payment_Add", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@transcation_id", transcation_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@customer_web_id", customer_web_id, SqlDbType.Int)
            oColSqlparram.Add("@customer_mobile_no", customer_mobile_no)
            oColSqlparram.Add("@Amount", Amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Credit_date", Credit_date, SqlDbType.Date)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Credit_Payment_Add", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [Select_payment]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Credit_Account_Pay_web", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        oColSqlparram.Add("@start_date", start_date, SqlDbType.DateTime)
        oColSqlparram.Add("@end_date", end_date, SqlDbType.DateTime)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Credit_Account_Pay", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAccount]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtAccount As DataTable = oClsDal.GetdataTableSp("Get_Account_Customer", oColSqlparram)

        Return dtAccount
    End Function

    Public Function [SelectAccount_cust]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtAccount As DataTable = oClsDal.GetdataTableSp("Get_Account_Customer_credit", oColSqlparram)

        Return dtAccount
    End Function

    Public Function [Get_CustDetail]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@customer_id", customer_web_id, SqlDbType.Int)
        Dim dtAccount As DataTable = oClsDal.GetdataTableSp("Get_Customer_Detail", oColSqlparram)

        Return dtAccount
    End Function

    Public Function [Select_customer]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@customer_id", customer_web_id, SqlDbType.Int)
        oColSqlparram.Add("@start_date", start_date, SqlDbType.DateTime)
        oColSqlparram.Add("@end_date", end_date, SqlDbType.DateTime)
        Dim dtAccount As DataTable = oClsDal.GetdataTableSp("Get_Credit_Account_Customer", oColSqlparram)

        Return dtAccount
    End Function

End Class

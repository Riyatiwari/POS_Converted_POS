Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsPayByLink
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"

    Private _req_id As Integer
    Private _pay_uuid As String
    Private _Amount As Double
    Private _ref_id As String
    Private _request_date As DateTime
    Private _link As String
    Private _Status As Byte
    Private _payment_ref As String
    Private _payment_date As DateTime
    Private _account_id As Integer
    Private _emailid As String
    Private _is_email As Byte
    Private _till_id As Integer


    Private objclsPayByLink As clsPayByLink
#End Region

#Region "Public Property"
    Public Property is_email() As Byte
        Get
            Return _is_email
        End Get
        Set(ByVal value As Byte)
            _is_email = value
        End Set
    End Property
    Public Property Status() As Byte
        Get
            Return _Status
        End Get
        Set(ByVal value As Byte)
            _Status = value
        End Set
    End Property
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            _Amount = value
        End Set
    End Property
    Public Property till_id() As Integer
        Get
            Return _till_id
        End Get
        Set(ByVal value As Integer)
            _till_id = value
        End Set
    End Property
    Public Property account_id() As Integer
        Get

            Return _account_id
        End Get
        Set(ByVal value As Integer)
            _account_id = value
        End Set
    End Property
    Public Property emailid() As String
        Get
            Return _emailid
        End Get
        Set(ByVal value As String)
            _emailid = value
        End Set
    End Property
    Public Property payment_ref() As String
        Get
            Return _payment_ref
        End Get
        Set(ByVal value As String)
            _payment_ref = value
        End Set
    End Property
    Public Property req_id() As Integer
        Get
            Return _req_id
        End Get
        Set(ByVal value As Integer)
            _req_id = value
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
    Public Property ref_id() As String
        Get
            Return _ref_id
        End Get
        Set(ByVal value As String)
            _ref_id = value
        End Set
    End Property
    Public Property link() As String
        Get
            Return _link
        End Get
        Set(ByVal value As String)
            _link = value
        End Set
    End Property
    Public Property payment_date() As DateTime
        Get
            Return _payment_date
        End Get
        Set(ByVal value As DateTime)
            _payment_date = value
        End Set
    End Property
    Public Property request_date() As DateTime
        Get
            Return _request_date
        End Get
        Set(ByVal value As DateTime)
            _request_date = value
        End Set
    End Property

#End Region

#Region "Function"

    Public Function checkRequest() As DataTable
        Dim dt As DataTable
        Try
            objclsPayByLink = New clsPayByLink()
            objclsPayByLink.pay_uuid = pay_uuid
            objclsPayByLink.till_id = till_id
            dt = objclsPayByLink.checkRequest()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function saveRequest() As DataTable
        Dim dt As DataTable
        Try
            objclsPayByLink = New clsPayByLink()

            objclsPayByLink.pay_uuid = pay_uuid
            objclsPayByLink.Amount = Amount
            objclsPayByLink.ref_id = ref_id
            objclsPayByLink.link = link
            objclsPayByLink.account_id = account_id
            objclsPayByLink.till_id = till_id
            objclsPayByLink.emailid = emailid
            dt = objclsPayByLink.saveRequst()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateOrderRequest() As DataTable
        Dim dt As DataTable
        Try
            objclsPayByLink = New clsPayByLink()

            objclsPayByLink.pay_uuid = pay_uuid
            objclsPayByLink.ref_id = ref_id
            dt = objclsPayByLink.UpdateOrderRequst()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region
End Class
Public Class clsPayByLink
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"

    Private _req_id As Integer
    Private _pay_uuid As String
    Private _Amount As Double
    Private _ref_id As String
    Private _request_date As DateTime
    Private _link As String
    Private _Status As Byte
    Private _payment_ref As String
    Private _payment_date As DateTime
    Private _account_id As Integer
    Private _emailid As String
    Private _is_email As Byte
    Private _till_id As Integer

    Private objclsPayByLink As clsUnit
#End Region

#Region "Public Property"
    Public Property is_email() As Byte
        Get
            Return _is_email
        End Get
        Set(ByVal value As Byte)
            _is_email = value
        End Set
    End Property
    Public Property Status() As Byte
        Get
            Return _Status
        End Get
        Set(ByVal value As Byte)
            _Status = value
        End Set
    End Property
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            _Amount = value
        End Set
    End Property
    Public Property till_id() As Integer
        Get
            Return _till_id
        End Get
        Set(ByVal value As Integer)
            _till_id = value
        End Set
    End Property
    Public Property account_id() As Integer
        Get

            Return _account_id
        End Get
        Set(ByVal value As Integer)
            _account_id = value
        End Set
    End Property
    Public Property emailid() As String
        Get
            Return _emailid
        End Get
        Set(ByVal value As String)
            _emailid = value
        End Set
    End Property
    Public Property payment_ref() As String
        Get
            Return _payment_ref
        End Get
        Set(ByVal value As String)
            _payment_ref = value
        End Set
    End Property
    Public Property req_id() As Integer
        Get
            Return _req_id
        End Get
        Set(ByVal value As Integer)
            _req_id = value
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
    Public Property ref_id() As String
        Get
            Return _ref_id
        End Get
        Set(ByVal value As String)
            _ref_id = value
        End Set
    End Property
    Public Property link() As String
        Get
            Return _link
        End Get
        Set(ByVal value As String)
            _link = value
        End Set
    End Property
    Public Property payment_date() As DateTime
        Get
            Return _payment_date
        End Get
        Set(ByVal value As DateTime)
            _payment_date = value
        End Set
    End Property
    Public Property request_date() As DateTime
        Get
            Return _request_date
        End Get
        Set(ByVal value As DateTime)
            _request_date = value
        End Set
    End Property

#End Region


    Public Function checkRequest() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@pay_uuid", pay_uuid)
        oColSqlparram.Add("@till_id", till_id)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Check_M_paybylink", oColSqlparram)

        Return dtlogin
    End Function

    Public Function saveRequst() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@pay_uuid", pay_uuid)
        oColSqlparram.Add("@Amount", Amount)
        oColSqlparram.Add("@ref_id", ref_id)
        oColSqlparram.Add("@link", link)
        oColSqlparram.Add("@account_id", account_id)
        oColSqlparram.Add("@till_id", till_id)
        oColSqlparram.Add("@email_id", emailid)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("m_paylink_save", oColSqlparram)

        Return dtlogin
    End Function
    Public Function UpdateOrderRequst() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@pay_uuid", pay_uuid)
        oColSqlparram.Add("@ref_id", ref_id)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("m_paylink_update_order", oColSqlparram)

        Return dtlogin
    End Function
End Class

Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_defaultValue

#Region "Public Variables"
    Private _cmp_id As Integer
    Private objclsDefault As defaultValue

#End Region

#Region "Public Property"
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
#End Region

    Public Function GET_Currency() As DataTable
        Dim dt As DataTable
        Try
            objclsDefault = New defaultValue()
            'objclsDefault.cmp_id = cmp_id
            dt = objclsDefault.GET_Currency()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Currency() As DataTable
        Dim dt As DataTable
        Try
            objclsDefault = New defaultValue()
            'objclsCustomer.cmp_id = cmp_id
            dt = objclsDefault.Currency()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Country() As DataTable
        Dim dt As DataTable
        Try
            objclsDefault = New defaultValue()
            'objclsCustomer.cmp_id = cmp_id
            dt = objclsDefault.Country()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Booking() As DataTable
        Dim dt As DataTable
        Try
            objclsDefault = New defaultValue()
            'objclsCustomer.cmp_id = cmp_id
            dt = objclsDefault.Booking()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function DeviceType() As DataTable
        Dim dt As DataTable
        Try
            objclsDefault = New defaultValue()
            objclsDefault.cmp_id = cmp_id
            dt = objclsDefault.DeviceType()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Expense() As DataTable
        Dim dt As DataTable
        Try
            objclsDefault = New defaultValue()
            'objclsCustomer.cmp_id = cmp_id
            dt = objclsDefault.Expense()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Unit() As DataTable
        Dim dt As DataTable
        Try
            objclsDefault = New defaultValue()
            objclsDefault.cmp_id = cmp_id
            dt = objclsDefault.Unit()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class defaultValue
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


#Region "Public Variables"
    Private _cmp_id As Integer
#End Region

#Region "Public Property"
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
#End Region

    Public Function GET_Currency() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Currency", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Currency() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("default_Currency", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Unit() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("default_Unit", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Expense() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("default_Expense", oColSqlparram)

        Return dtlogin
    End Function

    Public Function DeviceType() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("default_DeviceType", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Booking() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("default_Booking", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Country() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("default_Country", oColSqlparram)

        Return dtlogin
    End Function

End Class

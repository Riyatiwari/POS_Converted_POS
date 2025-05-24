Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading


Public Class Controller_clsExpenseMaster
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Exp_id As Integer
    Private _name As String
    Private _is_active As Byte
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _date1 As System.DateTime
    Private _date2 As System.DateTime
    Private _location_id As Int32
    Private _cmp_id As Int32
    Private _is_expense As Byte
    Private _duration As String
    Private objclsExpenseMaster As clsExpenseMaster
#End Region

#Region "Public Property"
    Public Property duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
        End Set
    End Property
    Public Property is_expense() As Byte
        Get
            Return _is_expense
        End Get
        Set(ByVal value As Byte)
            _is_expense = value
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
    Public Property Exp_id() As Integer
        Get
            Return _Exp_id
        End Get
        Set(ByVal value As Integer)
            _Exp_id = value
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
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
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

    Public Property date1() As System.DateTime
        Get
            Return _date1

        End Get
        Set(ByVal value As System.DateTime)
            _date1 = value
        End Set
    End Property
    Public Property date2() As System.DateTime
        Get
            Return _date2

        End Get
        Set(ByVal value As System.DateTime)
            _date2 = value
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
#End Region
#Region "Function"
    Public Function insert() As Boolean
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.Exp_id = Exp_id
            objclsExpenseMaster.name = name
            objclsExpenseMaster.is_active = is_active
            objclsExpenseMaster.is_expense = is_expense
            objclsExpenseMaster.Ip_address = Ip_address
            If objclsExpenseMaster.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.Exp_id = Exp_id
            objclsExpenseMaster.name = name
            objclsExpenseMaster.is_active = is_active
            objclsExpenseMaster.is_expense = is_expense
            objclsExpenseMaster.Ip_address = Ip_address
            If objclsExpenseMaster.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.Exp_id = Exp_id
            objclsExpenseMaster.name = name
            objclsExpenseMaster.is_active = is_active
            objclsExpenseMaster.is_expense = is_expense
            objclsExpenseMaster.Ip_address = Ip_address
            If objclsExpenseMaster.Delete() Then
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
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.Exp_id = Exp_id
            dt = objclsExpenseMaster.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            dt = objclsExpenseMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Expense]() As DataTable
        Dim dt As DataTable
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            dt = objclsExpenseMaster.Select_Expense()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_Expense_Bank]() As DataTable
        Dim dt As DataTable
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            dt = objclsExpenseMaster.Select_Expense_Bank()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Expense_Summary]() As DataTable
        Dim dt As DataTable
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.date1 = date1
            objclsExpenseMaster.date2 = date2
            objclsExpenseMaster.location_id = location_id
            dt = objclsExpenseMaster.Expense_Summary()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Expense_Summary_BD]() As DataTable
        Dim dt As DataTable
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.date1 = date1
            objclsExpenseMaster.date2 = date2
            objclsExpenseMaster.location_id = location_id
            objclsExpenseMaster.cmp_id = cmp_id
            objclsExpenseMaster.duration = duration

            dt = objclsExpenseMaster.Expense_Summary_BD()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Bank_Summary]() As DataTable
        Dim dt As DataTable
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.date1 = date1
            objclsExpenseMaster.date2 = date2
            dt = objclsExpenseMaster.Bank_Summary()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Stock_Summary]() As DataTable
        Dim dt As DataTable
        Try
            objclsExpenseMaster = New clsExpenseMaster()
            objclsExpenseMaster.date1 = date1
            objclsExpenseMaster.date2 = date2
            dt = objclsExpenseMaster.Stock_Summary()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsExpenseMaster
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _Exp_id As Integer
    Private _name As String
    Private _is_active As Byte
    Private _Ip_address As String
    Private _Tran_Type As String
    Private _date1 As System.DateTime
    Private _date2 As System.DateTime
    Private _location_id As Int32
    Private _cmp_id As Int32
    Private _is_expense As Byte
    Private _duration As String
    Private objclsExpenseMaster As clsExpenseMaster
#End Region

#Region "Public Property"

    Public Property duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
        End Set
    End Property
    Public Property is_expense() As Byte
        Get
            Return _is_expense
        End Get
        Set(ByVal value As Byte)
            _is_expense = value
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

    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property Exp_id() As Integer
        Get
            Return _Exp_id
        End Get
        Set(ByVal value As Integer)
            _Exp_id = value
        End Set
    End Property
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
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

    Public Property date1() As System.DateTime
        Get
            Return _date1

        End Get
        Set(ByVal value As System.DateTime)
            _date1 = value
        End Set
    End Property
    Public Property date2() As System.DateTime
        Get
            Return _date2

        End Get
        Set(ByVal value As System.DateTime)
            _date2 = value
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
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Exp_id", Exp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_expense", is_expense, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Expense", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Exp_id", Exp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_expense", is_expense, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Expense", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Exp_id", Exp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_expense", is_expense, SqlDbType.TinyInt)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Expense", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Exp_id", Exp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Expense_Master", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Expense_Master", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Expense]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@isBank", 0, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Expense", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select_Expense_Bank]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@isBank", 1, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Expense", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Expense_Summary]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@From_Date", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@To_Date", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@LocationId", location_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Daily_Expense", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Expense_Summary_BD]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@From_Date", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@To_Date", date2, SqlDbType.DateTime)
        oColSqlparram.Add("@LocationId", location_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@duration", duration)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Daily_Expense_BD", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Bank_Summary]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@From_Date", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@To_Date", date2, SqlDbType.DateTime)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Bank_Details", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Stock_Summary]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@From_Date", date1, SqlDbType.DateTime)
        oColSqlparram.Add("@To_Date", date2, SqlDbType.DateTime)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Daily_Stock", oColSqlparram)

        Return dtlogin
    End Function
End Class

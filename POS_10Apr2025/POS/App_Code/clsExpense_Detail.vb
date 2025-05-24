Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsExpense_Detail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _SingleRecord As Integer
    Private _Expense_Detail_id As Integer
    Private _Location_id As Integer
    Private _From_Date As DateTime
    Private _Expense_id As Integer
    Private _PerDay_amount As Decimal
    Private _PerDayTotal_amount As Decimal
    Private _PerWeek_amount As Decimal
    Private _Description As String
    Private _ip_address As String
    Private _Tran_Type As String
    Private _isFix As Int16
    Private _isCash As Int16
    Private objclsstock As clsExpense_Detail
#End Region

#Region "Public Property"
    Public Property SingleRecord() As Integer
        Get
            Return _SingleRecord
        End Get
        Set(ByVal value As Integer)
            _SingleRecord = value
        End Set
    End Property

    Public Property Expense_Detail_id() As Integer
        Get
            Return _Expense_Detail_id
        End Get
        Set(ByVal value As Integer)
            _Expense_Detail_id = value
        End Set
    End Property

    Public Property Location_id() As Integer
        Get
            Return _Location_id
        End Get
        Set(ByVal value As Integer)
            _Location_id = value
        End Set
    End Property

    Public Property Expense_id() As Integer
        Get
            Return _Expense_id
        End Get
        Set(ByVal value As Integer)
            _Expense_id = value
        End Set
    End Property

    Public Property From_Date() As System.DateTime
        Get
            Return _From_Date
        End Get
        Set(ByVal value As System.DateTime)
            _From_Date = value
        End Set
    End Property

    Public Property PerDay_amount() As Decimal
        Get
            Return _PerDay_amount
        End Get
        Set(ByVal value As Decimal)
            _PerDay_amount = value
        End Set
    End Property

    Public Property PerDayTotal_amount() As Decimal
        Get
            Return _PerDayTotal_amount
        End Get
        Set(ByVal value As Decimal)
            _PerDayTotal_amount = value
        End Set
    End Property
    Public Property PerWeek_amount() As Decimal
        Get
            Return _PerWeek_amount
        End Get
        Set(ByVal value As Decimal)
            _PerWeek_amount = value
        End Set
    End Property

    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
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
    Public Property isFix() As Int16
        Get
            Return _isFix
        End Get
        Set(ByVal value As Int16)
            _isFix = value
        End Set
    End Property
    Public Property isCash() As Int16
        Get
            Return _isCash
        End Get
        Set(ByVal value As Int16)
            _isCash = value
        End Set
    End Property

#End Region
    Public Function Insert() As Boolean
        Try
            objclsstock = New clsExpense_Detail()
            objclsstock.Expense_Detail_id = Expense_Detail_id
            objclsstock.Location_id = Location_id
            objclsstock.Expense_id = Expense_id
            objclsstock.From_Date = From_Date
            objclsstock.PerDay_amount = PerDay_amount
            objclsstock.PerDayTotal_amount = PerDayTotal_amount
            objclsstock.ip_address = ip_address
            objclsstock.isFix = isFix
            objclsstock.isCash = isCash
            objclsstock.description = Description
            objclsstock.PerWeek_amount = PerWeek_amount

            If objclsstock.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsstock = New clsExpense_Detail()
            objclsstock.Expense_Detail_id = Expense_Detail_id
            objclsstock.Location_id = Location_id
            objclsstock.Expense_id = Expense_id
            objclsstock.From_Date = From_Date
            objclsstock.PerDay_amount = PerDay_amount
            objclsstock.PerDayTotal_amount = PerDayTotal_amount
            objclsstock.ip_address = ip_address
            If objclsstock.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsstock = New clsExpense_Detail()
            objclsstock.Expense_Detail_id = Expense_Detail_id
            objclsstock.Location_id = Location_id
            objclsstock.Expense_id = Expense_id
            objclsstock.From_Date = From_Date
            objclsstock.PerDay_amount = PerDay_amount
            objclsstock.PerDayTotal_amount = PerDayTotal_amount
            objclsstock.ip_address = ip_address
            If objclsstock.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectExpense]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsExpense_Detail()
            objclsstock.Expense_id = Expense_id
            dt = objclsstock.selectexpense()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectExpenseDetail]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsExpense_Detail()
            dt = objclsstock.selectexpense_detals()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Exepense_grid]() As DataTable
        Dim dt As DataTable
        Try
            objclsstock = New clsExpense_Detail()
            objclsstock.SingleRecord = SingleRecord
            dt = objclsstock.[Select_Exepense_grid]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
Public Class clsExpense_Detail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Public Variables"
    Private _SingleRecord As Integer
    Private _Expense_Detail_id As Integer
    Private _Location_id As Integer
    Private _From_Date As DateTime
    Private _Expense_id As Integer
    Private _PerDay_amount As Decimal
    Private _PerWeek_amount As Decimal
    Private _PerDayTotal_amount As Decimal
    Private _ip_address As String
    Private _Tran_Type As String
    Private _isFix As Int16
    Private _isCash As Int16
    Private _description As String
    Private objclsstock As clsExpense_Detail

#End Region

#Region "Public Property"
    Public Property SingleRecord() As Integer
        Get
            Return _SingleRecord
        End Get
        Set(ByVal value As Integer)
            _SingleRecord = value
        End Set
    End Property

    Public Property Expense_Detail_id() As Integer
        Get
            Return _Expense_Detail_id
        End Get
        Set(ByVal value As Integer)
            _Expense_Detail_id = value
        End Set
    End Property

    Public Property Location_id() As Integer
        Get
            Return _Location_id
        End Get
        Set(ByVal value As Integer)
            _Location_id = value
        End Set
    End Property

    Public Property Expense_id() As Integer
        Get
            Return _Expense_id
        End Get
        Set(ByVal value As Integer)
            _Expense_id = value
        End Set
    End Property

    Public Property From_Date() As System.DateTime
        Get
            Return _From_Date
        End Get
        Set(ByVal value As System.DateTime)
            _From_Date = value
        End Set
    End Property

    Public Property PerDay_amount() As Decimal
        Get
            Return _PerDay_amount
        End Get
        Set(ByVal value As Decimal)
            _PerDay_amount = value
        End Set
    End Property
    Public Property PerWeek_amount() As Decimal
        Get
            Return _PerWeek_amount
        End Get
        Set(ByVal value As Decimal)
            _PerWeek_amount = value
        End Set
    End Property
    Public Property isFix() As Int16
        Get
            Return _isFix
        End Get
        Set(ByVal value As Int16)
            _isFix = value
        End Set
    End Property
    Public Property isCash() As Int16
        Get
            Return _isCash
        End Get
        Set(ByVal value As Int16)
            _isCash = value
        End Set
    End Property
    Public Property PerDayTotal_amount() As Decimal
        Get
            Return _PerDayTotal_amount
        End Get
        Set(ByVal value As Decimal)
            _PerDayTotal_amount = value
        End Set
    End Property
    Public Property description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
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
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Expense_Detail_id", Expense_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Expense_id", Expense_id, SqlDbType.Int)
            oColSqlparram.Add("@From_Date", From_Date, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@PerDay_amount", PerDay_amount, SqlDbType.Decimal)

            oColSqlparram.Add("@PerDayTotal_amount", PerDayTotal_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@is_fix", isFix, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_cash", isCash, SqlDbType.TinyInt)
            oColSqlparram.Add("@description", description, SqlDbType.NVarChar)
            oColSqlparram.Add("@PerWeek_amount", PerWeek_amount, SqlDbType.Decimal)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Expense_Detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Expense_Detail_id", Expense_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Expense_id", Expense_id, SqlDbType.Int)
            oColSqlparram.Add("@From_Date", From_Date, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@PerDay_amount", PerDay_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@PerDayTotal_amount", PerDayTotal_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@is_fix", isFix, SqlDbType.TinyInt)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Expense_Detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Expense_Detail_id", Expense_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@location_id", Location_id, SqlDbType.Int)
            oColSqlparram.Add("@Expense_id", Expense_id, SqlDbType.Int)
            oColSqlparram.Add("@From_Date", From_Date, SqlDbType.DateTime)
            oColSqlparram.Add("@ip_address", ip_address, SqlDbType.NVarChar)
            oColSqlparram.Add("@PerDay_amount", PerDay_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@PerDayTotal_amount", PerDayTotal_amount, SqlDbType.Decimal)
            oColSqlparram.Add("@Tran_Type", "D")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Expense_Detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function selectexpense() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Expense_id", Expense_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Expense_By_Id", oColSqlparram)
        Return dtlogin
    End Function
    Public Function selectexpense_detals() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Expense_Detail_id", Expense_Detail_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Expense_Detail", oColSqlparram)
        Return dtlogin
    End Function

    Public Function [Select_Exepense_grid]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@SingleRecord", SingleRecord, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Expense_grid", oColSqlparram)

        Return dtlogin
    End Function
End Class

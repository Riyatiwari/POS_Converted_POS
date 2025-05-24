Imports Microsoft.VisualBasic
Imports System
Imports System.Data


Public Class Controller_clsExtra
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"

    Private objclsextra As clsextra
    Private _extra_id As Integer
    Private _narration As String
    Private _amount As Decimal
    Private _for_Date As System.DateTime
    Private _row_id As Integer

#End Region

#Region "Public Property"
    Public Property extra_id() As Integer
        Get
            Return _extra_id
        End Get
        Set(ByVal value As Integer)
            _extra_id = value
        End Set
    End Property
    Public Property narration() As String
        Get
            Return _narration
        End Get
        Set(ByVal value As String)
            _narration = value
        End Set
    End Property
    Public Property amount() As Decimal
        Get
            Return _amount
        End Get
        Set(ByVal value As Decimal)
            _amount = value
        End Set
    End Property

    Public Property for_Date() As System.DateTime
        Get
            Return _for_Date
        End Get
        Set(ByVal value As System.DateTime)
            _for_Date = value
        End Set
    End Property

    Public Property row_id() As Integer
        Get
            Return _row_id
        End Get
        Set(ByVal value As Integer)
            _row_id = value
        End Set
    End Property

#End Region

    Public Function Insert() As Boolean
        Try
            objclsextra = New clsextra()
            objclsextra.extra_id = extra_id
            objclsextra.narration = narration
            objclsextra.amount = amount
            objclsextra.for_Date = for_Date

            If objclsextra.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsextra = New clsextra()
            objclsextra.extra_id = extra_id
            objclsextra.narration = narration
            objclsextra.amount = amount
            objclsextra.for_Date = for_Date
            If objclsextra.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsextra = New clsextra()
            objclsextra.extra_id = extra_id
            objclsextra.narration = narration
            objclsextra.amount = amount
            objclsextra.for_Date = for_Date
            If objclsextra.Delete() Then
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
            objclsextra = New clsextra()
            objclsextra.extra_id = extra_id
            dt = objclsextra.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsextra = New clsextra()
            'objclsstock.stock_id = stock_id
            'objclsextra.row_id = row_id
            dt = objclsextra.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsextra
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"

    Private objclsextra As clsextra
    Private _extra_id As Integer
    Private _narration As String
    Private _amount As Decimal
    Private _for_Date As System.DateTime
    Private _row_id As Integer

#End Region

#Region "Public Property"
    Public Property extra_id() As Integer
        Get
            Return _extra_id
        End Get
        Set(ByVal value As Integer)
            _extra_id = value
        End Set
    End Property
    Public Property narration() As String
        Get
            Return _narration
        End Get
        Set(ByVal value As String)
            _narration = value
        End Set
    End Property
    Public Property amount() As Decimal
        Get
            Return _amount
        End Get
        Set(ByVal value As Decimal)
            _amount = value
        End Set
    End Property

    Public Property for_Date() As System.DateTime
        Get
            Return _for_Date
        End Get
        Set(ByVal value As System.DateTime)
            _for_Date = value
        End Set
    End Property

    Public Property row_id() As Integer
        Get
            Return _row_id
        End Get
        Set(ByVal value As Integer)
            _row_id = value
        End Set
    End Property
#End Region
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@extra_id", extra_id)
            oColSqlparram.Add("@Narration", narration)
            oColSqlparram.Add("@Amount", amount, SqlDbType.Decimal)
            oColSqlparram.Add("@For_Date", for_Date, SqlDbType.DateTime)
            oColSqlparram.Add("@Tran_Type", "I")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Sales_Extra", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@extra_id", extra_id)
            oColSqlparram.Add("@Narration", narration)
            oColSqlparram.Add("@Amount", amount, SqlDbType.Decimal)
            oColSqlparram.Add("@For_Date", for_Date, SqlDbType.DateTime)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Sales_Extra", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@extra_id", extra_id)
            oColSqlparram.Add("@Narration", narration)
            oColSqlparram.Add("@Amount", amount, SqlDbType.Decimal)
            oColSqlparram.Add("@For_Date", for_Date, SqlDbType.Date)
            oColSqlparram.Add("@Tran_Type", "D")

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Sales_Extra", oColSqlparram)

            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Extra_id", extra_id, SqlDbType.Int)
        'oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Extra_Id", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@row_id", row_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Extra", oColSqlparram)

        Return dtlogin
    End Function


End Class
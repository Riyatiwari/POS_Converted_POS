Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsSizeMaster
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Product_Price_Id As Integer
    Private _cmp_id As Integer
    Private _PPrice As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_active As Byte
    Private _is_default As Byte
    Private objclsSizeMaster As clsSizeMaster
#End Region

#Region "Public Property"
    Public Property is_default() As Byte
        Get
            Return _is_default
        End Get
        Set(ByVal value As Byte)
            _is_default = value
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
    Public Property Product_Price_Id() As Integer
        Get
            Return _Product_Price_Id
        End Get
        Set(ByVal value As Integer)
            _Product_Price_Id = value
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
    Public Property PPrice() As String
        Get
            Return _PPrice
        End Get
        Set(ByVal value As String)
            _PPrice = value
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
    Public Property login_id() As Integer
        Get
            Return _login_id
        End Get
        Set(ByVal value As Integer)
            _login_id = value
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
#Region "Function"
    Public Function insert() As Boolean
        Try
            objclsSizeMaster = New clsSizeMaster()
            objclsSizeMaster.Product_Price_Id = Product_Price_Id
            objclsSizeMaster.cmp_id = cmp_id
            objclsSizeMaster.PPrice = PPrice
            objclsSizeMaster.Ip_address = Ip_address
            objclsSizeMaster.login_id = login_id
            objclsSizeMaster.is_active = is_active
            objclsSizeMaster.is_default = is_default
            If objclsSizeMaster.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsSizeMaster = New clsSizeMaster()
            objclsSizeMaster.Product_Price_Id = Product_Price_Id
            objclsSizeMaster.cmp_id = cmp_id
            objclsSizeMaster.PPrice = PPrice
            objclsSizeMaster.Ip_address = Ip_address
            objclsSizeMaster.login_id = login_id
            objclsSizeMaster.is_active = is_active
            objclsSizeMaster.is_default = is_default
            If objclsSizeMaster.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsSizeMaster = New clsSizeMaster()
            objclsSizeMaster.Product_Price_Id = Product_Price_Id
            objclsSizeMaster.cmp_id = cmp_id
            objclsSizeMaster.PPrice = PPrice
            objclsSizeMaster.Ip_address = Ip_address
            objclsSizeMaster.login_id = login_id
            objclsSizeMaster.Tran_Type = Tran_Type
            objclsSizeMaster.is_active = is_active
            objclsSizeMaster.is_default = is_default
            If objclsSizeMaster.Delete() Then
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
            objclsSizeMaster = New clsSizeMaster()
            objclsSizeMaster.Product_Price_Id = Product_Price_Id
            objclsSizeMaster.cmp_id = cmp_id
            dt = objclsSizeMaster.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsSizeMaster = New clsSizeMaster()
            'objclsSizeMaster.Product_Price_Id = Product_Price_Id
            objclsSizeMaster.cmp_id = cmp_id
            dt = objclsSizeMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_PPrice]() As DataTable
        Dim dt As DataTable
        Try
            objclsSizeMaster = New clsSizeMaster()
            objclsSizeMaster.cmp_id = cmp_id
            dt = objclsSizeMaster.[Select_PPrice]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_PPrice_For_Gm]() As DataTable
        Dim dt As DataTable
        Try
            objclsSizeMaster = New clsSizeMaster()
            objclsSizeMaster.cmp_id = cmp_id
            dt = objclsSizeMaster.Select_PPrice_For_Gm()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_PPrice_For_Ml]() As DataTable
        Dim dt As DataTable
        Try
            objclsSizeMaster = New clsSizeMaster()
            objclsSizeMaster.cmp_id = cmp_id
            dt = objclsSizeMaster.Select_PPrice_For_Ml()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [SelectUnitNO]() As DataTable
        Dim dt As DataTable
        Try
            objclsSizeMaster = New clsSizeMaster()

            dt = objclsSizeMaster.[SelectUnitNO]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsSizeMaster
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"
    Private _Product_Price_Id As Integer
    Private _cmp_id As Integer
    Private _PPrice As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_active As Byte
    Private _is_default As Byte
    Private objclsSizeMaster As clsSizeMaster
#End Region

#Region "Public Property"

    Public Property is_default() As Byte
        Get
            Return _is_default
        End Get
        Set(ByVal value As Byte)
            _is_default = value
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
    Public Property Product_Price_Id() As Integer
        Get
            Return _Product_Price_Id
        End Get
        Set(ByVal value As Integer)
            _Product_Price_Id = value
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
    Public Property PPrice() As String
        Get
            Return _PPrice
        End Get
        Set(ByVal value As String)
            _PPrice = value
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
    Public Property login_id() As Integer
        Get
            Return _login_id
        End Get
        Set(ByVal value As Integer)
            _login_id = value
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
            oColSqlparram.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@PPrice", PPrice)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_default", is_default, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Price", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@PPrice", PPrice)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_default", is_default, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Price", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@PPrice", PPrice)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_default", is_default, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Price", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Product_Price_Id", Product_Price_Id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_Price", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Product_Price", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_PPrice]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_PPrice_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_PPrice_For_Gm]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_PPrice_By_Cmp_For_Gm", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_PPrice_For_Ml]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_PPrice_By_Cmp_For_Ml", oColSqlparram)

        Return dtlogin
    End Function



    Public Function [SelectUnitNO]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_PPrice", oColSqlparram)

        Return dtlogin
    End Function
End Class

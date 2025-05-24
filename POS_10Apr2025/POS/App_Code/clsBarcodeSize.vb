Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading
Public Class Controller_clsBarcodeSize
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Barcode_Size_id As Integer
    Private _cmp_id As Integer
    Private _Barcode As String
    Private _Size_Id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _product_id As Integer
    Private _is_active As Byte
    Private objclsBarcodeSize As clsBarcodeSize
#End Region

#Region "Public Property"
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property Barcode_Size_id() As Integer
        Get
            Return _Barcode_Size_id
        End Get
        Set(ByVal value As Integer)
            _Barcode_Size_id = value
        End Set
    End Property
    Public Property product_id() As Integer
        Get
            Return _product_id
        End Get
        Set(ByVal value As Integer)
            _product_id = value
        End Set
    End Property
    Public Property Size_Id() As Integer
        Get
            Return _Size_Id
        End Get
        Set(ByVal value As Integer)
            _Size_Id = value
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
    Public Property Barcode() As String
        Get
            Return _Barcode
        End Get
        Set(ByVal value As String)
            _Barcode = value
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
            objclsBarcodeSize = New clsBarcodeSize()
            objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsBarcodeSize.Size_Id = Size_Id
            objclsBarcodeSize.cmp_id = cmp_id
            objclsBarcodeSize.Barcode = Barcode
            objclsBarcodeSize.Ip_address = Ip_address
            objclsBarcodeSize.login_id = login_id
            objclsBarcodeSize.product_id = product_id
            objclsBarcodeSize.is_active = is_active
            If objclsBarcodeSize.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsBarcodeSize = New clsBarcodeSize()
            objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsBarcodeSize.cmp_id = cmp_id
            objclsBarcodeSize.Barcode = Barcode
            objclsBarcodeSize.Size_Id = Size_Id
            objclsBarcodeSize.Ip_address = Ip_address
            objclsBarcodeSize.login_id = login_id
            objclsBarcodeSize.product_id = product_id
            objclsBarcodeSize.is_active = is_active
            If objclsBarcodeSize.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsBarcodeSize = New clsBarcodeSize()
            objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsBarcodeSize.cmp_id = cmp_id
            objclsBarcodeSize.Barcode = Barcode
            objclsBarcodeSize.Size_Id = Size_Id
            objclsBarcodeSize.Ip_address = Ip_address
            objclsBarcodeSize.login_id = login_id
            objclsBarcodeSize.Tran_Type = Tran_Type
            objclsBarcodeSize.product_id = product_id
            objclsBarcodeSize.is_active = is_active
            If objclsBarcodeSize.Delete() Then
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
            objclsBarcodeSize = New clsBarcodeSize()
            objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsBarcodeSize.cmp_id = cmp_id
            dt = objclsBarcodeSize.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeSize = New clsBarcodeSize()
            'objclsBarcodeSize.Barcode_Size_id = Barcode_Size_id
            objclsBarcodeSize.cmp_id = cmp_id
            dt = objclsBarcodeSize.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Barcode]() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeSize = New clsBarcodeSize()
            objclsBarcodeSize.cmp_id = cmp_id
            dt = objclsBarcodeSize.[Select_Barcode]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Barcode_exists]() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeSize = New clsBarcodeSize()
            objclsBarcodeSize.cmp_id = cmp_id
            objclsBarcodeSize.product_id = product_id
            objclsBarcodeSize.Barcode = Barcode
            dt = objclsBarcodeSize.[Select_Barcode_exists]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsBarcodeSize
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()



#Region "Public Variables"
    Private _Barcode_Size_id As Integer
    Private _cmp_id As Integer
    Private _Barcode As String
    Private _Size_Id As Integer
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _product_id As Integer
    Private _is_active As Byte
    Private objclsBarcodeSize As clsBarcodeSize
#End Region

#Region "Public Property"
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
        End Set
    End Property
    Public Property Barcode_Size_id() As Integer
        Get
            Return _Barcode_Size_id
        End Get
        Set(ByVal value As Integer)
            _Barcode_Size_id = value
        End Set
    End Property
    Public Property product_id() As Integer
        Get
            Return _product_id
        End Get
        Set(ByVal value As Integer)
            _product_id = value
        End Set
    End Property
    Public Property Size_Id() As Integer
        Get
            Return _Size_Id
        End Get
        Set(ByVal value As Integer)
            _Size_Id = value
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
    Public Property Barcode() As String
        Get
            Return _Barcode
        End Get
        Set(ByVal value As String)
            _Barcode = value
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
            oColSqlparram.Add("@Barcode_Size_id", Barcode_Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_Id", Size_Id, SqlDbType.Int)
            oColSqlparram.Add("@Barcode", Barcode)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Barcode_Size", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Barcode_Size_id", Barcode_Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Size_Id", Size_Id, SqlDbType.Int)
            oColSqlparram.Add("@Barcode", Barcode)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Barcode_Size", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Barcode_Size_id", Barcode_Size_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Barcode", Barcode)
            oColSqlparram.Add("@Size_Id", Size_Id, SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Barcode_Size", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Barcode_Size_id", Barcode_Size_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Barcode_Size", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Barcode_Size", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Barcode]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Barcode_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function


    Public Function [Select_Barcode_exists]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@barcode", Barcode)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Barcode_product", oColSqlparram)

        Return dtlogin
    End Function
End Class


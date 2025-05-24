Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsBarcodeExchange


    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"


    Private _Product_id As Integer
    Private _other_id As Integer
    Private _tsales_id As Integer
    Private _name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _Price As Integer
    Private _Aimage As Byte()
    Private _Tran_Type As String
    Private objclsBarcodeExchangeMaster As clsBarcodeExchange
#End Region
#Region "Public Property"

    Public Property Product_id() As Integer
        Get
            Return _Product_id
        End Get
        Set(ByVal value As Integer)
            _Product_id = value
        End Set
    End Property
    Public Property tsales_id() As Integer
        Get
            Return _tsales_id
        End Get
        Set(ByVal value As Integer)
            _tsales_id = value
        End Set
    End Property
    Public Property other_id() As Integer
        Get
            Return _other_id
        End Get
        Set(ByVal value As Integer)
            _other_id = value
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

    Public Property description() As String

        Get
            Return _description

        End Get
        Set(ByVal value As String)
            _description = value
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

    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property Price() As Integer
        Get
            Return _Price
        End Get
        Set(ByVal value As Integer)
            _Price = value
        End Set
    End Property
    Public Property Aimage() As Byte()
        Get
            Return _Aimage
        End Get
        Set(ByVal value As Byte())
            _Aimage = value
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

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()
            objclsBarcodeExchangeMaster.cmp_id = cmp_id
            dt = objclsBarcodeExchangeMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_Category() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()
            'objclsBarcodeExchangeMaster.cmp_id = cmp_id
            dt = objclsBarcodeExchangeMaster.Select_Category()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_Sendsales() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()
            'objclsBarcodeExchangeMaster.cmp_id = cmp_id
            dt = objclsBarcodeExchangeMaster.Select_Sendsales()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_GetPrice() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()
            'objclsBarcodeExchangeMaster.cmp_id = cmp_id
            dt = objclsBarcodeExchangeMaster.Select_GetPrice()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function update_barstock() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()
            objclsBarcodeExchangeMaster.Product_id = Product_id
            objclsBarcodeExchangeMaster.other_id = other_id
            objclsBarcodeExchangeMaster.tsales_id = tsales_id
            dt = objclsBarcodeExchangeMaster.update_barstock()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function update_GetPrice() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()
            objclsBarcodeExchangeMaster.other_id = other_id
            objclsBarcodeExchangeMaster.Price = Price
            dt = objclsBarcodeExchangeMaster.update_GetPrice()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Select_Items() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()
            'objclsBarcodeExchangeMaster.cmp_id = cmp_id
            dt = objclsBarcodeExchangeMaster.Select_Items()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Get_stockRecords() As DataTable
        Dim dt As DataTable
        Try
            objclsBarcodeExchangeMaster = New clsBarcodeExchange()

            dt = objclsBarcodeExchangeMaster.Get_stockRecords()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class

Public Class clsBarcodeExchange

    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"


    Private _Product_id As Integer
    Private _other_id As Integer
    Private _tsales_id As Integer
    Private _name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _Price As Integer
    Private _Aimage As Byte()
    Private _Tran_Type As String
    Private objclsBarcodeExchangeMaster As clsBarcodeExchange
#End Region

#Region "Public Property"




    Public Property Product_id() As Integer
        Get
            Return _Product_id
        End Get
        Set(ByVal value As Integer)
            _Product_id = value
        End Set
    End Property
    Public Property other_id() As Integer
        Get
            Return _other_id
        End Get
        Set(ByVal value As Integer)
            _other_id = value
        End Set
    End Property
    Public Property tsales_id() As Integer
        Get
            Return _tsales_id
        End Get
        Set(ByVal value As Integer)
            _tsales_id = value
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

    Public Property description() As String
        Get
            Return _description

        End Get
        Set(ByVal value As String)
            _description = value
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

    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property Price() As Integer
        Get
            Return _Price
        End Get
        Set(ByVal value As Integer)
            _Price = value
        End Set
    End Property
    Public Property Aimage() As Byte()
        Get
            Return _Aimage
        End Get
        Set(ByVal value As Byte())
            _Aimage = value
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

    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Allergy", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_Category() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        ' oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_ProductCategories", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_Sendsales() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        ' oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("GetBarstockSales", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_GetPrice() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        ' oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("GetBarstockItems", oColSqlparram)

        Return dtlogin
    End Function
    Public Function update_barstock() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", Product_id, SqlDbType.Int)
        oColSqlparram.Add("@other_id", other_id, SqlDbType.Int)
        oColSqlparram.Add("@tsales_id", tsales_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("update_barstockpush", oColSqlparram)

        Return dtlogin
    End Function
    Public Function update_GetPrice() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Other_id", other_id)
        oColSqlparram.Add("@Price", Price)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("updateBarstockItems", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Select_Items() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        ' oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_ProductItems", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Get_stockRecords() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_barRecords", oColSqlparram)

        Return dtlogin
    End Function

End Class


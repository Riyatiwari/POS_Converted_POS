Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsProductImg
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"


    Private _product_id As Integer
    Private _id As Integer
    Private _image_name As Byte()
    Private _cmp_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime

    Private _Tran_Type As String
    Private objclProdImg As clsProductImg
#End Region
#Region "Public Property"

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
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
    Public Property image_name() As Byte()
        Get
            Return _image_name
        End Get
        Set(value As Byte())
            _image_name = value
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
#End Region
    Public Function Insert() As Boolean
        Try
            objclProdImg = New clsProductImg()
            objclProdImg.product_id = product_id
            objclProdImg.image_name = image_name
            objclProdImg.cmp_id = cmp_id
            objclProdImg.Tran_Type = Tran_Type
            If objclProdImg.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function delete_image() As Boolean
        Try
            objclProdImg = New clsProductImg()
            objclProdImg.id = id
            If objclProdImg.delete_image() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Selectpro]() As DataTable
        Dim dt As DataTable
        Try
            objclProdImg = New clsProductImg()

            dt = objclProdImg.[Selectpro]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclProdImg = New clsProductImg()
            objclProdImg.product_id = product_id
            objclProdImg.image_name = image_name
            objclProdImg.cmp_id = cmp_id
            objclProdImg.Tran_Type = Tran_Type
            If objclProdImg.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclProdImg = New clsProductImg()
            objclProdImg.product_id = product_id
            objclProdImg.image_name = image_name
            objclProdImg.cmp_id = cmp_id
            objclProdImg.Tran_Type = Tran_Type
            If objclProdImg.Update() Then
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
            objclProdImg = New clsProductImg()
            objclProdImg.product_id = product_id
            objclProdImg.cmp_id = cmp_id
            dt = objclProdImg.[Selectimg]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsProductImg
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"


    Private _product_id As Integer
    Private _id As Integer
    Private _image_name As Byte()
    Private _cmp_id As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime

    Private _Tran_Type As String
    Private objclProdImg As clsProductImg
#End Region
#Region "Public Property"


    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
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
    Public Property image_name() As Byte()
        Get
            Return _image_name
        End Get
        Set(value As Byte())
            _image_name = value
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

    Public Property created_date() As System.DateTime
        Get
            Return _created_date
        End Get
        Set(ByVal value As System.DateTime)
            _created_date = value
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
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@image_name ", image_name, SqlDbType.Image)
            If Not image_name Is Nothing Then
                ' oColSqlparram.Add("@image_name", image_name, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Image", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function delete_image() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@id", id, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Image_Delete", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Selectpro]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_ProductInfo", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_AllergyInfo", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Selectimg]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Product_Images", oColSqlparram)

        Return dtlogin
    End Function


    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@image_name ", image_name)
            If Not image_name Is Nothing Then
                oColSqlparram.Add("@image_name", image_name, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Image", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@image_name ", image_name)

            If Not image_name Is Nothing Then
                oColSqlparram.Add("@image_name", image_name, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Product_Image", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

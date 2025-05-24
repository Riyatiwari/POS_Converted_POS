Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsCondiment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Condiment_Id As Integer
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _Condiment As String
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_add_substract As Byte
    Private _quantity As Integer
    Private _unit As Integer
    Private _choices As Integer
    Private _Condiment_Image_id As Integer
    Private _condiment_image As Byte()
    Private _IsBySize As Integer
    Private _sizeID As Decimal
    Private _UseProductCondi As Byte
    Private objclsCondiment As clsCondiment
#End Region

#Region "Public Property"

    Public Property is_add_substract() As Byte
        Get
            Return _is_add_substract
        End Get
        Set(ByVal value As Byte)
            _is_add_substract = value
        End Set
    End Property
    Public Property Condiment_Id() As Integer
        Get
            Return _Condiment_Id
        End Get
        Set(ByVal value As Integer)
            _Condiment_Id = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property Condiment() As String
        Get
            Return _Condiment
        End Get
        Set(ByVal value As String)
            _Condiment = value
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

    Public Property UseProductCondi() As Byte
        Get
            Return _UseProductCondi
        End Get
        Set(ByVal value As Byte)
            _UseProductCondi = value
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

    Public Property Quantity() As Integer
        Get
            Return _quantity
        End Get
        Set(ByVal value As Integer)
            _quantity = value
        End Set
    End Property


    Public Property Unit() As Integer
        Get
            Return _unit
        End Get
        Set(ByVal value As Integer)
            _unit = value
        End Set
    End Property

    Public Property sizeID() As Decimal
        Get
            Return _sizeID
        End Get
        Set(ByVal value As Decimal)
            _sizeID = value
        End Set
    End Property
    Public Property IsBySize() As Integer
        Get
            Return _IsBySize
        End Get
        Set(ByVal value As Integer)
            _IsBySize = value
        End Set
    End Property

    Public Property choices() As Integer
        Get
            Return _choices
        End Get
        Set(ByVal value As Integer)
            _choices = value
        End Set
    End Property
    Public Property Condiment_Image_id() As Integer
        Get
            Return _Condiment_Image_id
        End Get
        Set(ByVal value As Integer)
            _Condiment_Image_id = value
        End Set
    End Property
    Public Property condiment_image() As Byte()
        Get
            Return _condiment_image
        End Get
        Set(value As Byte())
            _condiment_image = value
        End Set
    End Property

#End Region
#Region "Function"
    Public Function ImgDelete() As Boolean
        Try
            objclsCondiment = New clsCondiment()
            objclsCondiment.Condiment_Image_id = Condiment_Image_id
            objclsCondiment.cmp_id = cmp_id
            objclsCondiment.Tran_Type = Tran_Type
            If objclsCondiment.ImageDelete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [ImageSelect]() As DataTable
        Dim dt As DataTable
        Try
            objclsCondiment = New clsCondiment()
            objclsCondiment.Condiment_Id = Condiment_Id
            objclsCondiment.cmp_id = cmp_id
            dt = objclsCondiment.[Selectimg]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ImageInsert() As Boolean
        Try
            objclsCondiment = New clsCondiment()
            objclsCondiment.Condiment_Id = Condiment_Id
            objclsCondiment.condiment_image = condiment_image
            objclsCondiment.cmp_id = cmp_id
            objclsCondiment.Tran_Type = Tran_Type
            If objclsCondiment.ImageInsert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert() As Integer
        Try
            objclsCondiment = New clsCondiment()
            objclsCondiment.Condiment_Id = Condiment_Id
            objclsCondiment.cmp_id = cmp_id
            objclsCondiment.Condiment = Condiment
            objclsCondiment.ip_address = ip_address
            objclsCondiment.login_id = login_id
            objclsCondiment.is_active = is_active
            objclsCondiment.product_id = product_id
            objclsCondiment.is_add_substract = is_add_substract
            objclsCondiment.Quantity = Quantity
            objclsCondiment.Unit = Unit
            objclsCondiment.choices = choices
            objclsCondiment.IsBySize = IsBySize
            objclsCondiment.sizeID = sizeID
            objclsCondiment.UseProductCondi = UseProductCondi
            Dim r As Integer
            r = objclsCondiment.Insert()
            Return r


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsCondiment = New clsCondiment()
            objclsCondiment.Condiment_Id = Condiment_Id
            objclsCondiment.cmp_id = cmp_id
            objclsCondiment.Condiment = Condiment
            objclsCondiment.ip_address = ip_address
            objclsCondiment.login_id = login_id
            objclsCondiment.is_active = is_active
            objclsCondiment.product_id = product_id
            objclsCondiment.is_add_substract = is_add_substract
            objclsCondiment.Quantity = Quantity
            objclsCondiment.Unit = Unit
            objclsCondiment.choices = choices
            objclsCondiment.IsBySize = IsBySize
            objclsCondiment.sizeID = sizeID
            objclsCondiment.UseProductCondi = UseProductCondi

            If objclsCondiment.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsCondiment = New clsCondiment()
            objclsCondiment.Condiment_Id = Condiment_Id
            objclsCondiment.cmp_id = cmp_id
            objclsCondiment.Condiment = Condiment
            objclsCondiment.ip_address = ip_address
            objclsCondiment.login_id = login_id
            objclsCondiment.is_active = is_active
            objclsCondiment.product_id = product_id
            objclsCondiment.is_add_substract = is_add_substract
            objclsCondiment.Quantity = Quantity
            objclsCondiment.Unit = Unit
            objclsCondiment.choices = choices
            objclsCondiment.IsBySize = IsBySize
            objclsCondiment.sizeID = sizeID
            objclsCondiment.UseProductCondi = UseProductCondi

            If objclsCondiment.Delete() Then
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
            objclsCondiment = New clsCondiment()
            objclsCondiment.Condiment_Id = Condiment_Id
            objclsCondiment.cmp_id = cmp_id
            dt = objclsCondiment.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SelectDefault() As DataTable
        Dim dt As DataTable
        Try
            objclsCondiment = New clsCondiment()

            dt = objclsCondiment.SelectDefault()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsCondiment = New clsCondiment()
            objclsCondiment.cmp_id = cmp_id
            dt = objclsCondiment.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectCondimentList]() As DataTable
        Dim dt As DataTable
        Try
            objclsCondiment = New clsCondiment()
            'objclsCondiment.product_id = product_id
            objclsCondiment.cmp_id = cmp_id
            dt = objclsCondiment.[SelectCondimentList]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region

End Class
Public Class clsCondiment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


#Region "Public Variables"
    Private _Condiment_Id As Integer
    Private _product_id As Integer
    Private _cmp_id As Integer
    Private _Condiment As String
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _is_add_substract As Byte
    Private _quantity As Integer
    Private _unit As Integer
    Private _choices As Integer
    Private _Condiment_Image_id As Integer
    Private _condiment_image As Byte()
    Private _IsBySize As Integer
    Private _sizeID As Decimal
    Private _UseProductCondi As Byte

    Private objclsCondiment As clsCondiment
#End Region

#Region "Public Property"
    Public Property IsBySize() As Integer
        Get
            Return _IsBySize
        End Get
        Set(ByVal value As Integer)
            _IsBySize = value
        End Set
    End Property

    Public Property is_add_substract() As Byte
        Get
            Return _is_add_substract
        End Get
        Set(ByVal value As Byte)
            _is_add_substract = value
        End Set
    End Property
    Public Property Condiment_Id() As Integer
        Get
            Return _Condiment_Id
        End Get
        Set(ByVal value As Integer)
            _Condiment_Id = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property Condiment() As String
        Get
            Return _Condiment
        End Get
        Set(ByVal value As String)
            _Condiment = value
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

    Public Property UseProductCondi() As Byte
        Get
            Return _UseProductCondi
        End Get
        Set(ByVal value As Byte)
            _UseProductCondi = value
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

    Public Property Quantity() As Integer
        Get
            Return _quantity
        End Get
        Set(ByVal value As Integer)
            _quantity = value
        End Set
    End Property


    Public Property Unit() As Integer
        Get
            Return _unit
        End Get
        Set(ByVal value As Integer)
            _unit = value
        End Set
    End Property
    Public Property sizeID() As Decimal
        Get
            Return _sizeID
        End Get
        Set(ByVal value As Decimal)
            _sizeID = value
        End Set
    End Property
    Public Property choices() As Integer
        Get
            Return _choices
        End Get
        Set(ByVal value As Integer)
            _choices = value
        End Set
    End Property
    Public Property condiment_image() As Byte()
        Get
            Return _condiment_image
        End Get
        Set(value As Byte())
            _condiment_image = value
        End Set
    End Property
    Public Property Condiment_Image_id() As Integer
        Get
            Return _Condiment_Image_id
        End Get
        Set(ByVal value As Integer)
            _Condiment_Image_id = value
        End Set
    End Property
#End Region
#Region "Function"
    Public Function Insert() As Integer
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment", Condiment)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_add_substract", is_add_substract, SqlDbType.TinyInt)
            oColSqlparram.Add("@quntity", Quantity, SqlDbType.Int)
            oColSqlparram.Add("@unit", Unit, SqlDbType.Int)
            oColSqlparram.Add("@choices", choices, SqlDbType.Int)
            oColSqlparram.Add("@IsBySize", IsBySize, SqlDbType.Int)
            oColSqlparram.Add("@sizeID", sizeID, SqlDbType.Decimal)
            oColSqlparram.Add("@UseProductCondi", UseProductCondi, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Condiment", oColSqlparram)
            Return dtlogin.Rows(0)("Condiment_Id")


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment", Condiment)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_add_substract", is_add_substract, SqlDbType.TinyInt)
            oColSqlparram.Add("@quntity", Quantity, SqlDbType.Int)
            oColSqlparram.Add("@unit", Unit, SqlDbType.Int)
            oColSqlparram.Add("@choices", choices, SqlDbType.Int)
            oColSqlparram.Add("@IsBySize", IsBySize, SqlDbType.Int)
            oColSqlparram.Add("@sizeID", sizeID, SqlDbType.Decimal)
            oColSqlparram.Add("@UseProductCondi", UseProductCondi, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Condiment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment", Condiment)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@is_add_substract", is_add_substract, SqlDbType.TinyInt)
            oColSqlparram.Add("@quntity", Quantity, SqlDbType.Int)
            oColSqlparram.Add("@unit", Unit, SqlDbType.Int)
            oColSqlparram.Add("@choices", choices, SqlDbType.Int)
            oColSqlparram.Add("@IsBySize", IsBySize, SqlDbType.Int)
            oColSqlparram.Add("@sizeID", sizeID, SqlDbType.Decimal)
            oColSqlparram.Add("@UseProductCondi", UseProductCondi, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Condiment", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Condiment", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectDefault]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Condiment_Default", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Condiment", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectCondimentList]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@product_id", product_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Condiment_Product", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Selectimg]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Condiment_Images", oColSqlparram)

        Return dtlogin
    End Function
    Public Function ImageInsert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Condiment_Id", Condiment_Id, SqlDbType.Int)
            oColSqlparram.Add("@condiment_image ", condiment_image, SqlDbType.Image)
            If Not condiment_image Is Nothing Then
                ' oColSqlparram.Add("@image_name", image_name, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Condiment_Image", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ImageDelete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@condiment_image_id", Condiment_Image_id, SqlDbType.Int)

            If Not condiment_image Is Nothing Then
                oColSqlparram.Add("@condiment_image", condiment_image, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Condiment_Image", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region

End Class

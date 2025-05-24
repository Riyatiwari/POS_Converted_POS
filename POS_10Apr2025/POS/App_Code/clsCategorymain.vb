Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading


Public Class Controller_clsCategoryMain
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Category_id As Integer
    Private _key_map_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Is_web_available As Byte
    Private _Ip_address As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _Tran_Type As String
    Private _sorting_no As Integer
    Private _form_name As String
    Private _dt As DataTable
    Private _location_id As Integer
    Private _Strlocation_id As String
    Private _click_and_collect As Byte
    Private _deliver As Byte
    Private _Order_at_table As Byte
    Private _description_sales As String
    Private _Aimage As Byte()
    Private objclsCategory As clsCategoryMain
#End Region

#Region "Public Property"
    Public Property Category_id() As Integer
        Get
            Return _Category_id
        End Get
        Set(ByVal value As Integer)
            _Category_id = value
        End Set
    End Property
    Public Property key_map_id() As Integer
        Get
            Return _key_map_id
        End Get
        Set(ByVal value As Integer)
            _key_map_id = value
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
    Public Property Is_web_available() As Byte
        Get
            Return _Is_web_available
        End Get
        Set(ByVal value As Byte)
            _Is_web_available = value
        End Set
    End Property
    Public Property Is_active() As Byte
        Get
            Return _Is_active
        End Get
        Set(ByVal value As Byte)
            _Is_active = value
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
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
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
    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
        End Set
    End Property
    Public Property form_name() As String
        Get
            Return _form_name
        End Get
        Set(ByVal value As String)
            _form_name = value
        End Set
    End Property
    Public Property dt() As DataTable
        Get
            Return _dt
        End Get
        Set(ByVal value As DataTable)
            _dt = value
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
    Public Property Strlocation_id() As String
        Get
            Return _Strlocation_id
        End Get
        Set(ByVal value As String)
            _Strlocation_id = value
        End Set
    End Property
    Public Property click_and_collect() As Byte
        Get
            Return _click_and_collect
        End Get
        Set(ByVal value As Byte)
            _click_and_collect = value
        End Set
    End Property
    Public Property deliver() As Byte
        Get
            Return _deliver
        End Get
        Set(ByVal value As Byte)
            _deliver = value
        End Set
    End Property
    Public Property Order_at_table() As Byte
        Get
            Return _Order_at_table
        End Get
        Set(ByVal value As Byte)
            _Order_at_table = value
        End Set
    End Property
    Public Property Description_sales() As String
        Get
            Return _description_sales
        End Get
        Set(ByVal value As String)
            _description_sales = value
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
#End Region

    'Public Function Insert() As Boolean
    '    Try
    '        objclsCategory = New clsCategorymain()
    '        objclsCategory.Category_id = Category_id
    '        objclsCategory.key_map_id = key_map_id
    '        objclsCategory.cmp_id = cmp_id
    '        objclsCategory.name = name
    '        objclsCategory.description = description
    '        objclsCategory.Is_active = Is_active
    '        objclsCategory.Ip_address = Ip_address
    '        objclsCategory.login_id = login_id
    '        objclsCategory.machine_id = machine_id
    '        objclsCategory.sorting_no = sorting_no
    '        If objclsCategory.Insert() Then
    '            Return True
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Insert() As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.Category_id = Category_id
            objclsCategory.key_map_id = key_map_id
            objclsCategory.cmp_id = cmp_id
            objclsCategory.name = name
            objclsCategory.description = description
            objclsCategory.Is_active = Is_active
            objclsCategory.Ip_address = Ip_address
            objclsCategory.login_id = login_id
            objclsCategory.machine_id = machine_id
            objclsCategory.sorting_no = sorting_no
            objclsCategory.Is_web_available = Is_web_available
            objclsCategory.Strlocation_id = Strlocation_id
            objclsCategory.click_and_collect = click_and_collect
            objclsCategory.deliver = deliver
            objclsCategory.Order_at_table = Order_at_table
            objclsCategory.Description_sales = Description_sales
            objclsCategory.Aimage = Aimage
            Dim dt As New DataTable
            dt = objclsCategory.Insert()
            Return dt


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function Update() As Boolean
    '    Try
    '        objclsCategory = New clsCategorymain()
    '        objclsCategory.Category_id = Category_id
    '        objclsCategory.key_map_id = key_map_id
    '        objclsCategory.cmp_id = cmp_id
    '        objclsCategory.name = name
    '        objclsCategory.description = description
    '        objclsCategory.Is_active = Is_active
    '        objclsCategory.Ip_address = Ip_address
    '        objclsCategory.login_id = login_id
    '        objclsCategory.machine_id = machine_id
    '        objclsCategory.sorting_no = sorting_no
    '        If objclsCategory.Update() Then
    '            Return True
    '        End If
    '        Return False
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Update() As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.Category_id = Category_id
            objclsCategory.key_map_id = key_map_id
            objclsCategory.cmp_id = cmp_id
            objclsCategory.name = name
            objclsCategory.description = description
            objclsCategory.Is_active = Is_active
            objclsCategory.Ip_address = Ip_address
            objclsCategory.login_id = login_id
            objclsCategory.machine_id = machine_id
            objclsCategory.sorting_no = sorting_no
            objclsCategory.Is_web_available = Is_web_available
            objclsCategory.Strlocation_id = Strlocation_id
            objclsCategory.click_and_collect = click_and_collect
            objclsCategory.deliver = deliver
            objclsCategory.Order_at_table = Order_at_table
            objclsCategory.Description_sales = Description_sales
            objclsCategory.Aimage = Aimage
            Dim dt As New DataTable
            dt = objclsCategory.Update()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.Category_id = Category_id
            objclsCategory.key_map_id = key_map_id
            objclsCategory.cmp_id = cmp_id
            objclsCategory.name = name
            objclsCategory.description = description
            objclsCategory.Is_active = Is_active
            objclsCategory.Ip_address = Ip_address
            objclsCategory.login_id = login_id
            objclsCategory.machine_id = machine_id
            objclsCategory.sorting_no = sorting_no
            objclsCategory.Tran_Type = Tran_Type
            objclsCategory.Is_web_available = Is_web_available
            objclsCategory.Strlocation_id = Strlocation_id
            objclsCategory.click_and_collect = click_and_collect
            objclsCategory.deliver = deliver
            objclsCategory.Order_at_table = Order_at_table
            objclsCategory.Description_sales = Description_sales
            objclsCategory.Aimage = Aimage
            If objclsCategory.Delete() Then
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
            objclsCategory = New clsCategoryMain()
            objclsCategory.Category_id = Category_id
            objclsCategory.cmp_id = cmp_id
            dt = objclsCategory.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            'objclsCategory.Category_id = Category_id
            objclsCategory.cmp_id = cmp_id
            dt = objclsCategory.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectCategoryByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.cmp_id = cmp_id
            dt = objclsCategory.[SelectCategoryByCmp]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectProductGroupByCategory() As DataTable
        Dim dt As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.cmp_id = cmp_id
            objclsCategory.Category_id = Category_id
            dt = objclsCategory.SelectProductGroupByCategory()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select1]() As DataTable
        Dim dt As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.cmp_id = cmp_id
            objclsCategory.form_name = form_name
            dt = objclsCategory.[Select1]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectCategoryByCmpALL]() As DataTable
        Dim dt As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.cmp_id = cmp_id
            dt = objclsCategory.[SelectCategoryByCmpALL]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_sorting_no() As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.Category_id = Category_id

            objclsCategory.cmp_id = cmp_id

            Dim dt As New DataTable
            dt = objclsCategory.Update_sorting_no()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_sorting_no_dt() As DataTable
        Try
            objclsCategory = New clsCategoryMain()
            objclsCategory.dt = dt
            Dim dt1 As New DataTable

            objclsCategory.Update_sorting_no_dt()
            'Return dt1
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCategoryMain
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


#Region "Public Variables"
    Private _Category_id As Integer
    Private _key_map_id As Integer
    Private _cmp_id As Integer
    Private _name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Is_active As Byte
    Private _Ip_address As String
    Private _login_id As Integer
    Private _machine_id As Integer
    Private _Tran_Type As String
    Private _sorting_no As Integer
    Private _form_name As String
    Private _dt As DataTable
    Private _Is_web_available As Byte
    Private _location_id As Integer
    Private _Strlocation_id As String
    Private _click_and_collect As Byte
    Private _deliver As Byte
    Private _Order_at_table As Byte
    Private _description_sales As String
    Private _Aimage As Byte()
    Private objclsCategory As clsCategory
#End Region

#Region "Public Property"
    Public Property Description_sales() As String
        Get
            Return _description_sales
        End Get
        Set(ByVal value As String)
            _description_sales = value
        End Set
    End Property
    Public Property Category_id() As Integer
        Get
            Return _Category_id
        End Get
        Set(ByVal value As Integer)
            _Category_id = value
        End Set
    End Property
    Public Property key_map_id() As Integer
        Get
            Return _key_map_id
        End Get
        Set(ByVal value As Integer)
            _key_map_id = value
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
    Public Property Is_active() As Byte
        Get
            Return _Is_active
        End Get
        Set(ByVal value As Byte)
            _Is_active = value
        End Set
    End Property
    Public Property Is_web_available() As Byte
        Get
            Return _Is_web_available
        End Get
        Set(ByVal value As Byte)
            _Is_web_available = value
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
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
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
    Public Property sorting_no() As Integer
        Get
            Return _sorting_no
        End Get
        Set(ByVal value As Integer)
            _sorting_no = value
        End Set
    End Property
    Public Property form_name() As String
        Get
            Return _form_name
        End Get
        Set(ByVal value As String)
            _form_name = value
        End Set
    End Property


    Public Property dt() As DataTable
        Get
            Return _dt
        End Get
        Set(ByVal value As DataTable)
            _dt = value
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
    Public Property Strlocation_id() As String
        Get
            Return _Strlocation_id
        End Get
        Set(ByVal value As String)
            _Strlocation_id = value
        End Set
    End Property
    Public Property click_and_collect() As Byte
        Get
            Return _click_and_collect
        End Get
        Set(ByVal value As Byte)
            _click_and_collect = value
        End Set
    End Property
    Public Property deliver() As Byte
        Get
            Return _deliver
        End Get
        Set(ByVal value As Byte)
            _deliver = value
        End Set
    End Property
    Public Property Order_at_table() As Byte
        Get
            Return _Order_at_table
        End Get
        Set(ByVal value As Byte)
            _Order_at_table = value
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
#End Region

    'Public Function Insert() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
    '        oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@name", name)
    '        oColSqlparram.Add("@description", description)
    '        oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@Ip_address", Ip_address)
    '        oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
    '        oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
    '        oColSqlparram.Add("@Tran_Type", "I")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Category", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Insert() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
            oColSqlparram.Add("@Is_web_available", Is_web_available, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@location_id", Strlocation_id)
            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.TinyInt)
            oColSqlparram.Add("@deliver", deliver, SqlDbType.TinyInt)
            oColSqlparram.Add("@Order_at_table", Order_at_table, SqlDbType.TinyInt)
            oColSqlparram.Add("@description_Sales", Description_sales)
            If Not Aimage Is Nothing Then
                oColSqlparram.Add("@Aimage", Aimage, SqlDbType.Image)
            End If
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainCategory", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function Update() As Boolean
    '    Try
    '        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
    '        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
    '        oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
    '        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
    '        oColSqlparram.Add("@name", name)
    '        oColSqlparram.Add("@description", description)
    '        oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
    '        oColSqlparram.Add("@Ip_address", Ip_address)
    '        oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
    '        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
    '        oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
    '        oColSqlparram.Add("@Tran_Type", "U")
    '        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Category", oColSqlparram)
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Function Update() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
            oColSqlparram.Add("@Is_web_available", Is_web_available, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@location_id", Strlocation_id)
            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.TinyInt)
            oColSqlparram.Add("@deliver", deliver, SqlDbType.TinyInt)
            oColSqlparram.Add("@Order_at_table", Order_at_table, SqlDbType.TinyInt)
            oColSqlparram.Add("@description_Sales", Description_sales)
            If Not Aimage Is Nothing Then
                oColSqlparram.Add("@Aimage", Aimage, SqlDbType.Image)
            End If
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainCategory", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update_sorting_no() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Upd_M_Category_sorting", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@key_map_id", key_map_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description", description)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@sorting_no", sorting_no, SqlDbType.Int)
            oColSqlparram.Add("@Is_web_available", Is_web_available, SqlDbType.TinyInt)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@location_id", Strlocation_id)
            oColSqlparram.Add("@click_and_collect", click_and_collect, SqlDbType.TinyInt)
            oColSqlparram.Add("@deliver", deliver, SqlDbType.TinyInt)
            oColSqlparram.Add("@Order_at_table", Order_at_table, SqlDbType.TinyInt)
            oColSqlparram.Add("@description_Sales", Description_sales)
            If Not Aimage Is Nothing Then
                oColSqlparram.Add("@Aimage", Aimage, SqlDbType.Image)
            End If
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_MainCategory", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_MainCategory", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_MainCategory", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectCategoryByCmp]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Main_Category_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function


    Public Function SelectProductGroupByCategory() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@MainCategoryID", Category_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_ProductGroup_By_MainCategory", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select1]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@form_name", form_name)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Autogenerate_Sorting_No_Master", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectCategoryByCmpALL]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Category_By_Cmp_all", oColSqlparram)

        Return dtlogin
    End Function
    Public Function Update_sorting_no_dt() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@tblCustomers", dt)
            oClsDal.GetdataTableSp("Insert_Customers", oColSqlparram)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
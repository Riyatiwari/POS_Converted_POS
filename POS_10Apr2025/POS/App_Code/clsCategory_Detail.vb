Imports System
Imports System.Data
Imports Microsoft.VisualBasic


Public Class Controller_clsCategory_Detail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

    Private _Category_Detail_id As Integer
    Private _Category_id As Integer
    Private _Machine_id As Integer
    Private _cmp_id As Integer
    Private _ip_address As String
    Private _Login_id As Integer
    Private objclsCategory_Detail As clsCategory_Detail
    Public Property Category_Detail_id() As Integer
        Get
            Return _Category_Detail_id
        End Get
        Set(ByVal value As Integer)
            _Category_Detail_id = value
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
    Public Property Machine_id() As Integer
        Get
            Return _Machine_id
        End Get
        Set(ByVal value As Integer)
            _Machine_id = value
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
    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
        End Set
    End Property
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
        End Set
    End Property


    Public Function Insert() As DataTable
        Try
            objclsCategory_Detail = New clsCategory_Detail()
            objclsCategory_Detail.Category_Detail_id = Category_Detail_id
            objclsCategory_Detail.Category_id = Category_id
            objclsCategory_Detail.Machine_id = Machine_id
            objclsCategory_Detail.cmp_id = cmp_id
            objclsCategory_Detail.ip_address = ip_address
            objclsCategory_Detail.Login_id = Login_id
            Dim dt As New DataTable
            dt = objclsCategory_Detail.Insert()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As DataTable
        Try
            objclsCategory_Detail = New clsCategory_Detail()
            objclsCategory_Detail.Category_Detail_id = Category_Detail_id
            objclsCategory_Detail.Category_id = Category_id
            objclsCategory_Detail.Machine_id = Machine_id
            objclsCategory_Detail.cmp_id = cmp_id
            objclsCategory_Detail.ip_address = ip_address
            objclsCategory_Detail.Login_id = Login_id
            Dim dt As New DataTable
            dt = objclsCategory_Detail.Update()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As DataTable
        Try
            objclsCategory_Detail = New clsCategory_Detail()
            objclsCategory_Detail.Category_Detail_id = Category_Detail_id
            objclsCategory_Detail.Category_id = Category_id
            objclsCategory_Detail.Machine_id = Machine_id
            objclsCategory_Detail.cmp_id = cmp_id
            objclsCategory_Detail.ip_address = ip_address
            objclsCategory_Detail.Login_id = Login_id
            Dim dt As New DataTable
            dt = objclsCategory_Detail.Delete()
            Return dt


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsCategory_Detail = New clsCategory_Detail()
            objclsCategory_Detail.cmp_id = cmp_id
            dt = objclsCategory_Detail.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectMachineByCategory]() As DataTable
        Dim dt As DataTable
        Try
            objclsCategory_Detail = New clsCategory_Detail()
            objclsCategory_Detail.cmp_id = cmp_id
            objclsCategory_Detail.Category_id = Category_id
            dt = objclsCategory_Detail.[SelectMachineByCategory]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
Public Class clsCategory_Detail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Private _Category_Detail_id As Integer
    Private _Category_id As Integer
    Private _Machine_id As Integer
    Private _cmp_id As Integer
    Private _ip_address As String
    Private _Login_id As Integer
    Private objclsCategory_Detail As clsCategory_Detail()
    Public Property Category_Detail_id() As Integer
        Get
            Return _Category_Detail_id
        End Get
        Set(ByVal value As Integer)
            _Category_Detail_id = value
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
    Public Property Machine_id() As Integer
        Get
            Return _Machine_id
        End Get
        Set(ByVal value As Integer)
            _Machine_id = value
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
    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
        End Set
    End Property
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
        End Set
    End Property


    Public Function Insert() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Category_Detail_id", Category_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@Machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", ip_address)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Category_Details", oColSqlparram)
            Return dtlogin

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Category_Detail_id", Category_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@Machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", ip_address)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Category_Details", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Category_Detail_id", Category_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
            oColSqlparram.Add("@Machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", ip_address)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Category_Details", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Machine_Detail", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectMachineByCategory]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Category_id", Category_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Machine_By_Category", oColSqlparram)

        Return dtlogin
    End Function
End Class

Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsMachine_Detail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub


    Private _Machine_Detail_id As Integer
    Private _Machine_id As Integer
    Private _Printer_id As Integer
    Private _Device_id As Integer
    Private _cmp_id As Integer
    Private _ip_address As String
    Private _Login_id As Integer
    Private objclsMachine_Detail As clsMachine_Detail
    Public Property Machine_Detail_id() As Integer
        Get
            Return _Machine_Detail_id
        End Get
        Set(ByVal value As Integer)
            _Machine_Detail_id = value
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

    Public Property Printer_id() As Integer
        Get
            Return _Printer_id
        End Get
        Set(ByVal value As Integer)
            _Printer_id = value
        End Set
    End Property
    Public Property Device_id() As Integer
        Get
            Return _Device_id
        End Get
        Set(ByVal value As Integer)
            _Device_id = value
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
            objclsMachine_Detail = New clsMachine_Detail()
            objclsMachine_Detail.Machine_Detail_id = Machine_Detail_id
            objclsMachine_Detail.Machine_id = Machine_id
            objclsMachine_Detail.Device_id = Device_id
            objclsMachine_Detail.Printer_id = Printer_id
            objclsMachine_Detail.cmp_id = cmp_id
            objclsMachine_Detail.ip_address = ip_address
            objclsMachine_Detail.Login_id = Login_id
            Dim dt As New DataTable
            dt = objclsMachine_Detail.Insert()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As DataTable
        Try
            objclsMachine_Detail = New clsMachine_Detail()
            objclsMachine_Detail.Machine_Detail_id = Machine_Detail_id
            objclsMachine_Detail.Machine_id = Machine_id
            objclsMachine_Detail.Device_id = Device_id
            objclsMachine_Detail.Printer_id = Printer_id
            objclsMachine_Detail.cmp_id = cmp_id
            objclsMachine_Detail.ip_address = ip_address
            objclsMachine_Detail.Login_id = Login_id
            Dim dt As New DataTable
            dt = objclsMachine_Detail.Update()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As DataTable
        Try
            objclsMachine_Detail = New clsMachine_Detail()
            objclsMachine_Detail.Machine_Detail_id = Machine_Detail_id
            objclsMachine_Detail.Machine_id = Machine_id
            objclsMachine_Detail.Device_id = Device_id
            objclsMachine_Detail.Printer_id = Printer_id
            objclsMachine_Detail.cmp_id = cmp_id
            objclsMachine_Detail.ip_address = ip_address
            objclsMachine_Detail.Login_id = Login_id
            Dim dt As New DataTable
            dt = objclsMachine_Detail.Delete()
            Return dt


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine_Detail = New clsMachine_Detail()
            objclsMachine_Detail.cmp_id = cmp_id
            dt = objclsMachine_Detail.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectPrinterDeviceByMachine]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine_Detail = New clsMachine_Detail()
            objclsMachine_Detail.cmp_id = cmp_id
            objclsMachine_Detail.Machine_id = Machine_id
            dt = objclsMachine_Detail.[SelectPrinterDeviceByMachine]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_Device]() As DataTable
        Dim dt As DataTable
        Try
            objclsMachine_Detail = New clsMachine_Detail()
            objclsMachine_Detail.cmp_id = cmp_id
            dt = objclsMachine_Detail.[Select_Device]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsMachine_Detail
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub


    Private _Machine_Detail_id As Integer
    Private _Machine_id As Integer
    Private _Printer_id As Integer
    Private _Device_id As Integer
    Private _cmp_id As Integer
    Private _ip_address As String
    Private _Login_id As Integer
    Private objclsMachine_Detail As clsMachine_Detail()
    Public Property Machine_Detail_id() As Integer
        Get
            Return _Machine_Detail_id
        End Get
        Set(ByVal value As Integer)
            _Machine_Detail_id = value
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
    Public Property Printer_id() As Integer
        Get
            Return _Printer_id
        End Get
        Set(ByVal value As Integer)
            _Printer_id = value
        End Set
    End Property
    Public Property Device_id() As Integer
        Get
            Return _Device_id
        End Get
        Set(ByVal value As Integer)
            _Device_id = value
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
            oColSqlparram.Add("@Machine_Detail_id", Machine_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@Machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Printer_id", Printer_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_id", Device_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", ip_address)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine_Details", oColSqlparram)
            Return dtlogin
           
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Machine_Detail_id", Machine_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@Machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Printer_id", Printer_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_id", Device_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", ip_address)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine_Details", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Machine_Detail_id", Machine_Detail_id, SqlDbType.Int)
            oColSqlparram.Add("@Machine_id", Machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Printer_id", Printer_id, SqlDbType.Int)
            oColSqlparram.Add("@Device_id", Device_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", ip_address)
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Machine_Details", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Printer_Device_Detail", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_Device]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Device_Detail", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectPrinterDeviceByMachine]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", Machine_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_PrinterDevice_By_Machine", oColSqlparram)

        Return dtlogin
    End Function
End Class

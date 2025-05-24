Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsTillShifts
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"


    Private _tillshift_id As Integer
    Private _machine_id As Integer
    Private _shift_name As String
    Private _active As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _Tran_Type As String
    Private _shift_no As Integer
    Private _venue_id As Integer
    Private objclsTillShiftMaster As cls_TillShiftMaster
#End Region
#Region "Public Property"

    Public Property tillshift_id() As Integer
        Get
            Return _tillshift_id
        End Get
        Set(ByVal value As Integer)
            _tillshift_id = value
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

    Public Property venue_id() As Integer
        Get
            Return _venue_id
        End Get
        Set(ByVal value As Integer)
            _venue_id = value
        End Set
    End Property

    Public Property shift_name() As String
        Get
            Return _shift_name
        End Get
        Set(ByVal value As String)
            _shift_name = value
        End Set
    End Property

    Public Property shift_no() As Integer
        Get
            Return _shift_no
        End Get
        Set(ByVal value As Integer)
            _shift_no = value
        End Set
    End Property

    Public Property active() As Integer
        Get
            Return _active
        End Get
        Set(ByVal value As Integer)
            _active = value
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
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.cmp_id = cmp_id
            objclsTillShiftMaster.tillshift_id = tillshift_id
            objclsTillShiftMaster.machine_id = machine_id
            objclsTillShiftMaster.shift_name = shift_name
            objclsTillShiftMaster.active = active
            objclsTillShiftMaster.Tran_Type = Tran_Type
            objclsTillShiftMaster.shift_no = shift_no
            objclsTillShiftMaster.venue_id = venue_id
            If objclsTillShiftMaster.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsertD() As Boolean
        Try
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.cmp_id = cmp_id
            objclsTillShiftMaster.tillshift_id = tillshift_id
            objclsTillShiftMaster.machine_id = machine_id
            'objclsTillShiftMaster.shift_name = shift_name
            objclsTillShiftMaster.active = active
            objclsTillShiftMaster.Tran_Type = Tran_Type
            'objclsTillShiftMaster.shift_no = shift_no
            objclsTillShiftMaster.venue_id = venue_id
            If objclsTillShiftMaster.InsertD() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.cmp_id = cmp_id
            dt = objclsTillShiftMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.cmp_id = cmp_id
            objclsTillShiftMaster.tillshift_id = tillshift_id
            objclsTillShiftMaster.machine_id = machine_id
            objclsTillShiftMaster.shift_name = shift_name
            objclsTillShiftMaster.active = active
            objclsTillShiftMaster.Tran_Type = Tran_Type
            If objclsTillShiftMaster.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.cmp_id = cmp_id
            objclsTillShiftMaster.tillshift_id = tillshift_id
            objclsTillShiftMaster.machine_id = machine_id
            objclsTillShiftMaster.shift_name = shift_name
            objclsTillShiftMaster.active = active
            objclsTillShiftMaster.Tran_Type = Tran_Type
            objclsTillShiftMaster.shift_no = shift_no
            objclsTillShiftMaster.venue_id = venue_id
            If objclsTillShiftMaster.Update() Then
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
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.tillshift_id = tillshift_id
            objclsTillShiftMaster.cmp_id = cmp_id
            dt = objclsTillShiftMaster.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetShiftShortingNo() As DataTable
        Dim dt As DataTable
        Try
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.venue_id = venue_id
            objclsTillShiftMaster.cmp_id = cmp_id
            dt = objclsTillShiftMaster.GetShiftShortingNo()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetShiftVal() As DataTable
        Dim dt As DataTable
        Try
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.venue_id = venue_id
            objclsTillShiftMaster.cmp_id = cmp_id
            objclsTillShiftMaster.shift_no = shift_no
            dt = objclsTillShiftMaster.Get_M_Shift_Val()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetShiftUpdateVal() As DataTable
        Dim dt As DataTable
        Try
            objclsTillShiftMaster = New cls_TillShiftMaster()
            objclsTillShiftMaster.machine_id = machine_id
            objclsTillShiftMaster.cmp_id = cmp_id
            objclsTillShiftMaster.shift_no = shift_no
            objclsTillShiftMaster.tillshift_id = tillshift_id
            objclsTillShiftMaster.shift_name = shift_name
            dt = objclsTillShiftMaster.Get_M_Shift_Update_Val()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



End Class


Public Class cls_TillShiftMaster

    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"


    Private _tillshift_id As Integer
    Private _machine_id As Integer
    Private _shift_name As String
    Private _active As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _Tran_Type As String
    Private _shift_no As Integer
    Private _venue_id As Integer
    Private objclsTillShiftMaster As cls_TillShiftMaster
#End Region

#Region "Public Property"




    Public Property tillshift_id() As Integer
        Get
            Return _tillshift_id
        End Get
        Set(ByVal value As Integer)
            _tillshift_id = value
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

    Public Property shift_name() As String
        Get
            Return _shift_name
        End Get
        Set(ByVal value As String)
            _shift_name = value
        End Set
    End Property
    Public Property active() As Integer
        Get
            Return _active
        End Get
        Set(ByVal value As Integer)
            _active = value
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

    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property

    Public Property shift_no() As Integer
        Get
            Return _shift_no
        End Get
        Set(ByVal value As Integer)
            _shift_no = value
        End Set
    End Property

    Public Property venue_id() As Integer
        Get
            Return _venue_id
        End Get
        Set(ByVal value As Integer)
            _venue_id = value
        End Set
    End Property

#End Region
    Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@tillshift_id ", tillshift_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id ", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@shift_name", shift_name)
            oColSqlparram.Add("@active ", active, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@shift_no ", shift_no, SqlDbType.Int)
            oColSqlparram.Add("@venue_id ", venue_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Till_Shifts", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function InsertD() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@tillshift_id ", tillshift_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id ", machine_id, SqlDbType.Int)
            'oColSqlparram.Add("@shift_name", shift_name)
            oColSqlparram.Add("@active ", active, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            'oColSqlparram.Add("@shift_no ", shift_no, SqlDbType.Int)
            oColSqlparram.Add("@venue_id ", venue_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Till_Shifts_Default", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@tillshift_id ", tillshift_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id ", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@shift_name", shift_name)
            oColSqlparram.Add("@active ", active, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@shift_no", shift_no)
            oColSqlparram.Add("@venue_id ", venue_id, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Till_Shifts", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@tillshift_id ", tillshift_id, SqlDbType.Int)
            oColSqlparram.Add("@machine_id ", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@shift_name", shift_name)
            oColSqlparram.Add("@active ", active, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Till_Shifts", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_TillShifts", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@tillshift_id", tillshift_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_TillShiftsInfo", oColSqlparram)

        Return dtlogin
    End Function
    Public Function GetShiftShortingNo() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_ShiftNoByTill", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Get_M_Shift_Val() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@venue_id", venue_id, SqlDbType.Int)
        oColSqlparram.Add("@shift_no", _shift_no, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Shift_Val", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Get_M_Shift_Update_Val() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
        oColSqlparram.Add("@shift_no", _shift_no, SqlDbType.Int)
        oColSqlparram.Add("@tillshift_id", _tillshift_id, SqlDbType.Int)
        oColSqlparram.Add("@shift_name", _shift_name)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Shift_Update_Val", oColSqlparram)

        Return dtlogin
    End Function


End Class


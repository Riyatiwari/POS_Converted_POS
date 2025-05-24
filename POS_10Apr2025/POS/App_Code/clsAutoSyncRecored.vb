Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsAutoSyncRecored
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"

    Private _AutoSync_Id As Integer
    Private _cmp_id As Integer
    Private _Venue_Id As Integer
    Private _Location_Id As Integer
    Private _Till_Id As Integer
    Private _SyncBtn_No As Integer
    Private _SyncFlag As Integer
    Private _login_id As Integer
    Private _ip_address As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _Page_list As String
    Private _page_name As String
    Private objclsAutoSyncRecored As clsAutoSyncRecored
#End Region
#Region "Public Property"

    Public Property Page_list() As String
        Get
            Return _Page_list
        End Get
        Set(ByVal value As String)
            _Page_list = value
        End Set
    End Property

    Public Property page_name() As String
        Get
            Return _page_name
        End Get
        Set(ByVal value As String)
            _page_name = value
        End Set
    End Property

    Public Property AutoSync_Id() As Integer
        Get
            Return _AutoSync_Id
        End Get
        Set(ByVal value As Integer)
            _AutoSync_Id = value
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

    Public Property Venue_Id() As Integer
        Get
            Return _Venue_Id
        End Get
        Set(ByVal value As Integer)
            _Venue_Id = value
        End Set
    End Property

    Public Property Location_Id() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
        End Set
    End Property

    Public Property Till_Id() As Integer
        Get
            Return _Till_Id
        End Get
        Set(ByVal value As Integer)
            _Till_Id = value
        End Set
    End Property

    Public Property SyncBtn_No() As Integer
        Get
            Return _SyncBtn_No
        End Get
        Set(ByVal value As Integer)
            _SyncBtn_No = value
        End Set
    End Property

    Public Property SyncFlag() As Integer
        Get
            Return _SyncFlag
        End Get
        Set(ByVal value As Integer)
            _SyncFlag = value
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
    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
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

    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
#End Region


    Public Function SelectForm() As DataTable
        Dim dt As DataTable
        Try
            objclsAutoSyncRecored = New clsAutoSyncRecored()
            objclsAutoSyncRecored.cmp_id = cmp_id
            dt = objclsAutoSyncRecored.SelectForm()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert() As Boolean
        Try
            objclsAutoSyncRecored = New clsAutoSyncRecored()
            objclsAutoSyncRecored.cmp_id = cmp_id
            objclsAutoSyncRecored.Venue_Id = Venue_Id
            objclsAutoSyncRecored.Location_Id = Location_Id
            objclsAutoSyncRecored.Till_Id = Till_Id
            objclsAutoSyncRecored.SyncBtn_No = SyncBtn_No
            objclsAutoSyncRecored.SyncFlag = SyncFlag
            objclsAutoSyncRecored.login_id = login_id
            objclsAutoSyncRecored.Page_list = Page_list
            objclsAutoSyncRecored.page_name = page_name
            objclsAutoSyncRecored.Tran_Type = Tran_Type
            If objclsAutoSyncRecored.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class clsAutoSyncRecored
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"

    Private _AutoSync_Id As Integer
    Private _cmp_id As Integer
    Private _Venue_Id As Integer
    Private _Location_Id As Integer
    Private _Till_Id As Integer
    Private _SyncBtn_No As Integer
    Private _SyncFlag As Integer
    Private _login_id As Integer
    Private _ip_address As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _Tran_Type As String
    Private _Page_list As String
    Private _page_name As String
    Private objclsAutoSyncRecored As clsAutoSyncRecored
#End Region
#Region "Public Property"

    Public Property page_name() As String
        Get
            Return _page_name
        End Get
        Set(ByVal value As String)
            _page_name = value
        End Set
    End Property

    Public Property Page_list() As String
        Get
            Return _Page_list
        End Get
        Set(ByVal value As String)
            _Page_list = value
        End Set
    End Property

    Public Property AutoSync_Id() As Integer
        Get
            Return _AutoSync_Id
        End Get
        Set(ByVal value As Integer)
            _AutoSync_Id = value
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

    Public Property Venue_Id() As Integer
        Get
            Return _Venue_Id
        End Get
        Set(ByVal value As Integer)
            _Venue_Id = value
        End Set
    End Property

    Public Property Location_Id() As Integer
        Get
            Return _Location_Id
        End Get
        Set(ByVal value As Integer)
            _Location_Id = value
        End Set
    End Property

    Public Property Till_Id() As Integer
        Get
            Return _Till_Id
        End Get
        Set(ByVal value As Integer)
            _Till_Id = value
        End Set
    End Property

    Public Property SyncBtn_No() As Integer
        Get
            Return _SyncBtn_No
        End Get
        Set(ByVal value As Integer)
            _SyncBtn_No = value
        End Set
    End Property

    Public Property SyncFlag() As Integer
        Get
            Return _SyncFlag
        End Get
        Set(ByVal value As Integer)
            _SyncFlag = value
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
    Public Property ip_address() As String
        Get
            Return _ip_address
        End Get
        Set(ByVal value As String)
            _ip_address = value
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

    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
#End Region

    Public Function SelectForm() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_Id", cmp_id)
        Dim dt As DataTable = oClsDal.GetdataTableSp("Get_MasterPages", oColSqlparram)

        Return dt
    End Function

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Venue_Id", Venue_Id, SqlDbType.Int)
            oColSqlparram.Add("@Location_Id", Location_Id, SqlDbType.Int)
            oColSqlparram.Add("@Till_Id", Till_Id, SqlDbType.Int)
            oColSqlparram.Add("@SyncBtn_No", SyncBtn_No, SqlDbType.Int)
            oColSqlparram.Add("@SyncFlag", SyncFlag, SqlDbType.Int)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@Page_list", Page_list)
            oColSqlparram.Add("@page_name", page_name)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_AutoSyncRecored", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

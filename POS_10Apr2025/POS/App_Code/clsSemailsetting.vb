Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading


Public Class Controller_clsSemailsetting
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Tran_id As Integer
    Private _cmp_id As Integer
    Private _MailServer As String
    Private _MailServer_UserName As String
    Private _MailServer_Password As String
    Private _Port As String
    Private _From_Email As String
    Private _SSL As Byte
    Private _Alias1 As String
    Private _Is_MES As Byte
    Private _MES_URI As String
    Private _Reply_to As String
    Private _S_Type As Byte
    Private _Created_date As System.DateTime
    Private _Modified_date As System.DateTime
    Private _Login_id As Integer
    Private _Ip_address As String
    Private objclsSemailsetting As clsSemailsetting
#End Region

#Region "Public Property"
    Public Property Tran_id() As Integer
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Integer)
            _Tran_id = value
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
    Public Property MailServer() As String
        Get
            Return _MailServer
        End Get
        Set(ByVal value As String)
            _MailServer = value
        End Set
    End Property
    Public Property MailServer_UserName() As String
        Get
            Return _MailServer_UserName
        End Get
        Set(ByVal value As String)
            _MailServer_UserName = value
        End Set
    End Property
    Public Property MailServer_Password() As String
        Get
            Return _MailServer_Password
        End Get
        Set(ByVal value As String)
            _MailServer_Password = value
        End Set
    End Property
    Public Property Port() As String
        Get
            Return _Port
        End Get
        Set(ByVal value As String)
            _Port = value
        End Set
    End Property
    Public Property From_Email() As String
        Get
            Return _From_Email
        End Get
        Set(ByVal value As String)
            _From_Email = value
        End Set
    End Property
    Public Property SSL() As Byte
        Get
            Return _SSL
        End Get
        Set(ByVal value As Byte)
            _SSL = value
        End Set
    End Property
    Public Property Alias1() As String
        Get
            Return _Alias1
        End Get
        Set(ByVal value As String)
            _Alias1 = value
        End Set
    End Property

    Public Property Is_MES() As Byte
        Get
            Return _Is_MES
        End Get
        Set(ByVal value As Byte)
            _Is_MES = value
        End Set
    End Property
    Public Property MES_URI() As String
        Get
            Return _MES_URI
        End Get
        Set(ByVal value As String)
            _MES_URI = value
        End Set
    End Property
    Public Property Reply_to() As String
        Get
            Return _Reply_to
        End Get
        Set(ByVal value As String)
            _Reply_to = value
        End Set
    End Property
    Public Property S_Type() As Byte
        Get
            Return _S_Type
        End Get
        Set(ByVal value As Byte)
            _S_Type = value
        End Set
    End Property

    Public Property Created_date() As System.DateTime
        Get
            Return _Created_date

        End Get
        Set(ByVal value As System.DateTime)
            _Created_date = value
        End Set
    End Property
    Public Property Modified_date() As System.DateTime
        Get
            Return _Modified_date

        End Get
        Set(ByVal value As System.DateTime)
            _Modified_date = value
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

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            objclsSemailsetting = New clsSemailsetting()
            objclsSemailsetting.Tran_id = Tran_id
            objclsSemailsetting.cmp_id = cmp_id
            objclsSemailsetting.MailServer = MailServer
            objclsSemailsetting.MailServer_UserName = MailServer_UserName
            objclsSemailsetting.MailServer_Password = MailServer_Password
            objclsSemailsetting.Port = Port
            objclsSemailsetting.From_Email = From_Email
            objclsSemailsetting.SSL = SSL
            objclsSemailsetting.Alias1 = Alias1
            objclsSemailsetting.Is_MES = Is_MES
            objclsSemailsetting.MES_URI = MES_URI
            objclsSemailsetting.Reply_to = Reply_to
            objclsSemailsetting.S_Type = S_Type
            objclsSemailsetting.Login_id = Login_id
            objclsSemailsetting.Ip_address = Ip_address
            If objclsSemailsetting.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsSemailsetting = New clsSemailsetting()
            objclsSemailsetting.Tran_id = Tran_id
            objclsSemailsetting.cmp_id = cmp_id
            objclsSemailsetting.MailServer = MailServer
            objclsSemailsetting.MailServer_UserName = MailServer_UserName
            objclsSemailsetting.MailServer_Password = MailServer_Password
            objclsSemailsetting.Port = Port
            objclsSemailsetting.From_Email = From_Email
            objclsSemailsetting.SSL = SSL
            objclsSemailsetting.Alias1 = Alias1
            objclsSemailsetting.Is_MES = Is_MES
            objclsSemailsetting.MES_URI = MES_URI
            objclsSemailsetting.Reply_to = Reply_to
            objclsSemailsetting.S_Type = S_Type
            objclsSemailsetting.Login_id = Login_id
            objclsSemailsetting.Ip_address = Ip_address
            If objclsSemailsetting.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsSemailsetting = New clsSemailsetting()
            objclsSemailsetting.Tran_id = Tran_id
            objclsSemailsetting.cmp_id = cmp_id
            objclsSemailsetting.MailServer = MailServer
            objclsSemailsetting.MailServer_UserName = MailServer_UserName
            objclsSemailsetting.MailServer_Password = MailServer_Password
            objclsSemailsetting.Port = Port
            objclsSemailsetting.From_Email = From_Email
            objclsSemailsetting.SSL = SSL
            objclsSemailsetting.Alias1 = Alias1
            objclsSemailsetting.Is_MES = Is_MES
            objclsSemailsetting.MES_URI = MES_URI
            objclsSemailsetting.Reply_to = Reply_to
            objclsSemailsetting.S_Type = S_Type
            objclsSemailsetting.Login_id = Login_id
            objclsSemailsetting.Ip_address = Ip_address
            If objclsSemailsetting.Delete() Then
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
            objclsSemailsetting = New clsSemailsetting()
            objclsSemailsetting.Tran_id = Tran_id
            objclsSemailsetting.cmp_id = cmp_id
            dt = objclsSemailsetting.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectAll() As DataTable
        Dim dt As DataTable
        Try
            objclsSemailsetting = New clsSemailsetting()
            objclsSemailsetting.cmp_id = cmp_id
            dt = objclsSemailsetting.SelectAll()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsSemailsetting
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Tran_id As Integer
    Private _cmp_id As Integer
    Private _MailServer As String
    Private _MailServer_UserName As String
    Private _MailServer_Password As String
    Private _Port As String
    Private _From_Email As String
    Private _SSL As Byte
    Private _Alias1 As String
    Private _Is_MES As Byte
    Private _MES_URI As String
    Private _Reply_to As String
    Private _S_Type As Byte
    Private _Created_date As System.DateTime
    Private _Modified_date As System.DateTime
    Private _Login_id As Integer
    Private _Ip_address As String

#End Region
#Region "Public Property"
    Public Property Tran_id() As Integer
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Integer)
            _Tran_id = value
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
    Public Property MailServer() As String
        Get
            Return _MailServer
        End Get
        Set(ByVal value As String)
            _MailServer = value
        End Set
    End Property
    Public Property MailServer_UserName() As String
        Get
            Return _MailServer_UserName
        End Get
        Set(ByVal value As String)
            _MailServer_UserName = value
        End Set
    End Property
    Public Property MailServer_Password() As String
        Get
            Return _MailServer_Password
        End Get
        Set(ByVal value As String)
            _MailServer_Password = value
        End Set
    End Property
    Public Property Port() As String
        Get
            Return _Port
        End Get
        Set(ByVal value As String)
            _Port = value
        End Set
    End Property
    Public Property From_Email() As String
        Get
            Return _From_Email
        End Get
        Set(ByVal value As String)
            _From_Email = value
        End Set
    End Property
    Public Property SSL() As Byte
        Get
            Return _SSL
        End Get
        Set(ByVal value As Byte)
            _SSL = value
        End Set
    End Property
    Public Property Alias1() As String
        Get
            Return _Alias1
        End Get
        Set(ByVal value As String)
            _Alias1 = value
        End Set
    End Property
    Public Property Is_MES() As Byte
        Get
            Return _Is_MES
        End Get
        Set(ByVal value As Byte)
            _Is_MES = value
        End Set
    End Property
    Public Property MES_URI() As String
        Get
            Return _MES_URI
        End Get
        Set(ByVal value As String)
            _MES_URI = value
        End Set
    End Property
    Public Property Reply_to() As String
        Get
            Return _Reply_to
        End Get
        Set(ByVal value As String)
            _Reply_to = value
        End Set
    End Property
    Public Property S_Type() As Byte
        Get
            Return _S_Type
        End Get
        Set(ByVal value As Byte)
            _S_Type = value
        End Set
    End Property

    Public Property Created_date() As System.DateTime
        Get
            Return _Created_date

        End Get
        Set(ByVal value As System.DateTime)
            _Created_date = value
        End Set
    End Property
    Public Property Modified_date() As System.DateTime
        Get
            Return _Modified_date

        End Get
        Set(ByVal value As System.DateTime)
            _Modified_date = value
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

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property
#End Region
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_id", Tran_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@MailServer", MailServer)
            oColSqlparram.Add("@MailServer_UserName", MailServer_UserName)
            oColSqlparram.Add("@MailServer_Password", MailServer_Password)
            oColSqlparram.Add("@Port", Port)
            oColSqlparram.Add("@From_Email", From_Email)
            oColSqlparram.Add("@SSL", SSL, SqlDbType.TinyInt)
            oColSqlparram.Add("@Alias1", Alias1)
            oColSqlparram.Add("@Is_MES", Is_MES, SqlDbType.TinyInt)
            oColSqlparram.Add("@MES_URI", MES_URI)
            oColSqlparram.Add("@Reply_to", Reply_to)
            oColSqlparram.Add("@S_Type", S_Type)
            oColSqlparram.Add("@Login_id", Login_id)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_S_Email_Setting", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_id", Tran_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@MailServer", MailServer)
            oColSqlparram.Add("@MailServer_UserName", MailServer_UserName)
            oColSqlparram.Add("@MailServer_Password", MailServer_Password)
            oColSqlparram.Add("@Port", Port)
            oColSqlparram.Add("@From_Email", From_Email)
            oColSqlparram.Add("@SSL", SSL, SqlDbType.TinyInt)
            oColSqlparram.Add("@Alias1", Alias1)
            oColSqlparram.Add("@Is_MES", Is_MES, SqlDbType.TinyInt)
            oColSqlparram.Add("@MES_URI", MES_URI)
            oColSqlparram.Add("@Reply_to", Reply_to)
            oColSqlparram.Add("@S_Type", S_Type)
            oColSqlparram.Add("@Login_id", Login_id)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_S_Email_Setting", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_id", Tran_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@MailServer", MailServer)
            oColSqlparram.Add("@MailServer_UserName", MailServer_UserName)
            oColSqlparram.Add("@MailServer_Password", MailServer_Password)
            oColSqlparram.Add("@Port", Port)
            oColSqlparram.Add("@From_Email", From_Email)
            oColSqlparram.Add("@SSL", SSL, SqlDbType.TinyInt)
            oColSqlparram.Add("@Alias1", Alias1)
            oColSqlparram.Add("@Is_MES", Is_MES, SqlDbType.TinyInt)
            oColSqlparram.Add("@MES_URI", MES_URI)
            oColSqlparram.Add("@Reply_to", Reply_to)
            oColSqlparram.Add("@S_Type", S_Type)
            oColSqlparram.Add("@Login_id", Login_id)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_S_Email_Setting", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Tran_id", Tran_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_S_Email_Setting", oColSqlparram)

        Return dtlogin
    End Function

    Public Function SelectAll() As DataTable
       
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_S_Email_Setting", oColSqlparram)

        Return dtlogin
    End Function
End Class
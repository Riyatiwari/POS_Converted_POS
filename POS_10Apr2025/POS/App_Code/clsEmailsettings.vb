Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System

Public Class Controller_clsEmailsettings
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Constructor"
    Public Sub New()
    End Sub
#End Region

#Region "Private Variables"
    Private _Tran_id As Decimal
    Private _Company_id As Decimal
    Private _MailServer As String
    Private _MailServer_UserName As String
    Private _MailServer_Password As String
    Private _Port As String
    Private _From_Email As String
    Private _ssl As Byte
    Private _Alias As String
    Private _is_MES As Byte
    Private _MES_URI As String
    Private _Reply_to As String
    Private _S_Type As Byte
    Private _Ip_address As String
    Private _location_id As Integer
    Private _cmp_id As Integer
    Private objclsEmailsettings As clsEmailsettings
#End Region

#Region "Public Properties"
    Public Property Tran_id() As Decimal
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Decimal)
            _Tran_id = value
        End Set
    End Property
    Public Property Company_id() As Decimal
        Get
            Return _Company_id
        End Get
        Set(ByVal value As Decimal)
            _Company_id = value
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
    Public Property ssl() As Byte
        Get
            Return _ssl
        End Get
        Set(ByVal value As Byte)
            _ssl = value
        End Set
    End Property
    Public Property [Alias]() As String
        Get
            Return _Alias
        End Get
        Set(ByVal value As String)
            _Alias = value
        End Set
    End Property
    Public Property is_MES() As Byte
        Get
            Return _is_MES
        End Get
        Set(ByVal value As Byte)
            _is_MES = value
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
    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
#End Region

#Region "Public Methods"
    Public Function [Select]() As Boolean
        Dim dt As DataTable
        Try
            objclsEmailsettings = New clsEmailsettings()

            objclsEmailsettings.Tran_id = Tran_id

            dt = objclsEmailsettings.[Select]()

            If dt.Rows.Count > 0 Then
                Company_id = Val(dt.Rows(0)("Company_id").ToString)
                MailServer = dt.Rows(0)("MailServer").ToString
                MailServer_UserName = dt.Rows(0)("MailServer_UserName").ToString
                MailServer_Password = dt.Rows(0)("MailServer_Password").ToString
                Port = dt.Rows(0)("Port").ToString
                From_Email = dt.Rows(0)("From_Email").ToString
                ssl = dt.Rows(0)("ssl").ToString
                [Alias] = dt.Rows(0)("Alias").ToString
                is_MES = dt.Rows(0)("is_MES").ToString
                MES_URI = dt.Rows(0)("MES_URI").ToString
                Reply_to = dt.Rows(0)("Reply_to").ToString
                S_Type = dt.Rows(0)("S_Type").ToString
                Ip_address = dt.Rows(0)("Ip_address").ToString
                ' location_id = dt.Rows(0)("location_id").ToString

            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert() As Boolean
        Try
            objclsEmailsettings = New clsEmailsettings()
            objclsEmailsettings.Tran_id = Tran_id
            objclsEmailsettings.Company_id = Company_id
            objclsEmailsettings.MailServer = MailServer
            objclsEmailsettings.MailServer_UserName = MailServer_UserName
            objclsEmailsettings.MailServer_Password = MailServer_Password
            objclsEmailsettings.Port = Port
            objclsEmailsettings.From_Email = From_Email
            objclsEmailsettings.ssl = ssl
            objclsEmailsettings.[Alias] = [Alias]
            objclsEmailsettings.is_MES = is_MES
            objclsEmailsettings.MES_URI = MES_URI
            objclsEmailsettings.Reply_to = Reply_to
            objclsEmailsettings.S_Type = S_Type
            objclsEmailsettings.Ip_address = Ip_address
            objclsEmailsettings.location_id = location_id

            If objclsEmailsettings.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsEmailsettings = New clsEmailsettings()

            objclsEmailsettings.Tran_id = Tran_id
            objclsEmailsettings.Company_id = Company_id
            objclsEmailsettings.MailServer = MailServer
            objclsEmailsettings.MailServer_UserName = MailServer_UserName
            objclsEmailsettings.MailServer_Password = MailServer_Password
            objclsEmailsettings.Port = Port
            objclsEmailsettings.From_Email = From_Email
            objclsEmailsettings.ssl = ssl
            objclsEmailsettings.[Alias] = [Alias]
            objclsEmailsettings.is_MES = is_MES
            objclsEmailsettings.MES_URI = MES_URI
            objclsEmailsettings.Reply_to = Reply_to
            objclsEmailsettings.S_Type = S_Type
            objclsEmailsettings.Ip_address = Ip_address
            objclsEmailsettings.location_id = location_id
            If objclsEmailsettings.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsEmailsettings = New clsEmailsettings()

            objclsEmailsettings.Tran_id = Tran_id
            objclsEmailsettings.Company_id = Company_id
            objclsEmailsettings.MailServer = MailServer
            objclsEmailsettings.MailServer_UserName = MailServer_UserName
            objclsEmailsettings.MailServer_Password = MailServer_Password
            objclsEmailsettings.Port = Port
            objclsEmailsettings.From_Email = From_Email
            objclsEmailsettings.ssl = ssl
            objclsEmailsettings.[Alias] = [Alias]
            objclsEmailsettings.is_MES = is_MES
            objclsEmailsettings.MES_URI = MES_URI
            objclsEmailsettings.Reply_to = Reply_to
            objclsEmailsettings.S_Type = S_Type
            objclsEmailsettings.Ip_address = Ip_address
            objclsEmailsettings.location_id = location_id
            If objclsEmailsettings.Delete() Then

                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Function [SelectByCompany]() As DataTable
    '    Dim dt As DataTable
    '    Try
    '        objclsEmailsettings = New clsEmailsettings()
    '        objclsEmailsettings.Company_id = Company_id
    '        dt = objclsEmailsettings.[SelectByCompany]()
    '        Return dt
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Public Function [SelectByCompany]() As DataTable
        Dim dt As DataTable
        Try
            objclsEmailsettings = New clsEmailsettings()
            objclsEmailsettings.Company_id = Company_id
            dt = objclsEmailsettings.[SelectByCompany]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region


End Class

Public Class clsEmailsettings
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

#Region "Constructor"
    Public Sub New()
    End Sub
#End Region

#Region "Private Variables"
    Private _Tran_id As Decimal
    Private _Company_id As Decimal
    Private _MailServer As String
    Private _MailServer_UserName As String
    Private _MailServer_Password As String
    Private _Port As String
    Private _From_Email As String
    Private _ssl As Byte
    Private _Alias As String
    Private _is_MES As Byte
    Private _MES_URI As String
    Private _Reply_to As String
    Private _S_Type As Byte
    Private _Ip_address As String
    Private _location_id As Integer
    Private _cmp_id As Integer
    Private objclsEmailsettings As clsEmailsettings
#End Region

#Region "Public Properties"
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property Tran_id() As Decimal
        Get
            Return _Tran_id
        End Get
        Set(ByVal value As Decimal)
            _Tran_id = value
        End Set
    End Property
    Public Property Company_id() As Decimal
        Get
            Return _Company_id
        End Get
        Set(ByVal value As Decimal)
            _Company_id = value
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
    Public Property ssl() As Byte
        Get
            Return _ssl
        End Get
        Set(ByVal value As Byte)
            _ssl = value
        End Set
    End Property
    Public Property [Alias]() As String
        Get
            Return _Alias
        End Get
        Set(ByVal value As String)
            _Alias = value
        End Set
    End Property
    Public Property is_MES() As Byte
        Get
            Return _is_MES
        End Get
        Set(ByVal value As Byte)
            _is_MES = value
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
    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
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
#End Region

#Region "Public Methods"
    Public Function [Select]() As DataTable
        Dim dtemailsetting As DataTable
        Try
            dtemailsetting = oClsDal.Getdatatable("select * from S_Email_Settings where Tran_id=" + Tran_id.ToString)

            Return dtemailsetting
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectALL]() As DataTable

        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Company_id)
            Dim dtEmail As DataTable = oClsDal.GetdataTableSp("Get_Email_ALl", oColSqlparram)

            Return dtEmail
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_id", Tran_id)
            oColSqlparram.Add("@Company_id", Company_id, SqlDbType.Int)
            oColSqlparram.Add("@MailServer", MailServer)
            oColSqlparram.Add("@MailServer_UserName", MailServer_UserName)
            oColSqlparram.Add("@MailServer_Password", MailServer_Password)
            oColSqlparram.Add("@Port", Port)
            oColSqlparram.Add("@From_Email", From_Email)
            oColSqlparram.Add("@ssl", ssl, SqlDbType.TinyInt)
            oColSqlparram.Add("@Alias", [Alias])
            oColSqlparram.Add("@is_MES", is_MES, SqlDbType.TinyInt)
            oColSqlparram.Add("@MES_URI", MES_URI)
            oColSqlparram.Add("@Reply_to", Reply_to)
            oColSqlparram.Add("@S_Type", S_Type, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Exec_Type", "I")
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)

            oClsDal.ExecStoredProcedure("P_M_EmailSettings", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_id", Tran_id)
            oColSqlparram.Add("@Company_id", Company_id, SqlDbType.Int)
            oColSqlparram.Add("@MailServer", MailServer)
            oColSqlparram.Add("@MailServer_UserName", MailServer_UserName)
            oColSqlparram.Add("@MailServer_Password", MailServer_Password)
            oColSqlparram.Add("@Port", Port)
            oColSqlparram.Add("@From_Email", From_Email)
            oColSqlparram.Add("@ssl", ssl, SqlDbType.TinyInt)
            oColSqlparram.Add("@Alias", [Alias])
            oColSqlparram.Add("@is_MES", is_MES, SqlDbType.TinyInt)
            oColSqlparram.Add("@MES_URI", MES_URI)
            oColSqlparram.Add("@Reply_to", Reply_to)
            oColSqlparram.Add("@S_Type", S_Type, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Exec_Type", "U")
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)

            oClsDal.ExecStoredProcedure("P_M_EmailSettings", oColSqlparram)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_id", Tran_id)
            oColSqlparram.Add("@Company_id", Company_id, SqlDbType.Int)
            oColSqlparram.Add("@MailServer", MailServer)
            oColSqlparram.Add("@MailServer_UserName", MailServer_UserName)
            oColSqlparram.Add("@MailServer_Password", MailServer_Password)
            oColSqlparram.Add("@Port", Port)
            oColSqlparram.Add("@From_Email", From_Email)
            oColSqlparram.Add("@ssl", ssl, SqlDbType.TinyInt)
            oColSqlparram.Add("@Alias", [Alias])
            oColSqlparram.Add("@is_MES", is_MES, SqlDbType.TinyInt)
            oColSqlparram.Add("@MES_URI", MES_URI)
            oColSqlparram.Add("@Reply_to", Reply_to)
            oColSqlparram.Add("@S_Type", S_Type, SqlDbType.TinyInt)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Exec_Type", "D")
            oColSqlparram.Add("@location_id", location_id, SqlDbType.Int)

            oClsDal.GetdataTableSp("P_M_EmailSettings", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectByCompany]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", Company_id)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Email_setting_id", oColSqlparram)

        Return dtlogin
    End Function

#End Region


End Class
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading


Public Class Controller_clsLogin
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Login_id As Integer
    Private _cmp_id As Integer
    Private _emp_id As Integer
    Private _User_name As String
    Private _Password As String
    Private _NewPassword As String
    Private _Is_active As Byte
    Private _Type As Integer
    Private _Created_date As System.DateTime
    Private _Modified_date As System.DateTime
    Private _Ip_address As String
    Private _OldPassword As String
    Private _email_id As String
    Private _till_code As Integer
    Private _staff_id As Integer
    Private _Store_UUID As String

    Private objclsLogin As clsLogin
#End Region

#Region "Public Property"
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
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
    Public Property emp_id() As Integer
        Get
            Return _emp_id
        End Get
        Set(ByVal value As Integer)
            _emp_id = value
        End Set
    End Property
    Public Property User_name() As String
        Get
            Return _User_name
        End Get
        Set(ByVal value As String)
            _User_name = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property NewPassword() As String
        Get
            Return _NewPassword
        End Get
        Set(ByVal value As String)
            _NewPassword = value
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


    Public Property Type() As Integer
        Get
            Return _Type
        End Get
        Set(ByVal value As Integer)
            _Type = value
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
    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property
    Public Property OldPassword() As String
        Get
            Return _OldPassword
        End Get
        Set(ByVal value As String)
            _OldPassword = value
        End Set
    End Property
    Public Property till_code() As Integer
        Get
            Return _till_code
        End Get
        Set(ByVal value As Integer)
            _till_code = value
        End Set
    End Property
    Public Property email_id() As String
        Get
            Return _email_id
        End Get
        Set(ByVal value As String)
            _email_id = value
        End Set
    End Property
    Public Property staff_id() As Integer
        Get
            Return _staff_id
        End Get
        Set(ByVal value As Integer)
            _staff_id = value
        End Set
    End Property
    Public Property Store_UUID() As String
        Get
            Return _Store_UUID
        End Get
        Set(ByVal value As String)
            _Store_UUID = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Login_id = Login_id
            objclsLogin.cmp_id = cmp_id
            objclsLogin.emp_id = emp_id
            objclsLogin.User_name = User_name
            objclsLogin.Password = Password
            objclsLogin.Is_active = Is_active
            objclsLogin.Type = Type
            objclsLogin.Ip_address = Ip_address
            If objclsLogin.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Login_id = Login_id
            objclsLogin.cmp_id = cmp_id
            objclsLogin.emp_id = emp_id
            objclsLogin.User_name = User_name
            objclsLogin.Password = Password
            objclsLogin.Is_active = Is_active
            objclsLogin.Type = Type
            objclsLogin.Ip_address = Ip_address
            If objclsLogin.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Login_id = Login_id
            objclsLogin.cmp_id = cmp_id
            objclsLogin.emp_id = emp_id
            objclsLogin.User_name = User_name
            objclsLogin.Password = Password
            objclsLogin.Is_active = Is_active
            objclsLogin.Type = Type
            objclsLogin.Ip_address = Ip_address
            If objclsLogin.Delete() Then
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
            objclsLogin = New clsLogin()
            objclsLogin.User_name = User_name
            objclsLogin.Password = Password
            dt = objclsLogin.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [enablelogin]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Store_UUID = Store_UUID

            dt = objclsLogin.[enablelogin]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectUser]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.User_name = User_name
            objclsLogin.Password = Password
            dt = objclsLogin.[SelectUser]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.cmp_id = cmp_id
            dt = objclsLogin.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectByUname]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.User_name = User_name
            dt = objclsLogin.[SelectByUname]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Change]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Login_id = Login_id
            objclsLogin.cmp_id = cmp_id
            objclsLogin.Password = Password
            objclsLogin.NewPassword = NewPassword
            dt = objclsLogin.[Change]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Change_U]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Login_id = Login_id
            objclsLogin.cmp_id = cmp_id
            objclsLogin.NewPassword = NewPassword
            dt = objclsLogin.[Change_U]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Reset]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Login_id = Login_id
            objclsLogin.NewPassword = NewPassword
            dt = objclsLogin.[Reset]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectSignIn]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.till_code = till_code
            dt = objclsLogin.[SelectSignIn]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [ResetPassword]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.cmp_id = cmp_id
            dt = objclsLogin.[ResetPassword]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [ResetPasswordStaffEmail]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.cmp_id = cmp_id
            objclsLogin.staff_id = staff_id
            dt = objclsLogin.[ResetPasswordStaffEmail]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectUsernameForgotPsw]() As DataTable
        Dim dt As DataTable
        Try
            objclsLogin = New clsLogin()
            objclsLogin.Login_id = Login_id
            dt = objclsLogin.[SelectUsernameForgotPsw]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsLogin
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Login_id As Integer
    Private _cmp_id As Integer
    Private _emp_id As Integer
    Private _till_code As Integer
    Private _User_name As String
    Private _Password As String
    Private _NewPassword As String
    Private _Is_active As Byte
    Private _Type As Integer
    Private _Created_date As System.DateTime
    Private _Modified_date As System.DateTime
    Private _Ip_address As String
    Private _OldPassword As String
    Private _email_id As String
    Private _staff_id As Integer
    'Private _Store_UUID As String
    Private Property _Store_UUID As String
#End Region
#Region "Public Property"
    Public Property Login_id() As Integer
        Get
            Return _Login_id
        End Get
        Set(ByVal value As Integer)
            _Login_id = value
        End Set
    End Property
    Public Property till_code() As Integer
        Get
            Return _till_code
        End Get
        Set(ByVal value As Integer)
            _till_code = value
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
    Public Property emp_id() As Integer
        Get
            Return _emp_id
        End Get
        Set(ByVal value As Integer)
            _emp_id = value
        End Set
    End Property
    Public Property User_name() As String
        Get
            Return _User_name
        End Get
        Set(ByVal value As String)
            _User_name = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property NewPassword() As String
        Get
            Return _NewPassword
        End Get
        Set(ByVal value As String)
            _NewPassword = value
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
    Public Property Type() As Integer
        Get
            Return _Type
        End Get
        Set(ByVal value As Integer)
            _Type = value
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

    Public Property Ip_address() As String
        Get
            Return _Ip_address
        End Get
        Set(ByVal value As String)
            _Ip_address = value
        End Set
    End Property
    Public Property OldPassword() As String
        Get
            Return _OldPassword
        End Get
        Set(ByVal value As String)
            _OldPassword = value
        End Set
    End Property
    Public Property email_id() As String
        Get
            Return _email_id
        End Get
        Set(ByVal value As String)
            _email_id = value
        End Set
    End Property
    Public Property staff_id() As Integer
        Get
            Return _staff_id
        End Get
        Set(ByVal value As Integer)
            _staff_id = value
        End Set
    End Property
    Public Property Store_UUID() As String
        Get
            Return _Store_UUID
        End Get
        Set(ByVal value As String)
            _Store_UUID = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@emp_id", emp_id, SqlDbType.Int)
            oColSqlparram.Add("@User_Name", User_name)
            oColSqlparram.Add("@Password", Password)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Type", Type, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Login", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@emp_id", emp_id, SqlDbType.Int)
            oColSqlparram.Add("@User_Name", User_name)
            oColSqlparram.Add("@Password", Password)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Type", Type, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Login", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@emp_id", emp_id, SqlDbType.Int)
            oColSqlparram.Add("@User_Name", User_name)
            oColSqlparram.Add("@Password", Password)
            oColSqlparram.Add("@Is_active", Is_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@Type", Type, SqlDbType.Int)
            oColSqlparram.Add("@Ip_address", Ip_address)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Login", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Username", User_name)
        oColSqlparram.Add("@Password", Password)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("do_Login", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [enablelogin]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@store_uuid", Store_UUID)


        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("enablelogin", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectUser]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Username", User_name)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("get_Login", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Login1", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Change]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
        oColSqlparram.Add("@Password", Password)
        oColSqlparram.Add("@NewPassword", NewPassword)
        oColSqlparram.Add("@Tran_type", "C")
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_ChangePwd", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Change_U]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
        oColSqlparram.Add("@Password", "")
        oColSqlparram.Add("@NewPassword", NewPassword)
        oColSqlparram.Add("@Tran_type", "U")
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_ChangePwd", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectByUname]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Username", User_name)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_ForgotPassword", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Reset]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
        oColSqlparram.Add("@NewPassword", NewPassword)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_ResetPassword", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectSignIn]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@till_code", till_code)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("do_SignIn", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [ResetPassword]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Reset_Password_Template", oColSqlparram)
        Return dtlogin
    End Function
    Public Function [ResetPasswordCmpEmail]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Reset_Pass_Cmp_EmailId", oColSqlparram)
        Return dtlogin
    End Function
    Public Function [ResetPasswordStaffEmail]() As DataTable
        Dim ds As New DataSet
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", Cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Reset_Pass_Staff_EmailId", oColSqlparram)
        Return dtlogin
    End Function

    Public Function [SelectUsernameForgotPsw]() As DataTable

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Login_id", Login_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Username_ForgotPassword", oColSqlparram)

        Return dtlogin
    End Function
End Class
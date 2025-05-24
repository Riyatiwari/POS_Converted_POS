Imports Telerik.Web.UI
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsCustomer
#Region "Public Variables"
    Private _customer_id As Integer
    Private _cmp_id As Integer
    Private _first_name As String
    Private _last_name As String
    Private _email As String
    Private _contact_no As String
    Private _Alternet_contact As String
    Private _address As String
    Private _country_id As Integer
    Private _state_id As Integer
    Private _city_id As Integer
    Private _postal_code As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _other_id As String
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private _profile_id As Integer
    Private _AccountID As Integer
    Private _Is_credit As Byte
    Private _CardNumber As String
    Private _ExpDate As System.DateTime
    Private _CustomerProfile As Byte()
    Private _price_level As Integer
    Private objclsCustomer As clsCustomer

#End Region

#Region "Public Property"

    Public Property CustomerProfile() As Byte()
        Get
            Return _CustomerProfile
        End Get
        Set(ByVal value As Byte())
            _CustomerProfile = value
        End Set
    End Property
    Public Property ExpDate() As System.DateTime
        Get
            Return _ExpDate
        End Get
        Set(ByVal value As System.DateTime)
            _ExpDate = value
        End Set
    End Property
    Public Property CardNumber() As String
        Get
            Return _CardNumber
        End Get
        Set(ByVal value As String)
            _CardNumber = value
        End Set
    End Property
    Public Property Is_credit() As Byte
        Get
            Return _Is_credit
        End Get
        Set(ByVal value As Byte)
            _Is_credit = value
        End Set
    End Property
    Public Property AccountID() As Integer
        Get
            Return _AccountID
        End Get
        Set(ByVal value As Integer)
            _AccountID = value
        End Set
    End Property
    Public Property customer_id() As Integer
        Get
            Return _customer_id
        End Get
        Set(ByVal value As Integer)
            _customer_id = value
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
    Public Property first_name() As String
        Get
            Return _first_name
        End Get
        Set(ByVal value As String)
            _first_name = value
        End Set
    End Property
    Public Property last_name() As String
        Get
            Return _last_name
        End Get
        Set(ByVal value As String)
            _last_name = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Public Property contact_no() As String
        Get
            Return _contact_no
        End Get
        Set(ByVal value As String)
            _contact_no = value
        End Set
    End Property
    Public Property Alternet_contact() As String
        Get
            Return _Alternet_contact
        End Get
        Set(ByVal value As String)
            _Alternet_contact = value
        End Set
    End Property

    Public Property address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property
    Public Property country_id() As Integer
        Get
            Return _country_id
        End Get
        Set(ByVal value As Integer)
            _country_id = value
        End Set
    End Property
    Public Property state_id() As Integer
        Get
            Return _state_id
        End Get
        Set(ByVal value As Integer)
            _state_id = value
        End Set
    End Property
    Public Property city_id() As Integer
        Get
            Return _city_id
        End Get
        Set(ByVal value As Integer)
            _city_id = value
        End Set
    End Property
    Public Property postal_code() As String
        Get
            Return _postal_code
        End Get
        Set(ByVal value As String)
            _postal_code = value
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
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
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
    Public Property other_id() As String
        Get
            Return _other_id
        End Get
        Set(ByVal value As String)
            _other_id = value
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
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property
    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
        End Set
    End Property

    Public Property price_level() As Integer
        Get
            Return _price_level
        End Get
        Set(ByVal value As Integer)
            _price_level = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.customer_id = customer_id
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            objclsCustomer.Alternet_contact = Alternet_contact
            objclsCustomer.address = address
            objclsCustomer.country_id = country_id
            objclsCustomer.state_id = state_id
            objclsCustomer.city_id = city_id
            objclsCustomer.postal_code = postal_code
            objclsCustomer.is_active = is_active
            objclsCustomer.ip_address = ip_address
            objclsCustomer.login_id = login_id
            objclsCustomer.other_id = other_id
            objclsCustomer.machine_id = machine_id
            objclsCustomer.profile_id = profile_id
            objclsCustomer.AccountID = AccountID
            objclsCustomer.price_level = price_level
            If objclsCustomer.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert_upload() As Boolean
        Try
            objclsCustomer = New clsCustomer()

            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            objclsCustomer.Alternet_contact = Alternet_contact
            objclsCustomer.address = address

            If objclsCustomer.Insert_upload() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'change on 17/03/2021
    Public Function Insert_account() As DataTable
        Dim dt As DataTable
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.customer_id = customer_id
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            'objclsCustomer.Alternet_contact = Alternet_contact
            objclsCustomer.address = address
            objclsCustomer.country_id = country_id
            objclsCustomer.state_id = state_id
            objclsCustomer.city_id = city_id
            objclsCustomer.postal_code = postal_code
            'objclsCustomer.is_active = is_active
            'objclsCustomer.ip_address = ip_address
            'objclsCustomer.login_id = login_id
            objclsCustomer.other_id = other_id
            'objclsCustomer.machine_id = machine_id
            objclsCustomer.profile_id = profile_id
            objclsCustomer.AccountID = AccountID
            objclsCustomer.Is_credit = Is_credit
            objclsCustomer.CardNumber = CardNumber
            objclsCustomer.ExpDate = ExpDate
            objclsCustomer.CustomerProfile = CustomerProfile
            objclsCustomer.price_level = price_level
            dt = objclsCustomer.Insert_account()
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.customer_id = customer_id
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            objclsCustomer.Alternet_contact = Alternet_contact
            objclsCustomer.address = address
            objclsCustomer.country_id = country_id
            objclsCustomer.state_id = state_id
            objclsCustomer.city_id = city_id
            objclsCustomer.postal_code = postal_code
            objclsCustomer.is_active = is_active
            objclsCustomer.ip_address = ip_address
            objclsCustomer.login_id = login_id
            objclsCustomer.other_id = other_id
            objclsCustomer.machine_id = machine_id
            objclsCustomer.profile_id = profile_id
            objclsCustomer.AccountID = AccountID
            objclsCustomer.price_level = price_level
            If objclsCustomer.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function Update_account() As DataTable
        Try

            Dim dt As New DataTable

            objclsCustomer = New clsCustomer()
            objclsCustomer.customer_id = customer_id
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            'objclsCustomer.Alternet_contact = Alternet_contact
            objclsCustomer.address = address
            objclsCustomer.country_id = country_id
            objclsCustomer.state_id = state_id
            objclsCustomer.city_id = city_id
            objclsCustomer.postal_code = postal_code
            'objclsCustomer.is_active = is_active
            'objclsCustomer.ip_address = ip_address
            'objclsCustomer.login_id = login_id
            objclsCustomer.other_id = other_id
            'objclsCustomer.machine_id = machine_id
            objclsCustomer.profile_id = profile_id
            objclsCustomer.AccountID = AccountID
            objclsCustomer.Is_credit = Is_credit
            objclsCustomer.CardNumber = CardNumber
            objclsCustomer.ExpDate = ExpDate
            objclsCustomer.CustomerProfile = CustomerProfile
            objclsCustomer.price_level = price_level
            dt = objclsCustomer.Update_account()
            Return dt


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete_account() As Boolean
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.customer_id = customer_id
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            objclsCustomer.Alternet_contact = Alternet_contact
            objclsCustomer.address = address
            objclsCustomer.country_id = country_id
            objclsCustomer.state_id = state_id
            objclsCustomer.city_id = city_id
            objclsCustomer.postal_code = postal_code
            'objclsCustomer.is_active = is_active
            'objclsCustomer.ip_address = ip_address
            'objclsCustomer.login_id = login_id
            objclsCustomer.other_id = other_id
            'objclsCustomer.machine_id = machine_id
            objclsCustomer.profile_id = profile_id
            objclsCustomer.AccountID = AccountID
            objclsCustomer.Is_credit = Is_credit
            objclsCustomer.CardNumber = CardNumber
            objclsCustomer.ExpDate = ExpDate
            objclsCustomer.CustomerProfile = CustomerProfile
            objclsCustomer.price_level = price_level
            If objclsCustomer.Delete_account() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Delete() As Boolean
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.customer_id = customer_id
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            objclsCustomer.Alternet_contact = Alternet_contact
            objclsCustomer.address = address
            objclsCustomer.country_id = country_id
            objclsCustomer.state_id = state_id
            objclsCustomer.city_id = city_id
            objclsCustomer.postal_code = postal_code
            objclsCustomer.is_active = is_active
            objclsCustomer.ip_address = ip_address
            objclsCustomer.login_id = login_id
            objclsCustomer.other_id = other_id
            objclsCustomer.machine_id = machine_id
            objclsCustomer.profile_id = profile_id
            objclsCustomer.AccountID = AccountID
            objclsCustomer.price_level = price_level
            If objclsCustomer.Delete() Then
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
            objclsCustomer = New clsCustomer()
            objclsCustomer.cmp_id = cmp_id
            'objclsCustomer.customer_id = customer_id
            objclsCustomer.AccountID = AccountID

            dt = objclsCustomer.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.cmp_id = cmp_id
            'objclsCustomer.customer_id = customer_id
            dt = objclsCustomer.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select_List]() As DataTable
        Dim dt As DataTable
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.cmp_id = cmp_id
            dt = objclsCustomer.[Select_List]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectByContact]() As DataTable
        Dim dt As DataTable
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.contact_no = contact_no
            dt = objclsCustomer.[SelectByContact]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectByEmail]() As DataTable
        Dim dt As DataTable
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.email = email
            dt = objclsCustomer.[SelectByEmail]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectProfile]() As DataTable
        Dim dt As DataTable
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.cmp_id = cmp_id
            dt = objclsCustomer.[SelectProfile]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_account_UPLOAD() As DataTable
        Dim dt As DataTable
        Try
            objclsCustomer = New clsCustomer()
            objclsCustomer.cmp_id = cmp_id
            objclsCustomer.first_name = first_name
            objclsCustomer.last_name = last_name
            objclsCustomer.email = email
            objclsCustomer.contact_no = contact_no
            objclsCustomer.address = address
            dt = objclsCustomer.Insert_account_UPLOAD()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCustomer
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _customer_id As Integer
    Private _cmp_id As Integer
    Private _first_name As String
    Private _last_name As String
    Private _email As String
    Private _contact_no As String
    Private _Alternet_contact As String
    Private _address As String
    Private _country_id As Integer
    Private _state_id As Integer
    Private _city_id As Integer
    Private _postal_code As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _other_id As String
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private _profile_id As Integer
    Private _AccountID As Integer
    Private _Is_credit As Byte
    Private _CardNumber As String
    Private _ExpDate As System.DateTime
    Private _CustomerProfile As Byte()
    Private objclsCustomer As clsCustomer
    Private _price_level As Integer

#End Region

#Region "Public Property"
    Public Property price_level() As Integer
        Get
            Return _price_level
        End Get
        Set(ByVal value As Integer)
            _price_level = value
        End Set
    End Property
    Public Property CustomerProfile() As Byte()
        Get
            Return _CustomerProfile
        End Get
        Set(ByVal value As Byte())
            _CustomerProfile = value
        End Set
    End Property
    Public Property ExpDate() As System.DateTime
        Get
            Return _ExpDate
        End Get
        Set(ByVal value As System.DateTime)
            _ExpDate = value
        End Set
    End Property
    Public Property CardNumber() As String
        Get
            Return _CardNumber
        End Get
        Set(ByVal value As String)
            _CardNumber = value
        End Set
    End Property
    Public Property Is_credit() As Byte
        Get
            Return _Is_credit
        End Get
        Set(ByVal value As Byte)
            _Is_credit = value
        End Set
    End Property


    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
        End Set
    End Property
    Public Property AccountID() As Integer
        Get
            Return _AccountID
        End Get
        Set(ByVal value As Integer)
            _AccountID = value
        End Set
    End Property
    Public Property customer_id() As Integer
        Get
            Return _customer_id
        End Get
        Set(ByVal value As Integer)
            _customer_id = value
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
    Public Property first_name() As String
        Get
            Return _first_name
        End Get
        Set(ByVal value As String)
            _first_name = value
        End Set
    End Property
    Public Property last_name() As String
        Get
            Return _last_name
        End Get
        Set(ByVal value As String)
            _last_name = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Public Property contact_no() As String
        Get
            Return _contact_no
        End Get
        Set(ByVal value As String)
            _contact_no = value
        End Set
    End Property
    Public Property Alternet_contact() As String
        Get
            Return _Alternet_contact
        End Get
        Set(ByVal value As String)
            _Alternet_contact = value
        End Set
    End Property

    Public Property address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property
    Public Property country_id() As Integer
        Get
            Return _country_id
        End Get
        Set(ByVal value As Integer)
            _country_id = value
        End Set
    End Property
    Public Property state_id() As Integer
        Get
            Return _state_id
        End Get
        Set(ByVal value As Integer)
            _state_id = value
        End Set
    End Property
    Public Property city_id() As Integer
        Get
            Return _city_id
        End Get
        Set(ByVal value As Integer)
            _city_id = value
        End Set
    End Property
    Public Property postal_code() As String
        Get
            Return _postal_code
        End Get
        Set(ByVal value As String)
            _postal_code = value
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
    Public Property is_active() As Byte
        Get
            Return _is_active
        End Get
        Set(ByVal value As Byte)
            _is_active = value
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
    Public Property other_id() As String
        Get
            Return _other_id
        End Get
        Set(ByVal value As String)
            _other_id = value
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
    Public Property machine_id() As Integer
        Get
            Return _machine_id
        End Get
        Set(ByVal value As Integer)
            _machine_id = value
        End Set
    End Property
#End Region



    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@first_name", first_name)
            oColSqlparram.Add("@last_name", last_name)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@postal_code", postal_code)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@AccountID", AccountID, SqlDbType.Int)

            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Price_level_id", _price_level, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Customer", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Insert_upload() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@first_name", first_name)
            oColSqlparram.Add("@last_name", last_name)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@address", address)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Customer_upload", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'change on 17/03/2021
    Public Function Insert_account() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@first_name", first_name)
            oColSqlparram.Add("@last_name", last_name)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@postal_code", postal_code)
            'oColSqlparram.Add("@is_active", is_active)
            'oColSqlparram.Add("@ip_address", ip_address)
            'oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            'oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@AccountID", AccountID, SqlDbType.Int)
            oColSqlparram.Add("@Is_credit", Is_credit)
            oColSqlparram.Add("@CardNumber", CardNumber)
            oColSqlparram.Add("@ExpDate", ExpDate, SqlDbType.DateTime)
            oColSqlparram.Add("@CustomerProfile", CustomerProfile, SqlDbType.Image)

            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@Price_level_id", _price_level, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Account", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@first_name", first_name)
            oColSqlparram.Add("@last_name", last_name)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@postal_code", postal_code)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@AccountID", AccountID, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@Price_level_id", _price_level, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Customer", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'change on 17/03/2021
    Public Function Update_account() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@first_name", first_name)
            oColSqlparram.Add("@last_name", last_name)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@postal_code", postal_code)
            'oColSqlparram.Add("@is_active", is_active)
            'oColSqlparram.Add("@ip_address", ip_address)
            'oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            'oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@AccountID", AccountID, SqlDbType.Int)
            oColSqlparram.Add("@Is_credit", Is_credit)
            oColSqlparram.Add("@CardNumber", CardNumber)
            oColSqlparram.Add("@ExpDate", ExpDate, SqlDbType.DateTime)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@Price_level_id", _price_level, SqlDbType.Int)
            If Not CustomerProfile Is Nothing Then
                oColSqlparram.Add("@CustomerProfile", CustomerProfile, SqlDbType.Image)
            End If

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Account", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'change on 17/03/2021
    Public Function Delete_account() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@first_name", first_name)
            oColSqlparram.Add("@last_name", last_name)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@postal_code", postal_code)
            'oColSqlparram.Add("@is_active", is_active)
            'oColSqlparram.Add("@ip_address", ip_address)
            'oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            'oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@AccountID", AccountID, SqlDbType.Int)
            oColSqlparram.Add("@Is_credit", Is_credit)
            oColSqlparram.Add("@CardNumber", CardNumber)
            oColSqlparram.Add("@ExpDate", ExpDate, SqlDbType.DateTime)
            oColSqlparram.Add("@CustomerProfile", CustomerProfile, SqlDbType.Image)

            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@Price_level_id", _price_level, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_Account", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@first_name", first_name)
            oColSqlparram.Add("@last_name", last_name)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@postal_code", postal_code)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@AccountID", AccountID, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@Price_level_id", _price_level, SqlDbType.Int)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Customer", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        oColSqlparram.Add("@AccountID", AccountID, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Customer", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@customer_id", customer_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Customer", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectByContact]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@contact_no", contact_no)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Customer", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectByEmail]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@email", email)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Customer_email", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [Select_List]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Customer_List", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectProfile]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtProfile As DataTable = oClsDal.GetdataTableSp("Get_M_Profile", oColSqlparram)

        Return dtProfile
    End Function
    Public Function Insert_account_UPLOAD() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@first_name", first_name)
        oColSqlparram.Add("@last_name", last_name)
        oColSqlparram.Add("@email", email)
        oColSqlparram.Add("@contact_no", contact_no)
        oColSqlparram.Add("@address", address)
        Dim dtProfile As DataTable = oClsDal.GetdataTableSp("P_Account_upload", oColSqlparram)

        Return dtProfile
    End Function
End Class
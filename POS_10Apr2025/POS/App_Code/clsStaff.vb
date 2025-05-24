Imports Telerik.Web.UI
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsStaff
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _m_staff_id As Integer
    Private _staff_id As Integer
    Private _cmp_id As Integer
    Private _till_code As String
    Private _till_active As Byte
    Private _photo As String
    Private _staff_code As String
    Private _name As String
    Private _joining_date As System.DateTime
    Private _branch_id As Integer
    Private _department_id As Integer
    Private _designation_id As Integer
    Private _address As String
    Private _country_id As Integer

    Private _state_id As Integer
    Private _city_id As Integer
    Private _national_id As String
    Private _contact_no As String
    Private _email As String

    Private _last_working_date As System.DateTime
    Private _role_id As Integer

    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _other_id As String
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private _Authentication_Code As String
    Private _Function_type_id As String
    Private _is_trainee As Byte
    Private _from_date As System.DateTime
    Private _to_date As System.DateTime
    Private _venue_id As String
    Private _UserUUID As String
    Private _password As String
    Private objclsStaff As clsStaff


#End Region

#Region "Public Property"

    Public Property venue_id() As String
        Get
            Return _venue_id
        End Get
        Set(ByVal value As String)
            _venue_id = value
        End Set
    End Property

    Public Property from_date() As System.DateTime
        Get
            Return _from_date
        End Get
        Set(ByVal value As System.DateTime)
            _from_date = value
        End Set
    End Property
    Public Property to_date() As System.DateTime
        Get
            Return _to_date
        End Get
        Set(ByVal value As System.DateTime)
            _to_date = value
        End Set
    End Property


    Public Property m_staff_id() As Integer
        Get
            Return _m_staff_id
        End Get
        Set(ByVal value As Integer)
            _m_staff_id = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property staff_code() As String
        Get
            Return _staff_code
        End Get
        Set(ByVal value As String)
            _staff_code = value
        End Set
    End Property
    Public Property till_active() As Byte
        Get
            Return _till_active
        End Get
        Set(ByVal value As Byte)
            _till_active = value
        End Set
    End Property
    Public Property photo() As String
        Get
            Return _photo
        End Get
        Set(ByVal value As String)
            _photo = value
        End Set
    End Property
    Public Property till_code() As String
        Get
            Return _till_code
        End Get
        Set(ByVal value As String)
            _till_code = value
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
    Public Property joining_date() As System.DateTime
        Get
            Return _joining_date
        End Get
        Set(ByVal value As System.DateTime)
            _joining_date = value
        End Set
    End Property
    Public Property branch_id() As Integer
        Get
            Return _branch_id
        End Get
        Set(ByVal value As Integer)
            _branch_id = value
        End Set
    End Property
    Public Property department_id() As Integer
        Get
            Return _department_id
        End Get
        Set(ByVal value As Integer)
            _department_id = value
        End Set
    End Property
    Public Property designation_id() As Integer
        Get
            Return _designation_id
        End Get
        Set(ByVal value As Integer)
            _designation_id = value
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
    Public Property national_id() As String
        Get
            Return _national_id
        End Get
        Set(ByVal value As String)
            _national_id = value
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
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Public Property last_working_date() As System.DateTime
        Get
            Return _last_working_date
        End Get
        Set(ByVal value As System.DateTime)
            _last_working_date = value
        End Set
    End Property
    Public Property role_id() As Integer
        Get
            Return _role_id
        End Get
        Set(ByVal value As Integer)
            _role_id = value
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
    Public Property Authentication_Code() As String
        Get
            Return _Authentication_Code
        End Get
        Set(ByVal value As String)
            _Authentication_Code = value
        End Set
    End Property

    Public Property Function_type_id() As String
        Get
            Return _Function_type_id
        End Get
        Set(ByVal value As String)
            _Function_type_id = value
        End Set
    End Property

    Public Property is_trainee() As Byte
        Get
            Return _is_trainee
        End Get
        Set(ByVal value As Byte)
            _is_trainee = value
        End Set
    End Property
    Public Property UserUUID() As String
        Get
            Return _UserUUID
        End Get
        Set(ByVal value As String)
            _UserUUID = value
        End Set
    End Property
    Public Property password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property


#End Region

    Public Function InsertStaffRulesMaster() As DataTable
        Try
            Dim dt As DataTable

            objclsStaff = New clsStaff()
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.name = name
            objclsStaff.is_active = is_active
            dt = objclsStaff.InsertStaffRulesMaster()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateStaffRulesMaster() As DataTable
        Try
            Dim dt As DataTable

            objclsStaff = New clsStaff()
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.name = name
            objclsStaff.is_active = is_active
            dt = objclsStaff.UpdateStaffRulesMaster()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert_Function_type() As Boolean
        Try
            objclsStaff = New clsStaff()
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.name = name
            objclsStaff.Function_type_id = Function_type_id
            If objclsStaff.Insert_Function_type() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function DeleteStaffRulesMaster() As Boolean
        Try
            objclsStaff = New clsStaff()
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.name = name
            objclsStaff.is_active = is_active
            If objclsStaff.DeleteStaffRulesMaster() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function delete_staff_function() As Boolean
        Try
            objclsStaff = New clsStaff()
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.name = name
            objclsStaff.Function_type_id = Function_type_id
            If objclsStaff.delete_staff_function() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Insert() As Boolean
        Try
            objclsStaff = New clsStaff()
            objclsStaff.staff_id = staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_code = staff_code
            objclsStaff.name = name
            objclsStaff.till_active = _till_active
            objclsStaff.photo = _photo
            objclsStaff.joining_date = joining_date
            objclsStaff.branch_id = branch_id
            objclsStaff.department_id = department_id
            objclsStaff.designation_id = designation_id
            objclsStaff.address = address
            objclsStaff.country_id = country_id
            objclsStaff.state_id = state_id
            objclsStaff.city_id = city_id
            objclsStaff.national_id = national_id
            objclsStaff.contact_no = contact_no
            objclsStaff.email = email
            objclsStaff.last_working_date = last_working_date
            objclsStaff.role_id = role_id
            objclsStaff.is_active = is_active
            objclsStaff.ip_address = ip_address
            objclsStaff.login_id = login_id
            objclsStaff.till_code = till_code
            objclsStaff.other_id = other_id
            objclsStaff.machine_id = machine_id
            objclsStaff.Function_type_id = Function_type_id
            objclsStaff.Authentication_Code = Authentication_Code
            objclsStaff.is_trainee = is_trainee
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.venue_id = venue_id
            If objclsStaff.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsStaff = New clsStaff()
            objclsStaff.staff_id = staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_code = staff_code
            objclsStaff.name = name
            objclsStaff.till_active = _till_active
            objclsStaff.photo = _photo
            objclsStaff.joining_date = joining_date
            objclsStaff.branch_id = branch_id
            objclsStaff.department_id = department_id
            objclsStaff.designation_id = designation_id
            objclsStaff.address = address
            objclsStaff.country_id = country_id
            objclsStaff.state_id = state_id
            objclsStaff.city_id = city_id
            objclsStaff.national_id = national_id

            objclsStaff.contact_no = contact_no
            objclsStaff.email = email
            objclsStaff.last_working_date = last_working_date
            objclsStaff.role_id = role_id
            objclsStaff.is_active = is_active
            objclsStaff.ip_address = ip_address
            objclsStaff.login_id = login_id
            objclsStaff.till_code = till_code
            objclsStaff.other_id = other_id
            objclsStaff.machine_id = machine_id
            objclsStaff.Authentication_Code = Authentication_Code
            objclsStaff.Function_type_id = Function_type_id
            objclsStaff.is_trainee = is_trainee
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.venue_id = venue_id
            If objclsStaff.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsStaff = New clsStaff()
            objclsStaff.staff_id = staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_code = staff_code
            objclsStaff.name = name
            objclsStaff.till_active = _till_active
            objclsStaff.photo = _photo
            objclsStaff.joining_date = joining_date
            objclsStaff.branch_id = branch_id
            objclsStaff.department_id = department_id
            objclsStaff.designation_id = designation_id
            objclsStaff.address = address
            objclsStaff.country_id = country_id
            objclsStaff.state_id = state_id
            objclsStaff.city_id = city_id
            objclsStaff.national_id = national_id

            objclsStaff.contact_no = contact_no
            objclsStaff.email = email
            objclsStaff.last_working_date = last_working_date
            objclsStaff.role_id = role_id
            objclsStaff.is_active = is_active
            objclsStaff.ip_address = ip_address
            objclsStaff.login_id = login_id
            objclsStaff.till_code = till_code
            objclsStaff.other_id = other_id
            objclsStaff.machine_id = machine_id
            objclsStaff.Authentication_Code = Authentication_Code
            objclsStaff.Function_type_id = Function_type_id
            objclsStaff.Tran_Type = Tran_Type
            objclsStaff.is_trainee = is_trainee
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.venue_id = venue_id
            If objclsStaff.Delete() Then
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
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_id = staff_id
            dt = objclsStaff.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [GetCompanyLocation]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id

            dt = objclsStaff.[GetCompanyLocation]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            'objclsStaff.staff_id = staff_id
            dt = objclsStaff.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function RegisterWithMail() As Boolean
        Try

            objclsStaff = New clsStaff()
            objclsStaff.staff_id = staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_code = staff_code
            objclsStaff.name = name
            objclsStaff.till_active = _till_active
            objclsStaff.photo = _photo
            objclsStaff.joining_date = joining_date
            objclsStaff.branch_id = branch_id
            objclsStaff.department_id = department_id
            objclsStaff.designation_id = designation_id
            objclsStaff.address = address
            objclsStaff.country_id = country_id
            objclsStaff.state_id = state_id
            objclsStaff.city_id = city_id
            objclsStaff.national_id = national_id
            objclsStaff.contact_no = contact_no
            objclsStaff.email = email
            objclsStaff.last_working_date = last_working_date
            objclsStaff.role_id = role_id
            objclsStaff.is_active = is_active
            objclsStaff.ip_address = ip_address
            objclsStaff.login_id = login_id
            objclsStaff.till_code = till_code
            objclsStaff.other_id = other_id
            objclsStaff.machine_id = machine_id
            objclsStaff.Function_type_id = Function_type_id
            objclsStaff.Authentication_Code = Authentication_Code
            objclsStaff.is_trainee = is_trainee
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.venue_id = venue_id
            objclsStaff.UserUUID = UserUUID
            objclsStaff.password = password
            If objclsStaff.RegisterWithMail() Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function RegisterWithMail_insert() As Boolean
        Try

            objclsStaff = New clsStaff()
            objclsStaff.staff_id = staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_code = staff_code
            objclsStaff.name = name
            objclsStaff.till_active = _till_active
            objclsStaff.photo = _photo
            objclsStaff.joining_date = joining_date
            objclsStaff.branch_id = branch_id
            objclsStaff.department_id = department_id
            objclsStaff.designation_id = designation_id
            objclsStaff.address = address
            objclsStaff.country_id = country_id
            objclsStaff.state_id = state_id
            objclsStaff.city_id = city_id
            objclsStaff.national_id = national_id
            objclsStaff.contact_no = contact_no
            objclsStaff.email = email
            objclsStaff.last_working_date = last_working_date
            objclsStaff.role_id = role_id
            objclsStaff.is_active = is_active
            objclsStaff.ip_address = ip_address
            objclsStaff.login_id = login_id
            objclsStaff.till_code = till_code
            objclsStaff.other_id = other_id
            objclsStaff.machine_id = machine_id
            objclsStaff.Function_type_id = Function_type_id
            objclsStaff.Authentication_Code = Authentication_Code
            objclsStaff.is_trainee = is_trainee
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.venue_id = venue_id
            objclsStaff.UserUUID = UserUUID
            objclsStaff.password = password
            If objclsStaff.RegisterWithMail_insert() Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function [Select_m_staff]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            objclsStaff.m_staff_id = m_staff_id
            dt = objclsStaff.[Select_m_staff]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll_rules]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            'objclsStaff.staff_id = staff_id
            dt = objclsStaff.[SelectAll_rules]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectAll_InOutDetail]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            objclsStaff.from_date = from_date
            objclsStaff.to_date = to_date
            dt = objclsStaff.[SelectAll_InOutDetail]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select_InOutDetail]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_id = staff_id
            objclsStaff.from_date = from_date
            objclsStaff.to_date = to_date
            dt = objclsStaff.[Select_InOutDetail]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function [SelectStaff]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            dt = objclsStaff.[SelectStaff]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function SelectSMaster() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            dt = objclsStaff.SelectSMaster()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function [SelectStaff_new]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            dt = objclsStaff.[SelectStaff_new]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [SelectStaffLoginId]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_id = staff_id
            dt = objclsStaff.[SelectStaffLoginId]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectStaffLoginIdForDiscount]() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            objclsStaff.staff_id = staff_id
            dt = objclsStaff.[SelectStaffLoginId_ForDiscount]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Select_rules() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            dt = objclsStaff.Select_rules()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetFunction_type() As DataTable
        Dim dt As DataTable
        Try
            objclsStaff = New clsStaff()
            objclsStaff.cmp_id = cmp_id
            objclsStaff.m_staff_id = m_staff_id
            dt = objclsStaff.GetFunction_type()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateEmailInMPStaff(cmp_id As Integer, staff_id As Integer, email As String) As DataTable
        Try
            Dim dt As DataTable

            objclsStaff = New clsStaff()
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.name = name
            objclsStaff.is_active = is_active
            dt = objclsStaff.UpdateEmailInMPStaff()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function UpdateEmailInStaff() As DataTable
        Try
            Dim dt As DataTable

            objclsStaff = New clsStaff()
            objclsStaff.m_staff_id = m_staff_id
            objclsStaff.cmp_id = cmp_id
            objclsStaff.name = name
            objclsStaff.is_active = is_active
            dt = objclsStaff.UpdateEmailInStaff()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class


Public Class clsStaff
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _m_staff_id As Integer
    Private _staff_id As Integer
    Private _cmp_id As Integer
    Private _till_code As String
    Private _till_active As Byte
    Private _photo As String
    Private _staff_code As String
    Private _name As String
    Private _joining_date As System.DateTime
    Private _branch_id As Integer
    Private _department_id As Integer
    Private _designation_id As Integer
    Private _address As String
    Private _country_id As Integer

    Private _state_id As Integer
    Private _city_id As Integer
    Private _national_id As String
    Private _contact_no As String
    Private _email As String

    Private _last_working_date As System.DateTime
    Private _role_id As Integer

    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _is_active As Byte
    Private _ip_address As String
    Private _login_id As Integer
    Private _other_id As String
    Private _Tran_Type As String
    Private _machine_id As Integer
    Private _Authentication_Code As String
    Private _Function_type_id As String
    Private _is_trainee As Byte
    Private _from_date As System.DateTime
    Private _to_date As System.DateTime
    Private _venue_id As String
    Private _UserUUID As String
    Private _password As String
    Private objclsStaff As clsStaff

#End Region

#Region "Public Property"

    Public Property venue_id() As String
        Get
            Return _venue_id
        End Get
        Set(ByVal value As String)
            _venue_id = value
        End Set
    End Property

    Public Property from_date() As System.DateTime
        Get
            Return _from_date
        End Get
        Set(ByVal value As System.DateTime)
            _from_date = value
        End Set
    End Property
    Public Property to_date() As System.DateTime
        Get
            Return _to_date
        End Get
        Set(ByVal value As System.DateTime)
            _to_date = value
        End Set
    End Property

    Public Property m_staff_id() As Integer
        Get
            Return _m_staff_id
        End Get
        Set(ByVal value As Integer)
            _m_staff_id = value
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
    Public Property cmp_id() As Integer
        Get
            Return _cmp_id
        End Get
        Set(ByVal value As Integer)
            _cmp_id = value
        End Set
    End Property
    Public Property staff_code() As String
        Get
            Return _staff_code
        End Get
        Set(ByVal value As String)
            _staff_code = value
        End Set
    End Property
    Public Property till_active() As Byte
        Get
            Return _till_active
        End Get
        Set(ByVal value As Byte)
            _till_active = value
        End Set
    End Property
    Public Property photo() As String
        Get
            Return _photo
        End Get
        Set(ByVal value As String)
            _photo = value
        End Set
    End Property
    Public Property till_code() As String
        Get
            Return _till_code
        End Get
        Set(ByVal value As String)
            _till_code = value
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
    Public Property joining_date() As System.DateTime
        Get
            Return _joining_date
        End Get
        Set(ByVal value As System.DateTime)
            _joining_date = value
        End Set
    End Property
    Public Property branch_id() As Integer
        Get
            Return _branch_id
        End Get
        Set(ByVal value As Integer)
            _branch_id = value
        End Set
    End Property
    Public Property department_id() As Integer
        Get
            Return _department_id
        End Get
        Set(ByVal value As Integer)
            _department_id = value
        End Set
    End Property
    Public Property designation_id() As Integer
        Get
            Return _designation_id
        End Get
        Set(ByVal value As Integer)
            _designation_id = value
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
    Public Property national_id() As String
        Get
            Return _national_id
        End Get
        Set(ByVal value As String)
            _national_id = value
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
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Public Property last_working_date() As System.DateTime
        Get
            Return _last_working_date
        End Get
        Set(ByVal value As System.DateTime)
            _last_working_date = value
        End Set
    End Property
    Public Property role_id() As Integer
        Get
            Return _role_id
        End Get
        Set(ByVal value As Integer)
            _role_id = value
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
    Public Property Authentication_Code() As String
        Get
            Return _Authentication_Code
        End Get
        Set(ByVal value As String)
            _Authentication_Code = value
        End Set
    End Property
    Public Property Function_type_id() As String
        Get
            Return _Function_type_id
        End Get
        Set(ByVal value As String)
            _Function_type_id = value
        End Set
    End Property

    Public Property is_trainee() As Byte
        Get
            Return _is_trainee
        End Get
        Set(ByVal value As Byte)
            _is_trainee = value
        End Set
    End Property
    Public Property UserUUID() As String
        Get
            Return _UserUUID
        End Get
        Set(ByVal value As String)
            _UserUUID = value
        End Set
    End Property
    Public Property password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property
#End Region

    Public Function DeleteStaffRulesMaster() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@m_staff_id", m_staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@Tran_Type", "D")
            oColSqlparram.Add("@is_active", is_active)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff_rules", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsertStaffRulesMaster() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@m_staff_id", m_staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@is_active", is_active)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff_rules", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateStaffRulesMaster() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@m_staff_id", m_staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@is_active", is_active)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff_rules", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function Insert_Function_type() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@m_staff_id", m_staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Function_type_id", Function_type_id, SqlDbType.Int)
            oColSqlparram.Add("@f_name", name)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff_rules_detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function delete_staff_function() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@m_staff_id", m_staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@Function_type_id", Function_type_id, SqlDbType.Int)
            oColSqlparram.Add("@f_name", name)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff_rules_detail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@staff_code", staff_code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@till_active", till_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@photo", photo)
            oColSqlparram.Add("@joining_date", joining_date, SqlDbType.DateTime)
            oColSqlparram.Add("@branch_id", branch_id, SqlDbType.Int)
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@designation_id", designation_id, SqlDbType.Int)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@national_id", national_id)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@last_working_date", last_working_date, SqlDbType.DateTime)
            oColSqlparram.Add("@role_id", role_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@till_code", till_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Authentication_Code", Authentication_Code)
            oColSqlparram.Add("@function_id", Function_type_id)
            oColSqlparram.Add("@is_trainee", is_trainee)
            oColSqlparram.Add("@m_staff_id", m_staff_id)
            oColSqlparram.Add("@venue_id", venue_id)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@staff_code", staff_code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@till_active", till_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@photo", photo)
            oColSqlparram.Add("@joining_date", joining_date, SqlDbType.DateTime)
            oColSqlparram.Add("@branch_id", branch_id, SqlDbType.Int)
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@designation_id", designation_id, SqlDbType.Int)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@national_id", national_id)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@last_working_date", last_working_date, SqlDbType.DateTime)
            oColSqlparram.Add("@role_id", role_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@till_code", till_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Authentication_Code", Authentication_Code)
            oColSqlparram.Add("@function_id", Function_type_id)
            oColSqlparram.Add("@is_trainee", is_trainee)
            oColSqlparram.Add("@m_staff_id", m_staff_id)
            oColSqlparram.Add("@venue_id", venue_id)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@staff_code", staff_code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@till_active", till_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@photo", photo)
            oColSqlparram.Add("@joining_date", joining_date, SqlDbType.DateTime)
            oColSqlparram.Add("@branch_id", branch_id, SqlDbType.Int)
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@designation_id", designation_id, SqlDbType.Int)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@national_id", national_id)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@last_working_date", last_working_date, SqlDbType.DateTime)
            oColSqlparram.Add("@role_id", role_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@till_code", till_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Authentication_Code", Authentication_Code)
            oColSqlparram.Add("@function_id", Function_type_id)
            oColSqlparram.Add("@is_trainee", is_trainee)
            oColSqlparram.Add("@m_staff_id", m_staff_id)
            oColSqlparram.Add("@venue_id", venue_id)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Staff", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function RegisterWithMail() As Boolean
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@staff_code", staff_code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@till_active", till_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@photo", photo)
            oColSqlparram.Add("@joining_date", joining_date, SqlDbType.DateTime)
            oColSqlparram.Add("@branch_id", branch_id, SqlDbType.Int)
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@designation_id", designation_id, SqlDbType.Int)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@national_id", national_id)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@last_working_date", last_working_date, SqlDbType.DateTime)
            oColSqlparram.Add("@role_id", role_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@till_code", till_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Authentication_Code", Authentication_Code)
            oColSqlparram.Add("@function_id", Function_type_id)
            oColSqlparram.Add("@is_trainee", is_trainee)
            oColSqlparram.Add("@m_staff_id", m_staff_id)
            oColSqlparram.Add("@venue_id", venue_id)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@UserUUID", UserUUID)
            oColSqlparram.Add("@password", Encrypt(password))
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("RegisterWithMail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function RegisterWithMail_insert() As Boolean
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@staff_code", staff_code)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@till_active", till_active, SqlDbType.TinyInt)
            oColSqlparram.Add("@photo", photo)
            oColSqlparram.Add("@joining_date", joining_date, SqlDbType.DateTime)
            oColSqlparram.Add("@branch_id", branch_id, SqlDbType.Int)
            oColSqlparram.Add("@department_id", department_id, SqlDbType.Int)
            oColSqlparram.Add("@designation_id", designation_id, SqlDbType.Int)
            oColSqlparram.Add("@address", address)
            oColSqlparram.Add("@country_id", country_id, SqlDbType.Int)
            oColSqlparram.Add("@state_id", state_id, SqlDbType.Int)
            oColSqlparram.Add("@city_id", city_id, SqlDbType.Int)
            oColSqlparram.Add("@national_id", national_id)
            oColSqlparram.Add("@contact_no", contact_no)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@last_working_date", last_working_date, SqlDbType.DateTime)
            oColSqlparram.Add("@role_id", role_id, SqlDbType.Int)
            oColSqlparram.Add("@is_active", is_active)
            oColSqlparram.Add("@ip_address", ip_address)
            oColSqlparram.Add("@login_id", login_id, SqlDbType.Int)
            oColSqlparram.Add("@till_code", till_code, SqlDbType.NVarChar)
            oColSqlparram.Add("@other_id", other_id)
            oColSqlparram.Add("@machine_id", machine_id, SqlDbType.Int)
            oColSqlparram.Add("@Authentication_Code", Authentication_Code)
            oColSqlparram.Add("@function_id", Function_type_id)
            oColSqlparram.Add("@is_trainee", is_trainee)
            oColSqlparram.Add("@m_staff_id", m_staff_id)
            oColSqlparram.Add("@venue_id", venue_id)
            oColSqlparram.Add("@Tran_Type", "I")
            oColSqlparram.Add("@UserUUID", UserUUID)
            oColSqlparram.Add("@password", Encrypt(password))
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("RegisterWithMail", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Staff", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [GetCompanyLocation]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("get_autofill_fields", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        'oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Staff", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_m_staff]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@m_staff_id", m_staff_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_staff_rules_master_forEdit", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll_rules]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_staff_rules_master", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectAll_InOutDetail]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@from_date", from_date, SqlDbType.DateTime)
        oColSqlparram.Add("@to_date", to_date, SqlDbType.DateTime)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_staff_InOut_Detail", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_InOutDetail]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
        oColSqlparram.Add("@from_date", from_date, SqlDbType.DateTime)
        oColSqlparram.Add("@to_date", to_date, SqlDbType.DateTime)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_InOut_Detail_By_SatffID", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectStaff]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Staff_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function SelectSMaster() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_staff_rules_master", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectStaff_new]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_StaffName_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [SelectStaffLoginId]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Staff_Login_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [SelectStaffLoginId_ForDiscount]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Staff_Login_By_Cmp_ForDiscount", oColSqlparram)

        Return dtlogin
    End Function

    Public Function Select_rules() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Function_By_Cmp", oColSqlparram)

        Return dtlogin
    End Function

    Public Function GetFunction_type() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        oColSqlparram.Add("@m_staff_id", m_staff_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_Function_by_staff", oColSqlparram)

        Return dtlogin
    End Function

    Public Function UpdateEmailInMPStaff() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
            oColSqlparram.Add("@email", email)
            oColSqlparram.Add("@Tran_Type", "U")
            oColSqlparram.Add("@is_active", 1)
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("", oColSqlparram)

            Return dtlogin
        Catch ex As Exception
            ' Handle the exception or log the error
            LogHelper.Error("UpdateEmailInMPStaff:" + ex.Message)
            Throw
        End Try
    End Function


    Public Function UpdateEmailInStaff() As DataTable
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@staff_id", staff_id, SqlDbType.Int)
            oColSqlparram.Add("@email", email)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            ' Handle the exception or log the error
            LogHelper.Error("UpdateEmailInMStaff:" + ex.Message)
            Throw
        End Try
    End Function


End Class

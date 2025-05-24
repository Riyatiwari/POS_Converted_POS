Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Public Class Controller_clsRole
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Role_Id As Decimal
    Private _Cmp_id As Decimal
    Private _Role_Name As String
    Private _Type As Byte
    Private _is_Active As Byte
    Private _is_View As Byte
    Private _is_add As Byte
    Private _is_Edit As Byte
    Private _is_Delete As Byte
    Private _data As String
    Private _Role_Detail As String
    Private _Form_Name As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Form_Id As Decimal
    Private objclsRole As clsRole
#End Region

#Region "Public Property"

    Public Property Form_Id() As Decimal
        Get
            Return _Form_Id
        End Get
        Set(value As Decimal)
            _Form_Id = value
        End Set
    End Property
    Public Property Role_Id() As Decimal
        Get
            Return _Role_Id
        End Get
        Set(value As Decimal)
            _Role_Id = value
        End Set
    End Property
    Public Property cmp_id() As Decimal
        Get
            Return _Cmp_id
        End Get
        Set(value As Decimal)
            _Cmp_id = value
        End Set
    End Property
    Public Property Role_Name() As String
        Get
            Return _Role_Name
        End Get
        Set(value As String)
            _Role_Name = value
        End Set
    End Property
    Public Property Role_Detail() As String
        Get
            Return _Role_Detail
        End Get
        Set(value As String)
            _Role_Detail = value
        End Set
    End Property
    Public Property Type() As Byte
        Get
            Return _Type
        End Get
        Set(value As Byte)
            _Type = value
        End Set
    End Property
    Public Property is_active() As Byte
        Get
            Return _is_Active
        End Get
        Set(value As Byte)
            _is_Active = value
        End Set
    End Property
    Public Property Form_Name() As String
        Get
            Return _Form_Name
        End Get
        Set(value As String)
            _Form_Name = value
        End Set
    End Property
    Public Property Data() As String
        Get
            Return _data
        End Get
        Set(value As String)
            _data = value
        End Set
    End Property

    Public Property is_View() As Byte
        Get
            Return _is_View
        End Get
        Set(value As Byte)
            _is_View = value
        End Set
    End Property
    Public Property is_add() As Byte
        Get
            Return _is_add
        End Get
        Set(value As Byte)
            _is_add = value
        End Set
    End Property
    Public Property is_Edit() As Byte
        Get
            Return _is_Edit
        End Get
        Set(value As Byte)
            _is_Edit = value
        End Set
    End Property
    Public Property is_Delete() As Byte
        Get
            Return _is_Delete
        End Get
        Set(value As Byte)
            _is_Delete = value
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
            objclsRole = New clsRole()
            objclsRole.Role_Id = Role_Id
            objclsRole.cmp_id = cmp_id
            objclsRole.Role_Name = Role_Name
            objclsRole.Type = Type
            objclsRole.Role_Detail = Role_Detail
            objclsRole.is_active = is_active
            objclsRole.Ip_address = Ip_address
            objclsRole.login_id = login_id
            objclsRole.Tran_Type = Tran_Type
            If objclsRole.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsRole = New clsRole()
            objclsRole.Role_Id = Role_Id
            objclsRole.cmp_id = cmp_id
            objclsRole.Role_Name = Role_Name
            objclsRole.Type = Type
            objclsRole.Role_Detail = Role_Detail
            objclsRole.is_active = is_active
            objclsRole.Ip_address = Ip_address
            objclsRole.login_id = login_id
            objclsRole.Tran_Type = Tran_Type
            If objclsRole.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsRole = New clsRole()
            objclsRole.Role_Id = Role_Id
            objclsRole.cmp_id = cmp_id
            objclsRole.Role_Name = Role_Name
            objclsRole.Type = Type
            objclsRole.Role_Detail = Role_Detail
            objclsRole.is_active = is_active
            objclsRole.Ip_address = Ip_address
            objclsRole.login_id = login_id
            objclsRole.Tran_Type = Tran_Type
            If objclsRole.Delete() Then
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
            objclsRole = New clsRole()
            objclsRole.Role_Id = Role_Id
            objclsRole.cmp_id = cmp_id
            objclsRole.Form_Name = Form_Name
            dt = objclsRole.[Select]()

            If dt.Rows.Count > 0 Then

                is_View = dt.Rows(0)("is_View")
                is_add = dt.Rows(0)("is_Add")
                is_Edit = dt.Rows(0)("is_Edit")
                is_Delete = dt.Rows(0)("is_Delete")

            End If

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsRole = New clsRole()
            objclsRole.cmp_id = cmp_id
            dt = objclsRole.[SelectAll]()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectRoleByCmp]() As DataTable
        Dim dt As DataTable
        Try
            objclsRole = New clsRole()
            objclsRole.cmp_id = cmp_id
            dt = objclsRole.[SelectRoleByCmp]()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectRoleAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsRole = New clsRole()
            objclsRole.cmp_id = cmp_id
            dt = objclsRole.[SelectRoleAll]()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectRole]() As DataTable
        Dim dt As DataTable
        Try
            objclsRole = New clsRole()
            objclsRole.cmp_id = cmp_id
            objclsRole.Role_Id = Role_Id
            dt = objclsRole.[SelectRole]()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAllForm]() As DataTable
        Dim dt As DataTable
        Try
            objclsRole = New clsRole()
            'objclsRole.cmp_id = cmp_id
            dt = objclsRole.[SelectAllForm]()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function SelectChildForm() As DataTable
        Dim dt As DataTable
        Try
            objclsRole = New clsRole()
            objclsRole.Form_Id = Form_Id
            dt = objclsRole.SelectChildForm()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectDRole]() As DataTable
        Dim dt As DataTable
        Try
            objclsRole = New clsRole()
            objclsRole.cmp_id = cmp_id
            objclsRole.Role_Id = Role_Id
            dt = objclsRole.[SelectDRole]()

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsRole
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Role_Id As Decimal
    Private _Cmp_id As Decimal
    Private _Role_Name As String
    Private _Type As Byte
    Private _is_Active As Byte
    Private _is_View As Byte
    Private _is_add As Byte
    Private _is_Edit As Byte
    Private _is_Delete As Byte
    Private _data As String
    Private _Role_Detail As String
    Private _Form_Name As String
    Private _Ip_address As String
    Private _login_id As Integer
    Private _Tran_Type As String
    Private _Form_Id As Decimal
    Private objclsRole As clsRole
#End Region

#Region "Public Property"
    Public Property Form_Id() As Decimal
        Get
            Return _Form_Id
        End Get
        Set(value As Decimal)
            _Form_Id = value
        End Set
    End Property
    Public Property Role_Id() As Decimal
        Get
            Return _Role_Id
        End Get
        Set(value As Decimal)
            _Role_Id = value
        End Set
    End Property
    Public Property cmp_id() As Decimal
        Get
            Return _Cmp_id
        End Get
        Set(value As Decimal)
            _Cmp_id = value
        End Set
    End Property
    Public Property Role_Name() As String
        Get
            Return _Role_Name
        End Get
        Set(value As String)
            _Role_Name = value
        End Set
    End Property
    Public Property Role_Detail() As String
        Get
            Return _Role_Detail
        End Get
        Set(value As String)
            _Role_Detail = value
        End Set
    End Property
    Public Property Type() As Byte
        Get
            Return _Type
        End Get
        Set(value As Byte)
            _Type = value
        End Set
    End Property
    Public Property is_active() As Byte
        Get
            Return _is_Active
        End Get
        Set(value As Byte)
            _is_Active = value
        End Set
    End Property
    Public Property Form_Name() As String
        Get
            Return _Form_Name
        End Get
        Set(value As String)
            _Form_Name = value
        End Set
    End Property
    Public Property Data() As String
        Get
            Return _data
        End Get
        Set(value As String)
            _data = value
        End Set
    End Property

    Public Property is_View() As Byte
        Get
            Return _is_View
        End Get
        Set(value As Byte)
            _is_View = value
        End Set
    End Property
    Public Property is_add() As Byte
        Get
            Return _is_add
        End Get
        Set(value As Byte)
            _is_add = value
        End Set
    End Property
    Public Property is_Edit() As Byte
        Get
            Return _is_Edit
        End Get
        Set(value As Byte)
            _is_Edit = value
        End Set
    End Property
    Public Property is_Delete() As Byte
        Get
            Return _is_Delete
        End Get
        Set(value As Byte)
            _is_Delete = value
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
    Public Property Tran_Type() As String
        Get
            Return _Tran_Type
        End Get
        Set(ByVal value As String)
            _Tran_Type = value
        End Set
    End Property
#End Region

    Public Function [Select_Role]() As DataTable


        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Role_id", Role_Id)
        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_Role_Name", oColSqlparram)


        Return dtRole
    End Function
    Public Function [Select_Form]() As DataTable


        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Role_id", Role_Id)
        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_Role_Form", oColSqlparram)


        Return dtRole
    End Function


    Public Function [Select]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_Id", cmp_id)
        oColSqlparram.Add("@Role_id", Role_Id)
        oColSqlparram.Add("@Form_Name", Form_Name)
        Dim dtRole As DataTable = oClsDal.GetdataTableSp("P_Get_Role", oColSqlparram)

        Return dtRole
    End Function
    Public Function [SelectAll]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_Id", cmp_id)

        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_M_Role", oColSqlparram)

        Return dtRole
    End Function
    Public Function [SelectRole]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_Id", cmp_id)
        oColSqlparram.Add("@Role_Id", Role_Id)
        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_M_Role", oColSqlparram)

        Return dtRole
    End Function
    Public Function [SelectDRole]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_Id", cmp_id)
        oColSqlparram.Add("@Role_Id", Role_Id)
        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_D_Role", oColSqlparram)

        Return dtRole
    End Function
    Public Function SelectChildForm() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Form_Id", Form_Id)

        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_ChildForm", oColSqlparram)

        Return dtRole
    End Function

    Public Function [SelectAllForm]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        'oColSqlparram.Add("@Cmp_Id", cmp_id)

        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_Form", oColSqlparram)

        Return dtRole
    End Function



    Public Function Insert() As Boolean
        Try
            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@Role_id", Role_Id, SqlDbType.Int)
            oclsParm.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oclsParm.Add("@Role_name", Role_Name)
            oclsParm.Add("@Role_Type", Type, SqlDbType.Int)
            oclsParm.Add("@Role_Detail", Role_Detail)
            oclsParm.Add("@is_Active", is_active)
            oclsParm.Add("@ip_address", Ip_address)
            oclsParm.Add("@login_id", login_id, SqlDbType.Int)
            oclsParm.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Role", oclsParm)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try

            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@Role_id", Role_Id, SqlDbType.Int)
            oclsParm.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oclsParm.Add("@Role_name", Role_Name)
            oclsParm.Add("@Role_Type", Type, SqlDbType.Int)
            oclsParm.Add("@Role_Detail", Role_Detail)
            oclsParm.Add("@is_Active", is_active)
            oclsParm.Add("@ip_address", Ip_address)
            oclsParm.Add("@login_id", login_id, SqlDbType.Int)
            oclsParm.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Role", oclsParm)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            Dim oclsParm As New ColSqlparram
            oclsParm.Add("@Role_id", Role_Id, SqlDbType.Int)
            oclsParm.Add("@Cmp_id", cmp_id, SqlDbType.Int)
            oclsParm.Add("@Role_name", Role_Name)
            oclsParm.Add("@Role_Type", Type, SqlDbType.Int)
            oclsParm.Add("@Role_Detail", Role_Detail)
            oclsParm.Add("@is_Active", is_active)
            oclsParm.Add("@ip_address", Ip_address)
            oclsParm.Add("@login_id", login_id, SqlDbType.Int)
            oclsParm.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Role", oclsParm)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [SelectRoleByCmp]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_Id", cmp_id)
        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_Role_By_Cmp", oColSqlparram)

        Return dtRole
    End Function
    Public Function [SelectRoleAll]() As DataTable
        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_Id", cmp_id)
        Dim dtRole As DataTable = oClsDal.GetdataTableSp("Get_Role_All", oColSqlparram)

        Return dtRole
    End Function
End Class
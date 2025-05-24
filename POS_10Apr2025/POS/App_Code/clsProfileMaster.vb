Imports Microsoft.VisualBasic
Imports System
Imports System.Data


Public Class Controller_clsProfileMaster

    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"

    Private _cmp_id As Integer
    Private _profile_id As Integer
    Private _profile_name As String
    Private _bonus_point As Integer
    Private _amount As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _purchase_amount As Integer
    Private _earn_bonus As Integer
    Private _Tran_Type As String
    Private _discount_percent As Integer
    Private _IsDefaul As Integer
    Private objclsProfileMaster As clsProfileMaster
#End Region
#Region "Public Property"

    Public Property IsDefaul() As Integer
        Get
            Return _IsDefaul
        End Get
        Set(ByVal value As Integer)
            _IsDefaul = value
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


    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
        End Set
    End Property

    Public Property profile_name() As String
        Get
            Return _profile_name
        End Get
        Set(ByVal value As String)
            _profile_name = value
        End Set
    End Property

    Public Property bonus_point() As Integer
        Get
            Return _bonus_point
        End Get
        Set(ByVal value As Integer)
            _bonus_point = value
        End Set
    End Property

    Public Property amount() As Integer
        Get
            Return _amount
        End Get
        Set(ByVal value As Integer)
            _amount = value
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
    Public Property purchase_amount() As Integer
        Get
            Return _purchase_amount
        End Get
        Set(ByVal value As Integer)
            _purchase_amount = value
        End Set
    End Property
    Public Property earn_bonus() As Integer
        Get
            Return _earn_bonus
        End Get
        Set(ByVal value As Integer)
            _earn_bonus = value
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
    Public Property discount_percent() As Integer
        Get
            Return _discount_percent
        End Get
        Set(ByVal value As Integer)
            _discount_percent = value
        End Set
    End Property
#End Region
    Public Function Insert() As Boolean
        Try
            objclsProfileMaster = New clsProfileMaster()
            objclsProfileMaster.cmp_id = cmp_id
            objclsProfileMaster.profile_id = profile_id
            objclsProfileMaster.profile_name = profile_name
            objclsProfileMaster.bonus_point = bonus_point
            objclsProfileMaster.purchase_amount = purchase_amount
            objclsProfileMaster.earn_bonus = earn_bonus
            objclsProfileMaster.amount = amount
            objclsProfileMaster.discount_percent = discount_percent
            objclsProfileMaster.IsDefaul = IsDefaul
            If objclsProfileMaster.Insert() Then
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
            objclsProfileMaster = New clsProfileMaster()
            objclsProfileMaster.cmp_id = cmp_id
            dt = objclsProfileMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsProfileMaster = New clsProfileMaster()
            objclsProfileMaster.cmp_id = cmp_id
            objclsProfileMaster.profile_id = profile_id
            objclsProfileMaster.profile_name = profile_name
            objclsProfileMaster.bonus_point = bonus_point
            objclsProfileMaster.amount = amount
            objclsProfileMaster.purchase_amount = purchase_amount
            objclsProfileMaster.earn_bonus = earn_bonus
            objclsProfileMaster.Tran_Type = Tran_Type
            objclsProfileMaster.discount_percent = discount_percent
            objclsProfileMaster.IsDefaul = IsDefaul
            If objclsProfileMaster.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsProfileMaster = New clsProfileMaster()
            objclsProfileMaster.cmp_id = cmp_id
            objclsProfileMaster.profile_id = profile_id
            objclsProfileMaster.profile_name = profile_name
            objclsProfileMaster.bonus_point = bonus_point
            objclsProfileMaster.amount = amount
            objclsProfileMaster.purchase_amount = purchase_amount
            objclsProfileMaster.earn_bonus = earn_bonus
            objclsProfileMaster.Tran_Type = Tran_Type
            objclsProfileMaster.discount_percent = discount_percent
            objclsProfileMaster.IsDefaul = IsDefaul
            If objclsProfileMaster.Update() Then
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
            objclsProfileMaster = New clsProfileMaster()
            objclsProfileMaster.profile_id = profile_id
            objclsProfileMaster.cmp_id = cmp_id
            dt = objclsProfileMaster.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SelectProfile() As DataTable
        Dim dt As DataTable
        Try
            objclsProfileMaster = New clsProfileMaster()
            objclsProfileMaster.cmp_id = cmp_id
            dt = objclsProfileMaster.SelectProfile()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class


Public Class clsProfileMaster
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"

    Private _cmp_id As Integer
    Private _profile_id As Integer
    Private _profile_name As String
    Private _bonus_point As Integer
    Private _amount As Integer
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _purchase_amount As Integer
    Private _earn_bonus As Integer
    Private _Tran_Type As String
    Private _discount_percent As Integer
    Private _IsDefaul As Integer
    Private objclsProfileMaster As clsProfileMaster
#End Region
#Region "Public Property"

    Public Property IsDefaul() As Integer
        Get
            Return _IsDefaul
        End Get
        Set(ByVal value As Integer)
            _IsDefaul = value
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
    Public Property profile_id() As Integer
        Get
            Return _profile_id
        End Get
        Set(ByVal value As Integer)
            _profile_id = value
        End Set
    End Property

    Public Property profile_name() As String
        Get
            Return _profile_name
        End Get
        Set(ByVal value As String)
            _profile_name = value
        End Set
    End Property

    Public Property bonus_point() As Integer
        Get
            Return _bonus_point
        End Get
        Set(ByVal value As Integer)
            _bonus_point = value
        End Set
    End Property

    Public Property amount() As Integer
        Get
            Return _amount
        End Get
        Set(ByVal value As Integer)
            _amount = value
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
    Public Property purchase_amount() As Integer
        Get
            Return _purchase_amount
        End Get
        Set(ByVal value As Integer)
            _purchase_amount = value
        End Set
    End Property
    Public Property earn_bonus() As Integer
        Get
            Return _earn_bonus
        End Get
        Set(ByVal value As Integer)
            _earn_bonus = value
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
    Public Property discount_percent() As Integer
        Get
            Return _discount_percent
        End Get
        Set(ByVal value As Integer)
            _discount_percent = value
        End Set
    End Property
#End Region
    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_name", profile_name)
            oColSqlparram.Add("@bonus_point", bonus_point, SqlDbType.Int)
            oColSqlparram.Add("@amount", amount, SqlDbType.Int)
            oColSqlparram.Add("@purchase_amount", purchase_amount, SqlDbType.Int)
            oColSqlparram.Add("@earn_bonus", earn_bonus, SqlDbType.Int)
            oColSqlparram.Add("@discount_percent", discount_percent, SqlDbType.Int)
            oColSqlparram.Add("@IsDefaul", IsDefaul, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Profile", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Profile", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Profile_info", oColSqlparram)

        Return dtlogin
    End Function


    Public Function Delete() As Boolean
        Try

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_name", profile_name)
            oColSqlparram.Add("@bonus_point", bonus_point, SqlDbType.Int)
            oColSqlparram.Add("@amount", amount, SqlDbType.Int)
            oColSqlparram.Add("@purchase_amount", purchase_amount, SqlDbType.Int)
            oColSqlparram.Add("@earn_bonus", earn_bonus, SqlDbType.Int)
            oColSqlparram.Add("@discount_percent", discount_percent, SqlDbType.Int)
            oColSqlparram.Add("@IsDefaul", IsDefaul, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Profile", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_id", profile_id, SqlDbType.Int)
            oColSqlparram.Add("@profile_name", profile_name)
            oColSqlparram.Add("@bonus_point", bonus_point, SqlDbType.Int)
            oColSqlparram.Add("@amount", amount, SqlDbType.Int)
            oColSqlparram.Add("@purchase_amount", purchase_amount, SqlDbType.Int)
            oColSqlparram.Add("@earn_bonus", earn_bonus, SqlDbType.Int)
            oColSqlparram.Add("@discount_percent", discount_percent, SqlDbType.Int)
            oColSqlparram.Add("@IsDefaul", IsDefaul, SqlDbType.Int)
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Profile", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function SelectProfile() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Profile", oColSqlparram)

        Return dtlogin
    End Function
End Class

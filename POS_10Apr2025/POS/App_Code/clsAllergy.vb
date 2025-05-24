Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class Controller_clsAllergy


    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"


    Private _allergy_id As Integer
    Private _name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _Aimage As Byte()
    Private _Tran_Type As String
    Private objclsAllergyMaster As clsAllergy
#End Region
#Region "Public Property"

    Public Property allergy_id() As Integer
        Get
            Return _allergy_id
        End Get
        Set(ByVal value As Integer)
            _allergy_id = value
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

    Public Property description() As String

        Get
            Return _description

        End Get
        Set(ByVal value As String)
            _description = value
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
    Public Property Aimage() As Byte()
        Get
            Return _Aimage
        End Get
        Set(ByVal value As Byte())
            _Aimage = value
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
            objclsAllergyMaster = New clsAllergy()
            objclsAllergyMaster.cmp_id = cmp_id
            objclsAllergyMaster.allergy_id = allergy_id
            objclsAllergyMaster.name = name
            objclsAllergyMaster.description = description
            objclsAllergyMaster.Aimage = Aimage
            objclsAllergyMaster.Tran_Type = Tran_Type
            If objclsAllergyMaster.Insert() Then
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
            objclsAllergyMaster = New clsAllergy()
            objclsAllergyMaster.cmp_id = cmp_id
            dt = objclsAllergyMaster.[SelectAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Delete() As Boolean
        Try
            objclsAllergyMaster = New clsAllergy()
            objclsAllergyMaster.cmp_id = cmp_id
            objclsAllergyMaster.allergy_id = allergy_id
            objclsAllergyMaster.name = name
            objclsAllergyMaster.description = description
            objclsAllergyMaster.Aimage = Aimage
            objclsAllergyMaster.Tran_Type = Tran_Type
            If objclsAllergyMaster.Delete() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            objclsAllergyMaster = New clsAllergy()
            objclsAllergyMaster.cmp_id = cmp_id
            objclsAllergyMaster.allergy_id = allergy_id
            objclsAllergyMaster.name = name
            objclsAllergyMaster.description = description
            objclsAllergyMaster.Aimage = Aimage
            objclsAllergyMaster.Tran_Type = Tran_Type
            If objclsAllergyMaster.Update() Then
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
            objclsAllergyMaster = New clsAllergy()
            objclsAllergyMaster.allergy_id = allergy_id
            objclsAllergyMaster.cmp_id = cmp_id
            dt = objclsAllergyMaster.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsAllergy

    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
#Region "Public Variables"


    Private _allergy_id As Integer
    Private _name As String
    Private _description As String
    Private _created_date As System.DateTime
    Private _modify_date As System.DateTime
    Private _cmp_id As Integer
    Private _Aimage As Byte()
    Private _Tran_Type As String
    Private objclsAllergyMaster As clsAllergy
#End Region

#Region "Public Property"




    Public Property allergy_id() As Integer
        Get
            Return _allergy_id
        End Get
        Set(ByVal value As Integer)
            _allergy_id = value
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

    Public Property description() As String
        Get
            Return _description

        End Get
        Set(ByVal value As String)
            _description = value
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
    Public Property Aimage() As Byte()
        Get
            Return _Aimage
        End Get
        Set(ByVal value As Byte())
            _Aimage = value
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
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@allergy_id ", allergy_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description ", description)
            If Not Aimage Is Nothing Then
                oColSqlparram.Add("@Aimage", Aimage, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "I")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Allergy", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [SelectAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Allergy", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@allergy_id", allergy_id, SqlDbType.Int)
        oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_AllergyInfo", oColSqlparram)

        Return dtlogin
    End Function



    Public Function Delete() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@allergy_id ", allergy_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description ", description)
            If Not Aimage Is Nothing Then
                oColSqlparram.Add("@Aimage", Aimage, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "D")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Allergy", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", cmp_id, SqlDbType.Int)
            oColSqlparram.Add("@allergy_id ", allergy_id, SqlDbType.Int)
            oColSqlparram.Add("@name", name)
            oColSqlparram.Add("@description ", description)
            If Not Aimage Is Nothing Then
                oColSqlparram.Add("@Aimage", Aimage, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Tran_Type", "U")
            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("P_M_Allergy", oColSqlparram)
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


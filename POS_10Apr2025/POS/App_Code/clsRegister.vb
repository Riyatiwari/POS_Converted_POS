'Imports Telerik.Web.UI
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading


Public Class Controller_clsRegister
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _Company_id As Integer
    Private _C_Name As String
    Private _C_Address As String
    Private _C_Starting_Date As Date
    Private _C_Domain As String
    Private _U_IP_Address As String
    Private _U_Login_id As Integer
    Private _C_Country As Integer
    Private _C_State As Integer
    Private _C_City As Integer
    Private _C_LiteVersion As Integer
    Private _C_Logo As Byte()
    Private _C_Code As String
    Private _C_Email As String
    Private _C_Description As String
    Private _C_Postal As String
    Private _C_Website As String
    Private _C_Contact As String
    Private _C_Registration_no As Integer
    Private _C_GST_VAT As Integer
    Private _C_CST_VAT As Integer
    Private _C_Service_tax_no As Integer
    Private _C_Pan_no As Integer
    Private _Branch_Name As String
    Private _Synchronization As String
    Private _Venue_Name As String
    Private _Vat_No As String
    Private _Receipt_Header As String
    Private _Receipt_Footer As String
    Private _log_off_time As Integer
    Private _par_sale_par_operator As Integer
    Private _Currency_Id As Integer
    Private _judo_id As String
    Private _judo_token As String
    Private _judo_secret As String
    Private _week_start_day As String
    Private _show_chips As Integer
    Private _BusinessDoneBy As Integer
    Private _Display_declaration As Integer
    Private _chk_duration As Integer
    Private _IsAddTax2 As Integer
    Private _IsExclusiveTax As Integer
    Private _IsPaymentGtway As Integer
    Private objclsRegister As clsRegister
#End Region

#Region "Public Property"

    Public Property IsPaymentGtway() As Integer
        Get
            Return _IsPaymentGtway
        End Get
        Set(value As Integer)
            _IsPaymentGtway = value
        End Set
    End Property

    Public Property IsExclusiveTax() As Integer
        Get
            Return _IsExclusiveTax
        End Get
        Set(value As Integer)
            _IsExclusiveTax = value
        End Set
    End Property

    Public Property IsAddTax2() As Integer
        Get
            Return _IsAddTax2
        End Get
        Set(value As Integer)
            _IsAddTax2 = value
        End Set
    End Property

    Public Property chk_duration() As Integer
        Get
            Return _chk_duration
        End Get
        Set(value As Integer)
            _chk_duration = value
        End Set
    End Property

    Public Property Display_declaration() As Integer
        Get
            Return _Display_declaration
        End Get
        Set(value As Integer)
            _Display_declaration = value
        End Set
    End Property
    Public Property Show_chips() As Integer
        Get
            Return _show_chips
        End Get
        Set(value As Integer)
            _show_chips = value
        End Set
    End Property

    Public Property BusinessDoneBy() As Integer
        Get
            Return _BusinessDoneBy
        End Get
        Set(value As Integer)
            _BusinessDoneBy = value
        End Set
    End Property
    Public Property Currency_Id() As Integer
        Get
            Return _Currency_Id
        End Get
        Set(value As Integer)
            _Currency_Id = value
        End Set
    End Property
    Public Property par_sale_par_operator() As Integer
        Get
            Return _par_sale_par_operator
        End Get
        Set(value As Integer)
            _par_sale_par_operator = value
        End Set
    End Property
    Public Property Company_id() As Integer
        Get
            Return _Company_id
        End Get
        Set(value As Integer)
            _Company_id = value
        End Set
    End Property

    Public Property judo_id() As String
        Get
            Return _judo_id
        End Get
        Set(value As String)
            _judo_id = value
        End Set
    End Property

    Public Property judo_token() As String
        Get
            Return _judo_token
        End Get
        Set(value As String)
            _judo_token = value
        End Set
    End Property

    Public Property judo_secret() As String
        Get
            Return _judo_secret
        End Get
        Set(value As String)
            _judo_secret = value
        End Set
    End Property
    Public Property Branch_Name() As String
        Get
            Return _Branch_Name
        End Get
        Set(value As String)
            _Branch_Name = value
        End Set
    End Property
    Public Property Synchronization() As String
        Get
            Return _Synchronization
        End Get
        Set(value As String)
            _Synchronization = value
        End Set
    End Property
    Public Property Venue_Name() As String
        Get
            Return _Venue_Name
        End Get
        Set(value As String)
            _Venue_Name = value
        End Set
    End Property
    Public Property C_Name() As String
        Get
            Return _C_Name
        End Get
        Set(value As String)
            _C_Name = value
        End Set
    End Property
    Public Property C_Address() As String
        Get
            Return _C_Address
        End Get
        Set(value As String)
            _C_Address = value
        End Set
    End Property
    Public Property C_Starting_Date() As Date
        Get
            Return _C_Starting_Date
        End Get
        Set(value As Date)
            _C_Starting_Date = value
        End Set
    End Property
    Public Property C_Domain() As String
        Get
            Return _C_Domain
        End Get
        Set(value As String)
            _C_Domain = value
        End Set
    End Property
    Public Property U_IP_Address() As String
        Get
            Return _U_IP_Address
        End Get
        Set(value As String)
            _U_IP_Address = value
        End Set
    End Property
    Public Property U_Login_id() As Integer
        Get
            Return _U_Login_id
        End Get
        Set(value As Integer)
            _U_Login_id = value
        End Set
    End Property
    Public Property C_Country() As Integer
        Get
            Return _C_Country
        End Get
        Set(value As Integer)
            _C_Country = value
        End Set
    End Property
    Public Property C_State() As Integer
        Get
            Return _C_State
        End Get
        Set(value As Integer)
            _C_State = value
        End Set
    End Property
    Public Property C_City() As Integer
        Get
            Return _C_City
        End Get
        Set(value As Integer)
            _C_City = value
        End Set
    End Property
    Public Property C_LiteVersion() As Integer
        Get
            Return _C_LiteVersion
        End Get
        Set(value As Integer)
            _C_LiteVersion = value
        End Set
    End Property
    Public Property C_Logo() As Byte()
        Get
            Return _C_Logo
        End Get
        Set(value As Byte())
            _C_Logo = value
        End Set
    End Property
    Public Property C_Code() As String
        Get
            Return _C_Code
        End Get
        Set(value As String)
            _C_Code = value
        End Set
    End Property
    Public Property C_Email() As String
        Get
            Return _C_Email
        End Get
        Set(value As String)
            _C_Email = value
        End Set
    End Property
    Public Property C_Description() As String
        Get
            Return _C_Description
        End Get
        Set(value As String)
            _C_Description = value
        End Set
    End Property
    Public Property C_Postal() As String
        Get
            Return _C_Postal
        End Get
        Set(value As String)
            _C_Postal = value
        End Set
    End Property
    Public Property C_Website() As String
        Get
            Return _C_Website
        End Get
        Set(value As String)
            _C_Website = value
        End Set
    End Property
    Public Property C_Contact() As String
        Get
            Return _C_Contact
        End Get
        Set(value As String)
            _C_Contact = value
        End Set
    End Property
    Public Property C_Registration_no() As Integer
        Get
            Return _C_Registration_no
        End Get
        Set(value As Integer)
            _C_Registration_no = value
        End Set
    End Property
    Public Property C_GST_VAT() As Integer
        Get
            Return _C_GST_VAT
        End Get
        Set(value As Integer)
            _C_GST_VAT = value
        End Set
    End Property
    Public Property C_CST_VAT() As Integer
        Get
            Return _C_CST_VAT
        End Get
        Set(value As Integer)
            _C_CST_VAT = value
        End Set
    End Property
    Public Property C_Service_tax_no() As Integer
        Get
            Return _C_Service_tax_no
        End Get
        Set(value As Integer)
            _C_Service_tax_no = value
        End Set
    End Property
    Public Property C_Pan_no() As Integer
        Get
            Return _C_Pan_no
        End Get
        Set(value As Integer)
            _C_Pan_no = value
        End Set
    End Property

    Public Property Vat_No() As String
        Get
            Return _Vat_No
        End Get
        Set(ByVal value As String)
            _Vat_No = value
        End Set
    End Property

    Public Property Receipt_Header() As String
        Get
            Return _Receipt_Header
        End Get
        Set(ByVal value As String)
            _Receipt_Header = value
        End Set
    End Property
    Public Property Receipt_Footer() As String
        Get
            Return _Receipt_Footer
        End Get
        Set(ByVal value As String)
            _Receipt_Footer = value
        End Set
    End Property
    Public Property log_off_time() As Integer
        Get
            Return _log_off_time
        End Get
        Set(value As Integer)
            _log_off_time = value
        End Set
    End Property


    Public Property week_start_day() As String
        Get
            Return _week_start_day
        End Get
        Set(ByVal value As String)
            _week_start_day = value
        End Set
    End Property
#End Region

    Public Function Insert() As Boolean
        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id
            objclsRegister.C_Name = C_Name
            objclsRegister.C_Address = C_Address
            objclsRegister.C_Starting_Date = C_Starting_Date
            objclsRegister.C_Domain = C_Domain
            objclsRegister.U_IP_Address = U_IP_Address
            objclsRegister.U_Login_id = U_Login_id
            objclsRegister.C_Country = C_Country
            objclsRegister.C_State = C_State
            objclsRegister.C_City = C_City
            objclsRegister.C_LiteVersion = C_LiteVersion
            objclsRegister.C_Logo = C_Logo
            objclsRegister.C_Code = C_Code
            objclsRegister.C_Email = C_Email
            objclsRegister.C_Description = C_Description
            objclsRegister.C_Postal = C_Postal
            objclsRegister.C_Website = C_Website
            objclsRegister.C_Contact = C_Contact
            objclsRegister.C_Registration_no = C_Registration_no
            objclsRegister.C_GST_VAT = C_GST_VAT
            objclsRegister.C_CST_VAT = C_CST_VAT
            objclsRegister.C_Service_tax_no = C_Service_tax_no
            objclsRegister.C_Pan_no = C_Pan_no
            objclsRegister.Branch_Name = Branch_Name
            objclsRegister.Synchronization = Synchronization
            objclsRegister.Venue_Name = Venue_Name
            objclsRegister.Vat_No = Vat_No
            objclsRegister.Receipt_Header = Receipt_Header
            objclsRegister.Receipt_Footer = Receipt_Footer
            objclsRegister.log_off_time = log_off_time
            objclsRegister.par_sale_par_operator = par_sale_par_operator
            objclsRegister.Currency_Id = Currency_Id
            objclsRegister.judo_id = judo_id
            objclsRegister.judo_token = judo_token
            objclsRegister.judo_secret = judo_secret
            objclsRegister.week_start_day = week_start_day
            objclsRegister.Show_chips = Show_chips
            objclsRegister.BusinessDoneBy = BusinessDoneBy
            objclsRegister.Display_declaration = Display_declaration
            objclsRegister.chk_duration = chk_duration
            objclsRegister.IsAddTax2 = IsAddTax2
            objclsRegister.IsExclusiveTax = IsExclusiveTax
            objclsRegister.IsPaymentGtway = IsPaymentGtway

            If objclsRegister.Insert() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id
            objclsRegister.C_Name = C_Name
            objclsRegister.C_Address = C_Address
            objclsRegister.C_Starting_Date = C_Starting_Date
            objclsRegister.C_Domain = C_Domain
            objclsRegister.U_IP_Address = U_IP_Address
            objclsRegister.U_Login_id = U_Login_id
            objclsRegister.C_Country = C_Country
            objclsRegister.C_State = C_State
            objclsRegister.C_City = C_City
            objclsRegister.C_LiteVersion = C_LiteVersion
            objclsRegister.C_Logo = C_Logo
            objclsRegister.C_Code = C_Code
            objclsRegister.C_Email = C_Email
            objclsRegister.C_Description = C_Description
            objclsRegister.C_Postal = C_Postal
            objclsRegister.C_Website = C_Website
            objclsRegister.C_Contact = C_Contact
            objclsRegister.C_Registration_no = C_Registration_no
            objclsRegister.C_GST_VAT = C_GST_VAT
            objclsRegister.C_CST_VAT = C_CST_VAT
            objclsRegister.C_Service_tax_no = C_Service_tax_no
            objclsRegister.C_Pan_no = C_Pan_no
            objclsRegister.Branch_Name = Branch_Name
            objclsRegister.Synchronization = Synchronization
            objclsRegister.Venue_Name = Venue_Name
            objclsRegister.Vat_No = Vat_No
            objclsRegister.Receipt_Header = Receipt_Header
            objclsRegister.Receipt_Footer = Receipt_Footer
            objclsRegister.log_off_time = log_off_time
            objclsRegister.par_sale_par_operator = par_sale_par_operator
            objclsRegister.Currency_Id = Currency_Id
            objclsRegister.judo_id = judo_id
            objclsRegister.judo_token = judo_token
            objclsRegister.judo_secret = judo_secret
            objclsRegister.week_start_day = week_start_day
            objclsRegister.Show_chips = Show_chips
            objclsRegister.BusinessDoneBy = BusinessDoneBy
            objclsRegister.Display_declaration = Display_declaration
            objclsRegister.chk_duration = chk_duration
            objclsRegister.IsAddTax2 = IsAddTax2
            objclsRegister.IsExclusiveTax = IsExclusiveTax
            objclsRegister.IsPaymentGtway = IsPaymentGtway

            If objclsRegister.Update() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_Price() As Boolean
        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id

            If objclsRegister.Update_Price() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Update_newStore() As Boolean
        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id
            objclsRegister.C_Country = C_Country
            objclsRegister.C_State = C_State
            objclsRegister.C_City = C_City
            objclsRegister.Currency_Id = Currency_Id
            If objclsRegister.Update_newStore() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function checkProduct() As DataTable

        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id

            Dim dt As DataTable = objclsRegister.checkProduct()


            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function checkgroup() As DataTable

        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id

            Dim dt As DataTable = objclsRegister.checkgroup()


            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [Select]() As DataTable
        Dim dt As DataTable
        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id
            dt = objclsRegister.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    '------------12-jan-2024
    Public Function [Select_fornew_user]() As DataTable
        Dim dt As DataTable
        Try
            objclsRegister = New clsRegister()

            dt = objclsRegister.[Select_fornew_user]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function [selectLite]() As DataTable
        Dim dt As DataTable
        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id
            dt = objclsRegister.[selectLite]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function [ClearAll]() As DataTable
        Dim dt As DataTable
        Try
            objclsRegister = New clsRegister()
            objclsRegister.Company_id = Company_id
            dt = objclsRegister.[ClearAll]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsRegister
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub
#Region "Public Variables"
    Private _Company_id As Integer
    Private _C_Name As String
    Private _C_Address As String
    Private _C_Starting_Date As Date
    Private _C_Domain As String
    Private _U_IP_Address As String
    Private _U_Login_id As Integer
    Private _C_Country As Integer
    Private _C_State As Integer
    Private _C_City As Integer
    Private _C_LiteVersion As Integer
    Private _C_Logo As Byte()
    Private _C_Code As String
    Private _C_Email As String
    Private _C_Description As String
    Private _C_Postal As String
    Private _C_Website As String
    Private _C_Contact As String
    Private _C_Registration_no As Integer
    Private _C_GST_VAT As Integer
    Private _C_CST_VAT As Integer
    Private _C_Service_tax_no As Integer
    Private _C_Pan_no As Integer
    Private _Branch_Name As String
    Private _Synchronization As String
    Private _Venue_Name As String
    Private _Vat_No As String
    Private _Receipt_Header As String
    Private _Receipt_Footer As String
    Private _log_off_time As Integer
    Private _par_sale_par_operator As Integer
    Private _Currency_Id As Integer
    Private _judo_id As String
    Private _judo_token As String
    Private _judo_secret As String
    Private _week_start_day As String
    Private _show_chips As Integer
    Private _BusinessDoneBy As Integer
    Private _Display_declaration As Integer
    Private _chk_duration As Integer
    Private _IsAddTax2 As Integer
    Private _IsExclusiveTax As Integer
    Private _IsPaymentGtway As Integer
    Private objclsRegister As clsRegister
#End Region

#Region "Public Property"
    Public Property IsPaymentGtway() As Integer
        Get
            Return _IsPaymentGtway
        End Get
        Set(value As Integer)
            _IsPaymentGtway = value
        End Set
    End Property

    Public Property IsExclusiveTax() As Integer
        Get
            Return _IsExclusiveTax
        End Get
        Set(value As Integer)
            _IsExclusiveTax = value
        End Set
    End Property

    Public Property IsAddTax2() As Integer
        Get
            Return _IsAddTax2
        End Get
        Set(value As Integer)
            _IsAddTax2 = value
        End Set
    End Property


    Public Property chk_duration() As Integer
        Get
            Return _chk_duration
        End Get
        Set(value As Integer)
            _chk_duration = value
        End Set
    End Property
    Public Property Display_declaration() As Integer
        Get
            Return _Display_declaration
        End Get
        Set(value As Integer)
            _Display_declaration = value
        End Set
    End Property
    Public Property Show_chips() As Integer
        Get
            Return _show_chips
        End Get
        Set(value As Integer)
            _show_chips = value
        End Set
    End Property

    Public Property BusinessDoneBy() As Integer
        Get
            Return _BusinessDoneBy
        End Get
        Set(value As Integer)
            _BusinessDoneBy = value
        End Set
    End Property
    Public Property Currency_Id() As Integer
        Get
            Return _Currency_Id
        End Get
        Set(value As Integer)
            _Currency_Id = value
        End Set
    End Property
    Public Property judo_id() As String
        Get
            Return _judo_id
        End Get
        Set(value As String)
            _judo_id = value
        End Set
    End Property

    Public Property judo_token() As String
        Get
            Return _judo_token
        End Get
        Set(value As String)
            _judo_token = value
        End Set
    End Property

    Public Property judo_secret() As String
        Get
            Return _judo_secret
        End Get
        Set(value As String)
            _judo_secret = value
        End Set
    End Property
    Public Property par_sale_par_operator() As Integer
        Get
            Return _par_sale_par_operator
        End Get
        Set(value As Integer)
            _par_sale_par_operator = value
        End Set
    End Property
    Public Property Company_id() As Integer
        Get
            Return _Company_id
        End Get
        Set(value As Integer)
            _Company_id = value
        End Set
    End Property
    Public Property Branch_Name() As String
        Get
            Return _Branch_Name
        End Get
        Set(value As String)
            _Branch_Name = value
        End Set
    End Property
    Public Property Synchronization() As String
        Get
            Return _Synchronization
        End Get
        Set(value As String)
            _Synchronization = value
        End Set
    End Property
    Public Property Venue_Name() As String
        Get
            Return _Venue_Name
        End Get
        Set(value As String)
            _Venue_Name = value
        End Set
    End Property
    Public Property C_Name() As String
        Get
            Return _C_Name
        End Get
        Set(value As String)
            _C_Name = value
        End Set
    End Property
    Public Property C_Address() As String
        Get
            Return _C_Address
        End Get
        Set(value As String)
            _C_Address = value
        End Set
    End Property
    Public Property C_Starting_Date() As Date
        Get
            Return _C_Starting_Date
        End Get
        Set(value As Date)
            _C_Starting_Date = value
        End Set
    End Property
    Public Property C_Domain() As String
        Get
            Return _C_Domain
        End Get
        Set(value As String)
            _C_Domain = value
        End Set
    End Property
    Public Property U_IP_Address() As String
        Get
            Return _U_IP_Address
        End Get
        Set(value As String)
            _U_IP_Address = value
        End Set
    End Property
    Public Property U_Login_id() As Integer
        Get
            Return _U_Login_id
        End Get
        Set(value As Integer)
            _U_Login_id = value
        End Set
    End Property
    Public Property C_Country() As Integer
        Get
            Return _C_Country
        End Get
        Set(value As Integer)
            _C_Country = value
        End Set
    End Property
    Public Property C_State() As Integer
        Get
            Return _C_State
        End Get
        Set(value As Integer)
            _C_State = value
        End Set
    End Property
    Public Property C_City() As Integer
        Get
            Return _C_City
        End Get
        Set(value As Integer)
            _C_City = value
        End Set
    End Property
    Public Property C_LiteVersion() As Integer
        Get
            Return _C_LiteVersion
        End Get
        Set(value As Integer)
            _C_LiteVersion = value
        End Set
    End Property
    Public Property C_Logo() As Byte()
        Get
            Return _C_Logo
        End Get
        Set(value As Byte())
            _C_Logo = value
        End Set
    End Property
    Public Property C_Code() As String
        Get
            Return _C_Code
        End Get
        Set(value As String)
            _C_Code = value
        End Set
    End Property
    Public Property C_Email() As String
        Get
            Return _C_Email
        End Get
        Set(value As String)
            _C_Email = value
        End Set
    End Property
    Public Property C_Description() As String
        Get
            Return _C_Description
        End Get
        Set(value As String)
            _C_Description = value
        End Set
    End Property
    Public Property C_Postal() As String
        Get
            Return _C_Postal
        End Get
        Set(value As String)
            _C_Postal = value
        End Set
    End Property
    Public Property C_Website() As String
        Get
            Return _C_Website
        End Get
        Set(value As String)
            _C_Website = value
        End Set
    End Property
    Public Property C_Contact() As String
        Get
            Return _C_Contact
        End Get
        Set(value As String)
            _C_Contact = value
        End Set
    End Property
    Public Property C_Registration_no() As Integer
        Get
            Return _C_Registration_no
        End Get
        Set(value As Integer)
            _C_Registration_no = value
        End Set
    End Property
    Public Property C_GST_VAT() As Integer
        Get
            Return _C_GST_VAT
        End Get
        Set(value As Integer)
            _C_GST_VAT = value
        End Set
    End Property
    Public Property C_CST_VAT() As Integer
        Get
            Return _C_CST_VAT
        End Get
        Set(value As Integer)
            _C_CST_VAT = value
        End Set
    End Property
    Public Property C_Service_tax_no() As Integer
        Get
            Return _C_Service_tax_no
        End Get
        Set(value As Integer)
            _C_Service_tax_no = value
        End Set
    End Property
    Public Property C_Pan_no() As Integer
        Get
            Return _C_Pan_no
        End Get
        Set(value As Integer)
            _C_Pan_no = value
        End Set
    End Property

    Public Property Vat_No() As String
        Get
            Return _Vat_No
        End Get
        Set(ByVal value As String)
            _Vat_No = value
        End Set
    End Property

    Public Property Receipt_Header() As String
        Get
            Return _Receipt_Header
        End Get
        Set(ByVal value As String)
            _Receipt_Header = value
        End Set
    End Property
    Public Property Receipt_Footer() As String
        Get
            Return _Receipt_Footer
        End Get
        Set(ByVal value As String)
            _Receipt_Footer = value
        End Set
    End Property
    Public Property log_off_time() As Integer
        Get
            Return _log_off_time
        End Get
        Set(value As Integer)
            _log_off_time = value
        End Set
    End Property

    Public Property week_start_day() As String
        Get
            Return _week_start_day
        End Get
        Set(ByVal value As String)
            _week_start_day = value
        End Set
    End Property

#End Region

    Public Function Insert() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Company_id", Company_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", C_Name)
            oColSqlparram.Add("@Address", C_Address)
            oColSqlparram.Add("@Starting_Date", C_Starting_Date, SqlDbType.DateTime)
            oColSqlparram.Add("@Domain", C_Domain)
            oColSqlparram.Add("@IP_Address", U_IP_Address)
            oColSqlparram.Add("@Login_id", U_Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Country", C_Country, SqlDbType.Int)
            oColSqlparram.Add("@State", C_State, SqlDbType.Int)
            oColSqlparram.Add("@City", C_City, SqlDbType.Int)
            oColSqlparram.Add("@ProductType", C_LiteVersion, SqlDbType.Int)
            If Not C_Logo Is Nothing Then
                oColSqlparram.Add("@Logo", C_Logo, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Code", C_Code)
            oColSqlparram.Add("@Email", C_Email)
            oColSqlparram.Add("@Description", C_Description)
            oColSqlparram.Add("@Postal", C_Postal)
            oColSqlparram.Add("@Website", C_Website)
            oColSqlparram.Add("@Contact", C_Contact)
            oColSqlparram.Add("@Registration_no", C_Registration_no, SqlDbType.Int)
            oColSqlparram.Add("@GST_VAT", C_GST_VAT, SqlDbType.Int)
            oColSqlparram.Add("@CST_VAT", C_CST_VAT, SqlDbType.Int)
            oColSqlparram.Add("@Service_tax_no", C_Service_tax_no, SqlDbType.Int)
            oColSqlparram.Add("@Pan_no", C_Pan_no, SqlDbType.Int)
            oColSqlparram.Add("@Branch_Name", Branch_Name)
            oColSqlparram.Add("@Synchronization", Synchronization)
            oColSqlparram.Add("@Venue_Name", Venue_Name)
            oColSqlparram.Add("@Vat_No", Vat_No)
            oColSqlparram.Add("@Receipt_Header", Receipt_Header)
            oColSqlparram.Add("@Receipt_Footer", Receipt_Footer)
            oColSqlparram.Add("@log_off_time", log_off_time)
            oColSqlparram.Add("@par_sale_par_operator", par_sale_par_operator, SqlDbType.Int)
            oColSqlparram.Add("@Currency_id", Currency_Id, SqlDbType.Int)
            oColSqlparram.Add("@judo_id", judo_id)
            oColSqlparram.Add("@judo_token", judo_token)
            oColSqlparram.Add("@judo_secret", judo_secret)
            oColSqlparram.Add("@week_start_day", week_start_day)
            oColSqlparram.Add("@Show_Chips", Show_chips, SqlDbType.TinyInt)
            oColSqlparram.Add("@BusinessDoneBy", BusinessDoneBy, SqlDbType.Int)
            oColSqlparram.Add("@Display_declaration", Display_declaration, SqlDbType.TinyInt)
            oColSqlparram.Add("@chk_duration", chk_duration, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsAddTax2", IsAddTax2, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsExclusiveTax", IsExclusiveTax, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsPaymentGtway", IsPaymentGtway, SqlDbType.TinyInt)

            oClsDal.ExecStoredProcedure("P_M_Company", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Company_id", Company_id, SqlDbType.Int)
            oColSqlparram.Add("@Name", C_Name)
            oColSqlparram.Add("@Address", C_Address)
            oColSqlparram.Add("@Starting_Date", C_Starting_Date, SqlDbType.DateTime)
            oColSqlparram.Add("@Domain", C_Domain)
            oColSqlparram.Add("@IP_Address", U_IP_Address)
            oColSqlparram.Add("@Login_id", U_Login_id, SqlDbType.Int)
            oColSqlparram.Add("@Country", C_Country, SqlDbType.Int)
            oColSqlparram.Add("@State", C_State, SqlDbType.Int)
            oColSqlparram.Add("@City", C_City, SqlDbType.Int)
            oColSqlparram.Add("@ProductType", C_LiteVersion, SqlDbType.Int)
            If Not C_Logo Is Nothing Then
                oColSqlparram.Add("@Logo", C_Logo, SqlDbType.Image)
            End If
            oColSqlparram.Add("@Code", C_Code)
            oColSqlparram.Add("@Email", C_Email)
            oColSqlparram.Add("@Description", C_Description)
            oColSqlparram.Add("@Postal", C_Postal)
            oColSqlparram.Add("@Website", C_Website)
            oColSqlparram.Add("@Contact", C_Contact)
            oColSqlparram.Add("@Registration_no", C_Registration_no, SqlDbType.Int)
            oColSqlparram.Add("@GST_VAT", C_GST_VAT, SqlDbType.Int)
            oColSqlparram.Add("@CST_VAT", C_CST_VAT, SqlDbType.Int)
            oColSqlparram.Add("@Service_tax_no", C_Service_tax_no, SqlDbType.Int)
            oColSqlparram.Add("@Pan_no", C_Pan_no, SqlDbType.Int)
            oColSqlparram.Add("@Branch_Name", Branch_Name)
            oColSqlparram.Add("@Synchronization", Synchronization)
            oColSqlparram.Add("@Venue_Name", Venue_Name)
            oColSqlparram.Add("@Vat_No", Vat_No)
            oColSqlparram.Add("@Receipt_Header", Receipt_Header)
            oColSqlparram.Add("@Receipt_Footer", Receipt_Footer)
            oColSqlparram.Add("@log_off_time", log_off_time)
            oColSqlparram.Add("@par_sale_par_operator", par_sale_par_operator, SqlDbType.Int)
            oColSqlparram.Add("@Currency_id", Currency_Id, SqlDbType.Int)
            oColSqlparram.Add("@judo_id", judo_id)
            oColSqlparram.Add("@judo_token", judo_token)
            oColSqlparram.Add("@judo_secret", judo_secret)
            oColSqlparram.Add("@week_start_day", week_start_day)
            oColSqlparram.Add("@Show_Chips", Show_chips, SqlDbType.TinyInt)
            oColSqlparram.Add("@BusinessDoneBy", BusinessDoneBy, SqlDbType.Int)
            oColSqlparram.Add("@Display_declaration", Display_declaration, SqlDbType.TinyInt)
            oColSqlparram.Add("@chk_duration", chk_duration, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsAddTax2", IsAddTax2, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsExclusiveTax", IsExclusiveTax, SqlDbType.TinyInt)
            oColSqlparram.Add("@IsPaymentGtway", IsPaymentGtway, SqlDbType.TinyInt)

            oClsDal.ExecStoredProcedure("P_M_Company", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Update_Price() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Company_id, SqlDbType.Int)

            oClsDal.ExecStoredProcedure("Update_M_Price_BY_Tax2Selection", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Function
    Public Function Update_newStore() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Company_id ", Company_id, SqlDbType.Int)
            oColSqlparram.Add("@Country", 80, SqlDbType.Int)
            oColSqlparram.Add("@State", 2511, SqlDbType.Int)
            oColSqlparram.Add("@City", 145729, SqlDbType.Int)
            oColSqlparram.Add("@Currency_id", 2, SqlDbType.Int)
            oClsDal.ExecStoredProcedure("update_New_store", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Function
    Public Function checkProduct() As DataTable
        Dim ds As New DataSet

        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Company_id, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Check_product", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)

            Return New DataTable()
        End Try


    End Function
    Public Function checkgroup() As DataTable
        Dim ds As New DataSet

        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Company_id, SqlDbType.Int)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("check_Group", oColSqlparram)
            Return dtlogin
        Catch ex As Exception
            Throw New Exception(ex.Message)

            Return New DataTable()
        End Try


    End Function


    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Company_id", Company_id)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_M_Company", oColSqlparram)

        Return dtlogin
    End Function

    Public Function [Select_fornew_user]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()

        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Select_fornew_user", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [selectLite]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_id", Company_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("GetGroupsAndDepartments", oColSqlparram)

        Return dtlogin
    End Function
    Public Function [ClearAll]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        oColSqlparram.Add("@Cmp_id", Company_id, SqlDbType.Int)
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("ClearAll_data", oColSqlparram)

        Return dtlogin
    End Function

End Class
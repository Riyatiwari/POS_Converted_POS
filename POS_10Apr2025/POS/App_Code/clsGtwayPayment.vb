Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Threading

Public Class Controller_clsGtwayPayment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Public Sub New()
    End Sub

#Region "Public Variables"
    Private _MerchantID As String
    Private _TID As String
    Private _StoreID As String
    Private _Date_val As String
    Private _Time As String
    Private _Ref As String
    Private _RelatedTransaction As String
    Private _AcquirerRef As String
    Private _Type As String
    Private _Class_val As String
    Private _Currency As String
    Private _Amount As String
    Private _CartID As String
    Private _CartDesc As String
    Private _Name As String
    Private _Address As String
    Private _Country As String
    Private _Postcode As String
    Private _Email As String
    Private _Phone As String
    Private _Card As String
    Private _Card_Type As String
    Private _CardCountry As String
    Private _Region As String
    Private _Expiry As String
    Private _AuthStatus As String
    Private _SettleStatus As String
    Private _AuthCode As String
    Private _AuthMessage As String
    Private _AVS_CVV_IVR As String
    Private _DS_Version As String
    Private _ECI As String
    Private _DSCAVV As String
    Private _DSXID As String
    Private _DSTRANSID As String
    Private _cmp_id As Integer
    Private _LocationID As Decimal
    Private _dt As DataTable
    Private objclsGtway As clsGtwayPayment
#End Region

#Region "Public Property"

    Public Property LocationID() As Decimal
        Get
            Return _LocationID
        End Get
        Set(ByVal value As Decimal)
            _LocationID = value
        End Set

    End Property
    Public Property dt() As DataTable
        Get
            Return _dt
        End Get
        Set(ByVal value As DataTable)
            _dt = value
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

    Public Property DSTRANSID() As String
        Get
            Return _DSTRANSID
        End Get
        Set(ByVal value As String)
            _DSTRANSID = value
        End Set
    End Property

    Public Property DSXID() As String
        Get
            Return _DSXID
        End Get
        Set(ByVal value As String)
            _DSXID = value
        End Set
    End Property

    Public Property DSCAVV() As String
        Get
            Return _DSCAVV
        End Get
        Set(ByVal value As String)
            _DSCAVV = value
        End Set
    End Property

    Public Property ECI() As String
        Get
            Return _ECI
        End Get
        Set(ByVal value As String)
            _ECI = value
        End Set
    End Property

    Public Property DS_Version() As String
        Get
            Return _DS_Version
        End Get
        Set(ByVal value As String)
            _DS_Version = value
        End Set
    End Property

    Public Property AVS_CVV_IVR() As String
        Get
            Return _AVS_CVV_IVR
        End Get
        Set(ByVal value As String)
            _AVS_CVV_IVR = value
        End Set
    End Property

    Public Property AuthMessage() As String
        Get
            Return _AuthMessage
        End Get
        Set(ByVal value As String)
            _AuthMessage = value
        End Set
    End Property

    Public Property AuthCode() As String
        Get
            Return _AuthCode
        End Get
        Set(ByVal value As String)
            _AuthCode = value
        End Set
    End Property

    Public Property SettleStatus() As String
        Get
            Return _SettleStatus
        End Get
        Set(ByVal value As String)
            _SettleStatus = value
        End Set
    End Property

    Public Property AuthStatus() As String
        Get
            Return _AuthStatus
        End Get
        Set(ByVal value As String)
            _AuthStatus = value
        End Set
    End Property

    Public Property Expiry() As String
        Get
            Return _Expiry
        End Get
        Set(ByVal value As String)
            _Expiry = value
        End Set
    End Property

    Public Property Region() As String
        Get
            Return _Region
        End Get
        Set(ByVal value As String)
            _Region = value
        End Set
    End Property

    Public Property CardCountry() As String
        Get
            Return _CardCountry
        End Get
        Set(ByVal value As String)
            _CardCountry = value
        End Set
    End Property

    Public Property Card_Type() As String
        Get
            Return _Card_Type
        End Get
        Set(ByVal value As String)
            _Card_Type = value
        End Set
    End Property

    Public Property Card() As String
        Get
            Return _Card
        End Get
        Set(ByVal value As String)
            _Card = value
        End Set
    End Property

    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Postcode() As String
        Get
            Return _Postcode
        End Get
        Set(ByVal value As String)
            _Postcode = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return _Country
        End Get
        Set(ByVal value As String)
            _Country = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property CartDesc() As String
        Get
            Return _CartDesc
        End Get
        Set(ByVal value As String)
            _CartDesc = value
        End Set
    End Property

    Public Property CartID() As String
        Get
            Return _CartID
        End Get
        Set(ByVal value As String)
            _CartID = value
        End Set
    End Property

    Public Property Amount() As String
        Get
            Return _Amount
        End Get
        Set(ByVal value As String)
            _Amount = value
        End Set
    End Property

    Public Property Currency() As String
        Get
            Return _Currency
        End Get
        Set(ByVal value As String)
            _Currency = value
        End Set
    End Property

    Public Property Class_val() As String
        Get
            Return _Class_val
        End Get
        Set(ByVal value As String)
            _Class_val = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    Public Property AcquirerRef() As String
        Get
            Return _AcquirerRef
        End Get
        Set(ByVal value As String)
            _AcquirerRef = value
        End Set
    End Property

    Public Property RelatedTransaction() As String
        Get
            Return _RelatedTransaction
        End Get
        Set(ByVal value As String)
            _RelatedTransaction = value
        End Set
    End Property

    Public Property Ref() As String
        Get
            Return _Ref
        End Get
        Set(ByVal value As String)
            _Ref = value
        End Set
    End Property

    Public Property Time() As String
        Get
            Return _Time
        End Get
        Set(ByVal value As String)
            _Time = value
        End Set
    End Property


    Public Property Date_val() As String
        Get
            Return _Date_val
        End Get
        Set(ByVal value As String)
            _Date_val = value
        End Set
    End Property


    Public Property StoreID() As String
        Get
            Return _StoreID
        End Get
        Set(ByVal value As String)
            _StoreID = value
        End Set
    End Property

    Public Property TID() As String
        Get
            Return _TID
        End Get
        Set(ByVal value As String)
            _TID = value
        End Set
    End Property


    Public Property MerchantID() As String
        Get
            Return _MerchantID
        End Get
        Set(ByVal value As String)
            _MerchantID = value
        End Set
    End Property


#End Region

    Public Function ImportData_new() As Boolean
        Try
            objclsGtway = New clsGtwayPayment()
            objclsGtway.MerchantID = MerchantID
            objclsGtway.StoreID = StoreID
            objclsGtway.TID = TID
            objclsGtway.Date_val = Date_val
            objclsGtway.Time = Time
            objclsGtway.Ref = Ref
            objclsGtway.RelatedTransaction = RelatedTransaction
            objclsGtway.AcquirerRef = AcquirerRef
            objclsGtway.Type = Type
            objclsGtway.Class_val = Class_val

            objclsGtway.Currency = Currency
            objclsGtway.Amount = Amount
            objclsGtway.CartID = CartID
            objclsGtway.CartDesc = CartDesc
            objclsGtway.Name = Name
            objclsGtway.Address = Address
            objclsGtway.Country = Country
            objclsGtway.Postcode = Postcode
            objclsGtway.Email = Email
            objclsGtway.Phone = Phone

            objclsGtway.Card = Card
            objclsGtway.Card_Type = Card_Type
            objclsGtway.CardCountry = CardCountry
            objclsGtway.Region = Region
            objclsGtway.Expiry = Expiry
            objclsGtway.AuthCode = AuthCode
            objclsGtway.AuthStatus = AuthStatus
            objclsGtway.SettleStatus = SettleStatus
            objclsGtway.AuthMessage = AuthMessage
            objclsGtway.AVS_CVV_IVR = AVS_CVV_IVR

            objclsGtway.DSCAVV = DSCAVV
            objclsGtway.DSTRANSID = DSTRANSID
            objclsGtway.DSXID = DSXID
            objclsGtway.DS_Version = DS_Version
            objclsGtway.ECI = ECI

            If objclsGtway.ImportData_new() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function InsertData() As Boolean
        Try
            objclsGtway = New clsGtwayPayment()
            objclsGtway.dt = dt

            If objclsGtway.InsertData() Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateMerchatID() As Boolean
        Try
            objclsGtway = New clsGtwayPayment()
            objclsGtway.MerchantID = MerchantID
            objclsGtway.LocationID = LocationID

            If objclsGtway.UpdateMerchatID() Then
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
            objclsGtway = New clsGtwayPayment()
            dt = objclsGtway.[Select]()
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsGtwayPayment
    Inherits BaseClass
    Dim oClsDal As ClsDataccess = New ClsDataccess()

    Public Sub New()

    End Sub
#Region "Public Variables"
    Private _MerchantID As String
    Private _TID As String
    Private _StoreID As String
    Private _Date_val As String
    Private _Time As String
    Private _Ref As String
    Private _RelatedTransaction As String
    Private _AcquirerRef As String
    Private _Type As String
    Private _Class_val As String
    Private _Currency As String
    Private _Amount As String
    Private _CartID As String
    Private _CartDesc As String
    Private _Name As String
    Private _Address As String
    Private _Country As String
    Private _Postcode As String
    Private _Email As String
    Private _Phone As String
    Private _Card As String
    Private _Card_Type As String
    Private _CardCountry As String
    Private _Region As String
    Private _Expiry As String
    Private _AuthStatus As String
    Private _SettleStatus As String
    Private _AuthCode As String
    Private _AuthMessage As String
    Private _AVS_CVV_IVR As String
    Private _DS_Version As String
    Private _ECI As String
    Private _DSCAVV As String
    Private _DSXID As String
    Private _DSTRANSID As String
    Private _cmp_id As Integer
    Private _LocationID As Decimal
    Private _dt As DataTable
#End Region

#Region "Public Property"

    Public Property LocationID() As Decimal
        Get
            Return _LocationID
        End Get
        Set(ByVal value As Decimal)
            _LocationID = value
        End Set
    End Property
    Public Property dt() As DataTable
        Get
            Return _dt
        End Get
        Set(ByVal value As DataTable)
            _dt = value
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

    Public Property DSTRANSID() As String
        Get
            Return _DSTRANSID
        End Get
        Set(ByVal value As String)
            _DSTRANSID = value
        End Set
    End Property

    Public Property DSXID() As String
        Get
            Return _DSXID
        End Get
        Set(ByVal value As String)
            _DSXID = value
        End Set
    End Property

    Public Property DSCAVV() As String
        Get
            Return _DSCAVV
        End Get
        Set(ByVal value As String)
            _DSCAVV = value
        End Set
    End Property

    Public Property ECI() As String
        Get
            Return _ECI
        End Get
        Set(ByVal value As String)
            _ECI = value
        End Set
    End Property

    Public Property DS_Version() As String
        Get
            Return _DS_Version
        End Get
        Set(ByVal value As String)
            _DS_Version = value
        End Set
    End Property

    Public Property AVS_CVV_IVR() As String
        Get
            Return _AVS_CVV_IVR
        End Get
        Set(ByVal value As String)
            _AVS_CVV_IVR = value
        End Set
    End Property

    Public Property AuthMessage() As String
        Get
            Return _AuthMessage
        End Get
        Set(ByVal value As String)
            _AuthMessage = value
        End Set
    End Property

    Public Property AuthCode() As String
        Get
            Return _AuthCode
        End Get
        Set(ByVal value As String)
            _AuthCode = value
        End Set
    End Property

    Public Property SettleStatus() As String
        Get
            Return _SettleStatus
        End Get
        Set(ByVal value As String)
            _SettleStatus = value
        End Set
    End Property

    Public Property AuthStatus() As String
        Get
            Return _AuthStatus
        End Get
        Set(ByVal value As String)
            _AuthStatus = value
        End Set
    End Property

    Public Property Expiry() As String
        Get
            Return _Expiry
        End Get
        Set(ByVal value As String)
            _Expiry = value
        End Set
    End Property

    Public Property Region() As String
        Get
            Return _Region
        End Get
        Set(ByVal value As String)
            _Region = value
        End Set
    End Property

    Public Property CardCountry() As String
        Get
            Return _CardCountry
        End Get
        Set(ByVal value As String)
            _CardCountry = value
        End Set
    End Property

    Public Property Card_Type() As String
        Get
            Return _Card_Type
        End Get
        Set(ByVal value As String)
            _Card_Type = value
        End Set
    End Property

    Public Property Card() As String
        Get
            Return _Card
        End Get
        Set(ByVal value As String)
            _Card = value
        End Set
    End Property

    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Postcode() As String
        Get
            Return _Postcode
        End Get
        Set(ByVal value As String)
            _Postcode = value
        End Set
    End Property

    Public Property Country() As String
        Get
            Return _Country
        End Get
        Set(ByVal value As String)
            _Country = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property CartDesc() As String
        Get
            Return _CartDesc
        End Get
        Set(ByVal value As String)
            _CartDesc = value
        End Set
    End Property

    Public Property CartID() As String
        Get
            Return _CartID
        End Get
        Set(ByVal value As String)
            _CartID = value
        End Set
    End Property

    Public Property Amount() As String
        Get
            Return _Amount
        End Get
        Set(ByVal value As String)
            _Amount = value
        End Set
    End Property

    Public Property Currency() As String
        Get
            Return _Currency
        End Get
        Set(ByVal value As String)
            _Currency = value
        End Set
    End Property

    Public Property Class_val() As String
        Get
            Return _Class_val
        End Get
        Set(ByVal value As String)
            _Class_val = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

    Public Property AcquirerRef() As String
        Get
            Return _AcquirerRef
        End Get
        Set(ByVal value As String)
            _AcquirerRef = value
        End Set
    End Property

    Public Property RelatedTransaction() As String
        Get
            Return _RelatedTransaction
        End Get
        Set(ByVal value As String)
            _RelatedTransaction = value
        End Set
    End Property

    Public Property Ref() As String
        Get
            Return _Ref
        End Get
        Set(ByVal value As String)
            _Ref = value
        End Set
    End Property

    Public Property Time() As String
        Get
            Return _Time
        End Get
        Set(ByVal value As String)
            _Time = value
        End Set
    End Property


    Public Property Date_val() As String
        Get
            Return _Date_val
        End Get
        Set(ByVal value As String)
            _Date_val = value
        End Set
    End Property


    Public Property StoreID() As String
        Get
            Return _StoreID
        End Get
        Set(ByVal value As String)
            _StoreID = value
        End Set
    End Property

    Public Property TID() As String
        Get
            Return _TID
        End Get
        Set(ByVal value As String)
            _TID = value
        End Set
    End Property


    Public Property MerchantID() As String
        Get
            Return _MerchantID
        End Get
        Set(ByVal value As String)
            _MerchantID = value
        End Set
    End Property


#End Region

    Public Function ImportData_new() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@MerchantID", MerchantID)
            oColSqlparram.Add("@StoreID", StoreID)
            oColSqlparram.Add("@TID", TID)
            oColSqlparram.Add("@Date", Date_val)
            oColSqlparram.Add("@Time", Time)
            oColSqlparram.Add("@Ref", Ref)
            oColSqlparram.Add("@RelatedTransaction", RelatedTransaction)
            oColSqlparram.Add("@AcquirerRef", AcquirerRef)
            oColSqlparram.Add("@Type", Type)
            oColSqlparram.Add("@Class", Class_val)
            oColSqlparram.Add("@Currency", Currency)
            oColSqlparram.Add("@Amount", Amount)
            oColSqlparram.Add("@CartID", CartID)
            oColSqlparram.Add("@CartDesc", CartDesc)
            oColSqlparram.Add("@Name", Name)
            oColSqlparram.Add("@Address", Address)
            oColSqlparram.Add("@Country", Country)
            oColSqlparram.Add("@Postcode", Postcode)
            oColSqlparram.Add("@Email", Email)
            oColSqlparram.Add("@Phone", Phone)
            oColSqlparram.Add("@Card", Card)
            oColSqlparram.Add("@Card_Type", Card_Type)
            oColSqlparram.Add("@CardCountry", CardCountry)
            oColSqlparram.Add("@Region", Region)
            oColSqlparram.Add("@Expiry", Expiry)
            oColSqlparram.Add("@AuthCode", AuthCode)
            oColSqlparram.Add("@AuthStatus", AuthStatus)
            oColSqlparram.Add("@AuthMessage", AuthMessage)
            oColSqlparram.Add("@SettleStatus", SettleStatus)
            oColSqlparram.Add("@AVS_CVV_IVR", AVS_CVV_IVR)
            oColSqlparram.Add("@DSCAVV", DSCAVV)
            oColSqlparram.Add("@DSTRANSID", DSTRANSID)
            oColSqlparram.Add("@DSXID", DSXID)
            oColSqlparram.Add("@DS_Version", DS_Version)
            oColSqlparram.Add("@ECI", ECI)


            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Import_GtwayPayment_Data", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsertData() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Dt", dt, SqlDbType.Structured)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Insert_PaymentGtWay_Data", oColSqlparram)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateMerchatID() As Boolean
        Try
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@gtway_LocationID", LocationID, SqlDbType.Decimal)
            oColSqlparram.Add("@gtway_MerchantID", MerchantID)

            Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Update_MerchantID_In_Location", oColSqlparram)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function [Select]() As DataTable
        Dim ds As New DataSet

        Dim oColSqlparram As ColSqlparram = New ColSqlparram()
        Dim dtlogin As DataTable = oClsDal.GetdataTableSp("Get_maxDate_From_GtwayPayment", oColSqlparram)

        Return dtlogin
    End Function

End Class

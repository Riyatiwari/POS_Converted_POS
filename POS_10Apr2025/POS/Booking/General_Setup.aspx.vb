Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_Settings2
    Inherits System.Web.UI.Page
    'Dim oClsDataccess As New ClsDataccess

#Region "Properties"
    Private Property BookingSettingsid() As Integer
        Get
            Try
                Return Utils.getInteger(ViewState("BookingSettingsid"))
            Catch
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            ViewState("BookingSettingsid") = value
        End Set
    End Property
#End Region

#Region "Methods"

#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        divContent.Visible = True
        divLogin.Visible = False
        'If Session("AllowSetting") IsNot Nothing Then
        '    If Convert.ToInt32(Session("AllowSetting")) = 1 Then
        '        divContent.Visible = True
        '        divLogin.Visible = False
        '    Else
        '        Response.Redirect("~/Login.aspx")
        '    End If
        'Else
        '    Response.Redirect("~/Login.aspx")
        'End If
        If (Not Page.IsPostBack) Then
            Dim conn As DBConnection = New DBConnection()
            Dim str As String = conn.SingleCell("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;")
            If (str <> "0") Then

                LoadVenueMappingData()
                LoadOtherVenue()
                LoadVenueDropdown()
                BindStoreMasterData()
                'LoadXMLData()
                BindTabGrid()
                BindDefaultField()
                ViewState("edit") = Nothing
                ViewState("save") = Nothing
                ViewState("edit_Redirect") = Nothing
            End If
        End If
    End Sub
#End Region

#Region "Controls Events"

    Protected Sub btnCreateTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateTable.Click
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim str As String = conn.SingleCell("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;")
            If (str = "0") Then
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[TabMaster]([TabId] [int] IDENTITY(1,1) NOT NULL,[TabName] [varchar](max) NULL,[TabType] [int] NULL,CONSTRAINT [PK_TabMaster] PRIMARY KEY CLUSTERED ([TabId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[VenueMaster]([BookingVenueId] [int] IDENTITY(1,1) NOT NULL,[VenueName] [varchar](max) NULL,[OtherVenueId] [int] NULL,CONSTRAINT [PK_VenueMaster] PRIMARY KEY CLUSTERED ([BookingVenueId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[StoreMaster]([OurStoreId] [int] IDENTITY(1,1) NOT NULL,[StoreName] [varchar](max) NULL,[BookingVenueId] [int] NULL,[OtherStoreId] [int] NULL,CONSTRAINT [PK_StoreMaster] PRIMARY KEY CLUSTERED ([OurStoreId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[bookingsettings]([BookingSettingsid] [int] IDENTITY(1,1) NOT NULL,[bookingtype] [nvarchar](100) NULL,[bookingtillid] [int] NULL,[Numberofrooms] [int] NULL,[Sort] [int] NULL,[Guestaccount] [int] NULL,[CheckedGuestAccount] [int] NULL,[cancelledRoom] [int] NULL,[TableGroup] [int] NULL,[StoreID] [int] NULL,[IsActive] [bit] NULL,[RedirectScheduleTo] [int] NULL,[IsShowOnWidget] [bit] NULL,[HotelTabID] [int] NULL,[RestTabID] [int] NULL,[livetablegroup] [int] NULL,CONSTRAINT [PK_bookingsettings] PRIMARY KEY CLUSTERED ([BookingSettingsid] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[bookings]([bookingid] [int] IDENTITY(1,1) NOT NULL,[covers] [int] NULL,[date] [smalldatetime] NULL,[comment] [nvarchar](max) NULL,[Roomid] [int] NULL,[arrivaldate] [smalldatetime] NULL,[departuredate] [smalldatetime] NULL,[checkedin] [int] NULL,[bookingref] [nvarchar](max) NULL,[bookingtotal] [decimal](18, 0) NULL,[deposit] [decimal](18, 0) NULL,[accountid] [int] NULL,[period] [int] NULL,[bookingtime] [time](7) NULL,[BookingSettingsid] [int] NULL,[IsCanceled] [bit] NULL,[CreatedDate] [datetime] NULL,[CreatedBy] [int] NULL,[IPAddress] [varchar](50) NULL,[GrandTotal] [decimal](18, 0) NULL,[BookingScheduleID] [int] NULL,[BookingScheduleDateId] [int] NULL,[IsOnline] [bit] NULL,[OurStoreId] [int] NULL,[GroupID] [int] NULL,[Allotted_Tables] [nvarchar](500) NULL,[TableNo] [int] NULL,[IsSeated] [tinyint] NOT NULL,[livetablegroup] [int] NULL,CONSTRAINT [PK_bookings] PRIMARY KEY CLUSTERED ([bookingid] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] GO SET ANSI_PADDING OFF GO ALTER TABLE [dbo].[bookings] ADD  CONSTRAINT [DF_bookings_IsSeated]  DEFAULT ((0)) FOR [IsSeated] GO")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[BookingServices]([BookingServiceID] [int] IDENTITY(1,1) NOT NULL,[bookingref] [nvarchar](max) NULL,[ProductID] [int] NULL,[Quantity] [int] NULL,[Price] [decimal](10, 2) NULL,[TotalPrice] [decimal](10, 2) NULL,CONSTRAINT [PK_BookingServices] PRIMARY KEY CLUSTERED ([BookingServiceID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[BookingSchedule]([BookingScheduleID] [int] IDENTITY(1,1) NOT NULL,[BookingSettingsID] [int] NULL,[Name] [varchar](50) NULL,[StartTime] [time](7) NULL,[EndTime] [time](7) NULL,[NumberOfCover] [int] NULL,[ServiceDuration] [int] NULL,[TimeSpan] [int] NULL,[PaymentType] [int] NULL,[DepositPercentage] [decimal](8, 2) NULL,[DepositAmount] [decimal](8, 2) NULL,[StartDate] [date] NULL,[EndDate] [date] NULL,[FutureReservationTime] [int] NULL,[OnlineMaxCovers] [int] NULL,[MinCovers] [int] NOT NULL,[One_booking_at_a_time] [tinyint] NOT NULL,[GroupID] [int] NOT NULL,[is_selectProduct] [tinyint] NOT NULL,[MCPTimeSpan] [int] NULL,CONSTRAINT [PK_BookingSchedule] PRIMARY KEY CLUSTERED ([BookingScheduleID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] GO SET ANSI_PADDING OFF GO ALTER TABLE [dbo].[BookingSchedule] ADD  CONSTRAINT [DF_BookingSchedule_MinCovers]  DEFAULT ((0)) FOR [MinCovers] GO ALTER TABLE [dbo].[BookingSchedule] ADD  CONSTRAINT [DF_BookingSchedule_One_booking_at_a_time]  DEFAULT ((0)) FOR [One_booking_at_a_time] GO ALTER TABLE [dbo].[BookingSchedule] ADD  CONSTRAINT [DF_BookingSchedule_GroupID]  DEFAULT ((0)) FOR [GroupID] GO ALTER TABLE [dbo].[BookingSchedule] ADD  CONSTRAINT [DF_BookingSchedule_is_selectProduct]  DEFAULT ((0)) FOR [is_selectProduct] GO")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[BookingScheduleDate]([BookingScheduleDateId] [int] IDENTITY(1,1) NOT NULL,[BookingScheduleId] [int] NULL,[BookingSettingsid] [int] NULL,[ScheduleDate] [date] NULL,[IsAvailable] [bit] NULL,[StartTime] [time](7) NULL,[EndTime] [time](7) NULL,[NumberOfCover] [int] NULL,[ServiceDuration] [int] NULL,[TimeSpan] [int] NULL,[PaymentType] [int] NULL,[DepositPercentage] [decimal](8, 2) NULL,[DepositAmount] [decimal](8, 2) NULL,CONSTRAINT [PK_BookingScheduleDate] PRIMARY KEY CLUSTERED ([BookingScheduleDateId] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[Booking_Initiate]([bookingid] [int] IDENTITY(1,1) NOT NULL,[covers] [int] NULL,[date] [smalldatetime] NULL,[comment] [nvarchar](max) NULL,[Roomid] [int] NULL,[arrivaldate] [smalldatetime] NULL,[departuredate] [smalldatetime] NULL,[checkedin] [int] NULL,[bookingref] [nvarchar](max) NULL,[bookingtotal] [decimal](10, 2) NULL,[deposit] [decimal](10, 2) NULL,[accountid] [int] NULL,[period] [int] NULL,[bookingtime] [time](7) NULL,[BookingSettingsid] [int] NULL,[IsCanceled] [bit] NULL,[CreatedDate] [datetime] NULL,[CreatedBy] [int] NULL,[IPAddress] [varchar](50) NULL,[GrandTotal] [decimal](10, 2) NULL,[BookingScheduleID] [int] NULL,[BookingScheduleDateId] [int] NULL,[IsOnline] [bit] NULL,[OurStoreId] [int] NULL,CONSTRAINT [PK_Bookings_Start] PRIMARY KEY CLUSTERED ([bookingid] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_Payment_Gateway]([PaymentID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,[Name] [nvarchar](100) NOT NULL,[Gateway] [nvarchar](100) NULL,[LoginID] [nvarchar](100) NULL,[TransactionKey] [nvarchar](200) NULL,[Password] [nvarchar](100) NULL,[URL] [nvarchar](1000) NULL,[ReturnURL] [nvarchar](1000) NULL,[CancelURL] [nvarchar](1000) NULL,[GeoZone] [nvarchar](100) NULL,[Status] [tinyint] NULL,[OrderNo] [numeric](18, 0) NULL,[TransactionMode] [nvarchar](50) NULL,[Currency] [nvarchar](50) NULL,[system_date] [datetime] NULL,CONSTRAINT [PK_M_Payment_Gateway] PRIMARY KEY CLUSTERED ([PaymentID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[T_Payment_Transaction]([Tran_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,[Gateway_id] [numeric](18, 0) NOT NULL,[AccountId] [numeric](18, 0) NULL,[Booking_id] [numeric](18, 0) NULL,[Transaction_ref_no] [nvarchar](300) NULL,[Amount] [numeric](18, 2) NULL,[Currency] [nvarchar](50) NULL,[Trasaction_date] [datetime] NULL,[booking_type] [tinyint] NULL,CONSTRAINT [PK_T_Payment_Transaction] PRIMARY KEY CLUSTERED ([Tran_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_Custom_Field]([FieldID] [int] IDENTITY(1,1) NOT NULL,[FieldName] [nvarchar](50) NULL,[FieldLable] [nvarchar](50) NULL,[FieldType] [int] NULL,[Required_Val] [tinyint] NULL,[MaxVal] [int] NULL,[Validation_Val] [nvarchar](50) NULL,[Choice] [nvarchar](50) NULL,[Sorting_no] [nvarchar](50) NULL,[Enable_Val] [tinyint] NULL,[Mapping_Field] [nvarchar](50) NULL,[Static_Field] [nvarchar](50) NULL,[Mapping_Field_Alies] [nvarchar](50) NULL,CONSTRAINT [PK_M_Custom_Field] PRIMARY KEY CLUSTERED ([FieldID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_FieldType]([Field_Type] [nvarchar](50) NULL,[Field_Number] [int] NULL,[Field_MapName] [nvarchar](50) NULL) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[T_Custome_FieldTracking]([Tracking_Name] [nvarchar](50) NULL,[Tracking_Enable] [int] NULL,[Sorting_No] [int] NULL) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[MultiVanue]( MultiVanueID int NOT NULL IDENTITY (1, 1),VanueName nvarchar(50) NULL,UserName nvarchar(50) NULL,Password nvarchar(50) NULL,DataBase_Name nvarchar(50) NULL,Sync_Date datetime NULL,Is_Active tinyint NULL,Created_Date datetime NULL,Modify_Date datetime NULL,[sync_day] [numeric](18, 0) NOT NULL)  ON [PRIMARY] GO ALTER TABLE [dbo].[MultiVanue] ADD  CONSTRAINT [DF_MultiVanue_sync_day]  DEFAULT ((3)) FOR [sync_day] GO")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[Booking_Synchronize] (BookingSync_ID int NOT NULL IDENTITY (1, 1),Sync_From nvarchar(100) NULL,Sync_To nvarchar(100) NULL,bookingref nvarchar(MAX) NULL,BookingDate datetime NULL,Sync_Date datetime NULL)  ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_Table]([Table_id] [int] IDENTITY(1,1) NOT NULL,[Table_name] [nvarchar](50) NULL,[MinCover] [int] NULL,[MaxCover] [int] NULL,[AllowedJoin] [nvarchar](1000) NULL,[TableNo] [int] NULL,[CreatedDate] [datetime] NOT NULL,[ModifyDate] [datetime] NOT NULL,[CreatedBy] [int] NULL,[IPAddress] [varchar](50) NULL,CONSTRAINT [PK_M_Table_Management] PRIMARY KEY CLUSTERED ([Table_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] GO SET ANSI_PADDING OFF GO")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_Table_Details]([Tran_id] [int] IDENTITY(1,1) NOT NULL,[BookingScheduleID] [int] NULL,[Table_id] [int] NULL,[pageX] [nvarchar](50) NULL,[pageY] [nvarchar](50) NULL,[Shape] [nvarchar](50) NULL,[Size] [nvarchar](50) NULL,[Direction] [nvarchar](50) NULL,[Caption] [nvarchar](100) NULL,CONSTRAINT [PK_M_Table_Details] PRIMARY KEY CLUSTERED ([Tran_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] GO")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_Table_Group]([GroupID] [int] IDENTITY(1,1) NOT NULL,[group_name] [nvarchar](50) NULL,[table_set] [nvarchar](1000) NULL,[CreatedDate] [datetime] NOT NULL,[ModifyDate] [datetime] NOT NULL,[CreatedBy] [int] NULL,[IPAddress] [nvarchar](50) NULL,CONSTRAINT [PK_M_Table_Group] PRIMARY KEY CLUSTERED ([GroupID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] GO")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_Table_Set]([Tran_id] [int] IDENTITY(1,1) NOT NULL,[cover_size] [int] NULL,[div_html] [nvarchar](max) NULL,[Shape] [nvarchar](50) NULL,CONSTRAINT [PK_M_Table_Set] PRIMARY KEY CLUSTERED ([Tran_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] GO")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[M_OpenTable]([Tran_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,[bookingref] [nvarchar](max) NULL,[TableID] [int] NULL, CONSTRAINT [PK_M_OpenTable] PRIMARY KEY CLUSTERED ([Tran_id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] GO")

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "testSuccess", "alert('Success! Tables created successfully.');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "testInfo", "alert('Info! Tables already created.');", True)
            End If
        Catch
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "testError", "alert('Error! While creating new tables.');", True)
        End Try
    End Sub
    Protected Sub btnRetun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetun.Click
        Session("AllowSetting") = Nothing
        Response.Redirect("Dashboard.aspx")
    End Sub

#End Region

    Protected Sub btnSignIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSignIn.Click
        If txtUserName.Text.Equals(Utils.SettingUsername) AndAlso Utils.Encrypt(txtPassword.Text).Equals(Utils.Encrypt(Utils.SettingPassword)) Then
            Session("AllowSetting") = "1"
            divLogin.Visible = False
            divContent.Visible = True
            lblInvalidCredential.Visible = False
        Else
            divLogin.Visible = True
            divContent.Visible = False
            lblInvalidCredential.Visible = True
        End If
    End Sub

    Private Function CheckNameExists(ByVal scheduleId As Int32, ByVal scheduleName As String) As Boolean
        Dim strQuery As String = String.Empty
        strQuery &= " SELECT * FROM BookingSchedule"
        strQuery &= " WHERE Name = '" & scheduleName & "' AND BookingScheduleID != " & scheduleId & " AND BookingSettingsID = " & BookingSettingsid

        Dim objConn As DBConnection = New DBConnection()
        Return objConn.CheckData(strQuery)
    End Function

#Region "Venue Mapping"
    Private Sub LoadVenueMappingData()
        Dim objCommon As Common = New Common()
        gvVenueMap.DataSource = objCommon.GetAllVenueMasterData()
        gvVenueMap.DataBind()
        LoadVenueDropdown()
    End Sub

    Private Sub LoadOtherVenue()
        Dim objCommon As Common = New Common()
        Dim dt As DataTable = objCommon.GetAllVenueForMaster().Tables(0)
        ddlOtherVenue.DataSource = dt
        ddlOtherVenue.DataValueField = "VenueID"
        ddlOtherVenue.DataTextField = "Name"
        ddlOtherVenue.DataBind()
        ddlOtherVenue.ClearSelection()
    End Sub

    Protected Sub btnSaveVenueMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveVenueMap.Click
        Dim objCommon As Common = New Common()
        objCommon.VenueMasterSave(Utils.ParseInt(hdnBookingVenueId.Value), txtVenueName.Text, Utils.ParseInt(ddlOtherVenue.SelectedValue))
        ClearVenueMapping()
        divVenueMap.Attributes.Add("class", "alert alert-success")
        divVenueMap.Visible = True
        lblVenueMapMsg.Text = "Venue Mapping Saved Successfully."
        LoadVenueMappingData()
    End Sub

    Private Sub ClearVenueMapping()
        txtVenueName.Text = ""
        hdnBookingVenueId.Value = 0
        ddlOtherVenue.ClearSelection()
        ddlOtherVenue.Enabled = True
        LoadOtherVenue()
    End Sub

    Protected Sub gvVenueMap_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvVenueMap.ItemCommand
        If (e.CommandName = "EditVenueMap") Then
            Dim objCommon As Common = New Common()
            Dim bookingVenueId As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingVenueId").ToString())
            hdnBookingVenueId.Value = bookingVenueId
            Dim dr As DataRow = objCommon.GetVenueMasterById(bookingVenueId)
            txtVenueName.Text = Utils.NullToString(dr("VenueName"))
            ddlOtherVenue.Items.Clear()
            Dim drVenue As DataSet = objCommon.GetVenueById(Utils.ParseInt(dr("OtherVenueId")))
            ddlOtherVenue.DataSource = drVenue
            ddlOtherVenue.DataValueField = "VenueID"
            ddlOtherVenue.DataTextField = "Name"
            ddlOtherVenue.DataBind()
            ddlOtherVenue.SelectedValue = Utils.NullToString(dr("OtherVenueId"))
            ddlOtherVenue.Enabled = False
        ElseIf (e.CommandName = "DeleteVenueMap") Then
            Dim objCommon As Common = New Common()
            Dim bookingVenueId As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingVenueId").ToString())
            objCommon.DeleteVenueMasterById(bookingVenueId)
            LoadVenueMappingData()
            LoadOtherVenue()
        End If
    End Sub

    Protected Sub gvVenueMap_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvVenueMap.ItemDataBound
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then
            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)
            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub

    Protected Sub btnCancelVenueName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelVenueName.Click
        ClearVenueMapping()
        ViewState("edit") = Nothing
        ViewState("save") = Nothing
    End Sub
#End Region

#Region "Store Mapping"

    Private Sub LoadVenueDropdown()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetAllVenueMasterData()
            ddlOurVenue.DataSource = ds
            ddlOurVenue.DataValueField = "BookingVenueId"
            ddlOurVenue.DataTextField = "VenueName"
            ddlOurVenue.DataBind()
            ddlOurVenue.ClearSelection()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlOurVenue_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlOurVenue.SelectedIndexChanged
        BindOtherStore(ddlOurVenue.SelectedValue)
    End Sub

    Private Sub BindOtherStore(ByVal bookingVenueId As Int32)
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetOtherStoreForStoreMaster(bookingVenueId)
        ddlOtherStore.DataSource = ds
        ddlOtherStore.DataValueField = "StoreID"
        ddlOtherStore.DataTextField = "Name"
        ddlOtherStore.DataBind()
        ddlOtherStore.ClearSelection()
    End Sub

    Public Sub BindStoreMasterData()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetAllStoreMasterData()

            gvStoreMap.DataSource = ds
            gvStoreMap.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ClearStoreMap()
        txtStoreName.Text = ""
        ddlOurVenue.ClearSelection()
        ddlOtherStore.ClearSelection()
        ddlOtherStore.Items.Clear()
        ddlOurVenue.Enabled = True
        ddlOtherStore.Enabled = True
        hdnOurStoreId.Value = "0"
    End Sub

    Protected Sub btnStoreMapSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStoreMapSave.Click
        Try
            Dim objCommon As Common = New Common()
            objCommon.StoreMasterSave(Utils.ParseInt(hdnOurStoreId.Value), txtStoreName.Text, Utils.ParseInt(ddlOurVenue.SelectedValue), Utils.ParseInt(ddlOtherStore.SelectedValue))
            divStoreMapMsg.Attributes.Add("class", "alert alert-success")
            divStoreMapMsg.Visible = True
            lblStoreMapMsg.Text = "Store Mapping Saved Successfully."
            ClearStoreMap()
            BindStoreMasterData()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnStoreMapCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStoreMapCancel.Click
        ClearStoreMap()
    End Sub

    Protected Sub gvStoreMap_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvStoreMap.ItemCommand
        If (e.CommandName = "EditStoreMap") Then
            Dim objCommon As Common = New Common()
            Dim OurStoreId As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("OurStoreId").ToString())
            hdnOurStoreId.Value = OurStoreId
            Dim dr As DataRow = objCommon.GetStoreMasterById(OurStoreId)
            txtStoreName.Text = Utils.NullToString(dr("StoreName"))
            ddlOurVenue.SelectedValue = Utils.NullToString(dr("BookingVenueId"))
            ddlOurVenue.Enabled = False
            Dim ds As DataSet = objCommon.GetOtherStoreById(Utils.ParseInt(dr("OtherStoreId")))
            ddlOtherStore.DataSource = ds
            ddlOtherStore.DataTextField = "Name"
            ddlOtherStore.DataValueField = "StoreID"
            ddlOtherStore.DataBind()
            ddlOtherStore.SelectedValue = Utils.NullToString(dr("OtherStoreId"))
            ddlOtherStore.Enabled = False
        ElseIf (e.CommandName = "DeleteStoreMap") Then
            Dim objCommon As Common = New Common()
            Dim OurStoreId As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("OurStoreId").ToString())
            objCommon.DeleteStoreMasterById(OurStoreId)
            BindStoreMasterData()
        End If
    End Sub

    Protected Sub gvStoreMap_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvStoreMap.ItemDataBound
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then
            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)
            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub
#End Region

    'Protected Sub btnSaveTabName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveTabName.Click
    '    Dim ds As DataSet = Common.GetXmlData()
    '    Dim dt As DataTable = ds.Tables("TabName")

    '    dt.Clear()

    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Tabs") = txtSearchTab.Text
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Tabs") = txtHotelTab.Text
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Tabs") = txtResTab.Text
    '    dt.Rows.Add(dr)

    '    Common.SetXmlData(ds)
    '    lblTabNameSave.Visible = True
    'End Sub

    'Private Sub LoadXMLData()
    '    Dim ds As DataSet = Common.GetXmlData()
    '    txtSearchTab.Text = ds.Tables("TabName").Rows(0)("Tabs").ToString()
    '    txtHotelTab.Text = ds.Tables("TabName").Rows(1)("Tabs").ToString()
    '    txtResTab.Text = ds.Tables("TabName").Rows(2)("Tabs").ToString()

    '    drpCurrencySymbol.SelectedValue = ds.Tables("Currency").Rows(0)("Symbol").ToString()
    'End Sub

    Protected Sub btnSaveCurrSymbol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveCurrSymbol.Click
        Dim ds As DataSet = Common.GetXmlData()
        Dim dt As DataTable = ds.Tables("Currency")

        dt.Clear()

        Dim dr As DataRow = dt.NewRow()
        dr("Symbol") = drpCurrencySymbol.SelectedValue
        dt.Rows.Add(dr)

        Common.SetXmlData(ds)
        lblCurrencyMessage.Visible = True
    End Sub

    Protected Sub btnSaveField_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveField.Click
        Try
            Dim oClsDataccess As New ClsDataccess
            Dim tran As String
            If hdnDefaultField.Value = "" Then
                tran = "I"
            Else
                tran = "U"
            End If

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@Tran_id", Val(hdnDefaultField.Value), SqlDbType.Int)
            oColSqlparram.Add("@venueId", Val(0), SqlDbType.Int)
            oColSqlparram.Add("@DefaultField", Val(ddlDefaultField.SelectedValue), SqlDbType.Int)
            oColSqlparram.Add("@ip_address", Request.UserHostAddress)
            oColSqlparram.Add("@login_id", Sessions.UserID, SqlDbType.Int)
            oColSqlparram.Add("@Tran_type", tran)
            oClsDataccess.ExecStoredProcedure("p_M_Venue_Settings", oColSqlparram)

            BindDefaultField()

            lblSaveField.Visible = True
        Catch ex As Exception

        End Try
    End Sub

#Region "Tab Settings"
    Private Sub BindTabGrid()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetAllTabNames()
        gvTabs.DataSource = ds
        gvTabs.DataBind()
    End Sub

    Private Sub BindDefaultField()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetDefaultField()
        If ds.Tables(0).Rows.Count > 0 Then
            ddlDefaultField.SelectedValue = ds.Tables(0).Rows(0)("DefaultField")
            hdnDefaultField.Value = ds.Tables(0).Rows(0)("Tran_id")
        End If
    End Sub

    Private Sub ClearTabData()
        txtTabName.Text = ""
        hdnTabId.Value = 0
        rdoRestaurent.Checked = True
        rdoHotel.Checked = False
    End Sub

    Private Sub SaveTabData()
        Try
            Dim strQuery As String
            Dim tabType As Int32
            If rdoHotel.Checked Then
                tabType = 1
            Else
                tabType = 2
            End If
            If hdnTabId.Value = "0" Then
                strQuery = " INSERT INTO TabMaster(TabName,TabType) VALUES ('" & txtTabName.Text & "'," & tabType & ")"
            Else
                strQuery = " UPDATE TabMaster SET TabName = '" & txtTabName.Text & "',TabType = " & tabType & " WHERE TabId = " & hdnTabId.Value
            End If

            Dim conn As DBConnection = New DBConnection()
            conn.Ins_Upd_Del(strQuery)
            ClearTabData()
            BindTabGrid()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSaveTabSetting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveTabSetting.Click
        SaveTabData()
    End Sub

    Protected Sub btnCancelTabSetting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelTabSetting.Click
        ClearTabData()
    End Sub

    Protected Sub gvTabs_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvTabs.ItemCommand

        If (e.CommandName = "EditTabName") Then
            hdnTabId.Value = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("TabId").ToString())
            Dim strQuery As String = " SELECT * FROM TabMaster WHERE TabId = " & hdnTabId.Value
            Dim conn As DBConnection = New DBConnection()
            Dim dr As DataRow = conn.SingleRow(strQuery)
            txtTabName.Text = Utils.NullToString(dr("TabName"))

            If Utils.NullToString(dr("TabType")) = "1" Then
                rdoHotel.Checked = True
            Else
                rdoRestaurent.Checked = True
            End If

        ElseIf (e.CommandName = "DeleteTabName") Then
            Dim tabID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("TabId").ToString())
            Dim strQuery As String = " DELETE FROM TabMaster WHERE TabId = " & tabID
            Dim conn As DBConnection = New DBConnection()
            conn.Ins_Upd_Del(strQuery)
            BindTabGrid()
        End If
    End Sub

    Protected Sub gvTabs_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvTabs.ItemDataBound

        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)


            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub

#End Region

End Class

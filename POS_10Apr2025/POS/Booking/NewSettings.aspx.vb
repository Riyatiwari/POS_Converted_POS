Imports System.Data
Imports Telerik.Web.UI

Partial Class BookingEasy_Settings2
    Inherits System.Web.UI.Page

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
    Private Sub BindVenue()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetAllVenueMasterData()
        ddlVenu.DataSource = ds.Tables(0)
        ddlVenu.DataValueField = "BookingVenueId"
        ddlVenu.DataTextField = "VenueName"
        ddlVenu.DataBind()
        ddlStore.ClearSelection()
    End Sub
    Private Sub BindStore(ByVal venueId As String)
        ddlStore.Items.Clear()
        'Dim conn As DBConnection = New DBConnection()
        'Dim ds As DataSet = conn.SelectData("SELECT * FROM Store WHERE StoreID NOT IN (Select StoreID From bookingsettings WHERE BookingSettingsid <> " + BookingSettingsid.ToString() + ") AND VenueID = " + venueId)
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetStoreMasterByBookingVenueId(venueId, BookingSettingsid)
        ddlStore.DataSource = ds.Tables(0)
        ddlStore.DataValueField = "OurStoreId"
        ddlStore.DataTextField = "StoreName"
        ddlStore.DataBind()
        ddlStore.ClearSelection()
    End Sub
    Private Sub BindGuestAc()
        ddlStore.Items.Clear()
        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData("select Accountid, Lastname from account where isparent >= 1")
        ddlGuestAc.DataSource = ds.Tables(0)
        ddlGuestAc.DataValueField = "Accountid"
        ddlGuestAc.DataTextField = "Lastname"
        ddlGuestAc.DataBind()
    End Sub
    Private Sub BindCheckedGuestAc()
        ddlStore.Items.Clear()
        Dim conn As DBConnection = New DBConnection()
        Dim ds As DataSet = conn.SelectData("select Accountid, Lastname from account where isparent >= 1")
        ddlCheckedGuestAc.DataSource = ds.Tables(0)
        ddlCheckedGuestAc.DataValueField = "Accountid"
        ddlCheckedGuestAc.DataTextField = "Lastname"
        ddlCheckedGuestAc.DataBind()
    End Sub
    Private Sub BindGrid()

        Dim conn As DBConnection = New DBConnection()
        Dim str As String = conn.SingleCell("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;")
        If (str = "1") Then

            Dim strQuery As String = "SELECT BS.*, CASE ISNULL(BS.bookingtype,0) WHEN '" + Utils.Encrypt("1") + "' THEN 'Room' WHEN '" + Utils.Encrypt("2") + "' THEN 'Table' ELSE 'Both' END AS BookingTypeName, S.StoreName As StoreName, V.VenueName AS VenueName,S1.StoreName AS RedirectToName FROM bookingsettings BS left outer join StoreMaster S ON S.OurStoreId = BS.StoreId left outer join VenueMaster V ON V.BookingVenueId = S.BookingVenueId LEFT JOIN bookingsettings BS1 ON BS.RedirectScheduleTo = BS1.BookingSettingsid LEFT JOIN StoreMaster S1 ON S1.OurStoreId = BS1.StoreId"

            If Not chkShowInactiveSetting.Checked Then
                strQuery &= " WHERE BS.IsActive=1"
                gvVenues.Columns(gvVenues.Columns.Count - 1).Visible = False
            Else
                gvVenues.Columns(gvVenues.Columns.Count - 1).Visible = True
            End If
            Dim ds As DataSet = conn.SelectData(strQuery)
            gvVenues.DataSource = ds.Tables(0)
            gvVenues.DataBind()
        Else
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            divMessageBox.Visible = True
            lblMessageBox.Text = "<strong>Error!</strong> You need to create tables."
        End If
    End Sub
    Private Sub Clear()
        ddlVenu.ClearSelection()
        ddlStore.ClearSelection()
        ddlSort.ClearSelection()
        ddlStore.Items.Clear()
        ddlBookingType.ClearSelection()
        ddlGuestAc.ClearSelection()
        ddlCheckedGuestAc.ClearSelection()
        ddlTableGroup.ClearSelection()
        ddlLiveTableGroup.ClearSelection()
        drpNoOfRooms.ClearSelection()
        ddlRedirectTo.SelectedValue = 0
        drpHotelTabs.ClearSelection()
        drpRestaurantTab.ClearSelection()
        drpHotelTabs.Enabled = False
        drpRestaurantTab.Enabled = False
        reqHotalTab.Enabled = False
        reqRestaurantTab.Enabled = False

        chkShowOnWidget.Checked = False

        pnlTable.Visible = False

        BookingSettingsid = 0

        divMessageBox.Attributes.Remove("class")
        divMessageBox.Visible = False

        ddlVenu.Enabled = True
        ddlStore.Enabled = True
        BindTabNameForLocationSettings()
    End Sub
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
            BindGrid()
            If (str <> "0") Then

                LoadVenueMappingData()
                LoadOtherVenue()
                LoadVenueDropdown()
                BindStoreMasterData()

                BindVenue()
                BindGuestAc()
                BindCheckedGuestAc()

                LoadXMLData()
                BindRedirectToLocations()

                BindTabGrid()
                BindTabNameForLocationSettings()

                BindgvTableAndDDl()
                BindgvGroupAndDDl()
                BindTableForGroup()

                ViewState("edit") = Nothing
                ViewState("save") = Nothing
                ViewState("edit_Redirect") = Nothing
            End If
        End If
    End Sub
#End Region

#Region "Controls Events"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim redirectTo As Int32 = 0
            If Not String.IsNullOrEmpty(ddlRedirectTo.SelectedValue) Then
                redirectTo = Utils.ParseInt(ddlRedirectTo.SelectedValue)
            End If

            Dim strQvuery As String

            If (BookingSettingsid > 0) Then

                If (ddlBookingType.SelectedValue >= "1") Then

                    strQvuery = "UPDATE [bookingsettings] SET "
                    strQvuery &= "[bookingtype] = '" & Utils.Encrypt(ddlBookingType.SelectedValue) & "'"
                    strQvuery &= ",[bookingtillid] = 1"
                    strQvuery &= ",[Numberofrooms] = " & drpNoOfRooms.SelectedValue
                    strQvuery &= ",[Sort] = " & ddlSort.SelectedValue
                    strQvuery &= ",[Guestaccount] = 0"
                    strQvuery &= ",[CheckedGuestAccount] = " & IIf(String.IsNullOrEmpty(ddlCheckedGuestAc.SelectedValue), "NULL", ddlCheckedGuestAc.SelectedValue)
                    strQvuery &= ",[cancelledRoom] = NULL"
                    strQvuery &= ",[StoreID] = " & ddlStore.SelectedValue
                    strQvuery &= ",[IsActive] = 1"

                    strQvuery &= ",[TableGroup] = " & ddlTableGroup.SelectedValue
                    strQvuery &= ",[RedirectScheduleTo] = " & redirectTo
                    strQvuery &= ",[IsShowOnWidget] = '" & chkShowOnWidget.Checked & "'"
                    strQvuery &= ",[HotelTabID] = " & Utils.ParseInt(drpHotelTabs.SelectedValue, 0)
                    strQvuery &= ",[RestTabID] = " & Utils.ParseInt(drpRestaurantTab.SelectedValue, 0)
                    strQvuery &= ",[livetablegroup] = " & ddlLiveTableGroup.SelectedValue
                    strQvuery &= " WHERE BookingSettingsid = " & BookingSettingsid.ToString()
                    strQvuery &= " SELECT " & BookingSettingsid.ToString()
                Else
                    strQvuery = "UPDATE [bookingsettings] SET "
                    strQvuery &= "[bookingtype] = '" & Utils.Encrypt(ddlBookingType.SelectedValue) & "'"
                    strQvuery &= ",[bookingtillid] = 1"
                    strQvuery &= ",[Numberofrooms] = " & drpNoOfRooms.SelectedValue
                    strQvuery &= ",[Sort] = " & ddlSort.SelectedValue
                    strQvuery &= ",[Guestaccount] = 0"
                    strQvuery &= ",[CheckedGuestAccount] = " & IIf(String.IsNullOrEmpty(ddlCheckedGuestAc.SelectedValue), "NULL", ddlCheckedGuestAc.SelectedValue)
                    strQvuery &= ",[cancelledRoom] = NULL"
                    strQvuery &= ",[StoreID] = " & ddlStore.SelectedValue
                    strQvuery &= ",[IsActive] = 1"

                    strQvuery &= ",[TableGroup] = NULL"
                    strQvuery &= ",[RedirectScheduleTo] = " & redirectTo
                    strQvuery &= ",[IsShowOnWidget] = '" & chkShowOnWidget.Checked & "'"
                    strQvuery &= ",[HotelTabID] = " & Utils.ParseInt(drpHotelTabs.SelectedValue, 0)
                    strQvuery &= ",[RestTabID] = " & Utils.ParseInt(drpRestaurantTab.SelectedValue, 0)
                    strQvuery &= ",[livetablegroup] = " & ddlLiveTableGroup.SelectedValue
                    strQvuery &= " WHERE BookingSettingsid = " & BookingSettingsid.ToString()
                    strQvuery &= " SELECT " & BookingSettingsid.ToString()
                End If
            Else
                If (ddlBookingType.SelectedValue >= "1") Then

                    strQvuery = "INSERT INTO [bookingsettings]("
                    strQvuery &= "[bookingtype],"
                    strQvuery &= "[bookingtillid],"
                    strQvuery &= "[Numberofrooms],"
                    strQvuery &= "[Sort],"
                    strQvuery &= "[Guestaccount],"
                    strQvuery &= "[CheckedGuestAccount],"
                    strQvuery &= "[cancelledRoom],"
                    strQvuery &= "[StoreID],"
                    strQvuery &= "[IsActive],"

                    strQvuery &= "[TableGroup], "
                    strQvuery &= "[RedirectScheduleTo], "
                    strQvuery &= "[HotelTabID], "
                    strQvuery &= "[RestTabID], "
                    strQvuery &= "[livetablegroup], "
                    strQvuery &= "[IsShowOnWidget]) "

                    strQvuery &= "VALUES('"
                    strQvuery &= Utils.Encrypt(ddlBookingType.SelectedValue)
                    strQvuery &= "',1"
                    strQvuery &= "," & drpNoOfRooms.SelectedValue
                    strQvuery &= "," & ddlSort.SelectedValue
                    strQvuery &= ",0"
                    strQvuery &= "," & IIf(String.IsNullOrEmpty(ddlCheckedGuestAc.SelectedValue), "NULL", ddlCheckedGuestAc.SelectedValue)
                    strQvuery &= ",NULL"
                    strQvuery &= "," & ddlStore.SelectedValue
                    strQvuery &= ",1"

                    strQvuery &= "," & ddlTableGroup.SelectedValue
                    strQvuery &= "," & redirectTo
                    strQvuery &= "," & Utils.ParseInt(drpHotelTabs.SelectedValue, 0)
                    strQvuery &= "," & Utils.ParseInt(drpRestaurantTab.SelectedValue, 0)
                    strQvuery &= "," & ddlLiveTableGroup.SelectedValue
                    strQvuery &= ",'" & chkShowOnWidget.Checked & "')"
                    strQvuery &= "SELECT SCOPE_IDENTITY()"
                Else
                    strQvuery = "INSERT INTO [bookingsettings]("
                    strQvuery &= "[bookingtype],"
                    strQvuery &= "[bookingtillid],"
                    strQvuery &= "[Numberofrooms],"
                    strQvuery &= "[Sort],"
                    strQvuery &= "[Guestaccount],"
                    strQvuery &= "[CheckedGuestAccount],"
                    strQvuery &= "[cancelledRoom],"
                    strQvuery &= "[StoreID],"
                    strQvuery &= "[IsActive], "
                    strQvuery &= "[RedirectScheduleTo], "
                    strQvuery &= "[HotelTabID], "
                    strQvuery &= "[RestTabID], "
                    strQvuery &= "[livetablegroup], "
                    strQvuery &= "[IsShowOnWidget]) "

                    strQvuery &= "VALUES('"
                    strQvuery &= Utils.Encrypt(ddlBookingType.SelectedValue)
                    strQvuery &= "',1"
                    strQvuery &= "," + drpNoOfRooms.SelectedValue
                    strQvuery &= "," + ddlSort.SelectedValue
                    strQvuery &= ",0"
                    strQvuery &= "," + IIf(String.IsNullOrEmpty(ddlCheckedGuestAc.SelectedValue), "NULL", ddlCheckedGuestAc.SelectedValue)
                    strQvuery &= ",NULL"
                    strQvuery &= "," + ddlStore.SelectedValue
                    strQvuery &= ",1 "
                    strQvuery &= "," + redirectTo.ToString() 'Add .ToString()
                    strQvuery &= "," & Utils.ParseInt(drpHotelTabs.SelectedValue, 0).ToString() 'Add .ToString()
                    strQvuery &= "," & Utils.ParseInt(drpRestaurantTab.SelectedValue, 0).ToString() 'Add .ToString()
                    strQvuery &= "," & ddlLiveTableGroup.SelectedValue
                    strQvuery &= "," + IIf(chkShowOnWidget.Checked = True, 1, 0).ToString() + ")" 'Add IIF(When Checked,1,0) AND .ToString()
                    strQvuery &= " SELECT SCOPE_IDENTITY()"
                End If
            End If

            Dim conn As DBConnection = New DBConnection()

            Dim retVal As Integer = Convert.ToInt32(conn.ExecuteScalar(strQvuery))

            strQvuery = "Select [RedirectScheduleTo] FROM [bookingsettings] WHERE BookingSettingsid = " + retVal.ToString() + ""

            Dim retredirect As Integer = Convert.ToInt32(conn.ExecuteScalar(strQvuery))

            If ViewState("Add_Schedule") = 1 Then
                Dim strQvuery1 As String = "delete BookingSchedule where BookingSettingsID = '" + retVal.ToString() + "'"
                conn.ExecuteNonQuery(strQvuery1)
            End If

            If retredirect = 0 Then
                If (retVal > 0) Then
                    If ddlBookingType.SelectedValue >= "1" Then
                        pnlTable.Visible = True
                        ddlVenu.Enabled = True
                        ddlStore.Enabled = False
                        BookingSettingsid = retVal
                        btnAddSchedule.Enabled = True
                        lblredirect.Visible = False
                        gvSchedule.Visible = True
                        LoadScheduleData(False)
                    Else
                        Clear()
                    End If

                    strQvuery = "select BookingSettingsid from bookingsettings where RedirectScheduleTo = " + retVal.ToString() + ""
                    Dim dateget As DataSet = conn.SelectData(strQvuery)
                    Dim sqlq As String = ""
                    For index As Integer = 0 To dateget.Tables(0).Rows.Count - 1
                        strQvuery = "delete BookingSchedule where BookingSettingsID = " + dateget.Tables(0).Rows(index).Item("BookingSettingsid").ToString() + ""
                        conn.ExecuteScalar(strQvuery)
                        Dim objCommon As Common = New Common()
                        objCommon.CopyRedirectToData(dateget.Tables(0).Rows(index).Item("BookingSettingsid").ToString(), Utils.ParseInt(retVal.ToString()))
                        'strQvuery = "INSERT INTO BookingSchedule "
                        'strQvuery += "(BookingSettingsID, Name, StartTime, EndTime, NumberOfCover, ServiceDuration, TimeSpan, PaymentType, DepositPercentage, DepositAmount, StartDate, "
                        'strQvuery += "EndDate, FutureReservationTime, OnlineMaxCovers, MinCovers, One_booking_at_a_time) "
                        'strQvuery += "select " + dateget.Tables(0).Rows(index).Item("BookingSettingsid").ToString() + " , Name, StartTime, EndTime, NumberOfCover, ServiceDuration, TimeSpan, PaymentType, DepositPercentage, DepositAmount, StartDate, "
                        'strQvuery += "EndDate, FutureReservationTime, OnlineMaxCovers, MinCovers, One_booking_at_a_time from BookingSchedule WHERE BookingSettingsID = " + retVal.ToString() + ""
                        'conn.ExecuteScalar(strQvuery)
                    Next

                    divMessageBox.Attributes.Add("class", "alert alert-success")
                    divMessageBox.Visible = True
                    lblMessageBox.Text = "<strong>Success!</strong> Location details saved successfully."
                    BindGrid()

                    If Not String.IsNullOrEmpty(ddlRedirectTo.SelectedValue) Then
                        If redirectTo > 0 Then
                            Dim objCommon As Common = New Common()
                            objCommon.CopyRedirectToData(retVal, Utils.ParseInt(ddlRedirectTo.SelectedValue))
                            LoadScheduleData(False)
                        End If
                    End If
                Else
                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                    divMessageBox.Visible = True
                    lblMessageBox.Text = "<strong>Error!</strong> While saving Location."
                End If
                ViewState("save_Redirect") = 0
            Else
                If (retVal > 0) Then

                    If ddlBookingType.SelectedValue >= "1" Then
                        pnlTable.Visible = True
                        ddlVenu.Enabled = False
                        ddlStore.Enabled = False
                        gvSchedule.Visible = False
                        'BookingSettingsid = retredirect
                        btnAddSchedule.Enabled = False
                        lblredirect.Visible = True
                        lblredirect.Text = "Redirect To : " + ddlRedirectTo.SelectedText
                        'LoadScheduleData(False)
                    Else
                        Clear()
                    End If

                    strQvuery = "select BookingSettingsid from bookingsettings where RedirectScheduleTo = " + retVal.ToString() + ""
                    Dim dateget As DataSet = conn.SelectData(strQvuery)
                    Dim sqlq As String = ""
                    For index As Integer = 0 To dateget.Tables(0).Rows.Count - 1
                        strQvuery = "delete BookingSchedule where BookingSettingsID = " + dateget.Tables(0).Rows(index).Item("BookingSettingsid").ToString() + ""
                        conn.ExecuteScalar(strQvuery)
                        Dim objCommon As Common = New Common()
                        objCommon.CopyRedirectToData(dateget.Tables(0).Rows(index).Item("BookingSettingsid").ToString(), Utils.ParseInt(retredirect.ToString()))
                        'strQvuery = "INSERT INTO BookingSchedule "
                        'strQvuery += "(BookingSettingsID, Name, StartTime, EndTime, NumberOfCover, ServiceDuration, TimeSpan, PaymentType, DepositPercentage, DepositAmount, StartDate, "
                        'strQvuery += "EndDate, FutureReservationTime, OnlineMaxCovers, MinCovers, One_booking_at_a_time) "
                        'strQvuery += "select " + dateget.Tables(0).Rows(index).Item("BookingSettingsid").ToString() + " , Name, StartTime, EndTime, NumberOfCover, ServiceDuration, TimeSpan, PaymentType, DepositPercentage, DepositAmount, StartDate, "
                        'strQvuery += "EndDate, FutureReservationTime, OnlineMaxCovers, MinCovers, One_booking_at_a_time from BookingSchedule WHERE BookingSettingsID = " + retVal.ToString() + ""
                        'conn.ExecuteScalar(strQvuery)
                    Next

                    divMessageBox.Attributes.Add("class", "alert alert-success")
                    divMessageBox.Visible = True
                    lblMessageBox.Text = "<strong>Success!</strong> Location details saved successfully."
                    BindGrid()

                    If Not String.IsNullOrEmpty(ddlRedirectTo.SelectedValue) Then
                        If redirectTo > 0 Then
                            Dim objCommon As Common = New Common()
                            objCommon.CopyRedirectToData(retVal, Utils.ParseInt(ddlRedirectTo.SelectedValue))
                            LoadScheduleData(False)
                            gvSchedule.Visible = False
                        End If
                    End If
                Else
                    divMessageBox.Attributes.Add("class", "alert alert-danger")
                    divMessageBox.Visible = True
                    lblMessageBox.Text = "<strong>Error!</strong> While saving Location."
                End If
                ViewState("save_Redirect") = 1
            End If
            ViewState("edit") = Nothing
            ViewState("save") = retVal
            ViewState("edit_Redirect") = Nothing

        Catch
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            divMessageBox.Visible = True
            lblMessageBox.Text = "<strong>Error!</strong> While saving store."
        End Try
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Clear()
        ViewState("edit") = Nothing
        ViewState("save") = Nothing
        ViewState("edit_Redirect") = Nothing
        ViewState("Add_Schedule") = Nothing
    End Sub

    Protected Sub ddlBookingType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlBookingType.SelectedIndexChanged
        'If (ddlBookingType.SelectedValue = "2" Or ddlBookingType.SelectedValue = "3") Then
        '    pnlTable.Visible = True

        'Else
        '    pnlTable.Visible = False

        'End If
        EnableTabSelection()
    End Sub

    Private Sub EnableTabSelection()
        If (ddlBookingType.SelectedValue = "1") Then
            drpHotelTabs.Enabled = True
            reqHotelTab.Enabled = True
            drpRestaurantTab.Enabled = True
            reqRestaurantTab.Enabled = True
        ElseIf (ddlBookingType.SelectedValue = "2") Then
            drpHotelTabs.Enabled = False
            reqHotelTab.Enabled = False
            drpRestaurantTab.Enabled = True
            reqRestaurantTab.Enabled = True
        ElseIf (ddlBookingType.SelectedValue = "3") Then
            drpHotelTabs.Enabled = True
            reqHotelTab.Enabled = True
            drpRestaurantTab.Enabled = True
            reqRestaurantTab.Enabled = True
        End If
    End Sub


    Protected Sub ddlVenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlVenu.SelectedIndexChanged
        BindStore(ddlVenu.SelectedValue)
    End Sub
    Protected Sub chkShowInactiveSetting_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowInactiveSetting.CheckedChanged
        BindGrid()
    End Sub

    'Protected Sub txtTimeGroupName1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeGroupName1.TextChanged
    '    rfvStartTime1.Enabled = txtTimeGroupName1.Text.Length > 0
    '    rfvEndTime1.Enabled = txtTimeGroupName1.Text.Length > 0
    'End Sub
    'Protected Sub txtTimeGroupName2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeGroupName2.TextChanged
    '    rfvStartTime2.Enabled = txtTimeGroupName2.Text.Length > 0
    '    rfvEndTime2.Enabled = txtTimeGroupName2.Text.Length > 0
    'End Sub
    'Protected Sub txtTimeGroupName3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTimeGroupName3.TextChanged
    '    rfvStartTime3.Enabled = txtTimeGroupName3.Text.Length > 0
    '    rfvEndTime3.Enabled = txtTimeGroupName3.Text.Length > 0
    'End Sub

    Protected Sub btnCreateTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateTable.Click
        Try
            Dim conn As DBConnection = New DBConnection()
            Dim str As String = conn.SingleCell("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;")
            If (str = "0") Then
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[TabMaster]([TabId] [int] IDENTITY(1,1) NOT NULL,[TabName] [varchar](max) NULL,[TabType] [int] NULL,CONSTRAINT [PK_TabMaster] PRIMARY KEY CLUSTERED ([TabId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[VenueMaster]([BookingVenueId] [int] IDENTITY(1,1) NOT NULL,[VenueName] [varchar](max) NULL,[OtherVenueId] [int] NULL,CONSTRAINT [PK_VenueMaster] PRIMARY KEY CLUSTERED ([BookingVenueId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[StoreMaster]([OurStoreId] [int] IDENTITY(1,1) NOT NULL,[StoreName] [varchar](max) NULL,[BookingVenueId] [int] NULL,[OtherStoreId] [int] NULL,CONSTRAINT [PK_StoreMaster] PRIMARY KEY CLUSTERED ([OurStoreId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[bookingsettings]([BookingSettingsid] [int] IDENTITY(1,1) NOT NULL,[bookingtype] [nvarchar](100) NULL,[bookingtillid] [int] NULL,[Numberofrooms] [int] NULL,[Sort] [int] NULL,[Guestaccount] [int] NULL,[CheckedGuestAccount] [int] NULL,[cancelledRoom] [int] NULL,[TableGroup] [int] NULL,[StoreID] [int] NULL,[IsActive] [bit] NULL,[RedirectScheduleTo] [int] NULL,[IsShowOnWidget] [bit] NULL,[HotelTabID] [int] NULL,[RestTabID] [int] NULL,[livetablegroup] [int] NULL,CONSTRAINT [PK_bookingsettings] PRIMARY KEY CLUSTERED ([BookingSettingsid] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]")
                conn.ExecuteNonQuery("CREATE TABLE [dbo].[bookings]([bookingid] [int] IDENTITY(1,1) NOT NULL,[covers] [int] NULL,[date] [smalldatetime] NULL,[comment] [nvarchar](max) NULL,[Roomid] [int] NULL,[arrivaldate] [smalldatetime] NULL,[departuredate] [smalldatetime] NULL,[checkedin] [int] NULL,[bookingref] [nvarchar](max) NULL,[bookingtotal] [decimal](10, 2) NULL,[deposit] [decimal](10, 2) NULL,[accountid] [int] NULL,[period] [int] NULL,[bookingtime] [time](7) NULL,[BookingSettingsid] [int] NULL,[IsCanceled] [bit] NULL,[CreatedDate] [datetime] NULL,[CreatedBy] [int] NULL,[IPAddress] [varchar](50) NULL,[GrandTotal] [decimal](10, 2) NULL,[BookingScheduleID] [int] NULL,[BookingScheduleDateId] [int] NULL,[IsOnline] BIT NULL,[OurStoreId] INT NULL,[GroupID] INT NULL,[Allotted_Tables] nvarchar(500) NULL,[TableNo] INT NULL,CONSTRAINT [PK_bookings] PRIMARY KEY CLUSTERED ([bookingid] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]")
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

                divMessageBox.Attributes.Add("class", "alert alert-success")
                divMessageBox.Visible = True
                lblMessageBox.Text = "<strong>Success!</strong> Tables created successfully."
            Else
                divMessageBox.Attributes.Add("class", "alert alert-info")
                divMessageBox.Visible = True
                lblMessageBox.Text = "<strong>Info!</strong> Tables already created."
            End If
        Catch
            divMessageBox.Attributes.Add("class", "alert alert-danger")
            divMessageBox.Visible = True
            lblMessageBox.Text = "<strong>Error!</strong> While creating new tables."
        End Try
    End Sub
    Protected Sub btnRetun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetun.Click
        Session("AllowSetting") = Nothing
        Response.Redirect("Dashboard.aspx")
    End Sub

    Protected Sub gvVenues_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvVenues.ItemCommand

        If (e.CommandName = "EditVenue") Then
            Dim conn As DBConnection = New DBConnection()
            Dim strQvuery_1 As String
            Clear()
            Dim bsid As Integer
            bsid = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingSettingsid").ToString())
            BookingSettingsid = bsid
            strQvuery_1 = "Select [RedirectScheduleTo] FROM [bookingsettings] WHERE BookingSettingsid = " + bsid.ToString() + ""
            Dim retredirect As Integer = Convert.ToInt32(conn.ExecuteScalar(strQvuery_1))

            BindRedirectToLocations()
            Dim strQvuery As String = "SELECT BS.*, V.BookingVenueId AS VenueID FROM bookingsettings BS left outer join StoreMaster S ON S.OurStoreId = BS.StoreId left outer join VenueMaster V ON V.BookingVenueId = S.BookingVenueId WHERE BookingSettingsid = " + BookingSettingsid.ToString()

            Dim dr As DataRow = conn.SingleRow(strQvuery)

            ddlVenu.SelectedValue = Utils.NullToStringColumns(dr, "VenueID")
            BindStore(ddlVenu.SelectedValue)
            ddlStore.SelectedValue = Utils.NullToStringColumns(dr, "StoreID")
            ddlSort.SelectedValue = Utils.NullToStringColumns(dr, "Sort")
            ddlBookingType.SelectedValue = Utils.Decrypt(Utils.NullToStringColumns(dr, "bookingtype"))
            ddlGuestAc.SelectedValue = Utils.NullToStringColumns(dr, "Guestaccount")
            If (Utils.NullToStringColumns(dr, "CheckedGuestAccount") <> "") Then
                ddlCheckedGuestAc.SelectedValue = Utils.NullToStringColumns(dr, "CheckedGuestAccount")
            End If
            drpNoOfRooms.SelectedValue = Utils.NullToStringColumns(dr, "Numberofrooms")
            ddlTableGroup.SelectedValue = Utils.NullToStringColumns(dr, "TableGroup")
            ddlLiveTableGroup.SelectedValue = Utils.NullToStringColumns(dr, "livetablegroup")
            ddlVenu.Enabled = False
            ddlStore.Enabled = False
            ddlRedirectTo.SelectedValue = Utils.NullToStringColumns(dr, "RedirectScheduleTo")
            chkShowOnWidget.Checked = Utils.ParseBool(dr("IsShowOnWidget").ToString())
            EnableTabSelection()
            drpHotelTabs.SelectedValue = Utils.NullToStringColumns(dr, "HotelTabID")
            drpRestaurantTab.SelectedValue = Utils.NullToStringColumns(dr, "RestTabID")
            'If (ddlBookingType.SelectedValue <> "1") Then

            If retredirect = 0 Then
                'BookingSettingsid = bsid
                lblredirect.Visible = False
                btnAddSchedule.Enabled = True
                gvSchedule.Visible = True
                LoadScheduleData2(False, bsid)
                ViewState("edit_Redirect") = Nothing
                ViewState("Add_Schedule") = 0
            Else
                'BookingSettingsid = retredirect
                lblredirect.Visible = True
                lblredirect.Text = "Redirect To : " + ddlRedirectTo.SelectedText
                btnAddSchedule.Enabled = False
                gvSchedule.Visible = False
                LoadScheduleData2(False, retredirect)
                ViewState("edit_Redirect") = bsid
                ViewState("Add_Schedule") = 1
            End If
            ViewState("edit") = bsid
            pnlTable.Visible = True
            'End If
        End If

        If (e.CommandName = "DeleteVenue") Then
            Dim bookingSettingsid As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingSettingsid").ToString()
            Dim conn As DBConnection = New DBConnection()
            Dim strQvuery As String = "Update bookingsettings Set IsActive=0 Where BookingSettingsid=" + bookingSettingsid
            conn.ExecuteNonQuery(strQvuery)
            BindGrid()

            divMessageBox.Attributes.Add("class", "alert alert-success")
            divMessageBox.Visible = True
            lblMessageBox.Text = "<strong>Success!</strong> Store deactived successfully."
        End If

        If (e.CommandName = "ActiveVenue") Then
            Dim bookingSettingsid As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("BookingSettingsid").ToString()
            Dim conn As DBConnection = New DBConnection()
            Dim strQvuery As String = "Update bookingsettings Set IsActive=1 Where BookingSettingsid=" + bookingSettingsid
            conn.ExecuteNonQuery(strQvuery)
            BindGrid()

            divMessageBox.Attributes.Add("class", "alert alert-success")
            divMessageBox.Visible = True
            lblMessageBox.Text = "<strong>Success!</strong> Store reactivated successfully."
        End If
    End Sub
    Protected Sub gvVenues_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvVenues.ItemDataBound

        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)
            Dim btnActive As ImageButton = TryCast(item("btnActive").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to deactive this record ?')")
            End If

            If (btnActive IsNot Nothing) Then
                btnActive.Attributes.Add("onclick", "return confirm('Are you sure you want to reactivate this record ?')")
            End If

            If (chkShowInactiveSetting.Checked) Then
                Dim isActive As String = TryCast(e.Item, GridDataItem).GetDataKeyValue("IsActive").ToString()
                Dim btnEdit As ImageButton = TryCast(item("btnEdit").Controls(0), ImageButton)


                If (isActive = "True") Then
                    btnEdit.Visible = True
                    btnDelete.Visible = True
                    btnActive.Visible = False
                Else
                    btnEdit.Visible = False
                    btnDelete.Visible = False
                    btnActive.Visible = True
                End If
            End If

        End If

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

    Protected Sub btnSaveTabName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveTabName.Click
        Dim ds As DataSet = Common.GetXmlData()
        Dim dt As DataTable = ds.Tables("TabName")

        dt.Clear()

        Dim dr As DataRow = dt.NewRow()
        dr("Tabs") = txtSearchTab.Text
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Tabs") = txtHotelTab.Text
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Tabs") = txtResTab.Text
        dt.Rows.Add(dr)

        Common.SetXmlData(ds)
        lblTabNameSave.Visible = True
    End Sub

    Private Sub LoadXMLData()
        Dim ds As DataSet = Common.GetXmlData()
        txtSearchTab.Text = ds.Tables("TabName").Rows(0)("Tabs").ToString()
        txtHotelTab.Text = ds.Tables("TabName").Rows(1)("Tabs").ToString()
        txtResTab.Text = ds.Tables("TabName").Rows(2)("Tabs").ToString()

        drpCurrencySymbol.SelectedValue = ds.Tables("Currency").Rows(0)("Symbol").ToString()
    End Sub

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

    Private Sub LoadScheduleData(ByVal isNew As Boolean)
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetBookingScheduleBySettingID(BookingSettingsid)
        If (isNew) Then
            Dim dr As DataRow = ds.Tables(0).NewRow()
            dr("BookingScheduleID") = 0
            dr("StartTime") = "00:00"
            dr("EndTime") = "00:00"
            dr("StartDate") = DateTime.Today
            dr("EndDate") = DateTime.Today.AddDays(1)
            ds.Tables(0).Rows.InsertAt(dr, 0)
            gvSchedule.EditIndex = 0
        End If
        gvSchedule.DataSource = ds
        gvSchedule.DataBind()
        If Not ViewState("edit_Redirect") = Nothing Then
            ViewState("edit_Redirect") = 1
        End If
        gvSchedule.Visible = True
    End Sub

    Private Sub LoadScheduleData2(ByVal isNew As Boolean, ByRef bid As Integer)
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetBookingScheduleBySettingID(bid)
        If (isNew) Then
            Dim dr As DataRow = ds.Tables(0).NewRow()
            dr("BookingScheduleID") = 0
            dr("StartTime") = "00:00"
            dr("EndTime") = "00:00"
            dr("StartDate") = DateTime.Today
            dr("EndDate") = DateTime.Today.AddDays(1)
            ds.Tables(0).Rows.InsertAt(dr, 0)
            gvSchedule.EditIndex = 0
        End If
        gvSchedule.DataSource = ds
        gvSchedule.DataBind()
    End Sub

    Protected Sub gvSchedule_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles gvSchedule.RowEditing
        Me.gvSchedule.EditIndex = e.NewEditIndex
        LoadScheduleData(False)
    End Sub

    Protected Sub gvSchedule_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles gvSchedule.RowCancelingEdit
        Me.gvSchedule.EditIndex = -1
        LoadScheduleData(False)
    End Sub

    Protected Sub btnAddSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSchedule.Click
        If Not ViewState("edit_Redirect") = Nothing And Not ViewState("edit_Redirect") = 1 Then
            Dim conn As DBConnection = New DBConnection()
            Dim strQvuery As String = "delete BookingSchedule where BookingSettingsID = '" + ViewState("edit_Redirect").ToString() + "'"
            conn.ExecuteNonQuery(strQvuery)
        End If
        LoadScheduleData(True)
    End Sub

    Protected Sub gvSchedule_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvSchedule.RowUpdating
        If Page.IsValid Then
            Dim index As Int32 = e.RowIndex

            Dim ScheduleID As Int32 = DirectCast(gvSchedule.DataKeys(index).Value, Int32)
            Dim SettingsID As Int32 = BookingSettingsid
            Dim Name As String = DirectCast(gvSchedule.Rows(index).FindControl("txtName"), TextBox).Text

            If CheckNameExists(ScheduleID, Name) Then
                lblDuplicateName.Text = "Schedule Name Already Exists"
                lblDuplicateName.Visible = True
            Else
                If DirectCast(gvSchedule.Rows(index).FindControl("chkone_booking_at_a_time"), CheckBox).Checked = True Then

                    If DirectCast(gvSchedule.Rows(index).FindControl("txtMinCover"), Telerik.Web.UI.RadNumericTextBox).Text = 0 Or DirectCast(gvSchedule.Rows(index).FindControl("txtMinCover"), Telerik.Web.UI.RadNumericTextBox).Text = Nothing Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Minimum Cover Needed');", True)
                    Else
                        Dim StartTime As TimeSpan = DirectCast(gvSchedule.Rows(index).FindControl("tpStartTime"), Telerik.Web.UI.RadTimePicker).SelectedTime.Value
                        Dim EndTime As TimeSpan = DirectCast(gvSchedule.Rows(index).FindControl("tpEndTime"), Telerik.Web.UI.RadTimePicker).SelectedTime.Value
                        Dim NoOfCover As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtNoOfCover"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim BOOKINGATATIME As Int32 = 1
                        Dim MinCover As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtMinCover"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim serviceDuration As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtServiceDuration"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim timeSpan As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtTimeSpan"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim paymentType As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("drpPaymentType"), Telerik.Web.UI.RadDropDownList).SelectedValue)
                        Dim dPercent As Decimal = Utils.ParseDecimal(DirectCast(gvSchedule.Rows(index).FindControl("txtPercentage"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim dAmt As Decimal = Utils.ParseDecimal(DirectCast(gvSchedule.Rows(index).FindControl("txtAmount"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim startDate As DateTime = Utils.ParseDateTime(DirectCast(gvSchedule.Rows(index).FindControl("dpStartDate"), Telerik.Web.UI.RadDatePicker).SelectedDate).ToString("s")
                        Dim endDate As DateTime = Utils.ParseDateTime(DirectCast(gvSchedule.Rows(index).FindControl("dpEndDate"), Telerik.Web.UI.RadDatePicker).SelectedDate).ToString("s")
                        Dim futureTime As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtFutureTime"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim onlineMaxCover As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtOnlineMaxCovers"), Telerik.Web.UI.RadNumericTextBox).Text)
                        Dim GroupID As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("ddlGroup"), Telerik.Web.UI.RadComboBox).SelectedValue)
                        Dim is_selectProduct As Int32 = IIf(DirectCast(gvSchedule.Rows(index).FindControl("chkis_selectProduct"), CheckBox).Checked = True, 1, 0)
                        Dim MCPTimeSpan As Int32 = IIf(DirectCast(gvSchedule.Rows(index).FindControl("txtMCPTimeSpan"), Telerik.Web.UI.RadNumericTextBox).Text = "", Nothing, DirectCast(gvSchedule.Rows(index).FindControl("txtMCPTimeSpan"), Telerik.Web.UI.RadNumericTextBox).Text)

                        Dim objCommon As Common = New Common()
                        objCommon.InsertUpdateBookingSchedule(ScheduleID, SettingsID, Name, StartTime, EndTime, NoOfCover, serviceDuration, timeSpan, paymentType, dPercent, dAmt, startDate, endDate, futureTime, onlineMaxCover, BOOKINGATATIME, MinCover, GroupID, is_selectProduct, MCPTimeSpan)

                        Me.gvSchedule.EditIndex = -1
                        LoadScheduleData(False)
                        lblDuplicateName.Text = String.Empty
                        lblDuplicateName.Visible = False

                        ' Copy Schedule Data To Child Record (i.e. Redirect To Records)
                        Dim dsChildStore As DataSet = objCommon.GetBookingSettingsByRedirectTo(SettingsID)
                        If dsChildStore IsNot Nothing AndAlso dsChildStore.Tables.Count > 0 AndAlso dsChildStore.Tables(0).Rows.Count > 0 Then

                            For index = 0 To dsChildStore.Tables(0).Rows.Count - 1
                                objCommon.CopyRedirectToData(Utils.ParseInt(dsChildStore.Tables(0).Rows(index)("BookingSettingsid")), SettingsID)
                            Next index


                        End If
                        ViewState("Add_Schedule") = 2
                    End If
                Else
                    Dim StartTime As TimeSpan = DirectCast(gvSchedule.Rows(index).FindControl("tpStartTime"), Telerik.Web.UI.RadTimePicker).SelectedTime.Value
                    Dim EndTime As TimeSpan = DirectCast(gvSchedule.Rows(index).FindControl("tpEndTime"), Telerik.Web.UI.RadTimePicker).SelectedTime.Value
                    Dim NoOfCover As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtNoOfCover"), Telerik.Web.UI.RadNumericTextBox).Text)
                    Dim BOOKINGATATIME As Int32 = 0
                    Dim MinCover As Int32 = 0
                    Dim serviceDuration As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtServiceDuration"), Telerik.Web.UI.RadNumericTextBox).Text)
                    Dim timeSpan As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtTimeSpan"), Telerik.Web.UI.RadNumericTextBox).Text)
                    Dim paymentType As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("drpPaymentType"), Telerik.Web.UI.RadDropDownList).SelectedValue)
                    Dim dPercent As Decimal = Utils.ParseDecimal(DirectCast(gvSchedule.Rows(index).FindControl("txtPercentage"), Telerik.Web.UI.RadNumericTextBox).Text)
                    Dim dAmt As Decimal = Utils.ParseDecimal(DirectCast(gvSchedule.Rows(index).FindControl("txtAmount"), Telerik.Web.UI.RadNumericTextBox).Text)
                    Dim startDate As DateTime = Utils.ParseDateTime(DirectCast(gvSchedule.Rows(index).FindControl("dpStartDate"), Telerik.Web.UI.RadDatePicker).SelectedDate).ToString("s")
                    Dim endDate As DateTime = Utils.ParseDateTime(DirectCast(gvSchedule.Rows(index).FindControl("dpEndDate"), Telerik.Web.UI.RadDatePicker).SelectedDate).ToString("s")
                    Dim futureTime As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtFutureTime"), Telerik.Web.UI.RadNumericTextBox).Text)
                    Dim onlineMaxCover As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("txtOnlineMaxCovers"), Telerik.Web.UI.RadNumericTextBox).Text)
                    Dim GroupID As Int32 = Utils.ParseInt(DirectCast(gvSchedule.Rows(index).FindControl("ddlGroup"), Telerik.Web.UI.RadComboBox).SelectedValue)
                    Dim is_selectProduct As Int32 = IIf(DirectCast(gvSchedule.Rows(index).FindControl("chkis_selectProduct"), CheckBox).Checked = True, 1, 0)
                    Dim MCPTimeSpan As Int32 = IIf(DirectCast(gvSchedule.Rows(index).FindControl("txtMCPTimeSpan"), Telerik.Web.UI.RadNumericTextBox).Text = "", Nothing, DirectCast(gvSchedule.Rows(index).FindControl("txtMCPTimeSpan"), Telerik.Web.UI.RadNumericTextBox).Text)


                    Dim objCommon As Common = New Common()
                    objCommon.InsertUpdateBookingSchedule(ScheduleID, SettingsID, Name, StartTime, EndTime, NoOfCover, serviceDuration, timeSpan, paymentType, dPercent, dAmt, startDate, endDate, futureTime, onlineMaxCover, BOOKINGATATIME, MinCover, GroupID, is_selectProduct, MCPTimeSpan)

                    Me.gvSchedule.EditIndex = -1
                    LoadScheduleData(False)
                    lblDuplicateName.Text = String.Empty
                    lblDuplicateName.Visible = False

                    ' Copy Schedule Data To Child Record (i.e. Redirect To Records)
                    Dim dsChildStore As DataSet = objCommon.GetBookingSettingsByRedirectTo(SettingsID)
                    If dsChildStore IsNot Nothing AndAlso dsChildStore.Tables.Count > 0 AndAlso dsChildStore.Tables(0).Rows.Count > 0 Then

                        For index = 0 To dsChildStore.Tables(0).Rows.Count - 1
                            objCommon.CopyRedirectToData(Utils.ParseInt(dsChildStore.Tables(0).Rows(index)("BookingSettingsid")), SettingsID)
                        Next index


                    End If
                    ViewState("Add_Schedule") = 2
                End If

            End If
        End If
    End Sub

    Protected Sub btnCloseData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCloseData.Click
        Clear()
        ViewState("edit") = Nothing
        ViewState("save") = Nothing
        ViewState("edit_Redirect") = Nothing
        ViewState("Add_Schedule") = Nothing
    End Sub

    Protected Sub gvSchedule_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSchedule.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dr As DataRowView
            dr = TryCast(e.Row.DataItem, DataRowView)
            If e.Row.RowIndex = gvSchedule.EditIndex Then
                Dim chk_one_booking_at_a_time As CheckBox = TryCast(e.Row.FindControl("chkone_booking_at_a_time"), CheckBox)
                Dim txt_MinCover As RadNumericTextBox = TryCast(e.Row.FindControl("txtMinCover"), RadNumericTextBox)
                If dr("One_booking_at_a_time").ToString() = "1" Then
                    chk_one_booking_at_a_time.Checked = True
                Else
                    chk_one_booking_at_a_time.Checked = False
                End If
            Else
                Dim lbl_one_booking_at_a_time As Label = TryCast(e.Row.FindControl("lblone_booking_at_a_time"), Label)
                Dim txt_MinCover As RadNumericTextBox = TryCast(e.Row.FindControl("txtMinCover"), RadNumericTextBox)
                If dr("One_booking_at_a_time").ToString() = "1" Then
                    lbl_one_booking_at_a_time.Text = "Yes"
                Else
                    lbl_one_booking_at_a_time.Text = "No"
                End If
            End If
            If e.Row.RowIndex = gvSchedule.EditIndex Then
                Dim chkis_selectProduct As CheckBox = TryCast(e.Row.FindControl("chkis_selectProduct"), CheckBox)
                If dr("is_selectProduct").ToString() = "1" Then
                    chkis_selectProduct.Checked = True
                Else
                    chkis_selectProduct.Checked = False
                End If
            Else
                Dim lblis_selectProduct As Label = TryCast(e.Row.FindControl("lblis_selectProduct"), Label)
                If dr("is_selectProduct").ToString() = "1" Then
                    lblis_selectProduct.Text = "Yes"
                Else
                    lblis_selectProduct.Text = "No"
                End If
            End If
            If e.Row.RowIndex = gvSchedule.EditIndex Then
                Dim drpPaymentType As RadDropDownList = TryCast(e.Row.FindControl("drpPaymentType"), RadDropDownList)
                drpPaymentType.SelectedValue = dr("PaymentType").ToString()
            Else
                Dim lblPaymentType As Label = TryCast(e.Row.FindControl("lblPaymentType"), Label)
                If dr("PaymentType").ToString() = "1" Then
                    lblPaymentType.Text = "Percentage Of Total Bill"
                ElseIf dr("PaymentType").ToString() = "2" Then
                    lblPaymentType.Text = "Open Deposit"
                ElseIf dr("PaymentType").ToString() = "3" Then
                    lblPaymentType.Text = "Deposit Amount"
                ElseIf dr("PaymentType").ToString() = "4" Then
                    lblPaymentType.Text = "Deposit Per Cover"
                End If
            End If
            If e.Row.RowIndex = gvSchedule.EditIndex Then
                Dim drpGroup As RadComboBox = TryCast(e.Row.FindControl("ddlGroup"), RadComboBox)
                Dim objCommon As Common = New Common()
                Dim ds As DataSet = objCommon.GetGroupNames()
                If ds.Tables.Count > 0 Then
                    drpGroup.DataSource = ds
                    drpGroup.DataTextField = "group_name"
                    drpGroup.DataValueField = "GroupID"
                    drpGroup.DataBind()
                    drpGroup.Items.Insert(0, New RadComboBoxItem("Select", 0))
                End If
                Try
                    If Not IsDBNull(dr("GroupID")) Then
                        If dr("GroupID") Is Nothing Then
                            drpGroup.SelectedValue = 0
                        Else
                            drpGroup.SelectedValue = dr("GroupID")
                        End If
                    End If
                Catch ex As Exception

                End Try
            Else
                Dim lblGroup As Label = TryCast(e.Row.FindControl("lblTSG"), Label)
                If dr("GroupID").ToString() = "" Or dr("GroupID").ToString() = Nothing Then
                    lblGroup.Text = "No Group Selected"
                Else
                    If IsDBNull(dr("GroupName")) Then
                        lblGroup.Text = "No Group Selected"
                    Else
                        lblGroup.Text = dr("GroupName")
                    End If

                End If
            End If
        End If

    End Sub

    Protected Sub gvSchedule_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvSchedule.RowCommand
        If e.CommandName = "Delete" Then
            Dim scheduleID As Int32 = Utils.ParseInt(e.CommandArgument.ToString())
            Dim oCommon As Common = New Common()
            oCommon.DeleteBookingSchedule(scheduleID)
            LoadScheduleData(False)
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

        BindVenue()
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

        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetAllVenueMasterData()
        ddlOurVenue.DataSource = ds
        ddlOurVenue.DataValueField = "BookingVenueId"
        ddlOurVenue.DataTextField = "VenueName"
        ddlOurVenue.DataBind()
        ddlOurVenue.ClearSelection()
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
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetAllStoreMasterData()

        gvStoreMap.DataSource = ds
        gvStoreMap.DataBind()
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
        Dim objCommon As Common = New Common()
        objCommon.StoreMasterSave(Utils.ParseInt(hdnOurStoreId.Value), txtStoreName.Text, Utils.ParseInt(ddlOurVenue.SelectedValue), Utils.ParseInt(ddlOtherStore.SelectedValue))
        divStoreMapMsg.Attributes.Add("class", "alert alert-success")
        divStoreMapMsg.Visible = True
        lblStoreMapMsg.Text = "Store Mapping Saved Successfully."
        ClearStoreMap()
        BindStoreMasterData()
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

    Private Sub BindRedirectToLocations()
        ddlRedirectTo.Items.Clear()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetRedirectToLocation(BookingSettingsid)
        ddlRedirectTo.DataSource = ds
        ddlRedirectTo.DataTextField = "StoreName"
        ddlRedirectTo.DataValueField = "BookingSettingsid"
        ddlRedirectTo.DataBind()
        ddlRedirectTo.Items.Insert(0, New DropDownListItem("No Redirect", "0"))
        ddlRedirectTo.SelectedValue = 0

    End Sub

#Region "Tab Settings"
    Private Sub BindTabGrid()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetAllTabNames()
        gvTabs.DataSource = ds
        gvTabs.DataBind()

        BindTabNameForLocationSettings()
    End Sub

    Private Sub ClearTabData()
        txtTabName.Text = ""
        hdnTabId.Value = 0
        rdoRestaurent.Checked = True
        rdoHotel.Checked = False
    End Sub

    Private Sub SaveTabData()
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

    Private Sub BindTabNameForLocationSettings()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetAllTabNamesByType(1)   ' 1 For Hotel
        drpHotelTabs.DataSource = ds
        drpHotelTabs.DataTextField = "TabName"
        drpHotelTabs.DataValueField = "TabId"
        drpHotelTabs.DataBind()
        drpHotelTabs.Items.Insert(0, New Telerik.Web.UI.DropDownListItem("Select Tab", "0"))
        ds = New DataSet()
        ds = objCommon.GetAllTabNamesByType(2)   ' 2 For Restaurant
        drpRestaurantTab.DataSource = ds
        drpRestaurantTab.DataTextField = "TabName"
        drpRestaurantTab.DataValueField = "TabId"
        drpRestaurantTab.DataBind()
        drpRestaurantTab.Items.Insert(0, New Telerik.Web.UI.DropDownListItem("Select Tab", "0"))
    End Sub

    Protected Sub ddlRedirectTo_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.DropDownListEventArgs) Handles ddlRedirectTo.SelectedIndexChanged
        If (ViewState("edit") = Nothing And ViewState("save") = Nothing) Then
            pnlTable.Visible = False
        End If

        If Not (ViewState("edit") = Nothing) Then
            If (ddlRedirectTo.SelectedValue = 0) Then
                pnlTable.Visible = True
                btnAddSchedule.Enabled = True
                lblredirect.Visible = False
                If ((ViewState("edit_Redirect") = Nothing) Or (ViewState("edit_Redirect") = 0)) Then
                    gvSchedule.Visible = True
                    LoadScheduleData2(False, ViewState("edit").ToString())
                End If
            Else
                pnlTable.Visible = True
                gvSchedule.Visible = False
                btnAddSchedule.Enabled = False
                lblredirect.Visible = True
                lblredirect.Text = "Redirect To : " + ddlRedirectTo.SelectedText
            End If
        End If

        If Not (ViewState("save") = Nothing) Then
            If (ddlRedirectTo.SelectedValue = 0) Then
                pnlTable.Visible = True
                'gvSchedule.Visible = True
                btnAddSchedule.Enabled = True
                lblredirect.Visible = False
                If (ViewState("save_Redirect") = 1) Then
                    gvSchedule.Visible = False
                Else
                    LoadScheduleData2(False, ViewState("save").ToString())
                    gvSchedule.Visible = True
                End If
            Else
                pnlTable.Visible = True
                gvSchedule.Visible = False
                btnAddSchedule.Enabled = False
                lblredirect.Visible = True
                lblredirect.Text = "Redirect To : " + ddlRedirectTo.SelectedText
            End If

        End If

    End Sub

    Protected Sub btnSaveTable_Click(sender As Object, e As System.EventArgs) Handles btnSaveTable.Click
        Try
            Dim strQuery As String
            Dim conn As DBConnection = New DBConnection()
            Dim objCommon As Common = New Common()
            Dim que = ""
            If hdn_Table_id.Value <> 0 Then
                que = "Select 1 from M_Table Where TableNo = " & txtTableNo.Text & " and table_id <> " + hdn_Table_id.Value + " "
            Else
                que = "Select 1 from M_Table Where TableNo = " & txtTableNo.Text
            End If
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count > 0) Then
                'no
                divMsgErr.Visible = True
                divMsgErr.Attributes.Add("class", "alert alert-danger")
                lblMsg.Text = "Table No already exist"
            Else

                Dim tablejoin As String = objCommon.GetSelectedValue(ddlTableJoin)
                If tablejoin = "" Or tablejoin = Nothing Then
                    tablejoin = ""
                End If
                If hdn_Table_id.Value <> 0 Then
                    strQuery = "UPDATE M_Table SET Table_name = '" + txtTableName.Text.ToString() + "', MinCover =" + txtMinCovers.Text.ToString() + ", MaxCover =" + txtMaxCovers.Text.ToString() + ", AllowedJoin ='" + tablejoin.ToString() + "', TableNo = " + txtTableNo.Text + ", ModifyDate =GETDATE(), CreatedBy =" + Sessions.UserID.ToString() + ", IPAddress ='" + Request.UserHostAddress.ToString() + "' "
                    strQuery += " where Table_id = " + hdn_Table_id.Value + " "
                Else
                    strQuery = "INSERT INTO M_Table (Table_name, MinCover, MaxCover, AllowedJoin, TableNo, CreatedDate, ModifyDate, CreatedBy, IPAddress)"
                    strQuery += " VALUES ('" + txtTableName.Text.ToString() + "'," + txtMinCovers.Text.ToString() + "," + txtMaxCovers.Text.ToString() + ",'" + tablejoin.ToString() + "'," + txtTableNo.Text.ToString() + ",GETDATE(),GETDATE()," + Sessions.UserID.ToString() + ",'" + Request.UserHostAddress.ToString() + "')"
                End If
                conn.Ins_Upd_Del(strQuery)
                divMsgErr.Visible = True
                divMsgErr.Attributes.Add("class", "alert alert-success")
                If hdn_Table_id.Value <> 0 Then
                    lblMsg.Text = "Table Updated Successfully "
                Else
                    lblMsg.Text = "Table Save Successfully"
                End If
                ClearTableField()
                BindgvTableAndDDl()
                BindTableForGroup()
                hdn_Table_id.Value = 0
                btnSaveTable.Text = "Save"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancelTable_Click(sender As Object, e As System.EventArgs) Handles btnCancelTable.Click
        ClearTableField()
        BindgvTableAndDDl()
        BindTableForGroup()
        hdn_Table_id.Value = 0
        btnSaveTable.Text = "Save"
        divMsgErr.Visible = False
    End Sub

    Public Sub BindgvTableAndDDl()
        Try
            gvTable.DataSource = Nothing
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNames()
            gvTable.DataSource = ds
            gvTable.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearTableField()
        txtTableName.Text = ""
        txtMinCovers.Text = ""
        txtMaxCovers.Text = ""
        txtTableNo.Text = ""
        ddlTableJoin.ClearCheckedItems()
    End Sub

    Public Sub GetTableNamesWithoutId()
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetTableNamesWithoutId(hdn_Table_id.Value)
        If ds.Tables.Count > 0 Then
            ddlTableJoin.DataSource = ds
            ddlTableJoin.DataTextField = "Table_name"
            ddlTableJoin.DataValueField = "Table_id"
            ddlTableJoin.DataBind()
        End If
    End Sub

    Protected Sub gvTable_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvTable.ItemDataBound
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub

    Protected Sub gvTable_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvTable.ItemCommand

        If (e.CommandName = "EditTableName") Then
            Try
                hdn_Table_id.Value = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("Table_id").ToString())
                Dim strQuery As String = " SELECT * FROM M_Table WHERE Table_id = " & hdn_Table_id.Value
                Dim conn As DBConnection = New DBConnection()
                Dim dr As DataRow = conn.SingleRow(strQuery)
                txtTableName.Text = Utils.NullToString(dr("Table_name"))
                txtMinCovers.Text = Utils.NullToString(dr("MinCover"))
                txtMaxCovers.Text = Utils.NullToString(dr("MaxCover"))
                txtTableNo.Text = Utils.NullToString(dr("TableNo"))
                GetTableNamesWithoutId()
                Dim collection As IList(Of RadComboBoxItem) = ddlTableJoin.Items
                Dim stArr As String() = Utils.NullToString(dr("AllowedJoin")).ToString().Split("#")
                For i As Integer = 0 To stArr.Length - 1
                    For Each item As RadComboBoxItem In collection
                        If stArr(i).ToString = item.Value Then
                            item.Checked = True
                            Exit For
                        End If

                    Next
                Next
                btnSaveTable.Text = "Update"
                divMsgErr.Visible = False
            Catch ex As Exception

            End Try

        ElseIf (e.CommandName = "DeleteTableName") Then
            Try
                Dim tabID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("Table_id").ToString())
                Dim strQuery As String = ""
                Dim conn As DBConnection = New DBConnection()
                Dim objCommon As Common = New Common()
                Dim que = "Select 1 from M_Table Where AllowedJoin LIKE '%" + tabID.ToString() + "%'"
                Dim ds As DataSet = conn.SelectData(que)
                If (ds.Tables(0).Rows.Count = 0) Then
                    Dim que1 = "Select 1 from M_Table_Group Where table_set LIKE '%" + tabID.ToString() + "%'"
                    Dim ds1 As DataSet = conn.SelectData(que1)
                    If (ds1.Tables(0).Rows.Count = 0) Then
                        Dim strQuery1 As String = " DELETE FROM M_Table WHERE Table_id = " & tabID
                        Dim conn1 As DBConnection = New DBConnection()
                        conn1.Ins_Upd_Del(strQuery1)
                        BindgvTableAndDDl()
                        BindTableForGroup()
                        divMsgErr.Visible = True
                        divMsgErr.Attributes.Add("class", "alert alert-success")
                        lblMsg.Text = "Group Deleted Successfully "
                    Else
                        divMsgErr.Visible = True
                        divMsgErr.Attributes.Add("class", "alert alert-danger")
                        lblMsg.Text = "Table assigned in group"
                    End If
                Else
                    divMsgErr.Visible = True
                    divMsgErr.Attributes.Add("class", "alert alert-danger")
                    lblMsg.Text = "Table Join with auther table"
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub btnSaveGroup_Click(sender As Object, e As System.EventArgs) Handles btnSaveGroup.Click
        Try
            Dim strQuery As String
            Dim conn As DBConnection = New DBConnection()
            Dim objCommon As Common = New Common()
            Dim que = ""
            If hdn_group_id.Value <> 0 Then
                que = "Select 1 from M_Table_Group Where UPPER(group_name) = UPPER('" & txtGroupName.Text & "') and GroupID <> " + hdn_group_id.Value + " "
            Else
                que = "Select 1 from M_Table_Group Where UPPER(group_name) = UPPER('" & txtGroupName.Text & "')"
            End If
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count > 0) Then
                'no
                divGMsgerr.Visible = True
                divGMsgerr.Attributes.Add("class", "alert alert-danger")
                lblGmsg.Text = "Group Name already exist"
            Else

                Dim tableSetjoin As String = objCommon.GetSelectedValue(ddlTableSet)
                If tableSetjoin <> "" Or tableSetjoin <> Nothing Then
                    If hdn_group_id.Value <> 0 Then
                        strQuery = "UPDATE M_Table_Group SET group_name = '" + txtGroupName.Text.ToString() + "', table_set ='" + tableSetjoin.ToString() + "', ModifyDate = GETDATE(), CreatedBy =" + Sessions.UserID.ToString() + ", IPAddress ='" + Request.UserHostAddress.ToString() + "' "
                        strQuery += " where GroupID = " + hdn_group_id.Value + " "
                    Else
                        strQuery = "INSERT INTO M_Table_Group (group_name, table_set, CreatedDate, ModifyDate, CreatedBy, IPAddress)"
                        strQuery += " VALUES ('" + txtGroupName.Text.ToString() + "','" + tableSetjoin.ToString() + "',GETDATE(),GETDATE()," + Sessions.UserID.ToString() + ",'" + Request.UserHostAddress.ToString() + "')"
                    End If
                    conn.Ins_Upd_Del(strQuery)
                    divGMsgerr.Visible = True
                    divGMsgerr.Attributes.Add("class", "alert alert-success")
                    If hdn_group_id.Value <> 0 Then
                        lblGmsg.Text = "Group Updated Successfully "
                    Else
                        lblGmsg.Text = "Group Saved Successfully"
                    End If
                    ClearGroupField()
                    BindgvGroupAndDDl()
                    hdn_group_id.Value = 0
                    btnSaveGroup.Text = "Save"
                Else
                    divGMsgerr.Visible = True
                    divGMsgerr.Attributes.Add("class", "alert alert-danger")
                    lblGmsg.Text = "Please select Proper Table to join with group"
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnCancelGroup_Click(sender As Object, e As System.EventArgs) Handles btnCancelGroup.Click
        ClearGroupField()
        BindgvGroupAndDDl()
        hdn_group_id.Value = 0
        btnSaveGroup.Text = "Save"
        divGMsgerr.Visible = False
    End Sub

    Public Sub BindTableForGroup()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNames()
            If ds.Tables.Count > 0 Then
                ddlTableJoin.DataSource = ds
                ddlTableJoin.DataTextField = "Table_name"
                ddlTableJoin.DataValueField = "Table_id"
                ddlTableJoin.DataBind()
                ddlTableSet.DataSource = ds
                ddlTableSet.DataTextField = "table_name"
                ddlTableSet.DataValueField = "table_id"
                ddlTableSet.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub BindgvGroupAndDDl()
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetGroupNames()
            gvGroup.DataSource = ds
            gvGroup.DataBind()
            ViewState("gvSchedule") = ds
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearGroupField()
        txtGroupName.Text = ""
        ddlTableSet.ClearCheckedItems()
    End Sub

    Protected Sub gvGroup_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gvGroup.ItemDataBound
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        If item IsNot Nothing Then

            Dim btnDelete As ImageButton = TryCast(item("btnDelete").Controls(0), ImageButton)

            If (btnDelete IsNot Nothing) Then
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record ?')")
            End If
        End If
    End Sub

    Protected Sub gvGroup_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvGroup.ItemCommand

        If (e.CommandName = "EditGroup") Then
            hdn_group_id.Value = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("GroupID").ToString())
            Dim strQuery As String = " SELECT * FROM M_Table_Group WHERE GroupID = " & hdn_group_id.Value
            Dim conn As DBConnection = New DBConnection()
            Dim dr As DataRow = conn.SingleRow(strQuery)
            txtGroupName.Text = Utils.NullToString(dr("group_name"))
            Dim collection As IList(Of RadComboBoxItem) = ddlTableSet.Items
            Dim stArr As String() = Utils.NullToString(dr("table_set")).ToString().Split("#")
            For i As Integer = 0 To stArr.Length - 1
                For Each item As RadComboBoxItem In collection
                    If stArr(i).ToString = item.Value Then
                        item.Checked = True
                        Exit For
                    End If
                Next
            Next
            btnSaveGroup.Text = "Update"
            divGMsgerr.Visible = False
        ElseIf (e.CommandName = "DeleteGroup") Then
            Dim grpID As Int32 = Convert.ToInt32(TryCast(e.Item, GridDataItem).GetDataKeyValue("GroupID").ToString())
            Dim conn As DBConnection = New DBConnection()
            Dim objCommon As Common = New Common()

            'Dim strQuery As String = " DELETE FROM M_Table_Group WHERE GroupID = " & grpID.ToString()
            'conn.Ins_Upd_Del(strQuery)
            'BindgvGroupAndDDl()
            'divGMsgerr.Visible = True
            'divGMsgerr.Attributes.Add("class", "alert alert-success")
            'lblGmsg.Text = "Group Deleted Successfully "
            Dim que = "select 1 from BookingSchedule where GroupID = " + grpID.ToString() + ""
            Dim ds As DataSet = conn.SelectData(que)
            If (ds.Tables(0).Rows.Count = 0) Then
                Dim strQuery1 As String = " DELETE FROM M_Table_Group WHERE GroupID = " & grpID.ToString()
                Dim conn1 As DBConnection = New DBConnection()
                conn1.Ins_Upd_Del(strQuery1)
                BindgvGroupAndDDl()
                divGMsgerr.Visible = True
                divGMsgerr.Attributes.Add("class", "alert alert-success")
                lblGmsg.Text = "Group Deleted Successfully "
            Else
                divGMsgerr.Visible = True
                divGMsgerr.Attributes.Add("class", "alert alert-danger")
                lblGmsg.Text = "Can not delete Group. Reference existing in schedule"
            End If

        End If
    End Sub

    Protected Sub gvTable_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvTable.NeedDataSource
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetTableNames()
        gvTable.DataSource = ds
    End Sub

    Protected Sub gvGroup_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvGroup.NeedDataSource
        Dim objCommon As Common = New Common()
        Dim ds As DataSet = objCommon.GetGroupNames()
        gvGroup.DataSource = ds
    End Sub
End Class

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
        If (Not Page.IsPostBack) Then
            Dim conn As DBConnection = New DBConnection()
            Dim str As String = conn.SingleCell("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;")
            BindGrid()
            If (str <> "0") Then

                BindVenue()
                BindGuestAc()
                BindCheckedGuestAc()

                BindRedirectToLocations()

                BindTabNameForLocationSettings()

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
            'If ddlTableGroup.SelectedValue <> ddlLiveTableGroup.SelectedValue Then
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
                        strQvuery &= ",[Guestaccount] = 0 "
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
                        strQvuery &= ",[Guestaccount] = 0 "
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
            'Else
            '    divMessageBox.Attributes.Add("class", "alert alert-danger")
            '    divMessageBox.Visible = True
            '    lblMessageBox.Text = "<strong>Other Table Group</strong> and <strong>Other Live Table Group</strong> can't be same."
            'End If

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
        If e.CommandName = "SDelete" Then
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


End Class

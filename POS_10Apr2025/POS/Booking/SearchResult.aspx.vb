Imports System.Data
Imports Telerik.Web.UI

Partial Class SearchResult
    Inherits System.Web.UI.Page

    Public ReadOnly Property CurrentTabID() As Int32
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("TabId")) Then
                Return Utils.ParseInt(Request.QueryString("TabId"))
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property TypeID() As Int32
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("TypeId")) Then
                Return Utils.ParseInt(Utils.Decrypt(Request.QueryString("TypeId")))
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property IsOnline() As Int32
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("IsOnline")) Then
                Return Utils.ParseInt(Request.QueryString("IsOnline"))
            Else
                Return 0
            End If
        End Get
    End Property

    Public Property ArrivalDate() As String
        Get
            If (Not ViewState("ArrivalDate") Is Nothing) Then
                Return ViewState("ArrivalDate")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("ArrivalDate") = value
        End Set
    End Property

    Public Property DepartureDate() As String
        Get
            If (Not ViewState("DepartureDate") Is Nothing) Then
                Return ViewState("DepartureDate")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("DepartureDate") = value
        End Set
    End Property

    Public ReadOnly Property VenueId() As String
        Get
            If ddlVenue.SelectedItem.Value <> 0 Then
                Try
                    Return ddlVenue.SelectedItem.Value.ToString()
                Catch ex As Exception
                    Return "0"
                End Try
            Else
                Return "0"
            End If
        End Get
    End Property

    Public ReadOnly Property CurrencySymbol() As String
        Get
            Dim ds As DataSet = Common.GetXmlData()
            If Not ds.Tables("Currency") Is Nothing Then
                Return DirectCast(ds.Tables("Currency").Rows(0)("Symbol").ToString(), String)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("AllowSetting") = Nothing
        If (Not Page.IsPostBack()) Then
            If CurrentTabID > 0 Then
                Common.BindRoomStoreDropdown(ddlVenue, CurrentTabID)
            Else
                Common.BindRoomStoreDropdownForWidgetRad(ddlVenue)
            End If
            'Common.BindVenue(ddlVenue)
            'ddlVenue.Items.Insert(0, New DropDownListItem("All", "0"))
            'ddlVenue.SelectedValue = "0"

            If (Not Request.QueryString("venue") Is Nothing And Not Request.QueryString("arrival") Is Nothing And Not Request.QueryString("departure") Is Nothing) Then
                ddlVenue.SelectedValue = Utils.Decrypt(Request.QueryString("venue"))
                dtpArrival.SelectedDate = Utils.Decrypt(Request.QueryString("arrival"))
                dtpDeparture.SelectedDate = Utils.Decrypt(Request.QueryString("departure"))
                BindType()
                ddlType.SelectedValue = TypeID()
                BindRepeater()
            End If
        End If

    End Sub

    Private Sub BindType()
        ddlType.Items.Clear()
        If ddlVenue.Items.Count > 0 Then
            Dim obj As Common = New Common()
            Dim ds As DataSet = obj.GetTableType(ddlVenue.SelectedValue, dtpArrival.SelectedDate)
            If ds IsNot Nothing Then
                If ds.Tables.Count > 0 Then
                    ddlType.DataSource = ds.Tables(0)
                    ddlType.DataTextField = "Name"
                    ddlType.DataValueField = "Id"
                    ddlType.DataBind()
                End If
            End If
        End If
    End Sub

    Public ReadOnly Property TypeID_byddl() As String
        Get
            If Not String.IsNullOrEmpty(ddlType.SelectedValue) Then
                Return Utils.Encrypt(ddlType.SelectedValue)
            Else
                Return 0
            End If
        End Get
    End Property

    Private Sub BindRepeater()
        If ddlType.Items.Count = Nothing Then
            repRooms.DataSource = Nothing
            repRooms.DataBind()
        Else
            ArrivalDate = Utils.Encrypt(dtpArrival.SelectedDate)
            DepartureDate = Utils.Encrypt(dtpDeparture.SelectedDate)

            Session("Arrivaldate_1") = Format(dtpArrival.SelectedDate, "dd/MM/yyyy")
            Session("Departure_1") = Format(dtpDeparture.SelectedDate, "dd/MM/yyyy")

            lblRoomsOf.Text = ddlVenue.SelectedText

            Dim que As String = ""
            que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='StoreRoomTypes')DROP TABLE StoreRoomTypes" & Environment.NewLine
            que &= " IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='bookedrooms')DROP TABLE bookedrooms" & Environment.NewLine
            que &= " CREATE TABLE bookedrooms (roomid int null, bookingid int null, date smalldatetime null,storeid int null)" & Environment.NewLine
            que &= " INSERT INTO bookedrooms (roomid, bookingid, date,storeid)" & Environment.NewLine
            que &= " SELECT B.RoomID,B.bookingid,B.date,S.OurStoreId" & Environment.NewLine
            que &= " FROM Bookings B" & Environment.NewLine
            que &= " INNER JOIN BookingSettings BS ON B.BookingSettingsID = BS.BookingSettingsID" & Environment.NewLine
            que &= " left outer join StoreMaster S ON BS.StoreID = S.OurStoreId" & Environment.NewLine
            que &= " WHERE BS.IsActive = 1 AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')" & Environment.NewLine
            que &= " AND (date BETWEEN '" + dtpArrival.SelectedDate.Value.ToString("s") + "' AND '" + dtpDeparture.SelectedDate.Value.ToString("s") + "') " & Environment.NewLine
            que &= " AND IsCanceled = 0" & Environment.NewLine
            'If (ddlVenue.SelectedValue <> "0") Then
            que &= " AND BS.BookingSettingsID = '" + ddlVenue.SelectedValue + "'" & Environment.NewLine
            'End If
            que &= " CREATE TABLE StoreRoomTypes(roomtypeid int,storeid int)" & Environment.NewLine
            que &= " INSERT INTO StoreRoomTypes(roomtypeid,storeid)" & Environment.NewLine
            que &= " SELECT DISTINCT P.ParentID,SM.OurStoreId" & Environment.NewLine
            que &= " FROM ProdStore PS INNER JOIN Product P ON PS.ProductID = P.ProductID AND P.IsParent = 0 AND P.INACTIVE = 0" & Environment.NewLine
            que &= " INNER JOIN Store S ON PS.StoreID = S.StoreID" & Environment.NewLine
            que &= " left outer join StoreMaster SM ON S.StoreID = SM.OtherStoreId" & Environment.NewLine
            que &= " INNER JOIN BookingSettings BS ON BS.StoreID = SM.OurStoreId" & Environment.NewLine
            que &= " WHERE BS.IsActive = 1 AND P.ProdSort = BS.Sort AND (BS.bookingtype = '" & Utils.Encrypt("1") & "' OR BS.bookingtype = '" & Utils.Encrypt("3") & "')" & Environment.NewLine
            que &= " AND NOT EXISTS (SELECT 1 FROM bookedrooms BR WHERE BR.roomid = PS.ProductID AND BR.storeid = PS.StoreID)" & Environment.NewLine
            'If (ddlVenue.SelectedValue <> "0") Then
            que &= " AND BS.BookingSettingsID = '" + ddlVenue.SelectedValue + "'" & Environment.NewLine
            'End If
            que &= " SELECT DISTINCT P.ProductID as RoomTypeID,P.Name as RoomType,cast(Price1Eachsize_1 *.01 as Decimal (10,2))as price, SM.StoreName StoreName,SM.OurStoreId AS StoreId" & Environment.NewLine
            que &= " FROM StoreRoomTypes SRT INNER JOIN Product P ON SRT.roomtypeid = P.ProductID" & Environment.NewLine
            que &= " left outer join StoreMaster SM ON SM.OurStoreId = SRT.storeid" & Environment.NewLine
            que &= " INNER JOIN Store S ON SM.OtherStoreId = S.StoreID" & Environment.NewLine
            que &= " INNER JOIN ProdStore PS ON SRT.roomtypeid = PS.ProductID AND SM.OurStoreId = SRT.storeid" & Environment.NewLine
            que &= " DROP TABLE StoreRoomTypes" & Environment.NewLine
            que &= " drop table bookedrooms" & Environment.NewLine

            Dim conn As DBConnection = New DBConnection()
            Dim ds As DataSet = conn.SelectData(que)
            repRooms.DataSource = ds.Tables(0)
            repRooms.DataBind()

            If (repRooms.Items.Count = 0) Then
                divMessageBox.Attributes.Add("class", "alert alert-info")
                divMessageBox.Visible = True
                lblMessageBox.Text = "No Record found."
            Else
                divMessageBox.Attributes.Add("class", "")
                divMessageBox.Visible = False
                lblMessageBox.Text = "No Record found."
            End If
        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindRepeater()
        If ddlType.Items.Count = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Type not found');", True)
        End If
    End Sub

    Protected Sub dtpArrival_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles dtpArrival.SelectedDateChanged
        dtpDeparture.SelectedDate = Convert.ToDateTime(dtpArrival.SelectedDate).AddDays(1)
        BindType()
        BindRepeater()
    End Sub
End Class

Imports System.Data
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Calendar

Partial Class BookingEasy_PopupChangeTable
    Inherits System.Web.UI.Page
    'Dim oClsDataccess As New ClsDataccess

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

    Public Property BookingRefNo() As String
        Get
            If (Not ViewState("BookingRefNo") Is Nothing) Then
                Return ViewState("BookingRefNo")
            End If
            Return ""
        End Get
        Set(ByVal value As String)
            ViewState("BookingRefNo") = value
        End Set
    End Property

    Public ReadOnly Property BookingRef() As String
        Get
            If Not String.IsNullOrEmpty(Request.QueryString("BR")) Then
                Return Utils.Decrypt(Request.QueryString("BR")).ToString()
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public Property CustomerID() As Int32
        Get
            If (Not ViewState("CustomerID") Is Nothing) Then
                Return ViewState("CustomerID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("CustomerID") = value
        End Set
    End Property

    Public Property VenueID() As Int32
        Get
            If (Not ViewState("VenueID") Is Nothing) Then
                Return ViewState("VenueID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("VenueID") = value
        End Set
    End Property

    Public Property StoreID() As Int32
        Get
            If (Not ViewState("StoreID") Is Nothing) Then
                Return ViewState("StoreID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("StoreID") = value
        End Set
    End Property


    Public Property BookingSettingID() As Int32
        Get
            If (Not ViewState("BookingSettingID") Is Nothing) Then
                Return ViewState("BookingSettingID")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("BookingSettingID") = value
        End Set
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

    Public Property NoOfCover() As Int32
        Get
            If (Not ViewState("NoOfCover") Is Nothing) Then
                Return ViewState("NoOfCover")
            End If
            Return 0
        End Get
        Set(ByVal value As Int32)
            ViewState("NoOfCover") = value
        End Set
    End Property

    Public Property BookingService() As DataTable
        Get
            If (ViewState("BookingService") IsNot Nothing) Then
                Return DirectCast(ViewState("BookingService"), DataTable)
            End If
            Dim conn As DBConnection = New DBConnection()
            Dim dt As DataTable = conn.SelectData("select * from BookingServices where BookingServiceID = -1").Tables(0)
            dt.Columns("BookingServiceID").AutoIncrement = True
            'dt.Columns.Add("ProductName")
            ViewState("BookingService") = dt
            Return dt
        End Get
        Set(ByVal value As DataTable)
            ViewState("BookingService") = value
        End Set
    End Property

    Public Property BookingScheduleID() As Integer
        Get
            If ViewState("bookingScheduleId") IsNot Nothing Then
                Try
                    Return Utils.getInteger(ViewState("bookingScheduleId").ToString())
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Int32)
            ViewState("bookingScheduleId") = value
        End Set
    End Property

    Public Property BookingScheduleDateId() As Integer
        Get
            If ViewState("bookingScheduleDateId") IsNot Nothing Then
                Try
                    Return Utils.getInteger(ViewState("bookingScheduleDateId").ToString())
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Int32)
            ViewState("bookingScheduleDateId") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Not Page.IsPostBack()) Then

                If Sessions.UserID = 0 Then
                    If Sessions.Login = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload1", "window.parent.location='../Login.aspx';", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
                    End If
                End If

                If Request.QueryString("bookingref").ToString() IsNot Nothing Then
                    Bind()
                    'lblBookingRef.Text = Request.QueryString("bookingref").ToString()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Bind()
        Try
            Dim que As String = "  "
            que += " SELECT SM.OurStoreId,SM.StoreName AS Location, B.bookingid,B.accountid, BSc.GroupID, B.covers, (CASE WHEN b.Allotted_Tables is NULL THEN 'No Table Allotted' ELSE (select Stuff((SELECT ', ' + convert(varchar(50),TableNo) from M_Table where Table_id in (select items from dbo.Split((b.Allotted_Tables),('#'))) FOR XML PATH('')), 1, 2, '')) END )as Allotted_Tables,"
            que += " A.FirstName+' '+A.LastName AS FullName, B.covers, convert(nvarchar(10),B.date,103) AS arrivaldate, B.bookingref, B.bookingtime, BSc.Name AS ScheduleName,B.date,convert(nvarchar(5),bookingtime,108) as time "
            que += " FROM bookings B INNER JOIN Account A ON B.accountid = A.accountid left outer join StoreMaster SM ON B.OurStoreId = SM.OurStoreId  "
            que += " INNER JOIN BookingSchedule BSc ON BSc.BookingScheduleID = b.BookingScheduleID "
            que += " WHERE B.bookingref = '" + Request.QueryString("bookingref").ToString() + "'"
            Dim conn As DBConnection = New DBConnection()
            Dim ds = conn.SelectData(que)
            If ds.Tables(0).Rows.Count > 0 Then
                lblBookingRef.Text = ds.Tables(0).Rows(0)("bookingref").ToString()
                lblCovers.Text = ds.Tables(0).Rows(0)("covers").ToString()
                lblDate.Text = ds.Tables(0).Rows(0)("arrivaldate").ToString()
                lblTime1.Text = ds.Tables(0).Rows(0)("time").ToString()
                lblLocation.Text = ds.Tables(0).Rows(0)("Location").ToString()
                lblSchedule.Text = ds.Tables(0).Rows(0)("ScheduleName").ToString()
                'dtpDate.SelectedDate = ds.Tables(0).Rows(0)("date")
                'txtNoOfCovers.Text = ds.Tables(0).Rows(0)("covers").ToString()
                lblAllottedTable.Text = ds.Tables(0).Rows(0)("Allotted_Tables").ToString()
                BindTableForGroup(ds.Tables(0).Rows(0)("GroupID").ToString(), ds.Tables(0).Rows(0)("bookingref").ToString())
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Sub BindTableForGroup(ByVal GroupId As Integer, ByVal bookingref As String)
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetTableNamesByGroupID(GroupId, bookingref)
            If ds.Tables.Count > 0 Then
                ddlTableSet.DataSource = ds
                ViewState("ds") = ds
                ddlTableSet.DataTextField = "table_name"
                ddlTableSet.DataValueField = "table_id"
                ddlTableSet.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSaveChangeTable_Click(sender As Object, e As System.EventArgs) Handles btnSaveChangeTable.Click

        If ddlTableSet.SelectedValue Is Nothing Then
            lblError.Visible = True
            lblError.Text = "Please Select Tables From Available Tables"
        Else
            Try
                Dim que As String = ""
                Dim conn As DBConnection = New DBConnection()
                Dim objCommon As Common = New Common()
                Dim tablejoin As String = objCommon.GetSelectedValue(ddlTableSet)

                Dim vals As String() = tablejoin.ToString().Split("#")

                Dim largest As Integer = Integer.MinValue
                Dim smallest As Integer = Integer.MaxValue

                For Each element As Integer In vals
                    largest = Math.Max(largest, element)
                    smallest = Math.Min(smallest, element)
                Next

                Console.WriteLine(largest)
                Console.WriteLine(smallest)
                Dim oDbConnection As New DBConnection()
                Dim query As DataSet = oDbConnection.SelectData("SELECT TableNo FROM M_Table where Table_id = " + smallest.ToString() + "")
                que = "Update Bookings Set Allotted_Tables='" & tablejoin.ToString() & "', TableNo='" & query.Tables(0).Rows(0)("TableNo").ToString() & "' WHERE bookingref = '" & lblBookingRef.Text.ToString() & "'"
                conn.ExecuteNonQuery(que)
                que = "if exists (select 1 from M_OpenTable where bookingref = '" & lblBookingRef.Text.ToString() & "') begin update Tables set TableNumber = " + query.Tables(0).Rows(0)("TableNo").ToString() + " where TableID = (select tableid from M_OpenTable where bookingref = '" & lblBookingRef.Text.ToString() & "') end"
                conn.ExecuteNonQuery(que)
                Bind()
                'lblSuccess.Text = "Booking Updated Successfully"
                Session("msgbox_Val") = "Update"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Reload1", "window.parent.location.reload();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "window.parent.location.reload(false);", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
                Bind()

            Catch ex As Exception

            End Try
        End If

    End Sub


End Class

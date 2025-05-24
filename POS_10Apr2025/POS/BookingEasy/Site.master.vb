Imports System.Data.SqlClient
Imports System.Data

Partial Class BookingEasy_Site
    Inherits System.Web.UI.MasterPage

    Public ReadOnly Property DBConnectionString() As String
        Get
            Return "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            'ConfigurationManager.ConnectionStrings("DBConnection").ConnectionString.ToString()
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Sessions.UserID <> 0 Then
            liUserInfo.Visible = True
            'liLeftMenu.Visible = True
            'liDashboardMenu.Visible = True
        Else
            If Sessions.Login = 0 Then
                Response.Redirect("~/Signin.aspx")
            End If
        End If

        If Sessions.TabID = 0 Or Sessions.TabID = Nothing Then
            If Sessions.UserID <> 0 Then
                aNavbarHome.HRef = "~\Booking\Dashboard.aspx?TabId=" + Utils.Encrypt("0") + ""
                aMenuHome.HRef = "~\Booking\Dashboard.aspx?TabId=" + Utils.Encrypt("0") + ""
            Else
                If Session("WidType") = "Table" Then
                    aNavbarHome.HRef = "TableBookingWidget.aspx"
                Else
                    aNavbarHome.HRef = "RoomBookingWidget.aspx"
                End If
            End If
        Else
            aNavbarHome.HRef = "~\Booking\Dashboard.aspx?TabId=" + Utils.Encrypt(Sessions.TabID) + ""
            aMenuHome.HRef = "~\Booking\Dashboard.aspx?TabId=" + Utils.Encrypt(Sessions.TabID) + ""
        End If

        Page.Header.DataBind()
        If Not IsPostBack Then
            ValidateUserRole()
            'LoadMenu()
            'LoadLeftMenu()
        End If
    End Sub

    Private Sub LoadLeftMenu()
        Dim strUrl As String
        strUrl = Request.Url.AbsolutePath.ToString.ToLower

        If strUrl.Contains("default.aspx") Then
            liRoom.Attributes.Add("class", "active")
            liRoomRes.Attributes.Add("class", "active")
        ElseIf strUrl.Contains("settings.aspx") Then
            liSetting.Attributes.Add("class", "active")
        ElseIf strUrl.Contains("searchbooking.aspx") Then
            liRoom.Attributes.Add("class", "active")
            liRoomSearch.Attributes.Add("class", "active")
        ElseIf strUrl.Contains("tablebookings.aspx") Then
            liTable.Attributes.Add("class", "active")
            liTableRes.Attributes.Add("class", "active")
        End If
    End Sub

    Private Sub ValidateUserRole()
        Dim strUrl As String = Request.Url.AbsolutePath.ToString.ToLower

        If (Sessions.BookingType = 0 And (strUrl.Contains("dashboard.aspx") Or strUrl.Contains("searching.aspx") Or strUrl.Contains("newsettings.aspx") Or strUrl.Contains("general_setup.aspx") Or strUrl.Contains("tablemanagement.aspx") Or strUrl.Contains("locationscheduledetails.aspx") Or strUrl.Contains("paymenttransaction.aspx") Or strUrl.Contains("paymentgateway.aspx") Or strUrl.Contains("dynamicfieldmaster.aspx") Or strUrl.Contains("booking_synchronize.aspx") Or strUrl.Contains("multivenue.aspx") And Not strUrl.Contains("BookingDetails.aspx"))) Then
            Response.Redirect("~/Login.aspx")
        End If

        If (Sessions.BookingType = 1 And (Not strUrl.Contains("dashboard.aspx") And Not strUrl.Contains("searching.aspx") And Not strUrl.Contains("searchresulttable.aspx") And Not strUrl.Contains("searchresult.aspx") And Not strUrl.Contains("bookingdetail.aspx") And Not strUrl.Contains("roomBookconfime.aspx") And Not strUrl.Contains("newsettings.aspx") And Not strUrl.Contains("general_setup.aspx") And Not strUrl.Contains("tablemanagement.aspx") And Not strUrl.Contains("locationscheduledetails.aspx") And Not strUrl.Contains("BookingDetails.aspx"))) Then
            Response.Redirect("~/Login.aspx")
        End If

        If (Sessions.BookingType = 2 And (Not strUrl.Contains("dashboard.aspx") And Not strUrl.Contains("searching.aspx") And Not strUrl.Contains("searchresulttable.aspx") And Not strUrl.Contains("bookingdetailtable.aspx") And Not strUrl.Contains("tablebookconfime.aspx") And Not strUrl.Contains("newsettings.aspx") And Not strUrl.Contains("general_setup.aspx") And Not strUrl.Contains("tablemanagement.aspx") And Not strUrl.Contains("locationscheduledetails.aspx") And Not strUrl.Contains("BookingDetails.aspx"))) Then
            Response.Redirect("~/Login.aspx")
        End If

    End Sub

    Private Sub LoadMenu()
        Dim conn As New SqlConnection(DBConnectionString())
        'With conn
        '    .ConnectionString = Connstring.Connstring("")
        'End With
        Dim Checksettings As String
        Checksettings = "IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Bookings')SELECT 1 AS res ELSE SELECT 0 AS res;"

        Dim Checksettingscmd As New SqlCommand(Checksettings, conn)
        conn.Open()
        Checksettingscmd.ExecuteNonQuery()
        Dim ds As New DataSet
        Dim adapter As New SqlDataAdapter(Checksettings, conn)
        adapter.Fill(ds)
        conn.Close()

        If ds.Tables(0).Rows(0)("res").ToString = "0" Then
            Response.Redirect("NewSettings.aspx")
        End If

        Dim Bookingtype As String
        Bookingtype = "select bookingtype from Bookingsettings"
        Dim BTcmd As New SqlCommand(Bookingtype, conn)
        conn.Open()
        BTcmd.ExecuteNonQuery()
        Dim Booktype As New SqlDataAdapter(Bookingtype, conn)
        Dim btype As New DataSet
        Booktype.Fill(btype)
        conn.Close()

        If Utils.Decrypt(btype.Tables(0).Rows(0)("bookingtype").ToString) = "3" Then
            liRoom.Visible = True
            liTable.Visible = True
        ElseIf Utils.Decrypt(btype.Tables(0).Rows(0)("bookingtype").ToString) = "2" Then
            liRoom.Visible = False
            liTable.Visible = True
        ElseIf Utils.Decrypt(btype.Tables(0).Rows(0)("bookingtype").ToString) = "1" Then
            liRoom.Visible = True
            liTable.Visible = False
        Else
            Response.Redirect("NewSettings.aspx")

        End If

    End Sub
End Class


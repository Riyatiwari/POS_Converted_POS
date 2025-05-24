
Imports System.Data

Partial Class BookingEasy_popTodaysArrival
    Inherits System.Web.UI.Page

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

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub btnCheckin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckin.Click
        Dim reff As String = Utils.NullToString(Request.QueryString("bookingref"))
        Dim cmmn As Common = New Common()
        Dim dr As DataRow = cmmn.GetBookingDetailByRef(reff)
        If dr IsNot Nothing Then
            Dim drSetting As DataRow = cmmn.GetBookingSettings(Utils.getInteger(dr("StoreID")))
            Dim qCheckin As String = "update account set parentid = " & drSetting("CheckedGuestAccount").ToString() & ", "
            qCheckin &= " creditlimit = " & ddLcredit.SelectedValue & " where accountid = '" & dr("accountid").ToString() & "'"
            qCheckin &= " Update bookings set checkedin = 1 where bookingref = '" & reff & "'"

            Dim conn As DBConnection = New DBConnection()
            conn.ExecuteNonQuery(qCheckin)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
        End If
    End Sub
End Class

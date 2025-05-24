
Imports System.Data

Partial Class BookingEasy_popTodaysDeparture
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If (Request.QueryString("bookingref") IsNot Nothing) Then
                Dim reff As String = Utils.NullToString(Request.QueryString("bookingref"))
                Dim cmmn As Common = New Common()
                Dim dr As DataRow = cmmn.GetBookingDetailByRef(reff)

                Dim accId As String = dr("AccountID").ToString()
                Dim conn As DBConnection = New DBConnection()

                Dim qur As String = "select cast(accountbalance *0.01 as Decimal (10,2))as accountbalance from account where accountid = '" & accId & "'"
                Dim accDet As DataRow = conn.SingleRow(qur)

                If accDet IsNot Nothing Then
                    lblAmount.Text = "This client has an account balance of " & accDet("accountbalance").ToString() & " complete check out ONLY if there is nothing owing"

                    If (Utils.getInteger(accDet("accountbalance")) = 0) Then
                        btnCheckOut.Enabled = True
                    Else
                        btnCheckOut.Enabled = False
                        divMessageBox.Visible = True
                        divMessageBox.Attributes.Add("class", "alert alert-danger")
                        lblMessageBox.Text = "You can not check this client out, please make nessary account payments in Other."
                    End If

                End If
            End If
        End If
    End Sub

    Protected Sub btnCheckOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOut.Click

        Dim reff As String = Utils.NullToString(Request.QueryString("bookingref"))
        Dim cmmn As Common = New Common()
        Dim dr As DataRow = cmmn.GetBookingDetailByRef(reff)
        If dr IsNot Nothing Then
            Dim drSetting As DataRow = cmmn.GetBookingSettings(Utils.getInteger(dr("StoreID")))

            Dim qCheckin As String = " update account set parentid = " & drSetting("Guestaccount").ToString()
            qCheckin &= " where accountid = '" & dr("accountid").ToString() & "'Update bookings set checkedin = 0 where bookingref = '" & reff & "'"

            Dim conn As DBConnection = New DBConnection()
            conn.ExecuteNonQuery(qCheckin)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Close Popup", "parent.$.colorbox.close();", True)
        End If

    End Sub
End Class


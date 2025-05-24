
Partial Class BookingEasy_TableBookingWidget
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Header.DataBind()
        Session("store") = Request.QueryString("s_name").ToString()



    End Sub
End Class

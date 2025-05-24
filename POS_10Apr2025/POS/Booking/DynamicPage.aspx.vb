
Partial Class BookingEasy_DynamicPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim navDiv As HtmlContainerControl = DirectCast(Me.Master.FindControl("navbar"), HtmlContainerControl)
        navDiv.Visible = False
        ucCustomer1.Category_type = Request.QueryString("cat").ToString()
    End Sub
End Class

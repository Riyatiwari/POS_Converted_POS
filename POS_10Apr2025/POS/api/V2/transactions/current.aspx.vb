Imports System.Web.Script.Serialization
Partial Class api_V2_transactions_current
    Inherits System.Web.UI.Page

    Private Sub api_V2_transactions_current_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        System.Threading.Thread.Sleep(1000)

        If Session("no") = Nothing Then
            Session("no") = "1"
        End If

        ' Response.Write(Session("no").ToString())

        Dim strResp As String = "{'scene':{'path':'app.views.layouts.payment.prompt_payment','text':'Insert, tap or swipe cardnSale : £4.00nnn'},'success':true}"

        If Val(Session("no")) >= 5 Then
            Session("no") = 1
            strResp = "{'scene':{'path':'app.views.layouts.idle','text':'Tap Green To Startnnnnn'},'success':true}"
        Else
            Session("no") = Val(Session("no")) + 1
        End If

        Dim j As JavaScriptSerializer = New JavaScriptSerializer()
        Dim a As Object = j.Deserialize(strResp, GetType(Object))

        Response.Write(serializer.Serialize(a))

    End Sub
End Class

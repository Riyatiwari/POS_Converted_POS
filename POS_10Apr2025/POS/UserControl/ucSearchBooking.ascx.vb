Imports System.Data

Partial Class UserControl_ucSearchBooking
    Inherits System.Web.UI.UserControl

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strFor As String
        If (rdoCustomer.Checked) Then
            strFor = "customer"
        Else
            strFor = "booking"
        End If

        Response.Redirect("Searching.aspx?search=" + txtSearchClient.Text.Trim() + "&for=" + strFor)

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim objCommon As Common = New Common()
            Dim ds As DataSet = objCommon.GetDefaultField()
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("DefaultField") = 0 Then
                    lblSearchOption.Text = "(Search By Email)"
                ElseIf ds.Tables(0).Rows(0)("DefaultField") = 1 Then
                    lblSearchOption.Text = "(Search By Mobile)"
                ElseIf ds.Tables(0).Rows(0)("DefaultField") = 2 Then
                    lblSearchOption.Text = "(Search By Email or Mobile)"
                End If
            Else
                lblSearchOption.Text = "(Search By Email)"
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

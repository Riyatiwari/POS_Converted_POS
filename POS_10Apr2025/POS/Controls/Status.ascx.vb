
Partial Class Controls_Status
    Inherits System.Web.UI.UserControl

    Protected Sub lbLogout_Click(sender As Object, e As System.EventArgs) Handles lbLogout.Click
        Try
            Session.RemoveAll()
            Response.Redirect("../SignIn.aspx?msg=Logout Successfully")
        Catch ex As Exception
            LogHelper.Error("Employee_master:binddlls:" + ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("emp_id") = "0" Then
                    btnType.Enabled = False
                Else
                    If Session("emp_role_type") = "0" Then
                        btnType.Enabled = True
                    Else
                        btnType.Enabled = False
                    End If

                End If



                Dim sUrl As String = HttpContext.Current.Request.Url.AbsoluteUri
                If sUrl.Contains("Ess") Then
                    btnType.SelectedToggleStateIndex = 1
                Else
                    btnType.SelectedToggleStateIndex = 0
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnType_CheckedChanged(sender As Object, e As System.EventArgs) Handles btnType.CheckedChanged
        Try
            
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnType_Click(sender As Object, e As System.EventArgs) Handles btnType.Click
        Try
            If btnType.SelectedToggleStateIndex = 0 Then
                Response.Redirect("~/Admin/default.aspx")

            Else
                Response.Redirect("~/Ess/E_Default.aspx")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

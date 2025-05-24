Imports System.Data
Imports System.Security.Cryptography
Partial Class defaultValues
    Inherits BaseClass
    Dim oclsDefault As New Controller_defaultValue()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("defaultValues:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCurrency_Click(sender As Object, e As EventArgs)
        Try

            Dim dt As DataTable = oclsDefault.GET_Currency()

            If dt.Rows.Count > 0 Then

                Session("Success") = "Record already exist"

            Else

                oclsDefault.Currency()
                Session("Success") = "Record inserted successfully"

            End If

            Response.Redirect("defaultValues.aspx", False)
        Catch ex As Exception
            LogHelper.Error("defaultValues:btnCurrency_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCountry_Click(sender As Object, e As EventArgs)
        Try
            oclsDefault.Country()
            Session("Success") = "Record inserted successfully"

            Response.Redirect("defaultValues.aspx", False)
        Catch ex As Exception
            LogHelper.Error("defaultValues:btnCountry_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnBooking_Click(sender As Object, e As EventArgs)
        Try
            oclsDefault.Booking()

            Session("Success") = "Record inserted successfully"

            Response.Redirect("defaultValues.aspx", False)
        Catch ex As Exception
            LogHelper.Error("defaultValues:btnBooking_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnDeviceType_Click(sender As Object, e As EventArgs)
        Try

            oclsDefault.cmp_id = Val(Session("cmp_id"))
            oclsDefault.DeviceType()

            Session("Success") = "Record inserted successfully"

            Response.Redirect("defaultValues.aspx", False)
        Catch ex As Exception
            LogHelper.Error("defaultValues:btnDeviceType_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnExpense_Click(sender As Object, e As EventArgs)
        Try
            oclsDefault.Expense()

            Session("Success") = "Record inserted successfully"

            Response.Redirect("defaultValues.aspx", False)
        Catch ex As Exception
            LogHelper.Error("defaultValues:btnExpense_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnUnit_Click(sender As Object, e As EventArgs)
        Try
            oclsDefault.cmp_id = Val(Session("cmp_id"))
            oclsDefault.Unit()

            Session("Success") = "Record inserted successfully"

            Response.Redirect("defaultValues.aspx", False)
        Catch ex As Exception
            LogHelper.Error("defaultValues:btnUnit_Click" + ex.Message)
        End Try
    End Sub
End Class

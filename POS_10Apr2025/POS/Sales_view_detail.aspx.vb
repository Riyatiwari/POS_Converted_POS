Imports System.Data
Partial Class Sales_view_detail
    Inherits System.Web.UI.Page

    Private Sub Sales_view_detail_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If

            If Not Session("Ingredient_dt") Is Nothing Then

                Dim Ingredient_dt As DataTable = DirectCast(Session("Ingredient_dt"), DataTable)

                If Ingredient_dt.Rows.Count > 0 Then
                    rdIngredient.DataSource = Ingredient_dt
                    rdIngredient.DataBind()
                Else
                    rdIngredient.DataSource = String.Empty
                    rdIngredient.DataBind()
                End If

                Session("Ingredient_dt") = ""
            End If

            If Not Session("Condiment_dt") Is Nothing Then

                Dim Condiment_dt As DataTable = DirectCast(Session("Condiment_dt"), DataTable)

                If Condiment_dt.Rows.Count > 0 Then
                    radListCondiment.DataSource = Condiment_dt
                    radListCondiment.DataBind()
                Else
                    radListCondiment.DataSource = String.Empty
                    radListCondiment.DataBind()
                End If

                Session("Condiment_dt") = ""
            End If

        End If
    End Sub

End Class

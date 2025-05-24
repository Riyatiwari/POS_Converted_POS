Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Profile_Master
    Inherits BaseClass
    Dim objclsProfileMaster As New Controller_clsProfileMaster()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("profile_id") = Nothing Then
                    BindProfile()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Profile_Master:Page_Load" + ex.Message)
        End Try
    End Sub
    Private Sub BindProfile()
        Try
            Dim sender As Object
            Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            objclsProfileMaster.cmp_id = Val(Session("cmp_id"))
            objclsProfileMaster.profile_id = Val(Session("profile_id"))
            Dim dt As DataTable = objclsProfileMaster.Select()
            If dt.Rows.Count > 0 Then
                txtproname.Text = dt.Rows(0)("profile_name").ToString
                txtbnsp.Text = dt.Rows(0)("bonus_point").ToString
                txtamt.Text = dt.Rows(0)("amount").ToString
                txtpurchase_amount.Text = dt.Rows(0)("purchase_amount").ToString
                txtdiscountPercent.Text = dt.Rows(0)("discount_percent").ToString

                txtearn_bonus.Text = dt.Rows(0)("earn_bonus").ToString

                If dt.Rows(0)("Is_Default").ToString() = "1" Then
                    chk_IsDefaul.Checked = True
                Else
                    chk_IsDefaul.Checked = False
                End If
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Profile_Master:BindProfile:" + ex.Message)
        End Try

    End Sub
    Protected Sub btnSave_Click1(sender As Object, e As EventArgs)

        Try
            objclsProfileMaster.cmp_id = Session("cmp_id")
            objclsProfileMaster.profile_name = txtproname.Text
            objclsProfileMaster.bonus_point = Val(IIf(txtbnsp.Text = "", 0, txtbnsp.Text))
            objclsProfileMaster.amount = Val(IIf(txtamt.Text = "", 0, txtamt.Text))
            objclsProfileMaster.purchase_amount = Val(IIf(txtpurchase_amount.Text = "", 0, txtpurchase_amount.Text))
            objclsProfileMaster.earn_bonus = Val(IIf(txtearn_bonus.Text = "", 0, txtearn_bonus.Text))
            objclsProfileMaster.discount_percent = Val(IIf(txtdiscountPercent.Text = "", 0, txtdiscountPercent.Text))
            objclsProfileMaster.IsDefaul = IIf(chk_IsDefaul.Checked = True, 1, 0)

            If Session("profile_id") = Nothing Then
                objclsProfileMaster.profile_id = 0
                objclsProfileMaster.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                objclsProfileMaster.profile_id = Val(Session("profile_id"))
                objclsProfileMaster.Update()
                Session("Success") = "Record updated successfully"
            End If

            Session("profile_id") = Nothing
            Response.Redirect("Profile_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Profile_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Profile_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Profile_Master:btnCancel_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Try
            If Session("profile_id") = Nothing Then
                Clear()
            Else
                BindProfile()
            End If
        Catch ex As Exception
            LogHelper.Error("Profile_Master:btnReset_Click" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txtproname.Text = ""
            txtbnsp.Text = ""
            txtamt.Text = ""
            txtdiscountPercent.Text = ""
            chk_IsDefaul.Checked = False

        Catch ex As Exception
            LogHelper.Error("Profile_Master:Clear" + ex.Message)
        End Try
    End Sub
End Class

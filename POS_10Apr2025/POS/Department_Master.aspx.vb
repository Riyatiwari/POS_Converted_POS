Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Department_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsDepartment As New Controller_clsDepartment()
    Dim objclsdeparmentcategory As New Controller_clsDepartmentCategory()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                bindDptCat()

                If Not Session("Department_id") = Nothing Then
                    BindDepartment()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Department_Master:Page_Load:" + ex.Message)
        End Try
    End Sub
    Public Sub bindDptCat()
        Try
            objclsdeparmentcategory.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsdeparmentcategory.SelectAll()
            rdDeptCat.DataSource = dt
            rdDeptCat.DataTextField = "department_category_name"
            rdDeptCat.DataValueField = "department_category_id"
            rdDeptCat.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            rdDeptCat.Items.Insert(0, li)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindDepartment()
        Try
            oclsDepartment.Cmp_id = Val(Session("cmp_id"))
            oclsDepartment.Department_id = Val(Session("Department_id"))
            Dim dtDepartment As DataTable = oclsDepartment.Select()
            If dtDepartment.Rows.Count > 0 Then
                txtName.Text = dtDepartment.Rows(0)("Name").ToString
                txtValue.Text = dtDepartment.Rows(0)("Value").ToString

                rdDeptCat.SelectedValue = 0
                rdDeptCat.Items.FindItemByValue(dtDepartment.Rows(0)("department_category_id").ToString).Selected = True
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Department_Master:BindDepartment:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Department_id") = Nothing Then
                Clear()
            Else
                BindDepartment()
            End If
        Catch ex As Exception
            LogHelper.Error("Department_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Department_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Department_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtName.Text = ""
            txtValue.Text = ""
        Catch ex As Exception
            LogHelper.Error("Department_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsDepartment.Cmp_id = Val(Session("cmp_id"))
            oclsDepartment.Name = txtName.Text.Trim()
            oclsDepartment.Value = Val(txtValue.Text)
            oclsDepartment.Ip_Address = Request.UserHostAddress
            oclsDepartment.Login_id = Val(Session("login_id"))
            oclsDepartment.Department_category_id = Val(rdDeptCat.SelectedValue)
            If Session("Department_id") = Nothing Then
                oclsDepartment.Department_id = 0
                oclsDepartment.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsDepartment.Department_id = Val(Session("Department_id"))
                oclsDepartment.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Department_id") = Nothing
            Response.Redirect("Department_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Department_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
End Class

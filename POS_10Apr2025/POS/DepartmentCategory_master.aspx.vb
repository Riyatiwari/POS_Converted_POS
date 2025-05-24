Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports iTextSharp.text

Partial Class DepartmentCategory_master
    Inherits System.Web.UI.Page
    Dim objclsdeparmentcategory As New Controller_clsDepartmentCategory()
    Dim dt As DataTable = New DataTable
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

                If Not Session("department_category_id") = Nothing Then
                    BindDep()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_master:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            objclsdeparmentcategory.cmp_id = Session("cmp_id")
            objclsdeparmentcategory.department_category_name = txtdepcatname.Text
            objclsdeparmentcategory.AcCode = txt_AcCode.Text
            objclsdeparmentcategory.is_active = IIf(ChkIsActive.Checked = True, 1, 0)
            If Session("department_category_id") = Nothing Then
                objclsdeparmentcategory.department_category_id = 0
                objclsdeparmentcategory.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                objclsdeparmentcategory.department_category_id = Val(Session("department_category_id"))
                objclsdeparmentcategory.Update()
                Session("Success") = "Record updated successfully"
            End If

            Session("department_category_id") = Nothing
            Response.Redirect("DepartmentCategory_list.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("DepartmentCategory_master:BindAllergy:" + ex.Message)
        End Try

    End Sub
    Private Sub BindDep()
        Try
            Dim sender As Object

            objclsdeparmentcategory.cmp_id = Val(Session("cmp_id"))
            objclsdeparmentcategory.department_category_id = Val(Session("department_category_id"))
            Dim dt As DataTable = objclsdeparmentcategory.Select()
            If dt.Rows.Count > 0 Then
                txtdepcatname.Text = dt.Rows(0)("department_category_name").ToString
                txt_AcCode.Text = dt.Rows(0)("AcCode").ToString
                If dt.Rows(0)("is_active").ToString = "1" Then
                    ChkIsActive.Checked = True
                Else
                    ChkIsActive.Checked = False
                End If
                'RadBinaryImage1.DataValue = dt.Rows(0)("amount").ToString


            End If




        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("DepartmentCategory_master:BindProfile:" + ex.Message)
        End Try

    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)


        Try
            If Session("department_category_id") = Nothing Then
                Clear()
            Else
                BindDep()
            End If
        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_master:btnReset_Click" + ex.Message)
        End Try



    End Sub
    Private Sub Clear()
        Try
            txtdepcatname.Text = ""
            txt_AcCode.Text = ""

            ChkIsActive.Checked = False

        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_master:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("DepartmentCategory_list.aspx", False)
        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_master:lnkNew_Click" + ex.Message)
        End Try
    End Sub
End Class

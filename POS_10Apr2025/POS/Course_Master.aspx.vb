Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Course_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsCourse As New Controller_clsCourse()
    Dim oclsCourseCat As New Controller_clsCourseCategory()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                BindCourseddl()

                If Not Session("Course_id") = Nothing Then
                    BindCourse()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Course_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindCourse()
        Try
            oclsCourse.Cmp_id = Val(Session("cmp_id"))
            oclsCourse.Course_id = Val(Session("Course_id"))
            Dim dtCourse As DataTable = oclsCourse.Select()
            If dtCourse.Rows.Count > 0 Then
                txtName.Text = dtCourse.Rows(0)("Name").ToString
                txtValue.Text = dtCourse.Rows(0)("Value").ToString
                rdCourCat.Items.FindItemByValue(dtCourse.Rows(0)("course_category_id").ToString).Selected = True
                If (dtCourse.Rows(0)("is_CheckSlot").ToString = "1") Then
                    chkCheckSLot.Checked = True
                Else
                    chkCheckSLot.Checked = False
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Course_Master:BindCourse:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("Course_id") = Nothing Then
                Clear()
            Else
                BindCourse()
            End If
        Catch ex As Exception
            LogHelper.Error("Course_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Course_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Course_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtName.Text = ""
            txtValue.Text = ""
        Catch ex As Exception
            LogHelper.Error("Course_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsCourse.Cmp_id = Val(Session("cmp_id"))
            oclsCourse.Name = txtName.Text.Trim()
            oclsCourse.Value = Val(txtValue.Text)
            oclsCourse.Ip_Address = Request.UserHostAddress
            oclsCourse.Login_id = Val(Session("login_id"))
            oclsCourse.Course_Category_id = Val(rdCourCat.SelectedValue)
            If chkCheckSLot.Checked = True Then
                oclsCourse.Is_checkSlot = 1
            Else
                oclsCourse.Is_checkSlot = 0
            End If

            If Session("Course_id") = Nothing Then
                oclsCourse.Course_id = 0
                oclsCourse.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsCourse.Course_id = Val(Session("Course_id"))
                oclsCourse.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("Course_id") = Nothing
            Response.Redirect("Course_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Course_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Public Sub BindCourseddl()
        Try
            oclsCourseCat.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsCourseCat.SelectAll()
            rdCourCat.DataSource = dt
            rdCourCat.DataTextField = "Name"
            rdCourCat.DataValueField = "course_category_id"
            rdCourCat.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            rdCourCat.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
End Class

Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class CourseCategory_Master
    Inherits System.Web.UI.Page
    Dim oclsCourse As New Controller_clsCourseCategory()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("course_category_id") = Nothing Then
                    BindCourse()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("CourseCategory_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindCourse()
        Try
            Dim sender As Object

            oclsCourse.cmp_id = Val(Session("cmp_id"))
            oclsCourse.course_category_id = Val(Session("course_category_id"))
            Dim dt As DataTable = oclsCourse.Select()
            If dt.Rows.Count > 0 Then
                'radcourse.ClearSelection()
                'radcourse.SelectedValue = Val(dt.Rows(0)("Course_id"))
                txtcatcatname.Text = (dt.Rows(0)("name"))
                If dt.Rows(0)("is_active").ToString = "1" Then
                    ChkIsActive.Checked = True
                Else
                    ChkIsActive.Checked = False
                End If

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("CourseCategory_master:BindProfile:" + ex.Message)
        End Try

    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
       Try
            oclsCourse.cmp_id = Session("cmp_id")

            oclsCourse.Name = txtcatcatname.Text.ToString()
            oclsCourse.is_active = IIf(ChkIsActive.Checked = True, 1, 0)
            If Session("course_category_id") = Nothing Then
                oclsCourse.course_category_id = 0
                oclsCourse.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsCourse.course_category_id = Val(Session("course_category_id"))
                oclsCourse.Update()
                Session("Success") = "Record updated successfully"
            End If

            Session("course_category_id") = Nothing
            Response.Redirect("CourseCategory_list.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("CourseCategory_master:BindAllergy:" + ex.Message)
        End Try


    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Try
            If Session("course_category_id") = Nothing Then
                Clear()
            Else
                BindCourse()
            End If
        Catch ex As Exception
            LogHelper.Error("CourseCategory_master:btnReset_Click" + ex.Message)
        End Try

    End Sub
    Private Sub Clear()
        Try
            txtcatcatname.Text = ""
            ChkIsActive.Checked = False

        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_master:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("CourseCategory_list.aspx", False)
        Catch ex As Exception
            LogHelper.Error("CourseCategory_master:lnkNew_Click" + ex.Message)
        End Try
    End Sub
    Public Sub BindCourseddl(ByRef ddl As RadComboBox, ByVal cmp_id As Integer)
        Try
            oclsCourse.cmp_id = cmp_id
            Dim dt As DataTable = oclsCourse.Selectcourse()
            ddl.DataSource = dt
            ddl.DataTextField = "Name"
            ddl.DataValueField = "Course_id"
            ddl.DataBind()
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            ddl.Items.Insert(0, li)

        Catch ex As Exception

        End Try
    End Sub
End Class

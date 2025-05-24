Imports System.Data
Imports Telerik.Web.UI
Partial Class CourseCategory_List
    Inherits System.Web.UI.Page
    Dim oclsCourse As New Controller_clsCourseCategory()
    Dim oClsDataccess As New ClsDataccess()
    Public Sub bindGrid()
        Try
            oclsCourse.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsCourse.SelectAll()
            If dt.Rows.Count > 0 Then
                rdcourse.DataSource = dt
                rdcourse.DataBind()
            Else
                rdcourse.DataSource = String.Empty
                rdcourse.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("CourseCategory_List:bindGrid" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Try
                If Not IsPostBack Then

                    If Not Session("success") = Nothing Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                        Session("success") = Nothing
                    End If
                    If Session("cmp_id") = Nothing Then
                        Response.Redirect("SignIn.aspx", False)
                    End If


                    bindGrid()

                End If
            Catch ex As Exception
                LogHelper.Error("CourseCategory_Master:Page_Load" + ex.Message)
            End Try

        Catch ex As Exception
            LogHelper.Error("CourseCategory_List:Page_Load" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try

            Session("course_category_id") = Nothing

            Response.Redirect("CourseCategory_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("CourseCategory_List:lnkNew_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub rddep_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rdcourse.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("course_category_id") = Val(e.CommandArgument)
                Response.Redirect("CourseCategory_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                delete(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("CourseCategory_List:rdprofile_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub delete(ByVal id As Integer)
        Try
            oclsCourse.course_category_id = Val(id)
            oclsCourse.cmp_id = Val(Session("cmp_id"))
            oclsCourse.Course_id = 0
            oclsCourse.is_active = 0
            oclsCourse.Tran_Type = "D"

            oclsCourse.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("CourseCategory_List:deleteAlrgy" + ex.Message)
        End Try
    End Sub
End Class

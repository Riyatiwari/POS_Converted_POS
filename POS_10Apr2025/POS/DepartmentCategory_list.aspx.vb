Imports System.Data
Imports Telerik.Web.UI
Partial Class DepartmentCategory_list
    Inherits BaseClass
    Dim objclsdeparmentcategory As New Controller_clsDepartmentCategory()
    Dim oClsDataccess As New ClsDataccess()
    Public Sub bindGrid()
        Try
            objclsdeparmentcategory.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = objclsdeparmentcategory.SelectAll()
            If dt.Rows.Count > 0 Then
                rddep.DataSource = dt
                rddep.DataBind()
            Else
                rddep.DataSource = String.Empty
                rddep.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("DepartmentCategory_list:bindGrid" + ex.Message)
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
                LogHelper.Error("DepartmentCategory_master:Page_Load" + ex.Message)
            End Try

        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_list:Page_Load" + ex.Message)
        End Try
    End Sub
   
    Protected Sub rddep_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rddep.ItemCommand
        Try
            If e.CommandName = "Edit" Then
                Session("department_category_id") = Val(e.CommandArgument)
                Response.Redirect("DepartmentCategory_master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deleteAlrgy(Val(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_list:rdprofile_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub deleteAlrgy(ByVal id As Integer)
        Try
            objclsdeparmentcategory.department_category_id = Val(id)
            objclsdeparmentcategory.cmp_id = Val(Session("cmp_id"))
            objclsdeparmentcategory.department_category_name = ""
            objclsdeparmentcategory.is_active = 0
            objclsdeparmentcategory.Tran_Type = "D"

            objclsdeparmentcategory.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("DepartmentCategory_list:deleteAlrgy" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs)
        Try

            Session("department_category_id") = Nothing

            Response.Redirect("DepartmentCategory_master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("DepartmentCategory_list:lnkNew_Click" + ex.Message)
        End Try
    End Sub
End Class

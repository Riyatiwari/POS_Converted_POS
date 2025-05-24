Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Class Stk_Purchase_Template
    Inherits BaseClass
    Dim oclstock As New Controller_clsStock()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Stk_Purchase_Template:Page_Load" + ex.Message)
        End Try
    End Sub

    Public Sub bindGrid()
        Try
            oclstock.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclstock.SelectAll_template()
            If dt.Rows.Count > 0 Then
                rdtemplate.DataSource = dt
                rdtemplate.DataBind()
            Else
                rdtemplate.DataSource = String.Empty
                rdtemplate.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stk_Purchase_Template:bindGrid" + ex.Message)
        End Try

    End Sub
    Protected Sub rdtemplate_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Delete" Then

                oclstock.cmp_id = Val(Session("cmp_id"))
                oclstock.Location_id = 0
                oclstock.login_id = 0
                oclstock.stk_rec_id = 0
                oclstock.ip_address = ""
                oclstock.templete_name = ""
                oclstock.supplier_code = ""
                oclstock.supplier_id = ""
                oclstock.Templete_id = Val(e.CommandArgument)

                oclstock.Delete_template()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Allergy_List:rdprofile_ItemCommand" + ex.Message)
        End Try
    End Sub
End Class

Imports System.Data
Imports Telerik.Web.UI

Partial Class ResDiary
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsResDiary As New Controller_clsResDiary


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("ResDiary_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Public Sub bindGrid()
        Try
            oclsResDiary.BookingId = 0
            Dim dt As DataTable = oclsResDiary.SelectAll()

            If dt.Rows.Count > 0 Then
                rdResDiary.DataSource = dt
                rdResDiary.DataBind()
            Else
                rdResDiary.DataSource = String.Empty
                rdResDiary.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("ResDiary_List:bindGrid:" + ex.Message)
        End Try

    End Sub

    Public Sub rebindGrid()
        Try
            oclsResDiary.BookingId = 0
            Dim dt As DataTable = oclsResDiary.SelectAll()

            If dt.Rows.Count > 0 Then
                rdResDiary.DataSource = dt
            Else
                rdResDiary.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("ResDiary_List:rebindGrid" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdResDiary_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdResDiary.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("ResDiary_List:rdResDiary_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub rdResDiary_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "View" Then
                Session("BookingId") = Val(e.CommandArgument)
                Response.Redirect("ResDiary_Detail.aspx", False)

            End If
        Catch ex As Exception
            LogHelper.Error("ResDiary_List:rdResDiary_ItemCommand:" + ex.Message)
        End Try
    End Sub
End Class

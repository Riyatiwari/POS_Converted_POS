Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Class Table_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsTable As New Controller_clsTable()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                oclsBind.BindLocation(ddLocation, Val(Session("cmp_id")))
                If Not Session("table_id") = Nothing Then
                    BindTable()
                Else
                    bindSortingNo()
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Table_Master:Page_Load:" + ex.Message)
        End Try
    End Sub
    Private Sub bindSortingNo()
        Try
            oclsTable.cmp_id = Val(Session("cmp_id"))
            oclsTable.form_name = "Table"
            Dim dtTable As DataTable = oclsTable.Select1()
            If dtTable.Rows.Count > 0 Then
                txtSortingno.Text = Val(dtTable.Rows(0)("SNO"))
            End If
        Catch ex As Exception
            LogHelper.Error("Table_Master:bindSortingNo:" + ex.Message)
        End Try
    End Sub

    Private Sub BindTable()
        Try
            oclsTable.cmp_id = Val(Session("cmp_id"))
            oclsTable.table_id = Val(Session("table_id"))
            Dim dtTable As DataTable = oclsTable.Select()
            If dtTable.Rows.Count > 0 Then
                txtTName.Text = dtTable.Rows(0)("Table_name").ToString
                txtMaxCover.Text = dtTable.Rows(0)("MaxCover").ToString
                txtMinCover.Text = dtTable.Rows(0)("MinCover").ToString
                txtSortingno.Text = dtTable.Rows(0)("SortingNo").ToString
                ddLocation.ClearSelection()
                ddLocation.SelectedValue = dtTable.Rows(0)("location_id").ToString

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Table_Master:BindTable:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("table_id") = Nothing Then
                Clear()
            Else
                BindTable()
            End If
        Catch ex As Exception
            LogHelper.Error("Table_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Try
            Response.Redirect("Table_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Table_Master:btnCancel_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            txtTName.Text = ""
            txtMaxCover.Text = ""
            txtMinCover.Text = ""
            ' txtShortingno.Text = ""

        Catch ex As Exception
            LogHelper.Error("Table_Master:Clear:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            oclsTable.cmp_id = Val(Session("cmp_id"))
            oclsTable.name = txtTName.Text.Trim()
            oclsTable.min_cover = Val(txtMinCover.Text)
            oclsTable.max_cover = Val(txtMaxCover.Text)

            oclsTable.shorting_no = txtSortingno.Text
            oclsTable.ip_address = Request.UserHostAddress
            oclsTable.login_id = Val(Session("login_id"))
            oclsTable.Location_id = ddLocation.SelectedValue
            oclsTable.machine_id = 0
            If Session("table_id") = Nothing Then
                oclsTable.table_id = 0
                oclsTable.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsTable.table_id = Val(Session("table_id"))
                oclsTable.Update()
                Session("Success") = "Record updated successfully"
            End If
            Session("table_id") = Nothing
            Response.Redirect("Table_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Table_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
End Class

Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Cash_Declaration
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclscshdecr As New Controller_clsExpense()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtdate.SelectedDate = DateTime.Now
                txtdate.MaxDate = DateTime.Now
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                'oclsBind.BindLocation(rdlocation, Val(Session("cmp_id")))
                'rdlocation.SelectedIndex = 0
                'oclsBind.BindMachine(ddlmachine, Val(Session("cmp_id")))
                oclscshdecr.cmp_id = Val(Session("cmp_id"))

                oclscshdecr.product_id = 0
                oclscshdecr.SingleRecord = 0
                Dim dtSize As DataTable = oclscshdecr.Select_SizeNPrice_By_Product()
                ViewState("View_Size") = dtSize
                BindGridCashGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            Dim RecordInsert As Integer = 1

            For Each item As GridDataItem In rdexpense.Items
                If Val(CType(item.FindControl("txtamount"), RadTextBox).Text) = 0 Then
                    RecordInsert = 0
                    Exit For
                End If
            Next

            If RecordInsert = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Fields with * can not be blank');", True)
            Else
                Insert_Stk_detail()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Cash_Declaration:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Session("stock_id") = Nothing
            Clear()
        Catch ex As Exception
            LogHelper.Error("Cash_Declration:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            'ddlexpense.SelectedIndex = 0
            txtdate.SelectedDate = DateTime.Now
            rdexpense.DataSource = String.Empty
            rdexpense.DataBind()

        Catch ex As Exception
            LogHelper.Error("Cash_Declration:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnaddnew_Click(sender As Object, e As EventArgs) Handles btnaddnew.Click
        Try
            rdexpense.Visible = True

            Dim RecordInsert As Integer = 1

            'For Each item As GridDataItem In rdexpense.Items
            '    If Val(CType(item.FindControl("txtcashexpenseamount"), RadTextBox).Text) = 0 Then
            '        RecordInsert = 0
            '        Exit For
            '    End If
            'Next

            If RecordInsert = 1 Then
                Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)

                oclscshdecr.cmp_id = Val(Session("cmp_id"))
                oclscshdecr.product_id = 0
                oclscshdecr.SingleRecord = 1
                Dim dtSize As DataTable = oclscshdecr.Select_SizeNPrice_By_Product()

                If dt1.Rows.Count > 0 Then
                    dtSize.Rows(0)("row_Id") = dt1.Rows.Count + 1
                End If
                dt1.Merge(dtSize)

                BindGridCashGrid()

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Fields with * can not be blank');", True)
            End If


        Catch ex As Exception
            LogHelper.Error("Cash_Declration:btnaddProduct_Click:" + ex.Message)
        End Try

    End Sub

    Protected Sub BindGridCashGrid()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView

            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdexpense.DataSource = dv
                rdexpense.DataBind()
            Else
                rdexpense.Enabled = True
                rdexpense.Visible = True
                rdexpense.DataSource = String.Empty
                rdexpense.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Cash_Declration:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub SaveRecordInDT()
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            For Each item As GridDataItem In rdexpense.Items
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdncashrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("Machine_id") = Val(CType(item.FindControl("ddlmachine"), RadComboBox).SelectedValue)
                        row("Amount") = (CType(item.FindControl("txtamount"), RadTextBox).Text).ToString()
                    End If
                Next
            Next
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:SaveRecordInDT:" + ex.Message)
        End Try
    End Sub

    'Protected Sub ddlexpense_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlexpense.SelectedIndexChanged
    '    BindGridCashGrid()
    'End Sub

    Protected Sub txtamount_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:txttotalStock_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub ddlmachine_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:txttotalStock_TextChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub Insert_Stk_detail()
        Try
            Dim dtSize As New DataTable()
            dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
            Dim Stk_Chg_rec_id As String

            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each row As DataRow In dt.Rows

                oclscshdecr.cmp_id = Val(Session("cmp_id"))
                oclscshdecr.Card_Amount = row("Amount")
                oclscshdecr.Cash_Amount = row("Amount")
                'oclscshdecr.product_id = 0
                oclscshdecr.Amount_Type = ddlamounttype.SelectedValue
                oclscshdecr.Machine_id = row("Machine_id")
                oclscshdecr.ip_address = Request.UserHostAddress
                oclscshdecr.login_id = Val(Session("login_id"))
                oclscshdecr.for_date = txtdate.SelectedDate
                oclscshdecr.Insert_Cash_Declaration()

            Next

            Session("Success") = "Record inserted successfully"
            Response.Redirect("Cash_Declaration_List.aspx", False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Some problem with save,Please try after some time.');", True)

        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:Insert_Stk_detail:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Cash_Declaration_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub rdproduct_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rdexpense.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Size_N_Price(id)
                BindGridCashGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:rdproduct_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub Delete_Size_N_Price(ByVal id As Integer)
        Try
            Dim dtsize As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            For Each row As DataRow In dtsize.Rows
                If row("row_id") = id Then
                    dtsize.Rows.Remove(row)
                    row.EndEdit()
                    dtsize.AcceptChanges()
                    Exit For
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)

        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:Delete_Size_N_Price:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdproduct_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdexpense.ItemDataBound
        Try

            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                oclsBind.BindLocation(CType(e.Item.FindControl("ddlmachine"), RadComboBox), Val(Session("cmp_id")))
                CType(e.Item.FindControl("ddlmachine"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdmachine"), HiddenField).Value

               
            End If
        Catch ex As Exception
            LogHelper.Error("Cash_Declaration:rdProduct_ItemDataBound:" + ex.Message)
        End Try
    End Sub

End Class


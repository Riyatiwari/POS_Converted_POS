Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Bank_Cash_Expense
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsbankcashexp As New Controller_clsExpense()
    Dim oclstock As New Controller_clsExpense_Detail()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("is_per") = "0"
                Dim baseDate As DateTime = DateTime.Now
                Dim today = baseDate
                Dim thisWeekStart = baseDate.AddDays(-CInt(baseDate.DayOfWeek))
                Dim thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1)

                txtdate.SelectedDate = thisWeekStart.AddDays(1)
                txtToDate.SelectedDate = thisWeekEnd
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                oclsBind.BindLocation(rdlocation, Val(Session("cmp_id")))
                rdlocation.SelectedIndex = 0
                'oclsbankcashexp.cmp_id = Val(Session("cmp_id"))
                'oclsbankcashexp.product_id = 0
                'oclsbankcashexp.SingleRecord = 0
                'Dim dtSize As DataTable = oclsbankcashexp.Select_SizeNPrice_By_Product()
                'ViewState("View_Size") = dtSize
                oclstock.SingleRecord = 0
                Dim dtSize As DataTable = oclstock.Select_Exepense_grid()
                ViewState("View_Size") = dtSize
                BindGrid()

                'bindSortingNo()

                'Dim e1 As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
                'ddlexpense_SelectedIndexChanged(ddlexpense, e1)

                'If (ddlexpense.SelectedValue = 1) Then
                '    BindGridBankGrid()
                'Else
                '    BindGridCashGrid()
                'End If
                BindGridCashGrid()

            End If
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub BindGrid()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdcashexpense.DataSource = dv
                rdcashexpense.DataBind()
            Else
                rdcashexpense.Enabled = True
                rdcashexpense.Visible = True
                rdcashexpense.DataSource = String.Empty
                rdcashexpense.DataBind()
                rdcashexpense.MasterTableView.GetColumn("Total_Amount").Visible = False
            End If

        Catch ex As Exception
            LogHelper.Error("Expense_Detail:BindGrid:" + ex.Message)
        End Try
    End Sub

    'Private Sub bindSortingNo()
    '    Try
    '        oclsbankcashexp.cmp_id = Val(Session("cmp_id"))
    '        oclsbankcashexp.form_name = "Stock Changes"
    '        Dim dtTable As DataTable = oclsbankcashexp.Select1()
    '        If dtTable.Rows.Count > 0 Then
    '            'Dim rec As String = "CHG00000000"
    '            Dim dt As String = (dtTable.Rows(0)("receipt_number"))
    '            'txtreceipt.Text = dt

    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:bindSortingNo" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)

            For Each row As DataRow In dt.Rows

                oclstock.Location_id = rdlocation.SelectedValue
                oclstock.Expense_id = Val(row("Expense"))
                oclstock.ip_address = Request.UserHostAddress
                oclstock.From_Date = txtdate.SelectedDate
                If chkClear.Checked = True Then
                    oclstock.PerDay_amount = Val(row("Total_Amount"))
                Else
                    oclstock.PerDay_amount = Val(row("PerDay"))
                End If
                'oclstock.PerDay_amount = Val(row("PerDay"))
                oclstock.PerDayTotal_amount = Val(row("PerDayTotal"))
                oclstock.PerWeek_amount = Val(row("PerWeekTotal"))
                oclstock.Description = (row("Description"))
                If chkClear.Checked = True Then
                    oclstock.isFix = 1
                Else
                    oclstock.isFix = 0
                End If
                If chkCash.Checked = True Then
                    oclstock.isCash = 1
                Else
                    oclstock.isCash = 0
                End If
                If Session("Expense_Detail_id") = Nothing Then
                    oclstock.Expense_Detail_id = 0
                    oclstock.Insert()
                    Response.Redirect("Bank_Cash_Expense_List.aspx", False)
                    Session("Success") = "Record inserted successfully"
                Else
                    oclstock.Expense_Detail_id = Val(Session("Expense_Detail_id"))
                    oclstock.Update()
                    Response.Redirect("Bank_Cash_Expense_List.aspx", False)
                    Session("Success") = "Record updated successfully"
                End If
            Next
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Bank_Cash_Expense:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Session("stock_id") = Nothing
            Clear()
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:btnReset_Click" + ex.Message)
        End Try
    End Sub

    Private Sub Clear()
        Try
            'ddlexpense.SelectedIndex = 0
            txtdate.SelectedDate = DateTime.Now
            rdcashexpense.DataSource = String.Empty
            rdcashexpense.DataBind()

        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:Clear" + ex.Message)
        End Try
    End Sub

    Protected Sub btnaddProduct_Click(sender As Object, e As EventArgs) Handles btnaddProduct.Click
        Try
            rdcashexpense.Visible = True

            Dim RecordInsert As Integer = 1

            For Each item As GridDataItem In rdcashexpense.Items
                If Val(CType(item.FindControl("ddlExpense"), RadComboBox).SelectedValue) = 0 Then
                    RecordInsert = 0
                    Exit For
                End If
            Next

            If RecordInsert = 1 Then
                Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)
                oclstock.SingleRecord = 1
                Dim dtSize As DataTable = oclstock.Select_Exepense_grid()

                If dt1.Rows.Count > 0 Then
                    dtSize.Rows(0)("row_Id") = dt1.Rows.Count + 1
                End If

                dt1.Merge(dtSize)

                BindGrid()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Fields with * can not be blank');", True)
            End If


        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:btnaddProduct_Click:" + ex.Message)
        End Try

    End Sub

    'Protected Sub rdproduct_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdproduct.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

    '            oclsBind.BindProductGroup(CType(e.Item.FindControl("ddlproductgroup"), RadComboBox), Val(Session("cmp_id")))
    '            oclsBind.BindUnit(CType(e.Item.FindControl("ddltotalunit"), RadComboBox), Val(Session("cmp_id")))
    '            oclsBind.BindProduct(CType(e.Item.FindControl("ddlproduct"), RadComboBox), Val(Session("cmp_id")))
    '            oclsBind.BindTax(CType(e.Item.FindControl("ddltax"), RadComboBox), Val(Session("cmp_id")))

    '            CType(e.Item.FindControl("ddlproduct"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdproduct"), HiddenField).Value
    '            CType(e.Item.FindControl("ddlproductgroup"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdproductgroup"), HiddenField).Value
    '            'CType(e.Item.FindControl("ddlstockdetails"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdstkdetail"), HiddenField).Value
    '            CType(e.Item.FindControl("ddltotalunit"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdunit"), HiddenField).Value
    '            If CType(e.Item.FindControl("hdnactive"), HiddenField).Value = 0 Then
    '                CType(e.Item.FindControl("chkisdamage"), CheckBox).Checked = False
    '            Else
    '                CType(e.Item.FindControl("chkisdamage"), CheckBox).Checked = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Bank_Cash_Expense:rdProduct_ItemDataBound:" + ex.Message)
    '    End Try
    'End Sub
    Protected Sub BindGridCashGrid()
        Try
            Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            Dim dv As DataView = dt.DefaultView

            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdcashexpense.DataSource = dv
                rdcashexpense.DataBind()

            Else

                rdcashexpense.Enabled = True
                rdcashexpense.Visible = True
                rdcashexpense.DataSource = String.Empty
                rdcashexpense.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub SaveRecordInDT()
        Try
            Dim dt1 As DataTable = DirectCast(ViewState("View_Size"), DataTable)
            For Each item As GridDataItem In rdcashexpense.Items
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                        row("Expense") = Val(CType(item.FindControl("ddlExpense"), RadComboBox).SelectedValue)
                        row("PerDay") = Val(CType(item.FindControl("txtPerDay"), RadTextBox).Text)
                        row("PerDayTotal") = Val(CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text)
                        row("PerWeekTotal") = Val(CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text)
                        row("Total_Amount") = Val(CType(item.FindControl("txtTotalamount"), RadTextBox).Text)
                        row("Description") = (CType(item.FindControl("txtDescription"), RadTextBox).Text)
                    End If
                Next
            Next
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:SaveRecordInDT:" + ex.Message)
        End Try
    End Sub

    'Protected Sub ddlexpense_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlexpense.SelectedIndexChanged

    '    BindGridCashGrid()
    'End Sub

    Protected Sub ddlExpense_SelectedIndexChanged(sender As Object, e As EventArgs)
        For Each item As GridDataItem In rdcashexpense.Items
            If CType(item.FindControl("ddlExpense"), RadComboBox).SelectedItem.Text = "Other" Then
                CType(item.FindControl("txtDescription"), RadTextBox).Visible = True
                CType(item.FindControl("lblPercentageDisp"), Label).Visible = False
            Else
                CType(item.FindControl("lblPercentageDisp"), Label).Visible = False
                CType(item.FindControl("txtDescription"), RadTextBox).Visible = False
                oclstock.Expense_id = CType(item.FindControl("ddlExpense"), RadComboBox).SelectedValue
                Dim dt As DataTable = oclstock.SelectExpense()
                If (dt.Rows.Count > 0) Then
                    If dt.Rows(0)("is_per").ToString() = "1" Then
                        Session("is_per") = "1"
                        CType(item.FindControl("lblPercentageDisp"), Label).Visible = True
                    Else
                        Session("is_per") = "0"
                        CType(item.FindControl("lblPercentageDisp"), Label).Visible = False
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub txtPerDay_TextChanged(sender As Object, e As EventArgs)

        Dim enddate As DateTime = Convert.ToDateTime(txtToDate.SelectedDate)
        Dim startdate As DateTime = Convert.ToDateTime(txtdate.SelectedDate)

        If Session("is_per") = "0" Then


            Dim count As Integer = (enddate - startdate).TotalDays
            If chkClear.Checked = False Then
                For Each item As GridDataItem In rdcashexpense.Items
                    CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerDay"), RadTextBox).Text) * 7) * 4.33
                    CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerDay"), RadTextBox).Text)) * 7
                Next
            Else
                For Each item As GridDataItem In rdcashexpense.Items
                    CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerDay"), RadTextBox).Text) * 7) * 4.33
                    CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerDay"), RadTextBox).Text)) * 7
                Next
            End If

        End If
        SaveRecordInDT()

    End Sub
    Protected Sub txtPerDayTotal_TextChanged(sender As Object, e As EventArgs)
        Dim enddate As DateTime = Convert.ToDateTime(txtToDate.SelectedDate)
        Dim startdate As DateTime = Convert.ToDateTime(txtdate.SelectedDate)

        If Session("is_per") = "0" Then

            Dim count As Integer = (enddate - startdate).TotalDays

            If chkClear.Checked = False Then
                For Each item As GridDataItem In rdcashexpense.Items

                    CType(item.FindControl("txtPerDay"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text) / 4.33) / 7
                    CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text) / 4.33)

                Next
            Else
                For Each item As GridDataItem In rdcashexpense.Items

                    CType(item.FindControl("txtPerDay"), RadTextBox).Text = Val(CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text)
                    CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text)) * 7

                Next
            End If
        End If
        SaveRecordInDT()
    End Sub

    Protected Sub txtPerWeekTotal_TextChanged(sender As Object, e As EventArgs)
        Dim enddate As DateTime = Convert.ToDateTime(txtToDate.SelectedDate)
        Dim startdate As DateTime = Convert.ToDateTime(txtdate.SelectedDate)

        If Session("is_per") = "0" Then


            Dim count As Integer = (enddate - startdate).TotalDays
            If chkClear.Checked = False Then
                For Each item As GridDataItem In rdcashexpense.Items
                    CType(item.FindControl("txtPerDay"), RadTextBox).Text = Val(CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text) / Val(7)
                    CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text)) * 4.33
                Next
            Else
                For Each item As GridDataItem In rdcashexpense.Items
                    CType(item.FindControl("txtPerDay"), RadTextBox).Text = Val(CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text) / Val(7)
                    CType(item.FindControl("txtPerDayTotal"), RadTextBox).Text = (Val(CType(item.FindControl("txtPerWeekTotal"), RadTextBox).Text) * 4.33)
                Next
            End If
        End If
        SaveRecordInDT()

    End Sub
    Protected Sub txtcashdescription_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:txttotalStock_TextChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtcashexpenseamount_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:txttotalStock_TextChanged:" + ex.Message)
        End Try
    End Sub

    'Protected Sub Insert_Stk_detail()
    '    'Try
    '    Dim dtSize As New DataTable()
    '    dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
    '    Dim Stk_Chg_rec_id As String

    '    Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)

    '    For Each row As DataRow In dt.Rows

    '        oclsbankcashexp.cmp_id = Val(Session("cmp_id"))
    '        oclsbankcashexp.Cash_Expense_Dcptn = row("cash_description")
    '        oclsbankcashexp.Cash_Expense_Amount = row("cash_expense_amount")
    '        'oclsbankcashexp.product_id = 0
    '        oclsbankcashexp.Float_Value = row("cash_expense_amount")
    '        oclsbankcashexp.Banking = row("cash_expense_amount")
    '        oclsbankcashexp.ip_address = Request.UserHostAddress
    '        oclsbankcashexp.login_id = Val(Session("login_id"))
    '        oclsbankcashexp.expense_type = ddlexpense.SelectedValue
    '        oclsbankcashexp.for_date = txtdate.SelectedDate

    '        'oclsbankcashexp.stk_det_id = 0
    '        oclsbankcashexp.Insert_Cash_Expense()
    '        Response.Redirect("Bank_Cash_Expense_List.aspx", False)
    '        Session("Success") = "Record inserted successfully"

    '    Next

    '    'Catch ex As Exception
    '    '    LogHelper.Error("Bank_Cash_Expense:Insert_Stk_detail:" + ex.Message)
    '    'End Try
    'End Sub


    Protected Sub Insert_Daily_Report()
        'Try
        'Dim dtSize As New DataTable()
        'dtSize.Columns.AddRange(New DataColumn(1) {New DataColumn("stock_rec_id"), New DataColumn("stock_red_id")})
        'Dim Stk_Chg_rec_id As String

        'Dim dt As DataTable = DirectCast(ViewState("View_Size"), DataTable)

        'For Each row As DataRow In dt.Rows

        oclsbankcashexp.cmp_id = Val(Session("cmp_id"))
        'oclsbankcashexp.Cash_Expense_Dcptn = row("cash_description")
        'oclsbankcashexp.Cash_Expense_Amount = row("cash_expense_amount")
        'oclsbankcashexp.product_id = 0

        oclsbankcashexp.ip_address = Request.UserHostAddress
        oclsbankcashexp.login_id = Val(Session("login_id"))
        'oclsbankcashexp.expense_type = ddlexpense.SelectedValue
        oclsbankcashexp.for_date = txtdate.SelectedDate

        'oclsbankcashexp.stk_det_id = 0
        oclsbankcashexp.Insert_Daily_Report()
        'Response.Redirect("Bank_Cash_Expense_List.aspx", False)
        Session("Success1") = "Record inserted successfully"

        'Next

        'Catch ex As Exception
        '    LogHelper.Error("Bank_Cash_Expense:Insert_Stk_detail:" + ex.Message)
        'End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Bank_Cash_Expense_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:btnCancel_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub rdcashexpense_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rdcashexpense.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                Delete_Expense_Grid(id)
                BindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Bank_Cash_Expense:rdcashexpense_ItemCommand:" + ex.Message)
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
            LogHelper.Error("Bank_Cash_Expense:Delete_Size_N_Price:" + ex.Message)
        End Try
    End Sub


    Protected Sub BtnSaveRecords_Click(sender As Object, e As EventArgs) Handles BtnSaveRecords.Click
        Try

            Dim RecordInsert As Integer = 1
            'If rdcashexpense.Visible = True Then
            'For Each item As GridDataItem In rdcashexpense.Items
            '    If Val(CType(item.FindControl("txtcashexpenseamount"), RadTextBox).Text) = 0 Then
            '        RecordInsert = 0
            '        Exit For
            '    End If
            'Next

            'If RecordInsert = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test2", "alert('Fields with * can not be blank');", True)
            'Else
            '    Insert_Stk_detail()
            'End If
            Insert_Daily_Report()

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Bank_Cash_Expense:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Private Sub rdcashexpense_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rdcashexpense.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                oclsBind.BindExpense(CType(e.Item.FindControl("ddlExpense"), RadComboBox))

                CType(e.Item.FindControl("ddlExpense"), RadComboBox).SelectedValue = CType(e.Item.FindControl("hdExpense"), HiddenField).Value

                If chkClear.Checked = True Then
                    rdcashexpense.MasterTableView.GetColumn("Per_Day").Visible = False
                    rdcashexpense.MasterTableView.GetColumn("Per_Week").Visible = False
                    rdcashexpense.MasterTableView.GetColumn("Per_Month").Visible = False
                    rdcashexpense.MasterTableView.GetColumn("Total_Amount").Visible = True
                Else
                    rdcashexpense.MasterTableView.GetColumn("Total_Amount").Visible = False
                    rdcashexpense.MasterTableView.GetColumn("Per_Day").Visible = True
                    rdcashexpense.MasterTableView.GetColumn("Per_Week").Visible = True
                    rdcashexpense.MasterTableView.GetColumn("Per_Month").Visible = True
                End If
                'rdcashexpense.MasterTableView.GetColumn("Quantity").Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Expense_Detail:rdProduct_ItemDataBound:" + ex.Message)
        End Try
    End Sub

    Protected Sub Delete_Expense_Grid(ByVal id As Integer)
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
            LogHelper.Error("Expense_Detail:Delete_Size_N_Price:" + ex.Message)
        End Try
    End Sub

    Private Sub chkClear_CheckedChanged(sender As Object, e As EventArgs) Handles chkClear.CheckedChanged
        If chkClear.Checked = True Then
            txtToDate.Visible = False
            divCash.Visible = True
        Else
            txtToDate.Visible = True
            divCash.Visible = False
        End If
        BindGrid()
    End Sub
    Protected Sub txtTotalamount_TextChanged(sender As Object, e As EventArgs)
        Try
            SaveRecordInDT()
        Catch ex As Exception

        End Try
    End Sub
End Class


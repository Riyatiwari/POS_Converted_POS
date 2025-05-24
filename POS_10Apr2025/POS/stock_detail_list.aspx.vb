Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting
Partial Class stock_detail_list
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsStock As New Controller_clsStock

    Public Sub bindGrid()
        Try
            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtForDate.SelectedDate.ToString() = "", txtForDate.SelectedDate = DateTime.Now, txtForDate.SelectedDate)
            ToDate = IIf(txtToDate.SelectedDate.ToString() = "", txtToDate.SelectedDate = DateTime.Now, txtToDate.SelectedDate)

            oclsStock.cmp_id = Val(Session("cmp_id"))
            oclsStock.FromDate = FromDate
            oclsStock.ToDate = ToDate
            oclsStock.duration = IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "This Week")
            Dim dt As DataTable = oclsStock.SelecStockDetail()

            If dt.Rows.Count > 0 Then
                rdStock.DataSource = dt
                rdStock.DataBind()
            Else
                rdStock.DataSource = String.Empty
                rdStock.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stock_Detail_List:bindGrid" + ex.Message)
        End Try

    End Sub
    Public Sub rebindGrid()
        Try
            oclsStock.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsStock.SelecStockDetail()

            If dt.Rows.Count > 0 Then
                rdStock.DataSource = dt
            Else
                rdStock.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stock_Detail_List:rebindGrid" + ex.Message)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                txtForDate.SelectedDate = System.DateTime.Now
                txtToDate.SelectedDate = System.DateTime.Now
                If Request.QueryString("pid") = Nothing Then
                    If Not Session("success") = Nothing Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                        Session("success") = Nothing
                    End If
                    If Val(Session("staff_role_id")) <> 0 Then
                        RoleCheck()
                    ElseIf Val(Session("staff_role_id")) = 0 Then
                        ViewState("view") = 1
                        ViewState("add") = 1
                        ViewState("edit") = 1
                        ViewState("delete") = 1
                    Else
                        ViewState("view") = 0
                        ViewState("edit") = 0
                        ViewState("add") = 0
                        ViewState("delete") = 0
                    End If
                    If Val(ViewState("view")) = 1 Or Val(ViewState("edit")) = 1 Or Val(ViewState("delete")) = 1 Then
                        divDevice.Visible = True
                    Else
                        divDevice.Visible = False
                    End If
                    'If Val(ViewState("add")) = 1 Then
                    '    lnkNew.Visible = True
                    'Else
                    '    lnkNew.Visible = False
                    'End If
                    bindGrid()
                Else
                    Dim pid As Int32 = Int32.Parse(Request.QueryString("pid").ToString())
                    Dim pgid As Int32 = Int32.Parse(Request.QueryString("pgid").ToString())
                    Dim lid As Int32 = Int32.Parse(Request.QueryString("L_ID").ToString())
                    Dim S_Date As DateTime = DateTime.Parse(Request.QueryString("Date").ToString())
                    Dim E_Date As DateTime = DateTime.Parse(Request.QueryString("E_Date").ToString())
                    Dim name As String = Request.QueryString("name").ToString()
                    'radDuration.SelectedValue = "Custom"
                    'txtForDate.SelectedDate = S_Date
                    'txtToDate.SelectedDate = E_Date
                    hdsearchvalueAfterEdit.Value = name

                    bindGrid()

                    For Each item As RepeaterItem In rdStock.Items

                        Dim is_finalsubmit As HiddenField = CType(item.FindControl("hf_isfinalsubmit"), HiddenField)
                        Dim hf_stk_chg_rec_id As HiddenField = CType(item.FindControl("hf_stk_chg_rec_id"), HiddenField)
                        Dim rptOrders As Repeater = TryCast(item.FindControl("rptOrders"), Repeater)

                        Dim pnlOrders As Panel = item.FindControl("pnlOrders")

                        oclsStock.cmp_id = Val(Session("cmp_id"))
                        oclsStock.Stk_Chg_rec_id = hf_stk_chg_rec_id.Value
                        pnlOrders.Visible = True
                        Dim dt As DataTable = oclsStock.Selectdetailrec()
                        If dt.Rows.Count > 0 Then
                            rptOrders.DataSource = dt
                            rptOrders.DataBind()
                        Else
                            rptOrders.DataSource = String.Empty
                            rptOrders.DataBind()
                        End If



                    Next
                End If

            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Detail_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Stock Changes"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If
            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If

            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception

            LogHelper.Error("Stock_Detail_List:RoleCheck" + ex.Message)
        End Try
    End Sub

    'Protected Sub rdStock_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rdStock.ItemCreated
    '    Try
    '        If TypeOf e.Item Is GridFilteringItem Then
    '            For Each nColumn As GridColumn In rdStock.MasterTableView.Columns
    '                If TypeOf nColumn Is GridBoundColumn Then
    '                    Dim nBColumn As GridBoundColumn = DirectCast(nColumn, GridBoundColumn)
    '                    Dim filerItem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
    '                    Dim textItem As TextBox = DirectCast(filerItem(nColumn.UniqueName).Controls(0), TextBox)
    '                    textItem.TabIndex = 1
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Detail_List:rdStock_ItemCreated" + ex.Message)
    '    End Try

    'End Sub


    'Protected Sub rdStock_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdStock.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Detail_List:rdDevice_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub rdStock_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                'Session("stock_id") = Convert.ToInt32(e.CommandArgument)
                'Response.Redirect("Stock_Management_Master.aspx", False)

                Session("Stk_Chg_rec_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Stock_Detail.aspx", False)

                'ElseIf e.CommandName = "view" Then

                '    oclsStock.cmp_id = Val(Session("cmp_id"))
                '    oclsStock.Stk_Chg_rec_id = Convert.ToInt32(e.CommandArgument)
                '    Session("id") = Convert.ToInt32(e.CommandArgument)

                '    Dim dt As DataTable = oclsStock.Selectdetailrec()

                '    If dt.Rows.Count > 0 Then
                '        rdStock.DataSource = dt
                '        rdStock.DataBind()
                '    Else
                '        rdStock.DataSource = String.Empty
                '        rdStock.DataBind()
                '    End If

            ElseIf e.CommandName = "Delete" Then
                deletestock(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Detail_List:rdStock_ItemCommand" + ex.Message)
        End Try
    End Sub

    'Protected Sub RadGrid1_DetailTableDataBind(source As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs)
    '    Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
    '    'Select Case e.DetailTableView.Name
    '    '    Case "stock"
    '    '        If True Then
    '    oclsStock.cmp_id = Val(Session("cmp_id"))
    '    oclsStock.Stk_Chg_rec_id = dataItem.GetDataKeyValue("stk_chg_rec_id").ToString()

    '    Dim dt As DataTable = oclsStock.Selectdetailrec()
    '    If dt.Rows.Count > 0 Then
    '        e.DetailTableView.DataSource = oclsStock.Selectdetailrec()
    '    Else
    '        e.DetailTableView.DataSource = Nothing
    '    End If

    '    'If dt.Rows.Count > 0 Then
    '    '    rdStock.DataSource = dt
    '    '    rdStock.DataBind()
    '    'Else
    '    '    rdStock.DataSource = String.Empty
    '    '    rdStock.DataBind()
    '    '        End If
    '    'Exit Select
    '    'End If

    '    'End Select
    'End Sub

    Protected Sub deletestock(ByVal id As Integer)
        Try
            oclsStock.Stk_Chg_rec_id = Val(id)
            oclsStock.stock_receive_date = DateTime.Now
            oclsStock.Location_id = 0
            oclsStock.login_id = Val(Session("login_id"))
            oclsStock.cmp_id = Val(Session("cmp_id"))
            oclsStock.ip_address = Request.UserHostAddress
            oclsStock.receipt_number = ""
            oclsStock.stock_detail = 0
            oclsStock.Del_ChgRec()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Stock_Detail_List:deletestock" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Stk_Chg_rec_id") = Nothing

            Dim dt As DataTable = oclsStock.check_pending_stock()
            If dt.Rows.Count = 0 Then
                Response.Redirect("Stock_Detail.aspx", False)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('You have Pending Stock Take in list.');", True)
            End If

        Catch ex As Exception
            LogHelper.Error("Stock_Detail_List:lnkNew_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnaddProduct_Click(sender As Object, e As EventArgs)
        Try
            'If rdchild.Visible = True Then
            '    rdchild.Visible = False
            'Else
            '    rdchild.Visible = True
            'End If

        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:btnAddNewSize_Click:" + ex.Message)
        End Try

    End Sub


    Protected Sub rdStock_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                ' Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                Dim is_finalsubmit As HiddenField = CType(e.Item.FindControl("hf_isfinalsubmit"), HiddenField)
                Dim hf_stk_chg_rec_id As HiddenField = CType(e.Item.FindControl("hf_stk_chg_rec_id"), HiddenField)
                Dim rptOrders As Repeater = TryCast(e.Item.FindControl("rptOrders"), Repeater)

                oclsStock.cmp_id = Val(Session("cmp_id"))
                oclsStock.Stk_Chg_rec_id = hf_stk_chg_rec_id.Value

                Dim dt As DataTable = oclsStock.Selectdetailrec()
                If dt.Rows.Count > 0 Then
                    rptOrders.DataSource = dt
                    rptOrders.DataBind()
                Else
                    rptOrders.DataSource = String.Empty
                    rptOrders.DataBind()
                End If



                'If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
                '    rdStock.MasterTableView.GetColumn("TemplateColumn").Visible = False
                'End If

                If Val(ViewState("edit")) = 1 Then
                    If is_finalsubmit.Value = 0 Then
                        CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
                    Else
                        CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
                    End If
                    'CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
                End If
                If Val(ViewState("delete")) = 1 Then
                    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Detail_List:rdStock_ItemDataBound" + ex.Message)
        End Try
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If

            bindGrid()
        Catch ex As Exception
            LogHelper.Error("Stock_Detail_List:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtForDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        Try

            txtToDate.SelectedDate = txtForDate.SelectedDate

        Catch ex As Exception
            LogHelper.Error("Stock_Detail_List:txtForDate_SelectedDateChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Try

            bindGrid()

        Catch ex As Exception
            LogHelper.Error("Stock_Detail_List:lnkView_Click:" + ex.Message)
        End Try
    End Sub
End Class

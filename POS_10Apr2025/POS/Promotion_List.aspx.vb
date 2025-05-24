Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports Telerik.Web.UI
Partial Class Promotion_List
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsBind As New clsBinding
    Dim oclsPromotion As New Controller_clsPromotion()
    Public oSessionManager As New SessionManager
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Promotion_List"

    Public Sub bindGrid()
        Try
            'oclsPromotion.table_promotion_id = 0
            oclsPromotion.Promo_name = ""
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsPromotion.SelectAll()

            oSessionManager.ResetSessionDT(Page_Name.ToString())

            If dt.Rows.Count > 0 Then
                rdProduct.DataSource = dt
                rdProduct.DataBind()
            Else
                rdProduct.DataSource = String.Empty
                rdProduct.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_List:bindGrid:" + ex.Message)
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                ElseIf Not Session("Update") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("Update").ToString() + "');", True)
                    Session("Update") = Nothing
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
                    ViewState("add") = 0
                    ViewState("edit") = 0
                    ViewState("delete") = 0
                End If
                If Val(ViewState("view")) = 1 Or Val(ViewState("delete")) = 1 Or Val(ViewState("edit")) = 1 Then
                    divPGroup.Visible = True
                Else
                    divPGroup.Visible = False
                End If
                If Val(ViewState("add")) = 1 Then
                    lnkNew.Visible = True
                Else
                    lnkNew.Visible = False
                End If
                bindGrid()
            End If

        Catch ex As Exception
            LogHelper.Error("Promotion_List:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Promotion"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_Edit = 1 Then
                ViewState("edit") = 1
            Else
                ViewState("edit") = 0
            End If
            If oclsRole.is_add = 1 Then
                ViewState("add") = 1
            Else
                ViewState("add") = 0
            End If

            If oclsRole.is_Delete = 1 Then
                ViewState("delete") = 1
            Else
                ViewState("delete") = 0
            End If

        Catch ex As Exception
            LogHelper.Error("Promotion_List:RoleCheck:" + ex.Message)
        End Try
    End Sub

    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("table_promotion_id") = Nothing
            Response.Redirect("Promotion.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Promotion_List:lnkNew_Click:" + ex.Message)
        End Try
    End Sub


    Protected Sub rdProduct_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            If e.CommandName = "Edit" Then
                'Session("table_promotion_id") = Convert.ToInt32(e.CommandArgument)
                Session("table_promotion_id") = e.CommandArgument
                Response.Redirect("Promotion.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                'deletePromotion(Convert.ToInt32(e.CommandArgument))
                deletePromotion(e.CommandArgument)
                bindGrid()

            End If
        Catch ex As Exception
            LogHelper.Error("Product_List:rdProduct_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub deletePromotion(ByVal id As String)
        Try
            oclsPromotion.table_promotion_id = 0
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            oclsPromotion.product_id = 0
            oclsPromotion.product_id1 = 0
            oclsPromotion.product_id2 = 0
            oclsPromotion.product_id3 = 0
            oclsPromotion.size_id = 0
            oclsPromotion.size_id1 = 0
            oclsPromotion.size_id2 = 0
            oclsPromotion.size_id3 = 0
            oclsPromotion.price_id = 0
            oclsPromotion.Discount_type = 0
            oclsPromotion.Discount = 0
            oclsPromotion.Recurrence = 0
            oclsPromotion.Month_flag = 0
            oclsPromotion.Months = ""
            oclsPromotion.Week_flag = 0
            oclsPromotion.Weeks = ""
            oclsPromotion.Days = ""
            oclsPromotion.on_day = ""
            oclsPromotion.Recurrence_flag = 0
            oclsPromotion.start_time = ""
            oclsPromotion.end_time = ""
            oclsPromotion.start_time1 = ""
            oclsPromotion.end_time1 = ""
            oclsPromotion.start_time2 = ""
            oclsPromotion.end_time2 = ""
            oclsPromotion.start_time3 = ""
            oclsPromotion.end_time3 = ""
            oclsPromotion.is_endless = 0
            oclsPromotion.Promo_name = id
            oclsPromotion.startDate = DateTime.Now
            oclsPromotion.EndDate = DateTime.Now
            oclsPromotion.combo_flag = 0
            oclsPromotion.combo_product_id = ""
            oclsPromotion.location_id = 0
            oclsPromotion.machine_id = 0
            oclsPromotion.venue_id = 0
            oclsPromotion.Is_active = 0
            oclsPromotion.Ip_address = ""
            oclsPromotion.login_id = 0
            oclsPromotion.product_group = 0
            oclsPromotion.product_group1 = 0
            oclsPromotion.product_group2 = 0
            oclsPromotion.product_group3 = 0
            oclsPromotion.duration_flag = 0
            oclsPromotion.no_of_product = 0
            oclsPromotion.Voucher_Type = ""
            oclsPromotion.Voucher_Code = ""
            oclsPromotion.Delete()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully');", True)


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Product_List:deleteProduct:" + ex.Message)
        End Try
    End Sub


    'Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
    '    Try

    '        rdProduct.Rebind()

    '        If Not IsPostBack Then
    '            Dim strVal As String = ""
    '            If HttpContext.Current.Session(DefStr + Page_Name) Is Nothing Then
    '                For Each column As GridColumn In rdProduct.MasterTableView.Columns
    '                    strVal += column.UniqueName + "#"
    '                Next
    '                oSessionManager.CreateSessionDT(Page_Name.ToString, strVal.ToString)
    '            Else
    '                Dim FilterExpression As String = ""
    '                For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
    '                    If row("FIN").ToString() <> "" And row("VAL").ToString() <> "" Then
    '                        Dim arr As Array = row("VAL").ToString().Split("#")
    '                        If Not [String].IsNullOrEmpty(FilterExpression) Then
    '                            FilterExpression += " AND "
    '                        End If
    '                        If arr.Length - 1 = 0 Then
    '                            Dim a As Integer = 0
    '                            Try
    '                                DateTime.Parse(row("VAL"))
    '                            Catch ex As Exception
    '                                a = 1
    '                            End Try

    '                            If a = 1 Then
    '                                FilterExpression += "([" + row("FIN").ToString() + "] Like '%" + row("VAL").ToString() + "%')"
    '                            Else
    '                                FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL") + "' )"
    '                            End If
    '                        Else
    '                            If row("VAL").ToString.Contains("01/01/1900") Then
    '                                FilterExpression += "( [" + row("FIN").ToString() + "] = '" + row("VAL").ToString().Replace("01/01/1900", "").Replace("#", "") + "' )"
    '                            Else
    '                                FilterExpression += "(([" + row("FIN").ToString() + "] >= '" + arr(0) + "') AND ([" + row("FIN").ToString() + "] <= '" + arr(1) + "'))"
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '                If FilterExpression = "" Then
    '                    rdProduct.MasterTableView.FilterExpression = String.Empty
    '                    rdProduct.MasterTableView.Rebind()
    '                Else
    '                    rdProduct.MasterTableView.FilterExpression = FilterExpression
    '                    rdProduct.MasterTableView.CurrentResetPageIndexAction = GridResetPageIndexAction.SetPageIndexToFirst
    '                    rdProduct.MasterTableView.Rebind()
    '                End If

    '            End If

    '        End If

    '        GridFilterBind()
    '    Catch ex As Exception
    '        LogHelper.Error("Employee_list:Page_LoadComplete:" + ex.Message)
    '    End Try
    'End Sub
    'Protected Sub GridFilterBind()
    '    Try
    '        For Each item As GridFilteringItem In rdProduct.MasterTableView.GetItems(GridItemType.FilteringItem)

    '            For Each row As DataRow In HttpContext.Current.Session(DefStr + Page_Name).Rows
    '                If row("FIN").ToString() <> "" And row("val").ToString() <> "" Then
    '                    Dim val As Integer = 0
    '                    Try
    '                        TryCast(item(row("FIN").ToString()).Controls(0), TextBox).Text = row("VAL").ToString
    '                    Catch ex As Exception
    '                        val = 1
    '                    End Try
    '                    If val = 1 Then
    '                        Try
    '                            If row("VAL") = "" Then
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = "ALL"
    '                            Else
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).SelectedValue = row("VAL").ToString
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadComboBox).Text = row("VAL").ToString
    '                            End If
    '                        Catch ex As Exception
    '                            val = 2
    '                        End Try
    '                    End If
    '                    If val = 2 Then
    '                        Try
    '                            If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing And Not TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker) Is Nothing Then
    '                                If row("val").ToString().Contains("01/01/1900") Then
    '                                    Dim arr As Array = row("val").ToString().Split("#")
    '                                    If arr(0).ToString.Contains("01/01/1900") = False Then
    '                                        TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
    '                                    End If
    '                                    If arr(1).ToString.Contains("01/01/1900") = False Then
    '                                        TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
    '                                    End If
    '                                ElseIf row("val").ToString().Contains("#") Then
    '                                    Dim arr As Array = row("val").ToString().Split("#")
    '                                    TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = arr(0).ToString
    '                                    TryCast(item(row("FIN").ToString()).Controls(3), RadDatePicker).SelectedDate = arr(1).ToString
    '                                End If
    '                            End If
    '                        Catch ex As Exception
    '                            val = 3
    '                        End Try
    '                    End If
    '                    If val = 3 Then
    '                        Try
    '                            If Not TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker) Is Nothing Then
    '                                TryCast(item(row("FIN").ToString()).Controls(1), RadDatePicker).SelectedDate = row("VAL").ToString
    '                            End If
    '                        Catch ex As Exception

    '                        End Try
    '                    End If
    '                End If
    '            Next
    '            'TryCast(item("Branch_Name").Controls(0), TextBox).Text = Session("flt_Branch_Code")
    '        Next
    '    Catch ex As Exception
    '        LogHelper.Error("Employee_list:GridFilterBind:" + ex.Message)
    '    End Try
    'End Sub

    'Public Sub ReBindGrid()
    '    Try
    '        'oclsPromotion.table_promotion_id = 0
    '        oclsPromotion.Promo_name = ""
    '        oclsPromotion.cmp_id = Val(Session("cmp_id"))
    '        Dim dt As DataTable = oclsPromotion.SelectAll()


    '        If dt.Rows.Count > 0 Then
    '            rdProduct.DataSource = dt
    '        Else
    '            rdProduct.DataSource = String.Empty
    '        End If

    '    Catch ex As Exception
    '        LogHelper.Error("Product_List:ReBindGrid():" + ex.Message)
    '    End Try
    'End Sub

    Private Sub lnkVoucher_Click(sender As Object, e As EventArgs) Handles lnkVoucher.Click
        Try
            Response.Redirect("Issue_Voucher_List.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

End Class

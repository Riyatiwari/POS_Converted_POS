Imports System.Data
Imports System.Drawing.Printing
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports Telerik.Web.UI

Partial Class Stock_Management_List
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
            oclsStock.FromDate = FromDate  'Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt")
            oclsStock.ToDate = ToDate  'Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt")
            oclsStock.duration = IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue.ToString, "This Week")
            Dim dt As DataTable = oclsStock.Select_dateWise()

            If dt.Rows.Count > 0 Then
                rdStock.DataSource = dt
                rdStock.DataBind()
            Else
                rdStock.DataSource = String.Empty
                rdStock.DataBind()
            End If


            'oclsStock.cmp_id = Val(Session("cmp_id"))
            'Dim dt As DataTable = oclsStock.SelectAll()

            'If dt.Rows.Count > 0 Then
            '    rdStock.DataSource = dt
            '    rdStock.DataBind()
            'Else
            '    rdStock.DataSource = String.Empty
            '    rdStock.DataBind()
            'End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stock_Management_List:bindGrid" + ex.Message)
        End Try

    End Sub

    Public Sub rebindGrid()
        Try
            oclsStock.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsStock.SelectAll()

            If dt.Rows.Count > 0 Then
                rdStock.DataSource = dt
            Else
                rdStock.DataSource = String.Empty
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Stock_Management_List:rebindGrid" + ex.Message)
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
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
                    If Val(ViewState("add")) = 1 Then
                        lnkNew.Visible = True
                    Else
                        lnkNew.Visible = False
                    End If

                    txtForDate.SelectedDate = System.DateTime.Now
                    txtToDate.SelectedDate = System.DateTime.Now
                    bindGrid()
                Else
                    Dim pid As Int32 = Int32.Parse(Request.QueryString("pid").ToString())
                    Dim pgid As Int32 = Int32.Parse(Request.QueryString("pgid").ToString())
                    Dim lid As Int32 = Int32.Parse(Request.QueryString("L_ID").ToString())
                    Dim S_Date As DateTime = DateTime.Parse(Request.QueryString("Date").ToString())
                    Dim E_Date As DateTime = DateTime.Parse(Request.QueryString("E_Date").ToString())
                    Dim name As String = Request.QueryString("name").ToString()
                    radDuration.SelectedValue = "Custom"
                    txtForDate.SelectedDate = S_Date
                    txtToDate.SelectedDate = E_Date
                    hdsearchvalueAfterEdit.Value = name

                    bindGrid()

                    For Each item As RepeaterItem In rdStock.Items

                        Dim i As Integer = Item.ItemIndex
                        Dim btnExpand As Button = DirectCast(item.FindControl("btnExpand"), Button)
                        Dim hd_stk_rec_id As HiddenField = DirectCast(item.FindControl("hd_stk_rec_id"), HiddenField)
                        Dim rptStockDetail As Repeater = DirectCast(item.FindControl("rptStockDetail"), Repeater)
                        Dim pnlStockDetail As Panel = DirectCast(item.FindControl("pnlStockDetail"), Panel)

                        If btnExpand.Text = "+" Then

                            oclsStock.cmp_id = Val(Session("cmp_id"))
                            oclsStock.stk_rec_id = Val(hd_stk_rec_id.Value)

                            Dim dt As DataTable = oclsStock.Selectproduct()
                            If dt.Rows.Count > 0 Then
                                rptStockDetail.DataSource = dt
                                rptStockDetail.DataBind()
                            Else
                                rptStockDetail.DataSource = String.Empty
                                rptStockDetail.DataBind()
                            End If

                            btnExpand.Text = "-"
                            pnlStockDetail.Visible = True

                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:Page_Load" + ex.Message)
        End Try
    End Sub

    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Stock Purchase"
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
            LogHelper.Error("Stock_Management_List:RoleCheck" + ex.Message)
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
    '        LogHelper.Error("Stock_Management_List:rdStock_ItemCreated" + ex.Message)
    '    End Try

    'End Sub

    'Protected Sub rdStock_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdStock.ItemDataBound
    '    Try
    '        If (TypeOf e.Item Is GridDataItem) Then
    '            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

    '            Dim is_finalsubmit As HiddenField = CType(e.Item.FindControl("hf_isfinalsubmit"), HiddenField)

    '            If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
    '                rdStock.MasterTableView.GetColumn("TemplateColumn").Visible = False
    '            End If
    '            If Val(ViewState("edit")) = 1 Then
    '                If is_finalsubmit.Value = 0 Then
    '                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
    '                Else
    '                    CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
    '                End If
    '                'CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = True
    '            Else
    '                CType(e.Item.FindControl("IbEdit"), LinkButton).Visible = False
    '            End If
    '            If Val(ViewState("delete")) = 1 Then
    '                CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = True
    '            Else
    '                CType(e.Item.FindControl("IbDelete"), LinkButton).Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_List:rdStock_ItemDataBound" + ex.Message)
    '    End Try
    'End Sub

    'Protected Sub rdStock_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdStock.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_List:rdStock_NeedDataSource" + ex.Message)
    '    End Try
    'End Sub

    Protected Sub deletestock(ByVal id As Integer)
        Try
            oclsStock.stk_rec_id = Val(id)
            oclsStock.stock_receive_date = DateTime.Now
            oclsStock.Location_id = 0
            oclsStock.total_stock = 0
            oclsStock.price = 0
            oclsStock.supplier_id = 0
            oclsStock.login_id = Val(Session("login_id"))
            oclsStock.cmp_id = Val(Session("cmp_id"))
            oclsStock.ip_address = Request.UserHostAddress
            oclsStock.receipt_number = ""
            oclsStock.supplier_code = ""
            oclsStock.is_finalsubmit = 0
            oclsStock.deletereceipt()


            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record deleted successfully.');", True)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Stock_Management_List:deletestock" + ex.Message)
        End Try
    End Sub
    Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        Try
            Session("Stk_Chg_rec_id") = Nothing
            Response.Redirect("Stock_Management_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:lnkNew_Click" + ex.Message)
        End Try

    End Sub


    'Protected Sub btnaddProduct_Click(sender As Object, e As EventArgs)
    '    Try
    '        If rdchild.Visible = True Then
    '            rdchild.Visible = False
    '        Else
    '            rdchild.Visible = True
    '        End If

    '    Catch ex As Exception
    '        LogHelper.Error("Stock_Management_Master:btnAddNewSize_Click:" + ex.Message)
    '    End Try

    'End Sub

    Protected Sub rdStock_DetailTableDataBind(source As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs)
        Try
            Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
            'Select Case e.DetailTableView.Name
            '    Case "stock"
            '        If True Then
            oclsStock.cmp_id = Val(Session("cmp_id"))
            oclsStock.stk_rec_id = dataItem.GetDataKeyValue("stk_rec_id").ToString()

            Dim dt As DataTable = oclsStock.Selectproduct()
            If dt.Rows.Count > 0 Then
                e.DetailTableView.DataSource = oclsStock.Selectproduct()
            Else
                e.DetailTableView.DataSource = Nothing
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:rdStock_DetailTableDataBind" + ex.Message)
        End Try

    End Sub

    Private Sub BindTable(ByVal cmp_id As Integer, ByVal stk_rec_id As Integer)
        Try

            ReportViewer1.Visible = False
            Dim connectionString = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password")
            Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            Dim sourceReportSource = New Telerik.Reporting.UriReportSource() With {
                 .Uri = Server.MapPath("~/Stock_Management_Detail.trdx")
            }
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(cmp_id)))
            sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("stkrecid", Val(stk_rec_id)))

            'Me.ReportViewer1.ReportSource = sourceReportSource
            ' Me.ReportViewer1.RefreshReport()

            RenderReport(sourceReportSource)

        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:BindTable:" + ex.Message)
        End Try

    End Sub

    Public Sub RenderReport(reportSource As Telerik.Reporting.ReportSource)
        Try
            Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
            Dim documentName As String = ""

            Dim deviceInfo As New System.Collections.Hashtable()
            deviceInfo("OutputFormat") = "PDF"

            Dim result As Telerik.Reporting.Processing.RenderingResult = reportProcessor.RenderReport("PDF", reportSource, deviceInfo)

            Dim fileName As String = result.DocumentName + "." + result.Extension
            Response.Clear()
            Response.ContentType = result.MimeType
            Response.Cache.SetCacheability(HttpCacheability.Private)
            Response.Expires = -1
            Response.Buffer = True
            Response.AddHeader("Content-Disposition", String.Format("{0};FileName=""{1}""", "attachment", fileName))
            Response.BinaryWrite(result.DocumentBytes)
            'Response.Flush()
            Response.End()
            'Response.Close()
        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:RenderReport:" + ex.Message)
        End Try

    End Sub
    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Try

            bindGrid()

        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:lnkView_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub txtForDate_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        Try

            txtToDate.SelectedDate = txtForDate.SelectedDate

        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:txtForDate_SelectedDateChanged:" + ex.Message)
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
            LogHelper.Error("Stock_Management_List:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub rdStock_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Try
            'Dim dataItem As GridDataItem = DirectCast(e.Item, GridDataItem)

            'If e.CommandName = "Edit" Then
            '    Session("stock_id") = Convert.ToInt32(e.CommandArgument)
            '    Response.Redirect("Stock_Management_Master.aspx", False)
            'ElseIf e.CommandName = "Delete" Then
            '    deletestock(Convert.ToInt32(e.CommandArgument))
            '    bindGrid()
            'ElseIf e.CommandName = "ViewDetail" Then
            '    Dim cmp_id As Integer = Val(Session("cmp_id"))
            '    Dim stk_rec_id As Integer = dataItem.GetDataKeyValue("stk_rec_id").ToString()
            '    BindTable(cmp_id, stk_rec_id)
            'End If

            If e.CommandName = "Edit" Then
                Session("stock_id") = Convert.ToInt32(e.CommandArgument)
                Response.Redirect("Stock_Management_Master.aspx", False)
            ElseIf e.CommandName = "Delete" Then
                deletestock(Convert.ToInt32(e.CommandArgument))
                bindGrid()
            Else
                Dim i As Integer = e.Item.ItemIndex
                Dim btnExpand As Button = DirectCast(rdStock.Items(i).FindControl("btnExpand"), Button)
                Dim hd_stk_rec_id As HiddenField = DirectCast(rdStock.Items(i).FindControl("hd_stk_rec_id"), HiddenField)
                Dim rptStockDetail As Repeater = DirectCast(rdStock.Items(i).FindControl("rptStockDetail"), Repeater)
                Dim pnlStockDetail As Panel = DirectCast(rdStock.Items(i).FindControl("pnlStockDetail"), Panel)

                If btnExpand.Text = "+" Then

                    oclsStock.cmp_id = Val(Session("cmp_id"))
                    oclsStock.stk_rec_id = Val(hd_stk_rec_id.Value)

                    Dim dt As DataTable = oclsStock.Selectproduct()
                    If dt.Rows.Count > 0 Then
                        rptStockDetail.DataSource = dt
                        rptStockDetail.DataBind()
                    Else
                        rptStockDetail.DataSource = String.Empty
                        rptStockDetail.DataBind()
                    End If

                    btnExpand.Text = "-"
                    pnlStockDetail.Visible = True

                    'Dim cmp_id As Integer = Val(Session("cmp_id"))
                    'Dim stk_rec_id As Integer = Val(hd_stk_rec_id.Value)
                    'BindTable(cmp_id, stk_rec_id)


                Else
                    btnExpand.Text = "+"
                    pnlStockDetail.Visible = False
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_List:rdStock_ItemCommand" + ex.Message)
        End Try
    End Sub
    Protected Sub rdStock_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim is_finalsubmit As HiddenField = CType(e.Item.FindControl("hf_isfinalsubmit"), HiddenField)

                If Val(ViewState("edit")) = 0 And Val(ViewState("delete")) = 0 Then
                    'rdStock.mastertableview.getcolumn("templatecolumn").visible = False

                End If
                If Val(ViewState("edit")) = 1 Then
                    If is_finalsubmit.Value = 0 Then
                        CType(e.Item.FindControl("ibedit"), LinkButton).Visible = True
                    Else
                        CType(e.Item.FindControl("ibedit"), LinkButton).Visible = False
                    End If
                Else
                    CType(e.Item.FindControl("ibedit"), LinkButton).Visible = False
                End If
                If Val(ViewState("delete")) = 1 Then
                    CType(e.Item.FindControl("ibdelete"), LinkButton).Visible = True
                Else
                    CType(e.Item.FindControl("ibdelete"), LinkButton).Visible = False
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("stock_management_list:rdstock_itemdatabound" + ex.Message)
        End Try
    End Sub
End Class

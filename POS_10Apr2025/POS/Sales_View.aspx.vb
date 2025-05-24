Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Sales_View
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim dt As DataTable
    Public Sub bindGrid()
        Try
            oclsSales.cmp_id = Val(Session("cmp_id"))
            oclsSales.sales_id = Val(Session("sales_id"))
            Dim dt As DataTable = oclsSales.SelectView()

            If dt.Rows.Count > 0 Then
                rdTSales.DataSource = dt
                rdTSales.DataBind()
            Else
                rdTSales.DataSource = String.Empty
                rdTSales.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Sales_View:bindGrid:" + ex.Message)
        End Try

    End Sub

    Public Sub bindcardGrid()
        Try
            oclsSales.cmp_id = Val(Session("cmp_id"))
            oclsSales.sales_id = Val(Session("sales_id"))
            Dim dt As DataTable = oclsSales.Selectcardview()

            If dt.Rows.Count > 0 Then
                rdcard.Visible = False
                divcard.Visible = False
                rdcard.DataSource = dt
                rdcard.DataBind()
            Else
                rdcard.Visible = False
                divcard.Visible = False
                rdcard.DataSource = String.Empty
                rdcard.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Sales_View:bindGrid:" + ex.Message)
        End Try

    End Sub

    Public Sub rebindGrid()
        Try
            oclsSales.cmp_id = Val(Session("cmp_id"))
            oclsSales.sales_id = Val(Session("sales_id"))
            Dim dt As DataTable = oclsSales.SelectView()

            If dt.Rows.Count > 0 Then
                rdTSales.DataSource = dt
            Else
                rdTSales.DataSource = String.Empty
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Sales_View:rebindGrid:" + ex.Message)
        End Try

    End Sub
    Private Sub BindIngredient(ByVal id As Integer)
        Try

            oclsSales.tsales_id = id
            Dim dt As DataTable = oclsSales.SelectIngredient()

            If dt.Rows.Count > 0 Then

                Session("Ingredient_dt") = dt


                'rdIngredient.Visible = True
                'rdIngredient.DataSource = dt
                'rdIngredient.DataBind()

            Else
                'rdIngredient.Visible = False
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindSize:" + ex.Message)
        End Try
    End Sub
    Private Sub BindCondiment(ByVal id As Integer)
        Try
            oclsSales.tsales_id = id
            Dim dt As DataTable = oclsSales.SelectCondiment()

            If dt.Rows.Count > 0 Then

                Session("Condiment_dt") = dt

                'radListCondiment.Visible = True
                'radListCondiment.DataSource = dt
                'radListCondiment.DataBind()
            Else
                'radListCondiment.Visible = False
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:BindCondiment:" + ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("sales_id") = Nothing Then
                    BindSales()

                End If
                bindGrid()
                bindcardGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Sales_View:Page_Load:" + ex.Message)
        End Try
    End Sub

    Private Sub BindSales()
        Try
            oclsSales.cmp_id = Val(Session("cmp_id"))
            oclsSales.sales_id = Val(Session("sales_id"))
            Dim dtSales As DataTable = oclsSales.Select()
            If dtSales.Rows.Count > 0 Then
                lbltAmt.InnerText = dtSales.Rows(0)("total_amount").ToString
                lblTDisc.InnerText = dtSales.Rows(0)("total_discount").ToString
                lblnetAmt.InnerText = dtSales.Rows(0)("net_amount").ToString
                lblCreatedDt.InnerText = dtSales.Rows(0)("created_date").ToString
                lblLogin.InnerText = dtSales.Rows(0)("UserName").ToString
                lblSType.InnerText = dtSales.Rows(0)("sale_type1").ToString
                lblReceivedAmount.InnerText = dtSales.Rows(0)("input_amount").ToString()
                lblchange.InnerText = dtSales.Rows(0)("change").ToString()
                lblLocation.InnerHtml = dtSales.Rows(0)("Location").ToString()

                lblwaddress.InnerHtml = dtSales.Rows(0)("web_address").ToString()
                lblwEmail.InnerHtml = dtSales.Rows(0)("web_email").ToString()
                lblwMobile.InnerHtml = dtSales.Rows(0)("web_mobile").ToString()
                lblwName.InnerHtml = dtSales.Rows(0)("web_name").ToString()

                lblsurcharge_amount.InnerHtml = dtSales.Rows(0)("surcharge_amount").ToString()
                lblNoOfCovers.InnerHtml = dtSales.Rows(0)("NoOfCovers").ToString()
                lblEatOutAmount.InnerHtml = dtSales.Rows(0)("EatOutAmount").ToString()
                lbltransaction_count.InnerHtml = dtSales.Rows(0)("transaction_count").ToString()
                lblshift_name.InnerHtml = dtSales.Rows(0)("shift_name").ToString()
                lblis_cash_table.InnerHtml = dtSales.Rows(0)("is_cash_table").ToString()
                lbltable_id.InnerHtml = dtSales.Rows(0)("table_id").ToString()
                lblTransType.InnerHtml = dtSales.Rows(0)("Ref_type").ToString()
                HdnRef_id.Value = dtSales.Rows(0)("ref_id").ToString()
                HdnMachine_id.Value = dtSales.Rows(0)("machine_id").ToString()
                lblRefNo.InnerHtml = dtSales.Rows(0)("ref_id").ToString()


                Dim sts As String
                sts = dtSales.Rows(0)("Ref_type").ToString()

                If (sts = "Online") Then
                    Dim conn As DBConnection = New DBConnection()
                    Dim que = " select 1 from m_sales_self_sync where sales_id = '" & oclsSales.sales_id & "' "
                    Dim ds As DataSet = conn.SelectData(que)

                    lblWsname.Visible = True
                    lblSyncStatus.Visible = True

                    If (ds.Tables(0).Rows.Count = 1) Then
                        lblSyncStatus.InnerHtml = "YES"
                    Else
                        lblSyncStatus.InnerHtml = "NO"
                    End If
                Else

                End If




            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Sales_View:BindSales:" + ex.Message)
        End Try
    End Sub



    'Protected Sub rdTSales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdTSales.NeedDataSource
    '    Try
    '        rebindGrid()
    '    Catch ex As Exception
    '        LogHelper.Error("Sales_View:rdTSales_NeedDataSource:" + ex.Message)
    '    End Try
    'End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            oclsSales.cmp_id = Val(Session("cmp_id"))
            oclsSales.sales_id = Val(Session("sales_id"))
            oclsSales.ref_id = HdnRef_id.Value
            oclsSales.machine_id = HdnMachine_id.Value

            oclsSales.insert_resync()

            Response.Redirect("Sales_List.aspx", False)

        Catch ex As Exception
            LogHelper.Error("Sales_View:rdTSales_NeedDataSource:" + ex.Message)
        End Try

    End Sub

    Private Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Try
            oclsSales.sales_id = Val(Session("sales_id"))

            oclsSales.delete_sync()

            Response.Redirect("Sales_List.aspx", False)

        Catch ex As Exception
            LogHelper.Error("Sales_View:rdTSales_NeedDataSource:" + ex.Message)
        End Try

    End Sub
    'Protected Sub rdTSales_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
    '    Try
    '        'If e.CommandName = "ViewIngredientCondiment" Then
    '        '    Dim script As String = "function f(){$find(""" + RadWindow1.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
    '        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyAddEditFeedback", script, True)

    '        '    BindCondiment(Convert.ToInt32(e.CommandArgument))
    '        '    BindIngredient(Convert.ToInt32(e.CommandArgument))
    '        'End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Protected Sub lnkview_Click(sender As Object, e As EventArgs)
        Try

            Dim btn As LinkButton = DirectCast(sender, LinkButton)

            Dim tsales As Int32 = Val(btn.CommandArgument.ToString())

            BindCondiment(Val(tsales))
            BindIngredient(Val(tsales))

            'If Not Session("Condiment_dt") = Nothing Or Not Session("Ingredient_dt") = Nothing Then

            Dim url As String = "Sales_view_detail.aspx"

            ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

            'End If

        Catch ex As Exception

        End Try
    End Sub
End Class




Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting
Imports System.Web.Services
Partial Class Product_return
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole


    <WebMethod>
    Public Sub BindData()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet
            If rdType.SelectedValue = "ALL" Then


                Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
                oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
                oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                oColSqlparram.Add("@product_id", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0"), SqlDbType.Int)
                oColSqlparram.Add("@category_id", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0"), SqlDbType.Int)
                oColSqlparram.Add("@Duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
                oColSqlparram.Add("@type", rdType.SelectedValue, SqlDbType.NVarChar)
                Dim dt As DataTable
                If hfSizelocation.Value = "0" Then
                    dt = oClsDal.GetdataTableSp("P_R_Product_return", oColSqlparram)
                Else
                    dt = oClsDal.GetdataTableSp("P_R_Product_location_return", oColSqlparram)
                End If

                rptProductSUmmary.DataSource = dt
                rptProductSUmmary.DataBind()
            ElseIf rdType.SelectedValue = "SALE" Then

                Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
                oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
                oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                oColSqlparram.Add("@product_id", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0"), SqlDbType.Int)
                oColSqlparram.Add("@category_id", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0"), SqlDbType.Int)
                oColSqlparram.Add("@Duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
                oColSqlparram.Add("@type", rdType.SelectedValue, SqlDbType.NVarChar)

                Dim dt As DataTable
                If hfSizelocation.Value = "0" Then
                    dt = oClsDal.GetdataTableSp("P_R_Product_return", oColSqlparram)
                Else
                    dt = oClsDal.GetdataTableSp("P_R_Product_location_return", oColSqlparram)
                End If
                rptProductSUmmary.DataSource = dt
                rptProductSUmmary.DataBind()
            ElseIf rdType.SelectedValue = "RETURN" Then
                Dim oColSqlparram As ColSqlparram = New ColSqlparram()
                oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
                oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
                oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
                oColSqlparram.Add("@product_id", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0"), SqlDbType.Int)
                oColSqlparram.Add("@category_id", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0"), SqlDbType.Int)
                oColSqlparram.Add("@Duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
                oColSqlparram.Add("@type", rdType.SelectedValue, SqlDbType.NVarChar)

                Dim dt As DataTable
                If hfSizelocation.Value = "0" Then
                    dt = oClsDal.GetdataTableSp("P_R_Product_return", oColSqlparram)
                Else
                    dt = oClsDal.GetdataTableSp("P_R_Product_location_return", oColSqlparram)
                End If
                rptProductSUmmary.DataSource = dt
                rptProductSUmmary.DataBind()
            End If



        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
    End Sub

    Private Sub PSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If
            'If Val(Session("staff_role_id")) <> 0 Then
            '    RoleCheck()
            'ElseIf Val(Session("staff_role_id")) = 0 Then
            '    ViewState("view") = 1
            'Else
            '    ViewState("view") = 0
            'End If
            'If Val(ViewState("view")) = 1 Then
            '    divFunction.Visible = True
            'Else
            '    divFunction.Visible = False
            'End If

            txtFrom.SelectedDate = System.DateTime.Now
            txtTo.SelectedDate = System.DateTime.Now

            binddll()

        End If
    End Sub
    Public Sub binddll()
        Try
            If radCategory.SelectedIndex = -1 Then
                oclsBind.BindProduct(radProduct, Val(Session("cmp_id")))
            End If
            oclsBind.BindProductGroupALL(radCategory, Val(Session("cmp_id")))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_return_Report:binddll:" + ex.Message)
        End Try
    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        BindData()
    End Sub
    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Product_return_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub lbtnsize_Click(sender As Object, e As EventArgs)
        If (hfSizelocation.Value = "0") Then
            hfSizelocation.Value = "1"
            lbtnsize.Text = "Hide Location"
        Else
            hfSizelocation.Value = "0"
            lbtnsize.Text = "Show Location"
        End If
        BindData()
    End Sub
    Protected Sub LinksizeA_Click(sender As Object, e As EventArgs)
        If (LinksizeA.Text = "Show Size") Then

            LinksizeA.Text = "Hide Size"
        Else

            LinksizeA.Text = "Show Size"
        End If
        BindData()
    End Sub


End Class

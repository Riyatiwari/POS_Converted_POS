
Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting

Partial Class Discount_Summary_Report
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@product_id", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0"), SqlDbType.Int)
            oColSqlparram.Add("@category_id", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0"), SqlDbType.Int)
            oColSqlparram.Add("@type", rbtDisplayReport.SelectedValue, SqlDbType.NVarChar)
            oColSqlparram.Add("@Duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedItem.Text, "Today"), SqlDbType.NVarChar)
            oColSqlparram.Add("@Discount_Name", IIf(radDiscount.SelectedIndex > 0, radDiscount.SelectedItem.Text, "0"), SqlDbType.NVarChar)
            oColSqlparram.Add("@machine_name", IIf(radMachine.SelectedIndex > 0, radMachine.SelectedItem.Text, "0"), SqlDbType.NVarChar)
            oColSqlparram.Add("@Venue_name", IIf(radVenue.SelectedIndex > 0, radVenue.SelectedItem.Text, "0"), SqlDbType.NVarChar)
            oColSqlparram.Add("@Location_name", IIf(radLocation.SelectedIndex > 0, radLocation.SelectedItem.Text, "0"), SqlDbType.NVarChar)
            oColSqlparram.Add("@Operator_name", IIf(radOperator.SelectedIndex > 0, radOperator.SelectedItem.Text, "0"), SqlDbType.NVarChar)

            If chkProduct.Checked = True Then
                oColSqlparram.Add("@type1", "2", SqlDbType.NVarChar)
            ElseIf chkDiscount.Checked = True Then
                oColSqlparram.Add("@type1", "3", SqlDbType.NVarChar)
            Else
                oColSqlparram.Add("@type1", "0", SqlDbType.NVarChar)
            End If

            Dim dt As DataTable
            dt = oClsDal.GetdataTableSp("P_R_Product_Discount", oColSqlparram)

            If dt.Rows.Count > 0 Then
                rptDiscSUmmary.DataSource = dt
                rptDiscSUmmary.DataBind()
            Else
                rptDiscSUmmary.DataSource = String.Empty
                rptDiscSUmmary.DataBind()
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Summary_Report:BindTable:" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                If Val(Session("staff_role_id")) <> 0 Then
                    RoleCheck()
                ElseIf Val(Session("staff_role_id")) = 0 Then
                    ViewState("view") = 1
                Else
                    ViewState("view") = 0
                End If
                If Val(ViewState("view")) = 1 Then
                    divFunction.Visible = True
                Else
                    divFunction.Visible = False
                End If

                txtFrom.SelectedDate = System.DateTime.Now
                txtTo.SelectedDate = System.DateTime.Now
                oclsBind.BindDiscount(radDiscount, Val(Session("cmp_id")))
                oclsBind.BindMachine(radMachine, Val(Session("cmp_id")))
                oclsBind.BindVenue(radVenue, Val(Session("cmp_id")))
                oclsBind.BindLocation(radLocation, Val(Session("cmp_id")))
                oclsBind.BindStaffBindStaffLoginIdForDiscount(radOperator, Val(Session("cmp_id")), 0)
                binddll()

            End If
        Catch ex As Exception
            LogHelper.Error("Product_Summary_Report:Page_Load:" + ex.Message)
        End Try

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            BindTable()
        Catch ex As Exception
            LogHelper.Error("Product_Summary_Report:LinkButton1_Click:" + ex.Message)
        End Try
    End Sub
    Public Sub binddll()
        Try
            If radCategory.SelectedIndex = -1 Then
                oclsBind.BindProduct(radProduct, Val(Session("cmp_id")))
            End If
            oclsBind.BindProductGroupALL(radCategory, Val(Session("cmp_id")))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Summary_Report:binddll:" + ex.Message)
        End Try
    End Sub

    Protected Sub radCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radCategory.SelectedIndexChanged
        Try
            If radCategory.SelectedIndex = 0 Then
                oclsBind.BindProduct(radProduct, Val(Session("cmp_id")))
            Else
                oclsBind.BindProductByProductGroup(radProduct, Val(Session("cmp_id")), Val(radCategory.SelectedValue))
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Summary_Report:radCategory_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
    Protected Sub RoleCheck()
        Try
            oclsRole = New Controller_clsRole()

            oclsRole.cmp_id = Val(Session("cmp_id"))
            oclsRole.Role_Id = Val(Session("staff_role_id"))
            oclsRole.Form_Name = "Discount Summary Report"
            oclsRole.Select()

            If oclsRole.is_View = 1 Then
                ViewState("view") = 1
            Else
                ViewState("view") = 0
            End If

            If oclsRole.is_add = 1 Then
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
            LogHelper.Error("Product_Summary_Report:RoleCheck:" + ex.Message)
        End Try
    End Sub


    Protected Sub chkDiscount_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkDiscount.Checked = True Then
                dv_Discount.Visible = True
            Else
                dv_Discount.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub rbtDisplayReport_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If rbtDisplayReport.SelectedValue.ToString() = "2" Then
                dv_Machine.Visible = False
                dv_Operator.Visible = False
                dv_Venue.Visible = True
                dv_Location.Visible = False
            ElseIf rbtDisplayReport.SelectedValue.ToString() = "1" Then
                dv_Venue.Visible = False
                dv_Operator.Visible = False
                dv_Machine.Visible = False
                dv_Location.Visible = True
            ElseIf rbtDisplayReport.SelectedValue.ToString() = "3" Then
                dv_Machine.Visible = False
                dv_Venue.Visible = False
                dv_Location.Visible = False
                dv_Operator.Visible = True
            Else
                dv_Machine.Visible = True
                dv_Venue.Visible = False
                dv_Location.Visible = False
                dv_Operator.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub radDuration_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If
        Catch ex As Exception
            LogHelper.Error("Sales_Report:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub
End Class



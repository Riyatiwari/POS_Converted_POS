
Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting

Partial Class Product_Summary_Report_New
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole

    Private Sub BindTable()
        Try


            ''ReportViewer1.Visible = True
            ''Dim connectionString As String = "Data Source=" & HttpContext.Current.Session("db_server") & ";" & "Initial Catalog=" & HttpContext.Current.Session("db_name") & ";" & "User ID=" & HttpContext.Current.Session("user_name") & ";" & "Password=" & HttpContext.Current.Session("password") & ";Min Pool Size=10;Max Pool Size=500;Connect Timeout=500"
            ''Dim connectionStringHandler = New ReportConnectionStringManager(connectionString)

            ''If chkProduct.Checked = True Then
            ''    If rdType.SelectedValue = "SALE" Then

            ''        Dim sourceReportSource1 = New UriReportSource() With {
            ''            .Uri = Server.MapPath("~/Product_Summary_Sale.trdx")
            ''       }

            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))

            ''        Dim reportSource1 = connectionStringHandler.UpdateReportSource(sourceReportSource1)
            ''        Me.ReportViewer1.ReportSource = reportSource1
            ''        Me.ReportViewer1.RefreshReport()
            ''    ElseIf rdType.SelectedValue = "RETURN" Then
            ''        Dim sourceReportSource2 = New UriReportSource() With {
            ''                        .Uri = Server.MapPath("~/Product_Summary_Return.trdx")
            ''                   }

            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))


            ''        Dim reportSource2 = connectionStringHandler.UpdateReportSource(sourceReportSource2)
            ''        Me.ReportViewer1.ReportSource = reportSource2
            ''        Me.ReportViewer1.RefreshReport()
            ''    Else
            ''        Dim sourceReportSource = New UriReportSource() With {
            ''             .Uri = Server.MapPath("~/Product_Summary_Rpt.trdx")
            ''        }

            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))


            ''        Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            ''        Me.ReportViewer1.ReportSource = reportSource
            ''        Me.ReportViewer1.RefreshReport()

            ''    End If

            ''Else
            ''    If rdType.SelectedValue = "SALE" Then

            ''        Dim sourceReportSource1 = New UriReportSource() With {
            ''            .Uri = Server.MapPath("~/Product_Summary_Sale1.trdx")
            ''       }

            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            ''        sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))

            ''        Dim reportSource1 = connectionStringHandler.UpdateReportSource(sourceReportSource1)
            ''        Me.ReportViewer1.ReportSource = reportSource1
            ''        Me.ReportViewer1.RefreshReport()
            ''    ElseIf rdType.SelectedValue = "RETURN" Then
            ''        Dim sourceReportSource2 = New UriReportSource() With {
            ''                        .Uri = Server.MapPath("~/Product_Summary_Return1.trdx")
            ''                   }

            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            ''        sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))


            ''        Dim reportSource2 = connectionStringHandler.UpdateReportSource(sourceReportSource2)
            ''        Me.ReportViewer1.ReportSource = reportSource2
            ''        Me.ReportViewer1.RefreshReport()
            ''    Else
            ''        Dim sourceReportSource = New UriReportSource() With {
            ''             .Uri = Server.MapPath("~/Product_Summary_Rpt1.trdx")
            ''        }

            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            ''        sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))


            ''        Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            ''        Me.ReportViewer1.ReportSource = reportSource
            ''        Me.ReportViewer1.RefreshReport()

            ''    End If
            ''End If


            'If rdType.SelectedValue = "SALE" Then

            '    Dim sourceReportSource1 = New UriReportSource() With { _
            '        .Uri = Server.MapPath("~/Product_Summary_Sale.trdx") _
            '   }

            '    sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            '    sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            '    sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    sourceReportSource1.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))

            '    Dim reportSource1 = connectionStringHandler.UpdateReportSource(sourceReportSource1)
            '    Me.ReportViewer1.ReportSource = reportSource1
            '    Me.ReportViewer1.RefreshReport()
            'ElseIf rdType.SelectedValue = "RETURN" Then
            '    Dim sourceReportSource2 = New UriReportSource() With { _
            '                    .Uri = Server.MapPath("~/Product_Summary_Return.trdx") _
            '               }

            '    sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            '    sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            '    sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    sourceReportSource2.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))


            '    Dim reportSource2 = connectionStringHandler.UpdateReportSource(sourceReportSource2)
            '    Me.ReportViewer1.ReportSource = reportSource2
            '    Me.ReportViewer1.RefreshReport()
            'Else
            '    Dim sourceReportSource = New UriReportSource() With { _
            '         .Uri = Server.MapPath("~/Product_Summary_Rpt.trdx") _
            '    }

            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("cmpid", Val(Session("cmp_id"))))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("productid", IIf(radProduct.SelectedIndex > 0, radProduct.SelectedValue, "0")))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("categoryid", IIf(radCategory.SelectedIndex > 0, radCategory.SelectedValue, "0")))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("type", rdType.SelectedValue))
            '    sourceReportSource.Parameters.Add(New Telerik.Reporting.Parameter("duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today")))


            '    Dim reportSource = connectionStringHandler.UpdateReportSource(sourceReportSource)
            '    Me.ReportViewer1.ReportSource = reportSource
            '    Me.ReportViewer1.RefreshReport()

            'End If


            'Dim report1 = New Report()
            'Dim reportHeader = TryCast(report1.Items.Find("reportHeaderSection1", True)(0), Telerik.Reporting.ReportHeaderSection)



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

    Protected Sub radCategory_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radCategory.SelectedIndexChanged
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
            oclsRole.Form_Name = "Product Summary"
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

    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
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



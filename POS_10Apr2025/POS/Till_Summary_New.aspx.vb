Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports Telerik.Reporting
Imports System.Web.Services

Partial Class Till_Summary_New
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole
    Dim dt As DataTable

    <WebMethod>
    Public Sub BindData()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet

            Dim dtwise As Integer = 0
            If chkDateWise.Checked = True Then
                dtwise = 0
            Else
                dtwise = 1
            End If

            If chkDateWise.Checked = True Then
                dtwise = 0
            Else
                dtwise = 1
            End If

            Dim n As String

            If chkPs2.Checked = True Then
                n = "0"
            ElseIf chkPG1.Checked = True Then
                n = "2"
            ElseIf chkPs4.Checked = True Then
                n = "1"
            End If

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()

            oColSqlparram.Add("@date1", IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@date2", IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), SqlDbType.DateTime)
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@location_id", "0")
            oColSqlparram.Add("@machine_id", "0")
            oColSqlparram.Add("@venue_id", "0")
            oColSqlparram.Add("@Duration", IIf(radDuration.SelectedIndex > 0, radDuration.SelectedValue, "Today"), SqlDbType.NVarChar)
            oColSqlparram.Add("@Withoutdate", dtwise, SqlDbType.Int)
            oColSqlparram.Add("@groupByCol", n, SqlDbType.Int)
            oColSqlparram.Add("@salestype", IIf(radsalesType.SelectedIndex > 0, radsalesType.SelectedValue.ToString, "All"))
            oColSqlparram.Add("@shift_name", IIf(radshifttype.SelectedIndex > 0, radshifttype.SelectedItem.Value.ToString, "0"))

            If hfSizelocation.Value = "0" Then

                ds = oClsDal.GetdatasetSp("P_TillSummary_new", oColSqlparram, "")
                dt = ds.Tables(0)

            Else
                ds = oClsDal.GetdatasetSp("P_TillSummary_new", oColSqlparram, "")
                dt = ds.Tables(0)

            End If

            Dim dtDepartments As DataTable = ds.Tables(2)
            If dtDepartments.Rows.Count >= 4 Then
                ViewState("Dept1") = dtDepartments.Rows(0)("Department_Category_name").ToString()
                ViewState("Dept2") = dtDepartments.Rows(1)("Department_Category_name").ToString()
                ViewState("Dept3") = dtDepartments.Rows(2)("Department_Category_name").ToString()
                ViewState("Dept4") = dtDepartments.Rows(3)("Department_Category_name").ToString()
            End If





            'Set Grand Total 
            Dim dt1 As DataTable = ds.Tables(1)
            hd_NetTotal.Value = dt1.Rows(0)("NetAmt").ToString()

            rptProductSUmmary.DataSource = dt
            rptProductSUmmary.DataBind()

            getShifts()
        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If

            txtFrom.SelectedDate = System.DateTime.Now
            txtTo.SelectedDate = System.DateTime.Now
            '  BindData()
            getShifts()
        End If
    End Sub

    Protected Sub radDuration_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles radDuration.SelectedIndexChanged
        Try
            If radDuration.SelectedItem.Value = "Custom" Then
                divcustom.Visible = True
            Else
                divcustom.Visible = False
            End If

            getShifts()
        Catch ex As Exception
            LogHelper.Error("Till_Summary_New:radDuration_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Protected Sub lbtnsize_Click(sender As Object, e As EventArgs) Handles lbtnsize.Click
        If (hfSizelocation.Value = "0") Then
            hfSizelocation.Value = "1"
            lbtnsize.Text = "Show Department & Course"
        Else
            hfSizelocation.Value = "0"
            lbtnsize.Text = "Show Department & Course"
        End If
        BindData()
    End Sub


    Public Sub getShifts()
        oclsBind.BindShiftByDuration(radshifttype, Val(Session("cmp_id")), IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtTo.SelectedDate), IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate), (radDuration.SelectedItem.Value))
    End Sub

    Protected Sub txtFrom_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub
    Protected Sub txtTo_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs)
        getShifts()
    End Sub
    Protected Sub LinkButton1_Click1(sender As Object, e As EventArgs)
        BindData()
    End Sub

    Private Sub rptProductSUmmary_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptProductSUmmary.ItemDataBound
        Try
            If (e.Item.ItemType = ListItemType.Header) Then
                Dim dept1 As Label = e.Item.FindControl("lblDept1")
                Dim dept2 As Label = e.Item.FindControl("lblDept2")
                Dim dept3 As Label = e.Item.FindControl("lblDept3")
                Dim dept4 As Label = e.Item.FindControl("lblDept4")
                If ViewState("Dept1") IsNot Nothing AndAlso ViewState("Dept2") IsNot Nothing AndAlso ViewState("Dept3") IsNot Nothing AndAlso ViewState("Dept4") IsNot Nothing Then
                    dept1.Text = ViewState("Dept1").ToString()
                    dept2.Text = ViewState("Dept2").ToString()
                    dept3.Text = ViewState("Dept3").ToString()
                    dept4.Text = ViewState("Dept4").ToString()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

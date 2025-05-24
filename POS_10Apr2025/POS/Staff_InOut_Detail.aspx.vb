Imports System.Data
Imports Telerik.Web.UI

Partial Class Staff_InOut_Detail
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess
    Dim oclsBind As New clsBinding
    Dim oclsRole As New Controller_clsRole
    Dim oclsStaff As New Controller_clsStaff()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                oclsBind.BindStaff_new(radStaff, Val(Session("cmp_id").ToString()))
                txtFrom.SelectedDate = System.DateTime.Now
                txtTo.SelectedDate = System.DateTime.Now

                bindGrid()
            End If
        Catch ex As Exception
            LogHelper.Error("Staff_InOut_Detail:Page_Load:" + ex.Message)
        End Try
    End Sub


    Public Sub bindGrid()
        Try
            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)
            ToDate = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)


            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.from_date = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt")
            oclsStaff.to_date = Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt")

            Dim dt As DataTable = oclsStaff.[SelectAll_InOutDetail]()
            If dt.Rows.Count > 0 Then
                RadGridMain.DataSource = dt
                RadGridMain.DataBind()
            Else
                RadGridMain.DataSource = String.Empty
                RadGridMain.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Staff_InOut_Detail:bindGrid:" + ex.Message)
        End Try

    End Sub

    Protected Sub btn_View_Click(sender As Object, e As EventArgs)
        Try

            Dim FromDate As String
            Dim ToDate As String
            FromDate = IIf(txtFrom.SelectedDate.ToString() = "", txtFrom.SelectedDate = DateTime.Now, txtFrom.SelectedDate)
            ToDate = IIf(txtTo.SelectedDate.ToString() = "", txtTo.SelectedDate = DateTime.Now, txtTo.SelectedDate)

            oclsStaff.cmp_id = Val(Session("cmp_id"))
            oclsStaff.staff_id = Val(radStaff.SelectedValue)
            oclsStaff.from_date = Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd hh:mm tt")
            oclsStaff.to_date = Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd hh:mm tt")

            Dim dt As DataTable = oclsStaff.[Select_InOutDetail]()
            If dt.Rows.Count > 0 Then
                RadGridMain.DataSource = dt
                RadGridMain.DataBind()
            Else
                RadGridMain.DataSource = String.Empty
                RadGridMain.DataBind()
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Staff_InOut_Detail:btn_View_Click:" + ex.Message)
        End Try
    End Sub
End Class

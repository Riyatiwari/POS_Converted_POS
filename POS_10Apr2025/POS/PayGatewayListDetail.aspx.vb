Imports System.Data

Partial Class PayGatewayListDetail
    Inherits System.Web.UI.Page

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Session("cmp_id") = Nothing Then
                Response.Redirect("SignIn.aspx", False)
            End If


            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@mID", Request.QueryString("mID").ToString())
            oColSqlparram.Add("@RID", Request.QueryString("RID").ToString())
            oColSqlparram.Add("@TID", "")
            oColSqlparram.Add("@P_date", Request.QueryString("P_date").ToString())
            oColSqlparram.Add("@Duration", Request.QueryString("Duration").ToString())
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@MonthName", Request.QueryString("M_Name").ToString())

            Dim dt As DataTable = oClsDal.GetdataTableSp("P_gtway_List_Detail", oColSqlparram)

            If (dt.Rows.Count > 0) Then

                rptListDetail.DataSource = dt
                rptListDetail.DataBind()
            Else

                rptListDetail.DataSource = String.Empty
                rptListDetail.DataBind()
            End If
        End If

    End Sub

    Protected Sub lnkview_Click(sender As Object, e As EventArgs)
        Try

            Dim btn As LinkButton = DirectCast(sender, LinkButton)
            Dim ref As String = btn.CommandArgument

            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@ref", ref)

            Dim dt As DataTable = oClsDal.GetdataTableSp("P_gtway_report_Ref_Detail", oColSqlparram)

            If (dt.Rows.Count > 0) Then

                Session("refdt") = dt

                Dim url As String = "PayGatewayDetail.aspx"

                ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

            End If
        Catch ex As Exception
            LogHelper.Error("PayGatewaySummary:lnkview_Click:" + ex.Message)
        End Try
    End Sub
End Class

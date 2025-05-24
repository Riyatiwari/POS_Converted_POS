Imports System.Data
Imports System.IO

Partial Class Controls_Menu_Accordian
    Inherits System.Web.UI.UserControl
    Dim oClsDataccess As New ClsDataccess

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Dim DT As DataTable
                Dim oclsParm As New ColSqlparram
                oclsParm.Add("@Role_Id", Session("staff_role_id").ToString(), SqlDbType.Int)
                oclsParm.Add("@Type", 0, SqlDbType.Int)
                oclsParm.Add("@FullAccess", If(Session("fullaccess") IsNot Nothing, Session("fullaccess"), 0), SqlDbType.Int)
                DT = oClsDataccess.GetdataTableSp("P_Load_Form", oclsParm)
                ViewState("dtMenu") = DT

                Dim dv As DataView = DT.DefaultView

                dv.RowFilter = " isnull(Parent_id,0) = 0 "
                DT = dv.ToTable
                rpMenu.DataSource = DT
                rpMenu.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rpMenu_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpMenu.ItemDataBound
        Try

            Dim id As Integer = CType(e.Item.FindControl("hfformid"), HiddenField).Value
            Dim dtSecond As DataTable = CType(ViewState("dtMenu"), DataTable)

            Dim dvSec As DataView = dtSecond.DefaultView
            dvSec.RowFilter = " isnull(Parent_id,0) =  " + id.ToString
            dtSecond = dvSec.ToTable

            CType(e.Item.FindControl("rpMenuSec"), Repeater).DataSource = dtSecond
            CType(e.Item.FindControl("rpMenuSec"), Repeater).DataBind()

        Catch ex As Exception
            Dim st As String = ex.ToString
        End Try
    End Sub


    Protected Sub rpMenuSec_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        Try

            Dim id As Integer = CType(e.Item.FindControl("hfSecformid"), HiddenField).Value
            Dim dtSecond As DataTable = CType(ViewState("dtMenu"), DataTable)

            Dim dvSec As DataView = dtSecond.DefaultView
            dvSec.RowFilter = " isnull(Parent_id,0) =  " + id.ToString
            dtSecond = dvSec.ToTable
            If dtSecond.Rows.Count > 0 Then


                CType(e.Item.FindControl("rpMenuThird"), Repeater).DataSource = dtSecond
                CType(e.Item.FindControl("rpMenuThird"), Repeater).DataBind()
            End If

        Catch ex As Exception
            Dim st As String = ex.ToString
        End Try
    End Sub
End Class

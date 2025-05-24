Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.ComponentModel

Partial Class Product_Transaction_Detail
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                bindGrid()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub bindGrid()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim dt As DataTable

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            If Request.QueryString("s_date") IsNot Nothing Then
                oColSqlparram.Add("@date1", Convert.ToDateTime(IIf(Request.QueryString("s_date").ToString() = "", DateTime.Now, Request.QueryString("s_date").ToString())), SqlDbType.DateTime)

            Else
                oColSqlparram.Add("@date1", DateTime.Now, SqlDbType.DateTime)

            End If
            If Request.QueryString("e_date") IsNot Nothing Then
                oColSqlparram.Add("@date2", Convert.ToDateTime(IIf(Request.QueryString("e_date").ToString() = "", DateTime.Now, Request.QueryString("e_date").ToString())), SqlDbType.DateTime)

            Else
                oColSqlparram.Add("@date2", DateTime.Now, SqlDbType.DateTime)

            End If

            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@product_id", Val(Request.QueryString("pid").ToString()), SqlDbType.Int)
            oColSqlparram.Add("@category_id", Val(Request.QueryString("cID").ToString()), SqlDbType.Int)
            oColSqlparram.Add("@Duration", Request.QueryString("D").ToString(), SqlDbType.NVarChar)
            oColSqlparram.Add("@type", Request.QueryString("type").ToString(), SqlDbType.NVarChar)
            oColSqlparram.Add("@Product_Return", Val(Request.QueryString("PR").ToString()), SqlDbType.Int)
            oColSqlparram.Add("@Location_Id", Val(Request.QueryString("LID").ToString()), SqlDbType.Int)
            oColSqlparram.Add("@Discount", Request.QueryString("Dis").ToString())
            oColSqlparram.Add("@other", Request.QueryString("other").ToString())
            oColSqlparram.Add("@cust_id", Val(Request.QueryString("cusId").ToString()), SqlDbType.Int)

            If Request.QueryString("deptid") IsNot Nothing Then
                oColSqlparram.Add("@deptid", Val(Request.QueryString("deptid").ToString()), SqlDbType.Int)
            End If

            dt = oClsDal.GetdataTableSp("Get_Product_Transaction_detail", oColSqlparram)

            If dt.Rows.Count > 0 Then
                rdProductDetail.DataSource = dt
                rdProductDetail.DataBind()

                rdProductDetailOther.DataSource = dt
                rdProductDetailOther.DataBind()

            Else
                rdProductDetail.DataSource = String.Empty
                rdProductDetail.DataBind()

                rdProductDetailOther.DataSource = String.Empty
                rdProductDetailOther.DataBind()

            End If

            If Request.QueryString("other").ToString() = "1" Then
                divOther.Visible = True
                divPGroup.Visible = False

            Else
                divOther.Visible = False
                divPGroup.Visible = True

            End If


        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Transaction_detail:bindGrid:" + ex.Message)
        End Try


    End Sub

    Protected Sub lnkview_Click(sender As Object, e As EventArgs)
        Try

            Dim btn As LinkButton = DirectCast(sender, LinkButton)
            Dim tranuuid As String = btn.CommandArgument
            Dim dt As DataTable
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@tran_uuid", tranuuid)
            dt = oClsDal.GetdataTableSp("Transaction_Details_View", oColSqlparram)

            If (dt.Rows.Count > 0) Then

                Session("dt") = dt

                Dim url As String = "Transaction_Detail.aspx"

                ClientScript.RegisterStartupScript(Me.GetType(), "OpenWin", "<script>openNewWin('" & url & "')</script>")

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Product_Transaction_detail:lnkview_Click:" + ex.Message)
        End Try

    End Sub
End Class

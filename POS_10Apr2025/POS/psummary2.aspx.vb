Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Partial Class psummary2
    Inherits System.Web.UI.Page
    Dim oclsBind As New clsBinding
    Dim oclsSales As New Controller_clsSales()
    Dim oclsRole As Controller_clsRole

    Public Sub BindData()
        Try
            Dim oClsDal As ClsDataccess = New ClsDataccess()
            Dim ds As New DataSet

            Dim oColSqlparram As ColSqlparram = New ColSqlparram()
            oColSqlparram.Add("@date1", System.DateTime.Now, SqlDbType.DateTime)
            oColSqlparram.Add("@date2", System.DateTime.Now, SqlDbType.DateTime)
            oColSqlparram.Add("@cmp_id", Val(Session("cmp_id")), SqlDbType.Int)
            oColSqlparram.Add("@product_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@category_id", 0, SqlDbType.Int)
            oColSqlparram.Add("@Duration", "This Year", SqlDbType.NVarChar)
            oColSqlparram.Add("@type", "ALL", SqlDbType.NVarChar)

            Dim dt As DataTable = oClsDal.GetdataTableSp("P_R_Product_New", oColSqlparram)



            rptProductSUmmary.DataSource = dt
            rptProductSUmmary.DataBind()

        Catch ex As Exception
            Dim err As String = ex.Message.ToString()
        End Try
    End Sub

    Private Sub PSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        BindData()
    End Sub

End Class

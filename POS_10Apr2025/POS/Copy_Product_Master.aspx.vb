Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Class Copy_Product_Master
    Inherits BaseClass
    Dim oclsProduct As New Controller_clsProduct()
    Dim oclsTax As New Controller_clsTax()
    Dim oclsSize As New Controller_clsSize()
    Dim oclsPrice As New Controller_clsPrice()
    Dim oclsUnit As New Controller_clsUnit()
    Dim oclsBarcodeSize As New Controller_clsBarcodeSize()
    Dim oclsProductIngredient As New Controller_clsProductIngredient()
    Dim oclsCondiment As New Controller_clsCondiment()
    Dim oclsProductCondiment As New Controller_clsProductCodiment()
    Dim oClsDal As ClsDataccess = New ClsDataccess()
    Dim oclsBind As New clsBinding

    Protected Sub bindProduct()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.product_id = 0
            Dim dtProduct As DataTable = oclsProduct.Select()

            If dtProduct.Rows.Count > 0 Then
                rdlocation.DataSource = dtProduct
                rdlocation.DataTextField = "name"
                rdlocation.DataValueField = "product_id"
                rdlocation.DataBind()
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:bindAutocompateIngredient:" + ex.Message)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'bindProduct()
                oclsBind.BindProduct(rdlocation, Val(Session("cmp_id")))
                'rdlocation.Attributes.Add("onclick", "radioMe(event);")
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
           
            oclsProduct.product_id = rdlocation.SelectedValue
            oclsProduct.name = txtPName.Text
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            oclsProduct.Copy_Insert()

            'Session("product_id") = Nothing
            Session("Update") = "Record Copy successfully"
            Response.Redirect("Product_List.aspx", False)

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Copy_Product_Master:btnSave_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Product_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Copy_Product_Master:btnCancel_Click" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Clear()
        Catch ex As Exception
            LogHelper.Error("Product_Master:btnReset_Click:" + ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            txtPName.Text = ""
            rdlocation.SelectedValue = 0
        Catch ex As Exception
            LogHelper.Error("Product_Master:Clear:" + ex.Message)
        End Try
    End Sub

End Class

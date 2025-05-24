Imports System.Data
Partial Class Demo_listbox_1
    'Inherits ITemplate
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
    Dim oclsAllergy As New Controller_clsAllergy()
    Public oSessionManager As New SessionManager
    Dim objclProdImg As New Controller_clsProductImg()
    Dim DefStr As String = "dt_"
    Dim Page_Name As String = "Show_Product_ForIngredient"
    Dim dtPrc As DataTable


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            oclsBind.BindPrinter(radPrinter, Val(Session("cmp_id")))

        Catch ex As Exception
            LogHelper.Error("Product_Master:Page_Load:" + ex.Message)
        End Try
    End Sub


End Class

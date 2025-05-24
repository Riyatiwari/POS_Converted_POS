Imports System.Data
Imports Telerik.Web.UI
'Imports Telerik.Reporting

Partial Class Downloads
    Inherits BaseClass
    Dim oClsDataccess As New ClsDataccess()
    Dim oclsRole As Controller_clsRole
    Dim oclsTax As New Controller_clsTax

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Not Session("success") = Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + Session("success").ToString() + "');", True)
                    Session("success") = Nothing
                End If



            End If

        Catch ex As Exception
            LogHelper.Error("Tax_List:Page_Load:" + ex.Message)
        End Try
    End Sub

End Class

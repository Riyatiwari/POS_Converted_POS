Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class support_Module_List
    Inherits BaseClass
    Dim oclsSales As New Controller_clsModule()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            LogHelper.Error("Module_List:Page_load:" + ex.Message)
        End Try
    End Sub

    Private Sub btnEnableBooking_Click(sender As Object, e As EventArgs) Handles btnEnableBooking.Click
        Try
            oclsSales.booking_Default()
        Catch ex As Exception
            LogHelper.Error("Module_List:Linkbutton1:" + ex.Message)
        End Try
    End Sub

    Private Sub btnDefaultImage_Click(sender As Object, e As EventArgs) Handles btnDefaultImage.Click
        Try
            oclsSales.default_image_web_sales()
        Catch ex As Exception
            LogHelper.Error("Module_List:Linkbutton2:" + ex.Message)
        End Try

    End Sub


End Class

Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Partial Class Kiosk_Settings
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsKioskSetting As New Controller_clsKioskSetting()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Not Session("machine_id") = Nothing Then
                    BindKioskSettings()
                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings : Page_Load: " + ex.Message)
        End Try
    End Sub

    Private Sub BindKioskSettings()
        Try
            oclsKioskSetting.cmp_id = Val(Session("cmp_id"))
            oclsKioskSetting.machine_id = Val(Session("machine_id"))
            Dim dt As DataTable = oclsKioskSetting.Select()
            If dt.Rows.Count > 0 Then
                ddlBG_Style.SelectedValue = dt.Rows(0)("BG_style").ToString()
                If ddlBG_Style.SelectedValue = 1 Then

                    BG_clr.Visible = True
                    BG_G_clr.Visible = False
                    BG_img.Visible = False

                ElseIf ddlBG_Style.SelectedValue = 2 Then
                    BG_clr.Visible = False
                    BG_G_clr.Visible = True
                    BG_img.Visible = False
                Else
                    BG_clr.Visible = False
                    BG_G_clr.Visible = False
                    BG_img.Visible = True
                End If

                radBG_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("BG_color"))
                radBG_G_clr1.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("BG_g_clr_1"))
                radBG_G_clr2.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("BG_g_clr_2"))
                radBG_G_clr3.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("BG_g_clr_3"))

                radQty_BG_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("QtyBtn_BG_clr"))
                radQty_border_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("QtyBtn_Border_clr"))
                radQty_FontClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("Qty_Text_clr"))
                Qty_lbl.Checked = True 'Is_Qty_label
                ddl_QtyFontSize.SelectedValue = dt.Rows(0)("Qty_FontSize").ToString()

                radPName_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("ProductName_clr"))
                ddlPName_size.SelectedValue = dt.Rows(0)("ProductName_FontSize").ToString()

                radSHI_backClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("StaticInfoBtn_BG_clr"))
                radSHI_fontClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("StaticInfoBtn_Text_clr"))

                radTMA_btn_back_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtBtn_BG_clr"))
                radTMA_btn_border_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtBtn_Border_clr"))
                radTMA_btn_font_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtBtn_Text_clr"))

                radTMA_HeaderClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtPopUp_Header_clr"))
                ddl_popupBtnStyle.SelectedValue = dt.Rows(0)("TellMeAbtPopUp_OkBtn_Style").ToString()

                txtHeaderText.Text = dt.Rows(0)("Header_text").ToString()
                rcpHeaderTextClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("Header_text_clr"))
                txtPaymentText.Text = dt.Rows(0)("payment_text").ToString()
                rcpPaymentTextClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("payment_text_clr"))

                If ddl_popupBtnStyle.SelectedValue = 1 Then

                    divTMA_clr.Visible = True
                    divTMA_G_clr.Visible = False

                ElseIf ddl_popupBtnStyle.SelectedValue = 2 Then
                    divTMA_clr.Visible = False
                    divTMA_G_clr.Visible = True

                End If

                radTMABtn_clr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtPopUp_OkBtn_clr"))
                radTMABtnGradient_clr1.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtPopUp_OkBtn_g_clr_1"))
                radTMABtnGradient_clr2.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtPopUp_OkBtn_g_clr_2"))
                radTMABtnGradient_clr3.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("TellMeAbtPopUp_OkBtn_g_clr_3"))

                radC_BtnClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("ConfirmBtn_clr"))
                radC_BtnBorderClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("ConfirmBtn_Border_clr"))
                radC_BtnFontClr.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("ConfirmBtn_Text_clr"))
                radC_Btn_GClr1.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("ConfirmBtn_g_clr_1"))
                radC_Btn_GClr2.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("ConfirmBtn_g_clr_2"))
                radC_Btn_GClr3.SelectedColor = ColorTranslator.FromHtml(dt.Rows(0)("ConfirmBtn_g_clr_3"))
                ddlConfirm_BtnStyle.SelectedValue = dt.Rows(0)("ConfirmBtn_Style").ToString()

                If ddlConfirm_BtnStyle.SelectedValue = 1 Then

                    divC_btnClr.Visible = True
                    divC_btn_GClr.Visible = False

                ElseIf ddlConfirm_BtnStyle.SelectedValue = 2 Then
                    divC_btnClr.Visible = False
                    divC_btn_GClr.Visible = True

                End If

                If dt.Rows(0)("BG_Image") IsNot DBNull.Value Then
                    Image1.Visible = True
                    Dim bytes As Byte() = DirectCast(dt.Rows(0)("BG_Image"), Byte())
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                    ViewState("BG_Image") = dt.Rows(0)("BG_Image")
                End If

            End If

        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings: BindKioskSettings: " + ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            oclsKioskSetting.machine_id = Val(Session("machine_id"))
            oclsKioskSetting.cmp_id = Val(Session("cmp_id"))
            oclsKioskSetting.BG_style = ddlBG_Style.SelectedValue
            If ddlBG_Style.SelectedValue = 1 Then
                oclsKioskSetting.BG_color = ColorTranslator.ToHtml(radBG_clr.SelectedColor)
                oclsKioskSetting.BG_g_clr_1 = ""
                oclsKioskSetting.BG_g_clr_2 = ""
                oclsKioskSetting.BG_g_clr_3 = ""
            ElseIf ddlBG_Style.SelectedValue = 2 Then
                oclsKioskSetting.BG_color = ""
                oclsKioskSetting.BG_g_clr_1 = ColorTranslator.ToHtml(radBG_G_clr1.SelectedColor)
                oclsKioskSetting.BG_g_clr_2 = ColorTranslator.ToHtml(radBG_G_clr2.SelectedColor)
                oclsKioskSetting.BG_g_clr_3 = ColorTranslator.ToHtml(radBG_G_clr3.SelectedColor)
            Else
                oclsKioskSetting.BG_color = ""
                oclsKioskSetting.BG_g_clr_1 = ""
                oclsKioskSetting.BG_g_clr_2 = ""
                oclsKioskSetting.BG_g_clr_3 = ""
            End If

            oclsKioskSetting.QtyBtn_BG_clr = ColorTranslator.ToHtml(radQty_BG_clr.SelectedColor)
            oclsKioskSetting.QtyBtn_Border_clr = ColorTranslator.ToHtml(radQty_border_clr.SelectedColor)
            oclsKioskSetting.QtyBtn_Text_clr = ColorTranslator.ToHtml(radQty_border_clr.SelectedColor)
            oclsKioskSetting.Qty_BG_clr = ColorTranslator.ToHtml(radQty_BG_clr.SelectedColor)
            oclsKioskSetting.Qty_Text_clr = ColorTranslator.ToHtml(radQty_FontClr.SelectedColor)
            oclsKioskSetting.Qty_FontSize = ddl_QtyFontSize.SelectedValue
            oclsKioskSetting.Is_Qty_label = IIf(Qty_lbl.Checked = True, 1, 0)
            oclsKioskSetting.StaticInfoBtn_BG_clr = ColorTranslator.ToHtml(radSHI_backClr.SelectedColor)
            oclsKioskSetting.StaticInfoBtn_Text_clr = ColorTranslator.ToHtml(radSHI_fontClr.SelectedColor)

            oclsKioskSetting.TellMeAbtBtn_BG_clr = ColorTranslator.ToHtml(radTMA_btn_back_clr.SelectedColor)
            oclsKioskSetting.TellMeAbtBtn_Border_clr = ColorTranslator.ToHtml(radTMA_btn_border_clr.SelectedColor)
            oclsKioskSetting.TellMeAbtBtn_Text_clr = ColorTranslator.ToHtml(radTMA_btn_font_clr.SelectedColor)

            oclsKioskSetting.TellMeAbtPopUp_Header_clr = ColorTranslator.ToHtml(radTMA_HeaderClr.SelectedColor)
            oclsKioskSetting.TellMeAbtPopUp_OkBtn_Style = ddl_popupBtnStyle.SelectedValue
            If ddl_popupBtnStyle.SelectedValue = 1 Then
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_clr = ColorTranslator.ToHtml(radTMABtn_clr.SelectedColor)
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_1 = ""
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_2 = ""
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_3 = ""

            ElseIf ddl_popupBtnStyle.SelectedValue = 2 Then
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_clr = ""
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_1 = ColorTranslator.ToHtml(radTMABtnGradient_clr1.SelectedColor)
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_2 = ColorTranslator.ToHtml(radTMABtnGradient_clr2.SelectedColor)
                oclsKioskSetting.TellMeAbtPopUp_OkBtn_g_clr_3 = ColorTranslator.ToHtml(radTMABtnGradient_clr3.SelectedColor)

            End If


            oclsKioskSetting.ProductName_clr = ColorTranslator.ToHtml(radPName_clr.SelectedColor)
            oclsKioskSetting.ProductName_FontSize = ddlPName_size.SelectedValue

            oclsKioskSetting.ConfirmBtn_Style = ddlConfirm_BtnStyle.SelectedValue

            If ddlConfirm_BtnStyle.SelectedValue = 1 Then
                oclsKioskSetting.ConfirmBtn_clr = ColorTranslator.ToHtml(radC_BtnClr.SelectedColor)
                oclsKioskSetting.ConfirmBtn_g_clr_1 = ""
                oclsKioskSetting.ConfirmBtn_g_clr_2 = ""
                oclsKioskSetting.ConfirmBtn_g_clr_3 = ""

            ElseIf ddlConfirm_BtnStyle.SelectedValue = 2 Then
                oclsKioskSetting.ConfirmBtn_clr = ""
                oclsKioskSetting.ConfirmBtn_g_clr_1 = ColorTranslator.ToHtml(radC_Btn_GClr1.SelectedColor)
                oclsKioskSetting.ConfirmBtn_g_clr_2 = ColorTranslator.ToHtml(radC_Btn_GClr2.SelectedColor)
                oclsKioskSetting.ConfirmBtn_g_clr_3 = ColorTranslator.ToHtml(radC_Btn_GClr3.SelectedColor)
            End If

            oclsKioskSetting.ConfirmBtn_Border_clr = ColorTranslator.ToHtml(radC_BtnBorderClr.SelectedColor)
            oclsKioskSetting.ConfirmBtn_Text_clr = ColorTranslator.ToHtml(radC_BtnFontClr.SelectedColor)

            oclsKioskSetting.Header_text = txtHeaderText.Text.ToString()
            oclsKioskSetting.Header_text_clr = ColorTranslator.ToHtml(rcpHeaderTextClr.SelectedColor)
            oclsKioskSetting.payment_text = txtPaymentText.Text.ToString()
            oclsKioskSetting.payment_text_clr = ColorTranslator.ToHtml(rcpPaymentTextClr.SelectedColor)

            Dim bytes As Byte()
            If fileupload.HasFile Then
                Using br As BinaryReader = New BinaryReader(fileupload.PostedFile.InputStream)
                    bytes = br.ReadBytes(fileupload.PostedFile.ContentLength)
                End Using
            Else
                bytes = DirectCast(ViewState("BG_Image"), Byte())
            End If
            oclsKioskSetting.BG_Image = bytes

            Dim obj As New Controller_clsKioskSetting()
            obj.cmp_id = Val(Session("cmp_id"))
            obj.machine_id = Val(Session("machine_id"))
            Dim dt As DataTable = obj.Select()
            If dt.Rows.Count > 0 Then

                oclsKioskSetting.Update()
                Session("Success") = "Kiosk Settings updated successfully"
            Else

                oclsKioskSetting.Insert()
                Session("Success") = "Kiosk Settings inserted successfully"
            End If

            Response.Redirect("Machine_Master.aspx", False)

        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings: btnSave_Click :" + ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Try

            Response.Redirect("Kiosk_Settings.aspx", False)

        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings: btnReset_Click :" + ex.Message)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("Machine_Master.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings: btnCancel_Click :" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlBG_Style_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            If ddlBG_Style.SelectedValue = 1 Then

                BG_clr.Visible = True
                BG_G_clr.Visible = False
                BG_img.Visible = False

            ElseIf ddlBG_Style.SelectedValue = 2 Then
                BG_clr.Visible = False
                BG_G_clr.Visible = True
                BG_img.Visible = False
            Else
                BG_clr.Visible = False
                BG_G_clr.Visible = False
                BG_img.Visible = True
            End If
        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings: ddlBG_Style_SelectedIndexChanged :" + ex.Message)
        End Try
    End Sub

    Protected Sub ddl_popupBtnStyle_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            If ddl_popupBtnStyle.SelectedValue = 1 Then

                divTMA_clr.Visible = True
                divTMA_G_clr.Visible = False

            ElseIf ddl_popupBtnStyle.SelectedValue = 2 Then
                divTMA_clr.Visible = False
                divTMA_G_clr.Visible = True

            End If

        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings: ddl_popupBtnStyle_SelectedIndexChanged :" + ex.Message)
        End Try
    End Sub

    Protected Sub ddlConfirm_BtnStyle_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            If ddlConfirm_BtnStyle.SelectedValue = 1 Then

                divC_btnClr.Visible = True
                divC_btn_GClr.Visible = False

            ElseIf ddlConfirm_BtnStyle.SelectedValue = 2 Then
                divC_btnClr.Visible = False
                divC_btn_GClr.Visible = True

            End If

        Catch ex As Exception
            LogHelper.Error("Kiosk_Settings: ddlConfirm_BtnStyle_SelectedIndexChanged :" + ex.Message)
        End Try
    End Sub
End Class

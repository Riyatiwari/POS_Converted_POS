Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Imports System.Net
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports System.IO

Partial Class Location_Master
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim objclsLocation As New clsLocation()
    Dim oclsLocation As New Controller_clsLocation()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If

                If Session("IsPaymentGtway") IsNot Nothing Then
                    If Val(Session("IsPaymentGtway")) = 1 Then

                        div_PGtway.Visible = True
                        BindStore()
                    Else
                        div_PGtway.Visible = False

                    End If
                End If

                oclsBind.BindCountry(radCountry)
                oclsBind.BindVenue1(radVenue, Val(Session("cmp_id")))

                divJudoPayid.Visible = False
                divJudoTokenid.Visible = False
                divJudoSecretid.Visible = False
                divCashflowId.Visible = False
                divCashflowUrl.Visible = False
                divCashflowAPIKey.Visible = False
                divCustomPayId.Visible = False
                divCustomPaySecret.Visible = False
                divCustomPayToken.Visible = False
                divCustomPayBase64.Visible = False
                divCustomPayURL.Visible = False

                If Not Session("location_id") = Nothing Then
                    BindLocation()

                    txtlCode.ReadOnly = True
                End If

            End If
            If tipas.SelectedValue = "Cashback" Then
                cashback.Visible = True
                lblcashback.Visible = True
                'cashback.Checked = True
            Else
                cashback.Visible = False
                lblcashback.Visible = False
                cashback.Checked = False

            End If

            If Val(txtsvccharg.Text.Trim()) > 0.00 Then
                chkclick.Visible = True
                chkkiosk.Visible = True
                chkorder.Visible = True
                chkdelivery.Visible = True
            Else
                chkclick.Visible = False
                chkkiosk.Visible = False
                chkorder.Visible = False
                chkdelivery.Visible = False
                chkclick.Checked = False
                chkkiosk.Checked = False
                chkorder.Checked = False
                chkdelivery.Checked = False
            End If
        Catch ex As Exception
            LogHelper.Error("Location_Master:Page_Load" + ex.Message)
        End Try
    End Sub

    Private Sub BindLocation()
        Try
            Dim sender As Object
            Dim e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            oclsLocation.location_id = Val(Session("location_id"))
            Dim dtLocation As DataTable = oclsLocation.Select()
            If dtLocation.Rows.Count > 0 Then

                'txt_tblASbox.Text = dtLocation.Rows(0)("Table_As_Box").ToString

                txtlCode.Text = dtLocation.Rows(0)("code").ToString
                txtName.Text = dtLocation.Rows(0)("name").ToString
                txtAddress.Text = dtLocation.Rows(0)("address").ToString
                txtsarttime.Text = dtLocation.Rows(0)("start_date").ToString
                textendtime.Text = dtLocation.Rows(0)("end_date").ToString
                radVenue.ClearSelection()
                radVenue.SelectedValue = Val(dtLocation.Rows(0)("venue_id"))
                Try
                    radCountry.ClearSelection()
                    radCountry.FindItemByValue(Val(dtLocation.Rows(0)("country_id"))).Selected = True
                    radCountry_SelectedIndexChanged(sender, e)

                    If Not Val(dtLocation.Rows(0)("state_id")) = 0 Then

                        radState.ClearSelection()
                        radState.FindItemByValue(Val(dtLocation.Rows(0)("state_id"))).Selected = True
                        radState_SelectedIndexChanged(sender, e)
                    End If
                    If Not Val(dtLocation.Rows(0)("city_id")) = 0 Then
                        radCity.ClearSelection()
                        radCity.FindItemByValue(Val(dtLocation.Rows(0)("city_id"))).Selected = True
                        ddljudo.ClearSelection()
                    End If

                    If Not Val(dtLocation.Rows(0)("onlinepayment")) = Nothing Then

                        ddljudo.FindItemByValue(Val(dtLocation.Rows(0)("onlinepayment"))).Selected = True
                        If ((dtLocation.Rows(0)("onlinepayment").ToString() = "1")) Then
                            divJudoPayid.Visible = True
                            divJudoTokenid.Visible = True
                            divJudoSecretid.Visible = True
                        ElseIf ((dtLocation.Rows(0)("onlinepayment").ToString() = "2")) Then
                            divCashflowId.Visible = True
                            divCashflowUrl.Visible = True
                            divCashflowAPIKey.Visible = True
                        ElseIf ((dtLocation.Rows(0)("onlinepayment").ToString() = "3")) Then
                            divCustomPayId.Visible = True
                            divCustomPaySecret.Visible = True
                            divCustomPayToken.Visible = True
                            divCustomPayBase64.Visible = True
                            divCustomPayURL.Visible = True
                        ElseIf ((dtLocation.Rows(0)("onlinepayment").ToString() = "4")) Then
                            divCashflowId.Visible = True
                            divCashflowUrl.Visible = True
                            divCashflowAPIKey.Visible = True
                        ElseIf ((dtLocation.Rows(0)("onlinepayment").ToString() = "5")) Then
                            divCashflowId.Visible = True
                            divCashflowUrl.Visible = True
                            divCashflowAPIKey.Visible = True
                        End If

                    End If

                Catch ex As Exception
                    LogHelper.Error("Location_Master:BindLocation" + ex.Message)
                End Try
                If dtLocation.Rows(0)("till_auto_log_off") = "Yes" Then
                    chkTillAutoLogOff.Checked = True
                Else
                    chkTillAutoLogOff.Checked = False
                End If

                If dtLocation.Rows(0)("Active").ToString() = "Yes" Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

                If dtLocation.Rows(0)("cashback").ToString() = "Yes" Then
                    cashback.Checked = True
                Else
                    cashback.Checked = False
                End If
                If dtLocation.Rows(0)("srv_charge_delivery").ToString() = "Yes" Then
                    chkdelivery.Checked = True
                Else
                    chkdelivery.Checked = False
                End If
                If dtLocation.Rows(0)("srv_charge_order").ToString() = "Yes" Then
                    chkorder.Checked = True
                Else
                    chkorder.Checked = False
                End If
                If dtLocation.Rows(0)("srv_charge_kiosk").ToString() = "Yes" Then
                    chkkiosk.Checked = True
                Else
                    chkkiosk.Checked = False
                End If
                If dtLocation.Rows(0)("srv_charge_clickcollect").ToString() = "Yes" Then
                    chkclick.Checked = True
                Else
                    chkclick.Checked = False
                End If








                If dtLocation.Rows(0)("is_GetCovers").ToString() = "Yes" Then
                    isGetCovers.Checked = True
                Else
                    isGetCovers.Checked = False
                End If


                txtMinAmount.Text = dtLocation.Rows(0)("min_amount").ToString
                If dtLocation.Rows(0)("is_delivery").ToString() = "Yes" Then
                    chkisDelivery.Checked = True
                Else
                    chkisDelivery.Checked = False
                End If
                If dtLocation.Rows(0)("websales_check_schedule").ToString() = "Yes" Then
                    chckwebsales.Checked = True
                Else
                    chckwebsales.Checked = False
                End If
                If dtLocation.Rows(0)("schedule_required").ToString() = "Yes" Then
                    chkScheRequired.Checked = True
                Else
                    chkScheRequired.Checked = False
                End If

                If dtLocation.Rows(0)("schedule_cnc").ToString() = "Yes" Then
                    chkScnc.Checked = True
                Else
                    chkScnc.Checked = False
                End If

                If dtLocation.Rows(0)("HourlySlot").ToString() = "Yes" Then
                    chkHourlySlot.Checked = True
                Else
                    chkHourlySlot.Checked = False
                End If

                ddlBtnStyle_GC.SelectedValue = dtLocation.Rows(0)("GC_Btn_stl").ToString()
                ddlImgType_GC.SelectedValue = dtLocation.Rows(0)("GC_Btn_img_typ").ToString()
                ddl_QtyFontSize_GC.SelectedValue = dtLocation.Rows(0)("GC_Btn_fnt_size").ToString()
                radQty_BG_clr_GC.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("GC_Btn_bkgd_clr").ToString())
                radtxt_clr_GC.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("GC_Btn_txt_clr").ToString())
                login_bg_clr.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("login_src_bkgd_clr").ToString())
                login_sr_clr.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("login_src_login_btn").ToString())
                txtcontactTill.Text = dtLocation.Rows(0)("Till_phn_no").ToString
                txtUrl.Text = dtLocation.Rows(0)("till_url").ToString


                ddlBtnStyle_C.SelectedValue = dtLocation.Rows(0)("C_Btn_stl").ToString()
                ddlImgType_C.SelectedValue = dtLocation.Rows(0)("C_Btn_img_typ").ToString()
                ddl_QtyFontSize_C.SelectedValue = dtLocation.Rows(0)("C_Btn_fnt_size").ToString()
                radQty_BG_clr_C.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("C_Btn_bkgd_clr").ToString())
                radtxt_clr_C.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("C_Btn_txt_clr").ToString())

                ddlBtnStyle_P.SelectedValue = dtLocation.Rows(0)("P_Btn_stl").ToString()
                ddlImgType_P.SelectedValue = dtLocation.Rows(0)("P_Btn_img_typ").ToString()
                ddl_QtyFontSize_P.SelectedValue = dtLocation.Rows(0)("P_Btn_fnt_size").ToString()
                radQty_BG_clr_P.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("P_Btn_bkgd_clr").ToString())
                radtxt_clr_P.SelectedColor = System.Drawing.ColorTranslator.FromHtml(dtLocation.Rows(0)("P_Btn_txt_clr").ToString())


                ddl_ImageOption_GC.SelectedValue = dtLocation.Rows(0)("GC_Btn_img_typ_pos").ToString()
                ddl_ImageOption_C.SelectedValue = dtLocation.Rows(0)("C_Btn_img_typ_pos").ToString()
                ddl_ImageOption_P.SelectedValue = dtLocation.Rows(0)("P_Btn_img_typ_pos").ToString()

                txtJudoId.Text = dtLocation.Rows(0)("judo_id").ToString
                txtJudoToken.Text = dtLocation.Rows(0)("judo_token").ToString
                txtjudosecret.Text = dtLocation.Rows(0)("judo_secret").ToString
                txtCashflowId.Text = dtLocation.Rows(0)("config_id").ToString
                txtCashflowUrl.Text = dtLocation.Rows(0)("url").ToString
                txtCashflowAPIKey.Text = dtLocation.Rows(0)("api_key").ToString

                txtCustomPayId.Text = dtLocation.Rows(0)("CustomPay_Id").ToString
                txtCustomPaySecret.Text = dtLocation.Rows(0)("CustomPay_Secret").ToString
                txtCustomPayToken.Text = dtLocation.Rows(0)("CustomPay_Token").ToString
                txtCustomPayBase64.Text = dtLocation.Rows(0)("CustomPay_Base64").ToString
                txtCustomPayURL.Text = dtLocation.Rows(0)("CustomPay_url").ToString

                txt_clientid.Text = dtLocation.Rows(0)("clientid").ToString
                txt_clientsecret.Text = dtLocation.Rows(0)("clientsecret").ToString
                txt_redirect_uri.Text = dtLocation.Rows(0)("redirect_uri").ToString

                If dtLocation.Rows(0)("onlinepayment").ToString = "1" Then
                    ddljudo.SelectedValue = 1
                    divJudoPayid.Visible = True
                    divJudoTokenid.Visible = True
                    divJudoSecretid.Visible = True
                    divCashflowId.Visible = False
                    divCashflowUrl.Visible = False
                    divCashflowAPIKey.Visible = False
                    divCustomPayId.Visible = False
                    divCustomPaySecret.Visible = False
                    divCustomPayToken.Visible = False
                    divCustomPayBase64.Visible = False
                    divCustomPayURL.Visible = False
                ElseIf dtLocation.Rows(0)("onlinepayment").ToString = "2" Then
                    ddljudo.SelectedValue = 2
                    divJudoPayid.Visible = False
                    divJudoTokenid.Visible = False
                    divJudoSecretid.Visible = False
                    divCashflowId.Visible = True
                    divCashflowUrl.Visible = True
                    divCashflowAPIKey.Visible = True
                    divCustomPayId.Visible = False
                    divCustomPaySecret.Visible = False
                    divCustomPayToken.Visible = False
                    divCustomPayBase64.Visible = False
                    divCustomPayURL.Visible = False
                ElseIf dtLocation.Rows(0)("onlinepayment").ToString = "3" Then
                    ddljudo.SelectedValue = 3
                    divJudoPayid.Visible = False
                    divJudoTokenid.Visible = False
                    divJudoSecretid.Visible = False
                    divCashflowId.Visible = False
                    divCashflowUrl.Visible = False
                    divCashflowAPIKey.Visible = False
                    divCustomPayId.Visible = True
                    divCustomPaySecret.Visible = True
                    divCustomPayToken.Visible = True
                    divCustomPayBase64.Visible = True
                    divCustomPayURL.Visible = True
                ElseIf dtLocation.Rows(0)("onlinepayment").ToString = "4" Then
                    ddljudo.SelectedValue = 4
                    divJudoPayid.Visible = False
                    divJudoTokenid.Visible = False
                    divJudoSecretid.Visible = False
                    divCashflowId.Visible = True
                    divCashflowUrl.Visible = True
                    divCashflowAPIKey.Visible = True
                    divCustomPayId.Visible = False
                    divCustomPaySecret.Visible = False
                    divCustomPayToken.Visible = False
                    divCustomPayBase64.Visible = False
                    divCustomPayURL.Visible = False
                ElseIf dtLocation.Rows(0)("onlinepayment").ToString = "5" Then
                    ddljudo.SelectedValue = 5
                    divJudoPayid.Visible = False
                    divJudoTokenid.Visible = False
                    divJudoSecretid.Visible = False
                    divCashflowId.Visible = True
                    divCashflowUrl.Visible = True
                    divCashflowAPIKey.Visible = True
                    divCustomPayId.Visible = False
                    divCustomPaySecret.Visible = False
                    divCustomPayToken.Visible = False
                    divCustomPayBase64.Visible = False
                    divCustomPayURL.Visible = False
                Else
                    ddljudo.SelectedValue = -1
                    divJudoPayid.Visible = False
                    divJudoTokenid.Visible = False
                    divJudoSecretid.Visible = False
                    divCashflowId.Visible = False
                    divCashflowUrl.Visible = False
                    divCashflowAPIKey.Visible = False
                    divCustomPayId.Visible = False
                    divCustomPaySecret.Visible = False
                    divCustomPayToken.Visible = False
                    divCustomPayBase64.Visible = False
                    divCustomPayURL.Visible = False
                End If

                If dtLocation.Rows(0)("is_skippay").ToString() = "Yes" Then
                    chkSkipPay.Checked = True
                Else
                    chkSkipPay.Checked = False
                End If

                If dtLocation.Rows(0)("is_email").ToString() = "Yes" Then
                    chkReceipt.Checked = True
                Else
                    chkReceipt.Checked = False
                End If

                If dtLocation.Rows(0)("Is_Email_APK").ToString() = "Yes" Then
                    chkEmailRec_APK.Checked = True
                Else
                    chkEmailRec_APK.Checked = False
                End If



                hdnOldName.Value = dtLocation.Rows(0)("name").ToString()

            End If

            txtEmail.Text = dtLocation.Rows(0)("LoctionEmail").ToString
            txtContact.Text = dtLocation.Rows(0)("LocationContact").ToString
            txtReciptHeader.Content = dtLocation.Rows(0)("header_reciept").ToString
            txtmessage_delivery.Content = dtLocation.Rows(0)("message_delivery").ToString
            txtmessage_order_table.Content = dtLocation.Rows(0)("message_order_table").ToString
            txtgtway_MerchantID.Text = dtLocation.Rows(0)("gtway_MerchantID").ToString

            '---------------16/09/2022------------------------
            BindStore()
            rad_StoreName.ClearSelection()
            rad_StoreName.FindItemByValue(Val(dtLocation.Rows(0)("gtway_StoreID"))).Selected = True
            rad_StoreName_SelectedIndexChanged(sender, e)

            If Not Val(dtLocation.Rows(0)("gtway_LocationID")) = 0 Then
                rad_StoreLocation.ClearSelection()
                rad_StoreLocation.FindItemByValue(Val(dtLocation.Rows(0)("gtway_LocationID"))).Selected = True
            End If

            '---------------16/09/2022------------------------

            ViewState("logo") = dtLocation.Rows(0)("L_image")
            If dtLocation.Rows(0)("L_image") IsNot DBNull.Value Then
                Image1.Visible = True
                Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("L_image"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                hdlogo.Value = Convert.ToBase64String(bytes)

                ViewState("Image") = 1

                RadBinaryImage1.DataValue = Nothing

                'RadBinaryImage1.DataValue = dt.Rows(0)("image_name")

                'For i = 0 To dt.Rows.Count - 1

                '    RadBinaryImage1.DataValue = dt.Rows(i)("image_name")
                'Next
                RadGrid1.DataSource = dtLocation
                RadGrid1.DataBind()

            End If

            'If dtLocation.Rows(0)("poslite").ToString() = "Yes" Then
            '    chkPOSLite.Checked = True
            'Else
            '    chkPOSLite.Checked = False
            'End If

            'If dtLocation.Rows(0)("is_sunmi_second_screen").ToString() = "Yes" Then
            '    chkSecondScreen.Checked = True
            'Else
            '    chkSecondScreen.Checked = False
            'End If


            ViewState("GraphicViewBackground") = dtLocation.Rows(0)("GraphicViewBackground")
            If dtLocation.Rows(0)("GraphicViewBackground") IsNot DBNull.Value Then
                ImageGraphic.Visible = True
                Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("GraphicViewBackground"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                ImageGraphic.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                hdngraphic.Value = Convert.ToBase64String(bytes)

                ViewState("Image") = 1

                RadBinaryImageGraphic.DataValue = Nothing

                'RadBinaryImage1.DataValue = dt.Rows(0)("image_name")

                'For i = 0 To dt.Rows.Count - 1

                '    RadBinaryImage1.DataValue = dt.Rows(i)("image_name")
                'Next
                RadGridGraphic.DataSource = dtLocation
                RadGridGraphic.DataBind()

            End If



            ViewState("GraphicViewTable") = dtLocation.Rows(0)("GraphicViewTable")

            If dtLocation.Rows(0)("GraphicViewTable") IsNot DBNull.Value Then
                ImageTable.Visible = True
                Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("GraphicViewTable"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                ImageTable.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                hdnTable.Value = Convert.ToBase64String(bytes)

                ViewState("Image") = 1

                RadBinaryImageTable.DataValue = Nothing

                'RadBinaryImage1.DataValue = dt.Rows(0)("image_name")

                'For i = 0 To dt.Rows.Count - 1

                '    RadBinaryImage1.DataValue = dt.Rows(i)("image_name")
                'Next
                RadGridTable.DataSource = dtLocation
                RadGridTable.DataBind()
            End If

            If dtLocation.Rows(0)("BG_Color") IsNot DBNull.Value Then

                radBackcolorpicker_m.SelectedColor = ColorTranslator.FromHtml(dtLocation.Rows(0)("BG_Color"))
            End If
            If dtLocation.Rows(0)("Font_Color") IsNot DBNull.Value Then

                radForcolorpicker_m.SelectedColor = ColorTranslator.FromHtml(dtLocation.Rows(0)("Font_Color"))
            End If

            If dtLocation.Rows(0)("Body_Color") IsNot DBNull.Value Then

                radbodycolorpicker_m.SelectedColor = ColorTranslator.FromHtml(dtLocation.Rows(0)("Body_Color"))
            End If

            If dtLocation.Rows(0)("Click_Collect_image") IsNot DBNull.Value Then
                Imageclickcollect.Visible = True
                Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("Click_Collect_image"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                Imageclickcollect.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                hdlogo.Value = Convert.ToBase64String(bytes)

                ViewState("Image") = 1

                RadBinaryclickandcollect.DataValue = dtLocation.Rows(0)("Click_Collect_image")
            End If
            If dtLocation.Rows(0)("Delivery_image") IsNot DBNull.Value Then
                Imagedeliver.Visible = True
                Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("Delivery_image"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                Imagedeliver.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                hdlogo.Value = Convert.ToBase64String(bytes)

                ViewState("Image") = 1

                RadBinarydelivery.DataValue = dtLocation.Rows(0)("Delivery_image")
            End If

            If dtLocation.Rows(0)("POS_logo") IsNot DBNull.Value Then
                imagePOSLOGO.Visible = True
                Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("POS_logo"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                imagePOSLOGO.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                hdlogo.Value = Convert.ToBase64String(bytes)

                ViewState("Image") = 1
                ViewState("PosLogoImage") = dtLocation.Rows(0)("POS_logo")

                RadBinaryPOSLogo.DataValue = dtLocation.Rows(0)("POS_logo")
            End If


            'If dtLocation.Rows(0)("OrderAtTable_image") IsNot DBNull.Value Then
            '    Image7.Visible = True
            '    Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("OrderAtTable_image"), Byte())
            '    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            '    Image7.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

            '    hdlogo.Value = Convert.ToBase64String(bytes)

            '    ViewState("Image") = 1

            '    RadBinaryorderattable.DataValue = dtLocation.Rows(0)("OrderAtTable_image")
            'End If


            If dtLocation.Rows(0)("OrderAtTable_image") IsNot DBNull.Value Then
                Image8.Visible = True
                Dim bytes As Byte() = DirectCast(dtLocation.Rows(0)("OrderAtTable_image"), Byte())
                Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                Image8.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String

                hdlogo.Value = Convert.ToBase64String(bytes)

                ViewState("Image") = 1

                RadBinaryorderattable.DataValue = dtLocation.Rows(0)("OrderAtTable_image")
            End If
            If dtLocation.Rows(0)("click_and_collect") = "Yes" Then
                chkIsClickcollect.Checked = True
            Else
                chkIsClickcollect.Checked = False
            End If
            If dtLocation.Rows(0)("Order_table") = "Yes" Then
                chkIsOrdertable.Checked = True
            Else
                chkIsOrdertable.Checked = False
            End If
            txtDCharges.Text = dtLocation.Rows(0)("delivery_charges").ToString()
            txtsvccharg.Text = dtLocation.Rows(0)("service_charges").ToString()

            Radtheme.ClearSelection()

            Radtheme.SelectedValue = Val(dtLocation.Rows(0)("theme"))


            tipas.ClearSelection()
            tipas.SelectedValue = dtLocation.Rows(0)("tipAs").ToString
            If tipas.SelectedValue = "Cashback" Then
                cashback.Visible = True
                lblcashback.Visible = True
                'cashback.Checked = True
            Else
                cashback.Visible = False
                lblcashback.Visible = False
                cashback.Checked = False
            End If

            Dim str As String = WebConfigurationManager.AppSettings("wpos_URL") + "?sv=" + Session("Storename").ToString() + "&cv=" + Session("cmp_id").ToString() + "&lv=" + Session("location_id").ToString()
            aCLick.HRef = str.Replace("OptionDelivery", "GroupCategory") + "&dv=0"
            aDelivery.HRef = str.Replace("OptionDelivery", "GroupCategory") + "&dv=1"
            aOrder.HRef = str.Replace("OptionDelivery", "GroupCategory") + "&dv=2"
            akiosk.HRef = str.Replace("OptionDelivery", "GroupCategory") + "&dv=3"

            txtFutureBookingDays.Text = dtLocation.Rows(0)("future_booking_days").ToString()


            'ViewState("secondscreenImage1") = dtLocation.Rows(0)("second_screen_image_1")
            'ViewState("secondscreenImage2") = dtLocation.Rows(0)("second_screen_image_2")
            'ViewState("secondscreenvideo") = dtLocation.Rows(0)("sunmi_video_path")

            If dtLocation.Rows(0)("POS_logo") IsNot DBNull.Value Then
                imagePOSLOGO.Visible = True
                btnRemoveLogo.Visible = True
            Else
                imagePOSLOGO.Visible = False
                btnRemoveLogo.Visible = False
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Location_Master:BindLocation:" + ex.Message)
        End Try

    End Sub
    Private Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then

                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                oclsLocation.cmp_id = Val(Session("cmp_id"))
                oclsLocation.location_id = Val(Session("location_id"))
                oclsLocation.ImgDelete()

                Dim dtLocation As DataTable = oclsLocation.Select()
                If dtLocation.Rows.Count > 0 Then

                    If dtLocation.Rows(0)("L_image") IsNot DBNull.Value Then

                        RadGrid1.DataSource = dtLocation
                        RadGrid1.DataBind()
                    Else
                        RadGrid1.DataSource = ""
                        RadGrid1.DataBind()
                    End If

                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Product_Master: RadGrid1_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Protected Sub radCountry_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radCountry.SelectedIndexChanged
        Try
            If radCountry.SelectedIndex = 0 Then
                radState.Items.Clear()
                radCity.Items.Clear()
            Else
                oclsBind.BindState(radState, radCountry.SelectedValue)
                radCity.Items.Clear()
            End If
        Catch ex As Exception
            LogHelper.Error("Location_Master:radCountry_SelectedIndexChanged" + ex.Message)
        End Try


    End Sub

    Protected Sub radState_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radState.SelectedIndexChanged
        Try
            If radState.SelectedIndex = 0 Then
                radCity.Items.Clear()
            Else
                oclsBind.BindCity(radCity, radState.SelectedValue)
            End If
        Catch ex As Exception
            LogHelper.Error("Location_Master:radState_SelectedIndexChanged" + ex.Message)
        End Try

    End Sub
    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If Session("location_id") = Nothing Then
                Clear()
            Else
                BindLocation()
            End If
        Catch ex As Exception
            LogHelper.Error("Location_Master:btnReset_Click" + ex.Message)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Location_List.aspx", False)
        Catch ex As Exception
            LogHelper.Error("Location_Master:btnCancel_Click" + ex.Message)
        End Try

    End Sub

    Private Sub Clear()
        Try
            txtlCode.Text = ""
            txtName.Text = ""
            txtAddress.Text = ""
            radCountry.SelectedValue = 0
            radState.Items.Clear()
            radCity.Items.Clear()
            radVenue.SelectedValue = 0
            chkTillAutoLogOff.Checked = True
            chkActive.Checked = True
            chkclick.Checked = False
            chkkiosk.Checked = False
            chkorder.Checked = False
            chkdelivery.Checked = False
            chkisDelivery.Checked = True
            chkReceipt.Checked = False
            chkEmailRec_APK.Checked = False
            isGetCovers.Checked = False
            cashback.Checked = False
            txtsarttime.Text = ""
            textendtime.Text = ""
            txtEmail.Text = ""
            txtContact.Text = ""
            txtgtway_MerchantID.Text = ""

        Catch ex As Exception
            LogHelper.Error("Location_Master:Clear" + ex.Message)
        End Try

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            oclsLocation.cmp_id = Val(Session("cmp_id"))
            oclsLocation.code = txtlCode.Text.Trim()
            oclsLocation.name = txtName.Text.Trim()
            oclsLocation.address = txtAddress.Text
            oclsLocation.country_id = Val(IIf(radCountry.SelectedValue = "", 0, radCountry.SelectedValue))
            oclsLocation.state_id = Val(IIf(radState.SelectedValue = "", 0, radState.SelectedValue))
            oclsLocation.city_id = Val(IIf(radCity.SelectedValue = "", 0, radCity.SelectedValue))
            oclsLocation.onlinepayment = Val(IIf(ddljudo.SelectedValue = "", 0, ddljudo.SelectedValue))
            oclsLocation.ip_address = Request.UserHostAddress
            oclsLocation.login_id = Val(Session("login_id"))
            oclsLocation.venue_id = Val(radVenue.SelectedValue)
            oclsLocation.till_auto_log_off = IIf(chkTillAutoLogOff.Checked = True, 1, 0)
            oclsLocation.is_active = IIf(chkActive.Checked = True, 1, 0)
            oclsLocation.cashback = IIf(cashback.Checked = True, 1, 0)
            oclsLocation.srv_charge_clickcollect = IIf(chkclick.Checked = True, 1, 0)
            oclsLocation.srv_charge_kiosk = IIf(chkkiosk.Checked = True, 1, 0)
            oclsLocation.srv_charge_order = IIf(chkorder.Checked = True, 1, 0)
            oclsLocation.srv_charge_delivery = IIf(chkdelivery.Checked = True, 1, 0)

            oclsLocation.min_amount = Val(txtMinAmount.Text)
            oclsLocation.is_delivery = IIf(chkisDelivery.Checked = True, 1, 0)
            oclsLocation.judo_id = txtJudoId.Text
            oclsLocation.judo_token = txtJudoToken.Text
            oclsLocation.judo_secret = txtjudosecret.Text
            oclsLocation.cashflow_id = txtCashflowId.Text
            oclsLocation.cashflow_url = txtCashflowUrl.Text
            oclsLocation.cashflow_api_key = txtCashflowAPIKey.Text
            oclsLocation.is_skipPay = IIf(chkSkipPay.Checked = True, 1, 0)
            oclsLocation.is_email = IIf(chkReceipt.Checked = True, 1, 0)
            oclsLocation.websales_check_schedule = IIf(chckwebsales.Checked = True, 1, 0)
            oclsLocation.start_date = txtsarttime.Text
            oclsLocation.end_date = textendtime.Text
            oclsLocation.Email = txtEmail.Text
            oclsLocation.Contact = IIf(txtContact.Text = "", Nothing, txtContact.Text)
            oclsLocation.header_reciept = WebUtility.HtmlDecode(txtReciptHeader.Content)
            oclsLocation.message_delivery = WebUtility.HtmlDecode(txtmessage_delivery.Content)
            oclsLocation.message_order_table = WebUtility.HtmlDecode(txtmessage_order_table.Content)
            oclsLocation.click_and_collect = IIf(chkIsClickcollect.Checked = True, 1, 0)
            oclsLocation.Order_table = IIf(chkIsOrdertable.Checked = True, 1, 0)
            oclsLocation.schedule_required = IIf(chkScheRequired.Checked = True, 1, 0)
            oclsLocation.schedule_cnc = IIf(chkScnc.Checked = True, 1, 0)
            oclsLocation.HourlySlot = IIf(chkHourlySlot.Checked = True, 1, 0)
            oclsLocation.CustomPay_id = txtCustomPayId.Text
            oclsLocation.CustomPay_token = txtCustomPayToken.Text
            oclsLocation.CustomPay_secret = txtCustomPaySecret.Text
            oclsLocation.CustomPay_Base64 = txtCustomPayBase64.Text
            oclsLocation.CustomPay_URL = txtCustomPayURL.Text
            oclsLocation.Is_Email_APK = IIf(chkEmailRec_APK.Checked = True, 1, 0)
            oclsLocation.Is_GetCovers = IIf(isGetCovers.Checked = True, 1, 0)

            oclsLocation.Till_url = txtUrl.Text
            oclsLocation.Till_Phn_no = txtcontactTill.Text
            oclsLocation.gtway_MerchantID = IIf(txtgtway_MerchantID.Text = "", Nothing, txtgtway_MerchantID.Text)
            oclsLocation.gtway_StoreID = Val(rad_StoreName.SelectedValue)
            oclsLocation.gtway_LocationID = Val(rad_StoreLocation.SelectedValue)
            If Val(rad_StoreName.SelectedValue) <> 0 Then
                oclsLocation.gtway_StoreName = rad_StoreName.SelectedItem.Text.ToString()
            Else
                oclsLocation.gtway_StoreName = ""
            End If

            If Val(rad_StoreLocation.SelectedValue) <> 0 Then
                oclsLocation.gtway_LocationName = rad_StoreLocation.SelectedItem.Text.ToString()
            Else
                oclsLocation.gtway_LocationName = ""
            End If


            oclsLocation.Client_ID = txt_clientid.Text
            oclsLocation.clientsecret = txt_clientsecret.Text
            oclsLocation.redirect_uri = txt_redirect_uri.Text

            oclsLocation.Table_As_Box = "Table" 'txt_tblASbox.Text.Trim()
            Dim i As Integer = 0
            For Each file As UploadedFile In fileupload.UploadedFiles
                i = i + 1
            Next
            If i > 0 Then
                RadBinaryImage1.DataValue = Nothing
                For Each file As UploadedFile In fileupload.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = fileupload.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    fileupload.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryImage1.DataValue = image
                    Image1.Visible = True
                    Dim bytes As Byte() = RadBinaryImage1.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image1.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next
            Else
                Try


                    If Not ViewState("logo").ToString() = Nothing Then
                        RadBinaryImage1.DataValue = ViewState("logo")
                    End If
                Catch ex As Exception

                End Try

            End If

            'oclsLocation.POSlite = IIf(chkPOSLite.Checked = True, 1, 0)

            oclsLocation.L_Image = RadBinaryImage1.DataValue
            Dim j As Integer = 0
            For Each file As UploadedFile In RadAsyncUpload1.UploadedFiles
                j = j + 1
            Next
            If j > 0 Then
                RadBinaryclickandcollect.DataValue = Nothing
                For Each file As UploadedFile In RadAsyncUpload1.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = RadAsyncUpload1.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    RadAsyncUpload1.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryclickandcollect.DataValue = image
                    Image4.Visible = True
                    Dim bytes As Byte() = RadBinaryclickandcollect.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image4.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next


            End If

            oclsLocation.Click_Collect_image = RadBinaryclickandcollect.DataValue
            Dim k As Integer = 0
            For Each file As UploadedFile In RadAsyncUpload2.UploadedFiles
                k = k + 1
            Next
            If k > 0 Then
                RadBinarydelivery.DataValue = Nothing
                For Each file As UploadedFile In RadAsyncUpload2.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = RadAsyncUpload2.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    RadAsyncUpload2.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinarydelivery.DataValue = image
                    Image6.Visible = True
                    Dim bytes As Byte() = RadBinarydelivery.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image6.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next

            End If

            oclsLocation.Delivery_image = RadBinarydelivery.DataValue

            Dim l As Integer = 0
            For Each file As UploadedFile In RadAsyncUpload3.UploadedFiles
                l = l + 1
            Next
            If l > 0 Then
                RadBinaryorderattable.DataValue = Nothing
                For Each file As UploadedFile In RadAsyncUpload3.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = RadAsyncUpload3.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    RadAsyncUpload3.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryorderattable.DataValue = image
                    Image8.Visible = True
                    Dim bytes As Byte() = RadBinaryorderattable.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image8.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next


            End If

            oclsLocation.OrderAtTable_image = RadBinaryorderattable.DataValue

            RadBinaryPOSLogo.DataValue = ViewState("PosLogoImage")
            Dim M As Integer = 0
            For Each file As UploadedFile In RadAsyncUpload_logo.UploadedFiles
                M = M + 1
            Next
            If M > 0 Then
                ' RadBinaryPOSLogo.DataValue = Nothing
                For Each file As UploadedFile In RadAsyncUpload_logo.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = RadAsyncUpload_logo.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    RadAsyncUpload_logo.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryPOSLogo.DataValue = image
                    Image3.Visible = True
                    Dim bytes As Byte() = RadBinaryPOSLogo.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    Image3.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next


            End If

            oclsLocation.POS_logo = RadBinaryPOSLogo.DataValue

            If radBackcolorpicker_m.SelectedColor.Name = "0" Then
                oclsLocation.BG_Color = "#0000FF"
            Else

                oclsLocation.BG_Color = ColorTranslator.ToHtml(radBackcolorpicker_m.SelectedColor)
            End If
            If radForcolorpicker_m.SelectedColor.Name = "0" Then
                oclsLocation.Font_Color = "#0000FF"
            Else
                oclsLocation.Font_Color = ColorTranslator.ToHtml(radForcolorpicker_m.SelectedColor)
            End If

            If radbodycolorpicker_m.SelectedColor.Name = "0" Then
                oclsLocation.Body_Color = "#0000FF"
            Else
                oclsLocation.Body_Color = ColorTranslator.ToHtml(radbodycolorpicker_m.SelectedColor)
            End If
            oclsLocation.machine_id = 0
            oclsLocation.delivery_charges = IIf(txtDCharges.Text = "", 0, Val(txtDCharges.Text))
            oclsLocation.service_charges = IIf(txtsvccharg.Text = "", 0, Val(txtsvccharg.Text))
            oclsLocation.theme = Val(IIf(Radtheme.SelectedValue = "", 0, Radtheme.SelectedValue))
            oclsLocation.tipAs = IIf(tipas.SelectedValue = "", "", tipas.SelectedValue)

            oclsLocation.oldname = hdnOldName.Value

            oclsLocation.future_booking_days = IIf(txtFutureBookingDays.Text = "", 0, Val(txtFutureBookingDays.Text))

            oclsLocation.GC_Btn_stl = ddlBtnStyle_GC.SelectedValue.ToString()
            oclsLocation.GC_Btn_img_typ = ddlImgType_GC.SelectedValue.ToString()
            oclsLocation.GC_Btn_fnt_size = ddl_QtyFontSize_GC.SelectedValue.ToString()
            oclsLocation.GC_Btn_bkgd_clr = ColorTranslator.ToHtml(radQty_BG_clr_GC.SelectedColor)
            oclsLocation.GC_Btn_txt_clr = ColorTranslator.ToHtml(radtxt_clr_GC.SelectedColor)
            oclsLocation.login_src_bkgd_clr = ColorTranslator.ToHtml(login_bg_clr.SelectedColor)
            oclsLocation.login_src_login_btn = ColorTranslator.ToHtml(login_sr_clr.SelectedColor)




            oclsLocation.C_Btn_stl = ddlBtnStyle_C.SelectedValue.ToString()
            oclsLocation.C_Btn_img_typ = ddlImgType_C.SelectedValue.ToString()
            oclsLocation.C_Btn_fnt_size = ddl_QtyFontSize_C.SelectedValue.ToString()
            oclsLocation.C_Btn_bkgd_clr = ColorTranslator.ToHtml(radQty_BG_clr_C.SelectedColor)
            oclsLocation.C_Btn_txt_clr = ColorTranslator.ToHtml(radtxt_clr_C.SelectedColor)

            oclsLocation.P_Btn_stl = ddlBtnStyle_P.SelectedValue.ToString()
            oclsLocation.P_Btn_img_typ = ddlImgType_P.SelectedValue.ToString()
            oclsLocation.P_Btn_fnt_size = ddl_QtyFontSize_P.SelectedValue.ToString()
            oclsLocation.P_Btn_bkgd_clr = ColorTranslator.ToHtml(radQty_BG_clr_P.SelectedColor)
            oclsLocation.P_Btn_txt_clr = ColorTranslator.ToHtml(radtxt_clr_P.SelectedColor)

            oclsLocation.GC_Btn_img_typ_pos = ddl_ImageOption_GC.SelectedValue.ToString()
            oclsLocation.C_Btn_img_typ_pos = ddl_ImageOption_C.SelectedValue.ToString()
            oclsLocation.P_Btn_img_typ_pos = ddl_ImageOption_P.SelectedValue.ToString()

            Dim h As Integer = 0

            For Each file As UploadedFile In fileupload2.UploadedFiles
                h = h + 1
            Next

            If h > 0 Then
                RadBinaryImageGraphic.DataValue = Nothing
                For Each file As UploadedFile In fileupload2.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = fileupload2.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    fileupload2.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryImageGraphic.DataValue = image
                    ImageGraphic.Visible = True
                    Dim bytes As Byte() = RadBinaryImageGraphic.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    ImageGraphic.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next
            Else
                Try
                    If Not ViewState("GraphicViewBackground").ToString() = Nothing Then
                        RadBinaryImageGraphic.DataValue = ViewState("GraphicViewBackground")
                    End If
                Catch ex As Exception

                End Try

            End If

            oclsLocation.GraphicViewBackground = RadBinaryImageGraphic.DataValue

            Dim o As Integer = 0
            For Each file As UploadedFile In fileupload3.UploadedFiles
                o = o + 1
            Next
            If o > 0 Then
                RadBinaryImageTable.DataValue = Nothing
                For Each file As UploadedFile In fileupload3.UploadedFiles
                    Dim image As Byte()
                    Dim fileLength As Long = fileupload3.UploadedFiles(0).InputStream.Length
                    image = New Byte(fileLength - 1) {}
                    fileupload3.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
                    RadBinaryImageTable.DataValue = image
                    ImageTable.Visible = True
                    Dim bytes As Byte() = RadBinaryImageTable.DataValue
                    Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                    ImageTable.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
                Next
            Else
                Try
                    If Not ViewState("GraphicViewTable").ToString() = Nothing Then
                        RadBinaryImageTable.DataValue = ViewState("GraphicViewTable")
                    End If
                Catch ex As Exception

                End Try

            End If

            oclsLocation.GraphicViewTable = RadBinaryImageTable.DataValue
            'oclsLocation.SunmiSecondScreen = IIf(chkSecondScreen.Checked = True, 1, 0)

            'o = 0
            'For Each file As UploadedFile In rauploadSSImage1.UploadedFiles
            '    o = o + 1
            'Next
            'If o > 0 Then
            '    RadBinaryImage3.DataValue = Nothing
            '    For Each file As UploadedFile In rauploadSSImage1.UploadedFiles
            '        Dim image As Byte()
            '        Dim fileLength As Long = rauploadSSImage1.UploadedFiles(0).InputStream.Length
            '        image = New Byte(fileLength - 1) {}
            '        rauploadSSImage1.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
            '        RadBinaryImage3.DataValue = image
            '        ImageTable.Visible = True
            '        Dim bytes As Byte() = RadBinaryImage3.DataValue
            '        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            '        ImageTable.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
            '    Next
            'Else
            '    Try
            '        If Not ViewState("secondscreenImage1").ToString() = Nothing Then
            '            RadBinaryImage3.DataValue = ViewState("secondscreenImage1")
            '        End If
            '    Catch ex As Exception

            '    End Try

            'End If

            'oclsLocation.SecondScreenImage1 = RadBinaryImage3.DataValue


            'o = 0
            'For Each file As UploadedFile In rauploadSSImage2.UploadedFiles
            '    o = o + 1
            'Next
            'If o > 0 Then
            '    RadBinaryImage4.DataValue = Nothing
            '    For Each file As UploadedFile In rauploadSSImage2.UploadedFiles
            '        Dim image As Byte()
            '        Dim fileLength As Long = rauploadSSImage2.UploadedFiles(0).InputStream.Length
            '        image = New Byte(fileLength - 1) {}
            '        rauploadSSImage2.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
            '        RadBinaryImage4.DataValue = image
            '        ImageTable.Visible = True
            '        Dim bytes As Byte() = RadBinaryImage4.DataValue
            '        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            '        ImageTable.ImageUrl = Convert.ToString("data:image/jpg;base64,") & base64String
            '    Next
            'Else
            '    Try
            '        If Not ViewState("secondscreenImage2").ToString() = Nothing Then
            '            RadBinaryImage4.DataValue = ViewState("secondscreenImage2")
            '        End If
            '    Catch ex As Exception

            '    End Try

            'End If

            'oclsLocation.SecondScreenImage2 = RadBinaryImage4.DataValue


            '  If rauploadSSvideo.HasFile Then

            'RadBinaryImage5.DataValue = Nothing
            '    'For Each file As UploadedFile In rauploadSSvideo.PostedFile
            '    'Dim image As Byte()
            '    'Dim fileLength As Long = rauploadSSvideo.UploadedFiles(0).InputStream.Length
            '    'image = New Byte(fileLength - 1) {}
            '    'rauploadSSvideo.UploadedFiles(0).InputStream.Read(image, 0, image.Length)
            '    'RadBinaryImage5.DataValue = image
            '    'ImageTable.Visible = True
            '    'Dim bytes As Byte() = RadBinaryImage5.DataValue
            '    'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            '    ''ImageTable.ImageUrl = Convert.ToString("data:video/mp4;base64,") & base64String
            '    Dim FileName As String = Path.GetFileName(rauploadSSvideo.PostedFile.FileName) + System.DateTime.Now.ToShortDateString().Replace(" ", "").Replace(":", "").Replace("-", "").Trim()
            '    Dim Extension As String = Path.GetExtension(rauploadSSvideo.PostedFile.FileName)
            '    Dim FolderPath As String = ConfigurationManager.AppSettings("~\Video\")

            '    Dim FilePath As String = Server.MapPath(FolderPath + FileName)
            '    rauploadSSvideo.SaveAs(FilePath)
            '    'Next
            'Else
            '    Try
            '        If Not ViewState("secondscreenvideo").ToString() = Nothing Then
            'RadBinaryImage5.DataValue = ViewState("secondscreenvideo")
            '        End If
            '    Catch ex As Exception

            '    End Try

            'End If

            'oclsLocation.sunmi_video_path = ViewState("secondscreenvideo").ToString()


            If Session("location_id") = Nothing Then
                oclsLocation.location_id = 0
                oclsLocation.Insert()
                Session("Success") = "Record inserted successfully"
            Else
                oclsLocation.location_id = Val(Session("location_id"))
                oclsLocation.Update()
                hdnOldName.Value = ""
                Session("Success") = "Record updated successfully"

            End If
            Session("location_id") = Nothing
            Response.Redirect("Location_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Location_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub btnRemoveLogo_Click(sender As Object, e As EventArgs)

        imagePOSLOGO.Visible = False
        RadBinaryPOSLogo.Visible = False
        HiddenField4.Value = ""
        ViewState("PosLogoImage") = Nothing

        btnRemoveLogo.Visible = False
    End Sub

    Protected Sub ddljudo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            Dim Check As String = ddljudo.SelectedItem.Text

            If Check = "Judo" Then
                divJudoPayid.Visible = True
                divJudoTokenid.Visible = True
                divJudoSecretid.Visible = True
                divCashflowId.Visible = False
                divCashflowUrl.Visible = False
                divCashflowAPIKey.Visible = False
                divCustomPayId.Visible = False
                divCustomPaySecret.Visible = False
                divCustomPayToken.Visible = False
                divCustomPayBase64.Visible = False
                divCustomPayURL.Visible = False
            ElseIf Check = "Cashflow" Then
                divJudoPayid.Visible = False
                divJudoTokenid.Visible = False
                divJudoSecretid.Visible = False
                divCashflowId.Visible = True
                divCashflowUrl.Visible = True
                divCashflowAPIKey.Visible = True
                divCustomPayId.Visible = False
                divCustomPaySecret.Visible = False
                divCustomPayToken.Visible = False
                divCustomPayBase64.Visible = False
                divCustomPayURL.Visible = False
            ElseIf Check = "CustomPay" Then
                divJudoPayid.Visible = False
                divJudoTokenid.Visible = False
                divJudoSecretid.Visible = False
                divCashflowId.Visible = False
                divCashflowUrl.Visible = False
                divCashflowAPIKey.Visible = False
                divCustomPayId.Visible = True
                divCustomPaySecret.Visible = True
                divCustomPayToken.Visible = True
                divCustomPayBase64.Visible = True
                divCustomPayURL.Visible = True
            ElseIf Check = "CardStream" Then
                divJudoPayid.Visible = False
                divJudoTokenid.Visible = False
                divJudoSecretid.Visible = False
                divCashflowId.Visible = True
                divCashflowUrl.Visible = True
                divCashflowAPIKey.Visible = True
                divCustomPayId.Visible = False
                divCustomPaySecret.Visible = False
                divCustomPayToken.Visible = False
                divCustomPayBase64.Visible = False
                divCustomPayURL.Visible = False
            Else
                divJudoPayid.Visible = False
                divJudoTokenid.Visible = False
                divJudoSecretid.Visible = False
                divCashflowId.Visible = False
                divCashflowUrl.Visible = False
                divCashflowAPIKey.Visible = False
                divCustomPayId.Visible = False
                divCustomPaySecret.Visible = False
                divCustomPayToken.Visible = False
                divCustomPayBase64.Visible = False
                divCustomPayURL.Visible = False
            End If



        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Location_Master:btnSave_Click" + ex.Message)
        End Try
    End Sub

    Private Sub RadGridGraphic_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGridGraphic.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then

                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                oclsLocation.cmp_id = Val(Session("cmp_id"))
                oclsLocation.location_id = Val(Session("location_id"))
                oclsLocation.ImgDeleteGraphic()

                Dim dtLocation As DataTable = oclsLocation.Select()
                If dtLocation.Rows.Count > 0 Then

                    If dtLocation.Rows(0)("GraphicViewBackground") IsNot DBNull.Value Then

                        RadGridGraphic.DataSource = dtLocation
                        RadGridGraphic.DataBind()
                    Else
                        RadGridGraphic.DataSource = ""
                        RadGridGraphic.DataBind()
                        ViewState("GraphicViewBackground") = ""
                    End If

                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Location_Master: RadGridGraphic_ItemCommand:" + ex.Message)
        End Try
    End Sub


    Private Sub RadGridTable_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGridTable.ItemCommand
        Try
            If e.CommandName = "DeleteVal" Then
                Dim id As Integer = Convert.ToInt32(e.CommandArgument)
                oclsLocation.cmp_id = Val(Session("cmp_id"))
                oclsLocation.location_id = Val(Session("location_id"))
                oclsLocation.ImgDeleteTable()
                Dim dtLocation As DataTable = oclsLocation.Select()
                If dtLocation.Rows.Count > 0 Then
                    If dtLocation.Rows(0)("GraphicViewTable") IsNot DBNull.Value Then
                        RadGridTable.DataSource = dtLocation
                        RadGridTable.DataBind()
                    Else
                        RadGridTable.DataSource = ""
                        RadGridTable.DataBind()
                        ViewState("GraphicViewTable") = ""
                    End If
                End If
            End If
        Catch ex As Exception
            LogHelper.Error("Location_Master: RadGridTable_ItemCommand:" + ex.Message)
        End Try
    End Sub

    Private Sub BindStore()
        Try
            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()
            Dim cmd As SqlCommand = New SqlCommand("Get_StoreName", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            strcon.Close()
            If dt.Rows.Count > 0 Then
                rad_StoreName.DataSource = dt
                rad_StoreName.DataTextField = "store_name"
                rad_StoreName.DataValueField = "store_id"
                rad_StoreName.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            rad_StoreName.Items.Insert(0, li)

        Catch ex As Exception
            LogHelper.Error("Location_Master:BindStore" + ex.Message)
        End Try
    End Sub

    Protected Sub rad_StoreName_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try

            Dim strcon As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("POSControllerConnectionString").ConnectionString)
            strcon.Open()
            Dim n As String = rad_StoreName.SelectedItem.Text
            Dim cmd As SqlCommand = New SqlCommand("Get_DB_Connection_Details", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@db_name", n)
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            strcon.Close()
            If dt.Rows.Count > 0 Then
                Session("Conn_String") = "Data Source=" + dt.Rows(0)("db_server") + ";Initial Catalog=" + dt.Rows(0)("db_name") + ";User ID=" + dt.Rows(0)("db_username") + ";Password=" + Decode_data(dt.Rows(0)("db_password")) + ";"

                BindStoreLocation()
            End If

        Catch ex As Exception
            LogHelper.Error("Location_Master: rad_StoreName_SelectedIndexChanged:" + ex.Message)
        End Try
    End Sub

    Private Sub BindStoreLocation()
        Try

            Dim strcon As SqlConnection = New SqlConnection(Session("Conn_String").ToString())
            strcon.Open()
            Dim n As String = rad_StoreName.SelectedItem.Text
            Dim cmd As SqlCommand = New SqlCommand("Get_Store_Location", strcon)
            cmd.CommandType = CommandType.StoredProcedure
            Dim adp As SqlDataAdapter = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            adp.Fill(dt)
            cmd.ExecuteNonQuery()
            strcon.Close()
            If dt.Rows.Count > 0 Then
                rad_StoreLocation.DataSource = dt
                rad_StoreLocation.DataTextField = "name"
                rad_StoreLocation.DataValueField = "location_id"
                rad_StoreLocation.DataBind()
            End If
            Dim li As RadComboBoxItem = New RadComboBoxItem("SELECT", "0")
            rad_StoreLocation.Items.Insert(0, li)

            Session("Conn_String") = ""
        Catch ex As Exception
            LogHelper.Error("Location_Master: BindStoreLocation:" + ex.Message)
        End Try
    End Sub

    Public Function Decode_data(ByVal str As String) As String
        Try
            Return Encoding.UTF8.GetString(System.Convert.FromBase64String(str))
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Protected Sub tipas_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        If tipas.SelectedValue = "Cashback" Then
            cashback.Visible = True
            lblcashback.Visible = True
            'cashback.Checked = True
        Else
            cashback.Visible = False
            lblcashback.Visible = False
            cashback.Checked = False
        End If
    End Sub

    Protected Sub txtsvccharg_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtsvccharg.Text.Trim()) > 0.00 Then
                chkclick.Visible = True
                chkkiosk.Visible = True
                chkorder.Visible = True
                chkdelivery.Visible = True
            Else
                chkclick.Visible = False
                chkkiosk.Visible = False
                chkorder.Visible = False
                chkdelivery.Visible = False
                chkclick.Checked = False
                chkkiosk.Checked = False
                chkorder.Checked = False
                chkdelivery.Checked = False
            End If
        Catch ex As Exception
            LogHelper.Error("Location_Master:txtsvccharg_TextChanged" + ex.Message)
        End Try
    End Sub
End Class

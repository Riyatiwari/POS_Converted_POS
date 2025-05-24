Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI
Partial Class Promotion
    Inherits BaseClass
    Dim oclsBind As New clsBinding
    Dim oclsPromotion As New Controller_clsPromotion()
    Dim oclsProduct As New Controller_clsProduct()
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("cmp_id") = Nothing Then
                    Response.Redirect("SignIn.aspx", False)
                End If
                txtstartdate.SelectedDate = System.DateTime.Now.ToShortDateString()

                div_EndDate.Visible = False

                bindIngredientGrid()

                If Not Session("table_promotion_id") = Nothing Then
                    BindPromotion()
                Else
                    oclsBind.BindProductGroup(RadPro_group, Val(Session("cmp_id")))
                    oclsBind.BindProductGroup(RadPro_group1, Val(Session("cmp_id")))
                    oclsBind.BindProductGroup(RadPro_group2, Val(Session("cmp_id")))
                    oclsBind.BindProductGroup(RadPro_group3, Val(Session("cmp_id")))
                    oclsBind.BindVenue(RadVenue, Val(Session("cmp_id")))
                    'dv_pro_group2.Attributes("style") = "margin-top:20px;"
                    'dv_pro_group3.Attributes("style") = "margin-top:20px;"
                    'BindGrid()
                    'div_EndDate.Visible = True
                    dv_OnDays.Visible = True
                End If
                txtenddate.MinDate = DateTime.Now
            End If
        Catch ex As Exception
            LogHelper.Error("Promotion:Page_Load:" + ex.Message)
        End Try
    End Sub

    Protected Sub rbPromotiontype_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If rbPromotiontype.SelectedItem.Value Is "1" Then
                dv_Month.Visible = False
                dv_Week.Visible = True
                dv_Days.Visible = False
                dv_OnDays.Visible = True
                'dv_Week.Attributes("style") = "float:right;margin-top:20px;"
                'dv_pro_group2.Attributes("style") = "margin-top:60px;"
                'dv_pro_group3.Attributes("style") = "margin-top:60px;"
                Rad_Months.ClearCheckedItems()
                Rad_Week.ClearCheckedItems()
                rad_Days.ClearCheckedItems()
                Rad_OnDays.ClearCheckedItems()
            ElseIf rbPromotiontype.SelectedItem.Value Is "2" Then
                dv_Month.Visible = True
                dv_Week.Visible = True
                dv_Days.Visible = False
                dv_OnDays.Visible = True
                'dv_Week.Attributes("style") = "margin-top:20px;"
                'dv_pro_group2.Attributes("style") = "margin-top:90px;"
                'dv_pro_group3.Attributes("style") = "margin-top:90px;"
                'div_AllDays.Attributes("style") = "margin-top:10px;"
                Rad_Months.ClearCheckedItems()
                Rad_Week.ClearCheckedItems()
                rad_Days.ClearCheckedItems()
                Rad_OnDays.ClearCheckedItems()
            Else
                dv_Month.Visible = False
                dv_Week.Visible = False
                dv_Days.Visible = False
                dv_OnDays.Visible = True
                'div_AllDays.Attributes("style") = "margin-top:75px;"
                'dv_pro_group2.Attributes("style") = "margin-top:175px;"
                'dv_pro_group3.Attributes("style") = "margin-top:175px;"
                Rad_Months.ClearCheckedItems()
                Rad_Week.ClearCheckedItems()
                rad_Days.ClearCheckedItems()
                Rad_OnDays.ClearCheckedItems()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RadPro_group_SelectedIndexChanged1(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindProductByProductGroup(Rad_Product, Val(Session("cmp_id")), RadPro_group.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RadPro_group1_SelectedIndexChanged1(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindProductByProductGroup(Rad_Product1, Val(Session("cmp_id")), RadPro_group1.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RadPro_group2_SelectedIndexChanged1(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindProductByProductGroup(Rad_Product2, Val(Session("cmp_id")), RadPro_group2.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RadPro_group3_SelectedIndexChanged1(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindProductByProductGroup(Rad_Product3, Val(Session("cmp_id")), RadPro_group3.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rad_Product_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindSizeUnitByProduct(rad_size, Val(Session("cmp_id")), Rad_Product.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rad_Product1_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindSizeUnitByProduct(Rad_size1, Val(Session("cmp_id")), Rad_Product1.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rad_Product2_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindSizeUnitByProduct(Rad_size2, Val(Session("cmp_id")), Rad_Product2.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rad_Product3_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindSizeUnitByProduct(Rad_size3, Val(Session("cmp_id")), Rad_Product3.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Rad_Promoname.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Promo name is require');", True)
                Return
            End If

            If RadVoucherType.SelectedValue IsNot "" Then
                If RadVoucherCode.Text Is "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Voucher Code  is require');", True)
                    Return
                End If
            End If

            If txtstartdate.SelectedDate.ToString() Is "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Start Date is require');", True)
                Return
            End If

            If chk_endless.Checked = False Then
                If txtenddate.SelectedDate.ToString() Is "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('End Date is require');", True)
                    Return
                End If
            End If

            If Rad_DiscountType.SelectedValue Is "SELECT" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Discount Type is require');", True)
                Return
            End If

            If Rad_DiscountType.SelectedValue Is "4" And chkFreeProduct.Checked = False Then
                If Rad_Discount.Text Is "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Discount  is require');", True)
                    Return
                End If
            End If

            If Rad_DiscountType.SelectedValue Is "4" Then
                If Rad_comboselect.SelectedValue Is "2" Then
                    If RadPro_group.SelectedIndex = 0 Or RadPro_group1.SelectedIndex = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Product group and Product group1  is require');", True)
                        Return
                    End If
                ElseIf Rad_comboselect.SelectedValue Is "3" Then
                    If RadPro_group.SelectedIndex = 0 Or RadPro_group1.SelectedIndex = 0 Or RadPro_group2.SelectedIndex = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Product group,Product group1 and Product group2 is require');", True)
                        Return
                    End If
                ElseIf Rad_comboselect.SelectedValue Is "4" Then
                    If RadPro_group.SelectedIndex = 0 Or RadPro_group1.SelectedIndex = 0 Or RadPro_group2.SelectedIndex = 0 Or RadPro_group3.SelectedIndex = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Product group,Product group1,Product group2 and Product group3 is require');", True)
                        Return
                    End If
                End If
            Else


                For Each item As GridDataItem In rdProduct.MasterTableView.Items
                    Dim txtDiscount As RadTextBox = DirectCast(item.FindControl("txtDiscount"), RadTextBox)
                    If txtDiscount.Text = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please enter discount amount');", True)
                        Return
                    Else

                    End If

                Next

            End If

            If Rad_DiscountType.SelectedValue Is "4" Then
                oclsPromotion.product_group = Val(RadPro_group.SelectedValue)
                oclsPromotion.product_group1 = Val(IIf(RadPro_group1.SelectedValue = "", 0, RadPro_group1.SelectedValue))
                oclsPromotion.product_group2 = Val(IIf(RadPro_group2.SelectedValue = "", 0, RadPro_group2.SelectedValue))
                oclsPromotion.product_group3 = Val(IIf(RadPro_group3.SelectedValue = "", 0, RadPro_group3.SelectedValue))
                oclsPromotion.cmp_id = Val(Session("cmp_id"))
                oclsPromotion.product_id = Val(Rad_Product.SelectedValue)
                oclsPromotion.product_id1 = Val(IIf(Rad_Product1.SelectedValue = "", 0, Rad_Product1.SelectedValue))
                oclsPromotion.product_id2 = Val(IIf(Rad_Product2.SelectedValue = "", 0, Rad_Product2.SelectedValue))
                oclsPromotion.product_id3 = Val(IIf(Rad_Product3.SelectedValue = "", 0, Rad_Product3.SelectedValue))
                oclsPromotion.size_id = Val(rad_size.SelectedValue)
                oclsPromotion.size_id1 = Val(IIf(Rad_size1.SelectedValue = "", 0, Rad_size1.SelectedValue))
                oclsPromotion.size_id2 = Val(IIf(Rad_size2.SelectedValue = "", 0, Rad_size2.SelectedValue))
                oclsPromotion.size_id3 = Val(IIf(Rad_size3.SelectedValue = "", 0, Rad_size3.SelectedValue))
                oclsPromotion.Discount_type = 3
                oclsPromotion.Discount = Val(Rad_Discount.Text)
                oclsPromotion.price_id = 0
                oclsPromotion.Promo_name = Rad_Promoname.Text
                oclsPromotion.Voucher_Type = IIf(RadVoucherType.SelectedValue = "", "", RadVoucherType.SelectedValue)
                oclsPromotion.Voucher_Code = RadVoucherCode.Text

                oclsPromotion.isFlatAmount = IIf(chkisFlatAmount.Checked = True, 1, 0)
                oclsPromotion.mixnmatch = IIf(chkMixnMatch.Checked = True, 1, 0)
                oclsPromotion.buy1_Get1 = IIf(ChkBuy1Get1.Checked = True, 1, 0)

                oclsPromotion.freeProduct = IIf(chkFreeProduct.Checked = True, 1, 0)
                oclsPromotion.freeProductAmount = txtFPAmount.Text
                oclsPromotion.AllfreeProduct = IIf(chkAllProd.Checked = True, 1, 0)
                oclsPromotion.EachSpend = IIf(chkOnEachSpend.Checked = True, 1, 0)
                oclsPromotion.isbarStock = IIf(isbarStock.Checked = True, 1, 0)
                oclsPromotion.FreeQty = txtQtyofProduct.Text
                oclsPromotion.Online_Coupon = Rad_onlinecoupon.Text


                Dim collection As IList(Of RadComboBoxItem) = Rad_Months.CheckedItems

                If (collection.Count > 0) Then
                    oclsPromotion.Month_flag = 1
                Else
                    oclsPromotion.Month_flag = 0
                End If

                Dim collection_week As IList(Of RadComboBoxItem) = Rad_Week.CheckedItems

                If (collection_week.Count > 0) Then
                    oclsPromotion.Week_flag = 1
                Else
                    oclsPromotion.Week_flag = 0
                End If

                If rbPromotiontype.SelectedItem.Value Is "0" Then
                    oclsPromotion.duration_flag = 0
                ElseIf rbPromotiontype.SelectedItem.Value Is "1" Then
                    oclsPromotion.duration_flag = 1
                Else
                    oclsPromotion.duration_flag = 2
                End If

                oclsPromotion.no_of_product = Val(Rad_comboselect.SelectedValue)


                If rbPromotiontype.SelectedItem.Value Is "0" And Rad_OnDays.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 1
                ElseIf Rad_OnDays.CheckedItems.Count > 0 And Rad_Months.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 And Rad_Week.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 2
                ElseIf rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 And Rad_Months.CheckedItems.Count = 0 And Rad_Week.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 3
                    oclsPromotion.Month_flag = 1
                ElseIf Rad_Week.CheckedItems.Count > 0 And rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 And Rad_Months.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 4
                    oclsPromotion.Week_flag = 1
                ElseIf Rad_Months.CheckedItems.Count > 0 And Rad_Week.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 5
                ElseIf Rad_Months.CheckedItems.Count > 0 And Rad_OnDays.CheckedItems.Count > 0 And Rad_Week.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 6
                ElseIf Rad_Months.CheckedItems.Count > 0 And Rad_Week.CheckedItems.Count > 0 And rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 7
                    oclsPromotion.Week_flag = 1
                    oclsPromotion.Month_flag = 0
                ElseIf rbPromotiontype.SelectedItem.Value Is "1" And Rad_Week.CheckedItems.Count > 0 And Rad_OnDays.CheckedItems.Count > 0 And Rad_Months.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 Then
                    oclsPromotion.Recurrence = 9
                Else
                    oclsPromotion.Recurrence = 8
                    oclsPromotion.Month_flag = 1
                End If
                'oclsPromotion.Recurrence = 0

                'oclsPromotion.Month_flag = 0 ' check month condition wise
                oclsPromotion.Months = GetSelectedValue(Rad_Months)
                'oclsPromotion.Week_flag = 0 ' check week condition wise

                Dim sel_Week As String
                sel_Week = GetSelectedValue(Rad_Week)

                If sel_Week.ToString Is "" Then
                    oclsPromotion.Weeks = "00"
                Else
                    oclsPromotion.Weeks = sel_Week
                End If

                'oclsPromotion.Weeks = GetSelectedValue(Rad_Week)
                'oclsPromotion.Days = GetSelectedValue(rad_Days)
                'oclsPromotion.on_day = GetSelectedValue(Rad_OnDays)

                Dim sel_Days As String
                sel_Days = GetSelectedValue(rad_Days)

                If sel_Days.ToString Is "" Then
                    oclsPromotion.Days = "00"
                Else
                    oclsPromotion.Days = sel_Days
                End If

                Dim sel_OnDays As String
                sel_OnDays = GetSelectedValue(Rad_OnDays)

                If sel_OnDays.ToString Is "" Then
                    oclsPromotion.on_day = "00"
                Else
                    oclsPromotion.on_day = sel_OnDays
                End If

                If rbPromotiontype.SelectedItem.Value Is "0" Then
                    oclsPromotion.Recurrence_flag = 1
                Else
                    oclsPromotion.Recurrence_flag = 0
                End If

                oclsPromotion.start_time = txtstarttime.TextWithLiterals

                If txtendtime.TextWithLiterals = "00:00" Then
                    oclsPromotion.end_time = "23:59"
                Else
                    oclsPromotion.end_time = txtendtime.TextWithLiterals
                End If

                'oclsPromotion.end_time = txtendtime.TextWithLiterals

                oclsPromotion.start_time1 = txtstarttime1.TextWithLiterals
                oclsPromotion.end_time1 = txtendtime1.TextWithLiterals
                oclsPromotion.start_time2 = txtstarttime2.TextWithLiterals
                oclsPromotion.end_time2 = txtendtime2.TextWithLiterals
                oclsPromotion.start_time3 = txtstarttime3.TextWithLiterals
                oclsPromotion.end_time3 = txtendtime3.TextWithLiterals
                oclsPromotion.startDate = txtstartdate.SelectedDate

                If chk_endless.Checked = True Then
                    oclsPromotion.EndDate = "#01/1/2099 12:00:00AM#"
                    oclsPromotion.is_endless = 1
                Else
                    oclsPromotion.EndDate = txtenddate.SelectedDate
                    oclsPromotion.is_endless = 0
                End If

                'oclsPromotion.EndDate = txtenddate.SelectedDate

                'If Rad_Product1.SelectedIndex > 0 Or Rad_Product2.SelectedIndex > 0 Or Rad_Product3.SelectedIndex > 0 Then
                '    oclsPromotion.combo_flag = 1
                'Else
                '    oclsPromotion.combo_flag = 0
                'End If

                If RadPro_group1.SelectedIndex > 0 Or RadPro_group2.SelectedIndex > 0 Or RadPro_group3.SelectedIndex > 0 Then
                    oclsPromotion.combo_flag = 1
                Else
                    oclsPromotion.combo_flag = 0
                End If

                If Rad_Product1.SelectedIndex > 0 And Rad_Product2.SelectedIndex > 0 And Rad_Product3.SelectedIndex > 0 Then
                    oclsPromotion.combo_product_id = Rad_Product.SelectedValue.ToString() + "#" + Rad_Product1.SelectedValue.ToString() + "#" + Rad_Product2.SelectedValue.ToString() + "#" + Rad_Product3.SelectedValue.ToString()
                ElseIf Rad_Product1.SelectedIndex > 0 And Rad_Product2.SelectedIndex > 0 Then
                    oclsPromotion.combo_product_id = Rad_Product.SelectedValue.ToString() + "#" + Rad_Product1.SelectedValue.ToString() + "#" + Rad_Product2.SelectedValue.ToString()
                ElseIf Rad_Product1.SelectedIndex > 0 Then
                    oclsPromotion.combo_product_id = Rad_Product.SelectedValue.ToString() + "#" + Rad_Product1.SelectedValue.ToString()
                Else
                    oclsPromotion.combo_product_id = ""
                End If

                'oclsPromotion.combo_product_id = ""
                'oclsPromotion.combo_product_id = Rad_Product.SelectedValue.ToString() + "#" + Rad_Product1.SelectedValue.ToString() + "#" + Rad_Product2.SelectedValue.ToString() + "#" + Rad_Product3.SelectedValue.ToString()

                oclsPromotion.location_id = Val(RadLocation.SelectedValue)
                oclsPromotion.machine_id = Val(RadTill.SelectedValue)
                oclsPromotion.venue_id = Val(RadVenue.SelectedValue)
                oclsPromotion.Is_active = 1
                oclsPromotion.Ip_address = Request.UserHostAddress
                oclsPromotion.login_id = Val(Session("login_id"))

                If Session("table_promotion_id") = Nothing Then

                    oclsPromotion.cmp_id = Val(Session("cmp_id"))
                    oclsPromotion.Promo_name = Rad_Promoname.Text
                    oclsPromotion.table_promotion_id = 0
                    Dim dt As DataTable = oclsPromotion.check_Duplicate_Promoname()

                    If dt.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Promo name already exits');", True)
                        Return
                    Else

                    End If


                    oclsPromotion.table_promotion_id = 0
                    Dim id As Integer = oclsPromotion.Insert()
                    Insert_PromotType(id)
                    ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record inserted successfully');window.location='Promotion.aspx';", True)
                    Session("Success") = "Record inserted successfully"
                    Response.Redirect("Promotion_List.aspx", False)
                Else
                    Dim dt1 As DataTable = DirectCast(ViewState("Promotion_Data_Edit"), DataTable)

                    oclsPromotion.cmp_id = Val(Session("cmp_id"))
                    oclsPromotion.Promo_name = Rad_Promoname.Text
                    oclsPromotion.table_promotion_id = dt1.Rows(0)("table_promotion_id")
                    Dim dt As DataTable = oclsPromotion.check_Duplicate_Promoname()

                    If dt.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Promo name already exits');", True)
                        Return
                    Else

                    End If

                    oclsPromotion.table_promotion_id = dt1.Rows(0)("table_promotion_id")
                    oclsPromotion.Update()
                    Insert_PromotType(Val(dt1.Rows(0)("table_promotion_id")))
                    Session("Success") = "Record updated successfully"
                    Response.Redirect("Promotion_List.aspx", False)
                End If

            Else

                For Each item As GridDataItem In rdProduct.MasterTableView.Items

                    Dim ddlSize As RadComboBox = DirectCast(item.FindControl("ddlsize"), RadComboBox)
                    Dim hdproductgroup As HiddenField = DirectCast(item.FindControl("hdproductgroup"), HiddenField)
                    Dim hdproduct As HiddenField = DirectCast(item.FindControl("hdproduct"), HiddenField)
                    Dim txtDiscount As RadTextBox = DirectCast(item.FindControl("txtDiscount"), RadTextBox)
                    Dim hdPromotionid As HiddenField = DirectCast(item.FindControl("hdPromotionid"), HiddenField)

                    'Dim Promotion_Id As String
                    'Dim AllProduct_id As List(Of String) = New List(Of String)

                    'For Each item1 As GridDataItem In rdProduct.MasterTableView.Items
                    '    Dim hdPromotionid1 As HiddenField = DirectCast(item1.FindControl("hdPromotionid"), HiddenField)

                    '    AllProduct_id.Add(hdPromotionid1.Value.ToString())

                    'Next

                    'Promotion_Id = String.Join(",", AllProduct_id.ToArray())

                    'oclsPromotion.cmp_id = Val(Session("cmp_id"))
                    'oclsPromotion.Promo_name = Rad_Promoname.Text
                    'Dim dt As DataTable = oclsPromotion.check_Duplicate_Promoname()

                    'If dt.Rows.Count > 0 Then
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Promo name already exits');", True)
                    '    Return
                    'Else

                    'End If


                    'If txtDiscount.Text > 0 Then
                    oclsPromotion.product_group = Val(hdproductgroup.Value)
                    oclsPromotion.product_group1 = 0
                    oclsPromotion.product_group2 = 0
                    oclsPromotion.product_group3 = 0
                    oclsPromotion.cmp_id = Val(Session("cmp_id"))
                    oclsPromotion.product_id = Val(hdproduct.Value)
                    oclsPromotion.product_id1 = 0
                    oclsPromotion.product_id2 = 0
                    oclsPromotion.product_id3 = 0
                    oclsPromotion.size_id = Val(ddlSize.SelectedValue)
                    oclsPromotion.size_id1 = 0
                    oclsPromotion.size_id2 = 0
                    oclsPromotion.size_id3 = 0
                    oclsPromotion.Discount_type = Val(Rad_DiscountType.SelectedValue)
                    oclsPromotion.Discount = txtDiscount.Text
                    oclsPromotion.Promo_name = Rad_Promoname.Text
                    oclsPromotion.Voucher_Type = IIf(RadVoucherType.SelectedValue = "", "", RadVoucherType.SelectedValue)
                    oclsPromotion.Voucher_Code = RadVoucherCode.Text
                    oclsPromotion.price_id = 0
                    oclsPromotion.Online_Coupon = Rad_onlinecoupon.Text

                    oclsPromotion.freeProduct = IIf(chkFreeProduct.Checked = True, 1, 0)
                    oclsPromotion.freeProductAmount = txtFPAmount.Text
                    oclsPromotion.AllfreeProduct = IIf(chkAllProd.Checked = True, 1, 0)
                    'oclsPromotion.Recurrence = 0
                    oclsPromotion.EachSpend = IIf(chkOnEachSpend.Checked = True, 1, 0)
                    oclsPromotion.isbarStock = IIf(isbarStock.Checked = True, 1, 0)
                    oclsPromotion.FreeQty = txtQtyofProduct.Text

                    Dim collection As IList(Of RadComboBoxItem) = Rad_Months.CheckedItems

                    If (collection.Count > 0) Then
                        oclsPromotion.Month_flag = 1
                    Else
                        oclsPromotion.Month_flag = 0
                    End If

                    Dim collection_week As IList(Of RadComboBoxItem) = Rad_Week.CheckedItems

                    If (collection_week.Count > 0) Then
                        oclsPromotion.Week_flag = 1
                    Else
                        oclsPromotion.Week_flag = 0
                    End If

                    If rbPromotiontype.SelectedItem.Value Is "0" Then
                        oclsPromotion.duration_flag = 0
                    ElseIf rbPromotiontype.SelectedItem.Value Is "1" Then
                        oclsPromotion.duration_flag = 1
                    Else
                        oclsPromotion.duration_flag = 2
                    End If

                    oclsPromotion.no_of_product = Val(Rad_comboselect.SelectedValue)

                    If rbPromotiontype.SelectedItem.Value Is "0" And Rad_OnDays.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 1
                    ElseIf Rad_OnDays.CheckedItems.Count > 0 And Rad_Months.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 And Rad_Week.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 2
                    ElseIf rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 And Rad_Months.CheckedItems.Count = 0 And Rad_Week.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 3
                        oclsPromotion.Month_flag = 1
                    ElseIf Rad_Week.CheckedItems.Count > 0 And rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 And Rad_Months.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 4
                        oclsPromotion.Week_flag = 1
                    ElseIf Rad_Months.CheckedItems.Count > 0 And Rad_Week.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 5
                    ElseIf Rad_Months.CheckedItems.Count > 0 And Rad_OnDays.CheckedItems.Count > 0 And Rad_Week.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 6
                    ElseIf Rad_Months.CheckedItems.Count > 0 And Rad_Week.CheckedItems.Count > 0 And rad_Days.CheckedItems.Count = 0 And Rad_OnDays.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 7
                        oclsPromotion.Week_flag = 1
                    ElseIf rbPromotiontype.SelectedItem.Value Is "1" And Rad_Week.CheckedItems.Count > 0 And Rad_OnDays.CheckedItems.Count > 0 And Rad_Months.CheckedItems.Count = 0 And rad_Days.CheckedItems.Count = 0 Then
                        oclsPromotion.Recurrence = 9
                    Else
                        oclsPromotion.Recurrence = 8
                        oclsPromotion.Month_flag = 1
                    End If

                    'oclsPromotion.Month_flag = 0 ' check month condition wise
                    oclsPromotion.Months = GetSelectedValue(Rad_Months)
                    'oclsPromotion.Week_flag = 0 ' check week condition wise
                    'oclsPromotion.Weeks = GetSelectedValue(Rad_Week)
                    'oclsPromotion.Days = GetSelectedValue(rad_Days)
                    'oclsPromotion.on_day = GetSelectedValue(Rad_OnDays)

                    Dim sel_Week As String
                    sel_Week = GetSelectedValue(Rad_Week)

                    If sel_Week.ToString Is "" Then
                        oclsPromotion.Weeks = "00"
                    Else
                        oclsPromotion.Weeks = sel_Week
                    End If

                    'oclsPromotion.Weeks = GetSelectedValue(Rad_Week)
                    'oclsPromotion.Days = GetSelectedValue(rad_Days)
                    'oclsPromotion.on_day = GetSelectedValue(Rad_OnDays)

                    Dim sel_Days As String
                    sel_Days = GetSelectedValue(rad_Days)

                    If sel_Days.ToString Is "" Then
                        oclsPromotion.Days = "00"
                    Else
                        oclsPromotion.Days = sel_Days
                    End If

                    Dim sel_OnDays As String
                    sel_OnDays = GetSelectedValue(Rad_OnDays)

                    If sel_OnDays.ToString Is "" Then
                        oclsPromotion.on_day = "00"
                    Else
                        oclsPromotion.on_day = sel_OnDays
                    End If

                    If rbPromotiontype.SelectedItem.Value Is "0" Then
                        oclsPromotion.Recurrence_flag = 1
                    Else
                        oclsPromotion.Recurrence_flag = 0
                    End If

                    oclsPromotion.start_time = txtstarttime.TextWithLiterals

                    If txtendtime.TextWithLiterals = "00:00" Then
                        oclsPromotion.end_time = "23:59"
                    Else
                        oclsPromotion.end_time = txtendtime.TextWithLiterals
                    End If

                    'oclsPromotion.end_time = txtendtime.TextWithLiterals

                    oclsPromotion.start_time1 = txtstarttime1.TextWithLiterals
                    oclsPromotion.end_time1 = txtendtime1.TextWithLiterals
                    oclsPromotion.start_time2 = txtstarttime2.TextWithLiterals
                    oclsPromotion.end_time2 = txtendtime2.TextWithLiterals
                    oclsPromotion.start_time3 = txtstarttime3.TextWithLiterals
                    oclsPromotion.end_time3 = txtendtime3.TextWithLiterals
                    oclsPromotion.startDate = txtstartdate.SelectedDate


                    If chk_endless.Checked = True Then
                        oclsPromotion.EndDate = "#01/1/2099 12:00:00AM#"
                        oclsPromotion.is_endless = 1
                    Else
                        oclsPromotion.EndDate = txtenddate.SelectedDate
                        oclsPromotion.is_endless = 0
                    End If

                    'oclsPromotion.EndDate = txtenddate.SelectedDate

                    If Rad_Product1.SelectedIndex > 0 Or Rad_Product2.SelectedIndex > 0 Or Rad_Product3.SelectedIndex > 0 Then
                        oclsPromotion.combo_flag = 1
                    Else
                        oclsPromotion.combo_flag = 0
                    End If

                    oclsPromotion.combo_product_id = ""

                    oclsPromotion.location_id = Val(RadLocation.SelectedValue)
                    oclsPromotion.machine_id = Val(RadTill.SelectedValue)
                    oclsPromotion.venue_id = Val(RadVenue.SelectedValue)
                    oclsPromotion.Is_active = 1
                    oclsPromotion.Ip_address = Request.UserHostAddress
                    oclsPromotion.login_id = Val(Session("login_id"))

                    If hdPromotionid.Value.ToString() = 0 Then

                        'Dim Promotion_Id As String
                        'Dim AllProduct_id As List(Of String) = New List(Of String)

                        'For Each item1 As GridDataItem In rdProduct.MasterTableView.Items
                        '    Dim hdPromotionid1 As HiddenField = DirectCast(item1.FindControl("hdPromotionid"), HiddenField)

                        '    AllProduct_id.Add(hdPromotionid1.Value.ToString())

                        'Next

                        'Promotion_Id = String.Join(",", AllProduct_id.ToArray())

                        'oclsPromotion.cmp_id = Val(Session("cmp_id"))
                        'oclsPromotion.Promo_name = Rad_Promoname.Text
                        'oclsPromotion.promotion_id = ""
                        'Dim dt As DataTable = oclsPromotion.check_Duplicate_Promoname_Edit()

                        'If dt.Rows.Count > 0 Then
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Promo name already exits');", True)
                        '    Return
                        'Else

                        'End If
                        oclsPromotion.table_promotion_id = 0
                        Dim id As Integer = oclsPromotion.Insert()
                        Insert_PromotType(id)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record inserted successfully');window.location='Promotion.aspx';", True)
                        Session("Success") = "Record inserted successfully"
                        Response.Redirect("Promotion_List.aspx", False)
                    Else

                        Dim Promotion_Id As String
                        Dim AllProduct_id As List(Of String) = New List(Of String)

                        For Each item1 As GridDataItem In rdProduct.MasterTableView.Items
                            Dim hdPromotionid1 As HiddenField = DirectCast(item1.FindControl("hdPromotionid"), HiddenField)

                            AllProduct_id.Add(hdPromotionid1.Value.ToString())

                        Next

                        Promotion_Id = String.Join(",", AllProduct_id.ToArray())

                        oclsPromotion.cmp_id = Val(Session("cmp_id"))
                        oclsPromotion.Promo_name = Rad_Promoname.Text
                        oclsPromotion.promotion_id = Promotion_Id
                        Dim dt As DataTable = oclsPromotion.check_Duplicate_Promoname_Edit()

                        If dt.Rows.Count > 0 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Promo name already exits');", True)
                            Return
                        Else

                        End If

                        oclsPromotion.table_promotion_id = Val(hdPromotionid.Value)
                        oclsPromotion.Update()
                        Insert_PromotType(Val(hdPromotionid.Value))
                        Session("Success") = "Record updated successfully"
                        Response.Redirect("Promotion_List.aspx", False)
                    End If

                    'If Session("table_promotion_id") = Nothing Then
                    '    oclsPromotion.table_promotion_id = 0
                    '    Dim id As Integer = oclsPromotion.Insert()
                    '    Insert_PromotType(id)
                    '    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Record inserted successfully');window.location='Promotion.aspx';", True)
                    '    Session("Success") = "Record inserted successfully"
                    '    Response.Redirect("Promotion_List.aspx", False)
                    'Else
                    '    oclsPromotion.table_promotion_id = Val(Session("table_promotion_id"))
                    '    oclsPromotion.Update()
                    '    Insert_PromotType(Val(Session("table_promotion_id")))
                    '    Session("Success") = "Record updated successfully"
                    '    Response.Redirect("Promotion_List.aspx", False)
                    'End If

                    'End If

                Next
            End If

            Session("table_promotion_id") = Nothing
            Response.Redirect("Promotion_List.aspx", False)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Promotion:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Clear()
        Rad_DiscountType.ClearSelection()
        RadVenue.ClearSelection()
        RadTill.ClearSelection()
        RadLocation.ClearSelection()
        Rad_Product.ClearSelection()
        Rad_Product1.ClearSelection()
        Rad_Product2.ClearSelection()
        Rad_Product3.ClearSelection()
        rad_size.ClearSelection()
        Rad_size1.ClearSelection()
        Rad_size2.ClearSelection()
        Rad_size3.ClearSelection()
        RadPro_group.ClearSelection()
        RadPro_group1.ClearSelection()
        RadPro_group2.ClearSelection()
        RadPro_group3.ClearSelection()
        Rad_comboselect.ClearSelection()
        Rad_Discount.Text = ""
        txtstarttime.Text = "00:00"
        txtendtime.Text = "00:00"
        div_Combo.Visible = False
        div_Combo2.Visible = False
        dv_pro_group.Visible = False
        dv_pro_group1.Visible = False
        dv_pro_group2.Visible = False
        dv_pro_group3.Visible = False
        dv_pro_name.Visible = False
        dv_pro_name1.Visible = False
        dv_pro_name2.Visible = False
        dv_pro_name3.Visible = False
        dv_size.Visible = False
        dv_size1.Visible = False
        dv_size2.Visible = False
        dv_size3.Visible = False
        divPGroup.Visible = False
        Rad_Months.ClearCheckedItems()
        Rad_Week.ClearCheckedItems()
        rad_Days.ClearCheckedItems()
        Rad_OnDays.ClearCheckedItems()
        ChkBuy1Get1.Checked = False
        chkFreeProduct.Checked = False
        chkAllProd.Checked = False
        chkOnEachSpend.Checked = False
        isbarStock.Checked = False
        txtFPAmount.Text = "0"
    End Sub

    Private Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        Try
            Response.Redirect("Promotion_List.aspx", False)
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Rad_DiscountType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If Rad_DiscountType.SelectedItem.Text Is "Combo" Then
                div_Combo.Visible = True
                div_Combo2.Visible = True
                divPGroup.Visible = False
                dv_Discount.Visible = True
                dv_BtnAddProduct.Visible = False
                divcombooption.Visible = True
                divfeeproduct.Visible = False
                'dv_Location.Attributes("style") = "margin-top:15px;"
                'dv_Till.Attributes("style") = "margin-top:15px;"
            ElseIf Rad_DiscountType.SelectedItem.Text Is "Discount %" Or Rad_DiscountType.SelectedItem.Text Is "Discount Amt" Or Rad_DiscountType.SelectedItem.Text Is "Price" Then

                'If Not Session("table_promotion_id") = Nothing Then

                'Else
                '    Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                'End If
                divfeeproduct.Visible = True
                divcombooption.Visible = False
                'divPGroup.Visible = True
                dv_BtnAddProduct.Visible = True
                div_Combo.Visible = False
                div_Combo2.Visible = False
                dv_pro_group.Visible = False
                dv_pro_group1.Visible = False
                dv_pro_group2.Visible = False
                dv_pro_group3.Visible = False
                dv_pro_name.Visible = False
                dv_pro_name1.Visible = False
                dv_pro_name2.Visible = False
                dv_pro_name3.Visible = False
                dv_size.Visible = False
                dv_size1.Visible = False
                dv_size2.Visible = False
                dv_size3.Visible = False
                dv_Discount.Visible = False
                Rad_comboselect.ClearSelection()
                'dv_Location.Attributes("style") = "margin-top:25px;"
                'dv_Till.Attributes("style") = "margin-top:25px;"
            Else
                div_Combo.Visible = False
                div_Combo2.Visible = False
                divPGroup.Visible = False
                dv_Discount.Visible = False
                dv_pro_group.Visible = False
                dv_pro_group1.Visible = False
                dv_pro_group2.Visible = False
                dv_pro_group3.Visible = False
                dv_pro_name.Visible = False
                dv_pro_name1.Visible = False
                dv_pro_name2.Visible = False
                dv_pro_name3.Visible = False
                dv_size.Visible = False
                dv_size1.Visible = False
                dv_size2.Visible = False
                dv_size3.Visible = False
                dv_BtnAddProduct.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rad_comboselect_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If Rad_comboselect.SelectedValue Is "2" Then
                dv_pro_group.Visible = True
                dv_pro_group1.Visible = True
                dv_pro_name.Visible = True
                dv_pro_name1.Visible = True
                dv_size.Visible = True
                dv_size1.Visible = True
                'dv_pro_group2.Attributes("style") = "margin-top:70px;"
                'dv_pro_group3.Attributes("style") = "margin-top:70px;"
                dv_pro_group2.Visible = False
                dv_pro_group3.Visible = False
                dv_pro_name2.Visible = False
                dv_pro_name3.Visible = False
                dv_size2.Visible = False
                dv_size3.Visible = False
            ElseIf Rad_comboselect.SelectedValue Is "3" Then
                dv_pro_group.Visible = True
                dv_pro_group1.Visible = True
                dv_pro_group2.Visible = True
                dv_pro_name.Visible = True
                dv_pro_name1.Visible = True
                dv_pro_name2.Visible = True
                dv_size.Visible = True
                dv_size1.Visible = True
                dv_size2.Visible = True

                'If rbPromotiontype.SelectedItem.Value Is "1" Then
                '    dv_pro_group2.Attributes("style") = "margin-top:60px;"
                '    dv_pro_group3.Attributes("style") = "margin-top:60px;"
                'Else
                '    dv_pro_group2.Attributes("style") = "margin-top:175px;"
                '    dv_pro_group3.Attributes("style") = "margin-top:175px;"
                'End If

                'dv_pro_group2.Attributes("style") = "margin-top:70px;"
                '    dv_pro_group3.Attributes("style") = "margin-top:70px;"
                dv_pro_group3.Visible = False
                dv_pro_name3.Visible = False
                dv_size3.Visible = False
            ElseIf Rad_comboselect.SelectedValue Is "4" Then
                dv_pro_group.Visible = True
                dv_pro_group1.Visible = True
                dv_pro_group2.Visible = True
                dv_pro_group3.Visible = True
                dv_pro_name.Visible = True
                dv_pro_name1.Visible = True
                dv_pro_name2.Visible = True
                dv_pro_name3.Visible = True
                dv_size.Visible = True
                dv_size1.Visible = True
                dv_size2.Visible = True
                dv_size3.Visible = True
                'If rbPromotiontype.SelectedItem.Value Is "1" Then
                '    dv_pro_group2.Attributes("style") = "margin-top:60px;"
                '    dv_pro_group3.Attributes("style") = "margin-top:60px;"
                'Else
                '    dv_pro_group2.Attributes("style") = "margin-top:175px;"
                '    dv_pro_group3.Attributes("style") = "margin-top:175px;"
                'End If
                'dv_pro_group2.Attributes("style") = "margin-top:70px;"
                'dv_pro_group3.Attributes("style") = "margin-top:70px;"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindGrid()
        Try
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsPromotion.Select_Product_For_Promotoin()
            Dim dv As DataView = dt.DefaultView
            If dt.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdProduct.DataSource = dv
                rdProduct.DataBind()
            Else
                rdProduct.Enabled = True
                rdProduct.Visible = True
                rdProduct.DataSource = String.Empty
                rdProduct.DataBind()
            End If


        Catch ex As Exception
            LogHelper.Error("Promotion:BindGrid:" + ex.Message)
        End Try
    End Sub

    Protected Sub rdproduct_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rdProduct.ItemDataBound
        Try

            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

                oclsBind.BindSizeUnitByProduct(CType(e.Item.FindControl("ddlsize"), RadComboBox), Val(Session("cmp_id")), CType(e.Item.FindControl("hdproduct"), HiddenField).Value)

                If Session("table_promotion_id") = Nothing Then
                    'Dim dt1 As DataTable = DirectCast(ViewState("Promotion_Data"), DataTable)

                    'For Each row As DataRow In dt1.Rows
                    '    If CType(e.Item.FindControl("hdnrow_Id"), HiddenField).Value.ToString() = row("row_Id").ToString() Then
                    '        CType(e.Item.FindControl("ddlproduct"), RadComboBox).SelectedValue = row("Product_id").ToString()
                    '        CType(e.Item.FindControl("ddlproductgroup"), RadComboBox).SelectedValue = row("Product_group").ToString()
                    '        'CType(e.Item.FindControl("ddltotalunit"), RadComboBox).SelectedValue = row("Unit_Id").ToString()
                    '        CType(e.Item.FindControl("ddltotalunit"), RadComboBox).FindItemByText("Qty").Selected = True
                    '    End If

                    'Next
                Else
                    Dim dt1 As DataTable = DirectCast(ViewState("Promotion_Data"), DataTable)

                    For Each row As DataRow In dt1.Rows
                        If CType(e.Item.FindControl("hdproduct"), HiddenField).Value.ToString() = row("product_id").ToString() Then

                            CType(e.Item.FindControl("txtDiscount"), RadTextBox).Text = row("Discount").ToString()
                            'CType(e.Item.FindControl("ddlsize"), RadComboBox).SelectedValue = row("size_id").ToString()
                            CType(e.Item.FindControl("hdPromotionid"), HiddenField).Value = row("table_promotion_id").ToString()
                        End If
                    Next

                    Dim dt As DataTable = DirectCast(ViewState("Promotion_Data1"), DataTable)

                    For Each row As DataRow In dt.Rows
                        If CType(e.Item.FindControl("hdproduct"), HiddenField).Value.ToString() = row("product_id").ToString() Then
                            CType(e.Item.FindControl("ddlsize"), RadComboBox).SelectedValue = row("size_id").ToString()
                        End If
                    Next

                End If

            End If
        Catch ex As Exception
            LogHelper.Error("Stock_Management_Master:rdProduct_ItemDataBound:" + ex.Message)
        End Try
    End Sub
    Protected Sub RadVenue_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindLocationByVenue(RadLocation, Val(Session("cmp_id")), RadVenue.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub RadLocation_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            oclsBind.BindMachineByLocation(RadTill, Val(Session("cmp_id")), RadLocation.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Insert_PromotType(ByVal id As Integer)
        Try

            oclsPromotion.table_promotion_id = id
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            oclsPromotion.Delete_Edit()

            Dim collection As IList(Of RadComboBoxItem) = Rad_Months.CheckedItems

            For Each item As RadComboBoxItem In collection

                Dim value As String = item.Value

                oclsPromotion.table_promotion_id = id
                oclsPromotion.promo_type = 2
                oclsPromotion.promo_value = item.Value
                oclsPromotion.cmp_id = Val(Session("cmp_id"))
                oclsPromotion.Ip_address = Request.UserHostAddress
                oclsPromotion.login_id = Val(Session("login_id"))
                oclsPromotion.Insert_PromotionType()
            Next

            Dim collection_Week As IList(Of RadComboBoxItem) = Rad_Week.CheckedItems

            For Each item As RadComboBoxItem In collection_Week

                Dim value As String = item.Value

                oclsPromotion.table_promotion_id = id
                oclsPromotion.promo_type = 1
                oclsPromotion.promo_value = item.Value
                oclsPromotion.cmp_id = Val(Session("cmp_id"))
                oclsPromotion.Ip_address = Request.UserHostAddress
                oclsPromotion.login_id = Val(Session("login_id"))
                oclsPromotion.Insert_PromotionType()
            Next

            Dim collection_Days As IList(Of RadComboBoxItem) = rad_Days.CheckedItems

            For Each item As RadComboBoxItem In collection_Days

                Dim value As String = item.Value

                oclsPromotion.table_promotion_id = id
                oclsPromotion.promo_type = 3
                oclsPromotion.promo_value = item.Value
                oclsPromotion.cmp_id = Val(Session("cmp_id"))
                oclsPromotion.Ip_address = Request.UserHostAddress
                oclsPromotion.login_id = Val(Session("login_id"))
                oclsPromotion.Insert_PromotionType()
            Next

            Dim collection_OnDays As IList(Of RadComboBoxItem) = Rad_OnDays.CheckedItems

            For Each item As RadComboBoxItem In collection_OnDays

                Dim value As String = item.Value

                oclsPromotion.table_promotion_id = id
                oclsPromotion.promo_type = 4
                oclsPromotion.promo_value = item.Value
                oclsPromotion.cmp_id = Val(Session("cmp_id"))
                oclsPromotion.Ip_address = Request.UserHostAddress
                oclsPromotion.login_id = Val(Session("login_id"))
                oclsPromotion.Insert_PromotionType()
            Next

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Promotion:btnSave_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub BindPromotion()
        Try
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            'oclsPromotion.table_promotion_id = Val(Session("table_promotion_id"))
            oclsPromotion.Promo_name = Session("table_promotion_id")
            Dim dt As DataTable = oclsPromotion.SelectAll()

            ViewState("Promotion_Data_Edit") = dt

            Rad_Promoname.Text = dt.Rows(0)("Promo_name")
            RadVoucherType.SelectedValue = dt.Rows(0)("Voucher_Type")

            If RadVoucherType.SelectedValue IsNot "" Then
                dv_VoucherCode.Visible = True
            Else
                dv_VoucherCode.Visible = False
            End If

            RadVoucherCode.Text = dt.Rows(0)("Voucher_Code")
            txtstartdate.SelectedDate = dt.Rows(0)("startDate")
            txtenddate.SelectedDate = dt.Rows(0)("EndDate")
            txtstarttime.TextWithLiterals = dt.Rows(0)("start_time")
            txtendtime.TextWithLiterals = dt.Rows(0)("end_time")

            txtstarttime1.TextWithLiterals = dt.Rows(0)("start_time1")
            txtendtime1.TextWithLiterals = dt.Rows(0)("end_time1")
            txtstarttime2.TextWithLiterals = dt.Rows(0)("start_time2")
            txtendtime2.TextWithLiterals = dt.Rows(0)("end_time2")
            txtstarttime3.TextWithLiterals = dt.Rows(0)("start_time3")
            txtendtime3.TextWithLiterals = dt.Rows(0)("end_time3")
            Rad_onlinecoupon.Text = dt.Rows(0)("Online_Coupon")
            chkMixnMatch.Checked = IIf(dt.Rows(0)("mixMatch") = "0", False, True)
            ChkBuy1Get1.Checked = IIf(dt.Rows(0)("buy1_Get1") = "0", False, True)

            chkFreeProduct.Checked = IIf(dt.Rows(0)("freeProduct") = "0", False, True)
            txtFPAmount.Text = dt.Rows(0)("FreeProductAmount").ToString()
            chkAllProd.Checked = IIf(dt.Rows(0)("allfreeProduct") = "0", False, True)

            chkOnEachSpend.Checked = IIf(dt.Rows(0)("oneachspend") = "0", False, True)
            isbarStock.Checked = IIf(dt.Rows(0)("is_BarStockExchange") = "0", False, True)

            txtQtyofProduct.Text = dt.Rows(0)("FreeQty").ToString()

            chkisFlatAmount.Checked = IIf(dt.Rows(0)("isFlatAmount") = "0", False, True)
            If (txtstarttime.TextWithLiterals = "00:00" And txtendtime.TextWithLiterals = "00:00") Or (txtstarttime.TextWithLiterals = "00:00" And txtendtime.TextWithLiterals = "23:59") Then
                chk_AllDays.Checked = True
            Else
                chk_AllDays.Checked = False
                div_Slot1.Visible = True
                div_Slot2.Visible = True
                div_Slot3.Visible = True
                div_Slot4.Visible = True
                div_time1.Visible = True
                div_etime1.Visible = True
                'div_AllDays.Attributes("style") = "margin-top:10px;"
                'div_time2.Attributes("style") = "margin-top:0px;"
                'div_entime2.Attributes("style") = "margin-top:0px;"
                'div_time3.Attributes("style") = "margin-top:0px;"
                'div_entime3.Attributes("style") = "margin-top:0px;"
            End If

            If txtstarttime1.TextWithLiterals = "00:00" And txtendtime1.TextWithLiterals = "00:00" Then

            Else
                If chk_AllDays.Checked = True Then
                    chk_solt1.Checked = False
                    div_sttime1.Visible = False
                    div_endtime1.Visible = False
                Else
                    chk_solt1.Checked = True
                    div_sttime1.Visible = True
                    div_endtime1.Visible = True
                End If

            End If

            If txtstarttime2.TextWithLiterals = "00:00" And txtendtime2.TextWithLiterals = "00:00" Then

            Else
                If chk_AllDays.Checked = True Then
                    chk_solt2.Checked = False
                    div_time2.Visible = False
                    div_entime2.Visible = False
                Else
                    chk_solt2.Checked = True
                    div_time2.Visible = True
                    div_entime2.Visible = True
                End If

            End If

            If txtstarttime3.TextWithLiterals = "00:00" And txtendtime3.TextWithLiterals = "00:00" Then

            Else
                If chk_AllDays.Checked = True Then
                    chk_solt3.Checked = False
                    div_time3.Visible = False
                    div_entime3.Visible = False
                Else
                    chk_solt3.Checked = True
                    div_time3.Visible = True
                    div_entime3.Visible = True
                End If

            End If

            If dt.Rows(0)("is_endless") = 1 Then
                div_EndDate.Visible = False
                chk_endless.Checked = True
            Else
                div_EndDate.Visible = True
                chk_endless.Checked = False
            End If

            If dt.Rows(0)("combo_flag") = 1 Then
                Rad_DiscountType.SelectedValue = 4
            Else
                Rad_DiscountType.SelectedValue = dt.Rows(0)("Discount_type")
            End If
            'Rad_DiscountType.SelectedValue = dt.Rows(0)("Discount_type")

            Rad_comboselect.SelectedValue = dt.Rows(0)("no_of_product")

            If dt.Rows(0)("duration_flag") = "2" Then
                rbPromotiontype.SelectedValue = 2
                dv_Month.Visible = True
                dv_Week.Visible = True
                dv_Days.Visible = False

                'Dim week As String = Convert.ToString(dt.Rows(0)("Weeks").ToString())

                'If week.ToString() IsNot "00" Then
                '    dv_OnDays.Visible = False
                '    dv_Week.Visible = True
                '    'div_time2.Attributes("style") = "margin-top:65px;"
                '    'div_time3.Attributes("style") = "margin-top:65px;"
                'Else
                '    dv_OnDays.Visible = True
                '    dv_Week.Visible = False
                '    div_time2.Attributes("style") = "margin-top:0px;"
                '    div_time3.Attributes("style") = "margin-top:0px;"
                '    dv_Week.Attributes("style") = "margin-top:20px;"
                'End If

                If dt.Rows(0)("Weeks").ToString() = "00" Then
                    dv_OnDays.Visible = True
                    dv_Week.Visible = False
                    'div_time2.Attributes("style") = "margin-top:0px;"
                    'div_time3.Attributes("style") = "margin-top:0px;"
                    'dv_Week.Attributes("style") = "margin-top:20px;"
                Else
                    dv_OnDays.Visible = False
                    dv_Week.Visible = True
                End If

                'dv_OnDays.Visible = True
                'dv_Week.Attributes("style") = "margin-top:20px;"
                'dv_pro_group2.Attributes("style") = "margin-top:40px;"
                'dv_pro_group3.Attributes("style") = "margin-top:40px;"
            ElseIf dt.Rows(0)("duration_flag") = "1" Then
                rbPromotiontype.SelectedValue = 1
                dv_Month.Visible = False
                dv_Week.Visible = True
                dv_Days.Visible = False
                dv_OnDays.Visible = True
                'dv_Week.Attributes("style") = "float:right;margin-top:20px;"
                'dv_pro_group2.Attributes("style") = "margin-top:40px;"
                'dv_pro_group3.Attributes("style") = "margin-top:40px;"
            Else
                rbPromotiontype.SelectedValue = 0
                dv_Month.Visible = False
                dv_Week.Visible = False
                dv_Days.Visible = False
                dv_OnDays.Visible = True
                'dv_pro_group2.Attributes("style") = "margin-top:40px;"
                'dv_pro_group3.Attributes("style") = "margin-top:40px;"
            End If

            oclsBind.BindVenue(RadVenue, Val(Session("cmp_id")))
            RadVenue.SelectedValue = dt.Rows(0)("venue_id")

            RadLocation.ClearSelection()
            If RadVenue.SelectedValue = "0" Then
                oclsBind.BindLocationByVenue(RadLocation, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindLocationByVenue(RadLocation, Val(Session("cmp_id")), Val(RadVenue.SelectedValue))
            End If
            RadLocation.SelectedValue = dt.Rows(0)("location_id").ToString


            RadTill.ClearSelection()
            If RadLocation.SelectedValue = "0" Then
                oclsBind.BindMachineByLocation(RadTill, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindMachineByLocation(RadTill, Val(Session("cmp_id")), Val(RadLocation.SelectedValue))
            End If
            RadTill.SelectedValue = dt.Rows(0)("machine_id").ToString


            If Rad_DiscountType.SelectedValue IsNot "4" Then
                divPGroup.Visible = True
                dv_BtnAddProduct.Visible = True
                divcombooption.Visible = False
                divfeeproduct.Visible = True
            Else
                divfeeproduct.Visible = False
                div_Combo.Visible = True
                div_Combo2.Visible = True
                divcombooption.Visible = True
                Rad_Discount.Text = dt.Rows(0)("Discount").ToString
                dv_Discount.Visible = True
                dv_BtnAddProduct.Visible = False
                If Rad_comboselect.SelectedValue Is "2" Then
                    dv_pro_group.Visible = True
                    dv_pro_group1.Visible = True
                    dv_pro_name.Visible = True
                    dv_pro_name1.Visible = True
                    dv_size.Visible = True
                    dv_size1.Visible = True
                    'dv_pro_group2.Attributes("style") = "margin-top:70px;"
                    'dv_pro_group3.Attributes("style") = "margin-top:70px;"
                    dv_pro_group2.Visible = False
                    dv_pro_group3.Visible = False
                    dv_pro_name2.Visible = False
                    dv_pro_name3.Visible = False
                    dv_size2.Visible = False
                    dv_size3.Visible = False
                ElseIf Rad_comboselect.SelectedValue Is "3" Then
                    dv_pro_group.Visible = True
                    dv_pro_group1.Visible = True
                    dv_pro_group2.Visible = True
                    dv_pro_name.Visible = True
                    dv_pro_name1.Visible = True
                    dv_pro_name2.Visible = True
                    dv_size.Visible = True
                    dv_size1.Visible = True
                    dv_size2.Visible = True

                    'If rbPromotiontype.SelectedItem.Value Is "2" Then
                    '    dv_pro_group2.Attributes("style") = "margin-top:205px;"
                    '    dv_pro_group3.Attributes("style") = "margin-top:205px;"
                    'Else
                    '    dv_pro_group2.Attributes("style") = "margin-top:75px;"
                    '    dv_pro_group3.Attributes("style") = "margin-top:75px;"
                    'End If

                    'dv_pro_group2.Attributes("style") = "margin-top:70px;"
                    '    dv_pro_group3.Attributes("style") = "margin-top:70px;"
                    dv_pro_group3.Visible = False
                    dv_pro_name3.Visible = False
                    dv_size3.Visible = False
                ElseIf Rad_comboselect.SelectedValue Is "4" Then
                    dv_pro_group.Visible = True
                    dv_pro_group1.Visible = True
                    dv_pro_group2.Visible = True
                    dv_pro_group3.Visible = True
                    dv_pro_name.Visible = True
                    dv_pro_name1.Visible = True
                    dv_pro_name2.Visible = True
                    dv_pro_name3.Visible = True
                    dv_size.Visible = True
                    dv_size1.Visible = True
                    dv_size2.Visible = True
                    dv_size3.Visible = True
                    'If rbPromotiontype.SelectedItem.Value Is "2" Then
                    '    dv_pro_group2.Attributes("style") = "margin-top:205px;"
                    '    dv_pro_group3.Attributes("style") = "margin-top:205px;"
                    'Else
                    '    dv_pro_group2.Attributes("style") = "margin-top:75px;"
                    '    dv_pro_group3.Attributes("style") = "margin-top:75px;"
                    'End If
                    'dv_pro_group2.Attributes("style") = "margin-top:70px;"
                    'dv_pro_group3.Attributes("style") = "margin-top:70px;"
                End If

            End If


            oclsBind.BindProductGroup(RadPro_group, Val(Session("cmp_id")))
            oclsBind.BindProductGroup(RadPro_group1, Val(Session("cmp_id")))
            oclsBind.BindProductGroup(RadPro_group2, Val(Session("cmp_id")))
            oclsBind.BindProductGroup(RadPro_group3, Val(Session("cmp_id")))
            RadPro_group.SelectedValue = dt.Rows(0)("product_group")
            RadPro_group1.SelectedValue = dt.Rows(0)("product_group1")
            RadPro_group2.SelectedValue = dt.Rows(0)("product_group2")
            RadPro_group3.SelectedValue = dt.Rows(0)("product_group3")


            Rad_Product.ClearSelection()
            If RadPro_group.SelectedValue = "0" Then
                oclsBind.BindProductByProductGroup(Rad_Product, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindProductByProductGroup(Rad_Product, Val(Session("cmp_id")), Val(RadPro_group.SelectedValue))
            End If
            Rad_Product.SelectedValue = dt.Rows(0)("product_id")

            rad_size.ClearSelection()
            If Rad_Product.SelectedValue = "0" Then
                oclsBind.BindSizeUnitByProduct(rad_size, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindSizeUnitByProduct(rad_size, Val(Session("cmp_id")), Val(Rad_Product.SelectedValue))
            End If
            rad_size.SelectedValue = dt.Rows(0)("size_id")


            Rad_Product1.ClearSelection()
            If RadPro_group1.SelectedValue = "0" Then
                oclsBind.BindProductByProductGroup(Rad_Product1, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindProductByProductGroup(Rad_Product1, Val(Session("cmp_id")), Val(RadPro_group1.SelectedValue))
            End If
            Rad_Product1.SelectedValue = dt.Rows(0)("product_id1")

            Rad_size1.ClearSelection()
            If Rad_Product1.SelectedValue = "0" Then
                oclsBind.BindSizeUnitByProduct(Rad_size1, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindSizeUnitByProduct(Rad_size1, Val(Session("cmp_id")), Val(Rad_Product1.SelectedValue))
            End If
            Rad_size1.SelectedValue = dt.Rows(0)("size_id1")


            Rad_Product2.ClearSelection()
            If RadPro_group2.SelectedValue = "0" Then
                oclsBind.BindProductByProductGroup(Rad_Product2, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindProductByProductGroup(Rad_Product2, Val(Session("cmp_id")), Val(RadPro_group2.SelectedValue))
            End If
            Rad_Product2.SelectedValue = dt.Rows(0)("product_id2")

            Rad_size2.ClearSelection()
            If Rad_Product2.SelectedValue = "0" Then
                oclsBind.BindSizeUnitByProduct(Rad_size2, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindSizeUnitByProduct(Rad_size2, Val(Session("cmp_id")), Val(Rad_Product2.SelectedValue))
            End If
            Rad_size2.SelectedValue = dt.Rows(0)("size_id2")


            Rad_Product3.ClearSelection()
            If RadPro_group3.SelectedValue = "0" Then
                oclsBind.BindProductByProductGroup(Rad_Product3, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindProductByProductGroup(Rad_Product3, Val(Session("cmp_id")), Val(RadPro_group3.SelectedValue))
            End If
            Rad_Product3.SelectedValue = dt.Rows(0)("product_id3")

            Rad_size3.ClearSelection()
            If Rad_Product3.SelectedValue = "0" Then
                oclsBind.BindSizeUnitByProduct(Rad_size3, Val(Session("cmp_id")), 0)
            Else
                oclsBind.BindSizeUnitByProduct(Rad_size3, Val(Session("cmp_id")), Val(Rad_Product3.SelectedValue))
            End If
            Rad_size3.SelectedValue = dt.Rows(0)("size_id3")


            Dim collection As IList(Of RadComboBoxItem) = Rad_Months.Items
            Dim stArr As String() = dt.Rows(0)("Months").ToString().Split("#")

            For i As Integer = 0 To stArr.Length - 1
                For Each item As RadComboBoxItem In collection
                    If stArr(i).ToString = item.Value Then
                        item.Checked = True
                        Exit For
                    End If
                Next
            Next

            Dim collection_week As IList(Of RadComboBoxItem) = Rad_Week.Items
            Dim stArr_week As String() = dt.Rows(0)("Weeks").ToString().Split("#")

            For i As Integer = 0 To stArr_week.Length - 1
                For Each item As RadComboBoxItem In collection_week
                    If stArr_week(i).ToString = item.Value Then
                        item.Checked = True
                        Exit For
                    End If
                Next
            Next

            Dim collection_Days As IList(Of RadComboBoxItem) = rad_Days.Items
            Dim stArr_Days As String() = dt.Rows(0)("Days").ToString().Split("#")

            For i As Integer = 0 To stArr_Days.Length - 1
                For Each item As RadComboBoxItem In collection_Days
                    If stArr_Days(i).ToString = item.Value Then
                        item.Checked = True
                        Exit For
                    End If
                Next
            Next

            Dim collection_OnDays As IList(Of RadComboBoxItem) = Rad_OnDays.Items
            Dim stArr_OnDays As String() = dt.Rows(0)("on_day").ToString().Split("#")

            For i As Integer = 0 To stArr_OnDays.Length - 1
                For Each item As RadComboBoxItem In collection_OnDays
                    If stArr_OnDays(i).ToString = item.Value Then
                        item.Checked = True
                        Exit For
                    End If
                Next
            Next


            Dim Pro_Id As String

            Dim AllProduct_id As List(Of String) = New List(Of String)

            For Each row As DataRow In dt.Rows
                AllProduct_id.Add(row("product_id").ToString())
            Next

            Pro_Id = String.Join(",", AllProduct_id.ToArray())


            oclsPromotion.Pro_Id = Pro_Id
            'oclsPromotion.Pro_Id = Rad_Product.SelectedValue.ToString()
            oclsPromotion.cmp_id = Val(Session("cmp_id"))
            Dim dt1 As DataTable = oclsPromotion.Select_Product_For_Promotoin()
            Dim dv As DataView = dt1.DefaultView

            For Each row1 As DataRow In dt.Rows
                For Each row2 As DataRow In dt1.Rows
                    If row1("product_id").ToString() = row2("product_id").ToString() Then
                        row2("Discount") = row1("Discount")
                        row2("table_promotion_id") = row1("table_promotion_id")
                    End If

                Next
            Next

            ViewState("Promotion_Data") = dt1
            ViewState("Promotion_Data1") = dt


            If dt1.Rows.Count > 0 And dv.ToTable.Rows.Count > 0 Then
                rdProduct.DataSource = dv
                rdProduct.DataBind()

            Else
                rdProduct.Enabled = True
                rdProduct.Visible = True
                rdProduct.DataSource = String.Empty
                rdProduct.DataBind()
            End If

            'BindGrid()

        Catch ex As Exception
            LogHelper.Error("Promotion:BindGrid:" + ex.Message)
        End Try
    End Sub
    Protected Sub chk_endless_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_endless.Checked = True Then
                div_EndDate.Visible = False
            Else
                div_EndDate.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub bindIngredientGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredient()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
                rdcopyProduct.DataBind()
            Else
                rdcopyProduct.DataSource = String.Empty
                rdcopyProduct.DataBind()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Error in processing request');", True)
            LogHelper.Error("Change_Product_Group_List:bindGrid:" + ex.Message)
        End Try

    End Sub

    Private Sub btn_PromotionProductCancel_Click(sender As Object, e As EventArgs) Handles btn_PromotionProductCancel.Click
        Try
            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub rdcopyProduct_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rdcopyProduct.NeedDataSource
        Try
            ReBindGrid()
        Catch ex As Exception
            LogHelper.Error("Product_Master:rdcopyProduct_NeedDataSource:" + ex.Message)
        End Try
    End Sub

    Public Sub ReBindGrid()
        Try
            oclsProduct.cmp_id = Val(Session("cmp_id"))
            Dim dt As DataTable = oclsProduct.Get_M_product_ForIngredient()

            If dt.Rows.Count > 0 Then
                rdcopyProduct.DataSource = dt
            Else
                rdcopyProduct.DataSource = String.Empty
            End If

        Catch ex As Exception
            LogHelper.Error("Product_Master:ReBindGrid():" + ex.Message)
        End Try
    End Sub

    Private Sub btn_PromotionProduct_Click(sender As Object, e As EventArgs) Handles btn_PromotionProduct.Click
        Try
            Dim dt1 As New DataTable

            dt1.Columns.Add("product_id", GetType(Integer))

            For Each item As GridItem In rdcopyProduct.MasterTableView.Items
                Dim value As String
                Dim dataitem As GridDataItem = CType(item, GridDataItem)
                Dim cell As TableCell = dataitem("ClientSelectColumn")
                Dim checkBox As CheckBox = CType(cell.Controls(0), CheckBox)

                If checkBox.Checked Then
                    value = dataitem.GetDataKeyValue("product_id").ToString()
                    Session("product_For_Ingredient_id") = value.ToString()

                    Dim row As DataRow = dt1.NewRow()
                    row("product_id") = dataitem.GetDataKeyValue("product_id").ToString()

                    dt1.Rows.Add(row)

                End If

            Next

            ViewState("Product_Data") = dt1

            If Session("product_For_Ingredient_id") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Plese Select Checkbox');", True)
            Else

                If Session("table_promotion_id") = Nothing Then
                    Dim Pro_Id As String

                    Dim AllProduct_id As List(Of String) = New List(Of String)

                    For Each row As DataRow In dt1.Rows
                        AllProduct_id.Add(row("product_id").ToString())
                    Next

                    Pro_Id = String.Join(",", AllProduct_id.ToArray())

                    oclsPromotion.Pro_Id = Pro_Id
                    oclsPromotion.cmp_id = Val(Session("cmp_id"))
                    Dim dt As DataTable = oclsPromotion.Select_Product_For_Promotoin()

                    Dim dtingredient As DataTable = DirectCast(ViewState("Promotion_Data"), DataTable)

                    If dtingredient IsNot Nothing Then
                        Dim rows_to_remove As New List(Of DataRow)()

                        For Each row1 As DataRow In dtingredient.Rows
                            For Each row2 As DataRow In dt.Rows
                                If row1("product_id").ToString() = row2("product_id").ToString() Then
                                    rows_to_remove.Add(row2)
                                End If

                            Next
                        Next

                        For Each row As DataRow In rows_to_remove
                            dt.Rows.Remove(row)
                            dt.AcceptChanges()
                        Next

                        dtingredient.Merge(dt, True, MissingSchemaAction.Ignore)
                        ViewState("Promotion_Data") = dtingredient

                        Dim dv As DataView = dtingredient.DefaultView

                        ViewState("Promotion_Data") = dtingredient

                        divPGroup.Visible = True

                        rdProduct.DataSource = dv
                        rdProduct.DataBind()
                        upWall.Update()


                    Else
                        Dim dv As DataView = dt.DefaultView

                        ViewState("Promotion_Data") = dt

                        divPGroup.Visible = True

                        rdProduct.DataSource = dv
                        rdProduct.DataBind()
                        upWall.Update()

                    End If

                    Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                Else
                    Dim Pro_Id As String

                    Dim AllProduct_id As List(Of String) = New List(Of String)

                    For Each row As DataRow In dt1.Rows
                        AllProduct_id.Add(row("product_id").ToString())
                    Next

                    Dim dtingredient As DataTable = DirectCast(ViewState("Promotion_Data"), DataTable)
                    For Each row As DataRow In dtingredient.Rows
                        AllProduct_id.Add(row("product_id").ToString())
                    Next

                    Pro_Id = String.Join(",", AllProduct_id.ToArray())

                    oclsPromotion.Pro_Id = Pro_Id
                    oclsPromotion.cmp_id = Val(Session("cmp_id"))
                    Dim dt As DataTable = oclsPromotion.Select_Product_For_Promotoin()

                    Dim rows_to_remove As New List(Of DataRow)()

                    For Each row1 As DataRow In dtingredient.Rows
                        For Each row2 As DataRow In dt.Rows
                            If row1("product_id").ToString() = row2("product_id").ToString() Then
                                'row2.Delete()
                                'dt.AcceptChanges()
                                rows_to_remove.Add(row2)
                            End If

                        Next
                    Next

                    For Each row As DataRow In rows_to_remove
                        dt.Rows.Remove(row)
                        dt.AcceptChanges()
                    Next

                    dtingredient.Merge(dt, True, MissingSchemaAction.Ignore)
                    ViewState("Promotion_Data") = dtingredient



                    Dim dv As DataView = dtingredient.DefaultView

                    ViewState("Promotion_Data") = dtingredient

                    rdProduct.DataSource = dv
                    rdProduct.DataBind()
                    upWall.Update()

                    Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('" + ex.Message.ToString().Replace("'", "") + "');", True)
            LogHelper.Error("Promotion:btnSave_Click:" + ex.Message)
        End Try
    End Sub
    Protected Sub Rad_Week_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If Rad_Week.CheckedItems.Count > 0 Then

                If rbPromotiontype.SelectedValue Is "2" Then
                    dv_OnDays.Visible = False
                    'div_Slot3.Attributes("style") = "margin-top:95px;"
                    'div_Slot4.Attributes("style") = "margin-top:95px;"
                    'dv_pro_group2.Attributes("style") = "margin-top:175px;"
                    'dv_pro_group3.Attributes("style") = "margin-top:175px;"
                Else
                    dv_OnDays.Visible = True
                End If
            Else
                dv_OnDays.Visible = True
                'div_Slot3.Attributes("style") = "margin-top:30px;"
                'div_Slot4.Attributes("style") = "margin-top:30px;"
                'div_time2.Attributes("style") = "margin-top:0px;"
                'div_time3.Attributes("style") = "margin-top:0px;"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Rad_OnDays_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If Rad_OnDays.CheckedItems.Count > 0 Then

                If rbPromotiontype.SelectedValue Is "2" Then
                    dv_Week.Visible = False
                ElseIf rbPromotiontype.SelectedValue Is "0" Then
                    dv_Week.Visible = False
                Else
                    dv_Week.Visible = True
                End If
            Else
                dv_Week.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtDiscount_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim txtDiscount As RadTextBox = CType(sender, RadTextBox)
            Dim editItem1 As GridDataItem = CType(txtDiscount.NamingContainer, GridDataItem)
            Dim txtPrice As HiddenField = CType(editItem1.FindControl("hdprice"), HiddenField)

            Dim price As Decimal = Convert.ToDecimal(txtPrice.Value.ToString)
            Dim Discount As Decimal = Convert.ToDecimal(txtDiscount.Text)

            If Rad_DiscountType.SelectedValue Is "2" Or Rad_DiscountType.SelectedValue Is "3" Then
                If price >= Discount Then
                    btnSave.Enabled = True
                Else
                    btnSave.Enabled = False
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "test", "alert('Please enter discount less than product price');", True)
                    Return
                End If
            End If
            SavePromotionRecordInDT()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnAddNewProduct_Click(sender As Object, e As EventArgs)
        Try
            up_Pro_Ingredient.Update()
            Dim script As String = "function f(){$find(""" + rwEntryDetails.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "keyImmigration", script, True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SavePromotionRecordInDT()
        Try

            Dim dt1 As DataTable = DirectCast(ViewState("Promotion_Data"), DataTable)

            For Each item As GridDataItem In rdProduct.Items
                For Each row As DataRow In dt1.Rows
                    If CType(item.FindControl("hdproduct"), HiddenField).Value.ToString() = row("product_id").ToString() Then
                        row("table_promotion_id") = Val(CType(item.FindControl("hdPromotionid"), HiddenField).Value)
                        row("group_id") = Val(CType(item.FindControl("hdproductgroup"), HiddenField).Value)
                        row("Product_group") = CType(item.FindControl("lbproductgroup"), Label).Text
                        row("Product_Id") = Val(CType(item.FindControl("hdproduct"), HiddenField).Value)
                        row("Product_Name") = CType(item.FindControl("lbproductname"), Label).Text
                        row("Price") = Val(CType(item.FindControl("hdprice"), HiddenField).Value)
                        row("Discount") = CType(item.FindControl("txtDiscount"), RadTextBox).Text
                    End If
                Next
            Next

            'bind_drp_Size()

        Catch ex As Exception
            LogHelper.Error("Product_Master:SaveIngredientRecordInDT:" + ex.Message)
        End Try
    End Sub
    Protected Sub chk_solt1_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_solt1.Checked = True Then
                div_sttime1.Visible = True
                div_endtime1.Visible = True
            Else
                div_sttime1.Visible = False
                div_endtime1.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub chk_solt2_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_solt2.Checked = True Then
                div_time2.Visible = True
                div_entime2.Visible = True

            Else
                div_time2.Visible = False
                div_entime2.Visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub chk_solt3_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_solt3.Checked = True Then
                div_time3.Visible = True
                div_entime3.Visible = True

            Else
                div_time3.Visible = False
                div_entime3.Visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub chk_AllDays_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chk_AllDays.Checked = True Then
                div_Slot1.Visible = False
                div_Slot2.Visible = False
                div_Slot3.Visible = False
                div_Slot4.Visible = False
                div_time1.Visible = False
                div_etime1.Visible = False
                div_sttime1.Visible = False
                div_endtime1.Visible = False
                div_time2.Visible = False
                div_time3.Visible = False
                div_entime2.Visible = False
                div_entime3.Visible = False

                txtstarttime2.TextWithLiterals = "00:00"
                txtstarttime3.TextWithLiterals = "00:00"

                txtendtime2.TextWithLiterals = "00:00"
                txtendtime3.TextWithLiterals = "00:00"

                txtstarttime.TextWithLiterals = "00:00"
                txtstarttime1.TextWithLiterals = "00:00"

                txtendtime.TextWithLiterals = "00:00"
                txtendtime1.TextWithLiterals = "00:00"

                chk_solt1.Checked = False
                chk_solt2.Checked = False
                chk_solt3.Checked = False

                'dv_pro_group2.Attributes("style") = "margin-top:80px;"
                'dv_pro_group3.Attributes("style") = "margin-top:80px;"

            Else
                div_Slot1.Visible = True
                div_Slot2.Visible = True
                div_Slot3.Visible = True
                div_Slot4.Visible = True
                div_time1.Visible = True
                div_etime1.Visible = True
                'If rbPromotiontype.SelectedItem.Value Is "2" Then
                '    'div_AllDays.Attributes("style") = "margin-top:10px;"
                '    'If dv_OnDays.Visible = True Then
                '    '    'div_Slot3.Attributes("style") = "margin-top:25px;"
                '    '    'div_Slot4.Attributes("style") = "margin-top:25px;"
                '    '    'dv_pro_group2.Attributes("style") = "margin-top:180px;"
                '    '    'dv_pro_group3.Attributes("style") = "margin-top:180px;"
                '    'Else
                '    '    div_Slot3.Attributes("style") = "margin-top:85px;"
                '    '    div_Slot4.Attributes("style") = "margin-top:85px;"
                '    '    dv_pro_group2.Attributes("style") = "margin-top:180px;"
                '    '    dv_pro_group3.Attributes("style") = "margin-top:180px;"
                '    'End If

                'Else
                '    'div_AllDays.Attributes("style") = "margin-top:70px;"
                '    dv_pro_group2.Attributes("style") = "margin-top:170px;"
                '    dv_pro_group3.Attributes("style") = "margin-top:170px;"
                'End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RadVoucherType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Try
            If RadVoucherType.SelectedValue IsNot "" Then
                dv_VoucherCode.Visible = True
            Else
                dv_VoucherCode.Visible = False
                RadVoucherCode.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

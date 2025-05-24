<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Promotion.aspx.vb" Inherits="Promotion" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Promotion Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Promotion_List.aspx">Promotion List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Promotion Master</li>
        </ol>
    </div>

    <script>
        function openWindow(url) {
            var w = window.open(url, '', 'width=1000,height=700,top=80,left=300,toolbar=0,status=0,location=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }
    </script>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btn_PromotionProduct">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdcopyProduct" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Promotion Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>Promo name<span class="text-danger">&nbsp;*</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="Rad_Promoname" CssClass="form-control" Skin="" runat="server" placeholder="Promo name" Width="100%"></telerik:RadTextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Endless </label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chk_endless" runat="server" OnCheckedChanged="chk_endless_CheckedChanged" Checked="true" AutoPostBack="true" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Start Date <span class="text-danger">&nbsp;*</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtstartdate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="100%">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtstartdate" ValidationGroup="valid" Display="None"
                                            ErrorMessage="Start Date is required"></asp:RequiredFieldValidator>--%>
                                        &nbsp;<%--<telerik:RadMaskedTextBox ID="txtstarttime" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>--%>
                                    </div>

                                    <div class="col-md-6" id="div_EndDate" runat="server" visible="false">
                                        <label>End Date <span class="text-danger">&nbsp;*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtenddate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="100%">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                        &nbsp;<%--<telerik:RadMaskedTextBox ID="txtendtime" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>--%>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <asp:RadioButtonList ID="rbPromotiontype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rbPromotiontype_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text=" &nbsp;&nbspDaily&nbsp;&nbsp" Value="0" Selected></asp:ListItem>
                                            <%--<asp:ListItem Text="&nbsp;&nbspWeekly&nbsp;&nbsp;" Value="1"></asp:ListItem>--%>
                                            <asp:ListItem Text=" &nbsp;&nbspMonthly&nbsp;&nbsp" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-6" id="dv_OnDays" runat="server" visible="false">
                                        <label>On Days</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="Rad_OnDays" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                            Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" OnSelectedIndexChanged="Rad_OnDays_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Sun" Value="Sun" />
                                                <telerik:RadComboBoxItem Text="Mon" Value="Mon" />
                                                <telerik:RadComboBoxItem Text="Tue" Value="Tue" />
                                                <telerik:RadComboBoxItem Text="Wed" Value="Wed" />
                                                <telerik:RadComboBoxItem Text="Thu" Value="Thu" />
                                                <telerik:RadComboBoxItem Text="Fri" Value="Fri" />
                                                <telerik:RadComboBoxItem Text="Sat" Value="Sat" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6" id="div_Combo" runat="server" visible="false">
                                        <label>No of products </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="Rad_comboselect" runat="server" Width="100%" OnSelectedIndexChanged="Rad_comboselect_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="0" />
                                                <telerik:RadComboBoxItem Text="2" Value="2" />
                                                <telerik:RadComboBoxItem Text="3" Value="3" />
                                                <telerik:RadComboBoxItem Text="4" Value="4" />

                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6" id="dv_Month" runat="server" visible="false">
                                        <label>Months </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="Rad_Months" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                            Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Jan" Value="Jan" />
                                                <telerik:RadComboBoxItem Text="Feb" Value="Feb" />
                                                <telerik:RadComboBoxItem Text="Mar" Value="Mar" />
                                                <telerik:RadComboBoxItem Text="Apr" Value="Apr" />
                                                <telerik:RadComboBoxItem Text="May" Value="May" />
                                                <telerik:RadComboBoxItem Text="Jun" Value="Jun" />
                                                <telerik:RadComboBoxItem Text="Jul" Value="Jul" />
                                                <telerik:RadComboBoxItem Text="Aug" Value="Aug" />
                                                <telerik:RadComboBoxItem Text="Sept" Value="Sept" />
                                                <telerik:RadComboBoxItem Text="Oct" Value="Oct" />
                                                <telerik:RadComboBoxItem Text="Nov" Value="Nov" />
                                                <telerik:RadComboBoxItem Text="Dec" Value="Dec" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-6" id="dv_Week" runat="server" visible="false">
                                        <label>Week </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="Rad_Week" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                            Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" OnSelectedIndexChanged="Rad_Week_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="1" Value="01" />
                                                <telerik:RadComboBoxItem Text="2" Value="02" />
                                                <telerik:RadComboBoxItem Text="3" Value="03" />
                                                <telerik:RadComboBoxItem Text="4" Value="04" />
                                                <telerik:RadComboBoxItem Text="5" Value="05" />
                                                <telerik:RadComboBoxItem Text="6" Value="06" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <div class="clearfix"></div>
                                        <br />

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />




                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6" style="display: none;">
                                        <label>Voucher Type </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="RadVoucherType" runat="server" Width="100%" OnSelectedIndexChanged="RadVoucherType_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="" />
                                                <telerik:RadComboBoxItem Text="Voucher Promos" Value="Voucher Promos" />
                                                <telerik:RadComboBoxItem Text="gift cards and deposits" Value="gift cards and deposits" />
                                                <telerik:RadComboBoxItem Text="Discount certificates" Value="Discount certificates" />
                                                <telerik:RadComboBoxItem Text="product vouchers" Value="product vouchers" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="col-md-6" id="dv_VoucherCode" runat="server" visible="false" style="display: none;">
                                        <label>Voucher Code <span class="text-danger">&nbsp;*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="RadVoucherCode" CssClass="form-control" Skin="" runat="server" placeholder="Voucher Code" Width="100%"></telerik:RadTextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Discount Type <span class="text-danger">&nbsp;*</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="Rad_DiscountType" runat="server" Width="100%" OnSelectedIndexChanged="Rad_DiscountType_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="SELECT" />
                                                <telerik:RadComboBoxItem Text="Discount %" Value="1" />
                                                <telerik:RadComboBoxItem Text="Discount Amt" Value="2" />
                                                <telerik:RadComboBoxItem Text="Price" Value="3" />
                                                <telerik:RadComboBoxItem Text="Combo" Value="4" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <%--<asp:RequiredFieldValidator ID="rflocation" runat="server" ControlToValidate="Rad_DiscountType"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Discount Type is required" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="col-md-6" id="dv_Discount" runat="server" visible="false">
                                        <span class="text-danger">&nbsp;*</span><label>Discount </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="Rad_Discount" CssClass="form-control" Skin="" runat="server" placeholder="discount" Width="100%"></telerik:RadTextBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Venue</label>
                                        <telerik:RadComboBox ID="RadVenue" runat="server" Width="100%" OnSelectedIndexChanged="RadVenue_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Location</label>
                                        <telerik:RadComboBox ID="RadLocation" runat="server" Width="100%" OnSelectedIndexChanged="RadLocation_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="col-md-12">
                                        <label>Till</label>
                                        <telerik:RadComboBox ID="RadTill" runat="server" Width="100%">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Online Coupon</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="Rad_onlinecoupon" CssClass="form-control" Skin="" runat="server" placeholder="Online Coupon" Width="100%"></telerik:RadTextBox>
                                    </div>

                                     <div class="col-md-6">
                                        <div class="clearfix"></div>
                                        24Hrs &nbsp;
                                        <asp:CheckBox ID="chk_AllDays" runat="server" Checked="true" OnCheckedChanged="chk_AllDays_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                   
                                    <div class="clearfix"></div>
                                    <br />
                                    <div id="divcombooption" runat="server" visible="false">
                                    <div class="col-md-6">
                                        Mix n Match (Only for Combos)&nbsp;&nbsp;<asp:CheckBox ID="chkMixnMatch" runat="server" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        Consider Discount as Combo Price&nbsp;&nbsp;<asp:CheckBox ID="chkisFlatAmount" runat="server" />
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6" id="div3" runat="server">
                                        <div class="clearfix"></div>
                                        Buy 1 Get 1 Free&nbsp;&nbsp;<asp:CheckBox ID="ChkBuy1Get1" runat="server" />

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                        </div>
                                    <div id="divfeeproduct" runat="server">
                                    <div class="col-md-6" id="div4" runat="server">
                                        <div class="clearfix"></div>
                                        Free Product&nbsp;&nbsp;<asp:CheckBox ID="chkFreeProduct" runat="server" />

                                    </div>

                               
                                    <div class="col-md-6" id="div5" runat="server">
                                        <div class="clearfix"></div>
                                        Discount Only Applies to Orders Over&nbsp;&nbsp;<asp:TextBox CssClass="form-control" ID="txtFPAmount" runat="server" Text="0"></asp:TextBox>

                                    </div>
                                        
                                    <div class="clearfix"></div>
                                    <br />
                                     <div class="col-md-6" id="div2" runat="server">
                                        <div class="clearfix"></div>
                                        On Each Spend&nbsp;&nbsp;<asp:CheckBox ID="chkOnEachSpend" runat="server" />
                                         <asp:CheckBox ID="chkAllProd" runat="server" Checked="false" Visible="false"/>

                                    </div>
                                        
                                         <div class="col-md-6" id="div6" runat="server">
                                        <div class="clearfix"></div>
                                        Qty&nbsp;&nbsp;<asp:TextBox CssClass="form-control" ID="txtQtyofProduct" runat="server" Text="0"></asp:TextBox>

                                    </div>
                                        <div class="col-md-6" id="div7" runat="server">
                                        <div class="clearfix"></div>
                                        Is BarStockExchange&nbsp;&nbsp;<asp:CheckBox ID="isbarStock" runat="server" />
                                         <asp:CheckBox ID="CheckBox2" runat="server" Checked="false" Visible="false"/>

                                    </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />


                                    <div class="col-md-6" id="div_Combo2" runat="server" visible="false">
                                        &nbsp;
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />


                                    <div class="col-md-6" id="dv_Days" runat="server" visible="false">
                                        <label>Days </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="rad_Days" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                            Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="1" Value="01" />
                                                <telerik:RadComboBoxItem Text="2" Value="02" />
                                                <telerik:RadComboBoxItem Text="3" Value="03" />
                                                <telerik:RadComboBoxItem Text="4" Value="04" />
                                                <telerik:RadComboBoxItem Text="5" Value="05" />
                                                <telerik:RadComboBoxItem Text="6" Value="06" />
                                                <telerik:RadComboBoxItem Text="7" Value="07" />
                                                <telerik:RadComboBoxItem Text="8" Value="08" />
                                                <telerik:RadComboBoxItem Text="9" Value="09" />
                                                <telerik:RadComboBoxItem Text="10" Value="10" />
                                                <telerik:RadComboBoxItem Text="11" Value="11" />
                                                <telerik:RadComboBoxItem Text="12" Value="12" />
                                                <telerik:RadComboBoxItem Text="13" Value="13" />
                                                <telerik:RadComboBoxItem Text="14" Value="14" />
                                                <telerik:RadComboBoxItem Text="15" Value="15" />
                                                <telerik:RadComboBoxItem Text="16" Value="16" />
                                                <telerik:RadComboBoxItem Text="17" Value="17" />
                                                <telerik:RadComboBoxItem Text="18" Value="18" />
                                                <telerik:RadComboBoxItem Text="19" Value="19" />
                                                <telerik:RadComboBoxItem Text="20" Value="20" />
                                                <telerik:RadComboBoxItem Text="21" Value="21" />
                                                <telerik:RadComboBoxItem Text="22" Value="22" />
                                                <telerik:RadComboBoxItem Text="23" Value="23" />
                                                <telerik:RadComboBoxItem Text="24" Value="24" />
                                                <telerik:RadComboBoxItem Text="25" Value="25" />
                                                <telerik:RadComboBoxItem Text="26" Value="26" />
                                                <telerik:RadComboBoxItem Text="27" Value="27" />
                                                <telerik:RadComboBoxItem Text="28" Value="28" />
                                                <telerik:RadComboBoxItem Text="29" Value="29" />
                                                <telerik:RadComboBoxItem Text="30" Value="30" />
                                                <telerik:RadComboBoxItem Text="31" Value="31" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>


                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3" id="div_Slot1" runat="server" visible="false">
                                    <div class="clearfix"></div>
                                    Slot 1
                                </div>

                                <div class="col-md-3" id="div_Slot2" runat="server" visible="false">
                                    <div class="clearfix"></div>
                                    Slot 2 &nbsp;
                                        <asp:CheckBox ID="chk_solt1" runat="server" OnCheckedChanged="chk_solt1_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-md-3" id="div_Slot3" runat="server" visible="false">
                                    <div class="clearfix"></div>
                                    Slot 3 &nbsp;
                                        <asp:CheckBox ID="chk_solt2" runat="server" OnCheckedChanged="chk_solt2_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-md-3" id="div_Slot4" runat="server" visible="false">
                                    <div class="clearfix"></div>
                                    Slot 4 &nbsp;
                                        <asp:CheckBox ID="chk_solt3" runat="server" OnCheckedChanged="chk_solt3_CheckedChanged" AutoPostBack="true" />
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-3" id="div_time1" runat="server" visible="false">
                                    <label>Start Time1 </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtstarttime" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-3" id="div_sttime1" runat="server" visible="false">
                                    <label>Start Time 2</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtstarttime1" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-3" id="div_time2" runat="server" visible="false">
                                    <label>Start Time 3 </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtstarttime2" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-3" id="div_time3" runat="server" visible="false">
                                    <label>Start Time 4</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtstarttime3" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-3" id="div_etime1" runat="server" visible="false">
                                    <label>End Time 1</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtendtime" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-3" id="div_endtime1" runat="server" visible="false">
                                    <label>End Time 2</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtendtime1" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-3" id="div_entime2" runat="server" visible="false">
                                    <label>End Time 3</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtendtime2" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-3" id="div_entime3" runat="server" visible="false">
                                    <label>End Time 4</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadMaskedTextBox ID="txtendtime3" runat="server" Mask="<00..23>:<00..59>" Width="30%" Skin="" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                <asp:HiddenField ID="hdmnm" runat="server" />




                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3" id="dv_pro_group" visible="false" runat="server">

                                    <label>Product Group <span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="RadPro_group" runat="server" Width="100%" OnSelectedIndexChanged="RadPro_group_SelectedIndexChanged1" AutoPostBack="true">
                                    </telerik:RadComboBox>

                                </div>

                                <div class="col-md-3" id="dv_pro_group1" visible="false" runat="server">
                                    <label>Product Group 1 <span class="text-danger">&nbsp;*</span> </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="RadPro_group1" runat="server" Width="100%" OnSelectedIndexChanged="RadPro_group1_SelectedIndexChanged1" AutoPostBack="true">
                                    </telerik:RadComboBox>

                                </div>
                                <div class="col-md-3" id="dv_pro_group2" runat="server" visible="false">
                                    <label>Product Group 2 <span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="RadPro_group2" runat="server" Width="100%" OnSelectedIndexChanged="RadPro_group2_SelectedIndexChanged1" AutoPostBack="true">
                                    </telerik:RadComboBox>

                                </div>
                                <div class="col-md-3" id="dv_pro_group3" runat="server" visible="false">
                                    <label>Product Group 3 <span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="RadPro_group3" runat="server" Width="100%" OnSelectedIndexChanged="RadPro_group3_SelectedIndexChanged1" AutoPostBack="true">
                                    </telerik:RadComboBox>

                                </div>

                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-3" id="dv_pro_name" visible="false" runat="server">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <label>Product Name</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Rad_Product" runat="server" Width="100%" OnSelectedIndexChanged="Rad_Product_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-md-3" id="dv_pro_name1" visible="false" runat="server">
                                    <label>Product Name 1 </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Rad_Product1" runat="server" Width="100%" OnSelectedIndexChanged="Rad_Product1_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-3" id="dv_pro_name2" visible="false" runat="server">
                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                    <label>Product Name 2</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Rad_Product2" runat="server" Width="100%" OnSelectedIndexChanged="Rad_Product2_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-md-3" id="dv_pro_name3" visible="false" runat="server">
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                    <label>Product Name 3</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Rad_Product3" runat="server" Width="100%" OnSelectedIndexChanged="Rad_Product3_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </div>


                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-3" id="dv_size" visible="false" runat="server">
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <label>Size</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="rad_size" runat="server" Width="100%">
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-md-3" id="dv_size1" visible="false" runat="server">
                                    <label>Size 1 </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Rad_size1" runat="server" Width="100%">
                                    </telerik:RadComboBox>
                                </div>


                                <div class="col-md-3" id="dv_size2" visible="false" runat="server">
                                    <asp:HiddenField ID="HiddenField6" runat="server" />
                                    <label>Size 2</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Rad_size2" runat="server" Width="100%">
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-md-3" id="dv_size3" visible="false" runat="server">
                                    <asp:HiddenField ID="HiddenField8" runat="server" />
                                    <label>Size 3</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Rad_size3" runat="server" Width="100%">
                                    </telerik:RadComboBox>
                                </div>


                                <div class="clearfix"></div>
                                <br />


                            </div>
                            <div id="dv_BtnAddProduct" runat="server" visible="false">
                                <label class="lbl">&nbsp;</label>
                                <div class="clearfix"></div>
                                <asp:Button ID="btnAddNewProduct" class="btn btn-primary" runat="server" ValidationGroup="validSize" Text="Add new product" OnClick="btnAddNewProduct_Click" ToolTip="Click here to Add Product" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <%--<div class="row" id="divPGroup" runat="server" visible="false">--%>
                            <div class="col-lg-11 " style="overflow-y: auto; height: 250px;">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="upWall" runat="server" UpdateMode="conditional">
                                        <ContentTemplate>
                                            <div class="row" id="divPGroup" runat="server" visible="false">
                                                <div class="col-md-6">
                                                    Discount : 
                                                </div>

                                                <telerik:RadGrid ID="rdProduct" AutoGenerateColumns="False" AllowPaging="False"
                                                    AllowSorting="false" runat="server" CellSpacing="0"
                                                    GridLines="None" AllowMultiRowSelection="true" AllowFilteringByColumn="false"
                                                    Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true">
                                                    <ClientSettings Selecting-AllowRowSelect="true">
                                                        <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="600px"/>--%>
                                                    </ClientSettings>
                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                    <SortingSettings EnableSkinSortStyles="false" />

                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="product_id" EnableHeaderContextMenu="true"
                                                        TableLayout="Fixed">

                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="ProductGroup" UniqueName="Product_Group">
                                                                <HeaderStyle Width="20%" />
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdPromotionid" runat="server" Value='<%#Eval("table_promotion_id")%>' />
                                                                    <asp:HiddenField ID="hdproductgroup" runat="server" Value='<%#Eval("group_id")%>' />
                                                                    <asp:Label ID="lbproductgroup" runat="server" Text='<%#Eval("Product_group")%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Product" UniqueName="Product_id">
                                                                <HeaderStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdproduct" runat="server" Value='<%#Eval("Product_Id")%>' />
                                                                    <asp:Label ID="lbproductname" runat="server" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Discount *" UniqueName="Discount">
                                                                <HeaderStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdprice" runat="server" Value='<%#Eval("Price")%>' />
                                                                    <telerik:RadTextBox ID="txtDiscount" CssClass="form-control" Skin="" runat="server" placeholder="Discount" Width="100%"
                                                                        MaxLength="15" Text='<%# Eval("Discount") %>' OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true">
                                                                    </telerik:RadTextBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn HeaderText="Size" UniqueName="Size">
                                                                <HeaderStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <%-- <asp:HiddenField ID="hdsize" runat="server" Value='<%#Eval("size_id")%>' />--%>
                                                                    <telerik:RadComboBox ID="ddlsize" runat="server" Width="100%"></telerik:RadComboBox>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <%-- </div>--%>
                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Promotion" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Promotion" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Promotion" />
                    </div>
                </div>

            </div>

        </telerik:RadAjaxPanel>

        <telerik:RadWindow runat="server" ID="rwEntryDetails" Modal="true" Width="800px"
            Height="650px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
            ReloadOnShow="true" Behaviors="Close" Title="" EnableEmbeddedSkins="false" Style="overflow: hidden !important;">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">Product List For Promotion</div>
                        <div class="panel-body pan">
                            <div class="form-body pal">

                                <br />
                                <div class="row" id="div1" runat="server">
                                    <div class="col-lg-12 " style="overflow-y: auto; height: 445px;">
                                        <div class="table-responsive">
                                            <asp:UpdatePanel ID="up_Pro_Ingredient" runat="server" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <telerik:RadGrid ID="rdcopyProduct" AutoGenerateColumns="False" AllowPaging="False"
                                                        AllowSorting="True" runat="server" CellSpacing="0"
                                                        GridLines="None" AllowMultiRowSelection="true" AllowFilteringByColumn="True"
                                                        Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true" GroupingEnabled="true" MasterTableView-GroupsDefaultExpanded="false" RetainExpandStateOnRebind="true">
                                                        <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="false" AllowDragToGroup="true" AllowGroupExpandCollapse="False">
                                                        </ClientSettings>
                                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                                        <SortingSettings EnableSkinSortStyles="false" />

                                                        <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="product_id" EnableHeaderContextMenu="true"
                                                            TableLayout="Fixed" HierarchyDefaultExpanded="False">
                                                            <GroupByExpressions>
                                                                <telerik:GridGroupByExpression>
                                                                    <SelectFields>
                                                                        <telerik:GridGroupByField FieldAlias="Group" FieldName="Cat_Name"></telerik:GridGroupByField>
                                                                    </SelectFields>
                                                                    <GroupByFields>
                                                                        <telerik:GridGroupByField FieldName="Cat_Name"></telerik:GridGroupByField>
                                                                    </GroupByFields>
                                                                </telerik:GridGroupByExpression>
                                                            </GroupByExpressions>
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                                                <%--  <telerik:GridBoundColumn DataField="Cat_Name" HeaderText="Product Group" SortExpression="Cat_Name"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>--%>

                                                                <telerik:GridBoundColumn DataField="pro_Name" HeaderText="Product Name" SortExpression="name"
                                                                    ReadOnly="True" FilterControlAltText="Filter product name column" ShowFilterIcon="false"
                                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                                    AutoPostBackOnFilter="true">
                                                                </telerik:GridBoundColumn>

                                                                <telerik:GridBoundColumn DataField="base_unit" HeaderText="Base Unit" SortExpression="base_unit"
                                                                    ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true"
                                                                    ShowSortIcon="false" ReadOnly="true">
                                                                </telerik:GridBoundColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-top: 10px; text-align: center;">
                                    <asp:Button ID="btn_PromotionProduct" class="btn btn-primary" runat="server" Text="Save" ToolTip="Click here Add Product For Promotion" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_PromotionProductCancel" class="btn btn-primary" runat="server" Text="Cancel" ToolTip="Click here Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>



</asp:Content>


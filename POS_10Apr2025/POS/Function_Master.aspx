<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Function_Master.aspx.vb" Inherits="Function_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Function Map 
     
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">

    <script type="text/javascript">
        function DeleteItem(str) {
            debugger;
            var r = confirm(str);
            if (r == true) {
                $("#ContentPlaceHolder1_hf_swapstatus").val("1")
                <%BindFunction_Swap()%>;

            }
            else {
                $("#ContentPlaceHolder1_hf_swapstatus").val("0")

            }
        }
    </script>
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Function_List.aspx">Function Map List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Function Map Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Function Map Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label>Swap Till</label>
                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="RadSwapMachine" runat="server" Width="100%" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                        <asp:TextBox ID="hf_swapstatus" runat="server" Style="display: none;" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtMainFun" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMainFun"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Function name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Till <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="RadMachine" runat="server" Width="100%">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadMachine" ErrorMessage="Till is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-12">
                                        <label>
                                            Active &nbsp;
                                            <asp:CheckBox ID="ChkmainActive" runat="server" Checked="true" /></label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label>Edit Panel </label>
                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="Radeditpanel" runat="server" Width="100%" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid1"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtFName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFName"
                                            ValidationGroup="valid1" Display="None" ErrorMessage=" name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-12" id="divParentFunction" runat="server">
                                        <label>Parent Function </label>
                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="ddParent" runat="server" Width="100%" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />


                                    <div class="col-md-12">
                                        <label>Function Type <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="ddCaption" runat="server" Width="100%" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddCaption" ErrorMessage="Function type is required"
                                            ValidationGroup="valid1" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-12" id="divPayType" runat="server" visible="false">
                                        <label>Card Payment Type<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="ddlPayType" runat="server" Width="100%">
                                        </telerik:RadComboBox>

                                    </div>

                                    <div class="clearfix"></div>

                                    <div class="col-md-12" id="divZReport" runat="server" visible="false">
                                        <br />

                                        <label>Venue</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radVenue" runat="server" Width="100%" AutoPostBack="true"
                                            OnSelectedIndexChanged="radVenue_SelectedIndexChanged" EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                        </telerik:RadComboBox>
                                        <br />
                                        <br />
                                        <div class="clearfix"></div>
                                        <label>Location</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radLocation" runat="server" Width="100%" AutoPostBack="true"
                                            OnSelectedIndexChanged="radLocation_SelectedIndexChanged" EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                        </telerik:RadComboBox>
                                        <br />
                                        <br />
                                        <div class="clearfix"></div>
                                        <label>Till</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radTill" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                            Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="clearfix"></div>

                                    <div class="col-md-12" id="divPriceLevel" runat="server" visible="false">
                                        <br />
                                        <label>Price Level</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="rdPriceLevel" runat="server" Width="100%"
                                            EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                        </telerik:RadComboBox>
                                    </div>


                                    <div class="col-md-12" id="divAccountCreate" runat="server" visible="false">
                                        <br />
                                        <label>Profile Type</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="rdProfileType" runat="server" Width="100%"
                                            EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                        </telerik:RadComboBox>

                                        <br />
                                        <div class="clearfix"></div>
                                        <div class="col-md-12">
                                            <br />
                                            <label>Default Expiry Time</label>
                                            <br />
                                            <div class="col-md-6">
                                                <telerik:RadTextBox ID="txtDays" Skin="" CssClass="form-control"
                                                    runat="server" Width="100%" placeholder="Days" MaxLength="3">
                                                </telerik:RadTextBox>
                                            </div>
                                            <div class="col-md-6">

                                                <telerik:RadComboBox ID="rdExpTime" runat="server" Width="100%"
                                                    EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="DD" Value="DD" />
                                                        <telerik:RadComboBoxItem Text="MM" Value="MM" />
                                                        <telerik:RadComboBoxItem Text="YY" Value="YY" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="col-md-12" id="divSurchargeAmount" runat="server" visible="false">
                                        <br />
                                        <label>Surcharge Percentage&nbsp;(%)</label>

                                        <div class="clearfix"></div>

                                        <asp:TextBox ID="txtItem1" runat="server" Width="100%" CssClass="form-control" placeholder="Surcharge"></asp:TextBox>

                                    </div>
                                    <div class="col-md-12" id="divEOHO" runat="server" visible="false">
                                        <br />
                                        <label>Percentage&nbsp;(%)</label>

                                        <div class="clearfix"></div>

                                        <asp:TextBox ID="itemEOHO" runat="server" Width="100%" CssClass="form-control" placeholder="Percentage" Text="50"></asp:TextBox>
                                        <br />
                                        <div class="clearfix"></div>
                                        <label>Max Amount Per Person&nbsp;(%)</label>

                                        <div class="clearfix"></div>

                                        <asp:TextBox ID="txtEOHOAmount" runat="server" Width="100%" CssClass="form-control" Text="10"></asp:TextBox>
                                        <br />
                                        <div class="clearfix"></div>
                                        <label>Max Amount Per Person&nbsp;(%)</label>

                                        <div class="clearfix"></div>

                                        <telerik:RadComboBox ID="ddltax" runat="server" Width="100%"
                                            EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="col-md-12" id="divItem" runat="server" visible="false">
                                        <br />
                                        <label>Discount&nbsp;(%)</label>

                                        <div class="clearfix"></div>

                                        <asp:TextBox ID="txtItem" runat="server" Width="100%" CssClass="form-control" placeholder="Discount"></asp:TextBox>

                                    </div>
                                    <div class="col-md-12" id="divLauncher" runat="server" visible="false">
                                        <br />
                                        <asp:Label ID="lbllauncher" runat="server" Text="Launcher"></asp:Label>

                                        <div class="clearfix"></div>

                                        <asp:TextBox ID="txtLauncher" runat="server" Width="100%" CssClass="form-control" placeholder="Launcher"></asp:TextBox>
                                        <br />
                                        <asp:Label ID="lblAccountID" runat="server" Text="Launcher" Visible="false"></asp:Label>

                                        <asp:TextBox ID="txtAccountID" Visible="false" runat="server" Width="100%" CssClass="form-control" placeholder="AccountID"></asp:TextBox>

                                    </div>
                                    <div class="col-md-12" id="divAmount" runat="server" visible="false">
                                        <br />
                                        <label>Amount</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox ID="txtAmount" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Amount" MaxLength="8">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="col-md-12" id="divPayment" runat="server" visible="false">
                                        <br />
                                        <label>Payment Type</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddPayment" runat="server" Width="100%">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-12" id="divCardSale" runat="server" visible="false">
                                        <br />
                                        <label>Payment Type</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddlCardType" runat="server" Width="100%" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Credit Card" Value="0" Selected="true" />
                                                <telerik:RadComboBoxItem Text="Integrated Payment" Value="1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <div runat="server" id="divCardSub" visible="false">
                                            <div class="clearfix"></div>
                                            <br />
                                            <label>Payment Subtype</label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlCardSubType" runat="server" Width="100%" AutoPostBack="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="EVO" Value="EVO" Selected="true" />
                                                    <telerik:RadComboBoxItem Text="PAY WORKS" Value="PAY WORKS" />
                                                    <telerik:RadComboBoxItem Text="Kinetic" Value="Kinetic" />
                                                    <telerik:RadComboBoxItem Text="Kinetic Saturn" Value="Kinetic Saturn" />
                                                    <telerik:RadComboBoxItem Text="Room Payment" Value="Room Payment" />
                                                    <telerik:RadComboBoxItem Text="Add pay" Value="Add pay" />
                                                    <telerik:RadComboBoxItem Text="Elina" Value="Elina" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <div class="clearfix"></div>
                                            <div id="divRoomDetails" runat="server" visible="false">
                                                <br />
                                                <label>URL (Platform Address/URL) </label>
                                                <asp:TextBox ID="txtRoomPlatform" runat="server" Width="100%" CssClass="form-control" placeholder="URL"></asp:TextBox>
                                                <br />
                                                <label>UserName (Client Token/UserName) /</label>
                                                <asp:TextBox ID="txtRoomCToken" runat="server" Width="100%" CssClass="form-control" placeholder="Client Token"></asp:TextBox>
                                                <br />
                                                <label>Password (Access Token/Authorization)</label>
                                                <asp:TextBox ID="txtRoomAToken" runat="server" Width="100%" CssClass="form-control" placeholder="Access Token"></asp:TextBox>
                                                <br />
                                                <label>Service Id</label>
                                                <asp:TextBox ID="txtserviceid" runat="server" Width="100%" CssClass="form-control" placeholder="Service Id"></asp:TextBox>


                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" id="divTotalValue" runat="server" visible="false">
                                        <br />
                                        <label>Total Value</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox ID="txt_TotalValue" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Total Value" MaxLength="8">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="col-md-12" id="divSalesHandlingFee" runat="server" visible="false">
                                        <br />
                                        <label>Sales Handling Product Id</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox ID="txt_SalesHandlingFee" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Sales Handling Product Id" MaxLength="8">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="col-md-12" id="divPaymentHandlingFee" runat="server" visible="false">
                                        <br />
                                        <label>Payment Handling Product Id</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox ID="txt_PaymentHandlingFee" Skin="" CssClass="form-control" Value="0"
                                            runat="server" Width="100%" placeholder="Payment Handling Product Id" MaxLength="8">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="col-md-12" id="divTaxAmount" runat="server" visible="false">
                                        <br />
                                        <div style="display: none;">
                                            <label>Tax Amount</label>
                                            <div class="clearfix"></div>
                                            <telerik:RadNumericTextBox ID="txt_TaxAmount" Skin="" CssClass="form-control" Value="0"
                                                runat="server" Width="100%" placeholder="Tax Amount" MaxLength="8">
                                                <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>


                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12" style="display: none;">
                                        <label>Sorting No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtSortinigNo" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Sorting No" MaxLength="4" Enabled="false">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <div class="col-md-3">

                                            <label>Background Color </label>
                                            <telerik:RadColorPicker ID="radBackcolorpicker" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-3">
                                            <label>For Color </label>
                                            <telerik:RadColorPicker ID="radForcolorpicker" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-3">
                                            <label>&nbsp;</label>
                                            <div class="clearfix"></div>
                                            <label>
                                                Active &nbsp;
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                        </div>
                                        <div class="col-md-3">
                                            <label>&nbsp;</label>
                                            <div class="clearfix"></div>
                                            <label>
                                                Big Button &nbsp;
                                            <asp:CheckBox ID="chkbigbutton" runat="server" Checked="false" /></label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">

                                        <div class="col-md-6">

                                            <label runat="server" visible="false" id="lblgroupbydept">
                                                Group By Department &nbsp;
                                            <asp:CheckBox ID="chkgroupbydept" runat="server" Checked="false" AutoPostBack="true" Visible="false" /></label>
                                        </div>
                                        <div class="col-md-6">

                                            <label runat="server" visible="false" id="lblgroupbycourse">
                                                Group By Course &nbsp;
                                            <asp:CheckBox ID="chkgroupbycourse" runat="server" Checked="false" AutoPostBack="true" Visible="false" /></label>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-12" id="divgroupbydept" runat="server" visible="true">

                                        <div class="col-md-6">
                                            <label runat="server" visible="false" id="lbldept">Department </label>

                                            <telerik:RadComboBox ID="ddldeaprtmentid" runat="server" Width="100%" AutoPostBack="true" EnableCheckAllItemsCheckBox="True" CheckBoxes="True"
                                                Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Visible="false">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="col-md-6">

                                            <label runat="server" visible="false" id="lblcourse">Course </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlcourseid" runat="server" Width="100%" EnableCheckAllItemsCheckBox="True" CheckBoxes="True"
                                                Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" AutoPostBack="true" Visible="false">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <asp:Button ID="btn_save" class="btn btn-primary" runat="server" ValidationGroup="valid1" Text="Save" Style="margin-left: 220px;" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_clear" class="btn btn-primary" runat="server" Text="Clear" Visible="true" />
                                    </div>

                                    <div class="clearfix"></div>

                                </div>
                            </div>

                            <div class="clearfix"></div>

                            <div id="div_7by7" runat="server" style="overflow-x: scroll">
                                <table style="border: 1px solid black; padding: 5%; width: 100%;">
                                    <tr style="margin-left: 3%; margin-right: 3%;">
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_1" class="btn btn-primary" runat="server" Text="F1" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_2" class="btn btn-primary" runat="server" Text="F2" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 3%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_3" class="btn btn-primary" runat="server" Text="F3" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_4" class="btn btn-primary" runat="server" Text="F4" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_5" class="btn btn-primary" runat="server" Text="F5" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_6" class="btn btn-primary" runat="server" Text="F6" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_7" class="btn btn-primary" runat="server" Text="F7" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_8" class="btn btn-primary" runat="server" Text="F8" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_9" class="btn btn-primary" runat="server" Text="F9" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_10" class="btn btn-primary" runat="server" Text="F10" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_11" class="btn btn-primary" runat="server" Text="F11" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_12" class="btn btn-primary" runat="server" Text="F12" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_13" class="btn btn-primary" runat="server" Text="F13" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_14" class="btn btn-primary" runat="server" Text="F14" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                    </tr>
                                    <tr style="margin-left: 3%; margin-right: 3%; margin-bottom: auto">
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_15" class="btn btn-primary" runat="server" Text="F15" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_16" class="btn btn-primary" runat="server" Text="F16" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;  
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_17" class="btn btn-primary" runat="server" Text="F17" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;  
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_18" class="btn btn-primary" runat="server" Text="F18" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;   
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_19" class="btn btn-primary" runat="server" Text="F19" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;    
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_20" class="btn btn-primary" runat="server" Text="F20" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_21" class="btn btn-primary" runat="server" Text="F21" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_22" class="btn btn-primary" runat="server" Text="F22" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_23" class="btn btn-primary" runat="server" Text="F23" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_24" class="btn btn-primary" runat="server" Text="F24" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_25" class="btn btn-primary" runat="server" Text="F25" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_26" class="btn btn-primary" runat="server" Text="F26" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_27" class="btn btn-primary" runat="server" Text="F27" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_28" class="btn btn-primary" runat="server" Text="F28" Style="margin-top: 10px; margin-bottom: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                    </tr>
                                    <tr style="margin-left: 3%; margin-right: 3%; display: none">


                                        <th width="5%">
                                            <asp:Button ID="btn_7by7_29" class="btn btn-primary" runat="server" Text="F29" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                        &nbsp;
                                         <th width="5%">
                                             <asp:Button ID="btn_7by7_30" class="btn btn-primary" runat="server" Text="F30" Style="margin-top: 10px; width: 80%; font-size: 10px; height: 50px; margin-right: 1%; margin-left: 1%;" /></th>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Function Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Function Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Function Details" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


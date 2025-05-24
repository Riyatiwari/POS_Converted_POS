<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Key_Map_Master.aspx.vb" Inherits="Key_Map_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Key Map Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Key_Map_List.aspx">Key Map List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Key Map Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Key Map Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtKName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Key map name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Display Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtDisName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDisName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Display name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12" style="display: none;">
                                        <label>Description </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtdesc" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Description" Width="100%"></telerik:RadTextBox>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Sorting No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtSortingno" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Sorting No" MaxLength="4" Enabled="false">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>

                                    </div>
                                    <div class="clearfix"></div>

                                    <br />
                                    <div class="col-md-12">
                                        <label>Size <span class="text-danger">*</span> </label>
                                        <telerik:RadComboBox ID="ddlsize" runat="server" Width="100%" OnSelectedIndexChanged="ddlsize_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="SELECT" Text="SELECT" />
                                                <telerik:RadComboBoxItem Value="4*4" Text="4*4" />
                                                <telerik:RadComboBoxItem Value="4*6" Text="4*6" />
                                                <telerik:RadComboBoxItem Value="5*5" Text="5*5" />
                                                <telerik:RadComboBoxItem Value="6*6" Text="6*6" />
                                                <telerik:RadComboBoxItem Value="7*7" Text="7*7" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsize" ErrorMessage="Size is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>

                                    <br />
                                    <div class="col-md-12">
                                        <label>
                                            Active &nbsp;
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <label>Font Color </label>
                                            <telerik:RadColorPicker ID="radForcolorpicker_m" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-6">
                                            <label>Background Color </label>
                                            <telerik:RadColorPicker ID="radBackcolorpicker_m" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" OnColorChanged="radBackcolorpicker_m_ColorChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12" style="display: none;">
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="valid1"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Group Category</label><div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddlMainCategory" runat="server" Width="100%" OnSelectedIndexChanged="ddlMainCategory_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMainCategory" ErrorMessage="Group Category is required"
                                            ValidationGroup="valid1" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>--%>
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>

                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="valid1"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Product Group <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadComboBox ID="RadPro_group" runat="server" Width="100%" OnSelectedIndexChanged="RadPro_group_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rfvDeviceType" runat="server" ControlToValidate="RadPro_group" ErrorMessage="Product group is required"
                                            ValidationGroup="valid1" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Product </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="Radproduct" runat="server" Width="100%" OnSelectedIndexChanged="Radproduct_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Size </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="RadSize" runat="server" Width="100%">
                                        </telerik:RadComboBox>

                                    </div>
                                    <div class="clearfix"></div>

                                    <br />
                                    <div class="col-md-12">
                                        <label>Font Color </label>
                                        <telerik:RadColorPicker ID="radForcolorpicker" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />

                                    </div>
                                    <div class="clearfix"></div>

                                    <br />
                                    <div class="col-md-12">
                                        <label>Background Color</label>
                                        <telerik:RadColorPicker ID="radBackcolorpicker" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <asp:Button ID="btn_copy" class="btn btn-primary" runat="server" Text="Copy" Style="margin-left: 220px;" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_save" class="btn btn-primary" runat="server" ValidationGroup="valid1" Text="Save" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_clear" class="btn btn-primary" runat="server" Text="Clear" />
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        <label>Venue </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radVenue" runat="server" Width="100%" OnSelectedIndexChanged="radVenue_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="col-md-4">
                                        <label>Location </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radLocation" runat="server" Width="100%" OnSelectedIndexChanged="radLocation_SelectedIndexChanged" AutoPostBack="true">
                                        </telerik:RadComboBox>

                                    </div>

                                    <div class="col-md-4">
                                        <label>Till </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radMachine" runat="server" Width="100%">
                                        </telerik:RadComboBox>

                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        <label>Button Style </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddlBtnStyle" runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="Flate" Text="Flat" />
                                                <telerik:RadComboBoxItem Value="Shadow" Text="Shadow" />
                                                <telerik:RadComboBoxItem Value="Full Shadow" Text="Full Shadow" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="col-md-4">
                                        <label>Image Type </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddlImgType" runat="server" Width="100%" OnSelectedIndexChanged="ddlImgType_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="None" Text="None" />
                                                <telerik:RadComboBoxItem Value="Round Image" Text="Round Image" />
                                                <telerik:RadComboBoxItem Value="Square Image" Text="Square Image" />
                                            </Items>
                                        </telerik:RadComboBox>

                                    </div>

                                    <div class="col-md-4" id="div_imgOption" runat="server" visible="false">
                                        <label>Image Option </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddl_ImageOption" runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="Left" Text="Left" />
                                                <telerik:RadComboBoxItem Value="Right" Text="Right" />
                                                <telerik:RadComboBoxItem Value="Top" Text="Top" />
                                                <telerik:RadComboBoxItem Value="Bottom" Text="Bottom" />
                                                <telerik:RadComboBoxItem Value="Background" Text="Background" />
                                            </Items>
                                        </telerik:RadComboBox>

                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />

                            <div id="div1" runat="server" visible="false">
                                <asp:DataList ID="rpt_button" runat="server" RepeatColumns="4" Width="100%" Style="margin-top: 10px;">
                                    <ItemTemplate>
                                        <asp:Button runat="server" class="btn btn-primary" Style="margin-top: 20px; margin-left: 10px; width: 90%; font-size: 10px; font-size: 10px;" />
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>

                            <div id="div2" runat="server" visible="false">
                                <asp:DataList ID="rpt_Button_5by5" runat="server" RepeatColumns="5" Width="100%" Style="margin-top: 10px;">
                                    <ItemTemplate>
                                        <asp:Button runat="server" class="btn btn-primary" Style="margin-top: 20px; margin-left: 10px; width: 90%; font-size: 10px; font-size: 10px;" />
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>

                            <div id="div3" runat="server" visible="false">
                                <asp:DataList ID="rpt_Button_6by6" runat="server" RepeatColumns="6" Width="100%" Style="margin-top: 10px;">
                                    <ItemTemplate>
                                        <asp:Button runat="server" class="btn btn-primary" Style="margin-top: 20px; margin-left: 10px; width: 90%; font-size: 10px; font-size: 10px;" />
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>

                            <div id="div4" runat="server" visible="false">
                                <asp:DataList ID="rpt_Button_7by7" runat="server" RepeatColumns="7" Width="100%" Style="margin-top: 10px;">
                                    <ItemTemplate>
                                        <asp:Button runat="server" class="btn btn-primary" Style="margin-top: 20px; margin-left: 10px; width: 90%; font-size: 10px; font-size: 10px;" />
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>

                            <div id="div_4by4" runat="server" visible="false">

                                <table style="border: 1px solid black;" width="100%">
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by4_1" class="btn btn-primary" runat="server" Text=""
                                                Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by4_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by4_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by4_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                    </tr>
                                </table>
                            </div>

                            <div id="div_4by6" runat="server" visible="false">
                                <table style="border: 1px solid black;" width="100%">
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by6_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by6_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by6_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by6_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by6_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_4by6_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                    </tr>
                                </table>
                            </div>

                            <div id="div_5by5" runat="server" visible="false">
                                <table style="border: 1px solid black;" width="100%">
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_5by5_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_5by5_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_5by5_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_5by5_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_5by5_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_25" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                    </tr>
                                </table>
                            </div>

                            <div id="div_6by6" runat="server" visible="false">
                                <table style="border: 1px solid black;" width="100%">
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_6by6_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_6by6_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_6by6_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_6by6_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_6by6_25" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_26" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_27" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_28" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_29" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_30" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_6by6_31" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_32" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_33" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_34" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_35" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_36" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                    </tr>
                                </table>
                            </div>

                            <div id="div_7by7" runat="server" visible="false">
                                <table style="border: 1px solid black;" width="100%">
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_7by7_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_7by7_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_7by7_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_7by7_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_25" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_26" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_27" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_28" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_7by7_29" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_30" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_31" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_32" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_33" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_34" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_35" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_7by7_36" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_37" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_38" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_39" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_40" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_41" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_42" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                    </tr>
                                    <tr>
                                        <th width="350px;">
                                            <asp:Button ID="btn_7by7_43" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_44" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_45" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_46" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_47" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_48" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_49" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Key Map Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Key Map Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Key Map Details" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


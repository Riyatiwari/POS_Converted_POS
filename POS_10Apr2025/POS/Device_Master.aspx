<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Device_Master.aspx.vb" Inherits="Device_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Device Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Device_List.aspx">Device List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Device Master</li>
        </ol>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <input type="text" style="display: none" />
    <input type="password" style="display: none" />
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Device Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtDName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Device name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <%-- <div class="clearfix"></div>
                                    <br />--%>
                                    <div class="col-md-6">
                                        <label>Code </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtCode" CssClass="form-control" Skin="" runat="server" placeholder="Code" Width="100%"></telerik:RadTextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Serial No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtSerialNo" CssClass="form-control" Skin="" runat="server" placeholder="Serial No" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSerialNo"
                                            ErrorMessage="Enter only numeric and characters in serial no" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Till<span class="text-danger">&nbsp;*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddMachine" runat="server" Width="100%">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rfvMachine" runat="server" ControlToValidate="ddMachine" ErrorMessage="Till is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-12">
                                        <label>Device Category<span class="text-danger">&nbsp;*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddlCategory" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlCategory" ErrorMessage="Device Category is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-12">
                                        <label>Device Type<span class="text-danger">&nbsp;*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddDeviceType" runat="server" Width="100%" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rfvDeviceType" runat="server" ControlToValidate="ddDeviceType" ErrorMessage="Device type is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-md-12" id="Device_SubType" runat="server" visible="false">
                                        <div class="clearfix"></div>
                                        <br />

                                        <label>Device SubType</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddDeviceSubType" runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="0" />
                                                <telerik:RadComboBoxItem Text="Star TSP650" Value="1" />
                                                <telerik:RadComboBoxItem Text="Kinetic Saturn" Value="2" />
                                                <telerik:RadComboBoxItem Text="Sunmi" Value="3" />
                                                <telerik:RadComboBoxItem Text="Dualscreen" Value="4" />
                                                <telerik:RadComboBoxItem Text="iMin D1" Value="5" />
                                                <telerik:RadComboBoxItem Text="iMin D4" Value="6" />
                                                <telerik:RadComboBoxItem Text="Falcon" Value="7" />
                                                <telerik:RadComboBoxItem Text="Castles Pay" Value="8" />
                                                <telerik:RadComboBoxItem Text="Nucleus" Value="9" />
                                                <telerik:RadComboBoxItem Text="Pax A920" Value="10" />
                                                <telerik:RadComboBoxItem Text="Ingenico" Value="11" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div id="divPrinter" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <label>Network Type <span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddNetworkType" runat="server" Width="100%" AutoPostBack="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="SELECT" Value="SELECT" />
                                                    <telerik:RadComboBoxItem Text="Serial Port" Value="Serial Port" />
                                                    <telerik:RadComboBoxItem Text="USB" Value="USB" />
                                                    <telerik:RadComboBoxItem Text="LAN" Value="LAN" />
                                                    <telerik:RadComboBoxItem Text="MINIPOS" Value="MINIPOS" />
                                                    <telerik:RadComboBoxItem Text="ADD PAY" Value="ADD PAY" />
                                                    <telerik:RadComboBoxItem Text="SUNMI" Value="SUNMI" />
                                                    <telerik:RadComboBoxItem Text="iMin D1" Value="iMin D1" />
                                                    <telerik:RadComboBoxItem Text="iMin D4" Value="iMin D4" />
                                                    <telerik:RadComboBoxItem Text="Falcon" Value="Falcon" />
                                                    <telerik:RadComboBoxItem Text="Castles Pay" Value="Castles Pay" />
                                                    <telerik:RadComboBoxItem Text="Nucleus" Value="Nucleus" />
                                                    <telerik:RadComboBoxItem Text="Pax A920" Value="Pax A920" />
                                                    <telerik:RadComboBoxItem Text="Ingenico" Value="Ingenico" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddNetworkType"
                                                ValidationGroup="valid" Display="none" ErrorMessage="Network type is required" CssClass="text-danger" InitialValue="SELECT">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div id="divUSB" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <label>Vender Id <span class="text-danger">*</span> </label>
                                                <div class="clearfix"></div>

                                                <telerik:RadNumericTextBox MinValue="0" ID="txtvenderid" Skin="" CssClass="form-control"
                                                    runat="server" Width="100%" placeholder="Vender Id">
                                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                                <asp:RequiredFieldValidator ID="rqvenderid" runat="server" ControlToValidate="txtvenderid"
                                                    ValidationGroup="valid" Display="none" ErrorMessage="Vender id is required" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtvenderid"
                                                    ErrorMessage="Enter only numeric and characters in alias" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                        </div>
                                        <div id="divSerialPort" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <label>Budrate <span class="text-danger">*</span> </label>
                                                <div class="clearfix"></div>
                                                <telerik:RadTextBox ID="txtBudrate" CssClass="form-control" Skin="" runat="server" placeholder="Budrate" Width="100%"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="rqbudrate" runat="server" ControlToValidate="txtBudrate"
                                                    ValidationGroup="valid" Display="none" ErrorMessage="Budrate is required" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBudrate"
                                                    ErrorMessage="Enter only numeric and characters in Budrate" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12">
                                                <label>Device Name <span class="text-danger">*</span> </label>
                                                <div class="clearfix"></div>
                                                <telerik:RadTextBox ID="txtDeviceName" CssClass="form-control" Skin="" runat="server" placeholder="Device Name" Width="100%"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="rqdevicename" runat="server" ControlToValidate="txtDeviceName"
                                                    ValidationGroup="valid" Display="none" ErrorMessage="Device name is required" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDeviceName"
                                                    ErrorMessage="Enter only numeric and characters in alias" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                        </div>
                                        <div id="divLAN" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <label>Ip Address <span class="text-danger">*</span> </label>
                                                <div class="clearfix"></div>
                                                <telerik:RadTextBox ID="txtIpAddress" CssClass="form-control" Skin="" runat="server" placeholder="Enter this format : 192-168-1-20" Width="100%"></telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="rqipaddress" runat="server" ControlToValidate="txtIpAddress"
                                                    ValidationGroup="valid" Display="none" ErrorMessage="Ip address is required" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12">
                                                <label>Port <span class="text-danger">*</span> </label>
                                                <div class="clearfix"></div>
                                                <%-- <telerik:RadTextBox ID="txtport" CssClass="form-control" Skin="" runat="server" placeholder="Port" Width="100%"></telerik:RadTextBox>--%>
                                                <telerik:RadNumericTextBox MinValue="0" ID="txtport" Skin="" CssClass="form-control"
                                                    runat="server" Width="100%" placeholder="Port">
                                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtport"
                                                    ErrorMessage="Enter only numeric and characters in alias" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rqport" runat="server" ControlToValidate="txtport"
                                                    ValidationGroup="valid" Display="none" ErrorMessage="Port is required" CssClass="text-danger">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                        </div>
                                    </div>
                                    <div id="divEVO" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <label>User Name<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtUserName" CssClass="form-control" Skin="" runat="server" placeholder="User Name" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUserName" ErrorMessage="User Name is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>Password<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtpassword" CssClass="form-control" Skin="" runat="server" placeholder="Password" Width="100%" TextMode="Password"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtpassword" ErrorMessage="Password is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>Bluetooth Name<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtblutoothName" CssClass="form-control" Skin="" runat="server" placeholder="Bluetooth Name" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtblutoothName" ErrorMessage="Bluetooth Name is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>Application Profile Id<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="TxtAPI" CssClass="form-control" Skin="" runat="server" placeholder="API" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtAPI" ErrorMessage="API is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>Service Key<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtServicekey" CssClass="form-control" Skin="" runat="server" placeholder="Service Key" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtServicekey" ErrorMessage="Service key is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>

                                    <div id="divKinetic" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <label>User Name<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtUserName1" CssClass="form-control" Skin="" runat="server" placeholder="User Name" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtUserName1" ErrorMessage="User Name is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>Password<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtpassword1" CssClass="form-control" Skin="" runat="server" placeholder="Password" Width="100%" TextMode="Password"></telerik:RadTextBox>
                                            <asp:HiddenField ID="hfPassword" runat="server" />
                                            <asp:RequiredFieldValidator ID="rvPassword" runat="server" ControlToValidate="txtpassword1" ErrorMessage="Password is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>IP Address<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtIpAddress1" CssClass="form-control" Skin="" runat="server" placeholder="Enter this format : 192.168.1.20" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtIpAddress1" ErrorMessage="Ip address is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>Port <span class="text-danger">*</span> </label>
                                            <div class="clearfix"></div>
                                            <%-- <telerik:RadTextBox ID="txtport" CssClass="form-control" Skin="" runat="server" placeholder="Port" Width="100%"></telerik:RadTextBox>--%>
                                            <telerik:RadNumericTextBox MinValue="0" ID="txtPortKinetic" Skin="" CssClass="form-control"
                                                runat="server" Width="100%" placeholder="Port">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPortKinetic"
                                                ErrorMessage="Enter only numeric and characters in alias" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPortKinetic"
                                                ValidationGroup="valid" Display="none" ErrorMessage="Port is required" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                       <div id="divKinetic1" runat="server" visible="false">
                                       <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>IP Address<span class="text-danger">*</span></label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtIpAddress2" CssClass="form-control" Skin="" runat="server" placeholder="Enter this format : 192.168.1.20" Width="100%"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtIpAddress2" ErrorMessage="Ip address is required"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="col-md-12">
                                            <label>Port <span class="text-danger">*</span> </label>
                                            <div class="clearfix"></div>
                                            <%-- <telerik:RadTextBox ID="txtport" CssClass="form-control" Skin="" runat="server" placeholder="Port" Width="100%"></telerik:RadTextBox>--%>
                                            <telerik:RadNumericTextBox MinValue="0" ID="txtPortKinetic1" Skin="" CssClass="form-control"
                                                runat="server" Width="100%" placeholder="Port">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPortKinetic1"
                                                ErrorMessage="Enter only numeric and characters in alias" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtPortKinetic1"
                                                ValidationGroup="valid" Display="none" ErrorMessage="Port is required" CssClass="text-danger">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Width<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtwidth" CssClass="form-control" Skin="" runat="server" placeholder="Width" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtwidth" ErrorMessage="Width is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Active &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Device Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Device Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Device Details" />
                    </div>
                </div>
            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>

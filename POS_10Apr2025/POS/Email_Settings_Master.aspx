<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Email_Settings_Master.aspx.vb" Inherits="Email_Settings_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Email Settings Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Email Settings Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="text" style="display: none" />
    <input type="password" style="display: none" />
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow" runat="server" id="divCompany">
                <div class="panel-heading">Email Settings Master</div>
                <div class="panel-body pan" >

                    <div class="form-body pal">
                        <div class="row">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validemail"
                                DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <h4>Email Information</h4>
                                    <asp:HiddenField ID="hfEmailId" runat="server" Value="0" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group" runat="server" visible="false">
                                    <div class="col-md-12 col-sm-6">
                                        <asp:RadioButton ID="rbtnemail" GroupName="ESType" Checked="True" Text="Email" runat="server" AutoPostBack="true" />&nbsp;
                            <asp:RadioButton ID="rbtnsms" GroupName="ESType" Text="SMS" runat="server" AutoPostBack="true" Visible="false" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <br />
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 col-sm-6">
                                        <label>
                                            Email Server URL  <span class="text-danger">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="text-danger"
                                                ControlToValidate="txtserver" ValidationGroup="validemail" Display="None" ErrorMessage="Email server url is required"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadTextBox ID="txtserver" CssClass="form-control" Skin="" runat="server" placeholder="Email Server URL" Width="100%"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-12 col-sm-6">
                                        <label>
                                            User Name  <span class="text-danger">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  CssClass="text-danger"
                            ControlToValidate="txtusername1" ValidationGroup="validemail" Display="None" ErrorMessage="User name is required"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadTextBox ID="txtusername1" CssClass="form-control" Skin="" runat="server" placeholder="User Name" Width="100%"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-12 col-sm-6">
                                        <label>
                                            Password  <span class="text-danger">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-danger"
                            ControlToValidate="txtpassword1" ValidationGroup="validemail" Display="None" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadTextBox ID="txtpassword1" CssClass="form-control" Skin="" runat="server" placeholder="Password" Width="100%" TextMode="Password"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-12 col-sm-6">
                                        <label>
                                            Email Port Sender ID  <span class="text-danger">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-danger"
                            ControlToValidate="txtport" ValidationGroup="validemail" Display="None" ErrorMessage="Email port sender id is required"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadTextBox ID="txtport" CssClass="form-control" Skin="" runat="server" placeholder="Email Port Sender ID" Width="100%"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" id="divOtherInfo" runat="server">
                                <div class="form-group">
                                    <h4>Other Information</h4>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-6 col-sm-6">
                                        <label>From Email</label>
                                        <telerik:RadTextBox ID="txtfromemail" CssClass="form-control" Skin="" runat="server" placeholder="From Email" Width="100%"></telerik:RadTextBox>
                                          <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtfromemail" ValidationGroup="validemail" Display="None"
                                            CssClass="text-danger" ErrorMessage="Please enter valid from email " ValidationExpression="([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <label>Reply To</label>
                                        <telerik:RadTextBox ID="txtreplyto" CssClass="form-control" Skin="" runat="server" placeholder="Reply To" Width="100%"></telerik:RadTextBox>
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtreplyto" ValidationGroup="validemail" Display="None"
                                            CssClass="text-danger" ErrorMessage="Please enter valid reply to" ValidationExpression="([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-6 col-sm-6">
                                        <label class="form-label">
                                            SSL  <span class="text-danger">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  CssClass="text-danger"
                                            ControlToValidate="radssl" ValidationGroup="validemail"  Display="None" ErrorMessage="Ssl is required" InitialValue="SELECT"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radssl" runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="SELECT" Selected="true" />
                                                <telerik:RadComboBoxItem Text="True" Value="1" />
                                                <telerik:RadComboBoxItem Text="False" Value="0" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-6 col-sm-6">

                                        <label class="form-label">
                                            Email Alias</label>
                                        <telerik:RadTextBox ID="txtemailalias" CssClass="form-control" Skin="" runat="server" placeholder="Email Alias" Width="100%"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-6 col-sm-6">
                                        <label class="form-label">
                                            Microsoft Exchange Server</label><div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radmes" runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="0" Selected="true" />
                                                <telerik:RadComboBoxItem Text="True" Value="1" />
                                                <telerik:RadComboBoxItem Text="False" Value="0" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-6 col-sm-6">
                                        <label class="form-label">
                                            MES URI</label>
                                        <telerik:RadTextBox ID="txtmesuri" CssClass="form-control" Skin="" runat="server" placeholder="Microsoft exchange server URI" Width="100%"></telerik:RadTextBox>
                                    </div>
                                      <div class="col-md-6 col-sm-6">
                                        <label class="form-label">
                                            Location</label><div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddLocation" runat="server" AutoPostBack="true" Width="100%">
                                           
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                    </div>
                    
                     <div class="form-actions text-right pal" id="Div_Button" runat="server" >
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="validemail" ToolTip="Click here to Save Email Settings" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Email Settings" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Email Settings" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


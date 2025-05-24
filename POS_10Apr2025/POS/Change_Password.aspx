<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Change_Password.aspx.vb" Inherits="Change_Password" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Change Password
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Change Password</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="text" style="display: none" />
    <input type="password" style="display: none" />
    <br />
    <div class="col-lg-12 ">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow" id="divchangepassword" runat="server">
            <div class="panel-heading">Change Password</div>

            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validpass"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                <div class="col-md-12" runat="server" visible="false" id="divCheck">
                                    <asp:RadioButton ID="rbtnPass" GroupName="ESType" Checked="True" runat="server" AutoPostBack="true" />&nbsp; Change Password&nbsp;
                                        <asp:RadioButton ID="rbtnSPass" GroupName="ESType" runat="server" AutoPostBack="true" />&nbsp;Change User Password
                                        <div class="clearfix"></div>
                                    <br />
                                </div>
                                <div runat="server" id="divEmployee">
                                    <div class="col-md-6">
                                        <label>User <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <asp:DropDownList ID="radEmployee" runat="server" AutoPostBack="true" Width="100%" Filter="Contains" AllowCustomText="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfEmployee" runat="server" ControlToValidate="radEmployee"
                                            ValidationGroup="validpass" Display="none" CssClass="text-danger" ErrorMessage="User is required" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>
                                <div runat="server" id="divOldPass">
                                    <div class="col-md-6">
                                        <label>Old Password <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox ID="txtOldPassword" CssClass="form-control" Skin="" runat="server" placeholder="Old Password" Width="100%" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfOldPass" runat="server" ControlToValidate="txtOldPassword" ErrorMessage="Old password is required"
                                            ValidationGroup="validpass" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>
                                <div>
                                    <div class="col-md-6">
                                        <label>New Password <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox ID="txtNewPassword" CssClass="form-control" Skin="" runat="server" placeholder="New Password" Width="100%" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New password is required"
                                            ValidationGroup="validpass" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>
                                <div>
                                    <div class="col-md-6">
                                        <label>Confirm Password <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <asp:TextBox ID="txtConfirm" CssClass="form-control" Skin="" runat="server" placeholder="Confirm Password" Width="100%" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConfirm" ErrorMessage="Confirm password is required"
                                            ValidationGroup="validpass" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-md-12">
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New password and confirm password does not match" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirm"
                                        CssClass="text-danger" ValidationGroup="validpass" Display="Dynamic"></asp:CompareValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
                <div class="form-actions text-right pal" id="Div_Button" runat="server">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="validpass" ToolTip="Click here to Save Password" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Password" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel" />
                </div>
            </div>


        </div>

        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


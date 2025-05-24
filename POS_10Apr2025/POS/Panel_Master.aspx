<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Panel_Master.aspx.vb" Inherits="Panel_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Panel Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Panel_List.aspx">Panel List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Panel Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />

    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Panel Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                        <label>
                                            Panel <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txpanel" CssClass="form-control" Skin="" runat="server" placeholder="Panel" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txpanel"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Panel is required">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Active Panel &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true"  /></label>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                        </div>
                        <div class="row">
                        </div>

                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Panel" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Panel Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Panel Details" />
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
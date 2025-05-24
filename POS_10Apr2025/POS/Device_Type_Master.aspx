<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Device_Type_Master.aspx.vb" Inherits="Device_Type_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Device Type Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Device_Type_List.aspx">Device Type List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Device Type Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />

    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Device Type Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                        <label>
                                            Name <span class="text-danger">*</span>
                                        </label>
                                        <div class="clearfix"></div>

                                        <telerik:RadTextBox ID="txtName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtName"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Device type name is required">
                                        </asp:RequiredFieldValidator>
                                        
                                    </div>
                                     <div class="clearfix"></div>
                                    <br />
                                      
                                     <div class="col-md-6">
                                        <label>
                                            Active &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
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
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Device Type Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Device Type Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Device Type Details" />
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>


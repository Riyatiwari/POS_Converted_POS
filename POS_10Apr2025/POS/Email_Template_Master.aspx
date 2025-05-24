<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Email_Template_Master.aspx.vb" Inherits="Email_Template_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Email Template Master
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Email_Template_List.aspx">Email Template List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Email Template Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Email Template Master</div>
                <div class="panel-body pan" runat="server" id="divCompany">

                    <asp:HiddenField ID="Email_Template_ID" runat="server" Value="0" />
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-sm-6 col-md-6">
                                <label>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                    Name<span class="text-danger">&nbsp;*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Template name is required" CssClass="text-danger"
                            ControlToValidate="txtTempName" ValidationGroup="valid" Display="None"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtTempName" CssClass="form-control span12 form-Control_half" runat="server" placeholder="Name"></asp:TextBox>
                                
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6 col-md-6">
                                <label>
                                    Subject
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="text-danger"
                            ControlToValidate="txtSubject" ValidationGroup="validTemp" Display="None"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtSubject" CssClass="form-control span12 form-Control_half" runat="server" placeholder="Subject"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSubject"
                                            ErrorMessage="Enter only numeric and characters in subject" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-6 col-md-6">
                                <label>
                                    Body
                                </label>
                                <telerik:RadEditor ID="txtbody" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="300px" Style="overflow: auto;">
                                </telerik:RadEditor>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Template" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Template" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Template" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>

<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Printer_Master.aspx.vb" Inherits="Printer_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Printer Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Printer_List.aspx">Printer List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Printer Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12 ">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Printer Master</div>
            <div class="panel-body pan">

                <div class="form-body pal">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtPName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"
                                        AutoPostBack="true" OnTextChanged="txtPName_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPName"
                                        ValidationGroup="valid" Display="none" ErrorMessage="Printer name is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Alias </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtPAlias" CssClass="form-control" Skin="" runat="server" placeholder="Alias" Width="100%"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPAlias"
                                        ErrorMessage="Enter only numeric and characters in alias" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                </div>

                                <div class="clearfix"></div>

                                <br />
                                <div class="col-lg-12 ">
                                    <asp:RadioButtonList ID="rbprintproduct" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Text=" &nbsp;&nbspPrint Product Small&nbsp;&nbsp" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;&nbspPrint Product Large&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;&nbspPrint Product Extra Large&nbsp;&nbsp;" Value="2"></asp:ListItem>

                                    </asp:RadioButtonList>
                                </div>

                                <div class="clearfix"></div>

                                <br />
                                <div class="col-lg-12 ">
                                    <asp:RadioButtonList ID="rbprintcondiments" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Text="&nbsp;&nbspPrint Condiments Small&nbsp;&nbsp" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;&nbspPrint Condiments Large&nbsp;&nbsp" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;&nbspPrint Condiments Extra Large&nbsp;&nbsp" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="clearfix"></div>

                                <br />

                                <div class="col-md-12">
                                    <label>
                                        Group By &nbsp; &nbsp;
                                               
                                            <asp:CheckBox ID="chkgroupby" runat="server" Checked="false" AutoPostBack="true" /></label>
                                    &nbsp; &nbsp; &nbsp;
                                         <label>
                                             ConsoliDate &nbsp; &nbsp;
                                        <asp:CheckBox ID="chkdate" runat="server" Checked="False" AutoPostBack="true" /></label>
                                </div>

                            </div>
                            <div class="clearfix"></div>

                            <br />
                            <div id="divgroupby" runat="server" visible="false">
                                <div class="col-md-12">
                                    <label>Group By With:</label>

                                    <asp:DropDownList ID="ddlgroupby" runat="server" Width="100%" AutoPostBack="true">
                                        <asp:ListItem Text="None" Value="0" Selected="true" />
                                        <asp:ListItem Text="Departmnet" Value="1" />
                                        <asp:ListItem Text="Course" Value="2" />
                                        <asp:ListItem Text="Departmnet Category" Value="3" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                            <br />

                            <div class="col-md-6">
                                <label>
                                    Is OrderFlag &nbsp;
                                            <asp:CheckBox ID="chkOrderFlag" runat="server" /></label>
                            </div>


                            <div class="clearfix"></div>
                            <br />

                            <div class="col-md-6">
                                <label>
                                    Active &nbsp;
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                            </div>

                            <div class="col-md-6">
                                <label>
                                    Default Printer &nbsp;
                                            <asp:CheckBox ID="chkDefaultPrinter" runat="server" /></label>
                                <br />
                                <i>Note: This will be consider as default printer for print.</i>
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
                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Printer Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Printer Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Printer Details" />
            </div>

        </div>

    </div>

    <%--        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


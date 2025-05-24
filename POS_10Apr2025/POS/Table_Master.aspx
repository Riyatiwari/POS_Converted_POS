<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Table_Master.aspx.vb" Inherits="Table_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Table Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Table_List.aspx">Table List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Table Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Table Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtTName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Table Name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label class="lbl">Location <span class="text-danger">*</span></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddLocation"
                                            ValidationGroup="validSize" Display="none" CssClass="text-danger" ErrorMessage="Location is required" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddLocation" runat="server" Width="100%" ></telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Minimum Cover <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtMinCover" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Minimum Cover" MaxLength="4">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMinCover"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Minimum cover is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Maximum Cover <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtMaxCover" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Maximum Cover" MaxLength="4">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMaxCover"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Maximum cover is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Enter a value greater or equal than minimum cover value  " ControlToCompare="txtMinCover" ControlToValidate="txtMaxCover" Operator="GreaterThanEqual" ValidationGroup="valid" Display="None" CssClass="text-danger" Type="Integer"></asp:CompareValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Sorting No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtSortingno" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Sorting No" MaxLength="4" Value="0">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>

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
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Table Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Table Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Table Details" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

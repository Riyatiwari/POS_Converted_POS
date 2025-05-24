<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="IssueVoucher_Master.aspx.vb" Inherits="IssueVoucher_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Issue Voucher Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Tax_List.aspx">Tax List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Issue Voucher Master</li>
        </ol>
    </div>

    <script type="text/javascript">
        function handleChange(input) {
            if (input.value < 0) input.value = 0;
            if (input.value >= 100) input.value = 99.99;
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Issue Voucher Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    
                                    <div class="col-md-12">
                                        <label>Voucher </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddVoucher" runat="server" Width="100%" AutoPostBack="true">
                                          
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Start Value <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <div class="clearfix"></div>
                                        <%--Amt--%>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtStartValue" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Value" MaxLength="8">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartValue"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Value is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                        <%--%--%>
                                        <telerik:RadNumericTextBox MinValue="0" ID="tValue" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Value" MaxLength="8" Visible="false">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartValue"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Value is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                        
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                     <div class="col-md-12">
                                        <label>End Value <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <div class="clearfix"></div>
                                        <%--Amt--%>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtEndVal" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Value" MaxLength="8">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndVal"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Value is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                        <%--%--%>
                                        <telerik:RadNumericTextBox MinValue="0" ID="RadNumericTextBox2" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Value" MaxLength="8" Visible="false">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEndVal"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Value is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                        
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
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Tax Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Tax Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Tax Details" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


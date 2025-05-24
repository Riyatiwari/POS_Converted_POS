<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Credit_Account_Payment_Master.aspx.vb" Inherits="Credit_Account_Payment_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Credit Account Payment Master
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Credit_Account_Payment_List.aspx">Credit Account Payment List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Credit Account Payment Master</li>
        </ol>
    </div>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Credit Account Payment Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Customer Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <%--<telerik:RadTextBox ID="txtName" CssClass="form-control" Skin="" runat="server" placeholder="Customer Name" Width="100%"></telerik:RadTextBox>--%>

                                        <telerik:RadComboBox ID="RadAccount" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="RadAccount_SelectedIndexChanged" >
                                        </telerik:RadComboBox>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Mobile No. </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox Enabled="false" ID="txtMobileNo" CssClass="form-control" Skin="" runat="server" placeholder="Mobile No." Width="100%">

                                        </telerik:RadTextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Amount <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtAmount" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Amount">
                                            <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                        </telerik:RadNumericTextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Value is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="rexDiscount" runat="server"
                                            ValidationExpression="(^100([.]0{1,2})?)$|(^\d{1,2}([.]\d{1,2})?)$"
                                            ErrorMessage="Cannot discount over 100%" ControlToValidate="txtAmount"
                                            ValidationGroup="valid" Display="None" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Credit Date <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtCreditDate" runat="server" DateInput-EmptyMessage="Credit Date" MinDate="01/01/1000" MaxDate="01/01/3000" Width="100%" Skin="MetroTouch">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCreditDate"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Credit date is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Details" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>


</asp:Content>


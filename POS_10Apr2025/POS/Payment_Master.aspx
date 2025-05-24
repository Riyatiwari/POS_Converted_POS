<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Payment_Master.aspx.vb" Inherits="Payment_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Payment Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Payment_List.aspx">Payment List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Payment Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Payment Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtPName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPName"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                        
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Hostname <span class="text-danger">*</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txthostname" CssClass="form-control" Skin="" runat="server" placeholder="Hostname" Width="100%"></telerik:RadTextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txthostname"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Hostname is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>      <br />
                                      <div class="col-md-12">
                                        <label>Password <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                            <asp:HiddenField ID="hfpass" runat="server"/>
                                        <telerik:RadTextBox ID="txtpassword" CssClass="form-control"  TextMode="Password"  Skin="" runat="server" placeholder="Password" Width="100%"></telerik:RadTextBox>
                                             <asp:RequiredFieldValidator ID="rqPass" runat="server" ControlToValidate="txtpassword"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Password is required" CssClass="text-danger" Enabled="false"   >
                                        </asp:RequiredFieldValidator>
                                        
                                    </div>
                                    <div class="clearfix"></div>      <br />
                                      <div class="col-md-12">
                                        <label>Currency Code <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                      <%--  <telerik:RadTextBox ID="txtcurrencycode" CssClass="form-control" Skin="" runat="server" placeholder="Currency Code" Width="100%"></telerik:RadTextBox>--%>
                                           <telerik:RadNumericTextBox MinValue="0" ID="txtcurrencycode" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Currency Code">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                         
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcurrencycode"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Currency code is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                     <div class="col-md-12">
                                        <label>Transaction Type <span class="text-danger">*</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddTransactionType" runat="server" Width="100%" >
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="SELECT" />
                                                <telerik:RadComboBoxItem Text="Sale" Value="Sale" />
                                                <telerik:RadComboBoxItem Text="Refund" Value="Refund" />
                                                 <telerik:RadComboBoxItem Text="Referral" Value="Referral" />
                                                 <telerik:RadComboBoxItem Text="Sale Reversal" Value="Sale Reversal" />
                                                 <telerik:RadComboBoxItem Text="Refund Reversal" Value="Refund Reversal" />                                          
                                            </Items>
                                        </telerik:RadComboBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddTransactionType"
                                            ValidationGroup="valid" Display="none" ErrorMessage="Transaction type is required" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
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
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Payment Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Payment Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Payment Details" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


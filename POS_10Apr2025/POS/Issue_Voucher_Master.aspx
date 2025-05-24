<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Issue_Voucher_Master.aspx.vb" MasterPageFile="~/MasterPage.master" Inherits="Issue_Voucher_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Voucher Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Issue_Voucher_List.aspx">Issue Voucher List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Issue Voucher Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Issue Voucher  Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="container">
                            <div class="form-group">
                                <div class="row mt-3">
                                    <div class="clearfix"></div>

                                    <div class="col-sm-6">

                                        <label>Account<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="RadAccount" runat="server" Width="30%" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-sm-6">

                                        <label>Voucher </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="RadVoucher" runat="server" Width="30%" AutoPostBack="true"
                                            OnSelectedIndexChanged="RadVoucher_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="form-group">
                                <div class="row mt-3">
                                    <div class="clearfix"></div>
                                    <div class="col-sm-6">
                                        <label>Deposit Amount </label>

                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox ID="RadTxtdepamount" CssClass="form-control" Skin="" runat="server" OnTextChanged="RadTxtdepamount_TextChanged" Width="50%" placeholder="Deposit Amount ">
                                            <NumberFormat DecimalDigits="2" AllowRounding="false" KeepNotRoundedValue="true" />
                                        </telerik:RadNumericTextBox>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="col-sm-6">

                                        <label>Ref No </label>
                                        <div class="clearfix"></div>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtrefno" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="Ref No"></telerik:RadTextBox>
                                        <div class="clearfix"></div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="form-group">
                                <div class="row mt-3">
                                    <div class="clearfix"></div>
                                    <div class="col-sm-6" style="display:none">

                                        <label>Issue Date Time <span class="text-danger">&nbsp;</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtissuetime" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="30%">
                                            <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar ID="Calendar3" runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </div>
                                    <div class="col-sm-6">

                                        <label>Duration</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radDuration" runat="server" AutoPostBack="true" Width="50%"
                                            OnSelectedIndexChanged="radDuration_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Today" Value="Today" />
                                                <telerik:RadComboBoxItem Text="This Week" Value="This Week" />
                                                <telerik:RadComboBoxItem Text="This Month" Value="This Month" />
                                                <telerik:RadComboBoxItem Text="6 Month" Value="6 Month" />
                                                <telerik:RadComboBoxItem Text="This Year" Value="This Year" />
                                                <telerik:RadComboBoxItem Text="Endless" Value="Endless" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <div class="clearfix"></div>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="container">
                            <div class="form-group">
                                <div class="row mt-3">
                                    <div class="clearfix"></div>
                                    <div class="col-sm-6">
                                        <label>Issue Date Time <span class="text-danger">&nbsp;</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtstarttime" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="30%"
                                            OnSelectedDateChanged="txtstarttime_SelectedDateChanged">
                                            <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar ID="Calendar1" runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </div>
                                    <div class="col-sm-6" id="end_date" runat="server">

                                        <label>End Date <span class="text-danger">&nbsp;</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtenddate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="30%">
                                            <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar ID="Calendar2" runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <%--<div class="form-body pal">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtproname"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Profile Name  is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                                
                                <div class="col-md-12">
                                    <label>Profile Name <span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtproname" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Bonus Point</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtbnsp" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Amount </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtamt" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                <%--  </div>--%>

                        <%-- <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Purchase Amount</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtpurchase_amount" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                 <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Earn Bonus</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtearn_bonus" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                   </div>
                                 </div> 
                              
                                <div class="clearfix"></div>
                                <br />
                            
                        </div>
                        <div class="row">
                        </div>
                        </div>--%>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="valid" ToolTip="Click here to Save Profile Details" />&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" OnClick="btnReset_Click" ToolTip="Click here to Reset Profile Details" />&nbsp;&nbsp;&nbsp;
            
                       <asp:Button ID="btnCancel" class="btn btn-green" runat="server" OnClick="btnCancel_Click" Text="Cancel" ToolTip="Click here to Cancel Profile Details" />
                    </div>
                </div>

            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

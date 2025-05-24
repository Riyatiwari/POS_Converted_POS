<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Voucher_Master.aspx.vb" Inherits="Voucher_Master" %>



 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Voucher Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Voucher_List.aspx">Voucher List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Voucher Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Voucher  Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="container">
                            <div class="form-group">
                                <div class="row mt-3">
                                    <div class="clearfix"></div>
                                    <div class="col-sm-4">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Voucher Name<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtvochername" CssClass="form-control" Skin="" runat="server" Width="70%" placeholder="Voucher Name"></telerik:RadTextBox>
                                        <div class="clearfix"></div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtvochername" ErrorMessage=" Please Enter The Voucher Name "
                                            ValidationGroup="valid"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-4">

                                        <label>Voucher Type<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="RadVoucherType" runat="server" Width="70%" OnSelectedIndexChanged="RadVoucherType_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="" />
                                                <telerik:RadComboBoxItem Text="Gift Cards" Value="Gift Cards" />
                                                <telerik:RadComboBoxItem Text="Deposits" Value="Deposits" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-sm-4">
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

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container">
                            <div class="form-group">
                                <div class="row  mt-3">
                                    <div class="clearfix"></div>
                                    <div class="col-sm-4">
                                        <label>Start Date <span class="text-danger">&nbsp;</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtstarttime" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="50%"
                                            OnSelectedDateChanged="txtstarttime_SelectedDateChanged">
                                            <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar ID="Calendar3" runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                    </div>
                                    <div class="col-sm-4" id="end_date" runat="server">

                                        <label>End Date <span class="text-danger">&nbsp;</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtenddate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="50%">
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
                        <div class="container">
                            <div class="form-group">
                                <div class="row  mt-3">
                                    <div class="col-sm-6" id="chkendless" runat="server">
                                        <label>Endless </label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chk_endless" runat="server" OnCheckedChanged="chk_endless_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
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
                    <div class="row">
                    </div>
                    <div class="row">
                    </div>

                </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" ValidationGroup="valid" runat="server" Text="Save" ToolTip="Click here to Save Profile Details" OnClick="btnSave_Click1" />&nbsp;&nbsp;&nbsp;
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

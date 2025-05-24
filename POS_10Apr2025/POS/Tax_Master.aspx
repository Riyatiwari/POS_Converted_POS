<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Tax_Master.aspx.vb" Inherits="Tax_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Tax Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Tax_List.aspx">Tax List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Tax Master</li>
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
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        --%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Tax Master</div>
            <div class="panel-body pan">

                <div class="form-body pal">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                        ValidationGroup="valid" Display="none" ErrorMessage="Tax name is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Mode </label>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="ddMode" runat="server" Width="100%" 
                                        AutoPostBack="true" OnSelectedIndexChanged="ddMode_SelectedIndexChanged">
                                        <Items>
                                            <asp:ListItem Text="SELECT" Value="SELECT" />
                                            <asp:ListItem Text="%" Value="%" />
                                            <asp:ListItem Text="Amt" Value="Amt" />
                                        </Items>
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Value <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <div class="clearfix"></div>
                                    <%--Amt--%>
                                    <asp:TextBox MinValue="0" ID="txtValue1" Skin="" CssClass="form-control"
                                        runat="server" Width="100%" placeholder="Value" MaxLength="8">
                                           <%-- <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtValue1"
                                        ValidationGroup="valid" Display="none" ErrorMessage="Value is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                    <%--%--%>
                                    <asp:TextBox MinValue="0" ID="txtValue" Skin="" CssClass="form-control"
                                        runat="server" Width="100%" placeholder="Value" MaxLength="8" Visible="false">
                                            <%--<NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                    </asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValue"
                                        ValidationGroup="valid" Display="none" ErrorMessage="Value is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="rexDiscount" runat="server"
                                        ValidationExpression="(^100([.]0{1,2})?)$|(^\d{1,2}([.]\d{1,2})?)$"
                                        ErrorMessage="Cannot discount over 100%" ControlToValidate="txtValue"
                                        ValidationGroup="valid" Display="None" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Effective Date <span class="text-danger">*</span></label><div class="clearfix"></div>

                                    <div class="clearfix"></div>
                                      <telerik:RadDatePicker ID="txtEffDate" runat="server" DateInput-EmptyMessage="Effective Date" MinDate="01/01/1000" MaxDate="01/01/3000" Width="100%" Skin="MetroTouch">
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                        <Calendar runat="server" FirstDayOfWeek="Monday">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEffDate"
                                        ValidationGroup="valid" Display="none" ErrorMessage="Effective date is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clearfix"></div>
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
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Tax Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Tax Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Tax Details" />
                </div>

            </div>

        </div>

        <%--</telerik:RadAjaxPanel>--%>
    </div>

    <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


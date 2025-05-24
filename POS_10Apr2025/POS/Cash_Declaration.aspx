<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cash_Declaration.aspx.vb" Inherits="Cash_Declaration" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Cash Declaration
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <%--  <li class="active"><a href="Tax_List.aspx">Tax List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>--%>
            <li class="active">Cash Declaration</li>
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

    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Cash Declaration</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <%--<div class="col-md-12">--%>
                            <div class="form-group">
                                <div class="col-md-12">

                                    <%--    <div class="col-md-2">
                                                 <asp:HiddenField ID="hdreceipt_index" runat="server" Value='<%#Eval("receipt_index")%>' />
                                            <label>Receipt Number</label><div class="clearfix"></div>

                                            <telerik:RadTextBox ID="txtreceipt" CssClass="form-control" Skin="" runat="server" placeholder="ReceiptNumber" Width="100%" MaxLength="200" ReadOnly="true"></telerik:RadTextBox>
                                              </div>
                                    <div class="col-md-2">
                                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Location <span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="rdlocation" runat="server" Width="100%" Skin="MetroTouch">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rflocation" runat="server" ControlToValidate="rdlocation"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="col-md-4">
                                        <label>For Date<span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtdate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="100%">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                            <Calendar runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator runat="server" ID="rqfordate" ControlToValidate="txtdate" ValidationGroup="valid" Display="None"
                                            ErrorMessage="Date is required"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="col-md-4">

                                        <label>Amount Type<span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>

                                        <telerik:RadComboBox ID="ddlamounttype" runat="server" Width="100%" AutoPostBack="true" Skin="MetroTouch">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="0" />
                                                <telerik:RadComboBoxItem Text="Card" Value="1" />
                                                <telerik:RadComboBoxItem Text="Cash" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>

                                    </div>

                                    <div class="col-md-2">
                                        <label>&nbsp;</label>
                                        <div class="clearfix"></div>
                                        <asp:Button ID="btnaddnew" class="btn btn-primary" runat="server" ValidationGroup="validSize" Text="+" ToolTip="Click here to Add New Cash Declaration" />
                                    </div>
                                   
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <telerik:RadGrid ID="rdexpense" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                            PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                            PagerStyle-AlwaysVisible="false" Skin="MetroTouch" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" Width="100%" Visible="false">
                                            <ClientSettings>
                                                <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                            </ClientSettings>
                                            <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false" Font-Size="90%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Amount *" UniqueName="Amount">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemTemplate>

                                                            <telerik:RadTextBox ID="txtamount" CssClass="form-control" Skin="" runat="server" placeholder="" Width="100%"
                                                                MaxLength="15" Text='<%# Eval("Amount")%>' AutoPostBack="true" OnTextChanged="txtamount_TextChanged" ReadOnly="false">
                                                            </telerik:RadTextBox>
                                                        </ItemTemplate>

                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridTemplateColumn HeaderText="Location " UniqueName="Location">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdmachine" runat="server" Value='<%#Eval("Machine_id")%>' />
                                                            <telerik:RadComboBox ID="ddlmachine" runat="server" Width="100%" AutoPostBack="true" Skin="MetroTouch" OnSelectedIndexChanged="ddlmachine_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="rqstockdetail" ControlToValidate="ddlmachine" ValidationGroup="valid" Display="None" InitialValue="SELECT"
                                                                ErrorMessage="Stock Detail is required"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdncashrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                            <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Save" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Reset" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip=" Cancel" />
                    </div>

                </div>
            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


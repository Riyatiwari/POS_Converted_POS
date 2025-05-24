<%@ Page Title="Search Booking" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="SearchResultTable.aspx.vb" Inherits="SearchResultTable" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .time-slot
        {
            float: left;
            margin: 5px;
        }
        .canvasjs-chart-credit
        {
            visibility: hidden !important;
        }
        .canvasjs-chart-canvas
        {
            width: 100% !important;
        }
    </style>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title">
        <div>
            <h1>
                <i class="fa fa fa-search"></i>Search Schedule
            </h1>
            <h4>
                Searching for Schedule.
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-search"></i>Search</h3>
                    <div class="box-tool">
                        <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="form-horizontal">
                        <div class="row" style="padding-left: 20px;">
                            Location:
                            <telerik:RadDropDownList ID="ddlVenue" Skin="MetroTouch" runat="server" Width="200px"
                                AutoPostBack="true" DropDownHeight="200px">
                            </telerik:RadDropDownList>
                            <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue"
                                ForeColor="Red" ErrorMessage="Please select Venue." Display="Dynamic" ValidationGroup="Search">
                            </asp:RequiredFieldValidator>
                            Date:
                            <telerik:RadDatePicker ID="dtpDate" runat="server" EnableTyping="true" Width="140px"
                                Skin="MetroTouch" AutoPostBack="true">
                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="dtpDate"
                                ForeColor="Red" ErrorMessage="*" Display="Static" ValidationGroup="SearchTable">
                            </asp:RequiredFieldValidator>
                            <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="120px"
                                DropDownHeight="120px" AutoPostBack="True">
                            </telerik:RadDropDownList>
                            <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
                            </asp:RequiredFieldValidator>
                            <%--<telerik:RadTimePicker ID="dtpTime" runat="server" EnableTyping="true" Width="100px" Skin="MetroTouch" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="rfvArrival" runat="server" ControlToValidate="dtpTime" ForeColor="Red" ErrorMessage="*" Display="Static" ValidationGroup="SearchTable">
                                    </asp:RequiredFieldValidator>--%>
                            No Of People:
                            <telerik:RadNumericTextBox ID="txtNoOfCovers" runat="server" Type="Number" Skin="Office2010Blue"
                                NumberFormat-DecimalDigits="0" Width="200px" Height="32px" DataType="Integer"
                                CssClass="form-control tg">
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="rfvNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable"
                                CssClass="rfv" Enabled="True">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="regNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                Type="Integer" MinimumValue="1" MaximumValue="9999" ForeColor="Red" ErrorMessage="*"
                                Display="Dynamic" ValidationGroup="SearchTable" CssClass="rfv" Enabled="True"></asp:RangeValidator>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                ValidationGroup="SearchTable" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="box" id="srchRslt" runat="server" >
                <div class="box-title">
                    <h3>
                        <i class="fa fa-table"></i>Search Result</h3>
                    <div class="box-tool">
                        <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="form-horizontal">
                        <div style="font-weight: bold; margin-bottom: 5px;">
                            Schedule Of
                            <asp:Label ID="lblTablesOf" Text="" runat="server" />
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="divMessageBox" runat="server" visible="False">
                                    <button data-dismiss="alert" class="close">
                                        ×</button>
                                    <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <asp:Repeater ID="repTables" runat="server">
                            <ItemTemplate>
                                <table cellpadding="4px" cellspacing="0" border="0" style="width: 100%; border: 1px solid gray;
                                    margin-bottom: 5px;">
                                    <tr>
                                        <td>
                                            <b>Schedule Name: </b>
                                            <%# Eval("Schedule")%>
                                            <asp:HiddenField ID="hdnStoreId" runat="server" Value='<%# Eval("StoreID")%>' />
                                            <asp:HiddenField ID="hdnSettingId" runat="server" Value='<%# Eval("SettingID")%>' />
                                        </td>
                                        <td style="width: 700px; text-align: center;">
                                            <asp:Repeater ID="repTimeSlot" runat="server" OnItemCommand="repTimeSlot_ItemCommand">
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="lnkBooked" runat="server" CssClass='<%# IIf(Eval("IsAvailable") = "1", "btn  btn-success btn-sm time-slot", "btn btn-gray btn-sm time-slot")%> ' 
                                                            Text='<%# Eval("SlotTime")%>' Enabled='<%# IIf(Eval("IsAvailable") = "1", True, False)%>' CommandArgument='<%# Eval("SlotTime")%>' CommandName="BookTable">
                                                            </asp:Button>--%>
                                                    <asp:Button ID="lnkBooked" runat="server" CssClass='<%# IIf(Eval("IsAvailable") = "1", "btn  btn-success btn-sm time-slot", "btn btn-gray btn-sm time-slot")%> '
                                                        Text='<%# TimeSpan.Parse(Eval("SlotTime").ToString()).ToString("hh\:mm")%>' Enabled='<%# IIf(Eval("IsAvailable") = "1", True, False)%>'
                                                        CommandArgument='<%# TimeSpan.Parse(Eval("SlotTime").ToString()).ToString("hh\:mm")%>'
                                                        CommandName="BookTable"></asp:Button>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="clear: both;">
                                                    </div>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12" id="chart" runat="server">
            <div class="box">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-table"></i>Table Overview</h3>
                    <div class="box-tool">
                        <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="form-horizontal">
                        <div class="row" style="padding-left: 20px;">
                            Date:
                            <telerik:RadDatePicker ID="rdpFTDate" runat="server" EnableTyping="true" Width="200px"
                                Skin="MetroTouch" AutoPostBack="true">
                                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="row" style="padding-left: 20px; padding-right: 20px;">
                            <div style="text-align: right;">
                                <asp:ImageButton ID="imgbtnRefreshChart" runat="server" Width="20px" Height="20px"
                                    ToolTip="Refresh Future Tables" ImageUrl="~/Booking/Images/refresh1.png" />
                            </div>
                            <br />
                            <br />
                            <div>
                                <script type="text/javascript">
                                                    <%= jscript %>
                                </script>
                                <script type="text/javascript" src="../Theme/js/canvasjs.min.js"></script>
                                <div id="chartContainer" style="width: 100%; height: 451px; overflow: scroll;">
                                </div>
                                <a id="ahref" href="" style="visibility: hidden;" class="edit-booking"></a>
                            </div>
                        </div>
                        <br />
                        <div class="row" style="padding-left: 20px; padding-right: 20px; text-align: center;">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-1">
                                <div style="height: 20px; width: 20px; background-color: #2F75B5; text-align: center;
                                    float: left;">
                                </div>
                                <div style="float: left; margin-left: 5px;">
                                    &nbsp;Booked</div>
                            </div>
                            <div class="col-md-2">
                                <div style="height: 20px; width: 20px; background-color: #AEAAAA; text-align: center;
                                    float: left;">
                                </div>
                                <div style="float: left; margin-left: 5px;">
                                    &nbsp;Joined to another table</div>
                            </div>
                            <div class="col-md-1">
                                <div style="height: 20px; width: 20px; background-color: #cacece; text-align: center;
                                    float: left;">
                                </div>
                                <div style="float: left; margin-left: 5px;">
                                    &nbsp;Closed</div>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

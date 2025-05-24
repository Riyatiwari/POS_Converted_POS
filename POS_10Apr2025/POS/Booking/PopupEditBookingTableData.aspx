<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupEditBookingTableData.aspx.vb"
    Inherits="BookingEasy_PopupEditBookingTableData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Theme/assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty-responsive.css" />
    <link rel="stylesheet" type="text/css" href="/css/Grid.ss.css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnlSearch" runat="server" Visible="true">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <style type="text/css" media="screen">
                            .container
                            {
                                width: 90%;
                                padding-left: 20px;
                            }
                            .left20
                            {
                                width: 20%;
                                float: left;
                            }
                            .left40
                            {
                                width: 40%;
                                float: left;
                            }
                            .clear
                            {
                                clear: both;
                            }
                        </style>
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-search"></i>Booking Details</h3>
                        </div>
                        <div class="container">
                            <div class="left20">
                                <b>
                                    <label class="col-lg-3 control-label">
                                        Booking:</label></b>
                            </div>
                            <div class="left20">
                                <asp:Label ID="lblBookingRef" runat="server"></asp:Label><br />
                            </div>
                            <div class="left20">
                                <b>
                                    <label class="col-lg-4 control-label">
                                        Covers:</label></b>
                            </div>
                            <div class="left20">
                                <asp:Label ID="lblCovers" runat="server"></asp:Label>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="left20">
                                <b>
                                    <label class="col-lg-3 control-label">
                                        Date:</label></b>
                            </div>
                            <div class="left20">
                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                            </div>
                            <div class="left20">
                                <b>
                                    <label class="col-lg-4 control-label">
                                        Time:</label></b>
                            </div>
                            <div class="left20">
                                <asp:Label ID="lblTime1" runat="server"></asp:Label>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="left20">
                                <b>
                                    <label class="col-lg-4 control-label">
                                        Location:</label></b>
                            </div>
                            <div class="left20">
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </div>
                            <div class="left20">
                                <b>
                                    <label class="col-lg-4 control-label">
                                        Schedule:</label></b>
                            </div>
                            <div class="left20">
                                <asp:Label ID="lblSchedule" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
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
                                    <b>Venue:</b>
                                    <asp:Label ID="lblVenue" runat="server"></asp:Label>
                                    &nbsp;&nbsp; <b>Date:</b>
                                    <telerik:RadDatePicker ID="dtpDate" runat="server" EnableTyping="true" Width="140px"
                                        Skin="MetroTouch" AutoPostBack="true">
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="dtpDate"
                                        ForeColor="Red" ErrorMessage="*" Display="Static" ValidationGroup="SearchTable">
                                    </asp:RequiredFieldValidator>
                                    &nbsp;&nbsp; <b>Schedule:</b>
                                    <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="120px"
                                        DropDownHeight="120px" AutoPostBack="True">
                                    </telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
                                    </asp:RequiredFieldValidator>
                                    &nbsp;&nbsp; <b>No Of People:</b>
                                    <telerik:RadNumericTextBox ID="txtNoOfCovers" runat="server" Type="Number" Skin="Office2010Blue"
                                        NumberFormat-DecimalDigits="0" Width="50px" DataType="Integer" Height="32px"
                                        CssClass="form-control tg">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rfvNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable"
                                        CssClass="rfv" Enabled="True">
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="regNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                        Type="Integer" MinimumValue="1" MaximumValue="9999" ForeColor="Red" ErrorMessage="*"
                                        Display="Dynamic" ValidationGroup="SearchTable" CssClass="rfv" Enabled="True"></asp:RangeValidator>&nbsp;&nbsp;
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                        ValidationGroup="SearchTable" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box">
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
                                    Tables Of
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
                                                                CommandName="BookTable" style="margin:5px;"></asp:Button>
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
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblTime" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-md-4 controls" style="text-align: center;">
                                        <asp:Button ID="btnConfitmBooking" runat="server" Text="Update Booking" CssClass="btn btn-success"
                                            Visible="false" OnClientClick="return confirm('Are you sure you want to Update this booking?');" />
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>

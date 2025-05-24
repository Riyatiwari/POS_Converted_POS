<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RoomBooking.ascx.vb"
    Inherits="UserControl_RoomBooking" %>
<div class="row">
            <div class="row" style="padding-left: 41px;">
        <label>
            <b>Venue:</b>&nbsp;</label>
        <telerik:RadDropDownList ID="ddlVenue" Skin="MetroTouch" runat="server" Width="200px"
            DropDownHeight="200px" AutoPostBack="true">
        </telerik:RadDropDownList>
        <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue"
            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="Search">
        </asp:RequiredFieldValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label>
            <b>Schedule:</b>&nbsp;</label>
        <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="200px"
            DropDownHeight="200px" AutoPostBack="true">
        </telerik:RadDropDownList>
        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="Search">
        </asp:RequiredFieldValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label>
            <b>Arrival:</b>&nbsp;</label>
        <telerik:RadDatePicker ID="dtpArrival" runat="server" EnableTyping="true" Width="200px"
            Skin="MetroTouch" AutoPostBack="True">
            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
            </DateInput>
        </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="rfvArrival" runat="server" ControlToValidate="dtpArrival"
            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="Search">
        </asp:RequiredFieldValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <label>
            <b>Departure:</b>&nbsp;</label>
        <telerik:RadDatePicker ID="dtpDeparture" runat="server" EnableTyping="true" Width="200px"
            Skin="MetroTouch">
            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
            </DateInput>
        </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="rfvDeparture" runat="server" ControlToValidate="dtpDeparture"
            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="Search">
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cmp" runat="server" ControlToValidate="dtpArrival" ControlToCompare="dtpDeparture"
            ForeColor="Red" ErrorMessage="Arrival date must be less then departure date."
            Operator="LessThanEqual" Type="Date" Display="Dynamic" ValidationGroup="Search"></asp:CompareValidator>
            &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
            ValidationGroup="Search" />
    </div>
</div>

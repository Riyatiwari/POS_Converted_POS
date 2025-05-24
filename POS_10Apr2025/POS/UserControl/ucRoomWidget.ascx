<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucRoomWidget.ascx.vb"
    Inherits="UserControl_ucRoomWidget" %>
<div class="form-group" style="text-align: center;">
    <h3>
        <label class="control-label" style="text-align: center;">
            Book Your Room</label>
    </h3>
</div>
<div class="form-group">
    <label class="col-lg-2 control-label" style="text-align: right;">
        Venue:</label>
    <div class="col-sm-10 col-lg-9 controls">
        <div class="input-group">
            <asp:DropDownList ID="ddlVenue" runat="server" AutoPostBack="true" CssClass="form-control input-lg">
            </asp:DropDownList>
            <span class="input-group-addon"><i class="glyphicon glyphicon-cutlery"></i></span>
        </div>
        <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue"
            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
        </asp:RequiredFieldValidator>
    </div>
</div>
<div class="form-group">
    <label class="col-lg-2 control-label">
        Schedule:</label>
    <div class="col-sm-10 col-lg-9 controls">
        <div class="input-group">
            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" CssClass="form-control input-lg">
            </asp:DropDownList>
            <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
        </div>
    </div>
</div>
<div class="form-group">
    <label class="col-lg-2 control-label">
        Arrival:</label>
    <div class="col-sm-10 col-lg-9 controls">
        <telerik:RadDatePicker ID="dtpArrival" runat="server" EnableTyping="true" Width="100%"
            Skin="MetroTouch" AutoPostBack="True">
            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
        </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="dtpArrival"
            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
        </asp:RequiredFieldValidator>
    </div>
</div>
<div class="form-group">
    <label class="col-lg-2 control-label">
        Departure:</label>
    <div class="col-sm-10 col-lg-9 controls">
        <telerik:RadDatePicker ID="dtpDeparture" runat="server" EnableTyping="true" Width="100%"
            Skin="MetroTouch">
            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
        </telerik:RadDatePicker>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpDeparture"
            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
        </asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" ForeColor="Red"  runat="server" ErrorMessage="Arrival Date should be Greater than Departure Date"  Display="Dynamic" Type="Date" ValidationGroup="SearchTable"  Operator="GreaterThan" ControlToCompare="dtpArrival" ControlToValidate="dtpDeparture"></asp:CompareValidator>
    </div>
</div>
<div class="form-group">
    <label class="col-lg-2 control-label">
            </label>
        <div class="col-sm-10 col-lg-9 controls">
            <div class="input-group">
                <asp:Button ID="btnSearch" runat="server" Text="Reserve Room" CssClass="btn btn-success"
            ValidationGroup="SearchTable" />
            </div>
        </div>
<%--    <div class="col-sm-12 col-lg-12">
        
        <asp:Button ID="btnMaxCover" runat="server" Text="More People" CssClass="btn btn-info btnMaxCover"/>
    </div>--%>
</div>
<%--<div id="divMessage" style="position: absolute; left: 200px; top: 325px; background-color: White;
    border: solid black 1px; padding: 10px; text-align: justify; font-size: 12px;
    width: 150px;">
    We are sorry but we cannot accept a more than&nbsp;<asp:Label ID="lblMaxCover" runat="server"></asp:Label> people booking via our online booking, please
    call <asp:Label ID="lblVenueName" runat="server"></asp:Label> on <asp:Label ID="lblNumber" runat="server"></asp:Label> to reserve your Schedule
</div>--%>
<asp:LinkButton ID="lnkDateChange" runat="server" Text="test" Visible="false"></asp:LinkButton>

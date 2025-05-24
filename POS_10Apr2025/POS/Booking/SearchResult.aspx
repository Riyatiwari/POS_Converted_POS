<%@ Page Title="Search Booking" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="SearchResult.aspx.vb" Inherits="SearchResult" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style>
.col-md-3 {
    width: 21% !Important;
}
</style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa fa-search"></i>Search Hotel
                    </h1>
                    <h4>
                        Searching for Hotel
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="row" style="padding-left: 34px; padding-bottom: 5px">
                    <asp:CompareValidator ID="cmp" runat="server" ControlToValidate="dtpArrival" ControlToCompare="dtpDeparture"
                        ForeColor="Red" ErrorMessage="Arrival date must be less then departure date."
                        Operator="LessThanEqual" Type="Date" Display="Dynamic" ValidationGroup="Search"></asp:CompareValidator>
                </div>
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
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="col-lg-3 control-label">
                                            Venue:</label>
                                        <div class="col-sm-9 col-lg-9 controls">
                                            <telerik:RadDropDownList ID="ddlVenue" Skin="MetroTouch" runat="server" Width="192px"
                                                DropDownHeight="200px" DefaultMessage="- Select -" AutoPostBack="true">
                                            </telerik:RadDropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue"
                                                ForeColor="Red" ErrorMessage="Please select Venue." Display="Dynamic" ValidationGroup="Search">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="col-lg-3 control-label">
                                            Schedule:</label>
                                        <div class="col-sm-9 col-lg-9 controls">
                                            <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="192px"
                                                DropDownHeight="200px">
                                            </telerik:RadDropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="col-lg-4 control-label">
                                            Arrival:</label>
                                        <div class="col-sm-9 col-lg-8 controls">
                                            <telerik:RadDatePicker ID="dtpArrival" runat="server" EnableTyping="true" Width="192px"
                                                Skin="MetroTouch" AutoPostBack="True">
                                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                            </telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator ID="rfvArrival" runat="server" ControlToValidate="dtpArrival"
                                                ForeColor="Red" ErrorMessage="Please select arrival date." Display="Dynamic"
                                                ValidationGroup="Search">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="col-lg-4 control-label">
                                            Departure:</label>
                                        <div class="col-sm-9 col-lg-8 controls">
                                            <telerik:RadDatePicker ID="dtpDeparture" runat="server" EnableTyping="true" Width="192px"
                                                Skin="MetroTouch">
                                                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                            </telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator ID="rfvDeparture" runat="server" ControlToValidate="dtpDeparture"
                                                ForeColor="Red" ErrorMessage="Please select departure date." Display="Dynamic"
                                                ValidationGroup="Search">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                            ValidationGroup="Search" />
                                    </div>
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
                                    Rooms Of
                                    <asp:Label ID="lblRoomsOf" Text="" runat="server" />
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
                                <asp:Repeater ID="repRooms" runat="server">
                                    <ItemTemplate>
                                        <table cellpadding="4px" cellspacing="0" border="0" style="width: 100%; border: 1px solid gray;
                                            margin-bottom: 5px;">
                                            <tr>
                                                <td>
                                                    <b>Hotel Name: </b>
                                                    <%# Eval("StoreName")%>
                                                </td>
                                                <td rowspan="3" style="width: 100px; text-align: center;">
                                                    <a href="BookingDetail.aspx?StoreId=<%# Utils.Encrypt(Eval("StoreId")).ToString().Replace("+", "___")%>&arrival=<%=ArrivalDate %>&departure=<%=DepartureDate %>&RoomType=<%# Eval("RoomTypeID")%>&VenueID=<%=VenueId %>&TypeID=<%=TypeID_byddl %>&IsOnline=<%=IsOnline %>"
                                                        class="btn btn-primary">Book Now</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Room Type:</b>
                                                    <%# Eval("RoomType")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Price:</b>
                                                    <%= CurrencySymbol %>&nbsp;<%# Eval("price")%>/ Night
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="custom_progress">
                <div id="overlay_load">
                </div>
                <div id="loading">
                    <img src="Images/ajax-loader.gif " alt="" />
                    <br />
                    Please Wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

<%@ Page Title="" Language="VB" MasterPageFile="~/BookingEasy/Site.master" AutoEventWireup="false"
    CodeFile="CustomerBooking.aspx.vb" Inherits="BookingEasy_CustomerBooking" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-search"></i>Search Your Booking</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-12 col-lg-12 controls">
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtBookingRef" runat="server" class="form-control" placeholder="Enter Booking Ref No."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqBookingRefNo" runat="server" ControlToValidate="txtBookingRef"
                                                ForeColor="Red" ErrorMessage="Enter Booking Ref No." Display="Dynamic" CssClass="rfv"
                                                ValidationGroup="Search">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Enter Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                ForeColor="Red" ErrorMessage="Enter Email." Display="Dynamic" CssClass="rfv"
                                                ValidationGroup="Search">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                ForeColor="Red" ErrorMessage="Enter Email In Proper Format." Display="Dynamic"
                                                CssClass="rfv" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="Search"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                                ValidationGroup="Search" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-list"></i>Search Result</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div id="divRoomDetail" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h3>
                                            Room Booking</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-hover fill-head" style="margin-bottom: 0"
                                            id="tblBooking" runat="server">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        Name
                                                    </th>
                                                    <th>
                                                        Room Type
                                                    </th>
                                                    <th>
                                                        Arrival
                                                    </th>
                                                    <th>
                                                        Depature
                                                    </th>
                                                    <th>
                                                        Number Of Nights
                                                    </th>
                                                    <th>
                                                        Total Price
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblName" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRoomType" Text="" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblArrival" Text="" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDepature" Text="" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNumberOfNights" Text="" runat="server" />
                                                    </td>
                                                    <td>
                                                        <%= CurrencySymbol %>&nbsp;<asp:Label ID="lblTotalPrice" Text="" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-hover fill-head" style="margin-bottom: 0">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        Product
                                                    </th>
                                                    <th>
                                                        Quantity
                                                    </th>
                                                    <th>
                                                        Total Price
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptServices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("Name")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Quantity")%>
                                                            </td>
                                                            <td>
                                                                <%= CurrencySymbol %>&nbsp;
                                                                <%# Eval("TotalPrice")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div id="divTableDetail" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h3>
                                            Table Booking</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-hover fill-head" style="margin-bottom: 0">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        Name
                                                    </th>
                                                    <th>
                                                        Booking Date
                                                    </th>
                                                    <th>
                                                        Booking Time
                                                    </th>
                                                    <th>
                                                        Number Of Covers
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblResName" Text="" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBookingDate" Text="" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBookingTime" Text="" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblNoOfCovers" Text="" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-hover fill-head" style="margin-bottom: 0">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        Product
                                                    </th>
                                                    <th>
                                                        Quantity
                                                    </th>
                                                    <th>
                                                        Total Price
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTableServices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("Name")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Quantity")%>
                                                            </td>
                                                            <td>
                                                                <%= CurrencySymbol %>&nbsp;
                                                                <%# Eval("TotalPrice")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div id="divErrorMessage" runat="server" visible="false">
                                <asp:Label ID="lblMessage" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
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

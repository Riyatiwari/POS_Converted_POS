<%@ Page Title="Schedule Booking Confirmation" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="TableBookConfime_FirstPayment.aspx.vb" Inherits="BookingEasy_TableBookConfime_FirstPayment"
    Debug="true" EnableViewStateMac="false" ValidateRequest="false" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <script type="text/javascript">
       history.pushState({ page: 1 }, "Title 1", "#no-back");
       window.onhashchange = function (event) {
           window.location.hash = "no-back";
       };
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title">
        <div>
            <h1>
                <i class="fa fa-file-text-o"></i>Booking Confirmation
            </h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box">
                <div class="box-content">
                    <div class="row">
                        <div class="col-md-12" style="text-align: right">
                            <asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-primary">
                                <span class="glyphicon glyphicon-print"></span> Print
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkEmail" runat="server" CssClass="btn btn-primary">
                                 <i class="fa fa-envelope"></i> Email
                            </asp:LinkButton>
                            <a id="aHome" runat="server" class="btn btn-primary">Home</a>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <asp:Panel ID="pnlPrint" runat="server" CssClass="form-horizontal">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12" id="divMessage" runat="server" visible="false" style="margin-top: 15px;">
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="col-lg-2 control-label">
                                    <b>Full Name:</b></label>
                                <asp:Label ID="lblFullName" runat="server" class="col-lg-2 control-label" Text=""
                                    Style="text-align: left;"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="col-lg-2 control-label">
                                    <b>Email:</b></label>
                                <asp:Label ID="lblEmail" runat="server" class="col-lg-2 control-label" Text="" Style="text-align: left;"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="col-lg-2 control-label">
                                    <b>Mobile:</b></label>
                                <asp:Label ID="lblMobile" runat="server" class="col-lg-2 control-label" Text="" Style="text-align: left;"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label class="col-lg-2 control-label">
                                    <b>Booking Ref No:</b></label>
                                <asp:Label ID="lblBookingRef" runat="server" class="col-lg-2 control-label" Text=""
                                    Style="text-align: left;" Font-Bold="true" ForeColor="Blue"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                &nbsp;
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" runat="server" id="table">
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
                                            <th>
                                                Alloted Tables
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblName" Text="" runat="server" />
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
                                             <td>
                                                <asp:Label ID="lblAlltedTable" Text="" runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-md-12" runat="server" id="room">
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
                                                <asp:Label ID="lblNameU" Text="" runat="server" />
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
                                    <tfoot>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <th>
                                                Total Amount :
                                            </th>
                                            <th>
                                                <%= CurrencySymbol %>&nbsp;
                                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <th>
                                                Deposited Amount :
                                            </th>
                                            <th>
                                                <%= CurrencySymbol %>&nbsp;
                                                <asp:Label ID="lblDepositedAmount" runat="server"></asp:Label>
                                            </th>
                                        </tr>
                                    </tfoot>
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
                                <label class="col-lg-2 control-label">
                                    <b>Comment:</b></label>
                                <label class="col-lg-2 control-label" style="text-align: left;">
                                    <asp:Label ID="lblComment" runat="server" Text="" Style="text-align: left;"></asp:Label>
                                </label>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnStoreID" runat="server" Value="0" />
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Booking_Confirm.aspx.vb"
    Inherits="Booking_Confirm" MasterPageFile="~/BookingEasy/Site.master" Title="Payment Information" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        input[type="radio"]
        {
            margin-right: 7px;
        }
    </style>
    <script language="Javascript">
       <!--
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
       //-->
    </script>
    <div class="page-title">
        <div>
            <h1>
                <i class="fa fa-file-text-o"></i>Payment Summary
            </h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box">
                <div class="box-content">
                    <div class="row">
                        <div class="col-md-12" style="text-align: right; display: none;">
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
                                            <th>
                                                Edit Booking
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
                                              <%--  <a href="" id="aChangeTable" runat="server" class="edit-booking">
                                                    <asp:Button ID="btnChangeTable" runat="server" Text="Edit" style="background:none;border:none;color:Blue;" />
                                                </a>--%>

                                                <asp:LinkButton ID="lnkpopup" runat="server" EnableViewState="true">
                                                    <a href="" id="btnChangeTable" runat="server" class="edit-booking">Edit</a></asp:LinkButton>
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
                                                        <asp:HiddenField ID="hfTotalPrice" Value='<%# Eval("TotalPrice")%>' runat="server" />
                                                        <asp:HiddenField ID="hfName" Value='<%# Eval("Name")%>' runat="server" />
                                                        <asp:HiddenField ID="hfQuantity" Value='<%# Eval("Quantity")%>' runat="server" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                &nbsp;
                                            </th>
                                            <th>
                                                <asp:Label ID="lblTotal" Text="Total Amount :" runat="server" />
                                            </th>
                                            <th>
                                                <%= CurrencySymbol %>&nbsp;
                                                <asp:Label ID="lblTotalPrice" runat="server" />
                                            </th>
                                        </tr>
                                        <tr>
                                            <th>
                                                &nbsp;
                                            </th>
                                            <th>
                                                <asp:Label ID="lblRequired" Text="Deposit Amount :" runat="server" />
                                            </th>
                                            <th id="deposit_lbl" runat="server">
                                                <%= CurrencySymbol %>&nbsp;
                                                <asp:Label ID="lblRequiredAmount" runat="server" />
                                            </th>
                                            <th id="deposit_txt" runat="server">
                                                <%= CurrencySymbol %>&nbsp;
                                                <asp:TextBox ID="txtDeposit" runat="server" onkeypress="return isNumberKey(event)" />
                                                <asp:RequiredFieldValidator ID="rfvDeposit" runat="server" ControlToValidate="txtDeposit"
                                                    ForeColor="Red" ErrorMessage="*" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </th>
                                        </tr>
                                        <%--<tr >
                                        <th>&nbsp;</th>
                                            <th>
                                                <asp:Label ID="lblPayable" Text="Payable Amount :" runat="server" />
                                            </th>
                                            <th>
                                            <%= CurrencySymbol %>&nbsp;
                                            <asp:Label ID="lblPayableAmount" Text="00.00" runat="server" />
                                            </th>
                                        </tr>--%>
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
    <div class="box" id="radioField" runat="server">
        <div class="box-content">
            <p style="font-size: larger; margin-left: 7px;">
                <b>Payment Option : </b>
            </p>
            <asp:RadioButtonList runat="server" ID="rblPaymentGateway" CellSpacing="20" RepeatLayout="Table"
                CellPadding="6" RepeatColumns="3" AutoPostBack="true">
            </asp:RadioButtonList>
        </div>
    </div>
    <div style="text-align: center; width: 100%">
        <asp:Button ID="payNow" runat="Server" Text="Pay Now" CssClass="btn btn-primary" />
        <asp:Button ID="ConfirmNow" runat="Server" Text="Confirm Booking" Visible="false"
            CssClass="btn btn-primary" />
    </div>
</asp:Content>

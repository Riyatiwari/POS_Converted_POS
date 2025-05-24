<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TableBooking.ascx.vb"
    Inherits="UserControl_TableBooking" %>
<div class="row">
            <div class="row" style="padding-left: 41px;">
                <label>
                    <b>Location:</b>&nbsp;</label>
                <telerik:RadDropDownList ID="ddlVenue" Skin="MetroTouch" runat="server" Width="200px"
                    DropDownHeight="200px" AutoPostBack="true">
                </telerik:RadDropDownList>
                <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue"
                    ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
                </asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                <label>
                    <b>Date:</b>&nbsp;</label>
                <telerik:RadDatePicker ID="dtpDate" runat="server" EnableTyping="true" Width="200px"
                    AutoPostBack="true" Skin="MetroTouch">
                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="dtpDate"
                    ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
                </asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <label>
                    <b>Schedule:</b>&nbsp;</label>
                <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="200px"
                    DropDownHeight="200px">
                </telerik:RadDropDownList>
                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                    ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
                </asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <label>
                    <b>No Of People:</b>&nbsp;</label>
                <%--<telerik:RadDropDownList ID="ddlNoOfCovers" Skin="MetroTouch" runat="server" Width="200px"
            DropDownHeight="200px" AutoPostBack="True">
            <Items>
                <telerik:DropDownListItem Value="1" Text="1" Selected="True"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="2" Text="2"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="3" Text="3"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="4" Text="4"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="5" Text="5"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="6" Text="6"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="7" Text="7"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="8" Text="8"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="9" Text="9"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="10" Text="10"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="11" Text="11"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="12" Text="12"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="13" Text="13"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="14" Text="14"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="15" Text="15"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="16" Text="16"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="17" Text="17"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="18" Text="18"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="19" Text="19"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="20" Text="20"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="-1" Text="Other"></telerik:DropDownListItem>
            </Items>
        </telerik:RadDropDownList>--%>
                <telerik:RadNumericTextBox ID="txtNoOfCovers" runat="server" Type="Number" Skin="Office2010Blue"
                    NumberFormat-DecimalDigits="0" Width="200px" Height="32px" DataType="Integer"
                    CssClass="form-control">
                </telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator ID="rfvNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                    ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable"
                    CssClass="rfv" Enabled="True">
                </asp:RequiredFieldValidator>
                <asp:RangeValidator ID="regNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                    Type="Integer" MinimumValue="1" MaximumValue="9999" ForeColor="Red" ErrorMessage="*"
                    Display="Dynamic" ValidationGroup="SearchTable" CssClass="rfv" Enabled="True"></asp:RangeValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                    ValidationGroup="SearchTable" />
            </div>
   
</div>

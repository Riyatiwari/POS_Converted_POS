<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucSearchBooking.ascx.vb" Inherits="UserControl_ucSearchBooking" %>
<asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
    <div class="form-group">
        <div class="col-sm-12 col-lg-12 controls">
            <div class="input-group">
                <span class="input-group-addon"><i class="fa fa-search"></i></span>
                <asp:TextBox ID="txtSearchClient" runat="server" class="form-control" placeholder="Search here..." Width="93%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSearchClient" runat="server" ControlToValidate="txtSearchClient" ForeColor="Red" ErrorMessage="*" Display="Static" ValidationGroup="Searching">
                </asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12 col-lg-12 controls">
            <asp:RadioButton ID="rdoCustomer" Text="Customer" Checked="true" runat="server" GroupName="SearchGroup" />
            <br />
            <span class="help-inline">&nbsp;<asp:Label ID="lblSearchOption" runat="server"></asp:Label></span>
            <br />
            <asp:RadioButton ID="rdoBooking" Text="Booking" runat="server" GroupName="SearchGroup" />
            <br />
            <span class="help-inline">&nbsp;(Search By Booking Ref No)</span>
        </div>
    </div>
    <div class="row" style="text-align: center; margin-bottom: 10px;">
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"  ValidationGroup="Searching"/>
    </div>
</asp:Panel>

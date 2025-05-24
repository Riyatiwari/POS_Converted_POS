<%@ Control Language="VB" AutoEventWireup="false" CodeFile="E_Menu.ascx.vb" Inherits="Controls_E_Menu" %>
<telerik:RadMenu runat="server" ID="RadMenu1" DataSourceID="SqlDataSource1" DataFieldID="Form_id"
    DataTextField="Form_Name" Style="z-index: 1; top: 73px; left: 0px;" EnableRoundedCorners="true"
    EnableShadows="true" DataFieldParentID="parent_id" EnableTextHTMLEncoding="true"
    Skin="Black" DataNavigateUrlField="url" DataValueField="Form_id">
    <ExpandAnimation Type="InOutSine" />
</telerik:RadMenu>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:HRMSConnectionString %>" 
    SelectCommand="P_Load_Form" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="Role_Id" SessionField="roleId" 
            Type="Decimal" />
            <asp:Parameter DefaultValue="1" Name="Type" Type="Decimal" />
    </SelectParameters>
</asp:SqlDataSource>

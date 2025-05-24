<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Menu_Accordian.ascx.vb" Inherits="Controls_Menu_Accordian" %>
<%--<telerik:RadMenu runat="server" ID="RadMenu1" DataSourceID="SqlDataSource1" DataFieldID="Form_id"
    DataTextField="Form_Name" Style="z-index: 1; top: 0px; left: 0px;" width="100%" EnableRoundedCorners="true"
    EnableShadows="true" DataFieldParentID="parent_id" EnableTextHTMLEncoding="true"
    DataNavigateUrlField="url" DataValueField="Form_id" Flow="Vertical" SkinID="Glow" >
    
    <ExpandAnimation Type="InOutSine" Duration="200" />
</telerik:RadMenu>
--%>
<asp:Repeater ID="rpMenu" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <asp:HiddenField ID="hfformid" runat="server" Value='<%#Eval("Form_id") %>' />
        <li><a href='<%#Eval("URL") %>'><i class='<%# Eval("Image")%>'>
            <div class="icon-bg bg-grey">
            </div>
        </i><span class="menu-title">
            <%# Eval("Alias")%></span><%--<span class="fa arrow"></span>--%></a>
            <asp:Repeater ID="rpMenuSec" runat="server" OnItemDataBound="rpMenuSec_ItemDataBound">
                <HeaderTemplate>
                    <ul class="nav nav-second-level">
                    
                </HeaderTemplate>
                <ItemTemplate>
                <asp:HiddenField ID="hfSecformid" runat="server" Value='<%#Eval("Form_id") %>' />
                    <li><a href='<%#Eval("URL") %>'><i class="fa fa-angle-right"></i><span class="submenu-title">
                        <%# Eval("Alias")%></span></a>
                        <asp:Repeater ID="rpMenuThird" runat="server">
                            <HeaderTemplate>
                                <ul class="nav nav-third-level">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><a href='<%#Eval("URL") %>'><i class="fa fa-angle-right"></i><span class="submenu-title">
                                    <%# Eval("Alias")%></span></a> </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </li>
    </ItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>
<%--EnableEmbeddedSkins="false"--%>
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HRMSConnectionString %>" SelectCommand="P_Load_Form"
    SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="Role_Id" SessionField="roleId" Type="Decimal" />
        <asp:Parameter DefaultValue="0" Name="Type" Type="Decimal" />
    </SelectParameters>
</asp:SqlDataSource>--%>

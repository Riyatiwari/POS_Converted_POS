<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Show_Product_ForIngredient.aspx.vb" Inherits="Show_Product_ForIngredient" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Product List For Ingredients</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                     
                        <br />
                        <div class="row" id="divPGroup" runat="server">
                            <div class="col-lg-12 " style="overflow-y: auto; height: 550px;">
                                <div class="table-responsive">
                                    <telerik:RadGrid ID="rdcopyProduct" AutoGenerateColumns="False" AllowPaging="False"
                                        AllowSorting="True" runat="server" CellSpacing="0"
                                        GridLines="None" AllowMultiRowSelection="true" AllowFilteringByColumn="True"
                                        Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true" GroupingEnabled="true" MasterTableView-GroupsDefaultExpanded="false" RetainExpandStateOnRebind="true">
                                        <ClientSettings Selecting-AllowRowSelect="true"  AllowDragToGroup="true" AllowGroupExpandCollapse="False">
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="600px"/>--%>
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <SortingSettings EnableSkinSortStyles="false" />

                                        <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="product_id" EnableHeaderContextMenu="true"
                                            TableLayout="Fixed"   HierarchyDefaultExpanded="False">
                                             <GroupByExpressions>
                                                                <telerik:GridGroupByExpression>
                                                                    <SelectFields>
                                                                        <telerik:GridGroupByField FieldAlias="Group" FieldName="Cat_Name"></telerik:GridGroupByField>
                                                                    </SelectFields>
                                                                    <GroupByFields>
                                                                        <telerik:GridGroupByField FieldName="Cat_Name"></telerik:GridGroupByField>
                                                                    </GroupByFields>
                                                                </telerik:GridGroupByExpression>
                                                            </GroupByExpressions>
                                            <Columns>
                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                               <%--  <telerik:GridBoundColumn DataField="Cat_Name" HeaderText="Product Group" SortExpression="Cat_Name"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">
                                                </telerik:GridBoundColumn>--%>

                                                <telerik:GridBoundColumn DataField="pro_Name" HeaderText="Product Name" SortExpression="name"
                                                    ReadOnly="True" FilterControlAltText="Filter product name column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>
                                               
                                                <telerik:GridBoundColumn DataField="base_unit" HeaderText="Base Unit" SortExpression="base_unit"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                        <div style="padding-top: 10px; text-align: center;">
                            <asp:Button ID="btnSave" class="btn btn-primary"  runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to change product group" />
                        </div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>

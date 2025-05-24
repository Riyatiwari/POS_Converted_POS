<%@ Page Title="Copy Product List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Copy_Product_List.aspx.vb" Inherits="Copy_Product_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   Copy Product List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
              <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li><i class="fa fa-right"></i>&nbsp;<a href="Product_List.aspx">Product List</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Copy Product List</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%--<div class="col-lg-12" id="divPGroup" runat="server">--%>
    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Copy Product List</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                              <%--  <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Product</asp:LinkButton>--%>
                                <asp:LinkButton ID="lnkCopy" runat="server" class="btn btn-primary"><i class="fa fa-forward"></i>&nbsp;Next</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divPGroup" runat="server">
                            <div class="col-lg-8">
                                <div class="table-responsive">
                                    <telerik:RadGrid ID="rdcopyProduct" AutoGenerateColumns="False" AllowPaging="True"
                                        AllowSorting="True" runat="server" CellSpacing="0" PageSize="100"
                                        GridLines="None" AllowMultiRowSelection="false" AllowFilteringByColumn="True"
                                        Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true"  GroupingEnabled="true"  MasterTableView-GroupsDefaultExpanded="false" RetainExpandStateOnRebind="true" >
                                          <ClientSettings Selecting-AllowRowSelect="true">
                                                    </ClientSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <SortingSettings EnableSkinSortStyles="false" />
                                         <ClientSettings AllowDragToGroup="true"  AllowGroupExpandCollapse ="False"></ClientSettings>
                                        <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="product_id" EnableHeaderContextMenu="true"
                                            TableLayout="Fixed"   HierarchyDefaultExpanded="False">
                                            <GroupByExpressions>
                                                <telerik:GridGroupByExpression>
                                                    <SelectFields>
                                                        <telerik:GridGroupByField FieldAlias="Group" FieldName="category_name"></telerik:GridGroupByField>
                                                    </SelectFields>
                                                    <GroupByFields>
                                                        <telerik:GridGroupByField FieldName="category_name"></telerik:GridGroupByField>
                                                    </GroupByFields>
                                                </telerik:GridGroupByExpression>
                                            </GroupByExpressions>
                                            <Columns>
                                                   <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn"/>
                                                   <%-- <telerik:GridBoundColumn DataField="code" HeaderText="code" SortExpression="code"
                                                    ReadOnly="True" FilterControlAltText="Filter Alpha_Code column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="code"
                                                    AutoPostBackOnFilter="false" HeaderStyle-Width="10%">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn DataField="name" HeaderText=" Name" SortExpression="name"
                                                    ReadOnly="True" FilterControlAltText="Filter product name column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name" 
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>
                                                 
                                                
                                              <%--  <telerik:GridBoundColumn DataField="category_name" HeaderText="Product Group" SortExpression="category_name"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">

                                                    <FilterTemplate>
                                                        <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="ddlCategory" AllowCustomText="true"
                                                            Filter="StartsWith" runat="server" Width="100%" AppendDataBoundItems="true" Skin="MetroTouch">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="All" Width="100%"/>
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>--%>

                                               <%-- <telerik:GridBoundColumn DataField="tax_name" HeaderText="Tax" SortExpression="tax_name"
                                                    ReadOnly="True" FilterControlAltText="Filter tax column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="Tax"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn DataField="Department" HeaderText=" Department" SortExpression="Department"
                                                    ReadOnly="True" FilterControlAltText="Filter department column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="Department" 
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="active" HeaderText="Active" SortExpression="active"
                                                    ReadOnly="True" FilterControlAltText="Filter active column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="active"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="Action" AllowFiltering="true"
                                                    ShowSortIcon="false">
                                                  <%--  <ItemTemplate>
                                                        <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("product_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("product_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("product_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                    </ItemTemplate>--%>
                                                    <FilterTemplate>
                                                        <asp:LinkButton ID="IbSearch" runat="server" CausesValidation="False" ImageUrl="#"
                                                            CssClass="btn btn-xs btn-success filter-submit" ToolTip="Search" CommandName="Search"><i class="fa fa-search"></i>&nbsp;Search</asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ImageUrl="#"
                                                            CssClass="btn btn-xs btn-info filter-cancel" ToolTip="Reset" CommandName="Clear"><i class="fa fa-times"></i>&nbsp;Reset</asp:LinkButton>
                                                    </FilterTemplate>

                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>

                            <div class="col-lg-4">
                                <div class="table-responsive">
                                            <telerik:RadGrid ID="rdMachineDetail" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                                AllowFilteringByColumn="false" SkinID="RadSkinManager1" AllowMultiRowSelection="true"
                                                Skin="MetroTouch">
                                                <ExportSettings FileName="Payment" IgnorePaging="false" ExportOnlyData="true">
                                                </ExportSettings>
                                                <ClientSettings Selecting-AllowRowSelect="true">
                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="300px" />
                                                </ClientSettings>
                                                <PagerStyle Mode="NextPrevAndNumeric" />
                                                <GroupingSettings CaseSensitive="false" />
                                                <SortingSettings EnableSkinSortStyles="false" />
                                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="Location_id" AllowMultiColumnSorting="true" TabIndex="0">
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Location Name" SortExpression="name"
                                                    ReadOnly="True" FilterControlAltText="Filter product name column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>
                                                      <%--  <telerik:GridTemplateColumn HeaderText="Location" UniqueName="Location">
                                                            <HeaderStyle Width="30%" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_Machine" runat="server" Value='<%# Eval("Location_id")%>' />
                                                                <asp:Label ID="lblMachine" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdmachine_id" runat="server" Value='<%# Eval("Location_id")%>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>--%>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>

                                        </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>


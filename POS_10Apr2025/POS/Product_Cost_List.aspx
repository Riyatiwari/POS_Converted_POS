<%@ Page Language="VB" Title="Change Product Group List" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_Cost_List.aspx.vb" Inherits="Product_Cost_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      Stock Cost List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li><i class="fa fa-right"></i>&nbsp;<a href="Stock_Cost.aspx">Stock Cost</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Stock Cost List</li>
        </ol>
    </div>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Stock Cost List</div>
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
                                        <ClientSettings Selecting-AllowRowSelect="true">
                                        </ClientSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <SortingSettings EnableSkinSortStyles="false" />

                                        <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="product_id" EnableHeaderContextMenu="true"
                                            TableLayout="Fixed" HierarchyDefaultExpanded="False">
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
                                                <telerik:GridBoundColumn DataField="name" HeaderText=" Name" SortExpression="name"
                                                    ReadOnly="True" FilterControlAltText="Filter product name column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn DataField="For_Date" HeaderText="Effective Date" SortExpression="For_Date"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Cost" HeaderText="Cost" SortExpression="Cost"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Department" HeaderText="Department" SortExpression="category_name"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">

                                                    <FilterTemplate>
                                                        <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="ddlDepartment" AllowCustomText="true"
                                                            Filter="StartsWith" runat="server" Width="100%" AppendDataBoundItems="true" Skin="MetroTouch">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="All" Width="100%" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Course_Name" HeaderText="Course" SortExpression="category_name"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">

                                                    <FilterTemplate>
                                                        <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="ddlCourse" AllowCustomText="true"
                                                            Filter="StartsWith" runat="server" Width="100%" AppendDataBoundItems="true" Skin="MetroTouch">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="All" Width="100%" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Unit" HeaderText="Unit" SortExpression="category_name"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">

                                                    <FilterTemplate>
                                                        <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="ddlUnit" AllowCustomText="true"
                                                            Filter="StartsWith" runat="server" Width="100%" AppendDataBoundItems="true" Skin="MetroTouch">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="All" Width="100%" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Base" HeaderText="Base" SortExpression="Base"
                                                    ReadOnly="True" FilterControlAltText="Filter Base column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="Base"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="Action" AllowFiltering="true"
                                                    ShowSortIcon="false">
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
                        </div>

                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>

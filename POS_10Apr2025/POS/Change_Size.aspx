<%@ Page Language="VB" Title="Change Product Group List" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Change_Size.aspx.vb" Inherits="Change_Size" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Change Product Group List

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li><i class="fa fa-right"></i>&nbsp;<a href="Product_List.aspx">Product List</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Change Product Group List</li>
        </ol>
    </div>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Change Product Size </div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="col-lg-6 ">
                                    Tax
                                <telerik:RadComboBox ID="radTax" runat="server" Width="60%">
                                </telerik:RadComboBox>
                                </div>

                            </div>
                            <br />

                        </div>
                        <br />
                        <div>Filters : </div>
                        <div class="col-lg-12 " style="padding-top: 20px;">
                            <div class="col-lg-6 ">
                                Group Cateogry
                                   <telerik:RadComboBox ID="rdGroupCateogry" runat="server" Width="60%" Style="margin-left: 60px;" AutoPostBack="true">
                                   </telerik:RadComboBox>
                            </div>
                            <div class="col-lg-6 ">
                                Product Group
                                    <telerik:RadComboBox ID="rdProductGroup" runat="server" Width="60%" Style="margin-left: 60px;" AutoPostBack="true">
                                    </telerik:RadComboBox>
                            </div>
                        </div>
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
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="category_id" EnableHeaderContextMenu="true"
                                            TableLayout="Fixed" HierarchyDefaultExpanded="False">

                                            <Columns>
                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                                <telerik:GridBoundColumn DataField="name" HeaderText="Product Group" SortExpression="name"
                                                    ReadOnly="True" FilterControlAltText="Filter Group name column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>



                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                        <div style="padding-top: 10px; text-align: center;">
                            <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to change product group" />
                        </div>
                    </div>
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>

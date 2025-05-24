<%@ Page Language="VB" Title="Change Product Group List" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Change_Product_Group_List.aspx.vb" Inherits="Change_Product_Group_List" %>

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
                <div class="panel-heading">Change Product Group List</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="col-lg-6 ">
                                    Product Group
                                <telerik:RadComboBox ID="radCategory" runat="server" Width="60%">
                                </telerik:RadComboBox>
                                </div>
                                <div class="col-lg-6 ">
                                    Printer
                                 <telerik:RadComboBox ID="radPrinter" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="60%"
                                     Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                 </telerik:RadComboBox>
                                </div>
                            </div>
                            <br />
                            <div class="col-lg-12 " style="padding-top: 20px;">
                                <div class="col-lg-6 ">
                                    Department
                                   <telerik:RadComboBox ID="radDept" runat="server" Width="60%" Style="margin-left: 10px;">
                                   </telerik:RadComboBox>
                                </div>

                                <div class="col-lg-6 ">
                                    Course
                                   <telerik:RadComboBox ID="radCourse" runat="server" Width="60%">
                                   </telerik:RadComboBox>
                                </div>
                            </div>
                            <br />
                            <div class="col-lg-12 " style="padding-top: 20px;">
                                <div class="col-lg-6 ">
                                    Unit
                                   <telerik:RadComboBox ID="radUnit" runat="server" Width="60%" Style="margin-left: 60px;">
                                   </telerik:RadComboBox>
                                </div>
                                <div class="col-lg-6 ">
                                    Base
                                   <telerik:RadNumericTextBox MinValue="0" MaxValue="999999999" ID="txtBase" Skin="" CssClass="form-control"
                                       runat="server" Width="60%" MaxLength="8" Style="margin-left: 10px;">
                                       <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />
                                   </telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-lg-12">
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Is Condiment  &nbsp;
                                            <asp:CheckBox ID="chkIsCondiment" runat="server" Checked="false" />
                                    </label>


                                    &nbsp;&nbsp;&nbsp;
                                    <label>
                                        Is Stock  &nbsp;
                                            <asp:CheckBox ID="chkIsStock" runat="server" Checked="false" />
                                    </label>

                                    &nbsp;&nbsp;&nbsp;
                                    <label>
                                        Is Web Available  &nbsp;
                                            <asp:CheckBox ID="chkIsAvailable" runat="server" Checked="false" />
                                    </label>

                                     &nbsp;&nbsp;&nbsp;
                                    <label>
                                        Is Unflag Printer  &nbsp;
                                            <asp:CheckBox ID="chkIsUnflagPrinter" runat="server" Checked="false" />
                                    </label>
                                </div>

                            </div>
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
                      <div class="clearfix"></div>
                        <div class="row" id="divPGroup" runat="server">
                            <br />
                            <div class="col-lg-12 " style="overflow-y: auto; height: 550px;">
                                <div class="table-responsive">
                                    <telerik:RadGrid ID="rdcopyProduct" AutoGenerateColumns="False" AllowPaging="False"
                                        AllowSorting="True" runat="server" CellSpacing="0"
                                        GridLines="None" AllowMultiRowSelection="true" AllowFilteringByColumn="True"
                                        Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true" GroupingEnabled="true" MasterTableView-GroupsDefaultExpanded="false" RetainExpandStateOnRebind="true">
                                        <ClientSettings Selecting-AllowRowSelect="true">
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="600px"/>--%>
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
                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" HeaderStyle-Width="10%" />
                                                <telerik:GridBoundColumn DataField="name" HeaderText=" Name" SortExpression="name"
                                                    ReadOnly="True" FilterControlAltText="Filter product name column" ShowFilterIcon="false"
                                                    ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                    AutoPostBackOnFilter="false">
                                                </telerik:GridBoundColumn>

                                                <%--   <telerik:GridBoundColumn DataField="category_name" HeaderText="Product Group" SortExpression="category_name"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">

                                                    <FilterTemplate>
                                                        <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="ddlCategory" AllowCustomText="true"
                                                            Filter="StartsWith" runat="server" Width="100%" AppendDataBoundItems="true" Skin="MetroTouch">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="All" Width="100%" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </FilterTemplate>
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn DataField="Printer_Name" HeaderText="Printer" SortExpression="category_name"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                    ShowSortIcon="false" ReadOnly="true">

                                                    <FilterTemplate>
                                                        <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="ddlPrinter" AllowCustomText="true"
                                                            Filter="StartsWith" runat="server" Width="100%" AppendDataBoundItems="true" Skin="MetroTouch">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="All" Width="100%" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </FilterTemplate>
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

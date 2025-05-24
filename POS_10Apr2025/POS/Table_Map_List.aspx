<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Table_Map_List.aspx.vb" Inherits="Table_Map_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Table Map List
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Table Map List</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <br />
    <%--<div class="col-lg-12" id="divKeyMap" runat="server">--%>
    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Table Map List</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Table Map</asp:LinkButton>&nbsp;&nbsp;
                                 <%--<asp:LinkButton ID="lnkCopy" runat="server" class="btn btn-primary"><i class="fa fa-forward"></i>&nbsp;Copy</asp:LinkButton>--%>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divKeyMap" runat="server">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <telerik:RadGrid ID="rdKayMap" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                        PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                        PagerStyle-AlwaysVisible="true" Skin="MetroTouch">
                                        <ClientSettings Selecting-AllowRowSelect="true">
                                        </ClientSettings>
                                        <ExportSettings FileName="Table Map" IgnorePaging="false" ExportOnlyData="true">
                                        </ExportSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <SortingSettings EnableSkinSortStyles="false" />
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Table_map_id" AllowMultiColumnSorting="true">


                                            <Columns>
                                                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Display Name"
                                                    SortExpression="Display Name" DataField="Display_Name">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Table Map Size"
                                                    SortExpression="Tablemap_size" DataField="Tablemap_size">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdtable_map_id" runat="server" Value='<%#Eval("Table_map_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("Table_map_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("Table_map_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                    </ItemTemplate>
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


<%@ Page Title="Create Waitlist" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="Waitlist_BookingDetailTable.aspx.vb" Inherits="BookingEasy_Waitlist_BookingDetailTable" %>

<%@ Register Src="~/UserControl/Customer.ascx" TagName="Customer" TagPrefix="uc" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .col-md-3, .col-lg-9
        {
            padding: 0px;
        }
        .form-control
        {
            display: inline;
        }
        .custom
        {
            float: left;
            width: 270px;
        }
        .chkmrng
        {
            margin-right: 5px;
        }
        
        .box .box-title h5
        {
            display: inline-block;
            line-height: 20px;
            margin: 0;
            color: #404040;
            white-space: nowrap;
            margin-left: 3%;
        }
    </style>
        <script>
            function myFunction() {
                var txtsearch = document.getElementById("txtSearchClient").value;

                if (txtsearch == "") {
                    document.getElementById("lblSearchMessage").value = "";
                }
            }   
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa-cutlery"></i>Create Waiting
                    </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-table"></i>Booking Summary</h3>
                            <h5>
                                <strong>
                                    <asp:Literal ID="lblStoreName" Text="" runat="server" /></h5>
                            </strong>
                            <h5>
                                <strong>Date:</strong>
                                <asp:Literal ID="lblDate" Text="" runat="server" /></h5>
                            <h5>
                                <strong>No Of Covers:</strong>
                                <asp:Literal ID="lblNoOfCovers" Text="" runat="server" /></h5>
                            <%--   <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a><a href="#"
                                    data-action="close"><i class="fa fa-times"></i></a>
                            </div>--%>
                        </div>
                        <%--   <div class="box-content">
                           
                            
                            <br />
                            
                            <br />
                        </div>--%>
                    </div>
                </div>
            </div>
            <%--    <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-table"></i>Extra Service</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a><a href="#"
                                    data-action="close"><i class="fa fa-times"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div style="padding-top: 10px;">
                                <telerik:RadGrid ID="gvServices" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                    AllowSorting="false" HeaderStyle-HorizontalAlign="Center" GroupingEnabled="true"
                                    ShowGroupPanel="false">
                                    <MasterTableView DataKeyNames="ProductID" GroupsDefaultExpanded="false">
                                        <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldName="ParentName" FieldAlias="Type" />
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="ParentName" />
                                                </GroupByFields>
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Product Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Unit Price">
                                                <ItemTemplate>
                                                    <%= CurrencySymbol %>&nbsp;<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <telerik:RadDropDownList ID="ddlQuantity" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="1" Text="1" Selected="true"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="2" Text="2"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="3" Text="3"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="4" Text="4"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="5" Text="5"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="6" Text="6"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="7" Text="7"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="8" Text="8"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="9" Text="9"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="10" Text="10"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                    </ClientSettings>
                                    <GroupingSettings />
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="row" id="pnlSearchClient" runat="server">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-table"></i>Search Client</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a><a href="#"
                                    data-action="close"><i class="fa fa-times"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="col-lg-12 controls">
                                <span class="help-inline">Enter
                                    <asp:Label ID="lblSearchOption" runat="server"></asp:Label></span>
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearchClient">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtSearchClient" runat="server" class="form-control" placeholder="Search here..."></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button ID="btnSearchClient" runat="server" Text="Search" CssClass="btn" ValidationGroup="SearchClient" OnClientClick="myFunction();"/>
                                        </span>
                                    </div>
                                </asp:Panel>
                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtSearchClient"
                                    ForeColor="Red" ErrorMessage="Please enter search text" Display="Dynamic" ValidationGroup="SearchClient"
                                    CssClass="rfv">
                                </asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblSearchMessage" runat="server"></asp:Label>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div id="divSearchResult" runat="server" class="col-lg-12 controls" style="padding-top: 10px;"
                                visible="False">
                                Search Result:
                                <telerik:RadGrid ID="gvSearchClient" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                    PageSize="10" Skin="Office2010Blue">
                                    <MasterTableView DataKeyNames="accountid">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="firstname" HeaderText="First Name" />
                                            <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" />
                                            <telerik:GridBoundColumn DataField="email1st" HeaderText="Email" />
                                            <telerik:GridBoundColumn DataField="Mobile" HeaderText="Mobile" />
                                        </Columns>
                                    </MasterTableView>
                                    <PagerStyle PageSizeControlType="None" />
                                    <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-table"></i>Customer Detail</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a><a href="#"
                                    data-action="close"><i class="fa fa-times"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div id="divMessageBox" runat="server" visible="false">
                                            <asp:Label ID="lbExistingCust" runat="server" Text=""></asp:Label>
                                        </div>
                                        <telerik:RadGrid ID="gvExistingCustomer" runat="server" AutoGenerateColumns="False"
                                            AllowPaging="true" PageSize="10" Skin="Office2010Blue" Visible="false">
                                            <MasterTableView DataKeyNames="accountid">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="firstname" HeaderText="First Name" />
                                                    <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" />
                                                    <telerik:GridBoundColumn DataField="email1st" HeaderText="Email" />
                                                    <telerik:GridBoundColumn DataField="Mobile" HeaderText="Mobile" />
                                                </Columns>
                                            </MasterTableView>
                                            <PagerStyle PageSizeControlType="None" />
                                            <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnAccountID" runat="server" Value="0" />
                                <uc:Customer ID="ucCustomer1" runat="server" />
                                <div class="form-group">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <label class="col-xs-3 col-lg-2 control-label">
                                                Comment:</label>
                                            <div class="col-sm-9 col-lg-10 controls">
                                                <asp:TextBox ID="txtComment" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                           <%-- <label class="col-xs-3 col-lg-2 control-label">
                                                Extra Service:</label>--%>
                                            <div class="col-sm-9 col-lg-10 controls">
                                                <telerik:RadGrid ID="gvServices" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                    AllowSorting="false" HeaderStyle-HorizontalAlign="Center" GroupingEnabled="true"
                                                    ShowGroupPanel="false" Visible="false" >
                                                    <MasterTableView DataKeyNames="ProductID" GroupsDefaultExpanded="false">
                                                        <GroupByExpressions>
                                                            <telerik:GridGroupByExpression>
                                                                <SelectFields>
                                                                    <telerik:GridGroupByField FieldName="ParentName" FieldAlias="Type" />
                                                                </SelectFields>
                                                                <GroupByFields>
                                                                    <telerik:GridGroupByField FieldName="ParentName" />
                                                                </GroupByFields>
                                                            </telerik:GridGroupByExpression>
                                                        </GroupByExpressions>
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Product Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Unit Price">
                                                                <ItemTemplate>
                                                                    <%= CurrencySymbol %>&nbsp;<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <telerik:RadDropDownList ID="ddlQuantity" Skin="MetroTouch" runat="server" Width="200px"
                                                                        DropDownHeight="200px">
                                                                        <Items>
                                                                            <telerik:DropDownListItem Value="1" Text="1" Selected="true"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="2" Text="2"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="3" Text="3"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="4" Text="4"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="5" Text="5"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="6" Text="6"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="7" Text="7"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="8" Text="8"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="9" Text="9"></telerik:DropDownListItem>
                                                                            <telerik:DropDownListItem Value="10" Text="10"></telerik:DropDownListItem>
                                                                        </Items>
                                                                    </telerik:RadDropDownList>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                    </ClientSettings>
                                                    <GroupingSettings />
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 col-lg-4">
                                    </div>
                                    <div class="col-sm-4 col-lg-4 controls" style="text-align: center;">
                                        <asp:Button ID="btnConfitmBooking" runat="server" Text="Create WaitList" CssClass="btn btn-success" />
                                    </div>
                                    <div class="col-sm-4 col-lg-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="custom_progress">
                <div id="overlay_load">
                </div>
                <div id="loading">
                    <img src="Images/ajax-loader.gif" alt="" />
                    <br />
                    Please Wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>

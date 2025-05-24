<%@ Page Title="General Setup" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="true" CodeFile="General_Setup.aspx.vb" Inherits="BookingEasy_Settings2"
    EnableEventValidation="false" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //        function ClientValidationFunction(sender, args) {
        //            debugger;
        //            var ele = sender.controltovalidate;
        //            var start = document.getElementById(ele.replace('EndTime', 'StartTime')).value.substring(11, 13);
        //            var end = document.getElementById(ele.replace('StartTime', 'EndTime')).value.substring(11, 13);

        //            if (start.length > 0 && end.length > 0) {

        //                start = Number(start);
        //                end = Number(end);

        //                if (start > end) {
        //                    args.IsValid = false;
        //                }
        //                else {
        //                    document.getElementById("ctl00_ContentPlaceHolder1_ttpStartTime1")).isvalid = 
        //                    args.IsValid = true;
        //                }
        //            }
        //            //2014-08-04-15-00-00
        //        } 
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /** Multiple rows and columns */
        .multipleRowsColumns .rcbItem, .multipleRowsColumns .rcbHovered
        {
            float: left;
            margin: 0 1px;
            min-height: 13px;
            overflow: hidden;
            padding: 2px 19px 2px 6px;
            width: 150px;
        }
        .rcbScroll
        {
            height: 200PX !important;
        }
        .rcbWidth
        {
            height: 200PX !important;
        }
    </style>
    <script language="Javascript">
       <!--
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode < 48 || charCode > 57)
                return false;
            return true;
        }
       //-->
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa-cog"></i>General Setup
                    </h1>
                    <h4>
                        Settings for schedule and hotels.
                    </h4>
                </div>
            </div>
            <div id="divLogin" runat="server">
                <div class="box">
                    <div class="box-title">
                        <h3>
                            <h3>
                                <i class="fa fa-table"></i>Access Credential</h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                            <h3>
                            </h3>
                        </h3>
                    </div>
                    <div class="box-content">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="controls">
                                        <asp:Label ID="lblInvalidCredential" runat="server" ForeColor="Red" Visible="false"
                                            Text="Invalid Username or Password"></asp:Label>
                                    </div>
                                    <div class="controls">
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtUserName" runat="server" placeholder="Username" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUserName" ControlToValidate="txtUserName" runat="server"
                                                ErrorMessage="Username requied." ForeColor="red" ValidationGroup="login"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" CssClass="form-control"
                                                TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" runat="server"
                                                ErrorMessage="Password requied." ForeColor="red" ValidationGroup="login"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnSignIn" runat="server" Text="Sign In" CssClass="btn btn-primary form-control"
                                                ValidationGroup="login" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divContent" runat="server" visible="false">
                <div class="row" style="text-align: center; margin-bottom: 10px;">
                    <asp:Button ID="btnCreateTable" runat="server" Text="Create Tables" CssClass="btn btn-primary" />
                    <asp:Button ID="btnRetun" runat="server" Text="Return to Start" CssClass="btn btn-primary" />
                    <asp:LinkButton ID="lnkBtnMultiVenue" CssClass="btn btn-primary" runat="server" PostBackUrl="~/Booking/MultiVenue.aspx">MultiVenue</asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnBookingSynchronize" CssClass="btn btn-primary" runat="server"
                        PostBackUrl="~/Booking/Booking_Synchronize.aspx">Booking Synchronize</asp:LinkButton>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Tabs
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label">
                                                    Tab Name :</label>
                                                <asp:HiddenField ID="hdnTabId" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtTabName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqTabName" runat="server" ControlToValidate="txtTabName"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TabSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label">
                                                    Tab Type :</label>
                                                <div class="col-sm-9 col-lg-8 controls" style="margin-top:1%;margin-left:-20px;">
                                                    <asp:RadioButton ID="rdoRestaurent" runat="server" Checked="true" GroupName="TabGroup"
                                                        Text="" />&nbsp;Schedule
                                                    &nbsp;&nbsp;<asp:RadioButton ID="rdoHotel" runat="server" GroupName="TabGroup" Text="" />&nbsp;Hotel
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:Button ID="btnSaveTabSetting" runat="server" Text="Save" ValidationGroup="TabSetting"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnCancelTabSetting" runat="server" Text="Cancel" CssClass="btn"
                                                        CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <telerik:RadGrid ID="gvTabs" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                AllowSorting="false">
                                                <MasterTableView DataKeyNames="TabId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="TabName" HeaderText="Tab Name" SortExpression="TabName"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="TabTypeName" HeaderText="Tab Type" SortExpression="TabTypeName"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditTabName"
                                                            Text="Edit" HeaderText="Edit" HeaderStyle-Width="100px" ImageUrl="Images/Icons/edit.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteTabName"
                                                            Text="Delete" HeaderText="Delete" HeaderStyle-Width="100px" ImageUrl="Images/Icons/delete.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Venue Mapping
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="divVenueMap" runat="server" visible="False">
                                                <asp:Label ID="lblVenueMapMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label">
                                                    Venue Name :</label>
                                                <asp:HiddenField ID="hdnBookingVenueId" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtVenueName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqVenueName" runat="server" ControlToValidate="txtVenueName"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="VenueMap"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="display:none;">
                                                    Other Venue :</label>
                                                <div class="col-sm-9 col-lg-8 controls" style="display:none;">
                                                    <telerik:RadDropDownList ID="ddlOtherVenue" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" DefaultMessage="- Select -">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="reqOtherVenue" runat="server" ControlToValidate="ddlOtherVenue"
                                                        ForeColor="Red" ErrorMessage="Please select Venue." Display="Dynamic" ValidationGroup="VenueMap"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:Button ID="btnSaveVenueMap" Visible="false" runat="server" Text="Save" ValidationGroup="VenueMap"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnCancelVenueName" Visible="false" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <telerik:RadGrid ID="gvVenueMap" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                AllowSorting="false">
                                                <MasterTableView DataKeyNames="BookingVenueId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="VenueName" HeaderText="Venue" SortExpression="VenueName"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Other Venue" SortExpression="Name"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" Visible="false" />
                                                        <telerik:GridButtonColumn Visible="false" ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditVenueMap"
                                                            Text="Edit" HeaderText="Edit" HeaderStyle-Width="100px" ImageUrl="Images/Icons/edit.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridButtonColumn Visible="false" ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteVenueMap"
                                                            Text="Delete" HeaderText="Delete" HeaderStyle-Width="100px" ImageUrl="Images/Icons/delete.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Location Mapping
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="divStoreMapMsg" runat="server" visible="False">
                                                <asp:Label ID="lblStoreMapMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-5 col-md-5 col-sm-5 control-label">
                                                    Location Name :</label>
                                                <asp:HiddenField ID="hdnOurStoreId" runat="server" Value="0" />
                                                <div class="col-sm-7 col-lg-6 col-md-7 controls">
                                                    <asp:TextBox ID="txtStoreName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqStoreName" runat="server" ControlToValidate="txtStoreName"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="StoreMap"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-5 col-md-5 col-sm-5 control-label">
                                                    Venue Name :</label>
                                                <div class="col-sm-7 col-lg-7 col-md-7 controls">
                                                    <telerik:RadDropDownList ID="ddlOurVenue" Skin="MetroTouch" runat="server" Width="180px"
                                                        DropDownHeight="200px" DefaultMessage="- Select -" AutoPostBack="true">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="reqOurVenue" runat="server" ControlToValidate="ddlOurVenue"
                                                        ForeColor="Red" ErrorMessage="Please select Venue." Display="Dynamic" ValidationGroup="StoreMap"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-5 col-md-5 col-sm-5 control-label" style="display:none">
                                                    Other Store :</label>
                                                <div class="col-sm-7 col-lg-6 col-md-7 controls"  style="display:none">
                                                    <telerik:RadDropDownList ID="ddlOtherStore" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" DefaultMessage="- Select -">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="reqOtherStore" runat="server" ControlToValidate="ddlOtherStore"
                                                        ForeColor="Red" ErrorMessage="Please select Store." Display="Dynamic" ValidationGroup="StoreMap"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-1 col-lg-1 col-md-1 col-sm-1 control-label">
                                                </label>
                                                <div class="col-sm-10 col-lg-10 col-md-10 controls">
                                                    <asp:Button ID="btnStoreMapSave" Visible="false" runat="server" Text="Save" ValidationGroup="StoreMap"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnStoreMapCancel" runat="server" Visible="false" Text="Cancel" CssClass="btn" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <telerik:RadGrid ID="gvStoreMap" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                AllowSorting="false">
                                                <MasterTableView DataKeyNames="OurStoreId">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="StoreName" HeaderText="Location" SortExpression="StoreName"
                                                            HeaderStyle-HorizontalAlign="Center" />
                                                        <telerik:GridBoundColumn DataField="VenueName" HeaderText="Venue" SortExpression="VenueName"
                                                            HeaderStyle-HorizontalAlign="Center" />
                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Other Store" SortExpression="Name"
                                                            HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                                        <telerik:GridButtonColumn Visible="false" ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditStoreMap"
                                                            Text="Edit" HeaderText="Edit" HeaderStyle-Width="100px" ImageUrl="Images/Icons/edit.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridButtonColumn Visible="false" ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteStoreMap"
                                                            Text="Delete" HeaderText="Delete" HeaderStyle-Width="100px" ImageUrl="Images/Icons/delete.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Tab Name Setting
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <asp:Label ID="lblTabNameSave" runat="server" ForeColor="Green" Visible="false" Text="Tab Name Save Successfully"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-3 control-label">
                                                    Search:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtSearchTab" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqSearchTab" runat="server" ControlToValidate="txtSearchTab"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TabName"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-3 control-label">
                                                    Hotel:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtHotelTab" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqHotalTab" runat="server" ControlToValidate="txtHotelTab"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TabName"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Schedule:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtResTab" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqResTab" runat="server" ControlToValidate="txtResTab"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TabName"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:Button ID="btnSaveTabName" runat="server" Text="Save" ValidationGroup="TabName"
                                                        CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Currency Setting
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <asp:Label ID="lblCurrencyMessage" runat="server" ForeColor="Green" Visible="false"
                                            Text="Currency Setting Save Successfully"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label">
                                                    Symbol:</label>
                                                <div class="col-sm-8 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="drpCurrencySymbol" Skin="MetroTouch" runat="server"
                                                        Width="200px" DropDownHeight="200px" DefaultMessage="- Select -">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="GBP (£)" Value="£" Selected="true"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="USD ($)" Value="$"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="Euro (€)" Value="€"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:Button ID="btnSaveCurrSymbol" runat="server" Text="Save" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Customer Search
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblSaveField" runat="server" ForeColor="Green" Visible="false" Text="Field Save Successfully"></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label">
                                                    Search by:</label>
                                                <div class="col-sm-8 col-lg-8 controls">
                                                    <asp:HiddenField ID="hdnDefaultField" runat="server" />
                                                    <telerik:RadDropDownList ID="ddlDefaultField" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" DefaultMessage="- Select -">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="Email" Value="0" Selected="true"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="Mobile" Value="1"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="Both" Value="2"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:Button ID="btnSaveField" runat="server" Text="Save" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="custom_progress">
                <div id="overlay_load">
                </div>
                <div id="loading">
                    <img src="Images/ajax-loader.gif " alt="" />
                    <br />
                    Please Wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

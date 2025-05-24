<%@ Page Title="Location & Schedule Details" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="LocationScheduleDetails.aspx.vb" Inherits="BookingEasy_Settings2"
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
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa-cog"></i>Location & Schedule Details
                    </h1>
                    <h4>
                        Settings for Location and Schedule.
                    </h4>
                </div>
            </div>
            <div id="divContent" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Location Details
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="divMessageBox" runat="server" visible="False">
                                                <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Venue:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlVenu" Skin="MetroTouch" runat="server" Width="200px"
                                                        AutoPostBack="True" DropDownHeight="200px" DefaultMessage="- Select -">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVenu" runat="server" ControlToValidate="ddlVenu"
                                                        ForeColor="Red" ErrorMessage="Please select Venue." Display="Dynamic" ValidationGroup="Settings"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Location:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlStore" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" DefaultMessage="- Select -">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvStore" runat="server" ControlToValidate="ddlStore"
                                                        ForeColor="Red" ErrorMessage="Please select store." Display="Dynamic" ValidationGroup="Settings"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Sort:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlSort" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" DefaultMessage="- Select -">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="0" Text="0"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="1" Text="1"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="2" Text="2"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="3" Text="3"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="4" Text="4"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="5" Text="5"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="6" Text="6"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="7" Text="7"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="8" Text="8"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="9" Text="9"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="10" Text="10"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="11" Text="11"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="12" Text="12"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="13" Text="13"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="14" Text="14"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="15" Text="15"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="16" Text="16"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="17" Text="17"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="18" Text="18"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="19" Text="19"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="20" Text="20"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="21" Text="21"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="22" Text="22"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="23" Text="23"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="24" Text="24"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="25" Text="25"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="26" Text="26"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="27" Text="27"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="28" Text="28"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="29" Text="29"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="30" Text="30"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSort" runat="server" ControlToValidate="ddlSort"
                                                        ForeColor="Red" ErrorMessage="Please select sort." Display="Dynamic" ValidationGroup="Settings"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Booking Type:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlBookingType" Skin="MetroTouch" runat="server" Width="200px"
                                                        AutoPostBack="True" DefaultMessage="- Select -">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="1" Text="Room"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="2" Text="Schedule"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="3" Text="Both"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvBookingType" runat="server" ControlToValidate="ddlBookingType"
                                                        ForeColor="Red" ErrorMessage="Please select booking type." Display="Dynamic"
                                                        ValidationGroup="Settings" CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Guest A/c:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlGuestAc" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" DefaultMessage="- Select -">
                                                    </telerik:RadDropDownList>
                                                   <%-- <asp:RequiredFieldValidator ID="rfvGuestAc" runat="server" ControlToValidate="ddlGuestAc"
                                                        ForeColor="Red" ErrorMessage="Please select Guest A/c." Display="Dynamic" ValidationGroup="Settings"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Checked Guest A/c:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlCheckedGuestAc" Skin="MetroTouch" runat="server"
                                                        Width="200px" DropDownHeight="200px" DefaultMessage="- Select -">
                                                    </telerik:RadDropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvCheckedGuestAc" runat="server" ControlToValidate="ddlCheckedGuestAc" ForeColor="Red" ErrorMessage="Please select checked guest A/c." Display="Dynamic" ValidationGroup="Settings"
                                                    CssClass="rfv">
                                                </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                 
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Hotel Tab:
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="drpHotelTabs" runat="server" Skin="MetroTouch" Width="200px"
                                                        DropDownHeight="200px" Enabled="false">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="reqHotelTab" runat="server" ControlToValidate="drpHotelTabs"
                                                        InitialValue="0" ErrorMessage="*" ForeColor="Red" Display="Dynamic" Enabled="false"
                                                        ValidationGroup="Settings">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Schedule Tab:
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="drpRestaurantTab" runat="server" Skin="MetroTouch" Width="200px"
                                                        DropDownHeight="200px" Enabled="false">
                                                    </telerik:RadDropDownList>
                                                    <asp:RequiredFieldValidator ID="reqRestaurantTab" runat="server" ControlToValidate="drpRestaurantTab"
                                                        InitialValue="0" ErrorMessage="*" ForeColor="Red" Display="Dynamic" Enabled="false"
                                                        ValidationGroup="Settings">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div id="Div2" class="form-group" runat="server">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Redirect To Schedule:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlRedirectTo" Skin="MetroTouch" runat="server" Width="200px"
                                                        DefaultMessage="No Redirect" DropDownHeight="200px" AutoPostBack="True">
                                                    </telerik:RadDropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                       <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                    Total Number Of Rooms:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="drpNoOfRooms" Skin="MetroTouch" runat="server" Width="200px"
                                                        AutoPostBack="True" DropDownHeight="200px">
                                                        <Items>
                                                            <telerik:DropDownListItem Selected="True" Text="1" Value="1"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="2" Value="2"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="3" Value="3"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="4" Value="4"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="5" Value="5"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="6" Value="6"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="7" Value="7"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="8" Value="8"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="9" Value="9"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="10" Value="10"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="11" Value="11"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="12" Value="12"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="13" Value="13"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="14" Value="14"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="15" Value="15"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="16" Value="16"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="17" Value="17"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="18" Value="18"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="19" Value="19"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="20" Value="20"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="21" Value="21"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="22" Value="22"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="23" Value="23"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="24" Value="24"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="25" Value="25"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="26" Value="26"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="27" Value="27"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="28" Value="28"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="29" Value="29"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="30" Value="30"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4"  style="display:none">
                                            <div id="Div1" class="form-group" runat="server">
                                                <label class="col-xs-3 col-lg-4 control-label"   style="display:none">
                                                    Other Table Group:</label>
                                                <div class="col-sm-9 col-lg-8 controls"  style="display:none">
                                                    <telerik:RadDropDownList ID="ddlTableGroup" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="0" Value="0" Selected="true"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="1" Value="1"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="2" Value="2"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="3" Value="3"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="4" Value="4"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="5" Value="5"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="6" Value="6"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="7" Value="7"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvTableGroup" runat="server" ControlToValidate="ddlTableGroup"
                                                                    ForeColor="Red" ErrorMessage="Please select table group." Display="Dynamic" ValidationGroup="Settings"
                                                                    CssClass="rfv pnl">
                                                                </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4"  style="display:none">
                                            <div id="Div3" class="form-group" runat="server"   >
                                                <label class="col-xs-3 col-lg-4 control-label"  style="display:none">
                                                    Other Live Table Group:</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlLiveTableGroup" Skin="MetroTouch" runat="server"
                                                        Width="200px" DropDownHeight="200px">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="0" Value="0" Selected="true"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="1" Value="1"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="2" Value="2"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="3" Value="3"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="4" Value="4"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="5" Value="5"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="6" Value="6"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Text="7" Value="7"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvTableGroup" runat="server" ControlToValidate="ddlTableGroup"
                                                                    ForeColor="Red" ErrorMessage="Please select table group." Display="Dynamic" ValidationGroup="Settings"
                                                                    CssClass="rfv pnl">
                                                                </asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label">
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:CheckBox ID="chkShowOnWidget" runat="server" Text="Show On Widget" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="pnlTable" runat="server" visible="False">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    Schedule Details
                                                </h4>
                                            </div>
                                            <div class="panel-body">
                                                <%--Grid--%>
                                                <div class="row">
                                                    <div class="col-md-10">
                                                        <asp:Label ID="lblredirect" runat="server" Visible="false" ForeColor="Green"></asp:Label>
                                                        <asp:Label ID="lblDuplicateName" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnAddSchedule" runat="server" Text="Add Schedule" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                                <div class="row" style="overflow: auto">
                                                    <asp:GridView ID="gvSchedule" runat="server" AutoGenerateColumns="false" CssClass="table"
                                                        DataKeyNames="BookingScheduleID"  >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtName" runat="server" class="form-control tg" Text='<%# Eval("Name") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtName"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StartDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadDatePicker ID="dpStartDate" runat="server" EnableTyping="true" Width="100%"
                                                                        Skin="MetroTouch" SelectedDate='<%# Eval("StartDate") %>'>
                                                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                                                    </telerik:RadDatePicker>
                                                                    <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dpStartDate"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadDatePicker ID="dpEndDate" runat="server" EnableTyping="true" Width="100%"
                                                                        Skin="MetroTouch" SelectedDate='<%# Eval("EndDate") %>'>
                                                                        <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                                                    </telerik:RadDatePicker>
                                                                    <asp:RequiredFieldValidator ID="reqEndDate" runat="server" ControlToValidate="dpEndDate"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStartTime" runat="server" Text='<%# Eval("StartTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTimePicker ID="tpStartTime" runat="server" EnableTyping="true" Width="100%"
                                                                        Skin="MetroTouch" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                        TimeView-TimeFormat="HH:mm" SelectedTime='<%# Eval("StartTime") %>'>
                                                                    </telerik:RadTimePicker>
                                                                    <asp:RequiredFieldValidator ID="reqStartTime" runat="server" ControlToValidate="tpStartTime"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle Width="40px" />
                                                                <ControlStyle Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEndTime" runat="server" Text='<%# Eval("EndTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadTimePicker ID="tpEndTime" runat="server" EnableTyping="true" Width="100%"
                                                                        Skin="MetroTouch" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                                        TimeView-TimeFormat="HH:mm" SelectedTime='<%# Eval("EndTime") %>'>
                                                                    </telerik:RadTimePicker>
                                                                    <asp:RequiredFieldValidator ID="reqEndTime" runat="server" ControlToValidate="tpEndTime"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle Width="40px" />
                                                                <ControlStyle Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No Of Cover">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNoOfCover" runat="server" Text='<%# Eval("NumberOfCover") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtNoOfCover" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="70px" DataType="Integer"
                                                                        CssClass="form-control tg" Text='<%# Eval("NumberOfCover") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="reqNoOfCover" runat="server" ControlToValidate="txtNoOfCover"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="One Booking At A Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblone_booking_at_a_time" runat="server" Text='<%# Eval("one_booking_at_a_time") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:CheckBox ID="chkone_booking_at_a_time" runat="server" AutoPostBack="True" Checked="false" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Min Cover">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMinCover" runat="server" Text='<%# Eval("MinCovers") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadNumericTextBox ID="txtMinCover" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="70px" DataType="Integer"
                                                                        CssClass="form-control tg" Text='<%# Eval("MinCovers") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="reqMinCover" runat="server" ControlToValidate="txtMinCover"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Service Duration(in Minutes)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblServiceDuration" runat="server" Text='<%# Eval("ServiceDuration") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:TextBox ID="txtServiceDuration" runat="server" class="form-control tg" Text='<%# Eval("ServiceDuration") %>'></asp:TextBox>--%>
                                                                    <telerik:RadNumericTextBox ID="txtServiceDuration" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="50px" DataType="Integer"
                                                                        CssClass="form-control tg" Text='<%# Eval("ServiceDuration") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="reqServiceDuration" runat="server" ControlToValidate="txtServiceDuration"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Time Span(in Minutes)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTimeSpan" runat="server" Text='<%# Eval("TimeSpan") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:TextBox ID="txtTimeSpan" runat="server" class="form-control tg isNumeric" Text='<%# Eval("TimeSpan") %>'></asp:TextBox>--%>
                                                                    <telerik:RadNumericTextBox ID="txtTimeSpan" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="70px" DataType="Integer"
                                                                        CssClass="form-control tg" Text='<%# Eval("TimeSpan") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="reqTimeSpan" runat="server" ControlToValidate="txtTimeSpan"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Max Covers / Time Span">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMCPTimeSpan" runat="server" Text='<%# Eval("MCPTimeSpan") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:TextBox ID="txtTimeSpan" runat="server" class="form-control tg isNumeric" Text='<%# Eval("TimeSpan") %>'></asp:TextBox>--%>
                                                                    <telerik:RadNumericTextBox ID="txtMCPTimeSpan" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="70px" DataType="Integer"
                                                                        CssClass="form-control tg" Text='<%# Eval("MCPTimeSpan") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="reqMCPTimeSpan" runat="server" ControlToValidate="txtMCPTimeSpan"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Force Product selection">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblis_selectProduct" runat="server" Text='<%# Eval("is_selectProduct") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:CheckBox ID="chkis_selectProduct" runat="server" Checked="false" />
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadDropDownList ID="drpPaymentType" Skin="MetroTouch" runat="server" DefaultMessage="--Select--">
                                                                        <Items>
                                                                            <telerik:DropDownListItem Value="1" Text="Percentage Of Total Bill" />
                                                                            <telerik:DropDownListItem Value="2" Text="Open Deposit" />
                                                                            <telerik:DropDownListItem Value="3" Text="Deposit Amount" />
                                                                            <telerik:DropDownListItem Value="4" Text="Deposit Per Cover" />
                                                                        </Items>
                                                                    </telerik:RadDropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqPaymentType" runat="server" ControlToValidate="drpPaymentType"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic"
                                                                        InitialValue="">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Deposit %">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPercentage" runat="server" Text='<%# Eval("DepositPercentage") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:TextBox ID="txtPercentage" runat="server" class="form-control tg" Text='<%# Eval("DepositPercentage") %>'></asp:TextBox>--%>
                                                                    <telerik:RadNumericTextBox ID="txtPercentage" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="2" Width="70px" CssClass="form-control tg"
                                                                        Text='<%# Eval("DepositPercentage") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle Width="40px" />
                                                                <ControlStyle Width="60px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Deposit Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("DepositAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:TextBox ID="txtAmount" runat="server" class="form-control tg" Text='<%# Eval("DepositAmount") %>'></asp:TextBox>--%>
                                                                    <telerik:RadNumericTextBox ID="txtAmount" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="2" Width="70px" CssClass="form-control tg"
                                                                        Text='<%# Eval("DepositAmount") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="40px" />
                                                                <ItemStyle Width="40px" />
                                                                <ControlStyle Width="60px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Future Reservation Time(in Minutes)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFutureTime" runat="server" Text='<%# Eval("FutureReservationTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:TextBox ID="txtTimeSpan" runat="server" class="form-control tg isNumeric" Text='<%# Eval("TimeSpan") %>'></asp:TextBox>--%>
                                                                    <telerik:RadNumericTextBox ID="txtFutureTime" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="50px" DataType="Integer"
                                                                        CssClass="form-control tg" Text='<%# Eval("FutureReservationTime") %>'>
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="reqFutureTime" runat="server" ControlToValidate="txtFutureTime"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Online Max Covers">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOnlineMaxCovers" runat="server" Text='<%# Eval("OnlineMaxCovers") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <%--<asp:TextBox ID="txtTimeSpan" runat="server" class="form-control tg isNumeric" Text='<%# Eval("TimeSpan") %>'></asp:TextBox>--%>
                                                                    <telerik:RadNumericTextBox ID="txtOnlineMaxCovers" runat="server" Height="32px" Type="Number"
                                                                        Skin="Office2010Blue" NumberFormat-DecimalDigits="0" Width="70px" DataType="Integer"
                                                                        CssClass="form-control tg" Text='<%# Eval("OnlineMaxCovers") %>' MinValue="1">
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="reqOnlineMaxCovers" runat="server" ControlToValidate="txtOnlineMaxCovers"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Table Set Group">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTSG" runat="server" Text='<%# Eval("GroupID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadComboBox ID="ddlGroup" Skin="MetroTouch" runat="server" Width="200px"
                                                                        DropDownHeight="200px">
                                                                    </telerik:RadComboBox>
                                                                    <%--<asp:RequiredFieldValidator ID="reqGroup" runat="server" ControlToValidate="ddlGroup"
                                                                        ErrorMessage="*" ValidationGroup="Schedule" ForeColor="Red" Display="Dynamic"
                                                                        InitialValue="">
                                                                    </asp:RequiredFieldValidator>--%>
                                                                </EditItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle Width="50px" />
                                                                <ControlStyle Width="150px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:CommandField ShowEditButton="true" EditImageUrl="Images/Icons/edit.png" ButtonType="Image"
                                                                UpdateImageUrl="Images/Icons/save.png" CancelImageUrl="Images/Icons/cancel.png" />--%>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CausesValidation="false">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/Icons/edit.png" />
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" ValidationGroup="Schedule">
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="Images/Icons/save.png" />
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/Icons/cancel.png" />
                                                                    </asp:LinkButton>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="Images/Icons/delete.png"
                                                                        CommandName="SDelete" CommandArgument='<%# Eval("BookingScheduleID") %>' OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                                        AlternateText="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <a href='<%# "BookingScheduleDate.aspx?BookingScheduleID=" + Eval("BookingScheduleID").ToString() + "&StartDate=" + Eval("StartDate").ToString() + "&EndDate=" + Eval("EndDate").ToString() %>'
                                                                        id="aUpdateDate" runat="server" class="schedule-date"><i class="fa fa-calendar">
                                                                        </i></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="text-align: center;">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Settings" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" />
                                        <asp:Button ID="btnCloseData" runat="server" Text="Clear Data" CssClass="btn btn-magenta" />
                                    </div>
                                    <%--<div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            List Of All Settings :-
                                        </h4>
                                    </div>
                                    <div class="panel-body">--%>
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="col-sm-9 col-lg-3 controls">
                                            <label class="checkbox">
                                                <asp:CheckBox ID="chkShowInactiveSetting" runat="server" AutoPostBack="True" />
                                                Show Inactive Setting
                                            </label>
                                        </div>
                                    </div>
                                    <telerik:RadGrid ID="gvVenues" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                        AllowSorting="false">
                                        <MasterTableView DataKeyNames="BookingSettingsid, IsActive">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="VenueName" HeaderText="Venue" SortExpression="VenueName"
                                                    HeaderStyle-HorizontalAlign="Center" />
                                                <telerik:GridBoundColumn DataField="StoreName" HeaderText="Store" SortExpression="StoreName"
                                                    HeaderStyle-HorizontalAlign="Center" />
                                                <telerik:GridBoundColumn DataField="BookingTypeName" HeaderText="Booking Type" HeaderStyle-HorizontalAlign="Center" />
                                                <telerik:GridBoundColumn DataField="RedirectToName" HeaderText="Redirect To" HeaderStyle-HorizontalAlign="Center" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditVenue"
                                                    Text="Edit" HeaderText="Edit" HeaderStyle-Width="100px" ImageUrl="Images/Icons/edit.png">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteVenue"
                                                    Text="Delete" HeaderText="Delete" HeaderStyle-Width="100px" ImageUrl="Images/Icons/delete.png">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnActive" CommandName="ActiveVenue"
                                                    Text="Active" HeaderText="Active" HeaderStyle-Width="100px" ImageUrl="Images/Icons/Active.png">
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
    <%--    </ContentTemplate>
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
    </asp:UpdateProgress>--%>
</asp:Content>

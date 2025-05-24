<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupBookingConfirmEdit.aspx.vb"
    Inherits="BookingEasy_PopupBookingConfirmEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Theme/assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty-responsive.css" />
    <link rel="stylesheet" type="text/css" href="/css/Grid.ss.css" />
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
            height: 250PX !important;
        }
        .rcbWidth
        {
            height: 250PX !important;
        }
    </style>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Cancle this Booking?");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm2" runat="server">
    </asp:ScriptManager>
    <div id="divError" runat="server" visible="false" style="text-align:center;">
        <asp:Label ID="lblNoBooking" ForeColor="Red" runat="server"></asp:Label>
    </div>
    <div class="box" id="divInfo" runat="server">
        <style type="text/css" media="screen">
            .container
            {
                width: 90%;
                padding-left: 20px;
            }
            .left20
            {
                width: 20%;
                float: left;
            }
            .left40
            {
                width: 40%;
                float: left;
            }
            .clear
            {
                clear: both;
            }
        </style>
        <div class="box-title">
            <h3>
                <i class="fa fa-search"></i>Booking Details</h3>
        </div>
        <div class="container">
            <div class="left20">
                <b>
                    <label class="col-lg-3 control-label">
                        Booking:</label></b>
            </div>
            <div class="left20">
                <asp:Label ID="lblBookingRef" runat="server"></asp:Label><br />
            </div>
            <div class="left20">
                <b>
                    <label class="col-lg-4 control-label">
                        Covers:</label></b>
            </div>
            <div class="left20">
                <asp:Label ID="lblCovers" runat="server"></asp:Label>
            </div>
            <div class="clear">
            </div>
            <div class="left20">
                <b>
                    <label class="col-lg-3 control-label">
                        Date:</label></b>
            </div>
            <div class="left20">
                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
            </div>
            <div class="left20">
                <b>
                    <label class="col-lg-4 control-label">
                        Time:</label></b>
            </div>
            <div class="left20">
                <asp:Label ID="lblTime1" runat="server"></asp:Label>
            </div>
            <div class="clear">
            </div>
            <div class="left20">
                <b>
                    <label class="col-lg-4 control-label">
                        Location:</label></b>
            </div>
            <div class="left20">
                <asp:Label ID="lblLocation" runat="server"></asp:Label>
            </div>
            <div class="left20">
                <b>
                    <label class="col-lg-4 control-label">
                        Schedule:</label></b>
            </div>
            <div class="left20">
                <asp:Label ID="lblSchedule" runat="server"></asp:Label>
            </div>
            <div class="clear">
            </div>
          <%--  <div class="left20">
                <b>
                    <label class="col-lg-4 control-label">
                        Allotted Table No:</label></b>
            </div>--%>
            <div class="left20">
                <asp:Label ID="lblAllottedTable" runat="server"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
        <asp:Panel ID="pnlSearch" runat="server" Visible="true">
            <div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-search"></i>Search</h3>
                                <div class="box-tool">
                                    <%--<a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>--%>
                                </div>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row" style="padding-left: 20px;">
                                        <b>Venue:</b>
                                        <asp:Label ID="lblVenue" runat="server"></asp:Label>&nbsp;&nbsp; <b>Date:</b>
                                        <telerik:RadDatePicker ID="dtpDate" runat="server" EnableTyping="true" Width="140px"
                                            Skin="MetroTouch" AutoPostBack="true">
                                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="dtpDate"
                                            ForeColor="Red" ErrorMessage="*" Display="Static" ValidationGroup="SearchTable">
                                        </asp:RequiredFieldValidator>
                                        <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="120px"
                                            DropDownHeight="120px" AutoPostBack="True">
                                        </telerik:RadDropDownList>
                                        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable">
                                        </asp:RequiredFieldValidator>
                                        &nbsp;&nbsp; <b>No Of People:</b>
                                        <%--<telerik:RadDropDownList ID="ddlNoOfCovers" Skin="MetroTouch" runat="server" Width="100px"
                                        DropDownHeight="200px" AutoPostBack="True">
                                        <Items>
                                            <telerik:DropDownListItem Value="1" Text="1" Selected="True"></telerik:DropDownListItem>
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
                                            <telerik:DropDownListItem Value="-1" Text="Other"></telerik:DropDownListItem>
                                        </Items>
                                    </telerik:RadDropDownList>--%>
                                        <telerik:RadNumericTextBox ID="txtNoOfCovers" runat="server" Type="Number" Skin="Office2010Blue"
                                            NumberFormat-DecimalDigits="0" Width="200px" DataType="Integer" Height="32px"
                                            CssClass="form-control tg">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTable"
                                            CssClass="rfv" Enabled="True">
                                        </asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="regNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                            Type="Integer" MinimumValue="1" MaximumValue="9999" ForeColor="Red" ErrorMessage="*"
                                            Display="Dynamic" ValidationGroup="SearchTable" CssClass="rfv" Enabled="True"></asp:RangeValidator>&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                            ValidationGroup="SearchTable" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="box">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-table"></i>Search Result</h3>
                                <div class="box-tool">
                                    <%--<a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>--%>
                                </div>
                            </div>
                            <div class="box-content" style="min-height: 200px;">
                                <div class="form-horizontal">
                                    <div style="font-weight: bold; margin-bottom: 5px;">
                                        Tables Of
                                        <asp:Label ID="lblTablesOf" Text="" runat="server" />
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div id="divMessageBox" runat="server" visible="False">
                                                <button data-dismiss="alert" class="close">
                                                    ×</button>
                                                <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Repeater ID="repTables" runat="server">
                                        <ItemTemplate>
                                            <table cellpadding="4px" cellspacing="0" border="0" style="width: 100%; border: 1px solid gray;
                                                margin-bottom: 5px;">
                                                <tr>
                                                    <td>
                                                        <b>Schedule Name: </b>
                                                        <%# Eval("Name")%>
                                                        <asp:HiddenField ID="hdnStoreId" runat="server" Value='<%# Eval("StoreID")%>' />
                                                        <asp:HiddenField ID="hdnSettingId" runat="server" Value='<%# Eval("SettingID")%>' />
                                                    </td>
                                                    <td style="width: 700px; text-align: center;">
                                                        <asp:Repeater ID="repTimeSlot" runat="server" OnItemCommand="repTimeSlot_ItemCommand">
                                                            <ItemTemplate>
                                                                <%--<asp:Button ID="lnkBooked" runat="server" CssClass='<%# IIf(Eval("IsAvailable") = "1", "btn  btn-success btn-sm time-slot", "btn btn-gray btn-sm time-slot")%> ' 
                                                            Text='<%# Eval("SlotTime")%>' Enabled='<%# IIf(Eval("IsAvailable") = "1", True, False)%>' CommandArgument='<%# Eval("SlotTime")%>' CommandName="BookTable">
                                                            </asp:Button>--%>
                                                                <asp:Button ID="lnkBooked" runat="server" CssClass='<%# IIf(Eval("IsAvailable") = "1", "btn  btn-success btn-sm time-slot", "btn btn-gray btn-sm time-slot")%> '
                                                                    Text='<%# TimeSpan.Parse(Eval("SlotTime").ToString()).ToString("hh\:mm")%>' Enabled='<%# IIf(Eval("IsAvailable") = "1", True, False)%>'
                                                                    CommandArgument='<%# TimeSpan.Parse(Eval("SlotTime").ToString()).ToString("hh\:mm")%>'
                                                                    CommandName="BookTable" Style="margin: 5px;"></asp:Button>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div style="clear: both;">
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlConfirm" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-11">
                    Venue :
                    <asp:Label ID="lblConfirmVenue" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-11">
                    Booking Date :
                    <asp:Label ID="lblBookingDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-11">
                    No Of People :
                    <asp:Label ID="lblNoOfPeople" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
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
                                            <telerik:GridTemplateColumn UniqueName="Action" HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("ProductID") %>' />
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
            </div>
            <div class="row" style="margin-bottom: 20px;">
                <div class="col-md-1">
                </div>
                <div class="col-md-11">
                    Comment :
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        
        <asp:Panel ID="pnlBtn" runat="server">
            <div class="row">
                <div class="col-md-12 controls" style="text-align: center; height:35px;">
                    <asp:Button ID="btnConfitmBooking" runat="server" Text="Update Booking" CssClass="btn btn-success"
                        Visible="false"  style="float:left;"/>
                    
                
                    
                    <asp:Button ID="btnChangeBooking" runat="server" Text="Change Booking" CssClass="btn btn-success" style="float:right; margin-right: 5px;" Visible="false"/>&nbsp;&nbsp;
                  
                </div>
            </div>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Default"
        Style="position: absolute; top: 0; left: 0; height: 100%; width: 100%; z-index: 1000;"
        IsSticky="true">
    </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>

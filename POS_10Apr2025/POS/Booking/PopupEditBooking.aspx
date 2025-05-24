<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupEditBooking.aspx.vb"
    Inherits="BookingEasy_PopupEditBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Theme/assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty-responsive.css" />
    <link rel="stylesheet" type="text/css" href="/css/Grid.ss.css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm2" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnlSearch" runat="server" Visible="true">
        <div>
            <div class="row">
                <div class="row" style="padding-left: 34px; padding-bottom: 5px">
                    <asp:CompareValidator ID="cmp" runat="server" ControlToValidate="dtpArrival" ControlToCompare="dtpDeparture"
                        ForeColor="Red" ErrorMessage="Arrival date must be less then departure date."
                        Operator="LessThanEqual" Type="Date" Display="Dynamic" ValidationGroup="Search"></asp:CompareValidator>
                </div>
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-search"></i>Search</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                    <b>
                                        <label class="col-lg-3 control-label">
                                            Venue:</label></b>
                                        <asp:Label ID="lblVenueName" runat="server"></asp:Label>&nbsp;&nbsp;
                                    <b>
                                        <label class="col-lg-4 control-label">
                                            Arrival:</label></b>
                                        <telerik:RadDatePicker ID="dtpArrival" runat="server" EnableTyping="true" Width="200px"
                                            Skin="MetroTouch" AutoPostBack="True">
                                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="rfvArrival" runat="server" ControlToValidate="dtpArrival"
                                            ForeColor="Red" ErrorMessage="Please select arrival date." Display="Dynamic"
                                            ValidationGroup="Search">
                                        </asp:RequiredFieldValidator>&nbsp;&nbsp;
                                    <b>
                                        <label class="col-lg-4 control-label">
                                            Departure:</label></b>
                                        <telerik:RadDatePicker ID="dtpDeparture" runat="server" EnableTyping="true" Width="200px"
                                            Skin="MetroTouch">
                                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                                        </DateInput>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="rfvDeparture" runat="server" ControlToValidate="dtpDeparture"
                                            ForeColor="Red" ErrorMessage="Please select departure date." Display="Dynamic"
                                            ValidationGroup="Search">
                                        </asp:RequiredFieldValidator>&nbsp;&nbsp;
                                    
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                            ValidationGroup="Search" />
                                    </div>
                                </div>
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
                            <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content">
                        <div class="form-horizontal">
                            <div style="font-weight: bold; margin-bottom: 5px;">
                                Rooms Of
                                <asp:Label ID="lblRoomsOf" Text="" runat="server" />
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="divMessageBox" runat="server" visible="False">
                                        <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <asp:Repeater ID="repRooms" runat="server">
                                <ItemTemplate>
                                    <table cellpadding="4px" cellspacing="0" border="0" style="width: 100%; border: 1px solid gray;
                                        margin-bottom: 5px;">
                                        <tr>
                                            <td>
                                                <b>Hotel Name: </b>
                                                <%# Eval("StoreName")%>
                                            </td>
                                            <td rowspan="3" style="width: 100px; text-align: center;">
                                                <%--<a href="BookingDetail.aspx?StoreId=<%# Utils.Encrypt(Eval("StoreId")).ToString().Replace("+", "___")%>&arrival=<%=ArrivalDate %>&departure=<%=DepartureDate %>&RoomType=<%# Eval("RoomTypeID")%>&VenueID=<%=VenueId %>"
                                                class="btn btn-primary">Book Now</a>--%>
                                                <asp:LinkButton ID="lnkBookNow" runat="server" CommandName="BOOKNOW" CommandArgument='<%# Convert.toString(Eval("StoreId")) + "," + Convert.ToString(Eval("RoomTypeID"))%>'
                                                    Text="Book Now"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Room Type:</b>
                                                <%# Eval("RoomType")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Price:</b> <%= CurrencySymbol %>&nbsp;<%# Eval("price")%>/ Night
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
    </asp:Panel>
    <asp:Panel ID="pnlExtraServices" runat="server" Visible="false">
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
        </div>
        <div class="row">
            <div class="form-horizontal">
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
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-xs-3 col-lg-4 control-label">
                                Deposit:</label>
                            <div class="col-sm-9 col-lg-8 controls">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-4 col-lg-4">
                    </div>
                    <div class="col-sm-4 col-lg-4 controls" style="text-align: center;">
                        <asp:Button ID="btnConfitmBooking" runat="server" Text="Update Booking" CssClass="btn btn-success" />
                    </div>
                    <div class="col-sm-4 col-lg-4">
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-2 col-lg-2">
                    </div>
                    <div class="col-sm-8 col-lg-8 controls" style="text-align: center;" id="divNoRooms"
                        runat="server" visible="False">
                        <asp:Label ID="lblNoRooms" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-sm-2 col-lg-2">
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>

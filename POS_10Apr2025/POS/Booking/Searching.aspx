<%@ Page Title="Search Booking" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="Searching.aspx.vb" Inherits="Searching" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .time-slot
        {
            float: left;
            margin: 5px;
        }
    </style>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa fa-search"></i>Searching
                    </h1>
                    <h4>
                        Search for customer and booking.
                    </h4>
                </div>
            </div>
            <div class="row">
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
                                <div class="form-group">
                                    <div class="col-sm-12 col-lg-12 controls">
                                        <asp:RadioButton ID="rdoCustomer" Text=" Customer" Checked="true" runat="server"
                                            GroupName="SearchGroup" />
                                        <span class="help-inline">&nbsp;<asp:Label ID="lblSearchOption" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;</span>
                                        <asp:RadioButton ID="rdoBooking" Text=" Booking" runat="server" GroupName="SearchGroup" />
                                        <span class="help-inline">&nbsp;(Search By Booking Ref No)</span>
                                    </div>
                                    <div class="col-sm-12 col-lg-12 controls">
                                        <div class="input-group">
                                            <span class="input-group-addon" style="background-color: #0090ff; color: #FFF; border: 1px solid #0090ff;">
                                                <i class="fa fa-search"></i></span>
                                            <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Search here..."></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                                    ValidationGroup="Searching" />
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch"
                                            ForeColor="Red" ErrorMessage="Please enter text." Display="Dynamic" ValidationGroup="Searching">
                                        </asp:RequiredFieldValidator>
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
                                <i class="fa fa-table"></i>Customers</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="divMessageBox" runat="server" visible="False">
                                            <button data-dismiss="alert" class="close">
                                                ×</button>
                                            <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <telerik:RadGrid ID="gvCustomers" runat="server" Skin="Office2010Blue" AutoGenerateColumns="false"
                                    Visible="False">
                                    <MasterTableView DataKeyNames="accountid">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="firstname" HeaderText="First Name" />
                                            <telerik:GridBoundColumn DataField="lastname" HeaderText="Last Name" />
                                            <telerik:GridBoundColumn DataField="mobile" HeaderText="Mobile" />
                                            <telerik:GridBoundColumn DataField="email1st" HeaderText="Email" />
                                            <telerik:GridTemplateColumn HeaderText="Edit">
                                                <ItemTemplate>
                                                    <a href='<%# "popEditCustomer.aspx?accId=" +  Eval("accountid").ToString() %>' runat="server"
                                                        class="edit-customer">Edit</a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
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
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-table"></i>Bookings</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="div1" runat="server" visible="False">
                                            <button data-dismiss="alert" class="close">
                                                ×</button>
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:LinkButton ID="imgbtn1" runat="server" AlternateText="Xlsx" PostBackUrl="~/BookingEasy/Searching.aspx"
                                            Style="float: right;" Visible="false">
                                        <img alt="" src="Images/excel.png"  height="30px" style="margin:4px"/>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <telerik:RadGrid ID="gvBookings" runat="server" Skin="Office2010Blue" AutoGenerateColumns="false"
                                    Visible="False">
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="Period,BookingRef,StoreId,Arrivaldate,Departuredate">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="No" UniqueName="No" AllowFiltering="false"
                                                Display="false">
                                                <ItemTemplate>
                                                    <%# Container.DataSetIndex+1 %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="BookingRef" HeaderText="Booking #" />
                                            <telerik:GridBoundColumn DataField="Period" HeaderText="Type" />
                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Hotel/Schedule Name" />
                                            <telerik:GridBoundColumn DataField="Arrivaldate" HeaderText="Arrival" DataFormatString="{0:dd/MM/yyyy}"/>
                                            <telerik:GridBoundColumn DataField="Departuredate" HeaderText="Departure" DataFormatString="{0:dd/MM/yyyy}" />
                                            <telerik:GridBoundColumn DataField="comment" HeaderText="Comment" />
                                            <telerik:GridTemplateColumn HeaderText="Cancel" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCancelBooking" runat="server" CommandName="CancelBooking"
                                                        OnClientClick="return confirm('Are you sure you want to delete this booking?');">Cancel Booking</asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Print" Visible="true" UniqueName="Print">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hndType" runat="server" Value='<%# Eval("Period") %>' />
                                                    <asp:LinkButton ID="lnkpopup1" runat="server" EnableViewState="true" Visible="false">
                                                        <a href='<%# "PopupPrintBookingTableData.aspx?bookingid=" + Eval("bookingid").ToString() %>'
                                                            id="aChangeTable1" runat="server" class="edit-booking">
                                                            <img src="Images/printer.png" style="height: 25px;" /></a></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkpopup2" runat="server" EnableViewState="true" Visible="false">
                                                        <a href='<%# "PopupPrintBookingHotel.aspx?bookingid=" + Eval("bookingid").ToString() %>'
                                                            id="a1" runat="server" class="edit-booking">
                                                            <img src="Images/printer.png" style="height: 25px;" /></a></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%--<telerik:GridTemplateColumn HeaderText="Update">
                                                <ItemTemplate>
                                                    <a href='<%# "PopupEditBooking.aspx?bookingref=" +  Eval("BookingRef").ToString() +"&customerid="+ Eval("AccountID").ToString()+"&venueid="+Eval("VenueID").ToString()+"&bookingsettingid="+ Eval("BookingSettingsID").ToString() %>'
                                                        id="aUpdateBooking" runat="server" class="edit-booking">Update Booking</a> <a href='<%# "c?bookingref=" +  Eval("BookingRef").ToString() +"&customerid="+ Eval("AccountID").ToString()+"&venueid="+Eval("VenueID").ToString()+"&bookingsettingid="+ Eval("BookingSettingsID").ToString() %>'
                                                            id="aUpdateBookingTable" runat="server" class="edit-booking">Update Booking</a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
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
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-title">
                            <h3>
                                <i class="fa fa-table"></i>Booking Details</h3>
                            <div class="box-tool">
                                <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div class="form-horizontal">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="divMessageBox1" runat="server" visible="False">
                                            <button data-dismiss="alert" class="close">
                                                ×</button>
                                            <asp:Label ID="lblMessageBox1" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div id="divBookingDetails" runat="server" visible="False">
                                            <a href="" id="aUpdateBooking" runat="server" class="edit-booking" visible="false"
                                                style="display: none;"></a>
                                            <%--<br />
                                            <br />--%>
                                            <telerik:RadGrid ID="gvBookingDetailsTable" runat="server" Skin="Office2010Blue"
                                                AutoGenerateColumns="false">
                                                <MasterTableView DataKeyNames="bookingid,bookingref,date,bookingtime">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="bookingref" HeaderText="Booking #" />
                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name" />
                                                        <telerik:GridBoundColumn DataField="date1" HeaderText="Booking Date" DataFormatString="{0:dd/MM/yyyy}"  />
                                                        <telerik:GridBoundColumn DataField="bookingtime" HeaderText="Booking Time" />
                                                        <telerik:GridBoundColumn DataField="covers" HeaderText="Number Of Covers" />
                                                        <telerik:GridTemplateColumn HeaderText="Update">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnbookingref" runat="server" Value='<%#Eval("bookingref")%>' />
                                                                <asp:HiddenField ID="hdncustomerid" runat="server" Value='<%#Eval("AccountID")%>' />
                                                                <asp:HiddenField ID="hdnIsSeated" runat="server" Value='<%#Eval("IsSeated")%>' />
                                                                <asp:HiddenField ID="hdnBookingSettingsid" runat="server" Value='<%#Eval("BookingSettingsid")%>' />
                                                                <%--<a href='<%# "PopupEditBookingTableData.aspx?bookingref=" +  ReffId() +"&customerid="+ AccountId()+"&venueid="+StoreId()+"&bookingsettingid="+ BookingSettingsid() %>'
                                                                    id="aUpdateBooking" runat="server" class="edit-booking">Update Booking</a>--%>
                                                                <a href="" id="aUpdateBooking" runat="server" class="edit-booking">Update Booking</a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Delete">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkCancelBooking" runat="server" CommandName="CancelBooking"
                                                                    CommandArgument='<%#Eval("bookingid")%>' OnClientClick="return confirm('Are you sure you want to delete this booking?');"><img src="Images/Icons/delete.png" alt="delete group" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                            <telerik:RadGrid ID="gvBookingDetails" runat="server" Skin="Office2010Blue" AutoGenerateColumns="false">
                                                <MasterTableView DataKeyNames="bookingid,bookingref,date">
                                                    <Columns>
                                                        <%--<telerik:GridTemplateColumn HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridBoundColumn DataField="bookingref" HeaderText="Booking #" />
                                                        <telerik:GridBoundColumn DataField="date" HeaderText="Date" />
                                                        <telerik:GridBoundColumn DataField="RoomType" HeaderText="Type" />
                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Room" />
                                                        <telerik:GridTemplateColumn HeaderText="Update">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <a href='<%# "PopupEditBookingRoom.aspx?bookingref=" +  ReffId() +"&customerid="+ AccountId()+"&StoreId="+StoreId()+"&bookingid="+ Eval("bookingid").ToString() %>'
                                                                    id="aUpdateBooking" runat="server" class="edit-bookingRoom">Update Booking</a>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Delete">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkCancelBooking" runat="server" CommandName="CancelBooking"
                                                                    CommandArgument='<%#Eval("bookingid")%>' OnClientClick="return confirm('Are you sure you want to delete this booking?');"><img src="Images/Icons/delete.png" alt="delete group" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                            <br />
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
                                            <br />
                                            <%--<b>Rooms : </b>
                                            <telerik:RadDropDownList ID="ddlRoomType" Skin="MetroTouch" runat="server" Width="250px"
                                                DropDownHeight="200px" AutoPostBack="True">
                                            </telerik:RadDropDownList>
                                            <telerik:RadDropDownList ID="ddlRoomName" Skin="MetroTouch" runat="server" Width="250px"
                                                DropDownHeight="200px" AutoPostBack="True">
                                            </telerik:RadDropDownList>
                                            <asp:Button ID="btnChangeRoom" runat="server" Text="Change Room" CssClass="btn btn-primary"
                                                ValidationGroup="SearchClient" Enabled="False" />--%>
                                            <asp:Button ID="btnSaveServices" runat="server" Text="Save Services" CssClass="btn btn-primary"
                                                ValidationGroup="SearchClient" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:LinkButton ID="lnkReloadUser" runat="server" Style="display: none;">ReloadUser</asp:LinkButton>
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

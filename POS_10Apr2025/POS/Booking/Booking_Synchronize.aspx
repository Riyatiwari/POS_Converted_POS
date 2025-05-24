<%@ Page Title="Booking Synchronize" Language="VB" MasterPageFile="~/BookingEasy/Site.master" AutoEventWireup="false" CodeFile="Booking_Synchronize.aspx.vb" Inherits="BookingEasy_Booking_Synchronize" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="page-title">
        <div>
            <h1>
                <i class="fa fa-file-text-o"></i>Booking Synchronize
            </h1>
        </div>
    </div>
    <div id="divContent" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-black">
                    <div class="box-title">
                        <h3>
                            <i class="fa fa-bars"></i>Transaction
                        </h3>
                    </div>
                    <div class="box-content">
                        <div class="form-horizontal">
                            <div class="row">
                                <div style="text-align: right; margin-right: 2%">
                                    <asp:LinkButton ID="imgbtn" runat="server"  AlternateText="Xlsx">
                                        <img alt="" src="Images/excel.png"  height="30px" style="margin:4px"/>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-md-12">
                                    <telerik:RadGrid ID="gvTabs" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                        AllowSorting="false" AllowPaging="True" AllowFilteringByColumn="True">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView DataKeyNames="BookingSync_ID">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Sync_From" HeaderText="From"
                                                    ShowFilterIcon="false" SortExpression="Sync_From" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridBoundColumn DataField="Sync_To" HeaderText="To" SortExpression="Sync_To"
                                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridBoundColumn DataField="bookingref" HeaderText="Booking Reference No." SortExpression="bookingref"
                                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridDateTimeColumn DataField="BookingDate" HeaderText="Booking Date"
                                                    CurrentFilterFunction="Between" ShowFilterIcon="false" SortExpression="BookingDate"
                                                    EnableRangeFiltering="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" />
                                                <telerik:GridDateTimeColumn DataField="Sync_Date" HeaderText="Synchronize Date"
                                                    CurrentFilterFunction="Between" ShowFilterIcon="false" SortExpression="Sync_Date"
                                                    EnableRangeFiltering="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"/>
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
    </div>
</asp:Content>


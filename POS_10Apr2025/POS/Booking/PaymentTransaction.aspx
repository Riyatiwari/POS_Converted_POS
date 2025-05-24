<%@ Page Language="VB" MasterPageFile="~/BookingEasy/Site.master" AutoEventWireup="false"
    CodeFile="PaymentTransaction.aspx.vb" Inherits="BookingEasy_PaymentTransaction"
    Title="Payment Transaction" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">

        function PrintPage() {

            var printContent = document.getElementById('<%= gvTabs.ClientID %>');

            var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');

            printWindow.document.write(printContent.innerHTML);

            printWindow.document.close();

            printWindow.focus();

            printWindow.print();

        }

    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title">
        <div>
            <h1>
                <i class="fa fa-file-text-o"></i>Payment Summary
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
                                    <asp:LinkButton ID="imgbtn" runat="server" AlternateText="Xlsx">
                                        <img alt="" src="Images/excel.png"  height="30px" style="margin:4px"/>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-md-12">
                                    <telerik:RadGrid ID="gvTabs" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                        AllowSorting="false" AllowPaging="True" AllowFilteringByColumn="True">
                                        <GroupingSettings CaseSensitive="false" />
                                        <ExportSettings HideStructureColumns="true">
                                        </ExportSettings>
                                        <MasterTableView DataKeyNames="Tran_id">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="No" UniqueName="No" AllowFiltering="false"
                                                    Display="false">
                                                    <ItemTemplate>
                                                        <%# Container.DataSetIndex+1 %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Transaction_ref_no" HeaderText="Transaction Reference No."
                                                    ShowFilterIcon="false" SortExpression="Transaction_ref_no" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridBoundColumn DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridBoundColumn DataField="email1st" HeaderText="Email" SortExpression="email1st"
                                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridDateTimeColumn DataField="Trasaction_date" HeaderText="Transaction Date"
                                                    CurrentFilterFunction="Between" ShowFilterIcon="false" SortExpression="Trasaction_date"
                                                    EnableRangeFiltering="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"  />
                                                <telerik:GridNumericColumn DataType="System.Decimal" DataField="Amount" HeaderText="Amount"
                                                    SortExpression="Amount" ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="200px" AutoPostBackOnFilter="true" Aggregate="Sum" DataFormatString="{0}" />
                                                <telerik:GridBoundColumn DataField="Currency" HeaderText="Currency" SortExpression="Currency"
                                                    ShowFilterIcon="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridBoundColumn DataField="booking_type" HeaderText="Booking Type" ShowFilterIcon="false"
                                                    SortExpression="booking_type" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                <telerik:GridTemplateColumn HeaderText="Print" Visible="true" AllowFiltering="false"
                                                    UniqueName="Print">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hndType" runat="server" Value='<%# Eval("booking_type") %>' />
                                                        <asp:LinkButton ID="lnkpopup1" runat="server" EnableViewState="true" Visible="false">
                                                            <a href='<%# "PopupPrintBookingTableData.aspx?bookingid=" + Eval("Booking_id").ToString() %>'
                                                                id="aChangeTable1" runat="server" class="edit-booking">
                                                                <img src="Images/printer.png" style="height: 25px;" /></a></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkpopup2" runat="server" EnableViewState="true" Visible="false">
                                                            <a href='<%# "PopupPrintBookingHotel.aspx?bookingid=" + Eval("Booking_id").ToString() %>'
                                                                id="a1" runat="server" class="edit-booking">
                                                                <img src="Images/printer.png" style="height: 25px;" /></a></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
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

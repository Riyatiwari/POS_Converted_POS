<%@ Page Title="Stock Management List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Management_List.aspx.vb" Inherits="Stock_Management_List" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Stock Purchase List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Stock Purchase List</li>
        </ol>
    </div>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid();
        });

        function Grid() {

            var getTxtValue = ''
            getTxtValue = $('#<%=txt_check.ClientID%>').val()

            $("#Psummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table2 = $('#Psummary').DataTable({
                  "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },
                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [

                    { "visible": true, "targets": +groupCol }

                ],

                "displayLength": 50,
                "searching": true,
                "ordering": false

            });

            var table1 = $('#Psummary').DataTable();

            $('#Psummary thead tr:eq(1) th').each(function (i) {

                var title = $(this).text();

                if (title == 'Table Name' && getTxtValue != '') {
                    $(this).html('<input type="text" value="' + getTxtValue + '" style="width:98%" placeholder="Search ' + title + '" />');

                }
                else {
                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }

                $('input', this).on('keyup change', function () {
                    if (table1.column(i).search() !== this.value) {
                        table1.column($(this).parent().index() + ':visible')
                            .search(this.value)
                            .draw();

                        if (title == 'Table Name') {

                            document.getElementById('<%= txt_check.ClientID %>').value = this.value;
                        }
                    }
                });

                if (getTxtValue != '') {

                    table1.column($(this).parent().index() + ':visible')
                        .search(getTxtValue)
                        .draw();

                }

            });
        }
    </script>


</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <style>
        html:first-child .RadWindow ul {
            border: 1px solid transparent;
            float: left !important;
        }

        .small_btn {
            height: 30px;
            padding: 8px;
        }

        .medium_small {
            height: 39px;
            padding: 11px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>
    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("IbViewDetail") >= 0 ||
                args.get_eventTarget().indexOf("IbViewDetail") >= 0 ||
                args.get_eventTarget().indexOf("IbViewDetail") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>--%>

    <div>
          <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />
        <asp:HiddenField runat="server" ID="txt_check" />
        <asp:HiddenField runat="server" ID="hdfRefId" />
    </div>

    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="onRequestStart">--%>
    <div class="panel panel-yellow">
        <div class="panel-heading">Stock Purchase List</div>
        <div class="panel-body pan">
            <div class="form-body pal">
                <div class="row">
                    <center>
                        <div class="col-lg-6">
                            Duration : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radDuration" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radDuration_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Today" Value="Today" />
                                                <telerik:RadComboBoxItem Text="Yesterday" Value="Yesterday" />
                                                <telerik:RadComboBoxItem Text="This Week" Value="This Week" Selected="true" />
                                                <telerik:RadComboBoxItem Text="This Month" Value="This Month" />
                                                <telerik:RadComboBoxItem Text="This Year" Value="This Year" />
                                                <telerik:RadComboBoxItem Text="Last Week" Value="Last Week" />
                                                <telerik:RadComboBoxItem Text="Last Month" Value="Last Month" />
                                                <telerik:RadComboBoxItem Text="Last Year" Value="Last Year" />
                                                <telerik:RadComboBoxItem Text="Custom" Value="Custom" />
                                            </Items>
                                        </telerik:RadComboBox>

                        </div>
                         <div class="clearfix"></div>
                         <br />

                        <div class="col-lg-12" id="divcustom" runat="server" visible="false">
                            <div class="col-lg-6">
                                From Date : &nbsp;
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000"
                                    MaxDate="01/01/3000" Skin="MetroTouch" OnSelectedDateChanged="txtForDate_SelectedDateChanged" AutoPostBack="true">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                            </div>

                            <div class="col-lg-6">
                                To Date : &nbsp;
                                <telerik:RadDatePicker ID="txtToDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date" Display="None" ValidationGroup="valid" ControlToCompare="txtForDate" ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <br />
                        </div>

                        <div class="col-lg-12">
                            <asp:LinkButton ID="lnkView" runat="server" class="btn btn-primary" ToolTip="View" OnClick="lnkView_Click">View</asp:LinkButton>
                            <div class="clearfix"></div>
                            <br />
                        </div>

                        </center>

                    <div class="clearfix"></div>
                    <br />

                    <div class="col-lg-12">
                        <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Stock Purchase</asp:LinkButton>
                    </div>
                </div>
                <br />
                <div class="row" id="divDevice" runat="server">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">

                                <asp:Repeater ID="rdStock" runat="server" OnItemCommand="rdStock_ItemCommand" OnItemDataBound="rdStock_ItemDataBound">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Receipt Number</th>
                                                <th>Total</th>
                                                <th>Location</th>
                                                <th>Supplier</th>
                                                <th>Supplier Code</th>
                                                <th>Receive Date</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnExpand" runat="server" Text="+" Style="font-weight: bold;" />

                                                <asp:HiddenField ID="hd_stk_rec_id" runat="server" Value='<%# Eval("stk_rec_id") %>' />
                                            </td>
                                            <td style="background-color: #ffffff;"><%#Eval("receipt_number") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("total_price") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("location") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Supplier") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Supplier_code") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("stock_date") %></td>
                                            <td style="background-color: #ffffff; text-align: center;" id="td_action" runat="server">
                                                <asp:HiddenField ID="hf_isfinalsubmit" runat="server" Value='<%#Eval("is_finalsubmit")%>' />

                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                    CommandArgument='<%#Eval("stk_rec_id")%>' CommandName="Edit" Visible="true"><i class="fa fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                                    <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                        CommandArgument='<%#Eval("stk_rec_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                                        <i class="fa fa-minus-square fa-lg"></i></asp:LinkButton>

                                            </td>
                                        </tr>

                                        <asp:Panel ID="pnlStockDetail" runat="server" Visible="false">
                                            <tr>
                                                <td></td>
                                                <td colspan="7">
                                                    <div style="display: none"><%#Eval("receipt_number") %></div>

                                                    <asp:Repeater ID="rptStockDetail" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-bordered" id="inner" cellspacing="0" rules="all" border="1"
                                                                style="margin-bottom: 0px;">
                                                                <tr>
                                                                    <th>Product Group</th>
                                                                    <th>Product</th>
                                                                    <th>Quantity</th>
                                                                    <th>Cost</th>
                                                                    <th>Tax</th>
                                                                    <th>Total Cost</th>
                                                                    <th>Receive Date</th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="background-color: #ffffff;"><%#Eval("product_group") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("product") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("total_stock") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("price") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("tax_amount") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("final_price") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("stock_date") %></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </td>

                                                <td style="display: none">
                                                    <div style="display: none"><%#Eval("total_price") %></div>
                                                </td>
                                                <td style="display: none">
                                                    <div style="display: none"><%#Eval("location") %></div>
                                                </td>
                                                <td style="display: none">
                                                    <div style="display: none"><%#Eval("Supplier") %></div>
                                                </td>
                                                <td style="display: none">
                                                    <div style="display: none"><%#Eval("Supplier_code") %></div>
                                                </td>
                                                <td style="display: none">
                                                    <div style="display: none"><%#Eval("stock_date") %></div>
                                                </td>
                                                <td style="display: none"></td>

                                                <td style="display: none"></td>
                                            </tr>
                                        </asp:Panel>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                            <tfoot>
                                <tr>
                                    <th></th>
                                    <th>Receipt Number</th>
                                    <th>Total</th>
                                    <th>Location</th>
                                    <th>Supplier</th>
                                    <th>Supplier Code</th>
                                    <th>Receive Date</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>

                                    </FooterTemplate>
                                </asp:Repeater>

                            </table>


                            <%--<telerik:RadGrid ID="rdStock" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                    PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                    PagerStyle-AlwaysVisible="true" Skin="MetroTouch" Width="100%" OnDetailTableDataBind="rdstock_DetailTableDataBind">
                                    <ExportSettings FileName="Device" IgnorePaging="false" ExportOnlyData="false">
                                    </ExportSettings>
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <SortingSettings EnableSkinSortStyles="false" />
                                    <MasterTableView DataKeyNames="stk_rec_id" AllowMultiColumnSorting="True" Font-Size="100%">
                                        <DetailTables>
                                            <telerik:GridTableView DataKeyNames="stk_rec_id" Name="stock" Width="100%" AllowPaging="false" AllowFilteringByColumn="false">
                                                <Columns>
                                                    <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Product Group"
                                                        SortExpression="product_group" DataField="product_group">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Product"
                                                        SortExpression="product" DataField="product">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Quantity"
                                                        SortExpression="total_stock" DataField="total_stock">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Cost"
                                                        SortExpression="price" DataField="price">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Tax"
                                                        SortExpression="tax_amount" DataField="tax_amount">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Total Cost"
                                                        SortExpression="final_price" DataField="final_price">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Receive Date"
                                                        SortExpression="stock_date" DataField="stock_date">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Receipt Number" UniqueName="receipt_number" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" SortExpression="receipt_number" AllowFiltering="false" DataField="receipt_number">
                                                <HeaderStyle Width="13%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="IbViewDetail" runat="server" CausesValidation="False" ToolTip="View Detail" ForeColor="Blue" Font-Underline="true"
                                                        CommandArgument='<%#Eval("receipt_number")%>' CommandName="ViewDetail"><%#Eval("receipt_number")%></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Total"
                                                SortExpression="total_price" DataField="total_price">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Location"
                                                SortExpression="location" DataField="location">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Supplier"
                                                SortExpression="Supplier" DataField="Supplier">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Supplier Code"
                                                SortExpression="Supplier_code" DataField="Supplier_code">
                                            </telerik:GridBoundColumn>


                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Receive Date"
                                                SortExpression="stock_date" DataField="stock_date">
                                            </telerik:GridBoundColumn>


                                            <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                <HeaderStyle Width="13%" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hf_isfinalsubmit" runat="server" Value='<%#Eval("is_finalsubmit")%>' />

                                                    <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Click to view Product Detail"
                                                        CommandArgument='<%#Eval("stk_rec_id")%>' CommandName="Edit" Visible="true"><i class="fa fa-edit fa-lg"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                        CommandArgument='<%#Eval("stk_rec_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                                        <i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>

                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>--%>

                            <br />
                            <br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 ">
                    <telerik:ReportViewer ID="ReportViewer1" runat="server" Style="border: 1px solid #ccc;"
                        Width="90%" Height="500px" Visible="false" Resources-ExportSelectFormatText="PDF" />
                </div>
            </div>
        </div>
    </div>
    <%-- </telerik:RadAjaxPanel>--%>


    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
            color: #ffffff;
            border: #111111 1px solid;
        }

        .row th {
            background-color: #ffffff !important;
        }

        body {
            font-size: 12px;
        }

        tr.group, tr.group:hover {
            background-color: #8fd6fd !important;
            color: #111111;
            border: #111111 1px solid;
            font-weight: bold;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 0px;
        }
    </style>

</asp:Content>



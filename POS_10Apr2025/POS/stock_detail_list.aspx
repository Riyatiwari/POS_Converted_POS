<%@ Page Title="Stock Changes List" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="stock_detail_list.aspx.vb" Inherits="stock_detail_list" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Stock Changes List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Stock Changes List</li>
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

        function expand() {

          
        }

        function Grid() {


            $("#Psummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#Psummary').DataTable({
                   "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel','pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [

                    { "visible": true, "targets": +groupCol }

                ],

                "displayLength": 50,
                "searching": true
            });


            var table1 = $('#Psummary').DataTable();

            $('#Psummary thead tr:eq(1) th').each(function (i) {

                var title = $(this).text();
                if (i == 5) {
                    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                }
                else {

                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }
                $('input', this).on('keyup', function () {

                    if (table1.column(i).search() !== this.value) {
                        table1

                            .column($(this).parent().index() + ':visible')
                            .search(this.value)
                            .draw()
                            ;

                    }
                    else {
                        table1
                            .column($(this).parent().index() + ':visible')
                            .search('')
                            .draw()
                            ;

                    }
                });
            });
        }
    </script>

    <script type="text/javascript">
        $("body").on("click", "[src*=plus]", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("body").on("click", "[src*=minus]", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
     <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />
    <div class="panel panel-yellow">
        <div class="panel-heading">Stock Changes List</div>
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
                        <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Stock Change</asp:LinkButton>
                    </div>
                </div>

                <br />
                <div class="row" id="divDevice" runat="server">
                    <div class="col-lg-12">
                        <div class="table-responsive">

                            <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                <asp:Repeater ID="rdStock" runat="server" OnItemCommand="rdStock_ItemCommand"
                                    OnItemDataBound="rdStock_ItemDataBound">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Receipt Number</th>
                                                <th>Stock Detail</th>
                                                <th>Location</th>
                                                <th>For Date</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <img alt="" style="cursor: pointer" src="images/plus.png" />
                                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                    <asp:Repeater ID="rptOrders" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-bordered" id="inner" cellspacing="0" rules="all" border="1">
                                                                <tr>
                                                                    <th>Product Group</th>
                                                                    <th>Product</th>
                                                                    <th>Starting Qty</th>
                                                                    <th>Counted QTY</th>
                                                                    <th>Result</th>
                                                                    <th>For Date</th>
                                                                </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td style="background-color: #ffffff;"><%#Eval("product_group") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("product") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("Stk_on_hand") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("t_stock") %></td>

                                                                <td style="background-color: #ffffff;"><%#Eval("qty") %></td>
                                                                <td style="background-color: #ffffff;"><%#Eval("stock_date") %></td>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </asp:Panel>
                                                <asp:HiddenField ID="hf_stk_chg_rec_id" runat="server" Value='<%# Eval("stk_chg_rec_id") %>' />
                                            </td>
                                            <td style="background-color: #ffffff;"><%#Eval("receipt_number") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Stock_detail") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("location") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("stock_date") %></td>

                                            <td style="background-color: #ffffff;">
                                                <asp:HiddenField ID="hf_isfinalsubmit" runat="server" Value='<%#Eval("is_finalsubmit")%>' />

                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Click to view Product Detail"
                                                    CommandArgument='<%#Eval("stk_chg_rec_id")%>' CommandName="Edit" Visible="true"><i class="fa fa-edit fa-md"></i>
                                                </asp:LinkButton>

                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                            CommandArgument='<%#Eval("stk_chg_rec_id")%>' CommandName="Delete" 
                                                            OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                                            <i class="fa fa-trash fa-md"></i></asp:LinkButton>


                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                            <tfoot>
                                <tr>
                                    <th></th>
                                    <th>Receipt Number</th>
                                    <th>Stock Detail</th>
                                    <th>Location</th>
                                    <th>For Date</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>

                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>


                            <%--<telerik:RadGrid ID="rdStock" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                PagerStyle-AlwaysVisible="true" Skin="MetroTouch" Width="100%" OnDetailTableDataBind="RadGrid1_DetailTableDataBind">
                                <ExportSettings FileName="Device" IgnorePaging="false" ExportOnlyData="true">
                                </ExportSettings>
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="stk_chg_rec_id" AllowMultiColumnSorting="True" Font-Size="100%">
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="stk_chg_rec_id" Name="stock" Width="100%" AllowPaging="false" AllowFilteringByColumn="false">
                                            <Columns>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Product Group"
                                                    SortExpression="product_group" DataField="product_group">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Product"
                                                    SortExpression="product" DataField="product">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Quantity"
                                                    SortExpression="t_stock" DataField="t_stock" UniqueName="t_stock">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Result Qty"
                                                    SortExpression="qty" DataField="qty">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="For Date"
                                                    SortExpression="stock_date" DataField="stock_date">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Receipt Number" UniqueName="receipt"
                                            SortExpression="receipt_number" DataField="receipt_number">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Stock Detail"
                                            SortExpression="Stock_detail" DataField="Stock_detail" UniqueName="Stock_detail">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Location"
                                            SortExpression="location" DataField="location">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="For Date"
                                            SortExpression="stock_date" DataField="stock_date" UniqueName="stock_date">
                                        </telerik:GridBoundColumn>


                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                            <ItemTemplate>


                                                <asp:HiddenField ID="hf_isfinalsubmit" runat="server" Value='<%#Eval("is_finalsubmit")%>' />

                                                <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Click to view Product Detail"
                                                    CommandArgument='<%#Eval("stk_chg_rec_id")%>' CommandName="Edit" Visible="true"><i class="fa fa-edit fa-lg"></i>
                                                </asp:LinkButton>

                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                            CommandArgument='<%#Eval("stk_chg_rec_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
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
            </div>
        </div>
    </div>

    <%--  </telerik:RadAjaxPanel>--%>


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



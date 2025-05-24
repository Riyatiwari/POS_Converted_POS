<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Sales_View.aspx.vb" Inherits="Sales_View" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Sales View
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Sales_List.aspx">Sales List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Sales View</li>
        </ol>
    </div>
    <style type="text/css">
        .rlbItem {
            float: left !important;
        }

        .rlbGroup, .RadListBox {
            width: auto !important;
        }

        /*#RadListBox1 {
            border: 1px solid black;
        }*/

        .RadListBox_Silk .rlbGroup {
            border: none !important;
        }
    </style>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#Lsummary thead tr').clone(true).appendTo('#Lsummary thead');

            Grid();

        });

        function Grid() {


            $("#Lsummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#Lsummary').DataTable({

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel', 'pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    //{
                    //    "searchable": false, "targets": 1,
                    //    "visible": false
                    //    , "targets": +groupCol,
                    //"render": function (data, type, full, meta) {
                    //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    //}
                    { "visible": true, "targets": +groupCol },
                    {
                        'targets': [2], /* column index */
                        'orderable': false,
                    },
                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 50,
                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;

                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {

                        if (last !== group) {
                            $(rows).eq(i).before(
                                //'<tr class="group"><td colspan="8"> Location Group : ' + group + '</td> </tr>'
                            );
                            last = group;
                        }
                    });
                }
            });

            var table1 = $('#Lsummary').DataTable();
            $('#Lsummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                if (i == 11) {
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

    <script lang="javascript" type="text/javascript">

        function openNewWin(url) {
            var x = window.open(url, 'Sales View Detail', 'width=600,height=600,toolbar=1');
            x.focus();
        }
    </script>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        --%>
        <%--<asp:Button ID="btnReSync" runat="server"  class="btn btn-primary" ToolTip="ReSync" text="Re Sync"/>--%>


        <div class="panel panel-yellow">
            <div class="panel-heading">Sales View</div>
            <br />

            <div class="panel-body pan">

                <div class="form-body pal">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <label>Total Amount : </label>
                                    <label runat="server" id="lbltAmt"></label>
                                </div>

                                <div class="col-md-3">
                                    <label>Total Discount : </label>
                                    <label runat="server" id="lblTDisc"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Payment Type : </label>
                                    <label runat="server" id="lblSType"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Tender : </label>
                                    <label runat="server" id="lblReceivedAmount"></label>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-3">
                                    <label>Net Amount : </label>
                                    <label runat="server" id="lblnetAmt"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Created Date :</label>
                                    <label runat="server" id="lblCreatedDt"></label>

                                </div>
                                <div class="col-md-3">
                                    <label>Operator : </label>
                                    <label runat="server" id="lblLogin"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Change : </label>
                                    <label runat="server" id="lblchange"></label>
                                </div>
                                <br />
                                <br />
                                <div class="clearfix"></div>
                                <div class="col-md-3">
                                    <label>Location : </label>
                                    <label runat="server" id="lblLocation"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Name :</label>
                                    <label runat="server" id="lblwName"></label>

                                </div>
                                <div class="col-md-3">
                                    <label>Mobile : </label>
                                    <label runat="server" id="lblwMobile"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Email : </label>
                                    <label runat="server" id="lblwEmail"></label>
                                </div>
                                <br />
                                <br />
                                <div class="clearfix"></div>
                                <div class="col-md-3">
                                    <label>Address : </label>
                                    <label runat="server" id="lblwaddress"></label>
                                </div>
                                <br />
                                <br />
                                <div class="clearfix"></div>
                                <div class="col-md-3">
                                    <label>Surcharge Amount : </label>
                                    <label runat="server" id="lblsurcharge_amount"></label>
                                </div>

                                <div class="col-md-3">
                                    <label>No. Of Covers : </label>
                                    <label runat="server" id="lblNoOfCovers"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Eat Out Amount : </label>
                                    <label runat="server" id="lblEatOutAmount"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Transaction Count : </label>
                                    <label runat="server" id="lbltransaction_count"></label>
                                </div>
                                <br />
                                <br />
                                <div class="clearfix"></div>
                                <div class="col-md-3">
                                    <label>Cash Table : </label>
                                    <label runat="server" id="lblis_cash_table"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Table Id : </label>
                                    <label runat="server" id="lbltable_id"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Shift Name : </label>
                                    <label runat="server" id="lblshift_name"></label>
                                </div>
                                <div class="col-md-3">
                                    <label>Transaction Type : </label>
                                    <label runat="server" id="lblTransType"></label>
                                    <asp:HiddenField runat="server" ID="HdnRef_id" />
                                    <asp:HiddenField runat="server" ID="HdnMachine_id" />
                                </div>
                                <br />
                                <br />
                                <div class="col-md-3">
                                    <label>Ref. No : </label>
                                    <label runat="server" id="lblRefNo"></label>
                                </div>

                                <div class="col-md-3" runat="server">
                                    <asp:Label runat="server" ID="lblWsname" Visible="false">Web Sales Sync  :</asp:Label>
                                    <label runat="server" id="lblSyncStatus" visible="false"></label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="clearfix"></div>
                        <br />

                        <div class="row">
                            <div class="col-lg-12 ">
                                <div style="display: none;">
                                    <hr />
                                    &nbsp&nbsp<asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">Re Sync</asp:LinkButton>
                                    &nbsp&nbsp<asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-primary" ToolTip="RePrint">Re Print</asp:LinkButton>
                                </div>
                                <br />
                                <br />
                                <div class="table-responsive" style="width: 100%; overflow-x: auto">

                                    <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                        <asp:Repeater ID="rdTSales" runat="server">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                       <%-- <th>Sr No.</th>--%>
                                                        <th>Product Name</th>
                                                        <th>Quantity</th>
                                                        <th>Total Amount</th>
                                                        <th>Discount</th>
                                                        <th>Net Amount</th>
                                                        <th>Tax</th>
                                                        <th>Size</th>
                                                        <th>Price</th>
                                                        <th>Discount Name</th>
                                                        <th>Discount Mode</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%--<td style="background-color: #ffffff;"><%#Eval("number")%></td>--%>
                                                    <td style="background-color: #ffffff;"><%#Eval("name")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("quntity")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("total_amount")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("discount")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("net_amount")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("tax_amount")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Size")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Price")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Tsale_discount_name")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Tsale_discount_mode_type")%></td>
                                                    <td style="background-color: #ffffff;">
                                                        <asp:HiddenField ID="hftsales_id" runat="server" Value='<%#Eval("tsales_id")%>' />
                                                        <%--<asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" 
                                                            CommandArgument='<%#Eval("tsales_id")%>' CommandName="ViewIngredientCondiment" OnClientClick="return true;">
                                                            <i class="fa fa-search fa-lg"></i></asp:LinkButton>--%>

                                                        <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("tsales_id").ToString() %>' runat="server" 
                                                            ToolTip="View Ingredient and Condiment Details" OnClick="lnkview_Click">
                                                            <i class="fa fa-search" style="cursor: pointer" aria-hidden="true"></i>
                                                        </asp:LinkButton>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>
                                    <%--<th>Sr No.</th>--%>
                                    <th>Product Name</th>
                                    <th>Quantity</th>
                                    <th>Total Amount</th>
                                    <th>Discount</th>
                                    <th>Net Amount</th>
                                    <th>Tax</th>
                                    <th>Size</th>
                                    <th>Price</th>
                                    <th>Discount Name</th>
                                    <th>Discount Mode</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>

                                    <%--<telerik:RadGrid ID="rdTSales" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                        PageSize="10" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                        PagerStyle-AlwaysVisible="false" Skin="MetroTouch">
                                        <ExportSettings FileName="Sales" IgnorePaging="false" ExportOnlyData="true">
                                        </ExportSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <SortingSettings EnableSkinSortStyles="false" />
                                        <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataSetIndex+1 %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Product Name"
                                                    SortExpression="name" DataField="name">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Quantity"
                                                    SortExpression="quntity" DataField="quntity" DataType="System.String">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Total Amount"
                                                    SortExpression="total_amount" DataField="total_amount" DataType="System.String">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Discount"
                                                    SortExpression="discount" DataField="discount" DataType="System.String">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Net Amount"
                                                    SortExpression="net_amount" DataField="net_amount" DataType="System.String">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Tax"
                                                    SortExpression="tax_amount" DataField="tax_amount" DataType="System.String">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Size"
                                                    SortExpression="Size" DataField="Size" DataType="System.String">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Price"
                                                    SortExpression="Price" DataField="Price" DataType="System.String">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Discount Name"
                                                    SortExpression="Tsale_discount_name" DataField="Tsale_discount_name">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Discount Mode"
                                                    SortExpression="Tsale_discount_mode_type" DataField="Tsale_discount_mode_type">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hftsales_id" runat="server" Value='<%#Eval("sales_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="View Ingredient and Condiment Details"
                                                            CommandArgument='<%#Eval("tsales_id")%>' CommandName="ViewIngredientCondiment" OnClientClick="return true;"><i class="fa fa-search fa-lg"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>--%>

                                    <%--<telerik:RadWindowManager ID="RadWindowManager3" runat="server" Width="100%" Height="100%">
                                        <Windows>
                                            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" Height="450%" Width="600%"
                                                VisibleStatusbar="false" Title="Ingredients and Condiments" VisibleTitlebar="true" Skin="Metro" Behaviors="Close">
                                                <ContentTemplate>
                                                    <div>
                                                        <br />
                                                        <div class="panel panel-yellow">
                                                            <div class="panel-heading">Ingredients</div>
                                                            <telerik:RadGrid ID="rdIngredient" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                                PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                                                PagerStyle-AlwaysVisible="true" EnablePostBackOnRowClickproperty="true" Skin="MetroTouch" Width="100%"
                                                                AllowMultiRowSelection="true" Visible="False">

                                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn HeaderText="Ingredient" UniqueName="product">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIngredient" runat="server" Text='<%# Eval("Product")%>' />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Size" UniqueName="size">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsize" runat="server" Text='<%# Eval("Size")%>' />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </div>
                                                        <div>
                                                            <div class="panel panel-yellow">
                                                                <div class="panel-heading">Condiments</div>
                                                                <telerik:RadListBox RenderMode="Lightweight" ID="radListCondiment" runat="server">
                                                                    <ItemTemplate>
                                                                        <img src="images/checkbox.png" />
                                                                        <asp:Label ID="lblCondiment" runat="server" Text='<%#Eval("Condiment")%>' />
                                                                    </ItemTemplate>

                                                                </telerik:RadListBox>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                </ContentTemplate>
                                            </telerik:RadWindow>
                                        </Windows>
                                    </telerik:RadWindowManager>--%>

                                    <br />

                                    <div class="table-responsive" runat="server" id="divcard" style="overflow: scroll" visible="false">

                                        <table class="table table-bordered" id="Csummary" width="100%" cellspacing="0">
                                            <asp:Repeater ID="rdcard" runat="server">
                                                <HeaderTemplate>
                                                    <thead>
                                                        <tr>
                                                            <th>Card Holder</th>
                                                            <th>Expiration Date</th>
                                                            <th>Pan</th>
                                                            <th>Card Type</th>
                                                            <th>Amount</th>
                                                            <th>ISO Code</th>
                                                            <th>Status Message</th>
                                                            <th>Transaction Id</th>
                                                            <th>Payment Account Data Token</th>
                                                            <th>Retrival Reference No</th>
                                                            <th>Merchant Id</th>
                                                            <th>Terminal Id</th>
                                                            <th>Created Date</th>
                                                            <th>Card Method</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="background-color: #ffffff;"><%#Eval("card_holdername")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("expiration_date")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("pan")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("card_type")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("amount")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("type_ISO_currency_code")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("status_message")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("transaction_id")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("retrieval_reference_number")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("merchant_id")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("terminal_id")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("cd")%> </td>
                                                        <td style="background-color: #ffffff;"><%#Eval("card_method_type")%> </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                            <tfoot>
                                <tr>
                                    <th>Card Holder</th>
                                    <th>Expiration Date</th>
                                    <th>Pan</th>
                                    <th>Card Type</th>
                                    <th>Amount</th>
                                    <th>ISO Code</th>
                                    <th>Status Message</th>
                                    <th>Transaction Id</th>
                                    <th>Payment Account Data Token</th>
                                    <th>Retrival Reference No</th>
                                    <th>Merchant Id</th>
                                    <th>Terminal Id</th>
                                    <th>Created Date</th>
                                    <th>Card Method</th>
                                </tr>
                                </table>
                            </tfoot>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>

                                        <%--<telerik:RadGrid ID="rdcard" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                               AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                               PagerStyle-AlwaysVisible="false" Skin="MetroTouch" Visible="false">
                                               <ExportSettings FileName="Sales" IgnorePaging="false" ExportOnlyData="true">
                                               </ExportSettings>
                                               <PagerStyle Mode="NextPrevAndNumeric" />
                                               <GroupingSettings CaseSensitive="false" />
                                               <SortingSettings EnableSkinSortStyles="false" />
                                               <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true">
                                                   <Columns>

                                                       <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="No.">

                                                           <ItemTemplate>

                                                               <%# Container.DataSetIndex + 1 %>
                                                           </ItemTemplate>
                                                       </telerik:GridTemplateColumn>


                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Card Holder"
                                                           SortExpression="card_holdername" DataField="card_holdername" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Expiration Date"
                                                           SortExpression="expiration_date" DataField="expiration_date" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Pan"
                                                           SortExpression="pan" DataField="pan" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Card Type"
                                                           SortExpression="card_type" DataField="card_type" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Amount"
                                                           SortExpression="amount" DataField="amount" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="ISO Code"
                                                           SortExpression="type_ISO_currency_code" DataField="type_ISO_currency_code" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Status Message"
                                                           SortExpression="status_message" DataField="status_message" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Transaction Id"
                                                           SortExpression="transaction_id" DataField="transaction_id" DataType="System.String">
                                                       </telerik:GridBoundColumn>

                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Payment Account Data Token"
                                                           SortExpression="payment_account_data_token" DataField="payment_account_data_token" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Retrival Reference No"
                                                           SortExpression="retrieval_reference_number" DataField="retrieval_reference_number" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Merchant Id"
                                                           SortExpression="merchant_id" DataField="merchant_id" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Terminal Id"
                                                           SortExpression="terminal_id" DataField="terminal_id" DataType="System.String">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Created Date"
                                                           SortExpression="created_date" DataField="cd" DataType="System.DateTime">
                                                       </telerik:GridBoundColumn>
                                                       <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Card Method"
                                                           SortExpression="card_method_type" DataField="card_method_type" DataType="System.Int32">
                                                       </telerik:GridBoundColumn>

                                                   </Columns>
                                               </MasterTableView>
                                           </telerik:RadGrid>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


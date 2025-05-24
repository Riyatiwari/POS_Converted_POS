<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_Transaction_Detail.aspx.vb" Inherits="Product_Transaction_Detail" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Product Transaction Detail
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">

    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Product Transaction Detail</li>
        </ol>
    </div>

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
                    'excel', 'pdf'
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
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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

            $('input[type="search"]').on('keyup', function () {
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }
    </script>

    
    <script lang="javascript" type="text/javascript">

        function openNewWin(url) {
            var x = window.open(url, 'Table Transaction Report Detail', 'width=600,height=600,toolbar=1');
            x.focus();
        }
    </script>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Product Transaction Detail</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <div class="row" id="divPGroup" runat="server" visible="false">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdProductDetail" runat="server">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Product Name</th>
                                                    <th>Till</th>
                                                    <th>Operator</th>
                                                    <th>Qty</th>
                                                    <th>Amount</th>
                                                    <th>Discount</th>
                                                    <th>Transaction ID</th>
                                                    <th>Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("Product_name") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("till") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("operator") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("quntity") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("sales_discount") %></td>
                                                 <td style="background-color: #ffffff;">
                                                     <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("tran_uuid") %> ' runat="server" OnClick="lnkview_Click">
                                                       <%#Eval("tran_uuid") %>  </asp:LinkButton>
                                                    </td>
                                                <td style="background-color: #ffffff;"><%#Eval("t_date") %></td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>

                                    <th>Product Name</th>
                                    <th>Till</th>
                                    <th>Operator</th>
                                    <th>Qty</th>
                                    <th>Amount</th>
                                    <th>Discount</th>
                                    <th>Transaction ID</th>
                                    <th>Date</th>

                                </tr>
                                </table>
                            </tfoot>

                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>

                            </div>
                        </div>
                    </div>


                    <div class="row" id="divOther" runat="server" visible="false">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdProductDetailOther" runat="server">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Product Name</th>
                                                    <th>Till</th>
                                                    <th>Operator</th>
                                                    <th>Qty</th>
                                                    <th>Amount</th>
                                                    <th>Discount</th>
                                                    <th>Transaction ID</th>
                                                    <th>Date</th>
                                                    <th>Sale Type</th>
                                                    <th>Parent Product</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("Product_name") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("till") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("operator") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("quntity") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("sales_discount") %></td>
                                                <td style="background-color: #ffffff;">
                                                    <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("tran_uuid") %> ' runat="server" OnClick="lnkview_Click">
                                                       <%#Eval("tran_uuid") %>  </asp:LinkButton>
                                                    </td>
                                                <td style="background-color: #ffffff;"><%#Eval("t_date") %></td>
                                                 <td style="background-color: #ffffff;"><%#Eval("sale_type") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Parent_Product") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>

                                    <th>Product Name</th>
                                    <th>Till</th>
                                    <th>Operator</th>
                                    <th>Qty</th>
                                    <th>Amount</th>
                                    <th>Discount</th>
                                    <th>Transaction ID</th>
                                    <th>Date</th>
                                    <th>Sale Type</th>
                                    <th>Parent Product</th>

                                </tr>
                                </table>
                            </tfoot>

                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <asp:HiddenField runat="server" ID="hdnsearchvalue" />
    <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />


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


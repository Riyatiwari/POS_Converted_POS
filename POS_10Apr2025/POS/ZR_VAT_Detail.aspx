<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ZR_VAT_Detail.aspx.vb" Inherits="ZR_VAT_Detail" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    VAT Detail
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Tax List</li>
        </ol>
    </div>


    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#VAT_Detail thead tr').clone(true).appendTo('#VAT_Detail thead');

            Grid();

        });

        function Grid() {


            $("#VAT_Detail tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#VAT_Detail').DataTable({


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


            var table1 = $('#VAT_Detail').DataTable();

            $('#VAT_Detail thead tr:eq(1) th').each(function (i) {

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
        }
    </script>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <div class="col-lg-12">
        <div class="panel panel-yellow">
            <div class="panel-heading">VAT Detail</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row" id="divCustomer" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="VAT_Detail" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdVAT_Detail" runat="server">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Product</th>
                                                    <th>QTY</th>
                                                    <th>Size</th>
                                                    <th>Price</th>
                                                    <th>VAT</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("P_name") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Qty") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Size") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("VAT") %></td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <th>Product</th>
                                    <th>QTY</th>
                                    <th>Size</th>
                                    <th>Price</th>
                                    <th>VAT</th>
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
    </div>

    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
         
        ffff;
        der: #111111 1px olid;
        }

        r

        color: #ff fff ! body {
            12px;
        }

        tr.group
        er {
            color: 8fd6fd imprtant;
            color: #1111 1;
            111 1px solid;
            font-weight: old;
            table data d .dataTable thead td

        {
            padding: 0px;
        }
    </style>
</asp:Content>


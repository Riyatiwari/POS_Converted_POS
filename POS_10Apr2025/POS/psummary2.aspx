<%@ Page Language="VB" AutoEventWireup="false" CodeFile="psummary2.aspx.vb" Inherits="psummary2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Summary</title>

    <%--<link href="vendor2/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" />--%>
    <!-- Custom fonts for this template -->
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <%--<script src="vendor2/jquery/jquery.min.js"></script>
    <script src="vendor2/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.min.js"></script>


     
    <script src="vendor2/jquery-easing/jquery.easing.min.js"></script>

     
    <script src="js2/sb-admin-2.min.js"></script>

   
    <script src="vendor2/datatables/jquery.dataTables.min.js"></script>
    <script src="vendor2/datatables/dataTables.bootstrap4.min.js"></script>--%>

    <!-- Page level custom scripts -->
    <%--<script src="js2/demo/datatables-demo.js"></script>
    <script type="text/javascript" language="javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>--%>
    <link href="css2/sb-admin-2.min.css" rel="stylesheet" />
    <%--<link href="vendor2/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" />--%>
    <!-- Custom fonts for this template -->
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <%--<script src="vendor2/jquery/jquery.min.js"></script>
    <script src="vendor2/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.min.js"></script>--%>


    <!-- Core plugin JavaScript-->
    <%--<script src="vendor2/jquery-easing/jquery.easing.min.js"></script>--%>
    <script src="https://cdn.datatables.net/fixedheader/3.1.6/js/dataTables.fixedHeader.min.js"></script>

    <!-- Custom scripts for all pages-->
    <%-- <script src="js2/sb-admin-2.min.js"></script>--%>

    <!-- Page level plugins -->
    <%--  <script src="vendor2/datatables/jquery.dataTables.min.js"></script>
    <script src="vendor2/datatables/dataTables.bootstrap4.min.js"></script>--%>

    <!-- Page level custom scripts -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>
    
    <%--<script src="https://code.jquery.com/jquery-3.3.1.js"></script>--%>




    <%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.jqueryui.min.js"></script>--%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.css" />

    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.js"></script>

        <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.flash.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.html5.min.js"></script>

    
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" />


    <script type="text/javascript">

        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');


            Grid();

        });


        function Grid() {

            var currency = "";
            $("#Psummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = $("input[name='groupby']:checked").val();

            var table = $('#Psummary').DataTable({

                orderCellsTop: true,
                 dom: 'Bfrtip',
        "buttons": [
            'excel', 'pdf' 
        ],


                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 25,


                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;
                    var subTotal = new Array();
                    var groupID = -1;
                    var aData = new Array();
                    var index = 0;

                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {

                        console.log(group + ">>>" + i);
                        
                        var vals = api.row(api.row($(rows).eq(i)).index()).data();
                        var salary1 = vals[7] ? (vals[7]) : 0;
                         
                        currency = salary1.charAt(0);
                        var salary = parseFloat(salary1.replace("$", "").replace("£", "").replace(",", ""));
                        
                        if (typeof aData[group] == 'undefined') {
                            aData[group] = new Array();
                            aData[group].rows = [];
                            aData[group].salary = [];
                        }

                        aData[group].rows.push(i);
                        aData[group].salary.push(salary);

                    });


                    var idx = 0;



                    for (var ProductGroup in aData) {

                        idx = Math.max.apply(Math, aData[ProductGroup].rows);
                        
                        var sum = 0.0;
                        $.each(aData[ProductGroup].salary, function (k, v) {
                            sum = sum + v;
                            
                        });

                        //console.log(aData[ProductGroup].salary);
                        $(rows).eq(idx).after(
                            '<tr class="group"><td colspan="8"> Product Group : ' + ProductGroup + '</td>' +
                            '<td  colspan="2">Total :  ' + currency + ' '  + sum + '</td></tr>'
                        );

                    };



                }
            });


            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

                $('input', this).on('keyup change', function () {

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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="float: right; margin-right: 20px;">
                <table>

                    <tr>
                        <td><b>Group By :   </b>&nbsp;&nbsp;&nbsp;</td>

                        <td>
                            <input type="radio" id="chkPG" name="groupby" checked="checked" value="0" onclick="Grid()" />
                            Product Group </td>
                        <td>
                            <input type="radio" id="chkP" name="groupby" value="1" onclick="Grid()" />
                            Product  </td>
                    </tr>
                </table>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                        <asp:Repeater ID="rptProductSUmmary" runat="server">
                            <HeaderTemplate>

                                <thead>
                                    <tr>
                                        <th>ProductGroup</th>
                                        <th>Product</th>
                                        <th>Price</th>
                                        <th>Sales Qty</th>
                                        <th>Return Qty</th>
                                        <th>TotalAmount</th>
                                        <th>Discount</th>
                                        <th>NetAmount</th>
                                        <th>Total Tax</th>
                                        <th>Volume Sold</th>
                                        <th>Qty SOld</th>
                                    </tr>
                                </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("ProductGroup") %></td>
                                    <td><%#Eval("Pname") %></td>
                                    <td><%#Eval("price") %></td>
                                    <td><%#Eval("Sale_qunt") %></td>
                                    <td><%#Eval("return_qunt") %></td>
                                    <td><%#Eval("Total_amt") %></td>
                                    <td><%#Eval("Discount") %></td>
                                    <td><%#Eval("Net_amt") %></td>
                                    <td><%#Eval("Sales_tax_amount") %></td>
                                    <td><%#Eval("Volume_sold") %></td>
                                    <td><%#Eval("Qty_Sold") %></td>

                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            <tfoot>
                                <tr>
                                    <th>Product Group</th>
                                    <th>Product</th>
                                    <th>Price</th>
                                    <th>Sales Qty</th>
                                    <th>Return Qty</th>
                                    <th>Total Amount</th>
                                    <th>Discount</th>
                                    <th>Net Amount</th>
                                    <th>Total Tax</th>
                                    <th>Volume Sold</th>
                                    <th>Qty SOld</th>
                                </tr>
                                </table>
                            </tfoot>

                            </FooterTemplate>
                        </asp:Repeater>
                </div>
            </div>
        </div>
    </form>


    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #a3e4f7 !important;
        }
    </style>

</body>
</html>

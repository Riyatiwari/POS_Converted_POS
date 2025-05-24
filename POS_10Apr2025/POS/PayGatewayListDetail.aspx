<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PayGatewayListDetail.aspx.vb" Inherits="PayGatewayListDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
    <link rel="shortcut icon" href="images/icons/favicon.ico" />
    <link rel="apple-touch-icon" href="images/icons/favicon.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="images/icons/favicon-72x72.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="images/icons/favicon-114x114.png" />
    <link href="css/fonts/OpenSans.css" rel="stylesheet" />
    <link href="css/fonts/Oswald.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet"
        href="vendors/jquery-ui-1.10.4.custom/css/ui-lightness/jquery-ui-1.10.4.custom.min.css" />
    <link type="text/css" rel="stylesheet" href="vendors/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="vendors/bootstrap/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" class="default-style" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" class="style-change color-change" />
    <link type="text/css" rel="stylesheet" href="css/style-responsive.css" />
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
        }
    </script>

    <script lang="javascript" type="text/javascript">

        function openNewWin(url) {
            var x = window.open(url, 'Payment Gateway Detail', 'width=800,height=600,toolbar=1');
            x.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-yellow" style="padding: 10px;">
            <div>
                <asp:HiddenField runat="server" ID="hd_GTotal" />
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">

                        <asp:Repeater ID="rptListDetail" runat="server">
                            <HeaderTemplate>
                                <thead>
                                    <tr>
                                        <th>Resaller</th>
                                        <th>Merchant</th>
                                        <th>Terminal</th>
                                        <th>Date</th>
                                        <th>Time</th>
                                        <th>Ref</th>
                                        <th>Amount</th>
                                        <th>CardType</th>
                                        <th>AuthMessage</th>
                                    </tr>
                                </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="background-color: #ffffff;"><%#Eval("Resaller") %></td>
                                    <td style="background-color: #ffffff;"><%#Eval("Merchant") %></td>
                                    <td style="background-color: #ffffff;"><%#Eval("Terminal") %></td>
                                    <td style="background-color: #ffffff;"> <%# Eval("Date", "{0:dd/MM/yyyy}") %></td>
                                    <td style="background-color: #ffffff;"><%#Eval("Time") %></td>
                                    <td style="background-color: #ffffff;">
                                        <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("Ref").ToString() %>' runat="server" OnClick="lnkview_Click">
                                                         <%#Eval("Ref") %>               
                                        </asp:LinkButton>
                                    </td>
                                    <td style="background-color: #ffffff;"><%#Eval("Amount") %></td>
                                    <td style="background-color: #ffffff;"><%#Eval("Card_Type") %></td>
                                    <td style="background-color: #ffffff;"><%#Eval("AuthMessage") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            <tfoot>
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


    <script src="js/jquery-1.10.2.min.js"></script>
    <script src="js/jquery-migrate-1.2.1.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="vendors/bootstrap/js/bootstrap.min.js"></script>
    <script src="vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js"></script>
    <script src="js/main.js"></script>
    <script src="js/index.js"></script>
    <script src="js/wo/js/datatables.min.js"></script>
    <script src="js/wo/js/jquery.dataTables.min.js"></script>
    <script src="js/wo/js/dataTables.buttons.min.js"></script>
    <script src="js/wo/js/flash.min.js"></script>
    <script src="js/wo/js/jszip.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="js/wo/js/buttons.html5.min.js"></script>
    <link href="js/wo/css/dataTables.min2.css" rel="stylesheet" />
    <link href="js/wo/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="js/wo/css/datatables.min.css" rel="stylesheet" />
</body>
</html>

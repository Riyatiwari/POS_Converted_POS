<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Table_Transaction_Detail.aspx.vb" Inherits="Table_Transaction_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
    <link type="text/css" rel="stylesheet" href="vendors/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="vendors/bootstrap/css/bootstrap.min.css" />

    <!--Loading style-->
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" />
    <link type="text/css" rel="stylesheet" href="css/style-responsive.css" />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" />


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

            var tableTemp = $('#Psummary').DataTable();
            
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-yellow" style="padding: 10px;">
            <div class="col-md-12" runat="server" id="header" style="padding-top: 10px; width: 100%;">

                <table class="table table-bordered" width="100%" cellspacing="0">
                    <tbody>
                        <tr>
                            <td>Table name :
                <asp:Label ID="lbltable_name" runat="server" Text=""></asp:Label>
                            </td>
                            <td>Payment type :
                <asp:Label ID="lblpayment_type" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Ref no :
                <asp:Label ID="lblRef_No" runat="server" Text=""></asp:Label>

                            </td>
                             <td>Payment amount :
                <asp:Label ID="lblpayment_amt" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br />
            <div class="col-md-12" style="padding: 10px;">

                <asp:Literal ID="ltTable" runat="server" />

            </div>

            <div class="col-md-12" runat="server" id="footer" style="padding: 10px; background-color: #B8DBFD !important; color: #777">
                <br />
                <div style="text-align: left; padding-left: 20px;">
                    <%--   Table Name :
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <br />--%>
                    Table Status :
                <asp:Label ID="lblTableStatus" runat="server" Text=""></asp:Label>
                    <br />
                    Payment Amount :
                <asp:Label ID="lblPaymentamount" runat="server" Text=""></asp:Label>
                    <br />

                    Surcharge Amount :
                <asp:Label ID="lblSurcharge" runat="server" Text=""></asp:Label>
                    <br />
                    Payment Type :
                <asp:Label ID="lblPayment" runat="server" Text=""></asp:Label>
                      <br />
                    Total Change :
                <asp:Label ID="lblChange" runat="server" Text=""></asp:Label>
                    <br />
                    Tip Amount :
                <asp:Label ID="lblTip" runat="server" Text=""></asp:Label>
                </div>
                <br />
            </div>

        </div>
    </form>
</body>
</html>

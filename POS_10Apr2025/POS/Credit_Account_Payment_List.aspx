<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Credit_Account_Payment_List.aspx.vb" Inherits="Credit_Account_Payment_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Credit Account Payment List
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Credit Account Payment List</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>


    <script type="text/javascript" language="javascript">
        function redirect(id) {

            //alert(id);
            //sessionStorage.setItem("product_id",id);  
            //alert(sessionStorage.getItem("product_id"));

            //localStorage.setItem("product_id", id);


            //location.href = "Product_Master.aspx";
            //var val = $('[id*="hdProId"]').val();
            //alert($.session.get("product_id"));
            //var value = sessionStorage.getItem("product_id");            
            //location.href = "Product_Master.aspx";
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

        function checkCondiment() {
            var txt;
            var r = confirm("Do you want to copy all stores ?");
            if (r == true) {
                var z = confirm("Do you want to copy condiments ?");
                if (z == true) {

                    document.getElementById('<%= hdCondiments.ClientID %>').value = "1";
                    return true;
                }
                else {

                    document.getElementById('<%= hdCondiments.ClientID %>').value = "0";
                    return true;
                }
            } else {
                return false;
            }
            return false;
        }
    </script>



    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnDownload") >= 0 ||
                args.get_eventTarget().indexOf("btnDownload") >= 0 ||
                args.get_eventTarget().indexOf("btnDownload") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>

    <script type="text/javascript">

        $(document).ready(function () {


            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid();
            //GridSize();
            //GridCondiment();
            //GridBarcode();
            //GridPrinter();

            $("[id*=lblSzName0]").hide();
            $("[id*=lblPrc0]").hide();
            $("[id*=lblSize0]").hide();
            $("[id*=lblUnit0]").hide();

            $("[id*=lblClickAndCollect0]").hide();
            $("[id*=lblDeliver0]").hide();
            $("[id*=lblOrderAtTable0]").hide();

            $("[id*=lblTx0]").hide();


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

                //orderCellsTop: true,
                //dom: 'Bfrtip',
                //"buttons": [
                //    'excel', 'pdf'

                //],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": true, "targets": + groupCol }
                ],

                "order": [[groupCol, 'asc']],
                "displayLength": 50,
                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;

                    api.column(groupCol, { page: 'current' }).data().each(
                        function (group, i) {

                            //if (last !== group) {
                            //    $(rows).eq(i).before(
                            //        '<tr class="group"><td colspan="5"> Product Group : ' + group + '</td> </tr>'

                            //    );
                            //    last = group;
                            //}
                        });
                }
            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                stateSave: true
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


            $('input[type="search"]').on('keyup', function () {
                // alert($(this).val());
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }

    </script>



</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Credit Account Payment List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnResponseStart="OnRequestStart">
                        <div class="row" id="divPGroup" runat="server">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:Panel runat="server" ID="PnlPsummary">
                                        <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                            <asp:Repeater ID="rdCustomer" runat="server">
                                                <HeaderTemplate>
                                                    <thead>
                                                        <tr>
                                                            <th>Customer Name</th>
                                                            <th>Contact</th>
                                                            <th>Amount</th>
                                                            <%-- <th>Pay UUID</th>--%>
                                                            <th>Credit Date</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="background-color: #ffffff;"><%#Eval("full_name") %></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("customer_mobile_no") %></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("Amount") %></td>
                                                        <%--<td style="background-color: #ffffff;"><%#Eval("pay_uuid") %></td>--%>
                                                        <td style="background-color: #ffffff;"><%#Eval("credit_date") %></td>
                                                        <td style="background-color: #ffffff; text-align: center;">
                                                            <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                CommandArgument='<%#Eval("transcation_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa  fa-trash"></i></asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                            <tfoot>
                                <tr>
                                    <th>Customer Name</th>
                                    <th>Contact</th>
                                    <th>Amount</th>
                                    <%-- <th>Pay UUID</th>--%>
                                    <th>Credit Date</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>

                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </telerik:RadAjaxPanel>
                </div>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="hdCondiments" runat="server" Value="0" />
    <asp:HiddenField runat="server" ID="hdnsearchvalue" />
    <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />
    <asp:HiddenField runat="server" ID="hdsearchvalueSize" />
    <asp:HiddenField runat="server" ID="hdsearchvalueafterEditSize" />



    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
            ffff;
            der: #111111 1px olid;
        }

        .row th {
            background-color: #ffffff !important;
        }
    </style>




</asp:Content>


﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Issue_Voucher_List.aspx.vb" MasterPageFile="~/MasterPage.master" Inherits="Issue_Voucher_List" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Issue Voucher  List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Issue Voucher List</li>
        </ol>
    </div>


    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>


    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

    </script>

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
                        'targets': [5], /* column index */
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
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:HiddenField ID="hdCondiments" runat="server" Value="0" />
    <div class="col-lg-12">
        <div class="panel panel-yellow">
            <div class="panel-heading">Issue Voucher List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Issue Voucher</asp:LinkButton>
                        </div>
                    </div>
                    <br />
                   <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnResponseStart="OnRequestStart">--%>
                        <div class="row" id="divPGroup" runat="server">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                        <asp:Repeater ID="rdvoucher" runat="server">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>

                                                        <th>Account  </th>
                                                        <th>Voucher </th>
                                                        <th>Deposit Amount </th>
                                                        <th>Ref No </th>
                                                        <th>Issue Date</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>


                                                    <td style="background-color: #ffffff;"><%#Eval("Customer")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("voucher_name")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("deposit_amount")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("ref_no")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("issue_datetime", "{0:dd/M/yyyy}")%></td>

                                                    <td style="background-color: #ffffff;">
                                                        <asp:HiddenField ID="hfdn_id" runat="server" Value='<%#Eval("Issuevoucher_id")%>' />
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("Issuevoucher_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>

                                                        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                            CommandArgument='<%#Eval("Issuevoucher_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');">
                                                            <i class="fa fa-trash fa-md"></i></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>
                                    <th>Account  </th>
                                    <th>Voucher </th>
                                    <th>Deposit Amount </th>
                                    <th>Ref No </th>
                                    <th>Issue Date</th>

                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>
                    <%--</telerik:RadAjaxPanel>--%>
                </div>
            </div>
        </div>
    </div>

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
    </style>
</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Credit_Account_List.aspx.vb" Inherits="Credit_Account_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Customer Credit List
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Customer Credit List</li>
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
                        'targets': [1], /* column index */
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


    <%--    <script type="text/javascript">

        var checkbox = document.getElementById("chbx");

        function checkbox_changed(txt) {

            var nodeId = txt.id;
            var idVal = nodeId.replace("ContentPlaceHolder1_rptSizeDetails_txtSizeName_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_txtPrice_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_txtSize_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_txtUnit_", "");
            //idVal = idVal.replace("ctl00_ContentPlaceHolder1_rptSizeDetails_ddlTx", "");      
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_chkClickAndCollect_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_ChkDeliver_", "");
            idVal = idVal.replace("ContentPlaceHolder1_rptSizeDetails_ChkOrderAtTable_", "");
            var ids = "ContentPlaceHolder1_rptSizeDetails_chbx_";
            checkbox = document.getElementById(ids.concat(idVal));
            checkbox.checked = true;
        }
    </script>

    <script type="text/javascript">
        function getCheckBoxValues() {
            $('#rptSizeDetails input[type="checkbox"]').each(function () {
                if ($(this).prop('checked') == true) {
                    alert($(this).val());
                }
            });
        }
    </script>--%>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <br />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Customer Credit List</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <br />
                    <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnResponseStart="OnRequestStart">--%>
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
                                                        <th>Pay UUID</th>
                                                        <th>Credit Date</th>
                                                        <%-- <th>Action</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="background-color: #ffffff;"><%#Eval("full_name") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("customer_mobile_no") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Balance") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("pay_uuid") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("credit_date") %></td>
                                                    <%--<td style="background-color: #ffffff;">
                                                           
                                                        </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>
                                    <th>Customer Name</th>
                                    <th>Contact</th>
                                    <th>Amount</th>
                                    <th>Pay UUID</th>
                                    <th>Credit Date</th>
                                    <%-- <th>Action</th>--%>
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
                    <%-- </telerik:RadAjaxPanel>--%>
                </div>
            </div>
        </div>
    </div>

    <%--<asp:HiddenField ID="hdCondiments" runat="server" Value="0" />
    <asp:HiddenField runat="server" ID="hdnsearchvalue" />
    <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />
    <asp:HiddenField runat="server" ID="hdsearchvalueSize" />
    <asp:HiddenField runat="server" ID="hdsearchvalueafterEditSize" />

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>

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


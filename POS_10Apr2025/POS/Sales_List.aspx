<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Sales_List.aspx.vb" Inherits="Sales_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Sales List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Sales List</li>
        </ol>
    </div>

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
                "order": [[groupCol, 'desc']],
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
                if (i == 12) {
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
    <div class="col-lg-12" id="divCustomer" runat="server">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        --%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Sales List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12 " style="width: 100%; overflow-x: auto">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdSales" runat="server" OnItemCommand="rdSales_ItemCommand" OnItemDataBound="rdSales_ItemDataBound" >
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Sr No.</th>
                                                    <th>Create Date</th>
                                                    <th>Ref id</th>
                                                    <th>Total Amount</th>
                                                    <th>Sync</th>
                                                    <th>Payment Date</th>
                                                    <th>Payment Amount</th>
                                                    <th>Payment Ref</th>
                                                    <th>Type</th>
                                                    <th>Deliver Time</th>
                                                    <th>Deliver Date</th>
                                                    <th>pcode</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("number")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("created_date")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("ref_id")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("total_amount")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("sync")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("payment_date", "{0:dd/MM/yyyy}")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Payment_Amount")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("payment_ref")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("is_deliver")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("deliver_time")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Deliver_date")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("pcode")%></td>
                                                <td style="background-color: #ffffff;">
                                                    <asp:HiddenField ID="hfsales_id" runat="server" Value='<%#Eval("sales_id")%>' />
                                                    <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="View"
                                                        CommandArgument='<%#Eval("sales_id")%>' CommandName="Edit" OnClientClick="return true;">
                                                            <i class="fa fa-search fa-lg"></i></asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                      <asp:HiddenField ID="hd_is_table" runat="server" Value='<%#Eval("is_table")%>' />
                                                   
                                                    <asp:LinkButton ID="btn_mail" runat="server" CausesValidation="False" ToolTip="Send Mail"
                                                        CommandArgument='<%#Eval("sales_id")%>' CommandName="Mail">
                                                       <i class="fa fa-send fa-lg"></i></asp:LinkButton>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <th>Sr No.</th>
                                    <th>Create Date</th>
                                    <th>Ref id</th>
                                    <th>Total Amount</th>
                                    <th>Sync</th>
                                    <th>Payment Date</th>
                                    <th>Payment Amount</th>
                                    <th>Payment Ref</th>
                                    <th>Type</th>
                                    <th>Deliver Time</th>
                                    <th>Deliver Date</th>
                                    <th>pcode</th>
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
                </div>
            </div>

        </div>

        <%-- </telerik:RadAjaxPanel>--%>


        <telerik:RadWindow runat="server" ID="rwEntryDetails" Modal="true" Width="400px"
        Height="250px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
        ReloadOnShow="true" Behaviors="Close" Title="" EnableEmbeddedSkins="false" Style="overflow: hidden !important;">
        <ContentTemplate>
            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                <div class="panel panel-yellow">
                    <div class="panel-heading">Email Detail</div>
                    <div class="panel-body pan">
                        <div class="form-body pal">

                            <div id="dv_prodctlist" runat="server">

                                <div class="row">
                                    <div class="col-lg-12 ">
                                        <label>Email-ID : </label>
                                        <asp:TextBox Style="width: 100%" ID="txt_Email" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="padding-top: 15px; text-align: center;">
                                    <asp:Button ID="btn_send" class="btn btn-primary" runat="server" Text="Send" OnClick="btn_send_Click" />
                                    <asp:Button ID="btn_cancel" class="btn btn-primary" runat="server" Text="Cancel" OnClick="btn_cancel_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
        </ContentTemplate>
    </telerik:RadWindow>

    </div>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Voucher_Tran.aspx.vb" Inherits="Voucher_Tran" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Voucher Transaction List
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Voucher Transaction List</li>
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


            $('#ListSummary thead tr').clone(true).appendTo('#ListSummary thead');

            Grid();

        });

        function Grid() {


            $("#ListSummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#ListSummary').DataTable({


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
                        'targets': [4], /* column index */
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

            var table1 = $('#ListSummary').DataTable();
            $('#ListSummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();

                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');


                //if (i == 4) {
                //    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                //}
                //else {

                //    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                //}



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

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:HiddenField ID="hdCondiments" runat="server" Value="0" />
    <div class="col-lg-12">
        <div class="panel panel-yellow">
            <div class="panel-heading">Voucher Transaction List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-4 ">
                            From Date : &nbsp; 
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" AutoPostBack="true"
                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" OnSelectedDateChanged="txtForDate_SelectedDateChanged">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>

                        </div>
                        <div class="col-lg-4 ">
                            To Date : &nbsp;
                                <telerik:RadDatePicker ID="txtToDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date" Display="None" ValidationGroup="valid" ControlToCompare="txtForDate" ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                        </div>
                        <div class="col-lg-4 ">
                            <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" class="btn btn-primary">View</asp:LinkButton>
                            <asp:LinkButton ID="lnkClear" runat="server" OnClick="lnkClear_Click" class="btn btn-primary">Clear</asp:LinkButton>

                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <br />

                    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnResponseStart="OnRequestStart">--%>
                    <div class="row" id="divPGroup" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="ListSummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdvoucher" runat="server">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Customer </th>
                                                    <th>Voucher </th>
                                                    <th>Amount </th>
                                                    <th>Ref No </th>
                                                    <th>Transaction Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("Customer")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("voucher_name")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Amount")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("VoucherRef_no")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Tran_Date", "{0:dd/M/yyyy}")%></td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        <tfoot>
                                            <tr>
                                                <th>Customer  </th>
                                                <th>Voucher </th>
                                                <th>Amount </th>
                                                <th>Ref No </th>
                                                <th>Transaction Date</th>
                                            </tr>
                                            </table>
                                        </tfoot>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>

                            </div>
                        </div>
                    </div>
                    <%-- </telerik:RadAjaxPanel>--%>
                </div>
            </div>
        </div>
    </div>


    <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>


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


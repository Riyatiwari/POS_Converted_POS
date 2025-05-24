<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Payment_List.aspx.vb" Inherits="Payment_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Payment List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Payment List</li>
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

            var table1 = $('#Lsummary').DataTable();
            $('#Lsummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                if (i == 2) {
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
    <%-- <div class="col-lg-12" id="divCustomer" runat="server">--%>
    <div class="col-lg-12">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
            <div class="panel panel-yellow">
                <div class="panel-heading">Payment List</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Payment</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divCustomer" runat="server">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                        <asp:Repeater ID="rdPayment" runat="server" OnItemCommand="rdPayment_ItemCommand">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th>Payment Name</th>
                                                        <th>Active</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="background-color: #ffffff;"><%#Eval("name")%></td>
                                                     <td style="background-color: #ffffff;"><%#Eval("Active")%></td>

                                                    <td style="background-color: #ffffff;">
                                                         &nbsp;&nbsp;
                                                        <asp:HiddenField ID="hfpayment_id" runat="server" Value='<%#Eval("Payment_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("Payment_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("Payment_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                            <i class="fa fa-trash fa-md"></i></asp:LinkButton>
                                                    
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>

                                    <th>Payment Name</th>
                                    <th>Active</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>



                                    <%--<telerik:RadGrid ID="rdPayment" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                        PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                        PagerStyle-AlwaysVisible="true" Skin="MetroTouch">
                                        <ExportSettings FileName="Payment" IgnorePaging="false" ExportOnlyData="true">
                                        </ExportSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <SortingSettings EnableSkinSortStyles="false" />
                                        <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true" TabIndex="0">
                                            <Columns>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Payment Name"
                                                    SortExpression="name" DataField="name">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Active"
                                                    SortExpression="Active" DataField="Active">
                                                </telerik:GridBoundColumn>
                                               
                                                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfpayment_id" runat="server" Value='<%#Eval("Payment_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("Payment_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("Payment_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>--%>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

       <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>

</asp:Content>


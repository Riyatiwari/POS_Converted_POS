<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ResDiary_List.aspx.vb" Inherits="ResDiary" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    ResDiary List
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">ResDiary List</li>
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
                if (i == 6) {
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
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <div class="col-lg-12" id="divCustomer" runat="server">
       <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
            <div class="panel panel-yellow">
                <div class="panel-heading">ResDiary List</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <%--  <div class="col-lg-12 ">
                            <button class="btn btn-primary" runat="server" id="addTemplate">
                                <i class="fa fa-plus"></i>
                                New Printer
                            </button>
                        </div>--%>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12 " style="width: 100%; overflow-x: auto">
                                <div class="table-responsive">

                                    <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                        <asp:Repeater ID="rdResDiary" runat="server" OnItemCommand="rdResDiary_ItemCommand">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th>Sr No.</th>
                                                        <th>Booking ID</th>
                                                        <th>Reference No.</th>
                                                        <th>Customer Name</th>
                                                        <th>Covers</th>
                                                        <th>Visit Date Time</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="background-color: #ffffff;"><%#Eval("number")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("BookingId")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("BookingReference")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("CustomerFullName")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Covers")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("VisitDateTime")%></td>
                                                    <td style="background-color: #ffffff;"> &nbsp;&nbsp;
                                                        <asp:HiddenField ID="hfBookingId" runat="server" Value='<%#Eval("BookingId")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="View"
                                                            CommandArgument='<%#Eval("BookingId")%>' CommandName="View" OnClientClick="return true;">
                                                            <i class="fa fa-search "></i> </asp:LinkButton>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>
                                    <th>Sr No.</th>
                                    <th>Booking ID</th>
                                    <th>Reference No.</th>
                                    <th>Customer Name</th>
                                    <th>Covers</th>
                                    <th>Visit Date Time</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>


                                    <%--<telerik:RadGrid ID="rdResDiary" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                        PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                        PagerStyle-AlwaysVisible="true" Skin="MetroTouch" Width="100%">
                                        <ExportSettings FileName="ResDiary" IgnorePaging="false" ExportOnlyData="true">
                                        </ExportSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <SortingSettings EnableSkinSortStyles="false" />
                                        <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Sr No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataSetIndex + %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Booking ID"
                                                    SortExpression="BookingId" DataField="BookingId">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Reference No."
                                                    SortExpression="BookingReference" DataField="BookingReference">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Customer Name"
                                                    SortExpression="CustomerFullName" DataField="CustomerFullName">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Covers"
                                                    SortExpression="Covers" DataField="Covers">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridDateTimeColumn ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderText="Visit Date Time" SortExpression="VisitDateTime" DataField="VisitDateTime" DataFormatString="{0:dd/MM/yyyy HH:mm tt}" EnableTimeIndependentFiltering="true" PickerType="DatePicker" DataType="System.DateTime">
                                                </telerik:GridDateTimeColumn>

                                                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfBookingId" runat="server" Value='<%#Eval("BookingId")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="View"
                                                            CommandArgument='<%#Eval("BookingId")%>' CommandName="View" OnClientClick="return true;"><i class="fa fa-search "></i> View </asp:LinkButton>
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

      <%--  </telerik:RadAjaxPanel>--%>
    </div>

   <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>


</asp:Content>


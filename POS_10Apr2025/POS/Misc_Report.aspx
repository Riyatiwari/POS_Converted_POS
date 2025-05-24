<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Misc_Report.aspx.vb" Inherits="Misc_Report" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Miscellaneous Details

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Report</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script lang="javascript" type="text/javascript">
        function RowDblClick(sender, eventArgs) {
            var grid = $find("<%=rptzSUmmary.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var row = masterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var button = row.findElement("IbEdit");
            button.click();
        }

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
                "paging": false,
                "ordering": false,
                "info": false,

                "bFilter": false,
                orderCellsTop: true,
                dom: 'Bfrtip',
                buttons: [
                    'excel',
                    exportOptions: {
                        format: {
                            body: function (data, row, column, node) {
                                return data.replace('£', ' ');
                            }
                        }
                    }

                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": true, "targets": +groupCol },
                    {
                        //'targets': [3], /* column index */
                        'orderable': false,
                        'info': false,
                    }
                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 100,
                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;


                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {



                        if (last !== group) {
                            $(rows).eq(i).before(
                                //'<tr class="group"><td colspan="8"> Product Group : ' + group + '</td> </tr>'
                            );
                            last = group;
                        }
                    });
                }
            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%;visibility: hidden;display:none;" placeholder="" />');

            });
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />

    <div class="col-lg-12" id="divFunction" runat="server">
        <div class="panel panel-yellow" id="dinction" runat="server">
            <div class="panel-heading">Miscellaneous Details</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <br />
                    <div class="row">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                        <div class="col-lg-3" style="text-align: right">
                            <label>From Date : </label>
                        </div>
                        <div class="col-lg-3">
                            <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                <Calendar runat="server" FirstDayOfWeek="Monday">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </div>

                        <div id="divcustom" runat="server">

                            <div class="col-lg-3" style="text-align: right">
                                <label> To Date : </label>
                            </div>
                            <div class="col-lg-3">
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
                        </div>

                        <div class="clearfix"></div>
                        <br />
                        <div class="col-lg-3" style="text-align: right">
                            <label>Venue :</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList Width="70%" ID="radVenue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radVenue_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3" style="text-align: right">
                            <label>Location :</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList Width="70%" ID="radLocation" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <div class="clearfix"></div>
                        <br />


                        <div class="col-lg-6 " style="display: none;">
                            Duration : &nbsp;&nbsp;
                                        <asp:DropDownList Width="70%" ID="radDuration" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="Today" Value="Today" />
                                            <asp:ListItem Text="Yesterday" Value="Yesterday" />
                                            <asp:ListItem Text="This Week" Value="This Week" />
                                            <asp:ListItem Text="This Month" Value="This Month" />
                                            <asp:ListItem Text="This Year" Value="This Year" />
                                            <asp:ListItem Text="Last Week" Value="Last Week" />
                                            <asp:ListItem Text="Last Month" Value="Last Month" />
                                            <asp:ListItem Text="Last Year" Value="Last Year" />
                                            <asp:ListItem Text="Custom" Value="Custom" />
                                        </asp:DropDownList>

                        </div>
                        <div class="col-lg-6 " style="display: none;">
                            Till : &nbsp;
                            <asp:DropDownList Width="70%" ID="radMachine" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <br />

                        <div class="col-lg-12" style="text-align: center">
                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                        </div>
                        <div class="clearfix"></div>
                        <br />

                        <div style="width: 100%">

                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table table-bordered" id="Psummary" cellspacing="0">
                                        <asp:Repeater ID="rptzSUmmary" runat="server">

                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th>Ref id</th>
                                                        <th>Table Name</th>
                                                        <th>Table Date</th>
                                                        <th>Transaction Total</th>
                                                        <th>Transaction Date</th>
                                                        <th>Payment Amount</th>
                                                        <th>Payment Date</th>
                                                        <th>Location</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="background-color: #ffffff;"><%#Eval("ref_id")%></td>
                                                    <td style="background-color: #ffffff;">
                                                        <a target="_blank" style="text-decoration: none;" href='<%#String.Format("Table_Transaction.aspx?T_name={0}&L_ID={1}&Date={2}&S_ID={3}", Eval("table_name"), Eval("location_id"), Eval("table_date"), Eval("sales_id")) %>'><%#Eval("table_name")%></a>
                                                    </td>
                                                    <td style="background-color: #ffffff;"><%#Eval("table_date")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("transaction_total")%></td>

                                                    <td style="background-color: #ffffff;"><%#Eval("transaction_date")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("payment_amount")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("payment_date")%></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("name")%></td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>




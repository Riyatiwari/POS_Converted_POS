<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Xero_integration.aspx.vb" Inherits="Xero_integration" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Xero Integration
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Xero Integration</li>
        </ol>
    </div>

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
                    'excel', 'pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": true, "targets": +groupCol }
                ],

                "displayLength": 25,
                "searching": true
            });

            var table1 = $('#Psummary').DataTable();

            $('#Psummary thead tr:eq(1) th').each(function (i) {

                var title = $(this).text();
                if (i == 0) {
                    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                }
                else {

                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }

                $('input', this).on('keyup', function () {

                    if (table1.column(i).search() !== this.value) {
                        table1.column($(this).parent().index() + ':visible')
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
            tableTemp.column(1).visible(false);
        }
    </script>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12" id="divFunction" runat="server">
        <div class="panel panel-yellow" id="dinction" runat="server">
            <div class="panel-heading">Xero Integration</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div id="divcustom" runat="server">
                            <div class="col-lg-3" style="text-align: right">
                                <label>From Date : </label>
                            </div>
                            <div class="col-lg-3 ">
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch"
                                    AutoPostBack="true" OnSelectedDateChanged="txtForDate_SelectedDateChanged">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>

                            </div>
                            <div class="col-lg-2" style="text-align: right">
                                <label>To Date : </label>
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
                            <label>Venue : </label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList Width="70%" ID="radVenue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radVenue_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2" style="text-align: right">
                            <label>Location : </label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList Width="70%" ID="radLocation" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                        <br />
                        <div class="col-lg-12" style="text-align: center">
                            <asp:LinkButton ID="btn_view" runat="server" class="btn btn-primary btn-sm" OnClick="btn_view_Click" Text="View"></asp:LinkButton>
                        </div>
                        <div class="clearfix"></div>
                        <br />

                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdIntegration_list" runat="server" OnItemDataBound="rdIntegration_list_ItemDataBound">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <asp:Repeater ID="Header1" runat="server">
                                                        <ItemTemplate>
                                                            <th><%# Container.DataItem %>
                                                            </th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff; text-align: center;">
                                                    <asp:HiddenField ID="hd_location_id" runat="server" Value='<%#Eval("Location_id")%>' />
                                                    <asp:HiddenField ID="hd_Date" runat="server" Value='<%#Eval("Date")%>' />
                                                     <asp:HiddenField ID="hd_tax" runat="server" Value='<%#Eval("Tax")%>' />
                                                    <asp:CheckBox ID="chk_location_id" runat="server" />
                                                </td>
                                                <asp:Repeater ID="Item1" runat="server">
                                                    <ItemTemplate>
                                                        <td style="background-color: #ffffff;"><%# Container.DataItem %></td>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <%-- <tr>
                                    <th></th>
                                    <th>Date</th>
                                    <th>Location</th>
                                    <th>Sales After Discount</th>
                                    <th>Food</th>
                                    <th>Beverages</th>
                                    <th>Other</th>
                                </tr>--%>
                                </table>
                            </tfoot>

                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <br />

                        <div class="col-lg-12" style="text-align: center">
                            <asp:LinkButton ID="btn_submit" runat="server" class="btn btn-primary btn-sm" OnClick="btn_submit_Click" Text="Submit"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cash_Declaration_List.aspx.vb" Inherits="Cash_Declaration_List" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Cash Declaration List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Cash Declaration List</li>
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

    <div class="col-lg-12">
        <%--  <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Cash Declaration List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-8">
                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Cash Declaration</asp:LinkButton>
                        </div>
                        <div class="col-lg-3">
                            <asp:LinkButton ID="lnkImport" runat="server" class="btn btn-outline btn-primary"><i class="fa fa-plus"></i>&nbsp;Import Declaration</asp:LinkButton>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-lg-4">
                            Duration : &nbsp;
                                        <asp:DropDownList Width="80%" ID="radDuration" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radDuration_SelectedIndexChanged">
                                            <Items>
                                                <asp:ListItem Text="Today" Value="Today" />
                                                <asp:ListItem Text="Yesterday" Value="Yesterday" />
                                                <asp:ListItem Text="This Week" Value="This Week" />
                                                <asp:ListItem Text="This Month" Value="This Month" />
                                                <asp:ListItem Text="This Year" Value="This Year" />
                                                <asp:ListItem Text="Last Week" Value="Last Week" />
                                                <asp:ListItem Text="Last Month" Value="Last Month" />
                                                <asp:ListItem Text="Last Year" Value="Last Year" />
                                                <asp:ListItem Text="Custom" Value="Custom" />
                                            </Items>
                                        </asp:DropDownList>
                        </div>
                        <div id="divcustom" runat="server" visible="false">
                            <div class="col-lg-4 ">
                                From Date : &nbsp;
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date"
                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" AutoPostBack="true" OnSelectedDateChanged="txtForDate_SelectedDateChanged">
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

                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12" style="text-align: center;">
                            <asp:LinkButton ID="btnView" runat="server" class="btn btn-primary" ToolTip="View" ValidationGroup="valid" OnClick="btnView_Click">View</asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="divImport" runat="server" visible="false" style="border: 1px solid #808080">
                        <div class="panel-heading">Import Declaration </div>
                        <br />
                        <div class="col-lg-12 ">

                            <div class="form-body pal">
                                <div class="row">
                                    <div class="col-lg-5 ">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:LinkButton ID="lnkImportFile" runat="server" class="btn btn-outline btn-primary" ToolTip="Upload" ValidationGroup="valid">Upload</asp:LinkButton>
                                        &nbsp;
                                <a href="../Files/Declaration_Import_Data.xlsx" style="color: blue; text-decoration: underline; float: right; margin-top: 10px;">Download Template</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>

                    <br />
                    <br />
                    <div class="row" id="divBarcode" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdexpense" runat="server" OnItemCommand="rdexpense_ItemCommand">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>For Date</th>
                                                    <th>Cash Amount</th>
                                                    <th>Card Amount</th>
                                                    <th>Table Balance</th>
                                                    <th>On Account</th>
                                                    <th>Location</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("for_date") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("cash_amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("card_amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("tablebalance") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("onaccount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Machine") %></td>

                                                <td style="background-color: #ffffff;">&nbsp;&nbsp;
                                                    <asp:HiddenField ID="hdcsh_id" runat="server" Value='<%#Eval("csh_id")%>' />
                                                    <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                        CommandArgument='<%#Eval("csh_id")%>' CommandName="Delete"
                                                        OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                                        <i class="fa fa-trash fa-md"></i></asp:LinkButton>

                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <th>For Date</th>
                                    <th>Cash Amount</th>
                                    <th>Card Amount</th>
                                    <th>Table Balance</th>
                                    <th>On Account</th>
                                    <th>Location</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>

                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>


                                <%--<telerik:RadGrid ID="rdexpense" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                    PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                    PagerStyle-AlwaysVisible="true" Skin="MetroTouch">
                                    <ExportSettings FileName="Barcode" IgnorePaging="false" ExportOnlyData="true">
                                    </ExportSettings>
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <SortingSettings EnableSkinSortStyles="false" />
                                    <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true" AllowPaging="true">
                                        <Columns>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="For Date"
                                                SortExpression="for_date" DataField="for_date">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Cash Amount"
                                                SortExpression="cash_amount" DataField="cash_amount">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Card Amount"
                                                SortExpression="card_amount" DataField="card_amount">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Table Balance"
                                                SortExpression="tablebalance" DataField="tablebalance">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="On Account"
                                                SortExpression="onaccount" DataField="onaccount">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Location"
                                                SortExpression="Machine" DataField="Machine">
                                            </telerik:GridBoundColumn>
                                           
                                            <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdcsh_id" runat="server" Value='<%#Eval("csh_id")%>' />
                                                   <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                        CommandArgument='<%#Eval("csh_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
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
        <%--</telerik:RadAjaxPanel>--%>
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

        body {
            font-size: 12px;
        }

        tr.group, tr.group:hover {
            background-color: #8fd6fd !important;
            color: #111111;
            border: #111111 1px solid;
            font-weight: bold;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 0px;
        }
    </style>
</asp:Content>

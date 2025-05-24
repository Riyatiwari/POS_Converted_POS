<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Function_List.aspx.vb" Inherits="Function_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Function Map List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Function Map List</li>
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
                if (i == 0 || i == 4) {
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
    <div class="col-lg-12">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>

        <div class="panel panel-yellow">
            <div class="panel-heading">Function Map List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Function Map</asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="lnkCopy" runat="server" class="btn btn-primary"><i class="fa fa-forward"></i>&nbsp;Copy</asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="divFunction" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdFunction" runat="server" OnItemCommand="rdFunction_ItemCommand">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Name </th>
                                                    <th>Till</th>
                                                    <th>Active</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td style="background-color: #ffffff; text-align: center;">
                                                    <asp:HiddenField ID="hd_FunctionId" runat="server" Value='<%#Eval("mainfunction_id")%>' />
                                                    <asp:CheckBox ID="chk_FunctionId" runat="server" />
                                                </td>
                                                <td style="background-color: #ffffff;"><%#Eval("name")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Till_name")%></td>
                                                <td style="background-color: #ffffff;"><%#Eval("is_active")%></td>

                                                <td style="background-color: #ffffff;">
                                                    <asp:HiddenField ID="hdFunction_id" runat="server" Value='<%#Eval("mainfunction_id")%>' />
                                                    <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                        CommandArgument='<%#Eval("mainfunction_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("mainfunction_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                            <i class="fa fa-trash fa-md"></i></asp:LinkButton>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <th></th>
                                    <th>Name </th>
                                    <th>Till</th>
                                    <th>Active</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>

                                <%--<telerik:RadGrid ID="rdFunction" AutoGenerateColumns="False" AllowPaging="True"
                                    AllowSorting="True" runat="server" CellSpacing="0" PageSize="50"
                                    GridLines="None" AllowMultiRowSelection="false" AllowFilteringByColumn="True"
                                    Width="100%" Height="100%" EnableLinqExpressions="false" EnableEmbeddedSkins="true" Skin="MetroTouch" PagerStyle-AlwaysVisible="true">
                                    <ClientSettings Selecting-AllowRowSelect="true">
                                    </ClientSettings>
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <SortingSettings EnableSkinSortStyles="false" />

                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="mainfunction_id" EnableHeaderContextMenu="true"
                                        TableLayout="Fixed">
                                        <Columns>

                                            <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                                            <telerik:GridBoundColumn DataField="name" HeaderText="Name" SortExpression="name"
                                                ReadOnly="True" FilterControlAltText="Filter Function name column" ShowFilterIcon="false"
                                                ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="name"
                                                AutoPostBackOnFilter="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Till_name" HeaderText="Till" SortExpression="Till_name"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="false"
                                                ShowSortIcon="false" ReadOnly="true">
                                                <FilterTemplate>
                                                    <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="RadMachine" AllowCustomText="true"
                                                        Filter="StartsWith" runat="server" Width="100%" AppendDataBoundItems="true" Skin="MetroTouch">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="All" Width="100%" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </FilterTemplate>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="is_active" HeaderText="Active" SortExpression="is_active"
                                                ReadOnly="True" FilterControlAltText="Filter active column" ShowFilterIcon="false"
                                                ShowSortIcon="false" CurrentFilterFunction="StartsWith" UniqueName="active"
                                                AutoPostBackOnFilter="false">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdFunction_id" runat="server" Value='<%#Eval("mainfunction_id")%>' />
                                                    <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                        CommandArgument='<%#Eval("mainfunction_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("mainfunction_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <FilterTemplate>
                                                    <asp:LinkButton ID="IbSearch" runat="server" CausesValidation="False" ImageUrl="#"
                                                        CssClass="btn btn-xs btn-success filter-submit" ToolTip="Search" CommandName="Search"><i class="fa fa-search"></i>&nbsp;Search</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ImageUrl="#"
                                                        CssClass="btn btn-xs btn-info filter-cancel" ToolTip="Reset" CommandName="Clear"><i class="fa fa-times"></i>&nbsp;Reset</asp:LinkButton>
                                                </FilterTemplate>
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

    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Function_Type_List.aspx.vb" Inherits="Function_Type_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Function Type List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Function Type List</li>
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
                    'excel','pdf'
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

    <div class="col-lg-12">
       <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>

            <div class="panel panel-yellow">
                <div class="panel-heading">Function Type List</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Function Type</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divFunctionType" runat="server">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">

                                    <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                        <asp:Repeater ID="rdFunctionType" runat="server" OnItemCommand="rdFunctionType_ItemCommand">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Active</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="background-color: #ffffff;"><%#Eval("name") %></td>
                                                    <td style="background-color: #ffffff;">
                                                        <asp:LinkButton ID="IbUpdate" runat="server" CausesValidation="False" ToolTip="Update"
                                                            CommandArgument='<%#Eval("function_type_id")%>' CommandName="Update" ForeColor="Blue"
                                                            Font-Underline="true" OnClientClick="return true;"><%#Eval("Active")%></asp:LinkButton>
                                                    </td>
                                                    <td style="background-color: #ffffff;">&nbsp;&nbsp;
                                                      <asp:HiddenField ID="hdfunction_type_id" runat="server" Value='<%#Eval("function_type_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("function_type_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("function_type_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                            <i class="fa fa-trash fa-md"></i></asp:LinkButton>

                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>
                                    <th>Name</th>
                                    <th>Active</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>

                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>

                                    <%--<telerik:RadGrid ID="rdFunctionType" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                        PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                        PagerStyle-AlwaysVisible="true" Skin="MetroTouch">
                                        <ExportSettings FileName="Function Type" IgnorePaging="false" ExportOnlyData="true">
                                        </ExportSettings>
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <SortingSettings EnableSkinSortStyles="false" />
                                        <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true">
                                            <Columns>
                                                <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Name"
                                                    SortExpression="name" DataField="name">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn1" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="IbUpdate" runat="server" CausesValidation="False" ToolTip="Update"
                                                            CommandArgument='<%#Eval("function_type_id")%>' CommandName="Update" ForeColor="Blue" Font-Underline="true" OnClientClick="return true;"><%#Eval("Active")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn1" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="IbUpdatePanel" runat="server" CausesValidation="False" ToolTip="UpdatePanel"
                                                            CommandArgument='<%#Eval("function_type_id")%>' CommandName="UpdatePanel" ForeColor="Blue" Font-Underline="true" OnClientClick="return true;"><%#Eval("Panel")%></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdfunction_type_id" runat="server" Value='<%#Eval("function_type_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("function_type_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("function_type_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
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



    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>

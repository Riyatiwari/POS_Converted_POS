<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_Group_Main_List.aspx.vb" Inherits="Product_Group_Main_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Group Category List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Group Category List</li>
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

                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },
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

                "displayLength": 50,
                "searching": true
            });


            var table1 = $('#Psummary').DataTable();

            $('#Psummary thead tr:eq(1) th').each(function (i) {

                var title = $(this).text();
                if (i == 1) {
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

            $('input[type="search"]').on('keyup', function () {
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }
    </script>

    <script language="javascript" type="text/javascript">
        function RowDblClick(sender, eventArgs) {

            var grid = $find("<%=rdCategory.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var row = masterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var button = row.findElement("IbEdit");

            button.click();
        }
    </script>


</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Group Category  List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">

                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Group Category </asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="divPGroup" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdCategory" runat="server" OnItemCommand="rdCategory_ItemCommand">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Name</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("name") %></td>
                                                <td style="background-color: #ffffff;">&nbsp;&nbsp;
                                                        <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("Category_id")%>' />

                                                    <%--<a href='<%#"Product_Group_Main_Master.aspx?category_id=" & Eval("Category_id") %>'>
                                                           <i class="fa fa-edit fa-lg"></i>
                                                       </a>--%>

                                                    <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                        PostBackUrl='<%#"Product_Group_Main_Master.aspx?category_id=" & Eval("Category_id") %>'
                                                        CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("Category_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-trash"></i></asp:LinkButton>

                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>

                                    <th>Name</th>
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

    </div>

    <asp:HiddenField runat="server" ID="hdnsearchvalue" />
    <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />

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


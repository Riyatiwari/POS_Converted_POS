<%@ Page Title="Product List" Language="VB" MasterPageFile="~/MasterPage_woJS.master" AutoEventWireup="false" CodeFile="ProductList.aspx.vb" Inherits="ProductList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Product List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Product List</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />    
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
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
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnDownload") >= 0 ||
                args.get_eventTarget().indexOf("btnDownload") >= 0 ||
                args.get_eventTarget().indexOf("btnDownload") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>

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
                    { "visible": false, "targets": + groupCol }
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
                                '<tr class="group"><td colspan="5"> Product Group : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });




                }
            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">
        <div class="panel panel-yellow">
            <div class="panel-heading">Product List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Product</asp:LinkButton>
                            <asp:LinkButton ID="lnkCopy" runat="server" class="btn btn-primary"><i class="fa fa-copy"></i>&nbsp;Copy Product</asp:LinkButton>
                            <asp:LinkButton ID="lnkChangeProductGroup" runat="server" class="btn btn-primary"><i class="fa fa-copy"></i>&nbsp;Change Product Group</asp:LinkButton>

                            <div style="float: right;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                    <ContentTemplate>

                                        <table width="100%">
                                            <tr>
                                                <td width="25%">Import file : 
                                                </td>
                                                <td width="60%" style="margin-left: 10px;">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </td>
                                                <td width="20%">
                                                    <asp:LinkButton ID="btnUpload" runat="server" class="btn btn-primary" Text="Upload"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <a href="../Files/Product_Sample.xlsx" style="color: blue; text-decoration: underline; float: right; margin-top: 10px;">Download Template</a>
                            </div>
                        </div>
                        <div class="col-lg-12 ">
                            <div id="dv_Mapping" style="float: left; margin-top: 10px;" visible="false" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td width="20%">
                                            <label>Product Group</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlproductGroup" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="25%" style="padding-left: 20px;">
                                            <label>Product Name</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlproductName" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Price</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlPrice" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Unit</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlUnit" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <asp:LinkButton ID="btnSave" runat="server" Style="margin-left: 10px; margin-top: 20px;" class="btn btn-primary"><i class="fa fa-copy"></i>&nbsp;Save</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <br />

                            </div>
                        </div>
                    </div>
                    <br />
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnResponseStart="OnRequestStart">
                        <div class="row" id="divPGroup" runat="server">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                        <asp:Repeater ID="rdProduct" runat="server">
                                            <HeaderTemplate>

                                                <thead>
                                                    <tr>
                                                        <th>Product Group</th>
                                                        <th>Product</th>
                                                        <th>Department</th>
                                                        <th>Status</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="background-color: #ffffff;"><%#Eval("category_name") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("name") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("Department") %></td>
                                                    <td style="background-color: #ffffff;"><%#Eval("active") %></td>
                                                    <td style="background-color: #ffffff;">
                                                        <asp:HiddenField ID="hfCategory_id" runat="server" Value='<%#Eval("product_id")%>' />
                                                        <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                            CommandArgument='<%#Eval("product_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                    <%--                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("product_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>--%>


                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>
                                <tr>
                                    <th>Product Group</th>
                                    <th>Product</th>
                                    <th>Department</th>
                                    <th>Status</th>
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
                    </telerik:RadAjaxPanel>
                </div>
            </div>
        </div>
    </div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
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
    <%--<script src="https://cdn.datatables.net/fixedheader/3.1.6/js/dataTables.fixedHeader.min.js"></script>--%>
    <script src="js/wo/js/fixedheader.min.js"></script>

    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.css" />--%>
    <link href="js/wo/css/datatables.min.css" rel="stylesheet" />

    <%--<script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.20/datatables.min.js"></script>--%>
    <script src="js/wo/js/jquery.dataTables.min.js"></script>    

    <%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>--%>
    <script src="js/wo/js/jquery.dataTables.min.js"></script>

    <%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/dataTables.buttons.min.js"></script>--%>
    <script src="js/wo/js/dataTables.buttons.min.js"></script>

    <%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.flash.min.js"></script>--%>
    <script src="js/wo/js/buttons.flash.min.js"></script>

    <%--<script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>--%>
    <script src="js/wo/js/jszip.min.js"></script>

    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>

    <script type="text/javascript" language="javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    
    <%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.html5.min.js"></script>--%>
    <script src="js/wo/js/buttons.html5.min.js"></script>

    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" />--%>
    <link href="js/wo/css/jquery.dataTables.min.css" rel="stylesheet" />

    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" />--%>
    <link href="js/wo/css/buttons.dataTables.min.css" rel="stylesheet" />
</asp:Content>




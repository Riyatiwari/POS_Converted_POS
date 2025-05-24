<%@ Page Language="VB" Title="Change Product Group List" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Cost.aspx.vb" Inherits="Stock_Cost" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Stock Cost
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
          <%--  <li><i class="fa fa-right"></i>&nbsp;<a href="Product_List.aspx">Product List</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>--%>
            <li class="active">Stock Cost</li>
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
                    //'excel', 'pdf'

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
                                '<tr class="group"><td colspan="3"> Group : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });
                }
            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                stateSave: true
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


            $('input[type="search"]').on('keyup', function () {
                // alert($(this).val());
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }

    </script>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Stock Cost</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-2">
                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary">
                                <i class="fa fa-list"></i>&nbsp;Stock Cost List</asp:LinkButton>
                        </div>
                        <div class="col-lg-10" style="float: right;">
                            <div class="col-lg-12">

                                <div class="col-lg-3 ">
                                    Location <span class="text-danger">*</span> : &nbsp;
                                    <telerik:RadComboBox ID="rdlocation" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rdlocation_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rflocation" runat="server" ControlToValidate="rdlocation"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger" InitialValue="SELECT">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-9" style="text-align: right;">
                                    <asp:Button ID="btnShowPanel" runat="server" class="btn btn-primary" Text="Upload" OnClick="btnShowPanel_Click"></asp:Button>
                                    &nbsp;
                                   <asp:Button ID="btnDownload" runat="server" class="btn btn-primary" Text="Download" OnClick="btnDownload_Click"></asp:Button>
                                    &nbsp;
                                  <a href="../Files/Product_Cost.xlsx" style="display: none; color: blue; text-decoration: underline; margin-top: 10px;">Download Template</a>

                                </div>

                            </div>

                            <div class="clearfix"></div>
                            <br />
                            <div class="col-lg-4"></div>
                            <div class="col-lg-8" runat="server" id="divUpload" visible="false" style="text-align: right;">

                                <div class="col-lg-3">Import file : </div>
                                &nbsp;
                                  <div class="col-lg-4">
                                      <asp:FileUpload ID="FileUpload1" runat="server" />
                                  </div>
                                &nbsp;
                                <div class="col-lg-4">
                                    <asp:LinkButton ID="btnUpload" runat="server" class="btn btn-primary" Text="Show in Grid"></asp:LinkButton>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                            </div>
                        </div>

                    </div>
                    <br />
                    <div class="row" id="divPGroup" runat="server">
                        <div class="col-lg-12 " style="overflow-y: auto; height: 550px;">
                            <div class="table-responsive">
                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:UpdatePanel ID="upWall" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Repeater ID="rdcopyProduct" runat="server">
                                                <HeaderTemplate>
                                                    <thead>
                                                        <tr>
                                                            <th>Group</th>
                                                            <th>Name</th>
                                                            <th>Cost</th>
                                                            <th>Base Size</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="background-color: #ffffff;"><%#Eval("category_name") %> </td>
                                                        <td style="background-color: #ffffff;">
                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                                            <asp:Label ID="hd_product_id" Style="display: none;" runat="server" Text='<%#Eval("product_id") %>'></asp:Label>
                                                            <%--<asp:HiddenField runat="server" ID="hd_product_id" Value='<%#Eval("product_id")%>' />--%>
                                                        </td>
                                                        <td style="background-color: #ffffff;">
                                                            <telerik:RadNumericTextBox RenderMode="Lightweight" ID="txtcost" CssClass="form-control"
                                                                Skin="" runat="server" placeholder="Cost" Width="50%" DisplayText='<%#Eval("cost")%>'>
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td style="background-color: #ffffff;"><%#Eval("Base_Size") %> </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                            <tfoot>
                                <tr>
                                    <th>Group</th>
                                    <th>Name</th>
                                    <th>Cost</th>
                                    <th>Base Size</th>
                                </tr>
                                </table>
                            </tfoot>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </table>

                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom: 10px; text-align: center;">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ToolTip="Click here to change product group" />
                    </div>
                </div>

                <div class="col-lg-12 ">
                    <asp:GridView ID="grid_view1" EmptyDataText="No Records Found." runat="server" Border="1" BorderColor="Black"
                        AutoGenerateColumns="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                        </Columns>
                    </asp:GridView>
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
            ffff;
            der: #111111 1px olid;
        }

        r

        color: #ff fff ! body {
            12px;
        }

        tr.group
        er {
            color: 8fd6fd imprtant;
            color: #1111 1;
            111 1px solid;
            font-weight: old;
            table data d le.dataTable thead td

        {
            padding: 0px;
        }
    </style>
</asp:Content>

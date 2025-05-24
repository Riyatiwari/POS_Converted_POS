<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Location_List.aspx.vb" Inherits="Location_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Location List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Location List</li>
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
                if (i == 10) {
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
    <%--<div class="col-lg-12" id="divLocation" runat="server">--%>
    <div class="col-lg-12">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Location List</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:LinkButton ID="lnkNew" runat="server" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Location</asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="lnkSyncallTill" runat="server" class="btn btn-primary" OnClick="lnkSyncallTill_Click"><i class="fa fa-sync"></i>&nbsp;Sync to all Tills</asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="lnkUtiltyPage" runat="server" class="btn btn-primary" PostBackUrl="~/Auto_Sync_Recored.aspx"  ><i class="fa fa-cog"></i>&nbsp;Open Sync Utility</asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="divLocation" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdLocaton" runat="server" OnItemCommand="rdLocaton_ItemCommand"
                                        OnItemDataBound="rdLocaton_ItemDataBound">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Code</th>
                                                    <th>Location</th>
                                                    <th>City</th>
                                                    <th>Venue</th>
                                                    <th>Till Auto Log Off</th>
                                                    <th>Mapping MerchantID</th>
                                                    <th>Mapping Store</th>
                                                    <th>Mapping Location</th>
                                                    <th>Active</th>
                                                    <th>Web Sales Link</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%#Eval("code") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("name") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("cname") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("venue_name") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("till_auto_log_off") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("gtway_MerchantID") %> </td>
                                                <td style="background-color: #ffffff;">
                                                    <%#Eval("gtway_StoreName") %>
                                                    <asp:HiddenField ID="gtway_StoreName" runat="server" Value='<%#Eval("gtway_StoreName")%>' />
                                                </td>
                                                <td style="background-color: #ffffff;"><%#Eval("gtway_LocationName") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("Active") %> </td>
                                                <td style="background-color: #ffffff;">

                                                    <asp:HiddenField ID="hfwLocation_id" runat="server" Value='<%#Eval("location_id")%>' />
                                                    <a id="lnkWpos" runat="server" href="" title="Link" >Link</a>

                                                </td>
                                                <td style="background-color: #ffffff;">&nbsp;&nbsp;
                                                     <asp:HiddenField runat="server" ID="hdnOldName" Value='<%#Eval("name")%>' />
                                                    <asp:HiddenField ID="hfLocation_id" runat="server" Value='<%#Eval("location_id")%>' />
                                                    <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                        CommandArgument='<%#Eval("location_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("location_id")%>' CommandName="Delete"
                                            OnClientClick="return confirm('Are you sure you want to delete this record ?');">
                                            <i class="fa fa-trash fa-md"></i></asp:LinkButton>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <th>Code</th>
                                    <th>Location</th>
                                    <th>City</th>
                                    <th>Venue</th>
                                    <th>Till Auto Log Off</th>
                                    <th>Mapping MerchantID</th>
                                    <th>Mapping Store</th>
                                    <th>Mapping Location</th>
                                    <th>Active</th>
                                    <th>Web Sales Link</th>
                                    <th>Action</th>
                                </tr>
                                </table>
                            </tfoot>

                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>

                                <%--<telerik:RadGrid ID="rdLocaton" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                    PageSize="50" AllowFilteringByColumn="true" SkinID="RadSkinManager1"
                                    PagerStyle-AlwaysVisible="true" Skin="MetroTouch">
                                    <ExportSettings FileName="Location" IgnorePaging="false" ExportOnlyData="true">
                                    </ExportSettings>
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <SortingSettings EnableSkinSortStyles="false" />
                                    <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true">
                                        <Columns>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Code"
                                                SortExpression="code" DataField="code">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Location"
                                                SortExpression="name" DataField="name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="City"
                                                SortExpression="cname" DataField="cname">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Venue"
                                                SortExpression="venue_name" DataField="venue_name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Till Auto Log Off"
                                                SortExpression="till_auto_log_off" DataField="till_auto_log_off">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="true" HeaderText="Active"
                                                SortExpression="Active" DataField="Active">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Web Sales Link" UniqueName="TemplateColumn" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfwLocation_id" runat="server" Value='<%#Eval("location_id")%>' />
                                                    <a id="lnkWpos" runat="server" href="" title="Link" target="_blank">Link</a>

                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="hdnOldName" Value='<%#Eval("name")%>' />
                                                    <asp:HiddenField ID="hfLocation_id" runat="server" Value='<%#Eval("location_id")%>' />
                                                    <asp:LinkButton ID="IbEdit" runat="server" CausesValidation="False" ToolTip="Edit"
                                                        CommandArgument='<%#Eval("location_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                            CommandArgument='<%#Eval("location_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
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


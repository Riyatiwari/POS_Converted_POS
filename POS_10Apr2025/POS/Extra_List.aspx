<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Extra_List.aspx.vb" Inherits="Extra_List" %>

<%--<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>--%>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Extra List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="js2/demo/datatables-demo.js"></script>
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Extra List</li>
        </ol>
    </div>
    <script language="javascript" type="text/javascript">
        function RowDblClick(sender, eventArgs) {

            var grid = $find("<%=rdExtra.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var row = masterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var button = row.findElement("IbEdit");

            button.click();
        }
    </script>

    

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
                        'targets': [3], /* column index */
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
                if (i == 3) {
                    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                }
                else {

                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }



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

       <script type="text/javascript">

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }



    </script>


</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <%--<div class="col-lg-12" id="divCustomer" runat="server">--%>
    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Extra List</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="col-md-2">
                                    <asp:HiddenField ID="hf_Row_Id" runat="server" Visible="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Narration<span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="rdNarration" runat="server" Width="100%" Skin="MetroTouch" EmptyMessage="Select">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Select" Value="0" />
                                            <telerik:RadComboBoxItem Text="Net" Value="Net" />
                                            <telerik:RadComboBoxItem Text="Cash" Value="Cash" />
                                            <telerik:RadComboBoxItem Text="Card" Value="Card" />
                                            <telerik:RadComboBoxItem Text="Vat" Value="Vat" />
                                            <telerik:RadComboBoxItem Text="Vat0" Value="Vat0" />
                                            <telerik:RadComboBoxItem Text="Vat5" Value="Vat5" />
                                            <telerik:RadComboBoxItem Text="Vat20" Value="Vat20" />
                                            <telerik:RadComboBoxItem Text="dept1" Value="dept1" />
                                            <telerik:RadComboBoxItem Text="dept2" Value="dept2" />
                                            <telerik:RadComboBoxItem Text="dept3" Value="dept3" />
                                            <telerik:RadComboBoxItem Text="dept4" Value="dept4" />

                                        </Items>
                                    </telerik:RadComboBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdNarration" ValidationGroup="valid"
                                        runat="server" ErrorMessage="Narration is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <label>For Date<span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                    <telerik:RadDatePicker ID="txtDate" runat="server" DateInput-EmptyMessage="For Date" MinDate="01/01/1000" MaxDate="01/01/3000" Width="100%" Skin="MetroTouch">
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                        <Calendar runat="server" FirstDayOfWeek="Monday">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
                                        ValidationGroup="valid" Display="none" ErrorMessage="For date is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">

                                    <label>Amount</label><div class="clearfix"></div>

                                    <telerik:RadTextBox ID="txtAmount" onkeypress="return isNumberKey(event,this)"  CssClass="form-control" Skin="" runat="server" placeholder="Amount" Width="100%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtAmount"
                                        ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Amount is required">
                                    </asp:RequiredFieldValidator>
                                </div>




                                <div class="col-md-2">
                                    <asp:LinkButton ID="lnkNew" ValidationGroup="valid" runat="server" class="btn btn-primary">Save</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <br />
                        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnResponseStart="OnRequestStart">
                            <div class="row" id="divPGroup" runat="server">
                                <div class="col-lg-12 ">
                                    <div class="table-responsive">
                                        <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                                            <asp:Repeater ID="rdExtra" runat="server">
                                                <HeaderTemplate>
                                                    <thead>
                                                        <tr>

                                                            <th>Narration</th>
                                                            <th>Amount</th>
                                                            <th>For_Date</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="background-color: #ffffff;"><%#Eval("Narration")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("Amount")%></td>
                                                        <td style="background-color: #ffffff;"><%#Eval("For_Date")%></td>
                                                        <td style="background-color: #ffffff;">
                                                            <asp:HiddenField ID="hfdn_id" runat="server" Value='<%#Eval("Extra_id")%>' />
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ToolTip="Edit"
                                                                CommandArgument='<%#Eval("Extra_id")%>' CommandName="Edit" OnClientClick="return true;"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>

                                                            &nbsp;&nbsp;<asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                CommandArgument='<%#Eval("Extra_id")%>' CommandName="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                            <tfoot>
                                <tr>
                                    <th>Narration</th>
                                    <th>Amount</th>
                                    <th>For Date</th>

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

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
            ffff;
            der: #111111 1px olid;
        }

        r


        background-color: #ffffff !important;
        }
    </style>
</asp:Content>


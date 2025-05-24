<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Customer_Credit_Report.aspx.vb" Inherits="Customer_Credit_Report" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Customer Credit Report
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Customer Credit Report</li>
        </ol>
    </div>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

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
                  if (i == 0) {
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


    <script lang="javascript" type="text/javascript">

        function openNewWin(url) {
            var x = window.open(url, 'Customer Credit Report Detail', 'width=600,height=600,toolbar=1');
            x.focus();
        }
    </script>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <br />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Customer Credit Report</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <div class="row">
                        <div class="col-lg-4">

                            <label>Customer</label>
                            <div class="clearfix"></div>
                            <telerik:RadComboBox ID="RadAccount" runat="server" Width="60%">
                            </telerik:RadComboBox>

                        </div>
                        <div class="col-lg-4">

                            <label>From Date </label>
                            <div class="clearfix"></div>
                            <telerik:RadDatePicker ID="txtFromDate" runat="server" DateInput-EmptyMessage="Date"
                                MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="60%"
                                AutoPostBack="true">
                                <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" />
                                <Calendar ID="Calendar3" runat="server" FirstDayOfWeek="Monday">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-lg-4">

                            <label>To Date </label>
                            <div class="clearfix"></div>
                            <telerik:RadDatePicker ID="txtToDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="60%">
                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" />
                                <Calendar ID="Calendar1" runat="server" FirstDayOfWeek="Monday">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="row">

                        <div class="clearfix"></div>
                        <div class="col-lg-12 " style="text-align: center;">
                            <br />
                            <asp:LinkButton ID="lnkView" runat="server" class="btn btn-primary">View</asp:LinkButton>

                        </div>
                    </div>

                    <br />
                    <br />

                    <div class="row" id="divPGroup" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdCustomer" runat="server" OnItemCommand="rdCustomer_ItemCommand">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Customer Name</th>
                                                    <th>Credit Date</th>
                                                    
                                                   <%-- <th style="display: none;">Contact</th>
                                                    <th style="display: none;">Balance</th>--%>
                                                    <th>Opening Balance</th>
                                                    <th>Transaction Amount</th>
                                                    <th>Closing Balance</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnExpand" runat="server" Text="+" Style="font-weight: bold;" />

                                                    <asp:HiddenField ID="hf_sales_id" runat="server" Value='<%# Eval("sales_id") %>' />
                                                </td>
                                                <td style="background-color: #ffffff;"><%#Eval("full_name") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("credit_date") %></td>
                                               
                                               <%-- <td style="background-color: #ffffff; display: none;"><%#Eval("customer_mobile_no") %></td>
                                                <td style="background-color: #ffffff; display: none;"><%#Eval("Balance") %></td>--%>
                                                <td style="background-color: #ffffff;"><%#Eval("Opening_balance") %></td>
                                                 <td style="background-color: #ffffff;"><%#Eval("Amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("closing_balance") %></td>
                                                <td style="text-align: center;">
                                                    <asp:LinkButton ID="btn_mail" runat="server" CausesValidation="False" ToolTip="Send Mail"
                                                        CommandArgument='<%#Eval("sales_id")%>' CommandName="Mail" OnClientClick="return confirm('Are you sure you want to send Mail ?');">
                                                       <i class="fa fa-send fa-lg"></i></asp:LinkButton>
                                                </td>
                                            </tr>

                                            <asp:Panel runat="server" ID="pnlOrders" Visible="false">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td colspan="6">
                                                        <asp:Repeater ID="rptOrders" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered" id="inner" cellspacing="0" rules="all" border="1" style="margin-bottom: 0px;">
                                                                    <tr>
                                                                        <th>Created Date</th>
                                                                        <th>Total Amount</th>
                                                                        <th>Payment Amount</th>
                                                                        <th>Operator</th>
                                                                        <th>Till</th>
                                                                        <th>Transaction ID</th>
                                                                        <th>Details</th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>

                                                                    <td style="background-color: #ffffff;"><%#Eval("created_date") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("Total_Amount") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("payment_amount") %></td>

                                                                    <td style="background-color: #ffffff;"><%#Eval("Operator") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("Till") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("tran_uuid") %></td>
                                                                    <td style="background-color: #ffffff;">
                                                                        <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("sales_id").ToString() + "#" + Eval("tran_uuid").ToString()  %>' runat="server" OnClick="lnkview_Click1">
                                                                        <i class="fa fa-search" style="cursor: pointer" aria-hidden="true"></i>
                                                                        </asp:LinkButton>
                                                                    </td>

                                                                </tr>


                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </asp:Panel>


                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <th></th>
                                    <th>Customer Name</th>
                                    <th>Credit Date</th>
                                   
                                   <%-- <th style="display: none;">Contact</th>
                                    <th style="display: none;">Balance</th>--%>
                                    <th>Opening Balance</th>
                                     <th>Transaction Amount</th>
                                    <th>Closing Balance</th>
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



    <%--   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>

    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
            color: #ffffff;
            border: #111111 1px solid;
        }

        .row th {
            background-color: #ffff a
        }


        body {
            ize: 12px;
        }

        tr.g
        :hover {
            und-col r: #8fdfd import color: # 11111 #111111 1px sold;
            font-weigt: bod;
            t ble. t table.dataTable thead td

        {
            padding: 0px;
        }
    </style>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Duplicate_transaction.aspx.vb" Inherits="Duplicate_transaction" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Duplicate Transaction Report

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Duplicate Transaction Report</li>
        </ol>
    </div>



    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid_1();
            Grid_2();

        });


        function Grid_1() {

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

                excel: [{
                    filterable: true,
                    columns: ':visible'
                }],

                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [

                    { "visible": false, "targets": +groupCol }

                ],

                "displayLength": 50,
                "searching": true
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

        function Grid_2() {

            $("#Tsummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#Tsummary').DataTable({

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel'
                ],

                excel: [{
                    filterable: true,
                    columns: ':visible'
                }],

                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [

                    { "visible": false, "targets": +groupCol }

                ],

                "displayLength": 50,
                "searching": true
            });


            var table1 = $('#Tsummary').DataTable();

            $('#Tsummary thead tr:eq(1) th').each(function (i) {

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
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12" id="divFunction" runat="server">

        <div class="panel panel-yellow">
            <div class="panel-heading">Duplicate Transaction Report</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <center>
                                  <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                               <div  id="divcustom" runat="server" visible="false" >
                                <div class="col-lg-6 ">
                                From Date : &nbsp;
                                <telerik:RadDatePicker ID="txtFrom" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday" >
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                  
                               </div>    
                                 <div class="col-lg-6 ">                                     
                                  To Date : &nbsp;
                                <telerik:RadDatePicker ID="txtTo" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday" >
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                      
                                     </div>
                                   </div>
                                  <div class="clearfix"></div>
                                <br /> 

                                              <div class="col-lg-6 ">                                     
                                               
                                     Duration : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radDuration" runat="server" AutoPostBack="true" >
                                             <Items>
                                                <telerik:RadComboBoxItem Text="Today" Value="Today" />
                                                 <telerik:RadComboBoxItem Text="Yesterday" Value="Yesterday" />
                                                <telerik:RadComboBoxItem Text="This Week" Value="This Week" />
                                                 <telerik:RadComboBoxItem Text="This Month" Value="This Month" />
                                                 <telerik:RadComboBoxItem Text="This Year" Value="This Year" />
                                                 <telerik:RadComboBoxItem Text="Last Week" Value="Last Week" />
                                                 <telerik:RadComboBoxItem Text="Last Month" Value="Last Month" />
                                                 <telerik:RadComboBoxItem Text="Last Year" Value="Last Year" />
                                                 <telerik:RadComboBoxItem Text="Custom" Value="Custom" />
                                            </Items>
                                        </telerik:RadComboBox>
                                      
                                     </div>         

                             <div class="col-lg-6 ">                                     
                                               
                                     Select Type : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="rdSelect" runat="server">
                                             <Items>
                                                <telerik:RadComboBoxItem Text="Master" Value="Master" />
                                                 <telerik:RadComboBoxItem Text="Transaction" Value="Transaction" />
                                            </Items>
                                        </telerik:RadComboBox>
                                      
                                     </div>  
                                 <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date"  Display="None"  ValidationGroup="valid" ControlToCompare="txtFrom" ControlToValidate="txtTo" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                <div class="clearfix"></div>
                                <br />
                                 <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                              <br />
                              </center>
                    </div>

                    <div class="card-body">
                        <div class="table-responsive" id="grid_1" runat="server" visible="false">
                            <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                <asp:Repeater ID="rptProductSUmmary" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Sales ID</th>
                                                <th>Date</th>
                                                <th>Till</th>
                                                <%--  <th>Operator</th>--%>
                                                <th>Table Name</th>
                                                <%--<th>Total Amount</th>
                                                <th>Total Discount</th>--%>
                                                <th>Net Amount</th>
                                                <th>Payment</th>
                                                <%--  <th>Payment Type</th>--%>
                                                <th>Reference</th>
                                                <th>Transaction ID</th>
                                                <%--   <th>Action</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td></td>
                                            <td style="background-color: #ffffff;"><%#Eval("sales_id") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Date") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Till") %></td>
                                            <%--  <td style="background-color: #ffffff;"><%#Eval("UserName") %></td>--%>
                                            <td style="background-color: #ffffff;"><%#Eval("table_name") %></td>
                                            <%-- <td style="background-color: #ffffff;"><%#Eval("total_amount") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("total_discount") %></td>--%>
                                            <td style="background-color: #ffffff;"><%#Eval("net_amount") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("payment") %></td>
                                            <%-- <td style="background-color: #ffffff;"><%#Eval("sale_type") %></td>--%>
                                            <td style="background-color: #ffffff;"><%#Eval("srno") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("tran_uuid") %></td>
                                            <%-- <td style="background-color: #ffffff;">
                                                <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("sales_id").ToString() + "#" + Eval("tran_uuid").ToString() + "#Tran_Report#" + Eval("sale_type").ToString()  %>' runat="server" OnClick="lnkview_Click">
                                                                        <i class="fa fa-search" style="cursor: pointer" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                            <tfoot>

                                <tr>
                                    <th></th>
                                    <th>Sales ID</th>
                                    <th>Date</th>
                                    <th>Till</th>
                                    <%--<th>Operator</th>--%>
                                    <th>Table Name</th>
                                    <%--  <th>Total Amount</th>
                                    <th>Total Discount</th>--%>
                                    <th>Net Amount</th>
                                    <th>Payment</th>
                                    <%--  <th>Payment Type</th>--%>
                                    <th>Reference</th>
                                    <th>Transaction ID</th>
                                    <%--<th>Action</th>--%>
                                </tr>
                                </table>
                            </tfoot>

                                    </FooterTemplate>
                                </asp:Repeater>
                        </div>

                        <div class="table-responsive" id="grid_2" runat="server" visible="false">
                            <table class="table table-bordered" id="Tsummary" width="100%" cellspacing="0">
                                <asp:Repeater ID="rptTranDetail" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Date</th>
                                                <th>ReferenceID </th>
                                                <th>Tref ID</th>
                                                <th>Product Name</th>
                                                <th>Quntity</th>
                                                <th>Total Amount</th>
                                                <th>TSales ID</th>
                                                <th>Transaction UUID</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td></td>
                                           <td style="background-color: #ffffff;"><%#Eval("created_date") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("ref_id") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("tref_id") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("product_name") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("quntity") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("sales_total_amount") %></td>
                                             <td style="background-color: #ffffff;"><%#Eval("tsales_id") %></td>
                                             <td style="background-color: #ffffff;"><%#Eval("tran_uuid") %></td>
                                            <%-- <td style="background-color: #ffffff;">
                                                <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("sales_id").ToString() + "#" + Eval("tran_uuid").ToString() + "#Tran_Report#" + Eval("sale_type").ToString()  %>' runat="server" OnClick="lnkview_Click">
                                                                        <i class="fa fa-search" style="cursor: pointer" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                            <tfoot>

                                <tr>
                                    <th></th>
                                    <th>Date</th>
                                    <th>ReferenceID </th>
                                    <th>Tref ID</th>
                                    <th>Product Name</th>
                                    <th>Quntity</th>
                                    <th>Total Amount</th>
                                    <th>TSales ID</th>
                                    <th>Transaction UUID</th>
                                </tr>
                                </table>
                            </tfoot>

                                    </FooterTemplate>
                                </asp:Repeater>
                        </div>


                    </div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>


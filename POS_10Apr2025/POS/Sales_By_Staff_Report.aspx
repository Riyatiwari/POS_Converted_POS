<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Sales_By_Staff_Report.aspx.vb" Inherits="Sales_By_Staff_Report" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Operator Product Summary
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">

    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Operator Product Summary</li>
        </ol>
    </div>


    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
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
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">--%>

        <div class="panel panel-yellow">
            <div class="panel-heading">Operator Product Summary</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <center>
                                <div  id="divcustom" runat="server" visible="false" >
                                 <div class="col-lg-6 ">
                                From Date : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <%-- <div class="col-lg-6 ">
                                     Product Group : &nbsp;

                                        <telerik:RadComboBox ID="radCategory" runat="server" AutoPostBack="true"  >
                                        </telerik:RadComboBox>
                                      
                                     </div>  
                                 <div class="col-lg-6 ">
                                      Product : &nbsp;
                                        <telerik:RadComboBox ID="radProduct" runat="server" >
                                        </telerik:RadComboBox>
                                     </div>
                                 <div class="clearfix"></div>--%>
                                 <%--<br />--%>

                                       <div class="col-lg-6">                                     
                                  Duration : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                     Operator : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radOprator" runat="server" >
                                        </telerik:RadComboBox>                                      
                                     </div>

                                 <div class="clearfix"></div>
                                <br />
                               <div class="col-lg-6 ">          
                                            <asp:RadioButtonList ID="rdType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Selected="True" Text="&nbsp;ALL&nbsp;" Value="ALL"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;SALE&nbsp;" Value="SALE"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;RETURN&nbsp;" Value="RETURN"></asp:ListItem>
                                            </asp:RadioButtonList>              
                                   </div>
                                <div class="clearfix"></div>
                                <br />

                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report">View</asp:LinkButton>
                                     
                              <br /><br />
                             <%-- <div class="col-lg-12" >
                                 <telerik:ReportViewer ID="ReportViewer1" runat="server" Style="border: 1px solid #ccc;"
                                                Width="88%" Height="550px" Visible="false" />
                                    </div>--%>
                              </center>
                    </div>

                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                <asp:Repeater ID="rptProductSUmmary" runat="server">
                                    <HeaderTemplate>

                                        <thead>

                                            <tr>
                                                <th>Operator</th>
                                                <th>Product</th>
                                                <th>Price</th>
                                                <th>Sales Quantity</th>
                                                <th>Return Quantity</th>
                                                <th>Total Amount</th>
                                                <th>Discount</th>
                                                <th>Net Amount</th>
                                                <th>Total Tax</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <tr>
                                            <td style="background-color: #ffffff;"><%#Eval("UserName") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Pname") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("price") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Sale_qunt") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("return_qunt") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Total_amt") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Discount") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Net_amt") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Sales_tax_amount") %></td>

                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                            <tfoot>

                                <tr>
                                    <th>Operator</th>
                                    <th>Product</th>
                                    <th>Price</th>
                                    <th>Sales Quantity</th>
                                    <th>Return Quantity</th>
                                    <th>Total Amount</th>
                                    <th>Discount</th>
                                    <th>Net Amount</th>
                                    <th>Total Tax</th>

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

        <%-- </telerik:RadAjaxPanel>--%>
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

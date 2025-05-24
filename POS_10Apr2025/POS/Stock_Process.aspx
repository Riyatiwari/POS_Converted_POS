<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Process.aspx.vb" Inherits="Stock_Process" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Hourly Payment Report
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Stock Re-Process</li>
        </ol>
    </div>

<%--    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
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

            var groupCol = -1;

            var table = $('#Psummary').DataTable({

                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },
                orderCellsTop: false,
                dom: 'Bfrtip',
                "buttons": [
                    'excel'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [

                    { "visible": true, "targets": +groupCol },

                    { "orderable": false, "targets": -1 }

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

            $('input[type="search"]').on('keyup', function () {
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }
    </script>--%>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-yellow">
        <div class="panel-heading">Hourly Payment Report</div>
        <div class="panel-body pan">
            <div class="form-body pal">
                <div class="row">
                    <div class="col-lg-12 ">
                        <asp:Label id="lblMsg" runat="server" Text="" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <asp:HiddenField ID="hdnfromdate" runat="server" />
                    <center>
                                
                                      
                                    
                                   
                               <div id="divcustom" runat="server">
                                 <div class="col-lg-6 ">
                                From Date : &nbsp;
                                <telerik:RadDatePicker  Width="40%"  ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch" >
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
                                <telerik:RadDatePicker Enabled="false"  Width="40%"  ID="txtToDate"  DateInput-ReadOnly="true"  runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday"  >
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                      <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date"  Display="None"  ValidationGroup="valid" ControlToCompare="txtForDate" ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                     </div>
                                     <div class="clearfix"></div>
                                <br />
                                    <br />
                                    </div>

                           <div class="col-lg-6">
                                      Location : &nbsp;
                                        <asp:DropDownList ID="radLocation" runat="server" Width="40%"  >
                                        </asp:DropDownList>
                                     </div>
                                     <div class="clearfix"></div>
                                <br />
                              <div class="col-lg-12 " >
                                  <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="Start" ValidationGroup="valid">Re-Process Stock</asp:LinkButton>
                              </div>
                        <div class="clearfix"></div>
                                <br />
                              </center>
                </div>


                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0" style="display:none;">
                            <asp:Repeater ID="rdPayment" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                             <th>Day</th>
                                            <th>Date</th>
                                            <th>Time</th>
                                            <th>Total Payment</th>
                                            <th>Total Sales</th>
                                            <th>Beverages</th>
                                            <th>Food</th>
                                            <th>Other</th>
                                            <th>Department Category 1</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="background-color: #ffffff;"><%#Eval("Day_name") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("S_Date") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("SlotStart").ToString() + " - " + Eval("SlotEnd").ToString()  %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Number") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Total_sales") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Beverages") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Food") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Other") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Department_Category_1") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            <tfoot>
                                <tr>
                                      <th>Day</th>
                                    <th>Date</th>
                                    <th>Time</th>
                                    <th>Total Payment</th>
                                    <th>Total Sales</th>
                                    <th>Beverages</th>
                                    <th>Food</th>
                                    <th>Other</th>
                                    <th>Department Category 1</th>
                                </tr>

                            </tfoot>

                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
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


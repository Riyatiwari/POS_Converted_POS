<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Till_Summary_Report.aspx.vb" Inherits="Till_Summary_Report" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"> 
    Till Summary Report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Till Summary Report</li>
        </ol>
    </div>
    
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

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
                           { "searchable": false,"visible": true, "targets": +groupCol },
                       {
                            /* column index */
                           'orderable': true,
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
                {
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
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ContentPlaceHolder1_rbtDisplayReport input {
            margin-left: 5px;
        }
    </style>
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

            <div class="panel panel-yellow">
                <div class="panel-heading">Till Summary Report</div>
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
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch">
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
                                <telerik:RadDatePicker ID="txtToDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday" >
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
                                    </div>
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
                                     Venue :
                                        <telerik:RadComboBox ID="radVenue" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>                               
                                   <div class="clearfix"></div>
                                <br />
                                 <div class="col-lg-6 ">
                                      Location : &nbsp;
                                        <telerik:RadComboBox ID="radLocation" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                     </div>
                             
                                  <div class="col-lg-6 ">
                                     Till : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radMachine" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>   
                                <div class="clearfix"></div>
                                <br />   
                                    <div class="col-lg-6 ">
                                      <div style="display: table;">
                                         <label style="float:left;"> Display Report By : &nbsp;</label>
                                        <asp:RadioButtonList ID="rbtDisplayReport" runat="server" RepeatColumns="2" style="float:left; margin-right:2px" >
                                                <asp:ListItem Text="Tills" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Location" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Venue" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Operater" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                          </div>
                                     </div>
                                  <div class="col-lg-6 ">
                                     &nbsp;
                                     </div>   
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-lg-12 "> 
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                                     </div>
                              <br /><br /><br />
                              <div class="col-lg-12 " >
                                 <%--<telerik:ReportViewer ID="ReportViewer1" runat="server" Style="border: 1px solid #ccc;"
                                                Width="85%" Height="650px" />--%>
                                    <div class="card-body">
                    <div class="table-responsive" style="overflow-x:auto">
                        <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                            <asp:Repeater ID="rpttillSUmmary" runat="server">
                                <HeaderTemplate>

                                    <thead>
                                        <tr>
                                            <th>Venue</th>
                                            <th>Location</th>
                                            <th>Till</th>
                                            <th>Name</th>
                                            <th>Timespan</th>
                                            <th>Qty Transactions</th>
                                            <th>Gross Sales</th>
                                            <th>Total Discount</th>
                                            <th>Net Total</th>
                                            <th>%of Net Total</th>
                                            <th>Cost Of Sales</th>
                                            <th>ProfitAmt</th>
                                            <th>Profit%</th>
                                            <th>Date Time Last Trans</th>
                                            <%--<th>Product Group</th>
                                            <th>Group Category</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                         <td style="background-color: #ffffff;"><%#Eval("venue")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("location")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Machine")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Name")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("TimeSpan")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("QtyTransactions")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("GrossSales")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("TotalDiscount")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("NettTotal")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("PerofNettTotal")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("CostofSales")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("ProfitAmt")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Profit")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("DatetimeLastTrans")%></td>
                                       <%--  <td style="background-color: #ffffff;"><%#Eval("ProductGroup")%></td>
                                         <td style="background-color: #ffffff;"><%#Eval("ProductCategory")%></td>--%>

                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            <tfoot>
                                <tr>
                                     <th>Venue</th>
                                            <th>Location</th>
                                            <th>Till</th>
                                            <th>Name</th>
                                            <th>Timespan</th>
                                            <th>Qty Transactions</th>
                                            <th>Gross Sales</th>
                                            <th>Total Discount</th>
                                            <th>Net Total</th>
                                            <th>%of Net Total</th>
                                            <th>Cost Of Sales</th>
                                            <th>ProfitAmt/th>
                                            <th>Profit%</th>
                                            <th>Date Time Last Trans</th>
                                             <%--   <th>Product Group</th>
                                            <th>Group Category</th>--%>
                                </tr>
                                </table>
                            </tfoot>

                                </FooterTemplate>
                            </asp:Repeater>
                    </div>
                </div>
                                    </div>
                              </center>
                        </div>
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
            color: #ffffff;
            border: #111111 1px solid;
        }

        .row th {
            background-color: #ffffff !important;
        }

        body {
            font-size: 12px;
        }
        /*.table.dataTable thead th
        {
            padding:1px;
        }*/
    </style>
</asp:Content>



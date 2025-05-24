<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EatOut.aspx.vb" Inherits="EatOut" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Eat Out

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Eat Out</li>
        </ol>
    </div>
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script language="javascript" type="text/javascript">
        function RowDblClick(sender, eventArgs) {
            var grid = $find("<%=rptzSUmmary.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var row = masterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var button = row.findElement("IbEdit");
            button.click();
        }
    </script>
    <script language="javascript" type="text/javascript">

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
                "paging": false,
                "ordering": false,
                "info": false,

                "bFilter": false,
                orderCellsTop: true,
                dom: 'Bfrtip',
                buttons: [
                    'excel' ,
                        exportOptions: {
                            format: {
                                body: function (data, row, column, node) {
                                    return data.replace('£', ' ');
                                }
                            }
                        }

                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": true, "targets": +groupCol },
                    {
                        //'targets': [3], /* column index */
                        'orderable': false,
                        'info': false,
                    }
                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 100,
                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;


                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {



                        if (last !== group) {
                            $(rows).eq(i).before(
                                //'<tr class="group"><td colspan="8"> Product Group : ' + group + '</td> </tr>'
                            );
                            last = group;
                        }
                    });
                }
            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                //if (i == 2) {
                $(this).html('<input type="text" style="width:98%;visibility: hidden;display:none;" placeholder="" />');
                //}
                //else {

                //$(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                //}
                //$('input', this).on('keyup change', function () {

                //    if (table1.column(i).search() !== this.value) {
                //        table1

                //            .column($(this).parent().index() + ':visible')
                //            .search(this.value)
                //            .draw()
                //            ;

                //    }
                //    else {
                //        table1
                //            .column($(this).parent().index() + ':visible')
                //            .search('')
                //            .draw()
                //            ;

                //    }


                //});
            });
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">


        <div class="panel panel-yellow" id="dinction" runat="server">
            <div class="panel-heading">Eat Out</div>

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
                                    <br />
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
                                     Venue : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                     Till : &nbsp;
                                        <telerik:RadComboBox ID="radMachine" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>   
                                <br /> 
                                   
                                <div class="col-lg-12 "> 
                       
                                
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary"  ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                                     </div>
                              <br /><br />
                          
                                <div  id="dincgtion" runat="server" >                            
                                    <br />
                                <div class="col-lg-12 ">
                                    <table>
                                       <tr><h1 style="color:black">Eat Out</h1><br /></tr> 

                                         
                                          
                                       
                                    </table>
                                    <br />
                                    <br />
                                </div>
                                    <br />
                               
                                    <br />
                                     </div>
                                 <div style="width:70%">
                                 
                                    <div class="card-body">
                    <div class="table-responsive" >
                        <table class="table table-bordered"  id="Psummary" cellspacing="0">
                            <asp:Repeater ID="rptzSUmmary" runat="server" >
                               
                                    <HeaderTemplate>
                                    <thead>
                                        <tr>
                                              <th>Date</th>
                                            <th>Payment Amount</th>
                                            <th>No of Covers</th>
                                         </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                         <td style="background-color: #ffffff;"><%#Eval("Fordate")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Amount")%></td>
                                        <td style="background-color: #ffffff;"><%#Eval("noofcovers")%></td>
                                    </tr>
                                </ItemTemplate>
                                
                            </asp:Repeater>
                            </table>
                    </div>
                </div>
                                    </div>
                                   
                              
                              </center>
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>




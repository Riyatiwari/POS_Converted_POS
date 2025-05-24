<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Transfered_Data.aspx.vb" Inherits="Transfered_Data" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    API List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
            <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Cloud_Net.aspx">Cloud Net</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Transfered Data</li>
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
            <div class="panel-heading">Transfered Data</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                          <div class="col-lg-6 ">
                                From Date : &nbsp;
                                 <telerik:RadDatePicker ID="txtForDate" style="width:160px;"  runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch" >
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
                                <div class="clearfix"></div>
                                <br />
                    </div>
                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                                 
                     <div class="col-sm-6 ">
                   <%-- <label> Date :</label> &nbsp;--%>
                                
                         </div>
                    <br />
                    <div class="row" id="divApi" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                 <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdhistory" runat="server">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Machine ID</th>
                                                    
                                                    <th>Key</th>
                                                    <th>API Value</th>
                                                    <th>URL</th>
                                                    <th>store_name</th>
                                                    <th>Timestamp</th>
                                                    <th>cassette</th>
                                                    <th>Total Quantity</th>
                                                    <th>Location</th>
                                              
                                                   <%-- <th>API</th>--%>
                                                    
                                        
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                              
                                                <td style="background-color: #ffffff;"><%#Eval("machine_id") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("Api_key") %> </td>
                                                  <td style="background-color: #ffffff;"><%#Eval("value") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("url") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("Store_name") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("timestamp") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("cassette") %> </td>
                                                <td style="background-color: #ffffff;"><%#Eval("reading") %> </td>
                                              
                                                <td style="background-color: #ffffff;"><%#Eval("locationname") %> </td>
                                            
                                               
                                                
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                            <th>Machine ID</th>
                                                    <th>API Value</th>
                                                    <th>Key</th>
                                                  
                                                    <th>URL</th>
                                                    <th>store_name</th>
                                                    <th>Timestamp</th>
                                                    <th>Cassette</th>
                                                    <th>Total Quantity</th>
                                                    <th>Location</th>
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

   
</asp:Content>


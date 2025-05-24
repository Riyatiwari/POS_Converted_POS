<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Auto_Sync_History.aspx.vb" Inherits="Auto_Sync_History" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Sync History
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Auto_Sync_Recored.aspx">Auto Sync Record</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Sync History</li>
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
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

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
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12">
        <div class="panel panel-yellow">
            <div class="panel-heading">Sync History</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <div class="row">
                        <center>
                               
                                <div  id="divcustom" runat="server" visible="false" >
                                 <div class="col-lg-6 ">
                                From Date : &nbsp;
                                
                                   <telerik:RadDatePicker width="25%" ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                <Calendar runat="server" FirstDayOfWeek="Monday">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                               </div>    
                                 <div class="col-lg-6 ">                                     
                                  To Date : &nbsp;
                                      <telerik:RadDatePicker width="25%" ID="txtToDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                <Calendar runat="server" FirstDayOfWeek="Monday">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date" Display="None" ValidationGroup="valid" ControlToCompare="txtForDate" ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>

                                 </div>
                                     <div class="clearfix"></div>
                                <br />
                                    </div>
                                 <div class="col-lg-6 ">
                                     Duration : &nbsp;&nbsp;
                                         <asp:DropDownList ID="radDuration" width="25%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radDuration_SelectedIndexChanged">
                                <Items>
                                    <asp:ListItem Text="Today" Value="Today" />
                                    <asp:ListItem Text="Yesterday" Value="Yesterday" />
                                    <asp:ListItem Text="This Week" Value="This Week" />
                                    <asp:ListItem Text="This Month" Value="This Month" />
                                    <asp:ListItem Text="This Year" Value="This Year" />
                                    <asp:ListItem Text="Last Week" Value="Last Week" />
                                    <asp:ListItem Text="Last Month" Value="Last Month" />
                                    <asp:ListItem Text="Last Year" Value="Last Year" />
                                    <asp:ListItem Text="Custom" Value="Custom" />
                                </Items>
                            </asp:DropDownList>
                                      
                                     </div>         
                                 <div class="col-lg-6 ">
                                     Venue : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                         <asp:DropDownList ID="radVenue" width="25%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radVenue_SelectedIndexChanged">
                            </asp:DropDownList>
                                      
                                     </div>                               
                                   <div class="clearfix"></div>
                                <br />
                                 <div class="col-lg-6 ">
                                      Location : &nbsp;
                                       <asp:DropDownList ID="radLocation" width="25%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radLocation_SelectedIndexChanged">
                            </asp:DropDownList>
                                     </div>
                             
                                  <div class="col-lg-6 ">
                                     Till : &nbsp;
                                       <asp:DropDownList ID="radMachine" width="25%" runat="server"> </asp:DropDownList>
                                     </div>   
                               
                             <br />
                             <br />
                                <div class="col-lg-12 "> 
                                <asp:LinkButton ID="lnk_View" runat="server" class="btn btn-primary" OnClick="lnk_View_Click">View</asp:LinkButton>
                                </div>
                              <br /><br />
                          
                              </center>
                    </div>


                    <div class="clearfix"></div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rdHistory" runat="server">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th>Venue</th>
                                                    <th>Location</th>
                                                    <th>Till</th>
                                                    <th>Master Page</th>
                                                    <th>Sync Status</th>
                                                    <th>Sync Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="background-color: #ffffff;"><%# Eval("venue_name")%></td>
                                                <td style="background-color: #ffffff;"><%# Eval("location")%></td>
                                                <td style="background-color: #ffffff;"><%# Eval("till")%></td>
                                                <td style="background-color: #ffffff;"><%# Eval("Page_name")%></td>
                                                <td style="background-color: #ffffff;"><%# Eval("Sync_status")%></td>
                                                <td style="background-color: #ffffff;"><%# Eval("sync_date")%></td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <th>Venue</th>
                                    <th>Location</th>
                                    <th>Till</th>
                                    <th>Master Page</th>
                                    <th>Sync Status</th>
                                    <th>Sync Date</th>
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


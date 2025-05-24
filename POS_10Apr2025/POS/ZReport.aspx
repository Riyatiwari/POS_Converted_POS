<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ZReport.aspx.vb" Inherits="ZReport" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Z Report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Z Report</li>
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

        var main0;
        var Duration0;
        var Venue0;
        var Location0;
        var Till0;
        var salesType0;
        var radshifttype0;
        var txtemail0;
        var txtForDate0;
        var txtToDate0;
        var NumberOfTran0;

        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid();

        });

        function disp_alert() {

            Duration0 = $('[id*="radDuration"]').val();
            salesType0 = $('[id*="radsalesType"]').val();
            txtemail0 = document.getElementById('<%= txtemail.ClientID %>').value

            if ($('[id*="radVenue"]').val() == 'SELECT') {
                Venue0 = ''
            }
            else {
                Venue0 = $('[id*="radVenue"]').val();
            }
            if ($('[id*="radLocation"]').val() == 'SELECT') {
                Location0 = ''
            }
            else {
                Location0 = $('[id*="radLocation"]').val();
            }

            if ($('[id*="radMachine"]').val() == 'SELECT') {
                Till0 = ''
            }
            else {
                Till0 = $('[id*="radMachine"]').val();
            }

            if ($('[id*="radshifttype"]').val() == 'SELECT') {
                radshifttype0 = ''
            }
            else {
                radshifttype0 = $('[id*="radshifttype"]').val();
            }


            if (Duration0 == 'Custom') {
                txtToDate0 = document.getElementById('<%= txtToDate.ClientID %>').value
                txtForDate0 = document.getElementById('<%= txtForDate.ClientID %>').value
            }
            else {
                txtForDate0 = ''
                txtToDate0 = ''
            }

            NumberOfTran0 = document.getElementById('<%= hf_NumOfTran.ClientID %>').value

        }

        function Grid() {

            disp_alert();

            var newLine = "\r\n"

            $('#Psummary').prepend('<tr style="display:none"><td></td><td></td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> Number of Transaction :</td><td>' + NumberOfTran0 + '</td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> Shift type :</td><td>' + radshifttype0 + '</td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> Sales Type :</td><td> ' + salesType0 + '</td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> Till : </td><td> ' + Till0 + ' </td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> Location :</td><td>' + Location0 + '</td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> Venue :</td><td> ' + Venue0 + ' </td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> Duration :</td><td>' + Duration0 + '</td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> To Date :</td> <td> ' + txtToDate0 + '</td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td> From Date :</td><td>' + txtForDate0 + '</td></tr>');
            $('#Psummary').prepend('<tr style="display:none"><td></td><td></td></tr>');

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
                    {
                        extend: 'excel',
                        title: 'Z Report'
                    }
                ],

                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": true, "targets": +groupCol },
                    {
                        'orderable': false,
                        'info': false,
                    }
                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 100,
            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {

                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%;visibility: hidden;display:none;" placeholder="" />');
            });

        }

        function tSpeedValue(txt) {

            var at = txt.value;

        }
    </script>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <div class="panel panel-yellow" id="dinction" runat="server">
            <div class="panel-heading">Z Report</div>
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
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch"  OnSelectedDateChanged="txtForDate_SelectedDateChanged" AutoPostBack="true">
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
                                <telerik:RadDatePicker ID="txtToDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch" OnSelectedDateChanged="txtToDate_SelectedDateChanged" AutoPostBack="true">
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
                                  
                            <br />
                            <div class="col-lg-6 ">
                                          Sales type : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radsalesType" runat="server" AutoPostBack="true" >
                                             <Items>
                                                <telerik:RadComboBoxItem Text="All" Value="All" />
                                                 <telerik:RadComboBoxItem Text="Till" Value="Till" />
                                                <telerik:RadComboBoxItem Text="Online" Value="Online" />                                                 
                                            </Items>
                                        </telerik:RadComboBox>                                      
                                     </div>
                            
                            <div class="col-lg-6 ">
                                          Shift : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radshifttype" runat="server" AutoPostBack="true" >                                            
                                        </telerik:RadComboBox>                                      
                                     </div>
                            <br />
                                          <br />
                            <div class="col-lg-6 ">&nbsp;
                                     <asp:LinkButton ID="btnemail" runat="server" class="btn btn-primary btn-sm" OnClick="btnemail_Click" ToolTip="View Report in Email" ValidationGroup="valid" Text="Send Email"></asp:LinkButton>                                          
                                &nbsp;&nbsp;
                            <asp:TextBox ID="txtemail" runat="server" >
                                        </asp:TextBox>    
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                Display="Dynamic" ErrorMessage="Invalid email address" />    
                                </div>
                              <div class="col-lg-6 ">
                                     Operator : &nbsp;
                                        <telerik:RadComboBox ID="rdOperator" runat="server">
                                        </telerik:RadComboBox>
                                      
                                     </div>  
                            
                             <br />
                                          <br />
                                <div class="col-lg-12 "> 
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary"  ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                                     </div>
                              <br /><br />
                          
                                <div  id="dincgtion" runat="server" >                            
                                    <br />
                                <div class="col-lg-12 ">
                                    <table>
                                       <tr><h1 style="color:black">Z Report</h1><br /></tr> 
                                        <tr><td style="color:black " id="tdAddress" runat="server">  </td>
                                         </tr>                                        
                                       
                                    </table>
                                    <br />
                                    <br />
                                </div>
                                    <br />
                                <div>
                                    <table style="width: 60%;"  >
                                        <tr>
                                            <td colspan="5">First Transaction Date : <asp:label ID="lblFirst" runat="server"></asp:label></td>
                                             <td style="text-align: right;">From Date: <asp:label ID="lblfromdate" runat="server"></asp:label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">Last Transaction Date : <asp:label ID="lblLsttransction" runat="server"></asp:label></td>
                                            <td style="text-align: right;"> To Date: <asp:label ID="lblTodate" runat="server"></asp:label></td>
                                             </tr>
                                        <tr>
                                            <td colspan="5">Number Of Transaction: <asp:label ID="lblNoofTransaction" runat="server"></asp:label></td>
                                             <td style="text-align: right;">Number Of Returns: <asp:label ID="lblReturn" runat="server"></asp:label></td>
                                        </tr>
                                    </table>
                                </div>
                                    <br />
                                     </div>
                                 <div style="width:70%">
                                    <div class="card-body">
                    <div class="table-responsive" >
                        <table class="table table-bordered"  id="Psummary" cellspacing="0">
                            <asp:HiddenField ID="hf_NumOfTran" runat="server" Value='0' />
                            <asp:Repeater ID="rptzSUmmary" OnItemDataBound="rptzSUmmary_ItemDataBound" runat="server">                                                               
                                <HeaderTemplate>
                                    <thead>                                        
                                        <tr>
                                              <th>Description</th>
                                            <th>Number</th>
                                         </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>                                    
                                    <tr>
                                         <td style="background-color: #ffffff;">
                                             <%# Eval("Description")%>
                                             
                                         </td>
                                        <td style="background-color: #ffffff;"><%#Eval("Number")%></td>

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




<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Transaction_Details.aspx.vb" Inherits="Stock_Transaction_Details" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   Stock Transaction Detail
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Stock Transactions Detail </li>
        </ol>
    </div>
      <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>



    <%--<link href="css2/sb-admin-2.min.css" rel="stylesheet" />--%>
    <script type="text/javascript" language="javascript">

        function numberWithCommas(number) {
            var parts = number.toString().split(".");
            parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return parts.join(".");
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
                "search": {
                    "search": $('#<%=hdsearchvalueAfterEdit.ClientID%>').val()
                },

                orderCellsTop: true,
                dom: 'Bfrtip',
                "buttons": [
                    'excel', 'pdf'

                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
                ],

                "order": [[groupCol, 'asc']],
                "displayLength": 1500,
                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;


                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {
                       
                        if (last !== group) {

                            $(rows).eq(i).before(
                                '<tr class="group"><td colspan="11"> Product Group : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });
                }
            });
            
            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                stateSave: true
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
                // alert($(this).val());
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }


    </script>
</asp:Content>

 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">

        <div class="panel panel-yellow">
            <div class="panel-heading">Stock Transactions Detail Report </div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                        </div>
                    </div>
                    <br />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                    <div class="row">
                        <center>
                              <center>
                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="valid"
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
                                       <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date"  Display="None"  ValidationGroup="valid" ControlToCompare="txtFrom" ControlToValidate="txtTo" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                     </div>
                                  </div>
                                <div class="clearfix"></div>
                                <br />
                                 <div class="col-lg-6">                                     
                                  Duration : &nbsp;
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
                             Location : &nbsp;
                            <telerik:RadComboBox ID="radLocation" runat="server" >
                                        </telerik:RadComboBox>
                        </div>

                               
                                                               
                                 <div class="clearfix"></div>
                                <br />

                          <div class="col-lg-6 ">
                                     Product Group : &nbsp;
                                        <telerik:RadComboBox ID="radCategory" OnSelectedIndexChanged="radCategory_SelectedIndexChanged" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>

                                 <div class="col-lg-6 ">
                                      Product : &nbsp;
                                        <telerik:RadComboBox ID="radProduct" runat="server" >
                                        </telerik:RadComboBox>
                                     </div>
                                
                     
                                 
                               
                                <div class="clearfix"></div>
                                <br />
                        <div class="col-lg-6" style="visibility:hidden;">
                                    Product Return &nbsp;
                                            <asp:CheckBox ID="chkReturn" runat="server" />
                                       
                                    </div>

                          <div class="col-lg-6" style="visibility:hidden;">          
                                            <asp:RadioButtonList ID="rdType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" style="margin-left :108px;">
                                                <asp:ListItem Selected="True" Text="&nbsp;ALL&nbsp;" Value="ALL"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;SALE&nbsp;" Value="SALE"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;RETURN&nbsp;" Value="RETURN"></asp:ListItem>
                                            </asp:RadioButtonList>              
                                   </div>
                         <div class="clearfix"></div>
                                <br />
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                                     
                               
                              <br /><br />
                              <div class="col-lg-12 " >
                               </div>
                              </center> 
                              <br /><br />
                                  <div style="text-align:right;padding-right:25px;">
                                        <asp:ImageButton Visible="false" ID="btnexp" OnClick="btnExcl_ServerClick" runat="server" ImageUrl="~/images/excel.png" Height="30px"  />  
                                </div>
                              <div class="col-lg-12 " >
                                  
                                  

                                       <div class="card-body">
                                <div class="table-responsive">

                                    <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">

                                        <asp:Repeater ID="rpstockSUmmary" runat="server">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th class="header">Product Group</th>
                                                        <th class="header">Product</th>
                                                        <th class="header">Base</th>
                                                        <th class="header">Type</th>
                                                        <th class="header">Qty</th>
                                                        <th class="header">Opening Stock</th>
                                                        <th class="header">Variance</th>
                                                        <th class="header">End Qty</th>
                                                        <th class="header">Username</th>
                                                        <th class="header">Date</th>
                                                        <th class="header">Time</th>
                                                        <th class="header">Location</th>
                                                        
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <tr>
                                                    <td class="body"><%#Eval("Productgroup")%></td>
                                                    <td class="body"><%#Eval("Product")%></td>
                                                    <td class="body"><%#Eval("Base")%></td>
                                                    <td class="body"><%#Eval("Type")%></td>
                                                    <td class="body"><%#Eval("Qty")%></td>
                                                    <td class="body"><%#Eval("Opening Stock")%></td>
                                                    <td class="body"><%#Eval("Variance")%></td>
                                                    <td class="body"><%#Eval("End Qty")%></td>
                                                    <td class="body"><%#Eval("Username")%></td>
                                                    <td class="body"><%#Eval("Date")%></td>
                                                    <td class="body"><%#Eval("Time")%></td>
                                                    <td class="body"><%#Eval("Location")%></td>
                                                   
                                                   
                                                   
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>

                                <tr>
                                                        <th class="header">Product Group</th>
                                                        <th class="header">Product</th>
                                                        <th class="header">Base</th>
                                                        <th class="header">Type</th>
                                                        <th class="header">Qty</th>
                                                        <th class="header">Opening Stock</th>
                                                        <th class="header">Variance</th>
                                                        <th class="header">End Qty</th>
                                                        <th class="header">Username</th>
                                                        <th class="header">Date</th>
                                                        <th class="header">Time</th>
                                                        <th class="header">Location</th>
                                  
                                </tr>
                                </table>
                            </tfoot>

                                            </FooterTemplate>
                                        </asp:Repeater>


                                    </table>
                                </div>
                            </div>

                                  <br />
                              
                                  
                                    </div>
                              </center>
                    </div>
                </div>
            </div>

        </div>


    </div>
       
    <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />
     <asp:HiddenField runat="server" ID="hdnsearchvalue" />
    
    <asp:HiddenField runat="server" ID="hdsearchvalueSize" />
    <asp:HiddenField runat="server" ID="hdsearchvalueafterEditSize" />



    <style>
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
                text-align: right;
            }


            .mGrid th {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .mGrid .alt {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }

            .mGrid .pgr {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    r: #66;
                    oration: none;
                    .mGrid .pgr a:hover

        {
            color: #000;
            text-decoration: none;
        }
    </style>
     
    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
            ffff;
            der: #111111 1px olid;
        }

        .row th {
            background-color: #ffffff !important;
        }
    </style>
</asp:Content>

<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Table_Report.aspx.vb" Inherits="Table_Report" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Table Report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Table Report</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>



    <%--<link href="css2/sb-admin-2.min.css" rel="stylesheet" />--%>
    <script type="text/javascript">

        var RcptDetails = "";
        function GetChildGridData(ref_id, d1, d2) {
            
            var results = new Array();

            var resuljk = $.ajax({
                type: 'POST',
                url: 'Table_Report.aspx/GetRcptDetails',
                data: '{ref_id: "' + ref_id + '",date1: "' + d1 + '",Location_Id: "' + d2 + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


        function OnSuccess(response) {
            RcptDetails = "";
            RcptDetails = response.d;
            var RcptDetails = $.parseJSON(response.d);
            if (RcptDetails.length > 0) {
                for (var i = 0; i < RcptDetails.length; i++) {
                    var row = "<tr><td></td><td>" + RcptDetails[i].Product + "</td><td>" + RcptDetails[i].Size_Name + "</td><td>" + RcptDetails[i].quntity + "</td><td>" + RcptDetails[i].sales_discount + "</td><td>" + RcptDetails[i].sales_total_amount + "</td><td>" + RcptDetails[i].Date + "</td></tr>";
                    $('#resultTable').append(row);
                }
            }
        }
                     

        var groupCol = 0
        var groupTitle = ""
        $(document).ready(function () {


            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');


            Grid();


            //var tablepB = $('#Psummary').DataTable(); {

            //    tablepB.column(7).visible(false);

            //}
            
        });



        function Grid() {

            var tableLC = $('#Psummary').DataTable();
            locIDD = tableLC.column(6).data();


            $("#Psummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );



            var table = $('#Psummary').DataTable({



                orderCellsTop: true,
                dom: 'Bfrtip',

                buttons: [
                    'excelHtml5'


                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "ordering": false,
                //"columnDefs": [
                //    { "visible": false, "targets": + groupCol }
                //],
                //"order": [[groupCol, 'asc']],

                //"columnDefs": [{ "targets": -1, "data": null, 
                //    "defaultContent": "<input id='btnDetails' class='btn btn-success' width='25px' value='Get Details' />"  }],

                "dislayLength": 50,
                "searching": true,
                "drawCallback": function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;
                    var subTotal = new Array();
                    var groupID = -1;
                    var aData = new Array();
                    var index = 0;
                    var symbol = ""







                }
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

            $('#Psummary tbody').on('click', 'td.details-control', function () {
               
                var tr = $(this).closest('tr');
                var row = table.row(tr);
                 
                var d1 = $("#ContentPlaceHolder1_hdnfromdate").val($("#ctl00_ContentPlaceHolder1_txtFrom").val()).val();
                var d2 = $("#ContentPlaceHolder1_hdntodate").val($("#ctl00_ContentPlaceHolder1_txtTo").val()).val();
               
                if (row.child.isShown()) {
                    row.child.hide();
                    // tr.find("td").eq(0).toggleClass('fa-minus-square');
                    tr.find("td").eq(8).removeClass('fa fa-minus');
                    tr.find("td").eq(8).addClass('fa fa-plus');
                    //toggleClass();
                     
                }
                else {
                    
                    tr.find("td").eq(8).removeClass('fa fa-plus');
                    tr.find("td").eq(8).addClass('fa fa-minus');
                    //tr.find("td").eq(0).toggleClass('fa fa-plus');
                    
                    var ref_id = "";
                    var ref_id = tr.find("td").eq(0).html();
                    // var StkRcptId = tr.find("td:eq(0)").text();
                    // Open this row
                                       
                    //var loc = currentRow.find("td:eq(6)").text(); 
                    //alert(loc);

                    table1.rows().every(function () {
                        this

                            .child(
                                $(
                                    '<div>' +
                                    '<table id="resultTable"  class="table table-bordered display">' +
                                    '<tr>' +
                                    '<th></th>' +
                                    '<th>Product</th>' +
                                    '<th>Size Name</th>' +
                                    '<th>Qty.</th>' +
                                    '<th>Discount</th>' +
                                    '<th>Total Amount</th>' +
                                    '<th>Date</th>' +
                                    '</tr>' +
                                    '</table>' +
                                    '</div>'
                                )
                            )
                            .hide();
                    });
                    d1 = tr.find("td").eq(6).html();
                    var Id = tr.find("td").eq(7).html();
                     
                    d2 = Id
                   
                    GetChildGridData(ref_id, d1, d2);
                    row.child.show();
                }
                
            });
                                          
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-yellow">
        <div class="panel-heading">Table Report</div>
        <div class="panel-body pan">
            <div class="form-body pal">
                <div class="row">
                    <div class="col-lg-12 ">
                    </div>
                </div>
                <br />
                <div class="row">
                    <asp:HiddenField runat="server" ID="hdlocId" />
                    <asp:HiddenField ID="hdnfromdate" runat="server" />
                    <asp:HiddenField ID="hdntodate" runat="server" />
                    <asp:HiddenField ID="hfSizelocation" runat="server" Value="0" />
                    <center>
                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                              <div  id="divcustom" runat="server"  >
                            <div class="col-lg-6 ">
                                From Date : &nbsp;
                                <telerik:RadDatePicker ID="txtFrom" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch"  AutoPostBack="true">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday" >
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                  
                               </div>    
                                 <div class="col-lg-6 " >                                     
                                  To Date : &nbsp;
                                <telerik:RadDatePicker ID="txtTo" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch"  AutoPostBack="true">
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
                                 <div class="col-lg-6" runat="server" visible="false" >                                     
                                   Duration :&nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radDuration" runat="server" AutoPostBack="true" >
                                             <Items>
                                                <telerik:RadComboBoxItem Text="Today" Value="Today"/>
                                                 <telerik:RadComboBoxItem Text="Yesterday" Value="Yesterday" />
                                                <telerik:RadComboBoxItem Text="This Week" Value="This Week" />
                                                 <telerik:RadComboBoxItem Text="This Month" Value="This Month"  />
                                                 <telerik:RadComboBoxItem Text="This Year" Value="This Year" />
                                                 <telerik:RadComboBoxItem Text="Last Week" Value="Last Week" />
                                                 <telerik:RadComboBoxItem Text="Last Month" Value="Last Month"/>
                                                 <telerik:RadComboBoxItem Text="Last Year" Value="Last Year" />
                                                 <telerik:RadComboBoxItem Text="Custom" Value="Custom"  Selected="true"/>
                                            </Items>
                                        </telerik:RadComboBox>
                                      
                                     </div> 
                        
                         <div class="col-lg-6 ">
                                   Venue :   &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radVenue" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>
                          <div class="col-lg-6 ">
                                      Location :&nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radLocation"  runat="server" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                     </div>
                                      <div class="clearfix"></div>
                                <br />
                         

                                              
                                 
                                                               
                              
                                    <div class="col-lg-6 ">
                                       Till : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radMachine" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>  

                                      <div class="clearfix"></div>
                                <br />
                               
                               
                              <div class="col-lg-12 " >
                               </div>
                              </center>
                </div>
                <div style="float: right; margin-right: 20px;">


                    <div class="clearfix"></div>
                    <br />

                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>


                    <br />
                    <br />
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">



                            <asp:Repeater ID="rptProductSUmmary" runat="server">
                                <HeaderTemplate>

                                    <thead>
                                        <tr>
                                            <asp:Repeater ID="Header1" runat="server">

                                                <ItemTemplate>

                                                    <th><%# Container.DataItem %>
                                                    </th>
                                                </ItemTemplate>

                                            </asp:Repeater>

                                            <th></th>
                                            <th style="display: none"></th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <asp:Repeater ID="Item1" runat="server">

                                            <ItemTemplate>


                                                <td style="background-color: #ffffff;"><%# Container.DataItem %></td>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <td class="details-control fa fa-plus" style="width: 30px;"></td>
                                        <td style="display: none"><%#Eval("ref_id")%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            <tfoot>
                                </table>
                            </tfoot>

                                </FooterTemplate>
                            </asp:Repeater>
                    </div>
                </div>

            </div>
        </div>

    </div>




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

<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="PayGatewaySummary.aspx.vb" Inherits="PayGatewaySummary" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Payment Gateway Summary
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Payment Gateway Summary</li>
        </ol>
    </div>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript">
        var groupCol = 0
        var groupTitle = ""
        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');
            Grid();

            $('#Psummary1 thead tr').clone(true).appendTo('#Psummary1 thead');

            var chkVenu = $("#<%= chkPs2.ClientID %>").is(":checked");
            var chkTill = $("#<%= chkPG1.ClientID %>").is(":checked");
            var chkLocation = $("#<%= chkPs4.ClientID %>").is(":checked");
            if (chkVenu == true) {
                groupCol = 1;
                groupTitle = "Reseller";
            }
            if (chkTill == true) {
                groupCol = 3;
                groupTitle = "Terminal";
            }
            if (chkLocation == true) {
                groupCol = 2;
                groupTitle = "Merchant";
            }

            Grid1();
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
                "searching": true,
                "ordering": false

            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {

                var title = $(this).text();
                $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');

                $('input', this).on('keyup change', function () {

                    if (table1.column(i).search() !== this.value) {
                        table1.column($(this).parent().index() + ':visible')
                            .search(this.value)
                            .draw();

                    }
                    else {
                        table1.column($(this).parent().index() + ':visible')
                            .search('')
                            .draw();
                    }
                });
            });
        }


        function Grid1() {

            var chkVenu = $("#<%= chkPs2.ClientID %>").is(":checked");
            var chkTill = $("#<%= chkPG1.ClientID %>").is(":checked");
            var chkLocation = $("#<%= chkPs4.ClientID %>").is(":checked");
            if (chkVenu == true) {
                groupCol = 0;
                groupTitle = "Reseller";
            }
            if (chkTill == true) {
                groupCol = 2;
                groupTitle = "Terminal";
            }
            if (chkLocation == true) {
                groupCol = 1;
                groupTitle = "Merchant";
            }

            $("#Psummary1 tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var table = $('#Psummary1').DataTable({

                orderCellsTop: true,
                dom: 'Bfrtip',

                buttons: [
                    {
                        extend: 'excel',
                        exportOptions: {
                            format: {
                                body: function (data, row, column, node) {
                                    return data.replace('£', '').replace('$', '').replace('R', '');
                                }
                            }
                        }
                    }
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 50,
                "searching": true,
                "drawCallback": function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var aData = new Array();
                    var symbol = "";

                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {

                        var vals = api.row(api.row($(rows).eq(i)).index()).data();

                        var salary1 = vals[6] ? (vals[6]) : 0;
                        symbol = salary1.charAt(0);

                        var salary = parseFloat(salary1.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));

                        if (typeof aData[group] == 'undefined') {

                            aData[group] = new Array();
                            aData[group].rows = [];
                            aData[group].salary = [];

                        }

                        aData[group].rows.push(i);
                        aData[group].salary.push(salary);

                    });

                    var idx = 0;
                    var idx1 = 0;
                    var p_val = 0;

                    //Grand Total 
                    var G_Total = document.getElementById('<%= hd_GTotal.ClientID %>').value;
                    var TotalTill = document.getElementById('<%= hd_TotalTill.ClientID %>').value;
                    $(rows).eq(0).before(
                        '<tr class="group"><td colspan="5"> Grand Total </td>' +
                        '<td> ' + symbol + ' ' + parseFloat(G_Total).toFixed(2) + '  </td> ' +
                        '<td  colspan="3"> ' + symbol + ' ' + parseFloat(TotalTill).toFixed(2) + '  </td> ' +
                        '</tr > '
                    );

                    for (var Venue in aData) {
                        idx = Math.max.apply(Math, aData[Venue].rows);
                        var sum = 0.0;

                        $.each(aData[Venue].salary, function (k, v) {

                            sum = sum + v;
                        });

                        if (idx1 == 0) {
                            if (p_val == 1) {

                                $(rows).eq(idx1).after(
                                    '<tr class="group"><td colspan="5">   ' + groupTitle + ' : ' + Venue + '</td>' +
                                    '<td  colspan="4">' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td> ' +
                                    '</tr > '
                                );
                            }
                            else {
                                $(rows).eq(idx1).before(

                                    '<tr class="group"><td colspan="5"> ' + groupTitle + ' : ' + Venue + '</td>' +
                                    '<td  colspan="4">' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td> ' +
                                    '</tr > '
                                );

                                p_val = idx1 + 1;
                            }
                        }
                        else {
                            $(rows).eq(idx1).after(
                                '<tr class="group"><td colspan="5">   ' + groupTitle + ' : ' + Venue + '</td>' +
                                '<td  colspan="4">' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td> ' +
                                '</tr > '
                            );
                        }

                        idx1 = idx;
                    };
                }
            });

            var table1 = $('#Psummary1').DataTable();
            $('#Psummary1 thead tr:eq(1) th').each(function (i) {
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

    <script lang="javascript" type="text/javascript">

        function openNewWin(url) {
            var x = window.open(url, 'Payment Gateway Detail', 'width=800,height=600,toolbar=1');
            x.focus();
        }
    </script>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-yellow">
        <div class="panel-heading">Payment Gateway Summary Report</div>
        <div class="panel-body pan">
            <div class="form-body pal">
                <div class="row">
                    <asp:HiddenField ID="hfSizelocation" runat="server" Value="0" />
                    <center>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                 DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                            <div  id="divcustom" runat="server" visible="false" >
                            <div class="col-lg-6 ">
                                From Date : &nbsp;
                                <telerik:RadDatePicker ID="txtFrom" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch" OnSelectedDateChanged="txtFrom_SelectedDateChanged"  AutoPostBack="true">
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
                                <telerik:RadDatePicker ID="txtTo" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch" OnSelectedDateChanged="txtTo_SelectedDateChanged" AutoPostBack="true">
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
                                  Duration : &nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radDuration" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radDuration_SelectedIndexChanged" >
                                             <Items>
                                                <telerik:RadComboBoxItem Text="Today" Value="Today"  Selected="true"/>
                                                 <telerik:RadComboBoxItem Text="Yesterday" Value="Yesterday" />
                                                <telerik:RadComboBoxItem Text="This Week" Value="This Week" />
                                                 <telerik:RadComboBoxItem Text="This Month" Value="This Month"  />
                                                 <telerik:RadComboBoxItem Text="This Year" Value="This Year" />
                                                 <telerik:RadComboBoxItem Text="Last Week" Value="Last Week" />
                                                 <telerik:RadComboBoxItem Text="Last Month" Value="Last Month"/>
                                                 <telerik:RadComboBoxItem Text="Last Year" Value="Last Year" />
                                                 <telerik:RadComboBoxItem Text="Custom" Value="Custom" />
                                            </Items>
                                        </telerik:RadComboBox>
                                     </div> 

                         <div class="col-lg-6 ">
                                          Merchant ID : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radMerchantID" runat="server" >
                                        </telerik:RadComboBox>   
                                     </div>
                       

                         <div class="clearfix"></div>
                         <br />
                         <div class="col-lg-6 " style="display :none ;">
                                          Sales type : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radsalesType" runat="server" AutoPostBack="true" >
                                             <Items>
                                                <telerik:RadComboBoxItem Text="All" Value="All" />
                                                 <telerik:RadComboBoxItem Text="Till" Value="Till" />
                                                <telerik:RadComboBoxItem Text="Online" Value="Online" />                                                 
                                            </Items>
                                        </telerik:RadComboBox>   
                                     </div>

                          <div class="col-lg-6 " style="display :none ;">
                                         &nbsp;&nbsp; Shift : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radshifttype" runat="server" AutoPostBack="true" >                                            
                                        </telerik:RadComboBox>  
                                     </div>
                                       
                         <div class="col-lg-6 ">
                                          Reseller : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="RadReseller" runat="server" >
                                             </telerik:RadComboBox>   
                                     </div>

                       
                        
                                 <div class="clearfix"></div>
                                <br />
                                
                              <div class="col-lg-12 " >
                               </div>
                              </center>
                </div>
                <div class="row">
                    <div style="float: right; margin-right: 20px;">
                        <table>

                            <tr>
                                <td>
                                    <u>
                                        <asp:CheckBox ID="chkDateWise" runat="server" Checked="true" />&nbsp; Date Wise
                                    </u>&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td><b>Group By : </b>&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:RadioButton ID="chkPs2" GroupName="groupby" runat="server" Checked="true" Text="Reseller" />

                                </td>
                                <td>
                                    <asp:RadioButton ID="chkPG1" GroupName="groupby" runat="server" Text="Terminal" />
                                </td>

                                <td>
                                    <asp:RadioButton ID="chkPs4" GroupName="groupby" runat="server" Text="Merchant" />
                                </td>

                            </tr>
                        </table>

                        <div class="clearfix"></div>
                        <br />

                    </div>
                </div>
                <div class="row">
                    <center>
                        
                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid"
                        OnClick="LinkButton1_Click">View</asp:LinkButton>
                    <br />
                    <br />
                    </center>
                </div>

                <div>
                    <asp:HiddenField runat="server" ID="hd_GTotal" />
                    <asp:HiddenField runat="server" ID="hd_TotalTill" />
                </div>

                <div class="card-body" runat="server" id="div_rpt1" visible="false">
                    <div class="table-responsive">
                        <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">

                            <asp:Repeater ID="rptProductSUmmary" runat="server" OnItemCommand="rptProductSUmmary_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Resaller</th>
                                            <th>Merchant</th>
                                            <th>Terminal</th>
                                            <th>Date</th>
                                            <th>Time</th>
                                            <th>Ref</th>
                                            <th>Total Card</th>
                                            <th>Total Till</th>
                                            <th>CardType</th>
                                            <th>AuthMessage</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnExpand" CommandName="+" runat="server" Text="+" Style="font-weight: bold;" />
                                            <asp:HiddenField ID="hd_MerchantID" runat="server" Value='<%#Eval("MerchantID") %>' />
                                            <asp:HiddenField ID="hd_Date" runat="server" Value='<%#Eval("Pass_Date") %>' />
                                            <asp:HiddenField ID="hd_Duration" runat="server" Value='<%#Eval("Duration") %>' />
                                        </td>
                                        <td style="background-color: #ffffff;"><%#Eval("Resaller") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Merchant") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Terminal") %></td>
                                        <td style="background-color: #ffffff;">
                                            <a target="_blank" style="text-decoration: none;" href='<%# string.Format("PayGatewayListDetail.aspx?mID={0}&RID={1}&TID={2}&P_date={3}&Duration={4}&M_Name={5}", Eval("MerchantID"), Eval("RID"), "", Eval("Pass_Date"), Eval("Duration"), Eval("M_Name")) %>'>
                                                <%# Eval("Date", "{0:dd/MM/yyyy}") %> </a>
                                        </td>
                                        <td style="background-color: #ffffff;"><%#Eval("Time") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Ref") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Amount") %></td>
                                        <td style="background-color: #ffffff;">
                                            <a target="" style="text-decoration: none;" href='<%# string.Format("SignIn.aspx?SName={0}&fDate={1}&Duration={2}", Eval("gtway_StoreName") , Eval("Pass_Date"), Eval("Duration")) %>'>
                                                <%#Eval("TotalSales") %></a>
                                        </td>
                                        <td style="background-color: #ffffff;"><%#Eval("Card_Type") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("AuthMessage") %></td>
                                    </tr>

                                    <asp:Panel runat="server" ID="dtl_panel" Visible="false">
                                        <tr>
                                            <td></td>
                                            <td colspan="10">
                                                <asp:Repeater ID="rptOrders" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered" id="inner" cellspacing="0" rules="all" border="1"
                                                            style="margin-bottom: 0px;">
                                                            <tr>
                                                                <th>Resaller</th>
                                                                <th>Merchant</th>
                                                                <th>Terminal</th>
                                                                <th>Date</th>
                                                                <th>Time</th>
                                                                <th>Ref</th>
                                                                <th>Total Card</th>
                                                                <th>CardType</th>
                                                                <th>AuthMessage</th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="background-color: #ffffff;"><%#Eval("Resaller") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Merchant") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Terminal") %></td>
                                                            <td style="background-color: #ffffff;"><%# Eval("Date", "{0:dd/MM/yyyy}") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Time") %></td>
                                                            <td style="background-color: #ffffff;">
                                                                <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("Ref").ToString() %>' runat="server" OnClick="lnkview_Click">
                                                                        <%#Eval("Ref") %>               
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Amount") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("Card_Type") %></td>
                                                            <td style="background-color: #ffffff;"><%#Eval("AuthMessage") %></td>
                                                        </tr>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </asp:Panel>
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

                <div class="card-body" runat="server" id="div_rpt2" visible="false">
                    <div class="table-responsive">

                        <table class="table table-bordered" id="Psummary1" width="100%" cellspacing="0">
                            <asp:Repeater ID="rptResellerTerminal" runat="server">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>Resaller</th>
                                            <th>Merchant</th>
                                            <th>Terminal</th>
                                            <th>Date</th>
                                            <th>Time</th>
                                            <th>Ref</th>
                                            <th>Total Card</th>
                                            <th>Total Till</th>
                                            <th>CardType</th>
                                            <th>AuthMessage</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="background-color: #ffffff;"><%#Eval("Resaller") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Merchant") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Terminal") %></td>
                                        <td style="background-color: #ffffff;">
                                            <a target="_blank" style="text-decoration: none;" href='<%# string.Format("PayGatewayListDetail.aspx?mID={0}&RID={1}&TID={2}&P_date={3}&Duration={4}&M_Name={5}", Eval("MerchantID"), Eval("RID"), "", Eval("Pass_Date"), Eval("Duration"), Eval("M_Name")) %>'>
                                                <%# Eval("Date", "{0:dd/MM/yyyy}") %></a>
                                        </td>
                                        <td style="background-color: #ffffff;"><%#Eval("Time") %></td>
                                        <td style="background-color: #ffffff;">
                                            <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("Ref").ToString() %>' runat="server" OnClick="lnkview_Click">
                                                         <%#Eval("Ref") %>               
                                            </asp:LinkButton>
                                        </td>
                                        <td style="background-color: #ffffff;"><%#Eval("Amount") %></td>
                                        <td style="background-color: #ffffff;">
                                            <a target="" style="text-decoration: none;" href='<%# string.Format("SignIn.aspx?SName={0}&fDate={1}&Duration={2}", Eval("gtway_StoreName") , Eval("Pass_Date"), Eval("Duration")) %>'>
                                                <%#Eval("TotalSales") %></a>
                                        </td>
                                        <td style="background-color: #ffffff;"><%#Eval("Card_Type") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("AuthMessage") %></td>
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


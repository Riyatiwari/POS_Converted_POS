<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Management_report.aspx.vb" Inherits="Stock_Management_report" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Stock Report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Stock Report</li>
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

        var Duration0;
        var txtForDate0;
        var txtToDate0;


        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid();

        });

        function disp_alert() {

            Duration0 = $('[id*="radDuration"]').val();

            if (Duration0 == 'Custom') {
                txtToDate0 = document.getElementById('<%= txtToDate.ClientID %>').value
                txtForDate0 = document.getElementById('<%= txtForDate.ClientID %>').value
            }
            else {
                txtForDate0 = ''
                txtToDate0 = ''
            }
        }


        function Grid() {

            //disp_alert();
            //var html = "<td> Duration :</td><td>'" + Duration0 + "'</td>";

            //$('#Psummary').prepend(html);

            //$('#Psummary').prepend('test');
            //$('#Psummary').prepend('<tr style="display:none"><td> Duration :</td><td>' + Duration0 + '</td></tr>');
            //$('#Psummary').prepend('<tr style="display:none"><td> To Date :</td> <td> ' + txtToDate0 + '</td></tr>');
            //$('#Psummary').prepend('<tr style="display:none"><td> From Date :</td><td>' + txtForDate0 + '</td></tr>');
            //$('#Psummary').prepend('<tr style="display:none"><td></td><td></td></tr>');



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
                buttons: [
                    {
                        extend: 'excel',
                        exportOptions: {
                            format: {
                                body: function (data, row, column, node) {
                                    return data.replace('£', '').replace('$', '');
                                }
                            }
                        }

                    }
                ],
                //"buttons": [
                //    'excel'
                //],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol },
                    {
                        //'targets': [3], /* column index */
                        'orderable': false,
                        'info': false,
                    }
                ],
                "order": [[groupCol, 'asc']],
                "displayLength": 100,

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


                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {
                        var vals = api.row(api.row($(rows).eq(i)).index()).data();
                        var salary1 = vals[2] ? (vals[2]) : 0;
                        var sales1 = vals[3] ? (vals[3]) : 0;
                        var purchase1 = vals[4] ? (vals[4]) : 0;
                        var waste1 = vals[5] ? (vals[5]) : 0;
                        var stockTake1 = vals[6] ? (vals[6]) : 0;
                        var PostStock1 = vals[7] ? (vals[7]) : 0;
                        var stockIngredient1 = vals[8] ? (vals[8]) : 0;
                        var endStock1 = vals[9] ? (vals[9]) : 0;
                        var costofhand1 = vals[10] ? (vals[10]) : 0;


                        var salary = parseFloat(salary1.replace("Q", "").replace("£", "").replace(",", ""));
                        var sales = parseFloat(sales1.replace("Q", "").replace("£", "").replace(",", ""));
                        var purchase = parseFloat(purchase1.replace("Q", "").replace("£", "").replace(",", ""));
                        var waste = parseFloat(waste1.replace("Q", "").replace("£", "").replace(",", ""));
                        var stockTake = parseFloat(stockTake1.replace("Q", "").replace("£", "").replace(",", ""));
                        var PostStock = parseFloat(PostStock1.replace("Q", "").replace("£", "").replace(",", ""));
                        var stockIngredient = parseFloat(stockIngredient1.replace("Q", "").replace("£", "").replace(",", ""));
                        var endStock = parseFloat(endStock1.replace("Q", "").replace("£", "").replace(",", ""));
                        var costofhand = parseFloat(costofhand1.replace("Q", "").replace("£", "").replace(",", ""));

                        if (typeof aData[group] == 'undefined') {
                            aData[group] = new Array();
                            aData[group].rows = [];
                            aData[group].salary = [];
                            aData[group].sales = [];
                            aData[group].purchase = [];
                            aData[group].waste = [];
                            aData[group].stockTake = [];
                            aData[group].PostStock = [];
                            aData[group].stockIngredient = [];
                            aData[group].endStock = [];
                            aData[group].costofhand = [];

                        }

                        aData[group].rows.push(i);
                        aData[group].salary.push(salary);
                        aData[group].sales.push(sales);
                        aData[group].purchase.push(purchase);
                        aData[group].waste.push(waste);
                        aData[group].stockTake.push(stockTake);
                        aData[group].PostStock.push(PostStock);
                        aData[group].stockIngredient.push(stockIngredient);
                        aData[group].endStock.push(endStock);
                        aData[group].endStock.push(costofhand);

                    });

                    var idx = 0;
                    var idx1 = 0;
                    var last = null;
                    var p_val = 0;

                    for (var ProductGroup in aData) {

                        idx = Math.max.apply(Math, aData[ProductGroup].rows);
                        var sum = 0.0;
                        var sumsales = 0.0;
                        var sumpurchase = 0.0;
                        var sumwaste = 0.0;
                        var sumstockTake = 0.0;
                        var sumPostStock = 0.0;
                        var sumStockIngredient = 0.0;
                        var sumendStock = 0.0;
                        var sumcostofhand = 0.0;

                        $.each(aData[ProductGroup].salary, function (k, v) {
                            sum = sum + v;
                        });

                        $.each(aData[ProductGroup].sales, function (k, v) {
                            sumsales = sumsales + v;
                        });

                        $.each(aData[ProductGroup].purchase, function (k, v) {
                            sumpurchase = sumpurchase + v;
                        });

                        $.each(aData[ProductGroup].waste, function (k, v) {
                            sumwaste = sumwaste + v;
                        });

                        $.each(aData[ProductGroup].stockTake, function (k, v) {
                            sumstockTake = sumstockTake + v;
                        });

                        $.each(aData[ProductGroup].PostStock, function (k, v) {
                            sumPostStock = sumPostStock + v;
                        });

                        $.each(aData[ProductGroup].stockIngredient, function (k, v) {
                            sumStockIngredient = sumStockIngredient + v;
                        });

                        $.each(aData[ProductGroup].endStock, function (k, v) {
                            sumendStock = sumendStock + v;
                        });

                        $.each(aData[ProductGroup].costofhand, function (k, v) {
                            sumcostofhand = sumcostofhand + v;
                        });

                        if (idx1 == 0) {
                            if (p_val == 1) {
                                $(rows).eq(idx1).after(
                                    '<tr class="group"><td colspan="1">    Product Group  : ' + ProductGroup + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sum).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumsales).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumpurchase).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumwaste).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumstockTake).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumPostStock).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumStockIngredient).toFixed(2)) + '</td>' +
                                    ' <td colspan="2">    ' + numberWithCommas(parseFloat(sumendStock).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumcostofhand).toFixed(2)) + '</td>'+
                                    '</tr > '
                                );
                            }
                            else {

                                $(rows).eq(idx1).before(
                                    '<tr class="group"><td colspan="1">    Product Group  : ' + ProductGroup + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sum).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumsales).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumpurchase).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumwaste).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumstockTake).toFixed(2)) + '</td>' +
                                     ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumPostStock).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumStockIngredient).toFixed(2)) + '</td>' +
                                    ' <td colspan="2">    ' + numberWithCommas(parseFloat(sumendStock).toFixed(2)) + '</td>' +
                                    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumcostofhand).toFixed(2)) + '</td>' +
                                    '</tr > '
                                );


                                p_val = idx1 + 1;
                            }
                        }
                        else {

                            $(rows).eq(idx1).after(
                                '<tr class="group"><td colspan="1">    Product Group  : ' + ProductGroup + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sum).toFixed(2)) + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumsales).toFixed(2)) + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumpurchase).toFixed(2)) + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumwaste).toFixed(2)) + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumstockTake).toFixed(2)) + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumPostStock).toFixed(2)) + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumStockIngredient).toFixed(2)) + '</td>' +
                                ' <td colspan="2">    ' + numberWithCommas(parseFloat(sumendStock).toFixed(2)) + '</td>' +
                                ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumcostofhand).toFixed(2)) + '</td>' +
                                '</tr > '
                            );

                        }


                        idx1 = idx;


                        //$(rows).eq(idx).after(
                        //    '<tr class="group"><td colspan="1">    Product Group  : ' + ProductGroup + '</td>' +
                        //    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sum).toFixed(2)) + '</td>' +
                        //    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumsales).toFixed(2)) + '</td>' +
                        //    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumpurchase).toFixed(2)) + '</td>' +
                        //    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumwaste).toFixed(2)) + '</td>' +
                        //    ' <td colspan="1">    ' + numberWithCommas(parseFloat(sumstockTake).toFixed(2)) + '</td>' +
                        //    ' <td colspan="2">    ' + numberWithCommas(parseFloat(sumendStock).toFixed(2)) + '</td>' +
                        //    '</tr > '
                        //);
                    };
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
                <div class="panel-heading">Stock Report</div>
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
                                     Duration : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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


                                       <div class="col-lg-6 " style="visibility:hidden;">
                                     Flag : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="ddlflag" runat="server">
                                             <Items>
                                               <telerik:RadComboBoxItem Text="Changed" Value="1" Selected="true" />
                                                <telerik:RadComboBoxItem Text="Unchanged" Value="2"/>
                                            </Items>
                                        </telerik:RadComboBox>
                                    
                                     </div>     


                              
                                <br />   
                       
                                  <div class="col-lg-6 ">
                                     &nbsp;
                                     </div>   
                                <div class="clearfix"></div>
                                <br />
                                 <div class="col-lg-6 ">
                                     Product Group : &nbsp;&nbsp;
                                         <telerik:RadComboBox ID="radCategory" runat="server">
                                        </telerik:RadComboBox>
                                     </div>     
                                    <div class="col-lg-6 ">
                                     Location : &nbsp;&nbsp;
                                         <telerik:RadComboBox ID="radLocation" runat="server">
                                        </telerik:RadComboBox>
                                     </div> 
                                  <div class="col-lg-6 ">
                                     &nbsp;
                                     </div>   
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-lg-12 "> 
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid" PostBackUrl="~/Stock_Management_report.aspx">View</asp:LinkButton>
                                     </div>
                              <br /><br /><br />
                            
                              </center>
                            <div class="card-body">
                                <div class="table-responsive" style="overflow:scroll;">

                                    <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">

                                        <asp:Repeater ID="rpstockSUmmary" runat="server">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th class="header">ProductGroup</th>
                                                        <th class="header">Product</th>
                                                        <th class="header">First day On hand</th>
                                                        <th class="header">Sales</th>
                                                        <th class="header">Stock Purchase</th>
                                                        <th class="header">Wastage Qty</th>
                                                        <th class="header">Stock take Qty</th>
                                                        <th class="header">Post Stock</th>
                                                        <th class="header">Stock Used Ingredient</th>
                                                        <th class="header">Last day On hand</th>
                                                        <th class="header">Stock Receive Date</th>
                                                        <th class="header">Cost Of Stock On Hand</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <tr>
                                                    <td class="body"><%#Eval("Product_group")%></td>
                                                    <td class="body"><%#Eval("Product_name")%></td>
                                                    <td class="body"><%#Eval("start_QTY")%></td>
                                                    <td class="body"><%#Eval("sales")%></td>
                                                    <td class="body"><%#Eval("purchase_stock")%></td>
                                                    <td class="body"><%#Eval("westage_qty")%></td>
                                                    <td class="body"><%#Eval("stock_take_qty")%></td>
                                                     <td class="body"><%#Eval("post_stock")%></td>
                                                    <td class="body"><%#Eval("stock_used_ingredient")%></td>
                                                    <td class="body"><%#Eval("Ending_QTY")%></td>
                                                    <td class="body"><%#Eval("Stock_receive_date")%></td>
                                                    <td class="body"><%#Eval("cost_of_stock_on_hand")%> </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                            <tfoot>

                                <tr>

                                    <th class="header">Product Group</th>
                                    <th class="header">Product</th>
                                    <th class="header">First day On hand</th>
                                    <th class="header">Sales</th>
                                    <th class="header">Stock Purchase</th>
                                    <th class="header">Wastage Qty</th>
                                    <th class="header">Stock take Qty</th>
                                     <th class="header">Post Stock</th>
                                     <th class="header">Stock Used Ingredient</th>
                                    <th class="header">Last day On hand</th>
                                    <th class="header">Stock Receive Date</th>
                                     <th class="header">Cost Of Stock On Hand</th>
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



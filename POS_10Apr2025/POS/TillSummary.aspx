<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TillSummary.aspx.vb" Inherits="TillSummary" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Till Summary
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Till Summary</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <%--<link href="css2/sb-admin-2.min.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        var groupCol = 0
        var groupTitle = ""
        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            //  groupCol = $("input[name='groupby']:checked").val();
            var chkVenu = $("#<%= chkPs2.ClientID %>").is(":checked");  //$("input[name='groupby']:checked").val();
            var chkTill = $("#<%= chkPG1.ClientID %>").is(":checked");  //$("input[name='groupby']:checked").val();
            var chkLocation = $("#<%= chkPs4.ClientID %>").is(":checked");  //$("input[name='groupby']:checked").val();
            if (chkVenu == true) {
                groupCol = 0;
                groupTitle = "Venue";
            }
            if (chkTill == true) {
                groupCol = 0;
                groupTitle = "Till Total";
            }
            if (chkLocation == true) {
                groupCol = 1;
                groupTitle = "Location";
            }
            Grid();
        });

        function Grid() {

            var chkVenu = $("#<%= chkPs2.ClientID %>").is(":checked");  //$("input[name='groupby']:checked").val();
            var chkTill = $("#<%= chkPG1.ClientID %>").is(":checked");  //$("input[name='groupby']:checked").val();
            var chkLocation = $("#<%= chkPs4.ClientID %>").is(":checked");  //$("input[name='groupby']:checked").val();
            if (chkVenu == true) {
                groupCol = 0;
                groupTitle = "Venue";
            }
            if (chkTill == true) {
                groupCol = 0;
                groupTitle = "Total";
            }
            if (chkLocation == true) {
                groupCol = 1;
                groupTitle = "Location";
            }

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

                        var salary1 = vals[5] ? (vals[5]) : 0;
                        var gross1 = vals[6] ? (vals[6]) : 0;
                        var dis1 = vals[7] ? (vals[7]) : 0;
                        var net1 = vals[8] ? (vals[8]) : 0;
                        var Surcharge1 = vals[10] ? (vals[10]) : 0;
                        var VAT1 = vals[11] ? (vals[11]) : 0;
                        var VAT0 = vals[12] ? (vals[12]) : 0;
                        var VAT5 = vals[13] ? (vals[13]) : 0;
                        var VAT12 = vals[14] ? (vals[14]) : 0;
                        var VAT20 = vals[15] ? (vals[15]) : 0;
                        var dept11 = vals[16] ? (vals[16]) : 0;
                        var dept21 = vals[17] ? (vals[17]) : 0;
                        var dept31 = vals[18] ? (vals[18]) : 0;
                        var dept41 = vals[19] ? (vals[19]) : 0;
                        //var cat11 = vals[19] ? (vals[19]) : 0;
                        //var cat21 = vals[20] ? (vals[20]) : 0;
                        //var cat31 = vals[21] ? (vals[21]) : 0;
                        //var cat41 = vals[22] ? (vals[22]) : 0;
                        symbol = gross1.charAt(0);

                        var salary = parseFloat(salary1.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var gross = parseFloat(gross1.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var dis = parseFloat(dis1.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var net = parseFloat(net1.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var vat = parseFloat(VAT1.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var vat0 = parseFloat(VAT0.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var vat5 = parseFloat(VAT5.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var vat12 = parseFloat(VAT12.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var vat20 = parseFloat(VAT20.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));

                        var Surcharge = parseFloat(Surcharge1.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));

                        var dept1 = parseFloat(dept11.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var dept2 = parseFloat(dept21.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var dept3 = parseFloat(dept31.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));
                        var dept4 = parseFloat(dept41.replace("$", "").replace("£", "").replace("R", "").replace(",", ""));

                        //var cat1 = parseFloat(cat11.replace("$", "").replace("£", "").replace(",", ""));
                        //var cat2 = parseFloat(cat21.replace("$", "").replace("£", "").replace(",", ""));
                        //var cat3 = parseFloat(cat31.replace("$", "").replace("£", "").replace(",", ""));
                        //var cat4 = parseFloat(cat41.replace("$", "").replace("£", "").replace(",", ""));

                        if (typeof aData[group] == 'undefined') {

                            aData[group] = new Array();
                            aData[group].rows = [];
                            aData[group].salary = [];
                            aData[group].gross = [];
                            aData[group].dis = [];
                            aData[group].net = [];
                            aData[group].Surcharge = [];
                            aData[group].vat = [];
                            aData[group].vat0 = [];
                            aData[group].vat5 = [];
                            aData[group].vat12 = [];
                            aData[group].vat20 = [];

                            aData[group].dept1 = [];
                            aData[group].dept2 = [];
                            aData[group].dept3 = [];
                            aData[group].dept4 = [];

                            //aData[group].cat1 = [];
                            //aData[group].cat2 = [];
                            //aData[group].cat3 = [];
                            //aData[group].cat4 = [];

                        }

                        aData[group].rows.push(i);
                        aData[group].salary.push(salary);
                        aData[group].gross.push(gross);
                        aData[group].dis.push(dis);
                        aData[group].net.push(net);
                        aData[group].Surcharge.push(Surcharge);

                        aData[group].vat.push(vat);
                        aData[group].vat0.push(vat0);
                        aData[group].vat5.push(vat5);
                        aData[group].vat12.push(vat12);
                        aData[group].vat20.push(vat20);

                        aData[group].dept1.push(dept1);
                        aData[group].dept2.push(dept2);
                        aData[group].dept3.push(dept3);
                        aData[group].dept4.push(dept4);

                        //aData[group].cat1.push(cat1);
                        //aData[group].cat2.push(cat2);
                        //aData[group].cat3.push(cat3);
                        //aData[group].cat4.push(cat4);
                    });


                    var idx = 0;
                    var idx1 = 0;
                    var last = null;
                    var p_val = 0;

                    for (var Venue in aData) {

                        idx = Math.max.apply(Math, aData[Venue].rows);

                        var sum = 0.0;
                        var grosssum = 0.0;
                        var dissum = 0.0;
                        var netsum = 0.0;
                        var SurchargeSum = 0.0;
                        var vatSum = 0.0;
                        var vatSum0 = 0.0;
                        var vatSum5 = 0.0;
                        var vatSum12 = 0.0;
                        var vatSum20 = 0.0;
                        var dept1sum = 0.0;
                        var dept2sum = 0.0;
                        var dept3sum = 0.0;
                        var dept4sum = 0.0;

                        //var cat1sum = 0.0;
                        //var cat2sum = 0.0;
                        //var cat3sum = 0.0;
                        //var cat4sum = 0.0;


                        $.each(aData[Venue].gross, function (k, v) {

                            grosssum = grosssum + v;
                        });

                        $.each(aData[Venue].salary, function (k, v) {

                            sum = sum + v;
                        });
                        $.each(aData[Venue].Surcharge, function (k, v) {

                            SurchargeSum = SurchargeSum + v;
                        });
                        $.each(aData[Venue].dis, function (k, v) {

                            dissum = dissum + v;
                        });

                        $.each(aData[Venue].net, function (k, v) {

                            netsum = netsum + v;
                        });

                        $.each(aData[Venue].vat, function (k, v) {

                            vatSum = vatSum + v;
                        });

                        $.each(aData[Venue].vat0, function (k, v) {

                            vatSum0 = vatSum0 + v;
                        });

                        $.each(aData[Venue].vat5, function (k, v) {

                            vatSum5 = vatSum5 + v;
                        });
                        $.each(aData[Venue].vat12, function (k, v) {

                            vatSum12 = vatSum12 + v;
                        });

                        $.each(aData[Venue].vat20, function (k, v) {

                            vatSum20 = vatSum20 + v;
                        });

                        $.each(aData[Venue].dept1, function (k, v) {

                            dept1sum = dept1sum + v;
                        });
                        $.each(aData[Venue].dept2, function (k, v) {

                            dept2sum = dept2sum + v;
                        });
                        $.each(aData[Venue].dept3, function (k, v) {

                            dept3sum = dept3sum + v;
                        });
                        $.each(aData[Venue].dept4, function (k, v) {

                            dept4sum = dept4sum + v;
                        });
                        //$.each(aData[Venue].cat1, function (k, v) {

                        //    cat1sum = cat1sum + v;
                        //});
                        //$.each(aData[Venue].cat2, function (k, v) {

                        //    cat2sum = cat2sum + v;
                        //});
                        //$.each(aData[Venue].cat3, function (k, v) {

                        //    cat3sum = cat3sum + v;
                        //});

                        //$.each(aData[Venue].cat4, function (k, v) {

                        //    cat4sum = cat4sum + v;
                        //});


                        if (idx1 == 0) {
                            if (p_val == 1) {

                                $(rows).eq(idx1).after(
                                    '<tr class="group"><td colspan="4">   ' + groupTitle + ' : ' + Venue + '</td>' +
                                    '<td  colspan="1">' + ' ' + parseFloat(sum).toFixed(2) + '</td> ' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(grosssum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dissum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(netsum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">   </td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(SurchargeSum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum0).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum5).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum12).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum20).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept1sum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept2sum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept3sum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept4sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat1sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat3sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat3sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat4sum).toFixed(2) + '</td>' +
                                    '</tr > '
                                );


                            }
                            else {

                                $(rows).eq(idx1).before(
                                    '<tr class="group"><td colspan="4">   ' + groupTitle + ' : ' + Venue + '</td>' +
                                    '<td  colspan="1">' + ' ' + parseFloat(sum).toFixed(2) + '</td> ' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(grosssum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dissum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(netsum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">   </td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(SurchargeSum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum0).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum5).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum12).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum20).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept1sum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept2sum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept3sum).toFixed(2) + '</td>' +
                                    '<td  colspan="1">' + symbol + ' ' + parseFloat(dept4sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat1sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat3sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat3sum).toFixed(2) + '</td>' +
                                    //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat4sum).toFixed(2) + '</td>' +
                                    '</tr > '
                                );

                                p_val = idx1 + 1;
                            }

                        }
                        else {
                            $(rows).eq(idx1).after(
                                '<tr class="group"><td colspan="4">   ' + groupTitle + ' : ' + Venue + '</td>' +
                                '<td  colspan="1">' + ' ' + parseFloat(sum).toFixed(2) + '</td> ' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(grosssum).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(dissum).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(netsum).toFixed(2) + '</td>' +
                                '<td  colspan="1">   </td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(SurchargeSum).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum0).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum5).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum12).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(vatSum20).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(dept1sum).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(dept2sum).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(dept3sum).toFixed(2) + '</td>' +
                                '<td  colspan="1">' + symbol + ' ' + parseFloat(dept4sum).toFixed(2) + '</td>' +
                                //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat1sum).toFixed(2) + '</td>' +
                                //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat3sum).toFixed(2) + '</td>' +
                                //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat3sum).toFixed(2) + '</td>' +
                                //'<td  colspan="1">' + symbol + ' ' + parseFloat(cat4sum).toFixed(2) + '</td>' +
                                '</tr > '
                            );
                        }


                        idx1 = idx;

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
            var tableTemp = $('#Psummary').DataTable();

            if ($("#" + '<%= lbtnsize.ClientID %>').text() == 'Show Size & Location') {
                tableTemp.column(12).visible(false);
                tableTemp.column(11).visible(false);

            }
           
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                        <telerik:RadComboBox ID="radDuration" runat="server" AutoPostBack="true" >
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
                                         &nbsp;&nbsp; Shift : &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radshifttype" runat="server" AutoPostBack="true" >                                            
                                        </telerik:RadComboBox>                                      
                                     </div>
                                                               
                                 <div class="clearfix"></div>
                                <br />
                                
                              <div class="col-lg-12 " >
                               </div>
                              </center>
                </div>
                <div style="float: right; margin-right: 20px;">
                    <table>

                        <tr>
                            <td>
                                <u>
                                    <asp:CheckBox ID="chkDateWise" runat="server" Checked="true" />&nbsp; Date Wise
                                    <asp:LinkButton ID="lbtnsize" Visible="false" runat="server" OnClick="lbtnsize_Click">Show Department & Course</asp:LinkButton>
                                </u>&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td><b>Group By :   </b>&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <%-- <input type="radio" id="chkPs2" name="groupby"  title="Venue" checked="checked" value="0" onclick="Grid()" />--%>
                                <asp:RadioButton ID="chkPs2" GroupName="groupby" runat="server" Checked="true" Text="Venue" />
                                <%--  Venue --%>

                            </td>
                            <td>
                                <%--<input type="radio" id="chkPG1" name="groupby"  title="Till" value="2" onclick="Grid()" />--%>
                                <asp:RadioButton ID="chkPG1" GroupName="groupby" runat="server" Text="Till" />
                                <%--Till --%>

                            </td>


                            <td>
                                <%-- <input type="radio" id="chkPs4" name="groupby"  value="1" title="Location" onclick="Grid()" />--%>
                                <asp:RadioButton ID="chkPs4" GroupName="groupby" runat="server" Text="Location" />
                                <%-- Location--%>

                            </td>

                        </tr>
                    </table>

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

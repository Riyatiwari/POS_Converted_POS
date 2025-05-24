<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_return.aspx.vb" Inherits="Product_return" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     Product Return
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" Runat="Server">
 <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Product Return</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>




    <%--<link href="css2/sb-admin-2.min.css" rel="stylesheet" />--%>
    <script type="text/javascript">

        $(document).ready(function () {

            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid();

            var groupCol = $("input[name='groupby']:checked").val();

        });

        function Grid() {

            var groupCol = $("input[name='groupby']:checked").val();


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
                                    return data.replace('£', ' ').replace('$', ' ');
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

                        // console.log(group+">>>"+i);

                        var vals = api.row(api.row($(rows).eq(i)).index()).data();
                        var salary1 = vals[7] ? (vals[7]) : 0;
                        symbol = salary1.charAt(0);
                        var salary = parseFloat(salary1.replace("$", "").replace("£", "").replace(",", ""));

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
                    var hl_val = 0;
                    var hs_val = 0;
                    var other_val = 0;


                    for (var ProductGroup in aData) {
                        idx = Math.max.apply(Math, aData[ProductGroup].rows);

                        var sum = 0.0;
                        $.each(aData[ProductGroup].salary, function (k, v) {
                            sum = sum + v;
                        });


                        console.log(aData[ProductGroup].salary);

                        if (idx1 == 0) {
                            if ($("#" + '<%= lbtnsize.ClientID %>').text() == 'Show Location' && $("#" + '<%= LinksizeA.ClientID %>').text() == 'Show Size') {

                                if (p_val == 1) {
                                    $(rows).eq(idx1).after(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="6">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );
                                }
                                else {
                                    $(rows).eq(idx1).before(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="6">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );

                                    p_val = idx1 + 1;

                                }
                            }
                            else if ($("#" + '<%= lbtnsize.ClientID %>').text() == 'Hide Location' && $("#" + '<%= LinksizeA.ClientID %>').text() == 'Hide Size') {

                                if (hl_val == 1) {

                                    $(rows).eq(idx1).after(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="6">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );
                                }
                                else {

                                    $(rows).eq(idx1).before(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="6">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );

                                    hl_val = idx1 + 1;

                                }



                            }
                            else if ($("#" + '<%= LinksizeA.ClientID %>').text() == 'Hide Size' || $("#" + '<%= LinksizeA.ClientID %>').text() == 'Hide Location') {

                                if (hs_val == 1) {

                                    $(rows).eq(idx1).after(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="5">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );

                                }
                                else {

                                    $(rows).eq(idx1).before(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="5">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );

                                    hs_val = idx1 + 1;

                                }
                            }
                            else {

                                if (other_val == 1) {
                                    $(rows).eq(idx1).after(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="5">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );
                                }
                                else {

                                    $(rows).eq(idx1).before(
                                        '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                        '<td  colspan="5">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                    );

                                    other_val = idx1 + 1;

                                }
                            }
                        }
                        else {

                            if ($("#" + '<%= lbtnsize.ClientID %>').text() == 'Show Location' && $("#" + '<%= LinksizeA.ClientID %>').text() == 'Show Size') {
                                $(rows).eq(idx1).after(
                                    '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                    '<td  colspan="6">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                );
                            }
                            else if ($("#" + '<%= lbtnsize.ClientID %>').text() == 'Hide Location' && $("#" + '<%= LinksizeA.ClientID %>').text() == 'Hide Size') {

                                $(rows).eq(idx1).after(
                                    '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                    '<td  colspan="6">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                );
                            }
                            else if ($("#" + '<%= LinksizeA.ClientID %>').text() == 'Hide Size' || $("#" + '<%= LinksizeA.ClientID %>').text() == 'Hide Location') {

                                $(rows).eq(idx1).after(
                                    '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                    '<td  colspan="5">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                );
                            }
                            else {

                                $(rows).eq(idx1).after(
                                    '<tr class="group"><td colspan="6">   ' + $("input[name='groupby']:checked").attr('title') + ' : ' + ProductGroup + '</td>' +
                                    '<td  colspan="5">Total :  ' + symbol + ' ' + parseFloat(sum).toFixed(2) + '</td></tr>'
                                );
                            }
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
            if ($("#" + '<%= lbtnsize.ClientID %>').text() == 'Show Location') {
                tableTemp.column(12).visible(false);
            }
            if ($("#" + '<%= LinksizeA.ClientID %>').text() == 'Show Size') {

                tableTemp.column(11).visible(false);
            }
            //alert($("#"+ '<%= lbtnsize.ClientID %>').text());

        }
    </script>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="panel panel-yellow">
        <div class="panel-heading">Product Return Report</div>
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
                                  Duration : &nbsp;&nbsp;&nbsp;&nbsp;
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
                                     Product Group : &nbsp;
                                        <telerik:RadComboBox ID="radCategory" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>
                                                               
                                 <div class="clearfix"></div>
                                <br />
                                 <div class="col-lg-6 ">
                                      Product : &nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="radProduct" runat="server" >
                                        </telerik:RadComboBox>
                                     </div>
                                

                                 
                               <div class="col-lg-6">          
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
                </div>
                <div style="float: right; margin-right: 20px;">
                    <table>

                        <tr>
                            <td>
                                <u>
                                    <asp:LinkButton ID="lbtnsize" runat="server" OnClick="lbtnsize_Click">Show Location</asp:LinkButton>

                                </u>&nbsp <u>
                                    <asp:LinkButton ID="LinksizeA" runat="server" OnClick="LinksizeA_Click">Show Size</asp:LinkButton></u> &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td><b>Group By :   </b>&nbsp;&nbsp;&nbsp;</td>

                            <td>
                                <input type="radio" id="chkPG" name="groupby" title="Product Group" checked="checked" value="0" onclick="Grid()" />
                                Product Group </td>
                            <td>
                                <input type="radio" id="chkP" name="groupby" title="Product" value="1" onclick="Grid()" />
                                Product  </td>
                            <td>
                                <input type="radio" id="chkPsl" name="groupby" value="11" title="Size" onclick="Grid()" data-name="Size" />
                                Size </td>
                            <td>
                                <input type="radio" id="chkPsl1" name="groupby" value="12" title="Location" onclick="Grid()" />
                                Location </td>
                        </tr>
                    </table>


                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                            <asp:Repeater ID="rptProductSUmmary" runat="server">
                                <HeaderTemplate>

                                    <thead>

                                        <tr>
                                            <th>ProductGroup</th>
                                            <th>Product</th>
                                            <th>Price</th>
                                            <th>Sales Qty</th>
                                            <th>Return Qty</th>
                                            <th>TotalAmount</th>
                                            <th>Discount</th>
                                            <th>NetAmount</th>
                                            <th>Total Tax</th>
                                            <th>Volume Sold</th>
                                            <th>Qty Sold</th>
                                            <th>Size</th>
                                            <th>Location</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr>
                                        <td style="background-color: #ffffff;"><%#Eval("ProductGroup") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Pname") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("price") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Sale_qunt") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("return_qunt") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Total_amt") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Discount") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Net_amt") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Sales_tax_amount") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Volume_sold") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Qty_Sold") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Size") %></td>
                                        <td style="background-color: #ffffff;"><%#Eval("Locationname") %></td>

                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            <tfoot>

                                <tr>
                                    <th>Product Group</th>
                                    <th>Product</th>
                                    <th>Price</th>
                                    <th>Sales Qty</th>
                                    <th>Return Qty</th>
                                    <th>Total Amount</th>
                                    <th>Discount</th>
                                    <th>Net Amount</th>
                                    <th>Total Tax</th>
                                    <th>Volume Sold</th>
                                    <th>Qty Sold</th>
                                    <th>Size</th>
                                    <th>Location</th>
                                </tr>
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


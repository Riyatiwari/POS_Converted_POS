<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Discount_Summary_Report.aspx.vb" Inherits="Discount_Summary_Report" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Discount Summary 
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Discount Summary </li>
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

            $('input[type="search"]').on('keyup', function () {
                $('#<%=hdnsearchvalue.ClientID%>').val($(this).val())
            });
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12" id="divFunction" runat="server">

        <div class="panel panel-yellow">
            <div class="panel-heading">Discount Summary </div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                        <div id="divcustom" runat="server" visible="false">
                            <div class="col-lg-3" style="text-align: right;">
                                <label>From Date :</label>
                            </div>
                            <div class="col-lg-3">
                                <telerik:RadDatePicker ID="txtFrom" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>

                            </div>
                            <div class="col-lg-3" style="text-align: right;">
                                <label>To Date : </label>
                            </div>
                            <div class="col-lg-3">
                                <telerik:RadDatePicker ID="txtTo" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch">
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date" Display="None" ValidationGroup="valid" ControlToCompare="txtFrom" ControlToValidate="txtTo" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>

                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3" style="text-align: right;">
                            <label>Duration :</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList Width="70%" ID="radDuration" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radDuration_SelectedIndexChanged1">
                                <asp:ListItem Text="Today" Value="Today" />
                                <asp:ListItem Text="Yesterday" Value="Yesterday" />
                                <asp:ListItem Text="This Week" Value="This Week" />
                                <asp:ListItem Text="This Month" Value="This Month" />
                                <asp:ListItem Text="This Year" Value="This Year" />
                                <asp:ListItem Text="Last Week" Value="Last Week" />
                                <asp:ListItem Text="Last Month" Value="Last Month" />
                                <asp:ListItem Text="Last Year" Value="Last Year" />
                                <asp:ListItem Text="Custom" Value="Custom" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3" style="text-align: right;">
                            <label>By Product : </label>
                        </div>
                        <div class="col-lg-3">
                            <asp:CheckBox ID="chkProduct" runat="server" />
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <div class="row">
                        <div class="col-lg-3" style="text-align: right;">
                            <label>Product Group :</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList Width="70%" ID="radCategory" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3" style="text-align: right;">
                            <label>By Discount :</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:CheckBox ID="chkDiscount" runat="server" Checked="true" OnCheckedChanged="chkDiscount_CheckedChanged" AutoPostBack="true" />

                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />

                    <div class="row">
                        <div class="col-lg-3" style="text-align: right;">
                            <label>Product :</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList Width="70%" ID="radProduct" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div id="dv_Discount" runat="server" visible="true">
                            <div class="col-lg-3" style="text-align: right;">
                                <label>Discount name :</label>
                            </div>
                            <div class="col-lg-3">
                                <asp:DropDownList Width="70%" ID="radDiscount" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                    <br />

                    <div class="row">

                        <div class="col-lg-6"></div>
                        <div class="col-lg-3" style="text-align: right;">
                            <label>Display Report By :</label>
                        </div>
                        <div class="col-lg-3">
                            <asp:RadioButtonList ID="rbtDisplayReport" runat="server" RepeatColumns="2" Style="float: left; margin-right: 40px" OnSelectedIndexChanged="rbtDisplayReport_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Tills" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Location" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Venue" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Operater" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                    <br />
                    <div class="row">

                        <div class="col-lg-6"></div>
                        <div id="dv_Machine" runat="server" visible="true">
                            <div class="col-lg-3" style="text-align: right;">
                                <label>Till name :</label>
                            </div>
                            <div class="col-lg-3">
                                <asp:DropDownList Width="70%" ID="radMachine" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="dv_Venue" runat="server" visible="false">
                            <div class="col-lg-3" style="text-align: right;">
                                <label>Venue name : </label>
                            </div>
                            <div class="col-lg-3">
                                <asp:DropDownList Width="70%" ID="radVenue" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="dv_Location" runat="server" visible="false">
                            <div class="col-lg-3" style="text-align: right;">
                                <label>Location name :</label>
                            </div>
                            <div class="col-lg-3">
                                <asp:DropDownList Width="70%" ID="radLocation" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="dv_Operator" runat="server" visible="false">
                            <div class="col-lg-3" style="text-align: right;">
                                <label>Operator name :</label>
                            </div>
                            <div class="col-lg-3">
                                <asp:DropDownList Width="70%" ID="radOperator" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="clearfix"></div>
                    <br />

                    <div class="row">
                        <div class="col-lg-12" style="text-align: center;">
                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />

                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                <asp:Repeater ID="rptDiscSUmmary" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th>Operator name</th>
                                                <th>venue</th>
                                                <th>location</th>
                                                <th>Till</th>
                                                <th>Product</th>
                                                <th>Total Amount</th>
                                                <th>Discount</th>
                                                <th>Net Amount</th>
                                                <th>Discount Name</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <tr>
                                            <td style="background-color: #ffffff;"><%#Eval("name") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("venue") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("location") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Machine") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("pname") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("total_amt") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Discount") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Net_amt") %></td>
                                            <td style="background-color: #ffffff;"><%#Eval("Discount_name") %></td>

                                        </tr>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                            <tfoot>
                                <tr>
                                    <th>Operator name</th>
                                    <th>venue</th>
                                    <th>location</th>
                                    <th>Till</th>
                                    <th>Product</th>
                                    <th>Total Amount</th>
                                    <th>Discount</th>
                                    <th>Net Amount</th>
                                </tr>
                                </table>
                            </tfoot>

                                    </FooterTemplate>
                                </asp:Repeater>
                        </div>
                    </div>


                    <%-- <div class="col-lg-12 ">
                        <telerik:ReportViewer ID="ReportViewer1" runat="server" Style="border: 1px solid #ccc;"
                            Width="100%" Height="550px" Visible="false" />
                    </div>--%>
                </div>
            </div>
        </div>

    </div>

    <asp:HiddenField runat="server" ID="hdnsearchvalue" />
    <asp:HiddenField runat="server" ID="hdsearchvalueAfterEdit" />

</asp:Content>

<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Business_Done.aspx.vb" Inherits="Business_Done" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Business Done report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Business Done Report </li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            
            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            $('#bsummary thead tr').clone(true).appendTo('#bsummary thead');

            Gridbusiness();

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
                    'excel',
                    {
                        extend: 'pdfHtml5',
                        title: "Business Done report",
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                        }
                    }

                ],
                "bPaginate": false,
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "ordering": false,
                "columnDefs": [
                    //{
                    //    "searchable": false, "targets": 1,
                    //    "visible": false
                    //    , "targets": +groupCol,
                    //"render": function (data, type, full, meta) {
                    //    return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    //}
                    { "visible": true, "targets": +groupCol },
                    {

                        'orderable': false,
                    }

                ],

                "displayLength": 50,


            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                //if (i == 1) {
                //    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                //}
                //else {

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


            //var tablepsum = $('#Psummary').DataTable(); {
            //    tablepsum.column(14).visible(false);
            //    tablepsum.column(15).visible(false);
            //    tablepsum.column(16).visible(false);
            //    tablepsum.column(17).visible(false);
            //}

            var tablepsum = $('#Psummary').DataTable(); {
                //tablepsum.column(14).visible(false);
                //tablepsum.column(15).visible(false);
                tablepsum.column(16).visible(false);
                tablepsum.column(17).visible(false);
                // tablepsum.column(18).visible(false);
                //tablepsum.column(19).visible(false);
            }


        }

        function Gridbusiness() {

            $("#bsummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;
            

            var table1 = $('#bsummary').DataTable();
            $('#bsummary thead tr:eq(1) th').each(function (i) {
                var title = $(this).text();
                //if (i == 1) {
                //    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                //}
                //else {

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

            var table = $('#bsummary').DataTable({

                "bPaginate": false,
                dom: 'Bfrtip',
                "buttons": [
                    'excel',
                    {
                        extend: 'pdfHtml5',
                        title: "Business Done report",
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                        }
                    }
                    
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "ordering": false,
                "columnDefs": [


                    {
                        'orderable': false,
                    },
                    
                ],

                "displayLength": 50,
                "drawCallback": function (settings) {
                    
                }
            });
            var tableexp = $('#bsummary').DataTable(); {
                tableexp.column(8).visible(false);
            }

        }

    </script>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>

        <div class="panel panel-yellow">
            <div class="panel-heading">Business Done Report </div>
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
                                <div id="divcustom" runat="server" visible="false">
                                    
                                    <div class="col-lg-6 ">  
                                        &nbsp;
                                    </div>

                                    <div class="col-lg-6 ">
                                Select Date : &nbsp;
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch" >
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
                                 <div  class="col-lg-6 ">
                                         <label>Location</label>
                                        
                                        <telerik:RadComboBox ID="rdlocation" runat="server" >
                                        </telerik:RadComboBox>
                                       <%-- <asp:RequiredFieldValidator ID="rflocation" runat="server" ControlToValidate="rdlocation"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>--%>

                                    </div>
                            <div class="col-lg-6 ">
                                     Duration : &nbsp;&nbsp;
                                        <asp:DropDownList ID="radDuration" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radDuration_SelectedIndexChanged" >
                                             <Items>
                                                <asp:ListItem Text="This Week" Value="This Week" />
                                                 <asp:ListItem Text="Last Week" Value="Last Week" />
                                                  <asp:ListItem Text="2nd Last Week" Value="2nd Last Week" />
                                                 <asp:ListItem Text="3rd Last Week" Value="3rd Last Week" />
                                                 <asp:ListItem Text="4th Last Week" Value="4th Last Week" />
                                                 <asp:ListItem Text="Custom" Value="Custom" />
                                                 </Items>
                                        </asp:DropDownList>
                                     </div>
                                <%-- <div class="col-lg-6 ">                                     
                                  Week Start Date : &nbsp;
                                <telerik:RadDatePicker ID="txtdate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000"  >
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday" >
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                      <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date"  Display="None"  ValidationGroup="valid" ControlToCompare="txtForDate" ControlToValidate="txtdate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                     --%>
                    </div>

                    <div class="clearfix"></div>

                    <div class="clearfix"></div>
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-outline btn-primary" ToolTip="View Report">View</asp:LinkButton>

                    <br />
                    <br />
                    <br />
                    <br />
                    <%--<div style="text-align:right;padding-right:25px;">
                                        <asp:ImageButton ID="btnexp" OnClick="btnExcl_ServerClick" runat="server" ImageUrl="~/images/excel.png" Height="30px"  />  
                                </div>--%>
                    <div class="col-lg-12 ">


                        <div class="card-body">
                            <div class="table-responsive">
                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rptcash" runat="server" OnItemDataBound="rptcash_ItemDataBound">
                                        <HeaderTemplate>

                                            <thead>
                                                <tr>
                                                    <asp:Repeater ID="Header1" runat="server">
                                                        <ItemTemplate>
                                                            <th class="header"><%# Container.DataItem %>
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
                                <tr>
                                </tr>
                                </table>
                            </tfoot>

                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>

                        <div class="card-body" style="margin-top: 56px;">
                            <div class="table-responsive">
                                <table class="table table-bordered" id="bsummary" width="100%" cellspacing="0">
                                    <asp:Repeater ID="rptbusiness" runat="server" OnItemDataBound="rptbusiness_ItemDataBound">
                                        <HeaderTemplate>

                                            <thead>
                                                <tr>
                                                    <asp:Repeater ID="Header1" runat="server">
                                                        <ItemTemplate>
                                                            <th class="header"><%# Container.DataItem %>
                                                            </th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <th class="header">Total</th>
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
                                                <asp:Label ID="lblNameHead" runat="server" Text='<%# Eval("name")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblMon_Amount" runat="server" Text='<%# Eval("Monday")%>' Visible="false" />
                                                <asp:Label ID="lblTue_Amount" runat="server" Text='<%# Eval("Tuesday")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblWed_Amount" runat="server" Text='<%# Eval("Wednesday")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblThu_Amount" runat="server" Text='<%# Eval("Thursday")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblFri_Amount" runat="server" Text='<%# Eval("Friday")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblSat_Amount" runat="server" Text='<%# Eval("Saturday")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblSun_Amount" runat="server" Text='<%# Eval("Sunday")%>' Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hf_Currency" runat="server" Value='<%# Eval("Currency")%>' />
                                                <td class="body">
                                                    <asp:Label ID="lblAmount" runat="server" /></td>
                                            </tr>

                                        </ItemTemplate>


                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>
                                    <itemtemplate>
                                         
                                         <th class="header" >Total   </th>                                      
                                         <td class="body" ><asp:Label ID="lblMon_TotalAmount" runat="server" />   </td>                                      
                                         <td class="body" ><asp:Label ID="lblTue_TotalAmount" runat="server" />  </td>
                                         <td class="body" ><asp:Label ID="lblWed_TotalAmount" runat="server" />  </td>
                                         <td class="body" ><asp:Label ID="lblThu_TotalAmount" runat="server" />  </td>
                                         <td class="body" ><asp:Label ID="lblFri_TotalAmount" runat="server" />  </td>
                                         <td class="body" ><asp:Label ID="lblSat_TotalAmount" runat="server" />  </td>
                                         <td class="body" ><asp:Label ID="lblSun_TotalAmount" runat="server" />  </td>
                                         <td  class="body" > </td>
                                         <td class="body" ><asp:Label ID="lblTotalAmount" runat="server" />      </td>
                                        </itemtemplate>
                                </tr>
                            </tfoot>
                                        </FooterTemplate>
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

    <%--</telerik:RadAjaxPanel>--%>


    <%--   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>


    <style>
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            .mGrd t

        {
            g: 2px solid 1px # 1c1c1;
            right;
            . Grid padding 4px 2p;
            color: fff;
            background: 42424 ur(grd_hea p;
            : 52;
            0.9em;
        }

        .mGrid alt

        #fcfcf url g
        -x top;
        }

        .mGrid pgr

        242 ur(grdpgr.p g i
        }


        .pgr td
        -width: 0;

        x #666
        weight: bold
        o

        l n
        }

        Grid .pgr a {
            : tet-deoration ver

        {
            text-decoration: none;
        }
    </style>
</asp:Content>

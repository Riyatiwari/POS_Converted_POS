<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Table_Transaction.aspx.vb" Inherits="Table_Transaction" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Table Transaction Report
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">

    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Table Transaction Report</li>
        </ol>
    </div>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

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

            var getTxtValue = ''
            getTxtValue = $('#<%=txt_check.ClientID%>').val()
           <%--  var getRefValue = ''
            getRefValue = $('#<%=hdfRefId.ClientID%>').val()--%>

            var chkTable = $("#<%= chkOpenTables.ClientID %>").is(":checked");
            var t_name = $("#<%= chkOpenTables.ClientID %>").is(":checked");

            $("#Psummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table2 = $('#Psummary').DataTable({

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

                if (title == 'Table Name' && getTxtValue != '') {
                    $(this).html('<input type="text" value="' + getTxtValue + '" style="width:98%" placeholder="Search ' + title + '" />');

                }
                //else if (title == 'Reference Id' && getRefValue != '') {
                //    $(this).html('<input type="text" value="' + getRefValue + '" style="width:98%" placeholder="Search ' + title + '" />');

                //}
                else {
                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }
                 <%-- alert(document.getElementById('<%= txt_check.ClientID %>').value);--%>
                $('input', this).on('keyup change', function () {
                    <%--alert("*"+document.getElementById('<%= txt_check.ClientID %>').value);--%>
                    if (table1.column(i).search() !== this.value) {
                        table1.column($(this).parent().index() + ':visible')
                            .search(this.value)
                            .draw();
                        
                        if (title == 'Table Name') {
                          
                            document.getElementById('<%= txt_check.ClientID %>').value = this.value;
                        }
                       <%-- else if (title == 'Reference Id') {
                             alert(this.value);
                             alert(title);
                            document.getElementById('<%= hdfRefId.ClientID %>').value = this.value;
                            getRefValue = $('#<%=hdfRefId.ClientID%>').val();
                        }--%>
                    }
                    else {
                        //table1.column($(this).parent().index() + ':visible')
                        //    .search('')
                        //    .draw();

                    }

                });

                if (getTxtValue != '') {

                    table1.column($(this).parent().index() + ':visible')
                        .search(getTxtValue)
                        .draw();

                }


                //if (getRefValue != '' && title == 'Reference Id') {
                //    alert(getRefValue);
                //    table1.column($(this).parent().index() + ':visible')
                //        .search(getRefValue)
                //        .draw(); 

                //}
            });

            var tableTemp = $('#Psummary').DataTable();

            if (chkTable == true) {
                tableTemp.column(8).visible(true);
            }
            else {
                tableTemp.column(8).visible(false);
            }
         
        }
    </script>


    <script lang="javascript" type="text/javascript">

        function openNewWin(url) {
            var x = window.open(url, 'Table Transaction Report Detail', 'width=600,height=600,toolbar=1');
            x.focus();
        }
    </script>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:HiddenField runat="server" ID="txt_check" />
        <asp:HiddenField runat="server" ID="hdfRefId" />
    </div>
    <div class="panel panel-yellow">
        <div class="panel-heading">Table Transaction Report</div>
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
                                <telerik:RadDatePicker ID="txtFrom" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch" >
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
                                <telerik:RadDatePicker ID="txtTo" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" skin="MetroTouch"  >
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
                                <div class="col-lg-6 ">
                                       <asp:CheckBox ID="chkOpenTables" runat="server" /> &nbsp; Open Tables Only
                                     </div>
                                      <div class="clearfix"></div>
                                <br />
                               <div class="col-lg-6 ">
                                       Payment Type : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <telerik:RadComboBox ID="ddl_PayType" runat="server" >
                                            <Items>
                                                <telerik:RadComboBoxItem Value="-1" Text="SELECT" />
                                                <telerik:RadComboBoxItem Value="0" Text="Cash" />
                                                 <telerik:RadComboBoxItem Value="1" Text="Card" />
                                                 <telerik:RadComboBoxItem Value="20" Text="Integrated Card" />
                                                 <telerik:RadComboBoxItem Value="8" Text="Credit Account" />
                                                 <telerik:RadComboBoxItem Value="2" Text="Room Payment" />
                                                 <telerik:RadComboBoxItem Value="3" Text="Help Out" />
                                                 <telerik:RadComboBoxItem Value="4" Text="Gift Cards" />
                                                 <telerik:RadComboBoxItem Value="13" Text="Voucher" />
                                                 <telerik:RadComboBoxItem Value="5" Text="Deposits" />
                                                <telerik:RadComboBoxItem Value="6" Text="Elina" />
                                                <telerik:RadComboBoxItem Value="7" Text="Add Pay" />
                                             <%--    <telerik:RadComboBoxItem Value="10" Text="Card Online" />--%>
                                            </Items>
                                        </telerik:RadComboBox>
                                      
                                     </div>  

                               <div class="clearfix"></div>
                                <br />
                              <div class="col-lg-12 " >
                               </div>
                              </center>
                </div>
                <div style="text-align: center;">

                    <div class="clearfix"></div>
                    <br />

                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>

                    <br />
                    <br />
                </div>

                <div>
                    <div class="row" id="divPGroup" runat="server">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">

                                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">

                                    <asp:Repeater ID="rdCategory" runat="server" OnItemCommand="rdCategory_ItemCommand">
                                        <HeaderTemplate>
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Table Name</th>
                                                    <th>Created Date</th>
                                                    <th>Total Amount</th>
                                                    <th>Payment Amount</th>
                                                    <th>Location</th>
                                                    <th>Reference Id</th>
                                                    <th>Table Rename</th>
                                                    <th>Action</th>
                                                    <th>Mail</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnExpand" runat="server" Text="+" Style="font-weight: bold;" />
                                                    
                                                    <asp:HiddenField ID="hfNested_Ref_id" runat="server" Value='<%# Eval("Ref_id") %>' />
                                                </td>
                                                <td style="background-color: #ffffff;"><%#Eval("table_name") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("created_date") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("total_amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Payment_Amount") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("name") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("Ref_id") %></td>
                                                <td style="background-color: #ffffff;"><%#Eval("tablesname") %></td>
                                                <td style="background-color: #ffffff; text-align: center;">
                                                    <asp:LinkButton ID="IbClose" runat="server" CausesValidation="False" ToolTip="Close Table"
                                                        CommandArgument='<%#Eval("table_uuid")%>' CommandName="close" OnClientClick="return confirm('Are you sure you want to close this table ?');">
                                                       <i class="fa fa-door-closed fa-lg"></i></asp:LinkButton>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:LinkButton ID="btn_mail" runat="server" CausesValidation="False" ToolTip="Send Mail"
                                                        CommandArgument='<%#Eval("table_uuid")%>' CommandName="Mail">
                                                       <i class="fa fa-send fa-lg"></i></asp:LinkButton>
                                                </td>
                                                <asp:HiddenField ID="hdLocation" runat="server" Value='<%#Eval("location_id")%>' />
                                                <asp:HiddenField ID="hdDatetime" runat="server" Value='<%#Eval("created_date")%>' />
                                                <asp:HiddenField ID="hdtable_uuid" runat="server" Value='<%#Eval("table_uuid")%>' />
                                            </tr>

                                            <asp:Panel ID="pnlOrders" runat="server" Visible="false">
                                                <tr>

                                                    <td></td>
                                                    <td colspan="9">
                                                        <div style="display: none"><%#Eval("table_name") %></div>

                                                        <asp:Repeater ID="rptOrders" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered" id="inner" cellspacing="0" rules="all" border="1"
                                                                    style="margin-bottom: 0px;">
                                                                    <tr>
                                                                        <th>Created Date</th>
                                                                        <th>Total Amount</th>
                                                                        <th>Payment Amount</th>
                                                                        <th>Operator</th>
                                                                        <th>Till</th>
                                                                        <th>Transaction ID</th>
                                                                        <th>Details</th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="background-color: #ffffff;"><%#Eval("created_date") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("Total_Amount") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("payment_amount") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("Operator") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("Till") %></td>
                                                                    <td style="background-color: #ffffff;"><%#Eval("tran_uuid") %></td>
                                                                    <td style="background-color: #ffffff;">
                                                                        <asp:LinkButton ID="lnkview" CommandArgument='<%#Eval("sales_id").ToString() + "#" + Eval("tran_uuid").ToString()  %>' runat="server" OnClick="lnkview_Click">
                                                                        <i class="fa fa-search" style="cursor: pointer" aria-hidden="true"></i>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                </tr>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>


                                                    <td style="display: none">
                                                        <div style="display: none"><%#Eval("created_date") %></div>
                                                    </td>
                                                    <td style="display: none">
                                                        <div style="display: none"><%#Eval("total_amount") %></div>
                                                    </td>
                                                    <td style="display: none">
                                                        <div style="display: none"><%#Eval("Payment_Amount") %></div>
                                                    </td>
                                                    <td style="display: none">
                                                        <div style="display: none"><%#Eval("name") %></div>
                                                    </td>
                                                    <td style="display: none">
                                                        <div style="display: none"><%#Eval("Ref_id") %></div>
                                                    </td>
                                                    <td style="display: none">
                                                        <div style="display: none"><%#Eval("tablesname") %></div>
                                                    </td>
                                                    <td style="display: none"></td>
                                                    <td style="display: none"></td>
                                                </tr>
                                            </asp:Panel>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                            <tfoot>
                                <tr>

                                    <th></th>
                                    <th>Table Name</th>
                                    <th>Created Date</th>
                                    <th>Total Amount</th>
                                    <th>Payment Amount</th>
                                    <th>Location</th>
                                    <th>Reference Id</th>
                                    <th>Table Rename</th>
                                    <th>Action</th>
                                    <th>Mail</th>
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


    <telerik:RadWindow runat="server" ID="rwEntryDetails" Modal="true" Width="400px"
        Height="250px" KeepInScreenBounds="True" Skin="Bootstrap" VisibleStatusbar="False"
        ReloadOnShow="true" Behaviors="Close" Title="" EnableEmbeddedSkins="false" Style="overflow: hidden !important;">
        <ContentTemplate>
            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                <div class="panel panel-yellow">
                    <div class="panel-heading">Email Detail</div>
                    <div class="panel-body pan">
                        <div class="form-body pal">

                            <div id="dv_prodctlist" runat="server">

                                <div class="row">
                                    <div class="col-lg-12 ">
                                        <label>Email-ID : </label>
                                        <asp:TextBox Style="width: 100%" ID="txt_Email" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="padding-top: 15px; text-align: center;">
                                    <asp:Button ID="btn_send" class="btn btn-primary" runat="server" Text="Send" OnClick="btn_send_Click" />
                                    <asp:Button ID="btn_cancel" class="btn btn-primary" runat="server" Text="Cancel" OnClick="btn_cancel_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
        </ContentTemplate>
    </telerik:RadWindow>


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


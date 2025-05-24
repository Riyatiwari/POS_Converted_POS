<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="demo_table.aspx.vb" Inherits="demo_table" %>

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

    <style type="text/css">
        .hiddencol {
            display: none;
        }

        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%
        }

        .Grid th {
            background-color: #4374A6;
            color: White;
            font-size: 10pt;
            line-height: 200%
        }

        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%
        }

        .ChildGrid th {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height: 200%
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");

        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
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


                            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                DataKeyNames="ref_id" OnRowDataBound="OnRowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img alt="" style="cursor: pointer" src="images/plus.png" />
                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid" OnRowDataBound="OnRowDataBound_Child">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Product" HeaderText="Product" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="Size_Name" HeaderText="Size Name" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="sales_discount" HeaderText="Sales Discount" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="sales_total_amount" HeaderText="Sales Total Amount" />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="quntity" HeaderText="Qty." />
                                                        <asp:BoundField ItemStyle-Width="150px" DataField="date" HeaderText="Date" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="ref_id" HeaderText="Ref. Id" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="For_date" HeaderText="For Date" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="Table_name" HeaderText="Table Name" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="SalesAmount" HeaderText="Sales Amount" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="payment_amount" HeaderText="Payment Amount" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="Miscellaneous" HeaderText="Miscellaneous" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="created_date" HeaderText="Created Date" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="location_id" HeaderText="Location Id" ItemStyle-CssClass="hiddencol"/>
                                </Columns>
                            </asp:GridView>

                        </table>
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
            background-color:  !important;
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



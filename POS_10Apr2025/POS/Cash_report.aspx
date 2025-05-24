<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cash_report.aspx.vb" Inherits="Cash_report" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms"  TagPrefix="telerik" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  Cash Declaration Report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
   
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Cash Declaration report</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

            <div class="panel panel-yellow">
                <div class="panel-heading">Cash Declaration Report</div>
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
                                      
                                     </div>
                                   </div>
                                  <div class="clearfix"></div>
                                <br /> 

                                              <div class="col-lg-6 ">                                     
                                               
                                     Duration : &nbsp;&nbsp;
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

                                 <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date is greater than to date"  Display="None"  ValidationGroup="valid" ControlToCompare="txtFrom" ControlToValidate="txtTo" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                <div class="clearfix"></div>
                                <br />
                                 <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                              <br /><br />
                                <div class="col-lg-12 " >
                                 <telerik:ReportViewer ID="ReportViewer1" runat="server" Style="border: 1px solid #ccc;"
                                                Width="80%" Height="550px" Visible="false"  />
                                    </div>
                                
                              </center>
                        </div>
                    </div>
                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
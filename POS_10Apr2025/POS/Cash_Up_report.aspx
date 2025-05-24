<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Cash_Up_report.aspx.vb" Inherits="Cash_Up_report" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Cash Summary 
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Cash Summary  </li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

            <div class="panel panel-yellow">
                <div class="panel-heading">Cash Summary  </div>
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
                                <div id="divfrom" runat="server" visible="false">
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
                                  
                               </div>  
                                 <div class="col-lg-6 ">                                     
                                  Week Ending : &nbsp;
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
                                   
                             <%--   <br />--%>
                              <%--   <div class="col-lg-6 ">
                                     Venue : &nbsp; &nbsp; &nbsp; &nbsp;
                                        <telerik:RadComboBox ID="radVenue" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>                                      
                                     </div>                               
                                 <div class="col-lg-6 ">
                                      Location : &nbsp;
                                        <telerik:RadComboBox ID="radLocation" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                     </div>
                                
                                  <div class="clearfix"></div>
                                <br />
                          
                                  <div class="col-lg-6 ">
                                     Machine : &nbsp;
                                        <telerik:RadComboBox ID="radMachine" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>   
                                 <div class="col-lg-6 ">
                                     Device : &nbsp; &nbsp; &nbsp;
                                        <telerik:RadComboBox ID="radDevice" runat="server" AutoPostBack="true" >
                                        </telerik:RadComboBox>
                                      
                                     </div>   --%>
                                
                                <div class="clearfix"></div>
                                 <br />  
                              <%--  <br />--%>
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                                     
                              <br /><br />
                              <br /><br />
                              <div class="col-lg-12 " >
                                 <telerik:ReportViewer ID="ReportViewer1" runat="server" Style="border: 1px solid #ccc;"
                                                Width="100%" Height="550px" Visible="false" />
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

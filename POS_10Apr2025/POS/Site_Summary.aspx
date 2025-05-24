<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Site_Summary.aspx.vb" Inherits="Site_Summary" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Bank Summary report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Bank Summary Report </li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <%--    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" PostBackControls="btnexp">--%>

        <div class="panel panel-yellow">
            <div class="panel-heading">Bank Summary Report </div>
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
                                <telerik:RadDatePicker ID="txtForDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000"  >
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
                                <telerik:RadDatePicker ID="txtToDate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000"  >
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
                                
                                <div class="clearfix"></div>
                                 <br />  
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-outline btn-primary" ToolTip="View Report" ValidationGroup="valid">View</asp:LinkButton>
                                     
                              <br /><br />
                              <br /><br />
                                <div style="text-align:right;padding-right:25px;">
                                        <asp:ImageButton ID="btnexp" OnClick="btnExcl_ServerClick" runat="server" ImageUrl="~/images/excel.png" Height="30px"  />  
                                </div>
                              <div class="col-lg-12 " >

                           
                               

                                  <asp:GridView ID="gv_CashReport" runat="server"  AutoGenerateColumns="False" Width="100%" ShowFooter="true"   AllowPaging="false"  CssClass="mGrid"
    PagerStyle-CssClass="pgr"
    AlternatingRowStyle-CssClass="alt">
                                   <Columns>    
                                         <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            
                                           <%# Eval("Location")%>
                                        </ItemTemplate>
                                             
                                        </asp:TemplateField>
                                  
                                       <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            
                                           <%# Eval("for_date", "{0:dd/MM/yyyy}")%>
                                        </ItemTemplate>
                                             <FooterTemplate>
                                        Total :
                                        </FooterTemplate> 
                                        </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                        <asp:Label ID="lblMon_Amount" runat="server" Text='<%# Eval("Amount")%>' />
                                            <asp:HiddenField ID="hf_Currency" runat="server" Value='<%# Eval("Currency")%>'/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lblMon_TotalAmount" runat="server" />
                                        </FooterTemplate> </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Description">
                                       <ItemTemplate>
                                       <asp:Label ID="lbldesc" runat="server" Text='<%# Eval("Description") %>' />
                                       </ItemTemplate> 
                                       </asp:TemplateField>

                                        

                                  
                                    </Columns>
                                     
                                  </asp:GridView>
                                    </div>
                              </center>
                    </div>
                </div>
            </div>

        </div>

        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">

    </telerik:RadAjaxLoadingPanel>--%>


    <style>
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .mGrid th {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .mGrid .alt {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }

            .mGrid .pgr {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>

</asp:Content>
<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Expense_Summary.aspx.vb" Inherits="Expense_Summary" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Expense Summary report
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Expense Summary Report </li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>

        <div class="panel panel-yellow">
            <div class="panel-heading">Expense Summary Report </div>
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
                                 <div  class="col-lg-6 ">
                                         <label>Location <span class="text-danger">*</span></label>
                                        
                                        <telerik:RadComboBox ID="rdlocation" runat="server" >
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rflocation" runat="server" ControlToValidate="rdlocation"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>

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
                              
                                  <asp:GridView ID="gv_CashReport" runat="server" Border="1" BorderColor="Black" AutoGenerateColumns="False" Width="100%" ShowFooter="true" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="False"  CssClass="mGrid"
    PagerStyle-CssClass="pgr"
    AlternatingRowStyle-CssClass="alt">
                                   <Columns>    
                                  
                                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left"    >
                                         <ItemTemplate>
                            <div style="width: 100%; text-align: left;">
                                <asp:Label ID="lblNameHead" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label Text="Profit" Style="text-align: center;" runat="server"></asp:Label>
                        </FooterTemplate>                            

                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Monday">
                                        <ItemTemplate>
                                             <asp:HiddenField ID="hf_Currency" runat="server" Value='<%# Eval("Currency")%>'/>
                                        <asp:Label ID="lblMon_Amount" runat="server" Text='<%# Eval("Monday")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lblMon_TotalAmount" runat="server" />
                                        </FooterTemplate> </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Tuesday">
                                       <ItemTemplate>
                                       <asp:Label ID="lblTue_Amount" runat="server" Text='<%# Eval("Tuesday") %>' />
                                       </ItemTemplate> <FooterTemplate>
                                       <asp:Label ID="lblTue_TotalAmount" runat="server" />
                                       </FooterTemplate>
                                       </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Wednesday">
                                       <ItemTemplate>
                                       <asp:Label ID="lblWed_Amount" runat="server" Text='<%# Eval("Wednesday") %>' />
                                       </ItemTemplate> <FooterTemplate>
                                       <asp:Label ID="lblWed_TotalAmount" runat="server" />
                                       </FooterTemplate>
                                       </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Thursday">
                                       <ItemTemplate>
                                       <asp:Label ID="lblThu_Amount" runat="server" Text='<%# Eval("Thursday") %>' />
                                       </ItemTemplate> <FooterTemplate>
                                       <asp:Label ID="lblThu_TotalAmount" runat="server" />
                                       </FooterTemplate>
                                       </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Friday">
                                       <ItemTemplate>
                                       <asp:Label ID="lblFri_Amount" runat="server" Text='<%# Eval("Friday") %>' />
                                       </ItemTemplate> <FooterTemplate>
                                       <asp:Label ID="lblFri_TotalAmount" runat="server" />
                                       </FooterTemplate>
                                       </asp:TemplateField> 
                                       
                                       <asp:TemplateField HeaderText="Saturday">
                                       <ItemTemplate>
                                       <asp:Label ID="lblSat_Amount" runat="server" Text='<%# Eval("Saturday") %>' />
                                       </ItemTemplate> <FooterTemplate>
                                       <asp:Label ID="lblSat_TotalAmount" runat="server" />
                                       </FooterTemplate>
                                       </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Sunday">
                                       <ItemTemplate>
                                       <asp:Label ID="lblSun_Amount" runat="server" Text='<%# Eval("Sunday") %>' />
                                       </ItemTemplate> <FooterTemplate>
                                       <asp:Label ID="lblSun_TotalAmount" runat="server" />
                                       </FooterTemplate>
                                       </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total">
                                       <ItemTemplate>
                                       <asp:Label ID="lblAmount" runat="server"  />
                                       </ItemTemplate> <FooterTemplate>
                                       <asp:Label ID="lblTotalAmount" runat="server" />
                                       </FooterTemplate>
                                       </asp:TemplateField>

                                  
                                    </Columns>
                                     
                                  </asp:GridView>
                                    </div>
                              </center>
                    </div>
                </div>
            </div>

        </div>

        <%--</telerik:RadAjaxPanel>--%>
    </div>

    <%--   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
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
                text-align:right;
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


<%@ Page Title="Booking Details" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="BookingDetails.aspx.vb" Inherits="BookingEasy_BookingDetails" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Restore this Booking?");
        }
    </script>
    <script type="text/javascript">
        function OnClientClicked(sender, args) {
            var window = $find('<%=RadWindow_WalkinBooking.ClientID %>');
            window.close();
        }
    </script>
    <%--
      <script language="javascript" type="text/javascript">

          function PrintPage() {

              var printContent = document.getElementById('<%= gvTableBooking.ClientID %>');

              var printWindow = window.open("All Records", "Print Panel", 'left=50000,top=50000,width=0,height=0');

              printWindow.document.write(printContent.innerHTML);

              printWindow.document.close();

              printWindow.focus();

              printWindow.print();

          }

    </script>--%>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /** Multiple rows and columns */
        .multipleRowsColumns .rcbItem, .multipleRowsColumns .rcbHovered
        {
            float: left;
            margin: 0 1px;
            min-height: 13px;
            overflow: hidden;
            padding: 2px 19px 2px 6px;
            width: 190px;
        }
        .rcbScroll
        {
            height: 250PX !important;
        }
        .rcbWidth
        {
            height: 250PX !important;
        }
    </style>
    <style type="text/css">
        html .RadScheduler .red
        {
            background: #FF0000;
        }
        
        html .RadScheduler .pink
        {
            background: #FFC0CB;
        }
        
        html .RadScheduler .beige
        {
            background: #F5F5DC;
        }
        
        html .RadScheduler .white
        {
            background: #FFFFFF;
        }
        
        html .RadScheduler .green
        {
            background: #008000;
        }
        /*.rsContent .rsContentTable .rsRow .rsSatCol,  
         .rsContent .rsContentTable .rsRow .rsSunCol   
        {  
          background:#FFFFFF;  
           
        } */
        
        .TabHeader
        {
            background-color: #70767A;
            /*background-color: #767676;*/
            color: #333333;
            min-height: 35px;
            min-width: 60px;
            text-align: center;
            padding: 8px 5px 5px 5px;
            min-width: 100px;
        }
        .multiPage {
            display: inline-block;
            *display: inline;
            zoom: 1;
            border-color: #BDC1C5;
        }
        .canvasjs-chart-credit
        {
            visibility:hidden !Important;
        }
        
         input[type=radio]{
        margin:5px;  
    </style>
    <div class="page-title">
        <div>
            <h1>
                <i class="fa fa-building-o"></i>Booking List
            </h1>
        </div>
    </div>
    <div id="divContent" runat="server">
        <div class="row">
            <div class="col-md-12" id="divTableBooking" runat="server">
                <div class="box box-black">
                    <div class="box-title">
                        <h3>
                            <i class="fa fa-table"></i>Walk in</h3>
                        <div class="box-tool">
                            <a href="#" data-action="collapse"><i class="fa fa-chevron-up"></i></a>
                        </div>
                    </div>
                    <div class="box-content" style="height: auto;">
                        <div class="form-horizontal">
                            <div class="row">
                                <div class="col-md-12" id="div3" runat="server">
                                    <div id="divMessageBox" runat="server" visible="false">
                                        <asp:Label ID="lbExistingCust" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="row" style="padding-left: 41px;">
                                    <label>
                                        <b>Location:</b>&nbsp;</label>
                                    <telerik:RadDropDownList ID="ddlVenue" Skin="MetroTouch" runat="server" Width="200px"
                                        DropDownHeight="200px" AutoPostBack="true">
                                    </telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ControlToValidate="ddlVenue"
                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTablewalkin">
                                    </asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <label>
                                        <b>Schedule:</b>&nbsp;</label>
                                    <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="200px"
                                        DropDownHeight="200px" AutoPostBack="true">
                                    </telerik:RadDropDownList>
                                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTablewalkin">
                                    </asp:RequiredFieldValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <label>
                                        <b>No Of People:</b>&nbsp;</label>
                                    <%--<telerik:RadDropDownList ID="ddlNoOfCovers" Skin="MetroTouch" runat="server" Width="200px"
            DropDownHeight="200px" AutoPostBack="True">
            <Items>
                <telerik:DropDownListItem Value="1" Text="1" Selected="True"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="2" Text="2"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="3" Text="3"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="4" Text="4"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="5" Text="5"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="6" Text="6"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="7" Text="7"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="8" Text="8"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="9" Text="9"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="10" Text="10"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="11" Text="11"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="12" Text="12"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="13" Text="13"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="14" Text="14"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="15" Text="15"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="16" Text="16"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="17" Text="17"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="18" Text="18"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="19" Text="19"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="20" Text="20"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="-1" Text="Other"></telerik:DropDownListItem>
            </Items>
        </telerik:RadDropDownList>--%>
                                    <telerik:RadNumericTextBox ID="txtNoOfCovers" runat="server" Type="Number" Skin="Office2010Blue"
                                        NumberFormat-DecimalDigits="0" Width="200px" Height="32px" DataType="Integer"
                                        CssClass="form-control">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rfvNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="SearchTablewalkin"
                                        CssClass="rfv" Enabled="True">
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="regNoOfCovers" runat="server" ControlToValidate="txtNoOfCovers"
                                        Type="Integer" MinimumValue="1" MaximumValue="9999" ForeColor="Red" ErrorMessage="*"
                                        Display="Dynamic" ValidationGroup="SearchTablewalkin" CssClass="rfv" Enabled="True"></asp:RangeValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSearch" runat="server" Text="Walk in Booking" CssClass="btn btn-primary"
                                        ValidationGroup="SearchTablewalkin" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4" id="div1" runat="server">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-black">
                    <div class="box-title">
                        <h3>
                            <i class="fa fa-bars"></i>Booking Details
                        </h3>
                    </div>
                    <div class="box-content">
                        <div class="form-horizontal">
                            <div class="row" id="divRestaurentCal" runat="server">
                                <div class="col-md-12">
                                </div>
                                <div class="col-md-12">
                                    <div class="box">
                                        <div class="box-content">
                                            <div>
                                                <h5 style="font-weight: bold; text-align: center;">
                                                    <asp:Label ID="Label4" runat="server" Text=" "></asp:Label></h5>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<b>Date : </b>&nbsp;
                                                <telerik:RadDatePicker ID="rdpDate" runat="server" EnableTyping="true" Width="200px"
                                                    AutoPostBack="true" Skin="MetroTouch">
                                                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                                    </DateInput>
                                                </telerik:RadDatePicker>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<b>Location : </b>&nbsp;
                                                <telerik:RadDropDownList ID="drpTableStore" Skin="MetroTouch" runat="server" Width="250px"
                                                    DropDownHeight="200px" AutoPostBack="true">
                                                </telerik:RadDropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<b>Schedule : </b>&nbsp;
                                                <telerik:RadDropDownList ID="rddlType" Skin="MetroTouch" runat="server" Width="200px"
                                                    DropDownHeight="200px" AutoPostBack="true">
                                                </telerik:RadDropDownList>
                                            </div>
                                            <br />
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                                            </telerik:RadAjaxLoadingPanel>
                                            <telerik:RadWindow runat="server" ID="RadWindow_WalkinBooking" Modal="true" Width="950px"
                                                Height="600px" KeepInScreenBounds="True" Skin="Metro" VisibleStatusbar="False"
                                                EnableViewState="true" ReloadOnShow="true" Behaviors="Close" VisibleOnPageLoad="false"
                                                DestroyOnClose="true" Title="Walk in Booking">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <div class="box" id="divInfo" runat="server">
                                                                <style type="text/css" media="screen">
                                                                    .container
                                                                    {
                                                                        width: 90%;
                                                                        padding-left: 20px;
                                                                    }
                                                                    .left20
                                                                    {
                                                                        width: 20%;
                                                                        float: left;
                                                                    }
                                                                    .left40
                                                                    {
                                                                        width: 40%;
                                                                        float: left;
                                                                    }
                                                                    .clear
                                                                    {
                                                                        clear: both;
                                                                    }
                                                                </style>
                                                                <div class="box-title" style="margin-top: 6px;">
                                                                    <h3>
                                                                        <i class="fa fa-table"></i>Booking Details</h3>
                                                                </div>
                                                                <br />
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5" style="padding-bottom: 10px;">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Location :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblLocationW" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Schedule :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblScheduleW" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5" style="padding-bottom: 10px;">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Booking Date :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblBookingdateW" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Booking Time :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblBookingtimeW" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>No Of People:</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblTableW" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                            </div>
                                                            <div class="box" id="divInfo1" runat="server">
                                                                <div class="box-title">
                                                                    <h3>
                                                                        <i class="fa fa-search"></i>Available Table</h3>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-12" id="div0" runat="server">
                                                                        <div id="divMessageBox1" runat="server" visible="false">
                                                                            <asp:Label ID="lbExistingCust1" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Tables :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <telerik:RadComboBox ID="ddlTableSet" Skin="MetroTouch" runat="server" Width="200px"
                                                                                AutoPostBack="True" DropDownCssClass="multipleRowsColumns" AllowCustomText="true"
                                                                                EnableScreenBoundaryDetection="false" EnableCheckAllItemsCheckBox="True" DropDownWidth="480px"
                                                                                CheckBoxes="True" Filter="Contains" DropDownHeight="200px" ValidationGroup="popallottbl">
                                                                            </telerik:RadComboBox>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="popallottbl"
                                                                                ControlToValidate="ddlTableSet" ForeColor="Red" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            <asp:Label ID="Label2" runat="server" Font-Size="X-Small" Text="*Max = Max Covers"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <div>
                                                                <div class="col-md-1">
                                                                </div>
                                                                <div class="col-md-10" style="text-align: center;">
                                                                    <asp:Button ID="btn_Waitlist" runat="server" Text="Create Waitlist" CssClass="btn btn-primary"
                                                                        Width="110px" ValidationGroup="" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="Button1" runat="server" Text="Seat" CssClass="btn btn-primary" Width="60px"
                                                                        ValidationGroup="popallottbl" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn" Width="60px"
                                                                        OnClientClick="OnClientClicked" />
                                                                </div>
                                                                <div class="col-md-1">
                                                                </div>
                                                            </div>
                                                            <div class="clearfix">
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Button1" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1">
                                                        <ProgressTemplate>
                                                            <div id="custom_progress">
                                                                <div id="overlay_load">
                                                                </div>
                                                                <div id="loading">
                                                                    <img src="Images/ajax-loader.gif" alt="" />
                                                                    <br />
                                                                    Please Wait...
                                                                </div>
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </ContentTemplate>
                                            </telerik:RadWindow>
                                            <br />
                                            <telerik:RadTabStrip runat="server" ID="RadTabStrip2" Orientation="HorizontalTop"
                                                AutoPostBack="true" Width="100%" SelectedIndex="0" MultiPageID="RadMultiPage1"
                                                Skin="Silk" BackColor="#B6D1F2">
                                                <Tabs>
                                                    <telerik:RadTab Text="Booking List">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab Text="Table Overview" runat="server">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab Text="Table View">
                                                    </telerik:RadTab>
                                                    <telerik:RadTab Text="Waiting List">
                                                    </telerik:RadTab>
                                                </Tabs>
                                            </telerik:RadTabStrip>
                                            <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage"
                                                Width="100%">
                                                <telerik:RadPageView runat="server" ID="RadPageView1" BorderColor="#BCC0C4" BorderWidth="1">
                                                    <div class="box-content">
                                                        <div style="margin-bottom: 10px;">
                                                            <div style="text-align: left;">
                                                                <asp:Label ID="lblListVenueName" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblListDate" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div style="text-align: right; float: left;">
                                                            <asp:RadioButtonList ID="rdoBooking" runat="server" RepeatDirection="Horizontal"
                                                                CellSpacing="50" RepeatLayout="Table" AutoPostBack="true" Style="float: left">
                                                                <asp:ListItem Value="0" Selected="True">Bookings&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Value="1">Cancelled Booking&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div style="text-align: right; float: right;">
                                                            <asp:ImageButton ID="imgrefreshGrid" runat="server" Width="25px" Height="25px" ToolTip="Refresh Grid"
                                                                ImageUrl="~/Booking/Images/refresh1.png" />
                                                            <asp:LinkButton ID="imgbtn" runat="server" AlternateText="Xlsx">
                                                      <img alt="" src="Images/excel.png"  height="30px" style="margin:4px;margin-top:-20px;"/>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <telerik:RadGrid ID="gvTableBooking" runat="server" Skin="Office2010Blue" AutoGenerateColumns="false"
                                                            DataKeyNames="bookingref">
                                                            <MasterTableView>
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn HeaderText="No" UniqueName="No" AllowFiltering="false"
                                                                        Display="false">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataSetIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="Location" HeaderText="Location" />
                                                                    <telerik:GridBoundColumn DataField="Type" HeaderText="Schedule" />
                                                                    <telerik:GridBoundColumn DataField="bookingref" HeaderText="Booking Ref No" />
                                                                    <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                                                                    <telerik:GridBoundColumn DataField="covers" HeaderText="No Of People" />
                                                                    <telerik:GridBoundColumn DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                                    <telerik:GridBoundColumn DataField="Allotted_Tables" HeaderText="Allotted Table"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <telerik:GridBoundColumn DataField="bookingtime" HeaderText="Time" />
                                                                    <telerik:GridBoundColumn DataField="comment" HeaderText="Comment" />
                                                                    <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false"
                                                                        ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnbookingref" runat="server" Value='<%#Eval("bookingref")%>' />
                                                                            <asp:HiddenField ID="hdnClosed" runat="server" Value='<%#Eval("closed")%>' />
                                                                            <asp:HiddenField ID="hdnAllotted" runat="server" Value='<%#Eval("Allotted_Tables")%>' />
                                                                            <asp:LinkButton ID="lnkbtn" runat="server" CommandName="btnSeat" Text="Seat" SortExpression="Sync_Time"
                                                                                CommandArgument='<%#Eval("bookingref")%>' EnableViewState="true"> </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Update Booking" UniqueName="Update">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkpopup" runat="server" EnableViewState="true">
                                                                                <a href='<%# "PopupEditBookingTable.aspx?bookingref=" + Eval("bookingref").ToString() %>'
                                                                                    id="aChangeTable" runat="server" class="edit-booking">Update</a></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Print" UniqueName="Print">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkpopup1" runat="server" EnableViewState="true">
                                                                                <a href='<%# "PopupPrintBookingTableData.aspx?bookingid=" + Eval("bookingid").ToString() %>'
                                                                                    id="aChangeTable1" runat="server" class="edit-booking">
                                                                                    <img src="Images/printer.png" style="height: 25px;" /></a></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                        <telerik:RadGrid ID="gvCancelBooking" runat="server" Skin="Office2010Blue" AutoGenerateColumns="false"
                                                            DataKeyNames="bookingref">
                                                            <MasterTableView>
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn HeaderText="No" UniqueName="No" AllowFiltering="false"
                                                                        Display="false">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataSetIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="Location" HeaderText="Location" />
                                                                    <telerik:GridBoundColumn DataField="Type" HeaderText="Schedule" />
                                                                    <telerik:GridBoundColumn DataField="bookingref" HeaderText="Booking Ref No" />
                                                                    <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                                                                    <telerik:GridBoundColumn DataField="covers" HeaderText="No Of People" />
                                                                    <telerik:GridBoundColumn DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                                    <telerik:GridBoundColumn DataField="Allotted_Tables" HeaderText="Allotted Table No"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                                    <telerik:GridBoundColumn DataField="bookingtime" HeaderText="Time" />
                                                                    <telerik:GridBoundColumn DataField="comment" HeaderText="Comment" />
                                                                    <telerik:GridTemplateColumn HeaderText="Restore Booking" UniqueName="Restore">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbtn" runat="server" CommandName="btnRestore" Text="Restore"
                                                                                SortExpression="Sync_Time" CommandArgument='<%#Eval("bookingid")%>' EnableViewState="true"
                                                                                OnClientClick="if ( ! UserDeleteConfirmation()) return false;"> </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnBookingDate" runat="server" Value='<%#Eval("date")%>' />
                                                                            <asp:HiddenField ID="hdnBookingTime" runat="server" Value='<%#Eval("bookingtime")%>' />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                        <telerik:RadWindow runat="server" ID="RadWindow_ContentTemplate" Modal="true" Width="800px"
                                                            Height="300px" KeepInScreenBounds="True" Skin="Metro" Title="Informatic popup for Seat action"
                                                            VisibleStatusbar="False" EnableViewState="true" ReloadOnShow="true" Behaviors="Close">
                                                            <ContentTemplate>
                                                                <br />
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5" style="padding-bottom: 10px;">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Location :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblLocationWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Schedule :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblScheduleWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5" style="padding-bottom: 10px;">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Booking Ref No :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblBokingRefWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Booking Date :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblBookingDateWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5" style="padding-bottom: 10px;">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Full Name :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblFullNameWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>No. of people :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblCoversWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5" style="padding-bottom: 10px;">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Allotted Table :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblTableWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Time :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblTimeWin" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <div class="col-md-6">
                                                                            <label class="control-label">
                                                                                <b>Deposit Amount :</b></label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label ID="lblDepoAmount" class="control-label" runat="server" Text=""></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                                <br />
                                                                <div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                    <div class="col-md-10" style="text-align: center;">
                                                                        <asp:Button ID="btnSaveWin" runat="server" Text="Seat" CssClass="btn btn-primary"
                                                                            Width="60px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnCancelWin" runat="server" Text="Cancel" CssClass="btn" Width="60px" />
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix">
                                                                </div>
                                                            </ContentTemplate>
                                                        </telerik:RadWindow>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView runat="server" ID="RadPageView2" BorderColor="#BCC0C4" BorderWidth="1">
                                                    <div class="box-content">
                                                        <div class="form-horizontal">
                                                            <div class="row" style="padding-left: 20px;">
                                                                <div style="margin-bottom: 10px;">
                                                                    <asp:Label ID="lblErrorScript" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="divchart" runat="server" style="padding-left: 30px; padding-right: 30px;">
                                                                <div style="text-align: right;">
                                                                    <asp:ImageButton ID="imgbtnRefreshChart" runat="server" Width="25px" Height="25px"
                                                                        ToolTip="Refresh Future Tables" ImageUrl="~/Booking/Images/refresh1.png" />
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div>
                                                                    <script type="text/javascript">
                                                    <%= jscript %>
                                                                    </script>
                                                                    <script type="text/javascript" src="../Theme/js/canvasjs.min.js"></script>
                                                                    <div id="chartContainer" style="width: 100%; height: 451px;">
                                                                    </div>
                                                                    <a id="ahref" href="" style="visibility: hidden;" class="edit-booking"></a>
                                                                </div>
                                                                <br />
                                                                <div class="row" style="padding-left: 20px; padding-right: 20px; text-align: center;">
                                                                    <div class="col-md-4">
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <div style="height: 20px; width: 20px; background-color: #2F75B5; text-align: center;
                                                                            float: left;">
                                                                        </div>
                                                                        <div style="float: left; margin-left: 5px;">
                                                                            &nbsp;Booked</div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div style="height: 20px; width: 20px; background-color: #AEAAAA; text-align: center;
                                                                            float: left;">
                                                                        </div>
                                                                        <div style="float: left; margin-left: 5px;">
                                                                            &nbsp;Joined to another table</div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <div style="height: 20px; width: 20px; background-color: #cacece; text-align: center;
                                                                            float: left;">
                                                                        </div>
                                                                        <div style="float: left; margin-left: 5px;">
                                                                            &nbsp;Closed</div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView runat="server" ID="RadPageView3" BorderColor="#BCC0C4" BorderWidth="1">
                                                    <div class="box-content">
                                                        <div class="form-horizontal">
                                                            <div class="row" style="padding-left: 20px;">
                                                                <div style="margin-bottom: 10px;">
                                                                    <asp:Label ID="lblTableView" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div id="diviFrameTbl" runat="server" style="width: 100%;">
                                                                <iframe src="Tab_Table_Management.aspx" id="iframe1" width="100%" height="750px" scrolling="no" height="500"
                                                                    style="border-width: 0;"></iframe>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView runat="server" ID="RadPageView4" BorderColor="#BCC0C4" BorderWidth="1">
                                                    <div class="box-content">
                                                        <div class="form-horizontal">
                                                            <div id="div5" runat="server" style="width: 100%;">
                                                                <iframe src="WaitingList.aspx" width="100%" height="650px" style="border-width: 0;">
                                                                </iframe>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                            </telerik:RadMultiPage>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkLoadTableBooking" runat="server"></asp:LinkButton>
                            <asp:HiddenField ID="hdnSelectedDate" runat="server" Value="0" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

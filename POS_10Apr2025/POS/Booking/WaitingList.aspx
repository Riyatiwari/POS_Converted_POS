<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="WaitingList.aspx.vb"
    Inherits="WaitingList" EnableEventValidation="false" %>

<!doctype html>
<html lang="en">
<head>
    <meta http-equiv="refresh" content="300" charset="utf-8">
    <title>Table View</title>
    <link href="../Theme/live_view/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Theme/live_view/jquery.min.js" type="text/javascript"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../Theme/live_view/jquery-ui.js" type="text/javascript"></script>
    <link href="../Theme/live_view/live_view.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/resources/demos/style.css">
    <%--master page css--%>
    <link href="../css/Button.ss.css" rel="stylesheet" type="text/css" />
    <link href="../css/DropDownList.ss.css" rel="stylesheet" type="text/css" />
    <link href="../css/Input.ss.css" rel="stylesheet" type="text/css" />
    <link href="../css/Grid.ss.css" rel="stylesheet" type="text/css" />
    <link href="../css/CustomStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/TabStrip.ss.css" rel="stylesheet" type="text/css" />
    <link href="../Theme/assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Theme/assets/font-awesome/css/font-awesome.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../Theme/css/flaty.css" rel="stylesheet" type="text/css" />
    <link href="../Theme/css/flaty-responsive.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function OnClientClicked(sender, args) {
            var window = $find('<%=RadWindow_Waitlist.ClientID %>');
            window.close();
        }
    </script>
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
            background-color: #70767A; /*background-color: #767676;*/
            color: #333333;
            min-height: 35px;
            min-width: 60px;
            text-align: center;
            padding: 8px 5px 5px 5px;
            min-width: 100px;
        }
        .multiPage
        {
            display: inline-block;
            display: inline;
            zoom: 1;
            border-color: #BDC1C5;
        }
        .canvasjs-chart-credit
        {
            visibility: hidden !important;
        }
        
        
        input[type=radio]
        {
            margin: 5px;
        }
    </style>
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

</head>
<body bgcolor="White">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="lnkSaveShape">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="box-content">
        <div class="form-horizontal">
            <div class="row">
                <div class="col-md-12" id="div3" runat="server">
                    <div id="divMessageBox" runat="server" visible="false">
                        <asp:Label ID="lbExistingCust" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div style="width: 100%;">
                <telerik:RadGrid ID="gvwaitinglist" runat="server" Skin="Office2010Blue" AutoGenerateColumns="false"
                    DataKeyNames="BookingSettingsid">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="No" UniqueName="No" AllowFiltering="false"
                                Display="false">
                                <ItemTemplate>
                                    <%# Container.DataSetIndex+1 %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="StoreName" HeaderText="Location" />
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                            <telerik:GridBoundColumn DataField="comment" HeaderText="Comment" />
                            <telerik:GridBoundColumn DataField="covers" HeaderText="No Of People" />
                            <telerik:GridBoundColumn DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <telerik:GridBoundColumn DataField="waiting_no" HeaderText="Waiting No" />
                            <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false"
                                ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnStoreName" runat="server" Value='<%#Eval("StoreName")%>' />
                                    <asp:HiddenField ID="hdnwaitingno" runat="server" Value='<%#Eval("waiting_no")%>' />
                                    <asp:HiddenField ID="hdnwaitingid" runat="server" Value='<%#Eval("waiting_id")%>' />
                                    <asp:HiddenField ID="hdndate" runat="server" Value='<%#Eval("date")%>' />
                                    <asp:HiddenField ID="hdncomment" runat="server" Value='<%#Eval("comment")%>' />
                                    <asp:HiddenField ID="hdncovers" runat="server" Value='<%#Eval("covers")%>' />
                                    <asp:HiddenField ID="hdnaccountid" runat="server" Value='<%#Eval("accountid")%>' />
                                    <asp:HiddenField ID="hdnBookingSettingsid" runat="server" Value='<%#Eval("BookingSettingsid")%>' />
                                    <asp:HiddenField ID="hdnStoreID" runat="server" Value='<%#Eval("StoreID")%>' />
                                    <asp:LinkButton ID="lnkbtn" runat="server" Text="Offer Table" CommandName="btnOffer"
                                        SortExpression="" CommandArgument='<%#Eval("BookingSettingsid")%>' EnableViewState="true"
                                        CssClass="btn btn-primary" Style="width: 90px; height: auto; color: White;"> 
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <telerik:RadWindow runat="server" ID="RadWindow_Waitlist" Modal="true" Width="950px"
                Height="600px" KeepInScreenBounds="True" Skin="Metro" VisibleStatusbar="False"
                EnableViewState="true" ReloadOnShow="true" Behaviors="Close" VisibleOnPageLoad="false"
                DestroyOnClose="true" Title="Waiting Booking">
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
        </div>
    </div>
    </form>
</body>
</html>

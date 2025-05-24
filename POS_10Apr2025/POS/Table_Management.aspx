<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="Table_Management.aspx.vb"
    Inherits="Table_Management" EnableEventValidation="false" %>

<!doctype html>
<html lang="en">
<head>
    <meta http-equiv="refresh" content="120" charset="utf-8">
    <title>Table View</title>
    <link href="/Theme/live_view/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="/Theme/live_view/jquery.min.js" type="text/javascript"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="/Theme/live_view/jquery-ui.js" type="text/javascript"></script>
    <link href="/Theme/live_view/live_view.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/resources/demos/style.css">
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
        <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
            <script>
                function confirmSubmit(event) {
                    var ok = confirm("Are you sure you want to seat this guest?")
                    if (ok) {
                        var valueToSetTo = event
                        __doPostBack(lnkSet.name, valueToSetTo)
                    }
                }
                function Deletebtn(event) {
                    var ok = confirm("Are you sure you want to Delete?")
                    if (ok) {
                        var valueToSetTo = event.id
                        __doPostBack('deleteButton', valueToSetTo)
                    }
                }
            </script>
            <script type="text/javascript">

                function pageLoad() {
                    var $ = $telerik.$;
                    var dlistwidth = $('#<%= radShape.ClientID %>').width();
                    $('#<%= radShape.ClientID %>' + '_DropDown').width(dlistwidth);
                }
  
            </script>
            <script type="text/javascript">
                function showMenu(e) {
                    var contextMenu = $find("<%= RadContextMenu1.ClientID %>");
                    if ((!e.relatedTarget) || (!$telerik.isDescendantOrSelf(contextMenu.get_element(), e.relatedTarget))) {
                        contextMenu.show(e);
                        document.getElementById("hdnNameShape").value = e.currentTarget.id;
                    }
                    $telerik.cancelRawEvent(e);
                }
            </script>
            <script type="text/javascript">
                function showMenu2(e) {
                    var rightclick;
                    var e = e || window.event;
                    if (e.which == 3) {
                        var tr = e.currentTarget.parentElement;
                        var contextMenu = $find("<%= RadContextMenu2.ClientID %>");
                        if ((!e.relatedTarget) || (!$telerik.isDescendantOrSelf(contextMenu.get_element(), e.relatedTarget))) {
                            contextMenu.show(e);
                            document.getElementById("hdnShapeId").value = tr.getAttribute('id');
                        }
                        $telerik.cancelRawEvent(e);
                    }
                    else {
                        return false;
                    }
                }

            </script>
            <script type="text/javascript">
                function ConvertToImage(lnkSaveShape) {
                    html2canvas($("#dvTable")[0]).then(function (canvas) {
                        var base64 = canvas.toDataURL();
                        $("[id*=hfImageData]").val(base64);
                        __doPostBack(lnkSaveShape.name, "");
                    });
                    return false;
                }
            </script>
            <script type="text/javascript">
                function ConvertPageToImage(lnkExport) {
                    html2canvas($("#containmentwrapper")[0]).then(function (canvas) {
                        var base64 = canvas.toDataURL();
                        $("[id*=hdnExport]").val(base64);
                        __doPostBack(lnkExport.name, "");
                    });
                    return false;
                }
            </script>
            <script type="text/javascript">
                function OnClientClicked(sender, args) {
                    var window = $find('<%=RadWindow_ContentTemplate.ClientID %>');
                    window.close();
                }
            </script>
            <script type="text/javascript">
                function drag_Click() {
                    var rightclick;
                    var e = window.event;
                    if (e.which) rightclick = (e.which == 3);
                    else if (e.button) rightclick = (e.button == 2);
                    alert(rightclick); // true or false, you can trap right click here by if comparison
                }
            </script>
            <script type="text/javascript">
                function table_click(bookingref, status) {
                    if (status == "0") {
                        __doPostBack(lnkTableClick.name, bookingref)
                    } else {
                        return false;
                    }
                }
            </script>
            <script type="text/javascript">
                function OnClientClicked1(sender, args) {
                    var window = $find('<%=rwInfo.ClientID %>');
                    window.close();
                }
            </script>
        </telerik:RadScriptBlock>
        <telerik:RadContextMenu ID="RadContextMenu1" runat="server" Skin="MetroTouch">
            <Items>
                <telerik:RadMenuItem Text="Add" ImageUrl="../Theme/live_view/img/Add.png" />
            </Items>
        </telerik:RadContextMenu>
        <telerik:RadContextMenu ID="RadContextMenu2" runat="server" Skin="MetroTouch">
            <Items>
                <telerik:RadMenuItem Text="Edit" ImageUrl="../Theme/live_view/img/Edit.png" />
                <telerik:RadMenuItem Text="Send to back" ImageUrl="../Theme/live_view/img/Backward1.png" />
                <telerik:RadMenuItem Text="Bring to front" ImageUrl="../Theme/live_view/img/Forward1.png" />
                <telerik:RadMenuItem Text="Delete" ImageUrl="../Theme/live_view/img/delete_16.png" />
            </Items>
        </telerik:RadContextMenu>
        <asp:HiddenField ID="hdnExport" runat="server" />
        <input type="hidden" id="txtHidData" runat="server" />
        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />
        <div style="width: 100%;">
            <div style="float: right; width: 99%;">
                <div class="row" style="margin-bottom: 5px; height: 33px;">
                    <telerik:RadComboBox ID="ddLocation" runat="server" AutoPostBack="true" style="float:left" OnSelectedIndexChanged="ddLocation_SelectedIndexChanged" ></telerik:RadComboBox>
                      <asp:ImageButton ID="imgrefreshTableView" runat="server" Width="25px" Height="25px"  ToolTip="Refresh Table View"
                            OnClientClick="window.location.reload();" ImageUrl="~/Images/refresh1.png" />
                    <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
                         
                      
                        <asp:Button ID="lnkExport" Text="Export as Image" runat="server" UseSubmitBehavior="false"
                            ValidationGroup="RFShape" Visible="false" OnClick="lnkExport_Click" OnClientClick="return ConvertPageToImage(this)"
                            Style="margin-left: 5px; margin-right: 5px; padding: 7px; cursor: pointer;" CssClass="bodyfont btn-successBlue btnflat" />
                    </telerik:RadAjaxPanel>
                    <asp:Button ID="lnkDrag" Text="Drag" runat="server" UseSubmitBehavior="false" OnClick="drag_div"
                        Style="display: none;" />
                    <asp:Button ID="lnkSet" Text="Set" runat="server" UseSubmitBehavior="false" OnClick="Set_div"
                        Style="display: none;" />
                    <asp:LinkButton ID="lnkDashboard" CssClass="bodyfont btn-successBlue btnflat" runat="server"
                        Visible="false" PostBackUrl="~/Booking/Dashboard.aspx">Go to Dashboard
                    </asp:LinkButton>
                    <asp:Button ID="lnkTableClick" Text="Table" runat="server" UseSubmitBehavior="false"
                        OnClick="table_Click" Style="display: none;" />
                  
                </div>
                <div style="width: 100%; height: 600px; z-index: 100" id="containmentwrapper" runat="server"
                    oncontextmenu="return false;">
                    <asp:Repeater ID="rptDIV" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnTbl_id" runat="server" Value='<%# Eval("Table_id")%>' />
                            <asp:HiddenField ID="hdnAllowTable" runat="server" Value='<%# Eval("AllowedJoin")%>' />
                            <asp:HiddenField ID="hdnpageX" runat="server" Value='<%# Eval("XLeft")%>' />
                            <asp:HiddenField ID="hdnpageY" runat="server" Value='<%# Eval("YTop")%>' />
                            <div id='<%# Eval("Table_id")%>' class="draggable" style="z-index: 501; color: White;"
                                ondblclick="table_click('<%# Eval("bookingref")%>','<%# Eval("Status")%>')">
                                <%# Eval("div_html")%>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="rptOtherDIV" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="OTran_id" runat="server" Value='<%# Eval("OTran_id")%>' />
                            <asp:HiddenField ID="OpageX" runat="server" Value='<%# Eval("OpageX")%>' />
                            <asp:HiddenField ID="OpageY" runat="server" Value='<%# Eval("OpageY")%>' />
                            <asp:HiddenField ID="Tran_id" runat="server" Value='<%# Eval("Tran_id")%>' />
                            <div id='<%# Eval("OTran_id")%>' class="draggable" style='<%# "display: inline-block; cursor:move; z-index:" + DataBinder.Eval(Container.DataItem, "zindex") + ";" %>'>
                                <telerik:RadBinaryImage ID="RadBinaryImage2" runat="server" oncontextmenu="showMenu2(event)"
                                    Visible="true" Style='<%# "max-height:" + DataBinder.Eval(Container.DataItem, "height") + "; max-Width:" + DataBinder.Eval(Container.DataItem, "width") + ";" %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div style="float: left; width: 15%; height: 600px; display:none;" >
                <telerik:RadTabStrip runat="server" ID="RadTabStrip2" Orientation="HorizontalTop"
                    AutoPostBack="true" Width="100%" SelectedIndex="0" MultiPageID="RadMultiPage1"
                    Skin="Silk" BackColor="#B6D1F2">
                    <Tabs>
                        <telerik:RadTab Text="Objects" Width="40%">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Background" Width="60%">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <asp:HiddenField ID="hdnNameShape" runat="server" />
                <asp:HiddenField ID="hdnShapeId" runat="server" />
                <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage"
                    Width="100%">
                    <telerik:RadPageView runat="server" ID="RadPageView1" BorderColor="#6D7377" BackColor="#6D7377"
                        BorderWidth="1" Height="100%">
                        <div class="divmargin">
                            <div class="divbr">
                                <telerik:RadDropDownList ID="radShape" Skin="MetroTouch" runat="server" Width="100%"
                                    AutoPostBack="true" DefaultMessage="Shape">
                                </telerik:RadDropDownList>
                            </div>
                            <div class="divShapeVal">
                                <asp:Repeater ID="rpShapeVal" runat="server">
                                    <HeaderTemplate>
                                        <br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div id="<%#Eval("divId") %>" style="width: 100%; padding-bottom: 5px; cursor: pointer;"
                                            onclick="showMenu(event)">
                                            <div style="width: 100%; text-align: center;">
                                                <%#Eval("div_sample") %>
                                            </div>
                                            <div style="width: 100%; text-align: center;">
                                                <%#Eval("name") %>
                                            </div>
                                        </div>
                                        <hr style="margin-right: 10%; margin-left: 10%;" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="divWhit divbr2">
                                <b>*</b> For more information double click on the table
                                <br />
                                <b>*</b> Click
                                <asp:Image ID="Image2" runat="server" Style="width: 15px; height: 15px" src="Images/chair.png" />
                                on Booked table to Seat guest
                                
                            </div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView2" BorderColor="#6D7377" BackColor="#6D7377"
                        BorderWidth="1" Height="100%">
                        <div class="divmargin sizeMenuDiv">
                            <div class="divbr">
                                <div class="divBackground">
                                    <style>
                                        .wrapper
                                        {
                                            width: 90%;
                                            padding-top: 10px;
                                            padding-left: 10px;
                                            margin: 0 auto;
                                        }
                                        .left
                                        {
                                            float: left;
                                            width: 65%;
                                        }
                                        .right
                                        {
                                            float: right;
                                            width: 34%;
                                        }
                                    </style>
                                    <div class="wrapper">
                                        <div class="left">
                                            Color :</div>
                                        <div class="right">
                                            <telerik:RadColorPicker runat="server" ID="RadColorPicker2" ShowIcon="true" KeepInScreenBounds="true"
                                                AutoPostBack="true" PaletteModes="All">
                                            </telerik:RadColorPicker>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="wrapper">
                                        <div class="left">
                                            &nbsp;</div>
                                        <div class="right">
                                            <asp:LinkButton ID="lnkSaveBackground" CssClass="bodyfont lnkbtnNoColor btn-successBlue"
                                                OnClick="lnkSaveBackground_Click" runat="server">Save</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </div>
        <div>
            <div style="float: left; width: 100%; margin-top: 10px; border-color: #6D7377; border-width: 1px;
                border-style: solid;">
                <div style="width: 6%; float: left;">
                    <b style="margin: 5px;"><u>Legend:</u></b></div>
                <div style="width: 94%; float: left;">
                    <div style="display: block;">
                        <div style="display: table-cell; float: left;">
                            <div style="height: 20px; margin: 5px; width: 20px; float: left; background-color: #6EDA6E">
                            </div>
                            <div style="display: table-cell; float: left; margin: 5px;">
                                <asp:Label ID="Label23" runat="server" Text="Available"></asp:Label></div>
                            <div style="height: 20px; margin: 5px; width: 20px; float: left; background-color: #555e5e">
                            </div>
                            <div style="display: table-cell; float: left; margin: 5px;">
                                <asp:Label ID="Label22" runat="server" Text="Booked"></asp:Label></div>
                            <div style="height: 20px; margin: 5px; width: 20px; float: left; background-color: #949e9e">
                            </div>
                            <div style="display: table-cell; float: left; margin: 5px;">
                                <asp:Label ID="Label21" runat="server" Text="Seated"></asp:Label></div>
                        </div>
                    </div>
                    <asp:Repeater ID="rptLegend" runat="server">
                        <HeaderTemplate>
                            <div style="display: block;">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style="display: table-cell; float: left;">
                                <div style='<%# "height: 20px; margin:5px; width: 20px; float: left; background-color:#" + DataBinder.Eval(Container.DataItem, "color") + ";" %>'>
                                </div>
                                <div style="display: table-cell; float: left; margin: 5px;">
                                    <asp:Label ID="Label20" runat="server" Text='<%#Eval("state") %>'></asp:Label></div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <telerik:RadWindow runat="server" ID="RadWindow_ContentTemplate" Modal="true" Width="700px"
            Height="400px" KeepInScreenBounds="True" Skin="Metro" VisibleStatusbar="False"
            EnableViewState="true" ReloadOnShow="true" Behaviors="Close" VisibleOnPageLoad="false"
            DestroyOnClose="true">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
                    <div class="col-md-12" style="margin-top: 5px; height: 250px;">
                        <div style="float: left">
                            <div style="text-align: left !important; width: 100%; margin-top: 5px;">
                                <telerik:RadDropDownList ID="ddlSize" Skin="MetroTouch" runat="server" Width="250px"
                                    AutoPostBack="true" DefaultMessage="Size" DropDownWidth="250px">
                                    <Items>
                                        <telerik:DropDownListItem Text="10%" Value="10" />
                                        <telerik:DropDownListItem Text="20%" Value="20" />
                                        <telerik:DropDownListItem Text="30%" Value="30" />
                                        <telerik:DropDownListItem Text="40%" Value="40" />
                                        <telerik:DropDownListItem Text="50%" Value="50" />
                                        <telerik:DropDownListItem Text="60%" Value="60" />
                                        <telerik:DropDownListItem Text="70%" Value="70" />
                                        <telerik:DropDownListItem Text="80%" Value="80" />
                                        <telerik:DropDownListItem Text="90%" Value="90" />
                                    </Items>
                                </telerik:RadDropDownList>
                                <span style="color: Red;" runat="server" id="lblErrSize" visible="false"><b>&nbsp;*&nbsp;</b></span>
                            </div>
                            <br />
                            <div class="col-md-12" style="text-align: left !important; margin-top: 5px;">
                                <telerik:RadTextBox ID="txtCaption" runat="server" Skin="Office2010Blue" Width="250px"
                                    AutoPostBack="true" placeholder="Caption" Height="32px" CssClass="form-control tg"
                                    AutoCompleteType="Disabled" Display="false">
                                </telerik:RadTextBox>
                                <span style="color: Red;" runat="server" id="lblErrCaption" visible="false"><b>&nbsp;*&nbsp;</b></span>
                            </div>
                            <br />
                            <div class="col-md-12" style="text-align: left !important; margin-top: 5px;">
                                <label style="float: left">
                                    Change Color : &nbsp;&nbsp;&nbsp;&nbsp;</label>
                                <telerik:RadColorPicker runat="server" ID="RadColorPicker1" ShowIcon="true" CssClass="ColorPickerPreview"
                                    Style="float: left;" KeepInScreenBounds="true" AutoPostBack="true" PaletteModes="All">
                                </telerik:RadColorPicker>
                            </div>
                            <br />
                            <br />
                            <div class="col-md-12" style="text-align: left !important; margin-top: 5px;">
                                <telerik:RadButton ID="RadButton1" Style="float: right; height: 36px;" AutoPostBack="true"
                                    runat="server" OnClick="lnkCancel_Click" Text="Cancel" OnClientClicked="OnClientClicked"
                                    EnableEmbeddedSkins="false">
                                </telerik:RadButton>
                                <asp:Button ID="lnkSaveShape" Text="Add" runat="server" UseSubmitBehavior="false"
                                    ValidationGroup="RFShape" OnClick="lnkSaveShape_Click" OnClientClick="return ConvertToImage(this)"
                                    Style="margin-left: 5px; margin-right: 5px;" CssClass="bodyfont btn-successBlue btnflat" />
                            </div>
                        </div>
                        <div style="float: right; width: 50%;">
                            <div width="420px" height="250px" style="border-width: 0;">
                                <div style="width: 100%; text-align: center; vertical-align: middle;">
                                    <asp:Repeater ID="rptDemo" runat="server">
                                        <ItemTemplate>
                                            <div id="dvTable" style="display: inline-block">
                                                <%# Eval("div_display")%>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <br />
                                <asp:HiddenField ID="hfImageData" runat="server" />
                            </div>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow runat="server" ID="rwInfo" Modal="true" Width="400px" Height="520px"
            Title="Table Information" KeepInScreenBounds="True" Skin="Metro" VisibleStatusbar="False"
            EnableViewState="true" ReloadOnShow="true" Behaviors="Close" VisibleOnPageLoad="false"
            DestroyOnClose="true">
            <ContentTemplate>
                <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
                    <div style="width: 100%">
                        <br />
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label18" runat="server" Text="Booking Ref No." Width="50%"></asp:Label>
                            <asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="Table Number" Width="50%"></asp:Label>
                            <asp:Label ID="lblTableNo" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label1" runat="server" Text="Guest" Width="50%"></asp:Label>
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label16" runat="server" Text="Guest" Width="50%"></asp:Label>
                            <asp:Label ID="lblGuest" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label3" runat="server" Text="Balance" Width="50%"></asp:Label>
                            <asp:Label ID="lblBalance" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label4" runat="server" Text="Startime" Width="50%"></asp:Label>
                            <asp:Label ID="lblStartTime" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label6" runat="server" Text="Course 1 Sold" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse1Sold" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label8" runat="server" Text="Course 2 Sold" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse2Sold" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label10" runat="server" Text="Course 3 Sold" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse3Sold" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label5" runat="server" Text="Course 4 Sold" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse4Sold" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label7" runat="server" Text="Course 1 Served" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse1Served" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label11" runat="server" Text="Course 2 Served" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse2Served" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label13" runat="server" Text="Course 3 Served" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse3Served" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label15" runat="server" Text="Course 4 Served" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse4Served" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label9" runat="server" Text="Course 1 Away" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse1Away" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label14" runat="server" Text="Course 2 Away" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse2Away" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label17" runat="server" Text="Course 3 Away" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse3Away" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label19" runat="server" Text="Course 4 Away" Width="50%"></asp:Label>
                            <asp:Label ID="lblCourse4Away" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="padding-bottom: 5px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label12" runat="server" Text="Estimated time of completion" Width="50%"></asp:Label>
                            <asp:Label ID="lblETC" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div style="width: 100%">
                        <telerik:RadButton ID="lnkCloseWindow" Style="float: right; height: 36px;" AutoPostBack="true"
                            runat="server" Text="Cancel" OnClick="lnkCancel2_Click" OnClientClicked="OnClientClicked1"
                            EnableEmbeddedSkins="false">
                        </telerik:RadButton>
                    </div>
                </telerik:RadAjaxPanel>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
            <script>
                $(document).ready(function () {
                    <%= Script_text %>
                    <%= Script_text_O %>
                });
            </script>
            <script>
        <%= Script_offset %>
        <%= Script_offset_O %>
            </script>
            <script>
                $(".draggable").draggable({
                    snap: ".snap",
                    stop: function (event, ui) {
                        debugger;
                        var snapped = $(this).data('ui-draggable').snapElements;
                        var snappedTo = $.map(snapped, function (element) {
                            return element.snapping ? element.item : null;
                        });
                        var result = '';
                        $.each(snappedTo, function (idx, item) {
                            result += item.id + ",";
                        });
                        var position = $(this).data('ui-position');
                        $("#results").html("Snapped to: " + (result === '' ? "Nothing!" : result));
                        var valueToSetTo = event.target.id + '#' + ui.offset.left + '#' + ui.offset.top
                        __doPostBack(lnkDrag.name, valueToSetTo)
                    }
                });
            </script>
        </telerik:RadScriptBlock>
        <style type="text/css">
            .rbDecorated
            {
                background-color: #E04343;
                color: white;
                height: 36px;
                font-size: 15px;
                float: right;
            }
        </style>
        <br />
        <br />
        <br />
        <br />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Default"
        Style="position: absolute; top: 0; left: 0; height: 100%; width: 100%; z-index: 1000;"
        IsSticky="true">
    </telerik:RadAjaxLoadingPanel>
    <script src="../Theme/live_view/html2canvas.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

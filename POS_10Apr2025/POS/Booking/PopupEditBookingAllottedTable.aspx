<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupEditBookingAllottedTable.aspx.vb"
    Inherits="PopupEditBookingAllottedTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Theme/assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty-responsive.css" />
    <link rel="stylesheet" type="text/css" href="/css/Grid.ss.css" />
    <style>
        /** Multiple rows and columns */
        .multipleRowsColumns .rcbItem, .multipleRowsColumns .rcbHovered
        {
            float: left;
            margin: 0 1px;
            min-height: 13px;
            overflow: hidden;
            padding: 2px 19px 2px 6px;
            width: 150px;
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
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divErrorBinding" runat="server" visible="false">
                <asp:Label ID="lblErrorBinding" ForeColor="Red" runat="server" Text="You can't change tables for this Booking."></asp:Label>
            </div>
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
            <div id="divChangeTable" runat="server" visible="false">
                <div class="box">
                    <div class="box-title">
                        <h3>
                            Change Table</h3>
                    </div>
                    <div class="clear">
                    </div>
                    <br />
                    <div class="container">
                        <div class="left20">
                            <b>
                                <label class="col-lg-3 control-label">
                                    Booking:</label></b>
                        </div>
                        <div class="left20">
                            <asp:Label ID="lblBookingRef" runat="server"></asp:Label><br />
                        </div>
                        <div class="left20">
                            <b>
                                <label class="col-lg-4 control-label">
                                    Covers:</label></b>
                        </div>
                        <div class="left20">
                            <asp:Label ID="lblCovers" runat="server"></asp:Label>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="left20">
                            <b>
                                <label class="col-lg-3 control-label">
                                    Date:</label></b>
                        </div>
                        <div class="left20">
                            <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                        </div>
                        <div class="left20">
                            <b>
                                <label class="col-lg-4 control-label">
                                    Time:</label></b>
                        </div>
                        <div class="left20">
                            <asp:Label ID="lblTime1" runat="server"></asp:Label>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="left20">
                            <b>
                                <label class="col-lg-4 control-label">
                                    Location:</label></b>
                        </div>
                        <div class="left20">
                            <asp:Label ID="lblLocation" runat="server"></asp:Label>
                        </div>
                        <div class="left20">
                            <b>
                                <label class="col-lg-4 control-label">
                                    Schedule:</label></b>
                        </div>
                        <div class="left20">
                            <asp:Label ID="lblSchedule" runat="server"></asp:Label>
                        </div>
                        <div class="clear">
                        </div>
                        <br />
                        <b>
                            <label class="col-lg-3 control-label" style="padding-left: 15px;">
                                Available Tables:</label></b>&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadComboBox ID="ddlTableSet" Skin="MetroTouch" runat="server" Width="200px"
                            AutoPostBack="true" DropDownCssClass="multipleRowsColumns" AllowCustomText="true"
                            EnableScreenBoundaryDetection="false" EnableCheckAllItemsCheckBox="True" DropDownWidth="480px"
                            CheckBoxes="True" Filter="Contains" DropDownHeight="200px">
                        </telerik:RadComboBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="popallottbl"
                            ControlToValidate="ddlTableSet" ForeColor="Red" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label2" runat="server" Font-Size="X-Small" Text="*Max = Max Covers"></asp:Label>
                    </div>
                    <br />
                    <br />
                    <b>
                        <label class="col-lg-4 control-label" id="lblMaxCoverl" runat="server" visible="false">
                            Max Cover for Selected Tables:</label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMaxCover" runat="server" Text="0" Visible="false"></asp:Label>
                    <br />
                    <br />
                    <b>
                        <label class="col-lg-4 control-label" id="Label1" runat="server">
                        </label>
                        &nbsp;</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                    <br />
                    <br />
                    <div class="container" style="text-align: center;">
                        <asp:Button ID="btnConfitmBooking" runat="server" Text="Update Booking" CssClass="btn btn-success"
                            ValidationGroup="popallottbl" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel Booking" CssClass="btn btn-danger"
                            ValidationGroup="popallottbl" OnClientClick="return confirm('Are you sure you want to Cancel this booking ?');" />
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            &nbsp;<asp:Label ID="lblSuccess" runat="server" Text="" ForeColor="Green"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="custom_progress">
                <div id="overlay_load">
                </div>
                <div id="loading">
                    <img src="Images/ajax-loader.gif " alt="" />
                    <br />
                    Please Wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>

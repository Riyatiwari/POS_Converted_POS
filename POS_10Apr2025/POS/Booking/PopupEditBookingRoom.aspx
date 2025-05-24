<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupEditBookingRoom.aspx.vb"
    Inherits="BookingEasy_PopupEditBookingRoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Theme/assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty-responsive.css" />
    <link rel="stylesheet" type="text/css" href="/css/Grid.ss.css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div>
        <h3>
            Update Room</h3>
        <br />
        <b>
            <label class="col-lg-3 control-label">
                Booking:</label></b>&nbsp;&nbsp;
        <asp:Label ID="lblBookingRef" runat="server"></asp:Label>
        <br />
        <br />
        <b>
            <label class="col-lg-4 control-label">
                Date:</label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblDate" runat="server"></asp:Label>
        <br />
        <br />
        <b>
            <label class="col-lg-3 control-label">
                Room Type:</label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <telerik:RadDropDownList ID="ddlRoomType" Skin="MetroTouch" runat="server" Width="150px"
            DropDownHeight="100px" AutoPostBack="True">
        </telerik:RadDropDownList>
        <br />
        <br />
        <b>
            <label class="col-lg-4 control-label">
                Room:</label></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <telerik:RadDropDownList ID="ddlRoomName" Skin="MetroTouch" runat="server" Width="150px"
            DropDownHeight="100px">
        </telerik:RadDropDownList>
        &nbsp;&nbsp;
    </div>
    <br />
    <asp:Panel ID="pnlExtraServices" runat="server" Visible="true">
        <div class="row">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-4 col-lg-4">
                    </div>
                    <div class="col-sm-4 col-lg-4 controls" style="text-align: center;">
                        <asp:Button ID="btnConfitmBooking" runat="server" Text="Update Booking" CssClass="btn btn-success" OnClientClick="return confirm('Are you sure you want to Update this Record ?');" />
                    </div>
                    <div class="col-sm-4 col-lg-4">
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-2 col-lg-2">
                    </div>
                    <div class="col-sm-8 col-lg-8 controls" style="text-align: center;" id="divNoRooms"
                        runat="server" visible="False">
                        <asp:Label ID="lblNoRooms" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-sm-2 col-lg-2">
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>

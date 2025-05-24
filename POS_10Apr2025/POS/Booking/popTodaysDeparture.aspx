<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popTodaysDeparture.aspx.vb" Inherits="BookingEasy_popTodaysDeparture" %>

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
        <p>
            <strong>
                <asp:Literal ID="lblAmount" runat="server" Text=""></asp:Literal>
            </strong>
        </p>
        <asp:Button ID="btnCheckOut" runat="server" Text="Complete Check Out" CssClass="btn btn-primary" />
        <div id="divMessageBox" runat="server" visible="False">
            <button data-dismiss="alert" class="close">
                ×</button>
            <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popTodaysArrival.aspx.vb"
    Inherits="BookingEasy_popTodaysArrival" %>

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
        <%= CurrencySymbol %>
        <telerik:RadDropDownList ID="ddLcredit" runat="server" Skin="MetroTouch" DropDownHeight="200px">
            <Items>
                <telerik:DropDownListItem Value="10000" Text="100.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="15000" Text="150.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="20000" Text="200.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="20000" Text="250.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="30000" Text="300.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="30000" Text="350.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="40000" Text="400.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="40000" Text="450.00"></telerik:DropDownListItem>
                <telerik:DropDownListItem Value="50000" Text="500.00"></telerik:DropDownListItem>
            </Items>
        </telerik:RadDropDownList>
        <asp:Button ID="btnCheckin" runat="server" Text="Checkin" CssClass="btn btn-primary" />
    </div>
    </form>
</body>
</html>

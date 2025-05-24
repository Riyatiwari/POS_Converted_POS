<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Booking_Confirm_Process_Paypal.aspx.vb" 
Inherits="BookingEasy_Booking_Confirm_Process_Paypal" Title="Redirecting to payment gateway...." %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
<form runat="server">
<asp:Button ID="btnRedirect" runat="server" Text="Button" />
</form>
</body>


<%--<body onload="updateVal()">
    <form name="_xclick" id="paypalform" method="post" runat="server">
    <input type="text" name="amount" id="amount" runat="server" />
    
    <input type="hidden" name="cmd" value="_xclick" />
    <input type="hidden" name="business" value="zankar.vora@gmail.com" id="x_loginID"/>
    <input type="hidden" name="currency_code" id="x_currency" runat="server"/>
    <input type="hidden" name="item_name" id="x_item" value="123321" />
    <input type="hidden" name="return" value="http://localhost:58232/demo/Default3.aspx"/>
    <input type="hidden" name="cancel_return" value="http://localhost:58232/demo/Default3.aspx"/>
    <input type="hidden" name="rm" value="1" id="rm" />
    <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/x-click-but6.gif" name="submit" alt="PayPal - The safer, easier way to pay online!" />
    <input type="hidden" name="notify_url" value="http://localhost:58232/demo/Default3.aspx" />
    </form>
</body>--%>
</html>

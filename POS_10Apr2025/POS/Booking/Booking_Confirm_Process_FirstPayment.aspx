<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Booking_Confirm_Process_FirstPayment.aspx.vb"
    Inherits="Booking_Confirm_Process_FirstPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        window.onload = function () {
            var button = document.getElementById('btnlbl');
            button.form.submit();
        }
    </script>
    <style type="text/css">
        .btn
        {
            visibility: hidden; /*border-style: none;
            border-color: inherit;
            border-width: 0;
            background: url('//testcontent.authorize.net/images/buy-now-gold.gif') no-repeat 0 0;
            height: 47px;
            width: 150px;*/
        }
        .overlay
        {
            position: fixed;
            width: 100%;
            height: 100%;
            left: 0;
            top: 0;
            background: WhiteSmoke /*rgba(51,51,51,0.7)*/;
            z-index: 10;
        }
        .imageContainer
        {
            position: absolute; /*width: 100px;*/ /*the image width*/ /*height: 100px;/* /*the image height*/
            left: 50%;
            top: 50%;
            margin-left: -50px; /*half the image width*/
            margin-top: -50px; /*half the image height*/
        }
        .txtContainer
        {
            position: relative;
            top: 50%;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <input type="hidden" name="merchantID" id="merchantID" value="0" runat="server" />
    <input type="hidden" name="amount" value="1050" id="amount" runat="server" />
    <input type="hidden" name="countryCode" id="countryCode" runat="server" />
    <input type="hidden" name="currencyCode" id="currencyCode" runat="server" />
    <input type="hidden" name="transactionUnique" id="transactionUnique" runat="server" />
    <input type="hidden" name="orderRef" id="orderRef" runat="server" />
    <input type="hidden" name="redirectURL" id="redirectURL" runat="server" />
    <input type="hidden" name="signature" id="signature" runat="server" />
    <input type="hidden" name="customerEmail" id="customerEmail" runat="server" />
    <input type="hidden" name="customerPhone" id="customerPhone" runat="server" />
    <input type="hidden" name="customerAddress" id="customerAddress" runat="server" />
    <input type="hidden" name="customerPostCode" id="customerPostCode" runat="server" />
    <input class="btn" id="btnlbl" type="submit" value="Pay Now" />
    <div class='overlay'>
        <input type="image" src="Images/ajax-loader.gif" class="imageContainer" />
        <p class="txtContainer">
            Redirecting to Payment Gateway ... Please wait !!! Don't press Back or Refresh</p>
    </div>
    </form>
</body>
</html>

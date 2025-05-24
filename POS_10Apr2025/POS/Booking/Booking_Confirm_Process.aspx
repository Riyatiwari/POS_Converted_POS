<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Booking_Confirm_Process.aspx.vb"
    Inherits="Booking_Confirm_Process" Title="Redirecting to payment gateway...." %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Authorize.net</title>
    <script type="text/javascript">
        window.onload = function () {
            var button = document.getElementById('buttonLabel');
            button.form.submit();
        }
    </script>
    <style type="text/css">
        .btn
        {
            visibility: hidden;
            /*border-style: none;
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
    <!--
By default, this sample code is designed to post to our test server for
developer accounts: https://test.authorize.net/gateway/transact.dll
for real accounts (even in test mode), please make sure that you are
posting to: https://secure.authorize.net/gateway/transact.dll
-->
    <form id="simForm" runat="server" method="post" action="">
    <input type="hidden" />
    <input id="HiddenValue" type="hidden" value="Initial Value" runat="server" />
    <input type="hidden" runat="server" name="x_login" id="x_login" />
    <input type="hidden" runat="server" name="x_amount" id="x_amount" />
    <input type="hidden" runat="server" name="x_description" id="x_description" />
    <input type="hidden" runat="server" name="x_invoice_num" id="x_invoice_num" />
    <input type="hidden" runat="server" name="x_fp_sequence" id="x_fp_sequence" />
    <input type="hidden" runat="server" name="x_fp_timestamp" id="x_fp_timestamp" />
    <input type="hidden" runat="server" name="x_cust_id" id="x_cust_id" />
    <input type="hidden" runat="server" name="x_fp_hash" id="x_fp_hash" />
    <input type="hidden" runat="server" name="x_test_request" id="x_test_request" />
    <input type="hidden" runat="server" name="x_first_name" id="x_first_name" />
    <input type="hidden" runat="server" name="x_last_name" id="x_last_name" />
    <input type="hidden" runat="server" name="x_email" id="x_email" />
    <input type="hidden" runat="server" name="x_phone" id="x_phone" />
    <input type="hidden" runat="server" name="x_address" id="x_address" />
    <input type="hidden" name="x_show_form" value="PAYMENT_FORM" />

    <%--<asp:Image ID="Image1" runat="server" ImageUrl="//testcontent.authorize.net/images/buy-now-gold.gif" id="buttonLabel" onclick="return buttonLabel_onclick()"/>--%>
    <input class="btn" type="submit" runat="server" id="buttonLabel" clientidmode="Inherit" />

        <div class='overlay'>
        <input type="image" src="Images/ajax-loader.gif" class="imageContainer" />
        <p class="txtContainer">
            Redirecting to Payment Gateway ... Please wait !!! Don't press Back or Refresh</p>
    </div>
    </form>
    
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datePicker_demo.aspx.vb" Inherits="datePicker_demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        //$(function () {
        //    $("[id*=txtDate]").datepicker({
        //        showOn: 'button',
        //        buttonImageOnly: true,
        //        buttonImage: 'images/calendar.png'
        //    });
        //});

        $(document).ready(function () {
        $('#txtDate').datepicker({
            dateFormat: 'dd/mm/yy'
        });
    });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="txtDate" ReadOnly="true" ></asp:TextBox>
        </div>
    </form>
</body>
</html>

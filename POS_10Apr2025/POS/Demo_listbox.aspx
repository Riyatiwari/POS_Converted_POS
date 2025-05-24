<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Demo_listbox.aspx.vb" Inherits="Demo_listbox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/bootstrap-3.1.1.min.css" type="text/css" />
    <link rel="stylesheet" href="css/bootstrap-multiselect.css" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="js/bootstrap-2.3.2.min.js"></script>
    <script type="text/javascript" src="js/bootstrap-multiselect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <asp:ListBox ID="radPrinter" runat="server" Width="100%" SelectionMode="Multiple"></asp:ListBox>

           <%-- <select id="chkveg" multiple="multiple">
                <option value="cheese">Cheese</option>
                <option value="tomatoes">Tomatoes</option>
                <option value="mozarella">Mozzarella</option>
                <option value="mushrooms">Mushrooms</option>
                <option value="pepperoni">Pepperoni</option>
                <option value="onions">Onions</option>
            </select><br />--%>
            <br />

            <script type="text/javascript">
                $(function () {
                    $('#radPrinter').multiselect({
                        includeSelectAllOption: true
                    });
                    
                });
            </script>
        </div>
    </form>
</body>
</html>

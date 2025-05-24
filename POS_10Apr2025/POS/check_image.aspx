<%@ Page Language="VB" AutoEventWireup="false" CodeFile="check_image.aspx.vb" Inherits="check_image" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
         <%--   <asp:Image ID="imgProd" runat="server" ImageUrl="CustomerProfileHandler.ashx?Customer_id=2299&Cmp_id=2&store_name=POS_nike" />--%>

            <asp:Button runat="server" ID="btn" OnClick="btn_Click" Text="send" />
                           
        </div>
    </form>
</body>
</html>

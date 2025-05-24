<%@ Page Language="VB" AutoEventWireup="false" CodeFile="imagepost.aspx.vb" Inherits="imagepost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="simForm" runat="server" method="post" action="">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <input type="hidden" runat="server" name="ModuleName" id="ModuleName" />
    <input type="hidden" runat="server" name="Image" id="Image" />
    1<input type="submit" runat="server" id="buttonLabel" clientidmode="Inherit" />
     2<input type="submit" runat="server" id="Submit1" clientidmode="Inherit" />
    </form>
</body>
</html>

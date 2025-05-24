<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Encode_Decode.aspx.vb" Inherits="Encode_Decode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp; <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;&nbsp; <asp:Button ID="Button1" runat="server" Text="Encode" />
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>&nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="Decode" />
    </div>
        <br />
        <div>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>&nbsp; <asp:Label ID="Label3" runat="server"></asp:Label>&nbsp;&nbsp; <asp:Button ID="Button3" runat="server" Text="Decrypt" />
        <br />
        <br />
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>&nbsp;<asp:Label ID="Label4" runat="server"></asp:Label>&nbsp;&nbsp; <asp:Button ID="Button4" runat="server" Text="Encrypt" />
            <br />
         </div>
   

    </form>
</body>
</html>

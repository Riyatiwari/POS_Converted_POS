<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PayGatewayDetail.aspx.vb" Inherits="PayGatewayDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
</head>
<body>

    <div class="row">
        <div class="col-md-12">
            <table width="100%" cellspacing="0" border="1">
                <tbody>
                    <tr>
                        <td style="padding: 7px;"><b>Ref : </b>
                            <asp:Label ID="lblRef" runat="server" Text=""></asp:Label>
                        </td>
                        <td><b>Date :</b>
                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 7px;"><b>Time :</b>
                            <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
                        </td>
                        <td><b>RelatedTransaction :</b>
                            <asp:Label ID="lblRltdTran" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 7px;"><b>AcquirerRef : </b>
                            <asp:Label ID="lblAcquirerRef" runat="server" Text=""></asp:Label>
                        </td>
                        <td><b>Type :</b>
                            <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 7px;"><b>Class :</b>
                            <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>

                        </td>
                        <td><b>Auth Status :</b>
                            <asp:Label ID="lblAuthStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="padding: 7px;"><b>Currency : </b>
                            <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                        </td>
                        <td><b>Amount : </b>
                            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 7px;"><b>Card :</b>
                            <asp:Label ID="lblCard" runat="server" Text=""></asp:Label>
                        </td>
                        <td><b>Card Type :</b>
                            <asp:Label ID="lblCardType" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 7px;"><b>Card Country :</b>
                            <asp:Label ID="lblCardCountry" runat="server" Text=""></asp:Label>
                        </td>
                        <td><b>Region :</b>
                            <asp:Label ID="lblRegion" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 7px;"><b>Expiry :</b>
                            <asp:Label ID="lblExpiry" runat="server" Text=""></asp:Label>
                        </td>
                        <td><b>Auth Code :</b>
                            <asp:Label ID="lblAuthCode" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 7px;"><b>Auth Message :</b>
                            <asp:Label ID="lblAuthMessage" runat="server" Text=""></asp:Label>

                        </td>
                        <td><b>Settle Status :</b>
                            <asp:Label ID="lblSettleStatus" runat="server" Text=""></asp:Label>
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>
    </div>


</body>
</html>

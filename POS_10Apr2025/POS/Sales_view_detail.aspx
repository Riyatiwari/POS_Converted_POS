<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sales_view_detail.aspx.vb" Inherits="Sales_view_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ingredients and Condiments</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
    <link type="text/css" rel="stylesheet" href="vendors/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="vendors/bootstrap/css/bootstrap.min.css" />
    <!--Loading style vendors-->
    <link type="text/css" rel="stylesheet" href="vendors/animate.css/animate.css" />
    <link type="text/css" rel="stylesheet" href="vendors/iCheck/skins/all.css" />
    <!--Loading style-->
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" />
    <link type="text/css" rel="stylesheet" href="css/style-responsive.css" />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" />
    <%--<style>
        .page-form input[type='text'],
        .page-form input[type='password'],
        .page-form input[type='email'],
        .page-form select {
            height: 40px;
        }

        body {
            /*background-image: url("bg.jpg");*/
            background-repeat: no-repeat;
            background-size: cover;
        }

        .header {
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .login {
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .addons {
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }



        .button {
            background-color: #1D4E89; /* Green */
            border: none;
            color: #ffffff;
            padding: 15px 30px;
            text-align: center;
            text-decoration-color: antiquewhite;
            text-decoration: none;
            display: inline-block;
            font-size: 14px;
            margin: 45px 20px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
            font-family: "PT Sans",sans-serif;
            font-weight: 400;
            border-radius: 4px;
            box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);
        }

            .button a:link {
                background-color: #1D4E89;
                color: #ffffff;
                text-decoration: none;
                border: 0px solid #1D4E89;
                text-decoration-color: antiquewhite;
            }

            .button:hover {
                background-color: #13335A;
                color: rgb(255, 255, 255);
            }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-yellow">
            <div class="panel-heading">Ingredients</div>

            <table class="table table-bordered" id="Lsummary" width="100%" cellspacing="0">
                <asp:Repeater ID="rdIngredient" runat="server">
                    <HeaderTemplate>
                        <thead>
                            <tr>
                                <th>Ingredient</th>
                                <th>Size</th>
                            </tr>
                        </thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #ffffff;"><%#Eval("Product")%></td>
                            <td style="background-color: #ffffff;"><%#Eval("Size")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                                </table>
                    </FooterTemplate>
                </asp:Repeater>
            </table>

        </div>
        <br />
        <div>
            <div class="panel panel-yellow">
                <div class="panel-heading">Condiments</div>

                <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                    <asp:Repeater ID="radListCondiment" runat="server">
                        <HeaderTemplate>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="background-color: #ffffff;">
                                    <img src="images/checkbox.png" />
                                    <%#Eval("Condiment")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>

                <%-- <telerik:RadListBox RenderMode="Lightweight" ID="radListCondiment" runat="server">
                        <ItemTemplate>
                            <img src="images/checkbox.png" />
                            <asp:Label ID="lblCondiment" runat="server" Text='<%#Eval("Condiment")%>' />
                        </ItemTemplate>

                    </telerik:RadListBox>--%>
            </div>
        </div>
    </form>
</body>
</html>

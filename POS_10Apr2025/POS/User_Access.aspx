<%@ Page Language="VB" AutoEventWireup="false" CodeFile="User_Access.aspx.vb" Inherits="User_Access" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>UserSignIn</title>
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
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" class="default-style" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" id="theme-change"
        class="style-change color-change" />
    <link type="text/css" rel="stylesheet" href="css/style-responsive.css" />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <style>
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
    </style>
</head>
<body style="background-image: url(images/bg/bg.jpg); opacity: 0.9;">

    <div class="header">
        <img src="images/bg/logo.png" alt="TenderPOS Logo" />
    </div>
    <div class="page-form">
        <form action="User_Access.aspx" class="form" runat="server">
            <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
            <div class="header-content">
                <h1>Log In</h1>
            </div>
            <div class="body-content">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogIn">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mbm text-center">
                                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="require"></asp:Label>
                            </div>

                            <div id="divLogin" runat="server">
                             <%--   <div class="form-group">
                                    <div class="input-icon right">
                                        <i class="fa fa-home"></i>
                                        <telerik:RadTextBox ID="txtstorename" runat="server" Width="100%" BorderWidth="0" placeholder="Store Name" TabIndex="1" CssClass="form-control">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtstorename" runat="server" Display="Dynamic" ErrorMessage="Store name is required."
                                            ValidationGroup="vlogin" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>--%>
                                <div class="form-group">
                                    <div class="input-icon right">
                                        <i class="fa fa-user"></i>
                                        <telerik:RadTextBox ID="txtUname" runat="server" Width="100%" BorderWidth="0" placeholder="Email" TabIndex="1" CssClass="form-control">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtUname" runat="server" Display="Dynamic" ErrorMessage="Username is required."
                                            ValidationGroup="vlogin" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="input-icon right">
                                        <i class="fa fa-key"></i>
                                        <telerik:RadTextBox ID="txtUpassword" runat="server" InputType="Text" TextMode="Password" Width="100%" BorderWidth="0" placeholder="Password"
                                            TabIndex="2" CssClass="form-control">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password required." Display="Dynamic" ControlToValidate="txtUpassword"
                                            ValidationGroup="vlogin" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group pull-left">
                                    <%--<div class="checkbox-list">
                                <label>
                                    <input type="checkbox">&nbsp; Keep me signed in</label>
                            </div>--%>
                                </div>
                                <div class="form-group pull-right">
                                    <asp:LinkButton ID="btnLogIn" CssClass="btn btn-success" runat="server" ValidationGroup="vlogin">Log In &nbsp;<i class="fa fa-chevron-circle-right"></i></asp:LinkButton>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="forget-password">
                                    <h4>Forgotten your Password?</h4>
                                    <p>click <a href='Reset_Password.aspx' class='btn-forgot-pwd'>here</a> to reset your password.</p>
                                    <p>click <a href='SignIn.aspx' class='btn-forgot-pwd'>here </a>Back To Login</p>
                                    <div style="margin-left: 278px">
                                        <asp:Label ID="lblVersion" runat="server" Text="Version 1.0.3.0.0.0"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </form>
    </div>

    <div class="addons" style="display:none;">
        <button class="button button"><a style="color: white; font-family: 'PT Sans',sans-serif; font-weight: 400;" href="https://www.tenderpos.com">Sign Up</a> </button>
        <button class="button button"><a style="color: white; font-family: 'PT Sans',sans-serif; font-weight: 400;" href="https://www.tenderpos.com/pay/">TenderPay</a> </button>
        <button class="button button"><a style="color: white; font-family: 'PT Sans',sans-serif; font-weight: 400;" href="https://www.tenderpos.com/connect">TenderConnect</a> </button>
    </div>

    <script src="js/jquery-1.10.2.min.js"></script>
    <script src="js/jquery-migrate-1.2.1.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <!--loading bootstrap js-->
    <script src="vendors/bootstrap/js/bootstrap.min.js"></script>
    <script src="vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <script src="vendors/iCheck/icheck.min.js"></script>
    <script src="vendors/iCheck/custom.min.js"></script>
</body>
</html>

<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>SignIn</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
    <!--Loading bootstrap css-->
    <%--<link type="text/css" rel="stylesheet"
        href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,800italic,400,700,800" />
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Oswald:400,700,300" />--%>
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
    </style>
</head>
<body style="background-image: url(images/bg/2.jpg)">
    <div class="page-form">
        <form action="Login.aspx" class="form" runat="server">
            <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
            <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
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
                                <div class="form-group">
                                    <div class="input-icon right">
                                        <i class="fa fa-user"></i>
                                        <telerik:RadTextBox ID="txtTillCode" runat="server" Width="100%" BorderWidth="0" placeholder="Till Code" TabIndex="1" CssClass="form-control">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtTillCode" runat="server" Display="Dynamic" ErrorMessage="Till Code is required."
                                            ValidationGroup="vlogin" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <%--<div class="form-group">
                                    <div class="input-icon right">
                                        <i class="fa fa-key"></i>
                                        <telerik:RadTextBox ID="txtUpassword" runat="server" InputType="Text" TextMode="Password" Width="100%" BorderWidth="0" placeholder="Password"
                                            TabIndex="2" CssClass="form-control">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password required." Display="Dynamic" ControlToValidate="txtUpassword"
                                            ValidationGroup="vlogin" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                --%><div class="form-group pull-left">
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
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <%--                    <p>Log in with a social network:</p>--%>
            </div>
            <%--</telerik:RadAjaxPanel>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>--%>
        </form>
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



<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Forgot_Password.aspx.vb" Inherits="Forgot_Password" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Forgot Password</title>
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
        <form action="Forgot_Password.aspx" class="form" runat="server">
            <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                <div class="header-content">
                    <h1>Reset Password</h1>
                </div>
                <div class="body-content">
                    <div class="form-group">
                        <div class="input-icon right">
                            <i class="fa fa-key"></i>
                            <telerik:RadTextBox ID="txtNewPassword" runat="server" Width="100%" BorderWidth="0" placeholder="New Password" TabIndex="1" CssClass="form-control" TextMode="Password">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New Password Required"
                            ValidationGroup="reset" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-icon right">
                            <i class="fa fa-key"></i>
                            <telerik:RadTextBox ID="txtConfirm" runat="server" Width="100%" BorderWidth="0" placeholder="Confirm Password" TabIndex="2" CssClass="form-control" TextMode="Password">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirm" ErrorMessage="Confirm Password Required"
                            ValidationGroup="reset" Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password and confirm password don't match" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirm"
                                CssClass="text-danger" ValidationGroup="reset" Display="Dynamic"></asp:CompareValidator>
                        </div>
                    </div>
                    <div class="form-group pull-right">
                        <asp:Button ID="btnReset" runat="server" Text="Submit" ValidationGroup="reset" class="btn btn-success" />
                    </div>
                    <div class="clearfix"></div>
                    <div class="forget-password">
                        <h4>Go to Login Page</h4><p>click <a href='SignIn.aspx' class='btn-forgot-pwd'>here</a>.</p>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>
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
    <script>//BEGIN CHECKBOX & RADIO
        $('input[type="checkbox"]').iCheck({
            checkboxClass: 'icheckbox_minimal-grey',
            increaseArea: '20%' // optional
        });
        $('input[type="radio"]').iCheck({
            radioClass: 'iradio_minimal-grey',
            increaseArea: '20%' // optional
        });
        //END CHECKBOX & RADIO</script>
    
</body>
</html>

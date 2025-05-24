<%@ Page Language="VB" AutoEventWireup="false" CodeFile="mailReset_Password.aspx.vb" Inherits="mailReset_Password" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Reset Password</title>
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
    <div class="page-form" >
        <form  class="form" runat="server" style="align-content:center ;margin-left:10%;width:80%">


            <div>
    <h2>Reset Password</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="New Password" Required="true" Style="width:80%"></asp:TextBox>
    <br />
    <br />
    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password" Required="true" Style="width:80%"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnReset" runat="server" Text="Reset Password" OnClick="btnReset_Click" CssClass="btn btn-primary" style="margin-left:0%"/>
</div>



            


             <div class="forget-password">
                        <h4>Sign in to your account</h4><p>click <a href='User_Access.aspx' class='btn-forgot-pwd'>here</a>.</p>
                    </div>
         
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

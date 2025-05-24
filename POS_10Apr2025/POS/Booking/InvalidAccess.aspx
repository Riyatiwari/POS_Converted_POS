<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InvalidAccess.aspx.vb" Inherits="BookingEasy_PageNotFound" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8">
    <title>Page Not Found</title>
    <link href="../Theme/assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Theme/assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Theme/assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Theme/css/flaty.css" rel="stylesheet" type="text/css" />
    <link href="../Theme/css/flaty-responsive.css" rel="stylesheet" type="text/css" />
</head>
<body class="error-page">
    <form id="form1" runat="server">
    <!-- BEGIN Main Content -->
    <div class="error-wrapper">
        <h4>Unauthorized access</h4>
        <p>
            You have no rights to access this page.</p>
        <br />
        <hr />
        <%--<p class="clearfix">
            <a href="javascript:history.back()" class="pull-left">← Back to previous page</a>
        </p>--%>
    </div>
    <!-- END Main Content -->
    <!--basic scripts-->
    <script src="../Theme/js/jquery.min.js" type="text/javascript"></script>
    <script>        window.jQuery || document.write('<script src="assets/jquery/jquery-2.0.3.min.js"><\/script>')</script>
    <script src="../Theme/assets/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

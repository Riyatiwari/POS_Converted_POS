<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Transaction_Detail.aspx.vb" Inherits="Transaction_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
    <link type="text/css" rel="stylesheet" href="vendors/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="vendors/bootstrap/css/bootstrap.min.css" />

    <!--Loading style-->
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" />
    <link type="text/css" rel="stylesheet" href="css/style-responsive.css" />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/MyStyle.css" />


      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="vendors/googleapis/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-yellow" style="padding: 10px;">
           
            <div class="col-md-12" style="padding: 10px;">

                <asp:Literal ID="ltTable" runat="server" />

            </div>

        </div>
    </form>
</body>
</html>

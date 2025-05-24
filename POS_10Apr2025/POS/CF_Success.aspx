<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CF_Success.aspx.vb" Inherits="CF_Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css?family=Nunito+Sans:400,400i,700,900&display=swap" rel="stylesheet">
    <title>Payment Successful</title>
    <style>
        body {
            text-align: center;
            padding: 40px 0;
            background: #EBF0F5;
        }

        h1 {
            color: #88B04B;
            font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
            font-weight: 900;
            font-size: 40px;
            margin-bottom: 10px;
        }

        p {
            color: #404F5E;
            font-family: "Nunito Sans", "Helvetica Neue", sans-serif;
            font-size: 20px;
            margin: 0;
        }

        i {
            color: #9ABC66;
            font-size: 100px;
            line-height: 200px;
            margin-left: -15px;
        }

        .card {
            background: white;
            padding: 60px;
            border-radius: 4px;
            box-shadow: 0 2px 3px #C8D0D8;
            display: inline-block;
            margin: 0 auto;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">



        <div class="card">
            <div style="border-radius: 200px; height: 200px; width: 200px; background: #F8FAF5; margin: 0 auto;">
                <i class="checkmark">✓</i>
            </div>
            <h1>Success</h1>
            <p>
                We received your Payment;<br />
            </p>
            <p>
                Your transaction was successful!<br /><br />
                <b><u>Payment Details</u></b><br />
                </p>
            <p style="text-align:left;"><br />
                Transaction reference : 
                <asp:Label runat="server" ID="lblTranRef" Text="asd45462caeFD"></asp:Label> <br /><br />

                We advise to keep this number for future reference. 
                <br />
            </p>
            <br />
            <p style="font-style:italic;font-size:12px;">
            Order date : <asp:Label runat="server" ID="lblDatetime" Text=""></asp:Label><br />
                </p>

            <p>
               <a ID="lbtnLink" runat="server" > Take Me to Home >></a>
            </p>
        </div>
    </form>
</body>
</html>

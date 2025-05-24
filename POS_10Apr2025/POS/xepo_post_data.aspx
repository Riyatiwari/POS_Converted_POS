<%@ Page Language="VB" AutoEventWireup="false" CodeFile="xepo_post_data.aspx.vb" Inherits="xepo_post_data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="Authorization" content="Basic RDlDMEMyODc2RTE3NDM1OTk0NjYwM0NFNjJGRDQ5QUI6Y25vZS1pQ3RlSGF3ckR4WFJYaWdXdkktVXJIenp6bk1Bd3o3RWZicnROalBqYzU3" />
    <meta name="Content-Type" content="application/x-www-form-urlencoded" />
</head>
<body>

    <form action="https://identity.xero.com/connect/token" method="POST"  >
        <div>
            <label for="grant_type"></label>
            <input name="grant_type" id="grant_type" value="authorization_code">
        </div>
        <div>
            <label for="code"></label>
            <input name="code" id="code" value="10c38dd321d6d128ff6e4facc082b164231ade1eb1f969453715f06c7f5baf08">
        </div>
         <div>
            <label for="redirect_uri"></label>
            <input name="redirect_uri" id="redirect_uri" value="https://localhost:44319/Xero_Redirected.aspx">
        </div>

        <div>
            <button>Send</button>
        </div>
    </form>

      <%--  <div>
            <label for="Authorization"></label>
            <input name="Authorization" id="Authorization" value="Basic QTA1M0VDRkU3RTQ0NEIyNzg4MzE4N0IzNkZBRTRBQjE6dnpjc19Dc195NzRBaFROQmV5TmpXODYweFNlcUpFOXlnRWVmd2NCUkoxdUN1eXpv">
        </div>
         <div>
            <label for="Content-Type"></label>
            <input name="Content-Type" id="Content-Type" value="application/x-www-form-urlencoded">
        </div>--%>
       <%--<div>
            <label for="Authorization"></label>
            <input name="Authorization" id="Authorization" value="Basic QTA1M0VDRkU3RTQ0NEIyNzg4MzE4N0IzNkZBRTRBQjE6dnpjc19Dc195NzRBaFROQmV5TmpXODYweFNlcUpFOXlnRWVmd2NCUkoxdUN1eXpv">
        </div>
        <div>
            <label for="grant_type"></label>
            <input name="grant_type" id="grant_type" value="client_credentials">
        </div>
        <div>
            <label for="scope"></label>
            <input name="scope" id="scope" value="accounting.transactions">
        </div>--%>

</body>
</html>

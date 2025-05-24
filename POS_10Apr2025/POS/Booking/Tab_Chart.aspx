<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tab_Chart.aspx.vb" Inherits="Tab_Chart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script type="text/javascript">
          <%= jscript %>
        </script>
        <script type="text/javascript" src="../Theme/js/canvasjs.min.js"></script>
        <div id="chartContainer" style="width: 100%; height: 451px; overflow: scroll;">
        </div>
        <a id="ahref" href="" style="visibility: hidden;" class="edit-bookingAlloatedTables">
        </a>
    </div>
    </form>
</body>
</html>

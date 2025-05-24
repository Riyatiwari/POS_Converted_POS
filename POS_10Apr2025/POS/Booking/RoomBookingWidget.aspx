<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RoomBookingWidget.aspx.vb"
    Inherits="BookingEasy_TableBookingWidget" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/ucRoomWidget.ascx" TagName="RoomBooking" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Room Booking Widget</title>
    <link href='<%# ResolveUrl("~/css/Button.ss.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/css/DropDownList.ss.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/css/Input.ss.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/css/Grid.ss.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/css/CustomStyle.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/css/TabStrip.ss.css") %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%# ResolveUrl("~/Theme/assets/bootstrap-datepicker/css/datepicker.css") %>' />
    <link href='<%# ResolveUrl("~/Theme/assets/bootstrap/css/bootstrap.min.css") %>'
        rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/Theme/assets/font-awesome/css/font-awesome.min.css") %>'
        rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/Theme/css/flaty.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%# ResolveUrl("~/Theme/css/flaty-responsive.css") %>' rel="stylesheet"
        type="text/css" />
    <link href='<%# ResolveUrl("~/css/Grid.ss.css") %>' rel="stylesheet" type="text/css" />
</head>
<%--<body onload="afterPostBack();">--%>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <!-- BEGIN Container -->
    <div class="container" id="main-container">
        <!-- BEGIN Content -->
        <div id="main-content">
            <div class="row">
                <div class="form-horizontal">
                    <div class="col-md-4">
                        <uc1:RoomBooking ID="RoomBooking" runat="server" IsDropdownPostback="false" />
                    </div>
                </div>
            </div>
        </div>
        <!-- END Content -->
    </div>
    <!-- END Container -->
    <!--basic scripts-->
    <script src='<%= ResolveUrl("~/Theme/js/jquery.min.js") %>' type="text/javascript"></script>
    <%--<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>--%>
    <script type="text/javascript">        window.jQuery || document.write('<script src="assets/jquery/jquery-2.0.3.min.js"><\/script>')</script>
    <script src='<%= ResolveUrl("~/Theme/assets/bootstrap/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/jquery-slimscroll/jquery.slimscroll.min.js") %>'
        type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/jquery-cookie/jquery.cookie.js") %>'
        type="text/javascript"></script>
    <!--page specific plugin scripts-->
    <script src='<%= ResolveUrl("~/Theme/assets/flot/jquery.flot.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/flot/jquery.flot.resize.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/flot/jquery.flot.pie.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/flot/jquery.flot.stack.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/flot/jquery.flot.crosshair.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/flot/jquery.flot.tooltip.min.js") %>'
        type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/sparkline/jquery.sparkline.min.js") %>'
        type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/bootstrap-wizard/jquery.bootstrap.wizard.js") %>'></script>
    <script src='<%= ResolveUrl("~/Theme/assets/jquery-validation/dist/jquery.validate.min.js") %>'
        type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/assets/jquery-validation/dist/additional-methods.min.js") %>'
        type="text/javascript"></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Theme/assets/bootstrap-datepicker/js/bootstrap-datepicker.js") %>'></script>
    <!--flaty scripts-->
    <script src='<%= ResolveUrl("~/Theme/js/flaty.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Theme/js/flaty-demo-codes.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $('.date-picker').datepicker({
            format: 'dd/mm/yyyy'
        }).on('changeDate', function () { __doPostBack('TableBooking$lnkDateChange', ''); });

        $(document).ready(function () {
            $('#divMessage').hide();
        });

        $('.btnMaxCover').on("mouseover", function () { $('#divMessage').show(); });
        $('.btnMaxCover').on("mouseout", function () { $('#divMessage').hide(); });
    </script>
    </form>
</body>
</html>

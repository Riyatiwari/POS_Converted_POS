<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popEditCustomer.aspx.vb" Inherits="BookingEasy_popEditCustomer" %>

<%@ Register Src="../UserControl/Customer.ascx" TagName="Customer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/Theme/assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/assets/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty.css" />
    <link rel="stylesheet" type="text/css" href="/Theme/css/flaty-responsive.css" />
    <link rel="stylesheet" type="text/css" href="/css/Grid.ss.css" />
    <style>
        .row
        {
            margin-left: 0 !important;
            margin-right: 0 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm2" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-md-12" style="padding-left: 0">
            <div class="box">
                <div class="box-title">
                    <h3>
                        <i class="fa fa-table"></i>Customer Detail</h3>
                    <div class="box-tool">
                        <%--<a data-action="collapse" href="#"><i class="fa fa-chevron-up"></i></a><a data-action="close" href="#"><i class="fa fa-times"></i></a>--%>
                    </div>
                </div>
                <div class="box-content">
                    <div class="form-horizontal">
                        <uc1:Customer ID="ucCustomer1" runat="server" />
                        <div class="form-group">
                            <div class="col-sm-4 col-lg-4">
                            </div>
                            <div style="text-align: center;" class="col-sm-4 col-lg-4 controls">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" />
                            </div>
                            <div class="col-sm-4 col-lg-4">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

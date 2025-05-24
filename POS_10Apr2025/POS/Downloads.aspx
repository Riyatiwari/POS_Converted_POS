<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Downloads.aspx.vb" Inherits="Downloads" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Download
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Download List</li>
        </ol>
    </div>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <%--<div class="col-lg-12" id="divCustomer" runat="server">--%>
    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Download Latest Tender POS APK</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row" id="divCustomer" runat="server">
                            <div class="col-lg-12 ">
                                >   <a href="https://live.mytenderpos.com/Version/Version_New/TenderPOS.apk">Tender POS</a>
                                <br />
                                >   <a href="https://live.mytenderpos.com/Version/Version_New/Controller.apk">POS Controller</a>
                                <br />
                                >   <a href="https://live.mytenderpos.com/Version/Version_New/Permissions.apk">Port Permissions</a>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
            <div class="panel panel-yellow">
                <div class="panel-heading">Download Old Tender POS APK</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row" id="div1" runat="server">

                            <div class="col-lg-12 ">
                                >  <a href="https://live.mytenderpos.com/Version/Version_Old/TenderPOS.apk">Tender POS</a>
                                <br />
                                >  <a href="https://live.mytenderpos.com/Version/Version_Old/Controller.apk">POS Controller</a>
                                <br />
                                >   <a href="https://live.mytenderpos.com/Version/Version_Old/Permissions.apk">Port Permissions</a>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="panel panel-yellow">
                <div class="panel-heading">Download 2.0 Tender POS APK</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row" id="div2" runat="server">

                            <div class="col-lg-12 ">
                                >  <a href="https://live.mytenderpos.com/Version/Version_2/TenderPOS.apk">Tender POS</a>
                                <br />
                                >  <a href="https://live.mytenderpos.com/Version/Version_2/Controller.apk">POS Controller</a>
                                <br />
                                >   <a href="https://live.mytenderpos.com/Version/Version_2/Permissions.apk">Port Permissions</a>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="panel panel-yellow">
                <div class="panel-heading">Download Testing Tender POS APK</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row" id="div3" runat="server">

                            <div class="col-lg-12 ">
                                >  <a href="https://live.mytenderpos.com/Version/Version_testing/TenderPOS.apk">Tender POS</a>
                                <br />
                                >  <a href="https://live.mytenderpos.com/Version/Version_testing/Controller.apk">POS Controller</a>
                                <br />
                                >   <a href="https://live.mytenderpos.com/Version/Version_testing/Permissions.apk">Port Permissions</a>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>


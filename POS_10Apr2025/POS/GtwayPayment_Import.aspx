<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="GtwayPayment_Import.aspx.vb" Inherits="GtwayPayment_Import" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    GateWay Payment Import
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">GateWay Payment Import</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">GateWay Payment Import</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <div class="row">
                                            <center>
                                                 <div class="col-lg-4" style="text-align:right;">
                                                     Import file :   &nbsp;&nbsp;
                                                 </div>
                                                <div class="col-lg-4" style="text-align:left;">
                                                      <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </div>
                                                 </center>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <br />

                                        <div style="text-align: center; margin-right: 20px;">
                                            <asp:LinkButton ID="btnUpload" runat="server" class="btn btn-primary" Text="Submit" OnClick="btnUpload_Click"></asp:LinkButton>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>


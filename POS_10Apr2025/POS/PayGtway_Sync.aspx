<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="PayGtway_Sync.aspx.vb" Inherits="PayGtway_Sync" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Integration
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Integration</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Integration</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row" style="text-align: center;">
                        <div class="col-lg-12 ">
                            <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-primary" Text="Get Recored" OnClick="btnSubmit_Click"></asp:LinkButton>

                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="clearfix"></div>
                        <div class="col-lg-12">
                            <label runat="server" id="lbl_msg" style="font-weight: 700;">
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


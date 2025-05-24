<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="BarStock_Exchange_Master.aspx.vb" Inherits="BarStock_Exchange_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 BarStock Exchange Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">BarStock Exchange Master</li>
        </ol>
    </div>
    <script type="text/javascript">
         
 
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
 
            </div>


     <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow" runat="server" id="div1">
                <div class="panel-heading">BarStock Exchange </div>
                <div class="panel-body pan">

                   
                    </div>

                    <div class="form-actions text-center pal" id="Div2" runat="server">
                       
               
                    <asp:Button ID="price" type="submit" class="btn btn-primary" runat="server" Text="Send Category" OnClick="price_Click" />&nbsp;&nbsp;&nbsp;
              
                        <br /><br /><br />

                        <asp:Button ID="sales" type="submit" class="btn btn-primary" runat="server" Text="Send Items" OnClick="sales_Click" />&nbsp;&nbsp;&nbsp;
                    </div>

                </div>
 </telerik:RadAjaxPanel>
            </div>

 
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


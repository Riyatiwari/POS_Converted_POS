<%@ Page Title="" Language="VB" MasterPageFile="~/BookingEasy/Site.master" AutoEventWireup="false"
    CodeFile="DynamicPage.aspx.vb" Inherits="BookingEasy_DynamicPage" %>

    <%@ Register Src="~/UserControl/Customer.ascx" TagName="Customer" TagPrefix="uc" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="pnlContainer" runat="server" Style="overflow: hidden;">
            <style type="text/css">
                .test
                {
                    margin-right: 10px;
                    padding-top: 5px;
                }
            </style>
            <div class="box-content">
                <div class="form-horizontal">
                <uc:Customer ID="ucCustomer1"  runat="server" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

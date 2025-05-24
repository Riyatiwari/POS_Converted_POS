<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="defaultValues.aspx.vb" Inherits="defaultValues" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Default Values
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Default Values</li>
        </ol>
    </div>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <br />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Default Values</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Button ID="btnCurrency" class="btn btn-primary" runat="server" Text="Currency" Width="70%" OnClick="btnCurrency_Click" />&nbsp;&nbsp;&nbsp;
                   
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Button ID="btnCountry" class="btn btn-primary" runat="server" Text="Country / City / State" Width="70%" OnClick="btnCountry_Click" />&nbsp;&nbsp;&nbsp;
                   
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Button ID="btnBooking" class="btn btn-primary" runat="server" Text="Booking" Width="70%" OnClick="btnBooking_Click" />&nbsp;&nbsp;&nbsp;
                   
                        </div>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Button ID="btnDeviceType" class="btn btn-primary" runat="server" Text="Device Type" Width="70%" OnClick="btnDeviceType_Click" />&nbsp;&nbsp;&nbsp;
                   
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Button ID="btnExpense" class="btn btn-primary" runat="server" Text="Expense" Width="70%" OnClick="btnExpense_Click" />&nbsp;&nbsp;&nbsp;
                   
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Button ID="btnUnit" class="btn btn-primary" runat="server" Text="Unit" Width="70%" OnClick="btnUnit_Click" />&nbsp;&nbsp;&nbsp;
                   
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


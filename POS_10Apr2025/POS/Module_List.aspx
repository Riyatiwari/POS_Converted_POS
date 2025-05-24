<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Module_List.aspx.vb" Inherits="support_Module_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Support Modules List</li>
        </ol>
    </div>
    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<ul>
        
        <li><a href="#">Sales</a>              </li>
        <li><a href="#">web sales sync      </a></li>
        <li><a href="#">web sales payment  </a></li>
        <li><a href="#">web sales print       </a></li>
    </ul>--%>
    <br />
    <div class="container-fluid main_container">
        <div class="account-bottom">
            
                &nbsp&nbsp<asp:LinkButton ID="btnEnableBooking" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">Enable Booking</asp:LinkButton>
            &nbsp&nbsp<asp:LinkButton ID="btnDefaultImage" runat="server" class="btn btn-primary" ToolTip="View Report" ValidationGroup="valid">Default Image</asp:LinkButton>
            <br />
            <br />
            <div class="col-md-4 account-left" runat="server" visible="false">
                <div class="address" style="background-color: white">

                    <i class="glyphicon glyphicon-shopping-cart" style="font-size: 50px;"></i>
                    &nbsp&nbsp&nbsp&nbsp
                        <a href="#"><span style="font-size: 40px">sales</span> </a>
                </div>
                <br />
                <div class="address" style="background-color: white">

                    <i class="glyphicon glyphicon-transfer" style="font-size: 50px;"></i>
                    &nbsp&nbsp&nbsp&nbsp
                        <a href="#"><span style="font-size: 40px">web sales sync </span></a>
                </div>

            </div>


        </div>

        <div class="account-bottom">
            <div class="col-md-4 account-left" runat="server" visible="false">
                <div class="address" style="background-color: white">

                    <i class="glyphicon glyphicon-credit-card" style="font-size: 50px;"></i>
                    &nbsp&nbsp&nbsp&nbsp
                        <a href="#"><span style="font-size: 40px">web sales payment</span></a>
                </div><br />
                <div class="address " style="background-color: white" >

                    <i class="glyphicon glyphicon-print " style="font-size: 50px;"></i>
                    &nbsp&nbsp&nbsp&nbsp
                        <a href="#"><span style="font-size: 40px">web sales print</span> </a>
                </div>



            </div>
        </div>
    </div>

   
          

       

</asp:Content>


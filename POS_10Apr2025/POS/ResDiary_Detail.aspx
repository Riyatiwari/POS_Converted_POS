<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ResDiary_Detail.aspx.vb" Inherits="RD_Detail" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    ResDiary Details
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="ResDiary_List.aspx">ResDiary List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">ResDiary Details</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">ResDiary Details</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>Booking ID : </label>
                                        <label runat="server" id="lblBookingId"></label>
                                    </div>

                                    <div class="col-md-6">
                                        <label>Reference No. : </label>
                                        <label runat="server" id="lblref"></label>

                                    </div>


                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Customer Name : </label>
                                        <label runat="server" id="lblcust_name"></label>

                                    </div>
                                    <div class="col-md-6">
                                        <label>Visit Date Time : </label>
                                        <label runat="server" id="lblvisit_DT"></label>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Covers :</label>
                                        <label runat="server" id="lblcovers"></label>

                                    </div>
                                    <div class="col-md-6">
                                        <label>Duration : </label>
                                        <label runat="server" id="lblduration"></label>

                                    </div>



                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Turn Time : </label>
                                        <label runat="server" id="lblturn_time"></label>


                                    </div>
                                    <div class="col-md-6">
                                        <label>Is Customer VIP ? : </label>
                                        <label runat="server" id="lblcust_vip"></label>


                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Is Table Locked ? : </label>
                                        <label runat="server" id="lbltbl_locked"></label>

                                    </div>
                                    <div class="col-md-6">
                                        <label>Has Promotions : </label>
                                        <label runat="server" id="lblpromotion"></label>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Has Payments : </label>
                                        <label runat="server" id="lblpayments"></label>

                                    </div>
                                    <div class="col-md-6">
                                        <label>Is Leave Time Confirmed ? : </label>
                                        <label runat="server" id="lbl_leave_time"></label>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Is WalkIn ? : </label>
                                        <label runat="server" id="lblwalkin"></label>

                                    </div>
                                    <div class="col-md-6">
                                        <label>Number Of Bookings : </label>
                                        <label runat="server" id="lblnumof_booking"></label>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Comments : </label>
                                        <label runat="server" id="lblcomments"></label>

                                    </div>
                                    <div class="col-md-6">
                                        <label>Arrival Status : </label>
                                        <label runat="server" id="lblarrival_sts"></label>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Meal Status : </label>
                                        <label runat="server" id="lblmeal_sts"></label>
                                    </div>

                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Text="Back" ToolTip="Click here to go back" />
                    </div>
                </div>

            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>


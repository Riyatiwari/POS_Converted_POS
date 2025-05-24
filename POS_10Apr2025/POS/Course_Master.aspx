<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Course_Master.aspx.vb" Inherits="Course_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Course Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Course_List.aspx">Course List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Course Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

            <div class="panel panel-yellow">
                <div class="panel-heading">Course Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                     <div class="col-md-12">
                                        <label>Course Category<span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadComboBox ID="rdCourCat" runat="server"  EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%" >
                                    </telerik:RadComboBox>
                                        </div>
                                   <div class="clearfix"></div>
                                        <br />
                                    <div class="col-md-12">
                                        <label>Value <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtValue" CssClass="form-control" Skin="" runat="server" placeholder="Value" Width="100%"  ></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValue"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Value is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                        <br />
                                      <div class="col-md-12">
                                        <label>Check Slot Cover Limit<span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <div class="clearfix"></div>
                                         <asp:CheckBox id="chkCheckSLot" runat="server" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                   
                                </div>
                            </div>

                        </div>
                    </div>


                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Course Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Course Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Course Details" />
                    </div>
                </div>

            </div>
        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

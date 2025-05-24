<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TillShifts_Master.aspx.vb" MasterPageFile="~/MasterPage.master" Inherits="TillShifts_Master" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   Till Shifts Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="TillShifts_List.aspx">Till Shifts List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Till Shifts Master</li>
        </ol>
    </div>
    
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
     
            <div class="panel panel-yellow">
                <div class="panel-heading">Till Shifts Master</div>
                

                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="container">
                            <div class="form-group">
                                <div class="row mt-3">
                                    <div class="clearfix"></div>
                                         <div class="col-sm-6" >

                                <%--<label>Till </label>--%>
                                             <label>Venue </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="Radtill" runat="server" Width="40%"  AutoPostBack="true" visible="false">
                                        </telerik:RadComboBox>
                                        <telerik:RadComboBox ID="RadVenue" runat="server" Width="40%"  AutoPostBack="true">
                                        </telerik:RadComboBox>
                            </div>
                                    <div class="col-sm-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Shift Name<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtshiftname" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="Shift Name"></telerik:RadTextBox>
                                        <div class="clearfix"></div>
                                        
                                        <div class="clearfix"></div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtshiftname" ErrorMessage=" Please Enter The  Shift Name "
                                            ValidationGroup="valid"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-sm-6">
                                       <label>Sorting No<span class="text-danger"></span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtshiftno" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="Sorting No"></telerik:RadTextBox>
                                        </div>
                                   
                                </div>
                            </div>
                        </div>
                    
                        <div class="container">
                            <div class="form-group">
                                <div class="row  mt-3">
                                    <div class="col-sm-6">


                                        <label>Active </label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chk_Active" runat="server"/>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                </div>
                            </div>
                        </div>
                    </div>
                   
                    <div class="row">
                    </div>
                    <div class="row">
                    </div>

                </div>
                <div class="form-actions text-right pal">
                  
                    <asp:Button ID="btnSave" class="btn btn-primary" ValidationGroup="valid" runat="server" Text="Save" ToolTip="Click here to Save Till Shifts Details" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" OnClick="btnReset_Click" ToolTip="Click here to Reset Till Shifts Details" />&nbsp;&nbsp;&nbsp;
            
                       <asp:Button ID="btnCancel" class="btn btn-green" runat="server" OnClick="btnCancel_Click" Text="Cancel" ToolTip="Click here to Cancel Till Shifts Details" />
                </div>
            </div>
    </div>
   

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

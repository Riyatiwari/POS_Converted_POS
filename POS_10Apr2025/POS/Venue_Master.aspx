<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Venue_Master.aspx.vb" Inherits="Venue_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Venue Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Venue_List.aspx">Venue List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Venue Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Venue Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtVName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Venue name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                         
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Description </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtdesc" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine"  placeholder="Description" Width="100%"></telerik:RadTextBox>

                                    </div>
                                
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Sorting No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtSortingno" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Sorting No" MaxLength="4" Enabled="false">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Date & Time (If Date Change) </label>
                                        <div class="clearfix"></div>
                                       <telerik:RadMaskedTextBox ID="txtdatetime" runat="server" Mask="<00..23>:<00..59>"
                                                            Width="100%" Skin="" CssClass="form-control" >
                                                        </telerik:RadMaskedTextBox>
                                        
                                    </div>

                                     <div class="clearfix"></div>
                                    <br />
                                     <div class="col-md-12">
                                        <label>Auto Print Receipt &nbsp; &nbsp;
                                            <asp:CheckBox ID="chkprintreceipt" runat="server" Checked="true" /></label> &nbsp; &nbsp; &nbsp;
                                         <label>Auto Print Duplicate Receipt &nbsp; &nbsp;
                                        <asp:CheckBox ID="chkprintduplicatereceipt" runat="server" Checked="true" /></label>
                                    </div>

                                      <div class="clearfix"></div>
                                    <br />
                                    <div id="visible_false" runat="server" visible="false">
                                     <div class="col-md-12">
                                        <label>Group By &nbsp; &nbsp;
                                               
                                            <asp:CheckBox ID="chkgroupby" runat="server" Checked="false" AutoPostBack="true"/></label> &nbsp; &nbsp; &nbsp;
                                         <label>Consile Date &nbsp; &nbsp;
                                        <asp:CheckBox ID="chkdate" runat="server" Checked="False" AutoPostBack="true" Visible="false" /></label>
                                    </div>
                                 
                                </div>
                                   

                                  
                                <div id="divgroupby" runat="server" visible="false">
                                    <div class="col-md-12">
                                     <label>Group By With:</label>
                                  
                                        <telerik:RadComboBox ID="ddlgroupby" runat="server" Width="100%" AutoPostBack="true"  >
                                              <Items>
                                                                                   <telerik:RadComboBoxItem Text="None" Value="1" Selected="true"/>
                                                                                   <telerik:RadComboBoxItem Text="Departmnet" Value="2" />
                                                                                   <telerik:RadComboBoxItem Text="Course" Value="3"/>
                                                                                   </Items>
                                        </telerik:RadComboBox>
                                       <%-- <asp:RequiredFieldValidator ID="rfvDeviceType" runat="server" ControlToValidate="ddlgroupby" ErrorMessage=" Group By is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    </div>
                                
                                <div class="clearfix"></div>
                                    <br />

                                    
                                    <div class="col-md-12">
                                        <label>
                                            Active &nbsp;
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                    </div>
                                   <%-- <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>
                                            Open &nbsp;
                                            <asp:CheckBox ID="chkOpen" runat="server" Checked="true" /></label>
                                    </div>
                                    <div class="clearfix"></div>--%>
                                </div>
                            </div>

                         


                            <div class="col-md-6">
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Venue Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Venue Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Venue Details" />
                    </div>

                </div>

            <%--</div>--%>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Profile_Master.aspx.vb" Inherits="Profile_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Profile Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Profile_List.aspx">Profile List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Profile Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Profile  Master</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="container">
                        <div class="form-group">
                            <div class="col-md-6">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtproname"
                                    ValidationGroup="valid" Display="None" ErrorMessage="Profile Name  is required" CssClass="text-danger">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="row mt-3">
                                <div class="clearfix"></div>
                                <div class="col-sm-6">
                                    <label>Profile Name <span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtproname" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="Profile Name"></asp:TextBox>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-sm-6">

                                    <label>Discount in %</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtdiscountPercent" CssClass="form-control" runat="server" Width="50%" placeholder="Discount in %">
                                        <%--<NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="form-group">
                            <div class="row  mt-3">
                                <div class="clearfix"></div>
                                <div class="col-sm-6">
                                    <label>Reedem Value</label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtamt" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="Reedeem Value">
                                        <%--   <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                       <%-- <NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                    </asp:TextBox>
                                </div>
                                <div class="col-sm-6">

                                    <label>Reedeem Point</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtbnsp" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="Redeem Point">
                                        <%--<NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                    </asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="form-group">
                            <div class="row  mt-3">
                                <div class="col-sm-6">
                                    <label>Earn Point</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtearn_bonus" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="Earn Point">
                                        <%--<NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                    </asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <label>On Amount</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtpurchase_amount" CssClass="form-control" Skin="" runat="server" Width="50%" placeholder="On Amount">
                                        <%--<NumberFormat GroupSeparator="" DecimalDigits="2" KeepTrailingZerosOnFocus="true" />--%>
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="form-group">
                            <div class="row  mt-3">
                                <div class="col-sm-6">
                                    <label>Is Default</label><div class="clearfix"></div>
                                    <asp:CheckBox ID="chk_IsDefaul" runat="server" Checked="false" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--<div class="form-body pal">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtproname"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Profile Name  is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                                
                                <div class="col-md-12">
                                    <label>Profile Name <span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtproname" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Bonus Point</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtbnsp" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Amount </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtamt" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                <%--  </div>--%>

                    <%-- <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Purchase Amount</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtpurchase_amount" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                                </div>
                                 <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Earn Bonus</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtearn_bonus" CssClass="form-control" Skin="" runat="server" Width="100%"></telerik:RadTextBox>
                   </div>
                                 </div> 
                              
                                <div class="clearfix"></div>
                                <br />
                            
                        </div>
                        <div class="row">
                        </div>
                        </div>--%>
                </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Profile Details" OnClick="btnSave_Click1" />&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" OnClick="btnReset_Click" ToolTip="Click here to Reset Profile Details" />&nbsp;&nbsp;&nbsp;
            
                       <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Profile Details" />
                </div>
            </div>

        </div>
        <%--</telerik:RadAjaxPanel>--%>
    </div>

    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>

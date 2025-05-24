<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Barcode_Master.aspx.vb" Inherits="Barcode_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Barcode Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Barcode_List.aspx">Barcode List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Barcode Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />

    <div class="col-lg-12 ">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Barcode Master</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">

                                <div class="col-md-6">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                    <label>
                                        Barcode <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <asp:TextBox ID="txbarcode" CssClass="form-control" Skin="" runat="server" placeholder="Barcode" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txbarcode"
                                        ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Barcode is required">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">

                                    <label>
                                        Product <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <asp:DropDownList ID="radProduct" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="radProduct_SelectedIndexChanged">
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfproduct" runat="server" ControlToValidate="radProduct" ErrorMessage="Product is required"
                                        ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">

                                    <label>Size </label>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="radSize" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Active &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Barcode Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Barcode Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Barcode Details" />
                </div>
            </div>
        </div>
        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <%--  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>

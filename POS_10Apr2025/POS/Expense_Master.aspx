<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Expense_Master.aspx.vb" Inherits="Expense_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Expense Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Expense_List.aspx">Expense List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Expense Master</li>
        </ol>
    </div>

    <style>
        .radioButtonList input[type="radio"] {
            width: auto;
            float: left;
        }

        .radioButtonList label {
            width: auto;
            display: inline;
            float: left;
            margin-right:40px;
            margin-left :5px;
        }
    </style>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />

    <div class="col-lg-12 ">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
         --%>  
            <div class="panel panel-yellow">
                <div class="panel-heading">Expense Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                        <label>
                                            Expense <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <asp:TextBox ID="txexpense" CssClass="form-control" Skin="" runat="server" placeholder="Expense" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txexpense"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Expense is required">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">

                                        <asp:RadioButtonList CssClass="radioButtonList" ID="rbExpense" runat="server" RepeatDirection="horizontal">
                                            <asp:ListItem Value="0" Selected="True">Expense</asp:ListItem>
                                            <asp:ListItem Value="1">Other Income</asp:ListItem>
                                        </asp:RadioButtonList>

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

                        <div class="row">
                        </div>
                        <div class="row">
                        </div>

                    </div>
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Expense" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-primary" runat="server" Text="Reset" ToolTip="Click here to Reset Expense Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-primary" runat="server" Text="Cancel" ToolTip="Click here to Cancel Expense Details" />
                    </div>
                </div>
            </div>

        <%--</telerik:RadAjaxPanel>--%>
    </div>

   <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>

</asp:Content>

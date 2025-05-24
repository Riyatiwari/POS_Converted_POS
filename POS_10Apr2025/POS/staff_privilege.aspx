<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="staff_privilege.aspx.vb" Inherits="staff_privilege" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Staff Privilege
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="staff_privilege_list.aspx">Staff Privilege List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Staff Privilege</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .rtIn {
            width: 100%;
        }

        .rtUnchecked {
            height: 15px !Important;
        }

        td {
            /*border-top: 0px !Important;*/
        }
    </style>
    <br />
    <div class="col-lg-12 ">

        <div class="panel panel-yellow">
            <div class="panel-heading">Staff Privilege</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="knob-container">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Staff"
                                DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                            <div class="col-md-1">
                                Name  <span class="text-danger">*</span>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtRname" CssClass="form-control" runat="server" Width="50%"></asp:TextBox>

                                <br />
                                <label>
                                    Active &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                            </div>

                            <div class="col-md-6">

                                <br />
                                <label>
                                    Select All  &nbsp;
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" /></label>
                            </div>

                            <div class="col-md-3 col-sm-6">
                                <asp:RequiredFieldValidator ID="rfvRname" runat="server" ControlToValidate="txtRname" ErrorMessage="Staff name is required"
                                    ValidationGroup="Staff" Display="none" CssClass="text-danger">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 col-sm-6">
                            </div>
                        </div>
                    </div>
                    <div class="clearfix">
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:DataList ID="DL_rules" runat="server" RepeatColumns="5" RepeatDirection="Vertical" Width="100%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRules" runat="server"></asp:CheckBox>
                                        <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("name")%>' />
                                        <asp:HiddenField ID="hdfunction_type_id" runat="server" Value='<%# Eval("function_type_id")%>' />
                                        <asp:HiddenField ID="hd_name" runat="server" Value='<%# Eval("name")%>' />

                                    </ItemTemplate>
                                </asp:DataList>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-actions text-right pal">
                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="Staff" ToolTip="Click here to Save Rules Details"
                    OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" OnClick="btnCancel_Click" ToolTip="Click here to Cancel Rules Details" />
            </div>
        </div>


    </div>


</asp:Content>


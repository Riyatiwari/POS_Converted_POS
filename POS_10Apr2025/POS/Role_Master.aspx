<%@ Page Title="Role Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Role_Master.aspx.vb" Inherits="Role_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Role Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Role_List.aspx">Role List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Role Master</li>
        </ol>
    </div>
    <script type="text/javascript">
       
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
            <div class="panel panel-yellow">
                <div class="panel-heading">Role Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="knob-container">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="rolemaster"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                <div class="col-md-1">
                                    Name  <span class="text-danger">*</span>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRname" CssClass="form-control" Skin="" runat="server" placeholder="Role Name" Width="50%"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtRname" runat="server" class="form-control span12" placeholder="Role Name"></asp:TextBox>--%>
                                    
                                    <asp:HiddenField ID="hiRole_ID" runat="server" Value="0" />
                                    &nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;
                                    <label>
                                            Active &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                    <asp:RequiredFieldValidator ID="rfvRname" runat="server" ControlToValidate="txtRname" ErrorMessage="Role name is required"
                                        ValidationGroup="rolemaster" Display="none" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 col-sm-6">
                                </div>
                            </div>
                        </div>
                        <div class="clearfix">
                            <br />
                            <div class="row">
                                <div class="col-md-12" style="overflow: hidden;">
                                    <table class="table-responsive" style="width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="width: 50%;">Detail</th>
                                                <th style="width: 5%; text-align: center;">View</th>
                                                <th style="width: 14%; text-align: center;">Add</th>
                                                <th style="width: 5%; text-align: center;">Edit</th>
                                                <th style="width: 14%; text-align: center;">Delete</th>
                                                <th style="width: 20%; text-align: center;"></th>
                                            </tr>
                                        </thead>
                                    </table>
                                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                    <telerik:RadTreeView runat="server" ID="rtlForm" DataFieldID="Form_id" DataFieldParentID="Parent_id"
                                        CheckBoxes="true" CheckChildNodes="true" TriStateCheckBoxes="true" Width="100%" Style="overflow: hidden;">
                                       
                                        <NodeTemplate>
                                            <table class="table-responsive" style="width: 100%">
                                                <tbody>
                                                    <tr>

                                                        <td style="width: 45%">
                                                            <%# Eval("Alias")%>
                                                            <asp:HiddenField ID="hfFormid" runat="server" Value='<%# Eval("Form_Id") %>' />
                                                            <asp:HiddenField ID="hfParentId" runat="server" Value='<%# Eval("Parent_Id") %>' />
                                                        </td>
                                                        <td style="width: 10%; text-align: center;">
                                                            <asp:HiddenField ID="hv" runat="server" Value='<%# Eval("Form_view")%>' />
                                                            <asp:CheckBox ID="View" ToolTip="View" runat="server" AutoPostBack="true" OnCheckedChanged="chkcheck_CheckedChanged" />
                                                        </td>

                                                        <td style="width: 10%; text-align: center;">
                                                            <asp:HiddenField ID="ha" runat="server" Value='<%# Eval("Form_add")%>' />
                                                            <asp:CheckBox ID="Add" ToolTip="Add" runat="server" AutoPostBack="true" OnCheckedChanged="chkcheck_CheckedChanged" />
                                                        </td>
                                                        <td style="width: 10%; text-align: center;">
                                                            <asp:HiddenField ID="he" runat="server" Value='<%# Eval("Form_edit")%>' />
                                                            <asp:CheckBox ID="Edit" ToolTip="Edit" runat="server" AutoPostBack="true" OnCheckedChanged="chkcheck_CheckedChanged" />
                                                        </td>
                                                        <td style="width: 10%; text-align: center;">
                                                            <asp:HiddenField ID="hd" runat="server" Value='<%# Eval("Form_delete")%>' />
                                                            <asp:CheckBox ID="Delete" ToolTip="Delete" runat="server" AutoPostBack="true" OnCheckedChanged="chkcheck_CheckedChanged" />
                                                        </td>
                                                        <td style="width: 20%; text-align: center;">&nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </NodeTemplate>
                                         <DataBindings>
                                            <telerik:RadTreeNodeBinding TextField="Form_name"></telerik:RadTreeNodeBinding>
                                            <telerik:RadTreeNodeBinding Depth="0" Checkable="true" TextField="Form_name" Expanded="true"
                                                CssClass="rootNode"></telerik:RadTreeNodeBinding>
                                        </DataBindings>
                                    </telerik:RadTreeView>
                                  
                                </div>
                               
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSignIn" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="rolemaster" ToolTip="Click here to Save Role Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Role Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Role Details" />
                </div>
            </div>
            <%-- </div>--%>
       <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


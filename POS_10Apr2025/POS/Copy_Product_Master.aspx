<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Copy_Product_Master.aspx.vb" Inherits="Copy_Product_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   Copy Product Master       
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Product_List.aspx">Product List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Copy Product Master</li>
        </ol>
    </div>

    <style type="text/css">
        .rlbItem {
            float: left !important;
        }

        .rlbGroup, .RadListBox {
            width: auto !important;
        }

        /*#RadListBox1 {
            border: 1px solid black;
        }*/

        .RadListBox_Silk .rlbGroup {
            border: none !important;
        }

        .lbl {
            color: #777777;
            font-size: 13px;
            line-height: 1.42857;
            font-weight: normal;
            font-family: "Open Sans",sans-serif;
        }

        .rgHeader.rgCheck > input {
            display: none;
        }

        .RadAutoCompleteBoxPopup {
            width: auto !important;
            overflow-y: scroll;
            max-height: 262px;
        }

        .chkproductlist {
            max-height: 100px;
            height: auto !important;
            height: 100px;
        }
    </style>
  <%--  <script>
        function radioMe(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= rdlocation.ClientID%>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }
    </script>--%>


</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Copy Product Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>
                                            New Product Name <span class="text-danger">*</span>
                                            <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtPName"
                                                ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Name is required">
                                            </asp:RequiredFieldValidator></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtPName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPName"
                                                    ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Name is required">
                                                </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">

                                        <label>
                                            Select Product <span class="text-danger">*</span>
                                            <div class="clearfix"></div>
                                           <%-- <br />--%>
                                          <%--  <div style="overflow-y: scroll; width: 300px; height: 300px; margin-right: 5px">--%>
                                              
                                        </label>
                                        <div class="clearfix"></div>
                                      <telerik:RadComboBox ID="rdlocation" runat="server" Width="100%" Skin="MetroTouch">
                                            </telerik:RadComboBox>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="clearfix">

                    <br />
                    <div class="form-actions text-right pal">
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Product Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Product Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Product Details" />
                    </div>
                </div>
            </div>
    </div>

    </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


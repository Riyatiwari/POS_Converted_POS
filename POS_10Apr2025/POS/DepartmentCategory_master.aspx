<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="DepartmentCategory_master.aspx.vb" Inherits="DepartmentCategory_master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Department Category Master
</asp:Content>

 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Allergy_List.aspx">Department Category List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Department Category Master</li>
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
    </style>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Department Category  Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:HiddenField ID="hdBarcode_size" runat="server" Value='<%#Eval("Barcode_Size_Id")%>' />
                                <div class="col-md-6">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtdepcatname"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Deparment Category Name  is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-12">
                                    <label>Deparment Category Name <span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtdepcatname" CssClass="form-control" Skin="" runat="server"  Width="50%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                 <div class="col-md-12">
                                    <label>Account Code In Xero<span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox  ID="txt_AcCode" CssClass="form-control" Skin="" runat="server"  Width="50%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                               <div class="col-md-12">
                                        <label>
                                          Is Active &nbsp;
                                            <asp:CheckBox ID="ChkIsActive" runat="server"  Checked="true"/></label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                            </div>
                        </div>
                        <div class="row">
                        </div>
                        <div class="row">
                        </div>
                    </div>
                </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Department Category Details" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Department Category Details" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
            
                       <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Department Category Details" OnClick="btnCancel_Click" />
                </div>
            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CourseCategory_Master.aspx.vb" MasterPageFile="~/MasterPage.master" Inherits="CourseCategory_Master" %>


 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Course Category Master
</asp:Content>


 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="CourseCategory_List.aspx">Course Category List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Course Category Master</li>
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
                <div class="panel-heading">Course Category  Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:HiddenField ID="hdBarcode_size" runat="server" Value='<%#Eval("Barcode_Size_Id")%>' />
                             <%--   <div class="col-md-6">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtcoursecatname"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Deparment Category Name  is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>--%>
                                 <div class="col-md-6">

                                        <label>
                                            Course Category
                                        </label>
                                        <div class="clearfix"></div>
                                        
                                      <telerik:RadTextBox ID="txtcatcatname" CssClass="form-control" Skin="" runat="server"  Width="100%"></telerik:RadTextBox>
                                        <%--  <asp:RequiredFieldValidator ID="rfproduct" runat="server" ControlToValidate="radProduct" ErrorMessage="Product is required"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="clearfix"></div>

                                    <br />
                               <div class="col-md-12">
                                        <label>
                                          Is Active &nbsp;
                                            <asp:CheckBox ID="ChkIsActive" runat="server" Checked="true"/></label>
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
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Department Details" OnClick="btnSave_Click"/>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Department Details" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Department Details" OnClick="btnCancel_Click"/>
                    </div>
            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>



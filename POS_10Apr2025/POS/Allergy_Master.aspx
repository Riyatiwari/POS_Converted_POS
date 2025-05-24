<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Allergy_Master.aspx.vb" Inherits="Allergy_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Allergy Master
</asp:Content>

 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Product_List.aspx">Allergy List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Allergy Master</li>
        </ol>
    </div>

    <script>
        function OnClientFileSelectedHandler(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=Image1 .ClientID%>').prop('src', e.target.result)
                            .width(150)
                            .height(50);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }
        function OnTextKeyPress(objEvent) {
            var $th = objEvent.value;
            $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                return '';
            }));
        }

        function OnClientFileSize() {
            const fi = document.getElementById("<%=fileupload.ClientID%>");
              const fsize = fi.files.item(0).size;
              const file = Math.round((fsize / 1024));
              if (file > 5) {
                  alert('Image size is more than 5kb.Compress image file size and Try again.');

                  document.getElementById("<%=fileupload.ClientID%>").value = '';
            }
        }
    </script>
    <script>
        function openWindow(url) {
            var w = window.open(url, '', 'width=1000,height=700,top=80,left=300,toolbar=0,status=0,location=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

        //function checkboxuncheck(id) {
        //    alert("#" +id)
        //    $("#" +id).prop('checked', false);
        //}
    </script>

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

    <%-- <telerik:RadAjaxManagerProxy ID="RadAjaxManager1" runat="server">
        <AjaxSettings>

            <telerik:AjaxSetting AjaxControlID="imgUpload">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imgUpload" />
                </UpdatedControls>
            </telerik:AjaxSetting>


        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>--%>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        --%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Allergy  Master</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="col-md-6">
                        <div class="form-group">
                            <asp:HiddenField ID="hdBarcode_size" runat="server" Value='<%#Eval("Barcode_Size_Id")%>' />
                            <div class="col-md-6">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtname"
                                    ValidationGroup="valid" Display="None" ErrorMessage=" Name  is required" CssClass="text-danger">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-12">
                                <label>Name <span class="text-danger">*</span></label>
                                <div class="clearfix"></div>
                                <asp:TextBox ID="txtname" CssClass="form-control" Skin="" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-12">
                                <label>Description</label><div class="clearfix"></div>
                                <asp:TextBox ID="txtdescription" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Description" Rows="4" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-md-6" style="margin-top: 10px;">
                                <div class="clearfix"></div>
                                <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="150px" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-12" style="margin-top: 10px;">
                                <div class="clearfix"></div>
                                <br />
                                <label style="float: left;">Upload :  &nbsp;</label>
                                <asp:FileUpload ID="fileupload" runat="server" onChange="OnClientFileSize();" />

                                <%--<telerik:RadAsyncUpload ID="fileupload" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandler">
                                        <FileFilters>
                                            <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                        </FileFilters>
                                    </telerik:RadAsyncUpload>
                                    <i style="font-weight: lighter">
                                        <asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label></i>
                                    <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />--%>
                                <div class="clearfix"></div>
                                <asp:HiddenField ID="hdlogo" runat="server" />
                                <div class="col-md-6" style="margin-top: 10px;">
                                    <div class="clearfix"></div>
                                    <asp:Image ID="Image2" runat="server" Visible="false" Height="50px" Width="150px" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <div class="row">
                    </div>
                </div>
            </div>
            <div class="form-actions text-right pal">
                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Allergy Details" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Allergy Details" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
            
                       <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Allergy Details" OnClick="btnCancel_Click" />
            </div>
        </div>

        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


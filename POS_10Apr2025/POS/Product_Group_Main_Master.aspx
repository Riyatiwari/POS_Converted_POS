<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_Group_Main_Master.aspx.vb" Inherits="Product_Group_Main_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Group Category Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Product_Group_Main_List.aspx">Group Category List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Group Category Master</li>
        </ol>
    </div>

    <style>
        .mycheckbox input[type="checkbox"] {
            margin-right: 5px;
        }
    </style>

    <script>

        function OnClientFileSelectedHandler(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=Image1.ClientID%>').prop('src', e.target.result)
                            .width(150)
                            .height(50);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }

        function OnClientFileSize() {
            const fi = document.getElementById("<%=fileupload.ClientID%>");
            const fsize = fi.files.item(0).size;
            const file = Math.round((fsize / 1024));
            if (file > 500) {
                alert('Image size is more than 5kb.Compress image file size and Try again.');

                document.getElementById("<%=fileupload.ClientID%>").value = '';
            }
        }

        function OnTextKeyPress(objEvent) {
            var $th = objEvent.value;
            $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                return '';
            }));
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">

        <div class="panel panel-yellow">
            <div class="panel-heading">Group Category Master</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtCName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCName"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Product group name is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-12" style="display: none;">

                                            <div class="col-md-6">
                                                <label>
                                                    Available for web sales &nbsp;<asp:CheckBox ID="chkONline" runat="server" /></label>

                                            </div>
                                            <div class="col-md-6">


                                                <asp:CheckBoxList Visible="false" ID="chklocation" Style="margin-right: 5px;" runat="server" EnableCheckAllItemsCheckBox="True" class="mycheckbox" CheckBoxes="True" Width="100%"
                                                    Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Description </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtCdescription" CssClass="form-control" Skin="" runat="server" placeholder="Description" Width="80%" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Web Sales Description </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="rdDescSales" CssClass="form-control" Skin="" runat="server" placeholder="Web Sales Description" Width="100%" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-6">
                                    <label>Sorting No </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtsortingno" CssClass="form-control" runat="server" placeholder="Sorting No" Width="80%">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtsortingno"
                                        ErrorMessage="Invalid sorting no" ForeColor="Red" Display="None"
                                        ValidationExpression="^[0-9]*$" ValidationGroup="valid">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-12">

                                            <div class="col-md-6">
                                                <label>
                                                    Click and Collect&nbsp;
                                            <asp:CheckBox ID="chkIsClickcollect" runat="server" /></label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    Deliver&nbsp;
                                            <asp:CheckBox ID="chkIsDeliver" runat="server" /></label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-6">
                                                <label>
                                                    Order at table&nbsp;
                                            <asp:CheckBox ID="chkIsOrderattable" runat="server" /></label>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Active &nbsp;
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                </div>

                                <div class="clearfix"></div>

                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-md-6" style="margin-top: 10px;">
                                        <div class="clearfix"></div>
                                        <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="120px" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12" style="margin-top: 10px;">
                                        <div class="clearfix"></div>
                                        <br />
                                        <label style="float: left;">Upload :  &nbsp;</label>
                                        <asp:FileUpload ID="fileupload" runat="server" onChange="OnClientFileSize();" />
                                        <div class="clearfix"></div>
                                        <asp:HiddenField ID="hdlogo" runat="server" />
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <asp:Image ID="Image2" runat="server" Visible="false" Height="50px" Width="150px" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Product Group Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Product Group Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Product Group Details" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>


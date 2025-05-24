<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Condiment_Master.aspx.vb" Inherits="Condiment_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Condiment Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Condiment_List.aspx">Condiment List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Condiment Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function RemoveSpecialChar(txCondiment) {
            if (txCondiment.value != '' && txCondiment.value.match(/^[\w\s ]+$/) == null) {
                txCondiment.value = txCondiment.value.replace(/[\W\s]/g, '');
            }
        }
    </script>

    <script>
        function OnTextKeyPress(objEvent) {
            var $th = objEvent.value;
            $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {
                //$("#" + objEvent.id).val($th.replace(/^[\w ]+$/, function (str) {
                return '';
            }));
        }
    </script>
    <telerik:RadScriptBlock ID="radSript1" runat="server">
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

            function OnClientFileSize() {
                alert('Image size is more than 5kb.Compress image file size and Try again.');


            }

            function OnTextKeyPress(objEvent) {
                var $th = objEvent.value;
                $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                    return '';
                }));
            }
        </script>
    </telerik:RadScriptBlock>
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Condiment Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                        <label>
                                            Condiment <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <asp:TextBox ID="txCondiment" CssClass="form-control" Skin="" runat="server" placeholder="Condiment" Width="100%" onkeyup="OnTextKeyPress(this)" onpaste="OnTextKeyPress(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txCondiment"
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" ErrorMessage="Condiment is required">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>
                                            Product Group
                                        </label>
                                        <div class="clearfix"></div>
                                        <asp:DropDownList ID="ddl_productGroup" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_productGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>
                                            Product
                                        </label>
                                        <div class="clearfix"></div>
                                        <asp:DropDownList ID="radProduct" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="radProduct_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <asp:CheckBox runat="server" ID="chkProductCondi" />
                                        <label>
                                            Use Product Condiment
                                        </label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Condiment Type <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="radAddSub" runat="server" Width="100%">
                                            <Items>
                                                <asp:ListItem Value="2" Text="SELECT" />
                                                <asp:ListItem Value="1" Text="Add" />
                                                <asp:ListItem Value="0" Text="Subtract" />
                                            </Items>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfproduct" runat="server" ControlToValidate="radAddSub" ErrorMessage="Select condiment type "
                                            ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Quantity By <span class="text-danger">*</span></label>
                                        <asp:RadioButtonList ID="rdb_IsBySize" runat="server" Width="100%" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdb_IsBySize_SelectedIndexChanged1">
                                            <asp:ListItem Value="0" Text="By Quantity" Selected="True" />
                                            <asp:ListItem Value="1" Text="By Selling Size" />
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div id="ByQty" runat="server">
                                        <div class="col-md-6">
                                            <label>Quantity <span class="text-danger">*</span></label>
                                            <asp:TextBox ID="txtquantity" CssClass="form-control" Skin="" runat="server" placeholder="Quantity" Width="100%"></asp:TextBox>

                                        </div>
                                        <div class="clearfix"></div>
                                        <br />

                                        <div class="col-md-6">
                                            <label>Unit <span class="text-danger">*</span></label>
                                            <asp:DropDownList ID="ddlunit" runat="server" Width="100%" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>

                                    <div id="BySize" runat="server" visible="false">
                                        <div class="col-md-6">
                                            <label>Selling Size <span class="text-danger">*</span></label>

                                            <asp:RadioButtonList ID="rdbList" runat="server" Width="100%" RepeatColumns="2" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>

                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6" style="display: none">
                                        <label>Choices </label>
                                        <asp:DropDownList ID="ddlchoices" runat="server" Width="100%" AutoPostBack="true">
                                            <Items>
                                                <asp:ListItem Value="0" Text="SELECT" />
                                                <asp:ListItem Value="1" Text="Choice 1" />
                                                <asp:ListItem Value="2" Text="Choice 2" />
                                                <asp:ListItem Value="3" Text="Choice 3" />
                                            </Items>
                                        </asp:DropDownList>
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>

                                    <div class="col-md-6">
                                        <label>
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                                            Active
                                        </label>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px;">
                                        <div class="clearfix"></div>
                                        <br />
                                        <label>
                                            Upload :  &nbsp;</label>

                                        <telerik:RadAsyncUpload ID="fileupload" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandler"
                                            MaxFileSize="5000" OnClientValidationFailed="OnClientFileSize">
                                            <FileFilters>
                                                <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                            </FileFilters>
                                        </telerik:RadAsyncUpload>
                                        <i style="font-weight: lighter">
                                            <asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text="File size should not exceed 5Kb"></asp:Label>
                                        </i>
                                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />
                                        <div class="clearfix"></div>
                                        <asp:HiddenField ID="hdlogo" runat="server" />


                                    </div>
                                    <div class="col-md-12" style="margin-top: 10px;">
                                        <div class="clearfix"></div>
                                        <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />
                                        <telerik:RadGrid runat="server" ID="RadGrid1"
                                            AutoGenerateColumns="False" Width="97%">
                                            <MasterTableView DataKeyNames="Condiment_Image_id">
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Images">
                                                        <ItemTemplate>
                                                            <telerik:RadBinaryImage runat="server" ID="RadBinaryImage2" DataValue='<%#Eval("condiment_image")%>'
                                                                AutoAdjustImageControlSize="false" Height="80px" Width="80px" ToolTip='<%#Eval("condiment_image", "Photo of {0}")%>'
                                                                AlternateText='<%#Eval("condiment_image", "Photo of {0}")%>'></telerik:RadBinaryImage>
                                                        </ItemTemplate>

                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdimage_Id" runat="server" Value='<%#Eval("Condiment_Image_id")%>' />
                                                            <asp:LinkButton ID="IbADelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                CommandName="DeleteVal" CommandArgument='<%#Eval("Condiment_Image_id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>

                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
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
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Condiment Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Condiment Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Condiment Details" />
                </div>
            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>

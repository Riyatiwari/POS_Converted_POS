<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Demo_Product_Group_Main_Master.aspx.vb" Inherits="Demo_Product_Group_Main_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Product Group Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Product_Group_List.aspx">Product Group List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Product Group Master</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            function OnTextKeyPress(objEvent) {
                var $th = objEvent.value;
                $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                    return '';
                }));
            }
        </script>
    </telerik:RadScriptBlock>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%
        }

        .Grid th {
            background-color: #4374A6;
            color: White;
            font-size: 10pt;
            line-height: 200%
        }

        .imgclass {
            height: 100px;
            width: 150px;
        }
    </style>
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Product Group Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtCName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="80%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Product group name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <%--   <div class="col-md-6">
                                    </div>--%>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Description </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtCdescription" CssClass="form-control" Skin="" runat="server" placeholder="Description" Width="80%" TextMode="MultiLine" Rows="4"></telerik:RadTextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Sorting No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtsortingno" CssClass="form-control" Skin="" runat="server" placeholder="Sorting No" Width="80%"></telerik:RadTextBox>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="txtsortingno"
                                            ErrorMessage="Invalid sorting no" ForeColor="Red" Display="None"
                                            ValidationExpression="^[0-9]*$" ValidationGroup="valid">
                                        </asp:RegularExpressionValidator>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Active &nbsp;
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            Available for web sales &nbsp;
                                            <asp:CheckBox ID="chkONline" runat="server" /></label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Upload :  &nbsp;</label>
                                          <%--<asp:fileupload id="FileUpload1" runat="server" xmlns:asp="#unknown" />--%>
                                          
                                        <telerik:RadAsyncUpload ID="fileupload" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandler">
                                            <FileFilters>
                                                <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                            </FileFilters>
                                        </telerik:RadAsyncUpload>
                                        <i style="font-weight: lighter">
                                            <asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label></i>
                                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />
                                        <div class="clearfix"></div>
                                        <asp:HiddenField ID="hdlogo" runat="server" />
                                    </div>
                                     <div class="col-md-6">
                                         <img id="Image1" runat="server" alt="" class="imgclass"  />
                                       
                                           <%--  <asp:Image ID="Image1" runat="server" class="imgclass" />--%>
                                         
                                         </div>
                                </div>


                            </div>
                            <div class="col-md-12" style="margin-top: 10px;">
                                <div class="clearfix"></div>
                                <br />


                            </div>
                            <div class="col-md-12" style="margin-top: 10px;">
                                <div class="clearfix"></div>
                                <%--<asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />--%>
                                

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                            OnRowDataBound="OnRowDataBound">

                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="Location" HeaderText="Location" />
                                                <%--<asp:TemplateField visible="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="Hdsize_Detail_id" runat="server" Value='<%#Bind("size_Detail_id")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Click and Collect" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkCollect" runat="server" />
                                                        <asp:HiddenField ID="HdClickAndCollect" runat="server" Value='<%#Bind("click_and_collect")%>' />
                                                        <asp:HiddenField ID="hflocation_id" runat="server" Value='<%#Bind("location_id")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deliver" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDeliver" runat="server" />
                                                        <asp:HiddenField ID="HdChkDeliver" runat="server" Value='<%#Bind("deliver")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order At Table" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkTable" runat="server" />
                                                        <asp:HiddenField ID="HdChkOrderAtTable" runat="server" Value='<%#Bind("Order_at_table")%>' />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

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
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Product Group Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Product Group Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Product Group Details" />
                    </div>
                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>



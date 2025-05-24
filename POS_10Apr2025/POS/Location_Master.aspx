<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Location_Master.aspx.vb" Inherits="Location_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Location Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Location_List.aspx">Location List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Location Master</li>
        </ol>
    </div>
    <script>
        function Expand() {


            if (document.getElementById("divPref").style.display == 'none') {
                document.getElementById("divPref").style.display = 'block'

            }
            else {
                document.getElementById("divPref").style.display = 'none'

            }
            return false;

        }
        function OnClientFileSize() {
            alert('Image size is more than 10kb.Compress image file size and Try again.');
        }

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

        function OnClientFileSelectedHandlerGraphic(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=ImageGraphic.ClientID%>').prop('src', e.target.result)
                            .width(150)
                            .height(50);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }

        function OnClientFileSelectedHandlerTable(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=ImageTable.ClientID%>').prop('src', e.target.result)
                            .width(150)
                            .height(50);
                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }


        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }

        function OnClientFileSelectedHandlerclick(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=Imageclickcollect.ClientID%>').prop('src', e.target.result)
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


        function OnClientFileSelectedHandlerPOsLogo(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=imagePOSLOGO.ClientID%>').prop('src', e.target.result)
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

        function OnClientFileSelectedHandlerdeliver(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=Imagedeliver.ClientID%>').prop('src', e.target.result)
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

        function OnClientFileSelectedHandlerorder(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=Imageoderattable.ClientID%>').prop('src', e.target.result)
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

        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

    </script>

    <script type="text/javascript">
        function toggleCheckboxes() {
            var txtsvccharg = document.getElementById('<%= txtsvccharg.ClientID %>');
        var checkboxes = document.querySelectorAll('#<%= chkservice.ClientID %> input[type="checkbox"]');

            if (parseFloat(txtsvccharg.value.trim()) > 0.00) {
                checkboxes.forEach(function (checkbox) {
                    checkbox.style.display = "inline-block";
                });
            } else {
                checkboxes.forEach(function (checkbox) {
                    checkbox.style.display = "none";
                });
            }
        }

        // Call toggleCheckboxes when the page loads to set the initial visibility
        window.onload = toggleCheckboxes;
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Location Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <asp:HiddenField runat="server" ID="hdnOldName" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtName"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Location name is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-6">
                                    <label>Code </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtlCode" CssClass="form-control" Skin="" runat="server" placeholder="Location Code" Width="100%"></telerik:RadTextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlCode"
                                        ValidationGroup="valid" Display="Dynamic" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Address</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtAddress" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Address" Rows="4" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Country </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radCountry" runat="server" AutoPostBack="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-6">
                                    <label>State </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radState" runat="server" AutoPostBack="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>City</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radCity" runat="server" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Venue</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radVenue" runat="server" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Till Auto Log Off &nbsp;
                                        <asp:CheckBox ID="chkTillAutoLogOff" runat="server" Checked="false" /></label>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Active &nbsp;
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Min Amount for Delivery  &nbsp;
                                          <telerik:RadTextBox ID="txtMinAmount" CssClass="form-control" Skin="" runat="server" placeholder="Minimum Amount" Width="100%"></telerik:RadTextBox></label>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Delivery Available &nbsp;
                                          <asp:CheckBox ID="chkisDelivery" runat="server" /></label>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Skip Payment &nbsp;
                                    <asp:CheckBox ID="chkSkipPay" runat="server" Checked="true" /></label>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Email Receipt &nbsp;
                                    <asp:CheckBox ID="chkReceipt" runat="server" Checked="false" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Click and Collect&nbsp;
                                            <asp:CheckBox ID="chkIsClickcollect" runat="server" /></label>
                                </div>

                                <div class="col-md-6">
                                    <label>
                                        Order table&nbsp;
                                            <asp:CheckBox ID="chkIsOrdertable" runat="server" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <%--<br />
                                 <div class="col-md-6">
                                    <label>
                                        Kiosk&nbsp;
                                            <asp:CheckBox ID="iskiosk" runat="server" /></label>
                                </div>
                                <div class="clearfix"></div>--%>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Delivery Charges&nbsp;
                                              <telerik:RadTextBox ID="txtDCharges" CssClass="form-control" Skin="" runat="server" placeholder="Delivery Charges" Width="100%"></telerik:RadTextBox></label>
                               
                                    </div>
                                  
                                <div class="col-md-6">
                                   <label>
                                        Service Charges&nbsp;
                                        <telerik:RadTextBox ID="txtsvccharg" OnTextChanged="txtsvccharg_TextChanged"  CssClass="form-control"  Skin="" 
                                                            runat="server"  placeholder="Service Charges" Width="100%" AutoPostBack="true" OnKeyUp="toggleCheckboxes()"></telerik:RadTextBox>
                                    </label>
                                    <asp:Label ID="chkservice" runat="server">
                                        <asp:CheckBox ID="chkclick" runat="server" Checked="false" Text="Click & Collect" />&nbsp&nbsp&nbsp
                                        <asp:CheckBox ID="chkkiosk" runat="server" Checked="false" Text="Kiosk" /><br />
                                        <asp:CheckBox ID="chkorder" runat="server" Checked="false" Text="Order at table" />&nbsp&nbsp&nbsp
                                        <asp:CheckBox ID="chkdelivery" runat="server" Checked="false" Text="Delivery" />
                                    </asp:Label>
                                
                                
                                </div>
                                
                                <br />
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Email Receipt from APK &nbsp;
                                    <asp:CheckBox ID="chkEmailRec_APK" runat="server" Checked="false" /></label>
                                </div>
 
                                <br />
                                <br />
                              
                                 <div class="col-md-6">
                                    <label>
                                        Is GetCovers &nbsp;
                                    <asp:CheckBox ID="isGetCovers" runat="server" Checked="false" /></label>
                                </div>

                            
                                <br />
                                <br />

                               
                                <div class="col-md-6">
                                    <label>
                                        Theme 
                                        &nbsp;
                                        <telerik:RadComboBox ID="Radtheme" runat="server" ExpandDirection="Down" Width="100%" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Select" Value="0" />
                                                <telerik:RadComboBoxItem Text="With Images" Value="1" />
                                                <telerik:RadComboBoxItem Text="With No images" Value="2" />
                                                <telerik:RadComboBoxItem Text="Menu & Images" Value="3" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </label>
                                </div>
                                <div class="col-md-6">
                                     <label>
                                       Tip As
                                        &nbsp;
                                    <telerik:RadComboBox ID="tipas" runat="server" ExpandDirection="Down" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="tipas_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Select" Value="" />
                                                <telerik:RadComboBoxItem Text="Tip" Value="Tip" />
                                                <telerik:RadComboBoxItem Text="Cashback" Value="Cashback"/>
                                               
                                            </Items>
                                        </telerik:RadComboBox>
                                        <br />
                                        <br />
                                         
                                       <asp:Label ID="lblcashback" runat="server"  Visible="false">
                                       
                                    <asp:CheckBox ID="cashback" runat="server"  Checked="false" Text="Cashback Only On Tables"  Visible="false"/> 


                                            
                                       </asp:Label>
                                 </div>
                            
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-6" style="margin-top: 10px;">
                                                <div class="clearfix"></div>
                                                <asp:Image ID="Imageclickcollect" runat="server" Visible="false" Height="50px" Width="150px" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <div class="clearfix"></div>
                                                <br />
                                                <label>
                                                    Click And Collect Image  Upload :  &nbsp;</label>

                                                <telerik:RadAsyncUpload ID="RadAsyncUpload1" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandlerclick"
                                                    MaxFileSize="10000" OnClientValidationFailed="OnClientFileSize">
                                                    <FileFilters>
                                                        <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                    </FileFilters>
                                                </telerik:RadAsyncUpload>
                                                <i style="font-weight: lighter">
                                                    <asp:Label ID="Label1" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                    <asp:Label ID="Label12" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                                </i>
                                                <telerik:RadBinaryImage ID="RadBinaryclickandcollect" runat="server" Visible="false" />
                                                <div class="clearfix"></div>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <div class="col-md-6" style="margin-top: 10px;">
                                                    <div class="clearfix"></div>
                                                    <asp:Image ID="Image4" runat="server" Visible="false" Height="50px" Width="150px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-6" style="margin-top: 10px;">
                                                <div class="clearfix"></div>
                                                <asp:Image ID="Imagedeliver" runat="server" Visible="false" Height="50px" Width="150px" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <div class="clearfix"></div>
                                                <br />
                                                <label>
                                                    Delivery Image Upload :  &nbsp;</label>

                                                <telerik:RadAsyncUpload ID="RadAsyncUpload2" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandlerdeliver"
                                                    MaxFileSize="10000" OnClientValidationFailed="OnClientFileSize">
                                                    <FileFilters>
                                                        <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                    </FileFilters>
                                                </telerik:RadAsyncUpload>
                                                <i style="font-weight: lighter">
                                                    <asp:Label ID="Label3" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                    <asp:Label ID="Label11" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                                </i>
                                                <telerik:RadBinaryImage ID="RadBinarydelivery" runat="server" Visible="false" />
                                                <div class="clearfix"></div>
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                <div class="col-md-6" style="margin-top: 10px;">
                                                    <div class="clearfix"></div>
                                                    <asp:Image ID="Image6" runat="server" Visible="false" Height="50px" Width="150px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-6" style="margin-top: 10px;">
                                                <div class="clearfix"></div>
                                                <asp:Image ID="Imageoderattable" runat="server" Visible="false" Height="50px" Width="150px" />
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-12" style="margin-top: 10px;">
                                                <div class="clearfix"></div>
                                                <br />
                                                <label>
                                                    Order At Table Image Upload :  &nbsp;</label>

                                                <telerik:RadAsyncUpload ID="RadAsyncUpload3" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandlerorder"
                                                    MaxFileSize="10000" OnClientValidationFailed="OnClientFileSize">
                                                    <FileFilters>
                                                        <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                    </FileFilters>
                                                </telerik:RadAsyncUpload>
                                                <i style="font-weight: lighter">
                                                    <asp:Label ID="Label4" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                    <asp:Label ID="Label10" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                                </i>
                                                <telerik:RadBinaryImage ID="RadBinaryorderattable" runat="server" Visible="false" />
                                                <div class="clearfix"></div>
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                                <div class="col-md-6" style="margin-top: 10px;">
                                                    <div class="clearfix"></div>
                                                    <asp:Image ID="Image8" runat="server" Visible="false" Height="50px" Width="150px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-6">
                                    <a id="aCLick" runat="server" href="" style="cursor: pointer" title="Link" target="_blank">Click & Collect</a><div class="clearfix"></div>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <a id="aDelivery" runat="server" href="" style="cursor: pointer" title="Link" target="_blank">Delivery </a>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <a id="aOrder" runat="server" href="" style="cursor: pointer" title="Link" target="_blank">Order At Table</a><div class="clearfix"></div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <a id="akiosk" runat="server" href="" style="cursor: pointer" title="Link" target="_blank">Kiosk</a><div class="clearfix"></div>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Future Booking Days&nbsp;
                                      <telerik:RadTextBox ID="txtFutureBookingDays" CssClass="form-control" Skin="" runat="server" placeholder="Future Booking Days" Width="100%" onkeypress="return allowOnlyNumber(event);"></telerik:RadTextBox></label>
                                </div>
                                 <div class="clearfix"></div>
                                <br />

                                
                                
                              
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <asp:Button ID="btnPref" class="btn" Width="90%" runat="server" Text="Style Settings" OnClientClick="return Expand()" ToolTip="Button/Color Preferences" />
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-12" id="divPref" style="display: none; padding: 5px;">
                                    <label>
                                        Group Category&nbsp;</label>
                                    <div class="col-md-12" style="border: 1px solid #efefef; padding: 3px;">
                                        <div class="col-md-6" style="float: left">
                                            <label>Text Colour </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadColorPicker ID="radtxt_clr_GC" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-6" style="float: left">
                                            <label>Background Color </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadColorPicker ID="radQty_BG_clr_GC" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-6">
                                            <label>Button Style </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlBtnStyle_GC" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="Flate" Text="Flat" />
                                                    <telerik:RadComboBoxItem Value="Shadow" Text="Shadow" />
                                                    <telerik:RadComboBoxItem Value="Full Shadow" Text="Full Shadow" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                      
                                        <div class="col-md-6">
                                            <label>Image Type </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlImgType_GC" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="None" Text="None" />
                                                    <telerik:RadComboBoxItem Value="Round Image" Text="Round Image" />
                                                    <telerik:RadComboBoxItem Value="Square Image" Text="Square Image" />
                                                </Items>
                                            </telerik:RadComboBox>

                                        </div>
                                        <div class="col-md-6">
                                            <label>Font Size</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList runat="server" ID="ddl_QtyFontSize_GC" Width="100%">
                                                <asp:ListItem Text="Regular" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Small" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Medium" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Large" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Image Option </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddl_ImageOption_GC" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="Left" Text="Left" />
                                                    <telerik:RadComboBoxItem Value="Right" Text="Right" />
                                                    <telerik:RadComboBoxItem Value="Top" Text="Top" />
                                                    <telerik:RadComboBoxItem Value="Bottom" Text="Bottom" />
                                                    <telerik:RadComboBoxItem Value="Background" Text="Background" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>



                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <label>
                                        Category&nbsp;</label>
                                    <div class="col-md-12" style="border: 1px solid #efefef; padding: 3px;">
                                        <div class="col-md-6">
                                            <label>Background Color </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadColorPicker ID="radQty_BG_clr_C" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-6">
                                            <label>Text Colour </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadColorPicker ID="radtxt_clr_C" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-6">
                                            <label>Button Style </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlBtnStyle_C" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="Flate" Text="Flat" />
                                                    <telerik:RadComboBoxItem Value="Shadow" Text="Shadow" />
                                                    <telerik:RadComboBoxItem Value="Full Shadow" Text="Full Shadow" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="col-md-6">
                                            <label>Image Type </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlImgType_C" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="None" Text="None" />
                                                    <telerik:RadComboBoxItem Value="Round Image" Text="Round Image" />
                                                    <telerik:RadComboBoxItem Value="Square Image" Text="Square Image" />
                                                </Items>
                                            </telerik:RadComboBox>

                                        </div>
                                        <div class="col-md-6">
                                            <label>Font Size</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList runat="server" ID="ddl_QtyFontSize_C" Width="100%">
                                                <asp:ListItem Text="Regular" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Small" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Medium" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Large" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Image Option </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddl_ImageOption_C" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="Left" Text="Left" />
                                                    <telerik:RadComboBoxItem Value="Right" Text="Right" />
                                                    <telerik:RadComboBoxItem Value="Top" Text="Top" />
                                                    <telerik:RadComboBoxItem Value="Bottom" Text="Bottom" />
                                                    <telerik:RadComboBoxItem Value="Background" Text="Background" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <label>
                                        Product&nbsp;</label>
                                    <div class="col-md-12" style="border: 1px solid #efefef; padding: 3px;">
                                        <div class="col-md-6">
                                            <label>Background Color </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadColorPicker ID="radQty_BG_clr_P" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-6">
                                            <label>Text Colour </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadColorPicker ID="radtxt_clr_P" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                        </div>
                                        <div class="col-md-6">
                                            <label>Button Style </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlBtnStyle_P" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="Flate" Text="Flat" />
                                                    <telerik:RadComboBoxItem Value="Shadow" Text="Shadow" />
                                                    <telerik:RadComboBoxItem Value="Full Shadow" Text="Full Shadow" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="col-md-6">
                                            <label>Image Type </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddlImgType_P" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="None" Text="None" />
                                                    <telerik:RadComboBoxItem Value="Round Image" Text="Round Image" />
                                                    <telerik:RadComboBoxItem Value="Square Image" Text="Square Image" />
                                                </Items>
                                            </telerik:RadComboBox>

                                        </div>
                                        <div class="col-md-6">
                                            <label>Font Size</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList runat="server" ID="ddl_QtyFontSize_P" Width="100%">
                                                <asp:ListItem Text="Regular" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Small" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Medium" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Large" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-6">
                                            <label>Image Option </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddl_ImageOption_P" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="Left" Text="Left" />
                                                    <telerik:RadComboBoxItem Value="Right" Text="Right" />
                                                    <telerik:RadComboBoxItem Value="Top" Text="Top" />
                                                    <telerik:RadComboBoxItem Value="Bottom" Text="Bottom" />
                                                    <telerik:RadComboBoxItem Value="Background" Text="Background" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                     <label>
                                        Till &nbsp;</label>
                                    <div class="col-md-12" style="border: 1px solid #efefef; padding: 3px;">
                                        <div class="col-md-6">
                                            <div class="col-md-12" style="float: left">
                                                <label>Login Screen BG Color  </label>
                                                <div class="clearfix"></div>
                                                <telerik:RadColorPicker ID="login_bg_clr" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-12" style="float: left">
                                                <label>Login Screen Login button</label>
                                                <div class="clearfix"></div>
                                                <telerik:RadColorPicker ID="login_sr_clr" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />

                                        <div class="col-md-6">
                                            <div class="clearfix"></div>
                                            <label>Display Phone No.</label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox MinValue="0" ID="txtcontactTill" Skin="" CssClass="form-control"
                                                runat="server" Width="100%" placeholder="Display Phone No.">
                                            </telerik:RadTextBox>
                                        </div>


                                        <div class="col-md-6" id="div1" runat="server">
                                            <label>Display Url</label><div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtUrl" CssClass="form-control" Skin="" runat="server" placeholder="Display Url" Width="100%"></telerik:RadTextBox>


                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <dpos   iv class="col-md-6" style="margin-top: 10px;">
                                                        <div class="clearfix"></div>
                                                        <asp:Image ID="imagePOSLOGO" runat="server" Visible="false" Height="50px" Width="150px" />

                                                        </dpos>
                                                    <br />&nbsp&nbsp&nbsp&nbsp
                                                         <div class="col-md-5" style="margin-top: 10px;">
                                                    &nbsp &nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                               <asp:Button RenderMode="Lightweight" ID="btnRemoveLogo" runat="server" Visible="false" Text="x"  OnClick="btnRemoveLogo_Click" />


                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <br />
                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                        <div class="clearfix"></div>
                                                        <br />
                                                        <label>
                                                            POS logo :  &nbsp;</label>

                                                        <telerik:RadAsyncUpload ID="RadAsyncUpload_logo" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandlerPOsLogo"
                                                           OnClientValidationFailed="OnClientFileSize">
                                                            <FileFilters>
                                                                <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                            </FileFilters>
                                                        </telerik:RadAsyncUpload>
                                                        <i style="font-weight: lighter">
                                                            <asp:Label ID="Label13" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                            <asp:Label ID="Label14" runat="server" Text="(Size-2083(W)*2083(H))"></asp:Label>
                                                        </i>
                                                        <telerik:RadBinaryImage ID="RadBinaryPOSLogo" runat="server" Visible="false" />
                                                        <div class="clearfix"></div>
                                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                                        <div class="col-md-6" style="margin-top: 10px;">
                                                            <div class="clearfix"></div>
                                                            <asp:Image ID="Image3" runat="server" Visible="false" Height="50px" Width="150px" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="clearfix"></div>
                                        <br />

                                    </div>

                                </div>
                                  <div class="clearfix"></div>
                                <br />
                                
                        

                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label>
                                        Online Payment
                                        &nbsp;
                                        <telerik:RadComboBox ID="ddljudo" runat="server" ExpandDirection="Down" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddljudo_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Select" Value="0" />
                                                <telerik:RadComboBoxItem Text="Judo" Value="1" />
                                                <telerik:RadComboBoxItem Text="Cashflow" Value="2" />
                                                <telerik:RadComboBoxItem Text="CustomPay" Value="3" />
                                                <telerik:RadComboBoxItem Text="CardStream" Value="4" />
                                                <telerik:RadComboBoxItem Text="Positive Payment" Value="5" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </label>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-12" id="divJudoPayid" runat="server">
                                    <label>JudoPay Id</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtJudoId" CssClass="form-control" Skin="" runat="server" placeholder="JudoPay Id" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divJudoTokenid" runat="server">
                                    <label>JudoPay Token</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtJudoToken" CssClass="form-control" Skin="" runat="server" placeholder="JudoPay Token" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divJudoSecretid" runat="server">
                                    <label>JudoPay Secret</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtjudosecret" CssClass="form-control" Skin="" runat="server" placeholder="JudoPay Secret" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCashflowId" runat="server">
                                    <label>Id</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCashflowId" CssClass="form-control" Skin="" runat="server" placeholder="Cashflow Id" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCashflowUrl" runat="server">
                                    <label>Url</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCashflowUrl" CssClass="form-control" Skin="" runat="server" placeholder="Cashflow Url" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCashflowAPIKey" runat="server">
                                    <label>API Key</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCashflowAPIKey" CssClass="form-control" Skin="" runat="server" placeholder="Cashflow API Key" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCustomPayId" runat="server">
                                    <label>CustomPay Id</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCustomPayId" CssClass="form-control" Skin="" runat="server" placeholder="CustomPay Id" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCustomPayToken" runat="server">
                                    <label>CustomPay Token</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCustomPayToken" CssClass="form-control" Skin="" runat="server" placeholder="CustomPay Token" Width="100%"></telerik:RadTextBox>

                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCustomPaySecret" runat="server">
                                    <label>CustomPay Secret</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCustomPaySecret" CssClass="form-control" Skin="" runat="server" placeholder="CustomPay Secret" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCustomPayBase64" runat="server">
                                    <label>CustomPay Base64</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCustomPayBase64" CssClass="form-control" Skin="" runat="server" placeholder="CustomPay Base64" Width="100%"></telerik:RadTextBox>

                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12" id="divCustomPayURL" runat="server">
                                    <label>CustomPay URL</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCustomPayURL" CssClass="form-control" Skin="" runat="server" placeholder="CustomPay URL" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <%----------------------- added on 24/02/2022-----------------------%>
                                <div class="col-md-6">
                                    <label>Client Id</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txt_clientid" CssClass="form-control" Skin="" runat="server" placeholder="Client Id" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Client Secret</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txt_clientsecret" CssClass="form-control" Skin="" runat="server" placeholder="Client Secret" Width="100%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Redirect URI</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txt_redirect_uri" CssClass="form-control" Skin="" runat="server" placeholder="Redirect URI" Width="100%"></asp:TextBox>
                                </div>
                                <%----------------------- added on 24/02/2022-----------------------%>

                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-6">
                                    <label>
                                        Web sales Check Schedule &nbsp;
                                    <asp:CheckBox ID="chckwebsales" runat="server" Checked="false" /></label>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Schedule Required &nbsp;
                                    <asp:CheckBox ID="chkScheRequired" runat="server" Checked="false" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Schedule For Click And Collect &nbsp;
                                    <asp:CheckBox ID="chkScnc" runat="server" Checked="false" /></label>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Hourly Slot &nbsp;
                                    <asp:CheckBox ID="chkHourlySlot" runat="server" Checked="false" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Start Time &nbsp;
                                        <asp:TextBox ID="txtsarttime" runat="server" TextMode="Time" TimeFormat="HH:mm:ss"></asp:TextBox>
                                    </label>
                                </div>

                                <div class="col-md-6">
                                    <label>
                                        End Time &nbsp;
                                        <asp:TextBox ID="textendtime" runat="server" TextMode="Time" TimeFormat="HH:mm:ss"></asp:TextBox>
                                    </label>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-6">

                                    <label>Email </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtEmail" CssClass="form-control" Skin="" runat="server" placeholder="Email" Width="100%"></telerik:RadTextBox>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="register" Display="none"
                                        CssClass="text-danger" ErrorMessage="Email is invalid" ValidationExpression="([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})"></asp:RegularExpressionValidator>
                                </div>

                                <div class="col-md-6">
                                    <div class="clearfix"></div>
                                    <label>Contact </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox MinValue="0" ID="txtContact" Skin="" CssClass="form-control"
                                        runat="server" Width="100%" placeholder="Contact">
                                    </telerik:RadTextBox>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div id="div_PGtway" runat="server" visible="false">
                                    <div class="col-md-6">
                                        <label>Gateway MerchantID </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox MinValue="0" ID="txtgtway_MerchantID" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Gateway MerchantID">
                                        </telerik:RadTextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Store Name </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="rad_StoreName" runat="server" ExpandDirection="Down" Width="100%"
                                            AutoPostBack="true" OnSelectedIndexChanged="rad_StoreName_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>Location </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="rad_StoreLocation" runat="server" ExpandDirection="Down" Width="100%">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>


                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-12" style="margin-top: 3%;">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label>Terms & Condition</label><div class="clearfix"></div>
                                                    <telerik:RadEditor ID="txtReciptHeader" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="200px" Style="overflow: auto;">
                                                    </telerik:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    <label>Delivery</label><div class="clearfix"></div>
                                                    <telerik:RadEditor ID="txtmessage_delivery" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="200px" Style="overflow: auto;">
                                                    </telerik:RadEditor>
                                                </div>
                                                <div class="col-md-12">
                                                    <label>Ordet Table</label><div class="clearfix"></div>
                                                    <telerik:RadEditor ID="txtmessage_order_table" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="200px" Style="overflow: auto;">
                                                    </telerik:RadEditor>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 3%;">
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <label>Background Color</label>
                                                            <telerik:RadColorPicker ID="radBackcolorpicker_m" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>Font Color </label>
                                                            <telerik:RadColorPicker ID="radForcolorpicker_m" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            <label>Body Color </label>
                                                            <telerik:RadColorPicker ID="radbodycolorpicker_m" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />
                                            <telerik:RadGrid runat="server" ID="RadGrid1"
                                                AutoGenerateColumns="False" Width="97%">

                                                <MasterTableView DataKeyNames="location_id">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <telerik:RadBinaryImage runat="server" ID="RadBinaryImage2" DataValue='<%#Eval("L_image")%>'
                                                                    AutoAdjustImageControlSize="false" Height="80px" Width="80px" ToolTip='<%#Eval("L_image", "Photo of {0}")%>'
                                                                    AlternateText='<%#Eval("L_image", "Photo of {0}")%>'></telerik:RadBinaryImage>
                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdimage_Id" runat="server" Value='<%#Eval("location_id")%>' />
                                                                <asp:LinkButton ID="IbADelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("location_id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>

                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <br />
                                            <label>Upload :  &nbsp;</label>

                                            <telerik:RadAsyncUpload ID="fileupload" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandler"
                                                MaxFileSize="10000" OnClientValidationFailed="OnClientFileSize">
                                                <FileFilters>
                                                    <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                </FileFilters>
                                            </telerik:RadAsyncUpload>
                                            <i style="font-weight: lighter">
                                                <asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                <asp:Label ID="Label9" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                            </i>
                                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />
                                            <div class="clearfix"></div>
                                            <asp:HiddenField ID="hdlogo" runat="server" />
                                        </div>

                                        <div class="clearfix"></div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <asp:Image ID="ImageGraphic" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />
                                            <telerik:RadGrid runat="server" ID="RadGridGraphic"
                                                AutoGenerateColumns="False" Width="97%">

                                                <MasterTableView DataKeyNames="location_id">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <telerik:RadBinaryImage runat="server" ID="RadBinaryImage2" DataValue='<%#Eval("GraphicViewBackground")%>'
                                                                    AutoAdjustImageControlSize="false" Height="80px" Width="80px" ToolTip='<%#Eval("GraphicViewBackground", "Photo of {0}")%>'
                                                                    AlternateText='<%#Eval("GraphicViewBackground", "Photo of {0}")%>'></telerik:RadBinaryImage>
                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdimage_Id" runat="server" Value='<%#Eval("location_id")%>' />
                                                                <asp:LinkButton ID="IbADelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("location_id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>

                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <br />
                                            <label>Upload Graphic View Background :  &nbsp;</label>

                                            <telerik:RadAsyncUpload ID="fileupload2" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandlerGraphic"
                                                MaxFileSize="10000" OnClientValidationFailed="OnClientFileSize">
                                                <FileFilters>
                                                    <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                </FileFilters>
                                            </telerik:RadAsyncUpload>
                                            <i style="font-weight: lighter">
                                                <asp:Label ID="Label5" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                <asp:Label ID="Label8" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                            </i>
                                            <telerik:RadBinaryImage ID="RadBinaryImageGraphic" runat="server" Visible="false" />
                                            <div class="clearfix"></div>
                                            <asp:HiddenField ID="hdngraphic" runat="server" />
                                        </div>

                                        <div class="clearfix"></div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <asp:Image ID="ImageTable" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />
                                            <telerik:RadGrid runat="server" ID="RadGridTable"
                                                AutoGenerateColumns="False" Width="97%">

                                                <MasterTableView DataKeyNames="location_id">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <telerik:RadBinaryImage runat="server" ID="RadBinaryImage2" DataValue='<%#Eval("GraphicViewTable")%>'
                                                                    AutoAdjustImageControlSize="false" Height="80px" Width="80px" ToolTip='<%#Eval("GraphicViewTable", "Photo of {0}")%>'
                                                                    AlternateText='<%#Eval("GraphicViewTable", "Photo of {0}")%>'></telerik:RadBinaryImage>
                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdimage_Id" runat="server" Value='<%#Eval("location_id")%>' />
                                                                <asp:LinkButton ID="IbADelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("location_id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');"><i class="fa fa-minus-square-o fa-lg"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>

                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <br />
                                            <label>
                                                Upload Graphic View Table :  &nbsp;</label>
                                            <telerik:RadAsyncUpload ID="fileupload3" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandlerTable"
                                                MaxFileSize="10000" OnClientValidationFailed="OnClientFileSize">
                                                <FileFilters>
                                                    <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                </FileFilters>
                                            </telerik:RadAsyncUpload>
                                            <i style="font-weight: lighter">
                                                <asp:Label ID="Label6" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                <asp:Label ID="Label7" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                            </i>
                                            <telerik:RadBinaryImage ID="RadBinaryImageTable" runat="server" Visible="false" />
                                            <div class="clearfix"></div>
                                            <asp:HiddenField ID="hdnTable" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-actions text-right pal">
                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Location Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Location Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Location Details" />
            </div>
            
        </telerik:RadAjaxPanel>
        
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        
    </telerik:RadAjaxLoadingPanel>
</asp:Content>



<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Company_Master.aspx.vb" Inherits="Company_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Company Settings
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Company Settings</li>
        </ol>
    </div>
    <script type="text/javascript">
        function AutoFill() {

            var cname = document.getElementById('<%=txtName.ClientID%>').value;
            var hdcnm = document.getElementById('<%=hdcnm.ClientID%>').value;

            var editor = $find("<%= txtReciptHeader.ClientID%>");

            editor = editor.get_html()
            var f = editor.replace(hdcnm, cname)

            $find("<%=txtReciptHeader.ClientID%>").set_html(cname);

        }

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
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow" runat="server" id="divCompany">
                <div class="panel-heading">Company Settings</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="register"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <asp:HiddenField ID="hdcnm" runat="server" />
                                        <label>Company Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtName" CssClass="form-control" Skin="" runat="server" placeholder="Company Name" Width="100%" onchange="AutoFill()"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtName" ErrorMessage="Company name is required"
                                            ValidationGroup="register" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>


                                    </div>
                                    <div class="col-md-6">
                                        <label>Code <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtCode" CssClass="form-control" Skin="" runat="server" placeholder="Company Code" Width="100%" ReadOnly="true"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode" ErrorMessage="Code is required"
                                            ValidationGroup="register" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>Start Date <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadDatePicker ID="txtTDate" runat="server" DateInput-EmptyMessage="Start Date" MinDate="01/01/1000" MaxDate="01/01/3000" Width="100%" Skin="MetroTouch">
                                            <DateInput DateFormat="dd/MM/yyyy" runat="server" />
                                            <Calendar runat="server" FirstDayOfWeek="Monday">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtTDate" ErrorMessage="Start date is required"
                                            ValidationGroup="register" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Venue </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtVenue" CssClass="form-control" Skin="" runat="server" placeholder="Venue" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtVenue"
                                            ErrorMessage="Enter only numeric and characters in venue" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>Domain <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtDomain" CssClass="form-control" Skin="" runat="server" placeholder="@domain" Width="100%" ReadOnly="true"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="rfvDomain" runat="server" ControlToValidate="txtDomain" ErrorMessage="Domain is required"
                                            ValidationGroup="register" Display="none" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDomain" runat="server" ControlToValidate="txtDomain" ValidationGroup="register" Display="none" CssClass="text-danger"
                                            ErrorMessage="Domain name is invalid" ValidationExpression="@\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Email </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtEmail" CssClass="form-control" Skin="" runat="server" placeholder="Email" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="register" Display="none"
                                            CssClass="text-danger" ErrorMessage="Email is invalid" ValidationExpression="([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>Website </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtWebsite" CssClass="form-control" Skin="" runat="server" placeholder="Website" Width="100%"></telerik:RadTextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Contact </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox MinValue="0" ID="txtContact" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Contact">
                                        </telerik:RadTextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <label>Branch </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtBranchName" CssClass="form-control" Skin="" runat="server" placeholder="Branch" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBranchName"
                                            ErrorMessage="Enter only numeric and characters in branch" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Synchronization </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtSynchronization" CssClass="form-control" Skin="" runat="server" placeholder="Synchronization" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtSynchronization"
                                            ErrorMessage="Enter only numeric and characters in synchronization" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label>Description</label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtDescription" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Description" Width="100%" Rows="4"></telerik:RadTextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label>Receipt Header</label><div class="clearfix"></div>
                                        <telerik:RadEditor ID="txtReciptHeader" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="200px" Style="overflow: auto;">
                                        </telerik:RadEditor>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12" style="padding-bottom: 15px;">
                                        <label>Address</label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtAddress" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Address" Width="100%" Rows="4"></telerik:RadTextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="form-group">
                                        <div class="col-md-6">
                                            <label>Country <span class="text-danger">*</span></label><div class="clearfix"></div>
                                            <telerik:RadComboBox ID="radCountry" runat="server" AutoPostBack="true" Width="100%">
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="radCountry" ErrorMessage="Country is required"
                                                ValidationGroup="register" Display="none" CssClass="text-danger" InitialValue="--SELECT--">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6">
                                            <label>State <span class="text-danger">*</span></label><div class="clearfix"></div>
                                            <telerik:RadComboBox ID="radState" runat="server" AutoPostBack="true" Width="100%">
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="rfvradState" runat="server" ControlToValidate="radState" ErrorMessage="State is required"
                                                ValidationGroup="register" Display="none" CssClass="text-danger" InitialValue="--SELECT--">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6">
                                            <label>City <span class="text-danger">*</span></label><div class="clearfix"></div>
                                            <telerik:RadComboBox ID="radCity" runat="server" Width="100%">
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="rfvradCity" runat="server" ControlToValidate="radCity" ErrorMessage="City is required"
                                                ValidationGroup="register" Display="none" CssClass="text-danger" InitialValue="--SELECT--">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                         <div class="clearfix"></div> <br/>
                                          <div class="col-md-6">
                                            <label>Product Type <span class="text-danger">*</span></label><div class="clearfix"></div>
                                            <telerik:RadComboBox ID="version" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="Version_SelectedIndexChanged">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="TenderPOS" Value="1" />
                                                    <telerik:RadComboBoxItem Text="TenderLITE" Value="2" />
                                                </Items>
                                            </telerik:RadComboBox>
                                               <asp:RequiredFieldValidator ID="LiteVersion" runat="server" ControlToValidate="version" ValidationGroup="register" Display="none" InitialValue="--SELECT--"></asp:RequiredFieldValidator>
                                           <%-- <asp:RequiredFieldValidator ID="LiteVersion" runat="server" ControlToValidate="version"
                                                ValidationGroup="register" Display="none"  InitialValue="--SELECT--">  
                                            </asp:RequiredFieldValidator> --%> 
                                        </div>
                                        <div class="col-md-6">
                                            <label>Postal Code </label>
                                            <div class="clearfix"></div>
                                            <telerik:RadTextBox ID="txtPcode" CssClass="form-control" Skin="" runat="server" placeholder="Postal Code" Width="100%"></telerik:RadTextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPcode"
                                                ErrorMessage="Enter only numeric and characters in postal code" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </telerik:RadAjaxPanel>
                                <div class="form-group" style="overflow: hidden;">
                                    <div class="col-md-6">
                                        <label>Logo</label><div class="clearfix"></div>


                                        <telerik:RadAsyncUpload ID="fileupload" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandler"
                                            MaxFileSize="5000" OnClientValidationFailed="OnClientFileSize">
                                            <FileFilters>
                                                <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                            </FileFilters>
                                        </telerik:RadAsyncUpload>
                                        <i style="font-weight: lighter">
                                            <asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text="File size should not exceed 5Kb"></asp:Label>
                                        </i>
                                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />
                                        <div class="clearfix"></div>
                                        <asp:HiddenField ID="hdlogo" runat="server" />
                                        <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="150px" />

                                    </div>
                                    <div class="col-md-6">
                                        <label>Vat No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtVatNo" CssClass="form-control" Skin="" runat="server" placeholder="Vat No" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtVatNo"
                                            ErrorMessage="Enter only numeric and characters in vat no" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="clearfix"></div>

                                     

                                </div>
                          
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label>Receipt Footer</label><div class="clearfix"></div>
                                        <telerik:RadEditor ID="txtReciptFooter" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="200px" Style="overflow: auto;">
                                        </telerik:RadEditor>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group" style="overflow: hidden;">
                                    <div class="col-md-6">

                                        <label>Log Off Time</label>
                                        <i style="font-weight: lighter">
                                            <asp:Label ID="Label1" runat="server" Text="(In minutes)"></asp:Label></i>
                                        <telerik:RadNumericTextBox MinValue="0" ID="txtlogofftime" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="Log Off Time">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>

                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="col-md-6">

                                        <label>No.Of Park Sale Per Operator</label>

                                        <telerik:RadNumericTextBox MinValue="0" ID="txtparsaleperoperator" Skin="" CssClass="form-control"
                                            runat="server" Width="100%" placeholder="No.Of Park Sale Per Operator">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group" style="overflow: hidden;">
                                    <div class="col-md-6">
                                        <label>Currency </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddCurrency" runat="server" Width="100%" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddCurrency_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Week start day </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="radWeekStartDay" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Monday" Value="Monday" />
                                                <telerik:RadComboBoxItem Text="Tuesday" Value="Tuesday" />
                                                <telerik:RadComboBoxItem Text="Wednesday" Value="Wednesday" />
                                                <telerik:RadComboBoxItem Text="Thursday" Value="Thursday" />
                                                <telerik:RadComboBoxItem Text="Friday" Value="Friday" />
                                                <telerik:RadComboBoxItem Text="Saturday" Value="Saturday" />
                                                <telerik:RadComboBoxItem Text="Sunday" Value="Sunday" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                </div>

                                <div class="form-group" style="overflow: hidden;">
                                    <div class="col-md-6">
                                        <label>Business Done by</label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddBusinessDone" runat="server" Width="100%">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="SELECT" Value="0" />
                                                <telerik:RadComboBoxItem Text="Department" Value="1" />
                                                <telerik:RadComboBoxItem Text="Department Category" Value="2" />

                                            </Items>
                                        </telerik:RadComboBox>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="col-md-6" >
                                        <label>PaymentGateway</label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chk_PGtway" runat="server" Visible="false" />
                                    </div>

                                </div>

                                <div class="form-group" style="overflow: hidden;">
                                    <div class="col-md-6">
                                        <label>Show Chips Settings (On Backoffice)</label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chkChips" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Limit the duration</label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chk_duration" runat="server" />
                                    </div>
                                </div>

                                <div class="form-group" style="overflow: hidden;">
                                    <div class="col-md-6">
                                        <label>Display Cash Declaration (On Business Done)</label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chkDisplayDeclaration" runat="server" />
                                    </div>

                                    <div class="col-md-6" id="div_tax2" runat="server" visible="false">
                                        <label>Apply Seccond Tax</label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chkIsAddTax2" runat="server" />
                                    </div>

                                </div>

                                <div class="form-group" style="overflow: hidden;">

                                    <div class="col-md-8" id="div_ExclusiveTax" runat="server">
                                        <label>Tax Exclusive</label><br />
                                        <label class="text-danger">(Note : Exclusive tax will be apply only on new product.)</label>
                                        <div class="clearfix"></div>
                                        <asp:CheckBox ID="chkIsExclusiveTax" runat="server" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-actions text-right pal" id="Div_Button" runat="server">
                       <%-- <asp:Panel ID="pnlPopup" runat="server" CssClass="popup" --%>


                      <%-- <asp:Button ID="deleteAll" OnClientClick="return confirm('Are you sure you want to clear all data?');" OnClick="deleteAll_Click" AutoPostBack="True" class="btn btn-primary" runat="server" Text="Clear All" ValidationGroup="register" ToolTip="Click here to clear All data from Your Store" />--%>
                        <asp:LinkButton ID="deleteAll" OnClientClick="return confirm('Are you sure you want to clear all data?');" class="btn btn-primary"  style="margin-right:8px" runat="server" Text="Clear All Data "  OnClick="deleteAll_Click" /> 
                        <asp:LinkButton ID="btnEnableLogin" class="btn btn-primary" visible="false" style="margin-right:8px" runat="server" Text="Enable Register With Email " OnClientClick="return enableLogin();" OnClick="btnEnableLogin_Click" />

                 <%--  </asp:Panel>&nbsp--%>
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="register" ToolTip="Click here to Save Company Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Company Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Company Details" />
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


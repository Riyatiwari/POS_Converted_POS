<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Company_Register.aspx.vb" Inherits="Company_Register" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Register</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="Thu, 19 Nov 1900 08:52:00 GMT" />
    <!--Loading bootstrap css-->
   
    <link type="text/css" rel="stylesheet" href="vendors/font-awesome/css/font-awesome.min.css" />
    <link type="text/css" rel="stylesheet" href="vendors/bootstrap/css/bootstrap.min.css" />
    <!--Loading style vendors-->
    <link type="text/css" rel="stylesheet" href="vendors/animate.css/animate.css" />
    <link type="text/css" rel="stylesheet" href="vendors/iCheck/skins/all.css" />
    <!--Loading style-->

    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" class="default-style" />
    <link type="text/css" rel="stylesheet" href="css/themes/style1/yellow-green.css" id="theme-change"
        class="style-change color-change" />
    <link type="text/css" rel="stylesheet" href="css/style-responsive.css" />
    <link rel="shortcut icon" href="images/favicon.ico" />
    <script type="text/javascript" >
        function AutoFill() {
            var cname = document.getElementById('<%=txtName.ClientID%>').value;
            $find("<%=txtReciptHeader.ClientID%>").set_html(cname);
        }
    </script>
</head>

<body style="background-image: url(images/bg/2.jpg)">
    <div class="page-form" style="width: 70% !Important;">
        <form class="form" runat="server">
            <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
            <div class="header-content">
                <h1>Register</h1>
            </div>
            <div class="body-content">
                <div class="row">
                    <div class="col-sm-6 col-md-6">
                        <div class="form-group">
                            <div class="col-md-6">
                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="register"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                <label>Company Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                <telerik:RadTextBox ID="txtName" CssClass="form-control" Skin="" runat="server" placeholder="Company Name" Width="100%" onchange="AutoFill()"  ></telerik:RadTextBox>
                                <%--<asp:TextBox ID="txtName" CssClass="form-control span12 form-Control_half" runat="server" placeholder="Company Name"></asp:TextBox>--%>
                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtName" ErrorMessage="Company name is required"
                                    ValidationGroup="register" Display="none" CssClass="text-danger">
                                </asp:RequiredFieldValidator>
                                
                            </div>
                            <div class="col-md-6">
                                <label>Code <span class="text-danger">*</span></label><div class="clearfix"></div>
                                <telerik:RadTextBox ID="txtCode" CssClass="form-control" Skin="" runat="server" placeholder="Company Code" Width="100%"></telerik:RadTextBox>
                                <%--<asp:TextBox ID="txtCode" CssClass="form-control span12 form-Control_half" runat="server" placeholder="Company Code"></asp:TextBox>--%>
                                <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode" ErrorMessage="Code is required"
                                    ValidationGroup="register" Display="none" CssClass="text-danger">
                                </asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCode"
                                            ErrorMessage="Enter only numeric and characters in code" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="form-group">
                        </div>
                        <div class="form-group">
                            <div class="col-md-6">
                                <label>Start Date <span class="text-danger">*</span></label><div class="clearfix"></div>
                                <telerik:RadDatePicker ID="txtTDate" runat="server" DateInput-EmptyMessage="Start Date" MinDate="01/01/1000" MaxDate="01/01/3000" Width="100%" skin="MetroTouch">
                                    <DateInput DateFormat="dd/MM/yyyy" runat="server" />
                                    <Calendar runat="server" FirstDayOfWeek="Monday">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                </telerik:RadDatePicker>
                                <%--<asp:TextBox ID="txtDate" CssClass="form-control span12" runat="server"></asp:TextBox>--%>
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
                                <%--<label>Pan No </label>
                                <div class="clearfix"></div>
                                <telerik:RadNumericTextBox MinValue="0" ID="txtPan" Skin="" CssClass="form-control"
                                    runat="server" Width="100%" placeholder="Pan No">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>--%>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-6">
                                <label>Domain <span class="text-danger">*</span></label><div class="clearfix"></div>
                                <telerik:RadTextBox ID="txtDomain" CssClass="form-control" Skin="" runat="server" placeholder="@domain" Width="100%"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvDomain" runat="server" ControlToValidate="txtDomain" ErrorMessage="Domain is required"
                                    ValidationGroup="register" Display="none" CssClass="text-danger">
                                </asp:RequiredFieldValidator>
                            <%--    <asp:RegularExpressionValidator ID="revDomain" runat="server" ControlToValidate="txtDomain" ValidationGroup="register" Display="none" CssClass="text-danger"
                                    ErrorMessage="Invalid Domain" ValidationExpression="@(([\w-]+))"></asp:RegularExpressionValidator>--%>
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
                                <telerik:RadNumericTextBox MinValue="0" ID="txtContact" Skin="" CssClass="form-control"
                                    runat="server" Width="100%" placeholder="Contact">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                       
                       <%-- <div class="form-group">
                            <div class="col-md-12">
                                <label>Description</label><div class="clearfix"></div>
                                <telerik:RadTextBox ID="txtDescription" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Description" Width="100%"></telerik:RadTextBox>
                            </div>
                            <div class="clearfix"></div>
                        </div>--%>
                          <div class="form-group">
                            <div class="col-md-6">
                                <label>Branch </label>
                                <div class="clearfix"></div>
                                <telerik:RadTextBox ID="txtBranchName" CssClass="form-control" Skin="" runat="server" placeholder="Branch" Width="100%"></telerik:RadTextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBranchName"
                                            ErrorMessage="Enter only numeric and characters in branch name" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
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
                                <telerik:RadTextBox ID="txtDescription" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Description" Width="100%"></telerik:RadTextBox>
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
                                <telerik:RadTextBox ID="txtAddress" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Address" Width="100%" Rows="3"></telerik:RadTextBox>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label>Country <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radCountry" runat="server" AutoPostBack="true" Width="100%">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="radCountry" ErrorMessage="Country Required"
                                        ValidationGroup="register" Display="none" CssClass="text-danger" InitialValue="--SELECT--">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label>State <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radState" runat="server" AutoPostBack="true" Width="100%">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rfvradState" runat="server" ControlToValidate="radState" ErrorMessage="State Required"
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
                                    <asp:RequiredFieldValidator ID="rfvradCity" runat="server" ControlToValidate="radCity" ErrorMessage="City Required"
                                        ValidationGroup="register" Display="none" CssClass="text-danger" InitialValue="--SELECT--">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label>Postal Code </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtPcode" CssClass="form-control" Skin="" runat="server" placeholder="Postal Code" Width="100%"></telerik:RadTextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPcode"
                                            ErrorMessage="Enter only numeric and characters in postal code" ValidationGroup="register" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </telerik:RadAjaxPanel>
                        <div class="form-group" style="overflow: hidden;">
                            <div class="col-md-6">
                                <label>Logo</label><div class="clearfix"></div>
                                <telerik:RadAsyncUpload ID="fileupload" AllowedFileExtensions="jpg,jpeg,png,gif" runat="server" MultipleFileSelection="Disabled" class="pull-right"
                                    UploadedFilesRendering="BelowFileInput" MaxFileInputsCount="1">
                                </telerik:RadAsyncUpload>
                                <i style="font-weight:lighter "><asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg,.jpeg,.gif,.png)"></asp:Label></i>
                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" Visible="false" />
                            </div>
                            
                             <div class="col-md-6">
                                    <label>Vat No </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtVatNo" CssClass="form-control" Skin="" runat="server" placeholder="Vat No" Width="100%"></telerik:RadTextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtVatNo"
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
                         
                        <%--<div class="form-group" style="visibility: hidden">
                            <div class="col-md-6">
                                <label>Registration No </label>
                                <div class="clearfix"></div>
                                <telerik:RadNumericTextBox MinValue="0" ID="txtRegNo" Skin="" CssClass="form-control"
                                    runat="server" Width="100%" placeholder="Registration No">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-6">
                                <label>VAT No </label>
                                <div class="clearfix"></div>
                                <telerik:RadNumericTextBox MinValue="0" ID="txtGST" Skin="" CssClass="form-control"
                                    runat="server" Width="100%" placeholder="VAT No">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="form-group" style="visibility: hidden">
                            <div class="col-md-6">
                                <label>CST No </label>
                                <div class="clearfix"></div>
                                <telerik:RadNumericTextBox MinValue="0" ID="txtCST" Skin="" CssClass="form-control"
                                    runat="server" Width="100%" placeholder="CST No">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-6">
                                <label>Service Tax No </label>
                                <div class="clearfix"></div>
                                <telerik:RadNumericTextBox MinValue="0" ID="txtServiceTex" Skin="" CssClass="form-control"
                                    runat="server" Width="100%" placeholder="Service Tax No">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="clearfix"></div>
                        </div>--%>
                         <div class="form-group">
                                    <div class="col-md-6">
                                          <label>Log Off Time</label> <i style="font-weight:lighter "><asp:Label ID="Label1" runat="server" Text="(In minutes)"></asp:Label></i> 
                                       <telerik:RadNumericTextBox MinValue="0" ID="txtlogofftime" Skin="" CssClass="form-control"
                                    runat="server" Width="100%" placeholder="Log Off Time">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                                         <%--<telerik:RadTextBox ID="txtlogofftime" CssClass="form-control" Skin="" runat="server" placeholder="Log Off Time" Width="100%" ></telerik:RadTextBox>--%>
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
                                          <label>Currency </label><div class="clearfix"></div>
                                            <telerik:RadComboBox ID="ddCurrency" runat="server" Width="100%">
                                            </telerik:RadComboBox>
                                        </div></div>
                    </div>
                </div>
                <div class="text-right pal">
                           <asp:Button ID="btnRegister" class="btn btn-primary" runat="server" Text="Register" ValidationGroup="register" ToolTip="Click here to Register" />&nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset" />&nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel" />
                </div>
            </div>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>
        </form>
    </div>


    <script src="js/jquery-1.10.2.min.js"></script>
    <script src="js/jquery-migrate-1.2.1.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <!--loading bootstrap js-->
    <script src="vendors/bootstrap/js/bootstrap.min.js"></script>
    <script src="vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js"></script>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <script src="vendors/iCheck/icheck.min.js"></script>
    <script src="vendors/iCheck/custom.min.js"></script>
    <script>//BEGIN CHECKBOX & RADIO
        $('input[type="checkbox"]').iCheck({
            checkboxClass: 'icheckbox_minimal-grey',
            increaseArea: '20%' // optional
        });
        $('input[type="radio"]').iCheck({
            radioClass: 'iradio_minimal-grey',
            increaseArea: '20%' // optional
        });
        //END CHECKBOX & RADIO</script>
  
</body>
</html>

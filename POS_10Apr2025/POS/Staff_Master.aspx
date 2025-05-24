<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Staff_Master.aspx.vb" Inherits="Staff_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    User Master
</asp:Content>

 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Staff_List.aspx">User List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">User Master</li>
        </ol>
    </div>
    <script src="//code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function OnClientFileSize() {
            alert('Image size is more than 5kb.Compress image file size and Try again.');


        }

        function OnClientFileSelectedHandler(sender, eventArgs) {
            var input = eventArgs.get_fileInputField();

            if (sender.isExtensionValid(input.value)) {
                if (input.files && input.files[0] && sender) {
                    debugger;
                    var reader = new FileReader();

                    reader.onload = function (e) {

                        $('#<%=ImgPrv.ClientID%>').show()
                        $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                            .width(240)
                            .height(180);

                    };
                    reader.readAsDataURL(input.files[0]);
                }
            }
        }

    </script>


</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <asp:HiddenField ID="hdPhoto" runat="server" Value="" />
        <asp:HiddenField ID="hdtest" runat="server" Value="" />
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>

        <div class="panel panel-yellow">
            <div class="panel-heading">User Master</div>
            <div class="panel-body pan">
                <div class="form-body pal">

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label>Till Code<span class="text-danger" runat="server" id="sTillCode">&nbsp;*</span> </label>
                                    <div class="clearfix"></div>
                                    <%-- <telerik:RadNumericTextBox MinValue="0" ID="txtTillCode" Skin="" CssClass="form-control"
                                                runat="server" Width="100%" placeholder="Till Code" MaxLength="4">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>--%>

                                    <asp:TextBox ID="txtTillCode" CssClass="form-control" Skin="" runat="server" placeholder="Till Code" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfTillCode" runat="server" ControlToValidate="txtTillCode"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Till code is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtTillCode"
                                        ErrorMessage="Invalid Till Code" ValidationGroup="valid" ValidationExpression="[0-9]*$" Display="None">
                                    </asp:RegularExpressionValidator>

                                    <div class="clearfix"></div>
                                   <div >
                                       
                                  <br />
                                    <label>
                                     Access POS &nbsp &nbsp
                                    <asp:CheckBox ID="forpos" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="forpos_CheckedChanged" /></label>
                                 
                                       </div>  
                                    
                                </div>
                                                               
                                <div class="col-md-6">
                                    <label>Full Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtFName" CssClass="form-control" Skin="" runat="server" placeholder="Full Name" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtFName"
                                        ValidationGroup="valid" Display="None" ErrorMessage="First name is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFName"
                                        ErrorMessage="Enter only numeric and characters in user full name" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>

                                </div>
                                <br /> <br />

                                   <div class="col-md-6" style="margin-top:20px;">
                                    <label >POS Access Roles <span class="text-danger" runat="server" id="srules">&nbsp;*</span></label></label>
                                    <div class="clearfix"></div>
                                       <asp:DropDownList ID="ddl_SMaster" runat="server" Width="100%">
                                    </asp:DropDownList>
                                 
                                           <asp:RequiredFieldValidator ID="rkrules" runat="server" ControlToValidate="ddl_SMaster" InitialValue="SELECT"
                                        ValidationGroup="valid" Display="None" ErrorMessage="POS Access Roles selection is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                




                                    <%-- <telerik:RadComboBox ID="chkfunctiontype" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                        Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" AutoPostBack="true">
                                    </telerik:RadComboBox>--%>

                                    <%--<asp:GridView ID="chkfunctiontype" runat="server" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="True" ShowGroupPanel="False"
                                            AllowFilteringByColumn="false" CssClass="Grid"
                                            EmptyDataText="No data in the data source." ShowHeaderWhenEmpty="True" Width="100%" GridLines="None">

                                        <Columns>
                                            <asp:TemplateField HeaderText="POS Access Rules">
                                                <HeaderStyle Width="30%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_functiontype" runat="server" Value='<%# Eval("function_type_id")%>' />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblfunctiontype" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdfunctiontype" runat="server" Value='<%# Eval("function_type_id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>--%>
                                </div>
                           
                                <div class="col-md-6">
                                     <div class="clearfix"></div>
                                  <br />
                                   
                                    
                                    <label>
                                Access BackOffice&nbsp &nbsp
                                       <asp:CheckBox ID="backhoffice" Checked="false" runat="server"  AutoPostBack="true" OnCheckedChanged="backhoffice_CheckedChanged" /></label>
                              
                                </div>&nbsp &nbsp
                                <div class="col-md-6" >
                                    <asp:Label ID="role" runat="server">Access BackOffice Role</asp:Label><lable visible="false"><span class="text-danger" runat="server" id="srole">&nbsp;*</span></lable>
                                    <%--<label id="role">Access BackOffice Role<span class="text-danger" runat="server" id="srole">&nbsp;*</span></label>--%>
                                      
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="radRole" runat="server" Width="100%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radRole" InitialValue="SELECT"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Role is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                               
                                 <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <div class="clearfix"></div>

                                    <label>
                                       Trianee &nbsp &nbsp
                                    <asp:CheckBox ID="chktrainee" runat="server" /></label>
                            
                                     &nbsp &nbsp &nbsp &nbsp
                                  &nbsp &nbsp &nbsp &nbsp&nbsp&nbsp
                                    <label>
                                        Till Active &nbsp &nbsp
                                        <asp:CheckBox ID="chkTillActive" runat="server" Checked="true" AutoPostBack="true" /></label>
                               <div class="clearfix"></div>
                                       <br />

                                    <label>
                                        Active &nbsp
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>                                    
                                </div>
                                

                                 <div class="col-md-6">
                                    <label>Venue Rights</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="ddlVenue" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                        Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" AutoPostBack="true">
                                    </telerik:RadComboBox>
                              </div>


                                
                                



                             
                                <div class="clearfix"></div>
                              <br />
                                <div class="col-md-6">
                                    <label>Authentication Code </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtAuthentication_Code" CssClass="form-control" Skin="" runat="server" placeholder="Authentication Code" Width="100%"></asp:TextBox>


                                </div>
                                             <div class="col-md-6">
                                    <label>Other Id </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtOtherId" CssClass="form-control" Skin="" runat="server" placeholder="Other Id" Width="100%"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtOtherId"
                                        ErrorMessage="Enter only numeric and characters in other id" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>

                                </div>

                                <div class="clearfix"></div>
                                <br />
                               <%-- <div class="col-md-6">--%>
                                   <%-- <label  id="lblJoiningDate" runat="server" visible="false">Joining date <span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>--%>
                                    <telerik:RadDatePicker ID="txtTDate" Visible="false" runat="server" DateInput-EmptyMessage="Joining date" MinDate="01/01/1000" MaxDate="01/01/3000" Width="100%" Skin="MetroTouch">
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                        <Calendar runat="server" FirstDayOfWeek="Monday">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <asp:HiddenField ID="hiddenFieldDate" runat="server" Visible="true" />

                                   <%-- <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtTDate"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Joining date is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>--%>
                           <%--     </div>--%>
                               
                               


                                

                              
                                <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" >--%>





                                <div class="col-md-6">
                                    <label>Photo </label>
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                                    <telerik:RadAsyncUpload ID="RadUpload1" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" OnClientFileSelected="OnClientFileSelectedHandler"
                                        MaxFileSize="5000" OnClientValidationFailed="OnClientFileSize">
                                        <FileFilters>
                                            <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                        </FileFilters>
                                    </telerik:RadAsyncUpload>
                                    <i style="font-weight: lighter">
                                        <asp:Label ID="Label2" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="File size should not exceed 5Kb"></asp:Label>
                                    </i>
                                    <br />
                                </div>

                                 
                                
                                
                                <div class="clearfix"></div>
                                <br />
                                


                                <div class="col-md-6">

                                    <asp:Image ID="ImgPrv" Height="180px" Width="240px" runat="server" Style="display: none" /><br />
                                    <%--<div id="DivImage" hidden="hidden" >
                                         <asp:Image ID="Image1" Height="180px" Width="240px" runat="server"  Visible="false" BorderStyle="None" /><br />
                                        </div>--%>
                                </div>

                                <div class="clearfix"></div>
                                <br />


                               
                             

                                <div class="clearfix"></div>
                                <br />

                            </div>

                        </div>

                        
       
                        <div class="col-md-6">
                                                     <div class="col-md-6">
                                    <label>Email<span class="text-danger" runat="server" id="reqemail">&nbsp;*</span></label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtEmail" CssClass="form-control" Skin="" runat="server" placeholder="Email" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rkemail" runat="server" ControlToValidate="txtEmail"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Email is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="valid" Display="None"
                                        CssClass="text-danger" ErrorMessage="Invalid email" ValidationExpression="([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})"></asp:RegularExpressionValidator>
                                </div>

                            <div class="col-md-6">
                                    <label>Phone No.</label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox MinValue="0" ID="txtContact" Skin="" CssClass="form-control"
                                        runat="server" Width="100%" placeholder="Contact">
                                            <%--<NumberFormat GroupSeparator="" DecimalDigits="0" />--%>
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txtContact"
                                        ErrorMessage="Invalid contact no" ForeColor="Red" Display="None"
                                        ValidationExpression="^[0-9]*$" ValidationGroup="valid">
                                    </asp:RegularExpressionValidator>
                                </div>




                            
                            <div class="clearfix"></div>
                            <br />
                            <div class="form-group">
                                <div class="col-md-7" style="padding-bottom: 8px;margin-bottom:4px;">
                                    <label>Address</label><div class="clearfix"></div>
                                    <asp:TextBox ID="txtAddress" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Address" Rows="3" Width="100%"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <%--<telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
                                <div class="col-md-6">
                                    <label>Country </label>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="radCountry" runat="server" OnSelectedIndexChanged="radCountry_SelectedIndexChanged" AutoPostBack="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label>State </label>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="radState" runat="server" OnSelectedIndexChanged="radState_SelectedIndexChanged" AutoPostBack="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>City</label><div class="clearfix"></div>
                                    <asp:DropDownList ID="radCity" runat="server" EnableScreenBoundaryDetection="false" ExpandDirection="Down" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <%--</telerik:RadAjaxPanel>--%>
                                <div class="col-md-6">
                                    <label>Postal Code </label>
                                    <div class="clearfix"></div>
                                    <asp:TextBox ID="txtNational" CssClass="form-control" Skin="" runat="server" placeholder="Postal Code" Width="100%"></asp:TextBox>
                                </div>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNational"
                                    ErrorMessage="Enter only numeric and characters in postal code" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>

                                <div class="clearfix"></div>
                                <br />
                                <%--  <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server">--%>
                                

                   

                               

                                <%-- <div class="clearfix"></div>
                                    <br />--%>
                                <%--</telerik:RadAjaxPanel>--%>


                                
                             


                            </div>
                        </div>
                    </div>

                    <div class="row">
                    </div>
                    <div class="row">
                    </div>



                     <div class="col-md-6">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <%--<label>User Code <span class="text-danger">*</span></label><div class="clearfix"></div>--%>
                                    <asp:TextBox ID="txtSCode" CssClass="form-control" Skin="" runat="server" placeholder="User Code" Width="100%" ></asp:TextBox>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSCode"
                                        Display="None" ErrorMessage="Code is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>--%>


                                </div>

                </div>
                <div class="form-actions text-right pal">
                     <asp:Button ID="Remail" class="btn btn-primary" runat="server" ValidationGroup="valid" Text="Register And Save" ToolTip="Click here to Register with mail" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save User Details" />&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset User Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel User Details" />
                </div>
            </div>

        </div>
        <%--</telerik:RadAjaxPanel>--%>
    </div>
    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>


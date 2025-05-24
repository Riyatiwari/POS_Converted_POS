<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="API_Master.aspx.vb" Inherits="API_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  API Master
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="API_List.aspx">API List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">API Master</li>
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


        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }

        
        function OnTextKeyPress(objEvent) {
            var $th = objEvent.value;
            $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                return '';
            }));
        }


      
        function OnTextKeyPress(objEvent) {
            var $th = objEvent.value;
            $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                return '';
            }));
        }

        function OnTextKeyPress(objEvent) {
            var $th = objEvent.value;
            $("#" + objEvent.id).val($th.replace(/[^a-zA-Z0-9\s\_]*$/g, function (str) {

                return '';
            }));
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">API Master</div>
             <div class="panel-body pan">

                    <div class="form-body pal">


                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="register"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                                 <div class="col-md-6" style="margin-bottom:10px;margin-top:40px;">
                                    
                                    <label>
                                        API Name <span class="text-danger">*</span>   </label>
                                        &nbsp;
                                        <telerik:RadComboBox ID="ddlcloud" ValidationGroup="register" runat="server" ExpandDirection="Down" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlcloud_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Select" Value="0" />
                                                <telerik:RadComboBoxItem Text="Cloud Net API" Value="1" />
                                                <telerik:RadComboBoxItem Text="BarStock Exchange" Value="2" />
                                               <%-- <telerik:RadComboBoxItem Text="Cashflow" Value="2" />
                                                <telerik:RadComboBoxItem Text="CustomPay" Value="3" />
                                                <telerik:RadComboBoxItem Text="CardStream" Value="4" />
                                                <telerik:RadComboBoxItem Text="Positive Payment" Value="5" />--%>
                                            </Items>
                                        </telerik:RadComboBox>
                                               
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcloud" ErrorMessage="API is required"
                                                ValidationGroup="register" Display="none" CssClass="text-danger" InitialValue="--SELECT--">
                                  </asp:RequiredFieldValidator>
                                 <br />
                                </div>

                                <br /><br />


                                
                                <div class="col-md-12" id="divCloud_url" runat="server">
                                    <label>Cloud URL <span class="text-danger">*</span> </label><div class="clearfix"></div>
                                    
                                     <telerik:RadTextBox ID="txtUrl" CssClass="form-control" Skin="" runat="server" placeholder="Url" Width="100%"></telerik:RadTextBox>
                              <br /> <br />
                                </div>
                                
                               <div class="col-md-12" id="divCloud_Key" runat="server">
                                    <label>Cloud Key</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtcloudkey" CssClass="form-control" Skin="" runat="server" placeholder="Cloud Api Key" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlcloud"
                                        ValidationGroup="register" Display="None" ErrorMessage="ApiCloud" CssClass="text-danger">
                                  </asp:RequiredFieldValidator>
                                   <br />
                                </div>
                                  <br />

                                <div class="col-md-12" id="divCloud_Value" runat="server">
                                    <label>Cloud Value</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtCLDvalue" CssClass="form-control" Skin="" runat="server" placeholder="Cloud Api Value" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>
                              </div>
                      
                             
                            
                                <div class="col-md-12" id="divbarurl" runat="server">
                                    <label>Bar URL<span class="text-danger">*</span> </label><div class="clearfix"></div>
                                    
                                     <telerik:RadTextBox ID="txtbarurl" CssClass="form-control" Skin="" runat="server" placeholder="Url" Width="100%"></telerik:RadTextBox>
                              <br /> <br />
                                </div>
                                
                               <div class="col-md-12" id="divbartoken" runat="server">
                                    <label>Token</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtbarToken" CssClass="form-control" Skin="" runat="server" placeholder="Bar Token" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                   
                                   <br />
                                </div>
                                  <br />

                               <%-- <div class="col-md-12" id="divbarmode" runat="server">
                                    <label>Mode</label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtbarmode" CssClass="form-control" Skin="" runat="server" placeholder="Type Mode Only 1,2,or3" Width="100%"></telerik:RadTextBox>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>--%>
                              
                      
                            </div>
                           </div>  
                    <div class="col-md-6">
                      <div class="form-group"> 
                          <div class="col-md-6" style="margin-bottom:80px;margin-top:40px;">
                              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                       
                                        <label> 
                                   
                                             Location  &nbsp; <span class="text-danger">*</span>   </label>
                                        <telerik:RadComboBox ID="radLocation" ValidationGroup="register" runat="server" ExpandDirection="Down" Width="100%">
                                        </telerik:RadComboBox>

                                        <%--<telerik:RadComboBox ID="rad_StoreLocation" runat="server" ExpandDirection="Down" Width="155%">
                                        </telerik:RadComboBox>--%>
                                   
                                 <asp:RequiredFieldValidator ID="rvfloc" runat="server" ControlToValidate="radLocation"
                                    ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger">
                                  </asp:RequiredFieldValidator>
                              
                                    
                            </div>          
                        </div>          
                      </div>
                     </div>
              </div>
                 

      </div> 
            <div class="form-actions text-right pal">
                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="register" ToolTip="Click here to Save Location Details" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Location Details" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Location Details" />
            
                </div>
                            
        </telerik:RadAjaxPanel> 
        
    </div>

  <%--  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>





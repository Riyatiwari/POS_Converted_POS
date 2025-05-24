<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Machine_Master.aspx.vb" Inherits="Machine_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Till Master
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Machine_List.aspx">Till List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Till Master</li>
        </ol>
    </div>

    <script type="text/javascript">
            function OnClientFileSize() {
            alert('Image size is more than 10kb.Compress image file size and Try again.');
        }
        function AutoFill() {

            var cname = document.getElementById('<%=txtMName.ClientID%>').value;
            var hdcnm = document.getElementById('<%=hdmnm.ClientID%>').value;

            var editor = $find("<%= txtReciptHeader.ClientID%>");

            editor = editor.get_html()
            var f = editor.replace(hdcnm, cname)

            $find("<%=txtReciptHeader.ClientID%>").set_html(cname);

        }
    </script>
    <style>
        .mycheckbox input[type="checkbox"] {
            margin-right: 5px;
        }
    </style>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Till Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                            DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                        <asp:HiddenField ID="hdmnm" runat="server" />
                                        <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtMName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%" onchange="AutoFill()"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMName"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Till name is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>

                                    </div>

                                    <div class="col-md-6">
                                   <%--     <label>Code </label>--%>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtCode" Visible ="false" CssClass="form-control" Skin="" runat="server" placeholder="Code" Width="100%"></telerik:RadTextBox>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Serial No </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtSerialNo" CssClass="form-control" Skin="" runat="server" placeholder="Serial No" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtSerialNo"
                                            ErrorMessage="Enter only numeric and characters in serial no" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-12">
                                        <label>Mac Address </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="txtmacaddress" CssClass="form-control" Skin="" runat="server" placeholder="Mac Address" Width="100%"></telerik:RadTextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtmacaddress"
                                            ErrorMessage="Enter only numeric and characters in mac address" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Model   </label>
                                      <div class="clearfix"></div>
                                        <asp:DropDownList runat="server" ID="txtmodel" AutoPostBack="true" OnSelectedIndexChanged="ddlHardwareType_SelectedIndexChanged" Width="100%">
                                             <asp:ListItem Text="SELECT" Value="0" Selected="True"></asp:ListItem>  
                                            <asp:ListItem Text="Kinetic Saturn" Value="19" ></asp:ListItem>
                                            <asp:ListItem Text="iMin D1" Value="1" ></asp:ListItem>
                                            <asp:ListItem Text="iMin D4 " Value="2" ></asp:ListItem>
                                            <asp:ListItem Text="iMin Falcon" Value="3" ></asp:ListItem>                                            
                                            <asp:ListItem Text="Sunmi D2" Value="5" ></asp:ListItem>                                            
                                            <asp:ListItem Text="Sunmi T2" Value="6" ></asp:ListItem>                                            
                                            <asp:ListItem Text="Sunmi T2s" Value="7" ></asp:ListItem>                                            
                                            <asp:ListItem Text="PAX A920" Value="8" ></asp:ListItem>                                            
                                            <asp:ListItem Text="iMin Swan" Value="4" ></asp:ListItem>
                                            <asp:ListItem Text="Sunmi T2SLite" Value="9" ></asp:ListItem>  
                                            <asp:ListItem Text="iMin D1 POS" Value="10" ></asp:ListItem>
                                            <asp:ListItem Text="iMin D4 POS" Value="11" ></asp:ListItem>
                                            <asp:ListItem Text="PAX A920 +" Value="12" ></asp:ListItem>
                                            <asp:ListItem Text="Kinetic Saturn +" Value="13" ></asp:ListItem>
                                            <asp:ListItem Text="Kinetic Saturn PAY" Value="14" ></asp:ListItem>
                                            <asp:ListItem Text="PAX A920 PAY" Value="15" ></asp:ListItem> 
                                            <asp:ListItem Text="Ingenico" Value="16" ></asp:ListItem> 
                                            <asp:ListItem Text="Stripe S700" Value="17" ></asp:ListItem> 
                                            <asp:ListItem Text="Sunmi D3" Value="18" ></asp:ListItem> 

                                         
                                        </asp:DropDownList>

                                     
                                       <%-- <telerik:RadTextBox ID="txtmodel" CssClass="form-control" Skin="" runat="server" placeholder="Model" Width="100%"></telerik:RadTextBox>--%>
                                        <asp:RegularExpressionValidator  InitialValue="SELECT" ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtmodel"
                                            ErrorMessage="Enter only numeric and characters in model" ValidationGroup="valid" ValidationExpression="[a-zA-Z0-9 ]*$" Display="None"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <label>IP Address <span class="text-danger">*</span> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadTextBox ID="RadTxtipaddress" CssClass="form-control" Skin="" runat="server" placeholder="Code" Width="100%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadTxtipaddress"
                                            ValidationGroup="valid" Display="None" ErrorMessage="IP Address is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>Location<span class="text-danger">*</span></label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="ddLocation" runat="server" Width="100%">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator  InitialValue="SELECT" ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddLocation"
                                            ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                   
                                    <div class="col-md-6">
                                        <label>
                                            Hardware type 
                                       </label>
                                        <div class="clearfix"></div>
                                        <asp:DropDownList runat="server" ID="ddlHardwareType" AutoPostBack="true" OnSelectedIndexChanged="ddlHardwareType_SelectedIndexChanged" Width="100%">
                                            <asp:ListItem Text="Till" Value="0" ></asp:ListItem>
                                            <asp:ListItem Text="Mini POS" Value="1" ></asp:ListItem>
                                            <asp:ListItem Text="Saturn " Value="2" ></asp:ListItem>
                                            <asp:ListItem Text="Sunmi T2" Value="3" ></asp:ListItem>                                            
                                            <asp:ListItem Text="Sunmi D2" Value="5" ></asp:ListItem>                                            
                                            <asp:ListItem Text="Kiosk" Value="4" ></asp:ListItem>
                                            <asp:ListItem Text="Sunmi T2SLite" Value="6" ></asp:ListItem>
                                            <asp:ListItem Text="iMin D1" Value="7" ></asp:ListItem>
                                            <asp:ListItem Text="iMin D4" Value="8" ></asp:ListItem>
                                            <asp:ListItem Text="Falcon" Value="9" ></asp:ListItem>
                                            <asp:ListItem Text="PAX A920" Value="10" ></asp:ListItem>
                                            <asp:ListItem Text="Sunmi T2S" Value="11" ></asp:ListItem>
                                            <asp:ListItem Text="Castles Pay" Value="12" ></asp:ListItem>
                                            <asp:ListItem Text="Nucleus" Value="13" ></asp:ListItem>
                                             <asp:ListItem Text="Ingenico" Value="14" ></asp:ListItem>
                                             <asp:ListItem Text="Stripe S700" Value="15" ></asp:ListItem>
                                             <asp:ListItem Text="Sumni D3" Value="16" ></asp:ListItem>
                                        </asp:DropDownList>
                                         
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Assign &nbsp;
                                        <asp:CheckBox ID="chkAssign" runat="server" /></label>
                                    </div>

                                    <div class="col-md-6">
                                        <label>                                            
                                        <asp:CheckBox ID="chkminipos" Visible="false"  runat="server" /></label>
                                        
                                        <label>Master Till <%--<span class="text-danger">*</span>--%> </label>
                                        <div class="clearfix"></div>
                                        <telerik:RadComboBox ID="mastertill" runat="server" Width="100%">
                                        </telerik:RadComboBox> 
                                        <label visible="false">
                                         <%--till server--%>
                                        <asp:checkbox id="chktillsrvr" visible="false" runat="server" checked="false" /></label>

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
                                            Master Till (for Web Sales) &nbsp;
                                        <asp:CheckBox ID="chkwebSaleMaster" runat="server" Checked="False" /></label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Tips (Extra as Surcharge)&nbsp;
                                        <asp:CheckBox ID="chkExtraasSurcharge" runat="server" Checked="true" /></label>
                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            Only Tables Transaction&nbsp;
                                        <asp:CheckBox ID="chkOnlyTables" runat="server" Checked="true" /></label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Auto Surcharge&nbsp;
                                        <asp:CheckBox ID="chkAutoSurcharge" runat="server" AutoPostBack="true"
                                            Checked="false" OnCheckedChanged="chkAutoSurcharge_CheckedChanged" /></label>
                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            No Cash Drawer&nbsp;
                                        <asp:CheckBox ID="chkNoCashDrawer" runat="server" Checked="false" /></label>
                                    </div>

                                    <div runat="server" id="chk" visible="false">
                                        <div class="clearfix"></div>
                                        <br />

                                        <div class="col-md-6">
                                            <label>
                                                Tables&nbsp;
                                        <asp:CheckBox ID="chkTables" runat="server" /></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>
                                                Non tables&nbsp;
                                        <asp:CheckBox ID="chkNonTables" runat="server" /></label>
                                        </div>

                                        <div class="clearfix"></div>
                                        <br />

                                        <div class="col-md-6">
                                            <label>
                                                Cloakroom&nbsp;
                                        <asp:CheckBox ID="chkCloakroom" runat="server" /></label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>
                                                Chips&nbsp;
                                        <asp:CheckBox ID="chkChips" runat="server" /></label>
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            ReSync Request&nbsp;
                                        <asp:CheckBox ID="chkReSync_Request" runat="server" Checked="false" /></label>
                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            Print Server&nbsp;
                                        <asp:CheckBox ID="chk_PrintServer" runat="server" Checked="false" /></label>
                                    </div>

                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>
                                            Service Controller Start&nbsp;
                                        <asp:CheckBox ID="chkService_Controller_Start" runat="server" Checked="true" /></label>
                                        <br />
                                        <label>(Start or Stop controller service.) </label>
                                    </div>

                                    <div class="col-md-6">
                                        <label>
                                            Service Websale Print&nbsp;
                                        <asp:CheckBox ID="chkService_Websale_print" runat="server" Checked="true" /></label>
                                        <br />
                                        <label>(Check Web sale for kitchen print.) </label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                    <div class="col-md-6">
                                        <label>
                                            Service Print Share&nbsp;
                                        <asp:CheckBox ID="chkService_Print_Share" runat="server" Checked="true" /></label>
                                        <br />
                                        <label>(check services if attached kitchen printer is offline will try 3 times to get it online otherwise it will print to default printer.) </label>
                                    </div>

                                    <div class="col-md-6">
                                        <label>
                                            Service Print Share Other Till&nbsp;
                                        <asp:CheckBox ID="chkService_print_Share_Other_Till" runat="server" Checked="true" /></label>
                                        <br />
                                        <label>(Check for shared printer on from other till.) </label>
                                    </div>
                                    <div class="clearfix"></div>
                                    <br />


                                    <div class="col-md-6">
                                        <label>
                                            Service Create Booking Table&nbsp;
                                        <asp:CheckBox ID="chk_booking" runat="server" /></label>
                                        <br />
                                        <label>(Create table from BackOffice booking.) </label>
                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            Online Z-Report&nbsp;
                                        <asp:CheckBox ID="chk_OnlineZreport" runat="server" /></label>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="col-md-6">
                                        <label>
                                            Elina Transaction&nbsp;
                                        <asp:CheckBox ID="chk_ElinaTran" runat="server" /></label>


                                    </div>
                                         <br />

                                    <div class="col-md-6">
                                        <label>
                                            Quick Transaction&nbsp;
                                        <asp:CheckBox ID="chk_QuickTran" runat="server" AutoPostBack="true" OnCheckedChanged="chk_QuickTran_CheckedChanged" /></label>

                                    </div>
                                   
                                    <div class="clearfix"></div>
                                    <br />
                                    <div id="div_quickTran" runat="server" visible="false">

                                        <div class="col-md-6">
                                            <label>
                                                Kitchen Print&nbsp;
                                        <asp:CheckBox ID="chk_kitchenPrint" runat="server" /></label>

                                        </div>
                                        <div class="col-md-6">
                                            <label>
                                                Receipt Print&nbsp;
                                        <asp:CheckBox ID="chk_ReceiptPrint" runat="server" /></label>

                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                    </div>

                                       <br />
                                    <div class="col-md-6">
                                        <label>
                                            Pay communicator Services - Till Request&nbsp;
                                        <asp:CheckBox ID="chk_TillReq" runat="server" /></label>


                                    </div>
                                    <br />
                                     <div class="col-md-6">
                                        <label>
                                            Pay communicator Services - Kiosk Request&nbsp;
                                        <asp:CheckBox ID="chk_kioskReq" runat="server" /></label>


                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            
                                        <asp:CheckBox Visible="false" ID="chk_IsKiosk" runat="server" OnCheckedChanged="chk_IsKiosk_CheckedChanged" AutoPostBack="true" /></label>
                                    </div>
                                    <div class="col-md-6" id="Div_kiosk" runat="server" visible="false">
                                         <asp:LinkButton ID="lnk_Settings" runat="server" class="btn btn-primary" OnClick="lnk_Settings_Click">
                                             <i class="fa fa-cog"></i>&nbsp;Kiosk Settings</asp:LinkButton>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />

                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <label>Receipt Header</label><div class="clearfix"></div>
                                            <telerik:RadEditor ID="txtReciptHeader" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="200px" Style="overflow: auto;">
                                            </telerik:RadEditor>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <%-- <div class="col-md-6">
                                        <label>
                                            Keymap Name &nbsp; 
                                        </label>
                                    </div>--%>
                                    <div class="col-md-6 table-responsive" style="overflow-x: auto; overflow-y: auto; width: 100%; height: 350px;">

                                        <asp:GridView ID="rdKeymap" runat="server" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="True" ShowGroupPanel="False"
                                            AllowFilteringByColumn="false" CssClass="Grid"
                                            EmptyDataText="No data in the data source." ShowHeaderWhenEmpty="True" Width="100%" GridLines="None">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Keymap Name">
                                                    <HeaderStyle Width="50%" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkKeymap" runat="server"></asp:CheckBox>
                                                        <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("name")%>' />
                                                        <asp:HiddenField ID="hdKeymap_id" runat="server" Value='<%# Eval("key_map_id")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shorting Number">
                                                    <HeaderStyle Width="50%" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtShortingNum" runat="server" Width="30%"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>


                                    </div>
                                    <div class="clearfix"></div>

                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="col-md-12">

                                        <div class="table-responsive">
                                            <telerik:RadGrid ID="rdMachineDetail" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="True" runat="server" ShowGroupPanel="False"
                                                AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                                Skin="MetroTouch">
                                                <ExportSettings FileName="Payment" IgnorePaging="false" ExportOnlyData="true">
                                                </ExportSettings>
                                                <ClientSettings>
                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="300px" />
                                                </ClientSettings>
                                                <PagerStyle Mode="NextPrevAndNumeric" />
                                                <GroupingSettings CaseSensitive="false" />
                                                <SortingSettings EnableSkinSortStyles="false" />
                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="true" TabIndex="0">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Virtual Printer" UniqueName="printer">
                                                            <HeaderStyle Width="50%" />
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblPrinter" runat="server" Text='<%# Eval("Printer")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdprinter_id" runat="server" Value='<%# Eval("printer_id")%>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Device" UniqueName="Device">
                                                            <HeaderStyle Width="50%" />
                                                            <ItemTemplate>
                                                                <telerik:RadComboBox ID="ddlDevice" runat="server"></telerik:RadComboBox>

                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />

                                <div class="col-md-6">
                                    <label>
                                        Table Sharing &nbsp;
                                        <asp:CheckBox ID="chkboxtablesharing" runat="server" Checked="true" /></label>
                                </div>

                                <div class="col-md-6">
                                    <label>
                                        Printer Sharing &nbsp;
                                        <asp:CheckBox ID="chkboxprintersharing" runat="server" Checked="true" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>
                                        Master Sync Time &nbsp;
                                        <asp:TextBox ID="txttime" runat="server" TextMode="Time" TimeFormat="HH:mm"></asp:TextBox>

                                    </label>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Back to main function on till &nbsp;
                                        <asp:CheckBox ID="chckbacktomainfun" runat="server" Checked="true" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Function Name</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="RadCombofunction" runat="server" Width="100%">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        No Logout/Hold sale &nbsp;
                                 <asp:CheckBox ID="chk_NoLogout" runat="server" /></label>
                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-6">
                                    <label>Table Transaction Limit</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txt_TranLimit" CssClass="form-control" Skin="" runat="server"
                                        placeholder="Table Transaction Limit" Width="100%">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Gateway TerminalID</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txt_gtway_TID" CssClass="form-control" Skin="" runat="server"
                                        placeholder="Gateway TerminalID" Width="100%">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                  <div class="col-md-6" style="float: left" >
                                    <label>POS Lite</label>&nbsp;
                                    <asp:CheckBox ID="chkPOSLite" runat="server" Checked="false"  />


                                </div>
                                 <div class="clearfix"></div>
                                <br />
                                        <div class="col-md-6" style="float: left">
                                    <label>Enable Sunmi Second Screen</label>&nbsp;
                                    <asp:CheckBox ID="chkSecondScreen" runat="server" Checked="false" />


                                </div>
                                <div class="col-md-12" id="divSecondScreen" style="padding: 1px; border:1px #808080 solid;">
                                     <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <br />
                                            <label>Upload Image 1 :  &nbsp;</label>

                                            <telerik:RadAsyncUpload ID="rauploadSSImage1" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" 
                                                MaxFileSize="2000000" OnClientValidationFailed="OnClientFileSize">
                                                <FileFilters>
                                                    <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                </FileFilters>
                                            </telerik:RadAsyncUpload>
                                            <i style="font-weight: lighter">
                                                <asp:Label ID="Label13" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                <asp:Label ID="Label14" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                            </i>
                                           <asp:Image ID="ImageTable" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />
                                            <telerik:RadBinaryImage ID="RadBinaryImage3" runat="server"  Visible="false" />
                                            <div class="clearfix"></div>
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                        </div>
                                       <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <br />
                                            <label>Upload Image 2 :  &nbsp;</label>

                                            <telerik:RadAsyncUpload ID="rauploadSSImage2" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="jpg,jpeg,png,gif" 
                                                MaxFileSize="2000000" OnClientValidationFailed="OnClientFileSize">
                                                <FileFilters>
                                                    <telerik:FileFilter Description="Images(jpeg;jpg;gif;png)" Extensions="jpg,jpeg,png,gif" />
                                                </FileFilters>
                                            </telerik:RadAsyncUpload>
                                            <i style="font-weight: lighter">
                                                <asp:Label ID="Label15" runat="server" Text="Select file to upload(.jpg, .jpeg, .gif, .png)"></asp:Label>
                                                <asp:Label ID="Label16" runat="server" Text="File size should not exceed 10Kb"></asp:Label>
                                            </i>
                                             <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="150px" Style="display: none" />
                                            <telerik:RadBinaryImage ID="RadBinaryImage4" runat="server" Visible="false" />
                                            <div class="clearfix"></div>
                                            <asp:HiddenField ID="HiddenField5" runat="server" />
                                        </div>
                                       <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="clearfix"></div>
                                            <br />
                                            <label>Upload Video :  &nbsp;</label>
                                             
                                            <telerik:RadAsyncUpload ID="rauploadSSvideo" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="1" AllowedFileExtensions="mp4" 
                                                MaxFileSize="5000000" OnClientValidationFailed="OnClientFileSize" OnFileUploaded="rauploadSSvideo_FileUploaded">
                                                <FileFilters>
                                                    <telerik:FileFilter Description="Video(MP4)" Extensions="mp4" />
                                                </FileFilters>
                                            </telerik:RadAsyncUpload>
                                            <i style="font-weight: lighter">
                                                <asp:Label ID="Label17" runat="server" Text="Select file to upload(.mp4)"></asp:Label>
                                                <asp:Label ID="Label18" runat="server" Text="File size should not exceed 5Mb"></asp:Label>
                                            </i>
                                            <telerik:RadBinaryImage ID="RadBinaryImage5" runat="server" Visible="false" />
                                            <div class="clearfix"></div>
                                            <asp:HiddenField ID="HiddenField6" runat="server" />
                                        </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-12" style="padding-top: 30px;">
                                        <label>Receipt Footer</label><div class="clearfix"></div>
                                        <telerik:RadEditor ID="txtReciptFooter" runat="server" ToolsFile="Controls/RadToolBar.xml" CssClass="form-control span12" Width="100%" Height="200px" Style="overflow: auto;">
                                        </telerik:RadEditor>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                                <%-- <div class="col-md-6">
                                    <label>
                                        Function Name &nbsp; 
                                    </label>
                                </div>
                                <div class="col-md-6" style="overflow-x: auto; overflow-y: auto; width: 100%; height: 300px;">

                                    <asp:RadioButtonList ID="RadCombofunction" class="mycheckbox" runat="server" EnableCheckAllItemsCheckBox="True" CheckBoxes="True" Width="100%"
                                        Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false" ExpandDirection="Down">
                                    </asp:RadioButtonList>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnRegisterforQR" class="btn btn-primary" Onclick="btnRegisterforQR_Click" runat="server" Text="Register For QR"   ToolTip="Click here to Register For QR" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Machine Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Machine Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Machine Details" />
                </div>
            </div>



        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>



</asp:Content>


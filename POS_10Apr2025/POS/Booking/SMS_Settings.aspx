<%@ Page Title="SMS Settings" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="SMS_Settings.aspx.vb" Inherits="BookingEasy_SMS_Settings"
    EnableEventValidation="false" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //        function ClientValidationFunction(sender, args) {
        //            debugger;
        //            var ele = sender.controltovalidate;
        //            var start = document.getElementById(ele.replace('EndTime', 'StartTime')).value.substring(11, 13);
        //            var end = document.getElementById(ele.replace('StartTime', 'EndTime')).value.substring(11, 13);

        //            if (start.length > 0 && end.length > 0) {

        //                start = Number(start);
        //                end = Number(end);

        //                if (start > end) {
        //                    args.IsValid = false;
        //                }
        //                else {
        //                    document.getElementById("ctl00_ContentPlaceHolder1_ttpStartTime1")).isvalid = 
        //                    args.IsValid = true;
        //                }
        //            }
        //            //2014-08-04-15-00-00
        //        } 
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /** Multiple rows and columns */
        .multipleRowsColumns .rcbItem, .multipleRowsColumns .rcbHovered
        {
            float: left;
            margin: 0 1px;
            min-height: 13px;
            overflow: hidden;
            padding: 2px 19px 2px 6px;
            width: 150px;
        }
        .rcbScroll
        {
            height: 200PX !important;
        }
        .rcbWidth
        {
            height: 200PX !important;
        }
    </style>
    <script language="Javascript">
       <!--
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode < 48 || charCode > 57)
                return false;
            return true;
        }
       //-->
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa-cog"></i>SMS Settings
                    </h1>
                    <h4>
                        Settings for SMS
                    </h4>
                </div>
            </div>
            <div id="divContent" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>SMS Information
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-12" id="div3" runat="server">
                                            <div id="divMessageBox" runat="server" visible="false">
                                                <asp:Label ID="lbExistingCust" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                   <%-- <div class="row">
                                        <div class="col-md-12">
                                            <div id="divMessageBox" runat="server" visible="False">
                                                <asp:Label ID="lblSaveField" runat="server" ForeColor="Green" Visible="false" Text="Field Save Successfully"></asp:Label>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-2 control-label">
                                                    SMS URL :</label>
                                                <div class="col-sm-9 col-lg-6 controls">
                                                    <asp:TextBox ID="txt_SMS_URL" runat="server" CssClass="form-control" Placeholder="SMS URL"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-lg-2 controls">
                                                    <asp:RequiredFieldValidator ID="require_SMS" runat="server" ValidationGroup="SMSSetting"
                                                        ControlToValidate="txt_SMS_URL" ForeColor="Red" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-2 control-label">
                                                    Username :</label>
                                                <div class="col-sm-9 col-lg-6 controls">
                                                    <asp:TextBox ID="txt_Username" runat="server" CssClass="form-control" Placeholder="Username"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-lg-2 controls">
                                                    <asp:RequiredFieldValidator ID="require_Username" runat="server" ValidationGroup="SMSSetting"
                                                        ControlToValidate="txt_Username" ForeColor="Red" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-2 control-label">
                                                    Password :</label>
                                                <div class="col-sm-9 col-lg-6 controls">
                                                    <asp:TextBox ID="txt_Password" runat="server" TextMode="Password" CssClass="form-control"
                                                        Placeholder="Password"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-lg-2 controls">
                                                    <asp:RequiredFieldValidator ID="require_Password" runat="server" ValidationGroup="SMSSetting"
                                                        ControlToValidate="txt_Password" ForeColor="Red" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-2 control-label">
                                                    SMS Sender Id :</label>
                                                <div class="col-sm-9 col-lg-6 controls">
                                                    <asp:TextBox ID="txt_Senderid" runat="server" CssClass="form-control" Placeholder="SMS Sender Id"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3 col-lg-2 controls">
                                                    <asp:RequiredFieldValidator ID="require_Senderid" runat="server" ValidationGroup="SMSSetting"
                                                        ControlToValidate="txt_Senderid" ForeColor="Red" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 col-lg-4 controls" style="text-align: center;">
                                            <asp:Button ID="btnSaveSetting" runat="server" Text="Save" CssClass="btn btn-success"
                                                ValidationGroup="SMSSetting" />
                                            <asp:Button ID="btnResetCancel" runat="server" Text="Cancel" CssClass="btn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <telerik:RadGrid ID="gvSMSSetting" runat="server" Skin="Office2010Blue" AutoGenerateColumns="false"
                                DataKeyNames="" Visible="false">
                                <ExportSettings HideStructureColumns="true">
                                </ExportSettings>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="No" UniqueName="No" AllowFiltering="false"
                                            Display="false">
                                            <ItemTemplate>
                                                <%# Container.DataSetIndex+1 %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="MailServer" HeaderText="MailServerUrl" />
                                        <telerik:GridBoundColumn DataField="MailServer_Username" HeaderText="Username" />
                                        <telerik:GridBoundColumn DataField="MailServer_Port" HeaderText="Port" />
                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false"
                                            ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%-- <asp:HiddenField ID="hdnbookingref" runat="server" Value='<%#Eval("bookingref")%>' />
                                                <asp:HiddenField ID="hdnClosed" runat="server" Value='<%#Eval("closed")%>' />
                                                <asp:HiddenField ID="hdnAllotted" runat="server" Value='<%#Eval("Allotted_Tables")%>' />--%>
                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="btnEdit" Text="Edit"
                                                    SortExpression="" CommandArgument='' EnableViewState="true"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Delete" UniqueName="Delete">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="btnDelete" Text="Delete"
                                                    SortExpression="" CommandArgument='' EnableViewState="true"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

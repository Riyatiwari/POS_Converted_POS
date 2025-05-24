<%@ Page Title="Payment Gateway" Language="VB" MasterPageFile="~/BookingEasy/Site.master" AutoEventWireup="false"
    CodeFile="PaymentGateway.aspx.vb" Inherits="BookingEasy_PaymentGateway" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa-cog"></i>Payment Gateway
                    </h1>
                </div>
            </div>
            <div id="divContent" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Gateway Master
                                </h3>
                            </div>
                            <div class="box-content" id="btn" runat="server">
                                <div class="row">
                                    <div style="text-align: right; margin-right: 2%">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                            <div class="box-content" id="divMessageBox" runat="server" visible="False">
                                <div class="row">
                                    <div>
                                        <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="box-content" id="AddField" runat="server">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Name :</label>
                                                <asp:HiddenField ID="hdnGatewayName" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtGatewayName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqGatewayName" runat="server" ControlToValidate="txtGatewayName"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="GatewaySetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Gateway :</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlGateway" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="Authorize.net" Text="Authorize.net" Selected="true"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="Paypal" Text="Paypal"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="FirstPayment" Text="FirstPayment"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Login ID :</label>
                                                <asp:HiddenField ID="hdnLoginId" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtLoginId" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqLoginId" runat="server" ControlToValidate="txtLoginId"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="GatewaySetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Transaction Key :</label>
                                                <asp:HiddenField ID="hdnTransactionKey" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtTransactionKey" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Password :</label>
                                                <asp:HiddenField ID="hdnPassword" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    URL :</label>
                                                <asp:HiddenField ID="hdnURL" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtURL" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvURL" runat="server" ControlToValidate="txtURL"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="GatewaySetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator> 
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Return URL :</label>
                                                <asp:HiddenField ID="hdnReturnURL" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtReturnURL" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Cancel URL :</label>
                                                <asp:HiddenField ID="hdnCancelURL" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtCancelURL" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Geo Zone :</label>
                                                <asp:HiddenField ID="hdnGeoZone" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtGeoZone" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Status :</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <telerik:RadDropDownList ID="ddlStatus" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="1" Text="Enable" Selected="true"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="0" Text="Disable"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Order No. :</label>
                                                <asp:HiddenField ID="hdnOrderNo" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Transaction Mode :</label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtTransactionMode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <%--<telerik:RadDropDownList ID="ddlTransactionMode" Skin="MetroTouch" runat="server"
                                                        Width="200px" DropDownHeight="200px" DefaultMessage="- Select -">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="Sendbox" Text="Sendbox"></telerik:DropDownListItem>
                                                            <telerik:DropDownListItem Value="Live" Text="Live"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Currency :</label>
                                                <asp:HiddenField ID="hdnCurrency" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Remark :</label>
                                                <asp:HiddenField ID="ddlRemark" runat="server" Value="0" />
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:TextBox ID="txtRemark" TextMode="multiline" Columns="50" Rows="5" runat="server"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label" style="text-align: left">
                                                </label>
                                                <div class="col-sm-9 col-lg-8 controls">
                                                    <asp:Button ID="btnSaveGatewaySetting" runat="server" Text="Save" ValidationGroup="GatewaySetting"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnCancelGatewaySetting" runat="server" Text="Cancel" CssClass="btn"
                                                        CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-content" id="GridField" runat="server">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <telerik:RadGrid ID="gvGateway" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                AllowSorting="false">
                                                <MasterTableView DataKeyNames="PaymentID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="Gateway" HeaderText="Gateway" SortExpression="Gateway"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="OrderNo" HeaderText="Order No" SortExpression="OrderNo"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="TransactionMode" HeaderText="Transaction Mode"
                                                            SortExpression="TransactionMode" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="Currency" HeaderText="Currency" SortExpression="Currency"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditTabName"
                                                            Text="Edit" HeaderText="Edit" HeaderStyle-Width="100px" ImageUrl="Images/Icons/edit.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteTabName"
                                                            Text="Delete" HeaderText="Delete" HeaderStyle-Width="100px" ImageUrl="Images/Icons/delete.png">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="custom_progress">
                <div id="overlay_load">
                </div>
                <div id="loading">
                    <img src="Images/ajax-loader.gif " alt="" />
                    <br />
                    Please Wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

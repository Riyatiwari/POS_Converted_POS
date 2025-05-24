<%@ Page Title="Multi Venue" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="MultiVenue.aspx.vb" Inherits="BookingEasy_MultiVanue" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <div>
                    <h1>
                        <i class="fa fa-cog"></i>Multi Venue
                    </h1>
                </div>
            </div>
            <div id="divContent" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Venue Details
                                </h3>
                            </div>
                            <div class="box-content" id="divAddbtn" runat="server">
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
                            <div class="box-content" id="AddDBDetails" runat="server" visible="false">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Venue :</label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <telerik:RadComboBox DropDownAutoWidth="Enabled" ID="ddlVenue" Skin="MetroTouch"
                                                        runat="server" Width="200px" DropDownWidth="200px" EmptyMessage="--Select--">
                                                        <%--<DefaultItem Text="--Select--" Value="0" Selected="true" />--%>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="--Select--" Value="0" Selected="true" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </div>
                                                <div class="col-sm-9 col-lg-3 controls">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlVenue"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="MultiVenueSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <%--<telerik:RadDropDownList ID="ddlVenue" Skin="MetroTouch" runat="server" Width="200px"
                                                        DropDownHeight="200px">
                                                        <Items>
                                                        <telerik:DropDownListItem Value="0" Text="--Select--" Selected="true"></telerik:DropDownListItem>
                                                        </Items>
                                                    </telerik:RadDropDownList>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Server Name :</label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <asp:TextBox ID="txtServerName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-9 col-lg-3 controls">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServerName"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="MultiVenueSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    User Name :</label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-9 col-lg-3 controls">
                                                    <asp:RequiredFieldValidator ID="reqUserName" runat="server" ControlToValidate="txtUserName"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="MultiVenueSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Password :</label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-9 col-lg-3 controls">
                                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="MultiVenueSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Database Name :</label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <asp:TextBox ID="txtDatabaseName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-9 col-lg-3 controls">
                                                    <asp:RequiredFieldValidator ID="reqDatabaseName" runat="server" ControlToValidate="txtDatabaseName"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="MultiVenueSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Sync Time :</label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <telerik:RadTimePicker ID="TimeOfCourse" runat="server" ShowPopupOnFocus="True" TimeView-TimeFormat="HH:mm"
                                                        Culture="en-GB">
                                                    </telerik:RadTimePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TimeOfCourse"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="MultiVenueSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Enable :</label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <asp:CheckBox ID="chkEnable" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                    Days of data sync :
                                                </label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <asp:TextBox ID="txtday" runat="server" CssClass="form-control" ToolTip="Insert days of data sync for first time synchronization"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3 control-label"  style="text-align: left">
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Value between 1 to 10 Only"
                                                        MinimumValue="1" MaximumValue="10" ForeColor="Red" ValidationGroup="MultiVenueSetting" type="integer" 
                                                        ControlToValidate="txtday" Display="Dynamic"></asp:RangeValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtday"
                                                        ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="MultiVenueSetting"
                                                        CssClass="rfv">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-lg-4 control-label" style="text-align: left">
                                                </label>
                                                <div class="col-sm-9 col-lg-5 controls">
                                                    <asp:Button ID="btnSaveSetting" runat="server" Text="Save" ValidationGroup="MultiVenueSetting"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnCancelSetting" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-content" id="divGridField" runat="server">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <telerik:RadGrid ID="gvMultiVanue" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                AllowSorting="false">
                                                <MasterTableView DataKeyNames="MultiVanueID">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="VenueName" HeaderText="Venue Name" SortExpression="VenueName"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="ServerName" HeaderText="Server Name" SortExpression="ServerName"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="UserName" HeaderText="User Name" SortExpression="UserName"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <telerik:GridBoundColumn DataField="DataBase_Name" HeaderText="DataBase Name" SortExpression="DataBase_Name"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                        <%--<telerik:GridBoundColumn DataField="Sync_Time" HeaderText="Sync Time" SortExpression="Sync_Time"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />--%>
                                                        <telerik:GridTemplateColumn HeaderText="Status" UniqueName="TemplateColumn" AllowFiltering="false"
                                                            DataField="Is_Active" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtn" runat="server" CommandName="btnChange" Text='<%#Eval("Is_Active")%>'
                                                                    SortExpression="Sync_Time" CommandArgument='<%#Eval("Is_Active")%>' EnableViewState="true"> </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
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
                                            <asp:HiddenField ID="hdnMultiVenue" runat="server" Value="0" />
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

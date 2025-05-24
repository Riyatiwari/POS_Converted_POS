<%@ Page Title="Custom Field" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="DynamicFieldMaster.aspx.vb" Inherits="BookingEasy_DynamicFieldMaster" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function openViewPage() {
            window.open('DynamicPage.aspx', 'ViewDetail', 'resizable=No, scrollbars=Yes, toolbar=no, menubar=no, location=no, directories=no, status=No', true)
            //         "viewDetail", "menubar=0,resizable=0,location=0,status=0", true)
        }
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div class="page-title">
                    <div>
                        <h1>
                            <i class="fa fa-cog"></i>Custom Field
                        </h1>
                    </div>
                </div>
                <div id="divContent" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-black">
                                <div class="box-title">
                                    <h3>
                                        <i class="fa fa-bars"></i>Field Master
                                    </h3>
                                </div>
                                <div class="box-content" id="divMessageBox" runat="server" visible="False">
                                    <div class="row">
                                        <div>
                                            <asp:Label ID="lblMessageBox" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-content" id="AddField" runat="server">
                                    <div class="form-horizontal" style="width: 60%; float: left">
                                        <div class="row">
                                            <div class="col-md-7">
                                                <div class="form-group">
                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                        Category Type :</label>
                                                    <div class="col-sm-8 col-lg-8 controls">
                                                        <telerik:RadDropDownList OnSelectedIndexChanged="RadDropdowntype_SelectedIndexChanged" AutoPostBack="true" ID="RadDropdowntype" Skin="MetroTouch" runat="server" Width="200px">
                                                            <Items>
                                                                <telerik:DropDownListItem Value="B" Text="Booking"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="C" Text="Click And Collect"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="D" Text="Delivery"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="T" Text="Order At Table"></telerik:DropDownListItem>
                                                            </Items>
                                                        </telerik:RadDropDownList>
                                                        <asp:Button ID="btnDefaultValue" OnClick="btnDefaultValue_Click" runat="server" Text="Add Default Columns" CssClass="btn"  />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-7">
                                                <div class="form-group">
                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                        Field :</label>
                                                    <div class="col-sm-8 col-lg-8 controls">
                                                        <telerik:RadDropDownList ID="ddlStaticField" Skin="MetroTouch" runat="server" Width="200px"
                                                            DropDownHeight="200px">
                                                            <Items>
                                                                <telerik:DropDownListItem Value="First Name" Text="First Name" Selected="true"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Last Name" Text="Last Name"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Mobile" Text="Mobile"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Work Number" Text="Work Number"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Email ID" Text="Email ID"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Address 1" Text="Address 1"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Address 2" Text="Address 2"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Address 3" Text="Address 3"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="City" Text="City"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Post Code" Text="Post Code"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="Custom" Text="Custom"></telerik:DropDownListItem>
                                                            </Items>
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <div class="col-sm-9 col-lg-8 controls">
                                                        <asp:Button ID="btnAddField" runat="server" Text="Add" CssClass="btn btn-primary"
                                                            ValidationGroup="btnField" />
                                                        <asp:Button ID="btnCancelField" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                                                            Visible="false" ValidationGroup="btnField" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="divFieldType" runat="server" visible="false">
                                            <div class="col-md-7">
                                                <div class="form-group">
                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                        Field Type:</label>
                                                    <div class="col-sm-8 col-lg-8 controls">
                                                        <telerik:RadDropDownList ID="ddlField" Skin="MetroTouch" runat="server" Width="200px"
                                                            DropDownHeight="200px">
                                                            <Items>
                                                                <telerik:DropDownListItem Value="1" Text="Textbox" Selected="true"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="2" Text="Checkbox"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="3" Text="Dropdown"></telerik:DropDownListItem>
                                                                <telerik:DropDownListItem Value="4" Text="Date"></telerik:DropDownListItem>
                                                            </Items>
                                                        </telerik:RadDropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <div class="col-sm-9 col-lg-8 controls">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="btnField" />
                                                        <asp:Button ID="btnCancelTabSetting" runat="server" Text="Cancel" CssClass="btn"
                                                            CausesValidation="false" Visible="false" ValidationGroup="btnField" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btntxt">
                                                <div id="DivTextBoxField" runat="server" visible="false" style="width: 99%;">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="box box-black">
                                                                <div class="box-title">
                                                                    <h3>Textbox
                                                                    </h3>
                                                                </div>
                                                                <div class="box-content" id="TextField" runat="server">
                                                                    <div class="form-horizontal">
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Label :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtTextboxLable" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="reqLable" runat="server" ControlToValidate="txtTextboxLable"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="txt" CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Type :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadDropDownList ID="ddlType" Skin="MetroTouch" runat="server" Width="200px"
                                                                                            DropDownHeight="200px">
                                                                                            <Items>
                                                                                                <telerik:DropDownListItem Value="1" Text="Single Line" Selected="true"></telerik:DropDownListItem>
                                                                                                <telerik:DropDownListItem Value="2" Text="Multi Line"></telerik:DropDownListItem>
                                                                                                <telerik:DropDownListItem Value="3" Text="Password"></telerik:DropDownListItem>
                                                                                            </Items>
                                                                                        </telerik:RadDropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Required :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Validation :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadDropDownList ID="reqValidation" Skin="MetroTouch" runat="server" Width="200px"
                                                                                            DropDownHeight="200px" AutoPostBack="true">
                                                                                            <Items>
                                                                                                <telerik:DropDownListItem Value="1" Text="Non" Selected="true"></telerik:DropDownListItem>
                                                                                                <telerik:DropDownListItem Value="2" Text="Email ID"></telerik:DropDownListItem>
                                                                                                <telerik:DropDownListItem Value="3" Text="Number"></telerik:DropDownListItem>
                                                                                            </Items>
                                                                                        </telerik:RadDropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Maximum :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtTextboxMaximum" runat="server" CssClass="form-control" ValidationGroup="txt"
                                                                                            onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ForeColor="Red"
                                                                                            ControlToValidate="txtTextboxMaximum" ValidationGroup="txt" Enabled="true" Display="Dynamic"></asp:RangeValidator>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTextboxMaximum"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="txt" CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-5">
                                                                                <div class="form-group">
                                                                                    <div>
                                                                                        <label class="col-xs-4 col-lg-4 control-label" style="text-align: left" runat="server"
                                                                                            id="lblTxtMax" visible="true">
                                                                                        </label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Sorting No :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtTextboxSortingNo" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTextboxSortingNo"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="txt" CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="txtStatus" runat="server">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Status :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadDropDownList ID="ddlTextboxStatus" Skin="MetroTouch" runat="server" Width="200px"
                                                                                            DropDownHeight="200px">
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
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <div class="col-xs-4 col-lg-4 control">
                                                                                        &nbsp;
                                                                                    </div>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:Button ID="btntxt" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="txt" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnCheckBox">
                                                <div id="DivCheckboxField" runat="server" visible="false" style="width: 99%;">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="box box-black">
                                                                <div class="box-title">
                                                                    <h3>CheckBox
                                                                    </h3>
                                                                </div>
                                                                <div class="box-content" id="CheckBox" runat="server">
                                                                    <div class="form-horizontal">
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Label :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtCheckboxLbl" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCheckboxLbl"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="check" CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Items :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtCheckboxChoiceItems" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnChackboxItems" runat="server" Value="0" />
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCheckboxChoiceItems"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="checkItem"
                                                                                            CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-5">
                                                                                <div class="form-group">
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:Button ID="addChkItems" runat="server" Text="Add" CssClass="btn btn-primary"
                                                                                            ValidationGroup="checkItem" />
                                                                                        <asp:Button ID="clrChkItems" runat="server" Text="Clear" CssClass="btn" CausesValidation="false"
                                                                                            Visible="false" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                    </label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadGrid ID="gvCheckBox" runat="server" AutoGenerateColumns="true" Skin="Office2010Blue"
                                                                                            AllowSorting="false" Visible="false">
                                                                                            <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                                                            </ClientSettings>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Sorting No :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtCheckBoxSorting" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCheckBoxSorting"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="check" CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="CheckBoxStatus" runat="server">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Status :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadDropDownList ID="ddlCheckBoxStatus" Skin="MetroTouch" runat="server"
                                                                                            Width="200px" DropDownHeight="200px">
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
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <div class="col-xs-4 col-lg-4 control">
                                                                                        &nbsp;
                                                                                    </div>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:Button ID="btnCheckBox" runat="server" Text="Submit" CssClass="btn btn-primary"
                                                                                            ValidationGroup="check" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel3" runat="server" DefaultButton="btnDropDown">
                                                <div id="DivDropdownField" runat="server" visible="false" style="width: 99%;">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="box box-black">
                                                                <div class="box-title">
                                                                    <h3>DropDown
                                                                    </h3>
                                                                </div>
                                                                <div class="box-content" id="Div1" runat="server">
                                                                    <div class="form-horizontal">
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Label :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtLableDropdown" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLableDropdown"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="ddl" CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Required :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:CheckBox ID="chkDropdownReq" runat="server" ValidationGroup="ddl" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Items :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtItemsDropdown" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <label id="lblError" class="col-xs-4 col-lg-4 control-label" style="text-align: left; color: Red;"
                                                                                            runat="server" visible="false">
                                                                                            *</label>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtItemsDropdown"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="ddlItem"
                                                                                            CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-5">
                                                                                <div class="form-group">
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:Button ID="addDropdownItems" runat="server" Text="Add" CssClass="btn btn-primary"
                                                                                            ValidationGroup="ddlItem" />
                                                                                        <asp:Button ID="clrDropdownItems" runat="server" Text="Clear" CssClass="btn" CausesValidation="false"
                                                                                            Visible="false" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="divDDLChoice" runat="server">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                    </label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadGrid ID="gvDropDown" runat="server" AutoGenerateColumns="true" Skin="Office2010Blue"
                                                                                            AllowSorting="false" Visible="false">
                                                                                            <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                                                            </ClientSettings>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Sorting No :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtDropDownSorting" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDropDownSorting"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="ddl" CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="DropDownStatus" runat="server">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Status :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadDropDownList ID="txtDropDownStatus" Skin="MetroTouch" runat="server">
                                                                                            <%--Width="200px" DropDownHeight="200px"--%>
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
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <div class="col-xs-4 col-lg-4 control">
                                                                                        &nbsp;
                                                                                    </div>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:Button ID="btnDropDown" runat="server" Text="Submit" CssClass="btn btn-primary"
                                                                                            ValidationGroup="ddl" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server" DefaultButton="btnDate">
                                                <div id="DivDateTime" runat="server" visible="false" style="width: 99%;">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="box box-black">
                                                                <div class="box-title">
                                                                    <h3>Date
                                                                    </h3>
                                                                </div>
                                                                <div class="box-content" id="Div2" runat="server">
                                                                    <div class="form-horizontal">
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Label :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtDTLbl" runat="server" CssClass="form-control" ValidationGroup="DateTime"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDTLbl"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="DateTime"
                                                                                            CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Required :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:CheckBox ID="chkDateReq" runat="server" ValidationGroup="DateTime" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Sorting No :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:TextBox ID="txtDatetimeSorting" runat="server" CssClass="form-control" ValidationGroup="DateTime"
                                                                                            onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDatetimeSorting"
                                                                                            ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="DateTime"
                                                                                            CssClass="rfv">
                                                                                        </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="DateStatus" runat="server">
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <label class="col-xs-4 col-lg-4 control-label" style="text-align: left">
                                                                                        Status :</label>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <telerik:RadDropDownList ID="ddlDateStatus" Skin="MetroTouch" runat="server" Width="200px"
                                                                                            DropDownHeight="200px" ValidationGroup="DateTime">
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
                                                                            <div class="col-md-7">
                                                                                <div class="form-group">
                                                                                    <div class="col-xs-4 col-lg-4 control">
                                                                                        &nbsp;
                                                                                    </div>
                                                                                    <div class="col-sm-8 col-lg-8 controls">
                                                                                        <asp:Button ID="btnDate" runat="server" Text="Submit" CssClass="btn btn-primary"
                                                                                            ValidationGroup="DateTime" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div>
                                        <a runat="server" style="font-size: larger;" id="pageopen" onclick="openViewPage()"
                                            onmouseover="this.style.cursor='pointer';">View Actual Page</a>
                                    </div>
                                    <br />
                                    <div id="diviFrame" runat="server" style="width: 40%; margin-left: 60%;">
                                        <iframe src="DynamicPage.aspx?cat=B" id="ifrmaCustomer" runat="server" width="100%" height="550px" style="border-width: 0;"></iframe>
                                    </div>
                                </div>
                                <div class="box-content" id="GridField" runat="server">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <telerik:RadGrid ID="gvField" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                    AllowSorting="false" AllowPaging="True" AllowFilteringByColumn="True">
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView DataKeyNames="FieldID">
                                                        <Columns>
                                                            <%--<telerik:GridBoundColumn DataField="FieldName" HeaderText="Field Type" SortExpression="FieldName"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />--%>
                                                            <telerik:GridTemplateColumn HeaderText="Field Type" UniqueName="FieldName" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-Width="200px" ShowFilterIcon="false" DataField="FieldName"
                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="HiddenField0" runat="server" Value='<%#Eval("Static_Field")%>' />
                                                                    <%#Eval("FieldName")%>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="FieldLable" HeaderText="Label" SortExpression="FieldLable"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                            <telerik:GridNumericColumn DataField="Sorting_no" HeaderText="Sorting No" SortExpression="Sorting_no"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" DataType="System.Decimal"
                                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" Aggregate="Sum" DataFormatString="{0}" />
                                                            <%--<telerik:GridNumericColumn DataField="Enable_Val" HeaderText="Enable" SortExpression="Enable_Val"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" DataType="System.Decimal"
                                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" Aggregate="Sum" DataFormatString="{0}" />--%>
                                                            <telerik:GridBoundColumn DataField="Enable_Val" HeaderText="Enable" SortExpression="Enable_Val"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                            <telerik:GridBoundColumn DataField="categoty_type" HeaderText="Category Type" SortExpression="categoty_type"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" ShowFilterIcon="false"
                                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditField"
                                                                Text="Edit" HeaderText="Edit" HeaderStyle-Width="100px" ImageUrl="Images/Icons/edit.png">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridButtonColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteField"
                                                                Text="Delete" HeaderText="Delete" HeaderStyle-Width="100px" ImageUrl="Images/Icons/delete.png">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings Selecting-AllowRowSelect="False" EnablePostBackOnRowClick="False">
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                                <asp:HiddenField ID="hdnUpdate" runat="server" Value="0" />
                                                <br />
                                                <div class="form-group">
                                                    <div class="col-md-5">
                                                        <%--<div style="width:15px; height:15px; background-color:Blue;"></div>--%>
                                                        <%--<asp:Panel ID="Panel5" runat="server" BackColor="SkyBlue" Width="11px" Height="11px"></asp:Panel>--%>
                                                        *
                                                        <asp:Image ID="Image1" runat="server" BackColor="SkyBlue" Width="10px"
                                                            Height="10px" Style="border: none;" />
                                                        Static Field
                                                    </div>
                                                </div>
                                            </div>
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

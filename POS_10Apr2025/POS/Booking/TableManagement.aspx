<%@ Page Title="Table Management" Language="VB" MasterPageFile="~/BookingEasy/Site.master"
    AutoEventWireup="false" CodeFile="TableManagement.aspx.vb" Inherits="BookingEasy_Settings2"
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
            height:200PX !Important;
        }
        .rcbWidth
        {
            height:200PX !Important;
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
                        <i class="fa fa-cog"></i>Table Management
                    </h1>
                    <h4>
                        Settings for Tables.
                    </h4>
                </div>
            </div>
            <div id="divContent" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-black">
                            <div class="box-title">
                                <h3>
                                    <i class="fa fa-bars"></i>Table Management
                                </h3>
                            </div>
                            <div class="box-content">
                                <div class="form-horizontal">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                Table Details
                                            </h4>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div id="divMsgErr" runat="server" visible="False">
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="col-xs-3 col-lg-6 control-label">
                                                            Table No:
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTableNo"
                                                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TableDetails"
                                                                CssClass="rfv">
                                                            </asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-9 col-lg-6 controls">
                                                            <asp:TextBox ID="txtTableNo" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-xs-3 col-lg-4 control-label">
                                                            Table Name:
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTableName"
                                                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TableDetails"
                                                                CssClass="rfv">
                                                            </asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-9 col-lg-8 controls">
                                                            <asp:TextBox ID="txtTableName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdn_Table_id" runat="server" Value="0" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-xs-3 col-lg-5 control-label">
                                                            Allowed Join Tables:
                                                        </label>
                                                        <div class="col-sm-9 col-lg-7 controls">
                                                            <telerik:RadComboBox ID="ddlTableJoin" Skin="MetroTouch" runat="server" Width="200px"
                                                                DropDownCssClass="multipleRowsColumns" AllowCustomText="true" EnableScreenBoundaryDetection="false"
                                                                EnableCheckAllItemsCheckBox="True" DropDownWidth="480px" CheckBoxes="True" Filter="Contains"
                                                                DropDownHeight="200px">
                                                            </telerik:RadComboBox>
                                                            <%--<telerik:RadComboBox ID="radProblem" runat="server" EnableCheckAllItemsCheckBox="True"
                                                                CheckBoxes="True" Filter="StartsWith" AllowCustomText="true" EnableScreenBoundaryDetection="false"
                                                                ExpandDirection="Down">
                                                            </telerik:RadComboBox>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="col-xs-3 col-lg-5 control-label">
                                                            Min Covers:
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMinCovers"
                                                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TableDetails"
                                                                CssClass="rfv">
                                                            </asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-9 col-lg-7 controls">
                                                            <asp:TextBox ID="txtMinCovers" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label class="col-xs-3 col-lg-5 control-label">
                                                            Max Covers:
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMaxCovers"
                                                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="TableDetails"
                                                                CssClass="rfv">
                                                            </asp:RequiredFieldValidator></label>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="TableDetails" ErrorMessage="Max Value can't be less" ControlToCompare="txtMaxCovers" ControlToValidate="txtMinCovers" Type="Integer" Operator="LessThan"></asp:CompareValidator>
                                                        <div class="col-sm-9 col-lg-7 controls">
                                                            <asp:TextBox ID="txtMaxCovers" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group" style="text-align: center;">
                                                    <asp:Button ID="btnSaveTable" runat="server" Text="Save" ValidationGroup="TableDetails"
                                                        CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnCancelTable" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <telerik:RadGrid ID="gvTable" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                        AllowPaging="true" PageSize="10" AllowSorting="true">
                                                        <MasterTableView DataKeyNames="Table_id">
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="TableNo" HeaderText="Table No" SortExpression="TableNo"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle Width="10%" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Table_name" HeaderText="Table Name" SortExpression="Table_name"
                                                                    HeaderStyle-HorizontalAlign="Center" AllowSorting="false">
                                                                    <HeaderStyle Width="20%" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="MinCover" HeaderText="Min Cover" SortExpression="MinCover"
                                                                    HeaderStyle-HorizontalAlign="Center" AllowSorting="false">
                                                                    <HeaderStyle Width="10%" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="MaxCover" HeaderText="Max Cover" SortExpression="MaxCover"
                                                                    HeaderStyle-HorizontalAlign="Center" AllowSorting="false">
                                                                    <HeaderStyle Width="10%" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="TableNames" HeaderText="AllowedJoin" SortExpression="TableNames"
                                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" AllowSorting="false">
                                                                    <HeaderStyle Width="30%" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditTableName"
                                                                    Text="Edit" HeaderText="Edit" HeaderStyle-Width="10%" ImageUrl="Images/Icons/edit.png">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteTableName"
                                                                    Text="Delete" HeaderText="Delete" HeaderStyle-Width="10%" ImageUrl="Images/Icons/delete.png">
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
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                Group Details
                                            </h4>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div id="divGMsgerr" runat="server" visible="False">
                                                    <asp:Label ID="lblGmsg" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-xs-3 col-lg-4 control-label">
                                                            Group Name:
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGroupName"
                                                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="GroupDetails"
                                                                CssClass="rfv">
                                                            </asp:RequiredFieldValidator></label>
                                                        <div class="col-sm-9 col-lg-8 controls">
                                                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdn_group_id" runat="server" Value="0" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-xs-3 col-lg-4 control-label">
                                                            Table Set:
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTableSet"
                                                                ForeColor="Red" ErrorMessage="*" Display="Dynamic" ValidationGroup="GroupDetails"
                                                                CssClass="rfv">
                                                            </asp:RequiredFieldValidator>
                                                        </label>
                                                        <div class="col-sm-9 col-lg-8 controls">
                                                            <telerik:RadComboBox ID="ddlTableSet" Skin="MetroTouch" runat="server" Width="200px"
                                                                DropDownCssClass="multipleRowsColumns" AllowCustomText="true" EnableScreenBoundaryDetection="false"
                                                                EnableCheckAllItemsCheckBox="True" DropDownWidth="480px" CheckBoxes="True" Filter="Contains"
                                                                DropDownHeight="200px">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <div class="col-sm-9 col-lg-12 controls">
                                                            <asp:Button ID="btnSaveGroup" runat="server" Text="Save" ValidationGroup="GroupDetails"
                                                                CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnCancelGroup" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <telerik:RadGrid ID="gvGroup" runat="server" AutoGenerateColumns="false" Skin="Office2010Blue"
                                                        AllowSorting="false" AllowPaging="true" PageSize="10">
                                                        <MasterTableView DataKeyNames="GroupID">
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="group_name" HeaderText="Group Name" SortExpression="group_name"
                                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                                <telerik:GridBoundColumn DataField="TableNames" HeaderText="Table Set" SortExpression="TableNames"
                                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnEdit" CommandName="EditGroup"
                                                                    Text="Edit" HeaderText="Edit" HeaderStyle-Width="100px" ImageUrl="Images/Icons/edit.png">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </telerik:GridButtonColumn>
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnDelete" CommandName="DeleteGroup"
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

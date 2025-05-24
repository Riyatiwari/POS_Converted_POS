<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Auto_Sync_Recored.aspx.vb" Inherits="Auto_Sync_Recored" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Auto Sync Recored
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>

            <li class="active">Auto Sync Record</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12 ">
        <div class="panel panel-yellow">
            <div class="panel-heading">Auto Sync Record</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-12">
                                 <asp:LinkButton ID="lnkSyncHistory" runat="server" OnClick="lnkSyncHistory_Click" class="btn btn-primary"><i class="fa fa-list"></i>&nbsp;Sync History</asp:LinkButton>
                        
                            </div>
                            <div class="clearfix"></div>
                            <br />

                            <div class="col-lg-9">
                                <div class="col-md-4">
                                    <label>Venue</label><div class="clearfix"></div>
                                    <asp:DropDownList ID="ddlVenue" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlVenue_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label>Location</label><div class="clearfix"></div>
                                    <asp:DropDownList ID="ddllocation" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <br />
                                <div class="clearfix"></div>
                                <br />
                                <br />
                              
                                <div class="col-md-12">
                                    <label>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                        Select All 
                                    </label>
                                </div>
                                <div class="clearfix"></div>
                                 <br />
                                <div class="col-md-12">
                                    <asp:DataList ID="DL_Pages" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPages" runat="server"></asp:CheckBox>
                                            <asp:Label ID="lbl_PageName" runat="server" Text='<%# Eval("Page_name")%>' />
                                            <asp:HiddenField ID="hdPage_ID" runat="server" Value='<%# Eval("Page_ID")%>' />
                                            <asp:HiddenField ID="hd_PageName" runat="server" Value='<%# Eval("Page_name")%>' />

                                        </ItemTemplate>
                                    </asp:DataList>

                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="col-lg-8" style="text-align: center;">
                                    <label style="font-size: 14px; font-weight: bold;">Till</label>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-lg-8" style="height: 250px; border: 1px solid black;">

                                    <asp:DataList ID="ddlTill" runat="server" RepeatDirection="Vertical" Width="100%">
                                        <ItemTemplate>
                                            <div>
                                                <asp:HiddenField Value='<%# Eval("machine_id") %>' runat="server" ID="hd_machineID" />
                                                <asp:CheckBox runat="server" ID="chk_till" />
                                                <asp:Label runat="server" ID="lblTillName" Text='<%# Eval("name") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />

                        </div>
                    </div>
                </div>
            </div>
            <div class="form-actions text-right pal">
            <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>


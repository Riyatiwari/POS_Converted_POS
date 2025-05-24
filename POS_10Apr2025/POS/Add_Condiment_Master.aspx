<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master"  CodeFile="Add_Condiment_Master.aspx.vb" Inherits="Add_Condiment_Master" %>



 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Condiment List
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Condiment Master</li>
        </ol>
    </div>
   
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />

    <div class="col-lg-12">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Condiment Master</div>
                <div class="panel-body pan">
                    <div class="form-body pal">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click" class="btn btn-primary"><i class="fa fa-plus"></i>&nbsp;New Condiment</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="divCondiment" runat="server">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                                           <div class="form-group">
                                                    <div class="col-md-12" style="overflow-y:auto;overflow-x:auto;width:100%;">
                                                        <telerik:RadGrid ID="rdcondiment" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                            PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1" OnItemDataBound="rdcondiment_ItemDataBound2"
                                                            PagerStyle-AlwaysVisible="false" Skin="MetroTouch" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" Width="100%">
                                                            <ClientSettings>
                                                             
                                                                <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                                            </ClientSettings>
                                                            <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false">
                                                                <Columns>
                                                                    
                                                                   
                                                                    <telerik:GridTemplateColumn HeaderText="condiment Name *" UniqueName="condimentName">
                                                                        <HeaderStyle Width="20%" />
                                                                        <ItemTemplate>
                                                                          
                                                                           <asp:HiddenField ID="hdnUnit_Id" runat="server" Value='<%#Eval("name")%>' />
                                                                            <telerik:RadTextBox ID="txtcondimnt" runat="server" Width="100%"  AutoPostBack="true"></telerik:RadTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                  <telerik:GridTemplateColumn HeaderText="Product Group" UniqueName="ProductGroup">
                                                                        <HeaderStyle Width="20%" />
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:HiddenField ID="hdn_prodgroup" runat="server" Value='<%#Eval("category_id")%>' />
                                                                            <telerik:RadComboBox ID="ddlprogroup" runat="server" Width="100%"  AutoPostBack="true" OnSelectedIndexChanged="ddlprogroup_SelectedIndexChanged"></telerik:RadComboBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Product" UniqueName="Product">
                                                                        <HeaderStyle Width="20%" />
                                                                        <ItemTemplate>
                                                                          
                                                                            <asp:HiddenField ID="hdn_pro" runat="server" Value='<%#Eval("product_id")%>' />
                                                                            <telerik:RadComboBox ID="ddlpro" runat="server" Width="100%"></telerik:RadComboBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                            
                                                        
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </div>
                                                </div>
                                            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Allergy Details" OnClick="btnSave_Click"/>&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Allergy Details" />&nbsp;&nbsp;&nbsp;
            
                       <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Allergy Details" />
                </div>
            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


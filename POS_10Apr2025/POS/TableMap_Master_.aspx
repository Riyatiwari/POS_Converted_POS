<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="TableMap_Master_.aspx.vb" Inherits="TableMap_Master_" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12 ">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
            <div class="panel panel-yellow">
                <div class="panel-heading">Table Map Master</div>
                <div class="panel-body pan">

                    <div class="form-body pal">
                        <div class="row">

                            <div class="form-group">
                                <div class="col-md-12">

                                    <asp:DataList ID="dlButton" runat="server" RepeatColumns="10" OnItemCommand="dlButton_ItemCommand" RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <th width="350px;">
                                                        <asp:Button ID="btn_10by10" class="btn btn-primary" runat="server" Text=""
                                                            Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;"/>
                                                        <asp:HiddenField runat="server" ID="hd_10by10" Value='<%#Eval("id")%>' />
                                                    </th>
                                                    &nbsp;
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>

                                </div>
                                <div class="clearfix"></div>
                                

                                <asp:Button ID="btn_save" class="btn btn-primary" runat="server" Text="Save" OnClick="btn_save_Click" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </telerik:RadAjaxPanel>
    </div>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


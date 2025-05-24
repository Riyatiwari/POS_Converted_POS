<%@ Page Title="PDF Demo" Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master"  CodeFile="PDF_Demo.aspx.vb" Inherits="PDF_Demo" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Stock Purchase List
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div>
            <asp:LinkButton ID="LinkButton1" ValidationGroup="ValidPayroll" runat="server" Text="View"
                                    CssClass="button medium blue" ToolTip="Click to View Report"
                                    meta:resourcekey="btnApplyResource1"> 
                                </asp:LinkButton>

        </div>
        <div>
             <asp:LinkButton ID="LinkButton2" ValidationGroup="ValidPayroll" runat="server" Text="Export To PDF"
                                    CssClass="button medium blue" ToolTip="Click to View Report" Visible="false"
                                    meta:resourcekey="btnApplyResource1"> 
                                </asp:LinkButton>
        </div>
        <div>
            <telerik:ReportViewer ID="ReportViewer1" runat="server" Style="border: 1px solid #ccc;"
                            Width="90%" Height="500px" Visible="false" Resources-ExportSelectFormatText="PDF" />
        </div>
</asp:Content>

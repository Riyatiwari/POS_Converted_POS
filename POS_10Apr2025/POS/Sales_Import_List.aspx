<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Sales_Import_List.aspx.vb" Inherits="Sales_Import_List" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Sales Import
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;<i
                class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Sales Import</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/Telerik_Report_Style.css" rel="stylesheet" type="text/css" />
    <br />
    <div class="col-lg-12" id="divFunction" runat="server">

        <div class="panel panel-yellow">
            <div class="panel-heading">Sales Import </div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                        </div>
                    </div>
                    <br />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>

                    <div class="row">
                        <center>
                                <div id="divfrom" runat="server" >
                            <div class="col-lg-6 ">
                                Upload : &nbsp;
                               
                                      </div> <div class="col-lg-6 ">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                               </div>  
                                  
                                
                                <div class="clearfix"></div>
                                 <br />  
                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-outline btn-primary" ToolTip="Upload" ValidationGroup="valid">Upload</asp:LinkButton>
                                        <asp:LinkButton ID="btnSave" runat="server" class="btn btn-outline btn-primary" ToolTip="Save" ValidationGroup="valid" Visible="false">Save</asp:LinkButton>
                            <asp:LinkButton ID="btnClear" runat="server" class="btn btn-outline btn-primary" ToolTip="Clear" ValidationGroup="valid">Clear</asp:LinkButton>&nbsp;&nbsp;
                             <a href="../Files/Sales_Import_Data.xlsx" style="color: blue; text-decoration: underline;  margin-top: 10px;">Download Template</a>
                                     
                              <br /><br />
                              <br /><br />
                                 
                              <div class="col-lg-12 " >

                           
                               

                                  <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="True" Width="100%" ShowFooter="true"   AllowPaging="false"  CssClass="mGrid"
    PagerStyle-CssClass="pgr"
    AlternatingRowStyle-CssClass="alt">
                                    
                                     
                                  </asp:GridView>
                                    </div>
                              </center>
                    </div>
                </div>
            </div>

        </div>


    </div>



    <style>
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            .mGrid th {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .mGrid .alt {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }

            .mGrid .pgr {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>

</asp:Content>

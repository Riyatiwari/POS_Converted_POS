<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Product_Import.aspx.vb" Inherits="Product_Import" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Product Import
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Product_List.aspx">Product List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Product Import</li>
        </ol>
    </div>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:HiddenField ID="hdCondiments" runat="server" Value="0" />
    <div class="col-lg-12">

        <div class="panel panel-yellow">
            <div class="panel-heading">Product Import</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <div class="row">
                                            <center>
                                                 <div class="col-lg-4" style="text-align:right;">
                                                     Import file :   &nbsp;&nbsp;
                                                 </div>
                                                <div class="col-lg-4" style="text-align:left;">
                                                      <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </div>
                                                <div class="col-lg-4" style="text-align:left;">
                                                     <a href="../Files/Product_Sample.xlsx" style="color: blue; text-decoration: underline; margin-top: 10px;">Download Template</a>

                                                </div>
                                                 </center>
                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <br />

                                        <div  style="text-align: center; margin-right: 20px;">
                                            <asp:LinkButton ID="btnUpload" runat="server" class="btn btn-primary" Text="Submit" OnClick="btnUpload_Click"></asp:LinkButton>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <br />

                        <div class="col-lg-12 ">
                            <div id="dv_Mapping" style="float: left; margin-top: 10px;" runat="server" visible="false">
                                <table width="100%">
                                    <tr>
                                        <td width="20%">
                                            <label>Product Group Category</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlproductGroupCategory" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Product Group</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlproductGroup" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Product Name</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlproductName" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Price</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlPrice" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Unit</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddlUnit" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;"></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <label>Base Size</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_BaseSize" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Department</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_department" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Printer 1</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_printer1" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Printer 2</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_printer2" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Printer 3</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_printer3" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>

                                        <td width="20%">
                                            <label>Size 1 Name</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size1Name" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 1 Qty</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size1" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 1 Unit</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size1unit" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 1 Price</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size1price" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>



                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 2 Name</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size2Name" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <label>Size 2 Qty</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size2" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 2 Unit</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size2unit" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>


                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 2 Price</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size2price" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>


                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 3 Name</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size3Name" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 3 Qty</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size3" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>

                                        <td width="20%">
                                            <label>Size 3 Unit</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size3unit" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>


                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 3 Price</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size3price" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>


                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 4 Name</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size4Name" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 4 Qty</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size4" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td width="20%" style="padding-left: 20px;">
                                            <label>Size 4 Unit</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size4unit" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <label>Size 4 Price</label>
                                            <div class="clearfix"></div>
                                            <asp:DropDownList ID="ddl_size4price" runat="server" Width="100%">
                                                <asp:ListItem Text="--select--" Value="0" Selected="true"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>


                                    </tr>
                                </table>
                                <div class="clearfix"></div>
                                <br />
                                <br />
                                <div style="text-align: center;">
                                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Style="margin-left: 10px; margin-top: 20px;" class="btn btn-primary"><i class="fa fa-copy"></i>&nbsp;Save</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>


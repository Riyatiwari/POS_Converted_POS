<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Kiosk_Settings.aspx.vb" Inherits="Kiosk_Settings" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Kiosk Setting Master
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Machine_List.aspx">Till List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active"><a href="Machine_Master.aspx">Till Master</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Kiosk Setting Master</li>
        </ol>
    </div>
    <script>
       <%-- function OnClientFileSize() {
            const fi = document.getElementById("<%=fileupload.ClientID%>");
            const fsize = fi.files.item(0).size;
            const file = Math.round((fsize / 1024));
            if (file > 5) {
                alert('Image size is more than 5kb.Compress image file size and Try again.');

                document.getElementById("<%=fileupload.ClientID%>").value = '';
            }
        }--%>

</script>
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="col-lg-12">
        <div class="panel panel-yellow">
            <div class="panel-heading">Kiosk Setting Master</div>
            <div class="panel-body pan">
                <div class="form-body pal">
                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Main Header</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Header Text</label>
                                <div class="clearfix"></div>
                                <asp:TextBox ID="txtHeaderText" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Header Text Color </label>
                                <div class="clearfix"></div>
                                <telerik:RadColorPicker ID="rcpHeaderTextClr" RenderMode="Lightweight" runat="server" SelectedColor="#efefef" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Background Color And Style</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <label>
                                Background Style <span class="text-danger">*</span>
                            </label>
                            <div class="clearfix"></div>
                            <asp:DropDownList ID="ddlBG_Style" runat="server" Width="70%"
                                OnSelectedIndexChanged="ddlBG_Style_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Color" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Gradient" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Image" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                            <div class="clearfix"></div>
                            <br />

                        </div>
                        <div class="col-md-6">
                            <div id="BG_clr" runat="server">
                                <div class="col-md-12">
                                    <label>Background Color </label>
                                    <telerik:RadColorPicker ID="radBG_clr" RenderMode="Lightweight"  SelectedColor="#1B7BBD" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                            </div>
                            <div id="BG_G_clr" runat="server" visible="false">

                                <div class="col-md-4">
                                    <label>Gradient Color 1</label>
                                    <telerik:RadColorPicker ID="radBG_G_clr1" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                                <div class="col-md-4">
                                    <label>Gradient Color 2</label>
                                    <telerik:RadColorPicker ID="radBG_G_clr2" RenderMode="Lightweight" runat="server" ShowIcon="true"  PaletteModes="All" />
                                </div>
                                <div class="col-md-4">
                                    <label>Gradient Color 3</label>
                                    <telerik:RadColorPicker ID="radBG_G_clr3" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>

                                <div class="clearfix"></div>
                                <br />
                            </div>
                            <div id="BG_img" runat="server" visible="false">
                                <div class="col-md-12">
                                    <div class="col-md-6" style="margin-top: 10px;">
                                        <div class="clearfix"></div>
                                        <asp:Image ID="Image1" runat="server" Visible="false" Height="50px" Width="120px" />
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12" style="margin-top: 10px;">
                                        <label style="float: left;">Upload :  &nbsp;</label>
                                        <asp:FileUpload ID="fileupload" runat="server" onChange="OnClientFileSize();" />
                                        <div class="clearfix"></div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Quantity Button (+,-) Color</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Background Color </label>
                                <telerik:RadColorPicker ID="radQty_BG_clr" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Border Color </label>
                                <telerik:RadColorPicker ID="radQty_border_clr" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Quantity Font Style</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Font Color</label>
                                <telerik:RadColorPicker ID="radQty_FontClr" RenderMode="Lightweight"   SelectedColor="#efefef" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-12">
                                <label>
                                    <asp:CheckBox runat="server" ID="Qty_lbl" />
                                    &nbsp; Quantity Label Visibility
                                </label>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Font Size</label>
                                <div class="clearfix"></div>
                                <asp:DropDownList runat="server" ID="ddl_QtyFontSize" Width="70%">
                                    <asp:ListItem Text="Regular" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Small" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Medium" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Large" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Product Name Style</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Font Color</label>
                                <telerik:RadColorPicker ID="radPName_clr" RenderMode="Lightweight"  SelectedColor="#efefef" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Font Size</label>
                                <div class="clearfix"></div>
                                <asp:DropDownList runat="server" ID="ddlPName_size" Width="70%">
                                    <asp:ListItem Text="Regular" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Small" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Medium" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Large" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Static Header Info Color</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Back Color</label>
                                <telerik:RadColorPicker ID="radSHI_backClr" RenderMode="Lightweight" runat="server"  SelectedColor="#1B7BBD" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Font Color</label>
                                <telerik:RadColorPicker ID="radSHI_fontClr" RenderMode="Lightweight"  SelectedColor="#ffffff" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Tell Me About Button Style</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Back Color</label>
                                <telerik:RadColorPicker ID="radTMA_btn_back_clr" RenderMode="Lightweight"   SelectedColor="#1B7BBD" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-12">
                                <label>Border Color</label>
                                <telerik:RadColorPicker ID="radTMA_btn_border_clr" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Font Color</label>
                                <telerik:RadColorPicker ID="radTMA_btn_font_clr" RenderMode="Lightweight"   SelectedColor="#efefef"  runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Tell Me About Popup Style</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>OK Button Style</label>
                                <div class="clearfix"></div>
                                <asp:DropDownList ID="ddl_popupBtnStyle" runat="server" Width="70%"
                                    OnSelectedIndexChanged="ddl_popupBtnStyle_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Color" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Gradient" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-12">
                                <label>Header Color</label>
                                <telerik:RadColorPicker ID="radTMA_HeaderClr" RenderMode="Lightweight"   SelectedColor="#1B7BBD" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>

                        <div class="col-md-6">
                            <div id="divTMA_clr" runat="server">
                                <div class="col-md-12">
                                    <label>Button Color </label>
                                    <telerik:RadColorPicker ID="radTMABtn_clr" RenderMode="Lightweight" runat="server"   SelectedColor="#1B7BBD" ShowIcon="true" PaletteModes="All" />
                                </div>
                            </div>
                            <div id="divTMA_G_clr" runat="server" visible="false">

                                <div class="col-md-4">
                                    <label>Gradient Color 1</label>
                                    <telerik:RadColorPicker ID="radTMABtnGradient_clr1" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                                <div class="col-md-4">
                                    <label>Gradient Color 2</label>
                                    <telerik:RadColorPicker ID="radTMABtnGradient_clr2" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                                <div class="col-md-4">
                                    <label>Gradient Color 3</label>
                                    <telerik:RadColorPicker ID="radTMABtnGradient_clr3" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <h4><b>Confirm Order Button Style</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Font Color</label>
                                <telerik:RadColorPicker ID="radC_BtnFontClr" RenderMode="Lightweight"   SelectedColor="#efefef" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-12">
                                <label>Button Style</label>
                                <div class="clearfix"></div>
                                <asp:DropDownList ID="ddlConfirm_BtnStyle" runat="server" Width="70%"
                                    OnSelectedIndexChanged="ddlConfirm_BtnStyle_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Color" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Gradient" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="clearfix"></div>
                            <br />

                        </div>

                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Border Color</label>
                                <telerik:RadColorPicker ID="radC_BtnBorderClr" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div id="divC_btnClr" runat="server">
                                <div class="col-md-12">
                                    <label>Button Color </label>
                                    <telerik:RadColorPicker ID="radC_BtnClr" RenderMode="Lightweight"   SelectedColor="#1B7BBD"  runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                            </div>
                            <div id="divC_btn_GClr" runat="server" visible="false">

                                <div class="col-md-4">
                                    <label>Gradient Color 1</label>
                                    <telerik:RadColorPicker ID="radC_Btn_GClr1" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                                <div class="col-md-4">
                                    <label>Gradient Color 2</label>
                                    <telerik:RadColorPicker ID="radC_Btn_GClr2" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                                <div class="col-md-4">
                                    <label>Gradient Color 3</label>
                                    <telerik:RadColorPicker ID="radC_Btn_GClr3" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>

                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>


                     <div class="row">
                        <div class="col-md-12">
                            <h4><b>Payment Screen</b></h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Payment Text</label>
                                <div class="clearfix"></div>
                                <asp:TextBox ID="txtPaymentText" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <label>Payment Text Color </label>
                                <div class="clearfix"></div>
                                <telerik:RadColorPicker ID="rcpPaymentTextClr"   SelectedColor="#efefef" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                            </div>
                            <div class="clearfix"></div>
                            <br />
                        </div>
                    </div>
                </div>
                <br />
                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click"
                        ValidationGroup="valid" ToolTip="Click here to Save Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" OnClick="btnReset_Click"
                        ToolTip="Click here to Reset Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="btn btn-green" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        ToolTip="Click here to Cancel Details" />
                </div>
            </div>

        </div>

    </div>
</asp:Content>


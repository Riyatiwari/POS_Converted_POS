<%@ Page Title="Stock Purchase" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Stock_Management_Master.aspx.vb" Inherits="Stock_Management_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Stock Purchase
</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Stock_Management_List.aspx">Stock Purchase List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Stock Purchase</li>
        </ol>
    </div>

    <link href="vendor2/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="css/fonts/nunito.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js2/demo/datatables-demo.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('#Psummary thead tr').clone(true).appendTo('#Psummary thead');

            Grid();
        });

        function Grid() {

            $("#Psummary tr").not(':first').hover(
                function () {
                    $(this).css("background", "#efefef").css("cursor", "Pointer").css("font-weight", "bold").css("box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset").css("-webkit-box-shadow", "0 4px 2px -3px rgba(0, 0, 0, 0.5) inset");
                },
                function () {
                    $(this).css("background", "").css("cursor", "").css("font-weight", "").css("box-shadow", "").css("-webkit-box-shadow", "");
                }
            );

            var groupCol = 0;

            var table = $('#Psummary').DataTable({
                orderCellsTop: true,
                //dom: 'Bfrtip',
                dom: 'lfrti',
                "buttons": [
                    //'excel', 'pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [

                    { "visible": true, "targets": +groupCol }

                ],
                "bPaginate": false,
                "displayLength": 15,
                "searching": true
            });

            var table1 = $('#Psummary').DataTable();

            $('#Psummary thead tr:eq(1) th').each(function (i) {

                var title = $(this).text();
                if (i == 0) {
                    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                }
                else {

                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }
                $('input', this).on('keyup', function () {

                    if (table1.column(i).search() !== this.value) {
                        table1

                            .column($(this).parent().index() + ':visible')
                            .search(this.value)
                            .draw()
                            ;

                    }
                    else {
                        table1
                            .column($(this).parent().index() + ':visible')
                            .search('')
                            .draw()
                            ;

                    }
                });
            });
        }

    </script>

    <style>
        #ContentPlaceHolder1_btn_Next:focus {
            border: 2px solid #4cff00;
        }

        #ContentPlaceHolder1_btn_Cancel:focus {
            border: 2px solid #4cff00;
        }

        #ContentPlaceHolder1_btn_Save:focus {
            border: 2px solid #4cff00;
        }

        .gvWidthHight {
            overflow: scroll;
            height: 250px;
            width: 100%;
            border: 1px solid lightgray;
        }

        .modal-backdrop{
            background-color: transparent !important;
        }
    </style>

    <script>
        //function popupClose() {

        //    $('#exampleModalCenter').modal().hide();
        //}
        function popup() {

            $('#exampleModalCenter').modal().show();
            
        }
        function check() {

            var input = document.getElementById("ContentPlaceHolder1_txt_Qty");

            input.addEventListener("keyup", function (event) {
                if (event.keyCode === 13) {
                    event.preventDefault();

                    var input2 = document.getElementById("ContentPlaceHolder1_txt_Price");

                    if (input2 == null) {
                        var input3 = document.getElementById("ContentPlaceHolder1_btn_Save");
                        input3.focus();

                    }
                    else {
                        input2.focus();
                    }

                }
            });

            var input = document.getElementById("ContentPlaceHolder1_txt_Price");

            input.addEventListener("keyup", function (event) {
                if (event.keyCode === 13) {
                    event.preventDefault();

                    var input2 = document.getElementById("ContentPlaceHolder1_chkTax");

                    if (input2 == null) {
                        var input3 = document.getElementById("ContentPlaceHolder1_chkTax");
                        input3.focus();

                    }
                    else {
                        input2.focus();
                    }

                }
            });

            var input = document.getElementById("ContentPlaceHolder1_chkTax");

            input.addEventListener("keyup", function (event) {
                if (event.keyCode === 13) {
                    event.preventDefault();

                    var input2 = document.getElementById("ContentPlaceHolder1_btn_Next");

                    if (input2 == null) {
                        var input3 = document.getElementById("ContentPlaceHolder1_btn_Save");
                        input3.focus();

                    }
                    else {
                        input2.focus();
                    }

                }
            });
        }

        function handleChange(input) {
            if (input.value < 0) input.value = 0;
            if (input.value >= 100) input.value = 99.99;
        }
    </script>

</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-lg-12 ">
        <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Stock Purchase</div>
            <div class="panel-body pan">

                <div class="form-body pal">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-md-12">

                                <div class="col-md-2">
                                    <asp:HiddenField ID="hdreceipt_index" runat="server" Value='<%#Eval("receipt_index")%>' />
                                    <label>Receipt Number</label><div class="clearfix"></div>

                                    <telerik:RadTextBox ID="txtreceipt" CssClass="form-control" Skin="" runat="server" placeholder="ReceiptNumber" Width="100%" MaxLength="200" ReadOnly="true"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Location<span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="rdlocation" runat="server" Width="100%" Skin="MetroTouch" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rflocation" runat="server" ControlToValidate="rdlocation"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Location is required" CssClass="text-danger" InitialValue="SELECT">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <label>Stock Date<span class="text-danger">&nbsp;*</span></label><div class="clearfix"></div>
                                    <telerik:RadDatePicker ID="txtdate" runat="server" DateInput-EmptyMessage="Date" MinDate="01/01/1000" MaxDate="01/01/3000" Skin="MetroTouch" Width="100%">
                                        <DateInput runat="server" DateFormat="dd/MM/yyyy" />
                                        <Calendar runat="server" FirstDayOfWeek="Monday">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>

                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtdate" ValidationGroup="valid" Display="None"
                                        ErrorMessage="Date is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">

                                    <label>Supplier</label><div class="clearfix"></div>

                                    <telerik:RadTextBox ID="txtsupplier" CssClass="form-control" Skin="" runat="server" placeholder="Supplier" Width="100%"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">

                                    <label>Supplier Code</label><div class="clearfix"></div>

                                    <telerik:RadTextBox ID="txtsuppliercode" CssClass="form-control" Skin="" runat="server" placeholder="Supplier Code" Width="100%"></telerik:RadTextBox>
                                </div>

                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <div class="clearfix"></div>
                                    <input id="btn_AddProduct" type="button" class="btn btn-primary" runat="server"
                                        data-toggle="modal" data-target="#exampleModalCenter" data-backdrop="static" data-keyboard="false"
                                        value="+" />
                                    <%-- <asp:Button ID="btnaddProduct" class="btn btn-primary" runat="server"
                                        ValidationGroup="validSize" data-toggle="modal" data-target="#exampleModalCenter"
                                        Text="+" ToolTip="Click here to Add New Product" />--%>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-2">
                                    <label>Purchase Template</label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="rdTemplate" runat="server" Width="100%" Skin="MetroTouch" AutoPostBack="true"
                                        OnSelectedIndexChanged="rdTemplate_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-md-2">

                                    <label>Set as Templete</label>&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkTemplete" runat="server" AutoPostBack="true" OnCheckedChanged="chkTemplete_CheckedChanged"></asp:CheckBox>
                                </div>
                                <div class="col-md-2" id="DivTempleteName" runat="server" visible="false">
                                    <label>Templete Name<span class="text-danger">*</span></label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtTemplete_Name" CssClass="form-control" Skin="" runat="server"
                                        placeholder="Templete Name" Width="100%" AutoPostBack="true" OnTextChanged="txtTemplete_Name_TextChanged">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="upWall" runat="server">
                                        <ContentTemplate>
                                            <div class="gvWidthHight">
                                                <asp:GridView ID="gd_Product" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                    CssClass="Grid" OnRowCommand="gd_Product_RowCommand" Style="border-color: lightgray;"
                                                    OnRowDataBound="gd_Product_RowDataBound" PageSize="10" Width="100%"
                                                    EmptyDataText="No records to display." ShowHeaderWhenEmpty="True"
                                                    EmptyDataRowStyle-VerticalAlign="Top" AllowSorting="false">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ProductGroup *">
                                                            <HeaderStyle Width="15%" Height="40px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdproductgroup" runat="server" Value='<%#Eval("Product_group")%>' />
                                                                <asp:DropDownList ID="ddlproductgroup" runat="server" placeholder="Product Group" Width="100%" Enabled="false" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlproductgroup_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product *">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdproduct" runat="server" Value='<%#Eval("Product_id")%>' />
                                                                <asp:DropDownList ID="ddlproduct" runat="server" placeholder="Product" Width="100%" Enabled="false" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Quantity *">
                                                            <HeaderStyle Width="7%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txttotalStock" runat="server" Text='<%# Eval("Total_Stock") %>' placeholder="Total Stock"
                                                                    CssClass="form-control" MaxLength="15" Width="100%"
                                                                    AutoPostBack="true" OnTextChanged="txttotalStock_TextChanged">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Volume *">
                                                            <HeaderStyle Width="7%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdunit" runat="server" Value='<%#Eval("unit_id")%>' />
                                                                <asp:DropDownList ID="ddltotalunit" runat="server" Width="100%" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddltotalunit_SelectedIndexChanged" Enabled="false">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Each Cost *">
                                                            <HeaderStyle Width="7%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdCostID" runat="server" Value='<%#Eval("cost_id")%>' />
                                                                <asp:HiddenField ID="hdBuyingSizeCost" runat="server" Value='<%#Eval("BuyingSizeCost")%>' />
                                                                <asp:HiddenField ID="hdnPrice_Id" runat="server" Value='<%#Eval("Price")%>' />
                                                                <asp:TextBox ID="txtprice" CssClass="form-control" runat="server" placeholder="Cost" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("Price")%>' OnTextChanged="txtprice_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total Cost">
                                                            <HeaderStyle Width="7%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdntotalPrice_Id" runat="server" Value='<%#Eval("Total_Price")%>' />
                                                                <asp:TextBox ID="txttotalprice" CssClass="form-control" runat="server" placeholder="Total Cost" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("Total_Price")%>' OnTextChanged="txttotalprice_TextChanged" AutoPostBack="true">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tax *">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdtaxsave" runat="server" Value='<%#Eval("tax_id")%>' />
                                                                <asp:DropDownList ID="ddltax" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddltax_SelectedIndexChanged"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inc/Exc Tax" Visible="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdtax" runat="server" Value='<%#Eval("Tax")%>' />
                                                                <asp:CheckBox ID="chktax" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chktax_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tax Amt" Visible="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdtaxamount" runat="server" Value='<%#Eval("tax_amount")%>' />
                                                                <asp:TextBox ID="txttaxamount" CssClass="form-control" Skin="" runat="server" placeholder="Tax Amount" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("tax_amount")%>' OnTextChanged="txttaxamount_TextChanged" AutoPostBack="true" Visible="false">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Final Cost">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdfinalprice" runat="server" Value='<%#Eval("final_price")%>' />
                                                                <asp:TextBox ID="txtfinalprice" CssClass="form-control" Skin="" runat="server" placeholder="Final Cost" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("final_price")%>' OnTextChanged="txtfinalprice_TextChanged" AutoPostBack="true" Enabled="false">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Damage" Visible="false">
                                                            <HeaderStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnactive" runat="server" Value='<%#Eval("Is_damage")%>' />
                                                                <asp:CheckBox ID="chkisdamage" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkisdamage_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action">
                                                            <HeaderStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                                <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');">
                                                                    <i class="fa fa-minus-square fa-lg"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>
                                            </div>

                                            <%--<telerik:RadGrid ID="rdproduct" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" runat="server" ShowGroupPanel="False"
                                                PageSize="10" AllowFilteringByColumn="false" SkinID="RadSkinManager1"
                                                PagerStyle-AlwaysVisible="false" Skin="MetroTouch" AllowAutomaticDeletes="false" AllowAutomaticUpdates="false" Width="100%" Visible="true" ClientSettings-AllowDragToGroup="true">

                                                <ClientSettings AllowRowsDragDrop="true">
                                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="200px" />
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView AutoGenerateColumns="False" AllowMultiColumnSorting="false" TableLayout="Auto" Width="100%" Font-Size="90%">

                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="ProductGroup*" UniqueName="Product_Group">
                                                            <HeaderStyle Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdproductgroup" runat="server" Value='<%#Eval("Product_group")%>' />
                                                                <telerik:RadComboBox ID="ddlproductgroup" runat="server" Enabled="false" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlproductgroup_SelectedIndexChanged"></telerik:RadComboBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlproductgroup" ErrorMessage="Select a Product Group" ValidationGroup="valid" Visible="false"></asp:RequiredFieldValidator>
                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Product *" UniqueName="Product_id">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdproduct" runat="server" Value='<%#Eval("Product_id")%>' />
                                                                <telerik:RadComboBox ID="ddlproduct" runat="server" Enabled="false" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged"></telerik:RadComboBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Quantity *" UniqueName="Total_stock">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <telerik:RadTextBox ID="txttotalStock" CssClass="form-control" Skin="" runat="server" placeholder="Total Stock" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("Total_Stock") %>' AutoPostBack="true" OnTextChanged="txttotalStock_TextChanged">
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Volume *" UniqueName="sizeTemp">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdunit" runat="server" Value='<%#Eval("unit_id")%>' />
                                                                <telerik:RadComboBox ID="ddltotalunit" runat="server" Width="100%" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddltotalunit_SelectedIndexChanged" Enabled="false">
                                                                </telerik:RadComboBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridTemplateColumn HeaderText="Each Cost *" UniqueName="PriceTemp">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdCostID" runat="server" Value='<%#Eval("cost_id")%>' />
                                                                <asp:HiddenField ID="hdBuyingSizeCost" runat="server" Value='<%#Eval("BuyingSizeCost")%>' />
                                                                <asp:HiddenField ID="hdnPrice_Id" runat="server" Value='<%#Eval("Price")%>' />
                                                                <telerik:RadTextBox ID="txtprice" CssClass="form-control" Skin="" runat="server" placeholder="Cost" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("Price")%>' OnTextChanged="txtprice_TextChanged" AutoPostBack="true">
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Total Cost" UniqueName="PriceTemp">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdntotalPrice_Id" runat="server" Value='<%#Eval("Total_Price")%>' />
                                                                <telerik:RadTextBox ID="txttotalprice" CssClass="form-control" Skin="" runat="server" placeholder="Total Cost" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("Total_Price")%>' OnTextChanged="txttotalprice_TextChanged" AutoPostBack="true">
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Tax *" UniqueName="sizeTemp">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdtaxsave" runat="server" Value='<%#Eval("tax_id")%>' />
                                                                <telerik:RadComboBox ID="ddltax" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddltax_SelectedIndexChanged1"></telerik:RadComboBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Inc/Exc Tax" UniqueName="sizeTemp" Visible="false">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdtax" runat="server" Value='<%#Eval("Tax")%>' />
                                                                <asp:CheckBox ID="chktax" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chktax_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Tax Amt" UniqueName="sizeTemp" Visible="false">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdtaxamount" runat="server" Value='<%#Eval("tax_amount")%>' />
                                                                <telerik:RadTextBox ID="txttaxamount" CssClass="form-control" Skin="" runat="server" placeholder="Tax Amount" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("tax_amount")%>' OnTextChanged="txttaxamount_TextChanged" AutoPostBack="true" Visible="false">
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Final Cost" UniqueName="sizeTemp">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdfinalprice" runat="server" Value='<%#Eval("final_price")%>' />
                                                                <telerik:RadTextBox ID="txtfinalprice" CssClass="form-control" Skin="" runat="server" placeholder="Final Cost" Width="100%"
                                                                    MaxLength="15" Text='<%# Eval("final_price")%>' OnTextChanged="txtfinalprice_TextChanged" AutoPostBack="true" Enabled="false">
                                                                </telerik:RadTextBox>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Is Damage" UniqueName="is_damage" AllowFiltering="false" Visible="false">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnactive" runat="server" Value='<%#Eval("Is_damage")%>' />
                                                                <asp:CheckBox ID="chkisdamage" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkisdamage_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="TemplateColumn" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnrow_Id" runat="server" Value='<%#Eval("row_Id")%>' />
                                                                <asp:LinkButton ID="IbDelete" runat="server" CausesValidation="False" ToolTip="Delete"
                                                                    CommandName="DeleteVal" CommandArgument='<%#Eval("row_Id")%>' OnClientClick="return confirm('Are you sure you want to Delete this Record ?');">
                                                                    <i class="fa fa-minus-square fa-lg"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-actions text-right pal">
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Stock" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Submit" ValidationGroup="valid" ToolTip="Click here to Submit Stock" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Stock" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Stock" />
                </div>

            </div>

        </div>
        <br />
        <%-- </telerik:RadAjaxPanel>--%>
    </div>

    <!-- Modal -->
    <asp:UpdatePanel ID="up_Ingredient" runat="server">
        <ContentTemplate>
            <div class="modal" id="exampleModalCenter" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-centered" role="document" style="width: 1200px !important;">
                    <div class="modal-content" style="width: 90%">
                        <div class="modal-body">
                            <div class="panel panel-yellow">
                                <div class="panel-heading">
                                    Product List For Stock Purchase
                                </div>
                                <div class="panel-body pan">
                                    <div class="form-body pal">

                                        <br />
                                        <div id="dv_prodctlist" runat="server">
                                            <div class="row" id="divPGroup" runat="server">
                                                <div class="col-lg-12 " style="overflow-y: auto; height: 445px;">
                                                    <div class="table-responsive">

                                                        <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                                            <asp:Repeater ID="rdProduct_list" runat="server">
                                                                <HeaderTemplate>
                                                                    <thead>
                                                                        <tr>
                                                                            <th></th>
                                                                            <th>Product Group</th>
                                                                            <th>Product</th>
                                                                            <th>Base Unit</th>
                                                                            <th>Buying Size</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chk_productID" runat="server" />
                                                                            <asp:HiddenField ID="hdProductID" runat="server" Value='<%# Eval("product_id") %>' />
                                                                        </td>
                                                                        <td style="background-color: #ffffff;"><%#Eval("Cat_Name") %></td>
                                                                        <td style="background-color: #ffffff;">
                                                                            <a target="_blank" href='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'><%#Eval("pro_Name") %></a>
                                                                        </td>
                                                                        <td style="background-color: #ffffff;"><%#Eval("base_unit") %></td>
                                                                        <td style="background-color: #ffffff;"><%#Eval("buying_size") %></td>
                                                                    </tr>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody>
                                                     <tfoot>
                                                         </table>
                                                     </tfoot>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="padding-top: 10px; text-align: center;">
                                                <asp:Button ID="btn_ProductIngredient" class="btn btn-primary bclose" runat="server" Text="OK"
                                                    ToolTip="Click here Add Product For Stock Purchase" />&nbsp;&nbsp;
                                                <asp:Button ID="btn_ProductIngrdientCancel" class="btn btn-primary" runat="server"
                                                    Text="Cancel" ToolTip="Click here Cancel" />
                                            </div>
                                        </div>

                                        <div id="dv_wizard" runat="server" visible="false">
                                            <div class="col-md-3">
                                                <label>
                                                    Name
                                                </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <a target="_blank" runat="server" id="link">
                                                        <asp:Label ID="txt_Name" runat="server"></asp:Label>
                                                    </a>
                                                    <%--<asp:LinkButton ID="txt_Name" runat="server" PostBackUrl='<%#"Product_Master.aspx?product_id=" & Eval("product_id") %>'></asp:LinkButton>--%>
                                                    <asp:HiddenField ID="hf_Product_Id" runat="server" Visible="false" />
                                                    <asp:HiddenField ID="hf_Category_Id" runat="server" Visible="false" />
                                                    <asp:HiddenField ID="hf_Unit_Id" runat="server" Visible="false" />
                                                    <asp:HiddenField ID="hf_Row_Id" runat="server" Visible="false" />
                                                </label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3">
                                                <label>
                                                    Qty</label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <asp:TextBox ID="txt_Qty" runat="server" TabIndex="1" OnTextChanged="txt_Qty_TextChanged" AutoPostBack="true"></asp:TextBox></label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3">
                                                <label>
                                                    Buying Size</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlCOstSize" runat="server" Width="35%"
                                                    OnSelectedIndexChanged="ddlCOstSize_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click">
                                                    <i class="fa fa-refresh" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3">
                                                <label>
                                                    Buying Size Cost
                                                </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <asp:TextBox ID="txt_Price" runat="server" OnTextChanged="txt_Price_TextChanged" AutoPostBack="true" TabIndex="2"></asp:TextBox></label>
                                            </div>

                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3">
                                                <label>
                                                    Total Cost
                                                </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <asp:TextBox ID="txt_TotalPrice" runat="server" TabIndex="3" Enabled="false"></asp:TextBox></label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3">
                                                <label>
                                                    Actual Qty</label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <asp:Label ID="lblActualQty" runat="server" Text="0"></asp:Label>&nbsp;<asp:Label ID="lblActuanlUnit" runat="server" Text="0"></asp:Label>
                                                    <asp:HiddenField ID="hfActualunitid" runat="server" Value="0" />
                                                </label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3">
                                                <label>
                                                    Cost for Each
                                                </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <asp:Label ID="lblCostEach" runat="server" Text="0"></asp:Label>

                                                </label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3" style="display: none;">
                                                <label>
                                                    Inc. Tax
                                                </label>
                                            </div>
                                            <div class="col-md-6" style="display: none;">
                                                <label>
                                                    <asp:CheckBox ID="chkTax" runat="server" TabIndex="4"></asp:CheckBox></label>
                                            </div>

                                            <div class="col-md-3">
                                                <label>
                                                    Tax Amount
                                                </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <asp:TextBox ID="txt_TaxAmount" runat="server" TabIndex="5" AutoPostBack="true"></asp:TextBox></label>
                                            </div>
                                            <div class="clearfix"></div>
                                            <br />
                                            <div class="col-md-3">
                                                <label>
                                                    Total Amount
                                                </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text="0"></asp:Label>
                                                    <asp:HiddenField ID="hdTax_ID" runat="server"></asp:HiddenField>
                                                </label>
                                            </div>

                                            <div class="clearfix"></div>
                                            <br />
                                            <div style="padding-top: 10px; text-align: center;">
                                                <asp:Button ID="btn_Cancel" class="btn btn-primary" runat="server" Text="Cancel" ToolTip="Click here Cancel" TabIndex="6" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_Next" class="btn btn-primary" runat="server" Text="Next" ToolTip="Click here Next" TabIndex="7" />&nbsp;&nbsp;
                                    <asp:Button ID="btn_Save" class="btn btn-primary" runat="server" Text="Save" ToolTip="Click here Save" Visible="false" TabIndex="8" AutoPostBack="true" />
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_ProductIngredient" />
            <asp:AsyncPostBackTrigger ControlID="btn_Next" />
            <asp:PostBackTrigger ControlID="btn_Save" />
            <asp:PostBackTrigger ControlID="btn_ProductIngrdientCancel" />
            <asp:PostBackTrigger ControlID="btn_Cancel" />

        </Triggers>
    </asp:UpdatePanel>

    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
            color: #ffffff;
            border: #111111 1px solid;
        }

        .row th {
            background-color: #ffffff !important;
        }
    </style>

</asp:Content>



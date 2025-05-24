<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KeyMap_Master.aspx.vb" Inherits="KeyMap_Master" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderLink" runat="Server">
    <div class="page-header pull-left">
        <ol class="breadcrumb page-breadcrumb pull-left">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Dashboard.aspx">Home</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><a href="Key_Map_List.aspx">Key Map List</a>&nbsp;&nbsp;
                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;
            </li>
            <li class="active">Key Map Master</li>
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
                dom: 'Bfrtip',

                //dom: 'lfrti',
                "buttons": [
                    //'excel', 'pdf'
                ],
                "stripeClasses": ['odd-row', 'even-row'],
                "destroy": true,
                "columnDefs": [
                    { "visible": false, "targets": + groupCol }
                ],

                "order": [[groupCol, 'asc']],
                "displayLength": 15000,
                "drawCallback": function (settings) {

                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;

                    api.column(groupCol, { page: 'current' }).data().each(function (group, i) {

                        if (last !== group) {

                            $(rows).eq(i).before(
                                '<tr class="group"><td colspan="3"> Product Group : ' + group + '</td> </tr>'

                            );
                            last = group;
                        }
                    });
                }
            });

            var table1 = $('#Psummary').DataTable();
            $('#Psummary thead tr:eq(1) th').each(function (i) {
                stateSave: true
                var title = $(this).text();
                if (i == 0 || i == 2) {
                    $(this).html('<input type="text" style="width:98%;visibility: hidden;" placeholder="" />');
                }
                else {

                    $(this).html('<input type="text" style="width:98%" placeholder="Search ' + title + '" />');
                }

                $('input', this).on('keyup change', function () {

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

    <script>

        function Expand() {

            //alert(document.getElementById("divDetails").style.display);

            if (document.getElementById("divDetails").style.display == 'none') {
                document.getElementById("divDetails").style.display = 'block'
 
            }
            else {
                document.getElementById("divDetails").style.display = 'none'
               
            }
            return false;

        }

        function popup() {

            //$('#ModalCenter').modal().show();

            var someSession = '<%= Session("CopyMode").ToString() %>';
            //alert(someSession);
            if (someSession != "1") { $('#ModalCenter').modal().show(); }

        }

        function popupTill() {
            $('#ModalCenterTill').modal().show();
        }

        function singleSelection(id) {
            var rb = document.getElementById(id);
            var rblist = document.getElementsByTagName("input");
            for (var i = 0; i < rblist.length; i++) {

                if (rblist[i].type == "radio" && rblist[i].id !== rb.id) {
                    rblist[i].checked = false
                }

            }

        }

    </script>

    <style>
        .gvWidthHight {
            overflow: scroll;
            height: 250px;
            width: 100%;
            border: 1px solid lightgray;
        }

        .modal-backdrop {
            background-color: transparent !important;
        }
    </style>

</asp:Content>
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <br />
    <div class="col-lg-12 ">
        <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div class="panel panel-yellow">
            <div class="panel-heading">Key Map Master</div>
            <div class="panel-body pan">

                <div class="form-body pal">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valid"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtKName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKName"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Key map name is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                 </div>
                                </div>
                            </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                    &nbsp;
                                    <br />
                                    <asp:Button ID="btnHide" class="btn btn-primary" runat="server" Text="Show/Hide Key Map Details"  OnClientClick="return Expand()" ToolTip="" />&nbsp;&nbsp;&nbsp;
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row" style="display:none;" id="divDetails">
                        
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                   &nbsp;
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Display Name <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtDisName" CssClass="form-control" Skin="" runat="server" placeholder="Name" Width="100%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDisName"
                                        ValidationGroup="valid" Display="None" ErrorMessage="Display name is required" CssClass="text-danger">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12" style="display: none;">
                                    <label>Description </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadTextBox ID="txtdesc" CssClass="form-control" Skin="" runat="server" TextMode="MultiLine" placeholder="Description" Width="100%"></telerik:RadTextBox>

                                </div>

                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Sorting No </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadNumericTextBox MinValue="0" ID="txtSortingno" Skin="" CssClass="form-control"
                                        runat="server" Width="100%" placeholder="Sorting No" MaxLength="4" Enabled="false">
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>

                                </div>
                                <div class="clearfix"></div>

                                <br />
                                <div class="col-md-12">
                                    <label>Size <span class="text-danger">*</span> </label>
                                    <telerik:RadComboBox ID="ddlsize" runat="server" Width="100%" OnSelectedIndexChanged="ddlsize_SelectedIndexChanged" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="SELECT" Text="SELECT" />
                                            <telerik:RadComboBoxItem Value="4*4" Text="4*4" />
                                            <telerik:RadComboBoxItem Value="4*6" Text="4*6" />
                                            <telerik:RadComboBoxItem Value="5*5" Text="5*5" />
                                            <telerik:RadComboBoxItem Value="6*6" Text="6*6" />
                                            <telerik:RadComboBoxItem Value="7*7" Text="7*7" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsize" ErrorMessage="Size is required"
                                        ValidationGroup="valid" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="clearfix"></div>

                                <br />
                                <div class="col-md-12">
                                    <label>
                                        Active &nbsp;
                                            <asp:CheckBox ID="chkActive" runat="server" Checked="true" /></label>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <label>Font Color </label>
                                        <telerik:RadColorPicker ID="radForcolorpicker_m" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                    </div>
                                    <div class="col-md-6">
                                        <label>Background Color </label>
                                        <telerik:RadColorPicker ID="radBackcolorpicker_m" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" OnColorChanged="radBackcolorpicker_m_ColorChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12" style="height: 390px; overflow: scroll;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="rdTill" runat="server" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="True" ShowGroupPanel="False"
                                                AllowFilteringByColumn="false" CssClass="Grid"
                                                EmptyDataText="No data in the data source." ShowHeaderWhenEmpty="True" Width="100%" GridLines="None" OnRowDataBound="rdTill_RowDataBound" OnRowCommand="rdTill_RowCommand">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Till Name">
                                                        <HeaderStyle Width="35%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkKeymap" runat="server"></asp:CheckBox>
                                                            <asp:LinkButton ID="lbl_name" runat="server" Text='<%# Eval("tillname")%>' CommandName="TillPop" CommandArgument='<%# Eval("machine_id")%>' />
                                                            <asp:HiddenField ID="hdmachine_id" runat="server" Value='<%# Eval("machine_id")%>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <HeaderStyle Width="35%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllocname" runat="server" Text='<%# Eval("locationname")%>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Shorting Number">
                                                        <HeaderStyle Width="30%" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtShortingNum" runat="server" Width="50%" Text='<%# Eval("shorting_num")%>'></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6" style="visibility: hidden;">
                            <div class="form-group" style="display: none;">
                                <div class="col-md-12" style="display: none;">
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="valid1"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Group Category</label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="ddlMainCategory" runat="server" Width="100%" OnSelectedIndexChanged="ddlMainCategory_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMainCategory" ErrorMessage="Group Category is required"
                                            ValidationGroup="valid1" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                        </asp:RequiredFieldValidator>--%>
                                    <div class="clearfix"></div>
                                    <br />
                                </div>

                                <div class="col-md-12">
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="valid1"
                                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" CssClass="message error no-margin"></asp:ValidationSummary>
                                    <label>Product Group <span class="text-danger">*</span></label><div class="clearfix"></div>
                                    <telerik:RadComboBox ID="RadPro_group" runat="server" Width="100%" OnSelectedIndexChanged="RadPro_group_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rfvDeviceType" runat="server" ControlToValidate="RadPro_group" ErrorMessage="Product group is required"
                                        ValidationGroup="valid1" Display="none" CssClass="text-danger" InitialValue="SELECT">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Product </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="Radproduct" runat="server" Width="100%" OnSelectedIndexChanged="Radproduct_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>

                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">
                                    <label>Size </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="RadSize" runat="server" Width="100%">
                                    </telerik:RadComboBox>

                                </div>
                                <div class="clearfix"></div>

                                <br />
                                <div class="col-md-12">
                                    <label>Font Color </label>
                                    <telerik:RadColorPicker ID="radForcolorpicker" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />

                                </div>
                                <div class="clearfix"></div>

                                <br />
                                <div class="col-md-12">
                                    <label>Background Color</label>
                                    <telerik:RadColorPicker ID="radBackcolorpicker" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-12">

                                    <asp:Button ID="btn_save" class="btn btn-primary" runat="server" ValidationGroup="valid1" Text="Save" />&nbsp;&nbsp;
                                        <asp:Button ID="btn_clear" class="btn btn-primary" runat="server" Text="Clear" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-md-12">
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <br />
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label>Venue </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radVenue" runat="server" Width="100%" OnSelectedIndexChanged="radVenue_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-md-4">
                                    <label>Location </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radLocation" runat="server" Width="100%" OnSelectedIndexChanged="radLocation_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>

                                </div>

                                <div class="col-md-4">
                                    <label>Till </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="radMachine" runat="server" Width="100%">
                                    </telerik:RadComboBox>

                                </div>
                                <div class="clearfix"></div>
                                <br />
                                <div class="col-md-4">
                                    <label>Button Style </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="ddlBtnStyle" runat="server" Width="100%">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="Flate" Text="Flat" />
                                            <telerik:RadComboBoxItem Value="Shadow" Text="Shadow" />
                                            <telerik:RadComboBoxItem Value="Full Shadow" Text="Full Shadow" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-md-4">
                                    <label>Image Type </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="ddlImgType" runat="server" Width="100%" OnSelectedIndexChanged="ddlImgType_SelectedIndexChanged" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="None" Text="None" />
                                            <telerik:RadComboBoxItem Value="Round Image" Text="Round Image" />
                                            <telerik:RadComboBoxItem Value="Square Image" Text="Square Image" />
                                        </Items>
                                    </telerik:RadComboBox>

                                </div>

                                <div class="col-md-4" id="div_imgOption" runat="server" visible="false">
                                    <label>Image Option </label>
                                    <div class="clearfix"></div>
                                    <telerik:RadComboBox ID="ddl_ImageOption" runat="server" Width="100%">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="Left" Text="Left" />
                                            <telerik:RadComboBoxItem Value="Right" Text="Right" />
                                            <telerik:RadComboBoxItem Value="Top" Text="Top" />
                                            <telerik:RadComboBoxItem Value="Bottom" Text="Bottom" />
                                            <telerik:RadComboBoxItem Value="Background" Text="Background" />
                                        </Items>
                                    </telerik:RadComboBox>

                                </div>
                            </div>
                        </div>
                        </div>

                <div class="row">
                        <asp:UpdatePanel ID="upWall" runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:Label ID="lblCopyText" Style="font-size: 20px; font-weight: bold;" runat="server" Text=""></asp:Label>
                                </div>
                                <div id="div_4by4" runat="server" visible="false">

                                    <table style="border: 1px solid black;" width="100%">
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by4_1" class="btn btn-primary" runat="server" Text=""
                                                    Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by4_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by4_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by4_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by4_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        </tr>
                                    </table>
                                </div>

                                <div id="div_4by6" runat="server" visible="false">
                                    <table style="border: 1px solid black;" width="100%">
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by6_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by6_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by6_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by6_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by6_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_4by6_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_4by6_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        </tr>
                                    </table>
                                </div>

                                <div id="div_5by5" runat="server" visible="false">
                                    <table style="border: 1px solid black;" width="100%">
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_5by5_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_5by5_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_5by5_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_5by5_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_5by5_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_5by5_25" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        </tr>
                                    </table>
                                </div>

                                <div id="div_6by6" runat="server" visible="false">
                                    <table style="border: 1px solid black;" width="100%">
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_6by6_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_6by6_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_6by6_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_6by6_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_6by6_25" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_26" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_27" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_28" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_29" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_30" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_6by6_31" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_32" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_33" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_34" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_35" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_6by6_36" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 90%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        </tr>
                                    </table>
                                </div>

                                <div id="div_7by7" runat="server" visible="false">
                                    <table style="border: 1px solid black;" width="100%">
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_7by7_1" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_2" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_3" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_4" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_5" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_6" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_7" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_7by7_8" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_9" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_10" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_11" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_12" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_13" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_14" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_7by7_15" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_16" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_17" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_18" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_19" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_20" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_21" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_7by7_22" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_23" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_24" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_25" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_26" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_27" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_28" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_7by7_29" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_30" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_31" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_32" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_33" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_34" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_35" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_7by7_36" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_37" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_38" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_39" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_40" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_41" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_42" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px;" /></th>
                                        </tr>
                                        <tr>
                                            <th width="350px;">
                                                <asp:Button ID="btn_7by7_43" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-left: 10px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_44" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_45" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_46" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_47" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_48" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                            &nbsp;
                                         <th width="350px;">
                                             <asp:Button ID="btn_7by7_49" class="btn btn-primary" runat="server" Text="" Style="margin-top: 10px; width: 70%; font-size: 10px; height: 50px; margin-bottom: 10px;" /></th>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="form-actions text-right pal">

                    <div style="text-align: left;">
                        <asp:Button ID="btnCopyMode" OnClick="btnCopyMode_Click" class="btn btn-primary" runat="server" Text="Copy Mode" ValidationGroup="valid" ToolTip="Click here to Start Copy button" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClearCopyMode" class="btn btn-primary" runat="server" Text="Normal Mode" ValidationGroup="valid" OnClick="btnClearCopyMode_Click" />&nbsp;&nbsp;&nbsp;
                    </div>

                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" ValidationGroup="valid" ToolTip="Click here to Save Key Map Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" class="btn btn-orange" runat="server" Text="Reset" ToolTip="Click here to Reset Key Map Details" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancle" class="btn btn-green" runat="server" Text="Cancel" ToolTip="Click here to Cancel Key Map Details" />
                </div>

            </div>

        </div>

        <%-- </telerik:RadAjaxPanel>--%>

        <!-- Modal -->
        <asp:UpdatePanel ID="up_model" runat="server">
            <ContentTemplate>

                <div class="modal" id="ModalCenter" tabindex="-1" role="dialog"
                    aria-labelledby="ModalCenterTitle" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog modal-dialog-centered" role="document" style="width: 800px !important;">
                        <div class="modal-content" style="width: 90%">
                            <div class="modal-body">

                                <div class="panel panel-yellow">
                                    <div class="panel-heading">
                                        Product List For Key map
                                    </div>
                                    <div class="panel-body pan">
                                        <div class="form-body pal">

                                            <div id="dv_prodctlist" runat="server">

                                                <h3>
                                                    <asp:RadioButton ID="rbProduct" runat="server" GroupName="prdSelection" Checked="true" /><b>Please Select Product</b></h3>
                                                <asp:HiddenField ID="hdProductNameSelected" runat="server" Value='' />

                                                <div class="clearfix"></div>
                                                <label>Product Group Filter :</label><div class="clearfix"></div>
                                                <asp:HiddenField ID="hfRowIdbutton" runat="server" Value="0" />
                                                <asp:Label ID="lblRowid" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlPrdGfilter" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlPrdGfilter_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <div class="clearfix"></div>
                                                <div class="row">
                                                    <div class="col-lg-12 " style="overflow-y: auto; height: 325px;">
                                                        <div class="table-responsive">
                                                            <table class="table table-bordered" id="Psummary" width="100%" cellspacing="0">
                                                                <asp:Repeater ID="rdProduct_list" runat="server" OnItemDataBound="rdProduct_list_ItemDataBound">
                                                                    <HeaderTemplate>
                                                                        <thead>
                                                                            <tr>
                                                                                <th>Product Group</th>
                                                                                <th></th>
                                                                                <th>Product</th>
                                                                                <th>Size</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td style="width: 30%; background-color: #ffffff;"><%#Eval("Cat_Name") %></td>
                                                                            <td style="width: 7%; text-align: center;">
                                                                                <%-- <asp:CheckBox ID="chk_productID" runat="server" />--%>
                                                                                <asp:RadioButton runat="server" ID="chk_productID" OnClick="javascript:singleSelection(this.id) " />
                                                                                <asp:HiddenField ID="hdProductID" runat="server" Value='<%# Eval("product_id") %>' />
                                                                                <asp:HiddenField ID="hdProductName" runat="server" Value='<%# Eval("pro_Name") %>' />
                                                                                <asp:HiddenField ID="hdCateogoryname" runat="server" Value='<%# Eval("Cat_Name") %>' />
                                                                            </td>
                                                                            <td style="width: 30%; background-color: #ffffff;"><%#Eval("pro_Name") %></td>
                                                                            <td style="width: 30%; background-color: #ffffff;">
                                                                                <asp:DropDownList runat="server" ID="ddlSize" Width="100%"></asp:DropDownList>
                                                                            </td>
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
                                                <br />
                                                <div class="clearfix"></div>

                                                <h3>
                                                    <asp:RadioButton ID="rbGroup" runat="server" GroupName="prdSelection" />&nbsp;&nbsp; <b>Alternatively Select Groups</b></h3>
                                                <div class="clearfix"></div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <label>Group Category</label><div class="clearfix"></div>
                                                        <asp:DropDownList ID="ddlGroupCategory" runat="server" Width="100%" OnSelectedIndexChanged="ddlGroupCategory_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <label>Product Group <span class="text-danger">*</span></label><div class="clearfix"></div>
                                                        <asp:DropDownList ID="ddlProductGroup" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                        <asp:Label runat="server" Text="" ID="lblddlProductGroup"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <br />
                                                <div class="col-md-12">
                                                    <div class="col-md-6">
                                                        <label>Font Color </label>
                                                        <telerik:RadColorPicker ID="radFontColor" RenderMode="Lightweight" runat="server" ShowIcon="true" PaletteModes="All" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>Background Color </label>
                                                        <telerik:RadColorPicker ID="radBackColor" RenderMode="Lightweight" runat="server" SelectedColor="#1B7BBD" ShowIcon="true" PaletteModes="All" />
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <br />
                                                <div style="padding-top: 10px; text-align: center;">

                                                    <asp:Button ID="btn_Ok" class="btn btn-primary" type="button" runat="server" OnClick="btn_Ok_Click" Text="OK" />&nbsp;&nbsp;
                                                    <asp:Button ID="btn_Cancel" class="btn btn-primary" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" />&nbsp;&nbsp;
                                                     <asp:Button ID="btn_Clear_pop" class="btn btn-primary" runat="server" Text="Clear" OnClick="btn_clear_Click" />
                                                    <asp:Button ID="btn_copy" OnClick="btn_copy_Click" class="btn btn-primary" runat="server" Text="Copy" />&nbsp;&nbsp;
                                                    <asp:Button ID="btn_paste" OnClick="btn_paste_Click" class="btn btn-primary" runat="server" Text="Paste" />&nbsp;&nbsp;
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
                <asp:PostBackTrigger ControlID="btn_Ok" />
                <asp:PostBackTrigger ControlID="btn_Cancel" />
                <asp:PostBackTrigger ControlID="btn_Clear_pop" />
                <asp:PostBackTrigger ControlID="btn_copy" />
                <asp:PostBackTrigger ControlID="btn_paste" />


            </Triggers>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="up_modeltill" runat="server">
            <ContentTemplate>
                <div class="modal" id="ModalCenterTill" tabindex="-1" role="dialog"
                    aria-labelledby="ModalCenterTitle" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog modal-dialog-centered" role="document" style="width: 800px !important;">
                        <div class="modal-content" style="width: 90%">
                            <div class="modal-body">
                                <div class="panel panel-yellow">
                                    <div class="panel-heading">
                                        <asp:Label ID="lblTillName" runat="server" Text="Keymap List"></asp:Label>
                                    </div>
                                    <div class="panel-body pan">
                                        <div class="form-body pal" style="height: 395px; overflow: scroll;">
                                            <asp:HiddenField ID="hdtillpoptill_id" runat="server" />
                                            <asp:GridView ID="rdKeymap" runat="server" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="True" ShowGroupPanel="False"
                                                AllowFilteringByColumn="false" CssClass="Grid"
                                                EmptyDataText="No data in the data source." ShowHeaderWhenEmpty="True" Width="100%" GridLines="None">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Keymap Name">
                                                        <HeaderStyle Width="50%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkKeymap" runat="server"></asp:CheckBox>
                                                            <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("name")%>' />
                                                            <asp:HiddenField ID="hdKeymap_id" runat="server" Value='<%# Eval("key_map_id")%>' />


                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shorting Number">
                                                        <HeaderStyle Width="50%" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtShortingNum" runat="server" Width="30%"></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>


                                        </div>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div style="padding-top: 10px; text-align: center;">
                                            <asp:Button ID="btnTillPopOk" class="btn btn-primary" runat="server" Text="OK" OnClick="btnTillPopOk_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnTillPopCancel" class="btn btn-primary" runat="server" Text="Cancel" />&nbsp;&nbsp;                                                     
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnTillPopOk" />
                <asp:PostBackTrigger ControlID="btnTillPopCancel" />



            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>

    <style type="text/css">
        tr.group,
        tr.group:hover {
            background-color: #cac9c9 !important;
            ffff;
            der: #111111 1px olid;
        }

        .row th {
            background-color: #ffffff !important;
        }
    </style>
</asp:Content>

